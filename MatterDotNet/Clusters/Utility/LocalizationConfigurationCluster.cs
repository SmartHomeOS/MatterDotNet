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
    /// Localization Configuration Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class LocalizationConfigurationCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x002B;

        /// <summary>
        /// Localization Configuration Cluster
        /// </summary>
        public LocalizationConfigurationCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected LocalizationConfigurationCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Attributes
        /// <summary>
        /// Get the Active Locale attribute
        /// </summary>
        public async Task<string> GetActiveLocale(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Set the Active Locale attribute
        /// </summary>
        public async Task SetActiveLocale (SecureSession session, string value) {
            await SetAttribute(session, 0, value);
        }

        /// <summary>
        /// Get the Supported Locales attribute
        /// </summary>
        public async Task<List<string>> GetSupportedLocales(SecureSession session) {
            List<string> list = new List<string>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 1))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add(reader.GetString(i, false)!);
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Localization Configuration Cluster";
        }
    }
}