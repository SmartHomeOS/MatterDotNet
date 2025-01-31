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

using MatterDotNet.Protocol.Cryptography;

namespace Generator
{
    public static class StructParser
    {
        public static Tag[] ParseStruct(Stream stream)
        {
            string? ns = null;
            List<Tag> tags = new List<Tag>();
            Tag? root = null;
            using (StreamReader sr = new StreamReader(stream))
            {
                Tag? parent = null;
                Tag? lastTag = null;
                while (!sr.EndOfStream)
                {
                    string? line = sr.ReadLine();
                    if (line == null)
                        break;
                    else
                        line = line.Trim();
                    if (line == "{")
                        parent = lastTag;
                    else if (line == "}" || line == "},") {
                        if (parent != null)
                            parent = parent.Parent;
                        if (parent == null && root != null)
                        {
                            tags.Add(root);
                            root = null;
                            lastTag = null;
                        }
                        else if (root == null)
                        {
                            ns = null;
                        }
                    }
                    else if (line.StartsWith("namespace"))
                    {
                        ns = line.Substring(10);
                        sr.ReadLine();
                    }
                    else if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("//"))
                    {
                        lastTag = parseLine(line, parent);
                        if (lastTag != null)
                            lastTag.Namespace = ns;
                        if (root == null)
                        {
                            root = lastTag;
                            parent = lastTag;
                        }
                        else if (lastTag != null)
                        {
                            parent!.Children.Add(lastTag);
                        }
                    }
                }
            }
            return tags.ToArray();
        }

