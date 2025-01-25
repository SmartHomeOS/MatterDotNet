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

namespace MatterDotNet.Clusters.MeasurementAndSensing
{
    /// <summary>
    /// The server cluster provides an interface to occupancy sensing functionality based on one or more sensing modalities, including configuration and provision of notifications of occupancy status.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 5)]
    public class OccupancySensing : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0406;

        /// <summary>
        /// The server cluster provides an interface to occupancy sensing functionality based on one or more sensing modalities, including configuration and provision of notifications of occupancy status.
        /// </summary>
        [SetsRequiredMembers]
        public OccupancySensing(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected OccupancySensing(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            Occupancy = new ReadAttribute<OccupancyBitmap>(cluster, endPoint, 0) {
                Deserialize = x => (OccupancyBitmap)DeserializeEnum(x)!
            };
            OccupancySensorType = new ReadAttribute<OccupancySensorTypeEnum>(cluster, endPoint, 1) {
                Deserialize = x => (OccupancySensorTypeEnum)DeserializeEnum(x)!
            };
            OccupancySensorTypeBitmapAttribute = new ReadAttribute<OccupancySensorTypeBitmap>(cluster, endPoint, 2) {
                Deserialize = x => (OccupancySensorTypeBitmap)DeserializeEnum(x)!
            };
            HoldTime = new ReadWriteAttribute<ushort>(cluster, endPoint, 3) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            HoldTimeLimits = new ReadAttribute<HoldTimeLimitsStruct>(cluster, endPoint, 4) {
                Deserialize = x => new HoldTimeLimitsStruct((object[])x!)
            };
            PIROccupiedToUnoccupiedDelay = new ReadWriteAttribute<ushort>(cluster, endPoint, 16) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            PIRUnoccupiedToOccupiedDelay = new ReadWriteAttribute<ushort>(cluster, endPoint, 17) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            PIRUnoccupiedToOccupiedThreshold = new ReadWriteAttribute<byte>(cluster, endPoint, 18) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0x01

            };
            UltrasonicOccupiedToUnoccupiedDelay = new ReadWriteAttribute<ushort>(cluster, endPoint, 32) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            UltrasonicUnoccupiedToOccupiedDelay = new ReadWriteAttribute<ushort>(cluster, endPoint, 33) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            UltrasonicUnoccupiedToOccupiedThreshold = new ReadWriteAttribute<byte>(cluster, endPoint, 34) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0x01

            };
            PhysicalContactOccupiedToUnoccupiedDelay = new ReadWriteAttribute<ushort>(cluster, endPoint, 48) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            PhysicalContactUnoccupiedToOccupiedDelay = new ReadWriteAttribute<ushort>(cluster, endPoint, 49) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            PhysicalContactUnoccupiedToOccupiedThreshold = new ReadWriteAttribute<byte>(cluster, endPoint, 50) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0x01

            };
        }

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
        public enum OccupancySensorTypeEnum : byte {
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
        /// Occupancy
        /// </summary>
        [Flags]
        public enum OccupancyBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Indicates the sensed occupancy state
            /// </summary>
            Occupied = 0x01,
        }

        /// <summary>
        /// Occupancy Sensor Type
        /// </summary>
        [Flags]
        public enum OccupancySensorTypeBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Indicates a passive infrared sensor.
            /// </summary>
            PIR = 0x01,
            /// <summary>
            /// Indicates a ultrasonic sensor.
            /// </summary>
            Ultrasonic = 0x02,
            /// <summary>
            /// Indicates a physical contact sensor.
            /// </summary>
            PhysicalContact = 0x04,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Hold Time Limits
        /// </summary>
        public record HoldTimeLimitsStruct : TLVPayload {
            /// <summary>
            /// Hold Time Limits
            /// </summary>
            public HoldTimeLimitsStruct() { }

            /// <summary>
            /// Hold Time Limits
            /// </summary>
            [SetsRequiredMembers]
            public HoldTimeLimitsStruct(object[] fields) {
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
                writer.WriteUShort(0, HoldTimeMin);
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
        /// Occupancy Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<OccupancyBitmap> Occupancy { get; init; }

        /// <summary>
        /// Occupancy Sensor Type Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<OccupancySensorTypeEnum> OccupancySensorType { get; init; }

        /// <summary>
        /// Occupancy Sensor Type Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<OccupancySensorTypeBitmap> OccupancySensorTypeBitmapAttribute { get; init; }

        /// <summary>
        /// Hold Time Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> HoldTime { get; init; }

        /// <summary>
        /// Hold Time Limits Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<HoldTimeLimitsStruct> HoldTimeLimits { get; init; }

        /// <summary>
        /// PIR Occupied To Unoccupied Delay Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> PIROccupiedToUnoccupiedDelay { get; init; }

        /// <summary>
        /// PIR Unoccupied To Occupied Delay Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> PIRUnoccupiedToOccupiedDelay { get; init; }

        /// <summary>
        /// PIR Unoccupied To Occupied Threshold Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte> PIRUnoccupiedToOccupiedThreshold { get; init; }

        /// <summary>
        /// Ultrasonic Occupied To Unoccupied Delay Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> UltrasonicOccupiedToUnoccupiedDelay { get; init; }

        /// <summary>
        /// Ultrasonic Unoccupied To Occupied Delay Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> UltrasonicUnoccupiedToOccupiedDelay { get; init; }

        /// <summary>
        /// Ultrasonic Unoccupied To Occupied Threshold Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte> UltrasonicUnoccupiedToOccupiedThreshold { get; init; }

        /// <summary>
        /// Physical Contact Occupied To Unoccupied Delay Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> PhysicalContactOccupiedToUnoccupiedDelay { get; init; }

        /// <summary>
        /// Physical Contact Unoccupied To Occupied Delay Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> PhysicalContactUnoccupiedToOccupiedDelay { get; init; }

        /// <summary>
        /// Physical Contact Unoccupied To Occupied Threshold Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte> PhysicalContactUnoccupiedToOccupiedThreshold { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Occupancy Sensing";
        }
    }
}