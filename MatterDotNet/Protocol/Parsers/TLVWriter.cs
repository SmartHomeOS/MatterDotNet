using MatterDotNet.Protocol.Payloads;
using System.Text;

namespace MatterDotNet.Protocol.Parsers
{
    public class TLVWriter
    {
        PayloadWriter writer;
        TLVControl control;
        ElementType type;
        int offset;
        uint tagNumber;
        int length = 0;

        public TLVWriter(PayloadWriter writer)
        {
            this.writer = writer;
        }

        private void WriteTag(TLVControl control, ElementType type)
        {
            writer.Write(((byte)control << 5) | (byte)type);
        }

        private void WriteTag(uint tagNumber, ElementType type)
        {
            writer.Write(((byte)TLVControl.ContextSpecific << 5) | (byte)type);
            writer.Write((byte)tagNumber);
        }

        public void StartStructure()
        {
            WriteTag(TLVControl.Anonymous, ElementType.Structure);
        }

        public void StartStructure(uint tagNumber)
        {
            WriteTag(tagNumber, ElementType.Structure);
        }

        public void EndContainer()
        {
            WriteTag(TLVControl.Anonymous, ElementType.EndOfContainer);
        }

        public void WriteByte(uint tagNumber, byte? value)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.Byte);
                writer.Write(value.Value);
            }
        }
        public void WriteSByte(uint tagNumber, sbyte? value)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.SByte);
                writer.Write(value.Value);
            }
        }
        public void WriteShort(uint tagNumber, short? value)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.Short);
                writer.Write(value.Value);
            }
        }
        public void WriteUShort(uint tagNumber, ushort? value)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.UShort);
                writer.Write(value.Value);
            }
        }
        public void WriteInt(uint tagNumber, int? value)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.Int);
                writer.Write(value.Value);
            }
        }
        public void WriteUInt(uint tagNumber, uint? value)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.UInt);
                writer.Write(value.Value);
            }
        }
        public void WriteLong(uint tagNumber, long? value)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.Long);
                writer.Write(value.Value);
            }
        }
        public void WriteULong(uint tagNumber, ulong? value)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.ULong);
                writer.Write(value.Value);
            }
        }
        public void WriteFloat(uint tagNumber, float? value)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.Float);
                writer.Write(value.Value);
            }
        }
        public void WriteDouble(uint tagNumber, double? value)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.Double);
                writer.Write(value.Value);
            }
        }

        public void WriteBool(uint tagNumber, bool? value)
        {
            if (value == true)
                WriteTag(tagNumber, ElementType.True);
            else if (value == false)
                WriteTag(tagNumber, ElementType.False);
            else
                WriteTag(tagNumber, ElementType.Null);
        }

        public void WriteString(uint tagNumber, string? value, byte size)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.Double);
                switch (size)
                {
                    case 1:
                        writer.Write((byte)Encoding.UTF8.GetByteCount(value));
                        break;
                    case 2:
                        writer.Write((ushort)Encoding.UTF8.GetByteCount(value));
                        break;
                    case 4:
                        writer.Write((uint)Encoding.UTF8.GetByteCount(value));
                        break;
                }
                writer.Write(value);
            }
        }

        public void WriteBytes(uint tagNumber, byte[]? value, byte size)
        {
            if (value == null)
                WriteTag(tagNumber, ElementType.Null);
            else
            {
                WriteTag(tagNumber, ElementType.Double);
                switch (size)
                {
                    case 1:
                        writer.Write((byte)value.Length);
                        break;
                    case 2:
                        writer.Write((ushort)value.Length);
                        break;
                    case 4:
                        writer.Write((uint)value.Length);
                        break;
                }
                writer.Write(value);
            }
        }
    }
}
