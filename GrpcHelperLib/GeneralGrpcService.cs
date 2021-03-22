﻿using System.Threading;
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

        protected ILogger Logger { get; set; }

        public GeneralGrpcService(
            ILoggerFactory loggerFactory, 
            ServerGrpcSubscribersBase serverGrpcSubscribers, 
            MessageProcessorBase messageProcessor)
        {
            _serverGrpcSubscribers = serverGrpcSubscribers;
            _messageProcessor = messageProcessor;
            Logger = loggerFactory.CreateLogger<GeneralGrpcService>();
        }

        public async Task CreateDuplexStreaming(
            IAsyncStreamReader<RequestMessage> requestStream, 
            IServerStreamWriter<ResponseMessage> responseStream, 
            ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            Logger.LogInformation($"Connection id: {httpContext.Connection.Id}");

            CancellationTokenSource cancellationTokenSource = new(); //??

            if (!await requestStream.MoveNext(cancellationTokenSource.Token))
                return;

            var clientId = _messageProcessor.GetClientId(requestStream.Current);
            Logger.LogInformation($"{clientId} connected");
            var subscriber = new SubscribersModel
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

                await _serverGrpcSubscribers.BroadcastMessageAsync(resultMessage);
            } while (await requestStream.MoveNext(cancellationTokenSource.Token));

            _serverGrpcSubscribers.RemoveSubscriber(subscriber);
            Logger.LogInformation($"{clientId} disconnected");
        }

        public void Dispose()
        {
            Logger.LogInformation("Cleaning up");
        }
    }
}
