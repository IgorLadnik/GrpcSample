using Microsoft.Extensions.Logging;
using GrpcHelperLib;

namespace GrpcServer
{
    public class MessageProcessor : MessageProcessorBase
    {
        public MessageProcessor(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }
    }
}
