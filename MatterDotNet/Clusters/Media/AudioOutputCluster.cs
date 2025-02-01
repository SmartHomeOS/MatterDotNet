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

namespace MatterDotNet.Clusters.Media
{
    /// <summary>
    /// This cluster provides an interface for controlling the Output on a media device such as a TV.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class AudioOutput : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x050b;

        /// <summary>
        /// This cluster provides an interface for controlling the Output on a media device such as a TV.
        /// </summary>
        [SetsRequiredMembers]
        public AudioOutput(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected AudioOutput(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            OutputList = new ReadAttribute<OutputInfo[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    OutputInfo[] list = new OutputInfo[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new OutputInfo(reader.GetStruct(i)!);
                    return list;
                }
            };
            CurrentOutput = new ReadAttribute<byte>(cluster, endPoint, 1) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0x00

            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports updates to output names
            /// </summary>
            NameUpdates = 1,
        }

        /// <summary>
        /// Output Type
        /// </summary>
        public enum OutputType : byte {
            /// <summary>
            /// HDMI
            /// </summary>
            HDMI = 0,
            /// <summary>
            /// 
            /// </summary>
            BT = 1,
            /// <summary>
            /// 
            /// </summary>
            Optical = 2,
            /// <summary>
            /// 
            /// </summary>
            Headphone = 3,
            /// <summary>
            /// 
            /// </summary>
            Internal = 4,
            /// <summary>
            /// 
            /// </summary>
            Other = 5,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Output Info
        /// </summary>
        public record OutputInfo : TLVPayload {
            /// <summary>
            /// Output Info
            /// </summary>
            public OutputInfo() { }

            /// <summary>
            /// Output Info
            /// </summary>
            [SetsRequiredMembers]
            public OutputInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Index = reader.GetByte(0)!.Value;
                OutputType = (OutputType)reader.GetUShort(1)!.Value;
                Name = reader.GetString(2, false)!;
            }
            public required byte Index { get; set; }
            public required OutputType OutputType { get; set; }
            public required string Name { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Index);
                writer.WriteUShort(1, (ushort)OutputType);
                writer.WriteString(2, Name);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record SelectOutputPayload : TLVPayload {
            public required byte Index { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Index);
                writer.EndContainer();
            }
        }

        private record RenameOutputPayload : TLVPayload {
            public required byte Index { get; set; }
            public required string Name { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Index);
                writer.WriteString(1, Name);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Select Output
        /// </summary>
        public async Task<bool> SelectOutput(SecureSession session, byte index, CancellationToken token = default) {
            SelectOutputPayload requestFields = new SelectOutputPayload() {
                Index = index,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Rename Output
        /// </summary>
        public async Task<bool> RenameOutput(SecureSession session, byte index, string name, CancellationToken token = default) {
            RenameOutputPayload requestFields = new RenameOutputPayload() {
                Index = index,
                Name = name,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields, token);
            return ValidateResponse(resp);
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Features supported by this cluster
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task<Feature> GetSupportedFeatures(SecureSession session)
        {
            return (Feature)(byte)(await GetAttribute(session, 0xFFFC))!;
        }

        /// <summary>
        /// Returns true when the feature is supported by the cluster
        /// </summary>
        /// <param name="session"></param>
        /// <param name="feature"></param>
        /// <returns></returns>
        public async Task<bool> Supports(SecureSession session, Feature feature)
        {
            return ((feature & await GetSupportedFeatures(session)) != 0);
        }

        /// <summary>
        /// Output List Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<OutputInfo[]> OutputList { get; init; }

        /// <summary>
        /// Current Output Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> CurrentOutput { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Audio Output";
        }
    }
}