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

using MatterDotNet.Attributes;
using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Media
{
    /// <summary>
    /// This cluster provides an interface for UX navigation within a set of targets on a device or endpoint.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class TargetNavigator : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0505;

        /// <summary>
        /// This cluster provides an interface for UX navigation within a set of targets on a device or endpoint.
        /// </summary>
        [SetsRequiredMembers]
        public TargetNavigator(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected TargetNavigator(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            TargetList = new ReadAttribute<TargetInfo[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    TargetInfo[] list = new TargetInfo[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new TargetInfo(reader.GetStruct(i)!);
                    return list;
                }
            };
            CurrentTarget = new ReadAttribute<byte>(cluster, endPoint, 1) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
        }

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

            /// <summary>
            /// Target Info
            /// </summary>
            [SetsRequiredMembers]
            public TargetInfo(object[] fields) {
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
            public required Status Status { get; set; }
            public string? Data { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Navigate Target
        /// </summary>
        public async Task<NavigateTargetResponse?> NavigateTarget(SecureSession session, byte target, string? data, CancellationToken token = default) {
            NavigateTargetPayload requestFields = new NavigateTargetPayload() {
                Target = target,
                Data = data,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new NavigateTargetResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Target List Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<TargetInfo[]> TargetList { get; init; }

        /// <summary>
        /// Current Target Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> CurrentTarget { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Target Navigator";
        }
    }
}