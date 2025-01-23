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

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// Attributes and commands for selecting a mode from a list of supported options.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class WaterHeaterMode : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x009E;

        /// <summary>
        /// Attributes and commands for selecting a mode from a list of supported options.
        /// </summary>
        public WaterHeaterMode(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected WaterHeaterMode(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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

        /// <summary>
        /// Mode Tag
        /// </summary>
        public enum ModeTag : ushort {
            Auto = 0x0000,
            Quick = 0x0001,
            Quiet = 0x0002,
            LowNoise = 0x0003,
            LowEnergy = 0x0004,
            Vacation = 0x0005,
            Min = 0x0006,
            Max = 0x0007,
            Night = 0x0008,
            Day = 0x0009,
            Off = 0x4000,
            Manual = 0x4001,
            Timed = 0x4002,
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
        public async Task<ChangeToModeResponse?> ChangeToMode(SecureSession session, byte newMode) {
            ChangeToModePayload requestFields = new ChangeToModePayload() {
                NewMode = newMode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
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
            return (byte?)(dynamic?)await GetAttribute(session, 3, true);
        }

        /// <summary>
        /// Set the On Mode attribute
        /// </summary>
        public async Task SetOnMode (SecureSession session, byte? value) {
            await SetAttribute(session, 3, value, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Water Heater Mode";
        }
    }
}