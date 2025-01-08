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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Content App Observer Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ContentAppObserverCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0510;

        /// <summary>
        /// Content App Observer Cluster
        /// </summary>
        public ContentAppObserverCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ContentAppObserverCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Status
        /// </summary>
        public enum StatusEnum {
            /// <summary>
            /// Command succeeded
            /// </summary>
            Success = 0,
            /// <summary>
            /// Data field in command was not understood by the Observer
            /// </summary>
            UnexpectedData = 1,
        }
        #endregion Enums

        #region Payloads
        private record ContentAppMessagePayload : TLVPayload {
            public required string Data { get; set; }
            public string? EncodingHint { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Data, 500);
                if (EncodingHint != null)
                    writer.WriteString(1, EncodingHint, 100);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Content App Message Response - Reply from server
        /// </summary>
        public struct ContentAppMessageResponse() {
            public required StatusEnum Status { get; set; }
            public string? Data { get; set; }
            public string? EncodingHint { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Content App Message
        /// </summary>
        public async Task<ContentAppMessageResponse?> ContentAppMessage(SecureSession session, string Data, string? EncodingHint) {
            ContentAppMessagePayload requestFields = new ContentAppMessagePayload() {
                Data = Data,
                EncodingHint = EncodingHint,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ContentAppMessageResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
                EncodingHint = (string?)GetOptionalField(resp, 2),
            };
        }
        #endregion Commands


        /// <inheritdoc />
        public override string ToString() {
            return "Content App Observer Cluster";
        }
    }
}