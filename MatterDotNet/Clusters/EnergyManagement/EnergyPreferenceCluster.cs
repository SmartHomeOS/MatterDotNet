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

namespace MatterDotNet.Clusters.EnergyManagement
{
    /// <summary>
    /// This cluster provides an interface to specify preferences for how devices should consume energy.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class EnergyPreference : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x009B;

        /// <summary>
        /// This cluster provides an interface to specify preferences for how devices should consume energy.
        /// </summary>
        [SetsRequiredMembers]
        public EnergyPreference(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected EnergyPreference(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            EnergyBalances = new ReadAttribute<Balance[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    Balance[] list = new Balance[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new Balance(reader.GetStruct(i)!);
                    return list;
                }
            };
            CurrentEnergyBalance = new ReadWriteAttribute<byte>(cluster, endPoint, 1) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            EnergyPriorities = new ReadAttribute<EnergyPriority[]>(cluster, endPoint, 2) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    EnergyPriority[] list = new EnergyPriority[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = (EnergyPriority)reader.GetUShort(i)!.Value;
                    return list;
                }
            };
            LowPowerModeSensitivities = new ReadAttribute<Balance[]>(cluster, endPoint, 3) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    Balance[] list = new Balance[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new Balance(reader.GetStruct(i)!);
                    return list;
                }
            };
            CurrentLowPowerModeSensitivity = new ReadWriteAttribute<byte>(cluster, endPoint, 4) {
                Deserialize = x => (byte)(dynamic?)x!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Device can balance energy consumption vs. another priority
            /// </summary>
            EnergyBalance = 1,
            /// <summary>
            /// Device can adjust the conditions for entering a low power mode
            /// </summary>
            LowPowerModeSensitivity = 2,
        }

        /// <summary>
        /// Energy Priority
        /// </summary>
        public enum EnergyPriority : byte {
            /// <summary>
            /// User comfort
            /// </summary>
            Comfort = 0,
            /// <summary>
            /// Speed of operation
            /// </summary>
            Speed = 1,
            /// <summary>
            /// Amount of Energy consumed by the device
            /// </summary>
            Efficiency = 2,
            /// <summary>
            /// Amount of water consumed by the device
            /// </summary>
            WaterConsumption = 3,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Balance
        /// </summary>
        public record Balance : TLVPayload {
            /// <summary>
            /// Balance
            /// </summary>
            public Balance() { }

            /// <summary>
            /// Balance
            /// </summary>
            [SetsRequiredMembers]
            public Balance(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Step = reader.GetByte(0)!.Value;
                Label = reader.GetString(1, true, 64);
            }
            public required byte Step { get; set; }
            public string? Label { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Step);
                if (Label != null)
                    writer.WriteString(1, Label, 64);
                writer.EndContainer();
            }
        }
        #endregion Records

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
        /// Energy Balances Attribute
        /// </summary>
        public required ReadAttribute<Balance[]> EnergyBalances { get; init; }

        /// <summary>
        /// Current Energy Balance Attribute
        /// </summary>
        public required ReadWriteAttribute<byte> CurrentEnergyBalance { get; init; }

        /// <summary>
        /// Energy Priorities Attribute
        /// </summary>
        public required ReadAttribute<EnergyPriority[]> EnergyPriorities { get; init; }

        /// <summary>
        /// Low Power Mode Sensitivities Attribute
        /// </summary>
        public required ReadAttribute<Balance[]> LowPowerModeSensitivities { get; init; }

        /// <summary>
        /// Current Low Power Mode Sensitivity Attribute
        /// </summary>
        public required ReadWriteAttribute<byte> CurrentLowPowerModeSensitivity { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Energy Preference";
        }
    }
}