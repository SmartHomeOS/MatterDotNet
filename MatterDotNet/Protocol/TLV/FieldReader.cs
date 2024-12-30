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

namespace MatterDotNet.Protocol.Parsers
{
    public class FieldReader(object[] fields)
    {
        public byte? GetByte(long tagNumber, bool nullable = false)
        {
            if (fields.Length <= tagNumber)
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (fields[tagNumber] == null && nullable)
                return null;
            if (fields[tagNumber] is byte value)
                return value;
            throw new InvalidDataException($"Tag {tagNumber}: Expected type byte but received {fields[tagNumber].GetType()}");
        }
        public sbyte? GetSByte(long tagNumber, bool nullable = false)
        {
            if (fields.Length <= tagNumber)
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (fields[tagNumber] == null && nullable)
                return null;
            if (fields[tagNumber] is sbyte value)
                return value;
            throw new InvalidDataException($"Tag {tagNumber}: Expected type sbyte but received {fields[tagNumber].GetType()}");
        }
        public bool? GetBool(long tagNumber, bool nullable = false)
        {
            if (fields.Length <= tagNumber)
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (fields[tagNumber] == null && nullable)
                return null;
            if (fields[tagNumber] is bool value)
                return value;
            throw new InvalidDataException($"Tag {tagNumber}: Expected type bool but received {fields[tagNumber].GetType()}");
        }
        public short? GetShort(long tagNumber, bool nullable = false)
        {
            if (fields.Length <= tagNumber)
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (fields[tagNumber] == null && nullable)
                return null;
            if (fields[tagNumber] is sbyte smallerVal)
                return smallerVal;
            if (fields[tagNumber] is short value)
                return value;
            throw new InvalidDataException($"Tag {tagNumber}: Expected type short but received {fields[tagNumber].GetType()}");
        }

        public ushort? GetUShort(long tagNumber, bool nullable = false)
        {
            if (fields.Length <= tagNumber)
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (fields[tagNumber] == null && nullable)
                return null;
            if (fields[tagNumber] is byte smallerVal)
                return smallerVal;
            if (fields[tagNumber] is ushort value)
                return value;
            throw new InvalidDataException($"Tag {tagNumber}: Expected type ushort but received {fields[tagNumber].GetType()}");
        }

        public int? GetInt(long tagNumber, bool nullable = false)
        {
            if (fields.Length <= tagNumber)
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (fields[tagNumber] == null && nullable)
                return null;
            if (fields[tagNumber] is sbyte smallestVal)
                return smallestVal;
            if (fields[tagNumber] is short smallerVal)
                return smallerVal;
            if (fields[tagNumber] is int value)
                return value;
            throw new InvalidDataException($"Tag {tagNumber}: Expected type int but received {fields[tagNumber].GetType()}");
        }

        public uint? GetUInt(long tagNumber, bool nullable = false)
        {
            if (fields.Length <= tagNumber)
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (fields[tagNumber] == null && nullable)
                return null;
            if (fields[tagNumber] is byte smallestVal)
                return smallestVal;
            if (fields[tagNumber] is ushort smallerVal)
                return smallerVal;
            if (fields[tagNumber] is uint value)
                return value;
            throw new InvalidDataException($"Tag {tagNumber}: Expected type uint but received {fields[tagNumber].GetType()}");
        }

        public long? GetLong(long tagNumber, bool nullable = false)
        {
            if (fields.Length <= tagNumber)
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (fields[tagNumber] == null && nullable)
                return null;
            if (fields[tagNumber] is sbyte smallestVal)
                return smallestVal;
            if (fields[tagNumber] is short smallerVal)
                return smallerVal;
            if (fields[tagNumber] is int smallVal)
                return smallVal;
            if (fields[tagNumber] is long value)
                return value;
            throw new InvalidDataException($"Tag {tagNumber}: Expected type long but received {fields[tagNumber].GetType()}");
        }

        public ulong? GetULong(long tagNumber, bool nullable = false)
        {
            if (fields.Length <= tagNumber)
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (fields[tagNumber] == null && nullable)
                return null;
            if (fields[tagNumber] is byte smallestVal)
                return smallestVal;
            if (fields[tagNumber] is ushort smallerVal)
                return smallerVal;
            if (fields[tagNumber] is uint smallVal)
                return smallVal;
            if (fields[tagNumber] is ulong value)
                return value;
            throw new InvalidDataException($"Tag {tagNumber}: Expected type ulong but received {fields[tagNumber].GetType()}");
        }

        public float? GetFloat(long tagNumber, bool nullable = false)
        {
            if (fields.Length <= tagNumber)
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (fields[tagNumber] == null && nullable)
                return null;
            if (fields[tagNumber] is float value)
                return value;
            throw new InvalidDataException($"Tag {tagNumber}: Expected type float but received {fields[tagNumber].GetType()}");
        }

        public double? GetDouble(long tagNumber, bool nullable = false)
        {
            if (fields.Length <= tagNumber)
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (fields[tagNumber] == null && nullable)
                return null;
            if (fields[tagNumber] is double value)
                return value;
            throw new InvalidDataException($"Tag {tagNumber}: Expected type double but received {fields[tagNumber].GetType()}");
        }

        public string? GetString(long tagNumber, bool nullable = false, int maxLength = int.MaxValue, int minLength = 0)
        {
            if (fields.Length <= tagNumber)
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (fields[tagNumber] == null && nullable)
                return null;
            if (fields[tagNumber] is not string value)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type string but received {fields[tagNumber].GetType()}");
            if (value.Length > maxLength)
                throw new InvalidDataException($"Constraint Violation! Max length is {maxLength} but received {value.Length}");
            if (value.Length < minLength)
                throw new InvalidDataException($"Constraint Violation! Min length is {minLength} but received {value.Length}");
            return value;
        }

        public byte[]? GetBytes(long tagNumber, bool nullable = false, int maxLength = int.MaxValue, int minLength = 0)
        {
            if (fields.Length <= tagNumber)
                throw new InvalidDataException("Tag " + tagNumber + " not present");
            if (fields[tagNumber] == null && nullable)
                return null;
            if (fields[tagNumber] is not byte[] value)
                throw new InvalidDataException($"Tag {tagNumber}: Expected type byte[] but received {fields[tagNumber].GetType()}");
            if (value.Length > maxLength)
                throw new InvalidDataException($"Constraint Violation! Max length is {maxLength} but received {value.Length}");
            if (value.Length < minLength)
                throw new InvalidDataException($"Constraint Violation! Min length is {minLength} but received {value.Length}");
            return value;
        }
    }
}
