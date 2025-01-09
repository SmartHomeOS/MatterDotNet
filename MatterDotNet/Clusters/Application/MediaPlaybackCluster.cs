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
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Media Playback Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class MediaPlaybackCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0506;

        /// <summary>
        /// Media Playback Cluster
        /// </summary>
        public MediaPlaybackCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected MediaPlaybackCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Advanced media seeking
            /// </summary>
            AdvancedSeek = 1,
            /// <summary>
            /// Variable speed playback
            /// </summary>
            VariableSpeed = 2,
            /// <summary>
            /// Text Tracks
            /// </summary>
            TextTracks = 4,
            /// <summary>
            /// Audio Tracks
            /// </summary>
            AudioTracks = 8,
            /// <summary>
            /// Can play audio during fast and slow playback speeds
            /// </summary>
            AudioAdvance = 16,
        }

        /// <summary>
        /// Characteristic
        /// </summary>
        public enum CharacteristicEnum {
            /// <summary>
            /// Textual information meant for display when no other text representation is selected. It is used to clarify dialogue, alternate languages, texted graphics or location/person IDs that are not otherwise covered in the dubbed/localized audio.
            /// </summary>
            ForcedSubtitles = 0,
            /// <summary>
            /// Textual or audio media component containing a textual description (intended for audio synthesis) or an audio description describing a visual component
            /// </summary>
            DescribesVideo = 1,
            /// <summary>
            /// Simplified or reduced captions as specified in [United States Code Title 47 CFR 79.103(c)(9)].
            /// </summary>
            EasyToRead = 2,
            /// <summary>
            /// A media characteristic that indicates that a track selection option includes frame-based content.
            /// </summary>
            FrameBased = 3,
            /// <summary>
            /// Main media component(s) which is/are intended for presentation if no other information is provided
            /// </summary>
            MainProgram = 4,
            /// <summary>
            /// A media characteristic that indicates that a track or media selection option contains original content.
            /// </summary>
            OriginalContent = 5,
            /// <summary>
            /// A media characteristic that indicates that a track or media selection option contains a language translation and verbal interpretation of spoken dialog.
            /// </summary>
            VoiceOverTranslation = 6,
            /// <summary>
            /// Textual media component containing transcriptions of spoken dialog and auditory cues such as sound effects and music for the hearing impaired.
            /// </summary>
            Caption = 7,
            /// <summary>
            /// Textual transcriptions of spoken dialog.
            /// </summary>
            Subtitle = 8,
            /// <summary>
            /// Textual media component containing transcriptions of spoken dialog and auditory cues such as sound effects and music for the hearing impaired.
            /// </summary>
            Alternate = 9,
            /// <summary>
            /// Media content component that is supplementary to a media content component of a different media component type.
            /// </summary>
            Supplementary = 10,
            /// <summary>
            /// Experience that contains a commentary (e.g. director’s commentary) (typically audio)
            /// </summary>
            Commentary = 11,
            /// <summary>
            /// Experience that contains an element that is presented in a different language from the original (e.g. dubbed audio, translated captions)
            /// </summary>
            DubbedTranslation = 12,
            /// <summary>
            /// Textual or audio media component containing a textual description (intended for audio synthesis) or an audio description describing a visual component
            /// </summary>
            Description = 13,
            /// <summary>
            /// Media component containing information intended to be processed by application specific elements.
            /// </summary>
            Metadata = 14,
            /// <summary>
            /// Experience containing an element for improved intelligibility of the dialogue.
            /// </summary>
            EnhancedAudioIntelligibility = 15,
            /// <summary>
            /// Experience that provides information, about a current emergency, that is intended to enable the protection of life, health, safety, and property, and may also include critical details regarding the emergency and how to respond to the emergency.
            /// </summary>
            Emergency = 16,
            /// <summary>
            /// Textual representation of a songs’ lyrics, usually in the same language as the associated song as specified in [SMPTE ST 2067-2].
            /// </summary>
            Karaoke = 17,
        }

        /// <summary>
        /// Playback State
        /// </summary>
        public enum PlaybackStateEnum {
            /// <summary>
            /// Media is currently playing (includes FF and REW)
            /// </summary>
            Playing = 0,
            /// <summary>
            /// Media is currently paused
            /// </summary>
            Paused = 1,
            /// <summary>
            /// Media is not currently playing
            /// </summary>
            NotPlaying = 2,
            /// <summary>
            /// Media is not currently buffering and playback will start when buffer has been filled
            /// </summary>
            Buffering = 3,
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
            /// Requested playback command is invalid in the current playback state.
            /// </summary>
            InvalidStateForCommand = 1,
            /// <summary>
            /// Requested playback command is not allowed in the current playback state. For example, attempting to fast-forward during a commercial might return NotAllowed.
            /// </summary>
            NotAllowed = 2,
            /// <summary>
            /// This endpoint is not active for playback.
            /// </summary>
            NotActive = 3,
            /// <summary>
            /// The FastForward or Rewind Command was issued but the media is already playing back at the fastest speed supported by the server in the respective direction.
            /// </summary>
            SpeedOutOfRange = 4,
            /// <summary>
            /// The Seek Command was issued with a value of position outside of the allowed seek range of the media.
            /// </summary>
            SeekOutOfRange = 5,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Playback Position
        /// </summary>
        public record PlaybackPosition : TLVPayload {
            /// <summary>
            /// Playback Position
            /// </summary>
            public PlaybackPosition() { }

            [SetsRequiredMembers]
            internal PlaybackPosition(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                UpdatedAt = TimeUtil.FromEpochUS(reader.GetULong(0))!.Value;
                Position = reader.GetULong(1, true);
            }
            public required DateTime UpdatedAt { get; set; }
            public required ulong? Position { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, TimeUtil.ToEpochUS(UpdatedAt));
                writer.WriteULong(1, Position);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Track Attributes
        /// </summary>
        public record TrackAttributes : TLVPayload {
            /// <summary>
            /// Track Attributes
            /// </summary>
            public TrackAttributes() { }

            [SetsRequiredMembers]
            internal TrackAttributes(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                LanguageCode = reader.GetString(0, false)!;
                {
                    Characteristics = new CharacteristicEnum[((object[])fields[1]).Length];
                    for (int i = 0; i < Characteristics.Length; i++) {
                        Characteristics[i] = (CharacteristicEnum)reader.GetUShort(-1)!.Value;
                    }
                }
                DisplayName = reader.GetString(2, true);
            }
            public required string LanguageCode { get; set; }
            public CharacteristicEnum[]? Characteristics { get; set; } = null;
            public string? DisplayName { get; set; } = null;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, LanguageCode, 32);
                if (Characteristics != null)
                if (Characteristics != null)
                {
                    writer.StartArray(1);
                    foreach (var item in Characteristics) {
                        writer.WriteUShort(-1, (ushort)item);
                    }
                    writer.EndContainer();
                }
                else
                    writer.WriteNull(1);
                if (DisplayName != null)
                    writer.WriteString(2, DisplayName, 256);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Track
        /// </summary>
        public record Track : TLVPayload {
            /// <summary>
            /// Track
            /// </summary>
            public Track() { }

            [SetsRequiredMembers]
            internal Track(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ID = reader.GetString(0, false)!;
                TrackAttributes = new TrackAttributes((object[])fields[1]);
            }
            public required string ID { get; set; }
            public required TrackAttributes TrackAttributes { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, ID, 32);
                TrackAttributes.Serialize(writer, 1);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record RewindPayload : TLVPayload {
            public required bool AudioAdvanceUnmuted { get; set; } = false;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBool(0, AudioAdvanceUnmuted);
                writer.EndContainer();
            }
        }

        private record FastForwardPayload : TLVPayload {
            public required bool AudioAdvanceUnmuted { get; set; } = false;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBool(0, AudioAdvanceUnmuted);
                writer.EndContainer();
            }
        }

        private record SkipForwardPayload : TLVPayload {
            public required ulong DeltaPositionMilliseconds { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, DeltaPositionMilliseconds);
                writer.EndContainer();
            }
        }

        private record SkipBackwardPayload : TLVPayload {
            public required ulong DeltaPositionMilliseconds { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, DeltaPositionMilliseconds);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Playback Response - Reply from server
        /// </summary>
        public struct PlaybackResponse() {
            public required StatusEnum Status { get; set; }
            public string? Data { get; set; }
        }

        private record SeekPayload : TLVPayload {
            public required ulong Position { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, Position);
                writer.EndContainer();
            }
        }

        private record ActivateAudioTrackPayload : TLVPayload {
            public required string TrackID { get; set; }
            public required byte? AudioOutputIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, TrackID, 32);
                writer.WriteByte(1, AudioOutputIndex);
                writer.EndContainer();
            }
        }

        private record ActivateTextTrackPayload : TLVPayload {
            public required string TrackID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, TrackID, 32);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Play
        /// </summary>
        public async Task<PlaybackResponse?> Play(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Pause
        /// </summary>
        public async Task<PlaybackResponse?> Pause(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Stop
        /// </summary>
        public async Task<PlaybackResponse?> Stop(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Start Over
        /// </summary>
        public async Task<PlaybackResponse?> StartOver(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Previous
        /// </summary>
        public async Task<PlaybackResponse?> Previous(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Next
        /// </summary>
        public async Task<PlaybackResponse?> Next(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Rewind
        /// </summary>
        public async Task<PlaybackResponse?> Rewind(SecureSession session, bool AudioAdvanceUnmuted) {
            RewindPayload requestFields = new RewindPayload() {
                AudioAdvanceUnmuted = AudioAdvanceUnmuted,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Fast Forward
        /// </summary>
        public async Task<PlaybackResponse?> FastForward(SecureSession session, bool AudioAdvanceUnmuted) {
            FastForwardPayload requestFields = new FastForwardPayload() {
                AudioAdvanceUnmuted = AudioAdvanceUnmuted,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Skip Forward
        /// </summary>
        public async Task<PlaybackResponse?> SkipForward(SecureSession session, ulong DeltaPositionMilliseconds) {
            SkipForwardPayload requestFields = new SkipForwardPayload() {
                DeltaPositionMilliseconds = DeltaPositionMilliseconds,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x08, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Skip Backward
        /// </summary>
        public async Task<PlaybackResponse?> SkipBackward(SecureSession session, ulong DeltaPositionMilliseconds) {
            SkipBackwardPayload requestFields = new SkipBackwardPayload() {
                DeltaPositionMilliseconds = DeltaPositionMilliseconds,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x09, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Seek
        /// </summary>
        public async Task<PlaybackResponse?> Seek(SecureSession session, ulong Position) {
            SeekPayload requestFields = new SeekPayload() {
                Position = Position,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0B, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Activate Audio Track
        /// </summary>
        public async Task<bool> ActivateAudioTrack(SecureSession session, string TrackID, byte AudioOutputIndex) {
            ActivateAudioTrackPayload requestFields = new ActivateAudioTrackPayload() {
                TrackID = TrackID,
                AudioOutputIndex = AudioOutputIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0C, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Activate Text Track
        /// </summary>
        public async Task<bool> ActivateTextTrack(SecureSession session, string TrackID) {
            ActivateTextTrackPayload requestFields = new ActivateTextTrackPayload() {
                TrackID = TrackID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0D, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Deactivate Text Track
        /// </summary>
        public async Task<bool> DeactivateTextTrack(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0E);
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
        /// Get the Current State attribute
        /// </summary>
        public async Task<PlaybackStateEnum> GetCurrentState(SecureSession session) {
            return (PlaybackStateEnum)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Get the Start Time attribute
        /// </summary>
        public async Task<DateTime?> GetStartTime(SecureSession session) {
            return (DateTime?)(dynamic?)await GetAttribute(session, 1, true) ?? null;
        }

        /// <summary>
        /// Get the Duration attribute
        /// </summary>
        public async Task<ulong?> GetDuration(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 2, true) ?? null;
        }

        /// <summary>
        /// Get the Sampled Position attribute
        /// </summary>
        public async Task<PlaybackPosition?> GetSampledPosition(SecureSession session) {
            return new PlaybackPosition((object[])(await GetAttribute(session, 3))!) ?? null;
        }

        /// <summary>
        /// Get the Playback Speed attribute
        /// </summary>
        public async Task<float> GetPlaybackSpeed(SecureSession session) {
            return (float?)(dynamic?)await GetAttribute(session, 4) ?? 0;
        }

        /// <summary>
        /// Get the Seek Range End attribute
        /// </summary>
        public async Task<ulong?> GetSeekRangeEnd(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 5, true) ?? null;
        }

        /// <summary>
        /// Get the Seek Range Start attribute
        /// </summary>
        public async Task<ulong?> GetSeekRangeStart(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 6, true) ?? null;
        }

        /// <summary>
        /// Get the Active Audio Track attribute
        /// </summary>
        public async Task<Track?> GetActiveAudioTrack(SecureSession session) {
            return new Track((object[])(await GetAttribute(session, 7))!) ?? null;
        }

        /// <summary>
        /// Get the Available Audio Tracks attribute
        /// </summary>
        public async Task<Track[]?> GetAvailableAudioTracks(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 8))!);
            Track[] list = new Track[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new Track(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Active Text Track attribute
        /// </summary>
        public async Task<Track?> GetActiveTextTrack(SecureSession session) {
            return new Track((object[])(await GetAttribute(session, 9))!) ?? null;
        }

        /// <summary>
        /// Get the Available Text Tracks attribute
        /// </summary>
        public async Task<Track[]?> GetAvailableTextTracks(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 10))!);
            Track[] list = new Track[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new Track(reader.GetStruct(i)!);
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Media Playback Cluster";
        }
    }
}