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
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.CHIP
{
    /// <summary>
    /// This cluster provides attributes and events for determining basic information about Nodes, which supports both Commissioning and operational determination of Node characteristics, such as Vendor ID, Product ID and serial number, which apply to the whole Node. Also allows setting user device information such as location.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class BasicInformation : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0028;

        /// <summary>
        /// This cluster provides attributes and events for determining basic information about Nodes, which supports both Commissioning and operational determination of Node characteristics, such as Vendor ID, Product ID and serial number, which apply to the whole Node. Also allows setting user device information such as location.
        /// </summary>
        [SetsRequiredMembers]
        public BasicInformation(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected BasicInformation(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            DataModelRevision = new ReadAttribute<ushort>(cluster, endPoint, 0) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            VendorName = new ReadAttribute<string>(cluster, endPoint, 1) {
                Deserialize = x => (string)(dynamic?)x!
            };
            VendorID = new ReadAttribute<ushort>(cluster, endPoint, 2) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            ProductName = new ReadAttribute<string>(cluster, endPoint, 3) {
                Deserialize = x => (string)(dynamic?)x!
            };
            ProductID = new ReadAttribute<ushort>(cluster, endPoint, 4) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            NodeLabel = new ReadWriteAttribute<string>(cluster, endPoint, 5) {
                Deserialize = x => (string)(dynamic?)x!
            };
            Location = new ReadWriteAttribute<string>(cluster, endPoint, 6) {
                Deserialize = x => (string?)(dynamic?)x ?? "xx"

            };
            HardwareVersion = new ReadAttribute<ushort>(cluster, endPoint, 7) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0

            };
            HardwareVersionString = new ReadAttribute<string>(cluster, endPoint, 8) {
                Deserialize = x => (string)(dynamic?)x!
            };
            SoftwareVersion = new ReadAttribute<uint>(cluster, endPoint, 9) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0

            };
            SoftwareVersionString = new ReadAttribute<string>(cluster, endPoint, 16) {
                Deserialize = x => (string)(dynamic?)x!
            };
            ManufacturingDate = new ReadAttribute<string>(cluster, endPoint, 17) {
                Deserialize = x => (string)(dynamic?)x!
            };
            PartNumber = new ReadAttribute<string>(cluster, endPoint, 18) {
                Deserialize = x => (string)(dynamic?)x!
            };
            ProductURL = new ReadAttribute<string>(cluster, endPoint, 19) {
                Deserialize = x => (string)(dynamic?)x!
            };
            ProductLabel = new ReadAttribute<string>(cluster, endPoint, 20) {
                Deserialize = x => (string)(dynamic?)x!
            };
            SerialNumber = new ReadAttribute<string>(cluster, endPoint, 21) {
                Deserialize = x => (string)(dynamic?)x!
            };
            LocalConfigDisabled = new ReadWriteAttribute<bool>(cluster, endPoint, 22) {
                Deserialize = x => (bool?)(dynamic?)x ?? false

            };
            Reachable = new ReadAttribute<bool>(cluster, endPoint, 23) {
                Deserialize = x => (bool?)(dynamic?)x ?? true

            };
            UniqueID = new ReadAttribute<string>(cluster, endPoint, 24) {
                Deserialize = x => (string)(dynamic?)x!
            };
            CapabilityMinima = new ReadAttribute<CapabilityMinimaStruct>(cluster, endPoint, 25) {
                Deserialize = x => new CapabilityMinimaStruct((object[])x!)
            };
            ProductAppearance = new ReadAttribute<ProductAppearanceStruct>(cluster, endPoint, 32) {
                Deserialize = x => new ProductAppearanceStruct((object[])x!)
            };
            SpecificationVersion = new ReadAttribute<uint>(cluster, endPoint, 33) {
                Deserialize = x => (uint)(dynamic?)x!
            };
            MaxPathsPerInvoke = new ReadAttribute<ushort>(cluster, endPoint, 34) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
        }

        #region Enums
        /// <summary>
        /// Product Finish
        /// </summary>
        public enum ProductFinish : byte {
            /// <summary>
            /// Product has some other finish not listed below.
            /// </summary>
            Other = 0,
            /// <summary>
            /// Product has a matte finish.
            /// </summary>
            Matte = 1,
            /// <summary>
            /// Product has a satin finish.
            /// </summary>
            Satin = 2,
            /// <summary>
            /// Product has a polished or shiny finish.
            /// </summary>
            Polished = 3,
            /// <summary>
            /// Product has a rugged finish.
            /// </summary>
            Rugged = 4,
            /// <summary>
            /// Product has a fabric finish.
            /// </summary>
            Fabric = 5,
        }

        /// <summary>
        /// Color
        /// </summary>
        public enum Color : byte {
            /// <summary>
            /// Approximately RGB #000000.
            /// </summary>
            Black = 0x0,
            /// <summary>
            /// Approximately RGB #000080.
            /// </summary>
            Navy = 0x1,
            /// <summary>
            /// Approximately RGB #008000.
            /// </summary>
            Green = 0x2,
            /// <summary>
            /// Approximately RGB #008080.
            /// </summary>
            Teal = 0x3,
            /// <summary>
            /// Approximately RGB #800080.
            /// </summary>
            Maroon = 0x4,
            /// <summary>
            /// Approximately RGB #800080.
            /// </summary>
            Purple = 0x5,
            /// <summary>
            /// Approximately RGB #808000.
            /// </summary>
            Olive = 0x6,
            /// <summary>
            /// Approximately RGB #808080.
            /// </summary>
            Gray = 0x7,
            /// <summary>
            /// Approximately RGB #0000FF.
            /// </summary>
            Blue = 0x8,
            /// <summary>
            /// Approximately RGB #00FF00.
            /// </summary>
            Lime = 0x9,
            /// <summary>
            /// Approximately RGB #00FFFF.
            /// </summary>
            Aqua = 0xA,
            /// <summary>
            /// Approximately RGB #FF0000.
            /// </summary>
            Red = 0xB,
            /// <summary>
            /// Approximately RGB #FF00FF.
            /// </summary>
            Fuchsia = 0xC,
            /// <summary>
            /// Approximately RGB #FFFF00.
            /// </summary>
            Yellow = 0xD,
            /// <summary>
            /// Approximately RGB #FFFFFF.
            /// </summary>
            White = 0xE,
            /// <summary>
            /// Typical hardware "Nickel" color.
            /// </summary>
            Nickel = 0xF,
            /// <summary>
            /// Typical hardware "Chrome" color.
            /// </summary>
            Chrome = 0x10,
            /// <summary>
            /// Typical hardware "Brass" color.
            /// </summary>
            Brass = 0x11,
            /// <summary>
            /// Typical hardware "Copper" color.
            /// </summary>
            Copper = 0x12,
            /// <summary>
            /// Typical hardware "Silver" color.
            /// </summary>
            Silver = 0x13,
            /// <summary>
            /// Typical hardware "Gold" color.
            /// </summary>
            Gold = 0x14,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Capability Minima
        /// </summary>
        public record CapabilityMinimaStruct : TLVPayload {
            /// <summary>
            /// Capability Minima
            /// </summary>
            public CapabilityMinimaStruct() { }

            /// <summary>
            /// Capability Minima
            /// </summary>
            [SetsRequiredMembers]
            public CapabilityMinimaStruct(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                CaseSessionsPerFabric = reader.GetUShort(0)!.Value;
                SubscriptionsPerFabric = reader.GetUShort(1)!.Value;
            }
            public required ushort CaseSessionsPerFabric { get; set; }
            public required ushort SubscriptionsPerFabric { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, CaseSessionsPerFabric);
                writer.WriteUShort(1, SubscriptionsPerFabric);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Product Appearance
        /// </summary>
        public record ProductAppearanceStruct : TLVPayload {
            /// <summary>
            /// Product Appearance
            /// </summary>
            public ProductAppearanceStruct() { }

            /// <summary>
            /// Product Appearance
            /// </summary>
            [SetsRequiredMembers]
            public ProductAppearanceStruct(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Finish = (ProductFinish)reader.GetUShort(0)!.Value;
                PrimaryColor = (Color)reader.GetUShort(1)!.Value;
            }
            public required ProductFinish Finish { get; set; }
            public required Color? PrimaryColor { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Finish);
                writer.WriteUShort(1, (ushort?)PrimaryColor);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Attributes
        /// <summary>
        /// Data Model Revision Attribute
        /// </summary>
        public required ReadAttribute<ushort> DataModelRevision { get; init; }

        /// <summary>
        /// Vendor Name Attribute
        /// </summary>
        public required ReadAttribute<string> VendorName { get; init; }

        /// <summary>
        /// Vendor ID Attribute
        /// </summary>
        public required ReadAttribute<ushort> VendorID { get; init; }

        /// <summary>
        /// Product Name Attribute
        /// </summary>
        public required ReadAttribute<string> ProductName { get; init; }

        /// <summary>
        /// Product ID Attribute
        /// </summary>
        public required ReadAttribute<ushort> ProductID { get; init; }

        /// <summary>
        /// Node Label Attribute
        /// </summary>
        public required ReadWriteAttribute<string> NodeLabel { get; init; }

        /// <summary>
        /// Location Attribute
        /// </summary>
        public required ReadWriteAttribute<string> Location { get; init; }

        /// <summary>
        /// Hardware Version Attribute
        /// </summary>
        public required ReadAttribute<ushort> HardwareVersion { get; init; }

        /// <summary>
        /// Hardware Version String Attribute
        /// </summary>
        public required ReadAttribute<string> HardwareVersionString { get; init; }

        /// <summary>
        /// Software Version Attribute
        /// </summary>
        public required ReadAttribute<uint> SoftwareVersion { get; init; }

        /// <summary>
        /// Software Version String Attribute
        /// </summary>
        public required ReadAttribute<string> SoftwareVersionString { get; init; }

        /// <summary>
        /// Manufacturing Date Attribute
        /// </summary>
        public required ReadAttribute<string> ManufacturingDate { get; init; }

        /// <summary>
        /// Part Number Attribute
        /// </summary>
        public required ReadAttribute<string> PartNumber { get; init; }

        /// <summary>
        /// Product URL Attribute
        /// </summary>
        public required ReadAttribute<string> ProductURL { get; init; }

        /// <summary>
        /// Product Label Attribute
        /// </summary>
        public required ReadAttribute<string> ProductLabel { get; init; }

        /// <summary>
        /// Serial Number Attribute
        /// </summary>
        public required ReadAttribute<string> SerialNumber { get; init; }

        /// <summary>
        /// Local Config Disabled Attribute
        /// </summary>
        public required ReadWriteAttribute<bool> LocalConfigDisabled { get; init; }

        /// <summary>
        /// Reachable Attribute
        /// </summary>
        public required ReadAttribute<bool> Reachable { get; init; }

        /// <summary>
        /// Unique ID Attribute
        /// </summary>
        public required ReadAttribute<string> UniqueID { get; init; }

        /// <summary>
        /// Capability Minima Attribute
        /// </summary>
        public required ReadAttribute<CapabilityMinimaStruct> CapabilityMinima { get; init; }

        /// <summary>
        /// Product Appearance Attribute
        /// </summary>
        public required ReadAttribute<ProductAppearanceStruct> ProductAppearance { get; init; }

        /// <summary>
        /// Specification Version Attribute
        /// </summary>
        public required ReadAttribute<uint> SpecificationVersion { get; init; }

        /// <summary>
        /// Max Paths Per Invoke Attribute
        /// </summary>
        public required ReadAttribute<ushort> MaxPathsPerInvoke { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Basic Information";
        }
    }
}