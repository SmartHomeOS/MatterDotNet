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
        public RefrigeratorAlarm(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected RefrigeratorAlarm(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
            /// <summary>
            /// The cabinet's door has been open for a vendor defined amount of time.
            /// </summary>
            DoorOpen = 0x01,
        }
        #endregion Enums

        #region Attributes
        /// <summary>
        /// Get the Mask attribute
        /// </summary>
        public async Task<Alarm> GetMask(SecureSession session) {
            return (Alarm)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Get the State attribute
        /// </summary>
        public async Task<Alarm> GetState(SecureSession session) {
            return (Alarm)await GetEnumAttribute(session, 2);
        }

        /// <summary>
        /// Get the Supported attribute
        /// </summary>
        public async Task<Alarm> GetSupported(SecureSession session) {
            return (Alarm)await GetEnumAttribute(session, 3);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Refrigerator Alarm";
        }
    }
}