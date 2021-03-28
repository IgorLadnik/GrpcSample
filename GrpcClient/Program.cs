using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GrpcHelperLib;
using RemoteInterfaces;

namespace GrpcClient
{
    class Program
    {
        const string HOST = "localhost";
        const int PORT = 19019;

        static async Task<int> Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
                builder.AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("System", LogLevel.Warning)
                       .AddFilter("GrpcClient.Program", LogLevel.Debug)
                       .AddConsole());
            var logger = loggerFactory.CreateLogger<Program>();

            logger.LogInformation("GrpcClient started.");
            //Console.WriteLine("GrpcClient started.");

            var pathCertificate = args.Length > 0 && args[0].ToLower() == "tls"
                        ? @"..\..\..\Certs\certificate.crt"
                        : null;

            var url = $"{HOST}:{PORT}";

            var nl = Environment.NewLine;
            var orgTextColor = Console.ForegroundColor;
           
            using GrpcClientBase client = new(loggerFactory);
            await client.Start(url, pathCertificate,
                response => Console.WriteLine($"\ncallback: {response.Payload.ToObject()}"), // onReceive
                () => // onConnection
                {
                    Console.Write($"Connected to server.{nl}ClientId = ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"{client.ClientId}");
                    Console.ForegroundColor = orgTextColor;
                    Console.WriteLine($".{nl}Enter string message to server.{nl}" +
                        $"You will get response if your message will contain question mark '?'.{nl}" +
                        $"Enter empty message to quit.{nl}");
                },
                () => Console.WriteLine("Shutting down...")); // onShuttingDown

            using Timer timer = new(async _ =>
            {
                Console.WriteLine();

                // Send (call method one way)
                {
                    Stopwatch sw = new();
                    sw.Start();
                    client.SendOneWay(IRemoteCall_Foo_Args(1));
                    sw.Stop();
                    Console.WriteLine($"Send one way duration:    {sw.ElapsedTicks}");
                }

                // Send (call method with response in a callback "onReceive()")
                {
                    Stopwatch sw = new();
                    sw.Start();
                    await client.SendAsync(IRemoteCall_Foo_Args(2));
                    sw.Stop();
                    Console.WriteLine($"Send with await duration: {sw.ElapsedTicks}");
                }

                long ticks1, ticks2;
                //Task<object> task1, task2; 

                // Call with reflection
                {
                    Stopwatch sw = new();
                    sw.Start();
                    var result = await client.RemoteMethodCallAsync(IRemoteCall_Foo_Args(1));
                    var echo = await client.RemoteMethodCallAsync("IRemoteCall1", "Echo", "some text - 1");
                    sw.Stop();
                    ticks1 = sw.ElapsedTicks;

                    Console.WriteLine($"   {result}");
                    Console.WriteLine($"   {echo}");
                }

                // Direct call
                {
                    Stopwatch sw = new();
                    sw.Start();
                    var result = await client.RemoteMethodCallAsync(IRemoteCall_Foo_Args(2));
                    var echo = await client.RemoteMethodCallAsync("IRemoteCall2", "Echo", "some text - 2");
                    sw.Stop();
                    ticks2 = sw.ElapsedTicks;

                    Console.WriteLine($"   {result}");
                    Console.WriteLine($"   {echo}");
                }

                //var result1 = await task1;
                //var result2 = await task2;
                //Console.WriteLine($"   {await task1} {await task2}");

                Console.WriteLine($"Reflected calls: {ticks1}\nDirect calls:    {ticks2}\nRatio: {((float)ticks1 / ticks2).ToString("f1")}");

                //client.SendOneWay("IRemoteCall1", "_delete_session");
            }, 
            null, 0, 5000);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            return 0;
        }

        private static object[] IRemoteCall_Foo_Args(int n) =>
            new object[]
            {
                 $"IRemoteCall{n}", "Foo",
                 "my name",
                 new Arg1[]
                 {
                    new() { Id = "0", Arg2Props = new() { new() { Id = "0.0" }, new() { Id = "0.1" }, } },
                    new() { Id = "1", Arg2Props = new() { new() { Id = "1.0" }, new() { Id = "1.1" }, } },
                 }
            };
    }
}
