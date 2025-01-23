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

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// The Sample MEI cluster showcases a cluster manufacturer extensions
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class SampleMEI : ClusterBase
    {
        internal const uint CLUSTER_ID = 0xFFF1FC20;

        /// <summary>
        /// The Sample MEI cluster showcases a cluster manufacturer extensions
        /// </summary>
        public SampleMEI(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected SampleMEI(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Payloads
        /// <summary>
        /// Add Arguments Response - Reply from server
        /// </summary>
        public struct AddArgumentsResponse() {
            public required byte ReturnValue { get; set; }
        }

        private record AddArgumentsPayload : TLVPayload {
            public required byte Arg1 { get; set; }
            public required byte Arg2 { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Arg1);
                writer.WriteByte(1, Arg2);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Add Arguments
        /// </summary>
        public async Task<AddArgumentsResponse?> AddArguments(SecureSession session, byte arg1, byte arg2) {
            AddArgumentsPayload requestFields = new AddArgumentsPayload() {
                Arg1 = arg1,
                Arg2 = arg2,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new AddArgumentsResponse() {
                ReturnValue = (byte)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Ping
        /// </summary>
        public async Task<bool> Ping(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00);
            return ValidateResponse(resp);
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Get the Flip Flop attribute
        /// </summary>
        public async Task<bool> GetFlipFlop(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 0) ?? false;
        }

        /// <summary>
        /// Set the Flip Flop attribute
        /// </summary>
        public async Task SetFlipFlop (SecureSession session, bool? value = false) {
            await SetAttribute(session, 0, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Sample MEI";
        }
    }
}