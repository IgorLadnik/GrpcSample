using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Hosting;
using Grpc.AspNetCore.Server;

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
}
}
