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

namespace Generator
{
    public static class ClassGenerator
    {
        public static readonly string HEADER = "// MatterDotNet Copyright (C) 2025 \n//\n// This program is free software: you can redistribute it and/or modify\n// it under the terms of the GNU Affero General Public License as published by\n// the Free Software Foundation, either version 3 of the License, or any later version.\n// This program is distributed in the hope that it will be useful,\n// but WITHOUT ANY WARRANTY, without even the implied warranty of\n// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.\n// See the GNU Affero General Public License for more details.\n// You should have received a copy of the GNU Affero General Public License\n// along with this program.  If not, see <http://www.gnu.org/licenses/>.\n//\n// WARNING: This file was auto-generated. Do not edit.\n\nusing MatterDotNet.Protocol.Parsers;\nusing MatterDotNet.Protocol.Payloads;\nusing System.Diagnostics.CodeAnalysis;\n";
        public static bool Emit(Stream stream, Tag tag)
        {
            StreamWriter writer = new StreamWriter(stream);
            {
                writer.NewLine = "\n";
                writer.WriteLine(HEADER);
                writer.WriteLine($"namespace MatterDotNet.Messages{(tag.Namespace != null ? '.' + tag.Namespace : "")}\n{{");
                WriteTag("    ", tag, writer);
                writer.Write("}");
                writer.Flush();
            }
            return true;
        }

