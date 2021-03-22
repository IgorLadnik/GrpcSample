using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using GrpcHelperLib.Communication;

namespace GrpcHelperLib
{
    public abstract class GrpcClientBase
    {
        public abstract AsyncDuplexStreamingCall<RequestMessage, ResponseMessage> CreateDuplexClient(Channel channel);

        public abstract RequestMessage CreateMessage(ByteString ob);

        public abstract ByteString MessagePayload { get; }

        public async Task Do(Channel channel, Action onConnection = null, Action onShuttingDown = null)
        {
            using var duplex = CreateDuplexClient(channel);
            onConnection?.Invoke();

            var responseTask = Task.Run(async () =>
            {
                while (await duplex.ResponseStream.MoveNext(CancellationToken.None))
                    Console.WriteLine(Encoding.UTF8.GetString(duplex.ResponseStream.Current.Payload.ToByteArray())); //??
            });

            ByteString ob;
            while ((ob = MessagePayload) != null)
                await duplex.RequestStream.WriteAsync(CreateMessage(ob));

            await duplex.RequestStream.CompleteAsync();

            onShuttingDown?.Invoke();
            await channel.ShutdownAsync();
        }
    }
}


