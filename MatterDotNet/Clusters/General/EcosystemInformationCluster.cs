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

using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// Provides extended device information for all the logical devices represented by a Bridged Node.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class EcosystemInformation : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0750;

        /// <summary>
        /// Provides extended device information for all the logical devices represented by a Bridged Node.
        /// </summary>
        [SetsRequiredMembers]
        public EcosystemInformation(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected EcosystemInformation(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            DeviceDirectory = new ReadAttribute<EcosystemDevice[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    EcosystemDevice[] list = new EcosystemDevice[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new EcosystemDevice(reader.GetStruct(i)!);
                    return list;
                }
            };
            LocationDirectory = new ReadAttribute<EcosystemLocation[]>(cluster, endPoint, 1) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    EcosystemLocation[] list = new EcosystemLocation[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new EcosystemLocation(reader.GetStruct(i)!);
                    return list;
                }
            };
        }

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
                DeviceName = reader.GetString(0, true, 64);
                DeviceNameLastEdit = TimeUtil.FromEpochUS(reader.GetULong(1, true));
                BridgedEndpoint = reader.GetUShort(2)!.Value;
                OriginalEndpoint = reader.GetUShort(3)!.Value;
                {
                    DeviceTypes = new Descriptor.DeviceType[reader.GetStruct(4)!.Length];
                    for (int n = 0; n < DeviceTypes.Length; n++) {
                        DeviceTypes[n] = new Descriptor.DeviceType((object[])((object[])fields[4])[n]);
                    }
                }
                {
                    UniqueLocationIDs = new string[reader.GetStruct(5)!.Length];
                    for (int n = 0; n < UniqueLocationIDs.Length; n++) {
                        UniqueLocationIDs[n] = reader.GetString(n, false)!;
                    }
                }
                UniqueLocationIDsLastEdit = TimeUtil.FromEpochUS(reader.GetULong(6))!.Value;
            }
            public string? DeviceName { get; set; }
            public DateTime? DeviceNameLastEdit { get; set; } = TimeUtil.EPOCH;
            public required ushort BridgedEndpoint { get; set; }
            public required ushort OriginalEndpoint { get; set; }
            public required Descriptor.DeviceType[] DeviceTypes { get; set; }
            public required string[] UniqueLocationIDs { get; set; }
            public required DateTime UniqueLocationIDsLastEdit { get; set; } = TimeUtil.EPOCH;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (DeviceName != null)
                    writer.WriteString(0, DeviceName, 64);
                if (DeviceNameLastEdit != null)
                    writer.WriteULong(1, TimeUtil.ToEpochUS(DeviceNameLastEdit!.Value));
                writer.WriteUShort(2, BridgedEndpoint);
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
                UniqueLocationID = reader.GetString(0, false, 64)!;
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

        #region Attributes
        /// <summary>
        /// Device Directory Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<EcosystemDevice[]> DeviceDirectory { get; init; }

        /// <summary>
        /// Location Directory Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<EcosystemLocation[]> LocationDirectory { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Ecosystem Information";
        }
    }
}