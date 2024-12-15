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

using Generator.Schema;
using System.Xml.Serialization;

namespace Generator
{
    public class ClusterGenerator
    {
        public static void Generate()
        {
            if (!Directory.Exists($"outputs\\Clusters\\"))
                Directory.CreateDirectory($"outputs\\Clusters\\");
            XmlSerializer deserializer = new XmlSerializer(typeof(Cluster));
            IEnumerable<string> clusterxmls = Directory.EnumerateFiles("..\\..\\..\\Clusters");
            foreach (string clusterxml in clusterxmls)
            {
                Console.WriteLine("Generating " + clusterxml + "...");
                Cluster? cluster = deserializer.Deserialize(File.OpenRead(clusterxml)) as Cluster;
                if (cluster == null)
                    throw new IOException("Failed to parse cluster " + clusterxml);
                WriteClass(cluster);
            }
        }

        private static void WriteClass(Cluster cluster)
        {
            string path = $"outputs\\Clusters\\" + cluster.name.Replace(" ", "") + ".cs";
            if (File.Exists(path))
                File.Delete(path);
            using (FileStream outstream = File.OpenWrite(path))
            {
                using (StreamWriter writer = new StreamWriter(outstream))
                    WriteClass(cluster, writer);
            }
        }

        private static void WriteClass(Cluster cluster, StreamWriter writer)
        {
            writer.WriteLine("// MatterDotNet Copyright (C) 2024 \n//\n// This program is free software: you can redistribute it and/or modify\n// it under the terms of the GNU Affero General Public License as published by\n// the Free Software Foundation, either version 3 of the License, or any later version.\n// This program is distributed in the hope that it will be useful,\n// but WITHOUT ANY WARRANTY, without even the implied warranty of\n// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.\n// See the GNU Affero General Public License for more details.\n// You should have received a copy of the GNU Affero General Public License\n// along with this program.  If not, see <http://www.gnu.org/licenses/>.\n//\n// WARNING: This file was auto-generated. Do not edit.\n".Replace("\n", "\r\n"));
            writer.WriteLine("using MatterDotNet.Messages.InteractionModel;\r\nusing MatterDotNet.Protocol;\r\nusing MatterDotNet.Protocol.Parsers;\r\nusing MatterDotNet.Protocol.Payloads;\r\nusing MatterDotNet.Protocol.Sessions;");
            writer.WriteLine();
            writer.WriteLine($"namespace MatterDotNet.Clusters\r\n{{");
            writer.WriteLine("    public class " + cluster.name.Replace(" ", "") + " : ClusterBase");
            writer.WriteLine("    {");
            writer.WriteLine("        private const uint CLUSTER_ID = " + cluster.clusterIds.clusterId.id + ";");
            writer.WriteLine();
            writer.WriteLine("        public " + cluster.name.Replace(" ", "") + "(ushort endPoint) : base(endPoint) { }");
            writer.WriteLine();
            if (cluster.dataTypes.@enum != null)
            {
                foreach (clusterDataTypesEnum enumType in cluster.dataTypes.@enum)
                    WriteEnum(enumType, writer);
            }
            if (cluster.dataTypes.@struct != null)
            {
                foreach (clusterDataTypesStruct structType in cluster.dataTypes.@struct)
                    WriteStruct(structType, cluster, writer);
            }
            if (cluster.commands != null && cluster.commands.Length > 0)
            {
                foreach (var command in cluster.commands)
                    WriteStruct(command, command.direction == "commandToServer", writer, cluster);
                WriteCommands(cluster, writer);
            }

            writer.WriteLine("        // Attributes");
            foreach (var attribute in cluster.attributes)
                WriteAttribute(attribute, writer);

            writer.WriteLine("    }");
            writer.Write("}");
            writer.Flush();
        }

        private static void WriteStruct(clusterCommand command, bool toServer, StreamWriter writer, Cluster cluster)
        {
            if (command.field == null)
                return;
            writer.WriteLine($"        {(toServer ? "private record" : "public struct")} " + GeneratorUtil.SanitizeName(command.name) + (toServer ? "Payload : TLVPayload {" : " {"));
            foreach (clusterCommandField field in command.field)
            {
                writer.Write("            public ");
                if (field.optionalConform == null)
                    writer.Write("required ");
                WriteType(field.type, null, writer);
                if (field.optionalConform != null)
                    writer.Write("?");
                if (field.name == GeneratorUtil.SanitizeName(command.name))
                    writer.Write(" " + field.name + "Field { get; set; }");
                else
                    writer.Write(" " + field.name + " { get; set; }");
                if (field.@default != null)
                    writer.WriteLine(" = " + SanitizeDefault(field.@default) + ";");
                else
                    writer.WriteLine();
            }
            if (toServer)
            {
                writer.WriteLine("            public override void Serialize(TLVWriter writer, long structNumber = -1) {");
                writer.WriteLine("                writer.StartStructure(structNumber);");
                foreach (clusterCommandField field in command.field)
                    WriteStructType(field.optionalConform != null, field.type, field.id, (field.name == GeneratorUtil.SanitizeName(command.name) ? field.name + "Field" : field.name), cluster, writer);
                writer.WriteLine("                writer.EndContainer();");
                writer.WriteLine("            }");
            }
            writer.WriteLine("        }");
            writer.WriteLine();
        }

