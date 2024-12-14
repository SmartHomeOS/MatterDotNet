using MatterDotNet.Protocol.Payloads;
using System.Text;

namespace MatterDotNet.Protocol.Parsers
{
    public class TLVWriter
    {
        PayloadWriter writer;

        public TLVWriter(PayloadWriter writer)
        {
            this.writer = writer;
        }

        private void WriteTag(TLVControl control, ElementType type)
        {
            writer.Write((byte)(((byte)control << 5) | (byte)type));
        }

        private void WriteTag(uint tagNumber, ElementType type)
        {
            writer.Write((byte)(((byte)TLVControl.ContextSpecific << 5) | (byte)type));
            writer.Write((byte)tagNumber);
        }

        public void StartStructure(uint tagNumber)
        {
            if (tagNumber == 0)
                WriteTag(TLVControl.Anonymous, ElementType.Structure);
            else
                WriteTag(tagNumber, ElementType.Structure);
        }

        public void StartArray(uint tagNumber)
        {
            WriteTag(tagNumber, ElementType.Array);
        }

        public void StartList()
        {
            WriteTag(TLVControl.Anonymous, ElementType.List);
        }

        public void StartList(uint tagNumber)
        {
            WriteTag(tagNumber, ElementType.List);
        }

        public void EndContainer()
        {
            WriteTag(TLVControl.Anonymous, ElementType.EndOfContainer);
        }

        public void WriteByte(uint tagNumber, byte? value)
        {
            if (!value.HasValue)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.Byte);
                writer.Write(value.Value);
            }
        }
        public void WriteSByte(uint tagNumber, sbyte? value)
        {
            if (!value.HasValue)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.SByte);
                writer.Write(value.Value);
            }
        }
        public void WriteShort(uint tagNumber, short? value)
        {
            if (!value.HasValue)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                if (value.Value < sbyte.MaxValue && value.Value > sbyte.MinValue)
                {
                    WriteSByte(tagNumber, (sbyte)value.Value);
                    return;
                }
                WriteTag(tagNumber, ElementType.Short);
                writer.Write(value.Value);
            }
        }
        public void WriteUShort(uint tagNumber, ushort? value)
        {
            if (!value.HasValue)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                if (value.Value < byte.MaxValue)
                {
                    WriteByte(tagNumber, (byte)value.Value);
                    return;
                }
                WriteTag(tagNumber, ElementType.UShort);
                writer.Write(value.Value);
            }
        }
        public void WriteInt(uint tagNumber, int? value)
        {
            if (!value.HasValue)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                if (value.Value < short.MaxValue && value.Value > short.MinValue)
                {
                    WriteShort(tagNumber, (short)value.Value);
                    return;
                }
                WriteTag(tagNumber, ElementType.Int);
                writer.Write(value.Value);
            }
        }
        public void WriteUInt(uint tagNumber, uint? value)
        {
            if (!value.HasValue)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                if (value.Value < ushort.MaxValue)
                {
                    WriteUShort(tagNumber, (ushort)value.Value);
                    return;
                }
                WriteTag(tagNumber, ElementType.UInt);
                writer.Write(value.Value);
            }
        }
        public void WriteLong(uint tagNumber, long? value)
        {
            if (!value.HasValue)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                if (value.Value < int.MaxValue && value.Value > int.MinValue)
                {
                    WriteInt(tagNumber, (int)value.Value);
                    return;
                }
                WriteTag(tagNumber, ElementType.Long);
                writer.Write(value.Value);
            }
        }
        public void WriteULong(uint tagNumber, ulong? value)
        {
            if (!value.HasValue)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                if (value.Value < uint.MaxValue)
                {
                    WriteUInt(tagNumber, (uint)value.Value);
                    return;
                }
                WriteTag(tagNumber, ElementType.ULong);
                writer.Write(value.Value);
            }
        }
        public void WriteFloat(uint tagNumber, float? value)
        {
            if (!value.HasValue)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.Float);
                writer.Write(value.Value);
            }
        }
        public void WriteDouble(uint tagNumber, double? value)
        {
            if (!value.HasValue)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.Double);
                writer.Write(value.Value);
            }
        }

        public void WriteBool(uint tagNumber, bool? value)
        {
            if (!value.HasValue)
                WriteTag(tagNumber, ElementType.Null);
            else if (value.Value == true)
                WriteTag(tagNumber, ElementType.True);
            else
                WriteTag(tagNumber, ElementType.False);
        }

        public void WriteString(uint tagNumber, string? value)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                if (value.Length <= byte.MaxValue)
                {
                    WriteTag(tagNumber, ElementType.String8);
                    writer.Write((byte)Encoding.UTF8.GetByteCount(value));
                }
                else if (value.Length <= ushort.MaxValue)
                {
                    WriteTag(tagNumber, ElementType.String16);
                    writer.Write((ushort)Encoding.UTF8.GetByteCount(value));
                }
                else
                {
                    WriteTag(tagNumber, ElementType.String32);
                    writer.Write((uint)Encoding.UTF8.GetByteCount(value));
                }
                writer.Write(value);
            }
        }

        public void WriteBytes(uint tagNumber, byte[]? value, byte length = 0)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                if (value.Length <= byte.MaxValue)
                {
                    WriteTag(tagNumber, ElementType.Bytes8);
                    writer.Write((byte)value.Length);
                }
                else if (value.Length <= ushort.MaxValue)
                {
                    WriteTag(tagNumber, ElementType.Bytes16);
                    writer.Write((ushort)value.Length);
                }
                else
                {
                    WriteTag(tagNumber, ElementType.Bytes32);
                    writer.Write((uint)value.Length);
                }
                writer.Write(value);
            }
        }

        public void WriteAny(uint tagNumber, object? any)
        {
            if (any is TLVPayload payload)
                payload.Serialize(this, tagNumber);
            else if (any is Array array)
            {
                StartArray(tagNumber);
                foreach (object item in array)
                    WriteAny(0, item);
                EndContainer();
            }
            else if (any == null)
                WriteTag(tagNumber, ElementType.Null);
            else if (any is bool)
                WriteBool(tagNumber, (bool)any);
            else if (any is uint || any is ulong || any is ushort || any is byte)
                WriteULong(tagNumber, (ulong)any);
            else if (any is byte[])
                WriteBytes(tagNumber, (byte[])any, 4);
        }
    }
}
