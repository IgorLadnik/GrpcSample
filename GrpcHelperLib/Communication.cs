#pragma warning disable 1591, 0612, 3021

using pb = Google.Protobuf;
using pbr = Google.Protobuf.Reflection;

namespace GrpcHelperLib.Communication 
{

  /// <summary>Holder for reflection information generated from proto</summary>
  public static partial class CommunicationReflection {

    #region Descriptor
    /// <summary>File descriptor for proto</summary>
    public static pbr.FileDescriptor Descriptor 
    {
      get { return descriptor; }
    }
    private static pbr.FileDescriptor descriptor;

    static CommunicationReflection() {
      byte[] descriptorData = System.Convert.FromBase64String(
          string.Concat(
            "ChNjb21tdW5pY2F0aW9uLnByb3RvEg1Db21tdW5pY2F0aW9uGh9nb29nbGUv",
            "cHJvdG9idWYvdGltZXN0YW1wLnByb3RvIvcBCg5SZXF1ZXN0TWVzc2FnZRIQ",
            "CghjbGllbnRJZBgBIAEoCRIRCgltZXNzYWdlSWQYAiABKAkSKAoEdHlwZRgD",
            "IAEoDjIaLkNvbW11bmljYXRpb24uTWVzc2FnZVR5cGUSKAoEdGltZRgEIAEo",
            "CzIaLmdvb2dsZS5wcm90b2J1Zi5UaW1lc3RhbXASLAoGc3RhdHVzGAUgASgO",
            "MhwuQ29tbXVuaWNhdGlvbi5NZXNzYWdlU3RhdHVzEg8KB3BheWxvYWQYBiAB",
            "KAwSLQoIcmVzcG9uc2UYByABKA4yGy5Db21tdW5pY2F0aW9uLlJlc3BvbnNl",
            "VHlwZSLJAQoPUmVzcG9uc2VNZXNzYWdlEhAKCGNsaWVudElkGAEgASgJEhEK",
            "CW1lc3NhZ2VJZBgCIAEoCRIoCgR0eXBlGAMgASgOMhouQ29tbXVuaWNhdGlv",
            "bi5NZXNzYWdlVHlwZRIoCgR0aW1lGAQgASgLMhouZ29vZ2xlLnByb3RvYnVm",
            "LlRpbWVzdGFtcBIsCgZzdGF0dXMYBSABKA4yHC5Db21tdW5pY2F0aW9uLk1l",
            "c3NhZ2VTdGF0dXMSDwoHcGF5bG9hZBgGIAEoCSpdCgtNZXNzYWdlVHlwZRIZ",
            "ChVNRVNTQUdFVFlQRV9VTkRFRklORUQQABIYChRNRVNTQUdFVFlQRV9PUkRJ",
            "TkFSWRABEhkKFU1FU1NBR0VUWVBFX0lNUE9SVEFOVBACKrEBCg1NZXNzYWdl",
            "U3RhdHVzEhsKF01FU1NBR0VTVEFUVVNfVU5ERUZJTkVEEAASGQoVTUVTU0FH",
            "RVNUQVRVU19DUkVBVEVEEAESFgoSTUVTU0FHRVNUQVRVU19TRU5UEAISGgoW",
            "TUVTU0FHRVNUQVRVU19SRUNFSVZFRBADEhsKF01FU1NBR0VTVEFUVVNfUFJP",
            "Q0VTU0VEEAQSFwoTTUVTU0FHRVNUQVRVU19FUlJPUhAFKmQKDFJlc3BvbnNl",
            "VHlwZRIaChZSRVNQT05TRVRZUEVfVU5ERUZJTkVEEAASGQoVUkVTUE9OU0VU",
            "WVBFX1JFUVVJUkVEEAESHQoZUkVTUE9OU0VUWVBFX05PVF9SRVFVSVJFRBAC",
            "MmEKCU1lc3NhZ2luZxJUCg9DcmVhdGVTdHJlYW1pbmcSHS5Db21tdW5pY2F0",
            "aW9uLlJlcXVlc3RNZXNzYWdlGh4uQ29tbXVuaWNhdGlvbi5SZXNwb25zZU1l",
            "c3NhZ2UoATABYgZwcm90bzM="));
      descriptor = pbr.FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr.FileDescriptor[] { Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, },
          new pbr.GeneratedClrTypeInfo(new[] {typeof(MessageType), typeof(MessageStatus), typeof(ResponseType), }, null, new pbr.GeneratedClrTypeInfo[] {
            new pbr.GeneratedClrTypeInfo(typeof(RequestMessage), RequestMessage.Parser, new[]{ "ClientId", "MessageId", "Type", "Time", "Status", "Payload", "Response" }, null, null, null, null),
            new pbr.GeneratedClrTypeInfo(typeof(ResponseMessage), ResponseMessage.Parser, new[]{ "ClientId", "MessageId", "Type", "Time", "Status", "Payload" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum MessageType 
  {
    [pbr.OriginalName("MESSAGETYPE_UNDEFINED")] Undefined = 0,
    [pbr.OriginalName("MESSAGETYPE_ORDINARY")] Ordinary = 1,
    [pbr.OriginalName("MESSAGETYPE_IMPORTANT")] Important = 2,
  }

  public enum MessageStatus 
  {
    [pbr.OriginalName("MESSAGESTATUS_UNDEFINED")] Undefined = 0,
    [pbr.OriginalName("MESSAGESTATUS_CREATED")] Created = 1,
    [pbr.OriginalName("MESSAGESTATUS_SENT")] Sent = 2,
    [pbr.OriginalName("MESSAGESTATUS_RECEIVED")] Received = 3,
    [pbr.OriginalName("MESSAGESTATUS_PROCESSED")] Processed = 4,
    [pbr.OriginalName("MESSAGESTATUS_ERROR")] Error = 5,
  }

  public enum ResponseType 
  {
    [pbr.OriginalName("RESPONSETYPE_UNDEFINED")] Undefined = 0,
    [pbr.OriginalName("RESPONSETYPE_REQUIRED")] Required = 1,
    [pbr.OriginalName("RESPONSETYPE_NOT_REQUIRED")] NotRequired = 2,
  }

  #endregion

  #region Messages
  public sealed partial class RequestMessage : pb.IMessage<RequestMessage>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb.IBufferMessage
  #endif
  {
    private static readonly pb.MessageParser<RequestMessage> _parser = new pb.MessageParser<RequestMessage>(() => new RequestMessage());
    private pb.UnknownFieldSet _unknownFields;
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb.MessageParser<RequestMessage> Parser { get { return _parser; } }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr.MessageDescriptor Descriptor {
      get { return CommunicationReflection.Descriptor.MessageTypes[0]; }
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr.MessageDescriptor pb.IMessage.Descriptor {
      get { return Descriptor; }
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RequestMessage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RequestMessage(RequestMessage other) : this() {
      clientId_ = other.clientId_;
      messageId_ = other.messageId_;
      type_ = other.type_;
      time_ = other.time_ != null ? other.time_.Clone() : null;
      status_ = other.status_;
      payload_ = other.payload_;
      response_ = other.response_;
      _unknownFields = pb.UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RequestMessage Clone() {
      return new RequestMessage(this);
    }

    /// <summary>Field number for the "clientId" field.</summary>
    public const int ClientIdFieldNumber = 1;
    private string clientId_ = "";
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string ClientId {
      get { return clientId_; }
      set {
        clientId_ = pb.ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "messageId" field.</summary>
    public const int MessageIdFieldNumber = 2;
    private string messageId_ = "";
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string MessageId {
      get { return messageId_; }
      set {
        messageId_ = pb.ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 3;
    private MessageType type_ = MessageType.Undefined;
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageType Type {
      get { return type_; }
      set {
        type_ = value;
      }
    }

    /// <summary>Field number for the "time" field.</summary>
    public const int TimeFieldNumber = 4;
    private Google.Protobuf.WellKnownTypes.Timestamp time_;
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Google.Protobuf.WellKnownTypes.Timestamp Time {
      get { return time_; }
      set {
        time_ = value;
      }
    }

    /// <summary>Field number for the "status" field.</summary>
    public const int StatusFieldNumber = 5;
    private MessageStatus status_ = MessageStatus.Undefined;
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageStatus Status {
      get { return status_; }
      set {
        status_ = value;
      }
    }

    /// <summary>Field number for the "payload" field.</summary>
    public const int PayloadFieldNumber = 6;
    private pb.ByteString payload_ = pb.ByteString.Empty;
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb.ByteString Payload {
      get { return payload_; }
      set {
        payload_ = pb.ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "response" field.</summary>
    public const int ResponseFieldNumber = 7;
    private ResponseType response_ = ResponseType.Undefined;
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ResponseType Response {
      get { return response_; }
      set {
        response_ = value;
      }
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as RequestMessage);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(RequestMessage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ClientId != other.ClientId) return false;
      if (MessageId != other.MessageId) return false;
      if (Type != other.Type) return false;
      if (!object.Equals(Time, other.Time)) return false;
      if (Status != other.Status) return false;
      if (Payload != other.Payload) return false;
      if (Response != other.Response) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (ClientId.Length != 0) hash ^= ClientId.GetHashCode();
      if (MessageId.Length != 0) hash ^= MessageId.GetHashCode();
      if (Type != MessageType.Undefined) hash ^= Type.GetHashCode();
      if (time_ != null) hash ^= Time.GetHashCode();
      if (Status != MessageStatus.Undefined) hash ^= Status.GetHashCode();
      if (Payload.Length != 0) hash ^= Payload.GetHashCode();
      if (Response != ResponseType.Undefined) hash ^= Response.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb.JsonFormatter.ToDiagnosticString(this);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb.CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (ClientId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ClientId);
      }
      if (MessageId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(MessageId);
      }
      if (Type != MessageType.Undefined) {
        output.WriteRawTag(24);
        output.WriteEnum((int) Type);
      }
      if (time_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Time);
      }
      if (Status != MessageStatus.Undefined) {
        output.WriteRawTag(40);
        output.WriteEnum((int) Status);
      }
      if (Payload.Length != 0) {
        output.WriteRawTag(50);
        output.WriteBytes(Payload);
      }
      if (Response != ResponseType.Undefined) {
        output.WriteRawTag(56);
        output.WriteEnum((int) Response);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb.IBufferMessage.InternalWriteTo(ref pb.WriteContext output) {
      if (ClientId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ClientId);
      }
      if (MessageId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(MessageId);
      }
      if (Type != MessageType.Undefined) {
        output.WriteRawTag(24);
        output.WriteEnum((int) Type);
      }
      if (time_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Time);
      }
      if (Status != MessageStatus.Undefined) {
        output.WriteRawTag(40);
        output.WriteEnum((int) Status);
      }
      if (Payload.Length != 0) {
        output.WriteRawTag(50);
        output.WriteBytes(Payload);
      }
      if (Response != ResponseType.Undefined) {
        output.WriteRawTag(56);
        output.WriteEnum((int) Response);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ClientId.Length != 0) {
        size += 1 + pb.CodedOutputStream.ComputeStringSize(ClientId);
      }
      if (MessageId.Length != 0) {
        size += 1 + pb.CodedOutputStream.ComputeStringSize(MessageId);
      }
      if (Type != MessageType.Undefined) {
        size += 1 + pb.CodedOutputStream.ComputeEnumSize((int) Type);
      }
      if (time_ != null) {
        size += 1 + pb.CodedOutputStream.ComputeMessageSize(Time);
      }
      if (Status != MessageStatus.Undefined) {
        size += 1 + pb.CodedOutputStream.ComputeEnumSize((int) Status);
      }
      if (Payload.Length != 0) {
        size += 1 + pb.CodedOutputStream.ComputeBytesSize(Payload);
      }
      if (Response != ResponseType.Undefined) {
        size += 1 + pb.CodedOutputStream.ComputeEnumSize((int) Response);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(RequestMessage other) {
      if (other == null) {
        return;
      }
      if (other.ClientId.Length != 0) {
        ClientId = other.ClientId;
      }
      if (other.MessageId.Length != 0) {
        MessageId = other.MessageId;
      }
      if (other.Type != MessageType.Undefined) {
        Type = other.Type;
      }
      if (other.time_ != null) {
        if (time_ == null) {
          Time = new Google.Protobuf.WellKnownTypes.Timestamp();
        }
        Time.MergeFrom(other.Time);
      }
      if (other.Status != MessageStatus.Undefined) {
        Status = other.Status;
      }
      if (other.Payload.Length != 0) {
        Payload = other.Payload;
      }
      if (other.Response != ResponseType.Undefined) {
        Response = other.Response;
      }
      _unknownFields = pb.UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb.CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb.UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            ClientId = input.ReadString();
            break;
          }
          case 18: {
            MessageId = input.ReadString();
            break;
          }
          case 24: {
            Type = (MessageType) input.ReadEnum();
            break;
          }
          case 34: {
            if (time_ == null) {
              Time = new Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(Time);
            break;
          }
          case 40: {
            Status = (MessageStatus) input.ReadEnum();
            break;
          }
          case 50: {
            Payload = input.ReadBytes();
            break;
          }
          case 56: {
            Response = (ResponseType) input.ReadEnum();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb.IBufferMessage.InternalMergeFrom(ref pb.ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb.UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            ClientId = input.ReadString();
            break;
          }
          case 18: {
            MessageId = input.ReadString();
            break;
          }
          case 24: {
            Type = (MessageType) input.ReadEnum();
            break;
          }
          case 34: {
            if (time_ == null) {
              Time = new Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(Time);
            break;
          }
          case 40: {
            Status = (MessageStatus) input.ReadEnum();
            break;
          }
          case 50: {
            Payload = input.ReadBytes();
            break;
          }
          case 56: {
            Response = (ResponseType) input.ReadEnum();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class ResponseMessage : pb.IMessage<ResponseMessage>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb.IBufferMessage
  #endif
  {
    private static readonly pb.MessageParser<ResponseMessage> _parser = new pb.MessageParser<ResponseMessage>(() => new ResponseMessage());
    private pb.UnknownFieldSet _unknownFields;
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb.MessageParser<ResponseMessage> Parser { get { return _parser; } }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr.MessageDescriptor Descriptor {
      get { return CommunicationReflection.Descriptor.MessageTypes[1]; }
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr.MessageDescriptor pb.IMessage.Descriptor {
      get { return Descriptor; }
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ResponseMessage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ResponseMessage(ResponseMessage other) : this() {
      clientId_ = other.clientId_;
      messageId_ = other.messageId_;
      type_ = other.type_;
      time_ = other.time_ != null ? other.time_.Clone() : null;
      status_ = other.status_;
      payload_ = other.payload_;
      _unknownFields = pb.UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ResponseMessage Clone() {
      return new ResponseMessage(this);
    }

    /// <summary>Field number for the "clientId" field.</summary>
    public const int ClientIdFieldNumber = 1;
    private string clientId_ = "";
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string ClientId {
      get { return clientId_; }
      set {
        clientId_ = pb.ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "messageId" field.</summary>
    public const int MessageIdFieldNumber = 2;
    private string messageId_ = "";
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string MessageId {
      get { return messageId_; }
      set {
        messageId_ = pb.ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 3;
    private MessageType type_ = MessageType.Undefined;
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageType Type {
      get { return type_; }
      set {
        type_ = value;
      }
    }

    /// <summary>Field number for the "time" field.</summary>
    public const int TimeFieldNumber = 4;
    private Google.Protobuf.WellKnownTypes.Timestamp time_;
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Google.Protobuf.WellKnownTypes.Timestamp Time {
      get { return time_; }
      set {
        time_ = value;
      }
    }

    /// <summary>Field number for the "status" field.</summary>
    public const int StatusFieldNumber = 5;
    private MessageStatus status_ = MessageStatus.Undefined;
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageStatus Status {
      get { return status_; }
      set {
        status_ = value;
      }
    }

    /// <summary>Field number for the "payload" field.</summary>
    public const int PayloadFieldNumber = 6;
    private string payload_ = "";
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Payload {
      get { return payload_; }
      set {
        payload_ = pb.ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ResponseMessage);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ResponseMessage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ClientId != other.ClientId) return false;
      if (MessageId != other.MessageId) return false;
      if (Type != other.Type) return false;
      if (!object.Equals(Time, other.Time)) return false;
      if (Status != other.Status) return false;
      if (Payload != other.Payload) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (ClientId.Length != 0) hash ^= ClientId.GetHashCode();
      if (MessageId.Length != 0) hash ^= MessageId.GetHashCode();
      if (Type != MessageType.Undefined) hash ^= Type.GetHashCode();
      if (time_ != null) hash ^= Time.GetHashCode();
      if (Status != MessageStatus.Undefined) hash ^= Status.GetHashCode();
      if (Payload.Length != 0) hash ^= Payload.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb.JsonFormatter.ToDiagnosticString(this);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb.CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (ClientId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ClientId);
      }
      if (MessageId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(MessageId);
      }
      if (Type != MessageType.Undefined) {
        output.WriteRawTag(24);
        output.WriteEnum((int) Type);
      }
      if (time_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Time);
      }
      if (Status != MessageStatus.Undefined) {
        output.WriteRawTag(40);
        output.WriteEnum((int) Status);
      }
      if (Payload.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(Payload);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb.IBufferMessage.InternalWriteTo(ref pb.WriteContext output) {
      if (ClientId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ClientId);
      }
      if (MessageId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(MessageId);
      }
      if (Type != MessageType.Undefined) {
        output.WriteRawTag(24);
        output.WriteEnum((int) Type);
      }
      if (time_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Time);
      }
      if (Status != MessageStatus.Undefined) {
        output.WriteRawTag(40);
        output.WriteEnum((int) Status);
      }
      if (Payload.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(Payload);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ClientId.Length != 0) {
        size += 1 + pb.CodedOutputStream.ComputeStringSize(ClientId);
      }
      if (MessageId.Length != 0) {
        size += 1 + pb.CodedOutputStream.ComputeStringSize(MessageId);
      }
      if (Type != MessageType.Undefined) {
        size += 1 + pb.CodedOutputStream.ComputeEnumSize((int) Type);
      }
      if (time_ != null) {
        size += 1 + pb.CodedOutputStream.ComputeMessageSize(Time);
      }
      if (Status != MessageStatus.Undefined) {
        size += 1 + pb.CodedOutputStream.ComputeEnumSize((int) Status);
      }
      if (Payload.Length != 0) {
        size += 1 + pb.CodedOutputStream.ComputeStringSize(Payload);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ResponseMessage other) {
      if (other == null) {
        return;
      }
      if (other.ClientId.Length != 0) {
        ClientId = other.ClientId;
      }
      if (other.MessageId.Length != 0) {
        MessageId = other.MessageId;
      }
      if (other.Type != MessageType.Undefined) {
        Type = other.Type;
      }
      if (other.time_ != null) {
        if (time_ == null) {
          Time = new Google.Protobuf.WellKnownTypes.Timestamp();
        }
        Time.MergeFrom(other.Time);
      }
      if (other.Status != MessageStatus.Undefined) {
        Status = other.Status;
      }
      if (other.Payload.Length != 0) {
        Payload = other.Payload;
      }
      _unknownFields = pb.UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb.CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb.UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            ClientId = input.ReadString();
            break;
          }
          case 18: {
            MessageId = input.ReadString();
            break;
          }
          case 24: {
            Type = (MessageType) input.ReadEnum();
            break;
          }
          case 34: {
            if (time_ == null) {
              Time = new Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(Time);
            break;
          }
          case 40: {
            Status = (MessageStatus) input.ReadEnum();
            break;
          }
          case 50: {
            Payload = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb.IBufferMessage.InternalMergeFrom(ref pb.ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb.UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            ClientId = input.ReadString();
            break;
          }
          case 18: {
            MessageId = input.ReadString();
            break;
          }
          case 24: {
            Type = (MessageType) input.ReadEnum();
            break;
          }
          case 34: {
            if (time_ == null) {
              Time = new Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(Time);
            break;
          }
          case 40: {
            Status = (MessageStatus) input.ReadEnum();
            break;
          }
          case 50: {
            Payload = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion
}
