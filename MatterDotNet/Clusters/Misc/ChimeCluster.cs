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

namespace MatterDotNet.Clusters.Misc
{
    /// <summary>
    /// This cluster provides facilities to configure and play Chime sounds, such as those used in a doorbell.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class Chime : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0556;

        /// <summary>
        /// This cluster provides facilities to configure and play Chime sounds, such as those used in a doorbell.
        /// </summary>
        public Chime(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected Chime(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Records
        /// <summary>
        /// Chime Sound
        /// </summary>
        public record ChimeSound : TLVPayload {
            /// <summary>
            /// Chime Sound
            /// </summary>
            public ChimeSound() { }

            /// <summary>
            /// Chime Sound
            /// </summary>
            [SetsRequiredMembers]
            public ChimeSound(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ChimeID = reader.GetByte(0)!.Value;
                Name = reader.GetString(1, false, 48)!;
            }
            public required byte ChimeID { get; set; }
            public required string Name { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, ChimeID);
                writer.WriteString(1, Name, 48);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Play Chime Sound
        /// </summary>
        public async Task<bool> PlayChimeSound(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00);
            return ValidateResponse(resp);
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Get the Installed Chime Sounds attribute
        /// </summary>
        public async Task<ChimeSound[]> GetInstalledChimeSounds(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            ChimeSound[] list = new ChimeSound[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new ChimeSound(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Active Chime ID attribute
        /// </summary>
        public async Task<byte> GetActiveChimeID(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 1) ?? 0;
        }

        /// <summary>
        /// Set the Active Chime ID attribute
        /// </summary>
        public async Task SetActiveChimeID (SecureSession session, byte? value = 0) {
            await SetAttribute(session, 1, value);
        }

        /// <summary>
        /// Get the Enabled attribute
        /// </summary>
        public async Task<bool> GetEnabled(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 2) ?? true;
        }

        /// <summary>
        /// Set the Enabled attribute
        /// </summary>
        public async Task SetEnabled (SecureSession session, bool? value = true) {
            await SetAttribute(session, 2, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Chime";
        }
    }
}