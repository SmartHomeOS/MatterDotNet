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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Mode Select Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class ModeSelectCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0050;

        /// <summary>
        /// Mode Select Cluster
        /// </summary>
        public ModeSelectCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ModeSelectCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
                Label = reader.GetString(0, false)!;
                Mode = reader.GetByte(1)!.Value;
                {
                    SemanticTags = new SemanticTag[((object[])fields[2]).Length];
                    for (int i = 0; i < SemanticTags.Length; i++) {
                        SemanticTags[i] = new SemanticTag((object[])fields[-1]);
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
        public async Task<bool> ChangeToMode(SecureSession session, byte NewMode) {
            ChangeToModePayload requestFields = new ChangeToModePayload() {
                NewMode = NewMode,
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
        /// Get the Description attribute
        /// </summary>
        public async Task<string> GetDescription(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Get the Standard Namespace attribute
        /// </summary>
        public async Task<ushort?> GetStandardNamespace(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 1, true) ?? null;
        }

        /// <summary>
        /// Get the Supported Modes attribute
        /// </summary>
        public async Task<ModeOption[]> GetSupportedModes(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 2))!);
            ModeOption[] list = new ModeOption[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new ModeOption(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Current Mode attribute
        /// </summary>
        public async Task<byte> GetCurrentMode(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 3))!;
        }

        /// <summary>
        /// Get the Start Up Mode attribute
        /// </summary>
        public async Task<byte?> GetStartUpMode(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 4, true);
        }

        /// <summary>
        /// Set the Start Up Mode attribute
        /// </summary>
        public async Task SetStartUpMode (SecureSession session, byte? value) {
            await SetAttribute(session, 4, value, true);
        }

        /// <summary>
        /// Get the On Mode attribute
        /// </summary>
        public async Task<byte?> GetOnMode(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 5, true) ?? null;
        }

        /// <summary>
        /// Set the On Mode attribute
        /// </summary>
        public async Task SetOnMode (SecureSession session, byte? value = null) {
            await SetAttribute(session, 5, value, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Mode Select Cluster";
        }
    }
}