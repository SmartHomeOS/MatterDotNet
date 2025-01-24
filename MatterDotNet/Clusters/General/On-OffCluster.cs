﻿// MatterDotNet Copyright (C) 2025 
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
        public On_Off(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected On_Off(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum StartUpOnOff : byte {
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
            None = 0,
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
        public async Task<bool> Off(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// On
        /// </summary>
        public async Task<bool> On(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Toggle
        /// </summary>
        public async Task<bool> Toggle(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Off With Effect
        /// </summary>
        public async Task<bool> OffWithEffect(SecureSession session, EffectIdentifier effectIdentifier, byte effectVariant) {
            OffWithEffectPayload requestFields = new OffWithEffectPayload() {
                EffectIdentifier = effectIdentifier,
                EffectVariant = effectVariant,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x40, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// On With Recall Global Scene
        /// </summary>
        public async Task<bool> OnWithRecallGlobalScene(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x41);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// On With Timed Off
        /// </summary>
        public async Task<bool> OnWithTimedOff(SecureSession session, OnOffControl onOffControl, ushort onTime, ushort offWaitTime) {
            OnWithTimedOffPayload requestFields = new OnWithTimedOffPayload() {
                OnOffControl = onOffControl,
                OnTime = onTime,
                OffWaitTime = offWaitTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x42, requestFields);
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
        /// Get the On Off attribute
        /// </summary>
        public async Task<bool> GetOnOff(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 0) ?? false;
        }

        /// <summary>
        /// Get the Global Scene Control attribute
        /// </summary>
        public async Task<bool> GetGlobalSceneControl(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 16384) ?? true;
        }

        /// <summary>
        /// Get the On Time attribute
        /// </summary>
        public async Task<ushort> GetOnTime(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16385) ?? 0;
        }

        /// <summary>
        /// Set the On Time attribute
        /// </summary>
        public async Task SetOnTime (SecureSession session, ushort? value = 0) {
            await SetAttribute(session, 16385, value);
        }

        /// <summary>
        /// Get the Off Wait Time attribute
        /// </summary>
        public async Task<ushort> GetOffWaitTime(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16386) ?? 0;
        }

        /// <summary>
        /// Set the Off Wait Time attribute
        /// </summary>
        public async Task SetOffWaitTime (SecureSession session, ushort? value = 0) {
            await SetAttribute(session, 16386, value);
        }

        /// <summary>
        /// Get the Start Up On Off attribute
        /// </summary>
        public async Task<StartUpOnOff?> GetStartUpOnOff(SecureSession session) {
            return (StartUpOnOff?)await GetEnumAttribute(session, 16387, true);
        }

        /// <summary>
        /// Set the Start Up On Off attribute
        /// </summary>
        public async Task SetStartUpOnOff (SecureSession session, StartUpOnOff? value) {
            await SetAttribute(session, 16387, value, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "On/Off";
        }
    }
}