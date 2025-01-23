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

namespace MatterDotNet.Clusters.CHIP
{
    /// <summary>
    /// The Fault Injection Cluster provide a means for a test harness to configure faults(for example triggering a fault in the system).
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class FaultInjection : ClusterBase
    {
        internal const uint CLUSTER_ID = 0xFFF1FC06;

        /// <summary>
        /// The Fault Injection Cluster provide a means for a test harness to configure faults(for example triggering a fault in the system).
        /// </summary>
        public FaultInjection(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected FaultInjection(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Fault Type
        /// </summary>
        public enum FaultType : byte {
            Unspecified = 0x00,
            SystemFault = 0x01,
            InetFault = 0x02,
            ChipFault = 0x03,
            CertFault = 0x04,
        }
        #endregion Enums

        #region Payloads
        private record FailAtFaultPayload : TLVPayload {
            public required FaultType Type { get; set; }
            public required uint Id { get; set; }
            public required uint NumCallsToSkip { get; set; }
            public required uint NumCallsToFail { get; set; }
            public required bool TakeMutex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Type);
                writer.WriteUInt(1, Id);
                writer.WriteUInt(2, NumCallsToSkip);
                writer.WriteUInt(3, NumCallsToFail);
                writer.WriteBool(4, TakeMutex);
                writer.EndContainer();
            }
        }

        private record FailRandomlyAtFaultPayload : TLVPayload {
            public required FaultType Type { get; set; }
            public required uint Id { get; set; }
            public required byte Percentage { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Type);
                writer.WriteUInt(1, Id);
                writer.WriteByte(2, Percentage);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Fail At Fault
        /// </summary>
        public async Task<bool> FailAtFault(SecureSession session, FaultType type, uint id, uint numCallsToSkip, uint numCallsToFail, bool takeMutex) {
            FailAtFaultPayload requestFields = new FailAtFaultPayload() {
                Type = type,
                Id = id,
                NumCallsToSkip = numCallsToSkip,
                NumCallsToFail = numCallsToFail,
                TakeMutex = takeMutex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Fail Randomly At Fault
        /// </summary>
        public async Task<bool> FailRandomlyAtFault(SecureSession session, FaultType type, uint id, byte percentage) {
            FailRandomlyAtFaultPayload requestFields = new FailRandomlyAtFaultPayload() {
                Type = type,
                Id = id,
                Percentage = percentage,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            return ValidateResponse(resp);
        }
        #endregion Commands


        /// <inheritdoc />
        public override string ToString() {
            return "Fault Injection";
        }
    }
}