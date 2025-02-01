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
    /// Attributes and commands for putting a device into Identification mode (e.g. flashing a light).
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 4)]
    public class Identify : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0003;

        /// <summary>
        /// Attributes and commands for putting a device into Identification mode (e.g. flashing a light).
        /// </summary>
        [SetsRequiredMembers]
        public Identify(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected Identify(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            IdentifyTime = new ReadWriteAttribute<ushort>(cluster, endPoint, 0) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0

            };
            IdentifyType = new ReadAttribute<IdentifyTypeEnum>(cluster, endPoint, 1) {
                Deserialize = x => (IdentifyTypeEnum)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Identify Type
        /// </summary>
        public enum IdentifyTypeEnum : byte {
            /// <summary>
            /// No presentation.
            /// </summary>
            None = 0,
            /// <summary>
            /// Light output of a lighting product.
            /// </summary>
            LightOutput = 1,
            /// <summary>
            /// Typically a small LED.
            /// </summary>
            VisibleIndicator = 2,
            /// <summary>
            /// 
            /// </summary>
            AudibleBeep = 3,
            /// <summary>
            /// Presentation will be visible on display screen.
            /// </summary>
            Display = 4,
            /// <summary>
            /// Presentation will be conveyed by actuator functionality such as through a window blind operation or in-wall relay.
            /// </summary>
            Actuator = 5,
        }

        /// <summary>
        /// Effect Identifier
        /// </summary>
        public enum EffectIdentifier : byte {
            /// <summary>
            /// e.g., Light is turned on/off once.
            /// </summary>
            Blink = 0x0,
            /// <summary>
            /// e.g., Light is turned on/off over 1 second and repeated 15 times.
            /// </summary>
            Breathe = 0x1,
            /// <summary>
            /// e.g., Colored light turns green for 1 second; non-colored light flashes twice.
            /// </summary>
            Okay = 0x2,
            /// <summary>
            /// e.g., Colored light turns orange for 8 seconds; non-colored light switches to the maximum brightness for 0.5s and then minimum brightness for 7.5s.
            /// </summary>
            ChannelChange = 0xB,
            /// <summary>
            /// Complete the current effect sequence before terminating. e.g., if in the middle of a breathe effect (as above), first complete the current 1s breathe effect and then terminate the effect.
            /// </summary>
            FinishEffect = 0xFE,
            /// <summary>
            /// Terminate the effect as soon as possible.
            /// </summary>
            StopEffect = 0xFF,
        }

        /// <summary>
        /// Effect Variant
        /// </summary>
        public enum EffectVariant : byte {
            /// <summary>
            /// Indicates the default effect is used
            /// </summary>
            Default = 0,
        }
        #endregion Enums

        #region Payloads
        private record IdentifyCommandPayload : TLVPayload {
            public required ushort IdentifyTime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, IdentifyTime);
                writer.EndContainer();
            }
        }

        private record TriggerEffectPayload : TLVPayload {
            public required EffectIdentifier EffectIdentifier { get; set; }
            public required EffectVariant EffectVariant { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)EffectIdentifier);
                writer.WriteUShort(1, (ushort)EffectVariant);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Identify Command
        /// </summary>
        public async Task<bool> IdentifyCommand(SecureSession session, ushort identifyTime, CancellationToken token = default) {
            IdentifyCommandPayload requestFields = new IdentifyCommandPayload() {
                IdentifyTime = identifyTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Trigger Effect
        /// </summary>
        public async Task<bool> TriggerEffect(SecureSession session, EffectIdentifier effectIdentifier, EffectVariant effectVariant, CancellationToken token = default) {
            TriggerEffectPayload requestFields = new TriggerEffectPayload() {
                EffectIdentifier = effectIdentifier,
                EffectVariant = effectVariant,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x40, requestFields, token);
            return ValidateResponse(resp);
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Identify Time Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> IdentifyTime { get; init; }

        /// <summary>
        /// Identify Type Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<IdentifyTypeEnum> IdentifyType { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Identify";
        }
    }
}