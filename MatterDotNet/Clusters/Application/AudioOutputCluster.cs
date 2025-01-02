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
    /// Audio Output Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class AudioOutputCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x050B;

        /// <summary>
        /// Audio Output Cluster
        /// </summary>
        public AudioOutputCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected AudioOutputCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum OutputTypeEnum {
            /// <summary>
            /// HDMI
            /// </summary>
            HDMI = 0,
            BT = 1,
            Optical = 2,
            Headphone = 3,
            Internal = 4,
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

            [SetsRequiredMembers]
            internal OutputInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Index = reader.GetByte(0)!.Value;
                OutputType = (OutputTypeEnum)reader.GetUShort(1)!.Value;
                Name = reader.GetString(2, false)!;
            }
            public required byte Index { get; set; }
            public required OutputTypeEnum OutputType { get; set; }
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
        public async Task<bool> SelectOutput(SecureSession session, byte Index) {
            SelectOutputPayload requestFields = new SelectOutputPayload() {
                Index = Index,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Rename Output
        /// </summary>
        public async Task<bool> RenameOutput(SecureSession session, byte Index, string Name) {
            RenameOutputPayload requestFields = new RenameOutputPayload() {
                Index = Index,
                Name = Name,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x01, requestFields);
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
        /// Get the Output List attribute
        /// </summary>
        public async Task<List<OutputInfo>> GetOutputList(SecureSession session) {
            List<OutputInfo> list = new List<OutputInfo>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add(new OutputInfo(reader.GetStruct(i)!));
            return list;
        }

        /// <summary>
        /// Get the Current Output attribute
        /// </summary>
        public async Task<byte> GetCurrentOutput(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 1))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Audio Output Cluster";
        }
    }
}