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
    /// Commands to trigger a Node to allow a new Administrator to commission it.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class AdministratorCommissioning : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x003c;

        /// <summary>
        /// Commands to trigger a Node to allow a new Administrator to commission it.
        /// </summary>
        [SetsRequiredMembers]
        public AdministratorCommissioning(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected AdministratorCommissioning(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            WindowStatus = new ReadAttribute<CommissioningWindowStatus>(cluster, endPoint, 0) {
                Deserialize = x => (CommissioningWindowStatus)DeserializeEnum(x)!
            };
            AdminFabricIndex = new ReadAttribute<byte?>(cluster, endPoint, 1, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            AdminVendorId = new ReadAttribute<ushort?>(cluster, endPoint, 2, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Node supports Basic Commissioning Method.
            /// </summary>
            Basic = 1,
        }

        /// <summary>
        /// Status Code
        /// </summary>
        public enum StatusCode : byte {
            Busy = 2,
            PAKEParameterError = 3,
            WindowNotOpen = 4,
        }

        /// <summary>
        /// Commissioning Window Status
        /// </summary>
        public enum CommissioningWindowStatus : byte {
            /// <summary>
            /// Commissioning window not open
            /// </summary>
            WindowNotOpen = 0,
            /// <summary>
            /// An Enhanced Commissioning Method window is open
            /// </summary>
            EnhancedWindowOpen = 1,
            /// <summary>
            /// A Basic Commissioning Method window is open
            /// </summary>
            BasicWindowOpen = 2,
        }
        #endregion Enums

        #region Payloads
        private record OpenCommissioningWindowPayload : TLVPayload {
            public required ushort CommissioningTimeout { get; set; }
            public required byte[] PAKEPasscodeVerifier { get; set; }
            public required ushort Discriminator { get; set; }
            public required uint Iterations { get; set; }
            public required byte[] Salt { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, CommissioningTimeout);
                writer.WriteBytes(1, PAKEPasscodeVerifier);
                writer.WriteUShort(2, Discriminator);
                writer.WriteUInt(3, Iterations);
                writer.WriteBytes(4, Salt, 32);
                writer.EndContainer();
            }
        }

        private record OpenBasicCommissioningWindowPayload : TLVPayload {
            public required ushort CommissioningTimeout { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, CommissioningTimeout);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Open Commissioning Window
        /// </summary>
        public async Task<bool> OpenCommissioningWindow(SecureSession session, ushort commandTimeoutMS, ushort commissioningTimeout, byte[] pAKEPasscodeVerifier, ushort discriminator, uint iterations, byte[] salt, CancellationToken token = default) {
            OpenCommissioningWindowPayload requestFields = new OpenCommissioningWindowPayload() {
                CommissioningTimeout = commissioningTimeout,
                PAKEPasscodeVerifier = pAKEPasscodeVerifier,
                Discriminator = discriminator,
                Iterations = iterations,
                Salt = salt,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x00, commandTimeoutMS, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Open Basic Commissioning Window
        /// </summary>
        public async Task<bool> OpenBasicCommissioningWindow(SecureSession session, ushort commandTimeoutMS, ushort commissioningTimeout, CancellationToken token = default) {
            OpenBasicCommissioningWindowPayload requestFields = new OpenBasicCommissioningWindowPayload() {
                CommissioningTimeout = commissioningTimeout,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x01, commandTimeoutMS, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Revoke Commissioning
        /// </summary>
        public async Task<bool> RevokeCommissioning(SecureSession session, ushort commandTimeoutMS, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x02, commandTimeoutMS, null, token);
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
        /// Window Status Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<CommissioningWindowStatus> WindowStatus { get; init; }

        /// <summary>
        /// Admin Fabric Index Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> AdminFabricIndex { get; init; }

        /// <summary>
        /// Admin Vendor Id Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort?> AdminVendorId { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Administrator Commissioning";
        }
    }
}