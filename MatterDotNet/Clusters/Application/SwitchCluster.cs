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

using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Sessions;

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Switch Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class SwitchCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x003B;

        /// <summary>
        /// Switch Cluster
        /// </summary>
        public SwitchCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected SwitchCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Switch is latching
            /// </summary>
            LatchingSwitch = 1,
            /// <summary>
            /// Switch is momentary
            /// </summary>
            MomentarySwitch = 2,
            /// <summary>
            /// Switch supports release
            /// </summary>
            MomentarySwitchRelease = 4,
            /// <summary>
            /// Switch supports long press
            /// </summary>
            MomentarySwitchLongPress = 8,
            /// <summary>
            /// Switch supports multi-press
            /// </summary>
            MomentarySwitchMultiPress = 16,
        }
        #endregion Enums

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
        /// Get the Number Of Positions attribute
        /// </summary>
        public async Task<byte> GetNumberOfPositions(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 0) ?? 2;
        }

        /// <summary>
        /// Get the Current Position attribute
        /// </summary>
        public async Task<byte> GetCurrentPosition(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 1) ?? 0;
        }

        /// <summary>
        /// Get the Multi Press Max attribute
        /// </summary>
        public async Task<byte> GetMultiPressMax(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 2) ?? 2;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Switch Cluster";
        }
    }
}