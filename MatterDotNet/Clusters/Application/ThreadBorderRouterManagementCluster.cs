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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Thread Border Router Management Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ThreadBorderRouterManagementCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0452;

        /// <summary>
        /// Thread Border Router Management Cluster
        /// </summary>
        public ThreadBorderRouterManagementCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ThreadBorderRouterManagementCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// The ability to change PAN configuration with pending dataset setting request.
            /// </summary>
            PANChange = 1,
        }
        #endregion Enums

        #region Payloads
        /// <summary>
        /// Dataset Response - Reply from server
        /// </summary>
        public struct DatasetResponse() {
            public required byte[] Dataset { get; set; }
        }

        private record SetActiveDatasetRequestPayload : TLVPayload {
            public required byte[] ActiveDataset { get; set; }
            public ulong? Breadcrumb { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, ActiveDataset, 254);
                if (Breadcrumb != null)
                    writer.WriteULong(1, Breadcrumb);
                writer.EndContainer();
            }
        }

        private record SetPendingDatasetRequestPayload : TLVPayload {
            public required byte[] PendingDataset { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, PendingDataset, 254);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Get Active Dataset Request
        /// </summary>
        public async Task<DatasetResponse?> GetActiveDatasetRequest(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00);
            if (!ValidateResponse(resp))
                return null;
            return new DatasetResponse() {
                Dataset = (byte[])GetField(resp, 0),
            };
        }

        /// <summary>
        /// Get Pending Dataset Request
        /// </summary>
        public async Task<DatasetResponse?> GetPendingDatasetRequest(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01);
            if (!ValidateResponse(resp))
                return null;
            return new DatasetResponse() {
                Dataset = (byte[])GetField(resp, 0),
            };
        }

        /// <summary>
        /// Set Active Dataset Request
        /// </summary>
        public async Task<bool> SetActiveDatasetRequest(SecureSession session, ushort commandTimeoutMS, byte[] ActiveDataset, ulong? Breadcrumb) {
            SetActiveDatasetRequestPayload requestFields = new SetActiveDatasetRequestPayload() {
                ActiveDataset = ActiveDataset,
                Breadcrumb = Breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x03, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Pending Dataset Request
        /// </summary>
        public async Task<bool> SetPendingDatasetRequest(SecureSession session, ushort commandTimeoutMS, byte[] PendingDataset) {
            SetPendingDatasetRequestPayload requestFields = new SetPendingDatasetRequestPayload() {
                PendingDataset = PendingDataset,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x04, commandTimeoutMS, requestFields);
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
        /// Get the Border Router Name attribute
        /// </summary>
        public async Task<string> GetBorderRouterName(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Get the Border Agent ID attribute
        /// </summary>
        public async Task<byte[]> GetBorderAgentID(SecureSession session) {
            return (byte[])(dynamic?)(await GetAttribute(session, 1))!;
        }

        /// <summary>
        /// Get the Thread Version attribute
        /// </summary>
        public async Task<ushort> GetThreadVersion(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 2))!;
        }

        /// <summary>
        /// Get the Interface Enabled attribute
        /// </summary>
        public async Task<bool> GetInterfaceEnabled(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 3) ?? false;
        }

        /// <summary>
        /// Get the Active Dataset Timestamp attribute
        /// </summary>
        public async Task<ulong?> GetActiveDatasetTimestamp(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 4, true) ?? 0;
        }

        /// <summary>
        /// Get the Pending Dataset Timestamp attribute
        /// </summary>
        public async Task<ulong?> GetPendingDatasetTimestamp(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 5, true) ?? 0;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Thread Border Router Management Cluster";
        }
    }
}