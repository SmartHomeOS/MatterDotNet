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
        public ElectricalPowerMeasurement(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ElectricalPowerMeasurement(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum PowerMode : byte {
            /// <summary>
            /// 
            /// </summary>
            Unknown = 0x00,
            /// <summary>
            /// Direct current
            /// </summary>
            DC = 0x01,
            /// <summary>
            /// Alternating current, either single-phase or polyphase
            /// </summary>
            AC = 0x02,
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
        /// Get the Power Mode attribute
        /// </summary>
        public async Task<PowerMode> GetPowerMode(SecureSession session) {
            return (PowerMode)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Get the Number Of Measurement Types attribute
        /// </summary>
        public async Task<byte> GetNumberOfMeasurementTypes(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 1))!;
        }

        /// <summary>
        /// Get the Accuracy attribute
        /// </summary>
        public async Task<MeasurementAccuracy[]> GetAccuracy(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 2))!);
            MeasurementAccuracy[] list = new MeasurementAccuracy[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new MeasurementAccuracy(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Ranges attribute
        /// </summary>
        public async Task<MeasurementRange[]> GetRanges(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 3))!);
            MeasurementRange[] list = new MeasurementRange[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new MeasurementRange(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Voltage [mV] attribute
        /// </summary>
        public async Task<long?> GetVoltage(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 4, true);
        }

        /// <summary>
        /// Get the Active Current [mA] attribute
        /// </summary>
        public async Task<long?> GetActiveCurrent(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 5, true);
        }

        /// <summary>
        /// Get the Reactive Current [mA] attribute
        /// </summary>
        public async Task<long?> GetReactiveCurrent(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 6, true);
        }

        /// <summary>
        /// Get the Apparent Current [mA] attribute
        /// </summary>
        public async Task<long?> GetApparentCurrent(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 7, true);
        }

        /// <summary>
        /// Get the Active Power [mW] attribute
        /// </summary>
        public async Task<long?> GetActivePower(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 8, true);
        }

        /// <summary>
        /// Get the Reactive Power [mW] attribute
        /// </summary>
        public async Task<long?> GetReactivePower(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 9, true);
        }

        /// <summary>
        /// Get the Apparent Power [mW] attribute
        /// </summary>
        public async Task<long?> GetApparentPower(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 10, true);
        }

        /// <summary>
        /// Get the RMS Voltage [mV] attribute
        /// </summary>
        public async Task<long?> GetRMSVoltage(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 11, true);
        }

        /// <summary>
        /// Get the RMS Current [mA] attribute
        /// </summary>
        public async Task<long?> GetRMSCurrent(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 12, true);
        }

        /// <summary>
        /// Get the RMS Power [mW] attribute
        /// </summary>
        public async Task<long?> GetRMSPower(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 13, true);
        }

        /// <summary>
        /// Get the Frequency attribute
        /// </summary>
        public async Task<long?> GetFrequency(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 14, true);
        }

        /// <summary>
        /// Get the Harmonic Currents attribute
        /// </summary>
        public async Task<HarmonicMeasurement[]?> GetHarmonicCurrents(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 15))!);
            HarmonicMeasurement[] list = new HarmonicMeasurement[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new HarmonicMeasurement(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Harmonic Phases attribute
        /// </summary>
        public async Task<HarmonicMeasurement[]?> GetHarmonicPhases(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 16))!);
            HarmonicMeasurement[] list = new HarmonicMeasurement[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new HarmonicMeasurement(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Power Factor attribute
        /// </summary>
        public async Task<long?> GetPowerFactor(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 17, true);
        }

        /// <summary>
        /// Get the Neutral Current [mA] attribute
        /// </summary>
        public async Task<long?> GetNeutralCurrent(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 18, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Electrical Power Measurement";
        }
    }
}