        private static void WriteTag(string indent, Tag tag, StreamWriter writer)
        {
            writer.Write($"{indent}public record {tag.Name}");
            writer.WriteLine($" : TLVPayload\n{indent}{{\n{indent}    /// <inheritdoc />\n{indent}    public {tag.Name}() {{}}\n\n{indent}    /// <inheritdoc />\n{indent}    [SetsRequiredMembers]\n{indent}    public {tag.Name}(Memory<byte> data) : this(new TLVReader(data)) {{}}\n");
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
            writer.WriteLine($"\n{indent}    [SetsRequiredMembers]\n{indent}    internal {tag.Name}(TLVReader reader, long structNumber = -1) {{");
            if (tag.Type == DataType.List)
                writer.WriteLine($"{indent}        reader.StartList(structNumber);");
            else if (tag.Type != DataType.Choice)
                writer.WriteLine($"{indent}        reader.StartStructure(structNumber);");
            foreach (Tag child in tag.Children)
            {
                string totalIndent = $"{indent}        ";
                if (child.Optional)
                {
                    writer.WriteLine($"{totalIndent}{((child.Parent?.Type == DataType.Choice && tag.Children.IndexOf(child) > 0) ? "else " : "")}if (reader.IsTag({child.TagNumber}))");
                    if (child.Type != DataType.List && child.Type != DataType.Array)
                        totalIndent += "    ";
                }
                switch (child.Type)
                {
                    case DataType.Array:
                        writer.WriteLine($"{totalIndent}{{");
                        writer.WriteLine($"{totalIndent}    reader.StartArray({child.TagNumber});");
                        writer.WriteLine($"{totalIndent}    List<{GetEnumerationType(child)}> items = new();");
                        writer.WriteLine($"{totalIndent}    while (!reader.IsEndContainer()) {{");
                        writer.WriteLine($"{totalIndent}        items.Add({GetReader(GetEnumerationType(child), GetEnumerationIndex(child), GetEnumerationNullable(child))});");
                        writer.WriteLine($"{totalIndent}    }}");
                        writer.WriteLine($"{totalIndent}    reader.EndContainer();");
                        writer.WriteLine($"{totalIndent}    {child.Name} = items.ToArray();");
                        writer.WriteLine($"{totalIndent}}}");
                        break;
                    case DataType.List:
                        writer.WriteLine($"{totalIndent}{{");
                        writer.WriteLine($"{totalIndent}    reader.StartList({child.TagNumber});");
                        writer.WriteLine($"{totalIndent}    {child.Name} = new();");
                        writer.WriteLine($"{totalIndent}    while (!reader.IsEndContainer()) {{");
                        writer.WriteLine($"{totalIndent}        {child.Name}.Add({GetReader(GetEnumerationType(child), GetEnumerationIndex(child), GetEnumerationNullable(child))});");
                        writer.WriteLine($"{totalIndent}    }}");
                        writer.WriteLine($"{totalIndent}    reader.EndContainer();");
                        writer.WriteLine($"{totalIndent}}}");
                        break;
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
                    case DataType.Enum:
                        if (child.LengthBytes == 1)
                            writer.WriteLine($"{totalIndent}{child.Name} = ({child.ReferenceName})reader.GetByte({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                        else
                            writer.WriteLine($"{totalIndent}{child.Name} = ({child.ReferenceName})reader.GetUShort({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                        break;
                    case DataType.FloatingPoint:
                        if (child.LengthBytes == 4)
                            writer.WriteLine($"{totalIndent}{child.Name} = reader.GetFloat({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                        else
                            writer.WriteLine($"{totalIndent}{child.Name} = reader.GetDouble({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!.Value;" : ";")}");
                        break;
                    case DataType.Bytes:
                        writer.Write($"{totalIndent}{child.Name} = reader.GetBytes({child.TagNumber}");
                        if (child.Nullable)
                            writer.Write(", true");
                        else if (child.Max != 0 || child.Min != 0)
                            writer.Write(", false");
                        if (child.Max != 0)
                            writer.Write($", {child.Max}");
                        if (child.Min != 0)
                        {
                            if (child.Max == 0)
                                writer.Write(", int.MaxValue");
                            writer.Write($", {child.Min}");
                        }
                        if (!child.Nullable && !child.Optional)
                            writer.WriteLine(")!;");
                        else
                            writer.WriteLine(");");
                        break;
                    case DataType.String:
                        writer.Write($"{totalIndent}{child.Name} = reader.GetString({child.TagNumber}");
                        if (child.Nullable)
                            writer.Write(", true");
                        else if (child.Max != 0 || child.Min != 0)
                            writer.Write(", false");
                        if (child.Max != 0)
                            writer.Write($", {child.Max}");
                        if (child.Min != 0)
                        {
                            if (child.Max == 0)
                                writer.Write(", int.MaxValue");
                            writer.Write($", {child.Min}");
                        }
                        if (!child.Nullable && !child.Optional)
                            writer.WriteLine(")!;");
                        else
                            writer.WriteLine(");");
                        break;
                    case DataType.Any:
                        writer.WriteLine($"{totalIndent}{child.Name} = reader.GetAny({child.TagNumber}{(child.Nullable ? ", true)" : ")")}{(!child.Nullable && !child.Optional ? "!;" : ";")}");
                        break;
                    case DataType.Reference:
                        writer.WriteLine($"{totalIndent}{child.Name} = new {child.ReferenceName}(reader, {child.TagNumber});");
                        break;

                }
            }
            if (tag.Type != DataType.Choice)
                writer.WriteLine($"{indent}        reader.EndContainer();");
            writer.WriteLine($"{indent}    }}\n\n{indent}    internal override void Serialize(TLVWriter writer, long structNumber = -1) {{");
            if (tag.Type == DataType.List)
                writer.WriteLine($"{indent}        writer.StartList(structNumber);");
            else if (tag.Type != DataType.Choice)
                writer.WriteLine($"{indent}        writer.StartStructure(structNumber);");
            foreach (Tag child in tag.Children)
            {
                string totalIndent = $"{indent}        ";
                if (child.Optional)
                {
                    writer.WriteLine($"{totalIndent}{((child.Parent?.Type == DataType.Choice && tag.Children.IndexOf(child) > 0) ? "else " : "")}if ({child.Name} != null)");
                    if (child.Type != DataType.List && child.Type != DataType.Array) 
                        totalIndent += "    ";
                }
                switch (child.Type)
                {
                    case DataType.Array:
                        writer.WriteLine($"{totalIndent}{{");
                        if (child.Min != 0 || child.Max != 0)
                        {
                            writer.Write($"{totalIndent}    Constrain({child.Name}, {child.Min}");
                            if (child.Max != 0)
                                writer.WriteLine($", {child.Max});");
                            else
                                writer.WriteLine(");");
                        }
                        writer.WriteLine($"{totalIndent}    writer.StartArray({child.TagNumber});");
                        writer.WriteLine($"{totalIndent}    foreach (var item in {child.Name}) {{");
                        writer.WriteLine($"{totalIndent}        {GetWriter(GetEnumerationType(child), GetEnumerationIndex(child))};");
                        writer.WriteLine($"{totalIndent}    }}");
                        writer.WriteLine($"{totalIndent}    writer.EndContainer();");
                        writer.WriteLine($"{totalIndent}}}");
                        break;
                    case DataType.List:
                        writer.WriteLine($"{totalIndent}{{");
                        if (child.Min != 0 || child.Max != 0)
                        {
                            writer.Write($"{totalIndent}    Constrain({child.Name}, {child.Min}");
                            if (child.Max != 0)
                                writer.WriteLine($", {child.Max});");
                            else
                                writer.WriteLine(");");
                        }
                        writer.WriteLine($"{totalIndent}    writer.StartList({child.TagNumber});");
                        writer.WriteLine($"{totalIndent}    foreach (var item in {child.Name}) {{");
                        writer.WriteLine($"{totalIndent}        {GetWriter(GetEnumerationType(child), GetEnumerationIndex(child))};");
                        writer.WriteLine($"{totalIndent}    }}");
                        writer.WriteLine($"{totalIndent}    writer.EndContainer();");
                        writer.WriteLine($"{totalIndent}}}");
                        break;
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
                    case DataType.Enum:
                        if (child.LengthBytes == 1)
                            writer.WriteLine($"{totalIndent}writer.WriteByte({child.TagNumber}, (byte){child.Name});");
                        else
                            writer.WriteLine($"{totalIndent}writer.WriteUShort({child.TagNumber}, (ushort){child.Name});");
                        break;
                    case DataType.FloatingPoint:
                        if (child.LengthBytes == 4)
                            writer.WriteLine($"{totalIndent}writer.WriteFloat({child.TagNumber}, {child.Name});");
                        else
                            writer.WriteLine($"{totalIndent}writer.WriteDouble({child.TagNumber}, {child.Name});");
                        break;
                    case DataType.Bytes:
                        writer.Write($"{totalIndent}writer.WriteBytes({child.TagNumber}, {child.Name}");
                        if (child.Max != 0)
                            writer.Write($", {child.Max}");
                        if (child.Min != 0)
                        {
                            if (child.Max == 0)
                                writer.Write($", int.MaxValue, {child.Min}");
                            else
                                writer.Write($", {child.Min}");
                        }
                        writer.WriteLine(");");
                        break;
                    case DataType.String:
                        writer.Write($"{totalIndent}writer.WriteString({child.TagNumber}, {child.Name}");
                        if (child.Max != 0)
                            writer.Write($", {child.Max}");
                        if (child.Min != 0)
                        {
                            if (child.Max == 0)
                                writer.Write($", int.MaxValue, {child.Min}");
                            else
                                writer.Write($", {child.Min}");
                        }
                        writer.WriteLine(");");
                        break;
                    case DataType.Any:
                        writer.WriteLine($"{totalIndent}writer.WriteAny({child.TagNumber}, {child.Name});");
                        break;
                    case DataType.Reference:
                        writer.WriteLine($"{totalIndent}{child.Name}.Serialize(writer, {child.TagNumber});");
                        break;

                }
            }
            if (tag.Type != DataType.Choice)
                writer.WriteLine($"{indent}        writer.EndContainer();");
            writer.WriteLine($"{indent}    }}");
            writer.WriteLine($"{indent}}}");
        }

