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
    /// Attributes and commands for selecting a mode from a list of supported options.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class ModeSelect : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0050;

        /// <summary>
        /// Attributes and commands for selecting a mode from a list of supported options.
        /// </summary>
        [SetsRequiredMembers]
        public ModeSelect(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ModeSelect(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            Description = new ReadAttribute<string>(cluster, endPoint, 0) {
                Deserialize = x => (string)(dynamic?)x!
            };
            StandardNamespace = new ReadAttribute<ushort?>(cluster, endPoint, 1, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            SupportedModes = new ReadAttribute<ModeOption[]>(cluster, endPoint, 2) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ModeOption[] list = new ModeOption[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new ModeOption(reader.GetStruct(i)!);
                    return list;
                }
            };
            CurrentMode = new ReadAttribute<byte>(cluster, endPoint, 3) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            StartUpMode = new ReadWriteAttribute<byte?>(cluster, endPoint, 4, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            OnMode = new ReadWriteAttribute<byte?>(cluster, endPoint, 5, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Dependency with the OnOff cluster
            /// </summary>
            OnOff = 1,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Semantic Tag
        /// </summary>
        public record SemanticTag : TLVPayload {
            /// <summary>
            /// Semantic Tag
            /// </summary>
            public SemanticTag() { }

            /// <summary>
            /// Semantic Tag
            /// </summary>
            [SetsRequiredMembers]
            public SemanticTag(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                MfgCode = reader.GetUShort(0)!.Value;
                Value = reader.GetUShort(1)!.Value;
            }
            public required ushort MfgCode { get; set; }
            public required ushort Value { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, MfgCode);
                writer.WriteUShort(1, Value);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Mode Option
        /// </summary>
        public record ModeOption : TLVPayload {
            /// <summary>
            /// Mode Option
            /// </summary>
            public ModeOption() { }

            /// <summary>
            /// Mode Option
            /// </summary>
            [SetsRequiredMembers]
            public ModeOption(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Label = reader.GetString(0, false, 64)!;
                Mode = reader.GetByte(1)!.Value;
                {
                    SemanticTags = new SemanticTag[reader.GetStruct(2)!.Length];
                    for (int n = 0; n < SemanticTags.Length; n++) {
                        SemanticTags[n] = new SemanticTag((object[])((object[])fields[2])[n]);
                    }
                }
            }
            public required string Label { get; set; }
            public required byte Mode { get; set; }
            public required SemanticTag[] SemanticTags { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Label, 64);
                writer.WriteByte(1, Mode);
                {
                    Constrain(SemanticTags, 0, 64);
                    writer.StartArray(2);
                    foreach (var item in SemanticTags) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record ChangeToModePayload : TLVPayload {
            public required byte NewMode { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, NewMode);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Change To Mode
        /// </summary>
        public async Task<bool> ChangeToMode(SecureSession session, byte newMode, CancellationToken token = default) {
            ChangeToModePayload requestFields = new ChangeToModePayload() {
                NewMode = newMode,
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
        /// Description Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> Description { get; init; }

        /// <summary>
        /// Standard Namespace Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort?> StandardNamespace { get; init; }

        /// <summary>
        /// Supported Modes Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ModeOption[]> SupportedModes { get; init; }

        /// <summary>
        /// Current Mode Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> CurrentMode { get; init; }

        /// <summary>
        /// Start Up Mode Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte?> StartUpMode { get; init; }

        /// <summary>
        /// On Mode Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte?> OnMode { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Mode Select";
        }
    }
}