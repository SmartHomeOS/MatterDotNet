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

namespace MatterDotNet.Clusters.Media
{
    /// <summary>
    /// This cluster provides an interface for controlling the current Channel on a device.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class Channel : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0504;

        /// <summary>
        /// This cluster provides an interface for controlling the current Channel on a device.
        /// </summary>
        [SetsRequiredMembers]
        public Channel(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected Channel(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            ChannelList = new ReadAttribute<ChannelInfo[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ChannelInfo[] list = new ChannelInfo[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new ChannelInfo(reader.GetStruct(i)!);
                    return list;
                }
            };
            Lineup = new ReadAttribute<LineupInfo?>(cluster, endPoint, 1, true) {
                Deserialize = x => new LineupInfo((object[])x!)

            };
            CurrentChannel = new ReadAttribute<ChannelInfo?>(cluster, endPoint, 2, true) {
                Deserialize = x => new ChannelInfo((object[])x!)

            };
        }

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
        /// Lineup Info Type
        /// </summary>
        public enum LineupInfoType : byte {
            /// <summary>
            /// Multi System Operator
            /// </summary>
            MSO = 0,
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
            /// More than one equal match for the ChannelInfoStruct passed in.
            /// </summary>
            MultipleMatches = 1,
            /// <summary>
            /// No matches for the ChannelInfoStruct passed in.
            /// </summary>
            NoMatches = 2,
        }

        /// <summary>
        /// Channel Type
        /// </summary>
        public enum ChannelType : byte {
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
        /// Recording Flag
        /// </summary>
        [Flags]
        public enum RecordingFlag : uint {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Scheduled = 0x0001,
            RecordSeries = 0x0002,
            Recorded = 0x0004,
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
                Type = (ChannelType?)reader.GetUShort(6, true);
            }
            public required ushort MajorNumber { get; set; }
            public required ushort MinorNumber { get; set; }
            public string? Name { get; set; }
            public string? CallSign { get; set; }
            public string? AffiliateCallSign { get; set; }
            public string? Identifier { get; set; }
            public ChannelType? Type { get; set; }
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
                    writer.WriteUShort(6, (ushort)Type);
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
                LineupInfoType = (LineupInfoType)reader.GetUShort(3)!.Value;
            }
            public required string OperatorName { get; set; }
            public string? LineupName { get; set; }
            public string? PostalCode { get; set; }
            public required LineupInfoType LineupInfoType { get; set; }
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
                Identifier = reader.GetString(0, false, 255)!;
                Channel = new ChannelInfo((object[])fields[1]);
                StartTime = TimeUtil.FromEpochSeconds(reader.GetUInt(2))!.Value;
                EndTime = TimeUtil.FromEpochSeconds(reader.GetUInt(3))!.Value;
                Title = reader.GetString(4, false, 255)!;
                Subtitle = reader.GetString(5, true, 255);
                Description = reader.GetString(6, true, 8192);
                {
                    AudioLanguages = new string[reader.GetStruct(7)!.Length];
                    for (int n = 0; n < AudioLanguages.Length; n++) {
                        AudioLanguages[n] = reader.GetString(n, false)!;
                    }
                }
                {
                    Ratings = new string[reader.GetStruct(8)!.Length];
                    for (int n = 0; n < Ratings.Length; n++) {
                        Ratings[n] = reader.GetString(n, false)!;
                    }
                }
                ThumbnailUrl = reader.GetString(9, true, 8192);
                PosterArtUrl = reader.GetString(10, true, 8192);
                DvbiUrl = reader.GetString(11, true, 8192);
                ReleaseDate = reader.GetString(12, true, 30);
                ParentalGuidanceText = reader.GetString(13, true, 255);
                RecordingFlag = (RecordingFlag?)reader.GetUInt(14, true);
                SeriesInfo = new SeriesInfo((object[])fields[15]);
                {
                    CategoryList = new ProgramCategory[reader.GetStruct(16)!.Length];
                    for (int n = 0; n < CategoryList.Length; n++) {
                        CategoryList[n] = new ProgramCategory((object[])((object[])fields[16])[n]);
                    }
                }
                {
                    CastList = new ProgramCast[reader.GetStruct(17)!.Length];
                    for (int n = 0; n < CastList.Length; n++) {
                        CastList[n] = new ProgramCast((object[])((object[])fields[17])[n]);
                    }
                }
                {
                    ExternalIDList = new ProgramCast[reader.GetStruct(18)!.Length];
                    for (int n = 0; n < ExternalIDList.Length; n++) {
                        ExternalIDList[n] = new ProgramCast((object[])((object[])fields[18])[n]);
                    }
                }
            }
            public required string Identifier { get; set; }
            public required ChannelInfo Channel { get; set; }
            public required DateTime StartTime { get; set; }
            public required DateTime EndTime { get; set; }
            public required string Title { get; set; }
            public string? Subtitle { get; set; }
            public string? Description { get; set; }
            public string[]? AudioLanguages { get; set; }
            public string[]? Ratings { get; set; }
            public string? ThumbnailUrl { get; set; }
            public string? PosterArtUrl { get; set; }
            public string? DvbiUrl { get; set; }
            public string? ReleaseDate { get; set; }
            public string? ParentalGuidanceText { get; set; }
            public RecordingFlag? RecordingFlag { get; set; }
            public SeriesInfo? SeriesInfo { get; set; }
            public ProgramCategory[]? CategoryList { get; set; }
            public ProgramCast[]? CastList { get; set; }
            public ProgramCast[]? ExternalIDList { get; set; }
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
                {
                    Constrain(AudioLanguages, 0, 10);
                    writer.StartArray(7);
                    foreach (var item in AudioLanguages) {
                        writer.WriteString(-1, item);
                    }
                    writer.EndContainer();
                }
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
                if (RecordingFlag != null)
                    writer.WriteUInt(14, (uint)RecordingFlag);
                if (SeriesInfo != null)
                    SeriesInfo.Serialize(writer, 15);
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
                {
                    Constrain(CastList, 0, 255);
                    writer.StartArray(17);
                    foreach (var item in CastList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
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
                Season = reader.GetString(0, false, 256)!;
                Episode = reader.GetString(1, false, 256)!;
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
                Category = reader.GetString(0, false, 256)!;
                SubCategory = reader.GetString(1, true, 256);
            }
            public required string Category { get; set; }
            public string? SubCategory { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Category, 256);
                if (SubCategory != null)
                    writer.WriteString(1, SubCategory, 256);
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
                Name = reader.GetString(0, false, 256)!;
                Role = reader.GetString(1, false, 256)!;
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
                After = reader.GetString(1, true, 8192);
                Before = reader.GetString(2, true, 8192);
            }
            public ushort? Limit { get; set; } = 0;
            public string? After { get; set; }
            public string? Before { get; set; }
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
            public PageToken? PreviousToken { get; set; }
            public PageToken? NextToken { get; set; }
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
                writer.WriteString(0, Name);
                writer.WriteString(1, Value);
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

