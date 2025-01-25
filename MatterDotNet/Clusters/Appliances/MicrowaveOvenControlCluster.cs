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

namespace MatterDotNet.Clusters.Appliances
{
    /// <summary>
    /// Attributes and commands for configuring the microwave oven control, and reporting cooking stats.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class MicrowaveOvenControl : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x005F;

        /// <summary>
        /// Attributes and commands for configuring the microwave oven control, and reporting cooking stats.
        /// </summary>
        [SetsRequiredMembers]
        public MicrowaveOvenControl(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected MicrowaveOvenControl(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            CookTime = new ReadAttribute<TimeSpan>(cluster, endPoint, 0) {
                Deserialize = x => (TimeSpan?)(dynamic?)x ?? TimeSpan.FromSeconds(30)

            };
            MaxCookTime = new ReadAttribute<TimeSpan>(cluster, endPoint, 1) {
                Deserialize = x => (TimeSpan)(dynamic?)x!
            };
            PowerSetting = new ReadAttribute<byte>(cluster, endPoint, 2) {
                Deserialize = x => (byte?)(dynamic?)x ?? 100

            };
            MinPower = new ReadAttribute<byte>(cluster, endPoint, 3) {
                Deserialize = x => (byte?)(dynamic?)x ?? 10

            };
            MaxPower = new ReadAttribute<byte>(cluster, endPoint, 4) {
                Deserialize = x => (byte?)(dynamic?)x ?? 100

            };
            PowerStep = new ReadAttribute<byte>(cluster, endPoint, 5) {
                Deserialize = x => (byte?)(dynamic?)x ?? 10

            };
            SupportedWatts = new ReadAttribute<ushort[]>(cluster, endPoint, 6) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ushort[] list = new ushort[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = reader.GetUShort(i)!.Value;
                    return list;
                }
            };
            SelectedWattIndex = new ReadAttribute<byte>(cluster, endPoint, 7) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            WattRating = new ReadAttribute<ushort>(cluster, endPoint, 8) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
        }

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
            public TimeSpan? CookTime { get; set; }
            public byte? PowerSetting { get; set; }
            public byte? WattSettingIndex { get; set; }
            public bool? StartAfterSetting { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (CookMode != null)
                    writer.WriteByte(0, CookMode);
                if (CookTime != null)
                    writer.WriteUInt(1, (uint)CookTime!.Value.TotalSeconds);
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
                writer.WriteUInt(0, (uint)TimeToAdd.TotalSeconds);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Set Cooking Parameters
        /// </summary>
        public async Task<bool> SetCookingParameters(SecureSession session, byte? cookMode, TimeSpan? cookTime, byte? powerSetting, byte? wattSettingIndex, bool? startAfterSetting) {
            SetCookingParametersPayload requestFields = new SetCookingParametersPayload() {
                CookMode = cookMode,
                CookTime = cookTime,
                PowerSetting = powerSetting,
                WattSettingIndex = wattSettingIndex,
                StartAfterSetting = startAfterSetting,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Add More Time
        /// </summary>
        public async Task<bool> AddMoreTime(SecureSession session, TimeSpan timeToAdd) {
            AddMoreTimePayload requestFields = new AddMoreTimePayload() {
                TimeToAdd = timeToAdd,
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
        /// Cook Time Attribute
        /// </summary>
        public required ReadAttribute<TimeSpan> CookTime { get; init; }

        /// <summary>
        /// Max Cook Time Attribute
        /// </summary>
        public required ReadAttribute<TimeSpan> MaxCookTime { get; init; }

        /// <summary>
        /// Power Setting Attribute
        /// </summary>
        public required ReadAttribute<byte> PowerSetting { get; init; }

        /// <summary>
        /// Min Power Attribute
        /// </summary>
        public required ReadAttribute<byte> MinPower { get; init; }

        /// <summary>
        /// Max Power Attribute
        /// </summary>
        public required ReadAttribute<byte> MaxPower { get; init; }

        /// <summary>
        /// Power Step Attribute
        /// </summary>
        public required ReadAttribute<byte> PowerStep { get; init; }

        /// <summary>
        /// Supported Watts Attribute
        /// </summary>
        public required ReadAttribute<ushort[]> SupportedWatts { get; init; }

        /// <summary>
        /// Selected Watt Index Attribute
        /// </summary>
        public required ReadAttribute<byte> SelectedWattIndex { get; init; }

        /// <summary>
        /// Watt Rating Attribute
        /// </summary>
        public required ReadAttribute<ushort> WattRating { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Microwave Oven Control";
        }
    }
}