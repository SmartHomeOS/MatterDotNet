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

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// Attributes and commands for switching devices between 'On' and 'Off' states.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 6)]
    public class On_Off : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0006;

        /// <summary>
        /// Attributes and commands for switching devices between 'On' and 'Off' states.
        /// </summary>
        [SetsRequiredMembers]
        public On_Off(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected On_Off(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            OnOff = new ReportAttribute<bool>(cluster, endPoint, 0) {
                Deserialize = x => (bool?)(dynamic?)x ?? false

            };
            GlobalSceneControl = new ReadAttribute<bool>(cluster, endPoint, 16384) {
                Deserialize = x => (bool?)(dynamic?)x ?? true

            };
            OnTime = new ReadWriteAttribute<ushort>(cluster, endPoint, 16385) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0

            };
            OffWaitTime = new ReadWriteAttribute<ushort>(cluster, endPoint, 16386) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0

            };
            StartUpOnOff = new ReadWriteAttribute<StartUpOnOffEnum?>(cluster, endPoint, 16387, true) {
                Deserialize = x => (StartUpOnOffEnum?)DeserializeEnum(x)
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Behavior that supports lighting applications.
            /// </summary>
            Lighting = 1,
            /// <summary>
            /// Device has DeadFrontBehavior Feature
            /// </summary>
            DeadFrontBehavior = 2,
            /// <summary>
            /// Device supports the OffOnly Feature feature
            /// </summary>
            OffOnly = 4,
        }

        /// <summary>
        /// Start Up On Off
        /// </summary>
        public enum StartUpOnOffEnum : byte {
            /// <summary>
            /// Set the OnOff attribute to FALSE
            /// </summary>
            Off = 0,
            /// <summary>
            /// Set the OnOff attribute to TRUE
            /// </summary>
            On = 1,
            /// <summary>
            /// If the previous value of the OnOff attribute is equal to FALSE, set the OnOff attribute to TRUE. If the previous value of the OnOff attribute is equal to TRUE, set the OnOff attribute to FALSE (toggle).
            /// </summary>
            Toggle = 2,
        }

        /// <summary>
        /// Effect Identifier
        /// </summary>
        public enum EffectIdentifier : byte {
            /// <summary>
            /// Delayed All Off
            /// </summary>
            DelayedAllOff = 0,
            /// <summary>
            /// Dying Light
            /// </summary>
            DyingLight = 1,
        }

        /// <summary>
        /// Delayed All Off Effect Variant
        /// </summary>
        public enum DelayedAllOffEffectVariant : byte {
            /// <summary>
            /// Fade to off in 0.8 seconds
            /// </summary>
            DelayedOffFastFade = 0,
            /// <summary>
            /// No fade
            /// </summary>
            NoFade = 1,
            /// <summary>
            /// 50% dim down in 0.8 seconds then fade to off in 12 seconds
            /// </summary>
            DelayedOffSlowFade = 2,
        }

        /// <summary>
        /// Dying Light Effect Variant
        /// </summary>
        public enum DyingLightEffectVariant : byte {
            /// <summary>
            /// 20% dim up in 0.5s then fade to off in 1 second
            /// </summary>
            DyingLightFadeOff = 0,
        }

        /// <summary>
        /// On Off Control
        /// </summary>
        [Flags]
        public enum OnOffControl : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Indicates a command is only accepted when in On state.
            /// </summary>
            AcceptOnlyWhenOn = 0x01,
        }
        #endregion Enums

        #region Payloads
        private record OffWithEffectPayload : TLVPayload {
            public required EffectIdentifier EffectIdentifier { get; set; }
            public required byte EffectVariant { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)EffectIdentifier);
                writer.WriteByte(1, EffectVariant);
                writer.EndContainer();
            }
        }

        private record OnWithTimedOffPayload : TLVPayload {
            public required OnOffControl OnOffControl { get; set; }
            public required ushort OnTime { get; set; }
            public required ushort OffWaitTime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)OnOffControl);
                writer.WriteUShort(1, OnTime);
                writer.WriteUShort(2, OffWaitTime);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Off
        /// </summary>
        public async Task<bool> Off(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, null, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// On
        /// </summary>
        public async Task<bool> On(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, null, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Toggle
        /// </summary>
        public async Task<bool> Toggle(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, null, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Off With Effect
        /// </summary>
        public async Task<bool> OffWithEffect(SecureSession session, EffectIdentifier effectIdentifier, byte effectVariant, CancellationToken token = default) {
            OffWithEffectPayload requestFields = new OffWithEffectPayload() {
                EffectIdentifier = effectIdentifier,
                EffectVariant = effectVariant,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x40, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// On With Recall Global Scene
        /// </summary>
        public async Task<bool> OnWithRecallGlobalScene(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x41, null, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// On With Timed Off
        /// </summary>
        public async Task<bool> OnWithTimedOff(SecureSession session, OnOffControl onOffControl, ushort onTime, ushort offWaitTime, CancellationToken token = default) {
            OnWithTimedOffPayload requestFields = new OnWithTimedOffPayload() {
                OnOffControl = onOffControl,
                OnTime = onTime,
                OffWaitTime = offWaitTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x42, requestFields, token);
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
        /// On Off Attribute [Read/Event]
        /// </summary>
        public required ReportAttribute<bool> OnOff { get; init; }

        /// <summary>
        /// Global Scene Control Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool> GlobalSceneControl { get; init; }

        /// <summary>
        /// On Time Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> OnTime { get; init; }

        /// <summary>
        /// Off Wait Time Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> OffWaitTime { get; init; }

        /// <summary>
        /// Start Up On Off Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<StartUpOnOffEnum?> StartUpOnOff { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "On/Off";
        }
    }
}