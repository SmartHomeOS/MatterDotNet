﻿// MatterDotNet Copyright (C) 2025 
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
    /// Bridged Device Basic Information Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class BridgedDeviceBasicInformationCluster : BasicInformationCluster
    {
        internal const uint CLUSTER_ID = 0x0039;

        /// <summary>
        /// Bridged Device Basic Information Cluster
        /// </summary>
        public BridgedDeviceBasicInformationCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }

        #region Attributes
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Bridged Device Basic Information Cluster";
        }
    }
}