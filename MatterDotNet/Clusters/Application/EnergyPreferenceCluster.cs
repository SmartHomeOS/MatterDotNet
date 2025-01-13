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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Energy Preference Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class EnergyPreferenceCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x009B;

        /// <summary>
        /// Energy Preference Cluster
        /// </summary>
        public EnergyPreferenceCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected EnergyPreferenceCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum EnergyPriorityEnum {
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
                Label = reader.GetString(1, true);
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
        /// Get the Energy Balances attribute
        /// </summary>
        public async Task<Balance[]> GetEnergyBalances(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            Balance[] list = new Balance[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new Balance(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Current Energy Balance attribute
        /// </summary>
        public async Task<byte> GetCurrentEnergyBalance(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 1))!;
        }

        /// <summary>
        /// Set the Current Energy Balance attribute
        /// </summary>
        public async Task SetCurrentEnergyBalance (SecureSession session, byte value) {
            await SetAttribute(session, 1, value);
        }

        /// <summary>
        /// Get the Energy Priorities attribute
        /// </summary>
        public async Task<EnergyPriorityEnum[]> GetEnergyPriorities(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 2))!);
            EnergyPriorityEnum[] list = new EnergyPriorityEnum[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = (EnergyPriorityEnum)reader.GetUShort(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Low Power Mode Sensitivities attribute
        /// </summary>
        public async Task<Balance[]> GetLowPowerModeSensitivities(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 3))!);
            Balance[] list = new Balance[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new Balance(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Current Low Power Mode Sensitivity attribute
        /// </summary>
        public async Task<byte> GetCurrentLowPowerModeSensitivity(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 4))!;
        }

        /// <summary>
        /// Set the Current Low Power Mode Sensitivity attribute
        /// </summary>
        public async Task SetCurrentLowPowerModeSensitivity (SecureSession session, byte value) {
            await SetAttribute(session, 4, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Energy Preference Cluster";
        }
    }
}