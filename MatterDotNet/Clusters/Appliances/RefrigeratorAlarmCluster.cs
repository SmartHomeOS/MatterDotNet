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

namespace MatterDotNet.Clusters.Appliances
{
    /// <summary>
    /// Attributes and commands for configuring the Refrigerator alarm.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class RefrigeratorAlarm : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0057;

        /// <summary>
        /// Attributes and commands for configuring the Refrigerator alarm.
        /// </summary>
        [SetsRequiredMembers]
        public RefrigeratorAlarm(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected RefrigeratorAlarm(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            Mask = new ReadAttribute<Alarm>(cluster, endPoint, 0) {
                Deserialize = x => (Alarm)DeserializeEnum(x)!
            };
            State = new ReadAttribute<Alarm>(cluster, endPoint, 2) {
                Deserialize = x => (Alarm)DeserializeEnum(x)!
            };
            Supported = new ReadAttribute<Alarm>(cluster, endPoint, 3) {
                Deserialize = x => (Alarm)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Alarm
        /// </summary>
        [Flags]
        public enum Alarm : uint {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            DoorOpen = 0x0001,
        }
        #endregion Enums

        #region Attributes
        /// <summary>
        /// Mask Attribute
        /// </summary>
        public required ReadAttribute<Alarm> Mask { get; init; }

        /// <summary>
        /// State Attribute
        /// </summary>
        public required ReadAttribute<Alarm> State { get; init; }

        /// <summary>
        /// Supported Attribute
        /// </summary>
        public required ReadAttribute<Alarm> Supported { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Refrigerator Alarm";
        }
    }
}