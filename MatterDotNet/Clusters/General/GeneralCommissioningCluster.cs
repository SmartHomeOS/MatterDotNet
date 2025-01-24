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

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// This cluster is used to manage global aspects of the Commissioning flow.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class GeneralCommissioning : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0030;

        /// <summary>
        /// This cluster is used to manage global aspects of the Commissioning flow.
        /// </summary>
        public GeneralCommissioning(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected GeneralCommissioning(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports Terms and Conditions acknowledgement
            /// </summary>
            TermsAndConditions = 1,
        }

        /// <summary>
        /// Commissioning Error
        /// </summary>
        public enum CommissioningError : byte {
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
            /// <summary>
            /// <see cref="RequiredTCNotAccepted"/> One or more required TC features from the Enhanced Setup Flow were not accepted.
            /// </summary>
            RequiredTCNotAccepted = 5,
            /// <summary>
            /// <see cref="TCAcknowledgementsNotReceived, TCAcknowledgementsNotReceived"/> No acknowledgements from the user for the TC features were received.
            /// </summary>
            TCAcknowledgementsNotReceived = 6,
            /// <summary>
            /// <see cref="TCMinVersionNotMet, TCMinVersionNotMet"/> The version of the TC features acknowledged by the user did not meet the minimum required version.
            /// </summary>
            TCMinVersionNotMet = 7,
        }

        /// <summary>
        /// Regulatory Location Type
        /// </summary>
        public enum RegulatoryLocationType : byte {
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

            /// <summary>
            /// Basic Commissioning Info
            /// </summary>
            [SetsRequiredMembers]
            public BasicCommissioningInfo(object[] fields) {
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
            public required ushort ExpiryLengthSeconds { get; set; }
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
            public required CommissioningError ErrorCode { get; set; }
            public required string DebugText { get; set; }
        }

        private record SetRegulatoryConfigPayload : TLVPayload {
            public required RegulatoryLocationType NewRegulatoryConfig { get; set; }
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
            public required CommissioningError ErrorCode { get; set; }
            public required string DebugText { get; set; }
        }

        /// <summary>
        /// Commissioning Complete Response - Reply from server
        /// </summary>
        public struct CommissioningCompleteResponse() {
            public required CommissioningError ErrorCode { get; set; }
            public required string DebugText { get; set; }
        }

        private record SetTCAcknowledgementsPayload : TLVPayload {
            public required ushort TCVersion { get; set; }
            public required ushort TCUserResponse { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, TCVersion);
                writer.WriteUShort(1, TCUserResponse);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Set TC Acknowledgements Response - Reply from server
        /// </summary>
        public struct SetTCAcknowledgementsResponse() {
            public required CommissioningError ErrorCode { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Arm Fail Safe
        /// </summary>
        public async Task<ArmFailSafeResponse?> ArmFailSafe(SecureSession session, ushort expiryLengthSeconds, ulong breadcrumb) {
            ArmFailSafePayload requestFields = new ArmFailSafePayload() {
                ExpiryLengthSeconds = expiryLengthSeconds,
                Breadcrumb = breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ArmFailSafeResponse() {
                ErrorCode = (CommissioningError)(byte)GetField(resp, 0),
                DebugText = (string)GetField(resp, 1),
            };
        }

        /// <summary>
        /// Set Regulatory Config
        /// </summary>
        public async Task<SetRegulatoryConfigResponse?> SetRegulatoryConfig(SecureSession session, RegulatoryLocationType newRegulatoryConfig, string countryCode, ulong breadcrumb) {
            SetRegulatoryConfigPayload requestFields = new SetRegulatoryConfigPayload() {
                NewRegulatoryConfig = newRegulatoryConfig,
                CountryCode = countryCode,
                Breadcrumb = breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new SetRegulatoryConfigResponse() {
                ErrorCode = (CommissioningError)(byte)GetField(resp, 0),
                DebugText = (string)GetField(resp, 1),
            };
        }

        /// <summary>
        /// Commissioning Complete
        /// </summary>
        public async Task<CommissioningCompleteResponse?> CommissioningComplete(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04);
            if (!ValidateResponse(resp))
                return null;
            return new CommissioningCompleteResponse() {
                ErrorCode = (CommissioningError)(byte)GetField(resp, 0),
                DebugText = (string)GetField(resp, 1),
            };
        }

        /// <summary>
        /// Set TC Acknowledgements
        /// </summary>
        public async Task<SetTCAcknowledgementsResponse?> SetTCAcknowledgements(SecureSession session, ushort tCVersion, ushort tCUserResponse) {
            SetTCAcknowledgementsPayload requestFields = new SetTCAcknowledgementsPayload() {
                TCVersion = tCVersion,
                TCUserResponse = tCUserResponse,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new SetTCAcknowledgementsResponse() {
                ErrorCode = (CommissioningError)(byte)GetField(resp, 0),
            };
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
        /// Get the Breadcrumb attribute
        /// </summary>
        public async Task<ulong> GetBreadcrumb(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 0) ?? 0x0000000000000000;
        }

        /// <summary>
        /// Set the Breadcrumb attribute
        /// </summary>
        public async Task SetBreadcrumb (SecureSession session, ulong? value = 0x0000000000000000) {
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
        public async Task<RegulatoryLocationType> GetRegulatoryConfig(SecureSession session) {
            return (RegulatoryLocationType)await GetEnumAttribute(session, 2);
        }

        /// <summary>
        /// Get the Location Capability attribute
        /// </summary>
        public async Task<RegulatoryLocationType> GetLocationCapability(SecureSession session) {
            return (RegulatoryLocationType)await GetEnumAttribute(session, 3);
        }

        /// <summary>
        /// Get the Supports Concurrent Connection attribute
        /// </summary>
        public async Task<bool> GetSupportsConcurrentConnection(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 4) ?? true;
        }

        /// <summary>
        /// Get the TC Accepted Version attribute
        /// </summary>
        public async Task<ushort> GetTCAcceptedVersion(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 5))!;
        }

        /// <summary>
        /// Get the TC Min Required Version attribute
        /// </summary>
        public async Task<ushort> GetTCMinRequiredVersion(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 6))!;
        }

        /// <summary>
        /// Get the TC Acknowledgements attribute
        /// </summary>
        public async Task<ushort> GetTCAcknowledgements(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 7) ?? 0x0000;
        }

        /// <summary>
        /// Get the TC Acknowledgements Required attribute
        /// </summary>
        public async Task<bool> GetTCAcknowledgementsRequired(SecureSession session) {
            return (bool)(dynamic?)(await GetAttribute(session, 8))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "General Commissioning";
        }
    }
}