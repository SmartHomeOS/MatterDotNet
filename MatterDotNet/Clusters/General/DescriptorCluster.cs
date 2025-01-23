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

using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// The Descriptor Cluster is meant to replace the support from the Zigbee Device Object (ZDO) for describing a node, its endpoints and clusters.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class Descriptor : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x001d;

        /// <summary>
        /// The Descriptor Cluster is meant to replace the support from the Zigbee Device Object (ZDO) for describing a node, its endpoints and clusters.
        /// </summary>
        public Descriptor(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected Descriptor(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// The TagList attribute is present
            /// </summary>
            TagList = 1,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Device Type
        /// </summary>
        public record DeviceType : TLVPayload {
            /// <summary>
            /// Device Type
            /// </summary>
            public DeviceType() { }

            /// <summary>
            /// Device Type
            /// </summary>
            [SetsRequiredMembers]
            public DeviceType(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                DeviceTypeField = (DeviceTypeEnum)reader.GetUInt(0)!.Value;
                Revision = reader.GetUShort(1)!.Value;
            }
            public required DeviceTypeEnum DeviceTypeField { get; set; }
            public required ushort Revision { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)DeviceTypeField);
                writer.WriteUShort(1, Revision);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Semantic Tag
        /// </summary>
        public record SemanticTag : TLVPayload {
            /// <summary>
            /// Semantic Tag
            /// </summary>
            public SemanticTag() { }

            /// <summary>
            /// Semantic Tag
            /// </summary>
            [SetsRequiredMembers]
            public SemanticTag(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                MfgCode = reader.GetUShort(0, true);
                NamespaceID = reader.GetByte(1)!.Value;
                Tag = reader.GetByte(2)!.Value;
                Label = reader.GetString(3, true);
            }
            public required ushort? MfgCode { get; set; }
            public required byte NamespaceID { get; set; }
            public required byte Tag { get; set; }
            public string? Label { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, MfgCode);
                writer.WriteByte(1, NamespaceID);
                writer.WriteByte(2, Tag);
                if (Label != null)
                    writer.WriteString(3, Label);
                writer.EndContainer();
            }
        }
        #endregion Records

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
        /// Get the Device Type List attribute
        /// </summary>
        public async Task<DeviceType[]> GetDeviceTypeList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            DeviceType[] list = new DeviceType[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new DeviceType(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Server List attribute
        /// </summary>
        public async Task<uint[]> GetServerList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 1))!);
            uint[] list = new uint[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetUInt(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Client List attribute
        /// </summary>
        public async Task<uint[]> GetClientList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 2))!);
            uint[] list = new uint[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetUInt(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Parts List attribute
        /// </summary>
        public async Task<ushort[]> GetPartsList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 3))!);
            ushort[] list = new ushort[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetUShort(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Tag List attribute
        /// </summary>
        public async Task<SemanticTag[]> GetTagList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 4))!);
            SemanticTag[] list = new SemanticTag[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new SemanticTag(reader.GetStruct(i)!);
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Descriptor";
        }
    }
}