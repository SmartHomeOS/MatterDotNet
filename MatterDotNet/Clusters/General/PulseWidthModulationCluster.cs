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
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// Cluster to control pulse width modulation
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class PulseWidthModulation : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x001c;

        /// <summary>
        /// Cluster to control pulse width modulation
        /// </summary>
        [SetsRequiredMembers]
        public PulseWidthModulation(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected PulseWidthModulation(uint cluster, ushort endPoint) : base(cluster, endPoint) {
        }


        /// <inheritdoc />
        public override string ToString() {
            return "Pulse Width Modulation";
        }
    }
}