// MatterDotNet Copyright (C) 2024 
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Buffers.Binary;
using System.Text;

namespace MatterDotNet.Protocol.Parsers
{
    internal class TLVParser
    {
        /// <summary>
        /// Parses a binary TLV into an object tree
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public static object? Parse(Span<byte> buffer)
        {
            int offset = 0;
            TLVControl control = (TLVControl)(buffer[offset] >> 5);
            ElementType type = (ElementType)(0x1F & buffer[offset++]);
            return readObject(buffer, control, type, ref offset);
        }

        private static object? readObject(Span<byte> buffer, TLVControl control, ElementType type, ref int offset)
        {
            int len = 0;
            object o;
            byte valueType;
            switch (type)
            {
                case ElementType.True:
                    return true;
                case ElementType.False:
                    return false;
                case ElementType.Null:
                    return null;
                case ElementType.UnsignedByte:
                    return buffer[offset++];
                case ElementType.UnsignedShort:
                    o = BinaryPrimitives.ReadUInt16LittleEndian(buffer.Slice(offset, 2));
                    offset += 2;
                    return o;
                case ElementType.UnsignedInt:
                    o = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Slice(offset, 4));
                    offset += 4;
                    return o;
                case ElementType.UnsignedLong:
                    o = BinaryPrimitives.ReadUInt64LittleEndian(buffer.Slice(offset, 8));
                    offset += 8;
                    return o;
                case ElementType.SignedByte:
                    return (sbyte)buffer[offset++];
                case ElementType.SignedShort:
                    o = BinaryPrimitives.ReadInt16LittleEndian(buffer.Slice(offset, 2));
                    offset += 2;
                    return o;
                case ElementType.SignedInt:
                    o = BinaryPrimitives.ReadInt32LittleEndian(buffer.Slice(offset, 4));
                    offset += 4;
                    return o;
                case ElementType.SignedLong:
                    o = BinaryPrimitives.ReadInt64LittleEndian(buffer.Slice(offset, 8));
                    offset += 8;
                    return o;
                case ElementType.Float:
                    o = BinaryPrimitives.ReadSingleLittleEndian(buffer.Slice(offset, 4));
                    offset += 4;
                    return o;
                case ElementType.Double:
                    o = BinaryPrimitives.ReadDoubleLittleEndian(buffer.Slice(offset, 8));
                    offset += 8;
                    return o;
                case ElementType.ByteBytes:
                case ElementType.ShortBytes:
                case ElementType.IntBytes:
                case ElementType.LongBytes:
                    len = ReadLength(buffer, type, ref offset);
                    o = buffer.Slice(offset, len).ToArray();
                    offset += len;
                    return o;
                case ElementType.ByteString:
                case ElementType.ShortString:
                case ElementType.IntString:
                case ElementType.LongString:
                    len = ReadLength(buffer, type, ref offset);
                    o = Encoding.UTF8.GetString(buffer.Slice(offset, len));
                    offset += len;
                    return o;

                case ElementType.List:
                    throw new NotImplementedException("TODO Implement Lists");
                case ElementType.Array:
                    o = new List<object?>();
                    valueType = buffer[offset++];
                    while ((ElementType)(0x1F & valueType) != ElementType.EndOfContainer)
                    {
                        object? val = readObject(buffer, (TLVControl)(valueType >> 5), (ElementType)(0x1F & valueType), ref offset);
                        ((List<object?>)o).Add(val);
                    }
                    return o;
                case ElementType.Structure:
                    o = new Dictionary<byte, object?>();
                    valueType = buffer[offset++];
                    while ((ElementType)(0x1F & valueType) != ElementType.EndOfContainer)
                    {
                        byte key = buffer[offset++];
                        object? val = readObject(buffer, (TLVControl)(valueType >> 5), (ElementType)(0x1F & valueType), ref offset);
                        ((Dictionary<byte, object?>)o).TryAdd(key, val);
                    }
                    return o;
            }
            throw new NotImplementedException($"Unknown Property Type {type}");
        }

        private static int ReadLength(Span<byte> span, ElementType type, ref int offset)
        {
            int ret;
            byte offsetSize = (byte)(1 << ((byte)type & 0x3));
            if (offsetSize == 1)
                return span[offset++];
            else if (offsetSize == 2)
            {
                ret = BinaryPrimitives.ReadUInt16LittleEndian(span.Slice(offset, 2));
                offset += 2;
                return ret;
            }
            else if (offsetSize == 4)
            {
                ret = (int)BinaryPrimitives.ReadUInt32LittleEndian(span.Slice(offset, 4));
                offset += 4;
                return ret;
            }
            else
                throw new InvalidDataException("Long strings are not supported");
        }
    }
}