        private static Tag? parseLine(string line, Tag? parent)
        {
            string[] parts = line.TrimEnd(',').Split([":", "=>"], StringSplitOptions.TrimEntries);
            if (parts.Length < 2)
                throw new Exception("Failed to parse: " + line);
            Tag tag = new Tag();
            tag.Parent = parent;
            string[] nameParts = parts[0].Split([" [", "]"], StringSplitOptions.TrimEntries);
            tag.Name = GeneratorUtil.SanitizeName(nameParts[0]);
            if (nameParts.Length > 1)
            {
                string[] constraints = nameParts[1].Split(',', StringSplitOptions.TrimEntries);
                foreach (string constraint in constraints)
                {
                    switch (constraint)
                    {
                        case "optional":
                            tag.Optional = true;
                            break;
                        default:
                            if (int.TryParse(constraint, out int tagnum))
                                tag.TagNumber = tagnum;
                            break;
                    }
                }
            }
            parts[1] = parts[1].Replace("ec-pub-key", $"OCTET STRING [ length {Crypto.PUBLIC_KEY_SIZE_BYTES} ]");
            parts[1] = parts[1].Replace("ec-signature", $"OCTET STRING [ length {Crypto.GROUP_SIZE_BYTES * 2} ]");
            parts[1] = parts[1].Replace("destination-identifier", $"OCTET STRING [ length {Crypto.HASH_LEN_BYTES} ]");
            string[] typeParts = parts[1].Split([" [", "]"], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            switch (typeParts[0].ToUpper())
            {
                case "UNSIGNED INTEGER":
                    tag.Type = DataType.UnsignedInteger;
                    break;
                case "ANY":
                    tag.Type = DataType.Any;
                    break;
                case "BOOLEAN":
                    tag.Type = DataType.Boolean;
                    break;
                case "STRUCTURE":
                    tag.Type = DataType.Structure;
                    break;
                case "CHOICE OF":
                    tag.Type = DataType.Choice;
                    break;
                case "SIGNED INTEGER":
                    tag.Type = DataType.Integer;
                    break;
                case "OCTET STRING":
                    tag.Type = DataType.Bytes;
                    break;
                case "STRING":
                    tag.Type = DataType.String;
                    break;
                case "ARRAY":
                case "ARRAY OF":
                    tag.Type = DataType.Array;
                    break;
                case "LIST":
                case "LIST OF":
                    tag.Type = DataType.List;
                    break;
                case "FLOAT32":
                    tag.Type = DataType.FloatingPoint;
                    tag.LengthBytes = 4;
                    break;
                case "FLOAT64":
                    tag.Type = DataType.FloatingPoint;
                    tag.LengthBytes = 8;
                    break;
                case "PERCENT100THS":
                    tag.Type = DataType.UnsignedInteger;
                    tag.LengthBytes = 2;
                    break;
                case "MEASUREMENTTYPEENUM":
                    tag.Type = DataType.Enum;
                    tag.LengthBytes = 2;
                    tag.ReferenceName = "MeasurementType";
                    break;
                default:
                    tag.Type = DataType.Reference;
                    tag.ReferenceName = GeneratorUtil.SanitizeName(typeParts[0]);
                    break;
            }
            if (typeParts.Length > 1)
            {
                string[] constraints = typeParts[1].Split(',', StringSplitOptions.TrimEntries);
                foreach (string constraint in constraints)
                {
                    switch (constraint)
                    {
                        case "length 8-bits":
                        case "range 8-bits":
                            tag.LengthBytes = 1;
                            break;
                        case "length 16-bits":
                        case "range 16-bits":
                            tag.LengthBytes = 2;
                            break;
                        case "length 32-bits":
                        case "range 32-bits":
                            tag.LengthBytes = 4;
                            break;
                        case "length 64-bits":
                        case "range 64-bits":
                            tag.LengthBytes = 8;
                            break;
                        case "length CRYPTO_AEAD_MIC_LENGTH_BYTES":
                            tag.Min = tag.Max = Crypto.AEAD_MIC_LENGTH_BYTES;
                            break;
                        case "length CRYPTO_PUBLIC_KEY_SIZE_BYTES":
                            tag.Min = tag.Max = Crypto.PUBLIC_KEY_SIZE_BYTES;
                            break;
                        case "length CRYPTO_HASH_LEN_BYTES":
                            tag.Min = tag.Max = Crypto.HASH_LEN_BYTES;
                            break;
                        case "nullable":
                            tag.Nullable = true;
                            break;
                    }
                    if (constraint.StartsWith("length", StringComparison.OrdinalIgnoreCase))
                    {
                        if (constraint.Contains(".."))
                        {
                            string[] rangeParts = constraint.Substring(7).Split("..");
                            tag.Min = int.Parse(rangeParts[0]);
                            if (int.TryParse(rangeParts[1], out int max))
                                tag.Max = max;
                        }
                        else if (int.TryParse(constraint.Substring(7), out int count))
                        {
                            tag.Min = count;
                            tag.Max = count;
                        }
                    }
                }
                if (typeParts.Length > 2 && typeParts[2].StartsWith("OF", StringComparison.InvariantCultureIgnoreCase))
                {
                    tag.ReferenceName = ParseType(typeParts[2].Substring(3));
                }
            }
            if (tag.LengthBytes == 0 && tag.Max != 0)
            {
                if (tag.Type == DataType.Integer)
                {
                    if (tag.Max < sbyte.MaxValue)
                        tag.LengthBytes = 1;
                    else if (tag.Max < short.MaxValue)
                        tag.LengthBytes = 2;
                    else if (tag.Max < int.MaxValue)
                        tag.LengthBytes = 4;
                    else
                        tag.LengthBytes = 8;
                }
                else
                {
                    if (tag.Max < byte.MaxValue)
                        tag.LengthBytes = 1;
                    else if (tag.Max < ushort.MaxValue)
                        tag.LengthBytes = 2;
                    else if (tag.Max < uint.MaxValue)
                        tag.LengthBytes = 4;
                    else
                        tag.LengthBytes = 8;
                }
            }
            if (tag.Parent?.Type == DataType.Choice)
                tag.Optional = true;
            return tag;
        }

        private static string ParseType(string type)
        {
            switch (type)
            {
                case "UNSIGNED INTEGER":
                    return "uint";
                case "BOOLEAN":
                    return "bool";
                case "SIGNED INTEGER":
                    return "int";
                case "OCTET STRING":
                    return "byte[]";
                case "STRING":
                    return "string";
                case "ARRAY":
                case "ARRAY OF":
                case "LIST":
                case "LIST OF":
                    throw new NotSupportedException("nested arrays");
                case "FLOAT32":
                    return "float";
                case "FLOAT64":
                    return "double";
                default:
                    return GeneratorUtil.SanitizeName(type);
            }
        }
    }
}
