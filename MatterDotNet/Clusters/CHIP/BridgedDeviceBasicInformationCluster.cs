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
    /// This Cluster serves two purposes towards a Node communicating with a Bridge: indicate that the functionality on the Endpoint where it is placed (and its Parts) is bridged from a non-CHIP technology; and provide a centralized collection of attributes that the Node MAY collect to aid in conveying information regarding the Bridged Device to a user, such as the vendor name, the model name, or user-assigned name.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class BridgedDeviceBasicInformation : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0039;

        /// <summary>
        /// This Cluster serves two purposes towards a Node communicating with a Bridge: indicate that the functionality on the Endpoint where it is placed (and its Parts) is bridged from a non-CHIP technology; and provide a centralized collection of attributes that the Node MAY collect to aid in conveying information regarding the Bridged Device to a user, such as the vendor name, the model name, or user-assigned name.
        /// </summary>
        [SetsRequiredMembers]
        public BridgedDeviceBasicInformation(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected BridgedDeviceBasicInformation(uint cluster, ushort endPoint) : base(cluster, endPoint) {
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
            Reachable = new ReadAttribute<bool>(cluster, endPoint, 23) {
                Deserialize = x => (bool?)(dynamic?)x ?? true

            };
            UniqueID = new ReadAttribute<string>(cluster, endPoint, 24) {
                Deserialize = x => (string)(dynamic?)x!
            };
            ProductAppearance = new ReadAttribute<ProductAppearanceStruct>(cluster, endPoint, 32) {
                Deserialize = x => new ProductAppearanceStruct((object[])x!)
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Support Bridged ICD Devices.
            /// </summary>
            BridgedICDSupport = 1048576,
        }

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

        #region Payloads
        private record KeepActivePayload : TLVPayload {
            public required uint StayActiveDuration { get; set; }
            public required uint TimeoutMs { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, StayActiveDuration);
                writer.WriteUInt(1, TimeoutMs, 3600000, 30000);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Keep Active
        /// </summary>
        public async Task<bool> KeepActive(SecureSession session, uint stayActiveDuration, uint timeoutMs) {
            KeepActivePayload requestFields = new KeepActivePayload() {
                StayActiveDuration = stayActiveDuration,
                TimeoutMs = timeoutMs,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x80, requestFields);
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
        /// Vendor Name Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> VendorName { get; init; }

        /// <summary>
        /// Vendor ID Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> VendorID { get; init; }

        /// <summary>
        /// Product Name Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> ProductName { get; init; }

        /// <summary>
        /// Product ID Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> ProductID { get; init; }

        /// <summary>
        /// Node Label Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<string> NodeLabel { get; init; }

        /// <summary>
        /// Hardware Version Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> HardwareVersion { get; init; }

        /// <summary>
        /// Hardware Version String Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> HardwareVersionString { get; init; }

        /// <summary>
        /// Software Version Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> SoftwareVersion { get; init; }

        /// <summary>
        /// Software Version String Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> SoftwareVersionString { get; init; }

        /// <summary>
        /// Manufacturing Date Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> ManufacturingDate { get; init; }

        /// <summary>
        /// Part Number Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> PartNumber { get; init; }

        /// <summary>
        /// Product URL Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> ProductURL { get; init; }

        /// <summary>
        /// Product Label Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> ProductLabel { get; init; }

        /// <summary>
        /// Serial Number Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> SerialNumber { get; init; }

        /// <summary>
        /// Reachable Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool> Reachable { get; init; }

        /// <summary>
        /// Unique ID Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> UniqueID { get; init; }

        /// <summary>
        /// Product Appearance Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ProductAppearanceStruct> ProductAppearance { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Bridged Device Basic Information";
        }
    }
}