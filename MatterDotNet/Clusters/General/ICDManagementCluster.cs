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
    /// Allows servers to ensure that listed clients are notified when a server is available for communication.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class ICDManagement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0046;

        /// <summary>
        /// Allows servers to ensure that listed clients are notified when a server is available for communication.
        /// </summary>
        [SetsRequiredMembers]
        public ICDManagement(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ICDManagement(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            IdleModeDuration = new ReadAttribute<uint>(cluster, endPoint, 0) {
                Deserialize = x => (uint?)(dynamic?)x ?? 1

            };
            ActiveModeDuration = new ReadAttribute<uint>(cluster, endPoint, 1) {
                Deserialize = x => (uint?)(dynamic?)x ?? 300

            };
            ActiveModeThreshold = new ReadAttribute<ushort>(cluster, endPoint, 2) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 300

            };
            RegisteredClients = new ReadAttribute<MonitoringRegistration[]>(cluster, endPoint, 3) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    MonitoringRegistration[] list = new MonitoringRegistration[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new MonitoringRegistration(reader.GetStruct(i)!);
                    return list;
                }
            };
            ICDCounter = new ReadAttribute<uint>(cluster, endPoint, 4) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0

            };
            ClientsSupportedPerFabric = new ReadAttribute<ushort>(cluster, endPoint, 5) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 1

            };
            UserActiveModeTriggerHint = new ReadAttribute<UserActiveModeTrigger>(cluster, endPoint, 6) {
                Deserialize = x => (UserActiveModeTrigger)DeserializeEnum(x)!
            };
            UserActiveModeTriggerInstruction = new ReadAttribute<string>(cluster, endPoint, 7) {
                Deserialize = x => (string)(dynamic?)x!
            };
            OperatingMode = new ReadAttribute<OperatingModeEnum>(cluster, endPoint, 8) {
                Deserialize = x => (OperatingModeEnum)DeserializeEnum(x)!
            };
            MaximumCheckInBackOff = new ReadAttribute<uint>(cluster, endPoint, 9) {
                Deserialize = x => (uint?)(dynamic?)x ?? 1

            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Device supports attributes and commands for the Check-In Protocol support.
            /// </summary>
            CheckInProtocolSupport = 1,
            /// <summary>
            /// Device supports the user active mode trigger feature.
            /// </summary>
            UserActiveModeTrigger = 2,
            /// <summary>
            /// Device supports operating as a Long Idle Time ICD.
            /// </summary>
            LongIdleTimeSupport = 4,
            /// <summary>
            /// Device supports dynamic switching from SIT to LIT operating modes.
            /// </summary>
            DynamicSitLitSupport = 8,
        }

        /// <summary>
        /// Client Type
        /// </summary>
        public enum ClientType : byte {
            Permanent = 0,
            Ephemeral = 1,
        }

        /// <summary>
        /// Operating Mode
        /// </summary>
        public enum OperatingModeEnum : byte {
            /// <summary>
            /// ICD is operating as a Short Idle Time ICD.
            /// </summary>
            SIT = 0,
            /// <summary>
            /// ICD is operating as a Long Idle Time ICD.
            /// </summary>
            LIT = 1,
        }

        /// <summary>
        /// User Active Mode Trigger
        /// </summary>
        [Flags]
        public enum UserActiveModeTrigger : uint {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            PowerCycle = 0x0001,
            SettingsMenu = 0x0002,
            CustomInstruction = 0x0004,
            DeviceManual = 0x0008,
            ActuateSensor = 0x0010,
            ActuateSensorSeconds = 0x0020,
            ActuateSensorTimes = 0x0040,
            ActuateSensorLightsBlink = 0x0080,
            ResetButton = 0x00100,
            ResetButtonLightsBlink = 0x00200,
            ResetButtonSeconds = 0x00400,
            ResetButtonTimes = 0x00800,
            SetupButton = 0x1000,
            SetupButtonSeconds = 0x2000,
            SetupButtonLightsBlink = 0x4000,
            SetupButtonTimes = 0x8000,
            AppDefinedButton = 0x10000,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Monitoring Registration
        /// </summary>
        public record MonitoringRegistration : TLVPayload {
            /// <summary>
            /// Monitoring Registration
            /// </summary>
            public MonitoringRegistration() { }

            /// <summary>
            /// Monitoring Registration
            /// </summary>
            [SetsRequiredMembers]
            public MonitoringRegistration(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                CheckInNodeID = reader.GetULong(1)!.Value;
                MonitoredSubject = reader.GetULong(2)!.Value;
                ClientType = (ClientType)reader.GetUShort(4)!.Value;
            }
            public required ulong CheckInNodeID { get; set; }
            public required ulong MonitoredSubject { get; set; }
            public required ClientType ClientType { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(1, CheckInNodeID);
                writer.WriteULong(2, MonitoredSubject);
                writer.WriteUShort(4, (ushort)ClientType);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record RegisterClientPayload : TLVPayload {
            public required ulong CheckInNodeID { get; set; }
            public required ulong MonitoredSubject { get; set; }
            public required byte[] Key { get; set; }
            public byte[]? VerificationKey { get; set; }
            public required ClientType ClientType { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, CheckInNodeID);
                writer.WriteULong(1, MonitoredSubject);
                writer.WriteBytes(2, Key, 16);
                if (VerificationKey != null)
                    writer.WriteBytes(3, VerificationKey, 16);
                writer.WriteUShort(4, (ushort)ClientType);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Register Client Response - Reply from server
        /// </summary>
        public struct RegisterClientResponse() {
            public required uint ICDCounter { get; set; }
        }

        private record UnregisterClientPayload : TLVPayload {
            public required ulong CheckInNodeID { get; set; }
            public byte[]? VerificationKey { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, CheckInNodeID);
                if (VerificationKey != null)
                    writer.WriteBytes(1, VerificationKey, 16);
                writer.EndContainer();
            }
        }

        private record StayActiveRequestPayload : TLVPayload {
            public required uint StayActiveDuration { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, StayActiveDuration);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Stay Active Response - Reply from server
        /// </summary>
        public struct StayActiveResponse() {
            public required uint PromisedActiveDuration { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Register Client
        /// </summary>
        public async Task<RegisterClientResponse?> RegisterClient(SecureSession session, ulong checkInNodeID, ulong monitoredSubject, byte[] key, byte[]? verificationKey, ClientType clientType) {
            RegisterClientPayload requestFields = new RegisterClientPayload() {
                CheckInNodeID = checkInNodeID,
                MonitoredSubject = monitoredSubject,
                Key = key,
                VerificationKey = verificationKey,
                ClientType = clientType,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new RegisterClientResponse() {
                ICDCounter = (uint)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Unregister Client
        /// </summary>
        public async Task<bool> UnregisterClient(SecureSession session, ulong checkInNodeID, byte[]? verificationKey) {
            UnregisterClientPayload requestFields = new UnregisterClientPayload() {
                CheckInNodeID = checkInNodeID,
                VerificationKey = verificationKey,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Stay Active Request
        /// </summary>
        public async Task<StayActiveResponse?> StayActiveRequest(SecureSession session, uint stayActiveDuration) {
            StayActiveRequestPayload requestFields = new StayActiveRequestPayload() {
                StayActiveDuration = stayActiveDuration,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new StayActiveResponse() {
                PromisedActiveDuration = (uint)GetField(resp, 0),
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
        /// Idle Mode Duration Attribute
        /// </summary>
        public required ReadAttribute<uint> IdleModeDuration { get; init; }

        /// <summary>
        /// Active Mode Duration Attribute
        /// </summary>
        public required ReadAttribute<uint> ActiveModeDuration { get; init; }

        /// <summary>
        /// Active Mode Threshold Attribute
        /// </summary>
        public required ReadAttribute<ushort> ActiveModeThreshold { get; init; }

        /// <summary>
        /// Registered Clients Attribute
        /// </summary>
        public required ReadAttribute<MonitoringRegistration[]> RegisteredClients { get; init; }

        /// <summary>
        /// ICD Counter Attribute
        /// </summary>
        public required ReadAttribute<uint> ICDCounter { get; init; }

        /// <summary>
        /// Clients Supported Per Fabric Attribute
        /// </summary>
        public required ReadAttribute<ushort> ClientsSupportedPerFabric { get; init; }

        /// <summary>
        /// User Active Mode Trigger Hint Attribute
        /// </summary>
        public required ReadAttribute<UserActiveModeTrigger> UserActiveModeTriggerHint { get; init; }

        /// <summary>
        /// User Active Mode Trigger Instruction Attribute
        /// </summary>
        public required ReadAttribute<string> UserActiveModeTriggerInstruction { get; init; }

        /// <summary>
        /// Operating Mode Attribute
        /// </summary>
        public required ReadAttribute<OperatingModeEnum> OperatingMode { get; init; }

        /// <summary>
        /// Maximum Check In Back Off Attribute
        /// </summary>
        public required ReadAttribute<uint> MaximumCheckInBackOff { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "ICD Management";
        }
    }
}