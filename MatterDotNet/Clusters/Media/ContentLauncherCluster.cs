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

namespace MatterDotNet.Clusters.Media
{
    /// <summary>
    /// This cluster provides an interface for launching content on a media player device such as a TV or Speaker.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ContentLauncher : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x050a;

        /// <summary>
        /// This cluster provides an interface for launching content on a media player device such as a TV or Speaker.
        /// </summary>
        [SetsRequiredMembers]
        public ContentLauncher(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ContentLauncher(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            AcceptHeader = new ReadAttribute<string[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    string[] list = new string[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = reader.GetString(i, false, 254)!;
                    return list;
                }
            };
            SupportedStreamingProtocols = new ReadAttribute<SupportedProtocols>(cluster, endPoint, 1) {
                Deserialize = x => (SupportedProtocols)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Metric Type
        /// </summary>
        public enum MetricType : byte {
            /// <summary>
            /// Dimensions defined in a number of Pixels
            /// </summary>
            Pixels = 0,
            /// <summary>
            /// Dimensions defined as a percentage
            /// </summary>
            Percentage = 1,
        }

        /// <summary>
        /// Parameter
        /// </summary>
        public enum ParameterEnum : byte {
            /// <summary>
            /// Actor represents an actor credited in video media content; for example, “Gaby Hoffman”
            /// </summary>
            Actor = 0x0,
            /// <summary>
            /// Channel represents the identifying data for a television channel; for example, "PBS"
            /// </summary>
            Channel = 0x1,
            /// <summary>
            /// A character represented in video media content; for example, “Snow White”
            /// </summary>
            Character = 0x2,
            /// <summary>
            /// A director of the video media content; for example, “Spike Lee”
            /// </summary>
            Director = 0x3,
            /// <summary>
            /// An event is a reference to a type of event; examples would include sports, music, or other types of events. For example, searching for "Football games" would search for a 'game' event entity and a 'football' sport entity.
            /// </summary>
            Event = 0x4,
            /// <summary>
            /// A franchise is a video entity which can represent a number of video entities, like movies or TV shows. For example, take the fictional franchise "Intergalactic Wars" which represents a collection of movie trilogies, as well as animated and live action TV shows. This entity type was introduced to account for requests by customers such as "Find Intergalactic Wars movies", which would search for all 'Intergalactic Wars' programs of the MOVIE MediaType, rather than attempting to match to a single title.
            /// </summary>
            Franchise = 0x5,
            /// <summary>
            /// Genre represents the genre of video media content such as action, drama or comedy.
            /// </summary>
            Genre = 0x6,
            /// <summary>
            /// League represents the categorical information for a sporting league; for example, "NCAA"
            /// </summary>
            League = 0x7,
            /// <summary>
            /// Popularity indicates whether the user asks for popular content.
            /// </summary>
            Popularity = 0x8,
            /// <summary>
            /// The provider (MSP) the user wants this media to be played on; for example, "Netflix".
            /// </summary>
            Provider = 0x9,
            /// <summary>
            /// Sport represents the categorical information of a sport; for example, football
            /// </summary>
            Sport = 0xA,
            /// <summary>
            /// SportsTeam represents the categorical information of a professional sports team; for example, "University of Washington Huskies"
            /// </summary>
            SportsTeam = 0xB,
            /// <summary>
            /// The type of content requested. Supported types are "Movie", "MovieSeries", "TVSeries", "TVSeason", "TVEpisode", "Trailer", "SportsEvent", "LiveEvent", and "Video"
            /// </summary>
            Type = 0xC,
            /// <summary>
            /// Video represents the identifying data for a specific piece of video content; for example, "Manchester by the Sea".
            /// </summary>
            Video = 0xD,
            /// <summary>
            /// Season represents the specific season number within a TV series.
            /// </summary>
            Season = 0xE,
            /// <summary>
            /// Episode represents a specific episode number within a Season in a TV series.
            /// </summary>
            Episode = 0xF,
            /// <summary>
            /// Represents a search text input across many parameter types or even outside of the defined param types.
            /// </summary>
            Any = 0x10,
        }

        /// <summary>
        /// Status
        /// </summary>
        public enum Status : byte {
            /// <summary>
            /// Command succeeded
            /// </summary>
            Success = 0,
            /// <summary>
            /// Requested URL could not be reached by device.
            /// </summary>
            URLNotAvailable = 1,
            /// <summary>
            /// Requested URL returned 401 error code.
            /// </summary>
            AuthFailed = 2,
            /// <summary>
            /// Requested Text Track (in PlaybackPreferences) not available
            /// </summary>
            TextTrackNotAvailable = 3,
            /// <summary>
            /// Requested Audio Track (in PlaybackPreferences) not available
            /// </summary>
            AudioTrackNotAvailable = 4,
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

        /// <summary>
        /// Supported Protocols
        /// </summary>
        [Flags]
        public enum SupportedProtocols : uint {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Device supports Dynamic Adaptive Streaming over HTTP (DASH)
            /// </summary>
            DASH = 0x0001,
            /// <summary>
            /// Device supports HTTP Live Streaming (HLS)
            /// </summary>
            HLS = 0x0002,
        }

        /// <summary>
        /// Feature
        /// </summary>
        [Flags]
        public enum Feature : uint {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            ContentSearch = 0x0001,
            URLPlayback = 0x0002,
            AdvancedSeek = 0x0004,
            TextTracks = 0x0008,
            AudioTracks = 0x0010,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Content Search
        /// </summary>
        public record ContentSearch : TLVPayload {
            /// <summary>
            /// Content Search
            /// </summary>
            public ContentSearch() { }

            /// <summary>
            /// Content Search
            /// </summary>
            [SetsRequiredMembers]
            public ContentSearch(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                {
                    ParameterList = new Parameter[reader.GetStruct(0)!.Length];
                    for (int n = 0; n < ParameterList.Length; n++) {
                        ParameterList[n] = new Parameter((object[])((object[])fields[0])[n]);
                    }
                }
            }
            public required Parameter[] ParameterList { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartArray(0);
                    foreach (var item in ParameterList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Additional Info
        /// </summary>
        public record AdditionalInfo : TLVPayload {
            /// <summary>
            /// Additional Info
            /// </summary>
            public AdditionalInfo() { }

            /// <summary>
            /// Additional Info
            /// </summary>
            [SetsRequiredMembers]
            public AdditionalInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Name = reader.GetString(0, false, 256)!;
                Value = reader.GetString(1, false, 8192)!;
            }
            public required string Name { get; set; }
            public required string Value { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Name, 256);
                writer.WriteString(1, Value, 8192);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Dimension
        /// </summary>
        public record Dimension : TLVPayload {
            /// <summary>
            /// Dimension
            /// </summary>
            public Dimension() { }

            /// <summary>
            /// Dimension
            /// </summary>
            [SetsRequiredMembers]
            public Dimension(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Width = reader.GetDouble(0)!.Value;
                Height = reader.GetDouble(1)!.Value;
                Metric = (MetricType)reader.GetUShort(2)!.Value;
            }
            public required double Width { get; set; }
            public required double Height { get; set; }
            public required MetricType Metric { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteDouble(0, Width);
                writer.WriteDouble(1, Height);
                writer.WriteUShort(2, (ushort)Metric);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Style Information
        /// </summary>
        public record StyleInformation : TLVPayload {
            /// <summary>
            /// Style Information
            /// </summary>
            public StyleInformation() { }

            /// <summary>
            /// Style Information
            /// </summary>
            [SetsRequiredMembers]
            public StyleInformation(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ImageURL = reader.GetString(0, true, 8192);
                Color = reader.GetString(1, true, 9);
                Size = new Dimension((object[])fields[2]);
            }
            public string? ImageURL { get; set; }
            public string? Color { get; set; }
            public Dimension? Size { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (ImageURL != null)
                    writer.WriteString(0, ImageURL, 8192);
                if (Color != null)
                    writer.WriteString(1, Color, 9);
                if (Size != null)
                    Size.Serialize(writer, 2);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Branding Information
        /// </summary>
        public record BrandingInformation : TLVPayload {
            /// <summary>
            /// Branding Information
            /// </summary>
            public BrandingInformation() { }

            /// <summary>
            /// Branding Information
            /// </summary>
            [SetsRequiredMembers]
            public BrandingInformation(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ProviderName = reader.GetString(0, false, 256)!;
                Background = new StyleInformation((object[])fields[1]);
                Logo = new StyleInformation((object[])fields[2]);
                ProgressBar = new StyleInformation((object[])fields[3]);
                Splash = new StyleInformation((object[])fields[4]);
                WaterMark = new StyleInformation((object[])fields[5]);
            }
            public required string ProviderName { get; set; }
            public StyleInformation? Background { get; set; }
            public StyleInformation? Logo { get; set; }
            public StyleInformation? ProgressBar { get; set; }
            public StyleInformation? Splash { get; set; }
            public StyleInformation? WaterMark { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, ProviderName, 256);
                if (Background != null)
                    Background.Serialize(writer, 1);
                if (Logo != null)
                    Logo.Serialize(writer, 2);
                if (ProgressBar != null)
                    ProgressBar.Serialize(writer, 3);
                if (Splash != null)
                    Splash.Serialize(writer, 4);
                if (WaterMark != null)
                    WaterMark.Serialize(writer, 5);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Parameter
        /// </summary>
        public record Parameter : TLVPayload {
            /// <summary>
            /// Parameter
            /// </summary>
            public Parameter() { }

            /// <summary>
            /// Parameter
            /// </summary>
            [SetsRequiredMembers]
            public Parameter(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Type = (ParameterEnum)reader.GetUShort(0)!.Value;
                Value = reader.GetString(1, false, 1024)!;
                {
                    ExternalIDList = new AdditionalInfo[reader.GetStruct(2)!.Length];
                    for (int n = 0; n < ExternalIDList.Length; n++) {
                        ExternalIDList[n] = new AdditionalInfo((object[])((object[])fields[2])[n]);
                    }
                }
            }
            public required ParameterEnum Type { get; set; }
            public required string Value { get; set; }
            public AdditionalInfo[]? ExternalIDList { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Type);
                writer.WriteString(1, Value, 1024);
                if (ExternalIDList != null)
                {
                    writer.StartArray(2);
                    foreach (var item in ExternalIDList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Playback Preferences
        /// </summary>
        public record PlaybackPreferences : TLVPayload {
            /// <summary>
            /// Playback Preferences
            /// </summary>
            public PlaybackPreferences() { }

            /// <summary>
            /// Playback Preferences
            /// </summary>
            [SetsRequiredMembers]
            public PlaybackPreferences(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                PlaybackPosition = reader.GetULong(0)!.Value;
                TextTrack = new TrackPreference((object[])fields[1]);
                {
                    AudioTracks = new TrackPreference[reader.GetStruct(2)!.Length];
                    for (int n = 0; n < AudioTracks.Length; n++) {
                        AudioTracks[n] = new TrackPreference((object[])((object[])fields[2])[n]);
                    }
                }
            }
            public required ulong PlaybackPosition { get; set; }
            public required TrackPreference TextTrack { get; set; }
            public TrackPreference[]? AudioTracks { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, PlaybackPosition);
                TextTrack.Serialize(writer, 1);
                if (AudioTracks != null)
                {
                    writer.StartArray(2);
                    foreach (var item in AudioTracks) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Track Preference
        /// </summary>
        public record TrackPreference : TLVPayload {
            /// <summary>
            /// Track Preference
            /// </summary>
            public TrackPreference() { }

            /// <summary>
            /// Track Preference
            /// </summary>
            [SetsRequiredMembers]
            public TrackPreference(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                LanguageCode = reader.GetString(0, false, 32)!;
                {
                    Characteristics = new Characteristic[reader.GetStruct(1)!.Length];
                    for (int n = 0; n < Characteristics.Length; n++) {
                        Characteristics[n] = (Characteristic)reader.GetUShort(n)!.Value;
                    }
                }
                AudioOutputIndex = reader.GetByte(2)!.Value;
            }
            public required string LanguageCode { get; set; }
            public Characteristic[]? Characteristics { get; set; }
            public required byte AudioOutputIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, LanguageCode, 32);
                if (Characteristics != null)
                {
                    writer.StartArray(1);
                    foreach (var item in Characteristics) {
                        writer.WriteUShort(-1, (ushort)item);
                    }
                    writer.EndContainer();
                }
                writer.WriteByte(2, AudioOutputIndex);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record LaunchContentPayload : TLVPayload {
            public required ContentSearch Search { get; set; }
            public required bool AutoPlay { get; set; }
            public string? Data { get; set; }
            public PlaybackPreferences? PlaybackPreferences { get; set; }
            public bool? UseCurrentContext { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                Search.Serialize(writer, 0);
                writer.WriteBool(1, AutoPlay);
                if (Data != null)
                    writer.WriteString(2, Data);
                if (PlaybackPreferences != null)
                    PlaybackPreferences.Serialize(writer, 3);
                if (UseCurrentContext != null)
                    writer.WriteBool(4, UseCurrentContext);
                writer.EndContainer();
            }
        }

        private record LaunchURLPayload : TLVPayload {
            public required string ContentURL { get; set; }
            public string? DisplayString { get; set; }
            public BrandingInformation? BrandingInformation { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, ContentURL);
                if (DisplayString != null)
                    writer.WriteString(1, DisplayString);
                if (BrandingInformation != null)
                    BrandingInformation.Serialize(writer, 2);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Launcher Response - Reply from server
        /// </summary>
        public struct LauncherResponse() {
            public required Status Status { get; set; }
            public string? Data { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Launch Content
        /// </summary>
        public async Task<LauncherResponse?> LaunchContent(SecureSession session, ContentSearch search, bool autoPlay, string? data, PlaybackPreferences? playbackPreferences, bool? useCurrentContext, CancellationToken token = default) {
            LaunchContentPayload requestFields = new LaunchContentPayload() {
                Search = search,
                AutoPlay = autoPlay,
                Data = data,
                PlaybackPreferences = playbackPreferences,
                UseCurrentContext = useCurrentContext,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new LauncherResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Launch URL
        /// </summary>
        public async Task<LauncherResponse?> LaunchURL(SecureSession session, string contentURL, string? displayString, BrandingInformation? brandingInformation, CancellationToken token = default) {
            LaunchURLPayload requestFields = new LaunchURLPayload() {
                ContentURL = contentURL,
                DisplayString = displayString,
                BrandingInformation = brandingInformation,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new LauncherResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Accept Header Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string[]> AcceptHeader { get; init; }

        /// <summary>
        /// Supported Streaming Protocols Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<SupportedProtocols> SupportedStreamingProtocols { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Content Launcher";
        }
    }
}