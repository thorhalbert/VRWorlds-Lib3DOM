// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: StandardsProtoBufs/GUID.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace StandardsProto {

  /// <summary>Holder for reflection information generated from StandardsProtoBufs/GUID.proto</summary>
  public static partial class GUIDReflection {

    #region Descriptor
    /// <summary>File descriptor for StandardsProtoBufs/GUID.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GUIDReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ch1TdGFuZGFyZHNQcm90b0J1ZnMvR1VJRC5wcm90bxIOU3RhbmRhcmRzUHJv",
            "dG8iHgoJUHJvdG9HdWlkEhEKCXV1aWRCeXRlcxgBIAEoDGIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::StandardsProto.ProtoGuid), global::StandardsProto.ProtoGuid.Parser, new[]{ "UuidBytes" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ProtoGuid : pb::IMessage<ProtoGuid> {
    private static readonly pb::MessageParser<ProtoGuid> _parser = new pb::MessageParser<ProtoGuid>(() => new ProtoGuid());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ProtoGuid> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::StandardsProto.GUIDReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ProtoGuid() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ProtoGuid(ProtoGuid other) : this() {
      uuidBytes_ = other.uuidBytes_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ProtoGuid Clone() {
      return new ProtoGuid(this);
    }

    /// <summary>Field number for the "uuidBytes" field.</summary>
    public const int UuidBytesFieldNumber = 1;
    private pb::ByteString uuidBytes_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString UuidBytes {
      get { return uuidBytes_; }
      set {
        uuidBytes_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ProtoGuid);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ProtoGuid other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (UuidBytes != other.UuidBytes) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (UuidBytes.Length != 0) hash ^= UuidBytes.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (UuidBytes.Length != 0) {
        output.WriteRawTag(10);
        output.WriteBytes(UuidBytes);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (UuidBytes.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(UuidBytes);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ProtoGuid other) {
      if (other == null) {
        return;
      }
      if (other.UuidBytes.Length != 0) {
        UuidBytes = other.UuidBytes;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            UuidBytes = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
