using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Grpc.Core;
using GrpcHelperLib.Communication;
using GrpcHelperLib.CommunicationServer;

namespace GrpcHelperLib
{
    public class DuplexServiceBase : Messaging.MessagingBase, IDisposable
    {
        private GeneralGrpcService _service;

        protected DuplexServiceBase(ILoggerFactory loggerFactory, ServerGrpcSubscribersBase serverGrpcSubscribers, MessageProcessorBase messageProcessor) =>        
            _service = new(loggerFactory, serverGrpcSubscribers, messageProcessor);
        
        public override async Task CreateStreaming(IAsyncStreamReader<RequestMessage> requestStream, IServerStreamWriter<ResponseMessage> responseStream, ServerCallContext context) =>
            await _service.CreateDuplexStreaming(requestStream, responseStream, context);

        public void Dispose() =>
            _service.Dispose();
    }
}
