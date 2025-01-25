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

namespace MatterDotNet.Clusters.CHIP
{
    /// <summary>
    /// Provides an interface for providing OTA software updates
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class OTASoftwareUpdateProvider : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0029;

        /// <summary>
        /// Provides an interface for providing OTA software updates
        /// </summary>
        [SetsRequiredMembers]
        public OTASoftwareUpdateProvider(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected OTASoftwareUpdateProvider(uint cluster, ushort endPoint) : base(cluster, endPoint) {
        }

        #region Enums
        /// <summary>
        /// Status
        /// </summary>
        public enum Status : byte {
            /// <summary>
            /// Indicates that the OTA Provider has an update available.
            /// </summary>
            UpdateAvailable = 0,
            /// <summary>
            /// Indicates OTA Provider may have an update, but it is not ready yet.
            /// </summary>
            Busy = 1,
            /// <summary>
            /// Indicates that there is definitely no update currently available from the OTA Provider.
            /// </summary>
            NotAvailable = 2,
            /// <summary>
            /// Indicates that the requested download protocol is not supported by the OTA Provider.
            /// </summary>
            DownloadProtocolNotSupported = 3,
        }

        /// <summary>
        /// Apply Update Action
        /// </summary>
        public enum ApplyUpdateAction : byte {
            /// <summary>
            /// Apply the update.
            /// </summary>
            Proceed = 0,
            /// <summary>
            /// Wait at least the given delay time.
            /// </summary>
            AwaitNextAction = 1,
            /// <summary>
            /// The OTA Provider is conveying a desire to rescind a previously provided Software Image.
            /// </summary>
            Discontinue = 2,
        }

        /// <summary>
        /// Download Protocol
        /// </summary>
        public enum DownloadProtocol : byte {
            /// <summary>
            /// Indicates support for synchronous BDX.
            /// </summary>
            BDXSynchronous = 0,
            /// <summary>
            /// Indicates support for asynchronous BDX.
            /// </summary>
            BDXAsynchronous = 1,
            /// <summary>
            /// Indicates support for HTTPS.
            /// </summary>
            HTTPS = 2,
            /// <summary>
            /// Indicates support for vendor specific protocol.
            /// </summary>
            VendorSpecific = 3,
        }

        /// <summary>
        /// Announcement Reason
        /// </summary>
        public enum AnnouncementReason : byte {
            SimpleAnnouncement = 0,
            UpdateAvailable = 1,
            UrgentUpdateAvailable = 2,
        }

        /// <summary>
        /// Update State
        /// </summary>
        public enum UpdateState : byte {
            Unknown = 0,
            Idle = 1,
            Querying = 2,
            DelayedOnQuery = 3,
            Downloading = 4,
            Applying = 5,
            DelayedOnApply = 6,
            RollingBack = 7,
            DelayedOnUserConsent = 8,
        }

        /// <summary>
        /// Change Reason
        /// </summary>
        public enum ChangeReason : byte {
            Unknown = 0,
            Success = 1,
            Failure = 2,
            TimeOut = 3,
            DelayByProvider = 4,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Provider Location
        /// </summary>
        public record ProviderLocation : TLVPayload {
            /// <summary>
            /// Provider Location
            /// </summary>
            public ProviderLocation() { }

            /// <summary>
            /// Provider Location
            /// </summary>
            [SetsRequiredMembers]
            public ProviderLocation(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ProviderNodeID = reader.GetULong(1)!.Value;
                Endpoint = reader.GetUShort(2)!.Value;
            }
            public required ulong ProviderNodeID { get; set; }
            public required ushort Endpoint { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(1, ProviderNodeID);
                writer.WriteUShort(2, Endpoint);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record QueryImagePayload : TLVPayload {
            public required ushort VendorID { get; set; }
            public required ushort ProductID { get; set; }
            public required uint SoftwareVersion { get; set; }
            public required DownloadProtocol[] ProtocolsSupported { get; set; }
            public ushort? HardwareVersion { get; set; }
            public string? Location { get; set; }
            public bool? RequestorCanConsent { get; set; }
            public byte[]? MetadataForProvider { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, VendorID);
                writer.WriteUShort(1, ProductID);
                writer.WriteUInt(2, SoftwareVersion);
                {
                    writer.StartArray(3);
                    foreach (var item in ProtocolsSupported) {
                        writer.WriteUShort(-1, (ushort)item);
                    }
                    writer.EndContainer();
                }
                if (HardwareVersion != null)
                    writer.WriteUShort(4, HardwareVersion);
                if (Location != null)
                    writer.WriteString(5, Location, 2);
                if (RequestorCanConsent != null)
                    writer.WriteBool(6, RequestorCanConsent);
                if (MetadataForProvider != null)
                    writer.WriteBytes(7, MetadataForProvider, 512);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Query Image Response - Reply from server
        /// </summary>
        public struct QueryImageResponse() {
            public required Status Status { get; set; }
            public uint? DelayedActionTime { get; set; }
            public string? ImageURI { get; set; }
            public uint? SoftwareVersion { get; set; }
            public string? SoftwareVersionString { get; set; }
            public byte[]? UpdateToken { get; set; }
            public bool? UserConsentNeeded { get; set; }
            public byte[]? MetadataForRequestor { get; set; }
        }

        private record ApplyUpdateRequestPayload : TLVPayload {
            public required byte[] UpdateToken { get; set; }
            public required uint NewVersion { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, UpdateToken, 32);
                writer.WriteUInt(1, NewVersion);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Apply Update Response - Reply from server
        /// </summary>
        public struct ApplyUpdateResponse() {
            public required ApplyUpdateAction Action { get; set; }
            public required uint DelayedActionTime { get; set; }
        }

        private record NotifyUpdateAppliedPayload : TLVPayload {
            public required byte[] UpdateToken { get; set; }
            public required uint SoftwareVersion { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, UpdateToken, 32);
                writer.WriteUInt(1, SoftwareVersion);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Query Image
        /// </summary>
        public async Task<QueryImageResponse?> QueryImage(SecureSession session, ushort vendorID, ushort productID, uint softwareVersion, DownloadProtocol[] protocolsSupported, ushort? hardwareVersion, string? location, bool? requestorCanConsent, byte[]? metadataForProvider) {
            QueryImagePayload requestFields = new QueryImagePayload() {
                VendorID = vendorID,
                ProductID = productID,
                SoftwareVersion = softwareVersion,
                ProtocolsSupported = protocolsSupported,
                HardwareVersion = hardwareVersion,
                Location = location,
                RequestorCanConsent = requestorCanConsent,
                MetadataForProvider = metadataForProvider,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new QueryImageResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                DelayedActionTime = (uint?)GetOptionalField(resp, 1),
                ImageURI = (string?)GetOptionalField(resp, 2),
                SoftwareVersion = (uint?)GetOptionalField(resp, 3),
                SoftwareVersionString = (string?)GetOptionalField(resp, 4),
                UpdateToken = (byte[]?)GetOptionalField(resp, 5),
                UserConsentNeeded = (bool?)GetOptionalField(resp, 6),
                MetadataForRequestor = (byte[]?)GetOptionalField(resp, 7),
            };
        }

        /// <summary>
        /// Apply Update Request
        /// </summary>
        public async Task<ApplyUpdateResponse?> ApplyUpdateRequest(SecureSession session, byte[] updateToken, uint newVersion) {
            ApplyUpdateRequestPayload requestFields = new ApplyUpdateRequestPayload() {
                UpdateToken = updateToken,
                NewVersion = newVersion,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ApplyUpdateResponse() {
                Action = (ApplyUpdateAction)(byte)GetField(resp, 0),
                DelayedActionTime = (uint)GetField(resp, 1),
            };
        }

        /// <summary>
        /// Notify Update Applied
        /// </summary>
        public async Task<bool> NotifyUpdateApplied(SecureSession session, byte[] updateToken, uint softwareVersion) {
            NotifyUpdateAppliedPayload requestFields = new NotifyUpdateAppliedPayload() {
                UpdateToken = updateToken,
                SoftwareVersion = softwareVersion,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields);
            return ValidateResponse(resp);
        }
        #endregion Commands


        /// <inheritdoc />
        public override string ToString() {
            return "OTA Software Update Provider";
        }
    }
}