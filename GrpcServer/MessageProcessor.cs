using System.Text;
using Microsoft.Extensions.Logging;
using Google.Protobuf;
using GrpcHelperLib;
using GrpcHelperLib.Communication;

namespace GrpcServer
{
    public class MessageProcessor : MessageProcessorBase
    {
        public MessageProcessor(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }

        public override ResponseMessage ProcessRequest(RequestMessage message) 
        {
            var response = base.ProcessRequest(message);
            response.Payload = message.Payload;
            return response;
        }
    }
}
