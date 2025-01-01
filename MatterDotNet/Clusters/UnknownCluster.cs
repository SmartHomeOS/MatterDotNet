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

namespace MatterDotNet.Clusters
{
    public class UnknownCluster : ClusterBase
    {
        public UnknownCluster(uint cluster, ushort endPoint) : base(cluster, endPoint)
        {
        }

        public uint ClusterID { get { return cluster; } }

        public override string ToString()
        {
            return $"Unknown Cluster: 0x{cluster:X4}";
        }
    }
}
