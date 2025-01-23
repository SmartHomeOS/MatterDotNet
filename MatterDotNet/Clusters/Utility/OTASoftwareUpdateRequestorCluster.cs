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
    /// Provides an interface for downloading and applying OTA software updates
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class OTASoftwareUpdateRequestor : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x002a;

        /// <summary>
        /// Provides an interface for downloading and applying OTA software updates
        /// </summary>
        public OTASoftwareUpdateRequestor(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected OTASoftwareUpdateRequestor(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Status
        /// </summary>
        public enum Status : byte {
            UpdateAvailable = 0x0,
            Busy = 0x1,
            NotAvailable = 0x2,
            DownloadProtocolNotSupported = 0x3,
        }

        /// <summary>
        /// Apply Update Action
        /// </summary>
        public enum ApplyUpdateAction : byte {
            Proceed = 0x0,
            AwaitNextAction = 0x1,
            Discontinue = 0x2,
        }

        /// <summary>
        /// Download Protocol
        /// </summary>
        public enum DownloadProtocol : byte {
            BDXSynchronous = 0x0,
            BDXAsynchronous = 0x1,
            HTTPS = 0x2,
            VendorSpecific = 0x3,
        }

        /// <summary>
        /// Announcement Reason
        /// </summary>
        public enum AnnouncementReason : byte {
            /// <summary>
            /// An OTA Provider is announcing its presence.
            /// </summary>
            SimpleAnnouncement = 0x0,
            /// <summary>
            /// An OTA Provider is announcing, either to a single Node or to a group of Nodes, that a new Software Image MAY be available.
            /// </summary>
            UpdateAvailable = 0x1,
            /// <summary>
            /// An OTA Provider is announcing, either to a single Node or to a group of Nodes, that a new Software Image MAY be available, which contains an update that needs to be applied urgently.
            /// </summary>
            UrgentUpdateAvailable = 0x2,
        }

        /// <summary>
        /// Update State
        /// </summary>
        public enum UpdateState : byte {
            /// <summary>
            /// Current state is not yet determined.
            /// </summary>
            Unknown = 0x0,
            /// <summary>
            /// Indicate a Node not yet in the process of software update.
            /// </summary>
            Idle = 0x1,
            /// <summary>
            /// Indicate a Node in the process of querying an OTA Provider.
            /// </summary>
            Querying = 0x2,
            /// <summary>
            /// Indicate a Node waiting after a Busy response.
            /// </summary>
            DelayedOnQuery = 0x3,
            /// <summary>
            /// Indicate a Node currently in the process of downloading a software update.
            /// </summary>
            Downloading = 0x4,
            /// <summary>
            /// Indicate a Node currently in the process of verifying and applying a software update.
            /// </summary>
            Applying = 0x5,
            /// <summary>
            /// Indicate a Node waiting caused by AwaitNextAction response.
            /// </summary>
            DelayedOnApply = 0x6,
            /// <summary>
            /// Indicate a Node in the process of recovering to a previous version.
            /// </summary>
            RollingBack = 0x7,
            /// <summary>
            /// Indicate a Node is capable of user consent.
            /// </summary>
            DelayedOnUserConsent = 0x8,
        }

        /// <summary>
        /// Change Reason
        /// </summary>
        public enum ChangeReason : byte {
            /// <summary>
            /// The reason for a state change is unknown.
            /// </summary>
            Unknown = 0x0,
            /// <summary>
            /// The reason for a state change is the success of a prior operation.
            /// </summary>
            Success = 0x1,
            /// <summary>
            /// The reason for a state change is the failure of a prior operation.
            /// </summary>
            Failure = 0x2,
            /// <summary>
            /// The reason for a state change is a time-out.
            /// </summary>
            TimeOut = 0x3,
            /// <summary>
            /// The reason for a state change is a request by the OTA Provider to wait.
            /// </summary>
            DelayByProvider = 0x4,
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
        private record AnnounceOTAProviderPayload : TLVPayload {
            public required ulong ProviderNodeID { get; set; }
            public required ushort VendorID { get; set; }
            public required AnnouncementReason AnnouncementReason { get; set; }
            public byte[]? MetadataForNode { get; set; }
            public required ushort Endpoint { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, ProviderNodeID);
                writer.WriteUShort(1, VendorID);
                writer.WriteUShort(2, (ushort)AnnouncementReason);
                if (MetadataForNode != null)
                    writer.WriteBytes(3, MetadataForNode, 512);
                writer.WriteUShort(4, Endpoint);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Announce OTA Provider
        /// </summary>
        public async Task<bool> AnnounceOTAProvider(SecureSession session, ulong providerNodeID, ushort vendorID, AnnouncementReason announcementReason, byte[]? metadataForNode, ushort endpoint) {
            AnnounceOTAProviderPayload requestFields = new AnnounceOTAProviderPayload() {
                ProviderNodeID = providerNodeID,
                VendorID = vendorID,
                AnnouncementReason = announcementReason,
                MetadataForNode = metadataForNode,
                Endpoint = endpoint,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Get the Default OTA Providers attribute
        /// </summary>
        public async Task<ProviderLocation[]> GetDefaultOTAProviders(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            ProviderLocation[] list = new ProviderLocation[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new ProviderLocation(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Set the Default OTA Providers attribute
        /// </summary>
        public async Task SetDefaultOTAProviders (SecureSession session, ProviderLocation[] value) {
            await SetAttribute(session, 0, value);
        }

        /// <summary>
        /// Get the Update Possible attribute
        /// </summary>
        public async Task<bool> GetUpdatePossible(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 1) ?? true;
        }

        /// <summary>
        /// Get the Update State attribute
        /// </summary>
        public async Task<UpdateState> GetUpdateState(SecureSession session) {
            return (UpdateState?)await GetEnumAttribute(session, 2) ?? UpdateState.Unknown;
        }

        /// <summary>
        /// Get the Update State Progress attribute
        /// </summary>
        public async Task<byte?> GetUpdateStateProgress(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 3, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "OTA Software Update Requestor";
        }
    }
}