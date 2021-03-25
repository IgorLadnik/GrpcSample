using GrpcHelperLib;
using RemoteInterfaces;

namespace RemoteImplementations
{
    public class RemoteCall : IRemoteCall
    {
        public int Foo(string name, Arg1[] arg1s)
        {
            return 7;
        }

        public string Echo(string text) => $"Echo: {text}";
    }

    public class RemoteCall1 : IRemoteCall1, ICallDirect
    {
        private static int objectsCount = 0;

        private int _id; 

        public RemoteCall1()
        {
            _id = ++objectsCount;
        }

        public int Foo(string name, Arg1[] arg1s)
        {
            return _id;
        }

        public string Echo(string text) => $"Echo1: {text}";

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
