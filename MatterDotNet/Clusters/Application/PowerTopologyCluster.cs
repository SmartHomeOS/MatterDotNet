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

namespace MatterDotNet.Clusters.MeasurementAndSensing
{
    /// <summary>
    /// The Power Topology Cluster provides a mechanism for expressing how power is flowing between endpoints.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class PowerTopology : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x009C;

        /// <summary>
        /// The Power Topology Cluster provides a mechanism for expressing how power is flowing between endpoints.
        /// </summary>
        public PowerTopology(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected PowerTopology(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// This endpoint provides or consumes power to/from the entire node
            /// </summary>
            NodeTopology = 1,
            /// <summary>
            /// This endpoint provides or consumes power to/from itself and its child endpoints
            /// </summary>
            TreeTopology = 2,
            /// <summary>
            /// This endpoint provides or consumes power to/from a specified set of endpoints
            /// </summary>
            SetTopology = 4,
            /// <summary>
            /// The specified set of endpoints may change
            /// </summary>
            DynamicPowerFlow = 8,
        }
        #endregion Enums

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
        /// Get the Available Endpoints attribute
        /// </summary>
        public async Task<ushort[]> GetAvailableEndpoints(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            ushort[] list = new ushort[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetUShort(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Active Endpoints attribute
        /// </summary>
        public async Task<ushort[]> GetActiveEndpoints(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 1))!);
            ushort[] list = new ushort[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetUShort(i)!.Value;
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Power Topology";
        }
    }
}