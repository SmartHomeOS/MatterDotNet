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
    public record IdentificationDeclaration : TLVPayload
    {
        /// <inheritdoc />
        public IdentificationDeclaration() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public IdentificationDeclaration(Memory<byte> data) : this(new TLVReader(data)) {}

        public record TargetApp : TLVPayload
        {
            /// <inheritdoc />
            public TargetApp() {}

            /// <inheritdoc />
            [SetsRequiredMembers]
            public TargetApp(Memory<byte> data) : this(new TLVReader(data)) {}

            public ushort? AppVendorId { get; set; } 
            public ushort? AppProductId { get; set; } 

            /// <inheritdoc />
            [SetsRequiredMembers]
            public TargetApp(TLVReader reader, uint structNumber = 0) {
                reader.StartStructure(structNumber);
                if (reader.IsTag(11))
                    AppVendorId = reader.GetUShort(11);
                if (reader.IsTag(12))
                    AppProductId = reader.GetUShort(12);
                reader.EndContainer();
            }

            /// <inheritdoc />
            public override void Serialize(TLVWriter writer, uint structNumber = 0) {
                writer.StartStructure(structNumber);
                if (AppVendorId != null)
                    writer.WriteUShort(11, AppVendorId);
                if (AppProductId != null)
                    writer.WriteUShort(12, AppProductId);
                writer.EndContainer();
            }
        }
        public ushort? VendorId { get; set; } 
        public ushort? ProductId { get; set; } 
        public string? DeviceName { get; set; } 
        public uint? DeviceType { get; set; } 
        public string? PairingInstruction { get; set; } 
        public uint? PairingHint { get; set; } 
        public string? RotatingDeviceId { get; set; } 
        public ushort? Port { get; set; } 
        public TargetApp[]? TargetAppList { get; set; } 
        public bool? NoPasscode { get; set; } 
        public bool? CdUponPasscodeDialog { get; set; } 
        public bool? CommissionerPasscode { get; set; } 
        public bool? CommissionerPasscodeReady { get; set; } 
        public bool? CancelPasscode { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public IdentificationDeclaration(TLVReader reader, uint structNumber = 0) {
            reader.StartStructure(structNumber);
            if (reader.IsTag(1))
                VendorId = reader.GetUShort(1);
            if (reader.IsTag(2))
                ProductId = reader.GetUShort(2);
            if (reader.IsTag(3))
                DeviceName = reader.GetString(3);
            if (reader.IsTag(4))
                DeviceType = reader.GetUInt(4);
            if (reader.IsTag(5))
                PairingInstruction = reader.GetString(5);
            if (reader.IsTag(6))
                PairingHint = reader.GetUInt(6);
            if (reader.IsTag(7))
                RotatingDeviceId = reader.GetString(7);
            if (reader.IsTag(8))
                Port = reader.GetUShort(8);
            if (reader.IsTag(9))
            {
                reader.StartArray(9);
                List<TargetApp> items = new();
                while (!reader.IsEndContainer()) {
                    items.Add(new TargetApp(reader, 10));
                }
                TargetAppList = items.ToArray();
            }
            if (reader.IsTag(13))
                NoPasscode = reader.GetBool(13);
            if (reader.IsTag(14))
                CdUponPasscodeDialog = reader.GetBool(14);
            if (reader.IsTag(15))
                CommissionerPasscode = reader.GetBool(15);
            if (reader.IsTag(16))
                CommissionerPasscodeReady = reader.GetBool(16);
            if (reader.IsTag(17))
                CancelPasscode = reader.GetBool(17);
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, uint structNumber = 0) {
            writer.StartStructure(structNumber);
            if (VendorId != null)
                writer.WriteUShort(1, VendorId);
            if (ProductId != null)
                writer.WriteUShort(2, ProductId);
            if (DeviceName != null)
                writer.WriteString(3, DeviceName, 1);
            if (DeviceType != null)
                writer.WriteUInt(4, DeviceType);
            if (PairingInstruction != null)
                writer.WriteString(5, PairingInstruction, 1);
            if (PairingHint != null)
                writer.WriteUInt(6, PairingHint);
            if (RotatingDeviceId != null)
                writer.WriteString(7, RotatingDeviceId, 1);
            if (Port != null)
                writer.WriteUShort(8, Port);
            if (TargetAppList != null)
            {
                writer.StartArray(9);
                foreach (var item in TargetAppList) {
                    item.Serialize(writer, 10);
                }
                writer.EndContainer();
            }
            if (NoPasscode != null)
                writer.WriteBool(13, NoPasscode);
            if (CdUponPasscodeDialog != null)
                writer.WriteBool(14, CdUponPasscodeDialog);
            if (CommissionerPasscode != null)
                writer.WriteBool(15, CommissionerPasscode);
            if (CommissionerPasscodeReady != null)
                writer.WriteBool(16, CommissionerPasscodeReady);
            if (CancelPasscode != null)
                writer.WriteBool(17, CancelPasscode);
            writer.EndContainer();
        }
    }
}