using System;
using System.Threading.Tasks;

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

            Client client = new();
            await client.Start($"localhost:{PORT}", pathCertificate,
                response => Console.WriteLine(response.Payload.ToStringUtf8()), // onReceive
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

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            return 0;
        }
    }
}
