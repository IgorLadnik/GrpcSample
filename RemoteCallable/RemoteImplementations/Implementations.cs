using System;
using GrpcHelperLib;
using RemoteInterfaces;

namespace RemoteImplementations
{
    public class RemoteCall1 : IRemoteCall1
    {
        public int Foo(string name, Arg1[] arg1s)
        {
            Console.WriteLine("*** RemoteCall1.Foo()");
            return 111;
        }

        public string Echo(string text) => $"Echo: {text}";
    }

    public class RemoteCall2 : IRemoteCall2, ICallDirect
    {
        private static int objectsCount = 0;

        private int _id; 

        public RemoteCall2()
        {
            _id = ++objectsCount;
            Console.WriteLine($"    *** RemoteCall2.Ctor() -> {_id}");
        }

        #region IRemoteCall2 implementation

        public int Foo(string name, Arg1[] arg1s)
        {
            Console.WriteLine($"    *** RemoteCall2.Foo() -> {_id}");
            return _id;
        }

        public string Echo(string text) => $"Echo1: {text}";

        #endregion // IRemoteCall2 implementation

        public object Call(string methodName, params object[] args)
        {
            switch (methodName)
            {
                case "Foo":
                    return Foo((string)args[0], (Arg1[])args[1]);

                case "Echo":
                    return Echo((string)args[0]);
            }

            return null;
        }
    }
}
