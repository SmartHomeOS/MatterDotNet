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
        [SetsRequiredMembers]
        public Chime(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected Chime(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            InstalledChimeSounds = new ReadAttribute<ChimeSound[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ChimeSound[] list = new ChimeSound[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new ChimeSound(reader.GetStruct(i)!);
                    return list;
                }
            };
            ActiveChimeID = new ReadWriteAttribute<byte>(cluster, endPoint, 1) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            Enabled = new ReadWriteAttribute<bool>(cluster, endPoint, 2) {
                Deserialize = x => (bool?)(dynamic?)x ?? true

            };
        }

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
        public async Task<bool> PlayChimeSound(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, null, token);
            return ValidateResponse(resp);
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Installed Chime Sounds Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ChimeSound[]> InstalledChimeSounds { get; init; }

        /// <summary>
        /// Active Chime ID Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte> ActiveChimeID { get; init; }

        /// <summary>
        /// Enabled Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<bool> Enabled { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Chime";
        }
    }
}