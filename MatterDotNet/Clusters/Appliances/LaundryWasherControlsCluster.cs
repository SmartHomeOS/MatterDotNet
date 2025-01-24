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
using MatterDotNet.Protocol.Sessions;

namespace MatterDotNet.Clusters.Appliances
{
    /// <summary>
    /// This cluster supports remotely monitoring and controlling the different types of functionality available to a washing device, such as a washing machine.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class LaundryWasherControls : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0053;

        /// <summary>
        /// This cluster supports remotely monitoring and controlling the different types of functionality available to a washing device, such as a washing machine.
        /// </summary>
        public LaundryWasherControls(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected LaundryWasherControls(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Multiple spin speeds supported
            /// </summary>
            Spin = 1,
            /// <summary>
            /// Multiple rinse cycles supported
            /// </summary>
            Rinse = 2,
        }

        /// <summary>
        /// Number Of Rinses
        /// </summary>
        public enum NumberOfRinses : byte {
            /// <summary>
            /// This laundry washer mode does not perform rinse cycles
            /// </summary>
            None = 0,
            /// <summary>
            /// This laundry washer mode performs normal rinse cycles determined by the manufacturer
            /// </summary>
            Normal = 1,
            /// <summary>
            /// This laundry washer mode performs an extra rinse cycle
            /// </summary>
            Extra = 2,
            /// <summary>
            /// This laundry washer mode performs the maximum number of rinse cycles determined by the manufacturer
            /// </summary>
            Max = 3,
        }
        #endregion Enums

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
        /// Get the Spin Speeds attribute
        /// </summary>
        public async Task<string[]> GetSpinSpeeds(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            string[] list = new string[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetString(i, false)!;
            return list;
        }

        /// <summary>
        /// Get the Spin Speed Current attribute
        /// </summary>
        public async Task<byte?> GetSpinSpeedCurrent(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 1, true);
        }

        /// <summary>
        /// Set the Spin Speed Current attribute
        /// </summary>
        public async Task SetSpinSpeedCurrent (SecureSession session, byte? value) {
            await SetAttribute(session, 1, value, true);
        }

        /// <summary>
        /// Get the Number Of Rinses attribute
        /// </summary>
        public async Task<NumberOfRinses> GetNumberOfRinses(SecureSession session) {
            return (NumberOfRinses)await GetEnumAttribute(session, 2);
        }

        /// <summary>
        /// Set the Number Of Rinses attribute
        /// </summary>
        public async Task SetNumberOfRinses (SecureSession session, NumberOfRinses value) {
            await SetAttribute(session, 2, value);
        }

        /// <summary>
        /// Get the Supported Rinses attribute
        /// </summary>
        public async Task<NumberOfRinses[]> GetSupportedRinses(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 3))!);
            NumberOfRinses[] list = new NumberOfRinses[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = (NumberOfRinses)reader.GetUShort(i)!.Value;
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Laundry Washer Controls";
        }
    }
}