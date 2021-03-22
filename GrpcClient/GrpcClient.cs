using System;
using Grpc.Core;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using GrpcHelperLib;
using GrpcHelperLib.Communication;
using GrpcHelperLib.CommunicationClient;

namespace GrpcClient
{
    public class Client : GrpcClientBase<RequestMessage, ResponseMessage>
    {
        public string ClientId { get; }

        public Client()
        {
            ClientId = $"{Guid.NewGuid()}";
        }

        public override AsyncDuplexStreamingCall<RequestMessage, ResponseMessage> CreateDuplexClient(Channel channel) =>
            new Messaging.MessagingClient(channel).CreateStreaming();

        public override RequestMessage CreateMessage(object ob)
        {
            var payload = $"{ob}";

            return new RequestMessage
            {
                ClientId = ClientId,
                MessageId = $"{Guid.NewGuid()}",
                Type = MessageType.Ordinary,
                Time = Timestamp.FromDateTime(DateTime.UtcNow),
                Response = payload.Contains('?') ? ResponseType.Required : ResponseType.NotRequired,
                Payload = ByteString.CopyFromUtf8(payload)
            };
        }

        public override string MessagePayload
        {
            get => Console.ReadLine();
        }
    }
}
