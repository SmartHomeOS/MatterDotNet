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
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters
{
    /// <summary>
    /// Mode Tag
    /// </summary>
    public record ModeTag : TLVPayload {
        /// <summary>
        /// Mode Tag
        /// </summary>
        public ModeTag() { }

        /// <summary>
        /// Mode Tag
        /// </summary>
        [SetsRequiredMembers]
        public ModeTag(object[] fields) {
            FieldReader reader = new FieldReader(fields);
            MfgCode = reader.GetUShort(0, true);
            Value = reader.GetUShort(1)!.Value;
        }
        public ushort? MfgCode { get; set; }
        public required ushort Value { get; set; }
        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            if (MfgCode != null)
                writer.WriteUShort(0, MfgCode);
            writer.WriteUShort(1, Value);
            writer.EndContainer();
        }
    }

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
            Label = reader.GetString(0, false, 64)!;
            Mode = reader.GetByte(1)!.Value;
            {
                ModeTags = new ModeTag[reader.GetStruct(2)!.Length];
                for (int n = 0; n < ModeTags.Length; n++) {
                    ModeTags[n] = new ModeTag((object[])((object[])fields[2])[n]);
                }
            }
        }
        public required string Label { get; set; }
        public required byte Mode { get; set; }
        public required ModeTag[] ModeTags { get; set; }
        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            writer.WriteString(0, Label, 64);
            writer.WriteByte(1, Mode);
            {
                Constrain(ModeTags, 0, 8);
                writer.StartArray(2);
                foreach (var item in ModeTags) {
                    item.Serialize(writer, -1);
                }
                writer.EndContainer();
            }
            writer.EndContainer();
        }
    }
}