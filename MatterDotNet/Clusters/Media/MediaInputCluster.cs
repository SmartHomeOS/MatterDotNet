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
    /// This cluster provides an interface for controlling the Input Selector on a media device such as a TV.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class MediaInput : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0507;

        /// <summary>
        /// This cluster provides an interface for controlling the Input Selector on a media device such as a TV.
        /// </summary>
        [SetsRequiredMembers]
        public MediaInput(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected MediaInput(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            InputList = new ReadAttribute<InputInfo[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    InputInfo[] list = new InputInfo[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new InputInfo(reader.GetStruct(i)!);
                    return list;
                }
            };
            CurrentInput = new ReadAttribute<byte>(cluster, endPoint, 1) {
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
            /// Supports updates to the input names
            /// </summary>
            NameUpdates = 1,
        }

        /// <summary>
        /// Input Type
        /// </summary>
        public enum InputType : byte {
            /// <summary>
            /// Indicates content not coming from a physical input.
            /// </summary>
            Internal = 0x0,
            /// <summary>
            /// 
            /// </summary>
            Aux = 0x1,
            /// <summary>
            /// 
            /// </summary>
            Coax = 0x2,
            /// <summary>
            /// 
            /// </summary>
            Composite = 0x3,
            /// <summary>
            /// 
            /// </summary>
            HDMI = 0x4,
            /// <summary>
            /// 
            /// </summary>
            Input = 0x5,
            /// <summary>
            /// 
            /// </summary>
            Line = 0x6,
            /// <summary>
            /// 
            /// </summary>
            Optical = 0x7,
            /// <summary>
            /// 
            /// </summary>
            Video = 0x8,
            /// <summary>
            /// 
            /// </summary>
            SCART = 0x9,
            /// <summary>
            /// 
            /// </summary>
            USB = 0xA,
            /// <summary>
            /// 
            /// </summary>
            Other = 0xB,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Input Info
        /// </summary>
        public record InputInfo : TLVPayload {
            /// <summary>
            /// Input Info
            /// </summary>
            public InputInfo() { }

            /// <summary>
            /// Input Info
            /// </summary>
            [SetsRequiredMembers]
            public InputInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Index = reader.GetByte(0)!.Value;
                InputType = (InputType)reader.GetUShort(1)!.Value;
                Name = reader.GetString(2, false)!;
                Description = reader.GetString(3, false)!;
            }
            public required byte Index { get; set; }
            public required InputType InputType { get; set; }
            public required string Name { get; set; }
            public required string Description { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Index);
                writer.WriteUShort(1, (ushort)InputType);
                writer.WriteString(2, Name);
                writer.WriteString(3, Description);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record SelectInputPayload : TLVPayload {
            public required byte Index { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Index);
                writer.EndContainer();
            }
        }

        private record RenameInputPayload : TLVPayload {
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
        /// Select Input
        /// </summary>
        public async Task<bool> SelectInput(SecureSession session, byte index) {
            SelectInputPayload requestFields = new SelectInputPayload() {
                Index = index,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Show Input Status
        /// </summary>
        public async Task<bool> ShowInputStatus(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Hide Input Status
        /// </summary>
        public async Task<bool> HideInputStatus(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Rename Input
        /// </summary>
        public async Task<bool> RenameInput(SecureSession session, byte index, string name) {
            RenameInputPayload requestFields = new RenameInputPayload() {
                Index = index,
                Name = name,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields);
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
        /// Input List Attribute
        /// </summary>
        public required ReadAttribute<InputInfo[]> InputList { get; init; }

        /// <summary>
        /// Current Input Attribute
        /// </summary>
        public required ReadAttribute<byte> CurrentInput { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Media Input";
        }
    }
}