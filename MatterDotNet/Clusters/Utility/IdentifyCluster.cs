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

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Identify Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 4)]
    public class IdentifyCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0003;

        /// <summary>
        /// Identify Cluster
        /// </summary>
        public IdentifyCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected IdentifyCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Effect Identifier
        /// </summary>
        public enum EffectIdentifierEnum {
            /// <summary>
            /// e.g., Light is turned on/off once.
            /// </summary>
            Blink = 0x00,
            /// <summary>
            /// e.g., Colored light turns orange for 8 seconds; non-colored light switches to the maximum brightness for 0.5s and then minimum brightness for 7.5s.
            /// </summary>
            ChannelChange = 0x0B,
            /// <summary>
            /// e.g., Light is turned on/off over 1 second and repeated 15 times.
            /// </summary>
            Breathe = 0x01,
            /// <summary>
            /// e.g., Colored light turns green for 1 second; non-colored light flashes twice.
            /// </summary>
            Okay = 0x02,
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
        public enum EffectVariantEnum {
            /// <summary>
            /// Indicates the default effect is used
            /// </summary>
            Default = 0x00,
        }

        /// <summary>
        /// Identify Type
        /// </summary>
        public enum IdentifyTypeEnum {
            /// <summary>
            /// No presentation.
            /// </summary>
            None = 0x00,
            /// <summary>
            /// Light output of a lighting product.
            /// </summary>
            LightOutput = 0x01,
            /// <summary>
            /// Typically a small LED.
            /// </summary>
            VisibleIndicator = 0x02,
            AudibleBeep = 0x03,
            /// <summary>
            /// Presentation will be visible on display screen.
            /// </summary>
            Display = 0x04,
            /// <summary>
            /// Presentation will be conveyed by actuator functionality such as through a window blind operation or in-wall relay.
            /// </summary>
            Actuator = 0x05,
        }
        #endregion Enums

        #region Payloads
        private record IdentifyPayload : TLVPayload {
            public required ushort IdentifyTime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, IdentifyTime);
                writer.EndContainer();
            }
        }

        private record TriggerEffectPayload : TLVPayload {
            public required EffectIdentifierEnum EffectIdentifier { get; set; }
            public required EffectVariantEnum EffectVariant { get; set; }
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
        /// Identify
        /// </summary>
        public async Task<bool> Identify(SecureSession session, ushort IdentifyTime) {
            IdentifyPayload requestFields = new IdentifyPayload() {
                IdentifyTime = IdentifyTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Trigger Effect
        /// </summary>
        public async Task<bool> TriggerEffect(SecureSession session, EffectIdentifierEnum EffectIdentifier, EffectVariantEnum EffectVariant) {
            TriggerEffectPayload requestFields = new TriggerEffectPayload() {
                EffectIdentifier = EffectIdentifier,
                EffectVariant = EffectVariant,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x40, requestFields);
            return ValidateResponse(resp);
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Get the Identify Time attribute
        /// </summary>
        public async Task<ushort> GetIdentifyTime(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 0) ?? 0;
        }

        /// <summary>
        /// Set the Identify Time attribute
        /// </summary>
        public async Task SetIdentifyTime (SecureSession session, ushort? value = 0) {
            await SetAttribute(session, 0, value);
        }

        /// <summary>
        /// Get the Identify Type attribute
        /// </summary>
        public async Task<IdentifyTypeEnum> GetIdentifyType(SecureSession session) {
            return (IdentifyTypeEnum)await GetEnumAttribute(session, 1);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Identify Cluster";
        }
    }
}