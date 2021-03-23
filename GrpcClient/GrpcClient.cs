using System;
using System.Text;
using Grpc.Core;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using GrpcHelperLib;
using GrpcHelperLib.Communication;
using GrpcHelperLib.CommunicationClient;

namespace GrpcClient
{
    public class Client : GrpcClientBase
    {
        public Client()
        {
            ClientId = $"{Guid.NewGuid()}";
        }

        public override AsyncDuplexStreamingCall<RequestMessage, ResponseMessage> CreateDuplexClient(Channel channel) =>
            new Messaging.MessagingClient(channel).CreateStreaming();

        public override RequestMessage CreateMessage(ByteString payload)
        {
            var strPayload = Encoding.UTF8.GetString(payload.ToByteArray());

            return new RequestMessage
            {
                ClientId = ClientId,
                MessageId = $"{Guid.NewGuid()}",
                Type = MessageType.Ordinary,
                Time = Timestamp.FromDateTime(DateTime.UtcNow),
                Response = strPayload.Contains('?') ? ResponseType.Required : ResponseType.NotRequired,
                Payload = ByteString.CopyFromUtf8(strPayload)
            };
        }

        public override ByteString MessagePayload => ByteString.CopyFromUtf8(Console.ReadLine());
    }
}
