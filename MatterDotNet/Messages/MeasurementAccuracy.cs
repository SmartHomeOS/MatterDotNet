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
    public record MeasurementAccuracy : TLVPayload
    {
        /// <inheritdoc />
        public MeasurementAccuracy() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public MeasurementAccuracy(object[] fields)
        {
            FieldReader reader = new FieldReader(fields);
            MeasurementType = (MeasurementType)reader.GetUShort(0)!.Value;
            Measured = reader.GetBool(1)!.Value;
            MinMeasuredValue = reader.GetByte(2)!.Value;
            if (reader.Has(2))
                MinMeasuredValue = reader.GetLong(2, true);
            if (reader.Has(3))
                MinMeasuredValue = reader.GetLong(3, true);
        }

        /// <inheritdoc />
        [SetsRequiredMembers]
        public MeasurementAccuracy(Memory<byte> data) : this(new TLVReader(data)) {}

        public required MeasurementType MeasurementType { get; set; } 
        public required bool Measured { get; set; } 
        public long? MinMeasuredValue { get; set; } 
        public long? MaxMeasuredValue { get; set; } 
        public List<MeasurementAccuracyRange>? AccuracyRanges { get; set; } 

        [SetsRequiredMembers]
        internal MeasurementAccuracy(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            MeasurementType = (MeasurementType)reader.GetUShort(0)!.Value;
            Measured = reader.GetBool(1)!.Value;
            if (reader.IsTag(2))
                MinMeasuredValue = reader.GetLong(2);
            if (reader.IsTag(3))
                MaxMeasuredValue = reader.GetLong(3);
            if (reader.IsTag(4))
            {
                reader.StartList(4);
                AccuracyRanges = new();
                while (!reader.IsEndContainer()) {
                    AccuracyRanges.Add(new MeasurementAccuracyRange(reader, -1));
                }
                reader.EndContainer();
            }
            reader.EndContainer();
        }

        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            writer.WriteUShort(0, (ushort)MeasurementType);
            writer.WriteBool(1, Measured);
            if (MinMeasuredValue != null)
                writer.WriteLong(2, MinMeasuredValue);
            if (MaxMeasuredValue != null)
                writer.WriteLong(3, MaxMeasuredValue);
            if (AccuracyRanges != null)
            {
                Constrain(AccuracyRanges, 1);
                writer.StartList(4);
                foreach (var item in AccuracyRanges) {
                    item.Serialize(writer, -1);
                }
                writer.EndContainer();
            }
            writer.EndContainer();
        }
    }
}