        private static void WriteStructType(bool optional, string type, byte id, string name, Cluster cluster, StreamWriter writer)
        {
            string totalIndent = "                ";
            if (optional)
            {
                writer.WriteLine($"{totalIndent}if ({name} != null)");
                if (type != "list" && type != "array")
                    totalIndent += "    ";
            }
            switch (type)
            {
                case "array":
                    writer.WriteLine($"{totalIndent}{{");
                    writer.WriteLine($"{totalIndent}    writer.StartArray({id});");
                    writer.WriteLine($"{totalIndent}    foreach (var item in {name}) {{");
                    // writer.WriteLine($"{totalIndent}        {GetWriter(GetEnumerationType(child), GetEnumerationIndex(child))};");
                    writer.WriteLine($"{totalIndent}    }}");
                    writer.WriteLine($"{totalIndent}    writer.EndContainer();");
                    writer.WriteLine($"{totalIndent}}}");
                    break;
                case "list":
                    writer.WriteLine($"{totalIndent}{{");
                    writer.WriteLine($"{totalIndent}    writer.StartList({id});");
                    writer.WriteLine($"{totalIndent}    foreach (var item in {name}) {{");
                    // writer.WriteLine($"{totalIndent}        {GetWriter(GetEnumerationType(child), GetEnumerationIndex(child))};");
                    writer.WriteLine($"{totalIndent}    }}");
                    writer.WriteLine($"{totalIndent}    writer.EndContainer();");
                    writer.WriteLine($"{totalIndent}}}");
                    break;
                case "bool":
                    writer.WriteLine($"{totalIndent}writer.WriteBool({id}, {name});");
                    break;
                case "int8":
                    writer.WriteLine($"{totalIndent}writer.WriteSByte({id}, {name});");
                    break;
                case "int16":
                case "temperature":
                    writer.WriteLine($"{totalIndent}writer.WriteShort({id}, {name});");
                    break;
                case "int24":
                case "int32":
                    writer.WriteLine($"{totalIndent}writer.WriteInt({id}, {name});");
                    break;
                case "int40":
                case "int48":
                case "int56":
                case "int64":
                case "power-mW":
                case "amperage-mA":
                case "voltage-mW":
                case "energy-mWh":
                    writer.WriteLine($"{totalIndent}writer.WriteLong({id}, {name});");
                    break;
                case "uint8":
                case "tag":
                case "namespace":
                case "fabric-idx":
                case "action-id":
                    writer.WriteLine($"{totalIndent}writer.WriteByte({id}, {name});");
                    break;
                case "uint16":
                case "group-id":
                case "endpoint-no":
                case "vendor-id":
                case "entry-idx":
                case "percent100ths":
                    writer.WriteLine($"{totalIndent}writer.WriteUShort({id}, {name});");
                    break;
                case "uint24":
                case "uint32":
                case "epoch-s":
                case "elapsed-s":
                case "devtype-id":
                case "cluster-id":
                case "attrib-id":
                case "field-id":
                case "event-id":
                case "command-id":
                case "trans-id":
                case "data-ver":
                    writer.WriteLine($"{totalIndent}writer.WriteUInt({id}, {name});");
                    break;
                case "uint40":
                case "uint48":
                case "uint56":
                case "uint64":
                case "epoch-us":
                case "posix-ms":
                case "systime-us":
                case "systime-ms":
                case "fabric-id":
                case "node-id":
                case "EUI64":
                case "event-no":
                case "SubjectID":
                    writer.WriteLine($"{totalIndent}writer.WriteULong({id}, {name});");
                    break;
                case "single":
                    writer.WriteLine($"{totalIndent}writer.WriteFloat({id}, {name});");
                    break;
                case "double":
                    writer.WriteLine($"{totalIndent}writer.WriteDouble({id}, {name});");
                    break;
                case "octstr":
                case "ipadr":
                case "ipv4adr":
                case "ipv6adr":
                case "ipv6pre":
                case "hwadr":
                    writer.WriteLine($"{totalIndent}writer.WriteBytes({id}, {name});");
                    break;
                case "string":
                    writer.WriteLine($"{totalIndent}writer.WriteString({id}, {name});");
                    break;
                default:
                    if (HasEnum(cluster, type))
                        writer.WriteLine($"{totalIndent}writer.WriteUShort({id}, (ushort){name});");
                    else
                        writer.WriteLine($"{totalIndent}{name}.Serialize(writer, {id});");
                    break;
            }
        }

