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
    /// Content Launcher Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class ContentLauncherCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x050A;

        /// <summary>
        /// Content Launcher Cluster
        /// </summary>
        public ContentLauncherCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ContentLauncherCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Device supports content search (non-app specific)
            /// </summary>
            ContentSearch = 1,
            /// <summary>
            /// Device supports basic URL-based file playback
            /// </summary>
            URLPlayback = 2,
            /// <summary>
            /// Enables clients to implement more advanced media seeking behavior in their user interface, such as for example a &quot;seek bar&quot;.
            /// </summary>
            AdvancedSeek = 4,
            /// <summary>
            /// Device or app supports Text Tracks.
            /// </summary>
            TextTracks = 8,
            /// <summary>
            /// Device or app supports Audio Tracks.
            /// </summary>
            AudioTracks = 16,
        }

        /// <summary>
        /// Metric Type
        /// </summary>
        public enum MetricTypeEnum {
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
        public enum ParameterEnum {
            /// <summary>
            /// Actor represents an actor credited in video media content; for example, “Gaby Hoffman”
            /// </summary>
            Actor = 0,
            /// <summary>
            /// Channel represents the identifying data for a television channel; for example, &quot;PBS&quot;
            /// </summary>
            Channel = 1,
            /// <summary>
            /// A character represented in video media content; for example, “Snow White”
            /// </summary>
            Character = 2,
            /// <summary>
            /// A director of the video media content; for example, “Spike Lee”
            /// </summary>
            Director = 3,
            /// <summary>
            /// An event is a reference to a type of event; examples would include sports, music, or other types of events. For example, searching for &quot;Football games&quot; would search for a 'game' event entity and a 'football' sport entity.
            /// </summary>
            Event = 4,
            /// <summary>
            /// A franchise is a video entity which can represent a number of video entities, like movies or TV shows. For example, take the fictional franchise &quot;Intergalactic Wars&quot; which represents a collection of movie trilogies, as well as animated and live action TV shows. This entity type was introduced to account for requests by customers such as &quot;Find Intergalactic Wars movies&quot;, which would search for all 'Intergalactic Wars' programs of the MOVIE MediaType, rather than attempting to match to a single title.
            /// </summary>
            Franchise = 5,
            /// <summary>
            /// Genre represents the genre of video media content such as action, drama or comedy.
            /// </summary>
            Genre = 6,
            /// <summary>
            /// League represents the categorical information for a sporting league; for example, &quot;NCAA&quot;
            /// </summary>
            League = 7,
            /// <summary>
            /// Popularity indicates whether the user asks for popular content.
            /// </summary>
            Popularity = 8,
            /// <summary>
            /// The provider (MSP) the user wants this media to be played on; for example, &quot;Netflix&quot;.
            /// </summary>
            Provider = 9,
            /// <summary>
            /// Sport represents the categorical information of a sport; for example, football
            /// </summary>
            Sport = 10,
            /// <summary>
            /// SportsTeam represents the categorical information of a professional sports team; for example, &quot;University of Washington Huskies&quot;
            /// </summary>
            SportsTeam = 11,
            /// <summary>
            /// The type of content requested. Supported types are &quot;Movie&quot;, &quot;MovieSeries&quot;, &quot;TVSeries&quot;, &quot;TVSeason&quot;, &quot;TVEpisode&quot;, &quot;Trailer&quot;, &quot;SportsEvent&quot;, &quot;LiveEvent&quot;, and &quot;Video&quot;
            /// </summary>
            Type = 12,
            /// <summary>
            /// Video represents the identifying data for a specific piece of video content; for example, &quot;Manchester by the Sea&quot;.
            /// </summary>
            Video = 13,
            /// <summary>
            /// Season represents the specific season number within a TV series.
            /// </summary>
            Season = 14,
            /// <summary>
            /// Episode represents a specific episode number within a Season in a TV series.
            /// </summary>
            Episode = 15,
            /// <summary>
            /// Represents a search text input across many parameter types or even outside of the defined param types.
            /// </summary>
            Any = 16,
        }

        /// <summary>
        /// Status
        /// </summary>
        public enum StatusEnum {
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
        /// Supported Protocols Bitmap
        /// </summary>
        [Flags]
        public enum SupportedProtocolsBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Device supports Dynamic Adaptive Streaming over HTTP (DASH)
            /// </summary>
            DASH = 1,
            /// <summary>
            /// Device supports HTTP Live Streaming (HLS)
            /// </summary>
            HLS = 2,
        }
        #endregion Enums

        #region Records
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
                Name = reader.GetString(0, false)!;
                Value = reader.GetString(1, false)!;
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
                ProviderName = reader.GetString(0, false)!;
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
                    ParameterList = new Parameter[((object[])fields[0]).Length];
                    for (int i = 0; i < ParameterList.Length; i++) {
                        ParameterList[i] = new Parameter((object[])fields[-1]);
                    }
                }
            }
            public required Parameter[] ParameterList { get; set; } = Array.Empty<Parameter>();
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
                Metric = (MetricTypeEnum)reader.GetUShort(2)!.Value;
            }
            public required double Width { get; set; }
            public required double Height { get; set; }
            public required MetricTypeEnum Metric { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteDouble(0, Width);
                writer.WriteDouble(1, Height);
                writer.WriteUShort(2, (ushort)Metric);
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
                Value = reader.GetString(1, false)!;
                {
                    ExternalIDList = new AdditionalInfo[((object[])fields[2]).Length];
                    for (int i = 0; i < ExternalIDList.Length; i++) {
                        ExternalIDList[i] = new AdditionalInfo((object[])fields[-1]);
                    }
                }
            }
            public required ParameterEnum Type { get; set; }
            public required string Value { get; set; }
            public AdditionalInfo[]? ExternalIDList { get; set; } = Array.Empty<AdditionalInfo>();
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Type);
                writer.WriteString(1, Value, 1024);
                if (ExternalIDList != null)
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
                PlaybackPosition = reader.GetULong(0, true);
                TextTrack = new TrackPreference((object[])fields[1]);
                {
                    AudioTracks = new TrackPreference[((object[])fields[2]).Length];
                    for (int i = 0; i < AudioTracks.Length; i++) {
                        AudioTracks[i] = new TrackPreference((object[])fields[-1]);
                    }
                }
            }
            public required ulong? PlaybackPosition { get; set; }
            public required TrackPreference? TextTrack { get; set; }
            public required TrackPreference[]? AudioTracks { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (PlaybackPosition != null)
                    writer.WriteULong(0, PlaybackPosition);
                if (TextTrack == null)
                    writer.WriteNull(1);
                else
                    TextTrack.Serialize(writer, 1);
                if (AudioTracks != null)
                {
                    writer.StartArray(2);
                    foreach (var item in AudioTracks) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                else
                    writer.WriteNull(2);
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
                ImageURL = reader.GetString(0, true);
                Color = reader.GetString(1, true);
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
                    writer.WriteString(1, Color, 7);
                if (Size != null)
                    Size.Serialize(writer, 2);
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
                LanguageCode = reader.GetString(0, false)!;
                {
                    Characteristics = new MediaPlaybackCluster.CharacteristicEnum[((object[])fields[1]).Length];
                    for (int i = 0; i < Characteristics.Length; i++) {
                        Characteristics[i] = (MediaPlaybackCluster.CharacteristicEnum)reader.GetUShort(-1)!.Value;
                    }
                }
                AudioOutputIndex = reader.GetByte(2, true);
            }
            public required string LanguageCode { get; set; }
            public MediaPlaybackCluster.CharacteristicEnum[]? Characteristics { get; set; } = null;
            public required byte? AudioOutputIndex { get; set; }
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
                if (AudioOutputIndex != null)
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
            public bool? UseCurrentContext { get; set; } = true;
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
            public PlaybackPreferences? PlaybackPreferences { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, ContentURL);
                if (DisplayString != null)
                    writer.WriteString(1, DisplayString);
                if (BrandingInformation != null)
                    BrandingInformation.Serialize(writer, 2);
                if (PlaybackPreferences != null)
                    PlaybackPreferences.Serialize(writer, 3);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Launcher Response - Reply from server
        /// </summary>
        public struct LauncherResponse() {
            public required StatusEnum Status { get; set; }
            public string? Data { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Launch Content
        /// </summary>
        public async Task<LauncherResponse?> LaunchContent(SecureSession session, ContentSearch Search, bool AutoPlay, string? Data, PlaybackPreferences? PlaybackPreferences, bool? UseCurrentContext) {
            LaunchContentPayload requestFields = new LaunchContentPayload() {
                Search = Search,
                AutoPlay = AutoPlay,
                Data = Data,
                PlaybackPreferences = PlaybackPreferences,
                UseCurrentContext = UseCurrentContext,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new LauncherResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Launch URL
        /// </summary>
        public async Task<LauncherResponse?> LaunchURL(SecureSession session, string ContentURL, string? DisplayString, BrandingInformation? BrandingInformation, PlaybackPreferences? PlaybackPreferences) {
            LaunchURLPayload requestFields = new LaunchURLPayload() {
                ContentURL = ContentURL,
                DisplayString = DisplayString,
                BrandingInformation = BrandingInformation,
                PlaybackPreferences = PlaybackPreferences,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new LauncherResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
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
        /// Get the Accept Header attribute
        /// </summary>
        public async Task<string[]> GetAcceptHeader(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            string[] list = new string[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetString(i, false)!;
            return list;
        }

        /// <summary>
        /// Get the Supported Streaming Protocols attribute
        /// </summary>
        public async Task<SupportedProtocolsBitmap> GetSupportedStreamingProtocols(SecureSession session) {
            return (SupportedProtocolsBitmap)await GetEnumAttribute(session, 1);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Content Launcher Cluster";
        }
    }
}