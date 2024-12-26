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
//
// WARNING: This file was auto-generated. Do not edit.

using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;

namespace MatterDotNet.Clusters
{
    /// <summary>
    /// General Commissioning Cluster
    /// </summary>
    public class GeneralCommissioningCluster : ClusterBase
    {
        private const uint CLUSTER_ID = 0x0030;

        /// <summary>
        /// General Commissioning Cluster
        /// </summary>
        public GeneralCommissioningCluster(ushort endPoint) : base(endPoint) { }

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
        public record BasicCommissioningInfo : TLVPayload {
            public required ushort FailSafeExpiryLengthSeconds { get; set; }
            public required ushort MaxCumulativeFailsafeSeconds { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
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
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ExpiryLengthSeconds);
                writer.WriteULong(1, Breadcrumb);
                writer.EndContainer();
            }
        }

        public struct ArmFailSafeResponse() {
            public required CommissioningErrorEnum ErrorCode { get; set; } = CommissioningErrorEnum.OK;
            public required string DebugText { get; set; } = "";
        }

        private record SetRegulatoryConfigPayload : TLVPayload {
            public required RegulatoryLocationTypeEnum NewRegulatoryConfig { get; set; }
            public required string CountryCode { get; set; }
            public required ulong Breadcrumb { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)NewRegulatoryConfig);
                writer.WriteString(1, CountryCode);
                writer.WriteULong(2, Breadcrumb);
                writer.EndContainer();
            }
        }

        public struct SetRegulatoryConfigResponse() {
            public required CommissioningErrorEnum ErrorCode { get; set; } = CommissioningErrorEnum.OK;
            public required string DebugText { get; set; } = "";
        }

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
            if (!validateResponse(resp))
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
            if (!validateResponse(resp))
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
            if (!validateResponse(resp))
                return null;
            return new CommissioningCompleteResponse() {
                ErrorCode = (CommissioningErrorEnum)(byte)GetField(resp, 0),
                DebugText = (string)GetField(resp, 1),
            };
        }
        #endregion Commands

        #region Attributes
        public ulong Breadcrumb { get; set; } = 0;

        public BasicCommissioningInfo BasicCommissioningInfoField { get; }

        public RegulatoryLocationTypeEnum RegulatoryConfig { get; }

        public RegulatoryLocationTypeEnum LocationCapability { get; } = RegulatoryLocationTypeEnum.IndoorOutdoor;

        public bool SupportsConcurrentConnection { get; } = true;
        #endregion Attributes
    }
}