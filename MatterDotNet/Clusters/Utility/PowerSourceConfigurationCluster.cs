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

using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Sessions;

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Power Source Configuration Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class PowerSourceConfigurationCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x002E;

        /// <summary>
        /// Power Source Configuration Cluster
        /// </summary>
        public PowerSourceConfigurationCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected PowerSourceConfigurationCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Attributes
        /// <summary>
        /// Get the Sources attribute
        /// </summary>
        public async Task<ushort[]> GetSources(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            ushort[] list = new ushort[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetUShort(i)!.Value;
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Power Source Configuration Cluster";
        }
    }
}