using RemoteInterfaces;

namespace RemoteImplementations
{
    public class RemoteCall : IRemoteCall
    {
        public int Foo(string name, Arg1[] arg1s)
        {
            return 7;
        }
    }
}
