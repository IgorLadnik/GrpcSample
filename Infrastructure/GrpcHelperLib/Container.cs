using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Text;
using Microsoft.Extensions.Logging;
using GrpcHelperLib.Communication;

namespace GrpcHelperLib
{
    public class Container : IDisposable
    {
        #region Internal descriptors classes

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

        #endregion // Internal descriptors classes

        private readonly ConcurrentDictionary<string, Descriptor> _dctInterface = new();
        private Timer _timer;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;

        protected Container(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<Container>();
        }

        #region Register 

        #region Register per call and per session

        // "impl" type should have default ctor!
        public Container Register(Type @interface, Type impl, bool isPerSession = false, int sessionLifeTimeInMin = -1)
        {
            _logger.LogInformation($"About to register interface '{@interface.Name}' with type '{impl.Name}', isPerSession = {isPerSession}, sessionLifeTimeInMin = {sessionLifeTimeInMin}");
            _dctInterface[@interface.Name] = new() { type = impl, isPerSession = isPerSession };

            if (isPerSession && sessionLifeTimeInMin > 0)
            {
                var sessionLifeTime = TimeSpan.FromMinutes(sessionLifeTimeInMin);
                _timer = new(_ =>
                {
                    var now = DateTime.UtcNow;
                    foreach (var dict in _dctInterface.Values?.Where(d => d.isPerSession)?.Select(d => d.dctSession))
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

            _logger.LogInformation($"Registered interface '{@interface.Name}' with type '{impl.Name}', isPerSession = {isPerSession}, sessionLifeTimeInMin = {sessionLifeTimeInMin}");
            return this;
        }

        public Container Register<TInteface, TImpl>(bool isPerSession = false, int sessionLifeTimeInMin = -1) where TImpl : TInteface, new() =>
            Register(typeof(TInteface), typeof(TImpl), isPerSession, sessionLifeTimeInMin);

        #endregion // Register per call and per session

        #region Register singleton

        public Container Register(Type @interface, object ob)
        {
            _dctInterface[@interface.Name] = new() { ob = AssignLoggerIfSupported(ob) };
            _logger.LogInformation($"Registered interface '{@interface.Name}' as singleton, type '{ob.GetType().Name}'");
            return this;
        }

        public Container Register<TInteface>(TInteface ob) =>
            Register(typeof(TInteface), ob);

        #endregion // Register singleton 

        #endregion // Register 

        #region Resolve & call methods

        private object Resolve(string interafceName, string clientId = null)
        {
            if (!_dctInterface.TryGetValue(interafceName, out Descriptor descriptor))
                return null;

            if (descriptor.ob != null)
                // Singleton
                return descriptor.ob;

            if (descriptor.type != null) 
            {
                if (!descriptor.isPerSession || string.IsNullOrEmpty(clientId))
                    // Per Call
                    return CreateInstanceWithLoggerIfSupported(descriptor.type);

                // Per Session
                if (descriptor.dctSession == null)
                    descriptor.dctSession = new();

                if (descriptor.dctSession.TryGetValue(clientId, out PerSessionDescriptor perSessionDescriptor)) 
                {
                    perSessionDescriptor.lastActivationInTicks = DateTime.UtcNow.Ticks;
                    return perSessionDescriptor.ob;
                }

                descriptor.dctSession[clientId] = perSessionDescriptor = new()
                {
                    ob = CreateInstanceWithLoggerIfSupported(descriptor.type),
                    lastActivationInTicks = DateTime.UtcNow.Ticks,
                };

                return perSessionDescriptor.ob;
            }

            return null;
        }

        private object CreateInstanceWithLoggerIfSupported(Type type) =>
            AssignLoggerIfSupported(Activator.CreateInstance(type));

        private object AssignLoggerIfSupported(object ob)
        {
            var log = ob as ILog;
            if (log != null)
                log.LoggerFactory = _loggerFactory;
            return ob;
        }

        public virtual object CallMethod(RequestMessage requestMessage)
        {
            var obs = requestMessage.Payload.ToArrayOfObjects();
            var interfaceName = $"{obs[0]}";
            var methodName = $"{obs[1]}";

            var localOb = Resolve(interfaceName, requestMessage.ClientId);
            if (localOb == null)
                return null;

            var methodAgrs = obs.Skip(2).ToArray();
            var directCall = localOb as IDirectCall;
            object retOb = null;
            if (directCall != null)
            {
                _logger.LogInformation($"Calling method '{methodName}()' of interface '{interfaceName}' - direct call");
                retOb = directCall.DirectCall(methodName, methodAgrs);
                _logger.LogInformation($"Called method '{methodName}()' of interface '{interfaceName}' - call with reflection");
            }
            else
            {
                _logger.LogInformation($"Calling method '{methodName}()' of interface '{interfaceName}' - call with reflection");
                var methodInfo = localOb?.GetType().GetMethod(methodName);
                retOb = methodInfo?.Invoke(localOb, methodAgrs);
                _logger.LogInformation($"Called method '{methodName}()' of interface '{interfaceName}' - call with reflection");
            }

            return retOb;
        }

        protected bool DeleteSessionIfRequested(RequestMessage requestMessage) 
        {
            var obs = requestMessage.Payload.ToArrayOfObjects();
            var methodName = $"{obs[1]}";
            var interfaceName = $"{obs[0]}";

            if (methodName != Ex.deleteSession)
                return false;

            StringBuilder sb = new();
            foreach (var k in _dctInterface.Keys)
            {
                var descriptor = _dctInterface[k];
                if (descriptor.isPerSession &&
                    descriptor.dctSession.TryRemove(requestMessage.ClientId, out PerSessionDescriptor psd))
                        sb.Append($"'{k}', ");
            }

            var tempStr = sb.ToString().Substring(0, sb.Length - 2);
            _logger.LogInformation($"Sessions for client '{requestMessage.ClientId}' have been deleted for interfaces {tempStr}");
            return true;
        }

        #endregion // Resolve & call methods

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
