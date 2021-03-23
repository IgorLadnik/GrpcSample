using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using GrpcHelperLib.Communication;

namespace GrpcHelperLib
{
    public abstract class GrpcClientBase : IDisposable
    {
        public string ClientId { get; protected set; }

        public abstract AsyncDuplexStreamingCall<RequestMessage, ResponseMessage> CreateDuplexClient(Channel channel);

        public abstract RequestMessage CreateMessage(ByteString ob);

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


