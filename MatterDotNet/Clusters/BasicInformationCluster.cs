// MatterDotNet Copyright (C) 2024 
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

namespace MatterDotNet.Clusters
{
    /// <summary>
    /// Basic Information Cluster
    /// </summary>
    public class BasicInformationCluster : ClusterBase
    {
        private const uint CLUSTER_ID = 0x0028;

        /// <summary>
        /// Basic Information Cluster
        /// </summary>
        public BasicInformationCluster(ushort endPoint) : base(endPoint) { }

        #region Enums
        /// <summary>
        /// Color
        /// </summary>
        public enum ColorEnum {
            /// <summary>
            /// Approximately RGB #000000.
            /// </summary>
            Black = 0,
            /// <summary>
            /// Approximately RGB #000080.
            /// </summary>
            Navy = 1,
            /// <summary>
            /// Approximately RGB #008000.
            /// </summary>
            Green = 2,
            /// <summary>
            /// Approximately RGB #008080.
            /// </summary>
            Teal = 3,
            /// <summary>
            /// Approximately RGB #800080.
            /// </summary>
            Maroon = 4,
            /// <summary>
            /// Approximately RGB #800080.
            /// </summary>
            Purple = 5,
            /// <summary>
            /// Approximately RGB #808000.
            /// </summary>
            Olive = 6,
            /// <summary>
            /// Approximately RGB #808080.
            /// </summary>
            Gray = 7,
            /// <summary>
            /// Approximately RGB #0000FF.
            /// </summary>
            Blue = 8,
            /// <summary>
            /// Approximately RGB #00FF00.
            /// </summary>
            Lime = 9,
            /// <summary>
            /// Approximately RGB #00FFFF.
            /// </summary>
            Aqua = 10,
            /// <summary>
            /// Approximately RGB #FF0000.
            /// </summary>
            Red = 11,
            /// <summary>
            /// Approximately RGB #FF00FF.
            /// </summary>
            Fuchsia = 12,
            /// <summary>
            /// Approximately RGB #FFFF00.
            /// </summary>
            Yellow = 13,
            /// <summary>
            /// Approximately RGB #FFFFFF.
            /// </summary>
            White = 14,
            /// <summary>
            /// Typical hardware "Nickel" color.
            /// </summary>
            Nickel = 15,
            /// <summary>
            /// Typical hardware "Chrome" color.
            /// </summary>
            Chrome = 16,
            /// <summary>
            /// Typical hardware "Brass" color.
            /// </summary>
            Brass = 17,
            /// <summary>
            /// Typical hardware "Copper" color.
            /// </summary>
            Copper = 18,
            /// <summary>
            /// Typical hardware "Silver" color.
            /// </summary>
            Silver = 19,
            /// <summary>
            /// Typical hardware "Gold" color.
            /// </summary>
            Gold = 20,
        }

        /// <summary>
        /// Product Finish
        /// </summary>
        public enum ProductFinishEnum {
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
        #endregion Enums

        #region Records
        public record CapabilityMinima : TLVPayload {
            public ushort? CaseSessionsPerFabric { get; set; } = 3;
            public ushort? SubscriptionsPerFabric { get; set; } = 3;
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (CaseSessionsPerFabric != null)
                    writer.WriteUShort(0, CaseSessionsPerFabric);
                if (SubscriptionsPerFabric != null)
                    writer.WriteUShort(1, SubscriptionsPerFabric);
                writer.EndContainer();
            }
        }

        public record ProductAppearance : TLVPayload {
            public required ProductFinishEnum Finish { get; set; }
            public required ColorEnum PrimaryColor { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Finish);
                writer.WriteUShort(1, (ushort)PrimaryColor);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Attributes
        public ushort DataModelRevision { get; }

        public string VendorName { get; }

        public ushort VendorID { get; }

        public string ProductName { get; }

        public ushort ProductID { get; }

        public string NodeLabel { get; set; } = "";

        public string Location { get; set; } = "XX";

        public ushort HardwareVersion { get; } = 0;

        public string HardwareVersionString { get; }

        public uint SoftwareVersion { get; } = 0;

        public string SoftwareVersionString { get; }

        public string ManufacturingDate { get; }

        public string PartNumber { get; }

        public string ProductURL { get; }

        public string ProductLabel { get; }

        public string SerialNumber { get; }

        public bool LocalConfigDisabled { get; set; } = false;

        public bool Reachable { get; } = true;

        public string UniqueID { get; }

        public CapabilityMinima CapabilityMinimaField { get; }

        public ProductAppearance ProductAppearanceField { get; }

        public uint SpecificationVersion { get; } = 0;

        public ushort MaxPathsPerInvoke { get; } = 1;
        #endregion Attributes
    }
}