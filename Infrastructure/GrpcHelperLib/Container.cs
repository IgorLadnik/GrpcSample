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
        }

        private ConcurrentDictionary<string, Descriptor> _dct = new();

        #region Register 

        #region RegisterPerCall

        // "impl" type should have default ctor!
        public Container Register(Type @interface, Type impl)
        {
            _dct[@interface.Name] = new() { type = impl };
            return this;
        }

        public Container Register<TInteface, TImpl>() where TImpl : TInteface, new() =>
            Register(typeof(TInteface), typeof(TImpl));

        #endregion // RegisterPerCall 

        #region RegisterSingleton

        public Container Register(Type @interface, object ob)
        {
            _dct[@interface.Name] = new() { ob = ob };
            return this;
        }

        public Container Register<TInteface>(TInteface ob) =>
            Register(typeof(TInteface), ob);

        #endregion // RegisterSingleton 

        #endregion // Register 

        public object Resolve(string interafceName)
        {
            if (!_dct.TryGetValue(interafceName, out Descriptor descriptor))
                return null;

            if (descriptor.ob != null)
                return descriptor.ob;

            if (descriptor.type != null)
                return Activator.CreateInstance(descriptor.type);

            return null;
        }

        public virtual object CallMethod(RequestMessage message)
        {
            var obs = (object[])message.Payload.ToObject();
            var interfaceName = $"{obs[0]}";
            var methodName = $"{obs[1]}";
            var localOb = Resolve(interfaceName);
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
