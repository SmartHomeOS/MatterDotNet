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
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Binding Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class BindingCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x001E;

        /// <summary>
        /// Binding Cluster
        /// </summary>
        public BindingCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected BindingCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Records
        /// <summary>
        /// Target
        /// </summary>
        public record Target : TLVPayload {
            /// <summary>
            /// Target
            /// </summary>
            public Target() { }

            [SetsRequiredMembers]
            internal Target(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Node = reader.GetULong(1)!.Value;
                Group = reader.GetUShort(2)!.Value;
                Endpoint = reader.GetUShort(3)!.Value;
                Cluster = reader.GetUInt(4, true);
            }
            public required ulong Node { get; set; }
            public required ushort Group { get; set; }
            public required ushort Endpoint { get; set; }
            public uint? Cluster { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(1, Node);
                writer.WriteUShort(2, Group, ushort.MaxValue, 1);
                writer.WriteUShort(3, Endpoint);
                if (Cluster != null)
                    writer.WriteUInt(4, Cluster);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Attributes
        /// <summary>
        /// Get the Binding attribute
        /// </summary>
        public async Task<Target[]> GetBinding(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            Target[] list = new Target[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new Target(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Set the Binding attribute
        /// </summary>
        public async Task SetBinding (SecureSession session, Target[] value) {
            await SetAttribute(session, 0, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Binding Cluster";
        }
    }
}