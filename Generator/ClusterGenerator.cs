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
using System.Collections.Immutable;
using System.Text;
using System.Xml.Serialization;

namespace Generator
{
    public class ClusterGenerator
    {
        private static HashSet<string> includes = new HashSet<string>();
        public static void Generate()
        {
            if (!Directory.Exists($"outputs\\Clusters\\"))
                Directory.CreateDirectory($"outputs\\Clusters\\");
            XmlSerializer deserializer = new XmlSerializer(typeof(Cluster));
            IEnumerable<string> clusterxmls = Directory.EnumerateFiles("..\\..\\..\\Clusters");
            foreach (string clusterxml in clusterxmls)
            {
                includes.Clear();
                if (clusterxml.EndsWith(".xml"))
                {
                    Console.WriteLine("Generating " + clusterxml + "...");
                    Cluster? cluster = deserializer.Deserialize(File.OpenRead(clusterxml)) as Cluster;
                    if (cluster == null)
                        throw new IOException("Failed to parse cluster " + clusterxml);
                    WriteClass(cluster);
                }
            }
        }

        private static void WriteClass(Cluster cluster)
        {
            string folder = $"outputs\\Clusters\\{GeneratorUtil.SanitizeName(cluster.classification.role) ?? "Misc"}\\";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            string path = folder + cluster.name.Replace(" ", "").Replace('/', '-') + ".cs";
            if (File.Exists(path))
                File.Delete(path);
            using (FileStream outstream = File.OpenWrite(path))
            {
                using (StreamWriter writer = new StreamWriter(outstream, Encoding.UTF8))
                {
                    writer.NewLine = "\n";
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    sw.NewLine = "\n";
                    WriteClass(cluster, sw);
                    writer.WriteLine("// MatterDotNet Copyright (C) 2025 \n//\n// This program is free software: you can redistribute it and/or modify\n// it under the terms of the GNU Affero General Public License as published by\n// the Free Software Foundation, either version 3 of the License, or any later version.\n// This program is distributed in the hope that it will be useful,\n// but WITHOUT ANY WARRANTY, without even the implied warranty of\n// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.\n// See the GNU Affero General Public License for more details.\n// You should have received a copy of the GNU Affero General Public License\n// along with this program.  If not, see <http://www.gnu.org/licenses/>.\n//\n// WARNING: This file was auto-generated. Do not edit.\n");
                    foreach (string include in includes.ToImmutableSortedSet())
                        writer.WriteLine("using " + include + ";");
                    writer.Write(sb);
                }
            }
        }

        private static void WriteClass(Cluster cluster, TextWriter writer)
        {
            includes.Add("MatterDotNet.Protocol.Parsers");
            includes.Add("MatterDotNet.Protocol.Payloads");
            includes.Add("MatterDotNet.Protocol.Sessions");
            writer.WriteLine();
            writer.WriteLine($"namespace MatterDotNet.Clusters.{GeneratorUtil.SanitizeName(cluster.classification.role) ?? "Misc"}\n{{");
            writer.WriteLine("    /// <summary>");
            writer.WriteLine($"    /// {cluster.name}");
            writer.WriteLine("    /// </summary>");
            writer.WriteLine("    public class " + cluster.name.Replace(" ", "").Replace('/', '_') + " : ClusterBase");
            writer.WriteLine("    {");
            writer.WriteLine("        private const uint CLUSTER_ID = " + cluster.clusterIds.clusterId.id + ";");
            writer.WriteLine();
            writer.WriteLine("        /// <summary>");
            writer.WriteLine($"        /// {cluster.name}");
            writer.WriteLine("        /// </summary>");
            writer.WriteLine("        public " + cluster.name.Replace(" ", "").Replace('/', '_') + "(ushort endPoint) : base(CLUSTER_ID, endPoint) { }");
            writer.WriteLine();
            if (cluster.dataTypes.@enum != null || (cluster.features != null && cluster.features.Length > 0))
            {
                writer.WriteLine("        #region Enums");
                bool firstEnum=true;
                if (cluster.features != null && cluster.features.Length > 0)
                {
                    firstEnum = false;
                    WriteFeatures(cluster.features, writer);
                }
                if (cluster.dataTypes.@enum != null)
                {
                    foreach (clusterDataTypesEnum enumType in cluster.dataTypes.@enum)
                    {
                        if (enumType.item == null)
                            continue;
                        if (firstEnum)
                            firstEnum = false;
                        else
                            writer.WriteLine();
                        WriteEnum(enumType, writer);
                    }
                }
                if (cluster.dataTypes.bitmap != null)
                {
                    foreach (clusterDataTypesBitfield bitmapType in cluster.dataTypes.bitmap)
                    {
                        if (bitmapType.bitfield == null)
                            continue;
                        if (firstEnum)
                            firstEnum = false;
                        else
                            writer.WriteLine();
                        WriteBitfield(bitmapType, writer);
                    }
                }
                writer.WriteLine("        #endregion Enums");
                writer.WriteLine();
            }
            if (cluster.dataTypes.@struct != null)
            {
                includes.Add("System.Diagnostics.CodeAnalysis");
                writer.WriteLine("        #region Records");
                bool firstRecord = true;
                foreach (clusterDataTypesStruct structType in cluster.dataTypes.@struct)
                {
                    if (structType.field == null)
                        continue;
                    if (firstRecord)
                        firstRecord = false;
                    else
                        writer.WriteLine();
                    WriteStruct(structType, cluster, writer);
                }
                writer.WriteLine("        #endregion Records");
                writer.WriteLine();
            }
            if (cluster.commands != null && cluster.commands.Length > 0)
            {
                includes.Add("MatterDotNet.Messages.InteractionModel");
                includes.Add("MatterDotNet.Protocol");
                writer.WriteLine("        #region Payloads");
                bool firstPayload = true;
                foreach (var command in cluster.commands)
                {
                    if (command.field == null)
                        continue;
                    if (firstPayload)
                        firstPayload = false;
                    else
                        writer.WriteLine();
                    WriteStruct(command, command.direction == "commandToServer", writer, cluster);
                }
                writer.WriteLine("        #endregion Payloads");
                writer.WriteLine();
                WriteCommands(cluster, writer);
            }

            if (cluster.attributes.Length > 0 || (cluster.features != null && cluster.features.Length > 0))
            {
                writer.WriteLine("        #region Attributes");
                bool firstAttribute = true;
                if (cluster.features != null && cluster.features.Length > 0)
                {
                    firstAttribute = false;
                    WriteFeatureFunctions(writer);
                }
                foreach (var attribute in cluster.attributes)
                {
                    if (attribute.type != null)
                    {
                        if (firstAttribute)
                            firstAttribute = false;
                        else
                            writer.WriteLine();
                        WriteAttribute(cluster, attribute, writer);
                    }
                }
                writer.WriteLine("        #endregion Attributes");
            }
            writer.WriteLine("    }");
            writer.Write("}");
            writer.Flush();
        }

