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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Dishwasher Alarm Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class DishwasherAlarmCluster : AlarmBaseCluster
    {
        internal const uint CLUSTER_ID = 0x005D;

        /// <summary>
        /// Dishwasher Alarm Cluster
        /// </summary>
        public DishwasherAlarmCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }

        #region Enums
        /// <summary>
        /// Alarm Bitmap
        /// </summary>
        [Flags]
        public enum AlarmBitmap {
            /// <summary>
            /// Water inflow is abnormal
            /// </summary>
            InflowError = 1,
            /// <summary>
            /// Water draining is abnormal
            /// </summary>
            DrainError = 2,
            /// <summary>
            /// Door or door lock is abnormal
            /// </summary>
            DoorError = 4,
            /// <summary>
            /// Unable to reach normal temperature
            /// </summary>
            TempTooLow = 8,
            /// <summary>
            /// Temperature is too high
            /// </summary>
            TempTooHigh = 16,
            /// <summary>
            /// Water level is abnormal
            /// </summary>
            WaterLevelError = 32,
        }
        #endregion Enums


        /// <inheritdoc />
        public override string ToString() {
            return "Dishwasher Alarm Cluster";
        }
    }
}