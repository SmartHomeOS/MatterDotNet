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
using System.Diagnostics.CodeAnalysis;

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
        [SetsRequiredMembers]
        public FanControl(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected FanControl(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            FanMode = new ReadWriteAttribute<FanModeEnum>(cluster, endPoint, 0) {
                Deserialize = x => (FanModeEnum)DeserializeEnum(x)!
            };
            FanModeSequence = new ReadAttribute<FanModeSequenceEnum>(cluster, endPoint, 1) {
                Deserialize = x => (FanModeSequenceEnum)DeserializeEnum(x)!
            };
            PercentSetting = new ReadWriteAttribute<byte?>(cluster, endPoint, 2, true) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            PercentCurrent = new ReadAttribute<byte>(cluster, endPoint, 3) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            SpeedMax = new ReadAttribute<byte>(cluster, endPoint, 4) {
                Deserialize = x => (byte?)(dynamic?)x ?? 1

            };
            SpeedSetting = new ReadWriteAttribute<byte?>(cluster, endPoint, 5, true) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            SpeedCurrent = new ReadAttribute<byte>(cluster, endPoint, 6) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            RockSupport = new ReadAttribute<Rock>(cluster, endPoint, 7) {
                Deserialize = x => (Rock)DeserializeEnum(x)!
            };
            RockSetting = new ReadWriteAttribute<Rock>(cluster, endPoint, 8) {
                Deserialize = x => (Rock)DeserializeEnum(x)!
            };
            WindSupport = new ReadAttribute<Wind>(cluster, endPoint, 9) {
                Deserialize = x => (Wind)DeserializeEnum(x)!
            };
            WindSetting = new ReadWriteAttribute<Wind>(cluster, endPoint, 10) {
                Deserialize = x => (Wind)DeserializeEnum(x)!
            };
            AirflowDirection = new ReadWriteAttribute<AirflowDirectionEnum>(cluster, endPoint, 11) {
                Deserialize = x => (AirflowDirectionEnum)DeserializeEnum(x)!
            };
        }

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
        public enum FanModeEnum : byte {
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
            /// <summary>
            /// 
            /// </summary>
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
        public enum FanModeSequenceEnum : byte {
            /// <summary>
            /// Fan is capable of off, low, medium and high modes
            /// </summary>
            OffLowMedHigh = 0,
            /// <summary>
            /// Fan is capable of off, low and high modes
            /// </summary>
            OffLowHigh = 1,
            /// <summary>
            /// Fan is capable of off, low, medium, high and auto modes
            /// </summary>
            OffLowMedHighAuto = 2,
            /// <summary>
            /// Fan is capable of off, low, high and auto modes
            /// </summary>
            OffLowHighAuto = 3,
            /// <summary>
            /// Fan is capable of off, high and auto modes
            /// </summary>
            OffHighAuto = 4,
            /// <summary>
            /// Fan is capable of off and high modes
            /// </summary>
            OffHigh = 5,
        }

        /// <summary>
        /// Step Direction
        /// </summary>
        public enum StepDirection : byte {
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
        /// Airflow Direction
        /// </summary>
        public enum AirflowDirectionEnum : byte {
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
        /// Rock
        /// </summary>
        [Flags]
        public enum Rock : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
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
            None = 0x0,
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
        public async Task<bool> Step(SecureSession session, StepDirection direction, bool? wrap, bool? lowestOff, CancellationToken token = default) {
            StepPayload requestFields = new StepPayload() {
                Direction = direction,
                Wrap = wrap,
                LowestOff = lowestOff,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
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
        /// Fan Mode Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<FanModeEnum> FanMode { get; init; }

        /// <summary>
        /// Fan Mode Sequence Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<FanModeSequenceEnum> FanModeSequence { get; init; }

        /// <summary>
        /// Percent Setting [%] Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte?> PercentSetting { get; init; }

        /// <summary>
        /// Percent Current [%] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> PercentCurrent { get; init; }

        /// <summary>
        /// Speed Max Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> SpeedMax { get; init; }

        /// <summary>
        /// Speed Setting Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte?> SpeedSetting { get; init; }

        /// <summary>
        /// Speed Current Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> SpeedCurrent { get; init; }

        /// <summary>
        /// Rock Support Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Rock> RockSupport { get; init; }

        /// <summary>
        /// Rock Setting Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<Rock> RockSetting { get; init; }

        /// <summary>
        /// Wind Support Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Wind> WindSupport { get; init; }

        /// <summary>
        /// Wind Setting Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<Wind> WindSetting { get; init; }

        /// <summary>
        /// Airflow Direction Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<AirflowDirectionEnum> AirflowDirection { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Fan Control";
        }
    }
}