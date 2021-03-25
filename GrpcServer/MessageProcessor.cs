using Microsoft.Extensions.Logging;
using GrpcHelperLib;
using RemoteInterfaces;
using RemoteImplementations;

namespace GrpcServer
{
    public class MessageProcessor : MessageProcessorBase
    {
        public MessageProcessor(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            //Register<IRemoteCall>(new RemoteCall());
            //Register<IRemoteCall1>(new RemoteCall1());

            Register<IRemoteCall, RemoteCall>();
            Register<IRemoteCall1, RemoteCall1>();
        }
    }
}
