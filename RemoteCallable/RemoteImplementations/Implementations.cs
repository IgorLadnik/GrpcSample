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
        public static IRemoteCall1 Factory() =>
            new RemoteCall1();

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

        public int Foo(string name, Arg1[] arg1s)
        {
            return 1;
        }

        public string Echo(string text) => $"Echo1: {text}";
    }
}
