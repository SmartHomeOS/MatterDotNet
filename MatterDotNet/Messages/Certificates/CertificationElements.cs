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

namespace MatterDotNet.Messages.Certificates
{
    public record CertificationElements : TLVPayload
    {
        /// <inheritdoc />
        public CertificationElements() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public CertificationElements(Memory<byte> data) : this(new TLVReader(data)) {}

        public required ushort Format_version { get; set; }
        public required ushort Vendor_id { get; set; }
        public required uint[] Product_id_array { get; set; }
        public required uint Device_type_id { get; set; }
        public required string Certificate_id { get; set; }
        public required byte Security_level { get; set; }
        public required ushort Security_information { get; set; }
        public required ushort Version_number { get; set; }
        public required byte Certification_type { get; set; }
        public ushort? Dac_origin_vendor_id { get; set; }
        public ushort? Dac_origin_product_id { get; set; }
        public byte[][]? Authorized_paa_list { get; set; }

        [SetsRequiredMembers]
        internal CertificationElements(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            Format_version = reader.GetUShort(0)!.Value;
            Vendor_id = reader.GetUShort(1)!.Value;
            {
                reader.StartArray(2);
                List<uint> items = new();
                while (!reader.IsEndContainer()) {
                    items.Add(reader.GetUInt(-1)!.Value);
                }
                reader.EndContainer();
                Product_id_array = items.ToArray();
            }
            Device_type_id = reader.GetUInt(3)!.Value;
            Certificate_id = reader.GetString(4, false, 19, 19)!;
            Security_level = reader.GetByte(5)!.Value;
            Security_information = reader.GetUShort(6)!.Value;
            Version_number = reader.GetUShort(7)!.Value;
            Certification_type = reader.GetByte(8)!.Value;
            if (reader.IsTag(9))
                Dac_origin_vendor_id = reader.GetUShort(9);
            if (reader.IsTag(10))
                Dac_origin_product_id = reader.GetUShort(10);
            if (reader.IsTag(11))
            {
                reader.StartArray(11);
                List<byte[]> items = new();
                while (!reader.IsEndContainer()) {
                    items.Add(reader.GetBytes(-1)!);
                }
                reader.EndContainer();
                Authorized_paa_list = items.ToArray();
            }
            reader.EndContainer();
        }

        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            writer.WriteUShort(0, Format_version);
            writer.WriteUShort(1, Vendor_id);
            {
                Constrain(Product_id_array, 1, 100);
                writer.StartArray(2);
                foreach (var item in Product_id_array) {
                    writer.WriteUInt(-1, item);
                }
                writer.EndContainer();
            }
            writer.WriteUInt(3, Device_type_id);
            writer.WriteString(4, Certificate_id, 19, 19);
            writer.WriteByte(5, Security_level);
            writer.WriteUShort(6, Security_information);
            writer.WriteUShort(7, Version_number);
            writer.WriteByte(8, Certification_type);
            if (Dac_origin_vendor_id != null)
                writer.WriteUShort(9, Dac_origin_vendor_id);
            if (Dac_origin_product_id != null)
                writer.WriteUShort(10, Dac_origin_product_id);
            if (Authorized_paa_list != null)
            {
                Constrain(Authorized_paa_list, 1, 10);
                writer.StartArray(11);
                foreach (var item in Authorized_paa_list) {
                    writer.WriteBytes(-1, item);
                }
                writer.EndContainer();
            }
            writer.EndContainer();
        }
    }
}