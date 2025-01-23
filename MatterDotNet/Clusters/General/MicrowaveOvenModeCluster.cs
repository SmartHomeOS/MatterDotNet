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

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// Attributes and commands for selecting a mode from a list of supported options.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class MicrowaveOvenMode : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x005E;

        /// <summary>
        /// Attributes and commands for selecting a mode from a list of supported options.
        /// </summary>
        public MicrowaveOvenMode(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected MicrowaveOvenMode(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Dependency with the OnOff cluster
            /// </summary>
            OnOff = 1,
        }

        /// <summary>
        /// Mode Tag
        /// </summary>
        public enum ModeTag : ushort {
            Auto = 0x0000,
            Quick = 0x0001,
            Quiet = 0x0002,
            LowNoise = 0x0003,
            LowEnergy = 0x0004,
            Vacation = 0x0005,
            Min = 0x0006,
            Max = 0x0007,
            Night = 0x0008,
            Day = 0x0009,
            Normal = 0x4000,
            Defrost = 0x4001,
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
        /// Get the Supported Modes attribute
        /// </summary>
        public async Task<ModeOption[]> GetSupportedModes(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            ModeOption[] list = new ModeOption[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new ModeOption(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Current Mode attribute
        /// </summary>
        public async Task<byte> GetCurrentMode(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 1))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Microwave Oven Mode";
        }
    }
}