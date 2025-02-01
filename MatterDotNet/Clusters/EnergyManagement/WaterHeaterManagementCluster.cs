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

using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.EnergyManagement
{
    /// <summary>
    /// This cluster is used to allow clients to control the operation of a hot water heating appliance so that it can be used with energy management.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class WaterHeaterManagement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0094;

        /// <summary>
        /// This cluster is used to allow clients to control the operation of a hot water heating appliance so that it can be used with energy management.
        /// </summary>
        [SetsRequiredMembers]
        public WaterHeaterManagement(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected WaterHeaterManagement(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            HeaterTypes = new ReadAttribute<WaterHeaterHeatSource>(cluster, endPoint, 0) {
                Deserialize = x => (WaterHeaterHeatSource)DeserializeEnum(x)!
            };
            HeatDemand = new ReadAttribute<WaterHeaterHeatSource>(cluster, endPoint, 1) {
                Deserialize = x => (WaterHeaterHeatSource)DeserializeEnum(x)!
            };
            TankVolume = new ReadAttribute<ushort>(cluster, endPoint, 2) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0

            };
            EstimatedHeatRequired = new ReadAttribute<long>(cluster, endPoint, 3) {
                Deserialize = x => (long?)(dynamic?)x ?? 0

            };
            TankPercentage = new ReadAttribute<byte>(cluster, endPoint, 4) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            BoostState = new ReadAttribute<BoostStateEnum>(cluster, endPoint, 5) {
                Deserialize = x => (BoostStateEnum)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Allows energy management control of the tank
            /// </summary>
            EnergyManagement = 1,
            /// <summary>
            /// Supports monitoring the percentage of hot water in the tank
            /// </summary>
            TankPercent = 2,
        }

        /// <summary>
        /// Boost State
        /// </summary>
        public enum BoostStateEnum : byte {
            /// <summary>
            /// Boost is not currently active
            /// </summary>
            Inactive = 0,
            /// <summary>
            /// Boost is currently active
            /// </summary>
            Active = 1,
        }

        /// <summary>
        /// Water Heater Heat Source
        /// </summary>
        [Flags]
        public enum WaterHeaterHeatSource : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Immersion Heating Element 1
            /// </summary>
            ImmersionElement1 = 0x01,
            /// <summary>
            /// Immersion Heating Element 2
            /// </summary>
            ImmersionElement2 = 0x02,
            /// <summary>
            /// Heat pump Heating
            /// </summary>
            HeatPump = 0x04,
            /// <summary>
            /// Boiler Heating (e.g. Gas or Oil)
            /// </summary>
            Boiler = 0x08,
            /// <summary>
            /// Other Heating
            /// </summary>
            Other = 0x10,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Water Heater Boost Info
        /// </summary>
        public record WaterHeaterBoostInfo : TLVPayload {
            /// <summary>
            /// Water Heater Boost Info
            /// </summary>
            public WaterHeaterBoostInfo() { }

            /// <summary>
            /// Water Heater Boost Info
            /// </summary>
            [SetsRequiredMembers]
            public WaterHeaterBoostInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Duration = TimeUtil.FromSeconds(reader.GetUInt(0))!.Value;
                OneShot = reader.GetBool(1, true);
                EmergencyBoost = reader.GetBool(2, true);
                TemporarySetpoint = reader.GetDecimal(3, true);
                TargetPercentage = reader.GetByte(4, true);
                TargetReheat = reader.GetByte(5, true);
            }
            public required TimeSpan Duration { get; set; }
            public bool? OneShot { get; set; } = false;
            public bool? EmergencyBoost { get; set; } = false;
            public decimal? TemporarySetpoint { get; set; }
            public byte? TargetPercentage { get; set; }
            public byte? TargetReheat { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)Duration.TotalSeconds, uint.MaxValue, 1);
                if (OneShot != null)
                    writer.WriteBool(1, OneShot);
                if (EmergencyBoost != null)
                    writer.WriteBool(2, EmergencyBoost);
                if (TemporarySetpoint != null)
                    writer.WriteDecimal(3, TemporarySetpoint);
                if (TargetPercentage != null)
                    writer.WriteByte(4, TargetPercentage);
                if (TargetReheat != null)
                    writer.WriteByte(5, TargetReheat);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record BoostPayload : TLVPayload {
            public required WaterHeaterBoostInfo BoostInfo { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                BoostInfo.Serialize(writer, 0);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Boost
        /// </summary>
        public async Task<bool> Boost(SecureSession session, WaterHeaterBoostInfo boostInfo, CancellationToken token = default) {
            BoostPayload requestFields = new BoostPayload() {
                BoostInfo = boostInfo,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Cancel Boost
        /// </summary>
        public async Task<bool> CancelBoost(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, null, token);
            return ValidateResponse(resp);
        }
        #endregion Commands

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
        /// Heater Types Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<WaterHeaterHeatSource> HeaterTypes { get; init; }

        /// <summary>
        /// Heat Demand Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<WaterHeaterHeatSource> HeatDemand { get; init; }

        /// <summary>
        /// Tank Volume Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> TankVolume { get; init; }

        /// <summary>
        /// Estimated Heat Required [mWh] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long> EstimatedHeatRequired { get; init; }

        /// <summary>
        /// Tank Percentage [%] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> TankPercentage { get; init; }

        /// <summary>
        /// Boost State Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<BoostStateEnum> BoostState { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Water Heater Management";
        }
    }
}