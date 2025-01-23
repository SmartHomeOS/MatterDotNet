﻿// MatterDotNet Copyright (C) 2025 
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
        public ICDManagement(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ICDManagement(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum OperatingMode : byte {
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
            PowerCycle = 0x1,
            SettingsMenu = 0x2,
            CustomInstruction = 0x4,
            DeviceManual = 0x8,
            ActuateSensor = 0x10,
            ActuateSensorSeconds = 0x20,
            ActuateSensorTimes = 0x40,
            ActuateSensorLightsBlink = 0x80,
            ResetButton = 0x100,
            ResetButtonLightsBlink = 0x200,
            ResetButtonSeconds = 0x400,
            ResetButtonTimes = 0x800,
            /// <summary>
            /// Press Setup Button to transition the device to ActiveMode
            /// </summary>
            SetupButton = 0x1000,
            /// <summary>
            /// Press Setup Button for N seconds to transition the device to ActiveMode
            /// </summary>
            SetupButtonSeconds = 0x2000,
            /// <summary>
            /// Press Setup Button until light blinks to transition the device to ActiveMode
            /// </summary>
            SetupButtonLightsBlink = 0x4000,
            /// <summary>
            /// Press Setup Button N times to transition the device to ActiveMode
            /// </summary>
            SetupButtonTimes = 0x8000,
            /// <summary>
            /// Press the N Button to transition the device to ActiveMode
            /// </summary>
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
        /// Get the Idle Mode Duration attribute
        /// </summary>
        public async Task<uint> GetIdleModeDuration(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 0) ?? 1;
        }

        /// <summary>
        /// Get the Active Mode Duration attribute
        /// </summary>
        public async Task<uint> GetActiveModeDuration(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 1) ?? 300;
        }

        /// <summary>
        /// Get the Active Mode Threshold attribute
        /// </summary>
        public async Task<ushort> GetActiveModeThreshold(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 2) ?? 300;
        }

        /// <summary>
        /// Get the Registered Clients attribute
        /// </summary>
        public async Task<MonitoringRegistration[]> GetRegisteredClients(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 3))!);
            MonitoringRegistration[] list = new MonitoringRegistration[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new MonitoringRegistration(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the ICD Counter attribute
        /// </summary>
        public async Task<uint> GetICDCounter(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 4) ?? 0;
        }

        /// <summary>
        /// Get the Clients Supported Per Fabric attribute
        /// </summary>
        public async Task<ushort> GetClientsSupportedPerFabric(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 5) ?? 1;
        }

        /// <summary>
        /// Get the User Active Mode Trigger Hint attribute
        /// </summary>
        public async Task<UserActiveModeTrigger> GetUserActiveModeTriggerHint(SecureSession session) {
            return (UserActiveModeTrigger)await GetEnumAttribute(session, 6);
        }

        /// <summary>
        /// Get the User Active Mode Trigger Instruction attribute
        /// </summary>
        public async Task<string> GetUserActiveModeTriggerInstruction(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 7))!;
        }

        /// <summary>
        /// Get the Operating Mode attribute
        /// </summary>
        public async Task<OperatingMode> GetOperatingMode(SecureSession session) {
            return (OperatingMode)await GetEnumAttribute(session, 8);
        }

        /// <summary>
        /// Get the Maximum Check In Back Off attribute
        /// </summary>
        public async Task<uint> GetMaximumCheckInBackOff(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 9) ?? 1;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "ICD Management";
        }
    }
}