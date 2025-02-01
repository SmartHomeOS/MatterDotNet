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

using MatterDotNet.Attributes;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

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
        [SetsRequiredMembers]
        public LaundryWasherControls(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected LaundryWasherControls(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            SpinSpeeds = new ReadAttribute<string[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    string[] list = new string[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = reader.GetString(i, false)!;
                    return list;
                }
            };
            SpinSpeedCurrent = new ReadWriteAttribute<byte?>(cluster, endPoint, 1, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            NumberOfRinses = new ReadWriteAttribute<NumberOfRinsesEnum>(cluster, endPoint, 2) {
                Deserialize = x => (NumberOfRinsesEnum)DeserializeEnum(x)!
            };
            SupportedRinses = new ReadAttribute<NumberOfRinsesEnum[]>(cluster, endPoint, 3) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    NumberOfRinsesEnum[] list = new NumberOfRinsesEnum[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = (NumberOfRinsesEnum)reader.GetUShort(i)!.Value;
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
        public enum NumberOfRinsesEnum : byte {
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
        /// Spin Speeds Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string[]> SpinSpeeds { get; init; }

        /// <summary>
        /// Spin Speed Current Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte?> SpinSpeedCurrent { get; init; }

        /// <summary>
        /// Number Of Rinses Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<NumberOfRinsesEnum> NumberOfRinses { get; init; }

        /// <summary>
        /// Supported Rinses Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<NumberOfRinsesEnum[]> SupportedRinses { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Laundry Washer Controls";
        }
    }
}