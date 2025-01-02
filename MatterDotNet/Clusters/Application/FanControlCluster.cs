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

using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Fan Control Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 4)]
    public class FanControlCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0202;

        /// <summary>
        /// Fan Control Cluster
        /// </summary>
        public FanControlCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected FanControlCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// 0-SpeedMax Fan Speeds
            /// </summary>
            MultiSpeed = 1,
            /// <summary>
            /// Automatic mode supported for fan speed
            /// </summary>
            Auto = 2,
            /// <summary>
            /// Rocking movement supported
            /// </summary>
            Rocking = 4,
            /// <summary>
            /// Wind emulation supported
            /// </summary>
            Wind = 8,
            /// <summary>
            /// Step command supported
            /// </summary>
            Step = 16,
            /// <summary>
            /// Airflow Direction attribute is supported
            /// </summary>
            AirflowDirection = 32,
        }

        /// <summary>
        /// Airflow Direction
        /// </summary>
        public enum AirflowDirectionEnum {
            /// <summary>
            /// Airflow is in the forward direction
            /// </summary>
            Forward = 0,
            /// <summary>
            /// Airflow is in the reverse direction
            /// </summary>
            Reverse = 1,
        }

        /// <summary>
        /// Fan Mode
        /// </summary>
        public enum FanModeEnum {
            /// <summary>
            /// Fan is off
            /// </summary>
            Off = 0,
            /// <summary>
            /// Fan using low speed
            /// </summary>
            Low = 1,
            /// <summary>
            /// Fan using medium speed
            /// </summary>
            Medium = 2,
            /// <summary>
            /// Fan using high speed
            /// </summary>
            High = 3,
            On = 4,
            /// <summary>
            /// Fan is using auto mode
            /// </summary>
            Auto = 5,
            /// <summary>
            /// Fan is using smart mode
            /// </summary>
            Smart = 6,
        }

        /// <summary>
        /// Fan Mode Sequence
        /// </summary>
        public enum FanModeSequenceEnum {
            /// <summary>
            /// Fan is capable of off, low, medium and high modes
            /// </summary>
            Off_Low_Med_High = 0,
            /// <summary>
            /// Fan is capable of off, low and high modes
            /// </summary>
            Off_Low_High = 1,
            /// <summary>
            /// Fan is capable of off, low, medium, high and auto modes
            /// </summary>
            Off_Low_Med_High_Auto = 2,
            /// <summary>
            /// Fan is capable of off, low, high and auto modes
            /// </summary>
            Off_Low_High_Auto = 3,
            /// <summary>
            /// Fan is capable of off, high and auto modes
            /// </summary>
            Off_High_Auto = 4,
            /// <summary>
            /// Fan is capable of off and high modes
            /// </summary>
            Off_High = 5,
        }

        /// <summary>
        /// Step Direction
        /// </summary>
        public enum StepDirectionEnum {
            /// <summary>
            /// Step moves in increasing direction
            /// </summary>
            Increase = 0,
            /// <summary>
            /// Step moves in decreasing direction
            /// </summary>
            Decrease = 1,
        }

        /// <summary>
        /// Rock Bitmap
        /// </summary>
        [Flags]
        public enum RockBitmap {
            /// <summary>
            /// Indicate rock left to right
            /// </summary>
            RockLeftRight = 1,
            /// <summary>
            /// Indicate rock up and down
            /// </summary>
            RockUpDown = 2,
            /// <summary>
            /// Indicate rock around
            /// </summary>
            RockRound = 4,
        }

        /// <summary>
        /// Wind Bitmap
        /// </summary>
        [Flags]
        public enum WindBitmap {
            /// <summary>
            /// Indicate sleep wind
            /// </summary>
            SleepWind = 1,
            /// <summary>
            /// Indicate natural wind
            /// </summary>
            NaturalWind = 2,
        }
        #endregion Enums

        #region Payloads
        private record StepPayload : TLVPayload {
            public required StepDirectionEnum Direction { get; set; } = StepDirectionEnum.Increase;
            public bool? Wrap { get; set; } = false;
            public bool? LowestOff { get; set; } = true;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Direction);
                if (Wrap != null)
                    writer.WriteBool(1, Wrap);
                if (LowestOff != null)
                    writer.WriteBool(2, LowestOff);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Step
        /// </summary>
        public async Task<bool> Step(SecureSession session, StepDirectionEnum Direction, bool? Wrap, bool? LowestOff) {
            StepPayload requestFields = new StepPayload() {
                Direction = Direction,
                Wrap = Wrap,
                LowestOff = LowestOff,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x00, requestFields);
            return ValidateResponse(resp);
        }
        #endregion Commands

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
        /// Get the Fan Mode attribute
        /// </summary>
        public async Task<FanModeEnum> GetFanMode(SecureSession session) {
            return (FanModeEnum)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Set the Fan Mode attribute
        /// </summary>
        public async Task SetFanMode (SecureSession session, FanModeEnum value) {
            await SetAttribute(session, 0, value);
        }

        /// <summary>
        /// Get the Fan Mode Sequence attribute
        /// </summary>
        public async Task<FanModeSequenceEnum> GetFanModeSequence(SecureSession session) {
            return (FanModeSequenceEnum)await GetEnumAttribute(session, 1);
        }

        /// <summary>
        /// Get the Percent Setting attribute
        /// </summary>
        public async Task<byte?> GetPercentSetting(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 2, true) ?? 0;
        }

        /// <summary>
        /// Set the Percent Setting attribute
        /// </summary>
        public async Task SetPercentSetting (SecureSession session, byte? value = 0) {
            await SetAttribute(session, 2, value, true);
        }

        /// <summary>
        /// Get the Percent Current attribute
        /// </summary>
        public async Task<byte> GetPercentCurrent(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 3))!;
        }

        /// <summary>
        /// Get the Speed Max attribute
        /// </summary>
        public async Task<byte> GetSpeedMax(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 4))!;
        }

        /// <summary>
        /// Get the Speed Setting attribute
        /// </summary>
        public async Task<byte?> GetSpeedSetting(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 5, true) ?? 0;
        }

        /// <summary>
        /// Set the Speed Setting attribute
        /// </summary>
        public async Task SetSpeedSetting (SecureSession session, byte? value = 0) {
            await SetAttribute(session, 5, value, true);
        }

        /// <summary>
        /// Get the Speed Current attribute
        /// </summary>
        public async Task<byte> GetSpeedCurrent(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 6))!;
        }

        /// <summary>
        /// Get the Rock Support attribute
        /// </summary>
        public async Task<RockBitmap> GetRockSupport(SecureSession session) {
            return (RockBitmap)await GetEnumAttribute(session, 7);
        }

        /// <summary>
        /// Get the Rock Setting attribute
        /// </summary>
        public async Task<RockBitmap> GetRockSetting(SecureSession session) {
            return (RockBitmap)await GetEnumAttribute(session, 8);
        }

        /// <summary>
        /// Set the Rock Setting attribute
        /// </summary>
        public async Task SetRockSetting (SecureSession session, RockBitmap value) {
            await SetAttribute(session, 8, value);
        }

        /// <summary>
        /// Get the Wind Support attribute
        /// </summary>
        public async Task<WindBitmap> GetWindSupport(SecureSession session) {
            return (WindBitmap)await GetEnumAttribute(session, 9);
        }

        /// <summary>
        /// Get the Wind Setting attribute
        /// </summary>
        public async Task<WindBitmap> GetWindSetting(SecureSession session) {
            return (WindBitmap)await GetEnumAttribute(session, 10);
        }

        /// <summary>
        /// Set the Wind Setting attribute
        /// </summary>
        public async Task SetWindSetting (SecureSession session, WindBitmap value) {
            await SetAttribute(session, 10, value);
        }

        /// <summary>
        /// Get the Airflow Direction attribute
        /// </summary>
        public async Task<AirflowDirectionEnum> GetAirflowDirection(SecureSession session) {
            return (AirflowDirectionEnum)await GetEnumAttribute(session, 11);
        }

        /// <summary>
        /// Set the Airflow Direction attribute
        /// </summary>
        public async Task SetAirflowDirection (SecureSession session, AirflowDirectionEnum value) {
            await SetAttribute(session, 11, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Fan Control Cluster";
        }
    }
}