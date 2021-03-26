﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using GrpcHelperLib.Communication;

namespace GrpcHelperLib
{
    public class Container : IDisposable
    {
        class Descriptor 
        {
            public Type type;
            public object ob;
            public bool isPerSession = false;
            public ConcurrentDictionary<string, PerSessionDescriptor> dctSession;
        }

        class PerSessionDescriptor
        {
            public object ob;

            private long _lastActivationInTicks;
            public long lastActivationInTicks
            {
                get => Interlocked.Read(ref _lastActivationInTicks);
                set => Interlocked.Exchange(ref _lastActivationInTicks, value);
            }
        }

        private readonly ConcurrentDictionary<string, Descriptor> _dct = new();
        private Timer _timer;

        #region Register 

        #region Register per call and per session

        // "impl" type should have default ctor!
        public Container Register(Type @interface, Type impl, bool isPerSession = false, int sessionLifeTimeInMin = -1)
        {
            _dct[@interface.Name] = new() { type = impl, isPerSession = isPerSession };

            if (isPerSession && sessionLifeTimeInMin > 0)
            {
                var sessionLifeTime = TimeSpan.FromMinutes(sessionLifeTimeInMin);
                _timer = new(_ =>
                {
                    var now = DateTime.UtcNow;
                    foreach (var dict in _dct.Values?.Where(d => d.isPerSession)?.Select(d => d.dctSession))
                    {
                        if (dict == null || dict.Count == 0)
                            continue;

                        foreach (var clientId in dict.Keys.ToArray())
                            if (now - new DateTime(dict[clientId].lastActivationInTicks) > sessionLifeTime)
                                dict.TryRemove(clientId, out PerSessionDescriptor psd);
                    }

                },
                null, TimeSpan.Zero, TimeSpan.FromMinutes(sessionLifeTimeInMin));
            }

            return this;
        }

        public Container Register<TInteface, TImpl>(bool isPerSession = false, int sessionLifeTimeInMin = -1) where TImpl : TInteface, new() =>
            Register(typeof(TInteface), typeof(TImpl), isPerSession, sessionLifeTimeInMin);

        #endregion // Register per call and per session

        #region Register singleton

        public Container Register(Type @interface, object ob)
        {
            _dct[@interface.Name] = new() { ob = ob };
            return this;
        }

        public Container Register<TInteface>(TInteface ob) =>
            Register(typeof(TInteface), ob);

        #endregion // Register singleton 

        #endregion // Register 

        public object Resolve(string interafceName, string clientId = null)
        {
            if (!_dct.TryGetValue(interafceName, out Descriptor descriptor))
                return null;

            if (descriptor.ob != null)
                // Singleton
                return descriptor.ob;

            if (descriptor.type != null) 
            {
                if (!descriptor.isPerSession || string.IsNullOrEmpty(clientId))
                    // Per Call
                    return Activator.CreateInstance(descriptor.type);

                // Per Session
                object ob;
                if (descriptor.dctSession == null)
                    descriptor.dctSession = new();

                if (descriptor.dctSession.TryGetValue(clientId, out PerSessionDescriptor perSessionDescriptor)) 
                {
                    perSessionDescriptor.lastActivationInTicks = DateTime.UtcNow.Ticks;
                    return perSessionDescriptor.ob;
                }

                descriptor.dctSession[clientId] = perSessionDescriptor = new()
                {
                    ob = Activator.CreateInstance(descriptor.type),
                    lastActivationInTicks = DateTime.UtcNow.Ticks,
                };

                return perSessionDescriptor.ob;
            }

            return null;
        }

        public virtual object CallMethod(RequestMessage message)
        {
            if (DeleteSessionIfRequested(message))
                return null;

            var obs = (object[])message.Payload.ToObject();
            var interfaceName = $"{obs[0]}";
            var methodName = $"{obs[1]}";

            var localOb = Resolve(interfaceName, message.ClientId);
            if (localOb == null)
                return null;

            var methodAgrs = obs.Skip(2).ToArray();
            var callDirect = localOb as ICallDirect;
            if (callDirect != null)
                return callDirect.Call(methodName, methodAgrs);
            else
            {
                var methodInfo = localOb?.GetType().GetMethod(methodName);
                return methodInfo?.Invoke(localOb, methodAgrs);
            }
        }

        private bool DeleteSessionIfRequested(RequestMessage message) 
        {
            var obs = (object[])message.Payload.ToObject();
            var methodName = $"{obs[1]}";
            var interfaceName = $"{obs[0]}";

            if (methodName != Ex.deleteSession ||
                !_dct.TryGetValue(interfaceName, out Descriptor descriptor) || 
                !descriptor.isPerSession)
                    return false;

            var clientIdToBeDeleted = descriptor.dctSession.Keys.Where(k => k == message.ClientId)?.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(clientIdToBeDeleted))
                return false;

            return descriptor.dctSession.TryRemove(clientIdToBeDeleted, out PerSessionDescriptor psd);
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
