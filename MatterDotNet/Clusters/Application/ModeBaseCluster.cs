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
using MatterDotNet.Protocol.Payloads.Status;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Mode Base Cluster
    /// </summary>
    public class ModeBaseCluster : ClusterBase
    {

        /// <summary>
        /// Mode Base Cluster
        /// </summary>
        public ModeBaseCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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

            [SetsRequiredMembers]
            internal ModeOption(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Label = reader.GetString(0, false)!;
                Mode = reader.GetByte(1)!.Value;
                {
                    ModeTags = new ModeTag[((object[])fields[2]).Length];
                    for (int i = 0; i < ModeTags.Length; i++) {
                        ModeTags[i] = new ModeTag((object[])fields[-1]);
                    }
                }
            }
            public required string Label { get; set; }
            public required byte Mode { get; set; }
            public required ModeTag[] ModeTags { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Label, 64);
                writer.WriteByte(1, Mode);
                {
                    Constrain(ModeTags, 0, 8);
                    writer.StartArray(2);
                    foreach (var item in ModeTags) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Mode Tag
        /// </summary>
        public record ModeTag : TLVPayload {
            /// <summary>
            /// Mode Tag
            /// </summary>
            public ModeTag() { }

            [SetsRequiredMembers]
            internal ModeTag(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                MfgCode = reader.GetUShort(0, true);
                Value = reader.GetUShort(1)!.Value;
            }
            public ushort? MfgCode { get; set; }
            public required ushort Value { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (MfgCode != null)
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

        /// <summary>
        /// Change To Mode Response - Reply from server
        /// </summary>
        public struct ChangeToModeResponse() {
            public required IMStatusCode Status { get; set; }
            public required string StatusText { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Change To Mode
        /// </summary>
        public async Task<ChangeToModeResponse?> ChangeToMode(SecureSession session, byte NewMode) {
            ChangeToModePayload requestFields = new ChangeToModePayload() {
                NewMode = NewMode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ChangeToModeResponse() {
                Status = (IMStatusCode)(byte)GetField(resp, 0),
                StatusText = (string)GetField(resp, 1),
            };
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
        /// Get the Supported Modes attribute
        /// </summary>
        public async Task<ModeOption[]> GetSupportedModes(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            ModeOption[] list = new ModeOption[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new ModeOption(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Current Mode attribute
        /// </summary>
        public async Task<byte> GetCurrentMode(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 1))!;
        }

        /// <summary>
        /// Get the Start Up Mode attribute
        /// </summary>
        public async Task<byte?> GetStartUpMode(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 2, true);
        }

        /// <summary>
        /// Set the Start Up Mode attribute
        /// </summary>
        public async Task SetStartUpMode (SecureSession session, byte? value) {
            await SetAttribute(session, 2, value, true);
        }

        /// <summary>
        /// Get the On Mode attribute
        /// </summary>
        public async Task<byte?> GetOnMode(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 3, true) ?? null;
        }

        /// <summary>
        /// Set the On Mode attribute
        /// </summary>
        public async Task SetOnMode (SecureSession session, byte? value = null) {
            await SetAttribute(session, 3, value, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Mode Base Cluster";
        }
    }
}