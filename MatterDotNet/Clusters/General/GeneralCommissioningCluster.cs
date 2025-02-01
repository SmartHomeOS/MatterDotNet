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
        [SetsRequiredMembers]
        public GeneralCommissioning(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected GeneralCommissioning(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            Breadcrumb = new ReadWriteAttribute<ulong>(cluster, endPoint, 0) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            BasicCommissioningInfo = new ReadAttribute<BasicCommissioningInfoStruct>(cluster, endPoint, 1) {
                Deserialize = x => new BasicCommissioningInfoStruct((object[])x!)
            };
            RegulatoryConfig = new ReadAttribute<RegulatoryLocationType>(cluster, endPoint, 2) {
                Deserialize = x => (RegulatoryLocationType)DeserializeEnum(x)!
            };
            LocationCapability = new ReadAttribute<RegulatoryLocationType>(cluster, endPoint, 3) {
                Deserialize = x => (RegulatoryLocationType)DeserializeEnum(x)!
            };
            SupportsConcurrentConnection = new ReadAttribute<bool>(cluster, endPoint, 4) {
                Deserialize = x => (bool?)(dynamic?)x ?? true

            };
            TCAcceptedVersion = new ReadAttribute<ushort>(cluster, endPoint, 5) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            TCMinRequiredVersion = new ReadAttribute<ushort>(cluster, endPoint, 6) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            TCAcknowledgements = new ReadAttribute<ushort>(cluster, endPoint, 7) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            TCAcknowledgementsRequired = new ReadAttribute<bool>(cluster, endPoint, 8) {
                Deserialize = x => (bool)(dynamic?)x!
            };
        }

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
        public record BasicCommissioningInfoStruct : TLVPayload {
            /// <summary>
            /// Basic Commissioning Info
            /// </summary>
            public BasicCommissioningInfoStruct() { }

            /// <summary>
            /// Basic Commissioning Info
            /// </summary>
            [SetsRequiredMembers]
            public BasicCommissioningInfoStruct(object[] fields) {
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
        public async Task<ArmFailSafeResponse?> ArmFailSafe(SecureSession session, ushort expiryLengthSeconds, ulong breadcrumb, CancellationToken token = default) {
            ArmFailSafePayload requestFields = new ArmFailSafePayload() {
                ExpiryLengthSeconds = expiryLengthSeconds,
                Breadcrumb = breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
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
        public async Task<SetRegulatoryConfigResponse?> SetRegulatoryConfig(SecureSession session, RegulatoryLocationType newRegulatoryConfig, string countryCode, ulong breadcrumb, CancellationToken token = default) {
            SetRegulatoryConfigPayload requestFields = new SetRegulatoryConfigPayload() {
                NewRegulatoryConfig = newRegulatoryConfig,
                CountryCode = countryCode,
                Breadcrumb = breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields, token);
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
        public async Task<CommissioningCompleteResponse?> CommissioningComplete(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, null, token);
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
        public async Task<SetTCAcknowledgementsResponse?> SetTCAcknowledgements(SecureSession session, ushort tCVersion, ushort tCUserResponse, CancellationToken token = default) {
            SetTCAcknowledgementsPayload requestFields = new SetTCAcknowledgementsPayload() {
                TCVersion = tCVersion,
                TCUserResponse = tCUserResponse,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields, token);
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
        /// Breadcrumb Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ulong> Breadcrumb { get; init; }

        /// <summary>
        /// Basic Commissioning Info Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<BasicCommissioningInfoStruct> BasicCommissioningInfo { get; init; }

        /// <summary>
        /// Regulatory Config Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<RegulatoryLocationType> RegulatoryConfig { get; init; }

        /// <summary>
        /// Location Capability Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<RegulatoryLocationType> LocationCapability { get; init; }

        /// <summary>
        /// Supports Concurrent Connection Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool> SupportsConcurrentConnection { get; init; }

        /// <summary>
        /// TC Accepted Version Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> TCAcceptedVersion { get; init; }

        /// <summary>
        /// TC Min Required Version Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> TCMinRequiredVersion { get; init; }

        /// <summary>
        /// TC Acknowledgements Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> TCAcknowledgements { get; init; }

        /// <summary>
        /// TC Acknowledgements Required Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool> TCAcknowledgementsRequired { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "General Commissioning";
        }
    }
}