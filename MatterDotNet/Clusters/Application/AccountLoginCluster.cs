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

namespace MatterDotNet.Clusters.Media
{
    /// <summary>
    /// This cluster provides commands that facilitate user account login on a Content App or a node. For example, a Content App running on a Video Player device, which is represented as an endpoint (see [TV Architecture]), can use this cluster to help make the user account on the Content App match the user account on the Client.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class AccountLogin : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x050e;

        /// <summary>
        /// This cluster provides commands that facilitate user account login on a Content App or a node. For example, a Content App running on a Video Player device, which is represented as an endpoint (see [TV Architecture]), can use this cluster to help make the user account on the Content App match the user account on the Client.
        /// </summary>
        public AccountLogin(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected AccountLogin(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Payloads
        private record GetSetupPINPayload : TLVPayload {
            public required string TempAccountIdentifier { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, TempAccountIdentifier, 100);
                writer.EndContainer();
            }
        }

        private record LoginPayload : TLVPayload {
            public required string TempAccountIdentifier { get; set; }
            public required string SetupPIN { get; set; }
            public ulong? Node { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, TempAccountIdentifier, 100);
                writer.WriteString(1, SetupPIN);
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

        /// <summary>
        /// Get Setup PIN Response - Reply from server
        /// </summary>
        public struct GetSetupPINResponse() {
            public required string SetupPIN { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Get Setup PIN
        /// </summary>
        public async Task<GetSetupPINResponse?> GetSetupPIN(SecureSession session, ushort commandTimeoutMS, string tempAccountIdentifier) {
            GetSetupPINPayload requestFields = new GetSetupPINPayload() {
                TempAccountIdentifier = tempAccountIdentifier,
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
        public async Task<bool> Login(SecureSession session, ushort commandTimeoutMS, string tempAccountIdentifier, string setupPIN, ulong? node) {
            LoginPayload requestFields = new LoginPayload() {
                TempAccountIdentifier = tempAccountIdentifier,
                SetupPIN = setupPIN,
                Node = node,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x02, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Logout
        /// </summary>
        public async Task<bool> Logout(SecureSession session, ushort commandTimeoutMS, ulong? node) {
            LogoutPayload requestFields = new LogoutPayload() {
                Node = node,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x03, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }
        #endregion Commands


        /// <inheritdoc />
        public override string ToString() {
            return "Account Login";
        }
    }
}