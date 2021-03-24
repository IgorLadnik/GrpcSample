using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcHelperLib.Communication;
using GrpcHelperLib.CommunicationClient;

namespace GrpcHelperLib
{
    public class GrpcClientBase : IDisposable
    {
        private ConcurrentDictionary<string, AutoResetEvent> _dctEv = new();
        private ConcurrentDictionary<string, ResponseMessage> _dctResult = new();

        public string ClientId { get; set; }

        public virtual AsyncDuplexStreamingCall<RequestMessage, ResponseMessage> CreateDuplexClient(Channel channel) =>
             new Messaging.MessagingClient(channel).CreateStreaming();

        public virtual RequestMessage CreateMessage(ByteString payload) =>
            new()
            {
                ClientId = ClientId,
                MessageId = $"{Guid.NewGuid()}",
                Type = MessageType.Ordinary,
                Time = Timestamp.FromDateTime(DateTime.UtcNow),
                Response = ResponseType.Required,
                Payload = payload
            };

        public virtual ByteString ToByteString(object ob) => ob.ToByteString();

        public virtual object ToObject(ByteString bs) => bs.ToObject();

        private AsyncDuplexStreamingCall<RequestMessage, ResponseMessage> _duplex;
        private Channel _channel;
        private Action _onShuttingDown;

        public Task<string> SendAsync(params object[] obs) =>
            Task.Run(async () =>
            {
                try
                {
                    var message = CreateMessage(ToByteString(obs));
                    await _duplex.RequestStream.WriteAsync(message);
                    return message.MessageId;
                }
                catch (Exception e)
                {
                    // Log error
                    return null;
                }
            });

        public Task<object> RemoteCallAsync(params object[] obs) =>
            Task.Run(async () =>
            {
                AutoResetEvent ev = new(false);
                var messageId = await SendAsync(obs);
                if (string.IsNullOrEmpty(messageId))
                    return null;

                _dctEv[messageId] = ev;
                ev.WaitOne();

                if (!_dctResult.TryRemove(messageId, out ResponseMessage responseMessage))
                    return null;

                return responseMessage.Payload.ToObject();
            });

        private bool OnReceive(ResponseMessage responseMessage) 
        {
            var messageId = responseMessage.MessageId;
            if (!_dctEv.TryRemove(messageId, out AutoResetEvent ev))
                return false;

            _dctResult[messageId] = responseMessage;
            ev.Set();
            return true;
        }

        public async Task<GrpcClientBase> Start(string url, string pathCertificate, Action<ResponseMessage> onReceive, Action onConnection = null, Action onShuttingDown = null)
        {
            _onShuttingDown = onShuttingDown;

            _channel = new(url, string.IsNullOrEmpty(pathCertificate) ? ChannelCredentials.Insecure : new SslCredentials(File.ReadAllText(pathCertificate)));  
            _duplex = CreateDuplexClient(_channel);
            
            onConnection?.Invoke();

            var responseTask = Task.Run(async () =>
            {
                while (await _duplex.ResponseStream.MoveNext(CancellationToken.None))
                {
                    var responseMessage = _duplex.ResponseStream.Current;
                    try
                    {
                        if (!OnReceive(responseMessage))
                            onReceive(responseMessage);
                    }
                    catch (Exception e) 
                    {
                        // Log error
                    }
                }
            });

            return this;
        }

        public void Dispose()
        {
            if (_duplex != null) 
            {
                _duplex.RequestStream.CompleteAsync();
                _onShuttingDown?.Invoke();
                _channel?.ShutdownAsync().Wait();
                _duplex?.Dispose();
            }
        }
    }
}


