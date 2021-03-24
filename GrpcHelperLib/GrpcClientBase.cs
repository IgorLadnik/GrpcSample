using System;
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

        public Task SendAsync(params object[] obs) =>
            Task.Run(async () =>
            {
                try
                {
                    await _duplex.RequestStream.WriteAsync(CreateMessage(ToByteString(obs)));
                }
                catch (Exception e)
                {
                    // Log error
                }
            });

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
                    try
                    {
                        onReceive(_duplex.ResponseStream.Current);
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


