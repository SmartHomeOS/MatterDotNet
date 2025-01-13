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

using MatterDotNet.Messages;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Ecosystem Information Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class EcosystemInformationCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0750;

        /// <summary>
        /// Ecosystem Information Cluster
        /// </summary>
        public EcosystemInformationCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected EcosystemInformationCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Records
        /// <summary>
        /// Ecosystem Device
        /// </summary>
        public record EcosystemDevice : TLVPayload {
            /// <summary>
            /// Ecosystem Device
            /// </summary>
            public EcosystemDevice() { }

            /// <summary>
            /// Ecosystem Device
            /// </summary>
            [SetsRequiredMembers]
            public EcosystemDevice(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                DeviceName = reader.GetString(0, true);
                DeviceNameLastEdit = TimeUtil.FromEpochUS(reader.GetULong(1, true));
                BridgedEndpoint = reader.GetUShort(2, true);
                OriginalEndpoint = reader.GetUShort(3, true);
                {
                    DeviceTypes = new DescriptorCluster.DeviceType[((object[])fields[4]).Length];
                    for (int i = 0; i < DeviceTypes.Length; i++) {
                        DeviceTypes[i] = new DescriptorCluster.DeviceType((object[])fields[-1]);
                    }
                }
                {
                    UniqueLocationIDs = new string[((object[])fields[5]).Length];
                    for (int i = 0; i < UniqueLocationIDs.Length; i++) {
                        UniqueLocationIDs[i] = reader.GetString(-1, false)!;
                    }
                }
                UniqueLocationIDsLastEdit = TimeUtil.FromEpochUS(reader.GetULong(6))!.Value;
            }
            public string? DeviceName { get; set; } = "";
            public DateTime? DeviceNameLastEdit { get; set; } = TimeUtil.EPOCH;
            public ushort? BridgedEndpoint { get; set; }
            public ushort? OriginalEndpoint { get; set; }
            public required DescriptorCluster.DeviceType[] DeviceTypes { get; set; }
            public required string[] UniqueLocationIDs { get; set; }
            public required DateTime UniqueLocationIDsLastEdit { get; set; } = TimeUtil.EPOCH;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (DeviceName != null)
                    writer.WriteString(0, DeviceName, 64);
                if (DeviceNameLastEdit != null)
                    writer.WriteULong(1, TimeUtil.ToEpochUS(DeviceNameLastEdit!.Value));
                if (BridgedEndpoint != null)
                    writer.WriteUShort(2, BridgedEndpoint);
                if (OriginalEndpoint != null)
                    writer.WriteUShort(3, OriginalEndpoint);
                {
                    writer.StartArray(4);
                    foreach (var item in DeviceTypes) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                {
                    Constrain(UniqueLocationIDs, 0, 64);
                    writer.StartArray(5);
                    foreach (var item in UniqueLocationIDs) {
                        writer.WriteString(-1, item);
                    }
                    writer.EndContainer();
                }
                writer.WriteULong(6, TimeUtil.ToEpochUS(UniqueLocationIDsLastEdit));
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Ecosystem Location
        /// </summary>
        public record EcosystemLocation : TLVPayload {
            /// <summary>
            /// Ecosystem Location
            /// </summary>
            public EcosystemLocation() { }

            /// <summary>
            /// Ecosystem Location
            /// </summary>
            [SetsRequiredMembers]
            public EcosystemLocation(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                UniqueLocationID = reader.GetString(0, false)!;
                LocationDescriptor = new LocationDescriptor((object[])fields[1]);
                LocationDescriptorLastEdit = TimeUtil.FromEpochUS(reader.GetULong(2))!.Value;
            }
            public required string UniqueLocationID { get; set; }
            public required LocationDescriptor LocationDescriptor { get; set; }
            public required DateTime LocationDescriptorLastEdit { get; set; } = TimeUtil.EPOCH;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, UniqueLocationID, 64);
                LocationDescriptor.Serialize(writer, 1);
                writer.WriteULong(2, TimeUtil.ToEpochUS(LocationDescriptorLastEdit));
                writer.EndContainer();
            }
        }
        #endregion Records


        /// <inheritdoc />
        public override string ToString() {
            return "Ecosystem Information Cluster";
        }
    }
}