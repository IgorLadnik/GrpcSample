using Grpc.Core;

namespace GrpcHelperLib
{
    public class SubscribersModel<TResponse>
    {
        public IServerStreamWriter<TResponse> Subscriber { get; set; }

        public string Id { get; set; }
    }
}





