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
    /// This cluster is used for managing the content control (including &quot;parental control&quot;) settings on a media device such as a TV, or Set-top Box.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ContentControl : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x050F;

        /// <summary>
        /// This cluster is used for managing the content control (including &quot;parental control&quot;) settings on a media device such as a TV, or Set-top Box.
        /// </summary>
        [SetsRequiredMembers]
        public ContentControl(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ContentControl(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            Enabled = new ReadAttribute<bool>(cluster, endPoint, 0) {
                Deserialize = x => (bool)(dynamic?)x!
            };
            OnDemandRatings = new ReadAttribute<RatingName[]>(cluster, endPoint, 1) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    RatingName[] list = new RatingName[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new RatingName(reader.GetStruct(i)!);
                    return list;
                }
            };
            OnDemandRatingThreshold = new ReadAttribute<string>(cluster, endPoint, 2) {
                Deserialize = x => (string)(dynamic?)x!
            };
            ScheduledContentRatings = new ReadAttribute<RatingName[]>(cluster, endPoint, 3) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    RatingName[] list = new RatingName[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new RatingName(reader.GetStruct(i)!);
                    return list;
                }
            };
            ScheduledContentRatingThreshold = new ReadAttribute<string>(cluster, endPoint, 4) {
                Deserialize = x => (string)(dynamic?)x!
            };
            ScreenDailyTime = new ReadAttribute<TimeSpan>(cluster, endPoint, 5) {
                Deserialize = x => (TimeSpan)(dynamic?)x!
            };
            RemainingScreenTime = new ReadAttribute<TimeSpan>(cluster, endPoint, 6) {
                Deserialize = x => (TimeSpan)(dynamic?)x!
            };
            BlockUnrated = new ReadAttribute<bool>(cluster, endPoint, 7) {
                Deserialize = x => (bool)(dynamic?)x!
            };
        }

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
        }
        #endregion Enums

        #region Records
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
                writer.WriteString(0, RatingNameField);
                if (RatingNameDesc != null)
                    writer.WriteString(1, RatingNameDesc);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record UpdatePINPayload : TLVPayload {
            public string? OldPIN { get; set; }
            public required string NewPIN { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (OldPIN != null)
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
            public TimeSpan? BonusTime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (PINCode != null)
                    writer.WriteString(0, PINCode, 6);
                if (BonusTime != null)
                    writer.WriteUInt(1, (uint)BonusTime!.Value.TotalSeconds);
                writer.EndContainer();
            }
        }

        private record SetScreenDailyTimePayload : TLVPayload {
            public required TimeSpan ScreenTime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)ScreenTime.TotalSeconds);
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
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Update PIN
        /// </summary>
        public async Task<bool> UpdatePIN(SecureSession session, string? oldPIN, string newPIN) {
            UpdatePINPayload requestFields = new UpdatePINPayload() {
                OldPIN = oldPIN,
                NewPIN = newPIN,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Reset PIN
        /// </summary>
        public async Task<ResetPINResponse?> ResetPIN(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01);
            if (!ValidateResponse(resp))
                return null;
            return new ResetPINResponse() {
                PINCode = (string)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Enable
        /// </summary>
        public async Task<bool> Enable(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Disable
        /// </summary>
        public async Task<bool> Disable(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Add Bonus Time
        /// </summary>
        public async Task<bool> AddBonusTime(SecureSession session, string? pINCode, TimeSpan? bonusTime) {
            AddBonusTimePayload requestFields = new AddBonusTimePayload() {
                PINCode = pINCode,
                BonusTime = bonusTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Screen Daily Time
        /// </summary>
        public async Task<bool> SetScreenDailyTime(SecureSession session, TimeSpan screenTime) {
            SetScreenDailyTimePayload requestFields = new SetScreenDailyTimePayload() {
                ScreenTime = screenTime,
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
        public async Task<bool> SetOnDemandRatingThreshold(SecureSession session, string rating) {
            SetOnDemandRatingThresholdPayload requestFields = new SetOnDemandRatingThresholdPayload() {
                Rating = rating,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x09, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Scheduled Content Rating Threshold
        /// </summary>
        public async Task<bool> SetScheduledContentRatingThreshold(SecureSession session, string rating) {
            SetScheduledContentRatingThresholdPayload requestFields = new SetScheduledContentRatingThresholdPayload() {
                Rating = rating,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0A, requestFields);
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
        /// Enabled Attribute
        /// </summary>
        public required ReadAttribute<bool> Enabled { get; init; }

        /// <summary>
        /// On Demand Ratings Attribute
        /// </summary>
        public required ReadAttribute<RatingName[]> OnDemandRatings { get; init; }

        /// <summary>
        /// On Demand Rating Threshold Attribute
        /// </summary>
        public required ReadAttribute<string> OnDemandRatingThreshold { get; init; }

        /// <summary>
        /// Scheduled Content Ratings Attribute
        /// </summary>
        public required ReadAttribute<RatingName[]> ScheduledContentRatings { get; init; }

        /// <summary>
        /// Scheduled Content Rating Threshold Attribute
        /// </summary>
        public required ReadAttribute<string> ScheduledContentRatingThreshold { get; init; }

        /// <summary>
        /// Screen Daily Time Attribute
        /// </summary>
        public required ReadAttribute<TimeSpan> ScreenDailyTime { get; init; }

        /// <summary>
        /// Remaining Screen Time Attribute
        /// </summary>
        public required ReadAttribute<TimeSpan> RemainingScreenTime { get; init; }

        /// <summary>
        /// Block Unrated Attribute
        /// </summary>
        public required ReadAttribute<bool> BlockUnrated { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Content Control";
        }
    }
}