        private static void WriteFeatureFunctions(TextWriter writer)
        {
            writer.WriteLine("        /// <summary>\n        /// Features supported by this cluster\n        /// </summary>\n        /// <param name=\"session\"></param>\n        /// <returns></returns>\n        public async Task<Feature> GetSupportedFeatures(SecureSession session)\n        {\n            return (Feature)(byte)(await GetAttribute(session, 0xFFFC))!;\n        }\n\n        /// <summary>\n        /// Returns true when the feature is supported by the cluster\n        /// </summary>\n        /// <param name=\"session\"></param>\n        /// <param name=\"feature\"></param>\n        /// <returns></returns>\n        public async Task<bool> Supports(SecureSession session, Feature feature)\n        {\n            return ((feature & await GetSupportedFeatures(session)) != 0);\n        }");
        }

        private static void WriteStruct(clusterCommand command, bool toServer, TextWriter writer, Cluster cluster)
        {
            if (!toServer)
                writer.WriteLine($"        /// <summary>\n        /// {GeneratorUtil.FieldNameToComment(command.name)} - Reply from server\n        /// </summary>");
           writer.WriteLine($"        {(toServer ? "private record" : "public struct")} " + GeneratorUtil.SanitizeName(command.name) + (toServer ? "Payload : TLVPayload {" : "() {"));
            foreach (clusterCommandField field in command.field)
            {
                if (field.type == null || field.disallowConform != null) //Reserved/removed fields
                    continue;
                writer.Write("            public ");
                if (field.optionalConform == null)
                    writer.Write("required ");
                WriteType(field.type, field.entry?.type, writer);
                if (field.optionalConform != null || field.quality?.nullable == true)
                    writer.Write("?");
                if (field.name == GeneratorUtil.SanitizeName(command.name))
                    writer.Write(" " + field.name + "Field { get; set; }");
                else
                    writer.Write(" " + field.name + " { get; set; }");
                if (field.@default != null && DefaultValid(field.@default))
                {
                    if (field.type.EndsWith("Enum"))
                        writer.WriteLine(" = " + field.type + "." + field.@default + ";");
                    else
                        writer.WriteLine(" = " + SanitizeDefault(field.@default, "") + ";");
                }
                else
                    writer.WriteLine();
            }
            if (toServer)
            {
                writer.WriteLine("            internal override void Serialize(TLVWriter writer, long structNumber = -1) {");
                writer.WriteLine("                writer.StartStructure(structNumber);");
                foreach (clusterCommandField field in command.field)
                {
                    if (field.type == null || field.disallowConform != null) //Reserved/removed fields
                        continue;
                    int? from = null;
                    int? to = null;
                    if (field.constraint != null)
                    {
                        switch (field.constraint.type)
                        {
                            case "min":
                            case "minCount":
                            case "minLength":
                                if (int.TryParse(field.constraint.value, out int fromVal))
                                    from = fromVal;
                                break;
                            case "max":
                            case "maxCount":
                            case "maxLength":
                                if (int.TryParse(field.constraint.value, out int toVal))
                                    to = toVal;
                                break;
                            case "between":
                            case "lengthBetween":
                                to = field.constraint.to;
                                from = field.constraint.from;
                                break;
                            case "desc":
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    WriteStructType(field.mandatoryConform == null, field.quality?.nullable == true, field.type, field.entry?.type, field.id, from, to, (field.name == GeneratorUtil.SanitizeName(command.name) ? field.name + "Field" : field.name), cluster, writer);
                }
                writer.WriteLine("                writer.EndContainer();");
                writer.WriteLine("            }");
            }
            writer.WriteLine("        }");
        }

        private static void WriteStructType(bool optional, bool nullable, string type, string? entryType, int id, int? from, int? to, string name, Cluster cluster, TextWriter writer)
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
                    if (optional || nullable)
                        writer.WriteLine($"{totalIndent}if ({name} != null)");
                    writer.WriteLine($"{totalIndent}{{");
                    if (from != null || to != null)
                        writer.WriteLine($"{totalIndent}    Constrain({name}, {from ?? 0}{(to == null ? "" : $", {to}")});");
                    writer.WriteLine($"{totalIndent}    writer.StartArray({id});");
                    writer.WriteLine($"{totalIndent}    foreach (var item in {name}) {{");
                    writer.Write("        ");
                    WriteStructType(false, false, entryType!, null, -1, null, null, "item", cluster, writer);
                    writer.WriteLine($"{totalIndent}    }}");
                    writer.WriteLine($"{totalIndent}    writer.EndContainer();");
                    writer.WriteLine($"{totalIndent}}}");
                    if (nullable)
                        writer.WriteLine($"{totalIndent}else\n{totalIndent}    writer.WriteNull({id});");
                    break;
                case "list":
                    if (optional || nullable)
                        writer.WriteLine($"{totalIndent}if ({name} != null)");
                    writer.WriteLine($"{totalIndent}{{");
                    if (from != null || to != null)
                        writer.WriteLine($"{totalIndent}    Constrain({name}, {from ?? 0}{(to == null ? "" : $", {to}")});");
                    writer.WriteLine($"{totalIndent}    writer.StartList({id});");
                    writer.WriteLine($"{totalIndent}    foreach (var item in {name}) {{");
                    writer.Write("        ");
                    WriteStructType(false, false, entryType!, null, -1, null, null, "item", cluster, writer);
                    writer.WriteLine($"{totalIndent}    }}");
                    writer.WriteLine($"{totalIndent}    writer.EndContainer();");
                    writer.WriteLine($"{totalIndent}}}");
                    if (nullable)
                        writer.WriteLine($"{totalIndent}else\n{totalIndent}    writer.WriteNull({id});");
                    break;
                case "bool":
                    writer.WriteLine($"{totalIndent}writer.WriteBool({id}, {name});");
                    break;
                case "int8":
                    writer.Write($"{totalIndent}writer.WriteSByte({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", sbyte.MaxValue");
                    if (from != null)
                        writer.WriteLine($", {from.Value});");
                    else
                        writer.WriteLine(");");
                    break;
                case "int16":
                case "temperature":
                    writer.Write($"{totalIndent}writer.WriteShort({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", short.MaxValue");
                    if (from != null)
                        writer.WriteLine($", {from.Value});");
                    else
                        writer.WriteLine(");");
                    break;
                case "int24":
                case "int32":
                    writer.Write($"{totalIndent}writer.WriteInt({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", int.MaxValue");
                    if (from != null)
                        writer.WriteLine($", {from.Value});");
                    else
                        writer.WriteLine(");");
                    break;
                case "int40":
                case "int48":
                case "int56":
                case "int64":
                case "power-mW":
                case "amperage-mA":
                case "voltage-mW":
                case "energy-mWh":
                    writer.Write($"{totalIndent}writer.WriteLong({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", long.MaxValue");
                    if (from != null)
                        writer.WriteLine($", {from.Value});");
                    else
                        writer.WriteLine(");");
                    break;
                case "uint8":
                case "enum8":
                case "tag":
                case "namespace":
                case "fabric-idx":
                case "action-id":
                    writer.Write($"{totalIndent}writer.WriteByte({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", byte.MaxValue");
                    if (from != null && from != 0)
                        writer.WriteLine($", {from.Value});");
                    else
                        writer.WriteLine(");");
                    break;
                case "uint16":
                case "enum16":
                case "group-id":
                case "endpoint-no":
                case "vendor-id":
                case "entry-idx":
                case "percent100ths":
                    writer.Write($"{totalIndent}writer.WriteUShort({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", ushort.MaxValue");
                    if (from != null && from != 0)
                        writer.WriteLine($", {from.Value});");
                    else
                        writer.WriteLine(");");
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
                    writer.Write($"{totalIndent}writer.WriteUInt({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", uint.MaxValue");
                    if (from != null && from != 0)
                        writer.WriteLine($", {from.Value});");
                    else
                        writer.WriteLine(");");
                    break;
                case "uint40":
                case "uint48":
                case "uint56":
                case "uint64":
                case "epoch-us":
                case "ref_DataTypePosixMs":
                case "systime-us":
                case "ref_DataTypeSystemTimeMs":
                case "fabric-id":
                case "node-id":
                case "EUI64":
                case "event-no":
                case "SubjectID":
                    writer.Write($"{totalIndent}writer.WriteULong({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", ulong.MaxValue");
                    if (from != null && from != 0)
                        writer.WriteLine($", {from.Value});");
                    else
                        writer.WriteLine(");");
                    break;
                case "single":
                    writer.Write($"{totalIndent}writer.WriteFloat({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", float.MaxValue");
                    if (from != null)
                        writer.WriteLine($", {from.Value});");
                    else
                        writer.WriteLine(");");
                    break;
                case "double":
                    writer.Write($"{totalIndent}writer.WriteDouble({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", double.MaxValue");
                    if (from != null)
                        writer.WriteLine($", {from.Value});");
                    else
                        writer.WriteLine(");");
                    break;
                case "ref_IpAdr":
                case "ref_Ipv4Adr":
                case "ref_Ipv6Adr":
                    writer.WriteLine($"{totalIndent}writer.WriteBytes({id}, {name}.GetAddressBytes());");
                    break;
                case "Hardware Address":
                    writer.WriteLine($"{totalIndent}writer.WriteBytes({id}, {name}.GetAddressBytes());");
                    break;
                case "octstr":
                case "ipv6pre":
                    writer.Write($"{totalIndent}writer.WriteBytes({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", int.MaxValue");
                    if (from != null)
                        writer.WriteLine($", {from.Value});");
                    else
                        writer.WriteLine(");");
                    break;
                case "string":
                    writer.Write($"{totalIndent}writer.WriteString({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", int.MaxValue");
                    if (from != null)
                        writer.WriteLine($", {from.Value});");
                    else
                        writer.WriteLine(");");
                    break;
                default:
                    if (HasEnum(cluster, type) || HasBitmap(cluster, type))
                        writer.WriteLine($"{totalIndent}writer.WriteUShort({id}, (ushort){name});");
                    else
                        writer.WriteLine($"{totalIndent}{name}.Serialize(writer, {id});");
                    break;
            }
        }

        private static void WriteFieldReader(bool optional, bool nullable, string type, string? entryType, int id, int? from, int? to, string name, string structName, Cluster cluster, TextWriter writer)
        {
            string totalIndent = "                ";
            if (id != -1 && type != "list")
            {
                if (name == GeneratorUtil.SanitizeName(structName))
                    writer.Write($"{totalIndent}{name}Field = ");
                else
                    writer.Write($"{totalIndent}{name} = ");
            }
            switch (type)
            {
                case "array":
                    throw new NotImplementedException();
                case "list":
                    writer.WriteLine($"{totalIndent}{{");
                    writer.Write($"{totalIndent}    {name} = new List<");
                    WriteType(entryType!, null, writer);
                    writer.WriteLine(">();");
                    writer.WriteLine($"{totalIndent}    foreach (var item in (List<object>)fields[{id}]) {{");
                    writer.Write($"{totalIndent}        {name}.Add(");
                    WriteFieldReader(false, false, entryType!, null, -1, null, null, "item", structName, cluster, writer);
                    writer.WriteLine($"{totalIndent}    }}");
                    writer.WriteLine($"{totalIndent}}}");
                    return;
                case "bool":
                    writer.Write($"reader.GetBool({id}");
                    break;
                case "int8":
                    writer.Write($"reader.GetSByte({id}");
                    break;
                case "int16":
                case "temperature":
                    writer.Write($"reader.GetShort({id}");
                    break;
                case "int24":
                case "int32":
                    writer.Write($"reader.GetInt({id}");
                    break;
                case "int40":
                case "int48":
                case "int56":
                case "int64":
                case "power-mW":
                case "amperage-mA":
                case "voltage-mW":
                case "energy-mWh":
                    writer.Write($"reader.GetLong({id}");
                    break;
                case "uint8":
                case "enum8":
                case "tag":
                case "namespace":
                case "fabric-idx":
                case "action-id":
                    writer.Write($"reader.GetByte({id}");
                    break;
                case "uint16":
                case "enum16":
                case "group-id":
                case "endpoint-no":
                case "vendor-id":
                case "entry-idx":
                case "percent100ths":
                    writer.Write($"reader.GetUShort({id}");
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
                    writer.Write($"reader.GetUInt({id}");
                    break;
                case "uint40":
                case "uint48":
                case "uint56":
                case "uint64":
                case "epoch-us":
                case "ref_DataTypePosixMs":
                case "systime-us":
                case "ref_DataTypeSystemTimeMs":
                case "fabric-id":
                case "node-id":
                case "EUI64":
                case "event-no":
                case "SubjectID":
                    writer.Write($"reader.GetULong({id}");
                    break;
                case "single":
                    writer.Write($"reader.GetFloat({id}");
                    break;
                case "double":
                    writer.Write($"reader.GetDouble({id}");
                    break;
                case "ref_IpAdr":
                    writer.WriteLine($"new IPAddress(reader.GetBytes({id}, {(optional ? "true" : "false")}, 16, 4{(id == -1 ? ")!))" : ")!)")};");
                    return;
                case "ref_Ipv4Adr":
                    writer.WriteLine($"new IPAddress(reader.GetBytes({id}, {(optional ? "true" : "false")}, 4, 4{(id == -1 ? ")!))" : ")!)")};");
                    return;
                case "ref_Ipv6Adr":
                    writer.WriteLine($"new IPAddress(reader.GetBytes({id}, {(optional ? "true" : "false")}, 16, 16{(id == -1 ? ")!))" : ")!)")};");
                    return;
                case "Hardware Address":
                    writer.WriteLine($"new PhysicalAddress(reader.GetBytes({id}, {(optional ? "true" : "false")}, 8, 6{(id == -1 ? ")!))" : ")!)")};");
                    return;
                case "octstr":
                case "ipv6pre":
                    writer.Write($"reader.GetBytes({id}, {(optional ? "true" : "false")}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", int.MaxValue");
                    if (from != null)
                        writer.WriteLine($", {from.Value}");
                    writer.Write(')');
                    if (!optional)
                        writer.Write('!');
                    if (id == -1)
                        writer.WriteLine(");");
                    else
                        writer.WriteLine(';');
                    return;
                case "string":
                    writer.Write($"reader.GetString({id}, {(optional ? "true" : "false")}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", int.MaxValue");
                    if (from != null)
                        writer.Write($", {from.Value}");
                    writer.Write(')');
                    if (!optional)
                        writer.Write('!');
                    if (id == -1)
                        writer.WriteLine(");");
                    else
                        writer.WriteLine(';');
                    return;
                default:
                    if (HasEnum(cluster, type) || HasBitmap(cluster, type))
                    {
                        writer.Write($"({type})reader.GetUShort({id}");
                        if (optional)
                            writer.Write(", true");
                        if (id == -1)
                            writer.WriteLine(")!.Value);");
                        else
                            writer.WriteLine(")!.Value;");
                    }
                    else
                        writer.WriteLine($"new {GeneratorUtil.SanitizeName(type)}({(id == -1 ? "(object[])item))" : "fields[{id}])")};");
                    return;
            }
            if (optional)
                writer.Write(", true)");
            else
                writer.Write(")!.Value");
            if (id == -1)
                writer.WriteLine(");");
            else
                writer.WriteLine(';');
        }

        private static bool HasEnum(Cluster cluster, string name)
        {
            if (cluster.dataTypes.@enum == null)
                return false;
            foreach (clusterDataTypesEnum enumType in cluster.dataTypes.@enum)
            {
                if (enumType.name == name)
                    return true;
            }
            return false;
        }

        private static bool HasEnumValue(Cluster cluster, string name, string value)
        {
            if (cluster.dataTypes.@enum == null)
                return false;
            foreach (clusterDataTypesEnum enumType in cluster.dataTypes.@enum)
            {
                if (enumType.name == name)
                {
                    foreach (var item in enumType.item)
                    {
                        if (item.name == value)
                            return true;
                    }
                }
            }
            return false;
        }

        private static bool HasBitmap(Cluster cluster, string name)
        {
            if (cluster.dataTypes.bitmap == null)
                return false;
            foreach (clusterDataTypesBitfield bitfieldType in cluster.dataTypes.bitmap)
            {
                if (bitfieldType.name == name)
                    return true;
            }
            return false;
        }

        private static bool HasBitmapValue(Cluster cluster, string name, string value)
        {
            if (cluster.dataTypes.bitmap == null)
                return false;
            foreach (clusterDataTypesBitfield bitfieldType in cluster.dataTypes.bitmap)
            {
                if (bitfieldType.name == name)
                {
                    foreach (var bitfield in bitfieldType.bitfield)
                    {
                        if (bitfield.name == value)
                            return true;
                    }
                }
            }
            return false;
        }

        private static bool HasStruct(Cluster cluster, string name)
        {
            if (cluster.dataTypes.@struct == null)
                return false;
            foreach (clusterDataTypesStruct structType in cluster.dataTypes.@struct)
            {
                if (structType.name == name)
                    return true;
            }
            return false;
        }

        private static void WriteCommands(Cluster cluster, TextWriter writer)
        {
            writer.WriteLine("        #region Commands");
            bool first = true;
            foreach (clusterCommand cmd in cluster.commands)
            {
                if (cmd.direction == "commandToServer")
                {
                    if (first)
                        first = false;
                    else
                        writer.WriteLine();
                    writer.WriteLine("        /// <summary>");
                    writer.WriteLine("        /// " + GeneratorUtil.FieldNameToComment(cmd.name));
                    writer.WriteLine("        /// </summary>");
                    writer.Write("        public async Task");
                    clusterCommand? response = GetResponseType(cluster.commands, cmd.response, writer);
                    if (cmd.response == "N")
                        writer.Write(" ");
                    else if (response == null)
                        writer.Write("<bool> ");
                    else
                        writer.Write("<" + GeneratorUtil.SanitizeName(response.name) + "?> ");
                    writer.Write(GeneratorUtil.SanitizeName(cmd.name) + "(SecureSession session");
                    if (cmd.access.timed)
                        writer.Write(", ushort commandTimeoutMS");
                    if (cmd.field != null)
                    {
                        foreach (var field in cmd.field)
                        {
                            if (field.type == null || field.disallowConform != null)
                                continue;
                            writer.Write(", ");
                            WriteType(field.type, field.entry?.type, writer);
                            if (field.optionalConform != null)
                                writer.Write('?');
                            writer.Write(" " + field.name);
                        }
                    }
                    writer.WriteLine(") {");
                    if (cmd.field != null)
                    {
                        writer.WriteLine("            " + GeneratorUtil.SanitizeName(cmd.name) + "Payload requestFields = new " + GeneratorUtil.SanitizeName(cmd.name) + "Payload() {");
                        foreach (var field in cmd.field)
                        {
                            if (field.type == null || field.disallowConform != null)
                                continue;
                            writer.WriteLine($"                {field.name} = {field.name},");
                        }
                        writer.WriteLine("            };");
                        if (cmd.access.timed)
                            writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, CLUSTER_ID, commandTimeoutMS, " + cmd.id + ", requestFields);");
                        else
                            writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, " + cmd.id + ", requestFields);");
                    }
                    else
                    {
                        if (cmd.response == "N")
                        {
                            if (cmd.access.timed)
                                writer.WriteLine("            await InteractionManager.SendTimedCommand(session, endPoint, CLUSTER_ID, commandTimeoutMS, " + cmd.id + ");");
                            else
                                writer.WriteLine("            await InteractionManager.SendCommand(session, endPoint, CLUSTER_ID, " + cmd.id + ");");
                        }
                        else
                        {
                            if (cmd.access.timed)
                                writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, CLUSTER_ID, commandTimeoutMS, " + cmd.id + ");");
                            else
                                writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, " + cmd.id + ");");
                        }
                    }
                    if (response == null)
                    {
                        if (cmd.response != "N")
                            writer.WriteLine("            return ValidateResponse(resp);");
                    }
                    else
                    {
                        writer.WriteLine("            if (!ValidateResponse(resp))");
                        writer.WriteLine("                return null;");
                        writer.WriteLine("            return new " + GeneratorUtil.SanitizeName(response.name) + "() {");
                        foreach (var field in response.field)
                        {
                            writer.Write("                " + field.name + " = (");
                            WriteType(field.type, field.entry?.type, writer);
                            if (field.optionalConform != null)
                            {
                                if (HasEnum(cluster, field.type) || HasBitmap(cluster, field.type))
                                    writer.Write("?)(byte");
                                writer.WriteLine($"?)GetOptionalField(resp, {field.id}),");
                            }
                            else
                            {
                                if (HasEnum(cluster, field.type) || HasBitmap(cluster, field.type))
                                    writer.Write(")(byte");
                                writer.WriteLine($")GetField(resp, {field.id}),");
                            }
                        }
                        writer.WriteLine("            };");
                    }
                    writer.WriteLine("        }");
                }
            }
            writer.WriteLine("        #endregion Commands");
            writer.WriteLine();
        }

        private static clusterCommand? GetResponseType(clusterCommand[] cmds, string response, TextWriter writer)
        {
            foreach (clusterCommand cmd in cmds)
            {
                if (cmd.name == response)
                    return cmd;
            }
            return null;
        }

        private static void WriteStruct(clusterDataTypesStruct structType, Cluster cluster, TextWriter writer)
        {
            writer.WriteLine($"        /// <summary>\n        /// {GeneratorUtil.FieldNameToComment(structType.name)}\n        /// </summary>");
            writer.WriteLine("        public record " + GeneratorUtil.SanitizeName(structType.name) + " : TLVPayload {\n            [SetsRequiredMembers]");
            
            writer.WriteLine($"            internal {GeneratorUtil.SanitizeName(structType.name)}(object[] fields) {{");
            writer.WriteLine("                FieldReader reader = new FieldReader(fields);");
            foreach (clusterDataTypesStructField field in structType.field)
                WriteFieldReader(field.mandatoryConform == null, field.quality?.nullable == true, field.type, field.entry?.type, field.id, field.constraint?.fromSpecified == true ? field.constraint.from : null, field.constraint?.toSpecified == true ? field.constraint.to : null, field.name, structType.name, cluster, writer);

            writer.WriteLine("            }");
            foreach (clusterDataTypesStructField field in structType.field)
            {
                bool hasDefault = field.@default != null && DefaultValid(field.@default);
                if (hasDefault && HasEnum(cluster, field.type) && !HasEnumValue(cluster, field.type, field.@default!))
                    hasDefault = false;
                if (hasDefault && HasBitmap(cluster, field.type) && !HasBitmapValue(cluster, field.type, field.@default!))
                    hasDefault = false;
                writer.Write("            public ");
                if (field.mandatoryConform != null)
                    writer.Write("required ");
                WriteType(field.type, field.entry?.type, writer);
                if (field.mandatoryConform == null || field.quality?.nullable == true)
                    writer.Write("?");
                if (field.name == GeneratorUtil.SanitizeName(structType.name))
                    writer.Write(" " + field.name + "Field { get; set; }");
                else
                    writer.Write(" " + field.name + " { get; set; }");
                if (hasDefault)
                {
                    if (HasEnum(cluster, field.type) || HasBitmap(cluster, field.type))
                        writer.WriteLine(" = " + field.type + "." + field.@default + ";");
                    else
                        writer.WriteLine(" = " + SanitizeDefault(field.@default!, "") + ";");
                }
                else
                    writer.WriteLine();
            }
            writer.WriteLine("            internal override void Serialize(TLVWriter writer, long structNumber = -1) {");
            writer.WriteLine("                writer.StartStructure(structNumber);");
            foreach (clusterDataTypesStructField field in structType.field)
            {
                int? from = null;
                int? to = null;
                if (field.constraint != null)
                {
                    switch (field.constraint.type)
                    {
                        case "min":
                        case "minCount":
                        case "minLength":
                            if (int.TryParse(field.constraint.value, out int fromVal))
                                from = fromVal;
                            break;
                        case "max":
                        case "maxCount":
                        case "maxLength":
                            if (int.TryParse(field.constraint.value, out int toVal))
                                to = toVal;
                            break;
                        case "between":
                        case "lengthBetween":
                            to = field.constraint.to;
                            from = field.constraint.from;
                            break;
                        case "desc":
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                WriteStructType(field.mandatoryConform == null, field.quality?.nullable == true, field.type, field.entry?.type, field.id, from, to, (field.name == GeneratorUtil.SanitizeName(structType.name) ? field.name + "Field" : field.name), cluster, writer);
            }
            writer.WriteLine("                writer.EndContainer();");
            writer.WriteLine("            }");
            writer.WriteLine("        }");
        }

        private static void WriteEnum(clusterDataTypesEnum enumType, TextWriter writer)
        {
            writer.WriteLine("        /// <summary>");
            writer.WriteLine("        /// " + GeneratorUtil.FieldNameToComment(enumType.name));
            writer.WriteLine("        /// </summary>");
            writer.WriteLine("        public enum " + GeneratorUtil.SanitizeName(enumType.name) + " {");
            foreach (clusterDataTypesEnumItem item in enumType.item)
            {
                if (item.summary != null)
                    writer.WriteLine("            /// <summary>\n            /// " + item.summary.Replace("[[ref_", "<see cref=\"").Replace("]]", "\"/>") + "\n            /// </summary>");
                writer.WriteLine("            " + item.name + " = " + item.value + ",");
            }
            writer.WriteLine("        }");
        }

        private static void WriteBitfield(clusterDataTypesBitfield enumType, TextWriter writer)
        {
            writer.WriteLine("        /// <summary>");
            writer.WriteLine("        /// " + GeneratorUtil.FieldNameToComment(enumType.name));
            writer.WriteLine("        /// </summary>");
            writer.WriteLine("        [Flags]");
            writer.WriteLine("        public enum " + GeneratorUtil.SanitizeName(enumType.name) + " {");
            foreach (clusterDataTypesBitfieldItem item in enumType.bitfield)
            {
                if (item.summary != null)
                    writer.WriteLine("            /// <summary>\n            /// " + item.summary.Replace("[[ref_", "<see cref=\"").Replace("]]", "\"/>") + "\n            /// </summary>");
                writer.WriteLine("            " + item.name + " = " + (1<<item.bit) + ",");
            }
            writer.WriteLine("        }");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="features"></param>
        /// <param name="writer"></param>
        private static void WriteFeatures(clusterFeature[] features, TextWriter writer)
        {
            writer.WriteLine("        /// <summary>");
            writer.WriteLine("        /// Supported Features");
            writer.WriteLine("        /// </summary>");
            writer.WriteLine("        [Flags]");
            writer.WriteLine("        public enum Feature {");
            foreach (clusterFeature item in features)
            {
                writer.WriteLine("            /// <summary>\n            /// " + item.summary.Replace("[[ref_", "<see cref=\"").Replace("]]", "\"/>") + "\n            /// </summary>");
                writer.WriteLine("            " + item.name + " = " + (1 << item.bit) + ",");
            }
            writer.WriteLine("        }");
        }

        private static void WriteAttribute(Cluster cluster, clusterAttribute attribute, TextWriter writer)
        {
            bool hasDefault = attribute.@default != null && DefaultValid(attribute.@default);
            if (hasDefault && HasEnum(cluster, attribute.type) && !HasEnumValue(cluster, attribute.type, attribute.@default!))
                hasDefault = false;
            if (hasDefault && HasBitmap(cluster, attribute.type) && !HasBitmapValue(cluster, attribute.type, attribute.@default!))
                hasDefault = false;
            if (attribute.access.read)
            {
                writer.WriteLine($"        /// <summary>\n        /// Get the {GeneratorUtil.FieldNameToComment(attribute.name)} attribute\n        /// </summary>");
                writer.Write("        public async Task<");
                WriteType(attribute.type, attribute.entry?.type, writer);
                if (attribute.quality?.nullable == true)
                    writer.Write('?');
                writer.WriteLine("> Get" + attribute.name + " (SecureSession session) {");
                writer.Write("            return ");
                if (HasStruct(cluster, attribute.type))
                    writer.Write($"new {GeneratorUtil.SanitizeName(attribute.type)}((object[])(await GetAttribute(session, " + Convert.ToUInt16(attribute.id, 16) + "))!)");
                else
                {
                    writer.Write('(');
                    WriteType(attribute.type, attribute.entry?.type, writer);
                    if (attribute.quality?.nullable == true || hasDefault)
                        writer.Write('?');
                    if (HasEnum(cluster, attribute.type) || HasBitmap(cluster, attribute.type))
                        writer.Write(")await GetEnumAttribute(session, " + Convert.ToUInt16(attribute.id, 16));
                    else
                    {
                        writer.Write(")(dynamic?)");
                        if (attribute.quality?.nullable != true && !hasDefault)
                            writer.Write('(');
                        writer.Write("await GetAttribute(session, " + Convert.ToUInt16(attribute.id, 16));
                    }
                    if (attribute.quality?.nullable == true)
                        writer.Write(", true");
                    writer.Write(')');
                    if (!HasEnum(cluster, attribute.type) && (attribute.quality?.nullable != true && !hasDefault))
                        writer.Write(")!");
                }

                if (hasDefault)
                {
                    if (HasEnum(cluster, attribute.type) || HasBitmap(cluster, attribute.type))
                        writer.WriteLine(" ?? " + attribute.type + "." + attribute.@default + ";");
                    else
                        writer.WriteLine(" ?? " + SanitizeDefault(attribute.@default!, attribute.type, attribute.entry?.type) + ";");
                }
                else
                    writer.WriteLine(";");
                writer.WriteLine("        }");
            }
            if (attribute.access.write)
            {
                if (attribute.access.read)
                    writer.WriteLine();
                writer.WriteLine($"        /// <summary>\n        /// Set the {GeneratorUtil.FieldNameToComment(attribute.name)} attribute\n        /// </summary>");
                writer.Write("        public async Task Set" + attribute.name + " (SecureSession session, ");
                WriteType(attribute.type, attribute.entry?.type, writer);
                if (attribute.quality?.nullable == true || hasDefault)
                    writer.Write('?');
                writer.Write(" value");
                if (hasDefault)
                {
                    if (HasEnum(cluster, attribute.type) || HasBitmap(cluster, attribute.type))
                        writer.Write(" = " + attribute.type + "." + attribute.@default);
                    else if (attribute.type != "list")
                        writer.Write(" = " + SanitizeDefault(attribute.@default!, attribute.type, attribute.entry?.type));
                }
                writer.WriteLine(") {");
                writer.Write("            await SetAttribute(session, " + Convert.ToUInt16(attribute.id, 16) + ", value");
                if (hasDefault && attribute.type == "list")
                    writer.Write(" ?? " + SanitizeDefault(attribute.@default!, attribute.type, attribute.entry?.type));
                if (attribute.quality?.nullable == true)
                    writer.Write(", true");
                writer.WriteLine(");");
                writer.WriteLine("        }");
            }
        }

        private static void WriteType(string type, string? entryType, TextWriter writer)
        {
            switch (type)
            {
                case "uint8":
                case "enum8":
                case "tag":
                case "namespace":
                case "fabric-idx":
                case "action-id":
                    writer.Write("byte");
                    break;
                case "uint16":
                case "enum16":
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
                case "ref_DataTypePosixMs":
                case "systime-us":
                case "ref_DataTypeSystemTimeMs":
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
                case "ref_IpAdr":
                case "ref_Ipv4Adr":
                case "ref_Ipv6Adr":
                    includes.Add("System.Net");
                    writer.Write("IPAddress");
                    break;
                case "Hardware Address":
                    includes.Add("System.Net.NetworkInformation");
                    writer.Write("PhysicalAddress");
                    break;
                case "octstr":
                case "ipv6pre":
                    writer.Write("byte[]");
                    break;
                case "bool":
                case "Bool":
                case "string":
                case "String":
                case "double":
                case "Double":
                    writer.Write($"{type.ToLower()}");
                    break;
                case "ref_SemTag":
                    includes.Add("MatterDotNet.Messages");
                    writer.Write("SemanticTag");
                    break;
                default:
                    writer.Write(GeneratorUtil.SanitizeName(type));
                    break;
            }
        }

        private static string SanitizeDefault(string value, string? type = null, string? listType = null)
        {
            if (value == "\"")
                return "\"\"";
            if (value == "empty")
            {
                if (type == "list")
                {
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    sw.Write("new ");
                    WriteType(type, listType, sw);
                    sw.Write("()");
                    return sb.ToString();
                }
                return "[]";
            }
            if (value.StartsWith('"'))
                return value;
            else
                return value.ToLowerInvariant();
        }

        private static bool DefaultValid(string value)
        {
            if (value.Equals("MS", StringComparison.InvariantCultureIgnoreCase))
                return false;
            if (value.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                return false;
            return true;
        }
    }
}