        private static object GetWriter(string? referenceName, string tagNumber)
        {
            switch (referenceName)
            {
                case "bool":
                    return $"writer.WriteBool({tagNumber}, item)";
                case "byte[]":
                    return $"writer.WriteBytes({tagNumber}, item)";
                case "float":
                    return $"writer.WriteFloat({tagNumber}, item)";
                case "double":
                    return $"writer.WriteDouble({tagNumber}, item)";
                case "int":
                    return $"writer.WriteInt({tagNumber}, item)";
                case "string":
                    return $"writer.WriteString({tagNumber}, item)";
                case "uint":
                    return $"writer.WriteUInt({tagNumber}, item)";
                default:
                    return $"item.Serialize(writer, {tagNumber})";
            }
        }

        private static object GetReader(string? referenceName, string tagNumber, bool nullAllowed)
        {
            switch (referenceName)
            {
                case "bool":
                    return $"reader.GetBool({tagNumber}){(nullAllowed ? "" : "!.Value")}";
                case "byte[]":
                    return $"reader.GetBytes({tagNumber}){(nullAllowed ? "" : "!")}";
                case "float":
                    return $"reader.GetFloat({tagNumber}){(nullAllowed ? "" : "!.Value")}";
                case "double":
                    return $"reader.GetDouble({tagNumber}){(nullAllowed ? "" : "!.Value")}";
                case "int":
                    return $"reader.GetInt({tagNumber}){(nullAllowed ? "" : "!.Value")}";
                case "string":
                    return $"reader.GetString({tagNumber}){(nullAllowed ? "" : "!")}";
                case "uint":
                    return $"reader.GetUInt({tagNumber}){(nullAllowed ? "" : "!.Value")}";
                default:
                    return $"new {referenceName}(reader, {tagNumber})";
            }
        }

        private static string GetType(Tag tag)
        {
            switch (tag.Type)
            {
                case DataType.Any:
                    return "object";
                case DataType.Array:
                    return GetEnumerationType(tag) + "[]";
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
                        case 0:
                        case 8:
                            return "long";
                        default:
                            throw new InvalidDataException(tag.Name + " has length " + tag.LengthBytes);
                    }
                case DataType.List:
                    return "List<" + GetEnumerationType(tag) + ">";
                case DataType.Null:
                    throw new InvalidDataException("type of " + tag.Name + " is null");
                case DataType.Reference:
                case DataType.Enum:
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
                        case 0:
                        case 8:
                            return "ulong";
                        default:
                            throw new InvalidDataException(tag.Name + " has length " + tag.LengthBytes);
                    }
                default:
                    throw new InvalidDataException("Unsupported type " + tag.Type);
            }
        }

        private static bool GetEnumerationNullable(Tag tag)
        {
            if (tag.Children.Count == 0)
                return tag.Nullable;
            return tag.Children[0].Nullable!;
        }

        private static string GetEnumerationType(Tag tag)
        {
            if (tag.Children.Count == 0)
                return tag.ReferenceName ?? "object";
            return tag.Children[0].Name!;
        }

        private static string GetEnumerationIndex(Tag tag)
        {
            if (tag.Children.Count == 0)
                return "-1";
            return tag.Children[0].TagNumber.ToString();
        }
    }
}
