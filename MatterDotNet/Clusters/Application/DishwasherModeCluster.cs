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
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Dishwasher Mode Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class DishwasherModeCluster : ModeBaseCluster
    {
        internal const uint CLUSTER_ID = 0x0059;

        /// <summary>
        /// Dishwasher Mode Cluster
        /// </summary>
        public DishwasherModeCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }

        #region Records
        /// <summary>
        /// Mode Option
        /// </summary>
        public record ModeOption : TLVPayload {
            /// <summary>
            /// Mode Option
            /// </summary>
            public ModeOption() { }

            /// <summary>
            /// Mode Option
            /// </summary>
            [SetsRequiredMembers]
            public ModeOption(object[] fields) {
                FieldReader reader = new FieldReader(fields);
            }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Attributes
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Dishwasher Mode Cluster";
        }
    }
}