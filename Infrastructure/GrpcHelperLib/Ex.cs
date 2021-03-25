using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Hosting;
using Grpc.AspNetCore.Server;
using Google.Protobuf;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GrpcHelperLib
{
    public static class Ex
    {
        public static IGrpcServerBuilder AddGrpcHelper(this IServiceCollection services) 
        {
            services.AddSingleton<ServerGrpcSubscribersBase>();
            services.AddSingleton<MessageProcessorBase>();

            return services.AddGrpc();
        }

        public static void ServerListenOptions(this ListenOptions listenOptions, string pathCetificate, string password)
        {
            listenOptions.UseHttps(pathCetificate, password);
            listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
        }

        #region Binary serialization 

        public static ByteString ToByteString(this object ob) 
        {
            using MemoryStream ms = new();           
            BinaryFormatter bf = new();
            bf.Serialize(ms, ob);
            ms.Position = 0;
            return ByteString.FromStream(ms);            
        }

        public static object ToObject(this ByteString bs)
        {
            using MemoryStream ms = new(bs.ToByteArray());
            ms.Seek(0, SeekOrigin.Begin);
            BinaryFormatter bf = new();
            return bf.Deserialize(ms);
        }

        public static T ToObject<T>(this ByteString bs)
        {
            using MemoryStream ms = new(bs.ToByteArray());
            ms.Seek(0, SeekOrigin.Begin);
            BinaryFormatter bf = new();
            return (T)((bf.Deserialize(ms) as object[])?[0]);
        }

        #endregion // Binary serialization 
    }
}
