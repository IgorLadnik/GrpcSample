using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GrpcHelperLib.Communication;

namespace GrpcHelperLib
{
    public class ServerGrpcSubscribersBase
    {
        private readonly ConcurrentDictionary<string, SubscribersModel> Subscribers = new();

        protected readonly ILogger _logger;

        public ServerGrpcSubscribersBase(ILoggerFactory loggerFactory) =>
            _logger = loggerFactory.CreateLogger<ServerGrpcSubscribersBase>();

        public void AddSubscriber(SubscribersModel subscriber)
        {
            bool added = Subscribers.TryAdd(subscriber.Id, subscriber);
            _logger.LogInformation($"New subscriber added: '{subscriber.Id}'");
            if (!added)
                _logger.LogInformation($"Could not add subscriber: '{subscriber.Id}'");
        }

        public void RemoveSubscriber(SubscribersModel subscriber)
        {
            try
            {
                Subscribers.TryRemove(subscriber.Id, out SubscribersModel item);
                _logger.LogInformation($"Force Remove: subscriber '{item.Id}' no longer works");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Could not remove subscriber '{subscriber.Id}'");
            }
        }

        public virtual bool SubscribersFilter(SubscribersModel subscriber, ResponseMessage message) =>
            subscriber.Id == message.ClientId;


        public async Task SendMessageAsync(ResponseMessage message)
        {
            foreach (var subscriber in Subscribers.Values)
            {
                var item = await SendMessageToSubscriber(subscriber, message);
                if (item != null)
                    RemoveSubscriber(item);
            }
        }

        private async Task<SubscribersModel> SendMessageToSubscriber(SubscribersModel subscriber, ResponseMessage message)
        {
            try
            {
                if (SubscribersFilter(subscriber, message))
                {
                    _logger.LogInformation($"Sending: message '{message}'");
                    await subscriber.Subscriber.WriteAsync(message);
                    _logger.LogInformation($"Message '{message.MessageId}' has been sent");
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Filure to send message '{message.MessageId}'");
                return subscriber;
            }
        }
    }
}
