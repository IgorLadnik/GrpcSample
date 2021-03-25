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
            //RegisterSingleton<IRemoteCall>(new RemoteCall());
            //RegisterSingleton<IRemoteCall1>(new RemoteCall1());

            RegisterPerCall<IRemoteCall, RemoteCall>();
            RegisterPerCall<IRemoteCall1, RemoteCall1>();
        }
    }
}
