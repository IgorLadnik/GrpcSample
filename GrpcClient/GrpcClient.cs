using System;
using Grpc.Core;
using GrpcHelperLib;
using Google.Protobuf.WellKnownTypes;
using System.Text;
using Google.Protobuf;
using Communication;
using C = CommunicationClient;

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
            new C.Messaging.MessagingClient(channel).CreateStreaming();

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
