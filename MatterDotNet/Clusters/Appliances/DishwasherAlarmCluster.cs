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

using MatterDotNet.Attributes;
using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Appliances
{
    /// <summary>
    /// Attributes and commands for configuring the Dishwasher alarm.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class DishwasherAlarm : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x005D;

        /// <summary>
        /// Attributes and commands for configuring the Dishwasher alarm.
        /// </summary>
        [SetsRequiredMembers]
        public DishwasherAlarm(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected DishwasherAlarm(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            Mask = new ReadAttribute<Alarm>(cluster, endPoint, 0) {
                Deserialize = x => (Alarm)DeserializeEnum(x)!
            };
            Latch = new ReadAttribute<Alarm>(cluster, endPoint, 1) {
                Deserialize = x => (Alarm)DeserializeEnum(x)!
            };
            State = new ReadAttribute<Alarm>(cluster, endPoint, 2) {
                Deserialize = x => (Alarm)DeserializeEnum(x)!
            };
            Supported = new ReadAttribute<Alarm>(cluster, endPoint, 3) {
                Deserialize = x => (Alarm)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports the ability to reset alarms
            /// </summary>
            Reset = 1,
        }

        /// <summary>
        /// Alarm
        /// </summary>
        [Flags]
        public enum Alarm : uint {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Water inflow is abnormal
            /// </summary>
            InflowError = 0x0001,
            /// <summary>
            /// Water draining is abnormal
            /// </summary>
            DrainError = 0x0002,
            /// <summary>
            /// Door or door lock is abnormal
            /// </summary>
            DoorError = 0x0004,
            /// <summary>
            /// Unable to reach normal temperature
            /// </summary>
            TempTooLow = 0x0008,
            /// <summary>
            /// Temperature is too high
            /// </summary>
            TempTooHigh = 0x0010,
            /// <summary>
            /// Water level is abnormal
            /// </summary>
            WaterLevelError = 0x0020,
        }
        #endregion Enums

        #region Payloads
        private record ResetPayload : TLVPayload {
            public required Alarm Alarms { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)Alarms);
                writer.EndContainer();
            }
        }

        private record ModifyEnabledAlarmsPayload : TLVPayload {
            public required Alarm Mask { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)Mask);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Reset
        /// </summary>
        public async Task<bool> Reset(SecureSession session, Alarm alarms, CancellationToken token = default) {
            ResetPayload requestFields = new ResetPayload() {
                Alarms = alarms,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Modify Enabled Alarms
        /// </summary>
        public async Task<bool> ModifyEnabledAlarms(SecureSession session, Alarm mask, CancellationToken token = default) {
            ModifyEnabledAlarmsPayload requestFields = new ModifyEnabledAlarmsPayload() {
                Mask = mask,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields, token);
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
        /// Mask Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Alarm> Mask { get; init; }

        /// <summary>
        /// Latch Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Alarm> Latch { get; init; }

        /// <summary>
        /// State Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Alarm> State { get; init; }

        /// <summary>
        /// Supported Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Alarm> Supported { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Dishwasher Alarm";
        }
    }
}