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
    /// This cluster provides a mechanism for querying data about the electrical energy imported or provided by the server.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ElectricalEnergyMeasurement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0091;

        /// <summary>
        /// This cluster provides a mechanism for querying data about the electrical energy imported or provided by the server.
        /// </summary>
        [SetsRequiredMembers]
        public ElectricalEnergyMeasurement(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ElectricalEnergyMeasurement(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            Accuracy = new ReadAttribute<MeasurementAccuracy>(cluster, endPoint, 0) {
                Deserialize = x => (MeasurementAccuracy)(dynamic?)x!
            };
            CumulativeEnergyImported = new ReadAttribute<EnergyMeasurement?>(cluster, endPoint, 1, true) {
                Deserialize = x => new EnergyMeasurement((object[])x!)
            };
            CumulativeEnergyExported = new ReadAttribute<EnergyMeasurement?>(cluster, endPoint, 2, true) {
                Deserialize = x => new EnergyMeasurement((object[])x!)
            };
            PeriodicEnergyImported = new ReadAttribute<EnergyMeasurement?>(cluster, endPoint, 3, true) {
                Deserialize = x => new EnergyMeasurement((object[])x!)
            };
            PeriodicEnergyExported = new ReadAttribute<EnergyMeasurement?>(cluster, endPoint, 4, true) {
                Deserialize = x => new EnergyMeasurement((object[])x!)
            };
            CumulativeEnergyReset = new ReadAttribute<CumulativeEnergyResetStruct?>(cluster, endPoint, 5, true) {
                Deserialize = x => new CumulativeEnergyResetStruct((object[])x!)
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Measurement of energy imported by the server
            /// </summary>
            ImportedEnergy = 1,
            /// <summary>
            /// Measurement of energy provided by the server
            /// </summary>
            ExportedEnergy = 2,
            /// <summary>
            /// Measurements are cumulative
            /// </summary>
            CumulativeEnergy = 4,
            /// <summary>
            /// Measurements are periodic
            /// </summary>
            PeriodicEnergy = 8,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Cumulative Energy Reset
        /// </summary>
        public record CumulativeEnergyResetStruct : TLVPayload {
            /// <summary>
            /// Cumulative Energy Reset
            /// </summary>
            public CumulativeEnergyResetStruct() { }

            /// <summary>
            /// Cumulative Energy Reset
            /// </summary>
            [SetsRequiredMembers]
            public CumulativeEnergyResetStruct(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ImportedResetTimestamp = TimeUtil.FromEpochSeconds(reader.GetUInt(0, true));
                ExportedResetTimestamp = TimeUtil.FromEpochSeconds(reader.GetUInt(1, true));
                ImportedResetSystime = TimeUtil.FromMillis(reader.GetULong(2, true));
                ExportedResetSystime = TimeUtil.FromMillis(reader.GetULong(3, true));
            }
            public DateTime? ImportedResetTimestamp { get; set; }
            public DateTime? ExportedResetTimestamp { get; set; }
            public TimeSpan? ImportedResetSystime { get; set; }
            public TimeSpan? ExportedResetSystime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (ImportedResetTimestamp != null)
                    writer.WriteUInt(0, TimeUtil.ToEpochSeconds(ImportedResetTimestamp!.Value));
                if (ExportedResetTimestamp != null)
                    writer.WriteUInt(1, TimeUtil.ToEpochSeconds(ExportedResetTimestamp!.Value));
                if (ImportedResetSystime != null)
                    writer.WriteULong(2, (ulong)ImportedResetSystime!.Value.TotalMilliseconds);
                if (ExportedResetSystime != null)
                    writer.WriteULong(3, (ulong)ExportedResetSystime!.Value.TotalMilliseconds);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Energy Measurement
        /// </summary>
        public record EnergyMeasurement : TLVPayload {
            /// <summary>
            /// Energy Measurement
            /// </summary>
            public EnergyMeasurement() { }

            /// <summary>
            /// Energy Measurement
            /// </summary>
            [SetsRequiredMembers]
            public EnergyMeasurement(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Energy = reader.GetLong(0)!.Value;
                StartTimestamp = TimeUtil.FromEpochSeconds(reader.GetUInt(1, true));
                EndTimestamp = TimeUtil.FromEpochSeconds(reader.GetUInt(2, true));
                StartSystime = TimeUtil.FromMillis(reader.GetULong(3, true));
                EndSystime = TimeUtil.FromMillis(reader.GetULong(4, true));
            }
            public required long Energy { get; set; }
            public DateTime? StartTimestamp { get; set; }
            public DateTime? EndTimestamp { get; set; }
            public TimeSpan? StartSystime { get; set; }
            public TimeSpan? EndSystime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteLong(0, Energy, 4611686018427387904, 0);
                if (StartTimestamp != null)
                    writer.WriteUInt(1, TimeUtil.ToEpochSeconds(StartTimestamp!.Value));
                if (EndTimestamp != null)
                    writer.WriteUInt(2, TimeUtil.ToEpochSeconds(EndTimestamp!.Value));
                if (StartSystime != null)
                    writer.WriteULong(3, (ulong)StartSystime!.Value.TotalMilliseconds);
                if (EndSystime != null)
                    writer.WriteULong(4, (ulong)EndSystime!.Value.TotalMilliseconds);
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
        /// Accuracy Attribute
        /// </summary>
        public required ReadAttribute<MeasurementAccuracy> Accuracy { get; init; }

        /// <summary>
        /// Cumulative Energy Imported Attribute
        /// </summary>
        public required ReadAttribute<EnergyMeasurement?> CumulativeEnergyImported { get; init; }

        /// <summary>
        /// Cumulative Energy Exported Attribute
        /// </summary>
        public required ReadAttribute<EnergyMeasurement?> CumulativeEnergyExported { get; init; }

        /// <summary>
        /// Periodic Energy Imported Attribute
        /// </summary>
        public required ReadAttribute<EnergyMeasurement?> PeriodicEnergyImported { get; init; }

        /// <summary>
        /// Periodic Energy Exported Attribute
        /// </summary>
        public required ReadAttribute<EnergyMeasurement?> PeriodicEnergyExported { get; init; }

        /// <summary>
        /// Cumulative Energy Reset Attribute
        /// </summary>
        public required ReadAttribute<CumulativeEnergyResetStruct?> CumulativeEnergyReset { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Electrical Energy Measurement";
        }
    }
}