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
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Valid Proxies Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ValidProxiesCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0044;

        /// <summary>
        /// Valid Proxies Cluster
        /// </summary>
        public ValidProxiesCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ValidProxiesCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Records
        /// <summary>
        /// Valid Proxy
        /// </summary>
        public record ValidProxy : TLVPayload {
            /// <summary>
            /// Valid Proxy
            /// </summary>
            public ValidProxy() { }

            /// <summary>
            /// Valid Proxy
            /// </summary>
            [SetsRequiredMembers]
            public ValidProxy(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                NodeID = reader.GetULong(1)!.Value;
            }
            public required ulong NodeID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(1, NodeID);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        /// <summary>
        /// Get Valid Proxies Response - Reply from server
        /// </summary>
        public struct GetValidProxiesResponse() {
            public required ulong[] ProxyNodeIdList { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Get Valid Proxies Request
        /// </summary>
        public async Task<GetValidProxiesResponse?> GetValidProxiesRequest(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00);
            if (!ValidateResponse(resp))
                return null;
            return new GetValidProxiesResponse() {
                ProxyNodeIdList = (ulong[])GetField(resp, 0),
            };
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Get the Valid Proxy List attribute
        /// </summary>
        public async Task<ValidProxy[]> GetValidProxyList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            ValidProxy[] list = new ValidProxy[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new ValidProxy(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Set the Valid Proxy List attribute
        /// </summary>
        public async Task SetValidProxyList (SecureSession session, ValidProxy[] value) {
            await SetAttribute(session, 0, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Valid Proxies Cluster";
        }
    }
}