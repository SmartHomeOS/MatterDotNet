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
    /// Content Control Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ContentControlCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x050F;

        /// <summary>
        /// Content Control Cluster
        /// </summary>
        public ContentControlCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ContentControlCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports managing screen time limits.
            /// </summary>
            ScreenTime = 1,
            /// <summary>
            /// Supports managing a PIN code which is used for restricting access to configuration of this feature.
            /// </summary>
            PINManagement = 2,
            /// <summary>
            /// Supports managing content controls for unrated content.
            /// </summary>
            BlockUnrated = 4,
            /// <summary>
            /// Supports managing content controls based upon rating threshold for on demand content.
            /// </summary>
            OnDemandContentRating = 8,
            /// <summary>
            /// Supports managing content controls based upon rating threshold for scheduled content.
            /// </summary>
            ScheduledContentRating = 16,
            /// <summary>
            /// Supports managing a set of channels that are prohibited.
            /// </summary>
            BlockChannels = 32,
            /// <summary>
            /// Supports managing a set of applications that are prohibited.
            /// </summary>
            BlockApplications = 64,
            /// <summary>
            /// Supports managing content controls based upon setting time window in which all contents and applications SHALL be blocked.
            /// </summary>
            BlockContentTimeWindow = 128,
        }

        /// <summary>
        /// Day Of Week Bitmap type
        /// </summary>
        [Flags]
        public enum DayOfWeekBitmapType {
            /// <summary>
            /// Sunday
            /// </summary>
            Sunday = 1,
            /// <summary>
            /// Monday
            /// </summary>
            Monday = 2,
            /// <summary>
            /// Tuesday
            /// </summary>
            Tuesday = 4,
            /// <summary>
            /// Wednesday
            /// </summary>
            Wednesday = 8,
            /// <summary>
            /// Thursday
            /// </summary>
            Thursday = 16,
            /// <summary>
            /// Friday
            /// </summary>
            Friday = 32,
            /// <summary>
            /// Saturday
            /// </summary>
            Saturday = 64,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// App Info
        /// </summary>
        public record AppInfo : TLVPayload {
            /// <summary>
            /// App Info
            /// </summary>
            public AppInfo() { }

            /// <summary>
            /// App Info
            /// </summary>
            [SetsRequiredMembers]
            public AppInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                CatalogVendorID = reader.GetUShort(0)!.Value;
                ApplicationID = reader.GetString(1, false)!;
            }
            public required ushort CatalogVendorID { get; set; }
            public required string ApplicationID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, CatalogVendorID);
                writer.WriteString(1, ApplicationID);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Block Channel
        /// </summary>
        public record BlockChannel : TLVPayload {
            /// <summary>
            /// Block Channel
            /// </summary>
            public BlockChannel() { }

            /// <summary>
            /// Block Channel
            /// </summary>
            [SetsRequiredMembers]
            public BlockChannel(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                BlockChannelIndex = reader.GetUShort(0, true);
                MajorNumber = reader.GetUShort(1)!.Value;
                MinorNumber = reader.GetUShort(2)!.Value;
                Identifier = reader.GetString(3, true);
            }
            public required ushort? BlockChannelIndex { get; set; }
            public required ushort MajorNumber { get; set; }
            public required ushort MinorNumber { get; set; }
            public string? Identifier { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, BlockChannelIndex);
                writer.WriteUShort(1, MajorNumber);
                writer.WriteUShort(2, MinorNumber);
                if (Identifier != null)
                    writer.WriteString(3, Identifier);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Rating Name
        /// </summary>
        public record RatingName : TLVPayload {
            /// <summary>
            /// Rating Name
            /// </summary>
            public RatingName() { }

            /// <summary>
            /// Rating Name
            /// </summary>
            [SetsRequiredMembers]
            public RatingName(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                RatingNameField = reader.GetString(0, false)!;
                RatingNameDesc = reader.GetString(1, true);
            }
            public required string RatingNameField { get; set; }
            public string? RatingNameDesc { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, RatingNameField, 8);
                if (RatingNameDesc != null)
                    writer.WriteString(1, RatingNameDesc, 64);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Time Period Struct type
        /// </summary>
        public record TimePeriodStructType : TLVPayload {
            /// <summary>
            /// Time Period Struct type
            /// </summary>
            public TimePeriodStructType() { }

            /// <summary>
            /// Time Period Struct type
            /// </summary>
            [SetsRequiredMembers]
            public TimePeriodStructType(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                StartHour = reader.GetByte(0)!.Value;
                StartMinute = reader.GetByte(1)!.Value;
                EndHour = reader.GetByte(2)!.Value;
                EndMinute = reader.GetByte(3)!.Value;
            }
            public required byte StartHour { get; set; }
            public required byte StartMinute { get; set; }
            public required byte EndHour { get; set; }
            public required byte EndMinute { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, StartHour, 23);
                writer.WriteByte(1, StartMinute, 59);
                writer.WriteByte(2, EndHour, 23);
                writer.WriteByte(3, EndMinute, 59);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Time Window
        /// </summary>
        public record TimeWindow : TLVPayload {
            /// <summary>
            /// Time Window
            /// </summary>
            public TimeWindow() { }

            /// <summary>
            /// Time Window
            /// </summary>
            [SetsRequiredMembers]
            public TimeWindow(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                TimeWindowIndex = reader.GetUShort(0, true);
                DayOfWeek = (DayOfWeekBitmapType)reader.GetUShort(1)!.Value;
                {
                    TimePeriod = new TimePeriodStructType[((object[])fields[2]).Length];
                    for (int i = 0; i < TimePeriod.Length; i++) {
                        TimePeriod[i] = new TimePeriodStructType((object[])fields[-1]);
                    }
                }
            }
            public required ushort? TimeWindowIndex { get; set; }
            public required DayOfWeekBitmapType DayOfWeek { get; set; }
            public required TimePeriodStructType[] TimePeriod { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, TimeWindowIndex);
                writer.WriteUShort(1, (ushort)DayOfWeek);
                {
                    writer.StartArray(2);
                    foreach (var item in TimePeriod) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record UpdatePINPayload : TLVPayload {
            public required string OldPIN { get; set; }
            public required string NewPIN { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, OldPIN, 6);
                writer.WriteString(1, NewPIN, 6);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Reset PIN Response - Reply from server
        /// </summary>
        public struct ResetPINResponse() {
            public required string PINCode { get; set; }
        }

        private record AddBonusTimePayload : TLVPayload {
            public string? PINCode { get; set; }
            public required TimeSpan BonusTime { get; set; } = TimeSpan.FromSeconds(300);
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (PINCode != null)
                    writer.WriteString(0, PINCode, 6);
                writer.WriteUInt(1, (uint)BonusTime.TotalSeconds);
                writer.EndContainer();
            }
        }

        private record SetScreenDailyTimePayload : TLVPayload {
            public required TimeSpan ScreenTime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)ScreenTime.TotalSeconds, 86400);
                writer.EndContainer();
            }
        }

        private record SetOnDemandRatingThresholdPayload : TLVPayload {
            public required string Rating { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Rating, 8);
                writer.EndContainer();
            }
        }

        private record SetScheduledContentRatingThresholdPayload : TLVPayload {
            public required string Rating { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Rating, 8);
                writer.EndContainer();
            }
        }

        private record AddBlockChannelsPayload : TLVPayload {
            public required BlockChannel[] Channels { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartArray(0);
                    foreach (var item in Channels) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        private record RemoveBlockChannelsPayload : TLVPayload {
            public required ushort[] ChannelIndexes { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartArray(0);
                    foreach (var item in ChannelIndexes) {
                        writer.WriteUShort(-1, item);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        private record AddBlockApplicationsPayload : TLVPayload {
            public required AppInfo[] Applications { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartArray(0);
                    foreach (var item in Applications) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        private record RemoveBlockApplicationsPayload : TLVPayload {
            public required AppInfo[] Applications { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartArray(0);
                    foreach (var item in Applications) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        private record SetBlockContentTimeWindowPayload : TLVPayload {
            public required TimeWindow TimeWindow { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                TimeWindow.Serialize(writer, 0);
                writer.EndContainer();
            }
        }

        private record RemoveBlockContentTimeWindowPayload : TLVPayload {
            public required ushort[] TimeWindowIndexes { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartArray(0);
                    foreach (var item in TimeWindowIndexes) {
                        writer.WriteUShort(-1, item);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Update PIN
        /// </summary>
        public async Task<bool> UpdatePIN(SecureSession session, ushort commandTimeoutMS, string OldPIN, string NewPIN) {
            UpdatePINPayload requestFields = new UpdatePINPayload() {
                OldPIN = OldPIN,
                NewPIN = NewPIN,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Reset PIN
        /// </summary>
        public async Task<ResetPINResponse?> ResetPIN(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x01);
            if (!ValidateResponse(resp))
                return null;
            return new ResetPINResponse() {
                PINCode = (string)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Enable
        /// </summary>
        public async Task<bool> Enable(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x03);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Disable
        /// </summary>
        public async Task<bool> Disable(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x04);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Add Bonus Time
        /// </summary>
        public async Task<bool> AddBonusTime(SecureSession session, string? PINCode, TimeSpan BonusTime) {
            AddBonusTimePayload requestFields = new AddBonusTimePayload() {
                PINCode = PINCode,
                BonusTime = BonusTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Screen Daily Time
        /// </summary>
        public async Task<bool> SetScreenDailyTime(SecureSession session, TimeSpan ScreenTime) {
            SetScreenDailyTimePayload requestFields = new SetScreenDailyTimePayload() {
                ScreenTime = ScreenTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Block Unrated Content
        /// </summary>
        public async Task<bool> BlockUnratedContent(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Unblock Unrated Content
        /// </summary>
        public async Task<bool> UnblockUnratedContent(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x08);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set On Demand Rating Threshold
        /// </summary>
        public async Task<bool> SetOnDemandRatingThreshold(SecureSession session, string Rating) {
            SetOnDemandRatingThresholdPayload requestFields = new SetOnDemandRatingThresholdPayload() {
                Rating = Rating,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x09, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Scheduled Content Rating Threshold
        /// </summary>
        public async Task<bool> SetScheduledContentRatingThreshold(SecureSession session, string Rating) {
            SetScheduledContentRatingThresholdPayload requestFields = new SetScheduledContentRatingThresholdPayload() {
                Rating = Rating,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0A, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Add Block Channels
        /// </summary>
        public async Task<bool> AddBlockChannels(SecureSession session, BlockChannel[] Channels) {
            AddBlockChannelsPayload requestFields = new AddBlockChannelsPayload() {
                Channels = Channels,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0B, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Remove Block Channels
        /// </summary>
        public async Task<bool> RemoveBlockChannels(SecureSession session, ushort[] ChannelIndexes) {
            RemoveBlockChannelsPayload requestFields = new RemoveBlockChannelsPayload() {
                ChannelIndexes = ChannelIndexes,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0C, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Add Block Applications
        /// </summary>
        public async Task<bool> AddBlockApplications(SecureSession session, AppInfo[] Applications) {
            AddBlockApplicationsPayload requestFields = new AddBlockApplicationsPayload() {
                Applications = Applications,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0D, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Remove Block Applications
        /// </summary>
        public async Task<bool> RemoveBlockApplications(SecureSession session, AppInfo[] Applications) {
            RemoveBlockApplicationsPayload requestFields = new RemoveBlockApplicationsPayload() {
                Applications = Applications,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0E, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Block Content Time Window
        /// </summary>
        public async Task<bool> SetBlockContentTimeWindow(SecureSession session, TimeWindow TimeWindow) {
            SetBlockContentTimeWindowPayload requestFields = new SetBlockContentTimeWindowPayload() {
                TimeWindow = TimeWindow,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0F, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Remove Block Content Time Window
        /// </summary>
        public async Task<bool> RemoveBlockContentTimeWindow(SecureSession session, ushort[] TimeWindowIndexes) {
            RemoveBlockContentTimeWindowPayload requestFields = new RemoveBlockContentTimeWindowPayload() {
                TimeWindowIndexes = TimeWindowIndexes,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x10, requestFields);
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
        /// Get the Enabled attribute
        /// </summary>
        public async Task<bool> GetEnabled(SecureSession session) {
            return (bool)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Get the On Demand Ratings attribute
        /// </summary>
        public async Task<RatingName[]> GetOnDemandRatings(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 1))!);
            RatingName[] list = new RatingName[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new RatingName(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the On Demand Rating Threshold attribute
        /// </summary>
        public async Task<string> GetOnDemandRatingThreshold(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 2))!;
        }

        /// <summary>
        /// Get the Scheduled Content Ratings attribute
        /// </summary>
        public async Task<RatingName[]> GetScheduledContentRatings(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 3))!);
            RatingName[] list = new RatingName[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new RatingName(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Scheduled Content Rating Threshold attribute
        /// </summary>
        public async Task<string> GetScheduledContentRatingThreshold(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 4))!;
        }

        /// <summary>
        /// Get the Screen Daily Time attribute
        /// </summary>
        public async Task<TimeSpan> GetScreenDailyTime(SecureSession session) {
            return (TimeSpan)(dynamic?)(await GetAttribute(session, 5))!;
        }

        /// <summary>
        /// Get the Remaining Screen Time attribute
        /// </summary>
        public async Task<TimeSpan> GetRemainingScreenTime(SecureSession session) {
            return (TimeSpan)(dynamic?)(await GetAttribute(session, 6))!;
        }

        /// <summary>
        /// Get the Block Unrated attribute
        /// </summary>
        public async Task<bool> GetBlockUnrated(SecureSession session) {
            return (bool)(dynamic?)(await GetAttribute(session, 7))!;
        }

        /// <summary>
        /// Get the Block Channel List attribute
        /// </summary>
        public async Task<BlockChannel[]> GetBlockChannelList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 8))!);
            BlockChannel[] list = new BlockChannel[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new BlockChannel(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Block Application List attribute
        /// </summary>
        public async Task<AppInfo[]> GetBlockApplicationList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 9))!);
            AppInfo[] list = new AppInfo[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new AppInfo(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Block Content Time Window attribute
        /// </summary>
        public async Task<TimeWindow[]> GetBlockContentTimeWindow(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 10))!);
            TimeWindow[] list = new TimeWindow[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new TimeWindow(reader.GetStruct(i)!);
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Content Control Cluster";
        }
    }
}