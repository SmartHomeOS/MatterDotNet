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
    /// Pressure Measurement Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class PressureMeasurementCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0403;

        /// <summary>
        /// Pressure Measurement Cluster
        /// </summary>
        public PressureMeasurementCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected PressureMeasurementCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Extended range and resolution
            /// </summary>
            Extended = 1,
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

        /// <summary>
        /// Get the Scaled Value attribute
        /// </summary>
        public async Task<short?> GetScaledValue(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 16, true) ?? 0;
        }

        /// <summary>
        /// Get the Min Scaled Value attribute
        /// </summary>
        public async Task<short?> GetMinScaledValue(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 17, true) ?? 0;
        }

        /// <summary>
        /// Get the Max Scaled Value attribute
        /// </summary>
        public async Task<short?> GetMaxScaledValue(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 18, true) ?? 0;
        }

        /// <summary>
        /// Get the Scaled Tolerance attribute
        /// </summary>
        public async Task<ushort> GetScaledTolerance(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 19) ?? 0;
        }

        /// <summary>
        /// Get the Scale attribute
        /// </summary>
        public async Task<sbyte> GetScale(SecureSession session) {
            return (sbyte?)(dynamic?)await GetAttribute(session, 20) ?? 0;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Pressure Measurement Cluster";
        }
    }
}