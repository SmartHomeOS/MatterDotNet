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
    /// Proxy Configuration Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ProxyConfigurationCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0042;

        /// <summary>
        /// Proxy Configuration Cluster
        /// </summary>
        public ProxyConfigurationCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ProxyConfigurationCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Records
        /// <summary>
        /// Configuration
        /// </summary>
        public record Configuration : TLVPayload {
            /// <summary>
            /// Configuration
            /// </summary>
            public Configuration() { }

            [SetsRequiredMembers]
            internal Configuration(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ProxyAllNodes = reader.GetBool(1)!.Value;
                {
                    SourceList = new ulong[((object[])fields[2]).Length];
                    for (int i = 0; i < SourceList.Length; i++) {
                        SourceList[i] = reader.GetULong(-1)!.Value;
                    }
                }
            }
            public required bool ProxyAllNodes { get; set; } = false;
            public required ulong[] SourceList { get; set; } = Array.Empty<ulong>();
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBool(1, ProxyAllNodes);
                {
                    writer.StartArray(2);
                    foreach (var item in SourceList) {
                        writer.WriteULong(-1, item);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Attributes
        /// <summary>
        /// Get the Configuration List attribute
        /// </summary>
        public async Task<Configuration[]> GetConfigurationList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            Configuration[] list = new Configuration[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new Configuration(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Set the Configuration List attribute
        /// </summary>
        public async Task SetConfigurationList (SecureSession session, Configuration[] value) {
            await SetAttribute(session, 0, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Proxy Configuration Cluster";
        }
    }
}