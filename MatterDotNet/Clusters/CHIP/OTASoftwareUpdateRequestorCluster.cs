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
        [SetsRequiredMembers]
        public OTASoftwareUpdateRequestor(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected OTASoftwareUpdateRequestor(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            DefaultOTAProviders = new ReadWriteAttribute<ProviderLocation[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ProviderLocation[] list = new ProviderLocation[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new ProviderLocation(reader.GetStruct(i)!);
                    return list;
                }
            };
            UpdatePossible = new ReadAttribute<bool>(cluster, endPoint, 1) {
                Deserialize = x => (bool?)(dynamic?)x ?? true

            };
            UpdateState = new ReadAttribute<UpdateStateEnum>(cluster, endPoint, 2) {
                Deserialize = x => (UpdateStateEnum?)DeserializeEnum(x) ?? UpdateStateEnum.Unknown

            };
            UpdateStateProgress = new ReadAttribute<byte?>(cluster, endPoint, 3, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
        }

        #region Enums
        /// <summary>
        /// Status
        /// </summary>
        public enum Status : byte {
            UpdateAvailable = 0,
            Busy = 1,
            NotAvailable = 2,
            DownloadProtocolNotSupported = 3,
        }

        /// <summary>
        /// Apply Update Action
        /// </summary>
        public enum ApplyUpdateAction : byte {
            Proceed = 0,
            AwaitNextAction = 1,
            Discontinue = 2,
        }

        /// <summary>
        /// Download Protocol
        /// </summary>
        public enum DownloadProtocol : byte {
            BDXSynchronous = 0,
            BDXAsynchronous = 1,
            HTTPS = 2,
            VendorSpecific = 3,
        }

        /// <summary>
        /// Announcement Reason
        /// </summary>
        public enum AnnouncementReason : byte {
            /// <summary>
            /// An OTA Provider is announcing its presence.
            /// </summary>
            SimpleAnnouncement = 0,
            /// <summary>
            /// An OTA Provider is announcing, either to a single Node or to a group of Nodes, that a new Software Image MAY be available.
            /// </summary>
            UpdateAvailable = 1,
            /// <summary>
            /// An OTA Provider is announcing, either to a single Node or to a group of Nodes, that a new Software Image MAY be available, which contains an update that needs to be applied urgently.
            /// </summary>
            UrgentUpdateAvailable = 2,
        }

        /// <summary>
        /// Update State
        /// </summary>
        public enum UpdateStateEnum : byte {
            /// <summary>
            /// Current state is not yet determined.
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// Indicate a Node not yet in the process of software update.
            /// </summary>
            Idle = 1,
            /// <summary>
            /// Indicate a Node in the process of querying an OTA Provider.
            /// </summary>
            Querying = 2,
            /// <summary>
            /// Indicate a Node waiting after a Busy response.
            /// </summary>
            DelayedOnQuery = 3,
            /// <summary>
            /// Indicate a Node currently in the process of downloading a software update.
            /// </summary>
            Downloading = 4,
            /// <summary>
            /// Indicate a Node currently in the process of verifying and applying a software update.
            /// </summary>
            Applying = 5,
            /// <summary>
            /// Indicate a Node waiting caused by AwaitNextAction response.
            /// </summary>
            DelayedOnApply = 6,
            /// <summary>
            /// Indicate a Node in the process of recovering to a previous version.
            /// </summary>
            RollingBack = 7,
            /// <summary>
            /// Indicate a Node is capable of user consent.
            /// </summary>
            DelayedOnUserConsent = 8,
        }

        /// <summary>
        /// Change Reason
        /// </summary>
        public enum ChangeReason : byte {
            /// <summary>
            /// The reason for a state change is unknown.
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// The reason for a state change is the success of a prior operation.
            /// </summary>
            Success = 1,
            /// <summary>
            /// The reason for a state change is the failure of a prior operation.
            /// </summary>
            Failure = 2,
            /// <summary>
            /// The reason for a state change is a time-out.
            /// </summary>
            TimeOut = 3,
            /// <summary>
            /// The reason for a state change is a request by the OTA Provider to wait.
            /// </summary>
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
        /// Default OTA Providers Attribute
        /// </summary>
        public required ReadWriteAttribute<ProviderLocation[]> DefaultOTAProviders { get; init; }

        /// <summary>
        /// Update Possible Attribute
        /// </summary>
        public required ReadAttribute<bool> UpdatePossible { get; init; }

        /// <summary>
        /// Update State Attribute
        /// </summary>
        public required ReadAttribute<UpdateStateEnum> UpdateState { get; init; }

        /// <summary>
        /// Update State Progress Attribute
        /// </summary>
        public required ReadAttribute<byte?> UpdateStateProgress { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "OTA Software Update Requestor";
        }
    }
}