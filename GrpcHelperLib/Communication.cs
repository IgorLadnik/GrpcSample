#pragma warning disable 1591, 0612, 3021

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;

namespace Communication
{

    /// <summary>Holder for reflection information generated from communication.proto</summary>
    public static partial class CommunicationReflection
    {

        #region Descriptor
        /// <summary>File descriptor for communication.proto</summary>
        public static pbr::FileDescriptor Descriptor
        {
            get { return descriptor; }
        }
        private static pbr::FileDescriptor descriptor;

        static CommunicationReflection()
        {
            byte[] descriptorData = global::System.Convert.FromBase64String(
                string.Concat(
                  "ChNjb21tdW5pY2F0aW9uLnByb3RvEg1Db21tdW5pY2F0aW9uGh9nb29nbGUv",
                  "cHJvdG9idWYvdGltZXN0YW1wLnByb3RvIskBCg5SZXF1ZXN0TWVzc2FnZRIQ",
                  "CghjbGllbnRJZBgBIAEoCRIRCgltZXNzYWdlSWQYAiABKAkSKAoEdHlwZRgD",
                  "IAEoDjIaLkNvbW11bmljYXRpb24uTWVzc2FnZVR5cGUSKAoEdGltZRgEIAEo",
                  "CzIaLmdvb2dsZS5wcm90b2J1Zi5UaW1lc3RhbXASLQoIcmVzcG9uc2UYBSAB",
                  "KA4yGy5Db21tdW5pY2F0aW9uLlJlc3BvbnNlVHlwZRIPCgdwYXlsb2FkGAYg",
                  "ASgMIq0BCg9SZXNwb25zZU1lc3NhZ2USEAoIY2xpZW50SWQYASABKAkSEQoJ",
                  "bWVzc2FnZUlkGAIgASgJEigKBHR5cGUYAyABKA4yGi5Db21tdW5pY2F0aW9u",
                  "Lk1lc3NhZ2VUeXBlEgwKBHRpbWUYBCABKAMSLAoGc3RhdHVzGAUgASgOMhwu",
                  "Q29tbXVuaWNhdGlvbi5NZXNzYWdlU3RhdHVzEg8KB3BheWxvYWQYBiABKAkq",
                  "XQoLTWVzc2FnZVR5cGUSGQoVTUVTU0FHRVRZUEVfVU5ERUZJTkVEEAASGAoU",
                  "TUVTU0FHRVRZUEVfT1JESU5BUlkQARIZChVNRVNTQUdFVFlQRV9JTVBPUlRB",
                  "TlQQAip9Cg1NZXNzYWdlU3RhdHVzEhsKF01FU1NBR0VTVEFUVVNfVU5ERUZJ",
                  "TkVEEAASGQoVTUVTU0FHRVNUQVRVU19DUkVBVEVEEAESGwoXTUVTU0FHRVNU",
                  "QVRVU19QUk9DRVNTRUQQAhIXChNNRVNTQUdFU1RBVFVTX0VSUk9SEAMqZAoM",
                  "UmVzcG9uc2VUeXBlEhoKFlJFU1BPTlNFVFlQRV9VTkRFRklORUQQABIZChVS",
                  "RVNQT05TRVRZUEVfUkVRVUlSRUQQARIdChlSRVNQT05TRVRZUEVfTk9UX1JF",
                  "UVVJUkVEEAIyYQoJTWVzc2FnaW5nElQKD0NyZWF0ZVN0cmVhbWluZxIdLkNv",
                  "bW11bmljYXRpb24uUmVxdWVzdE1lc3NhZ2UaHi5Db21tdW5pY2F0aW9uLlJl",
                  "c3BvbnNlTWVzc2FnZSgBMAFiBnByb3RvMw=="));
            descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
                new pbr::FileDescriptor[] { global::Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, },
                new pbr::GeneratedClrTypeInfo(new[] { typeof(global::Communication.MessageType), typeof(global::Communication.MessageStatus), typeof(global::Communication.ResponseType), }, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Communication.RequestMessage), global::Communication.RequestMessage.Parser, new[]{ "ClientId", "MessageId", "Type", "Time", "Response", "Payload" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Communication.ResponseMessage), global::Communication.ResponseMessage.Parser, new[]{ "ClientId", "MessageId", "Type", "Time", "Status", "Payload" }, null, null, null, null)
                }));
        }
        #endregion

    }
    #region Enums
    public enum MessageType
    {
        [pbr::OriginalName("MESSAGETYPE_UNDEFINED")] Undefined = 0,
        [pbr::OriginalName("MESSAGETYPE_ORDINARY")] Ordinary = 1,
        [pbr::OriginalName("MESSAGETYPE_IMPORTANT")] Important = 2,
    }

    public enum MessageStatus
    {
        [pbr::OriginalName("MESSAGESTATUS_UNDEFINED")] Undefined = 0,
        [pbr::OriginalName("MESSAGESTATUS_CREATED")] Created = 1,
        [pbr::OriginalName("MESSAGESTATUS_PROCESSED")] Processed = 2,
        [pbr::OriginalName("MESSAGESTATUS_ERROR")] Error = 3,
    }

    public enum ResponseType
    {
        [pbr::OriginalName("RESPONSETYPE_UNDEFINED")] Undefined = 0,
        [pbr::OriginalName("RESPONSETYPE_REQUIRED")] Required = 1,
        [pbr::OriginalName("RESPONSETYPE_NOT_REQUIRED")] NotRequired = 2,
    }

    #endregion

    #region Messages
    public sealed partial class RequestMessage : pb::IMessage<RequestMessage>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
