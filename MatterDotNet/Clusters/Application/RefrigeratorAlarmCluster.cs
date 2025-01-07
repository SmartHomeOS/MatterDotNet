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

using MatterDotNet.Protocol.Sessions;

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Refrigerator Alarm Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class RefrigeratorAlarmCluster : AlarmBaseCluster<RefrigeratorAlarmCluster.AlarmBitmap>
    {
        internal const uint CLUSTER_ID = 0x0057;

        /// <summary>
        /// Refrigerator Alarm Cluster
        /// </summary>
        public RefrigeratorAlarmCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public new enum Feature {
            /// <summary>
            /// Supports the ability to reset alarms
            /// </summary>
            Reset = 1,
        }

        /// <summary>
        /// Alarm Bitmap
        /// </summary>
        [Flags]
        public enum AlarmBitmap {
            /// <summary>
            /// The cabinet's door has been open for a vendor defined amount of time.
            /// </summary>
            DoorOpen = 1,
        }
        #endregion Enums

        #region Attributes
        /// <summary>
        /// Features supported by this cluster
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public new async Task<Feature> GetSupportedFeatures(SecureSession session)
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
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Refrigerator Alarm Cluster";
        }
    }
}