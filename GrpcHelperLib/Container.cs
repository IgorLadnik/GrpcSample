using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrpcHelperLib.Communication;

namespace GrpcHelperLib
{
    public class Container
    {
        private ConcurrentDictionary<string, System.Type> _dctPerCall = new();
        private ConcurrentDictionary<string, object> _dctSingleton = new();

        #region Register 

        // "impl" type should have default ctor!
        public Container RegisterPerCall(System.Type @interface, System.Type impl)
        {
            _dctPerCall[@interface.Name] = impl;
            return this;
        }

        public Container RegisterPerCall<TInteface, TImpl>() where TImpl : TInteface, new()
        {
            RegisterPerCall(typeof(TInteface), typeof(TImpl));
            return this;
        }

        public Container RegisterSingleton(System.Type @interface, object ob)
        {
            _dctSingleton[@interface.Name] = ob;
            return this;
        }

        #endregion // Register 

        #region Resolve 

        public object ResolvePerCall(string interafceName)
        {
            object ob = null;
            if (_dctPerCall.TryGetValue(interafceName, out System.Type type))
                ob = Activator.CreateInstance(type);

            return ob;
        }

        public object ResolveSingleton(string interafceName)
        {
            _dctSingleton.TryGetValue(interafceName, out object ob);
            return ob;
        }

        #endregion // Resolve 

        public virtual object CallMethod(RequestMessage message)
        {
            var obs = (object[])message.Payload.ToObject();
            var interfaceName = $"{obs[0]}";
            var localOb = ResolveSingleton(interfaceName);

            if (localOb == null)
                localOb = ResolvePerCall(interfaceName);

            var methodInfo = localOb?.GetType().GetMethod($"{obs[1]}");
            return methodInfo?.Invoke(localOb, obs.Skip(2).ToArray());
        }

    }
}
