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

namespace MatterDotNet.Clusters.Misc
{
    /// <summary>
    /// The WebRTC transport provider cluster provides a way for stream providers (e.g. Cameras) to stream or receive their data through WebRTC.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class WebRTCTransportProvider : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0553;

        /// <summary>
        /// The WebRTC transport provider cluster provides a way for stream providers (e.g. Cameras) to stream or receive their data through WebRTC.
        /// </summary>
        [SetsRequiredMembers]
        public WebRTCTransportProvider(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected WebRTCTransportProvider(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            CurrentSessions = new ReadAttribute<WebRTCSession[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    WebRTCSession[] list = new WebRTCSession[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new WebRTCSession(reader.GetStruct(i)!);
                    return list;
                }
            };
        }

        #region Enums
        /// <summary>
        /// Stream Type
        /// </summary>
        public enum StreamType : byte {
            Internal = 0,
            Recording = 1,
            Analysis = 2,
            LiveView = 3,
        }

        /// <summary>
        /// Web RTC End Reason
        /// </summary>
        public enum WebRTCEndReason : byte {
            IceFailed = 0x0,
            IceTimeout = 0x1,
            UserHangup = 0x2,
            UserBusy = 0x3,
            Replaced = 0x4,
            NoUserMedia = 0x5,
            InviteTimeout = 0x6,
            AnsweredElsewhere = 0x7,
            OutOfResources = 0x8,
            MediaTimeout = 0x9,
            LowPower = 0xA,
            UnknownReason = 0xB,
        }

        /// <summary>
        /// Web RTC Metadata Options
        /// </summary>
        [Flags]
        public enum WebRTCMetadataOptions : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            DataTLV = 0x01,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// ICE Server
        /// </summary>
        public record ICEServer : TLVPayload {
            /// <summary>
            /// ICE Server
            /// </summary>
            public ICEServer() { }

            /// <summary>
            /// ICE Server
            /// </summary>
            [SetsRequiredMembers]
            public ICEServer(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                {
                    Urls = new string[reader.GetStruct(1)!.Length];
                    for (int n = 0; n < Urls.Length; n++) {
                        Urls[n] = reader.GetString(n, false)!;
                    }
                }
                Username = reader.GetString(2, true);
                Credential = reader.GetString(3, true);
                CAID = reader.GetUShort(4, true);
            }
            public required string[] Urls { get; set; }
            public string? Username { get; set; }
            public string? Credential { get; set; }
            public ushort? CAID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartArray(1);
                    foreach (var item in Urls) {
                        writer.WriteString(-1, item);
                    }
                    writer.EndContainer();
                }
                if (Username != null)
                    writer.WriteString(2, Username);
                if (Credential != null)
                    writer.WriteString(3, Credential);
                if (CAID != null)
                    writer.WriteUShort(4, CAID);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Web RTC Session
        /// </summary>
        public record WebRTCSession : TLVPayload {
            /// <summary>
            /// Web RTC Session
            /// </summary>
            public WebRTCSession() { }

            /// <summary>
            /// Web RTC Session
            /// </summary>
            [SetsRequiredMembers]
            public WebRTCSession(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ID = reader.GetUShort(1)!.Value;
                PeerNodeID = reader.GetULong(2)!.Value;
                PeerFabricIndex = reader.GetByte(3)!.Value;
                StreamType = (StreamType)reader.GetUShort(4)!.Value;
                VideoStreamID = reader.GetUShort(5, true);
                AudioStreamID = reader.GetUShort(6, true);
                MetadataOptions = (WebRTCMetadataOptions)reader.GetUInt(7)!.Value;
            }
            public required ushort ID { get; set; }
            public required ulong PeerNodeID { get; set; }
            public required byte PeerFabricIndex { get; set; }
            public required StreamType StreamType { get; set; }
            public required ushort? VideoStreamID { get; set; }
            public required ushort? AudioStreamID { get; set; }
            public required WebRTCMetadataOptions MetadataOptions { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(1, ID);
                writer.WriteULong(2, PeerNodeID);
                writer.WriteByte(3, PeerFabricIndex);
                writer.WriteUShort(4, (ushort)StreamType);
                writer.WriteUShort(5, VideoStreamID);
                writer.WriteUShort(6, AudioStreamID);
                writer.WriteUInt(7, (uint)MetadataOptions);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record SolicitOfferPayload : TLVPayload {
            public required StreamType StreamType { get; set; }
            public ushort? VideoStreamID { get; set; }
            public ushort? AudioStreamID { get; set; }
            public ICEServer[]? ICEServers { get; set; }
            public string? ICETransportPolicy { get; set; }
            public WebRTCMetadataOptions? MetadataOptions { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)StreamType);
                if (VideoStreamID != null)
                    writer.WriteUShort(1, VideoStreamID);
                if (AudioStreamID != null)
                    writer.WriteUShort(2, AudioStreamID);
                if (ICEServers != null)
                {
                    writer.StartArray(3);
                    foreach (var item in ICEServers) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                if (ICETransportPolicy != null)
                    writer.WriteString(4, ICETransportPolicy);
                if (MetadataOptions != null)
                    writer.WriteUInt(5, (uint)MetadataOptions);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Solicit Offer Response - Reply from server
        /// </summary>
        public struct SolicitOfferResponse() {
            public required ushort WebRTCSessionID { get; set; }
            public required bool DeferredOffer { get; set; }
            public ushort? VideoStreamID { get; set; }
            public ushort? AudioStreamID { get; set; }
        }

        private record ProvideOfferPayload : TLVPayload {
            public required ushort? WebRTCSessionID { get; set; }
            public required string SDP { get; set; }
            public required StreamType StreamType { get; set; }
            public ushort? VideoStreamID { get; set; }
            public ushort? AudioStreamID { get; set; }
            public ICEServer[]? ICEServers { get; set; }
            public string? ICETransportPolicy { get; set; }
            public WebRTCMetadataOptions? MetadataOptions { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, WebRTCSessionID);
                writer.WriteString(1, SDP);
                writer.WriteUShort(2, (ushort)StreamType);
                if (VideoStreamID != null)
                    writer.WriteUShort(3, VideoStreamID);
                if (AudioStreamID != null)
                    writer.WriteUShort(4, AudioStreamID);
                if (ICEServers != null)
                {
                    writer.StartArray(5);
                    foreach (var item in ICEServers) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                if (ICETransportPolicy != null)
                    writer.WriteString(6, ICETransportPolicy);
                if (MetadataOptions != null)
                    writer.WriteUInt(7, (uint)MetadataOptions);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Provide Offer Response - Reply from server
        /// </summary>
        public struct ProvideOfferResponse() {
            public required ushort WebRTCSessionID { get; set; }
            public required ushort VideoStreamID { get; set; }
            public required ushort AudioStreamID { get; set; }
        }

        private record ProvideAnswerPayload : TLVPayload {
            public required ushort WebRTCSessionID { get; set; }
            public required string SDP { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, WebRTCSessionID);
                writer.WriteString(1, SDP);
                writer.EndContainer();
            }
        }

        private record ProvideICECandidatePayload : TLVPayload {
            public required ushort WebRTCSessionID { get; set; }
            public required string ICECandidate { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, WebRTCSessionID);
                writer.WriteString(1, ICECandidate);
                writer.EndContainer();
            }
        }

        private record EndSessionPayload : TLVPayload {
            public required ushort WebRTCSessionID { get; set; }
            public required WebRTCEndReason Reason { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, WebRTCSessionID);
                writer.WriteUShort(1, (ushort)Reason);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Solicit Offer
        /// </summary>
        public async Task<SolicitOfferResponse?> SolicitOffer(SecureSession session, StreamType streamType, ushort? videoStreamID, ushort? audioStreamID, ICEServer[]? iCEServers, string? iCETransportPolicy, WebRTCMetadataOptions? metadataOptions) {
            SolicitOfferPayload requestFields = new SolicitOfferPayload() {
                StreamType = streamType,
                VideoStreamID = videoStreamID,
                AudioStreamID = audioStreamID,
                ICEServers = iCEServers,
                ICETransportPolicy = iCETransportPolicy,
                MetadataOptions = metadataOptions,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new SolicitOfferResponse() {
                WebRTCSessionID = (ushort)GetField(resp, 0),
                DeferredOffer = (bool)GetField(resp, 1),
                VideoStreamID = (ushort?)GetOptionalField(resp, 2),
                AudioStreamID = (ushort?)GetOptionalField(resp, 3),
            };
        }

        /// <summary>
        /// Provide Offer
        /// </summary>
        public async Task<ProvideOfferResponse?> ProvideOffer(SecureSession session, ushort? webRTCSessionID, string sDP, StreamType streamType, ushort? videoStreamID, ushort? audioStreamID, ICEServer[]? iCEServers, string? iCETransportPolicy, WebRTCMetadataOptions? metadataOptions) {
            ProvideOfferPayload requestFields = new ProvideOfferPayload() {
                WebRTCSessionID = webRTCSessionID,
                SDP = sDP,
                StreamType = streamType,
                VideoStreamID = videoStreamID,
                AudioStreamID = audioStreamID,
                ICEServers = iCEServers,
                ICETransportPolicy = iCETransportPolicy,
                MetadataOptions = metadataOptions,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ProvideOfferResponse() {
                WebRTCSessionID = (ushort)GetField(resp, 0),
                VideoStreamID = (ushort)GetField(resp, 1),
                AudioStreamID = (ushort)GetField(resp, 2),
            };
        }

        /// <summary>
        /// Provide Answer
        /// </summary>
        public async Task<bool> ProvideAnswer(SecureSession session, ushort webRTCSessionID, string sDP) {
            ProvideAnswerPayload requestFields = new ProvideAnswerPayload() {
                WebRTCSessionID = webRTCSessionID,
                SDP = sDP,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Provide ICE Candidate
        /// </summary>
        public async Task<bool> ProvideICECandidate(SecureSession session, ushort webRTCSessionID, string iCECandidate) {
            ProvideICECandidatePayload requestFields = new ProvideICECandidatePayload() {
                WebRTCSessionID = webRTCSessionID,
                ICECandidate = iCECandidate,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// End Session
        /// </summary>
        public async Task<bool> EndSession(SecureSession session, ushort webRTCSessionID, WebRTCEndReason reason) {
            EndSessionPayload requestFields = new EndSessionPayload() {
                WebRTCSessionID = webRTCSessionID,
                Reason = reason,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07, requestFields);
            return ValidateResponse(resp);
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Current Sessions Attribute
        /// </summary>
        public required ReadAttribute<WebRTCSession[]> CurrentSessions { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "WebRTC Transport Provider";
        }
    }
}