        private static bool HasEnum(Cluster cluster, string name)
        {
            foreach (clusterDataTypesEnum enumType in cluster.dataTypes.@enum)
            {
                if (enumType.name == name)
                    return true;
            }
            return false;
        }

        private static void WriteCommands(Cluster cluster, StreamWriter writer)
        {
            writer.WriteLine("        // Commands");
            foreach (clusterCommand cmd in cluster.commands)
            {
                if (cmd.direction == "commandToServer")
                {
                    writer.Write("        public async Task");
                    clusterCommand? response = GetResponseType(cluster.commands, cmd.response, writer);
                    if (cmd.response == "N")
                        writer.Write(" ");
                    else if (response == null)
                        writer.Write("<bool> ");
                    else
                        writer.Write("<" + GeneratorUtil.SanitizeName(response.name) + "?> ");
                    writer.Write(GeneratorUtil.SanitizeName(cmd.name) + " (Exchange exchange");
                    if (cmd.field != null)
                    {
                        foreach (var field in cmd.field)
                        {
                            writer.Write(", ");
                            WriteType(field.type, null, writer);
                            writer.Write(" " + field.name);
                        }
                    }
                    writer.WriteLine(") {");
                    if (cmd.field != null)
                    {
                        writer.WriteLine("            " + GeneratorUtil.SanitizeName(cmd.name) + "Payload requestFields = new " + GeneratorUtil.SanitizeName(cmd.name) + "Payload() {");
                        foreach (var field in cmd.field)
                            writer.WriteLine($"                {field.name} = {field.name},");
                        writer.WriteLine("            };");
                        writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecCommand(exchange, endPoint, CLUSTER_ID, " + cmd.id + ", requestFields);");
                    }
                    else
                    {
                        if (cmd.response != "N")
                            writer.WriteLine("            await InteractionManager.SendCommand(exchange, endPoint, CLUSTER_ID, " + cmd.id + ");");
                        else
                            writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecCommand(exchange, endPoint, CLUSTER_ID, " + cmd.id + ");");
                    }
                    if (response == null)
                    {
                        if (cmd.response != "N")
                            writer.WriteLine("            return validateResponse(resp);");
                    }
                    else
                    {
                        writer.WriteLine("            if (!validateResponse(resp))");
                        writer.WriteLine("                return null;");
                        writer.WriteLine("            return new " + GeneratorUtil.SanitizeName(response.name) + "() {");
                        foreach (var field in response.field)
                        {
                            writer.Write("                " + field.name + " = (");
                            WriteType(field.type, null, writer);
                            if (field.optionalConform != null)
                                writer.WriteLine($"?)GetOptionalField(resp, {field.id}),");
                            else
                                writer.WriteLine($")GetField(resp, {field.id}),");
                        }
                        writer.WriteLine("            };");
                    }
                    writer.WriteLine("        }");
                    writer.WriteLine();
                }
            }
            
        }

        private static clusterCommand? GetResponseType(clusterCommand[] cmds, string response, StreamWriter writer)
        {
            foreach (clusterCommand cmd in cmds)
            {
                if (cmd.name == response)
                    return cmd;
            }
            return null;
        }

