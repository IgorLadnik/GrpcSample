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
            RegisterSingleton<IRemoteCall>(new RemoteCall());
            //RegisterPerCall<IRemoteCall, RemoteCall>();

            RegisterSingleton<IRemoteCall1>(new RemoteCall1());
        }
    }
}
