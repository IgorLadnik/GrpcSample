using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using GrpcHelperLib;
using RemoteInterfaces;

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

            using GrpcClientBase client = new() { ClientId = $"{Guid.NewGuid()}" };
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

            using Timer timer = new(async _ =>
            {
                await client.SendAsync(IRemoteCall_Foo);

                // Call with reflection
                Stopwatch sw1 = new();
                sw1.Start();
                var result = await client.RemoteCallAsync("IRemoteCall", "Foo",
                         "my name",
                         new Arg1[]
                         {
                            new() { Id = "0", Arg2Props = new() { new() { Id = "0.0" }, new() { Id = "0.1" }, } },
                            new() { Id = "1", Arg2Props = new() { new() { Id = "1.0" }, new() { Id = "1.1" }, } },
                         }
                    );
                var echo = await client.RemoteCallAsync("IRemoteCall", "Echo", "some text");
                sw1.Stop();
                var ticks1 = sw1.ElapsedTicks;

                Console.WriteLine($"   {result}");
                Console.WriteLine($"   {echo}");

                // Direct call
                Stopwatch sw2 = new();
                sw2.Start();
                result = await client.RemoteCallAsync("IRemoteCall1", "Foo",
                         "my name",
                         new Arg1[]
                         {
                            new() { Id = "0", Arg2Props = new() { new() { Id = "0.0" }, new() { Id = "0.1" }, } },
                            new() { Id = "1", Arg2Props = new() { new() { Id = "1.0" }, new() { Id = "1.1" }, } },
                         }
                    );
                echo = await client.RemoteCallAsync("IRemoteCall1", "Echo", "some text");
                sw2.Stop();
                var ticks2 = sw2.ElapsedTicks;

                Console.WriteLine($"   {result}");
                Console.WriteLine($"   {echo}");

                Console.WriteLine($"Reclected call: {ticks1}\nDirect call:    {ticks2}\nRatio: {((float)ticks1 / ticks2).ToString("f1")}");
            }, null, 0, 5000);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            return 0;
        }

        private static object[] IRemoteCall_Foo =>
            new object[]
            {
                 "IRemoteCall", "Foo", "my name",
                 new Arg1[]
                 {
                    new() { Id = "0", Arg2Props = new() { new() { Id = "0.0" }, new() { Id = "0.1" }, } },
                    new() { Id = "1", Arg2Props = new() { new() { Id = "1.0" }, new() { Id = "1.1" }, } },
                 }
            };
    }
}
