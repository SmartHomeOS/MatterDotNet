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

using MatterDotNet.Attributes;
using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Payloads.Status;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Robots
{
    /// <summary>
    /// Attributes and commands for selecting a mode from a list of supported options.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class RVCCleanMode : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0055;

        /// <summary>
        /// Attributes and commands for selecting a mode from a list of supported options.
        /// </summary>
        [SetsRequiredMembers]
        public RVCCleanMode(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected RVCCleanMode(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            SupportedModes = new ReadAttribute<ModeOption[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ModeOption[] list = new ModeOption[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new ModeOption(reader.GetStruct(i)!);
                    return list;
                }
            };
            CurrentMode = new ReportAttribute<byte>(cluster, endPoint, 1) {
                Deserialize = x => (byte)(dynamic?)x!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Cluster supports changing clean modes from non-Idle states
            /// </summary>
            DirectModeChange = 65536,
        }

        /// <summary>
        /// Status Code
        /// </summary>
        public enum StatusCode : byte {
            CleaningInProgress = 0x40,
        }

        /// <summary>
        /// Mode Tag
        /// </summary>
        public enum ModeTag : ushort {
            /// <summary>
            /// The device decides which options, features and setting values to use.
            /// </summary>
            Auto = 0x0,
            /// <summary>
            /// The mode of the device is optimizing for faster completion.
            /// </summary>
            Quick = 0x1,
            /// <summary>
            /// The device is silent or barely audible while in this mode.
            /// </summary>
            Quiet = 0x2,
            /// <summary>
            /// Either the mode is inherently low noise or the device optimizes for that.
            /// </summary>
            LowNoise = 0x3,
            /// <summary>
            /// The device is optimizing for lower energy usage in this mode. Sometimes called "Eco mode".
            /// </summary>
            LowEnergy = 0x4,
            /// <summary>
            /// A mode suitable for use during vacations or other extended absences.
            /// </summary>
            Vacation = 0x5,
            /// <summary>
            /// The mode uses the lowest available setting value.
            /// </summary>
            Min = 0x6,
            /// <summary>
            /// The mode uses the highest available setting value.
            /// </summary>
            Max = 0x7,
            /// <summary>
            /// The mode is recommended or suitable for use during night time.
            /// </summary>
            Night = 0x8,
            /// <summary>
            /// The mode is recommended or suitable for use during day time.
            /// </summary>
            Day = 0x9,
            /// <summary>
            /// While in this mode, the device is optimizing for improved cleaning.
            /// </summary>
            DeepClean = 0x4000,
            /// <summary>
            /// The device’s vacuuming feature is enabled in this mode.
            /// </summary>
            Vacuum = 0x4001,
            /// <summary>
            /// The device’s mopping feature is enabled in this mode.
            /// </summary>
            Mop = 0x4002,
        }
        #endregion Enums

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
            public string? StatusText { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Change To Mode
        /// </summary>
        public async Task<ChangeToModeResponse?> ChangeToMode(SecureSession session, byte newMode, CancellationToken token = default) {
            ChangeToModePayload requestFields = new ChangeToModePayload() {
                NewMode = newMode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new ChangeToModeResponse() {
                Status = (IMStatusCode)(byte)GetField(resp, 0),
                StatusText = (string?)GetOptionalField(resp, 1),
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
        /// Supported Modes Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ModeOption[]> SupportedModes { get; init; }

        /// <summary>
        /// Current Mode Attribute [Read/Event]
        /// </summary>
        public required ReportAttribute<byte> CurrentMode { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "RVC Clean Mode";
        }
    }
}