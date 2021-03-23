using Microsoft.Extensions.Logging;
using GrpcHelperLib;
using DtoLib;

namespace GrpcServer
{
    public class DuplexService : DuplexServiceBase
    {
        public DuplexService(ILoggerFactory loggerFactory, ServerGrpcSubscribers serverGrpcSubscribers, MessageProcessor messageProcessor)
            : base(loggerFactory, serverGrpcSubscribers, messageProcessor) 
        {
        }
    }
}