        /// <summary>
        /// Change Channel Response - Reply from server
        /// </summary>
        public struct ChangeChannelResponse() {
            public required Status Status { get; set; }
            public string? Data { get; set; }
        }

        private record GetProgramGuidePayload : TLVPayload {
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
            public ChannelInfo[]? ChannelList { get; set; }
            public PageToken? PageToken { get; set; }
            public RecordingFlag? RecordingFlag { get; set; }
            public AdditionalInfo[]? ExternalIDList { get; set; }
            public byte[]? Data { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (StartTime != null)
                    writer.WriteUInt(0, TimeUtil.ToEpochSeconds(StartTime!.Value));
                if (EndTime != null)
                    writer.WriteUInt(1, TimeUtil.ToEpochSeconds(EndTime!.Value));
                if (ChannelList != null)
                {
                    writer.StartArray(2);
                    foreach (var item in ChannelList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                if (PageToken != null)
                    PageToken.Serialize(writer, 3);
                if (RecordingFlag != null)
                    writer.WriteUInt(4, (uint)RecordingFlag);
                if (ExternalIDList != null)
                {
                    writer.StartArray(5);
                    foreach (var item in ExternalIDList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                if (Data != null)
                    writer.WriteBytes(6, Data);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Program Guide Response - Reply from server
        /// </summary>
        public struct ProgramGuideResponse() {
            public required ChannelPaging Paging { get; set; }
            public required Program[] ProgramList { get; set; }
        }

        private record RecordProgramPayload : TLVPayload {
            public required string ProgramIdentifier { get; set; }
            public required bool ShouldRecordSeries { get; set; }
            public required AdditionalInfo[] ExternalIDList { get; set; }
            public required byte[] Data { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, ProgramIdentifier);
                writer.WriteBool(1, ShouldRecordSeries);
                {
                    writer.StartArray(2);
                    foreach (var item in ExternalIDList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.WriteBytes(3, Data);
                writer.EndContainer();
            }
        }

        private record CancelRecordProgramPayload : TLVPayload {
            public required string ProgramIdentifier { get; set; }
            public required bool ShouldRecordSeries { get; set; }
            public required AdditionalInfo[] ExternalIDList { get; set; }
            public required byte[] Data { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, ProgramIdentifier);
                writer.WriteBool(1, ShouldRecordSeries);
                {
                    writer.StartArray(2);
                    foreach (var item in ExternalIDList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.WriteBytes(3, Data);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Change Channel
        /// </summary>
        public async Task<ChangeChannelResponse?> ChangeChannel(SecureSession session, string match) {
            ChangeChannelPayload requestFields = new ChangeChannelPayload() {
                Match = match,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ChangeChannelResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (string?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Change Channel By Number
        /// </summary>
        public async Task<bool> ChangeChannelByNumber(SecureSession session, ushort majorNumber, ushort minorNumber) {
            ChangeChannelByNumberPayload requestFields = new ChangeChannelByNumberPayload() {
                MajorNumber = majorNumber,
                MinorNumber = minorNumber,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Skip Channel
        /// </summary>
        public async Task<bool> SkipChannel(SecureSession session, short count) {
            SkipChannelPayload requestFields = new SkipChannelPayload() {
                Count = count,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Program Guide
        /// </summary>
        public async Task<ProgramGuideResponse?> GetProgramGuide(SecureSession session, DateTime? startTime, DateTime? endTime, ChannelInfo[]? channelList, PageToken? pageToken, RecordingFlag? recordingFlag, AdditionalInfo[]? externalIDList, byte[]? data) {
            GetProgramGuidePayload requestFields = new GetProgramGuidePayload() {
                StartTime = startTime,
                EndTime = endTime,
                ChannelList = channelList,
                PageToken = pageToken,
                RecordingFlag = recordingFlag,
                ExternalIDList = externalIDList,
                Data = data,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ProgramGuideResponse() {
                Paging = (ChannelPaging)GetField(resp, 0),
                ProgramList = GetArrayField<Program>(resp, 1),
            };
        }

        /// <summary>
        /// Record Program
        /// </summary>
        public async Task<bool> RecordProgram(SecureSession session, string programIdentifier, bool shouldRecordSeries, AdditionalInfo[] externalIDList, byte[] data) {
            RecordProgramPayload requestFields = new RecordProgramPayload() {
                ProgramIdentifier = programIdentifier,
                ShouldRecordSeries = shouldRecordSeries,
                ExternalIDList = externalIDList,
                Data = data,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Cancel Record Program
        /// </summary>
        public async Task<bool> CancelRecordProgram(SecureSession session, string programIdentifier, bool shouldRecordSeries, AdditionalInfo[] externalIDList, byte[] data) {
            CancelRecordProgramPayload requestFields = new CancelRecordProgramPayload() {
                ProgramIdentifier = programIdentifier,
                ShouldRecordSeries = shouldRecordSeries,
                ExternalIDList = externalIDList,
                Data = data,
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
        /// Channel List Attribute
        /// </summary>
        public required ReadAttribute<ChannelInfo[]> ChannelList { get; init; }

        /// <summary>
        /// Lineup Attribute
        /// </summary>
        public required ReadAttribute<LineupInfo?> Lineup { get; init; }

        /// <summary>
        /// Current Channel Attribute
        /// </summary>
        public required ReadAttribute<ChannelInfo?> CurrentChannel { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Channel";
        }
    }
}