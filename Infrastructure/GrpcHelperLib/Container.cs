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
        private ConcurrentDictionary<string, Type> _dctPerCall = new();
        private ConcurrentDictionary<string, object> _dctSingleton = new();

        #region Register 

        #region RegisterPerCall

        // "impl" type should have default ctor!
        public Container Register(Type @interface, Type impl)
        {
            _dctPerCall[@interface.Name] = impl;
            return this;
        }

        public Container Register<TInteface, TImpl>() where TImpl : TInteface, new() =>
            Register(typeof(TInteface), typeof(TImpl));

        #endregion // RegisterPerCall 

        #region RegisterSingleton

        public Container Register(Type @interface, object ob)
        {
            _dctSingleton[@interface.Name] = ob;
            return this;
        }

        public Container Register<TInteface>(TInteface ob) =>
            Register(typeof(TInteface), ob);

        #endregion // RegisterSingleton 

        #endregion // Register 

        public object Resolve(string interafceName)
        {
            if (_dctSingleton.TryGetValue(interafceName, out object ob))
                return ob;

            if (_dctPerCall.TryGetValue(interafceName, out Type type))
                ob = Activator.CreateInstance(type);

            return ob;
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
