﻿using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatterDotNet.Protocol.Parsers
{
    public class TLVReader
    {
        Memory<byte> data;
        TLVControl control;
        ElementType type;
        int offset;
        ushort vendorID;
        ushort profileNumber;
        uint tagNumber;
        int length = 0;

        public TLVReader(Memory<byte> data)
        {
            this.data = data;
            if (!ReadTag())
                throw new EndOfStreamException("Payload is empty");
        }

        public bool IsTag(uint tagNumber)
        {
            return this.tagNumber == tagNumber;
        }

        public bool IsTag(uint tagNumber, ushort vendorID, ushort profileNumber)
        {
            return this.tagNumber == tagNumber && this.vendorID == vendorID && this.profileNumber == profileNumber;
        }

        public bool IsEndContainer()
        {
            return this.type == ElementType.EndOfContainer;
        }

        public void StartStructure(uint structureNumber = 0)
        {
            if (type != ElementType.Structure)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type structure but received {type}");
            if (structureNumber != 0 && !IsTag(structureNumber))
                throw new InvalidDataException("Tag " + structureNumber + " not present");
            ReadTag();
        }

        public void StartArray(uint arrayNumber = 0)
        {
            if (type != ElementType.Array)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type array but received {type}");
            if (arrayNumber != 0 && !IsTag(arrayNumber))
                throw new InvalidDataException("Tag " + arrayNumber + " not present");
            ReadTag();
        }

        public void StartList(uint listNumber = 0)
        {
            if (type != ElementType.List)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type list but received {type}");
            if (listNumber != 0 && !IsTag(listNumber))
                throw new InvalidDataException("Tag " + listNumber + " not present");
            ReadTag();
        }

        public void EndContainer()
        {
            bool iterating = true;
            while (iterating && type != ElementType.EndOfContainer)
                iterating = ReadTag(); //TODO: This doesn't actually iterate
            if (iterating == true)
                ReadTag();
            else
                throw new InvalidDataException("End structure was not found");
        }

        public byte? GetByte(uint tagNumber, bool nullable = false)
        {
            if (!IsTag(tagNumber))
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (type == ElementType.Null && nullable)
                return null;
            if (type != ElementType.Byte)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type byte but received {type}");
            byte val = data.Span[offset++];
            ReadTag();
            return val;
        }
        public sbyte? GetSByte(uint tagNumber, bool nullable = false)
        {
            if (!IsTag(tagNumber))
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (type == ElementType.Null && nullable)
                return null;
            if (type != ElementType.SByte)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type sbyte but received {type}");
            sbyte val = (sbyte)data.Span[offset++];
            ReadTag();
            return val;
        }
        public bool? GetBool(uint tagNumber, bool nullable = false)
        {
            if (!IsTag(tagNumber))
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (type == ElementType.Null && nullable)
                return null;
            if (type != ElementType.True && type != ElementType.False)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type bool but received {type}");
            bool val = (type == ElementType.True);
            ReadTag();
            return val;
        }
        public short? GetShort(uint tagNumber, bool nullable = false)
        {
            if (!IsTag(tagNumber))
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (type == ElementType.Null && nullable)
                return null;
            if (type == ElementType.SByte)
                return GetSByte(tagNumber, nullable);
            if (type != ElementType.Short)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type short but received {type}");
            short val = BinaryPrimitives.ReadInt16LittleEndian(data.Slice(offset, 2).Span);
            offset += 2;
            ReadTag();
            return val;
        }

        public ushort? GetUShort(uint tagNumber, bool nullable = false)
        {
            if (!IsTag(tagNumber))
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (type == ElementType.Null && nullable)
                return null;
            if (type == ElementType.Byte)
                return GetByte(tagNumber, nullable);
            if (type != ElementType.UShort)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type ushort but received {type}");
            ushort val = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(offset, 2).Span);
            offset += 2;
            ReadTag();
            return val;
        }

        public int? GetInt(uint tagNumber, bool nullable = false)
        {
            if (!IsTag(tagNumber))
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (type == ElementType.Null && nullable)
                return null;
            if (type == ElementType.SByte)
                return GetSByte(tagNumber, nullable);
            if (type == ElementType.Short)
                return GetShort(tagNumber, nullable);
            if (type != ElementType.Int)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type int but received {type}");
            int val = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(offset, 4).Span);
            offset += 4;
            ReadTag();
            return val;
        }

        public uint? GetUInt(uint tagNumber, bool nullable = false)
        {
            if (!IsTag(tagNumber))
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (type == ElementType.Null && nullable)
                return null;
            if (type == ElementType.Byte)
                return GetByte(tagNumber, nullable);
            if (type == ElementType.UShort)
                return GetUShort(tagNumber, nullable);
            if (type != ElementType.UInt)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type uint but received {type}");
            uint val = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(offset, 4).Span);
            offset += 4;
            ReadTag();
            return val;
        }

        public long? GetLong(uint tagNumber, bool nullable = false)
        {
            if (!IsTag(tagNumber))
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (type == ElementType.Null && nullable)
                return null;
            if (type == ElementType.SByte)
                return GetSByte(tagNumber, nullable);
            if (type == ElementType.Short)
                return GetShort(tagNumber, nullable);
            if (type == ElementType.Int)
                return GetInt(tagNumber, nullable);
            if (type != ElementType.Long)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type long but received {type}");
            long val = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(offset, 8).Span);
            offset += 8;
            ReadTag();
            return val;
        }

        public ulong? GetULong(uint tagNumber, bool nullable = false)
        {
            if (!IsTag(tagNumber))
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (type == ElementType.Null && nullable)
                return null;
            if (type == ElementType.Byte)
                return GetByte(tagNumber, nullable);
            if (type == ElementType.UShort)
                return GetUShort(tagNumber, nullable);
            if (type == ElementType.UInt)
                return GetUInt(tagNumber, nullable);
            if (type != ElementType.ULong)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type ulong but received {type}");
            ulong val = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(offset, 8).Span);
            offset += 8;
            ReadTag();
            return val;
        }

        public float? GetFloat(uint tagNumber, bool nullable = false)
        {
            if (!IsTag(tagNumber))
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (type == ElementType.Null && nullable)
                return null;
            if (type != ElementType.Float)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type float but received {type}");
            float val = BinaryPrimitives.ReadSingleLittleEndian(data.Slice(offset, 4).Span);
            offset += 4;
            ReadTag();
            return val;
        }

        public double? GetDouble(uint tagNumber, bool nullable = false)
        {
            if (!IsTag(tagNumber))
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (type == ElementType.Null && nullable)
                return null;
            if (type != ElementType.Double)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type double but received {type}");
            double val = BinaryPrimitives.ReadDoubleLittleEndian(data.Slice(offset, 4).Span);
            offset += 4;
            ReadTag();
            return val;
        }

        public string? GetString(uint tagNumber, bool nullable = false)
        {
            if (!IsTag(tagNumber))
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (type == ElementType.Null && nullable)
                return null;
            if (type != ElementType.String8 && type != ElementType.String16 && type != ElementType.String32)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type string but received {type}");
            string val = Encoding.UTF8.GetString(data.Slice(offset, length).Span);
            offset += length;
            ReadTag();
            return val;
        }

        public byte[]? GetBytes(uint tagNumber, bool nullable = false)
        {
            if (!IsTag(tagNumber))
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (type == ElementType.Null && nullable)
                return null;
            if (type != ElementType.Bytes8 && type != ElementType.Bytes16 && type != ElementType.Bytes32)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type bytes but received {type}");
            byte[] val = data.Slice(offset, length).ToArray();
            offset += length;
            ReadTag();
            return val;
        }

        private bool ReadTag()
        {
            if (offset == data.Length)
                return false;
            control = (TLVControl)(data.Span[offset] >> 5);
            type = (ElementType)(0x1F & data.Span[offset++]);
            switch (control)
            {
                case TLVControl.ContextSpecific:
                    tagNumber = data.Span[offset++];
                    break;
                case TLVControl.CommonProfileShort:
                case TLVControl.ImplicitProfileShort:
                    tagNumber = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(offset, 2).Span);
                    offset += 2;
                    break;
                case TLVControl.CommonProfileInt:
                case TLVControl.ImplicitProfileInt:
                    tagNumber = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(offset, 4).Span);
                    offset += 4;
                    break;
                case TLVControl.FullyQualifiedInt:
                case TLVControl.FullyQualifiedShort:
                    vendorID = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(offset, 2).Span);
                    offset += 2;
                    profileNumber = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(offset, 2).Span);
                    offset += 2;
                    if (control == TLVControl.FullyQualifiedShort)
                    {
                        tagNumber = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(offset, 2).Span);
                        offset += 2;
                    }
                    else
                    {
                        tagNumber = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(offset, 4).Span);
                        offset += 4;
                    }
                    break;
                case TLVControl.Anonymous:
                    tagNumber = 0;
                    break;
            }
            switch (type)
            {
                case ElementType.Byte:
                case ElementType.SByte:
                    length = 1;
                    break;
                case ElementType.Short:
                case ElementType.UShort:
                    length = 2;
                    break;
                case ElementType.Int:
                case ElementType.UInt:
                case ElementType.Float:
                    length = 4;
                    break;
                case ElementType.Long:
                case ElementType.ULong:
                case ElementType.Double:
                    length = 8;
                    break;
                case ElementType.String8:
                case ElementType.String16:
                case ElementType.String32:
                case ElementType.String64:
                case ElementType.Bytes8:
                case ElementType.Bytes16:
                case ElementType.Bytes32:
                case ElementType.Bytes64:
                    byte offsetSize = (byte)(1 << ((byte)type & 0x3));
                    if (offsetSize == 1)
                        length = data.Span[offset++];
                    else if (offsetSize == 2)
                    {
                        length = BinaryPrimitives.ReadUInt16LittleEndian(data.Span.Slice(offset, 2));
                        offset += 2;
                    }
                    else if (offsetSize == 4)
                    {
                        length = (int)BinaryPrimitives.ReadUInt32LittleEndian(data.Span.Slice(offset, 4));
                        offset += 4;
                    }
                    else
                        throw new InvalidDataException("Long strings are not supported");
                    break;
            }
            return true;
        }
    }
}