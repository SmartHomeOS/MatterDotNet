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
    /// Channel Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class ChannelCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0504;

        /// <summary>
        /// Channel Cluster
        /// </summary>
        public ChannelCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ChannelCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Provides list of available channels.
            /// </summary>
            ChannelList = 1,
            /// <summary>
            /// Provides lineup info, which is a reference to an external source of lineup information.
            /// </summary>
            LineupInfo = 2,
            /// <summary>
            /// Provides electronic program guide information.
            /// </summary>
            ElectronicGuide = 4,
            /// <summary>
            /// Provides ability to record program.
            /// </summary>
            RecordProgram = 8,
        }

        /// <summary>
        /// Channel Type
        /// </summary>
        public enum ChannelTypeEnum {
            /// <summary>
            /// The channel is sourced from a satellite provider.
            /// </summary>
            Satellite = 0,
            /// <summary>
            /// The channel is sourced from a cable provider.
            /// </summary>
            Cable = 1,
            /// <summary>
            /// The channel is sourced from a terrestrial provider.
            /// </summary>
            Terrestrial = 2,
            /// <summary>
            /// The channel is sourced from an OTT provider.
            /// </summary>
            OTT = 3,
        }

        /// <summary>
        /// Lineup Info Type
        /// </summary>
        public enum LineupInfoTypeEnum {
            /// <summary>
            /// Multi System Operator
            /// </summary>
            MSO = 0,
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
            /// More than one equal match for the ChannelInfoStruct passed in.
            /// </summary>
            MultipleMatches = 1,
            /// <summary>
            /// No matches for the ChannelInfoStruct passed in.
            /// </summary>
            NoMatches = 2,
        }

        /// <summary>
        /// Recording Flag Bitmap
        /// </summary>
        [Flags]
        public enum RecordingFlagBitmap {
            /// <summary>
            /// The program is scheduled for recording.
            /// </summary>
            Scheduled = 1,
            /// <summary>
            /// The program series is scheduled for recording.
            /// </summary>
            RecordSeries = 2,
            /// <summary>
            /// The program is recorded and available to be played.
            /// </summary>
            Recorded = 4,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Channel Info
        /// </summary>
        public record ChannelInfo : TLVPayload {
            /// <summary>
            /// Channel Info
            /// </summary>
            public ChannelInfo() { }

            /// <summary>
            /// Channel Info
            /// </summary>
            [SetsRequiredMembers]
            public ChannelInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                MajorNumber = reader.GetUShort(0)!.Value;
                MinorNumber = reader.GetUShort(1)!.Value;
                Name = reader.GetString(2, true);
                CallSign = reader.GetString(3, true);
                AffiliateCallSign = reader.GetString(4, true);
                Identifier = reader.GetString(5, true);
                Type = (ChannelTypeEnum)reader.GetUShort(6, true)!.Value;
            }
            public required ushort MajorNumber { get; set; }
            public required ushort MinorNumber { get; set; }
            public string? Name { get; set; } = "";
            public string? CallSign { get; set; } = "";
            public string? AffiliateCallSign { get; set; } = "";
            public string? Identifier { get; set; } = "";
            public ChannelTypeEnum? Type { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, MajorNumber);
                writer.WriteUShort(1, MinorNumber);
                if (Name != null)
                    writer.WriteString(2, Name);
                if (CallSign != null)
                    writer.WriteString(3, CallSign);
                if (AffiliateCallSign != null)
                    writer.WriteString(4, AffiliateCallSign);
                if (Identifier != null)
                    writer.WriteString(5, Identifier);
                if (Type != null)
                    writer.WriteUShort(6, (ushort?)Type);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Channel Paging
        /// </summary>
        public record ChannelPaging : TLVPayload {
            /// <summary>
            /// Channel Paging
            /// </summary>
            public ChannelPaging() { }

            /// <summary>
            /// Channel Paging
            /// </summary>
            [SetsRequiredMembers]
            public ChannelPaging(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                PreviousToken = new PageToken((object[])fields[0]);
                NextToken = new PageToken((object[])fields[1]);
            }
            public PageToken? PreviousToken { get; set; } = null;
            public PageToken? NextToken { get; set; } = null;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (PreviousToken != null)
                    PreviousToken.Serialize(writer, 0);
                if (NextToken != null)
                    NextToken.Serialize(writer, 1);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Lineup Info
        /// </summary>
        public record LineupInfo : TLVPayload {
            /// <summary>
            /// Lineup Info
            /// </summary>
            public LineupInfo() { }

            /// <summary>
            /// Lineup Info
            /// </summary>
            [SetsRequiredMembers]
            public LineupInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                OperatorName = reader.GetString(0, false)!;
                LineupName = reader.GetString(1, true);
                PostalCode = reader.GetString(2, true);
                LineupInfoType = (LineupInfoTypeEnum)reader.GetUShort(3)!.Value;
            }
            public required string OperatorName { get; set; }
            public string? LineupName { get; set; } = "";
            public string? PostalCode { get; set; } = "";
            public required LineupInfoTypeEnum LineupInfoType { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, OperatorName);
                if (LineupName != null)
                    writer.WriteString(1, LineupName);
                if (PostalCode != null)
                    writer.WriteString(2, PostalCode);
                writer.WriteUShort(3, (ushort)LineupInfoType);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Page Token
        /// </summary>
        public record PageToken : TLVPayload {
            /// <summary>
            /// Page Token
            /// </summary>
            public PageToken() { }

            /// <summary>
            /// Page Token
            /// </summary>
            [SetsRequiredMembers]
            public PageToken(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Limit = reader.GetUShort(0, true);
                After = reader.GetString(1, true);
                Before = reader.GetString(2, true);
            }
            public ushort? Limit { get; set; } = 0;
            public string? After { get; set; } = "";
            public string? Before { get; set; } = "";
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (Limit != null)
                    writer.WriteUShort(0, Limit);
                if (After != null)
                    writer.WriteString(1, After, 8192);
                if (Before != null)
                    writer.WriteString(2, Before, 8192);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Program Cast
        /// </summary>
        public record ProgramCast : TLVPayload {
            /// <summary>
            /// Program Cast
            /// </summary>
            public ProgramCast() { }

            /// <summary>
            /// Program Cast
            /// </summary>
            [SetsRequiredMembers]
            public ProgramCast(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Name = reader.GetString(0, false)!;
                Role = reader.GetString(1, false)!;
            }
            public required string Name { get; set; }
            public required string Role { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Name, 256);
                writer.WriteString(1, Role, 256);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Program Category
        /// </summary>
        public record ProgramCategory : TLVPayload {
            /// <summary>
            /// Program Category
            /// </summary>
            public ProgramCategory() { }

            /// <summary>
            /// Program Category
            /// </summary>
            [SetsRequiredMembers]
            public ProgramCategory(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Category = reader.GetString(0, false)!;
                SubCategory = reader.GetString(1, true);
            }
            public required string Category { get; set; }
            public string? SubCategory { get; set; } = "";
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Category, 256);
                if (SubCategory != null)
                    writer.WriteString(1, SubCategory, 256);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Program
        /// </summary>
        public record Program : TLVPayload {
            /// <summary>
            /// Program
            /// </summary>
            public Program() { }

            /// <summary>
            /// Program
            /// </summary>
            [SetsRequiredMembers]
            public Program(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Identifier = reader.GetString(0, false)!;
                Channel = new ChannelInfo((object[])fields[1]);
                StartTime = TimeUtil.FromEpochSeconds(reader.GetUInt(2))!.Value;
                EndTime = TimeUtil.FromEpochSeconds(reader.GetUInt(3))!.Value;
                Title = reader.GetString(4, false)!;
                Subtitle = reader.GetString(5, true);
                Description = reader.GetString(6, true);
                {
                    AudioLanguages = new string[((object[])fields[7]).Length];
                    for (int i = 0; i < AudioLanguages.Length; i++) {
                        AudioLanguages[i] = reader.GetString(-1, false)!;
                    }
                }
                {
                    Ratings = new string[((object[])fields[8]).Length];
                    for (int i = 0; i < Ratings.Length; i++) {
                        Ratings[i] = reader.GetString(-1, false)!;
                    }
                }
                ThumbnailUrl = reader.GetString(9, true);
                PosterArtUrl = reader.GetString(10, true);
                DvbiUrl = reader.GetString(11, true);
                ReleaseDate = reader.GetString(12, true);
                ParentalGuidanceText = reader.GetString(13, true);
                RecordingFlag = (RecordingFlagBitmap)reader.GetUShort(14)!.Value;
                SeriesInfo = new SeriesInfo((object[])fields[15]);
                {
                    CategoryList = new ProgramCategory[((object[])fields[16]).Length];
                    for (int i = 0; i < CategoryList.Length; i++) {
                        CategoryList[i] = new ProgramCategory((object[])fields[-1]);
                    }
                }
                {
                    CastList = new ProgramCast[((object[])fields[17]).Length];
                    for (int i = 0; i < CastList.Length; i++) {
                        CastList[i] = new ProgramCast((object[])fields[-1]);
                    }
                }
                {
                    ExternalIDList = new ContentLauncherCluster.AdditionalInfo[((object[])fields[18]).Length];
                    for (int i = 0; i < ExternalIDList.Length; i++) {
                        ExternalIDList[i] = new ContentLauncherCluster.AdditionalInfo((object[])fields[-1]);
                    }
                }
            }
            public required string Identifier { get; set; }
            public required ChannelInfo Channel { get; set; }
            public required DateTime StartTime { get; set; }
            public required DateTime EndTime { get; set; }
            public required string Title { get; set; }
            public string? Subtitle { get; set; } = "";
            public string? Description { get; set; } = "";
            public string[]? AudioLanguages { get; set; } = Array.Empty<string>();
            public string[]? Ratings { get; set; } = Array.Empty<string>();
            public string? ThumbnailUrl { get; set; } = "";
            public string? PosterArtUrl { get; set; } = "";
            public string? DvbiUrl { get; set; } = "";
            public string? ReleaseDate { get; set; } = "";
            public string? ParentalGuidanceText { get; set; } = "";
            public required RecordingFlagBitmap RecordingFlag { get; set; }
            public SeriesInfo? SeriesInfo { get; set; } = null;
            public ProgramCategory[]? CategoryList { get; set; } = Array.Empty<ProgramCategory>();
            public ProgramCast[]? CastList { get; set; } = Array.Empty<ProgramCast>();
            public ContentLauncherCluster.AdditionalInfo[]? ExternalIDList { get; set; } = Array.Empty<ContentLauncherCluster.AdditionalInfo>();
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Identifier, 255);
                Channel.Serialize(writer, 1);
                writer.WriteUInt(2, TimeUtil.ToEpochSeconds(StartTime));
                writer.WriteUInt(3, TimeUtil.ToEpochSeconds(EndTime));
                writer.WriteString(4, Title, 255);
                if (Subtitle != null)
                    writer.WriteString(5, Subtitle, 255);
                if (Description != null)
                    writer.WriteString(6, Description, 8192);
                if (AudioLanguages != null)
                if (AudioLanguages != null)
                {
                    Constrain(AudioLanguages, 0, 10);
                    writer.StartArray(7);
                    foreach (var item in AudioLanguages) {
                        writer.WriteString(-1, item);
                    }
                    writer.EndContainer();
                }
                if (Ratings != null)
                if (Ratings != null)
                {
                    Constrain(Ratings, 0, 255);
                    writer.StartArray(8);
                    foreach (var item in Ratings) {
                        writer.WriteString(-1, item);
                    }
                    writer.EndContainer();
                }
                if (ThumbnailUrl != null)
                    writer.WriteString(9, ThumbnailUrl, 8192);
                if (PosterArtUrl != null)
                    writer.WriteString(10, PosterArtUrl, 8192);
                if (DvbiUrl != null)
                    writer.WriteString(11, DvbiUrl, 8192);
                if (ReleaseDate != null)
                    writer.WriteString(12, ReleaseDate, 30);
                if (ParentalGuidanceText != null)
                    writer.WriteString(13, ParentalGuidanceText, 255);
                writer.WriteUShort(14, (ushort)RecordingFlag);
                if (SeriesInfo != null)
                    SeriesInfo.Serialize(writer, 15);
                if (CategoryList != null)
                if (CategoryList != null)
                {
                    Constrain(CategoryList, 0, 255);
                    writer.StartArray(16);
                    foreach (var item in CategoryList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                if (CastList != null)
                if (CastList != null)
                {
                    Constrain(CastList, 0, 255);
                    writer.StartArray(17);
                    foreach (var item in CastList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                if (ExternalIDList != null)
                if (ExternalIDList != null)
                {
                    Constrain(ExternalIDList, 0, 255);
                    writer.StartArray(18);
                    foreach (var item in ExternalIDList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Series Info
        /// </summary>
        public record SeriesInfo : TLVPayload {
            /// <summary>
            /// Series Info
            /// </summary>
            public SeriesInfo() { }

            /// <summary>
            /// Series Info
            /// </summary>
            [SetsRequiredMembers]
            public SeriesInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Season = reader.GetString(0, false)!;
                Episode = reader.GetString(1, false)!;
            }
            public required string Season { get; set; }
            public required string Episode { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Season, 256);
                writer.WriteString(1, Episode, 256);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record ChangeChannelPayload : TLVPayload {
            public required string Match { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Match);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Change Channel Response - Reply from server
        /// </summary>
        public struct ChangeChannelResponse() {
            public required StatusEnum Status { get; set; }
            public string? Data { get; set; }
        }

        private record ChangeChannelByNumberPayload : TLVPayload {
            public required ushort MajorNumber { get; set; }
            public required ushort MinorNumber { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, MajorNumber);
                writer.WriteUShort(1, MinorNumber);
                writer.EndContainer();
            }
        }

        private record SkipChannelPayload : TLVPayload {
            public required short Count { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteShort(0, Count);
                writer.EndContainer();
            }
        }

        private record GetProgramGuidePayload : TLVPayload {
            public required DateTime StartTime { get; set; }
            public required DateTime EndTime { get; set; }
            public ChannelInfo[]? ChannelList { get; set; } = Array.Empty<ChannelInfo>();
            public PageToken? PageToken { get; set; } = null;
            public RecordingFlagBitmap? RecordingFlag { get; set; } = null;
            public ContentLauncherCluster.AdditionalInfo[]? ExternalIDList { get; set; } = Array.Empty<ContentLauncherCluster.AdditionalInfo>();
            public byte[]? Data { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, TimeUtil.ToEpochSeconds(StartTime));
                writer.WriteUInt(1, TimeUtil.ToEpochSeconds(EndTime));
                if (ChannelList != null)
                if (ChannelList != null)
                {
                    Constrain(ChannelList, 0, 255);
                    writer.StartArray(2);
                    foreach (var item in ChannelList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                if (PageToken != null)
                    PageToken.Serialize(writer, 3);
                if (RecordingFlag != null)
                    writer.WriteUShort(5, (ushort?)RecordingFlag);
                if (ExternalIDList != null)
                if (ExternalIDList != null)
                {
                    Constrain(ExternalIDList, 0, 255);
                    writer.StartArray(6);
                    foreach (var item in ExternalIDList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                if (Data != null)
                    writer.WriteBytes(7, Data, 8092);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Program Guide Response - Reply from server
        /// </summary>
        public struct ProgramGuideResponse() {
            public required ChannelPaging Paging { get; set; }
            public required Program[] ProgramList { get; set; } = Array.Empty<Program>();
        }

        private record RecordProgramPayload : TLVPayload {
            public required string ProgramIdentifier { get; set; }
            public required bool ShouldRecordSeries { get; set; }
            public ContentLauncherCluster.AdditionalInfo[]? ExternalIDList { get; set; } = Array.Empty<ContentLauncherCluster.AdditionalInfo>();
            public byte[]? Data { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, ProgramIdentifier, 255);
                writer.WriteBool(1, ShouldRecordSeries);
                if (ExternalIDList != null)
                if (ExternalIDList != null)
                {
                    Constrain(ExternalIDList, 0, 255);
                    writer.StartArray(2);
                    foreach (var item in ExternalIDList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                if (Data != null)
                    writer.WriteBytes(3, Data, 8092);
                writer.EndContainer();
            }
        }

        private record CancelRecordProgramPayload : TLVPayload {
            public required string ProgramIdentifier { get; set; }
            public required bool ShouldRecordSeries { get; set; }
            public ContentLauncherCluster.AdditionalInfo[]? ExternalIDList { get; set; } = Array.Empty<ContentLauncherCluster.AdditionalInfo>();
            public byte[]? Data { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, ProgramIdentifier, 255);
                writer.WriteBool(1, ShouldRecordSeries);
                if (ExternalIDList != null)
                if (ExternalIDList != null)
                {
                    Constrain(ExternalIDList, 0, 255);
                    writer.StartArray(2);
                    foreach (var item in ExternalIDList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                if (Data != null)
                    writer.WriteBytes(3, Data, 8092);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Change Channel
        /// </summary>
        public async Task<ChangeChannelResponse?> ChangeChannel(SecureSession session, string Match) {
            ChangeChannelPayload requestFields = new ChangeChannelPayload() {
                Match = Match,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ChangeChannelResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Change Channel By Number
        /// </summary>
        public async Task<bool> ChangeChannelByNumber(SecureSession session, ushort MajorNumber, ushort MinorNumber) {
            ChangeChannelByNumberPayload requestFields = new ChangeChannelByNumberPayload() {
                MajorNumber = MajorNumber,
                MinorNumber = MinorNumber,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Skip Channel
        /// </summary>
        public async Task<bool> SkipChannel(SecureSession session, short Count) {
            SkipChannelPayload requestFields = new SkipChannelPayload() {
                Count = Count,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Program Guide
        /// </summary>
        public async Task<ProgramGuideResponse?> GetProgramGuide(SecureSession session, DateTime StartTime, DateTime EndTime, ChannelInfo[]? ChannelList, PageToken? PageToken, RecordingFlagBitmap? RecordingFlag, ContentLauncherCluster.AdditionalInfo[]? ExternalIDList, byte[]? Data) {
            GetProgramGuidePayload requestFields = new GetProgramGuidePayload() {
                StartTime = StartTime,
                EndTime = EndTime,
                ChannelList = ChannelList,
                PageToken = PageToken,
                RecordingFlag = RecordingFlag,
                ExternalIDList = ExternalIDList,
                Data = Data,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ProgramGuideResponse() {
                Paging = (ChannelPaging)GetField(resp, 0),
                ProgramList = (Program[])GetField(resp, 1),
            };
        }

        /// <summary>
        /// Record Program
        /// </summary>
        public async Task<bool> RecordProgram(SecureSession session, string ProgramIdentifier, bool ShouldRecordSeries, ContentLauncherCluster.AdditionalInfo[]? ExternalIDList, byte[]? Data) {
            RecordProgramPayload requestFields = new RecordProgramPayload() {
                ProgramIdentifier = ProgramIdentifier,
                ShouldRecordSeries = ShouldRecordSeries,
                ExternalIDList = ExternalIDList,
                Data = Data,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Cancel Record Program
        /// </summary>
        public async Task<bool> CancelRecordProgram(SecureSession session, string ProgramIdentifier, bool ShouldRecordSeries, ContentLauncherCluster.AdditionalInfo[]? ExternalIDList, byte[]? Data) {
            CancelRecordProgramPayload requestFields = new CancelRecordProgramPayload() {
                ProgramIdentifier = ProgramIdentifier,
                ShouldRecordSeries = ShouldRecordSeries,
                ExternalIDList = ExternalIDList,
                Data = Data,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07, requestFields);
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
        /// Get the Channel List attribute
        /// </summary>
        public async Task<ChannelInfo[]> GetChannelList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            ChannelInfo[] list = new ChannelInfo[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new ChannelInfo(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Lineup attribute
        /// </summary>
        public async Task<LineupInfo?> GetLineup(SecureSession session) {
            return new LineupInfo((object[])(await GetAttribute(session, 1))!) ?? null;
        }

        /// <summary>
        /// Get the Current Channel attribute
        /// </summary>
        public async Task<ChannelInfo?> GetCurrentChannel(SecureSession session) {
            return new ChannelInfo((object[])(await GetAttribute(session, 2))!) ?? null;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Channel Cluster";
        }
    }
}