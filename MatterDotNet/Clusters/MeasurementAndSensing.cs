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

namespace MatterDotNet.Clusters
{
        /// <summary>
        /// Measurement Type
        /// </summary>
        public enum MeasurementType : ushort {
            Unspecified = 0x0,
            Voltage = 0x1,
            ActiveCurrent = 0x2,
            ReactiveCurrent = 0x3,
            ApparentCurrent = 0x4,
            ActivePower = 0x5,
            ReactivePower = 0x6,
            ApparentPower = 0x7,
            RMSVoltage = 0x8,
            RMSCurrent = 0x9,
            RMSPower = 0xA,
            Frequency = 0xB,
            PowerFactor = 0xC,
            NeutralCurrent = 0xD,
            ElectricalEnergy = 0xE,
        }
        /// <summary>
        /// Measurement Accuracy Range
        /// </summary>
        public record MeasurementAccuracyRange : TLVPayload {
            /// <summary>
            /// Measurement Accuracy Range
            /// </summary>
            public MeasurementAccuracyRange() { }

            /// <summary>
            /// Measurement Accuracy Range
            /// </summary>
            [SetsRequiredMembers]
            public MeasurementAccuracyRange(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                RangeMin = reader.GetLong(0)!.Value;
                RangeMax = reader.GetLong(1)!.Value;
                PercentMax = reader.GetUDecimal(2, true);
                PercentMin = reader.GetUDecimal(3, true);
                PercentTypical = reader.GetUDecimal(4, true);
                FixedMax = reader.GetULong(5, true);
                FixedMin = reader.GetULong(6, true);
                FixedTypical = reader.GetULong(7, true);
            }
            public required long RangeMin { get; set; }
            public required long RangeMax { get; set; }
            public decimal? PercentMax { get; set; }
            public decimal? PercentMin { get; set; }
            public decimal? PercentTypical { get; set; }
            public ulong? FixedMax { get; set; }
            public ulong? FixedMin { get; set; }
            public ulong? FixedTypical { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteLong(0, RangeMin, 4611686018427387904, -4611686018427387904);
                writer.WriteLong(1, RangeMax, 4611686018427387904, -4611686018427387904);
                if (PercentMax != null)
                    writer.WriteUDecimal(2, PercentMax);
                if (PercentMin != null)
                    writer.WriteUDecimal(3, PercentMin);
                if (PercentTypical != null)
                    writer.WriteUDecimal(4, PercentTypical);
                if (FixedMax != null)
                    writer.WriteULong(5, FixedMax, 4611686018427387903);
                if (FixedMin != null)
                    writer.WriteULong(6, FixedMin, 4611686018427387903);
                if (FixedTypical != null)
                    writer.WriteULong(7, FixedTypical, 4611686018427387903);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Measurement Accuracy
        /// </summary>
        public record MeasurementAccuracy : TLVPayload {
            /// <summary>
            /// Measurement Accuracy
            /// </summary>
            public MeasurementAccuracy() { }

            /// <summary>
            /// Measurement Accuracy
            /// </summary>
            [SetsRequiredMembers]
            public MeasurementAccuracy(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                MeasurementType = (MeasurementType)reader.GetUShort(0)!.Value;
                Measured = reader.GetBool(1)!.Value;
                MinMeasuredValue = reader.GetLong(2)!.Value;
                MaxMeasuredValue = reader.GetLong(3)!.Value;
                {
                    AccuracyRanges = new MeasurementAccuracyRange[reader.GetStruct(4)!.Length];
                    for (int n = 0; n < AccuracyRanges.Length; n++) {
                        AccuracyRanges[n] = new MeasurementAccuracyRange((object[])((object[])fields[4])[n]);
                    }
                }
            }
            public required MeasurementType MeasurementType { get; set; }
            public required bool Measured { get; set; } = false;
            public required long MinMeasuredValue { get; set; }
            public required long MaxMeasuredValue { get; set; }
            public required MeasurementAccuracyRange[] AccuracyRanges { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)MeasurementType);
                writer.WriteBool(1, Measured);
                writer.WriteLong(2, MinMeasuredValue, 4611686018427387904, -4611686018427387904);
                writer.WriteLong(3, MaxMeasuredValue, 4611686018427387904, -4611686018427387904);
                {
                    writer.StartArray(4);
                    foreach (var item in AccuracyRanges) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }
}