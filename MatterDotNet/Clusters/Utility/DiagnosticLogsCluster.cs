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
using MatterDotNet.Util;

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Diagnostic Logs Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class DiagnosticLogsCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0032;

        /// <summary>
        /// Diagnostic Logs Cluster
        /// </summary>
        public DiagnosticLogsCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected DiagnosticLogsCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Intent
        /// </summary>
        public enum IntentEnum {
            /// <summary>
            /// Logs to be used for end-user support
            /// </summary>
            EndUserSupport = 0,
            /// <summary>
            /// Logs to be used for network diagnostics
            /// </summary>
            NetworkDiag = 1,
            /// <summary>
            /// Obtain crash logs from the Node
            /// </summary>
            CrashLogs = 2,
        }

        /// <summary>
        /// Status
        /// </summary>
        public enum StatusEnum {
            /// <summary>
            /// Successful transfer of logs
            /// </summary>
            Success = 0,
            /// <summary>
            /// All logs has been transferred
            /// </summary>
            Exhausted = 1,
            /// <summary>
            /// No logs of the requested type available
            /// </summary>
            NoLogs = 2,
            /// <summary>
            /// Unable to handle request, retry later
            /// </summary>
            Busy = 3,
            /// <summary>
            /// The request is denied, no logs being transferred
            /// </summary>
            Denied = 4,
        }

        /// <summary>
        /// Transfer Protocol
        /// </summary>
        public enum TransferProtocolEnum {
            /// <summary>
            /// Logs to be returned as a response
            /// </summary>
            ResponsePayload = 0,
            /// <summary>
            /// Logs to be returned using BDX
            /// </summary>
            BDX = 1,
        }
        #endregion Enums

        #region Payloads
        private record RetrieveLogsRequestPayload : TLVPayload {
            public required IntentEnum Intent { get; set; }
            public required TransferProtocolEnum RequestedProtocol { get; set; }
            public string? TransferFileDesignator { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Intent);
                writer.WriteUShort(1, (ushort)RequestedProtocol);
                if (TransferFileDesignator != null)
                    writer.WriteString(2, TransferFileDesignator, 32);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Retrieve Logs Response - Reply from server
        /// </summary>
        public struct RetrieveLogsResponse() {
            public required StatusEnum Status { get; set; }
            public required byte[] LogContent { get; set; }
            public DateTime? UTCTimeStamp { get; set; }
            public TimeSpan? TimeSinceBoot { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Retrieve Logs Request
        /// </summary>
        public async Task<RetrieveLogsResponse?> RetrieveLogsRequest(SecureSession session, IntentEnum Intent, TransferProtocolEnum RequestedProtocol, string? TransferFileDesignator) {
            RetrieveLogsRequestPayload requestFields = new RetrieveLogsRequestPayload() {
                Intent = Intent,
                RequestedProtocol = RequestedProtocol,
                TransferFileDesignator = TransferFileDesignator,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new RetrieveLogsResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                LogContent = (byte[])GetField(resp, 1),
                UTCTimeStamp = (DateTime?)GetOptionalField(resp, 2),
                TimeSinceBoot = (TimeSpan?)GetOptionalField(resp, 3),
            };
        }
        #endregion Commands


        /// <inheritdoc />
        public override string ToString() {
            return "Diagnostic Logs Cluster";
        }
    }
}