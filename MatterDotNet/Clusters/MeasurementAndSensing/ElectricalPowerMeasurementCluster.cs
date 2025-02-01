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

using MatterDotNet.Attributes;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.MeasurementAndSensing
{
    /// <summary>
    /// This cluster provides a mechanism for querying data about electrical power as measured by the server.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ElectricalPowerMeasurement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0090;

        /// <summary>
        /// This cluster provides a mechanism for querying data about electrical power as measured by the server.
        /// </summary>
        [SetsRequiredMembers]
        public ElectricalPowerMeasurement(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ElectricalPowerMeasurement(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            PowerMode = new ReadAttribute<PowerModeEnum>(cluster, endPoint, 0) {
                Deserialize = x => (PowerModeEnum)DeserializeEnum(x)!
            };
            NumberOfMeasurementTypes = new ReadAttribute<byte>(cluster, endPoint, 1) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            Accuracy = new ReadAttribute<MeasurementAccuracy[]>(cluster, endPoint, 2) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    MeasurementAccuracy[] list = new MeasurementAccuracy[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new MeasurementAccuracy(reader.GetStruct(i)!);
                    return list;
                }
            };
            Ranges = new ReadAttribute<MeasurementRange[]>(cluster, endPoint, 3) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    MeasurementRange[] list = new MeasurementRange[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new MeasurementRange(reader.GetStruct(i)!);
                    return list;
                }
            };
            Voltage = new ReadAttribute<long?>(cluster, endPoint, 4, true) {
                Deserialize = x => (long?)(dynamic?)x
            };
            ActiveCurrent = new ReadAttribute<long?>(cluster, endPoint, 5, true) {
                Deserialize = x => (long?)(dynamic?)x
            };
            ReactiveCurrent = new ReadAttribute<long?>(cluster, endPoint, 6, true) {
                Deserialize = x => (long?)(dynamic?)x
            };
            ApparentCurrent = new ReadAttribute<long?>(cluster, endPoint, 7, true) {
                Deserialize = x => (long?)(dynamic?)x
            };
            ActivePower = new ReadAttribute<long?>(cluster, endPoint, 8, true) {
                Deserialize = x => (long?)(dynamic?)x
            };
            ReactivePower = new ReadAttribute<long?>(cluster, endPoint, 9, true) {
                Deserialize = x => (long?)(dynamic?)x
            };
            ApparentPower = new ReadAttribute<long?>(cluster, endPoint, 10, true) {
                Deserialize = x => (long?)(dynamic?)x
            };
            RMSVoltage = new ReadAttribute<long?>(cluster, endPoint, 11, true) {
                Deserialize = x => (long?)(dynamic?)x
            };
            RMSCurrent = new ReadAttribute<long?>(cluster, endPoint, 12, true) {
                Deserialize = x => (long?)(dynamic?)x
            };
            RMSPower = new ReadAttribute<long?>(cluster, endPoint, 13, true) {
                Deserialize = x => (long?)(dynamic?)x
            };
            Frequency = new ReadAttribute<long?>(cluster, endPoint, 14, true) {
                Deserialize = x => (long?)(dynamic?)x
            };
            HarmonicCurrents = new ReadAttribute<HarmonicMeasurement[]?>(cluster, endPoint, 15, true) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    HarmonicMeasurement[] list = new HarmonicMeasurement[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new HarmonicMeasurement(reader.GetStruct(i)!);
                    return list;
                }
            };
            HarmonicPhases = new ReadAttribute<HarmonicMeasurement[]?>(cluster, endPoint, 16, true) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    HarmonicMeasurement[] list = new HarmonicMeasurement[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new HarmonicMeasurement(reader.GetStruct(i)!);
                    return list;
                }
            };
            PowerFactor = new ReadAttribute<long?>(cluster, endPoint, 17, true) {
                Deserialize = x => (long?)(dynamic?)x
            };
            NeutralCurrent = new ReadAttribute<long?>(cluster, endPoint, 18, true) {
                Deserialize = x => (long?)(dynamic?)x
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports measurement of direct current
            /// </summary>
            DirectCurrent = 1,
            /// <summary>
            /// Supports measurement of alternating current
            /// </summary>
            AlternatingCurrent = 2,
            /// <summary>
            /// Supports polyphase measurements
            /// </summary>
            PolyphasePower = 4,
            /// <summary>
            /// Supports measurement of AC harmonics
            /// </summary>
            Harmonics = 8,
            /// <summary>
            /// Supports measurement of AC harmonic phases
            /// </summary>
            PowerQuality = 16,
        }

        /// <summary>
        /// Power Mode
        /// </summary>
        public enum PowerModeEnum : byte {
            /// <summary>
            /// 
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// Direct current
            /// </summary>
            DC = 1,
            /// <summary>
            /// Alternating current, either single-phase or polyphase
            /// </summary>
            AC = 2,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Measurement Range
        /// </summary>
        public record MeasurementRange : TLVPayload {
            /// <summary>
            /// Measurement Range
            /// </summary>
            public MeasurementRange() { }

            /// <summary>
            /// Measurement Range
            /// </summary>
            [SetsRequiredMembers]
            public MeasurementRange(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                MeasurementType = (MeasurementType)reader.GetUShort(0)!.Value;
                Min = reader.GetLong(1)!.Value;
                Max = reader.GetLong(2)!.Value;
                StartTimestamp = TimeUtil.FromEpochSeconds(reader.GetUInt(3, true));
                EndTimestamp = TimeUtil.FromEpochSeconds(reader.GetUInt(4, true));
                MinTimestamp = TimeUtil.FromEpochSeconds(reader.GetUInt(5, true));
                MaxTimestamp = TimeUtil.FromEpochSeconds(reader.GetUInt(6, true));
                StartSystime = TimeUtil.FromMillis(reader.GetULong(7, true));
                EndSystime = TimeUtil.FromMillis(reader.GetULong(8, true));
                MinSystime = TimeUtil.FromMillis(reader.GetULong(9, true));
                MaxSystime = TimeUtil.FromMillis(reader.GetULong(10, true));
            }
            public required MeasurementType MeasurementType { get; set; }
            public required long Min { get; set; }
            public required long Max { get; set; }
            public DateTime? StartTimestamp { get; set; }
            public DateTime? EndTimestamp { get; set; }
            public DateTime? MinTimestamp { get; set; }
            public DateTime? MaxTimestamp { get; set; }
            public TimeSpan? StartSystime { get; set; }
            public TimeSpan? EndSystime { get; set; }
            public TimeSpan? MinSystime { get; set; }
            public TimeSpan? MaxSystime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)MeasurementType);
                writer.WriteLong(1, Min, 4611686018427387904, -4611686018427387904);
                writer.WriteLong(2, Max, 4611686018427387904, -4611686018427387904);
                if (StartTimestamp != null)
                    writer.WriteUInt(3, TimeUtil.ToEpochSeconds(StartTimestamp!.Value));
                if (EndTimestamp != null)
                    writer.WriteUInt(4, TimeUtil.ToEpochSeconds(EndTimestamp!.Value));
                if (MinTimestamp != null)
                    writer.WriteUInt(5, TimeUtil.ToEpochSeconds(MinTimestamp!.Value));
                if (MaxTimestamp != null)
                    writer.WriteUInt(6, TimeUtil.ToEpochSeconds(MaxTimestamp!.Value));
                if (StartSystime != null)
                    writer.WriteULong(7, (ulong)StartSystime!.Value.TotalMilliseconds);
                if (EndSystime != null)
                    writer.WriteULong(8, (ulong)EndSystime!.Value.TotalMilliseconds);
                if (MinSystime != null)
                    writer.WriteULong(9, (ulong)MinSystime!.Value.TotalMilliseconds);
                if (MaxSystime != null)
                    writer.WriteULong(10, (ulong)MaxSystime!.Value.TotalMilliseconds);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Harmonic Measurement
        /// </summary>
        public record HarmonicMeasurement : TLVPayload {
            /// <summary>
            /// Harmonic Measurement
            /// </summary>
            public HarmonicMeasurement() { }

            /// <summary>
            /// Harmonic Measurement
            /// </summary>
            [SetsRequiredMembers]
            public HarmonicMeasurement(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Order = reader.GetByte(0)!.Value;
                Measurement = reader.GetLong(1, true);
            }
            public required byte Order { get; set; } = 1;
            public required long? Measurement { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Order, byte.MaxValue, 1);
                writer.WriteLong(1, Measurement, 4611686018427387904, -4611686018427387904);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Attributes
        /// <summary>
        /// Features supported by this cluster
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task<Feature> GetSupportedFeatures(SecureSession session)
        {
            return (Feature)(byte)(await GetAttribute(session, 0xFFFC))!;
        }

        /// <summary>
        /// Returns true when the feature is supported by the cluster
        /// </summary>
        /// <param name="session"></param>
        /// <param name="feature"></param>
        /// <returns></returns>
        public async Task<bool> Supports(SecureSession session, Feature feature)
        {
            return ((feature & await GetSupportedFeatures(session)) != 0);
        }

        /// <summary>
        /// Power Mode Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<PowerModeEnum> PowerMode { get; init; }

        /// <summary>
        /// Number Of Measurement Types Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfMeasurementTypes { get; init; }

        /// <summary>
        /// Accuracy Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<MeasurementAccuracy[]> Accuracy { get; init; }

        /// <summary>
        /// Ranges Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<MeasurementRange[]> Ranges { get; init; }

        /// <summary>
        /// Voltage [mV] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long?> Voltage { get; init; }

        /// <summary>
        /// Active Current [mA] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long?> ActiveCurrent { get; init; }

        /// <summary>
        /// Reactive Current [mA] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long?> ReactiveCurrent { get; init; }

        /// <summary>
        /// Apparent Current [mA] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long?> ApparentCurrent { get; init; }

        /// <summary>
        /// Active Power [mW] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long?> ActivePower { get; init; }

        /// <summary>
        /// Reactive Power [mW] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long?> ReactivePower { get; init; }

        /// <summary>
        /// Apparent Power [mW] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long?> ApparentPower { get; init; }

        /// <summary>
        /// RMS Voltage [mV] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long?> RMSVoltage { get; init; }

        /// <summary>
        /// RMS Current [mA] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long?> RMSCurrent { get; init; }

        /// <summary>
        /// RMS Power [mW] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long?> RMSPower { get; init; }

        /// <summary>
        /// Frequency Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long?> Frequency { get; init; }

        /// <summary>
        /// Harmonic Currents Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<HarmonicMeasurement[]?> HarmonicCurrents { get; init; }

        /// <summary>
        /// Harmonic Phases Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<HarmonicMeasurement[]?> HarmonicPhases { get; init; }

        /// <summary>
        /// Power Factor Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long?> PowerFactor { get; init; }

        /// <summary>
        /// Neutral Current [mA] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long?> NeutralCurrent { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Electrical Power Measurement";
        }
    }
}