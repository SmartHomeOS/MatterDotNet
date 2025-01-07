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
    /// Alarm Base Cluster
    /// </summary>
    public class AlarmBaseCluster<T> : ClusterBase where T : Enum
    {

        /// <summary>
        /// Alarm Base Cluster
        /// </summary>
        public AlarmBaseCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports the ability to reset alarms
            /// </summary>
            Reset = 1,
        }
        #endregion Enums

        #region Payloads
        private record ResetPayload : TLVPayload
        {
            public required T Alarms { get; set; } = (T)(dynamic)0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1)
            {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)(dynamic)Alarms);
                writer.EndContainer();
            }
        }

        private record ModifyEnabledAlarmsPayload : TLVPayload
        {
            public required T Mask { get; set; } = (T)(dynamic)0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1)
            {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)(dynamic)Mask);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Reset
        /// </summary>
        public async Task<bool> Reset(SecureSession session, T Alarms)
        {
            ResetPayload requestFields = new ResetPayload()
            {
                Alarms = Alarms,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Modify Enabled Alarms
        /// </summary>
        public async Task<bool> ModifyEnabledAlarms(SecureSession session, T Mask)
        {
            ModifyEnabledAlarmsPayload requestFields = new ModifyEnabledAlarmsPayload()
            {
                Mask = Mask,
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
        /// Get the Mask attribute
        /// </summary>
        public async Task<T> GetMask(SecureSession session) {
            return (T?)(dynamic?)await GetAttribute(session, 0) ?? (T)(dynamic)0;
        }

        /// <summary>
        /// Get the Latch attribute
        /// </summary>
        public async Task<T> GetLatch(SecureSession session) {
            return (T?)(dynamic?)await GetAttribute(session, 1) ?? (T)(dynamic)0;
        }

        /// <summary>
        /// Get the State attribute
        /// </summary>
        public async Task<T> GetState(SecureSession session) {
            return (T?)(dynamic?)await GetAttribute(session, 2) ?? (T)(dynamic)0;
        }

        /// <summary>
        /// Get the Supported attribute
        /// </summary>
        public async Task<T> GetSupported(SecureSession session) {
            return (T?)(dynamic?)await GetAttribute(session, 3) ?? (T)(dynamic)0;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Alarm Base Cluster";
        }
    }
}