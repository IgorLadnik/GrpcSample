#pragma warning disable 0414, 1591

using grpc = Grpc.Core;
using Google.Protobuf;

namespace CommunicationServer
{
  public static partial class Messaging
  {
    static readonly string __ServiceName = "CommunicationService.Messaging";

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
        __ServiceName,
        "CreateStreaming",
        __Marshaller_Communication_RequestMessage,
        __Marshaller_Communication_ResponseMessage);

    /// <summary>Service descriptor</summary>
    public static Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return CommunicationReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Messaging</summary>
    [grpc.BindServiceMethod(typeof(Messaging), "BindService")]
    public abstract partial class MessagingBase
    {
      public virtual System.Threading.Tasks.Task CreateStreaming(grpc.IAsyncStreamReader<RequestMessage> requestStream, grpc.IServerStreamWriter<ResponseMessage> responseStream, grpc.ServerCallContext context)
      {
        throw new grpc.RpcException(new grpc.Status(grpc.StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc.ServerServiceDefinition BindService(MessagingBase serviceImpl)
    {
      return grpc.ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_CreateStreaming, serviceImpl.CreateStreaming).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc.ServiceBinderBase serviceBinder, MessagingBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_CreateStreaming, serviceImpl == null ? null : new grpc.DuplexStreamingServerMethod<RequestMessage, ResponseMessage>(serviceImpl.CreateStreaming));
    }

  }
}

