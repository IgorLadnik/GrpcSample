using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GrpcHelperLib;
using RemoteInterfaces;
using GrpcHelperLib.Communication;

namespace GrpcClient
{
    class Program
    {
        const string defaultHost = "localhost";
        const int defaultPort = 19061;

        static async Task<int> Main(string[] args)
        {
            using ILoggerFactory loggerFactory =
                LoggerFactory.Create(builder =>
                    builder.AddSimpleConsole(options =>
                    {
                        options.IncludeScopes = false;
                        options.SingleLine = true;
                        options.TimestampFormat = "hh:mm:ss ";
                    }));
            var logger = loggerFactory.CreateLogger<Program>();

            logger.LogInformation("GrpcClient started.");

            var pathCertificate = args.Length > 0 && args[0].ToLower() == "tls" ? "Certs/certificate.crt" : null;

            var url = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]) ? args[1] : $"{defaultHost}:{defaultPort}";

            using GrpcClient client = new(loggerFactory); // ({ ClientId = $"{DateTime.UtcNow.Millisecond}" };
            await client.Start(url, pathCertificate,
                response => logger.LogInformation($"callback: {response.Payload.ToObject()}"),     // onReceive
                () => logger.LogInformation($"Connected to server. ClientId = {client.ClientId}"), // onConnection
                () => logger.LogInformation("Shutting down...")); // onShuttingDown

            using Timer timer = new(async _ =>
            {
                // Send (call method one way)
                {
                    Stopwatch sw = new();
                    sw.Start();
                    client.SendOneWay(IRemoteCall_Foo_Args(1));
                    sw.Stop();
                    logger.LogInformation($"Send one way duration:    {sw.ElapsedTicks}");
                }

                // Send (call method with response in a callback "onReceive()")
                {
                    Stopwatch sw = new();
                    sw.Start();
                    await client.SendAsync(IRemoteCall_Foo_Args(2));
                    sw.Stop();
                    logger.LogInformation($"Send with await duration: {sw.ElapsedTicks}");
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

                    logger.LogInformation($"{result}, {echo}");
                }

                // Direct call
                {
                    Stopwatch sw = new();
                    sw.Start();
                    var result = await client.RemoteMethodCallAsync(IRemoteCall_Foo_Args(2));
                    var echo = await client.RemoteMethodCallAsync("IRemoteCall2", "Echo", "some text - 2");
                    sw.Stop();
                    ticks2 = sw.ElapsedTicks;

                    logger.LogInformation($"{result}, {echo}");
                }

                //var result1 = await task1;
                //var result2 = await task2;
                //logger.LogInformation(($"{await task1} {await task2}");

                logger.LogInformation($"Reflected calls: {ticks1}");
                logger.LogInformation($"Direct calls:    {ticks2}");
                logger.LogInformation($"Ratio: {((float)ticks1 / ticks2).ToString("f1")}");

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

    class GrpcClient : GrpcClientBase 
    {
        public GrpcClient(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        { 
        }

        public override bool CheckResponse(ResponseMessage responseMessage) => base.CheckResponse(responseMessage); // illustration   
    }
}
