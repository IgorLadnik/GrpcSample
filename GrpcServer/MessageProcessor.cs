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
            // Singleton
            //Register<IRemoteCall1>(new RemoteCall1());
            //Register<IRemoteCall2>(new RemoteCall2());

            // Per Session
            Register<IRemoteCall1, RemoteCall1>(true);
            Register<IRemoteCall2, RemoteCall2>(true);
        }
    }
}
