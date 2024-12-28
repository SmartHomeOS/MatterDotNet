// MatterDotNet Copyright (C) 2024 
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
using MatterDotNet.Protocol;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;

namespace MatterDotNet.Clusters
{
    /// <summary>
    /// Administrator Commissioning Cluster
    /// </summary>
    public class AdministratorCommissioningCluster : ClusterBase
    {
        private const uint CLUSTER_ID = 0x003C;

        /// <summary>
        /// Administrator Commissioning Cluster
        /// </summary>
        public AdministratorCommissioningCluster(ushort endPoint) : base(endPoint) { }

        #region Enums
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
                writer.WriteUShort(2, Discriminator);
                writer.WriteUInt(3, Iterations);
                writer.WriteBytes(4, Salt);
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
        public async Task<bool> OpenCommissioningWindow(SecureSession session, ushort CommissioningTimeout, byte[] PAKEPasscodeVerifier, ushort Discriminator, uint Iterations, byte[] Salt) {
            OpenCommissioningWindowPayload requestFields = new OpenCommissioningWindowPayload() {
                CommissioningTimeout = CommissioningTimeout,
                PAKEPasscodeVerifier = PAKEPasscodeVerifier,
                Discriminator = Discriminator,
                Iterations = Iterations,
                Salt = Salt,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x00, requestFields);
            return validateResponse(resp);
        }

        /// <summary>
        /// Open Basic Commissioning Window
        /// </summary>
        public async Task<bool> OpenBasicCommissioningWindow(SecureSession session, ushort CommissioningTimeout) {
            OpenBasicCommissioningWindowPayload requestFields = new OpenBasicCommissioningWindowPayload() {
                CommissioningTimeout = CommissioningTimeout,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x01, requestFields);
            return validateResponse(resp);
        }

        /// <summary>
        /// Revoke Commissioning
        /// </summary>
        public async Task<bool> RevokeCommissioning(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x02);
            return validateResponse(resp);
        }
        #endregion Commands

        #region Attributes
        public CommissioningWindowStatusEnum WindowStatus { get; }

        public byte AdminFabricIndex { get; }

        public ushort AdminVendorId { get; }
        #endregion Attributes
    }
}