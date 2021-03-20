using Microsoft.Extensions.Logging;
using Communication;
using GrpcHelperLib;

namespace GrpcServer
{
    public class ServerGrpcSubscribers : ServerGrpcSubscribersBase<ResponseMessage>
    {
        public ServerGrpcSubscribers(ILoggerFactory loggerFactory) 
            : base(loggerFactory)
        {
        }

        public override bool Filter(SubscribersModel<ResponseMessage> subscriber, ResponseMessage message) => true;
    }
}
