using System;
using System.Collections.Concurrent;
using System.Text;
using Microsoft.Extensions.Logging;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using GrpcHelperLib.Communication;
using System.Linq;

namespace GrpcHelperLib
{
    public class MessageProcessorBase : Container
    {
        protected readonly ILogger _logger;

        public MessageProcessorBase(ILoggerFactory loggerFactory)
            : base(loggerFactory) =>
            _logger = loggerFactory.CreateLogger<MessageProcessorBase>();

        public string GetClientId(RequestMessage message) => message.ClientId;

        public virtual ResponseMessage ProcessRequest(RequestMessage message)
        {
            var result = CallMethod(message);
            ResponseMessage response = new()
            {
                ClientId = message.ClientId,
                MessageId = message.MessageId,
                Type = message.Type,
                Time = Timestamp.FromDateTime(DateTime.UtcNow),
                Status = MessageStatus.Processed,
                Payload = result.ToByteString()
            };

            return response;
        }


        public ResponseMessage Process(RequestMessage message)
        {
            var strPayload = Encoding.UTF8.GetString(message.Payload.ToByteArray());
            if (string.IsNullOrEmpty(strPayload))
                return null;


            // Request message processing should be placed here

            ResponseMessage responseMessage;

            try
            {
                _logger.LogInformation($"Message '{message.MessageId}' to be processed");
                responseMessage = ProcessRequest(message);
                _logger.LogInformation($"Message '{message.MessageId}' has been processed");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Could not process message '{message.MessageId}'");
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
                _logger.LogInformation($"Response on message '{message.MessageId}' generated");
                return responseMessage;
            }

            return null;
        }
    }
}

