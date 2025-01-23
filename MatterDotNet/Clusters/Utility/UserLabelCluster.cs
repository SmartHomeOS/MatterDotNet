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

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// The User Label Cluster provides a feature to tag an endpoint with zero or more labels.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class UserLabel : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0041;

        /// <summary>
        /// The User Label Cluster provides a feature to tag an endpoint with zero or more labels.
        /// </summary>
        public UserLabel(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected UserLabel(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Attributes
        /// <summary>
        /// Get the Label List attribute
        /// </summary>
        public async Task<FixedLabel.Label[]> GetLabelList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            FixedLabel.Label[] list = new FixedLabel.Label[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new FixedLabel.Label(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Set the Label List attribute
        /// </summary>
        public async Task SetLabelList (SecureSession session, FixedLabel.Label[] value) {
            await SetAttribute(session, 0, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "User Label";
        }
    }
}