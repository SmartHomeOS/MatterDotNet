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
    /// Administrator Commissioning Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class AdministratorCommissioningCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x003C;

        /// <summary>
        /// Administrator Commissioning Cluster
        /// </summary>
        public AdministratorCommissioningCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected AdministratorCommissioningCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        /// Commissioning Window Status
        /// </summary>
        public enum CommissioningWindowStatusEnum {
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
                writer.WriteUShort(2, Discriminator, 4095);
                writer.WriteUInt(3, Iterations, 100000, 1000);
                writer.WriteBytes(4, Salt, 32, 16);
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
        public async Task<bool> OpenCommissioningWindow(SecureSession session, ushort commandTimeoutMS, ushort CommissioningTimeout, byte[] PAKEPasscodeVerifier, ushort Discriminator, uint Iterations, byte[] Salt) {
            OpenCommissioningWindowPayload requestFields = new OpenCommissioningWindowPayload() {
                CommissioningTimeout = CommissioningTimeout,
                PAKEPasscodeVerifier = PAKEPasscodeVerifier,
                Discriminator = Discriminator,
                Iterations = Iterations,
                Salt = Salt,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x00, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Open Basic Commissioning Window
        /// </summary>
        public async Task<bool> OpenBasicCommissioningWindow(SecureSession session, ushort commandTimeoutMS, ushort CommissioningTimeout) {
            OpenBasicCommissioningWindowPayload requestFields = new OpenBasicCommissioningWindowPayload() {
                CommissioningTimeout = CommissioningTimeout,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x01, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Revoke Commissioning
        /// </summary>
        public async Task<bool> RevokeCommissioning(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x02, commandTimeoutMS);
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
        /// Get the Window Status attribute
        /// </summary>
        public async Task<CommissioningWindowStatusEnum> GetWindowStatus(SecureSession session) {
            return (CommissioningWindowStatusEnum)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Get the Admin Fabric Index attribute
        /// </summary>
        public async Task<byte?> GetAdminFabricIndex(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 1, true);
        }

        /// <summary>
        /// Get the Admin Vendor Id attribute
        /// </summary>
        public async Task<ushort?> GetAdminVendorId(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 2, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Administrator Commissioning Cluster";
        }
    }
}