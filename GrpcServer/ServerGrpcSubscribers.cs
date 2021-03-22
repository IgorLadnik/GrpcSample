using Microsoft.Extensions.Logging;
using GrpcHelperLib;
using GrpcHelperLib.Communication;

namespace GrpcServer
{
    public class ServerGrpcSubscribers : ServerGrpcSubscribersBase
    {
        public ServerGrpcSubscribers(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }

        public override bool SubscribersFilter(SubscribersModel subscriber, ResponseMessage message) => true;
    }
}
