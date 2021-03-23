using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Grpc.Core;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using GrpcHelperLib;
using GrpcHelperLib.Communication;
using GrpcHelperLib.CommunicationClient;
using ModelsLib;

namespace DtoLib
{
    public class Client : GrpcClientBase
    {
        public Client()
        {
            ClientId = $"{Guid.NewGuid()}";
        }

        public override AsyncDuplexStreamingCall<RequestMessage, ResponseMessage> CreateDuplexClient(Channel channel) =>
            new Messaging.MessagingClient(channel).CreateStreaming();

        public override RequestMessage CreateMessage(ByteString payload) =>
            new()
            {
                ClientId = ClientId,
                MessageId = $"{Guid.NewGuid()}",
                Type = MessageType.Ordinary,
                Time = Timestamp.FromDateTime(DateTime.UtcNow),
                Response = ResponseType.Required,
                Payload = payload
            };

    }
}
