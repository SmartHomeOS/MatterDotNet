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
    /// Account Login Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class AccountLoginCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x050E;

        /// <summary>
        /// Account Login Cluster
        /// </summary>
        public AccountLoginCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected AccountLoginCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Payloads
        private record GetSetupPINPayload : TLVPayload {
            public required string TempAccountIdentifier { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, TempAccountIdentifier, 100, 16);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Setup PIN Response - Reply from server
        /// </summary>
        public struct GetSetupPINResponse() {
            public required string SetupPIN { get; set; }
        }

        private record LoginPayload : TLVPayload {
            public required string TempAccountIdentifier { get; set; }
            public required string SetupPIN { get; set; }
            public ulong? Node { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, TempAccountIdentifier, 100, 16);
                writer.WriteString(1, SetupPIN, int.MaxValue, 8);
                if (Node != null)
                    writer.WriteULong(2, Node);
                writer.EndContainer();
            }
        }

        private record LogoutPayload : TLVPayload {
            public ulong? Node { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (Node != null)
                    writer.WriteULong(0, Node);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Get Setup PIN
        /// </summary>
        public async Task<GetSetupPINResponse?> GetSetupPIN(SecureSession session, ushort commandTimeoutMS, string TempAccountIdentifier) {
            GetSetupPINPayload requestFields = new GetSetupPINPayload() {
                TempAccountIdentifier = TempAccountIdentifier,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x00, commandTimeoutMS, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetSetupPINResponse() {
                SetupPIN = (string)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Login
        /// </summary>
        public async Task<bool> Login(SecureSession session, ushort commandTimeoutMS, string TempAccountIdentifier, string SetupPIN, ulong? Node) {
            LoginPayload requestFields = new LoginPayload() {
                TempAccountIdentifier = TempAccountIdentifier,
                SetupPIN = SetupPIN,
                Node = Node,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x02, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Logout
        /// </summary>
        public async Task<bool> Logout(SecureSession session, ushort commandTimeoutMS, ulong? Node) {
            LogoutPayload requestFields = new LogoutPayload() {
                Node = Node,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x03, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }
        #endregion Commands


        /// <inheritdoc />
        public override string ToString() {
            return "Account Login Cluster";
        }
    }
}