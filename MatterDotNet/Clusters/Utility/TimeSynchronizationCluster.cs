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

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Time Synchronization Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class TimeSynchronizationCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0038;

        /// <summary>
        /// Time Synchronization Cluster
        /// </summary>
        public TimeSynchronizationCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected TimeSynchronizationCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Server supports time zone.
            /// </summary>
            TimeZone = 1,
            /// <summary>
            /// Server supports an NTP or SNTP client.
            /// </summary>
            NTPClient = 2,
            /// <summary>
            /// Server supports an NTP server role.
            /// </summary>
            NTPServer = 4,
            /// <summary>
            /// Time synchronization client cluster is present.
            /// </summary>
            TimeSyncClient = 8,
        }

        /// <summary>
        /// Granularity
        /// </summary>
        public enum GranularityEnum {
            /// <summary>
            /// This indicates that the node is not currently synchronized with a UTC Time source and its clock is based on the Last Known Good UTC Time only.
            /// </summary>
            NoTimeGranularity = 0,
            /// <summary>
            /// This indicates the node was synchronized to an upstream source in the past, but sufficient clock drift has occurred such that the clock error is now &gt; 5 seconds.
            /// </summary>
            MinutesGranularity = 1,
            /// <summary>
            /// This indicates the node is synchronized to an upstream source using a low resolution protocol. UTC Time is accurate to ± 5 seconds.
            /// </summary>
            SecondsGranularity = 2,
            /// <summary>
            /// This indicates the node is synchronized to an upstream source using high resolution time-synchronization protocol such as NTP, or has built-in GNSS with some amount of jitter applying its GNSS timestamp. UTC Time is accurate to ± 50 ms.
            /// </summary>
            MillisecondsGranularity = 3,
            /// <summary>
            /// This indicates the node is synchronized to an upstream source using a highly precise time-synchronization protocol such as PTP, or has built-in GNSS. UTC time is accurate to ± 10 μs.
            /// </summary>
            MicrosecondsGranularity = 4,
        }

        /// <summary>
        /// Time Source
        /// </summary>
        public enum TimeSourceEnum {
            /// <summary>
            /// Node is not currently synchronized with a UTC Time source.
            /// </summary>
            None = 0,
            /// <summary>
            /// Node uses an unlisted time source.
            /// </summary>
            Unknown = 1,
            /// <summary>
            /// Node received time from a client using the SetUTCTime Command.
            /// </summary>
            Admin = 2,
            /// <summary>
            /// Synchronized time by querying the Time Synchronization cluster of another Node.
            /// </summary>
            NodeTimeCluster = 3,
            /// <summary>
            /// SNTP from a server not in the Matter network. NTS is not used.
            /// </summary>
            NonMatterSNTP = 4,
            /// <summary>
            /// NTP from servers not in the Matter network. None of the servers used NTS.
            /// </summary>
            NonMatterNTP = 5,
            /// <summary>
            /// SNTP from a server within the Matter network. NTS is not used.
            /// </summary>
            MatterSNTP = 6,
            /// <summary>
            /// NTP from servers within the Matter network. None of the servers used NTS.
            /// </summary>
            MatterNTP = 7,
            /// <summary>
            /// NTP from multiple servers in the Matter network and external. None of the servers used NTS.
            /// </summary>
            MixedNTP = 8,
            /// <summary>
            /// SNTP from a server not in the Matter network. NTS is used.
            /// </summary>
            NonMatterSNTPNTS = 9,
            /// <summary>
            /// NTP from servers not in the Matter network. NTS is used on at least one server.
            /// </summary>
            NonMatterNTPNTS = 10,
            /// <summary>
            /// SNTP from a server within the Matter network. NTS is used.
            /// </summary>
            MatterSNTPNTS = 11,
            /// <summary>
            /// NTP from a server within the Matter network. NTS is used on at least one server.
            /// </summary>
            MatterNTPNTS = 12,
            /// <summary>
            /// NTP from multiple servers in the Matter network and external. NTS is used on at least one server.
            /// </summary>
            MixedNTPNTS = 13,
            /// <summary>
            /// Time synchronization comes from a vendor cloud-based source (e.g. &quot;Date&quot; header in authenticated HTTPS connection).
            /// </summary>
            CloudSource = 14,
            /// <summary>
            /// Time synchronization comes from PTP.
            /// </summary>
            PTP = 15,
            /// <summary>
            /// Time synchronization comes from a GNSS source.
            /// </summary>
            GNSS = 16,
        }

        /// <summary>
        /// Time Zone Database
        /// </summary>
        public enum TimeZoneDatabaseEnum {
            /// <summary>
            /// Node has a full list of the available time zones
            /// </summary>
            Full = 0,
            /// <summary>
            /// Node has a partial list of the available time zones
            /// </summary>
            Partial = 1,
            /// <summary>
            /// Node does not have a time zone database
            /// </summary>
            None = 2,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// DST Offset
        /// </summary>
        public record DSTOffset : TLVPayload {
            /// <summary>
            /// DST Offset
            /// </summary>
            public DSTOffset() { }

            [SetsRequiredMembers]
            internal DSTOffset(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Offset = reader.GetInt(0)!.Value;
                ValidStarting = TimeUtil.FromEpochUS(reader.GetULong(1))!.Value;
                ValidUntil = TimeUtil.FromEpochUS(reader.GetULong(2, true));
            }
            public required int  Offset { get; set; }
            public required DateTime ValidStarting { get; set; }
            public required DateTime? ValidUntil { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteInt(0, Offset);
                writer.WriteULong(1, TimeUtil.ToEpochUS(ValidStarting));
                if (!ValidUntil.HasValue)
                    writer.WriteNull(2);
                else
                    writer.WriteULong(2, TimeUtil.ToEpochUS(ValidUntil!.Value));
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Fabric Scoped Trusted Time Source
        /// </summary>
        public record FabricScopedTrustedTimeSource : TLVPayload {
            /// <summary>
            /// Fabric Scoped Trusted Time Source
            /// </summary>
            public FabricScopedTrustedTimeSource() { }

            [SetsRequiredMembers]
            internal FabricScopedTrustedTimeSource(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                NodeID = reader.GetULong(0)!.Value;
                Endpoint = reader.GetUShort(1)!.Value;
            }
            public required ulong NodeID { get; set; }
            public required ushort Endpoint { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, NodeID);
                writer.WriteUShort(1, Endpoint);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Time Zone
        /// </summary>
        public record TimeZone : TLVPayload {
            /// <summary>
            /// Time Zone
            /// </summary>
            public TimeZone() { }

            [SetsRequiredMembers]
            internal TimeZone(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Offset = reader.GetInt(0)!.Value;
                ValidAt = TimeUtil.FromEpochUS(reader.GetULong(1))!.Value;
                Name = reader.GetString(2, true, 64, 0);
            }
            public required int  Offset { get; set; }
            public required DateTime ValidAt { get; set; }
            public string? Name { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteInt(0, Offset, 50400, -43200);
                writer.WriteULong(1, TimeUtil.ToEpochUS(ValidAt));
                if (Name != null)
                    writer.WriteString(2, Name, 64, 0);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Trusted Time Source
        /// </summary>
        public record TrustedTimeSource : TLVPayload {
            /// <summary>
            /// Trusted Time Source
            /// </summary>
            public TrustedTimeSource() { }

            [SetsRequiredMembers]
            internal TrustedTimeSource(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                FabricIndex = reader.GetByte(0)!.Value;
                NodeID = reader.GetULong(1)!.Value;
                Endpoint = reader.GetUShort(2)!.Value;
            }
            public required byte FabricIndex { get; set; }
            public required ulong NodeID { get; set; }
            public required ushort Endpoint { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, FabricIndex);
                writer.WriteULong(1, NodeID);
                writer.WriteUShort(2, Endpoint);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record SetUTCTimePayload : TLVPayload {
            public required DateTime UTCTime { get; set; } = TimeUtil.EPOCH;
            public required GranularityEnum Granularity { get; set; } = GranularityEnum.NoTimeGranularity;
            public TimeSourceEnum? TimeSource { get; set; } = TimeSourceEnum.None;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, TimeUtil.ToEpochUS(UTCTime));
                writer.WriteUShort(1, (ushort)Granularity);
                if (TimeSource != null)
                    writer.WriteUShort(2, (ushort?)TimeSource);
                writer.EndContainer();
            }
        }

        private record SetTrustedTimeSourcePayload : TLVPayload {
            public required FabricScopedTrustedTimeSource? TrustedTimeSource { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (TrustedTimeSource == null)
                    writer.WriteNull(0);
                else
                    TrustedTimeSource.Serialize(writer, 0);
                writer.EndContainer();
            }
        }

        private record SetTimeZonePayload : TLVPayload {
            public required List<TimeZone> TimeZone { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    Constrain(TimeZone, 1, 2);
                    writer.StartList(0);
                    foreach (var item in TimeZone) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Set Time Zone Response - Reply from server
        /// </summary>
        public struct SetTimeZoneResponse() {
            public required bool DSTOffsetsRequired { get; set; } = true;
        }

        private record SetDSTOffsetPayload : TLVPayload {
            public required List<DSTOffset> DSTOffset { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartList(0);
                    foreach (var item in DSTOffset) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        private record SetDefaultNTPPayload : TLVPayload {
            public required string? DefaultNTP { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, DefaultNTP, 128);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Set UTC Time
        /// </summary>
        public async Task<bool> SetUTCTime(SecureSession session, DateTime UTCTime, GranularityEnum Granularity, TimeSourceEnum? TimeSource) {
            SetUTCTimePayload requestFields = new SetUTCTimePayload() {
                UTCTime = UTCTime,
                Granularity = Granularity,
                TimeSource = TimeSource,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Trusted Time Source
        /// </summary>
        public async Task<bool> SetTrustedTimeSource(SecureSession session, FabricScopedTrustedTimeSource TrustedTimeSource) {
            SetTrustedTimeSourcePayload requestFields = new SetTrustedTimeSourcePayload() {
                TrustedTimeSource = TrustedTimeSource,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x01, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Time Zone
        /// </summary>
        public async Task<SetTimeZoneResponse?> SetTimeZone(SecureSession session, List<TimeZone> TimeZone) {
            SetTimeZonePayload requestFields = new SetTimeZonePayload() {
                TimeZone = TimeZone,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new SetTimeZoneResponse() {
                DSTOffsetsRequired = (bool)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Set DST Offset
        /// </summary>
        public async Task<bool> SetDSTOffset(SecureSession session, List<DSTOffset> DSTOffset) {
            SetDSTOffsetPayload requestFields = new SetDSTOffsetPayload() {
                DSTOffset = DSTOffset,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x04, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Default NTP
        /// </summary>
        public async Task<bool> SetDefaultNTP(SecureSession session, string DefaultNTP) {
            SetDefaultNTPPayload requestFields = new SetDefaultNTPPayload() {
                DefaultNTP = DefaultNTP,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x05, requestFields);
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
        /// Get the UTC Time attribute
        /// </summary>
        public async Task<DateTime?> GetUTCTime(SecureSession session) {
            return (DateTime?)(dynamic?)await GetAttribute(session, 0, true) ?? null;
        }

        /// <summary>
        /// Get the Granularity attribute
        /// </summary>
        public async Task<GranularityEnum> GetGranularity(SecureSession session) {
            return (GranularityEnum?)await GetEnumAttribute(session, 1) ?? GranularityEnum.NoTimeGranularity;
        }

        /// <summary>
        /// Get the Time Source attribute
        /// </summary>
        public async Task<TimeSourceEnum> GetTimeSource(SecureSession session) {
            return (TimeSourceEnum?)await GetEnumAttribute(session, 2) ?? TimeSourceEnum.None;
        }

        /// <summary>
        /// Get the Trusted Time Source attribute
        /// </summary>
        public async Task<TrustedTimeSource?> GetTrustedTimeSource(SecureSession session) {
            return new TrustedTimeSource((object[])(await GetAttribute(session, 3))!) ?? null;
        }

        /// <summary>
        /// Get the Default NTP attribute
        /// </summary>
        public async Task<string?> GetDefaultNTP(SecureSession session) {
            return (string?)(dynamic?)await GetAttribute(session, 4, true) ?? null;
        }

        /// <summary>
        /// Get the Time Zone attribute
        /// </summary>
        public async Task<List<TimeZone>> GetTimeZone(SecureSession session) {
            List<TimeZone> list = new List<TimeZone>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 5))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add(new TimeZone(reader.GetStruct(i)!));
            return list;
        }

        /// <summary>
        /// Get the DST Offset attribute
        /// </summary>
        public async Task<List<DSTOffset>> GetDSTOffset(SecureSession session) {
            List<DSTOffset> list = new List<DSTOffset>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 6))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add(new DSTOffset(reader.GetStruct(i)!));
            return list;
        }

        /// <summary>
        /// Get the Local Time attribute
        /// </summary>
        public async Task<DateTime?> GetLocalTime(SecureSession session) {
            return (DateTime?)(dynamic?)await GetAttribute(session, 7, true) ?? null;
        }

        /// <summary>
        /// Get the Time Zone Database attribute
        /// </summary>
        public async Task<TimeZoneDatabaseEnum> GetTimeZoneDatabase(SecureSession session) {
            return (TimeZoneDatabaseEnum?)await GetEnumAttribute(session, 8) ?? TimeZoneDatabaseEnum.None;
        }

        /// <summary>
        /// Get the NTP Server Available attribute
        /// </summary>
        public async Task<bool> GetNTPServerAvailable(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 9) ?? false;
        }

        /// <summary>
        /// Get the Time Zone List Max Size attribute
        /// </summary>
        public async Task<byte> GetTimeZoneListMaxSize(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 10))!;
        }

        /// <summary>
        /// Get the DST Offset List Max Size attribute
        /// </summary>
        public async Task<byte> GetDSTOffsetListMaxSize(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 11))!;
        }

        /// <summary>
        /// Get the Supports DNS Resolve attribute
        /// </summary>
        public async Task<bool> GetSupportsDNSResolve(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 12) ?? false;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Time Synchronization Cluster";
        }
    }
}