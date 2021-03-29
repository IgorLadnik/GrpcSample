using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using GrpcHelperLib;

namespace GrpcServer
{
    public class Program
    {
        const int defaultPort = 19061;

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
                        var port = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]) ? int.Parse(args[1]) : defaultPort;
                        if (args.Length > 0 && args[0].ToLower() == "tls")
                            options.Listen(IPAddress.Any, port, listenOptions => 
                                listenOptions.ServerListenOptions("Certs/grpcServer.pfx", "1511"));
                        else
                            options.Listen(IPAddress.Any, port);
                    });
                });
    }
}
