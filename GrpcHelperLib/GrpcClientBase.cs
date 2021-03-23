﻿using System;
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
        public abstract AsyncDuplexStreamingCall<RequestMessage, ResponseMessage> CreateDuplexClient(Channel channel);

        public abstract RequestMessage CreateMessage(ByteString ob);

        public abstract ByteString MessagePayload { get; }

        public async Task<GrpcClientBase> Do(string url, string pathCertificate, Action<ResponseMessage> onReceive, Action onConnection = null, Action onShuttingDown = null)
        {
            var channel = new Channel(url, string.IsNullOrEmpty(pathCertificate) ? ChannelCredentials.Insecure : new SslCredentials(File.ReadAllText(pathCertificate)));
    
            using var duplex = CreateDuplexClient(channel);
            onConnection?.Invoke();

            var responseTask = Task.Run(async () =>
            {
                while (await duplex.ResponseStream.MoveNext(CancellationToken.None))
                    onReceive(duplex.ResponseStream.Current);
            });

            ByteString ob;
            while ((ob = MessagePayload) != null)
                await duplex.RequestStream.WriteAsync(CreateMessage(ob));

            await duplex.RequestStream.CompleteAsync();

            onShuttingDown?.Invoke();
            await channel.ShutdownAsync();
            
            return this;
        }
    }
}


