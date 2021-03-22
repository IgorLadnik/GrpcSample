using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Grpc.Core;
using GrpcHelperLib;
using GrpcHelperLib.Communication;
using GrpcHelperLib.CommunicationServer;

namespace GrpcHelperLib
{
    public class DuplexService : Messaging.MessagingBase, IDisposable
    {
        private GeneralGrpcService _gs;

        public DuplexService(ILoggerFactory loggerFactory, ServerGrpcSubscribersBase serverGrpcSubscribers, MessageProcessorBase messageProcessor) =>        
            _gs = new GeneralGrpcService(loggerFactory, serverGrpcSubscribers, messageProcessor);
        
        public override async Task CreateStreaming(IAsyncStreamReader<RequestMessage> requestStream, IServerStreamWriter<ResponseMessage> responseStream, ServerCallContext context) =>
            await _gs.CreateDuplexStreaming(requestStream, responseStream, context);

        public void Dispose() =>
            _gs.Dispose();
    }
}
