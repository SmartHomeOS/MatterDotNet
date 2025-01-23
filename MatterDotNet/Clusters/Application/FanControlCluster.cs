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

namespace MatterDotNet.Clusters.HVAC
{
    /// <summary>
    /// An interface for controlling a fan in a heating/cooling system.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 4)]
    public class FanControl : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0202;

        /// <summary>
        /// An interface for controlling a fan in a heating/cooling system.
        /// </summary>
        public FanControl(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected FanControl(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        /// Fan Mode
        /// </summary>
        public enum FanMode : byte {
            /// <summary>
            /// Fan is off
            /// </summary>
            Off = 0x00,
            /// <summary>
            /// Fan using low speed
            /// </summary>
            Low = 0x01,
            /// <summary>
            /// Fan using medium speed
            /// </summary>
            Medium = 0x02,
            /// <summary>
            /// Fan using high speed
            /// </summary>
            High = 0x03,
            /// <summary>
            /// 
            /// </summary>
            On = 0x04,
            /// <summary>
            /// Fan is using auto mode
            /// </summary>
            Auto = 0x05,
            /// <summary>
            /// Fan is using smart mode
            /// </summary>
            Smart = 0x06,
        }

        /// <summary>
        /// Fan Mode Sequence
        /// </summary>
        public enum FanModeSequence : byte {
            /// <summary>
            /// Fan is capable of off, low, medium and high modes
            /// </summary>
            OffLowMedHigh = 0x00,
            /// <summary>
            /// Fan is capable of off, low and high modes
            /// </summary>
            OffLowHigh = 0x01,
            /// <summary>
            /// Fan is capable of off, low, medium, high and auto modes
            /// </summary>
            OffLowMedHighAuto = 0x02,
            /// <summary>
            /// Fan is capable of off, low, high and auto modes
            /// </summary>
            OffLowHighAuto = 0x03,
            /// <summary>
            /// Fan is capable of off, high and auto modes
            /// </summary>
            OffHighAuto = 0x04,
            /// <summary>
            /// Fan is capable of off and high modes
            /// </summary>
            OffHigh = 0x05,
        }

        /// <summary>
        /// Step Direction
        /// </summary>
        public enum StepDirection : byte {
            /// <summary>
            /// Step moves in increasing direction
            /// </summary>
            Increase = 0x00,
            /// <summary>
            /// Step moves in decreasing direction
            /// </summary>
            Decrease = 0x01,
        }

        /// <summary>
        /// Airflow Direction
        /// </summary>
        public enum AirflowDirection : byte {
            /// <summary>
            /// Airflow is in the forward direction
            /// </summary>
            Forward = 0x00,
            /// <summary>
            /// Airflow is in the reverse direction
            /// </summary>
            Reverse = 0x01,
        }

        /// <summary>
        /// Rock
        /// </summary>
        [Flags]
        public enum Rock : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Indicate rock left to right
            /// </summary>
            RockLeftRight = 0x01,
            /// <summary>
            /// Indicate rock up and down
            /// </summary>
            RockUpDown = 0x02,
            /// <summary>
            /// Indicate rock around
            /// </summary>
            RockRound = 0x04,
        }

        /// <summary>
        /// Wind
        /// </summary>
        [Flags]
        public enum Wind : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Indicate sleep wind
            /// </summary>
            SleepWind = 0x01,
            /// <summary>
            /// Indicate natural wind
            /// </summary>
            NaturalWind = 0x02,
        }
        #endregion Enums

        #region Payloads
        private record StepPayload : TLVPayload {
            public required StepDirection Direction { get; set; }
            public bool? Wrap { get; set; }
            public bool? LowestOff { get; set; }
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
        public async Task<bool> Step(SecureSession session, StepDirection direction, bool? wrap, bool? lowestOff) {
            StepPayload requestFields = new StepPayload() {
                Direction = direction,
                Wrap = wrap,
                LowestOff = lowestOff,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
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
        public async Task<FanMode> GetFanMode(SecureSession session) {
            return (FanMode)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Set the Fan Mode attribute
        /// </summary>
        public async Task SetFanMode (SecureSession session, FanMode value) {
            await SetAttribute(session, 0, value);
        }

        /// <summary>
        /// Get the Fan Mode Sequence attribute
        /// </summary>
        public async Task<FanModeSequence> GetFanModeSequence(SecureSession session) {
            return (FanModeSequence)await GetEnumAttribute(session, 1);
        }

        /// <summary>
        /// Get the Percent Setting [%] attribute
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
        /// Get the Percent Current [%] attribute
        /// </summary>
        public async Task<byte> GetPercentCurrent(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 3) ?? 0;
        }

        /// <summary>
        /// Get the Speed Max attribute
        /// </summary>
        public async Task<byte> GetSpeedMax(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 4) ?? 1;
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
            return (byte?)(dynamic?)await GetAttribute(session, 6) ?? 0;
        }

        /// <summary>
        /// Get the Rock Support attribute
        /// </summary>
        public async Task<Rock> GetRockSupport(SecureSession session) {
            return (Rock)await GetEnumAttribute(session, 7);
        }

        /// <summary>
        /// Get the Rock Setting attribute
        /// </summary>
        public async Task<Rock> GetRockSetting(SecureSession session) {
            return (Rock)await GetEnumAttribute(session, 8);
        }

        /// <summary>
        /// Set the Rock Setting attribute
        /// </summary>
        public async Task SetRockSetting (SecureSession session, Rock value) {
            await SetAttribute(session, 8, value);
        }

        /// <summary>
        /// Get the Wind Support attribute
        /// </summary>
        public async Task<Wind> GetWindSupport(SecureSession session) {
            return (Wind)await GetEnumAttribute(session, 9);
        }

        /// <summary>
        /// Get the Wind Setting attribute
        /// </summary>
        public async Task<Wind> GetWindSetting(SecureSession session) {
            return (Wind)await GetEnumAttribute(session, 10);
        }

        /// <summary>
        /// Set the Wind Setting attribute
        /// </summary>
        public async Task SetWindSetting (SecureSession session, Wind value) {
            await SetAttribute(session, 10, value);
        }

        /// <summary>
        /// Get the Airflow Direction attribute
        /// </summary>
        public async Task<AirflowDirection> GetAirflowDirection(SecureSession session) {
            return (AirflowDirection)await GetEnumAttribute(session, 11);
        }

        /// <summary>
        /// Set the Airflow Direction attribute
        /// </summary>
        public async Task SetAirflowDirection (SecureSession session, AirflowDirection value) {
            await SetAttribute(session, 11, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Fan Control";
        }
    }
}