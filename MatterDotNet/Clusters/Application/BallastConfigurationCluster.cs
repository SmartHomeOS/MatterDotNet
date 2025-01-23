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
        public BallastConfiguration(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected BallastConfiguration(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Ballast Status
        /// </summary>
        [Flags]
        public enum BallastStatus : byte {
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
        public enum LampAlarmMode : byte {
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
        /// Get the Physical Min Level attribute
        /// </summary>
        public async Task<byte> GetPhysicalMinLevel(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 0) ?? 0x01;
        }

        /// <summary>
        /// Get the Physical Max Level attribute
        /// </summary>
        public async Task<byte> GetPhysicalMaxLevel(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 1) ?? 0xFE;
        }

        /// <summary>
        /// Get the Ballast Status attribute
        /// </summary>
        public async Task<BallastStatus> GetBallastStatus(SecureSession session) {
            return (BallastStatus)await GetEnumAttribute(session, 2);
        }

        /// <summary>
        /// Get the Min Level attribute
        /// </summary>
        public async Task<byte> GetMinLevel(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 16) ?? 0x01;
        }

        /// <summary>
        /// Set the Min Level attribute
        /// </summary>
        public async Task SetMinLevel (SecureSession session, byte? value = 0x01) {
            await SetAttribute(session, 16, value);
        }

        /// <summary>
        /// Get the Max Level attribute
        /// </summary>
        public async Task<byte> GetMaxLevel(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 17) ?? 0xFE;
        }

        /// <summary>
        /// Set the Max Level attribute
        /// </summary>
        public async Task SetMaxLevel (SecureSession session, byte? value = 0xFE) {
            await SetAttribute(session, 17, value);
        }

        /// <summary>
        /// Get the Intrinsic Ballast Factor attribute
        /// </summary>
        public async Task<byte?> GetIntrinsicBallastFactor(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 20, true);
        }

        /// <summary>
        /// Set the Intrinsic Ballast Factor attribute
        /// </summary>
        public async Task SetIntrinsicBallastFactor (SecureSession session, byte? value) {
            await SetAttribute(session, 20, value, true);
        }

        /// <summary>
        /// Get the Ballast Factor Adjustment attribute
        /// </summary>
        public async Task<byte?> GetBallastFactorAdjustment(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 21, true) ?? 0xFF;
        }

        /// <summary>
        /// Set the Ballast Factor Adjustment attribute
        /// </summary>
        public async Task SetBallastFactorAdjustment (SecureSession session, byte? value = 0xFF) {
            await SetAttribute(session, 21, value, true);
        }

        /// <summary>
        /// Get the Lamp Quantity attribute
        /// </summary>
        public async Task<byte> GetLampQuantity(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 32))!;
        }

        /// <summary>
        /// Get the Lamp Type attribute
        /// </summary>
        public async Task<string> GetLampType(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 48))!;
        }

        /// <summary>
        /// Set the Lamp Type attribute
        /// </summary>
        public async Task SetLampType (SecureSession session, string value) {
            await SetAttribute(session, 48, value);
        }

        /// <summary>
        /// Get the Lamp Manufacturer attribute
        /// </summary>
        public async Task<string> GetLampManufacturer(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 49))!;
        }

        /// <summary>
        /// Set the Lamp Manufacturer attribute
        /// </summary>
        public async Task SetLampManufacturer (SecureSession session, string value) {
            await SetAttribute(session, 49, value);
        }

        /// <summary>
        /// Get the Lamp Rated Hours attribute
        /// </summary>
        public async Task<uint?> GetLampRatedHours(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 50, true) ?? 0xFFFFFF;
        }

        /// <summary>
        /// Set the Lamp Rated Hours attribute
        /// </summary>
        public async Task SetLampRatedHours (SecureSession session, uint? value = 0xFFFFFF) {
            await SetAttribute(session, 50, value, true);
        }

        /// <summary>
        /// Get the Lamp Burn Hours attribute
        /// </summary>
        public async Task<uint?> GetLampBurnHours(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 51, true) ?? 0x000000;
        }

        /// <summary>
        /// Set the Lamp Burn Hours attribute
        /// </summary>
        public async Task SetLampBurnHours (SecureSession session, uint? value = 0x000000) {
            await SetAttribute(session, 51, value, true);
        }

        /// <summary>
        /// Get the Lamp Alarm Mode attribute
        /// </summary>
        public async Task<LampAlarmMode> GetLampAlarmMode(SecureSession session) {
            return (LampAlarmMode)await GetEnumAttribute(session, 52);
        }

        /// <summary>
        /// Set the Lamp Alarm Mode attribute
        /// </summary>
        public async Task SetLampAlarmMode (SecureSession session, LampAlarmMode value) {
            await SetAttribute(session, 52, value);
        }

        /// <summary>
        /// Get the Lamp Burn Hours Trip Point attribute
        /// </summary>
        public async Task<uint?> GetLampBurnHoursTripPoint(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 53, true) ?? 0xFFFFFF;
        }

        /// <summary>
        /// Set the Lamp Burn Hours Trip Point attribute
        /// </summary>
        public async Task SetLampBurnHoursTripPoint (SecureSession session, uint? value = 0xFFFFFF) {
            await SetAttribute(session, 53, value, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Ballast Configuration";
        }
    }
}