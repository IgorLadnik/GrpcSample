using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Google.Protobuf;
using GrpcHelperLib;
using GrpcHelperLib.Communication;
using ModelsLib;

namespace DtoLib
{
    public class MessageProcessor : MessageProcessorBase
    {
        public MessageProcessor(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }

        public override ResponseMessage ProcessRequest(RequestMessage message) 
        {
            var ob = message.Payload.ToObject<IList<Arg1>>();
            Console.WriteLine(JsonConvert.SerializeObject(ob));

            var response = base.ProcessRequest(message);

            response.Payload = ((object)"OK").ToByteString();
            return response;
        }
    }
}
