// MatterDotNet Copyright (C) 2024 
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

namespace MatterDotNet.Messages.UserDirectedCommissioning
{
    public record CommissionerDeclaration : TLVPayload
    {
        /// <inheritdoc />
        public CommissionerDeclaration() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public CommissionerDeclaration(Memory<byte> data) : this(new TLVReader(data)) {}

        public ushort? ErrorCode { get; set; } 
        public bool? NeedsPasscode { get; set; } 
        public bool? NoAppsFound { get; set; } 
        public bool? PasscodeDialogDisplayed { get; set; } 
        public bool? CommissionerPasscode { get; set; } 
        public bool? QRCodeDisplayed { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public CommissionerDeclaration(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            if (reader.IsTag(1))
                ErrorCode = reader.GetUShort(1);
            if (reader.IsTag(2))
                NeedsPasscode = reader.GetBool(2);
            if (reader.IsTag(3))
                NoAppsFound = reader.GetBool(3);
            if (reader.IsTag(4))
                PasscodeDialogDisplayed = reader.GetBool(4);
            if (reader.IsTag(5))
                CommissionerPasscode = reader.GetBool(5);
            if (reader.IsTag(6))
                QRCodeDisplayed = reader.GetBool(6);
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            if (ErrorCode != null)
                writer.WriteUShort(1, ErrorCode);
            if (NeedsPasscode != null)
                writer.WriteBool(2, NeedsPasscode);
            if (NoAppsFound != null)
                writer.WriteBool(3, NoAppsFound);
            if (PasscodeDialogDisplayed != null)
                writer.WriteBool(4, PasscodeDialogDisplayed);
            if (CommissionerPasscode != null)
                writer.WriteBool(5, CommissionerPasscode);
            if (QRCodeDisplayed != null)
                writer.WriteBool(6, QRCodeDisplayed);
            writer.EndContainer();
        }
    }
}