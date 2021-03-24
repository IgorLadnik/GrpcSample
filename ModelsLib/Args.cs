//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;

//namespace ModelsLib
//{
//    [Serializable]
//    public class Arg1
//    {
//        public string Id { get; set; }
//        public List<Arg2> Arg2Props { get; set; }
//    }

//    [Serializable]
//    public class Arg2
//    {
//        public string Id { get; set; }
//    }

//    public interface IRemoteCall 
//    {
//        int Foo(string name, Arg1[] arg1s);
//    }

//    public class RemoteCall : IRemoteCall
//    {
//        public int Foo(string name, Arg1[] arg1s) 
//        {
//            return 7;
//        }
//    }

//    //public static class Container 
//    //{
//    //    private static ConcurrentDictionary<Type, Type> _dctPerCall = new();

//    //    // "impl" type should have default ctor!
//    //    public static void RegisterPerCall(Type @interface, Type impl) =>
//    //        _dctPerCall[@interface] = impl;

//    //    // "impl" type should have default ctor!
//    //    public static void RegisterPerCall<TInteface, TImpl>() where TImpl : TInteface, new() =>
//    //        _dctPerCall[typeof(TInteface)] = typeof(TImpl);

//    //    public static object ResolvePerCall(Type @interface) 
//    //    {
//    //        object ob = null;
//    //        if (_dctPerCall.TryGetValue(@interface, out Type type))
//    //            ob = Activator.CreateInstance(type);

//    //        return ob;
//    //    }

//    //    public static TInteface ResolvePerCall<TInteface>() =>
//    //        (TInteface)ResolvePerCall(typeof(TInteface));
//    //}
//}
