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
    /// Temperature Control Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class TemperatureControlCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0056;

        /// <summary>
        /// Temperature Control Cluster
        /// </summary>
        public TemperatureControlCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected TemperatureControlCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
            public short? TargetTemperature { get; set; }
            public byte? TargetTemperatureLevel { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (TargetTemperature != null)
                    writer.WriteShort(0, TargetTemperature);
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
        public async Task<bool> SetTemperature(SecureSession session, short? TargetTemperature, byte? TargetTemperatureLevel) {
            SetTemperaturePayload requestFields = new SetTemperaturePayload() {
                TargetTemperature = TargetTemperature,
                TargetTemperatureLevel = TargetTemperatureLevel,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
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
        /// Get the Temperature Setpoint attribute
        /// </summary>
        public async Task<short> GetTemperatureSetpoint(SecureSession session) {
            return (short)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Get the Min Temperature attribute
        /// </summary>
        public async Task<short> GetMinTemperature(SecureSession session) {
            return (short)(dynamic?)(await GetAttribute(session, 1))!;
        }

        /// <summary>
        /// Get the Max Temperature attribute
        /// </summary>
        public async Task<short> GetMaxTemperature(SecureSession session) {
            return (short)(dynamic?)(await GetAttribute(session, 2))!;
        }

        /// <summary>
        /// Get the Step attribute
        /// </summary>
        public async Task<short> GetStep(SecureSession session) {
            return (short)(dynamic?)(await GetAttribute(session, 3))!;
        }

        /// <summary>
        /// Get the Selected Temperature Level attribute
        /// </summary>
        public async Task<byte> GetSelectedTemperatureLevel(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 4))!;
        }

        /// <summary>
        /// Get the Supported Temperature Levels attribute
        /// </summary>
        public async Task<string[]> GetSupportedTemperatureLevels(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 5))!);
            string[] list = new string[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetString(i, false)!;
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Temperature Control Cluster";
        }
    }
}