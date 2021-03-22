#pragma warning disable 0414, 1591

using System;
using grpc = Grpc.Core;
using Google.Protobuf;
using GrpcHelperLib.Communication;

namespace GrpcHelperLib.CommunicationClient 
{
  public static partial class Messaging
  {
    static void __Helper_SerializeMessage(IMessage message, grpc.SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc.DeserializationContext context, MessageParser<T> parser) where T : IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc.Marshaller<RequestMessage> __Marshaller_Communication_RequestMessage = grpc.Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, RequestMessage.Parser));
    static readonly grpc.Marshaller<ResponseMessage> __Marshaller_Communication_ResponseMessage = grpc.Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, ResponseMessage.Parser));

    static readonly grpc.Method<RequestMessage, ResponseMessage> __Method_CreateStreaming = new grpc.Method<RequestMessage, ResponseMessage>(
        grpc.MethodType.DuplexStreaming,
        Communication.Communication.ServiceName,
        "CreateStreaming",
        __Marshaller_Communication_RequestMessage,
        __Marshaller_Communication_ResponseMessage);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return CommunicationReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for Messaging</summary>
    public partial class MessagingClient : grpc.ClientBase<MessagingClient>
    {
      /// <summary>Creates a new client for Messaging</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public MessagingClient(grpc.ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for Messaging that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public MessagingClient(grpc.CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected MessagingClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected MessagingClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual grpc.AsyncDuplexStreamingCall<RequestMessage, ResponseMessage> CreateStreaming(grpc.Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateStreaming(new grpc.CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc.AsyncDuplexStreamingCall<RequestMessage, ResponseMessage> CreateStreaming(grpc.CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_CreateStreaming, null, options);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override MessagingClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new MessagingClient(configuration);
      }
    }

  }
}

