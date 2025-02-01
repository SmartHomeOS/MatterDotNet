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
    /// Attributes and commands for configuring the temperature control, and reporting temperature.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class TemperatureControl : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0056;

        /// <summary>
        /// Attributes and commands for configuring the temperature control, and reporting temperature.
        /// </summary>
        [SetsRequiredMembers]
        public TemperatureControl(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected TemperatureControl(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            TemperatureSetpoint = new ReadAttribute<decimal>(cluster, endPoint, 0) {
                Deserialize = x => (decimal)(dynamic?)x!
            };
            MinTemperature = new ReadAttribute<decimal>(cluster, endPoint, 1) {
                Deserialize = x => (decimal)(dynamic?)x!
            };
            MaxTemperature = new ReadAttribute<decimal>(cluster, endPoint, 2) {
                Deserialize = x => (decimal)(dynamic?)x!
            };
            Step = new ReadAttribute<decimal>(cluster, endPoint, 3) {
                Deserialize = x => (decimal)(dynamic?)x!
            };
            SelectedTemperatureLevel = new ReadAttribute<byte>(cluster, endPoint, 4) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            SupportedTemperatureLevels = new ReadAttribute<string[]>(cluster, endPoint, 5) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    string[] list = new string[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = reader.GetString(i, false)!;
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
            /// Use actual temperature numbers
            /// </summary>
            TemperatureNumber = 1,
            /// <summary>
            /// Use temperature levels
            /// </summary>
            TemperatureLevel = 2,
            /// <summary>
            /// Use step control with temperature numbers
            /// </summary>
            TemperatureStep = 4,
        }
        #endregion Enums

        #region Payloads
        private record SetTemperaturePayload : TLVPayload {
            public decimal? TargetTemperature { get; set; }
            public byte? TargetTemperatureLevel { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (TargetTemperature != null)
                    writer.WriteDecimal(0, TargetTemperature);
                if (TargetTemperatureLevel != null)
                    writer.WriteByte(1, TargetTemperatureLevel);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Set Temperature
        /// </summary>
        public async Task<bool> SetTemperature(SecureSession session, decimal? targetTemperature, byte? targetTemperatureLevel, CancellationToken token = default) {
            SetTemperaturePayload requestFields = new SetTemperaturePayload() {
                TargetTemperature = targetTemperature,
                TargetTemperatureLevel = targetTemperatureLevel,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
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
        /// Temperature Setpoint [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal> TemperatureSetpoint { get; init; }

        /// <summary>
        /// Min Temperature [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal> MinTemperature { get; init; }

        /// <summary>
        /// Max Temperature [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal> MaxTemperature { get; init; }

        /// <summary>
        /// Step [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal> Step { get; init; }

        /// <summary>
        /// Selected Temperature Level Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> SelectedTemperatureLevel { get; init; }

        /// <summary>
        /// Supported Temperature Levels Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string[]> SupportedTemperatureLevels { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Temperature Control";
        }
    }
}