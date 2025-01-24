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

namespace MatterDotNet.Clusters.Media
{
    /// <summary>
    /// This cluster provides an interface for sending targeted commands to an Observer of a Content App on a Video Player device such as a Streaming Media Player, Smart TV or Smart Screen. The cluster server for Content App Observer is implemented by an endpoint that communicates with a Content App, such as a Casting Video Client. The cluster client for Content App Observer is implemented by a Content App endpoint. A Content App is informed of the NodeId of an Observer when a binding is set on the Content App. The Content App can then send the ContentAppMessage to the Observer (server cluster), and the Observer responds with a ContentAppMessageResponse.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ContentAppObserver : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0510;

        /// <summary>
        /// This cluster provides an interface for sending targeted commands to an Observer of a Content App on a Video Player device such as a Streaming Media Player, Smart TV or Smart Screen. The cluster server for Content App Observer is implemented by an endpoint that communicates with a Content App, such as a Casting Video Client. The cluster client for Content App Observer is implemented by a Content App endpoint. A Content App is informed of the NodeId of an Observer when a binding is set on the Content App. The Content App can then send the ContentAppMessage to the Observer (server cluster), and the Observer responds with a ContentAppMessageResponse.
        /// </summary>
        public ContentAppObserver(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ContentAppObserver(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Status
        /// </summary>
        public enum Status : byte {
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
            public string? Data { get; set; }
            public required string EncodingHint { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (Data != null)
                    writer.WriteString(0, Data);
                writer.WriteString(1, EncodingHint);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Content App Message Response - Reply from server
        /// </summary>
        public struct ContentAppMessageResponse() {
            public required Status Status { get; set; }
            public string? Data { get; set; }
            public string? EncodingHint { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Content App Message
        /// </summary>
        public async Task<ContentAppMessageResponse?> ContentAppMessage(SecureSession session, string? data, string encodingHint) {
            ContentAppMessagePayload requestFields = new ContentAppMessagePayload() {
                Data = data,
                EncodingHint = encodingHint,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ContentAppMessageResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
                EncodingHint = (string?)GetOptionalField(resp, 2),
            };
        }
        #endregion Commands


        /// <inheritdoc />
        public override string ToString() {
            return "Content App Observer";
        }
    }
}