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

namespace MatterDotNet.Clusters.Lighting
{
    /// <summary>
    /// Attributes and commands for configuring a lighting ballast.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 4)]
    public class BallastConfiguration : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0301;

        /// <summary>
        /// Attributes and commands for configuring a lighting ballast.
        /// </summary>
        [SetsRequiredMembers]
        public BallastConfiguration(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected BallastConfiguration(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            PhysicalMinLevel = new ReadAttribute<byte>(cluster, endPoint, 0) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0x01

            };
            PhysicalMaxLevel = new ReadAttribute<byte>(cluster, endPoint, 1) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0xFE

            };
            BallastStatus = new ReadAttribute<BallastStatusBitmap>(cluster, endPoint, 2) {
                Deserialize = x => (BallastStatusBitmap)DeserializeEnum(x)!
            };
            MinLevel = new ReadWriteAttribute<byte>(cluster, endPoint, 16) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0x01

            };
            MaxLevel = new ReadWriteAttribute<byte>(cluster, endPoint, 17) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0xFE

            };
            IntrinsicBallastFactor = new ReadWriteAttribute<byte?>(cluster, endPoint, 20, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            BallastFactorAdjustment = new ReadWriteAttribute<byte?>(cluster, endPoint, 21, true) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0xFF

            };
            LampQuantity = new ReadAttribute<byte>(cluster, endPoint, 32) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            LampType = new ReadWriteAttribute<string>(cluster, endPoint, 48) {
                Deserialize = x => (string)(dynamic?)x!
            };
            LampManufacturer = new ReadWriteAttribute<string>(cluster, endPoint, 49) {
                Deserialize = x => (string)(dynamic?)x!
            };
            LampRatedHours = new ReadWriteAttribute<uint?>(cluster, endPoint, 50, true) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0xFFFFFF

            };
            LampBurnHours = new ReadWriteAttribute<uint?>(cluster, endPoint, 51, true) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x000000

            };
            LampAlarmMode = new ReadWriteAttribute<LampAlarmModeBitmap>(cluster, endPoint, 52) {
                Deserialize = x => (LampAlarmModeBitmap)DeserializeEnum(x)!
            };
            LampBurnHoursTripPoint = new ReadWriteAttribute<uint?>(cluster, endPoint, 53, true) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0xFFFFFF

            };
        }

        #region Enums
        /// <summary>
        /// Ballast Status
        /// </summary>
        [Flags]
        public enum BallastStatusBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Operational state of the ballast.
            /// </summary>
            BallastNonOperational = 0x01,
            /// <summary>
            /// Operational state of the lamps.
            /// </summary>
            LampFailure = 0x02,
        }

        /// <summary>
        /// Lamp Alarm Mode
        /// </summary>
        [Flags]
        public enum LampAlarmModeBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// State of LampBurnHours alarm generation
            /// </summary>
            LampBurnHours = 0x01,
        }
        #endregion Enums

        #region Attributes
        /// <summary>
        /// Physical Min Level Attribute
        /// </summary>
        public required ReadAttribute<byte> PhysicalMinLevel { get; init; }

        /// <summary>
        /// Physical Max Level Attribute
        /// </summary>
        public required ReadAttribute<byte> PhysicalMaxLevel { get; init; }

        /// <summary>
        /// Ballast Status Attribute
        /// </summary>
        public required ReadAttribute<BallastStatusBitmap> BallastStatus { get; init; }

        /// <summary>
        /// Min Level Attribute
        /// </summary>
        public required ReadWriteAttribute<byte> MinLevel { get; init; }

        /// <summary>
        /// Max Level Attribute
        /// </summary>
        public required ReadWriteAttribute<byte> MaxLevel { get; init; }

        /// <summary>
        /// Intrinsic Ballast Factor Attribute
        /// </summary>
        public required ReadWriteAttribute<byte?> IntrinsicBallastFactor { get; init; }

        /// <summary>
        /// Ballast Factor Adjustment Attribute
        /// </summary>
        public required ReadWriteAttribute<byte?> BallastFactorAdjustment { get; init; }

        /// <summary>
        /// Lamp Quantity Attribute
        /// </summary>
        public required ReadAttribute<byte> LampQuantity { get; init; }

        /// <summary>
        /// Lamp Type Attribute
        /// </summary>
        public required ReadWriteAttribute<string> LampType { get; init; }

        /// <summary>
        /// Lamp Manufacturer Attribute
        /// </summary>
        public required ReadWriteAttribute<string> LampManufacturer { get; init; }

        /// <summary>
        /// Lamp Rated Hours Attribute
        /// </summary>
        public required ReadWriteAttribute<uint?> LampRatedHours { get; init; }

        /// <summary>
        /// Lamp Burn Hours Attribute
        /// </summary>
        public required ReadWriteAttribute<uint?> LampBurnHours { get; init; }

        /// <summary>
        /// Lamp Alarm Mode Attribute
        /// </summary>
        public required ReadWriteAttribute<LampAlarmModeBitmap> LampAlarmMode { get; init; }

        /// <summary>
        /// Lamp Burn Hours Trip Point Attribute
        /// </summary>
        public required ReadWriteAttribute<uint?> LampBurnHoursTripPoint { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Ballast Configuration";
        }
    }
}