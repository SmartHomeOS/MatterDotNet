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

namespace MatterDotNet.Messages.Certificates
{
    public record MatterCertificate : TLVPayload
    {
        /// <inheritdoc />
        public MatterCertificate() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public MatterCertificate(Memory<byte> data) : this(new TLVReader(data)) {}

        public required byte[] SerialNum { get; set; } 
        public required ulong SigAlgo { get; set; } 
        public required List<DnAttribute> Issuer { get; set; } 
        public required uint NotBefore { get; set; } 
        public required uint NotAfter { get; set; } 
        public required List<DnAttribute> Subject { get; set; } 
        public required ulong PubKeyAlgo { get; set; } 
        public required ulong EcCurveId { get; set; } 
        public required byte[] EcPubKey { get; set; } 
        public required List<Extension> Extensions { get; set; } 
        public required byte[] Signature { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public MatterCertificate(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            SerialNum = reader.GetBytes(1)!;
            SigAlgo = reader.GetULong(2)!.Value;
            {
                reader.StartList(3);
                Issuer = new();
                while (!reader.IsEndContainer()) {
                    Issuer.Add(new DnAttribute(reader, -1));
                }
                reader.EndContainer();
            }
            NotBefore = reader.GetUInt(4)!.Value;
            NotAfter = reader.GetUInt(5)!.Value;
            {
                reader.StartList(6);
                Subject = new();
                while (!reader.IsEndContainer()) {
                    Subject.Add(new DnAttribute(reader, -1));
                }
                reader.EndContainer();
            }
            PubKeyAlgo = reader.GetULong(7)!.Value;
            EcCurveId = reader.GetULong(8)!.Value;
            EcPubKey = reader.GetBytes(9)!;
            {
                reader.StartList(10);
                Extensions = new();
                while (!reader.IsEndContainer()) {
                    Extensions.Add(new Extension(reader, -1));
                }
                reader.EndContainer();
            }
            Signature = reader.GetBytes(11)!;
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            writer.WriteBytes(1, SerialNum);
            writer.WriteULong(2, SigAlgo);
            {
                writer.StartList(3);
                foreach (var item in Issuer) {
                    item.Serialize(writer, -1);
                }
                writer.EndContainer();
            }
            writer.WriteUInt(4, NotBefore);
            writer.WriteUInt(5, NotAfter);
            {
                writer.StartList(6);
                foreach (var item in Subject) {
                    item.Serialize(writer, -1);
                }
                writer.EndContainer();
            }
            writer.WriteULong(7, PubKeyAlgo);
            writer.WriteULong(8, EcCurveId);
            writer.WriteBytes(9, EcPubKey);
            {
                writer.StartList(10);
                foreach (var item in Extensions) {
                    item.Serialize(writer, -1);
                }
                writer.EndContainer();
            }
            writer.WriteBytes(11, Signature);
            writer.EndContainer();
        }
    }
}