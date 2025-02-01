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
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.MeasurementAndSensing
{
    /// <summary>
    /// Attributes and commands for monitoring activated carbon filters in a device
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ActivatedCarbonFilterMonitoring : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0072;

        /// <summary>
        /// Attributes and commands for monitoring activated carbon filters in a device
        /// </summary>
        [SetsRequiredMembers]
        public ActivatedCarbonFilterMonitoring(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ActivatedCarbonFilterMonitoring(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            Condition = new ReadAttribute<byte>(cluster, endPoint, 0) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            DegradationDirection = new ReadAttribute<DegradationDirectionEnum>(cluster, endPoint, 1) {
                Deserialize = x => (DegradationDirectionEnum)DeserializeEnum(x)!
            };
            ChangeIndication = new ReadAttribute<ChangeIndicationEnum>(cluster, endPoint, 2) {
                Deserialize = x => (ChangeIndicationEnum)DeserializeEnum(x)!
            };
            InPlaceIndicator = new ReadAttribute<bool>(cluster, endPoint, 3) {
                Deserialize = x => (bool)(dynamic?)x!
            };
            LastChangedTime = new ReadWriteAttribute<DateTime?>(cluster, endPoint, 4, true) {
                Deserialize = x => TimeUtil.FromEpochSeconds((uint)(dynamic?)x)
            };
            ReplacementProductList = new ReadAttribute<ReplacementProduct[]>(cluster, endPoint, 5) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ReplacementProduct[] list = new ReplacementProduct[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new ReplacementProduct(reader.GetStruct(i)!);
                    return list;
                }
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports monitoring the condition of the resource in percentage
            /// </summary>
            Condition = 1,
            /// <summary>
            /// Supports warning indication
            /// </summary>
            Warning = 2,
            /// <summary>
            /// Supports specifying the list of replacement products
            /// </summary>
            ReplacementProductList = 4,
        }

        /// <summary>
        /// Degradation Direction
        /// </summary>
        public enum DegradationDirectionEnum : byte {
            Up = 0,
            Down = 1,
        }

        /// <summary>
        /// Change Indication
        /// </summary>
        public enum ChangeIndicationEnum : byte {
            OK = 0,
            Warning = 1,
            Critical = 2,
        }

        /// <summary>
        /// Product Identifier Type
        /// </summary>
        public enum ProductIdentifierType : byte {
            UPC = 0,
            GTIN8 = 1,
            EAN = 2,
            GTIN14 = 3,
            OEM = 4,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Replacement Product
        /// </summary>
        public record ReplacementProduct : TLVPayload {
            /// <summary>
            /// Replacement Product
            /// </summary>
            public ReplacementProduct() { }

            /// <summary>
            /// Replacement Product
            /// </summary>
            [SetsRequiredMembers]
            public ReplacementProduct(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ProductIdentifierType = (ProductIdentifierType)reader.GetUShort(0)!.Value;
                ProductIdentifierValue = reader.GetString(1, false, 20)!;
            }
            public required ProductIdentifierType ProductIdentifierType { get; set; }
            public required string ProductIdentifierValue { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)ProductIdentifierType);
                writer.WriteString(1, ProductIdentifierValue, 20);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Commands
        /// <summary>
        /// Reset Condition
        /// </summary>
        public async Task<bool> ResetCondition(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, null, token);
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
        /// Condition [%] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> Condition { get; init; }

        /// <summary>
        /// Degradation Direction Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<DegradationDirectionEnum> DegradationDirection { get; init; }

        /// <summary>
        /// Change Indication Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ChangeIndicationEnum> ChangeIndication { get; init; }

        /// <summary>
        /// In Place Indicator Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool> InPlaceIndicator { get; init; }

        /// <summary>
        /// Last Changed Time Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<DateTime?> LastChangedTime { get; init; }

        /// <summary>
        /// Replacement Product List Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ReplacementProduct[]> ReplacementProductList { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Activated Carbon Filter Monitoring";
        }
    }
}