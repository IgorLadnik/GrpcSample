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

        public virtual ResponseMessage ProcessRequest(RequestMessage requestMessage)
        {
            if (DeleteSessionIfRequested(requestMessage))
                return null;

            var result = CallMethod(requestMessage);
            ResponseMessage response = new()
            {
                ClientId = requestMessage.ClientId,
                MessageId = requestMessage.MessageId,
                Type = requestMessage.Type,
                Time = Timestamp.FromDateTime(DateTime.UtcNow),
                Status = MessageStatus.Processed,
                Payload = result.ToByteString()
            };

            return response;
        }

        public ResponseMessage Process(RequestMessage message)
        {
            ResponseMessage responseMessage = null;
            try
            {
                if (CheckRequest(message))
                {
                    _logger.LogInformation($"Message '{message.MessageId}' has been processed");

                    responseMessage = ProcessRequest(message);

                    _logger.LogInformation($"Message '{message.MessageId}' has been processed");
                }
                else
                    _logger.LogError($"Message '{message?.MessageId}' failed to pass check");
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

        public virtual bool CheckRequest(RequestMessage requestMessage) =>
            requestMessage.Payload.ToArrayOfObjects().CheckArgs();
    }
}

