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

using MatterDotNet.Attributes;
using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.NetworkInfrastructure
{
    /// <summary>
    /// Manages the names and credentials of Thread networks visible to the user.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ThreadNetworkDirectory : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0453;

        /// <summary>
        /// Manages the names and credentials of Thread networks visible to the user.
        /// </summary>
        [SetsRequiredMembers]
        public ThreadNetworkDirectory(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ThreadNetworkDirectory(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            PreferredExtendedPanID = new ReadWriteAttribute<byte[]?>(cluster, endPoint, 0, true) {
                Deserialize = x => (byte[]?)(dynamic?)x
            };
            ThreadNetworks = new ReadAttribute<ThreadNetwork[]>(cluster, endPoint, 1) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ThreadNetwork[] list = new ThreadNetwork[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new ThreadNetwork(reader.GetStruct(i)!);
                    return list;
                }
            };
            ThreadNetworkTableSize = new ReadAttribute<byte>(cluster, endPoint, 2) {
                Deserialize = x => (byte)(dynamic?)x!
            };
        }

        #region Records
        /// <summary>
        /// Thread Network
        /// </summary>
        public record ThreadNetwork : TLVPayload {
            /// <summary>
            /// Thread Network
            /// </summary>
            public ThreadNetwork() { }

            /// <summary>
            /// Thread Network
            /// </summary>
            [SetsRequiredMembers]
            public ThreadNetwork(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ExtendedPanID = reader.GetBytes(0, false, 8)!;
                NetworkName = reader.GetString(1, false, 16)!;
                Channel = reader.GetUShort(2)!.Value;
                ActiveTimestamp = reader.GetULong(3)!.Value;
            }
            public required byte[] ExtendedPanID { get; set; }
            public required string NetworkName { get; set; }
            public required ushort Channel { get; set; }
            public required ulong ActiveTimestamp { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, ExtendedPanID, 8);
                writer.WriteString(1, NetworkName, 16);
                writer.WriteUShort(2, Channel);
                writer.WriteULong(3, ActiveTimestamp);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record AddNetworkPayload : TLVPayload {
            public required byte[] OperationalDataset { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, OperationalDataset, 254);
                writer.EndContainer();
            }
        }

        private record RemoveNetworkPayload : TLVPayload {
            public required byte[] ExtendedPanID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, ExtendedPanID, 8);
                writer.EndContainer();
            }
        }

        private record GetOperationalDatasetPayload : TLVPayload {
            public required byte[] ExtendedPanID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, ExtendedPanID, 8);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Operational Dataset Response - Reply from server
        /// </summary>
        public struct OperationalDatasetResponse() {
            public required byte[] OperationalDataset { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Add Network
        /// </summary>
        public async Task<bool> AddNetwork(SecureSession session, ushort commandTimeoutMS, byte[] operationalDataset, CancellationToken token = default) {
            AddNetworkPayload requestFields = new AddNetworkPayload() {
                OperationalDataset = operationalDataset,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x00, commandTimeoutMS, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Remove Network
        /// </summary>
        public async Task<bool> RemoveNetwork(SecureSession session, ushort commandTimeoutMS, byte[] extendedPanID, CancellationToken token = default) {
            RemoveNetworkPayload requestFields = new RemoveNetworkPayload() {
                ExtendedPanID = extendedPanID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x01, commandTimeoutMS, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Operational Dataset
        /// </summary>
        public async Task<OperationalDatasetResponse?> GetOperationalDataset(SecureSession session, byte[] extendedPanID, CancellationToken token = default) {
            GetOperationalDatasetPayload requestFields = new GetOperationalDatasetPayload() {
                ExtendedPanID = extendedPanID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new OperationalDatasetResponse() {
                OperationalDataset = (byte[])GetField(resp, 0),
            };
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Preferred Extended Pan ID Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte[]?> PreferredExtendedPanID { get; init; }

        /// <summary>
        /// Thread Networks Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ThreadNetwork[]> ThreadNetworks { get; init; }

        /// <summary>
        /// Thread Network Table Size Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> ThreadNetworkTableSize { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Thread Network Directory";
        }
    }
}