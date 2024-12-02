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

namespace Generator
{
    public static class ClassGenerator
    {
        private static readonly string HEADER = "// MatterDotNet Copyright (C) 2024 \n//\n// This program is free software: you can redistribute it and/or modify\n// it under the terms of the GNU Affero General Public License as published by\n// the Free Software Foundation, either version 3 of the License, or any later version.\n// This program is distributed in the hope that it will be useful,\n// but WITHOUT ANY WARRANTY, without even the implied warranty of\n// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.\n// See the GNU Affero General Public License for more details.\n// You should have received a copy of the GNU Affero General Public License\n// along with this program.  If not, see <http://www.gnu.org/licenses/>.\n//\n// WARNING: This file was auto-generated. Do not edit.\n\nusing MatterDotNet.Protocol.Parsers;\nusing MatterDotNet.Protocol.Payloads;\nusing System.Diagnostics.CodeAnalysis;\n";
        public static bool Emit(Stream stream, Tag tag)
        {
            StreamWriter writer = new StreamWriter(stream);
            {
                writer.WriteLine(HEADER);
                writer.WriteLine($"namespace MatterDotNet.Messages\n{{");
                WriteTag("    ", tag, writer);
                writer.Write("}");
                writer.Flush();
            }
            return true;
        }

        private static void WriteTag(string indent, Tag tag, StreamWriter writer)
        {
            writer.Write($"{indent}public class {tag.Name}");
            if (tag.Parent == null)
                writer.WriteLine($" : TLVPayload\n{indent}{{\n{indent}    /// <inheritdoc />\n{indent}    public {tag.Name}() {{}}\n\n{indent}    /// <inheritdoc />\n{indent}    [SetsRequiredMembers]\n{indent}    public {tag.Name}(Memory<byte> data) : this(new TLVReader(data)) {{}}\n");
            else
                writer.WriteLine($"\n{indent}{{");
            foreach (Tag child in tag.Children)
            {
                if ((child.Type == DataType.Array || child.Type == DataType.List) && child.Children.Count > 0)
                    WriteTag(indent + "    ", child.Children[0], writer);
            }
            foreach (Tag child in tag.Children)
            {
                if (child.Type != DataType.Structure)
                    writer.WriteLine($"{indent}    {(child.Optional? "public" : "public required")} {GetType(child)}{((child.Nullable || child.Optional) ? "?" : "")} {child.Name} {{ get; set; }} ");
            }
            if (tag.Parent == null) {
                writer.WriteLine($"\n{indent}    /// <inheritdoc />\n{indent}    [SetsRequiredMembers]\n{indent}    public {tag.Name}(TLVReader reader, uint structNumber = 0) {{");
                writer.WriteLine($"{indent}        reader.StartStructure(structNumber);");
                foreach (Tag child in tag.Children)
                {
                    string totalIndent = $"{indent}        ";
                    if (child.Optional)
                    {
                        writer.WriteLine($"{totalIndent}if (reader.IsTag({child.TagNumber}))");
                        totalIndent += "    ";
                    }
                    switch (child.Type)
                    {
                        case DataType.Boolean:
                            writer.WriteLine($"{totalIndent}{child.Name} = reader.GetBool({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                            break;
                        case DataType.Integer:
                            if (child.LengthBytes == 1)
                                writer.WriteLine($"{totalIndent}{child.Name} = reader.GetSByte({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                            else if (child.LengthBytes == 2)
                                writer.WriteLine($"{totalIndent}{child.Name} = reader.GetShort({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                            else if (child.LengthBytes == 4)
                                writer.WriteLine($"{totalIndent}{child.Name} = reader.GetInt({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                            else
                                writer.WriteLine($"{totalIndent}{child.Name} = reader.GetLong({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                            break;
                        case DataType.UnsignedInteger:
                            if (child.LengthBytes == 1)
                                writer.WriteLine($"{totalIndent}{child.Name} = reader.GetByte({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                            else if (child.LengthBytes == 2)
                                writer.WriteLine($"{totalIndent}{child.Name} = reader.GetUShort({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                            else if (child.LengthBytes == 4)
                                writer.WriteLine($"{totalIndent}{child.Name} = reader.GetUInt({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                            else
                                writer.WriteLine($"{totalIndent}{child.Name} = reader.GetULong({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                            break;
                        case DataType.FloatingPoint:
                            if (child.LengthBytes == 4)
                                writer.WriteLine($"{totalIndent}{child.Name} = reader.GetFloat({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                            else
                                writer.WriteLine($"{totalIndent}{child.Name} = reader.GetDouble({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                            break;
                        case DataType.Bytes:
                            writer.WriteLine($"{totalIndent}{child.Name} = reader.GetBytes({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!;" : ";")}");
                            break;
                        case DataType.String:
                            writer.WriteLine($"{totalIndent}{child.Name} = reader.GetString({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!;" : ";")}");
                            break;
                        case DataType.Reference:
                            writer.WriteLine($"{totalIndent}{child.Name} = new {child.ReferenceName}(reader, {child.TagNumber});");
                            break;

                    }
                }
                writer.WriteLine($"{indent}        reader.EndContainer();");
                writer.WriteLine($"{indent}    }}\n\n{indent}    /// <inheritdoc />\n{indent}    public override void Serialize(TLVWriter writer, uint structNumber = 0) {{");
                writer.WriteLine($"{indent}        writer.StartStructure(structNumber);");
                foreach (Tag child in tag.Children)
                {
                    string totalIndent = $"{indent}        ";
                    if (child.Optional)
                    {
                        writer.WriteLine($"{totalIndent}if ({child.Name} != null)");
                        totalIndent += "    ";
                    }
                    switch (child.Type)
                    {
                        case DataType.Boolean:
                            writer.WriteLine($"{totalIndent}writer.WriteBool({child.TagNumber}, {child.Name});");
                            break;
                        case DataType.Integer:
                            if (child.LengthBytes == 1)
                                writer.WriteLine($"{totalIndent}writer.WriteSByte({child.TagNumber}, {child.Name});");
                            else if (child.LengthBytes == 2)
                                writer.WriteLine($"{totalIndent}writer.WriteShort({child.TagNumber}, {child.Name});");
                            else if (child.LengthBytes == 4)
                                writer.WriteLine($"{totalIndent}writer.WriteInt({child.TagNumber}, {child.Name});");
                            else
                                writer.WriteLine($"{totalIndent}writer.WriteLong({child.TagNumber}, {child.Name});");
                            break;
                        case DataType.UnsignedInteger:
                            if (child.LengthBytes == 1)
                                writer.WriteLine($"{totalIndent}writer.WriteByte({child.TagNumber}, {child.Name});");
                            else if (child.LengthBytes == 2)
                                writer.WriteLine($"{totalIndent}writer.WriteUShort({child.TagNumber}, {child.Name});");
                            else if (child.LengthBytes == 4)
                                writer.WriteLine($"{totalIndent}writer.WriteUInt({child.TagNumber}, {child.Name});");
                            else
                                writer.WriteLine($"{totalIndent}writer.WriteULong({child.TagNumber}, {child.Name});");
                            break;
                        case DataType.FloatingPoint:
                            if (child.LengthBytes == 4)
                                writer.WriteLine($"{totalIndent}writer.WriteFloat({child.TagNumber}, {child.Name});");
                            else
                                writer.WriteLine($"{totalIndent}writer.WriteDouble({child.TagNumber}, {child.Name});");
                            break;
                        case DataType.Bytes:
                            writer.WriteLine($"{totalIndent}writer.WriteBytes({child.TagNumber}, {child.Name}, {child.LengthBytes});");
                            break;
                        case DataType.String:
                            writer.WriteLine($"{totalIndent}writer.WriteString({child.TagNumber}, {child.Name}, {child.LengthBytes});");
                            break;
                        case DataType.Reference:
                            writer.WriteLine($"{totalIndent}{child.Name}.Serialize(writer, {child.TagNumber});");
                            break;

                    }
                }
                writer.WriteLine($"{indent}        writer.EndContainer();\n{indent}    }}");
            }
            writer.WriteLine($"{indent}}}");
        }

        private static string GetType(Tag tag)
        {
            switch (tag.Type)
            {
                case DataType.Array:
                    if (tag.Children.Count == 0)
                        return "object[]";
                    return tag.Children[0].Name + "[]";
                case DataType.Boolean:
                    return "bool";
                case DataType.Bytes:
                    return "byte[]";
                case DataType.FloatingPoint:
                    if (tag.LengthBytes == 4)
                        return "float";
                    else if (tag.LengthBytes == 8)
                        return "double";
                    throw new InvalidDataException(tag.Name + " has length " + tag.LengthBytes);
                case DataType.Integer:
                    switch (tag.LengthBytes)
                    {
                        case 1:
                            return "sbyte";
                        case 2:
                            return "short";
                        case 4:
                            return "int";
                        case 8:
                            return "long";
                        default:
                            throw new InvalidDataException(tag.Name + " has length " + tag.LengthBytes);
                    }
                case DataType.List:
                    if (tag.Children.Count == 0)
                        return "List<object>";
                    return "List<" + tag.Children[0].Name + ">";
                case DataType.Null:
                    throw new InvalidDataException("type of " + tag.Name + " is null");
                case DataType.Reference:
                    return tag.ReferenceName!;
                case DataType.String:
                    return "string";
                case DataType.Structure:
                    return "class";
                case DataType.UnsignedInteger:
                    switch (tag.LengthBytes)
                    {
                        case 1:
                            return "byte";
                        case 2:
                            return "ushort";
                        case 4:
                            return "uint";
                        case 8:
                            return "ulong";
                        default:
                            throw new InvalidDataException(tag.Name + " has length " + tag.LengthBytes);
                    }
                default:
                    throw new InvalidDataException("Unsupported type " + tag.Type);
            }
        }
    }
}
