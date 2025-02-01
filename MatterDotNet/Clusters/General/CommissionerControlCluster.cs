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
    /// Supports the ability for clients to request the commissioning of themselves or other nodes onto a fabric which the cluster server can commission onto.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class CommissionerControl : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0751;

        /// <summary>
        /// Supports the ability for clients to request the commissioning of themselves or other nodes onto a fabric which the cluster server can commission onto.
        /// </summary>
        [SetsRequiredMembers]
        public CommissionerControl(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected CommissionerControl(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            SupportedDeviceCategories = new ReadAttribute<SupportedDeviceCategory>(cluster, endPoint, 0) {
                Deserialize = x => (SupportedDeviceCategory)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Device Category
        /// </summary>
        [Flags]
        public enum SupportedDeviceCategory : uint {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Aggregators which support Fabric Synchronization may be commissioned.
            /// </summary>
            FabricSynchronization = 0x0001,
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
            public required ushort ResponseTimeoutSeconds { get; set; }
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
        public async Task<bool> RequestCommissioningApproval(SecureSession session, ulong requestID, ushort vendorID, ushort productID, string? label, CancellationToken token = default) {
            RequestCommissioningApprovalPayload requestFields = new RequestCommissioningApprovalPayload() {
                RequestID = requestID,
                VendorID = vendorID,
                ProductID = productID,
                Label = label,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Commission Node
        /// </summary>
        public async Task<ReverseOpenCommissioningWindow?> CommissionNode(SecureSession session, ulong requestID, ushort responseTimeoutSeconds, CancellationToken token = default) {
            CommissionNodePayload requestFields = new CommissionNodePayload() {
                RequestID = requestID,
                ResponseTimeoutSeconds = responseTimeoutSeconds,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields, token);
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
        /// Supported Device Categories Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<SupportedDeviceCategory> SupportedDeviceCategories { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Commissioner Control";
        }
    }
}