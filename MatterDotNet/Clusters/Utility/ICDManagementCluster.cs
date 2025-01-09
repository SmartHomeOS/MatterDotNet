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

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// ICD Management Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class ICDManagementCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0046;

        /// <summary>
        /// ICD Management Cluster
        /// </summary>
        public ICDManagementCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ICDManagementCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        }

        /// <summary>
        /// Operating Mode
        /// </summary>
        public enum OperatingModeEnum {
            /// <summary>
            /// ICD is operating as a Short Idle Time ICD.
            /// </summary>
            SIT = 0,
            /// <summary>
            /// ICD is operating as a Long Idle Time ICD.
            /// </summary>
            LIT = 1,
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

            [SetsRequiredMembers]
            internal MonitoringRegistration(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                CheckInNodeID = reader.GetULong(1)!.Value;
                MonitoredSubject = reader.GetULong(2)!.Value;
            }
            public required ulong CheckInNodeID { get; set; }
            public required ulong MonitoredSubject { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(1, CheckInNodeID);
                writer.WriteULong(2, MonitoredSubject);
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
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, CheckInNodeID);
                writer.WriteULong(1, MonitoredSubject);
                writer.WriteBytes(2, Key, 16);
                if (VerificationKey != null)
                    writer.WriteBytes(3, VerificationKey, 16);
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
        public async Task<RegisterClientResponse?> RegisterClient(SecureSession session, ulong CheckInNodeID, ulong MonitoredSubject, byte[] Key, byte[]? VerificationKey) {
            RegisterClientPayload requestFields = new RegisterClientPayload() {
                CheckInNodeID = CheckInNodeID,
                MonitoredSubject = MonitoredSubject,
                Key = Key,
                VerificationKey = VerificationKey,
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
        public async Task<bool> UnregisterClient(SecureSession session, ulong CheckInNodeID, byte[]? VerificationKey) {
            UnregisterClientPayload requestFields = new UnregisterClientPayload() {
                CheckInNodeID = CheckInNodeID,
                VerificationKey = VerificationKey,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Stay Active Request
        /// </summary>
        public async Task<StayActiveResponse?> StayActiveRequest(SecureSession session, uint StayActiveDuration) {
            StayActiveRequestPayload requestFields = new StayActiveRequestPayload() {
                StayActiveDuration = StayActiveDuration,
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
        public async Task<uint> GetUserActiveModeTriggerHint(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 6) ?? 0;
        }

        /// <summary>
        /// Get the User Active Mode Trigger Instruction attribute
        /// </summary>
        public async Task<string> GetUserActiveModeTriggerInstruction(SecureSession session) {
            return (string?)(dynamic?)await GetAttribute(session, 7) ?? "";
        }

        /// <summary>
        /// Get the Operating Mode attribute
        /// </summary>
        public async Task<OperatingModeEnum> GetOperatingMode(SecureSession session) {
            return (OperatingModeEnum)await GetEnumAttribute(session, 8);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "ICD Management Cluster";
        }
    }
}