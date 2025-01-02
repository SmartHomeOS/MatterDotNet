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

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// General Commissioning Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class GeneralCommissioningCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0030;

        /// <summary>
        /// General Commissioning Cluster
        /// </summary>
        public GeneralCommissioningCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected GeneralCommissioningCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Commissioning Error
        /// </summary>
        public enum CommissioningErrorEnum {
            /// <summary>
            /// No error
            /// </summary>
            OK = 0,
            /// <summary>
            /// <see cref="ValueOutsideRange"/> Attempting to set regulatory configuration to a region or indoor/outdoor mode for which the server does not have proper configuration.
            /// </summary>
            ValueOutsideRange = 1,
            /// <summary>
            /// <see cref="InvalidAuthentication"/> Executed CommissioningComplete outside CASE session.
            /// </summary>
            InvalidAuthentication = 2,
            /// <summary>
            /// <see cref="NoFailSafe"/> Executed CommissioningComplete when there was no active ArmFailSafe Command.
            /// </summary>
            NoFailSafe = 3,
            /// <summary>
            /// <see cref="BusyWithOtherAdmin"/> Attempting to arm fail-safe or execute CommissioningComplete from a fabric different than the one associated with the current fail-safe context.
            /// </summary>
            BusyWithOtherAdmin = 4,
        }

        /// <summary>
        /// Regulatory Location Type
        /// </summary>
        public enum RegulatoryLocationTypeEnum {
            /// <summary>
            /// Indoor only
            /// </summary>
            Indoor = 0,
            /// <summary>
            /// Outdoor only
            /// </summary>
            Outdoor = 1,
            /// <summary>
            /// Indoor/Outdoor
            /// </summary>
            IndoorOutdoor = 2,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Basic Commissioning Info
        /// </summary>
        public record BasicCommissioningInfo : TLVPayload {
            /// <summary>
            /// Basic Commissioning Info
            /// </summary>
            public BasicCommissioningInfo() { }

            [SetsRequiredMembers]
            internal BasicCommissioningInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                FailSafeExpiryLengthSeconds = reader.GetUShort(0)!.Value;
                MaxCumulativeFailsafeSeconds = reader.GetUShort(1)!.Value;
            }
            public required ushort FailSafeExpiryLengthSeconds { get; set; }
            public required ushort MaxCumulativeFailsafeSeconds { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, FailSafeExpiryLengthSeconds);
                writer.WriteUShort(1, MaxCumulativeFailsafeSeconds);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record ArmFailSafePayload : TLVPayload {
            public required ushort ExpiryLengthSeconds { get; set; } = 900;
            public required ulong Breadcrumb { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ExpiryLengthSeconds);
                writer.WriteULong(1, Breadcrumb);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Arm Fail Safe Response - Reply from server
        /// </summary>
        public struct ArmFailSafeResponse() {
            public required CommissioningErrorEnum ErrorCode { get; set; } = CommissioningErrorEnum.OK;
            public required string DebugText { get; set; } = "";
        }

        private record SetRegulatoryConfigPayload : TLVPayload {
            public required RegulatoryLocationTypeEnum NewRegulatoryConfig { get; set; }
            public required string CountryCode { get; set; }
            public required ulong Breadcrumb { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)NewRegulatoryConfig);
                writer.WriteString(1, CountryCode, 2);
                writer.WriteULong(2, Breadcrumb);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Set Regulatory Config Response - Reply from server
        /// </summary>
        public struct SetRegulatoryConfigResponse() {
            public required CommissioningErrorEnum ErrorCode { get; set; } = CommissioningErrorEnum.OK;
            public required string DebugText { get; set; } = "";
        }

        /// <summary>
        /// Commissioning Complete Response - Reply from server
        /// </summary>
        public struct CommissioningCompleteResponse() {
            public required CommissioningErrorEnum ErrorCode { get; set; } = CommissioningErrorEnum.OK;
            public required string DebugText { get; set; } = "";
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Arm Fail Safe
        /// </summary>
        public async Task<ArmFailSafeResponse?> ArmFailSafe(SecureSession session, ushort ExpiryLengthSeconds, ulong Breadcrumb) {
            ArmFailSafePayload requestFields = new ArmFailSafePayload() {
                ExpiryLengthSeconds = ExpiryLengthSeconds,
                Breadcrumb = Breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ArmFailSafeResponse() {
                ErrorCode = (CommissioningErrorEnum)(byte)GetField(resp, 0),
                DebugText = (string)GetField(resp, 1),
            };
        }

        /// <summary>
        /// Set Regulatory Config
        /// </summary>
        public async Task<SetRegulatoryConfigResponse?> SetRegulatoryConfig(SecureSession session, RegulatoryLocationTypeEnum NewRegulatoryConfig, string CountryCode, ulong Breadcrumb) {
            SetRegulatoryConfigPayload requestFields = new SetRegulatoryConfigPayload() {
                NewRegulatoryConfig = NewRegulatoryConfig,
                CountryCode = CountryCode,
                Breadcrumb = Breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new SetRegulatoryConfigResponse() {
                ErrorCode = (CommissioningErrorEnum)(byte)GetField(resp, 0),
                DebugText = (string)GetField(resp, 1),
            };
        }

        /// <summary>
        /// Commissioning Complete
        /// </summary>
        public async Task<CommissioningCompleteResponse?> CommissioningComplete(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x04);
            if (!ValidateResponse(resp))
                return null;
            return new CommissioningCompleteResponse() {
                ErrorCode = (CommissioningErrorEnum)(byte)GetField(resp, 0),
                DebugText = (string)GetField(resp, 1),
            };
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Get the Breadcrumb attribute
        /// </summary>
        public async Task<ulong> GetBreadcrumb(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 0) ?? 0;
        }

        /// <summary>
        /// Set the Breadcrumb attribute
        /// </summary>
        public async Task SetBreadcrumb (SecureSession session, ulong? value = 0) {
            await SetAttribute(session, 0, value);
        }

        /// <summary>
        /// Get the Basic Commissioning Info attribute
        /// </summary>
        public async Task<BasicCommissioningInfo> GetBasicCommissioningInfo(SecureSession session) {
            return new BasicCommissioningInfo((object[])(await GetAttribute(session, 1))!);
        }

        /// <summary>
        /// Get the Regulatory Config attribute
        /// </summary>
        public async Task<RegulatoryLocationTypeEnum> GetRegulatoryConfig(SecureSession session) {
            return (RegulatoryLocationTypeEnum)await GetEnumAttribute(session, 2);
        }

        /// <summary>
        /// Get the Location Capability attribute
        /// </summary>
        public async Task<RegulatoryLocationTypeEnum> GetLocationCapability(SecureSession session) {
            return (RegulatoryLocationTypeEnum?)await GetEnumAttribute(session, 3) ?? RegulatoryLocationTypeEnum.IndoorOutdoor;
        }

        /// <summary>
        /// Get the Supports Concurrent Connection attribute
        /// </summary>
        public async Task<bool> GetSupportsConcurrentConnection(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 4) ?? true;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "General Commissioning Cluster";
        }
    }
}