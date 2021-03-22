using Microsoft.Extensions.DependencyInjection;
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
    }
}