#endif
    {
        private static readonly pb::MessageParser<RequestMessage> _parser = new pb::MessageParser<RequestMessage>(() => new RequestMessage());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<RequestMessage> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor
        {
            get { return global::Communication.CommunicationReflection.Descriptor.MessageTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor
        {
            get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public RequestMessage()
        {
            OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public RequestMessage(RequestMessage other) : this()
        {
            clientId_ = other.clientId_;
            messageId_ = other.messageId_;
            type_ = other.type_;
            time_ = other.time_ != null ? other.time_.Clone() : null;
            response_ = other.response_;
            payload_ = other.payload_;
            _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public RequestMessage Clone()
        {
            return new RequestMessage(this);
        }

        /// <summary>Field number for the "clientId" field.</summary>
        public const int ClientIdFieldNumber = 1;
        private string clientId_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string ClientId
        {
            get { return clientId_; }
            set
            {
                clientId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        /// <summary>Field number for the "messageId" field.</summary>
        public const int MessageIdFieldNumber = 2;
        private string messageId_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string MessageId
        {
            get { return messageId_; }
            set
            {
                messageId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        /// <summary>Field number for the "type" field.</summary>
        public const int TypeFieldNumber = 3;
        private global::Communication.MessageType type_ = global::Communication.MessageType.Undefined;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public global::Communication.MessageType Type
        {
            get { return type_; }
            set
            {
                type_ = value;
            }
        }

        /// <summary>Field number for the "time" field.</summary>
        public const int TimeFieldNumber = 4;
        private global::Google.Protobuf.WellKnownTypes.Timestamp time_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public global::Google.Protobuf.WellKnownTypes.Timestamp Time
        {
            get { return time_; }
            set
            {
                time_ = value;
            }
        }

        /// <summary>Field number for the "response" field.</summary>
        public const int ResponseFieldNumber = 5;
        private global::Communication.ResponseType response_ = global::Communication.ResponseType.Undefined;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public global::Communication.ResponseType Response
        {
            get { return response_; }
            set
            {
                response_ = value;
            }
        }

        /// <summary>Field number for the "payload" field.</summary>
        public const int PayloadFieldNumber = 6;
        private pb::ByteString payload_ = pb::ByteString.Empty;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public pb::ByteString Payload
        {
            get { return payload_; }
            set
            {
                payload_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other)
        {
            return Equals(other as RequestMessage);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(RequestMessage other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            if (ReferenceEquals(other, this))
            {
                return true;
            }
            if (ClientId != other.ClientId) return false;
            if (MessageId != other.MessageId) return false;
            if (Type != other.Type) return false;
            if (!object.Equals(Time, other.Time)) return false;
            if (Response != other.Response) return false;
            if (Payload != other.Payload) return false;
            return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode()
        {
            int hash = 1;
            if (ClientId.Length != 0) hash ^= ClientId.GetHashCode();
            if (MessageId.Length != 0) hash ^= MessageId.GetHashCode();
            if (Type != global::Communication.MessageType.Undefined) hash ^= Type.GetHashCode();
            if (time_ != null) hash ^= Time.GetHashCode();
            if (Response != global::Communication.ResponseType.Undefined) hash ^= Response.GetHashCode();
            if (Payload.Length != 0) hash ^= Payload.GetHashCode();
            if (_unknownFields != null)
            {
                hash ^= _unknownFields.GetHashCode();
            }
            return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override string ToString()
        {
            return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void WriteTo(pb::CodedOutputStream output)
        {
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
      if (Type != global::Communication.MessageType.Undefined) {
        output.WriteRawTag(24);
        output.WriteEnum((int) Type);
      }
      if (time_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Time);
      }
      if (Response != global::Communication.ResponseType.Undefined) {
        output.WriteRawTag(40);
        output.WriteEnum((int) Response);
      }
      if (Payload.Length != 0) {
        output.WriteRawTag(50);
        output.WriteBytes(Payload);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
#endif
        }

#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output)
        {
            if (ClientId.Length != 0)
            {
                output.WriteRawTag(10);
                output.WriteString(ClientId);
            }
            if (MessageId.Length != 0)
            {
                output.WriteRawTag(18);
                output.WriteString(MessageId);
            }
            if (Type != global::Communication.MessageType.Undefined)
            {
                output.WriteRawTag(24);
                output.WriteEnum((int)Type);
            }
            if (time_ != null)
            {
                output.WriteRawTag(34);
                output.WriteMessage(Time);
            }
            if (Response != global::Communication.ResponseType.Undefined)
            {
                output.WriteRawTag(40);
                output.WriteEnum((int)Response);
            }
            if (Payload.Length != 0)
            {
                output.WriteRawTag(50);
                output.WriteBytes(Payload);
            }
            if (_unknownFields != null)
            {
                _unknownFields.WriteTo(ref output);
            }
        }
#endif

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize()
        {
            int size = 0;
            if (ClientId.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(ClientId);
            }
            if (MessageId.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(MessageId);
            }
            if (Type != global::Communication.MessageType.Undefined)
            {
                size += 1 + pb::CodedOutputStream.ComputeEnumSize((int)Type);
            }
            if (time_ != null)
            {
                size += 1 + pb::CodedOutputStream.ComputeMessageSize(Time);
            }
            if (Response != global::Communication.ResponseType.Undefined)
            {
                size += 1 + pb::CodedOutputStream.ComputeEnumSize((int)Response);
            }
            if (Payload.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeBytesSize(Payload);
            }
            if (_unknownFields != null)
            {
                size += _unknownFields.CalculateSize();
            }
            return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(RequestMessage other)
        {
            if (other == null)
            {
                return;
            }
            if (other.ClientId.Length != 0)
            {
                ClientId = other.ClientId;
            }
            if (other.MessageId.Length != 0)
            {
                MessageId = other.MessageId;
            }
            if (other.Type != global::Communication.MessageType.Undefined)
            {
                Type = other.Type;
            }
            if (other.time_ != null)
            {
                if (time_ == null)
                {
                    Time = new global::Google.Protobuf.WellKnownTypes.Timestamp();
                }
                Time.MergeFrom(other.Time);
            }
            if (other.Response != global::Communication.ResponseType.Undefined)
            {
                Response = other.Response;
            }
            if (other.Payload.Length != 0)
            {
                Payload = other.Payload;
            }
            _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(pb::CodedInputStream input)
        {
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
            input.ReadRawMessage(this);
#else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
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
            Type = (global::Communication.MessageType) input.ReadEnum();
            break;
          }
          case 34: {
            if (time_ == null) {
              Time = new global::Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(Time);
            break;
          }
          case 40: {
            Response = (global::Communication.ResponseType) input.ReadEnum();
            break;
          }
          case 50: {
            Payload = input.ReadBytes();
            break;
          }
        }
      }
#endif
        }

#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input)
        {
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
                        break;
                    case 10:
                        {
                            ClientId = input.ReadString();
                            break;
                        }
                    case 18:
                        {
                            MessageId = input.ReadString();
                            break;
                        }
                    case 24:
                        {
                            Type = (global::Communication.MessageType)input.ReadEnum();
                            break;
                        }
                    case 34:
                        {
                            if (time_ == null)
                            {
                                Time = new global::Google.Protobuf.WellKnownTypes.Timestamp();
                            }
                            input.ReadMessage(Time);
                            break;
                        }
                    case 40:
                        {
                            Response = (global::Communication.ResponseType)input.ReadEnum();
                            break;
                        }
                    case 50:
                        {
                            Payload = input.ReadBytes();
                            break;
                        }
                }
            }
        }
#endif

    }

    public sealed partial class ResponseMessage : pb::IMessage<ResponseMessage>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
#endif
    {
        private static readonly pb::MessageParser<ResponseMessage> _parser = new pb::MessageParser<ResponseMessage>(() => new ResponseMessage());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<ResponseMessage> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor
        {
            get { return global::Communication.CommunicationReflection.Descriptor.MessageTypes[1]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor
        {
            get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public ResponseMessage()
        {
            OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public ResponseMessage(ResponseMessage other) : this()
        {
            clientId_ = other.clientId_;
            messageId_ = other.messageId_;
            type_ = other.type_;
            time_ = other.time_;
            status_ = other.status_;
            payload_ = other.payload_;
            _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public ResponseMessage Clone()
        {
            return new ResponseMessage(this);
        }

        /// <summary>Field number for the "clientId" field.</summary>
        public const int ClientIdFieldNumber = 1;
        private string clientId_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string ClientId
        {
            get { return clientId_; }
            set
            {
                clientId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        /// <summary>Field number for the "messageId" field.</summary>
        public const int MessageIdFieldNumber = 2;
        private string messageId_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string MessageId
        {
            get { return messageId_; }
            set
            {
                messageId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        /// <summary>Field number for the "type" field.</summary>
        public const int TypeFieldNumber = 3;
        private global::Communication.MessageType type_ = global::Communication.MessageType.Undefined;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public global::Communication.MessageType Type
        {
            get { return type_; }
            set
            {
                type_ = value;
            }
        }

        /// <summary>Field number for the "time" field.</summary>
        public const int TimeFieldNumber = 4;
        private long time_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public long Time
        {
            get { return time_; }
            set
            {
                time_ = value;
            }
        }

        /// <summary>Field number for the "status" field.</summary>
        public const int StatusFieldNumber = 5;
        private global::Communication.MessageStatus status_ = global::Communication.MessageStatus.Undefined;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public global::Communication.MessageStatus Status
        {
            get { return status_; }
            set
            {
                status_ = value;
            }
        }

        /// <summary>Field number for the "payload" field.</summary>
        public const int PayloadFieldNumber = 6;
        private string payload_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string Payload
        {
            get { return payload_; }
            set
            {
                payload_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other)
        {
            return Equals(other as ResponseMessage);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(ResponseMessage other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            if (ReferenceEquals(other, this))
            {
                return true;
            }
            if (ClientId != other.ClientId) return false;
            if (MessageId != other.MessageId) return false;
            if (Type != other.Type) return false;
            if (Time != other.Time) return false;
            if (Status != other.Status) return false;
            if (Payload != other.Payload) return false;
            return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode()
        {
            int hash = 1;
            if (ClientId.Length != 0) hash ^= ClientId.GetHashCode();
            if (MessageId.Length != 0) hash ^= MessageId.GetHashCode();
            if (Type != global::Communication.MessageType.Undefined) hash ^= Type.GetHashCode();
            if (Time != 0L) hash ^= Time.GetHashCode();
            if (Status != global::Communication.MessageStatus.Undefined) hash ^= Status.GetHashCode();
            if (Payload.Length != 0) hash ^= Payload.GetHashCode();
            if (_unknownFields != null)
            {
                hash ^= _unknownFields.GetHashCode();
            }
            return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override string ToString()
        {
            return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void WriteTo(pb::CodedOutputStream output)
        {
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
      if (Type != global::Communication.MessageType.Undefined) {
        output.WriteRawTag(24);
        output.WriteEnum((int) Type);
      }
      if (Time != 0L) {
        output.WriteRawTag(32);
        output.WriteInt64(Time);
      }
      if (Status != global::Communication.MessageStatus.Undefined) {
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
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output)
        {
            if (ClientId.Length != 0)
            {
                output.WriteRawTag(10);
                output.WriteString(ClientId);
            }
            if (MessageId.Length != 0)
            {
                output.WriteRawTag(18);
                output.WriteString(MessageId);
            }
            if (Type != global::Communication.MessageType.Undefined)
            {
                output.WriteRawTag(24);
                output.WriteEnum((int)Type);
            }
            if (Time != 0L)
            {
                output.WriteRawTag(32);
                output.WriteInt64(Time);
            }
            if (Status != global::Communication.MessageStatus.Undefined)
            {
                output.WriteRawTag(40);
                output.WriteEnum((int)Status);
            }
            if (Payload.Length != 0)
            {
                output.WriteRawTag(50);
                output.WriteString(Payload);
            }
            if (_unknownFields != null)
            {
                _unknownFields.WriteTo(ref output);
            }
        }
#endif

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize()
        {
            int size = 0;
            if (ClientId.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(ClientId);
            }
            if (MessageId.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(MessageId);
            }
            if (Type != global::Communication.MessageType.Undefined)
            {
                size += 1 + pb::CodedOutputStream.ComputeEnumSize((int)Type);
            }
            if (Time != 0L)
            {
                size += 1 + pb::CodedOutputStream.ComputeInt64Size(Time);
            }
            if (Status != global::Communication.MessageStatus.Undefined)
            {
                size += 1 + pb::CodedOutputStream.ComputeEnumSize((int)Status);
            }
            if (Payload.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(Payload);
            }
            if (_unknownFields != null)
            {
                size += _unknownFields.CalculateSize();
            }
            return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(ResponseMessage other)
        {
            if (other == null)
            {
                return;
            }
            if (other.ClientId.Length != 0)
            {
                ClientId = other.ClientId;
            }
            if (other.MessageId.Length != 0)
            {
                MessageId = other.MessageId;
            }
            if (other.Type != global::Communication.MessageType.Undefined)
            {
                Type = other.Type;
            }
            if (other.Time != 0L)
            {
                Time = other.Time;
            }
            if (other.Status != global::Communication.MessageStatus.Undefined)
            {
                Status = other.Status;
            }
            if (other.Payload.Length != 0)
            {
                Payload = other.Payload;
            }
            _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(pb::CodedInputStream input)
        {
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
            input.ReadRawMessage(this);
#else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
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
            Type = (global::Communication.MessageType) input.ReadEnum();
            break;
          }
          case 32: {
            Time = input.ReadInt64();
            break;
          }
          case 40: {
            Status = (global::Communication.MessageStatus) input.ReadEnum();
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
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input)
        {
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
                        break;
                    case 10:
                        {
                            ClientId = input.ReadString();
                            break;
                        }
                    case 18:
                        {
                            MessageId = input.ReadString();
                            break;
                        }
                    case 24:
                        {
                            Type = (global::Communication.MessageType)input.ReadEnum();
                            break;
                        }
                    case 32:
                        {
                            Time = input.ReadInt64();
                            break;
                        }
                    case 40:
                        {
                            Status = (global::Communication.MessageStatus)input.ReadEnum();
                            break;
                        }
                    case 50:
                        {
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

