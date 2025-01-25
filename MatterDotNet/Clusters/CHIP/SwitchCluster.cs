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
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.CHIP
{
    /// <summary>
    /// This cluster exposes interactions with a switch device, for the purpose of using those interactions by other devices.Two types of switch devices are supported: latching switch (e.g. rocker switch) and momentary switch (e.g. push button), distinguished with their feature flags.Interactions with the switch device are exposed as attributes (for the latching switch) and as events (for both types of switches). An interested party MAY subscribe to these attributes/events and thus be informed of the interactions, and can perform actions based on this, for example by sending commands to perform an action such as controlling a light or a window shade.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class Switch : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x003b;

        /// <summary>
        /// This cluster exposes interactions with a switch device, for the purpose of using those interactions by other devices.Two types of switch devices are supported: latching switch (e.g. rocker switch) and momentary switch (e.g. push button), distinguished with their feature flags.Interactions with the switch device are exposed as attributes (for the latching switch) and as events (for both types of switches). An interested party MAY subscribe to these attributes/events and thus be informed of the interactions, and can perform actions based on this, for example by sending commands to perform an action such as controlling a light or a window shade.
        /// </summary>
        [SetsRequiredMembers]
        public Switch(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected Switch(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            NumberOfPositions = new ReadAttribute<byte>(cluster, endPoint, 0) {
                Deserialize = x => (byte?)(dynamic?)x ?? 2

            };
            CurrentPosition = new ReadAttribute<byte>(cluster, endPoint, 1) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            MultiPressMax = new ReadAttribute<byte>(cluster, endPoint, 2) {
                Deserialize = x => (byte?)(dynamic?)x ?? 2

            };
        }

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
            /// Switch supports release events generation
            /// </summary>
            MomentarySwitchRelease = 4,
            /// <summary>
            /// Switch supports long press detection
            /// </summary>
            MomentarySwitchLongPress = 8,
            /// <summary>
            /// Switch supports multi-press detection
            /// </summary>
            MomentarySwitchMultiPress = 16,
            /// <summary>
            /// Switch is momentary, targeted at specific user actions (focus on multi-press and optionally long press) with a reduced event generation scheme
            /// </summary>
            ActionSwitch = 32,
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
        /// Number Of Positions Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfPositions { get; init; }

        /// <summary>
        /// Current Position Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> CurrentPosition { get; init; }

        /// <summary>
        /// Multi Press Max Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> MultiPressMax { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Switch";
        }
    }
}