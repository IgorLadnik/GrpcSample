using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using GrpcHelperLib.Communication;

namespace GrpcHelperLib
{
    public class MessageProcessorBase
    {
        protected ILogger Logger { get; set; }

        public MessageProcessorBase(ILoggerFactory loggerFactory) =>
            Logger = loggerFactory.CreateLogger<MessageProcessorBase>();

        public string GetClientId(RequestMessage message) => message.ClientId;

        public virtual ResponseMessage ProcessRequest(RequestMessage message) =>
            new ResponseMessage
            {
                ClientId = message.ClientId,
                MessageId = message.MessageId,
                Type = message.Type,
                Time = Timestamp.FromDateTime(DateTime.UtcNow),
                Status = MessageStatus.Processed,
            };

        public ResponseMessage Process(RequestMessage message)
        {
            var strPayload = Encoding.UTF8.GetString(message.Payload.ToByteArray());
            if (string.IsNullOrEmpty(strPayload))
                return null;

            //Logger.LogInformation($"To be processed: {strPayload}");

            // Request message processing should be placed here

            ResponseMessage responseMessage;

            try
            {
                responseMessage = ProcessRequest(message);
            }
            catch (Exception e) 
            {
                return new ResponseMessage
                {
                    ClientId = message.ClientId,
                    MessageId = message.MessageId,
                    Type = message.Type,
                    Time = Timestamp.FromDateTime(DateTime.UtcNow),
                    Payload = ByteString.CopyFromUtf8(e.Message),
                    Status = MessageStatus.Error,
                };
            }

            if (message.Response == ResponseType.Required && responseMessage != null) 
            {
                responseMessage.Status = MessageStatus.Processed;
                responseMessage.Time = Timestamp.FromDateTime(DateTime.UtcNow);
                return responseMessage;
            }

            return null;
        }
    }
}

