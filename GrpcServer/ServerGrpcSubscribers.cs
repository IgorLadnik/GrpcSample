﻿using Microsoft.Extensions.Logging;
using GrpcHelperLib;
using Communication;

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
