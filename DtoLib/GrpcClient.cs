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

        public override ByteString MessagePayload 
        {
            get
            {
                List<Arg1> arg1s = new()
                {
                    new() { Id = "0", Arg2Props = new() { new() { Id = "0.0" }, new() { Id = "0.1" }, } },
                    new() { Id = "1", Arg2Props = new() { new() { Id = "1.0" }, new() { Id = "1.1" }, } },
                };

                ByteString ret;
                using (MemoryStream ms = new())
                {
                    BinaryFormatter bf = new();
                    bf.Serialize(ms, arg1s);
                    ms.Position = 0;
                    ret = ByteString.FromStream(ms);
                }

                using (MemoryStream ms = new(ret.ToByteArray()))
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    BinaryFormatter bf = new();
                    var newArg1s = bf.Deserialize(ms);
                }

                Console.ReadLine();

                return ret;
            }
        }
    }
}
