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
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Media
{
    /// <summary>
    /// This cluster provides an interface for controlling Media Playback (PLAY, PAUSE, etc) on a media device such as a TV or Speaker.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class MediaPlayback : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0506;

        /// <summary>
        /// This cluster provides an interface for controlling Media Playback (PLAY, PAUSE, etc) on a media device such as a TV or Speaker.
        /// </summary>
        [SetsRequiredMembers]
        public MediaPlayback(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected MediaPlayback(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            CurrentState = new ReadAttribute<PlaybackState>(cluster, endPoint, 0) {
                Deserialize = x => (PlaybackState)DeserializeEnum(x)!
            };
            StartTime = new ReadAttribute<DateTime?>(cluster, endPoint, 1, true) {
                Deserialize = x => (DateTime?)(dynamic?)x ?? TimeUtil.EPOCH

            };
            Duration = new ReadAttribute<ulong?>(cluster, endPoint, 2, true) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0

            };
            SampledPosition = new ReadAttribute<PlaybackPosition?>(cluster, endPoint, 3, true) {
                Deserialize = x => new PlaybackPosition((object[])x!)
            };
            PlaybackSpeed = new ReadAttribute<float>(cluster, endPoint, 4) {
                Deserialize = x => (float?)(dynamic?)x ?? 0

            };
            SeekRangeEnd = new ReadAttribute<ulong?>(cluster, endPoint, 5, true) {
                Deserialize = x => (ulong?)(dynamic?)x
            };
            SeekRangeStart = new ReadAttribute<ulong?>(cluster, endPoint, 6, true) {
                Deserialize = x => (ulong?)(dynamic?)x
            };
            ActiveAudioTrack = new ReadAttribute<Track?>(cluster, endPoint, 7, true) {
                Deserialize = x => new Track((object[])x!)
            };
            AvailableAudioTracks = new ReadAttribute<Track[]?>(cluster, endPoint, 8, true) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    Track[] list = new Track[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new Track(reader.GetStruct(i)!);
                    return list;
                }
            };
            ActiveTextTrack = new ReadAttribute<Track?>(cluster, endPoint, 9, true) {
                Deserialize = x => new Track((object[])x!)
            };
            AvailableTextTracks = new ReadAttribute<Track[]?>(cluster, endPoint, 10, true) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    Track[] list = new Track[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new Track(reader.GetStruct(i)!);
                    return list;
                }
            };
        }

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
        /// Playback State
        /// </summary>
        public enum PlaybackState : byte {
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
        public enum Status : byte {
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

        /// <summary>
        /// Characteristic
        /// </summary>
        public enum Characteristic : byte {
            /// <summary>
            /// Textual information meant for display when no other text representation is selected. It is used to clarify dialogue, alternate languages, texted graphics or location/person IDs that are not otherwise covered in the dubbed/localized audio.
            /// </summary>
            ForcedSubtitles = 0x0,
            /// <summary>
            /// Textual or audio media component containing a textual description (intended for audio synthesis) or an audio description describing a visual component
            /// </summary>
            DescribesVideo = 0x1,
            /// <summary>
            /// Simplified or reduced captions as specified in [United States Code Title 47 CFR 79.103(c)(9)].
            /// </summary>
            EasyToRead = 0x2,
            /// <summary>
            /// A media characteristic that indicates that a track selection option includes frame-based content.
            /// </summary>
            FrameBased = 0x3,
            /// <summary>
            /// Main media component(s) which is/are intended for presentation if no other information is provided
            /// </summary>
            MainProgram = 0x4,
            /// <summary>
            /// A media characteristic that indicates that a track or media selection option contains original content.
            /// </summary>
            OriginalContent = 0x5,
            /// <summary>
            /// A media characteristic that indicates that a track or media selection option contains a language translation and verbal interpretation of spoken dialog.
            /// </summary>
            VoiceOverTranslation = 0x6,
            /// <summary>
            /// Textual media component containing transcriptions of spoken dialog and auditory cues such as sound effects and music for the hearing impaired.
            /// </summary>
            Caption = 0x7,
            /// <summary>
            /// Textual transcriptions of spoken dialog.
            /// </summary>
            Subtitle = 0x8,
            /// <summary>
            /// Textual media component containing transcriptions of spoken dialog and auditory cues such as sound effects and music for the hearing impaired.
            /// </summary>
            Alternate = 0x9,
            /// <summary>
            /// Media content component that is supplementary to a media content component of a different media component type.
            /// </summary>
            Supplementary = 0xA,
            /// <summary>
            /// Experience that contains a commentary (e.g. director’s commentary) (typically audio)
            /// </summary>
            Commentary = 0xB,
            /// <summary>
            /// Experience that contains an element that is presented in a different language from the original (e.g. dubbed audio, translated captions)
            /// </summary>
            DubbedTranslation = 0xC,
            /// <summary>
            /// Textual or audio media component containing a textual description (intended for audio synthesis) or an audio description describing a visual component
            /// </summary>
            Description = 0xD,
            /// <summary>
            /// Media component containing information intended to be processed by application specific elements.
            /// </summary>
            Metadata = 0xE,
            /// <summary>
            /// Experience containing an element for improved intelligibility of the dialogue.
            /// </summary>
            EnhancedAudioIntelligibility = 0xF,
            /// <summary>
            /// Experience that provides information, about a current emergency, that is intended to enable the protection of life, health, safety, and property, and may also include critical details regarding the emergency and how to respond to the emergency.
            /// </summary>
            Emergency = 0x10,
            /// <summary>
            /// Textual representation of a songs’ lyrics, usually in the same language as the associated song as specified in [SMPTE ST 2067-2].
            /// </summary>
            Karaoke = 0x11,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Track
        /// </summary>
        public record Track : TLVPayload {
            /// <summary>
            /// Track
            /// </summary>
            public Track() { }

            /// <summary>
            /// Track
            /// </summary>
            [SetsRequiredMembers]
            public Track(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ID = reader.GetString(0, false, 32)!;
                TrackAttributes = new TrackAttributes((object[])fields[1]);
            }
            public required string ID { get; set; }
            public required TrackAttributes? TrackAttributes { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, ID, 32);
                if (TrackAttributes == null)
                    writer.WriteNull(1);
                else
                    TrackAttributes.Serialize(writer, 1);
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

            /// <summary>
            /// Track Attributes
            /// </summary>
            [SetsRequiredMembers]
            public TrackAttributes(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                LanguageCode = reader.GetString(0, false, 32)!;
                DisplayName = reader.GetString(1, true);
            }
            public required string LanguageCode { get; set; }
            public string? DisplayName { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, LanguageCode, 32);
                if (DisplayName != null)
                    writer.WriteString(1, DisplayName);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Playback Position
        /// </summary>
        public record PlaybackPosition : TLVPayload {
            /// <summary>
            /// Playback Position
            /// </summary>
            public PlaybackPosition() { }

            /// <summary>
            /// Playback Position
            /// </summary>
            [SetsRequiredMembers]
            public PlaybackPosition(object[] fields) {
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
        #endregion Records

        #region Payloads
        private record RewindPayload : TLVPayload {
            public bool? AudioAdvanceUnmuted { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (AudioAdvanceUnmuted != null)
                    writer.WriteBool(0, AudioAdvanceUnmuted);
                writer.EndContainer();
            }
        }

        private record FastForwardPayload : TLVPayload {
            public bool? AudioAdvanceUnmuted { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (AudioAdvanceUnmuted != null)
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

        private record SeekPayload : TLVPayload {
            public required ulong Position { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, Position);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Playback Response - Reply from server
        /// </summary>
        public struct PlaybackResponse() {
            public required Status Status { get; set; }
            public string? Data { get; set; }
        }

        private record ActivateAudioTrackPayload : TLVPayload {
            public required string TrackID { get; set; }
            public required byte AudioOutputIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, TrackID);
                writer.WriteByte(1, AudioOutputIndex);
                writer.EndContainer();
            }
        }

        private record ActivateTextTrackPayload : TLVPayload {
            public required string TrackID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, TrackID);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Play
        /// </summary>
        public async Task<PlaybackResponse?> Play(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, null, token);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Pause
        /// </summary>
        public async Task<PlaybackResponse?> Pause(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, null, token);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Stop
        /// </summary>
        public async Task<PlaybackResponse?> Stop(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, null, token);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Start Over
        /// </summary>
        public async Task<PlaybackResponse?> StartOver(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, null, token);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Previous
        /// </summary>
        public async Task<PlaybackResponse?> Previous(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, null, token);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Next
        /// </summary>
        public async Task<PlaybackResponse?> Next(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, null, token);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Rewind
        /// </summary>
        public async Task<PlaybackResponse?> Rewind(SecureSession session, bool? audioAdvanceUnmuted, CancellationToken token = default) {
            RewindPayload requestFields = new RewindPayload() {
                AudioAdvanceUnmuted = audioAdvanceUnmuted,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Fast Forward
        /// </summary>
        public async Task<PlaybackResponse?> FastForward(SecureSession session, bool? audioAdvanceUnmuted, CancellationToken token = default) {
            FastForwardPayload requestFields = new FastForwardPayload() {
                AudioAdvanceUnmuted = audioAdvanceUnmuted,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Skip Forward
        /// </summary>
        public async Task<PlaybackResponse?> SkipForward(SecureSession session, ulong deltaPositionMilliseconds, CancellationToken token = default) {
            SkipForwardPayload requestFields = new SkipForwardPayload() {
                DeltaPositionMilliseconds = deltaPositionMilliseconds,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x08, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Skip Backward
        /// </summary>
        public async Task<PlaybackResponse?> SkipBackward(SecureSession session, ulong deltaPositionMilliseconds, CancellationToken token = default) {
            SkipBackwardPayload requestFields = new SkipBackwardPayload() {
                DeltaPositionMilliseconds = deltaPositionMilliseconds,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x09, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Seek
        /// </summary>
        public async Task<PlaybackResponse?> Seek(SecureSession session, ulong position, CancellationToken token = default) {
            SeekPayload requestFields = new SeekPayload() {
                Position = position,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0B, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new PlaybackResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Activate Audio Track
        /// </summary>
        public async Task<bool> ActivateAudioTrack(SecureSession session, string trackID, byte audioOutputIndex, CancellationToken token = default) {
            ActivateAudioTrackPayload requestFields = new ActivateAudioTrackPayload() {
                TrackID = trackID,
                AudioOutputIndex = audioOutputIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0C, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Activate Text Track
        /// </summary>
        public async Task<bool> ActivateTextTrack(SecureSession session, string trackID, CancellationToken token = default) {
            ActivateTextTrackPayload requestFields = new ActivateTextTrackPayload() {
                TrackID = trackID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0D, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Deactivate Text Track
        /// </summary>
        public async Task<bool> DeactivateTextTrack(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0E, null, token);
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
        /// Current State Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<PlaybackState> CurrentState { get; init; }

        /// <summary>
        /// Start Time Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<DateTime?> StartTime { get; init; }

        /// <summary>
        /// Duration Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong?> Duration { get; init; }

        /// <summary>
        /// Sampled Position Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<PlaybackPosition?> SampledPosition { get; init; }

        /// <summary>
        /// Playback Speed Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<float> PlaybackSpeed { get; init; }

        /// <summary>
        /// Seek Range End Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong?> SeekRangeEnd { get; init; }

        /// <summary>
        /// Seek Range Start Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong?> SeekRangeStart { get; init; }

        /// <summary>
        /// Active Audio Track Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Track?> ActiveAudioTrack { get; init; }

        /// <summary>
        /// Available Audio Tracks Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Track[]?> AvailableAudioTracks { get; init; }

        /// <summary>
        /// Active Text Track Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Track?> ActiveTextTrack { get; init; }

        /// <summary>
        /// Available Text Tracks Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Track[]?> AvailableTextTracks { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Media Playback";
        }
    }
}