using System;
using System.Collections.Concurrent;
using System.Linq;
using GrpcHelperLib.Communication;

namespace GrpcHelperLib
{
    public class Container
    {
        class Descriptor 
        {
            public Type type;
            public object ob;
            public bool isPerSession = false;
            public ConcurrentDictionary<string, object> dctSession;
        }

        private ConcurrentDictionary<string, Descriptor> _dct = new();

        #region Register 

        #region Register per call

        // "impl" type should have default ctor!
        public Container Register(Type @interface, Type impl, bool isPerSession = false)
        {
            _dct[@interface.Name] = new() { type = impl, isPerSession = isPerSession };
            return this;
        }

        public Container Register<TInteface, TImpl>(bool isPerSession = false) where TImpl : TInteface, new() =>
            Register(typeof(TInteface), typeof(TImpl), isPerSession);

        #endregion // Register per call 

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

                if (descriptor.dctSession.TryGetValue(clientId, out ob))
                    return ob;

                descriptor.dctSession[clientId] = ob = Activator.CreateInstance(descriptor.type);

                return ob;
            }

            return null;
        }

        public virtual object CallMethod(RequestMessage message)
        {
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

    }
}
