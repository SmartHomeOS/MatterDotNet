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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Microwave Oven Control Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class MicrowaveOvenControlCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x005F;

        /// <summary>
        /// Microwave Oven Control Cluster
        /// </summary>
        public MicrowaveOvenControlCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected MicrowaveOvenControlCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Power is specified as a unitless number or a percentage
            /// </summary>
            PowerAsNumber = 1,
            /// <summary>
            /// Power is specified in Watts
            /// </summary>
            PowerInWatts = 2,
            /// <summary>
            /// Supports the limit attributes used with the PWRNUM feature
            /// </summary>
            PowerNumberLimits = 4,
        }
        #endregion Enums

        #region Payloads
        private record SetCookingParametersPayload : TLVPayload {
            public byte? CookMode { get; set; }
            public TimeSpan? CookTime { get; set; } = TimeSpan.FromSeconds(30);
            public byte? PowerSetting { get; set; } = 100;
            public byte? WattSettingIndex { get; set; }
            public bool? StartAfterSetting { get; set; } = false;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (CookMode != null)
                    writer.WriteByte(0, CookMode);
                if (CookTime != null)
                    writer.WriteUInt(1, (uint)CookTime!.Value.TotalSeconds, uint.MaxValue, 1);
                if (PowerSetting != null)
                    writer.WriteByte(2, PowerSetting);
                if (WattSettingIndex != null)
                    writer.WriteByte(3, WattSettingIndex);
                if (StartAfterSetting != null)
                    writer.WriteBool(4, StartAfterSetting);
                writer.EndContainer();
            }
        }

        private record AddMoreTimePayload : TLVPayload {
            public required TimeSpan TimeToAdd { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)TimeToAdd.TotalSeconds, uint.MaxValue, 1);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Set Cooking Parameters
        /// </summary>
        public async Task<bool> SetCookingParameters(SecureSession session, byte? CookMode, TimeSpan? CookTime, byte? PowerSetting, byte? WattSettingIndex, bool? StartAfterSetting) {
            SetCookingParametersPayload requestFields = new SetCookingParametersPayload() {
                CookMode = CookMode,
                CookTime = CookTime,
                PowerSetting = PowerSetting,
                WattSettingIndex = WattSettingIndex,
                StartAfterSetting = StartAfterSetting,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Add More Time
        /// </summary>
        public async Task<bool> AddMoreTime(SecureSession session, TimeSpan TimeToAdd) {
            AddMoreTimePayload requestFields = new AddMoreTimePayload() {
                TimeToAdd = TimeToAdd,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
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
        /// Get the Cook Time attribute
        /// </summary>
        public async Task<TimeSpan> GetCookTime(SecureSession session) {
            return (TimeSpan?)(dynamic?)await GetAttribute(session, 0) ?? TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Get the Max Cook Time attribute
        /// </summary>
        public async Task<TimeSpan> GetMaxCookTime(SecureSession session) {
            return (TimeSpan)(dynamic?)(await GetAttribute(session, 1))!;
        }

        /// <summary>
        /// Get the Power Setting attribute
        /// </summary>
        public async Task<byte> GetPowerSetting(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 2))!;
        }

        /// <summary>
        /// Get the Min Power attribute
        /// </summary>
        public async Task<byte> GetMinPower(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 3) ?? 10;
        }

        /// <summary>
        /// Get the Max Power attribute
        /// </summary>
        public async Task<byte> GetMaxPower(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 4) ?? 100;
        }

        /// <summary>
        /// Get the Power Step attribute
        /// </summary>
        public async Task<byte> GetPowerStep(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 5) ?? 10;
        }

        /// <summary>
        /// Get the Supported Watts attribute
        /// </summary>
        public async Task<ushort[]> GetSupportedWatts(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 6))!);
            ushort[] list = new ushort[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetUShort(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Selected Watt Index attribute
        /// </summary>
        public async Task<byte> GetSelectedWattIndex(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 7))!;
        }

        /// <summary>
        /// Get the Watt Rating attribute
        /// </summary>
        public async Task<ushort> GetWattRating(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 8))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Microwave Oven Control Cluster";
        }
    }
}