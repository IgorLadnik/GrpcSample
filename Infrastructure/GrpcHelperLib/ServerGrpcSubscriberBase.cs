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

        private ILogger Logger { get; set; }

        public ServerGrpcSubscribersBase(ILoggerFactory loggerFactory) =>
            Logger = loggerFactory.CreateLogger<ServerGrpcSubscribersBase>();

        public void AddSubscriber(SubscribersModel subscriber)
        {
            bool added = Subscribers.TryAdd(subscriber.Id, subscriber);
            Logger.LogInformation($"New subscriber added: {subscriber.Id}");
            if (!added)
                Logger.LogInformation($"could not add subscriber: {subscriber.Id}");
        }

        public void RemoveSubscriber(SubscribersModel subscriber)
        {
            try
            {
                Subscribers.TryRemove(subscriber.Id, out SubscribersModel item);
                Logger.LogInformation($"Force Remove: {item.Id} - no longer works");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Could not remove {subscriber.Id}");
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
                    //Logger.LogInformation($"Sending: {message}");
                    await subscriber.Subscriber.WriteAsync(message);
                }

                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Could not send");
                return subscriber;
            }
        }
    }
}
