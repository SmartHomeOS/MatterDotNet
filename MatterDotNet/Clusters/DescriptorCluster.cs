﻿// MatterDotNet Copyright (C) 2025 
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

using MatterDotNet.Messages;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters
{
    /// <summary>
    /// Descriptor Cluster
    /// </summary>
    public class DescriptorCluster : ClusterBase
    {
        private const uint CLUSTER_ID = 0x001D;

        /// <summary>
        /// Descriptor Cluster
        /// </summary>
        public DescriptorCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }

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
            [SetsRequiredMembers]
            internal DeviceType(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                DeviceTypeField = reader.GetUInt(0)!.Value;
                Revision = reader.GetUShort(1)!.Value;
            }
            public required uint DeviceTypeField { get; set; }
            public required ushort Revision { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, DeviceTypeField);
                writer.WriteUShort(1, Revision);
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
        public async Task<List<DeviceType>> GetDeviceTypeList (SecureSession session) {
            return (List<DeviceType>)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Get the Server List attribute
        /// </summary>
        public async Task<List<uint>> GetServerList (SecureSession session) {
            return (List<uint>?)(dynamic?)await GetAttribute(session, 1) ?? new List<uint>();
        }

        /// <summary>
        /// Get the Client List attribute
        /// </summary>
        public async Task<List<uint>> GetClientList (SecureSession session) {
            return (List<uint>?)(dynamic?)await GetAttribute(session, 2) ?? new List<uint>();
        }

        /// <summary>
        /// Get the Parts List attribute
        /// </summary>
        public async Task<List<ushort>> GetPartsList (SecureSession session) {
            return (List<ushort>?)(dynamic?)await GetAttribute(session, 3) ?? new List<ushort>();
        }

        /// <summary>
        /// Get the Tag List attribute
        /// </summary>
        public async Task<List<SemanticTag>> GetTagList (SecureSession session) {
            return (List<SemanticTag>)(dynamic?)(await GetAttribute(session, 4))!;
        }
        #endregion Attributes
    }
}