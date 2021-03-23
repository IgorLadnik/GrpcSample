using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using GrpcHelperLib.Communication;

namespace GrpcHelperLib
{
    public abstract class GrpcClientBase
    {
        public string ClientId { get; protected set; }

        public abstract AsyncDuplexStreamingCall<RequestMessage, ResponseMessage> CreateDuplexClient(Channel channel);

        public abstract RequestMessage CreateMessage(ByteString ob);

        public abstract ByteString MessagePayload { get; }

        public async Task<GrpcClientBase> Start(string url, string pathCertificate, Action<ResponseMessage> onReceive, Action onConnection = null, Action onShuttingDown = null)
        {
            Channel channel = new(url, string.IsNullOrEmpty(pathCertificate) ? ChannelCredentials.Insecure : new SslCredentials(File.ReadAllText(pathCertificate)));
    
            using var duplex = CreateDuplexClient(channel);
            onConnection?.Invoke();

            var responseTask = Task.Run(async () =>
            {
                while (await duplex.ResponseStream.MoveNext(CancellationToken.None))
                {
                    try
                    {
                        onReceive(duplex.ResponseStream.Current);
                    }
                    catch (Exception e) 
                    {
                        // Log error
                    }
                }
            });

            ByteString payload;
            while ((payload = MessagePayload) != null)
            {
                try
                {
                    await duplex.RequestStream.WriteAsync(CreateMessage(payload));
                }
                catch (Exception e)
                {
                    // Log error
                }
            }

            await duplex.RequestStream.CompleteAsync();

            onShuttingDown?.Invoke();
            await channel.ShutdownAsync();
            
            return this;
        }
    }
}


