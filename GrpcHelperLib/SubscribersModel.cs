using Grpc.Core;
using GrpcHelperLib.Communication;

namespace GrpcHelperLib
{
    public class SubscribersModel
    {
        public IServerStreamWriter<ResponseMessage> Subscriber { get; set; }

        public string Id { get; set; }
    }
}





