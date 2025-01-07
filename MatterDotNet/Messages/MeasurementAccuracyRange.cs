// MatterDotNet Copyright (C) 2025 
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
//
// WARNING: This file was auto-generated. Do not edit.

using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Messages
{
    public record MeasurementAccuracyRange : TLVPayload
    {
        /// <inheritdoc />
        public MeasurementAccuracyRange() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public MeasurementAccuracyRange(Memory<byte> data) : this(new TLVReader(data)) {}

        public required long RangeMin { get; set; } 
        public required long RangeMax { get; set; } 
        public ushort? PercentMax { get; set; } 
        public ushort? PercentMin { get; set; } 
        public ushort? PercentTypical { get; set; } 
        public ulong? FixedMax { get; set; } 
        public ulong? FixedMin { get; set; } 
        public ulong? FixedTypical { get; set; } 

        [SetsRequiredMembers]
        internal MeasurementAccuracyRange(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            RangeMin = reader.GetLong(0)!.Value;
            RangeMax = reader.GetLong(1)!.Value;
            if (reader.IsTag(2))
                PercentMax = reader.GetUShort(2);
            if (reader.IsTag(3))
                PercentMin = reader.GetUShort(3);
            if (reader.IsTag(4))
                PercentTypical = reader.GetUShort(4);
            if (reader.IsTag(5))
                FixedMax = reader.GetULong(5);
            if (reader.IsTag(6))
                FixedMin = reader.GetULong(6);
            if (reader.IsTag(7))
                FixedTypical = reader.GetULong(7);
            reader.EndContainer();
        }

        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            writer.WriteLong(0, RangeMin);
            writer.WriteLong(1, RangeMax);
            if (PercentMax != null)
                writer.WriteUShort(2, PercentMax);
            if (PercentMin != null)
                writer.WriteUShort(3, PercentMin);
            if (PercentTypical != null)
                writer.WriteUShort(4, PercentTypical);
            if (FixedMax != null)
                writer.WriteULong(5, FixedMax);
            if (FixedMin != null)
                writer.WriteULong(6, FixedMin);
            if (FixedTypical != null)
                writer.WriteULong(7, FixedTypical);
            writer.EndContainer();
        }
    }
}