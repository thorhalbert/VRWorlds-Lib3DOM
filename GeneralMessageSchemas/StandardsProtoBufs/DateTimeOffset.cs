// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: StandardsProtoBufs/DateTimeOffset.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace VRWorlds.Schemas.Proto.Standards {

  /// <summary>Holder for reflection information generated from StandardsProtoBufs/DateTimeOffset.proto</summary>
  public static partial class DateTimeOffsetReflection {

    #region Descriptor
    /// <summary>File descriptor for StandardsProtoBufs/DateTimeOffset.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static DateTimeOffsetReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CidTdGFuZGFyZHNQcm90b0J1ZnMvRGF0ZVRpbWVPZmZzZXQucHJvdG8SIFZS",
            "V29ybGRzLlNjaGVtYXMuUHJvdG8uU3RhbmRhcmRzIi8KDkRhdGVUaW1lT2Zm",
            "c2V0Eg0KBXRpY2tzGAEgASgDEg4KBm9mZnNldBgCIAEoBWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::VRWorlds.Schemas.Proto.Standards.DateTimeOffset), global::VRWorlds.Schemas.Proto.Standards.DateTimeOffset.Parser, new[]{ "Ticks", "Offset" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class DateTimeOffset : pb::IMessage<DateTimeOffset> {
    private static readonly pb::MessageParser<DateTimeOffset> _parser = new pb::MessageParser<DateTimeOffset>(() => new DateTimeOffset());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<DateTimeOffset> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::VRWorlds.Schemas.Proto.Standards.DateTimeOffsetReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DateTimeOffset() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DateTimeOffset(DateTimeOffset other) : this() {
      ticks_ = other.ticks_;
      offset_ = other.offset_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DateTimeOffset Clone() {
      return new DateTimeOffset(this);
    }

    /// <summary>Field number for the "ticks" field.</summary>
    public const int TicksFieldNumber = 1;
    private long ticks_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long Ticks {
      get { return ticks_; }
      set {
        ticks_ = value;
      }
    }

    /// <summary>Field number for the "offset" field.</summary>
    public const int OffsetFieldNumber = 2;
    private int offset_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Offset {
      get { return offset_; }
      set {
        offset_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as DateTimeOffset);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(DateTimeOffset other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Ticks != other.Ticks) return false;
      if (Offset != other.Offset) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Ticks != 0L) hash ^= Ticks.GetHashCode();
      if (Offset != 0) hash ^= Offset.GetHashCode();
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
      if (Ticks != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(Ticks);
      }
      if (Offset != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Offset);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Ticks != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Ticks);
      }
      if (Offset != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Offset);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(DateTimeOffset other) {
      if (other == null) {
        return;
      }
      if (other.Ticks != 0L) {
        Ticks = other.Ticks;
      }
      if (other.Offset != 0) {
        Offset = other.Offset;
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
          case 8: {
            Ticks = input.ReadInt64();
            break;
          }
          case 16: {
            Offset = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code