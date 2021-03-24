using Microsoft.Extensions.Logging;
using GrpcHelperLib;
using ModelsLib;

namespace GrpcServer
{
    public class MessageProcessor : MessageProcessorBase
    {
        public MessageProcessor(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            RegisterPerCall<IRemoteCall, RemoteCall>();
        }
    }
}
