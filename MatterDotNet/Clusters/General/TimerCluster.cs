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

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// This cluster supports creating a simple timer functionality.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class Timer : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0047;

        /// <summary>
        /// This cluster supports creating a simple timer functionality.
        /// </summary>
        [SetsRequiredMembers]
        public Timer(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected Timer(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            SetTime = new ReadAttribute<TimeSpan>(cluster, endPoint, 0) {
                Deserialize = x => (TimeSpan)(dynamic?)x!
            };
            TimeRemaining = new ReadAttribute<TimeSpan>(cluster, endPoint, 1) {
                Deserialize = x => (TimeSpan)(dynamic?)x!
            };
            TimerState = new ReadAttribute<TimerStatus>(cluster, endPoint, 2) {
                Deserialize = x => (TimerStatus)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports the ability to reset timer
            /// </summary>
            Reset = 1,
        }

        /// <summary>
        /// Timer Status
        /// </summary>
        public enum TimerStatus : byte {
            Running = 0,
            Paused = 1,
            Expired = 2,
            Ready = 3,
        }
        #endregion Enums

        #region Payloads
        private record SetTimerPayload : TLVPayload {
            public required TimeSpan NewTime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)NewTime.TotalSeconds);
                writer.EndContainer();
            }
        }

        private record AddTimePayload : TLVPayload {
            public required TimeSpan AdditionalTime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)AdditionalTime.TotalSeconds);
                writer.EndContainer();
            }
        }

        private record ReduceTimePayload : TLVPayload {
            public required TimeSpan TimeReduction { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)TimeReduction.TotalSeconds);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Set Timer
        /// </summary>
        public async Task<bool> SetTimer(SecureSession session, TimeSpan newTime, CancellationToken token = default) {
            SetTimerPayload requestFields = new SetTimerPayload() {
                NewTime = newTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Reset Timer
        /// </summary>
        public async Task<bool> ResetTimer(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, null, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Add Time
        /// </summary>
        public async Task<bool> AddTime(SecureSession session, TimeSpan additionalTime, CancellationToken token = default) {
            AddTimePayload requestFields = new AddTimePayload() {
                AdditionalTime = additionalTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Reduce Time
        /// </summary>
        public async Task<bool> ReduceTime(SecureSession session, TimeSpan timeReduction, CancellationToken token = default) {
            ReduceTimePayload requestFields = new ReduceTimePayload() {
                TimeReduction = timeReduction,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields, token);
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
        /// Set Time Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<TimeSpan> SetTime { get; init; }

        /// <summary>
        /// Time Remaining Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<TimeSpan> TimeRemaining { get; init; }

        /// <summary>
        /// Timer State Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<TimerStatus> TimerState { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Timer";
        }
    }
}