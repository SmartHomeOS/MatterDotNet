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
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Water Heater Management Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class WaterHeaterManagementCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0094;

        /// <summary>
        /// Water Heater Management Cluster
        /// </summary>
        public WaterHeaterManagementCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected WaterHeaterManagementCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum BoostStateEnum {
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
        /// Water Heater Heat Source Bitmap
        /// </summary>
        [Flags]
        public enum WaterHeaterHeatSourceBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Immersion Heating Element 1
            /// </summary>
            ImmersionElement1 = 1,
            /// <summary>
            /// Immersion Heating Element 2
            /// </summary>
            ImmersionElement2 = 2,
            /// <summary>
            /// Heat pump Heating
            /// </summary>
            HeatPump = 4,
            /// <summary>
            /// Boiler Heating (e.g. Gas or Oil)
            /// </summary>
            Boiler = 8,
            /// <summary>
            /// Other Heating
            /// </summary>
            Other = 16,
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
                Duration = TimeSpan.FromSeconds(reader.GetUInt(0)!.Value);
                OneShot = reader.GetBool(1, true);
                EmergencyBoost = reader.GetBool(2, true);
                TemporarySetpoint = reader.GetShort(3, true);
                TargetPercentage = reader.GetByte(4, true);
                TargetReheat = reader.GetByte(5, true);
            }
            public required TimeSpan Duration { get; set; }
            public bool? OneShot { get; set; } = false;
            public bool? EmergencyBoost { get; set; } = false;
            public short? TemporarySetpoint { get; set; }
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
                    writer.WriteShort(3, TemporarySetpoint);
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
        public async Task<bool> Boost(SecureSession session, WaterHeaterBoostInfo BoostInfo) {
            BoostPayload requestFields = new BoostPayload() {
                BoostInfo = BoostInfo,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Cancel Boost
        /// </summary>
        public async Task<bool> CancelBoost(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01);
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
        /// Get the Heater Types attribute
        /// </summary>
        public async Task<WaterHeaterHeatSourceBitmap> GetHeaterTypes(SecureSession session) {
            return (WaterHeaterHeatSourceBitmap)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Get the Heat Demand attribute
        /// </summary>
        public async Task<WaterHeaterHeatSourceBitmap> GetHeatDemand(SecureSession session) {
            return (WaterHeaterHeatSourceBitmap)await GetEnumAttribute(session, 1);
        }

        /// <summary>
        /// Get the Tank Volume attribute
        /// </summary>
        public async Task<ushort> GetTankVolume(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 2) ?? 0;
        }

        /// <summary>
        /// Get the Estimated Heat Required attribute
        /// </summary>
        public async Task<long> GetEstimatedHeatRequired(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 3) ?? 0;
        }

        /// <summary>
        /// Get the Tank Percentage attribute
        /// </summary>
        public async Task<byte> GetTankPercentage(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 4) ?? 0;
        }

        /// <summary>
        /// Get the Boost State attribute
        /// </summary>
        public async Task<BoostStateEnum> GetBoostState(SecureSession session) {
            return (BoostStateEnum?)await GetEnumAttribute(session, 5) ?? BoostStateEnum.Inactive;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Water Heater Management Cluster";
        }
    }
}