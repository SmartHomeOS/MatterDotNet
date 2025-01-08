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

using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Proxy Discovery Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ProxyDiscoveryCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0043;

        /// <summary>
        /// Proxy Discovery Cluster
        /// </summary>
        public ProxyDiscoveryCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ProxyDiscoveryCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Payloads
        private record ProxyDiscoverRequestPayload : TLVPayload {
            public required ulong SourceNodeId { get; set; }
            public required ushort NumAttributePaths { get; set; }
            public required ushort NumEventPaths { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, SourceNodeId);
                writer.WriteUShort(1, NumAttributePaths);
                writer.WriteUShort(2, NumEventPaths);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Proxy Discover Response - Reply from server
        /// </summary>
        public struct ProxyDiscoverResponse() {
            public required ulong SourceNodeId { get; set; }
            public required ushort NumHopsToSource { get; set; }
            public required ushort AvailableCapacity { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Proxy Discover Request
        /// </summary>
        public async Task<bool> ProxyDiscoverRequest(SecureSession session, ulong SourceNodeId, ushort NumAttributePaths, ushort NumEventPaths) {
            ProxyDiscoverRequestPayload requestFields = new ProxyDiscoverRequestPayload() {
                SourceNodeId = SourceNodeId,
                NumAttributePaths = NumAttributePaths,
                NumEventPaths = NumEventPaths,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }
        #endregion Commands


        /// <inheritdoc />
        public override string ToString() {
            return "Proxy Discovery Cluster";
        }
    }
}