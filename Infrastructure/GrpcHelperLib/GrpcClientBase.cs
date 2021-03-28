﻿using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcHelperLib.Communication;
using GrpcHelperLib.CommunicationClient;

namespace GrpcHelperLib
{
    public class GrpcClientBase : IDisposable
    {
        private ConcurrentDictionary<string, AutoResetEvent> _dctEv = new();
        private ConcurrentDictionary<string, ResponseMessage> _dctResult = new();
        private ILogger _logger;

        public GrpcClientBase(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory?.CreateLogger<GrpcClientBase>();
            ClientId = $"{Guid.NewGuid()}";
        }

        public string ClientId { get; set; }

        private AsyncDuplexStreamingCall<RequestMessage, ResponseMessage> _duplex;
        private Channel _channel;
        private Action _onShuttingDown;

        public virtual AsyncDuplexStreamingCall<RequestMessage, ResponseMessage> CreateDuplexClient(Channel channel) =>
             new Messaging.MessagingClient(channel).CreateStreaming();

        public async Task<GrpcClientBase> Start(string url, string pathCertificate, Action<ResponseMessage> onReceive, Action onConnection = null, Action onShuttingDown = null)
        {
            _onShuttingDown = onShuttingDown;

            _channel = new(url, string.IsNullOrEmpty(pathCertificate) ? ChannelCredentials.Insecure : new SslCredentials(File.ReadAllText(pathCertificate)));
            _duplex = CreateDuplexClient(_channel);

            onConnection?.Invoke();

            var responseTask = Task.Run(async () =>
            {
                while (await _duplex.ResponseStream.MoveNext(CancellationToken.None))
                {
                    var responseMessage = _duplex.ResponseStream.Current;
                    try
                    {
                        var br = OnReceive(responseMessage);
                        if (br == null)
                            continue;

                        if (br == false)
                            onReceive?.Invoke(responseMessage);
                    }
                    catch (Exception e)
                    {
                        _logger?.LogError(e, "Error in OnReceive() / onReceive()");
                    }
                }
            });

            return this;
        }

        public Task<object> RemoteMethodCallAsync(params object[] obs) =>
            Task.Run(async () =>
            {
                var messageId = await SendAsync(obs);
                if (string.IsNullOrEmpty(messageId))
                    return null;

                AutoResetEvent ev = _dctEv[messageId] = new(false);
                ev.WaitOne();
                ev.Dispose();

                if (!_dctResult.TryRemove(messageId, out ResponseMessage responseMessage))
                    return null;

                return responseMessage.Payload.ToObject();
            });

        public Task<string> SendAsync(params object[] obs) =>
            Task.Run(async () =>
            {
                if (!obs.CheckArgs())
                {
                    _logger?.LogError("Error in SendAsync(): args failed to pass CheckArgs()");
                    return null;
                }

                try
                {
                    var message = CreateMessage(obs.ToByteString(), true);
                    await _duplex.RequestStream.WriteAsync(message);
                    return message.MessageId;
                }
                catch (Exception e)
                {
                    _logger?.LogError(e, "Error in SendAsync()");
                    return null;
                }
            });

        public void SendOneWay(params object[] obs) => SendAsync(obs);

        private RequestMessage CreateMessage(ByteString payload, bool isResponseRequied) =>
            new()
            {
                ClientId = ClientId,
                MessageId = $"{Guid.NewGuid()}",
                Type = MessageType.Ordinary,
                Time = Timestamp.FromDateTime(DateTime.UtcNow),
                Status = MessageStatus.Created,
                Response = isResponseRequied ? ResponseType.Required : ResponseType.NotRequired,
                Payload = payload
            };

        private bool? OnReceive(ResponseMessage responseMessage) 
        {
            if (!CheckResponse(responseMessage)) 
            {
                _logger?.LogError("Error in OnReceive(): 'responseMessage' failed to pass CheckResponse()");
                return null;
            }

            var messageId = responseMessage.MessageId;
            if (!_dctEv.TryRemove(messageId, out AutoResetEvent ev))
                return false;

            _dctResult[messageId] = responseMessage;
            ev.Set();
            return true;
        }

        public virtual bool CheckResponse(ResponseMessage responseMessage) 
        {
            var ob = responseMessage.Payload.ToObject(); //??
            return true;
        }

        public void Dispose()
        {
            if (_duplex != null) 
            {
                // Delete all sessions for this client, if any
                SendOneWay(Ex.allInterfaces, Ex.deleteSession);

                _onShuttingDown?.Invoke();
                _duplex.RequestStream.CompleteAsync();
                _channel?.ShutdownAsync().Wait();
                _duplex?.Dispose();
            }
        }
    }
}


