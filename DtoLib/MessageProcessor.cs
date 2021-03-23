using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Extensions.Logging;
using Google.Protobuf;
using GrpcHelperLib;
using GrpcHelperLib.Communication;

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
            var response = base.ProcessRequest(message);

            using MemoryStream ms = new(message.Payload.ToByteArray());
            ms.Seek(0, SeekOrigin.Begin);
            BinaryFormatter bf = new();
            var arg1s = bf.Deserialize(ms);
            
            response.Payload = message.Payload;
            return response;
        }
    }
}
