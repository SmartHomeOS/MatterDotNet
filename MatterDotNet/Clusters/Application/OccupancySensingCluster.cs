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
    /// Occupancy Sensing Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 4)]
    public class OccupancySensingCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0406;

        /// <summary>
        /// Occupancy Sensing Cluster
        /// </summary>
        public OccupancySensingCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected OccupancySensingCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Occupancy Sensor Type
        /// </summary>
        public enum OccupancySensorTypeEnum {
            /// <summary>
            /// Indicates a passive infrared sensor.
            /// </summary>
            PIR = 0,
            /// <summary>
            /// Indicates a ultrasonic sensor.
            /// </summary>
            Ultrasonic = 1,
            /// <summary>
            /// Indicates a passive infrared and ultrasonic sensor.
            /// </summary>
            PIRAndUltrasonic = 2,
            /// <summary>
            /// Indicates a physical contact sensor.
            /// </summary>
            PhysicalContact = 3,
        }

        /// <summary>
        /// Occupancy Bitmap
        /// </summary>
        [Flags]
        public enum OccupancyBitmap {
            /// <summary>
            /// Indicates the sensed occupancy state
            /// </summary>
            Occupied = 1,
        }

        /// <summary>
        /// Occupancy Sensor Type Bitmap
        /// </summary>
        [Flags]
        public enum OccupancySensorTypeBitmap {
            /// <summary>
            /// Indicates a passive infrared sensor.
            /// </summary>
            PIR = 1,
            /// <summary>
            /// Indicates a ultrasonic sensor.
            /// </summary>
            Ultrasonic = 2,
            /// <summary>
            /// Indicates a physical contact sensor.
            /// </summary>
            PhysicalContact = 4,
        }
        #endregion Enums

        #region Attributes
        /// <summary>
        /// Get the Occupancy attribute
        /// </summary>
        public async Task<OccupancyBitmap> GetOccupancy(SecureSession session) {
            return (OccupancyBitmap)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Get the Occupancy Sensor Type attribute
        /// </summary>
        public async Task<OccupancySensorTypeEnum> GetOccupancySensorType(SecureSession session) {
            return (OccupancySensorTypeEnum)await GetEnumAttribute(session, 1);
        }

        /// <summary>
        /// Get the Occupancy Sensor Type Bitmap attribute
        /// </summary>
        public async Task<OccupancySensorTypeBitmap> GetOccupancySensorTypeBitmap(SecureSession session) {
            return (OccupancySensorTypeBitmap)await GetEnumAttribute(session, 2);
        }

        /// <summary>
        /// Get the PIR Occupied To Unoccupied Delay attribute
        /// </summary>
        public async Task<ushort> GetPIROccupiedToUnoccupiedDelay(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16) ?? 0;
        }

        /// <summary>
        /// Set the PIR Occupied To Unoccupied Delay attribute
        /// </summary>
        public async Task SetPIROccupiedToUnoccupiedDelay (SecureSession session, ushort? value = 0) {
            await SetAttribute(session, 16, value);
        }

        /// <summary>
        /// Get the PIR Unoccupied To Occupied Delay attribute
        /// </summary>
        public async Task<ushort> GetPIRUnoccupiedToOccupiedDelay(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 17) ?? 0;
        }

        /// <summary>
        /// Set the PIR Unoccupied To Occupied Delay attribute
        /// </summary>
        public async Task SetPIRUnoccupiedToOccupiedDelay (SecureSession session, ushort? value = 0) {
            await SetAttribute(session, 17, value);
        }

        /// <summary>
        /// Get the PIR Unoccupied To Occupied Threshold attribute
        /// </summary>
        public async Task<byte> GetPIRUnoccupiedToOccupiedThreshold(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 18) ?? 1;
        }

        /// <summary>
        /// Set the PIR Unoccupied To Occupied Threshold attribute
        /// </summary>
        public async Task SetPIRUnoccupiedToOccupiedThreshold (SecureSession session, byte? value = 1) {
            await SetAttribute(session, 18, value);
        }

        /// <summary>
        /// Get the Ultrasonic Occupied To Unoccupied Delay attribute
        /// </summary>
        public async Task<ushort> GetUltrasonicOccupiedToUnoccupiedDelay(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 32) ?? 0;
        }

        /// <summary>
        /// Set the Ultrasonic Occupied To Unoccupied Delay attribute
        /// </summary>
        public async Task SetUltrasonicOccupiedToUnoccupiedDelay (SecureSession session, ushort? value = 0) {
            await SetAttribute(session, 32, value);
        }

        /// <summary>
        /// Get the Ultrasonic Unoccupied To Occupied Delay attribute
        /// </summary>
        public async Task<ushort> GetUltrasonicUnoccupiedToOccupiedDelay(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 33) ?? 0;
        }

        /// <summary>
        /// Set the Ultrasonic Unoccupied To Occupied Delay attribute
        /// </summary>
        public async Task SetUltrasonicUnoccupiedToOccupiedDelay (SecureSession session, ushort? value = 0) {
            await SetAttribute(session, 33, value);
        }

        /// <summary>
        /// Get the Ultrasonic Unoccupied To Occupied Threshold attribute
        /// </summary>
        public async Task<byte> GetUltrasonicUnoccupiedToOccupiedThreshold(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 34) ?? 1;
        }

        /// <summary>
        /// Set the Ultrasonic Unoccupied To Occupied Threshold attribute
        /// </summary>
        public async Task SetUltrasonicUnoccupiedToOccupiedThreshold (SecureSession session, byte? value = 1) {
            await SetAttribute(session, 34, value);
        }

        /// <summary>
        /// Get the Physical Contact Occupied To Unoccupied Delay attribute
        /// </summary>
        public async Task<ushort> GetPhysicalContactOccupiedToUnoccupiedDelay(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 48) ?? 0;
        }

        /// <summary>
        /// Set the Physical Contact Occupied To Unoccupied Delay attribute
        /// </summary>
        public async Task SetPhysicalContactOccupiedToUnoccupiedDelay (SecureSession session, ushort? value = 0) {
            await SetAttribute(session, 48, value);
        }

        /// <summary>
        /// Get the Physical Contact Unoccupied To Occupied Delay attribute
        /// </summary>
        public async Task<ushort> GetPhysicalContactUnoccupiedToOccupiedDelay(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 49) ?? 0;
        }

        /// <summary>
        /// Set the Physical Contact Unoccupied To Occupied Delay attribute
        /// </summary>
        public async Task SetPhysicalContactUnoccupiedToOccupiedDelay (SecureSession session, ushort? value = 0) {
            await SetAttribute(session, 49, value);
        }

        /// <summary>
        /// Get the Physical Contact Unoccupied To Occupied Threshold attribute
        /// </summary>
        public async Task<byte> GetPhysicalContactUnoccupiedToOccupiedThreshold(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 50) ?? 1;
        }

        /// <summary>
        /// Set the Physical Contact Unoccupied To Occupied Threshold attribute
        /// </summary>
        public async Task SetPhysicalContactUnoccupiedToOccupiedThreshold (SecureSession session, byte? value = 1) {
            await SetAttribute(session, 50, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Occupancy Sensing Cluster";
        }
    }
}