using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using GrpcHelperLib;

namespace GrpcServer
{
    public class Program
    {
        const int PORT = 19019;

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureKestrel(options =>
                    {
                        options.Limits.MinRequestBodyDataRate = null;
                        options.Listen(IPAddress.Any, PORT,
                        args?[0]?.ToLower() == "tls"
                                ? listenOptions => listenOptions.ServerListenOptions("grpcServer.pfx", "1511")
                                : null
                        );
                    });
                });
    }
}
