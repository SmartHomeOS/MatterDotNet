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
    /// Laundry Dryer Controls Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class LaundryDryerControlsCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x004A;

        /// <summary>
        /// Laundry Dryer Controls Cluster
        /// </summary>
        public LaundryDryerControlsCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected LaundryDryerControlsCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Dryness Level
        /// </summary>
        public enum DrynessLevelEnum {
            /// <summary>
            /// Provides a low dryness level for the selected mode
            /// </summary>
            Low = 0,
            /// <summary>
            /// Provides the normal level of dryness for the selected mode
            /// </summary>
            Normal = 1,
            /// <summary>
            /// Provides an extra dryness level for the selected mode
            /// </summary>
            Extra = 2,
            /// <summary>
            /// Provides the max dryness level for the selected mode
            /// </summary>
            Max = 3,
        }
        #endregion Enums

        #region Attributes
        /// <summary>
        /// Get the Supported Dryness Levels attribute
        /// </summary>
        public async Task<DrynessLevelEnum[]> GetSupportedDrynessLevels(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            DrynessLevelEnum[] list = new DrynessLevelEnum[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = (DrynessLevelEnum)reader.GetUShort(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Selected Dryness Level attribute
        /// </summary>
        public async Task<DrynessLevelEnum?> GetSelectedDrynessLevel(SecureSession session) {
            return (DrynessLevelEnum?)await GetEnumAttribute(session, 1, true);
        }

        /// <summary>
        /// Set the Selected Dryness Level attribute
        /// </summary>
        public async Task SetSelectedDrynessLevel (SecureSession session, DrynessLevelEnum? value) {
            await SetAttribute(session, 1, value, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Laundry Dryer Controls Cluster";
        }
    }
}