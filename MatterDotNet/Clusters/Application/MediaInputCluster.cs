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
    /// Media Input Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class MediaInputCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0507;

        /// <summary>
        /// Media Input Cluster
        /// </summary>
        public MediaInputCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected MediaInputCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum InputTypeEnum {
            /// <summary>
            /// Indicates content not coming from a physical input.
            /// </summary>
            Internal = 0,
            Aux = 1,
            Coax = 2,
            Composite = 3,
            HDMI = 4,
            Input = 5,
            Line = 6,
            Optical = 7,
            Video = 8,
            SCART = 9,
            USB = 10,
            Other = 11,
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

            [SetsRequiredMembers]
            internal InputInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Index = reader.GetByte(0)!.Value;
                InputType = (InputTypeEnum)reader.GetUShort(1)!.Value;
                Name = reader.GetString(2, false)!;
                Description = reader.GetString(3, false)!;
            }
            public required byte Index { get; set; }
            public required InputTypeEnum InputType { get; set; }
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
        public async Task<bool> SelectInput(SecureSession session, byte Index) {
            SelectInputPayload requestFields = new SelectInputPayload() {
                Index = Index,
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
        public async Task<bool> RenameInput(SecureSession session, byte Index, string Name) {
            RenameInputPayload requestFields = new RenameInputPayload() {
                Index = Index,
                Name = Name,
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
        /// Get the Input List attribute
        /// </summary>
        public async Task<List<InputInfo>> GetInputList(SecureSession session) {
            List<InputInfo> list = new List<InputInfo>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add(new InputInfo(reader.GetStruct(i)!));
            return list;
        }

        /// <summary>
        /// Get the Current Input attribute
        /// </summary>
        public async Task<byte> GetCurrentInput(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 1))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Media Input Cluster";
        }
    }
}