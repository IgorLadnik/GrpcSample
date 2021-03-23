using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DtoLib;
using GrpcHelperLib;
using ModelsLib;

namespace GrpcClient
{
    class Program
    {
        const int PORT = 19019;

        static async Task<int> Main(string[] args)
        {
            // TODO: Logger to be added

            Console.WriteLine("GrpcClient started.");

            var pathCertificate = args.Length > 0 && args[0].ToLower() == "tls"
                        ? @"..\..\..\Certs\certificate.crt"
                        : null;

            var nl = Environment.NewLine;
            var orgTextColor = Console.ForegroundColor;

            using Client client = new();
            await client.Start($"localhost:{PORT}", pathCertificate,
                response => Console.WriteLine(response.Payload.ToObject()), // onReceive
                () =>
                {
                    Console.Write($"Connected to server.{nl}ClientId = ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"{client.ClientId}");
                    Console.ForegroundColor = orgTextColor;
                    Console.WriteLine($".{nl}Enter string message to server.{nl}" +
                        $"You will get response if your message will contain question mark '?'.{nl}" +
                        $"Enter empty message to quit.{nl}");
                },
                () => Console.WriteLine("Shutting down...")
            );

            Timer timer = new(async _ => await client.SendAsync(GetTestObjects()), null, 0, 5000);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            return 0;
        }

        public static List<Arg1> GetTestObjects() =>
            new List<Arg1>
            {
                new() { Id = "0", Arg2Props = new() { new() { Id = "0.0" }, new() { Id = "0.1" }, } },
                new() { Id = "1", Arg2Props = new() { new() { Id = "1.0" }, new() { Id = "1.1" }, } },
            };
    }
}
