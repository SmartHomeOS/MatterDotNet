// MatterDotNet Copyright (C) 2024 
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

using MatterDotNet.Messages;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;

namespace MatterDotNet.Clusters
{
    /// <summary>
    /// Descriptor Cluster
    /// </summary>
    public class DescriptorCluster : ClusterBase
    {
        private const uint CLUSTER_ID = 0x001D;

        /// <summary>
        /// Descriptor Cluster
        /// </summary>
        public DescriptorCluster(ushort endPoint) : base(endPoint) { }

        #region Records
        public record DeviceType : TLVPayload {
            public required uint DeviceTypeField { get; set; }
            public required ushort Revision { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, DeviceTypeField);
                writer.WriteUShort(1, Revision);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Attributes
        public List<DeviceType> DeviceTypeList { get; }

        public List<uint> ServerList { get; } = [];

        public List<uint> ClientList { get; } = [];

        public List<ushort> PartsList { get; } = [];

        public List<SemanticTag> TagList { get; }
        #endregion Attributes
    }
}