// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: communication.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Communication {
  public static partial class Messaging
  {
    static readonly string __ServiceName = "Communication.Messaging";

    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc::Marshaller<global::Communication.RequestMessage> __Marshaller_Communication_RequestMessage = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Communication.RequestMessage.Parser));
    static readonly grpc::Marshaller<global::Communication.ResponseMessage> __Marshaller_Communication_ResponseMessage = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Communication.ResponseMessage.Parser));

    static readonly grpc::Method<global::Communication.RequestMessage, global::Communication.ResponseMessage> __Method_CreateStreaming = new grpc::Method<global::Communication.RequestMessage, global::Communication.ResponseMessage>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "CreateStreaming",
        __Marshaller_Communication_RequestMessage,
        __Marshaller_Communication_ResponseMessage);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Communication.CommunicationReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Messaging</summary>
    [grpc::BindServiceMethod(typeof(Messaging), "BindService")]
    public abstract partial class MessagingBase
    {
      public virtual global::System.Threading.Tasks.Task CreateStreaming(grpc::IAsyncStreamReader<global::Communication.RequestMessage> requestStream, grpc::IServerStreamWriter<global::Communication.ResponseMessage> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(MessagingBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_CreateStreaming, serviceImpl.CreateStreaming).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, MessagingBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_CreateStreaming, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::Communication.RequestMessage, global::Communication.ResponseMessage>(serviceImpl.CreateStreaming));
    }

  }
}
#endregion
