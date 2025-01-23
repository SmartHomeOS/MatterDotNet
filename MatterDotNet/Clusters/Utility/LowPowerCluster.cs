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
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// This cluster provides an interface for managing low power mode on a device.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class LowPower : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0508;

        /// <summary>
        /// This cluster provides an interface for managing low power mode on a device.
        /// </summary>
        public LowPower(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected LowPower(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Payloads
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Sleep
        /// </summary>
        public async Task<bool> Sleep(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00);
            return ValidateResponse(resp);
        }
        #endregion Commands


        /// <inheritdoc />
        public override string ToString() {
            return "Low Power";
        }
    }
}