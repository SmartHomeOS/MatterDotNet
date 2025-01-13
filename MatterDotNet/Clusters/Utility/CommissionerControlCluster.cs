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

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Commissioner Control Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class CommissionerControlCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0751;

        /// <summary>
        /// Commissioner Control Cluster
        /// </summary>
        public CommissionerControlCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected CommissionerControlCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Device Category Bitmap
        /// </summary>
        [Flags]
        public enum SupportedDeviceCategoryBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Aggregators which support Fabric Synchronization may be commissioned.
            /// </summary>
            FabricSynchronization = 1,
        }
        #endregion Enums

        #region Payloads
        private record RequestCommissioningApprovalPayload : TLVPayload {
            public required ulong RequestID { get; set; }
            public required ushort VendorID { get; set; }
            public required ushort ProductID { get; set; }
            public string? Label { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, RequestID);
                writer.WriteUShort(1, VendorID);
                writer.WriteUShort(2, ProductID);
                if (Label != null)
                    writer.WriteString(3, Label, 64);
                writer.EndContainer();
            }
        }

        private record CommissionNodePayload : TLVPayload {
            public required ulong RequestID { get; set; }
            public required ushort ResponseTimeoutSeconds { get; set; } = 30;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, RequestID);
                writer.WriteUShort(1, ResponseTimeoutSeconds, 120, 30);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Reverse Open Commissioning Window - Reply from server
        /// </summary>
        public struct ReverseOpenCommissioningWindow() {
            public required ushort CommissioningTimeout { get; set; }
            public required byte[] PAKEPasscodeVerifier { get; set; }
            public required ushort Discriminator { get; set; }
            public required uint Iterations { get; set; }
            public required byte[] Salt { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Request Commissioning Approval
        /// </summary>
        public async Task<bool> RequestCommissioningApproval(SecureSession session, ulong RequestID, ushort VendorID, ushort ProductID, string? Label) {
            RequestCommissioningApprovalPayload requestFields = new RequestCommissioningApprovalPayload() {
                RequestID = RequestID,
                VendorID = VendorID,
                ProductID = ProductID,
                Label = Label,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Commission Node
        /// </summary>
        public async Task<ReverseOpenCommissioningWindow?> CommissionNode(SecureSession session, ulong RequestID, ushort ResponseTimeoutSeconds) {
            CommissionNodePayload requestFields = new CommissionNodePayload() {
                RequestID = RequestID,
                ResponseTimeoutSeconds = ResponseTimeoutSeconds,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ReverseOpenCommissioningWindow() {
                CommissioningTimeout = (ushort)GetField(resp, 0),
                PAKEPasscodeVerifier = (byte[])GetField(resp, 1),
                Discriminator = (ushort)GetField(resp, 2),
                Iterations = (uint)GetField(resp, 3),
                Salt = (byte[])GetField(resp, 4),
            };
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Get the Supported Device Categories attribute
        /// </summary>
        public async Task<SupportedDeviceCategoryBitmap> GetSupportedDeviceCategories(SecureSession session) {
            return (SupportedDeviceCategoryBitmap)await GetEnumAttribute(session, 0);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Commissioner Control Cluster";
        }
    }
}