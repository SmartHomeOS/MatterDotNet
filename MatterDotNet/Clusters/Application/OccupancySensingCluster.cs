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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Occupancy Sensing Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 5)]
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
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports sensing using a modality not listed in the other bits
            /// </summary>
            Other = 1,
            /// <summary>
            /// Supports sensing using PIR (Passive InfraRed)
            /// </summary>
            PassiveInfrared = 2,
            /// <summary>
            /// Supports sensing using UltraSound
            /// </summary>
            Ultrasonic = 4,
            /// <summary>
            /// Supports sensing using a physical contact
            /// </summary>
            PhysicalContact = 8,
            /// <summary>
            /// Supports sensing using Active InfraRed measurement (e.g. time-of-flight or transflective/reflective IR sensing)
            /// </summary>
            ActiveInfrared = 16,
            /// <summary>
            /// Supports sensing using radar waves (microwave)
            /// </summary>
            Radar = 32,
            /// <summary>
            /// Supports sensing based on RF signal analysis
            /// </summary>
            RFSensing = 64,
            /// <summary>
            /// Supports sensing based on analyzing images
            /// </summary>
            Vision = 128,
        }

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
            /// Nothing Set
            /// </summary>
            None = 0,
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
            /// Nothing Set
            /// </summary>
            None = 0,
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

        #region Records
        /// <summary>
        /// Hold Time Limits
        /// </summary>
        public record HoldTimeLimits : TLVPayload {
            /// <summary>
            /// Hold Time Limits
            /// </summary>
            public HoldTimeLimits() { }

            /// <summary>
            /// Hold Time Limits
            /// </summary>
            [SetsRequiredMembers]
            public HoldTimeLimits(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                HoldTimeMin = reader.GetUShort(0)!.Value;
                HoldTimeMax = reader.GetUShort(1)!.Value;
                HoldTimeDefault = reader.GetUShort(2)!.Value;
            }
            public required ushort HoldTimeMin { get; set; }
            public required ushort HoldTimeMax { get; set; }
            public required ushort HoldTimeDefault { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, HoldTimeMin, ushort.MaxValue, 1);
                writer.WriteUShort(1, HoldTimeMax);
                writer.WriteUShort(2, HoldTimeDefault);
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
        /// Get the Hold Time attribute
        /// </summary>
        public async Task<ushort> GetHoldTime(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 3))!;
        }

        /// <summary>
        /// Set the Hold Time attribute
        /// </summary>
        public async Task SetHoldTime (SecureSession session, ushort value) {
            await SetAttribute(session, 3, value);
        }

        /// <summary>
        /// Get the Hold Time Limits attribute
        /// </summary>
        public async Task<HoldTimeLimits> GetHoldTimeLimits(SecureSession session) {
            return new HoldTimeLimits((object[])(await GetAttribute(session, 4))!);
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