        private static void WriteStruct(clusterDataTypesStruct structType, Cluster cluster, StreamWriter writer)
        {
            if (structType.field == null)
                return;
            writer.WriteLine("        public record " + GeneratorUtil.SanitizeName(structType.name) + " : TLVPayload {");
            foreach (clusterDataTypesStructField field in structType.field)
            {
                writer.Write("            public ");
                if (field.@default == null)
                    writer.Write("required ");
                WriteType(field.type, field.entry?.type, writer);
                if (field.@default != null)
                    writer.Write("?");
                if (field.name == GeneratorUtil.SanitizeName(structType.name))
                    writer.Write(" " + field.name + "Field { get; set; }");
                else
                    writer.Write(" " + field.name + " { get; set; }");
                if (field.@default != null)
                    writer.WriteLine(" = " + SanitizeDefault(field.@default) + ";");
                else
                    writer.WriteLine();
            }
            writer.WriteLine("            public override void Serialize(TLVWriter writer, long structNumber = -1) {");
            writer.WriteLine("                writer.StartStructure(structNumber);");
            foreach (clusterDataTypesStructField field in structType.field)
                WriteStructType(field.@default != null, field.type, field.id, (field.name == GeneratorUtil.SanitizeName(structType.name) ? field.name + "Field" : field.name), cluster, writer);
            writer.WriteLine("                writer.EndContainer();");
            writer.WriteLine("            }");
            writer.WriteLine("        }");
            writer.WriteLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="writer"></param>
        private static void WriteEnum(clusterDataTypesEnum enumType, StreamWriter writer)
        {
            if (enumType.item == null)
                return;
            writer.WriteLine("        public enum " + GeneratorUtil.SanitizeName(enumType.name) + " {");
            foreach (clusterDataTypesEnumItem item in enumType.item)
            {
                writer.WriteLine("            /// <summary>\r\n            /// " + item.summary.Replace("[[ref_", "<see cref=\"").Replace("]]", "\"/>") + "\r\n            /// </summary>");
                writer.WriteLine("            " + item.name + " = " + item.value + ",");
            }
            writer.WriteLine("        }");
            writer.WriteLine();
        }

        private static void WriteAttribute(clusterAttribute attribute, StreamWriter writer)
        {
            writer.Write("        public ");
            WriteType(attribute.type, attribute.entry?.type, writer);
            writer.Write(" " + attribute.name + " { ");
            if (attribute.access == null || attribute.access.read)
                writer.Write("get; ");
            if (attribute.access == null || attribute.access.write)
                writer.Write("set; ");
            if (attribute.@default == null || attribute.@default == "MS" || attribute.@default == "desc")
                writer.WriteLine("}");
            else if (attribute.type.EndsWith("Enum"))
                writer.WriteLine("} = " + attribute.type + "." + attribute.@default + ";");
            else
                writer.WriteLine("} = " + SanitizeDefault(attribute.@default) + ";");
        }

        private static void WriteType(string type, string? entryType, StreamWriter writer)
        {
            switch (type)
            {
                case "uint8":
                case "tag":
                case "namespace":
                case "fabric-idx":
                case "action-id":
                    writer.Write("byte");
                    break;
                case "uint16":
                case "group-id":
                case "endpoint-no":
                case "vendor-id":
                case "entry-idx":
                case "percent100ths":
                    writer.Write("ushort");
                    break;
                case "uint24":
                case "uint32":
                case "epoch-s":
                case "elapsed-s":
                case "devtype-id":
                case "cluster-id":
                case "attrib-id":
                case "field-id":
                case "event-id":
                case "command-id":
                case "trans-id":
                case "data-ver":
                    writer.Write("uint");
                    break;
                case "uint40":
                case "uint48":
                case "uint56":
                case "uint64":
                case "epoch-us":
                case "posix-ms":
                case "systime-us":
                case "systime-ms":
                case "fabric-id":
                case "node-id":
                case "EUI64":
                case "event-no":
                case "SubjectID":
                    writer.Write("ulong");
                    break;
                case "int8":
                    writer.Write("sbyte");
                    break;
                case "int16":
                case "temperature":
                    writer.Write("short");
                    break;
                case "int24":
                case "int32":
                    writer.Write("int ");
                    break;
                case "int40":
                case "int48":
                case "int56":
                case "int64":
                case "power-mW":
                case "amperage-mA":
                case "voltage-mW":
                case "energy-mWh":
                    writer.Write("long");
                    break;
                case "list":
                    writer.Write("List<");
                    WriteType(entryType!, null, writer);
                    writer.Write(">");
                    break;
                case "single":
                    writer.Write("float");
                    break;
                case "octstr":
                case "ipadr":
                case "ipv4adr":
                case "ipv6adr":
                case "ipv6pre":
                case "hwadr":
                    writer.Write("byte[]");
                    break;
                case "bool":
                case "string":
                case "double":
                    writer.Write($"{type}");
                    break;
                default:
                    writer.Write(GeneratorUtil.SanitizeName(type));
                    break;
            }
        }

        private static string SanitizeDefault(string value)
        {
            if (value == "\"")
                return "\"\"";
            if (value == "empty")
                return "[]";
            if (value.StartsWith('"'))
                return value;
            else
                return value.ToLowerInvariant();
        }
    }
}
