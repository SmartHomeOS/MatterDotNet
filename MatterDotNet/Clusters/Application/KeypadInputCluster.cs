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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Keypad Input Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class KeypadInputCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0509;

        /// <summary>
        /// Keypad Input Cluster
        /// </summary>
        public KeypadInputCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected KeypadInputCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports UP, DOWN, LEFT, RIGHT, SELECT, BACK, EXIT, MENU
            /// </summary>
            NavigationKeyCodes = 1,
            /// <summary>
            /// Supports CEC keys 0x0A (Settings) and 0x09 (Home)
            /// </summary>
            LocationKeys = 2,
            /// <summary>
            /// Supports numeric input 0..9
            /// </summary>
            NumberKeys = 4,
        }

        /// <summary>
        /// Cec Key Code
        /// </summary>
        public enum CecKeyCodeEnum {
            Select = 0x00,
            SetupMenu = 0x0A,
            ContentsMenu = 0x0B,
            FavoriteMenu = 0x0C,
            Exit = 0x0D,
            Up = 0x01,
            NumberEntryMode = 0x1D,
            Number11 = 0x1E,
            Number12 = 0x1F,
            Down = 0x02,
            Dot = 0x2A,
            Enter = 0x2B,
            Clear = 0x2C,
            NextFavorite = 0x2F,
            Left = 0x03,
            Right = 0x04,
            Eject = 0x4A,
            Forward = 0x4B,
            Backward = 0x4C,
            StopRecord = 0x4D,
            PauseRecord = 0x4E,
            Reserved = 0x4F,
            RightUp = 0x05,
            RightDown = 0x06,
            SelectAudioInputFunction = 0x6A,
            PowerToggleFunction = 0x6B,
            PowerOffFunction = 0x6C,
            PowerOnFunction = 0x6D,
            LeftUp = 0x07,
            LeftDown = 0x08,
            RootMenu = 0x09,
            MediaTopMenu = 0x10,
            MediaContextSensitiveMenu = 0x11,
            Number0OrNumber10 = 0x20,
            Numbers1 = 0x21,
            Numbers2 = 0x22,
            Numbers3 = 0x23,
            Numbers4 = 0x24,
            Numbers5 = 0x25,
            Numbers6 = 0x26,
            Numbers7 = 0x27,
            Numbers8 = 0x28,
            Numbers9 = 0x29,
            ChannelUp = 0x30,
            ChannelDown = 0x31,
            PreviousChannel = 0x32,
            SoundSelect = 0x33,
            InputSelect = 0x34,
            DisplayInformation = 0x35,
            Help = 0x36,
            PageUp = 0x37,
            PageDown = 0x38,
            Power = 0x40,
            VolumeUp = 0x41,
            VolumeDown = 0x42,
            Mute = 0x43,
            Play = 0x44,
            Stop = 0x45,
            Pause = 0x46,
            Record = 0x47,
            Rewind = 0x48,
            FastForward = 0x49,
            Angle = 0x50,
            SubPicture = 0x51,
            VideoOnDemand = 0x52,
            ElectronicProgramGuide = 0x53,
            TimerProgramming = 0x54,
            InitialConfiguration = 0x55,
            SelectBroadcastType = 0x56,
            SelectSoundPresentation = 0x57,
            PlayFunction = 0x60,
            PausePlayFunction = 0x61,
            RecordFunction = 0x62,
            PauseRecordFunction = 0x63,
            StopFunction = 0x64,
            MuteFunction = 0x65,
            RestoreVolumeFunction = 0x66,
            TuneFunction = 0x67,
            SelectMediaFunction = 0x68,
            SelectAvInputFunction = 0x69,
            F1Blue = 0x71,
            F2Red = 0x72,
            F3Green = 0x73,
            F4Yellow = 0x74,
            F5 = 0x75,
            Data = 0x76,
        }

        /// <summary>
        /// Status
        /// </summary>
        public enum StatusEnum {
            /// <summary>
            /// Succeeded
            /// </summary>
            Success = 0,
            /// <summary>
            /// Key code is not supported.
            /// </summary>
            UnsupportedKey = 1,
            /// <summary>
            /// Requested key code is invalid in the context of the responder's current state.
            /// </summary>
            InvalidKeyInCurrentState = 2,
        }
        #endregion Enums

        #region Payloads
        private record SendKeyPayload : TLVPayload {
            public required CecKeyCodeEnum KeyCode { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)KeyCode);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Send Key Response - Reply from server
        /// </summary>
        public struct SendKeyResponse() {
            public required StatusEnum Status { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Send Key
        /// </summary>
        public async Task<SendKeyResponse?> SendKey(SecureSession session, CecKeyCodeEnum KeyCode) {
            SendKeyPayload requestFields = new SendKeyPayload() {
                KeyCode = KeyCode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new SendKeyResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
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
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Keypad Input Cluster";
        }
    }
}