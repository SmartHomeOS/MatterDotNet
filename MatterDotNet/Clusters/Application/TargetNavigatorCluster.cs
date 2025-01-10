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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Target Navigator Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class TargetNavigatorCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0505;

        /// <summary>
        /// Target Navigator Cluster
        /// </summary>
        public TargetNavigatorCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected TargetNavigatorCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
            /// Requested target was not found in the TargetList
            /// </summary>
            TargetNotFound = 1,
            /// <summary>
            /// Target request is not allowed in current state.
            /// </summary>
            NotAllowed = 2,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Target Info
        /// </summary>
        public record TargetInfo : TLVPayload {
            /// <summary>
            /// Target Info
            /// </summary>
            public TargetInfo() { }

            [SetsRequiredMembers]
            internal TargetInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Identifier = reader.GetByte(0)!.Value;
                Name = reader.GetString(1, false)!;
            }
            public required byte Identifier { get; set; }
            public required string Name { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Identifier, 254);
                writer.WriteString(1, Name);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record NavigateTargetPayload : TLVPayload {
            public required byte Target { get; set; }
            public string? Data { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Target);
                if (Data != null)
                    writer.WriteString(1, Data);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Navigate Target Response - Reply from server
        /// </summary>
        public struct NavigateTargetResponse() {
            public required StatusEnum Status { get; set; }
            public string? Data { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Navigate Target
        /// </summary>
        public async Task<NavigateTargetResponse?> NavigateTarget(SecureSession session, byte Target, string? Data) {
            NavigateTargetPayload requestFields = new NavigateTargetPayload() {
                Target = Target,
                Data = Data,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new NavigateTargetResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Get the Target List attribute
        /// </summary>
        public async Task<TargetInfo[]> GetTargetList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            TargetInfo[] list = new TargetInfo[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new TargetInfo(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Current Target attribute
        /// </summary>
        public async Task<byte> GetCurrentTarget(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 1) ?? 0xFF;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Target Navigator Cluster";
        }
    }
}