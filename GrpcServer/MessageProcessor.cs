using System;
using System.Text;
using Microsoft.Extensions.Logging;
using GrpcHelperLib;
using GrpcHelperLib.Communication;

namespace GrpcServer
{
    public class MessageProcessor : MessageProcessorBase<RequestMessage, ResponseMessage>
    {
        public MessageProcessor(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }

        public override string GetClientId(RequestMessage message) => message.ClientId;

        public override ResponseMessage Process(RequestMessage message)
        {
            var strPayload = Encoding.UTF8.GetString(message.Payload.ToByteArray());
            if (string.IsNullOrEmpty(strPayload))
                return null;

            Logger.LogInformation($"To be processed: {strPayload}");

            //
            // Request message processing should be placed here
            //

            if (message.Response != ResponseType.Required)
                return null;
            
            var timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);

            try
            {
                return new ResponseMessage
                {
                    ClientId = message.ClientId,
                    MessageId = message.MessageId,
                    Type = message.Type,
                    Time = timestamp,
                    Payload = $"Response to \"{strPayload}\"",
                    Status = MessageStatus.Processed,
                };
            }
            catch (Exception e)
            {
                return new ResponseMessage
                {
                    ClientId = message.ClientId,
                    MessageId = message.MessageId,
                    Type = message.Type,
                    Time = timestamp,
                    Payload = e.Message,
                    Status = MessageStatus.Error,
                };
            }
        }
    }
}
