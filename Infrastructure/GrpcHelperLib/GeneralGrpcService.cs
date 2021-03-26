using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Grpc.Core;
using GrpcHelperLib.Communication;

namespace GrpcHelperLib
{
    public class GeneralGrpcService
    {
        private readonly ServerGrpcSubscribersBase _serverGrpcSubscribers;
        private readonly MessageProcessorBase _messageProcessor;
        private CancellationTokenSource _cts = new();

        protected ILogger _logger;

        public GeneralGrpcService(
            ILoggerFactory loggerFactory, 
            ServerGrpcSubscribersBase serverGrpcSubscribers, 
            MessageProcessorBase messageProcessor)
        {
            _serverGrpcSubscribers = serverGrpcSubscribers;
            _messageProcessor = messageProcessor;
            _logger = loggerFactory.CreateLogger<GeneralGrpcService>();
        }

        public async Task CreateDuplexStreaming(
            IAsyncStreamReader<RequestMessage> requestStream, 
            IServerStreamWriter<ResponseMessage> responseStream, 
            ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            _logger.LogInformation($"Connection id: '{httpContext.Connection.Id}'");

            if (!await requestStream.MoveNext(_cts.Token))
                return;

            var clientId = _messageProcessor.GetClientId(requestStream.Current);
            _logger.LogInformation($"Client '{clientId}' connected");
            SubscribersModel subscriber = new()
            {
                Subscriber = responseStream,
                Id = $"{clientId}"
            };

            _serverGrpcSubscribers.AddSubscriber(subscriber);

            do
            {
                if (requestStream.Current == null)
                    continue;

                var resultMessage = _messageProcessor.Process(requestStream.Current);
                if (resultMessage == null)
                    continue;

                await _serverGrpcSubscribers.SendMessageAsync(resultMessage);
            } while (await requestStream.MoveNext(_cts.Token));

            _serverGrpcSubscribers.RemoveSubscriber(subscriber);
            _logger.LogInformation($"Client '{clientId}' disconnected");
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            _logger.LogInformation("GeneralGrpcService is cleaning up");
            _messageProcessor?.Dispose();
        }
    }
}
