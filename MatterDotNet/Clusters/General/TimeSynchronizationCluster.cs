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

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// Accurate time is required for a number of reasons, including scheduling, display and validating security materials.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class TimeSynchronization : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0038;

        /// <summary>
        /// Accurate time is required for a number of reasons, including scheduling, display and validating security materials.
        /// </summary>
        [SetsRequiredMembers]
        public TimeSynchronization(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected TimeSynchronization(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            UTCTime = new ReadAttribute<DateTime?>(cluster, endPoint, 0, true) {
                Deserialize = x => (DateTime?)(dynamic?)x
            };
            Granularity = new ReadAttribute<GranularityEnum>(cluster, endPoint, 1) {
                Deserialize = x => (GranularityEnum)DeserializeEnum(x)!
            };
            TimeSource = new ReadAttribute<TimeSourceEnum>(cluster, endPoint, 2) {
                Deserialize = x => (TimeSourceEnum)DeserializeEnum(x)!
            };
            TrustedTimeSource = new ReadAttribute<TrustedTimeSourceStruct?>(cluster, endPoint, 3, true) {
                Deserialize = x => new TrustedTimeSourceStruct((object[])x!)
            };
            DefaultNTP = new ReadAttribute<string?>(cluster, endPoint, 4, true) {
                Deserialize = x => (string?)(dynamic?)x
            };
            TimeZone = new ReadAttribute<TimeZoneStruct[]>(cluster, endPoint, 5) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    TimeZoneStruct[] list = new TimeZoneStruct[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new TimeZoneStruct(reader.GetStruct(i)!);
                    return list;
                }
            };
            DSTOffset = new ReadAttribute<DSTOffsetStruct[]>(cluster, endPoint, 6) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    DSTOffsetStruct[] list = new DSTOffsetStruct[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new DSTOffsetStruct(reader.GetStruct(i)!);
                    return list;
                }
            };
            LocalTime = new ReadAttribute<DateTime?>(cluster, endPoint, 7, true) {
                Deserialize = x => (DateTime?)(dynamic?)x ?? DateTime.MaxValue

            };
            TimeZoneDatabase = new ReadAttribute<TimeZoneDatabaseEnum>(cluster, endPoint, 8) {
                Deserialize = x => (TimeZoneDatabaseEnum)DeserializeEnum(x)!
            };
            NTPServerAvailable = new ReadAttribute<bool>(cluster, endPoint, 9) {
                Deserialize = x => (bool?)(dynamic?)x ?? false

            };
            TimeZoneListMaxSize = new ReadAttribute<byte>(cluster, endPoint, 10) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            DSTOffsetListMaxSize = new ReadAttribute<byte>(cluster, endPoint, 11) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            SupportsDNSResolve = new ReadAttribute<bool>(cluster, endPoint, 12) {
                Deserialize = x => (bool?)(dynamic?)x ?? false

            };
        }

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
        /// Status Code
        /// </summary>
        public enum StatusCode : byte {
            TimeNotAccepted = 2,
        }

        /// <summary>
        /// Granularity
        /// </summary>
        public enum GranularityEnum : byte {
            /// <summary>
            /// This indicates that the node is not currently synchronized with a UTC Time source and its clock is based on the Last Known Good UTC Time only.
            /// </summary>
            NoTimeGranularity = 0,
            /// <summary>
            /// This indicates the node was synchronized to an upstream source in the past, but sufficient clock drift has occurred such that the clock error is now > 5 seconds.
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
        public enum TimeSourceEnum : byte {
            /// <summary>
            /// Node is not currently synchronized with a UTC Time source.
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Node uses an unlisted time source.
            /// </summary>
            Unknown = 0x1,
            /// <summary>
            /// Node received time from a client using the SetUTCTime Command.
            /// </summary>
            Admin = 0x2,
            /// <summary>
            /// Synchronized time by querying the Time Synchronization cluster of another Node.
            /// </summary>
            NodeTimeCluster = 0x3,
            /// <summary>
            /// SNTP from a server not in the Matter network. NTS is not used.
            /// </summary>
            NonMatterSNTP = 0x4,
            /// <summary>
            /// NTP from servers not in the Matter network. None of the servers used NTS.
            /// </summary>
            NonMatterNTP = 0x5,
            /// <summary>
            /// SNTP from a server within the Matter network. NTS is not used.
            /// </summary>
            MatterSNTP = 0x6,
            /// <summary>
            /// NTP from servers within the Matter network. None of the servers used NTS.
            /// </summary>
            MatterNTP = 0x7,
            /// <summary>
            /// NTP from multiple servers in the Matter network and external. None of the servers used NTS.
            /// </summary>
            MixedNTP = 0x8,
            /// <summary>
            /// SNTP from a server not in the Matter network. NTS is used.
            /// </summary>
            NonMatterSNTPNTS = 0x9,
            /// <summary>
            /// NTP from servers not in the Matter network. NTS is used on at least one server.
            /// </summary>
            NonMatterNTPNTS = 0xA,
            /// <summary>
            /// SNTP from a server within the Matter network. NTS is used.
            /// </summary>
            MatterSNTPNTS = 0xB,
            /// <summary>
            /// NTP from a server within the Matter network. NTS is used on at least one server.
            /// </summary>
            MatterNTPNTS = 0xC,
            /// <summary>
            /// NTP from multiple servers in the Matter network and external. NTS is used on at least one server.
            /// </summary>
            MixedNTPNTS = 0xD,
            /// <summary>
            /// Time synchronization comes from a vendor cloud-based source (e.g. "Date" header in authenticated HTTPS connection).
            /// </summary>
            CloudSource = 0xE,
            /// <summary>
            /// Time synchronization comes from PTP.
            /// </summary>
            PTP = 0xF,
            /// <summary>
            /// Time synchronization comes from a GNSS source.
            /// </summary>
            GNSS = 0x10,
        }

        /// <summary>
        /// Time Zone Database
        /// </summary>
        public enum TimeZoneDatabaseEnum : byte {
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
        /// Trusted Time Source
        /// </summary>
        public record TrustedTimeSourceStruct : TLVPayload {
            /// <summary>
            /// Trusted Time Source
            /// </summary>
            public TrustedTimeSourceStruct() { }

            /// <summary>
            /// Trusted Time Source
            /// </summary>
            [SetsRequiredMembers]
            public TrustedTimeSourceStruct(object[] fields) {
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

        /// <summary>
        /// Fabric Scoped Trusted Time Source
        /// </summary>
        public record FabricScopedTrustedTimeSource : TLVPayload {
            /// <summary>
            /// Fabric Scoped Trusted Time Source
            /// </summary>
            public FabricScopedTrustedTimeSource() { }

            /// <summary>
            /// Fabric Scoped Trusted Time Source
            /// </summary>
            [SetsRequiredMembers]
            public FabricScopedTrustedTimeSource(object[] fields) {
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
        public record TimeZoneStruct : TLVPayload {
            /// <summary>
            /// Time Zone
            /// </summary>
            public TimeZoneStruct() { }

            /// <summary>
            /// Time Zone
            /// </summary>
            [SetsRequiredMembers]
            public TimeZoneStruct(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Offset = reader.GetInt(0)!.Value;
                ValidAt = TimeUtil.FromEpochUS(reader.GetULong(1))!.Value;
                Name = reader.GetString(2, true, 64);
            }
            public required int Offset { get; set; }
            public required DateTime ValidAt { get; set; }
            public string? Name { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteInt(0, Offset, 50400, -43200);
                writer.WriteULong(1, TimeUtil.ToEpochUS(ValidAt));
                if (Name != null)
                    writer.WriteString(2, Name, 64);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// DST Offset
        /// </summary>
        public record DSTOffsetStruct : TLVPayload {
            /// <summary>
            /// DST Offset
            /// </summary>
            public DSTOffsetStruct() { }

            /// <summary>
            /// DST Offset
            /// </summary>
            [SetsRequiredMembers]
            public DSTOffsetStruct(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Offset = reader.GetInt(0)!.Value;
                ValidStarting = TimeUtil.FromEpochUS(reader.GetULong(1))!.Value;
                ValidUntil = TimeUtil.FromEpochUS(reader.GetULong(2, true));
            }
            public required int Offset { get; set; }
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
        #endregion Records

        #region Payloads
        private record SetUTCTimePayload : TLVPayload {
            public required DateTime UTCTime { get; set; }
            public required GranularityEnum Granularity { get; set; }
            public TimeSourceEnum? TimeSource { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, TimeUtil.ToEpochUS(UTCTime));
                writer.WriteUShort(1, (ushort)Granularity);
                if (TimeSource != null)
                    writer.WriteUShort(2, (ushort)TimeSource);
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
            public required TimeZoneStruct[] TimeZone { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartArray(0);
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
            public required bool DSTOffsetRequired { get; set; }
        }

        private record SetDSTOffsetPayload : TLVPayload {
            public required DSTOffsetStruct[] DSTOffset { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartArray(0);
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
        public async Task<bool> SetUTCTime(SecureSession session, DateTime uTCTime, GranularityEnum granularity, TimeSourceEnum? timeSource) {
            SetUTCTimePayload requestFields = new SetUTCTimePayload() {
                UTCTime = uTCTime,
                Granularity = granularity,
                TimeSource = timeSource,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Trusted Time Source
        /// </summary>
        public async Task<bool> SetTrustedTimeSource(SecureSession session, FabricScopedTrustedTimeSource? trustedTimeSource) {
            SetTrustedTimeSourcePayload requestFields = new SetTrustedTimeSourcePayload() {
                TrustedTimeSource = trustedTimeSource,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Time Zone
        /// </summary>
        public async Task<SetTimeZoneResponse?> SetTimeZone(SecureSession session, TimeZoneStruct[] timeZone) {
            SetTimeZonePayload requestFields = new SetTimeZonePayload() {
                TimeZone = timeZone,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new SetTimeZoneResponse() {
                DSTOffsetRequired = (bool)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Set DST Offset
        /// </summary>
        public async Task<bool> SetDSTOffset(SecureSession session, DSTOffsetStruct[] dSTOffset) {
            SetDSTOffsetPayload requestFields = new SetDSTOffsetPayload() {
                DSTOffset = dSTOffset,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Default NTP
        /// </summary>
        public async Task<bool> SetDefaultNTP(SecureSession session, string? defaultNTP) {
            SetDefaultNTPPayload requestFields = new SetDefaultNTPPayload() {
                DefaultNTP = defaultNTP,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields);
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
        /// UTC Time Attribute
        /// </summary>
        public required ReadAttribute<DateTime?> UTCTime { get; init; }

        /// <summary>
        /// Granularity Attribute
        /// </summary>
        public required ReadAttribute<GranularityEnum> Granularity { get; init; }

        /// <summary>
        /// Time Source Attribute
        /// </summary>
        public required ReadAttribute<TimeSourceEnum> TimeSource { get; init; }

        /// <summary>
        /// Trusted Time Source Attribute
        /// </summary>
        public required ReadAttribute<TrustedTimeSourceStruct?> TrustedTimeSource { get; init; }

        /// <summary>
        /// Default NTP Attribute
        /// </summary>
        public required ReadAttribute<string?> DefaultNTP { get; init; }

        /// <summary>
        /// Time Zone Attribute
        /// </summary>
        public required ReadAttribute<TimeZoneStruct[]> TimeZone { get; init; }

        /// <summary>
        /// DST Offset Attribute
        /// </summary>
        public required ReadAttribute<DSTOffsetStruct[]> DSTOffset { get; init; }

        /// <summary>
        /// Local Time Attribute
        /// </summary>
        public required ReadAttribute<DateTime?> LocalTime { get; init; }

        /// <summary>
        /// Time Zone Database Attribute
        /// </summary>
        public required ReadAttribute<TimeZoneDatabaseEnum> TimeZoneDatabase { get; init; }

        /// <summary>
        /// NTP Server Available Attribute
        /// </summary>
        public required ReadAttribute<bool> NTPServerAvailable { get; init; }

        /// <summary>
        /// Time Zone List Max Size Attribute
        /// </summary>
        public required ReadAttribute<byte> TimeZoneListMaxSize { get; init; }

        /// <summary>
        /// DST Offset List Max Size Attribute
        /// </summary>
        public required ReadAttribute<byte> DSTOffsetListMaxSize { get; init; }

        /// <summary>
        /// Supports DNS Resolve Attribute
        /// </summary>
        public required ReadAttribute<bool> SupportsDNSResolve { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Time Synchronization";
        }
    }
}