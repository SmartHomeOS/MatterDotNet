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

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Bridged Device Basic Information Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 4)]
    public class BridgedDeviceBasicInformationCluster : BasicInformationCluster
    {
        internal const uint CLUSTER_ID = 0x0039;

        /// <summary>
        /// Bridged Device Basic Information Cluster
        /// </summary>
        public BridgedDeviceBasicInformationCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Support bridged ICDs.
            /// </summary>
            BridgedICDSupport = 1048576,
        }
        #endregion Enums

        #region Payloads
        private record KeepActivePayload : TLVPayload {
            public required uint StayActiveDuration { get; set; }
            public required uint TimeoutMs { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, StayActiveDuration);
                writer.WriteUInt(1, TimeoutMs, 3600000, 30000);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Keep Active
        /// </summary>
        public async Task<bool> KeepActive(SecureSession session, uint StayActiveDuration, uint TimeoutMs) {
            KeepActivePayload requestFields = new KeepActivePayload() {
                StayActiveDuration = StayActiveDuration,
                TimeoutMs = TimeoutMs,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x80, requestFields);
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
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Bridged Device Basic Information Cluster";
        }
    }
}