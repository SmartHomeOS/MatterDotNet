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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Temperature Measurement Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 4)]
    public class TemperatureMeasurementCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0402;

        /// <summary>
        /// Temperature Measurement Cluster
        /// </summary>
        public TemperatureMeasurementCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected TemperatureMeasurementCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Attributes
        /// <summary>
        /// Get the Measured Value attribute
        /// </summary>
        public async Task<short?> GetMeasuredValue(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 0, true);
        }

        /// <summary>
        /// Get the Min Measured Value attribute
        /// </summary>
        public async Task<short?> GetMinMeasuredValue(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 1, true);
        }

        /// <summary>
        /// Get the Max Measured Value attribute
        /// </summary>
        public async Task<short?> GetMaxMeasuredValue(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 2, true);
        }

        /// <summary>
        /// Get the Tolerance attribute
        /// </summary>
        public async Task<ushort> GetTolerance(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 3) ?? 0;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Temperature Measurement Cluster";
        }
    }
}