//using System;
//using System.Collections.Generic;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Google.Protobuf;
//using GrpcHelperLib;
//using GrpcHelperLib.Communication;
//using ModelsLib;

//namespace DtoLib
//{
//    public class MessageProcessor : MessageProcessorBase
//    {
//        public MessageProcessor(ILoggerFactory loggerFactory)
//            : base(loggerFactory)
//        {
//            RegisterPerCall<IRemoteCall, RemoteCall>();
//        }
//    }
//}
