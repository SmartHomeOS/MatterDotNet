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

using Generator.Schema;
using MatterDotNet;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Generator
{
    public class ClusterGenerator
    {
        private static HashSet<string> includes = new HashSet<string>();
        private const string CLUSTER_HEADER = "// MatterDotNet Copyright (C) 2025 \n//\n// This program is free software: you can redistribute it and/or modify\n// it under the terms of the GNU Affero General Public License as published by\n// the Free Software Foundation, either version 3 of the License, or any later version.\n// This program is distributed in the hope that it will be useful,\n// but WITHOUT ANY WARRANTY, without even the implied warranty of\n// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.\n// See the GNU Affero General Public License for more details.\n// You should have received a copy of the GNU Affero General Public License\n// along with this program.  If not, see <http://www.gnu.org/licenses/>.\n//\n// WARNING: This file was auto-generated. Do not edit.\n";
        private static Dictionary<string, Dictionary<string, string>> bitmapComments = new Dictionary<string, Dictionary<string, string>>();
        private static Dictionary<string, Dictionary<string, string>> enumComments = new Dictionary<string, Dictionary<string, string>>();
        public static void Generate()
        {
            if (!Directory.Exists($"outputs\\Clusters\\"))
                Directory.CreateDirectory($"outputs\\Clusters\\");
            XmlSerializer commentDeserializer = new XmlSerializer(typeof(Comments));
            Comments? comments = commentDeserializer.Deserialize(File.OpenRead("..\\..\\..\\Comments\\comments.xml")) as Comments;
            foreach (var bitmapComment in comments.bitmap)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                foreach (var comment in bitmapComment.item)
                    item.Add(GeneratorUtil.EnsureHex(comment.value, 4), comment.comment);
                bitmapComments.Add(bitmapComment.name, item);
            }
            foreach (var enumComment in comments.@enum)
            {
                if (enumComment.item == null)
                    continue;
                Dictionary<string, string> item = new Dictionary<string, string>();
                foreach (var comment in enumComment.item)
                    item.Add(GeneratorUtil.EnsureHex(comment.value, 4), comment.comment);
                enumComments.Add(enumComment.name, item);
            }

            XmlSerializer deserializer = new XmlSerializer(typeof(configurator));
            IEnumerable<string> clusterxmls = Directory.EnumerateFiles("..\\..\\..\\Clusters");
            StreamWriter clusterBase = new StreamWriter("outputs\\ClusterBase.cs");
            foreach (string clusterxml in clusterxmls)
            {
                includes.Clear();
                if (clusterxml.EndsWith(".xml"))
                {
                    Console.WriteLine("Generating " + clusterxml + "...");
                    configurator? config = deserializer.Deserialize(File.OpenRead(clusterxml)) as configurator;
                    if (config == null)
                        throw new IOException("Failed to parse cluster " + clusterxml);
                    if (config.cluster != null && config.cluster.Length > 0)
                    {
                        foreach (rootConfiguratorCluster cluster in config.cluster)
                        {
                            if (cluster.code != null)
                                clusterBase.WriteLine($"                case {GeneratorUtil.SanitizeClassName(cluster.name)}.CLUSTER_ID:\n                    return new {GeneratorUtil.SanitizeClassName(config.cluster[0].name)}(endPoint);");
                            WriteClass(config, cluster);
                        }
                    }
                    else
                    {
                        if (config.@enum != null || config.@struct != null || config.bitmap != null)
                        {
                            // Global
                            using (FileStream outstream = File.OpenWrite("outputs\\" + GeneratorUtil.SanitizeName(Path.GetFileNameWithoutExtension(clusterxml)) + ".cs"))
                            {
                                using (StreamWriter writer = new StreamWriter(outstream, Encoding.UTF8))
                                {
                                    writer.NewLine = "\n";
                                    writer.WriteLine(CLUSTER_HEADER);
                                    if (config.@struct != null)
                                    {
                                        writer.WriteLine("using MatterDotNet.Protocol.Parsers;\nusing MatterDotNet.Protocol.Payloads;\nusing System.Diagnostics.CodeAnalysis;");
                                        writer.WriteLine();
                                    }
                                    writer.WriteLine($"namespace MatterDotNet.Clusters\n{{");
                                    bool firstEnum = true;

                                    // Enums
                                    if (config.@enum != null)
                                    {
                                        foreach (rootConfiguratorEnum enumType in config.@enum)
                                        {
                                            if (enumType.item == null)
                                                continue;
                                            if (enumType.name.StartsWith("TestGlobal"))
                                                continue;
                                            if (firstEnum)
                                                firstEnum = false;
                                            else
                                                writer.WriteLine();
                                            WriteEnum(enumType, writer, null);
                                        }
                                    }

                                    // Bitmaps
                                    if (config.bitmap != null)
                                    {
                                        foreach (rootConfiguratorBitmap bitmapType in config.bitmap)
                                        {
                                            if (bitmapType.type == null || bitmapType.field == null)
                                                continue;
                                            if (bitmapType.name.StartsWith("TestGlobal"))
                                                continue;
                                            if (firstEnum)
                                                firstEnum = false;
                                            else
                                                writer.WriteLine();
                                            WriteBitfield(bitmapType, writer, null);
                                        }
                                    }

                                    //Structs
                                    if (config.@struct != null)
                                    {
                                        //includes.Add("System.Diagnostics.CodeAnalysis");
                                        //includes.Add("MatterDotNet.Protocol.Payloads");
                                        bool firstRecord = true;
                                        foreach (rootConfiguratorStruct structType in config.@struct)
                                        {
                                            if (structType.item == null)
                                                continue;
                                            if (structType.name.StartsWith("TestGlobal"))
                                                continue;
                                            if (firstRecord)
                                                firstRecord = false;
                                            else
                                                writer.WriteLine();
                                            WriteRecord(structType, config, writer);
                                        }
                                    }

                                    writer.Write("}");
                                    writer.Flush();
                                }
                            }
                        }
                    }
                }
            }
            clusterBase.Close();
        }

        private static void WriteClass(configurator clusterConfig, rootConfiguratorCluster cluster)
        {
            string folder = $"outputs\\Clusters\\" + GeneratorUtil.SanitizeClassName(string.IsNullOrEmpty(cluster.domain) ? "Misc" : cluster.domain) + "\\";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            string path = folder + cluster.name.Replace(" ", "").Replace('/', '-') + "Cluster.cs";
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
                    WriteClass(clusterConfig, cluster, sw);
                    writer.WriteLine(CLUSTER_HEADER);
                    foreach (string include in includes.ToImmutableSortedSet())
                        writer.WriteLine("using " + include + ";");
                    writer.Write(sb);
                }
            }
        }

        private static void WriteClass(configurator clusterConfig, rootConfiguratorCluster cluster, TextWriter writer)
        {
            includes.Add("MatterDotNet.Protocol.Parsers");
            includes.Add("MatterDotNet.Protocol.Sessions");
            writer.WriteLine();
            writer.WriteLine($"namespace MatterDotNet.Clusters.{GeneratorUtil.SanitizeClassName(string.IsNullOrEmpty(cluster.domain) ? "Misc" : cluster.domain)}\n{{");
            writer.WriteLine("    /// <summary>");
            writer.WriteLine($"    /// {GeneratorUtil.SanitizeComment(cluster.description) ?? GeneratorUtil.FieldNameToComment(cluster.name)}");
            writer.WriteLine("    /// </summary>");
            if (cluster.globalAttribute != null && cluster.globalAttribute.Any(a => a.code == "0xFFFD"))
                writer.WriteLine($"    [ClusterRevision(CLUSTER_ID, {cluster.globalAttribute.First(a => a.code == "0xFFFD").value})]");
            else
                writer.WriteLine($"    [ClusterRevision(CLUSTER_ID, 1)]");
            writer.WriteLine("    public class " + GeneratorUtil.SanitizeClassName(cluster.name) + " : ClusterBase");
            writer.WriteLine("    {");
            if (!string.IsNullOrEmpty(cluster.code))
                writer.WriteLine("        internal const uint CLUSTER_ID = " + cluster.code + ";");
            writer.WriteLine();
            writer.WriteLine("        /// <summary>");
            writer.WriteLine($"        /// {GeneratorUtil.SanitizeComment(cluster.description) ?? GeneratorUtil.FieldNameToComment(cluster.name)}");
            writer.WriteLine("        /// </summary>");
            writer.WriteLine("        public " + GeneratorUtil.SanitizeClassName(cluster.name) + "(ushort endPoint) : base(CLUSTER_ID, endPoint) { }");
            //if (string.IsNullOrEmpty(cluster.classification.baseCluster) && !string.IsNullOrEmpty(clusterConfig.cluster.code))
            writer.WriteLine($"        /// <inheritdoc />\n        protected {GeneratorUtil.SanitizeClassName(cluster.name)}(uint cluster, ushort endPoint) : base(cluster, endPoint) {{ }}");
            writer.WriteLine();
            if (clusterConfig.@enum != null || clusterConfig.bitmap != null || (cluster.features != null && cluster.features.feature.Length > 0))
            {
                writer.WriteLine("        #region Enums");
                bool firstEnum=true;
                if (cluster.features != null && cluster.features.feature.Length > 0)
                {
                    firstEnum = false;
                    WriteFeatures(cluster.features, writer);
                }
                if (clusterConfig.@enum != null)
                {
                    foreach (rootConfiguratorEnum enumType in clusterConfig.@enum)
                    {
                        if (enumType.item == null)
                            continue;
                        if (firstEnum)
                            firstEnum = false;
                        else
                            writer.WriteLine();
                        WriteEnum(enumType, writer, cluster.code);
                    }
                }
                if (clusterConfig.bitmap != null)
                {
                    foreach (rootConfiguratorBitmap bitmapType in clusterConfig.bitmap)
                    {
                        if (bitmapType.type == null || bitmapType.field == null)
                            continue;
                        if (firstEnum)
                            firstEnum = false;
                        else
                            writer.WriteLine();
                        WriteBitfield(bitmapType, writer, cluster.code);
                    }
                }
                writer.WriteLine("        #endregion Enums");
                writer.WriteLine();
            }
            if (clusterConfig.@struct != null)
            {
                includes.Add("System.Diagnostics.CodeAnalysis");
                includes.Add("MatterDotNet.Protocol.Payloads");
                writer.WriteLine("        #region Records");
                bool firstRecord = true;
                foreach (rootConfiguratorStruct structType in clusterConfig.@struct)
                {
                    if (structType.item == null)
                        continue;
                    if (firstRecord)
                        firstRecord = false;
                    else
                        writer.WriteLine();
                    WriteRecord(structType, clusterConfig, writer);
                }
                writer.WriteLine("        #endregion Records");
                writer.WriteLine();
            }
            if (cluster.command != null && cluster.command.Length > 0)
            {
                includes.Add("MatterDotNet.Messages.InteractionModel");
                includes.Add("MatterDotNet.Protocol.Subprotocols");
                writer.WriteLine("        #region Payloads");
                bool firstPayload = true;
                foreach (var command in cluster.command)
                {
                    if (command.arg == null)
                        continue;
                    includes.Add("MatterDotNet.Protocol.Payloads");
                    if (firstPayload)
                        firstPayload = false;
                    else
                        writer.WriteLine();
                    WriteStruct(command, command.source == "client", writer, clusterConfig);
                }
                writer.WriteLine("        #endregion Payloads");
                writer.WriteLine();
                WriteCommands(clusterConfig, cluster, writer);
            }

            if ((cluster.attribute != null && cluster.attribute.Length > 0) || (cluster.features != null && cluster.features.feature.Length > 0))
            {
                writer.WriteLine("        #region Attributes");
                bool firstAttribute = true;
                if (cluster.features != null && cluster.features.feature.Length > 0)
                {
                    firstAttribute = false;
                    WriteFeatureFunctions(writer);
                }
                if (cluster.attribute != null && cluster.attribute.Length > 0)
                {
                    foreach (var attribute in cluster.attribute)
                    {
                        if (attribute.type != null && attribute?.mandatoryConform?.condition?.name != "Zigbee" && attribute?.side == "server")
                        {
                            if (firstAttribute)
                                firstAttribute = false;
                            else
                                writer.WriteLine();
                            WriteAttribute(clusterConfig, attribute, writer);
                        }
                    }
                }
                writer.WriteLine("        #endregion Attributes");
            }
            writer.WriteLine($"\n        /// <inheritdoc />\n        public override string ToString() {{\n            return \"{cluster.name}\";\n        }}");
            writer.WriteLine("    }");
            writer.Write("}");
            writer.Flush();
        }

        private static void WriteFeatureFunctions(TextWriter writer)
        {
            writer.WriteLine("        /// <summary>\n        /// Features supported by this cluster\n        /// </summary>\n        /// <param name=\"session\"></param>\n        /// <returns></returns>\n        public async Task<Feature> GetSupportedFeatures(SecureSession session)\n        {\n            return (Feature)(byte)(await GetAttribute(session, 0xFFFC))!;\n        }\n\n        /// <summary>\n        /// Returns true when the feature is supported by the cluster\n        /// </summary>\n        /// <param name=\"session\"></param>\n        /// <param name=\"feature\"></param>\n        /// <returns></returns>\n        public async Task<bool> Supports(SecureSession session, Feature feature)\n        {\n            return ((feature & await GetSupportedFeatures(session)) != 0);\n        }");
        }

        private static void WriteStruct(rootConfiguratorClusterCommand command, bool toServer, TextWriter writer, configurator clusterConfig)
        {
            if (!toServer)
                writer.WriteLine($"        /// <summary>\n        /// {GeneratorUtil.FieldNameToComment(command.name)} - Reply from server\n        /// </summary>");
            writer.WriteLine($"        {(toServer ? "private record" : "public struct")} " + GeneratorUtil.SanitizeName(command.name) + (toServer ? "Payload : TLVPayload {" : "() {"));
            if (command.arg != null)
            {
                foreach (rootConfiguratorClusterCommandArg field in command.arg)
                {
                    if (field.type == null) //Reserved/removed fields
                        continue;
                    field.type = CorrectFieldType(field.name, field.type);
                    writer.Write("            public ");
                    bool optional = field.optional;
                    if (!optional)
                        writer.Write("required ");
                    WriteType(field.array, field.type, writer, field.name);
                    if (optional || field.isNullable == true)
                        writer.Write("?");
                    if (field.name == GeneratorUtil.SanitizeName(command.name))
                        writer.Write(" " + GeneratorUtil.SanitizeName(field.name) + "Field { get; set; }");
                    else
                        writer.Write(" " + GeneratorUtil.SanitizeName(field.name) + " { get; set; }");
                    //TODO - Enable if commands have defaults
                    //if (field.@default != null && DefaultValid(field.@default))
                    //{
                    //    if (field.type.EndsWith("Enum") && field.@default != "0")
                    //        writer.WriteLine(" = " + field.type + "." + field.@default + ";");
                    //    else
                    //        writer.WriteLine(" = " + SanitizeDefault(field.@default, field.type, field.entry?.type) + ";");
                    //}
                    //else
                    writer.WriteLine();
                }
            }
            if (toServer)
            {
                writer.WriteLine("            internal override void Serialize(TLVWriter writer, long structNumber = -1) {");
                writer.WriteLine("                writer.StartStructure(structNumber);");
                if (command.arg != null)
                {
                    int fieldIdx = 0;
                    foreach (rootConfiguratorClusterCommandArg field in command.arg)
                    {
                        if (field.type == null) //Reserved/removed fields
                            continue;
                        long? from = null;
                        long? to = null;
                        if (field.min != null && long.TryParse(field.min, out long fromVal))
                            from = fromVal;
                        if (field.max != null && long.TryParse(field.max, out long toVal))
                            to = toVal;
                        if (field.lengthSpecified)
                            to = field.length;

                        WriteStructType(field.optional, field.isNullable, field.array ? "array" : field.type, field.array ? field.type : null, fieldIdx, from, to, (GeneratorUtil.SanitizeName(field.name) == GeneratorUtil.SanitizeName(command.name) ? GeneratorUtil.SanitizeName(field.name + "Field") : GeneratorUtil.SanitizeName(field.name)), clusterConfig, writer);
                        fieldIdx++;
                    }
                }
                writer.WriteLine("                writer.EndContainer();");
                writer.WriteLine("            }");
            }
            writer.WriteLine("        }");
        }

        private static string CorrectFieldType(string name, string type)
        {
            if (type == "enum8" && name == "Status")
                return "status";
            if (type == "octet_string" && (name == "ExtendedAddress" || name == "HardwareAddress"))
                return "hwadr";
            if (type == "octet_string" && name == "IPv4Addresses")
                return "ipv4adr";
            if (type == "octet_string" && name == "IPv6Addresses")
                return "ipv6adr";

            return type;
        }

        private static void WriteStructType(bool optional, bool nullable, string type, string? entryType, int id, long? from, long? to, string name, configurator clusterConfig, TextWriter writer)
        {
            bool unsigned = false;
            string totalIndent = "                ";
            if (optional)
            {
                writer.WriteLine($"{totalIndent}if ({name} != null)");
                if (type != "array")
                    totalIndent += "    ";
            }
            if (name == "MessageID" && type == "octet_string")
                type = name;
            switch (type)
            {
                case "array":
                case "ARRAY":
                    if (nullable && !optional)
                        writer.WriteLine($"{totalIndent}if ({name} != null)");
                    writer.WriteLine($"{totalIndent}{{");
                    if (from != null || to != null)
                        writer.WriteLine($"{totalIndent}    Constrain({name}, {from ?? 0}{(to == null ? "" : $", {to}")});");
                    writer.WriteLine($"{totalIndent}    writer.StartArray({id});");
                    writer.WriteLine($"{totalIndent}    foreach (var item in {name}) {{");
                    writer.Write("        ");
                    WriteStructType(false, false, entryType!, null, -1, null, null, "item", clusterConfig, writer);
                    writer.WriteLine($"{totalIndent}    }}");
                    writer.WriteLine($"{totalIndent}    writer.EndContainer();");
                    writer.WriteLine($"{totalIndent}}}");
                    if (nullable)
                        writer.WriteLine($"{totalIndent}else\n{totalIndent}    writer.WriteNull({id});");
                    return;
                case "boolean":
                    writer.WriteLine($"{totalIndent}writer.WriteBool({id}, {name});");
                    return;
                case "int8s":
                case "SignedTemperature":
                    writer.Write($"{totalIndent}writer.WriteSByte({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", sbyte.MaxValue");
                    break;
                case "int16s":
                case "temperature":
                case "TemperatureDifference":
                    writer.Write($"{totalIndent}writer.WriteShort({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", short.MaxValue");
                    break;
                case "int24s":
                case "int32s":
                    writer.Write($"{totalIndent}writer.WriteInt({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", int.MaxValue");
                    break;
                case "int40s":
                case "int48s":
                case "int56s":
                case "int64s":
                case "power_mw":
                case "amperage_ma":
                case "voltage_mv":
                case "energy_mwh":
                    writer.Write($"{totalIndent}writer.WriteLong({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", long.MaxValue");
                    break;
                case "int8u":
                case "INT8U":
                case "enum8":
                case "bitmap8":
                case "tag":
                case "namespace":
                case "fabric_idx":
                case "action_id":
                case "percent":
                case "UnsignedTemperature":
                    writer.Write($"{totalIndent}writer.WriteByte({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", byte.MaxValue");
                    unsigned = true;
                    break;
                case "int16u":
                case "bitmap16":
                case "enum16":
                case "group_id":
                case "endpoint_no":
                case "vendor_id":
                case "entry_idx":
                    writer.Write($"{totalIndent}writer.WriteUShort({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", ushort.MaxValue");
                    unsigned = true;
                    break;
                case "percent100ths":
                    writer.Write($"{totalIndent}writer.WriteDecimal({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", ushort.MaxValue"); //Intentionally ushort
                    unsigned = true;
                    break;
                case "int24u":
                case "int32u":
                case "bitmap32":
                case "cluster_id":
                case "attrib_id":
                case "field_id":
                case "event_id":
                case "command_id":
                case "trans_id":
                case "data_ver":
                    writer.Write($"{totalIndent}writer.WriteUInt({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", uint.MaxValue");
                    unsigned = true;
                    break;
                case "epoch_s":
                case "utc":
                    includes.Add("MatterDotNet.Util");
                    if (nullable && !optional)
                        writer.Write($"{totalIndent}if (!{name}.HasValue)\n{totalIndent}    writer.WriteNull({id});\n{totalIndent}else\n    ");
                    writer.Write($"{totalIndent}writer.WriteUInt({id}, TimeUtil.ToEpochSeconds({name}");
                    if (nullable || optional)
                        writer.Write("!.Value)");
                    else
                        writer.Write(")");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", uint.MaxValue");
                    unsigned = true;
                    break;
                case "elapsed_s":
                    if (nullable && !optional)
                        writer.Write($"{totalIndent}if (!{name}.HasValue)\n{totalIndent}    writer.WriteNull({id});\n{totalIndent}else\n    ");
                    writer.Write($"{totalIndent}writer.WriteUInt({id}, (uint){name}");
                    if (nullable || optional)
                        writer.Write("!.Value.TotalSeconds");
                    else
                        writer.Write(".TotalSeconds");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", uint.MaxValue");
                    unsigned = true;
                    break;
                case "epoch_us":
                    includes.Add("MatterDotNet.Util");
                    if (nullable && !optional)
                        writer.Write($"{totalIndent}if (!{name}.HasValue)\n{totalIndent}    writer.WriteNull({id});\n{totalIndent}else\n    ");
                    writer.Write($"{totalIndent}writer.WriteULong({id}, TimeUtil.ToEpochUS({name}");
                    if (nullable || optional)
                        writer.Write("!.Value)");
                    else
                        writer.Write(")");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", ulong.MaxValue");
                    unsigned = true;
                    break;
                case "systime_ms":
                case "systime_us":
                    if (nullable && !optional)
                        writer.Write($"{totalIndent}if (!{name}.HasValue)\n{totalIndent}    writer.WriteNull({id});\n{totalIndent}else\n    ");
                    writer.Write($"{totalIndent}writer.WriteULong({id}, (ulong){name}");
                    if (nullable || optional)
                        writer.Write("!.Value");
                    if (type == "systime_ms")
                        writer.Write(".TotalMilliseconds");
                    else
                        writer.Write(".TotalMicroseconds");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", ulong.MaxValue");
                    unsigned = true;
                    break;
                case "posix_ms":
                    if (nullable && !optional)
                        writer.Write($"{totalIndent}if (!{name}.HasValue)\n{totalIndent}    writer.WriteNull({id});\n{totalIndent}else\n    ");
                    writer.Write($"{totalIndent}writer.WriteULong({id}, {name}");
                    if (nullable || optional)
                        writer.Write("!.Value.ToUnixTimeMilliseconds()");
                    else
                        writer.Write(".ToUnixTimeMilliseconds()");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", ulong.MaxValue");
                    break;
                case "int40u":
                case "int48u":
                case "int56u":
                case "int64u":
                case "bitmap64":
                case "fabric_id":
                case "node_id":
                case "EUI64":
                case "event_no":
                case "subject_id":
                    writer.Write($"{totalIndent}writer.WriteULong({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", ulong.MaxValue");
                    break;
                case "single":
                    writer.Write($"{totalIndent}writer.WriteFloat({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", float.MaxValue");
                    break;
                case "double":
                    writer.Write($"{totalIndent}writer.WriteDouble({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", double.MaxValue");
                    break;
                case "ipadr":
                case "ipv4adr":
                case "ipv6adr":
                    if (nullable && !optional)
                        writer.Write($"{totalIndent}if ({name} == null)\n{totalIndent}    writer.WriteNull({id});\n{totalIndent}else\n    ");
                    writer.WriteLine($"{totalIndent}writer.WriteBytes({id}, {name}.GetAddressBytes());");
                    return;
                case "hwadr":
                    if (nullable && !optional)
                        writer.Write($"{totalIndent}if ({name} == null)\n{totalIndent}    writer.WriteNull({id});\n{totalIndent}else\n    ");
                    writer.WriteLine($"{totalIndent}writer.WriteBytes({id}, {name}.GetAddressBytes());");
                    return;
                case "devtype_id":
                    writer.WriteLine($"{totalIndent}writer.WriteUInt({id}, {(nullable ? "(uint?)" : "(uint)")}{name});");
                    return;
                case "MessageID":
                    if (nullable && !optional)
                        writer.Write($"{totalIndent}if ({name} == null)\n{totalIndent}    writer.WriteNull({id});\n{totalIndent}else\n    ");
                    writer.WriteLine($"{totalIndent}writer.WriteBytes({id}, {name}.ToByteArray());");
                    return;
                case "octet_string":
                case "long_octet_string":
                case "ipv6pre":
                    writer.Write($"{totalIndent}writer.WriteBytes({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", int.MaxValue");
                    break;
                case "char_string":
                case "CHAR_STRING":
                case "long_char_string":
                case "LONG_CHAR_STRING":
                    writer.Write($"{totalIndent}writer.WriteString({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", int.MaxValue");
                    break;
                default:
                    if (type == "status")
                        writer.WriteLine($"{totalIndent}writer.WriteByte({id}, {(nullable ? "(byte?)" : "(byte)")}{name});");
                    else if (HasEnum(clusterConfig, type))
                        writer.WriteLine($"{totalIndent}writer.WriteUShort({id}, {(nullable ? "(ushort?)" : "(ushort)")}{name});");
                    else if (HasBitmap(clusterConfig, type))
                        writer.WriteLine($"{totalIndent}writer.WriteUInt({id}, {(nullable ? "(uint?)" : "(uint)")}{name});");
                    else
                    {
                        if (nullable && !optional)
                            writer.Write($"{totalIndent}if ({name} == null)\n{totalIndent}    writer.WriteNull({id});\n{totalIndent}else\n    ");
                        writer.WriteLine($"{totalIndent}{name}.Serialize(writer, {id});");
                    }
                    return;
            }
            if (from != null && (!unsigned || from != 0))
                writer.WriteLine($", {from.Value});");
            else
                writer.WriteLine(");");
        }

        private static void WriteFieldReader(bool optional, bool nullable, bool array, string type, string id, long? from, long? to, string name, string structName, configurator clusterConfig, TextWriter writer)
        {
            string totalIndent = "                ";
            
            if (array)
            {
                writer.WriteLine($"{totalIndent}{{");
                writer.Write($"{totalIndent}    {name} = new ");
                WriteType(false, type, writer, name);
                writer.WriteLine($"[reader.GetStruct({id})!.Length];");
                writer.WriteLine($"{totalIndent}    for (int n = 0; n < {name}.Length; n++) {{");
                writer.Write($"{totalIndent}        {name}[n] = ");
                WriteFieldReader(false, false, false, type!, "n", null, null, $"fields[{id}]", structName, clusterConfig, writer);
                writer.WriteLine($"{totalIndent}    }}");
                writer.WriteLine($"{totalIndent}}}");
                return;
            }
            else if (id != "-1" && id != "i" && id != "n")
            {
                if (name == GeneratorUtil.SanitizeName(structName))
                    writer.Write($"{totalIndent}{name}Field = ");
                else
                    writer.Write($"{totalIndent}{name} = ");
            }
            if (name == "MessageID" && type == "octet_string")
                type = name;

            bool extraClose = false;
            switch (type)
            {
                case "boolean":
                    writer.Write($"reader.GetBool({id}");
                    break;
                case "int8s":
                case "SignedTemperature":
                    writer.Write($"reader.GetSByte({id}");
                    break;
                case "int16s":
                case "temperature":
                case "TemperatureDifference":
                    writer.Write($"reader.GetShort({id}");
                    break;
                case "int24s":
                case "int32s":
                    writer.Write($"reader.GetInt({id}");
                    break;
                case "int40s":
                case "int48s":
                case "int56s":
                case "int64s":
                case "power_mw":
                case "amperage_ma":
                case "voltage_mv":
                case "energy_mwh":
                    writer.Write($"reader.GetLong({id}");
                    break;
                case "int8u":
                case "INT8U":
                case "enum8":
                case "bitmap8":
                case "tag":
                case "namespace":
                case "fabric_idx":
                case "action_id":
                case "percent":
                case "UnsignedTemperature":
                    writer.Write($"reader.GetByte({id}");
                    break;
                case "int16u":
                case "enum16":
                case "bitmap16":
                case "group_id":
                case "endpoint_no":
                case "vendor_id":
                case "entry_idx":
                    writer.Write($"reader.GetUShort({id}");
                    break;
                case "percent100ths":
                    writer.Write($"reader.GetDecimal({id}");
                    break;
                case "int24u":
                case "int32u":
                case "bitmap32":
                case "cluster_id":
                case "attrib_id":
                case "field_id":
                case "event_id":
                case "command_id":
                case "trans_id":
                case "data_ver":
                    writer.Write($"reader.GetUInt({id}");
                    break;
                case "int40u":
                case "int48u":
                case "int56u":
                case "int64u":
                case "bitmap64":
                case "fabric_id":
                case "node_id":
                case "EUI64":
                case "event_no":
                case "subject_id":
                    writer.Write($"reader.GetULong({id}");
                    break;
                case "single":
                    writer.Write($"reader.GetFloat({id}");
                    break;
                case "double":
                    writer.Write($"reader.GetDouble({id}");
                    break;
                case "ipadr":
                    writer.WriteLine($"new IPAddress(reader.GetBytes({id}, {(optional ? "true" : "false")}, 16, 4)!);;");
                    return;
                case "ipv4adr":
                    writer.WriteLine($"new IPAddress(reader.GetBytes({id}, {(optional ? "true" : "false")}, 4, 4)!);;");
                    return;
                case "ipv6adr":
                    writer.WriteLine($"new IPAddress(reader.GetBytes({id}, {(optional ? "true" : "false")}, 16, 16)!);;");
                    return;
                case "hwadr":
                    writer.WriteLine($"new PhysicalAddress(reader.GetBytes({id}, {(optional ? "true" : "false")}, 8, 6)!);");
                    return;
                case "MessageID":
                    writer.WriteLine($"new Guid(reader.GetBytes({id}, {(optional ? "true" : "false")}, 16, 16)!);");
                    return;
                case "epoch_s":
                case "utc":
                    includes.Add("MatterDotNet.Util");
                    writer.Write($"TimeUtil.FromEpochSeconds(reader.GetUInt({id}");
                    extraClose = true;
                    break;
                case "elapsed_s":
                    includes.Add("MatterDotNet.Util");
                    writer.Write($"TimeUtil.FromSeconds(reader.GetUInt({id}");
                    extraClose = true;
                    break;
                case "epoch_us":
                    includes.Add("MatterDotNet.Util");
                    writer.Write($"TimeUtil.FromEpochUS(reader.GetULong({id}");
                    extraClose = true;
                    break;
                case "systime_us":
                    writer.Write($"TimeUtil.FromMicros(reader.GetULong({id}");
                    extraClose = true;
                    break;
                case "systime_ms":
                    writer.Write($"TimeUtil.FromMillis(reader.GetULong({id}");
                    extraClose = true;
                    break;
                case "posix_ms":
                    writer.Write($"DateTimeOffset.FromUnixTimeMilliseconds(reader.GetULong({id}");
                    extraClose = true;
                    break;
                case "devtype_id":
                    writer.Write($"(DeviceTypeEnum)reader.GetUInt({id}");
                    if (optional)
                        writer.Write(", true");
                    writer.WriteLine(")!.Value;");
                    return;
                case "octet_string":
                case "long_octet_string":
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
                    writer.WriteLine(';');
                    return;
                case "char_string":
                case "CHAR_STRING":
                case "long_char_string":
                case "LONG_CHAR_STRING":
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
                    writer.WriteLine(';');
                    return;
                default:
                    if (HasEnum(clusterConfig, type) || HasBitmap(clusterConfig, type) || type == "status")
                    {
                        if (type == "status")
                            writer.Write($"(IMStatusCode)reader.GetByte({id}");
                        else
                            writer.Write($"({GeneratorUtil.SanitizeName(type)}");
                        if (optional)
                            writer.Write('?');
                        if (type != "status")
                        {
                            if (HasEnum(clusterConfig, type))
                                writer.Write($")reader.GetUShort({id}");
                            else
                                writer.Write($")reader.GetUInt({id}");
                        }
                        if (optional)
                            writer.WriteLine(", true);");
                        else
                            writer.WriteLine(")!.Value;");
                    }
                    else {
                        writer.Write($"new ");
                        WriteType(false, type, writer, name);
                        if (id == "i")
                            writer.WriteLine("(reader.GetStruct(i)!);");
                        else if (id == "n")
                            writer.WriteLine($"((object[])((object[]){name})[n]);");
                        else
                            writer.WriteLine($"((object[])fields[{id}]);");
                    }
                    return;
            }

            if (extraClose && !(optional || nullable))
                writer.Write(")");
            if (optional || nullable)
                writer.Write(", true)");
            else
                writer.Write(")!.Value");
            if (extraClose && (optional || nullable))
                writer.Write(")");
            writer.WriteLine(';');
        }

        private static bool HasEnum(configurator config, string name)
        {
            if (name == "AreaTypeTag" || name == "AtomicRequestTypeEnum" || name == "MeasurementType" || name == "MeasurementTypeEnum")
                return true;
            if (name == "LandmarkTag" || name == "RelativePositionTag")
                return true;
            if (config.@enum == null)
                return false;
            foreach (rootConfiguratorEnum enumType in config.@enum)
            {
                if (enumType.name == name)
                    return true;
            }
            return false;
        }

        private static bool HasEnumValue(configurator config, string name, string value)
        {
            if (config.@enum == null)
                return false;
            foreach (rootConfiguratorEnum enumType in config.@enum)
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

        private static bool HasBitmap(configurator config, string name)
        {
            if (config.bitmap == null)
                return false;
            foreach (rootConfiguratorBitmap bitfieldType in config.bitmap)
            {
                if (bitfieldType.name == name)
                    return true;
            }
            return false;
        }

        private static bool HasBitmapValue(configurator config, string name, string value)
        {
            if (config.bitmap == null)
                return false;
            foreach (rootConfiguratorBitmap bitfieldType in config.bitmap)
            {
                if (bitfieldType.name == name)
                {
                    foreach (var bitfield in bitfieldType.field)
                    {
                        if (bitfield.name == value)
                            return true;
                    }
                }
            }
            return false;
        }

        private static bool HasStruct(configurator config, string name)
        {
            if (config.@struct == null)
                return false;
            foreach (rootConfiguratorStruct structType in config.@struct)
            {
                if (structType.name == name)
                    return true;
            }
            return false;
        }

        private static void WriteCommands(configurator clusterConfig, rootConfiguratorCluster cluster, TextWriter writer)
        {
            writer.WriteLine("        #region Commands");
            bool first = true;
            foreach (rootConfiguratorClusterCommand cmd in cluster.command)
            {
                if (cmd.source == "client")
                {
                    if (first)
                        first = false;
                    else
                        writer.WriteLine();
                    if (cmd.name == cluster.name)
                        cmd.name += "Command";
                    writer.WriteLine("        /// <summary>");
                    writer.WriteLine("        /// " + GeneratorUtil.FieldNameToComment(cmd.name));
                    writer.WriteLine("        /// </summary>");
                    writer.Write("        public async Task");
                    rootConfiguratorClusterCommand? response = GetResponseType(cluster.command, cmd.response, writer);
                    if (cmd.response == "N")
                        writer.Write(" ");
                    else if (response == null)
                        writer.Write("<bool> ");
                    else
                        writer.Write("<" + GeneratorUtil.SanitizeName(response.name) + "?> ");
                    writer.Write(GeneratorUtil.SanitizeName(cmd.name) + "(SecureSession session");
                    if (cmd.mustUseTimedInvoke)
                        writer.Write(", ushort commandTimeoutMS");
                    if (cmd.arg != null)
                    {
                        foreach (var field in cmd.arg)
                        {
                            if (field.type == null)
                                continue;
                            field.type = CorrectFieldType(field.name, field.type);
                            writer.Write(", ");
                            WriteType(field.array, field.type, writer, field.name);
                            if (field.optional || field.isNullable)
                                writer.Write('?');
                            writer.Write(" " + GeneratorUtil.SanitizeName(field.name, true));
                        }
                    }
                    writer.WriteLine(") {");
                    if (cmd.arg != null)
                    {
                        writer.WriteLine("            " + GeneratorUtil.SanitizeName(cmd.name) + "Payload requestFields = new " + GeneratorUtil.SanitizeName(cmd.name) + "Payload() {");
                        foreach (var field in cmd.arg)
                        {
                            if (field.type == null)
                                continue;
                            writer.WriteLine($"                {GeneratorUtil.SanitizeName(field.name)} = {GeneratorUtil.SanitizeName(field.name, true)},");
                        }
                        writer.WriteLine("            };");
                        if (cmd.mustUseTimedInvoke)
                            writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, " + cmd.code + ", commandTimeoutMS, requestFields);");
                        else
                            writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, " + cmd.code + ", requestFields);");
                    }
                    else
                    {
                        if (cmd.response == "N")
                        {
                            if (cmd.mustUseTimedInvoke)
                                writer.WriteLine("            await InteractionManager.SendTimedCommand(session, endPoint, cluster, " + cmd.code + ", commandTimeoutMS);");
                            else
                                writer.WriteLine("            await InteractionManager.SendCommand(session, endPoint, cluster, " + cmd.code + ");");
                        }
                        else
                        {
                            if (cmd.mustUseTimedInvoke)
                                writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, " + cmd.code + ", commandTimeoutMS);");
                            else
                                writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, " + cmd.code + ");");
                        }
                    }
                    if (response == null || response.arg == null)
                    {
                        if (cmd.response != "N")
                            writer.WriteLine("            return ValidateResponse(resp);");
                    }
                    else
                    {
                        writer.WriteLine("            if (!ValidateResponse(resp))");
                        writer.WriteLine("                return null;");
                        writer.WriteLine("            return new " + GeneratorUtil.SanitizeName(response.name) + "() {");
                        int fieldIdx = 0;
                        foreach (var field in response.arg)
                        {
                            if (field.array && field.type != null && HasStruct(clusterConfig, field.type))
                            {
                                writer.Write("                " + GeneratorUtil.SanitizeName(field.name) + " = ");
                                if (field.optional || field.isNullable)
                                    writer.Write("GetOptionalArrayField<");
                                else
                                    writer.Write("GetArrayField<");
                                WriteType(false, field.type, writer, field.name);
                                writer.WriteLine($">(resp, {fieldIdx}),");
                            }
                            else
                            {
                                writer.Write("                " + GeneratorUtil.SanitizeName(field.name) + " = ");
                                if (field.type == "posix_ms" && field.isNullable == true)
                                    writer.Write($"GetField(resp, {fieldIdx}) != null ? ");
                                else
                                    writer.Write('(');
                                WriteType(field.array, field.type!, writer, field.name);
                                if (field.optional)
                                {
                                    if (HasEnum(clusterConfig, field.type!) || HasBitmap(clusterConfig, field.type!))
                                        writer.Write("?)(byte");
                                    writer.WriteLine($"?)GetOptionalField(resp, {fieldIdx}),");
                                }
                                else
                                {
                                    if (field.type == "elapsed-s")
                                        writer.WriteLine($".FromSeconds((uint)GetField(resp, {fieldIdx}))),");
                                    else if (field.type == "systime_us")
                                        writer.WriteLine($".FromMicroseconds((ulong)GetField(resp, {fieldIdx}))),");
                                    else if (field.type == "systime_ms")
                                        writer.WriteLine($".FromMilliseconds((ulong)GetField(resp, {fieldIdx}))),");
                                    else if (field.type == "posix_ms")
                                    {
                                        writer.Write($".FromUnixTimeMilliseconds((long)(ulong)GetField(resp, {fieldIdx}))");
                                        if (field.isNullable == true)
                                            writer.WriteLine(" : null,");
                                        else
                                            writer.WriteLine("),");
                                    }
                                    else
                                    {
                                        if (field.isNullable == true)
                                            writer.Write('?');
                                        if (HasEnum(clusterConfig, field.type!) || HasBitmap(clusterConfig, field.type!) || field.type == "status")
                                            writer.Write(")(byte");
                                        writer.WriteLine($")GetField(resp, {fieldIdx}),");
                                    }
                                }
                            }
                            fieldIdx++;
                        }
                        writer.WriteLine("            };");
                    }
                    writer.WriteLine("        }");
                }
            }
            writer.WriteLine("        #endregion Commands");
            writer.WriteLine();
        }

        private static rootConfiguratorClusterCommand? GetResponseType(rootConfiguratorClusterCommand[] cmds, string response, TextWriter writer)
        {
            foreach (rootConfiguratorClusterCommand cmd in cmds)
            {
                if (cmd.name == response)
                    return cmd;
            }
            return null;
        }

        private static void WriteRecord(rootConfiguratorStruct structType, configurator clusterConfig, TextWriter writer)
        {
            writer.WriteLine($"        /// <summary>\n        /// {GeneratorUtil.FieldNameToComment(structType.name)}\n        /// </summary>");
            writer.WriteLine("        public record " + GeneratorUtil.SanitizeName(structType.name) + " : TLVPayload {");
            writer.WriteLine($"            /// <summary>\n            /// {GeneratorUtil.FieldNameToComment(structType.name)}\n            /// </summary>");
            writer.WriteLine($"            public {GeneratorUtil.SanitizeName(structType.name)}() {{ }}\n");
            writer.WriteLine($"            /// <summary>\n            /// {GeneratorUtil.FieldNameToComment(structType.name)}\n            /// </summary>");
            writer.WriteLine($"            [SetsRequiredMembers]");
            writer.WriteLine($"            public {GeneratorUtil.SanitizeName(structType.name)}(object[] fields) {{");
            writer.WriteLine("                FieldReader reader = new FieldReader(fields);");
            int fieldIdx = 0;
            foreach (rootConfiguratorStructItem field in structType.item)
            {
                long? to = null;
                long? from = null;
                if (field.type == null)
                    continue;
                field.type = CorrectFieldType(field.name, field.type);
                if (field.max != null && long.TryParse(field.max, out long toVal))
                    to = toVal;
                if (field.min != null && long.TryParse(field.min, out long fromVal))
                    from = fromVal;
                if (field.length != 0)
                    to = field.length;

                WriteFieldReader(field.optional, field.isNullable, field.array, field.type, field.fieldId == null ? fieldIdx.ToString() : field.fieldId, from, to, (GeneratorUtil.SanitizeName(field.name) == GeneratorUtil.SanitizeName(structType.name) ? GeneratorUtil.SanitizeName(field.name + "Field") : GeneratorUtil.SanitizeName(field.name)), structType.name, clusterConfig, writer);
                fieldIdx++;
            }
            writer.WriteLine("            }");
            foreach (rootConfiguratorStructItem field in structType.item)
            {
                if (field.type == null)
                    continue;
                bool hasDefault = field.@default != null && DefaultValid(field.@default);
                if (hasDefault && HasEnum(clusterConfig, field.type) && !HasEnumValue(clusterConfig, field.type, field.@default!))
                    hasDefault = false;
                if (hasDefault && HasBitmap(clusterConfig, field.type) && !HasBitmapValue(clusterConfig, field.type, field.@default!))
                    hasDefault = false;
                writer.Write("            public ");
                if (!field.optional)
                    writer.Write("required ");
                WriteType(field.array, field.type, writer, field.name);
                if (field.optional || field.isNullable)
                    writer.Write("?");
                if (field.name == GeneratorUtil.SanitizeName(structType.name))
                    writer.Write(" " + GeneratorUtil.SanitizeName(field.name) + "Field { get; set; }");
                else
                    writer.Write(" " + GeneratorUtil.SanitizeName(field.name) + " { get; set; }");
                if (hasDefault)
                {
                    if (HasEnum(clusterConfig, field.type) || HasBitmap(clusterConfig, field.type))
                        writer.WriteLine(" = " + GeneratorUtil.SanitizeName(field.type) + "." + field.@default + ";");
                    else
                        writer.WriteLine(" = " + SanitizeDefault(field.@default!, field.array ? "array" : field.type, field.type) + ";");
                }
                else
                    writer.WriteLine();
            }
            writer.WriteLine("            internal override void Serialize(TLVWriter writer, long structNumber = -1) {");
            writer.WriteLine("                writer.StartStructure(structNumber);");
            fieldIdx = 0;
            foreach (rootConfiguratorStructItem field in structType.item)
            {
                long? from = null;
                long? to = null;
                if (field.type == null)
                    continue;
                if (field.min != null && long.TryParse(field.min, out long fromVal))
                    from = fromVal;
                if (field.max != null && long.TryParse(field.max, out long toVal))
                    to = toVal;
                if (field.length != 0)
                    to = field.length;

                WriteStructType(field.optional, field.isNullable, field.array ? "array" : field.type, field.type, field.fieldId == null ? fieldIdx : int.Parse(field.fieldId), from, to, (GeneratorUtil.SanitizeName(field.name) == GeneratorUtil.SanitizeName(structType.name) ? GeneratorUtil.SanitizeName(field.name + "Field") : GeneratorUtil.SanitizeName(field.name)), clusterConfig, writer);
                fieldIdx++;
            }
            writer.WriteLine("                writer.EndContainer();");
            writer.WriteLine("            }");
            writer.WriteLine("        }");
        }

        private static void WriteEnum(rootConfiguratorEnum enumType, TextWriter writer, string? clusterId)
        {
            writer.WriteLine("        /// <summary>");
            writer.WriteLine("        /// " + GeneratorUtil.FieldNameToComment(enumType.name));
            writer.WriteLine("        /// </summary>");
            writer.Write("        public enum " + GeneratorUtil.SanitizeName(enumType.name) + " : ");
            WriteType(false, enumType.type, writer, enumType.name);
            writer.WriteLine(" {");
            string max = enumType.item.Last().value;
            foreach (rootConfiguratorEnumItem item in enumType.item)
            {
                if (!string.IsNullOrWhiteSpace(item.value))
                {
                    string value = item.value;
                    if (!value.StartsWith("0x"))
                    {
                        value = value.TrimStart('0');
                        if (value == string.Empty)
                            value = "0";
                    }
                    string key = GeneratorUtil.EnsureHex(clusterId) + "." + GeneratorUtil.SanitizeName(enumType.name);
                    if (item.summary != null)
                        writer.WriteLine("            /// <summary>\n            /// " + GeneratorUtil.SanitizeComment(item.summary) + "\n            /// </summary>");
                    else if (enumComments.ContainsKey(key) && enumComments[key].ContainsKey(GeneratorUtil.EnsureHex(value, 4)))
                        writer.WriteLine("            /// <summary>\n            /// " + enumComments[key][GeneratorUtil.EnsureHex(value, 4)] + "\n            /// </summary>");
                    else
                        Console.WriteLine("No comment found for " + item.name);
                    writer.WriteLine("            " + GeneratorUtil.SanitizeName(item.name) + " = " + GeneratorUtil.FormatValue(value, max) + ",");
                }
            }
            writer.WriteLine("        }");
        }

        private static void WriteBitfield(rootConfiguratorBitmap bitmapType, TextWriter writer, string? clusterId)
        {
            writer.WriteLine("        /// <summary>");
            writer.WriteLine("        /// " + GeneratorUtil.FieldNameToComment(bitmapType.name));
            writer.WriteLine("        /// </summary>");
            writer.WriteLine("        [Flags]");
            writer.Write("        public enum " + GeneratorUtil.SanitizeName(bitmapType.name) + " : ");
            WriteType(false, bitmapType.type, writer, bitmapType.name);
            writer.WriteLine(" {");
            writer.WriteLine("            /// <summary>\n            /// Nothing Set\n            /// </summary>");
            writer.WriteLine("            None = 0,");
            foreach (rootConfiguratorBitmapField item in bitmapType.field)
            {
                string key = GeneratorUtil.EnsureHex(clusterId) + "." + GeneratorUtil.SanitizeName(bitmapType.name);
                if (item.summary != null)
                    writer.WriteLine("            /// <summary>\n            /// " + GeneratorUtil.SanitizeComment(item.summary) + "\n            /// </summary>");
                else if (bitmapComments.ContainsKey(key) && bitmapComments[key].ContainsKey(GeneratorUtil.EnsureHex(item.mask, 4)))
                    writer.WriteLine("            /// <summary>\n            /// " + bitmapComments[key][GeneratorUtil.EnsureHex(item.mask, 4)] + "\n            /// </summary>");
                else
                    Console.WriteLine("No comment found for " + item.name);
                writer.WriteLine("            " + GeneratorUtil.SanitizeName(item.name) + " = " + GeneratorUtil.EnsureHex(item.mask, bitmapType.type == "bitmap8" ? 2 : 4) + ",");
            }
            writer.WriteLine("        }");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="features"></param>
        /// <param name="writer"></param>
        private static void WriteFeatures(rootConfiguratorClusterFeatures features, TextWriter writer)
        {
            writer.WriteLine("        /// <summary>");
            writer.WriteLine("        /// Supported Features");
            writer.WriteLine("        /// </summary>");
            writer.WriteLine("        [Flags]");
            writer.WriteLine("        public enum Feature {");
            foreach (rootConfiguratorClusterFeaturesFeature item in features.feature)
            {
                if (item.summary != null)
                    writer.WriteLine("            /// <summary>\n            /// " + GeneratorUtil.SanitizeComment(item.summary) + "\n            /// </summary>");
                writer.WriteLine("            " + GeneratorUtil.SanitizeName(item.name) + " = " + (1 << item.bit) + ",");
            }
            writer.WriteLine("        }");
        }

        private static void WriteAttribute(configurator clusterConfig, rootConfiguratorClusterAttribute attribute, TextWriter writer)
        {
            bool hasDefault = attribute.@default != null && DefaultValid(attribute.@default);
            if (hasDefault && HasEnum(clusterConfig, attribute.type) && !HasEnumValue(clusterConfig, attribute.type, attribute.@default!))
                hasDefault = false;
            if (hasDefault && HasBitmap(clusterConfig, attribute.type) && !HasBitmapValue(clusterConfig, attribute.type, attribute.@default!))
                hasDefault = false;
            //TODO - Verify no write only properties
            //if (attribute. == true)
            {
                writer.WriteLine($"        /// <summary>\n        /// Get the {GeneratorUtil.FieldNameToComment(attribute.description ?? attribute.text, attribute.type)} attribute\n        /// </summary>");
                writer.Write("        public async Task<");
                bool array = "array".Equals(attribute.type, StringComparison.InvariantCultureIgnoreCase);
                WriteType(array, array ? attribute.entryType : attribute.type, writer, attribute.description ?? attribute.text);
                if (attribute.isNullable == true)
                    writer.Write('?');
                writer.WriteLine("> Get" + (attribute.description ?? attribute.text) + "(SecureSession session) {");
                if (!array)
                    writer.Write("            return ");
                if (HasStruct(clusterConfig, attribute.type))
                    writer.Write($"new {GeneratorUtil.SanitizeName(attribute.type)}((object[])(await GetAttribute(session, " + Convert.ToUInt32(attribute.code, 16) + "))!)");
                else if (array)
                {
                    writer.WriteLine($"            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, {Convert.ToUInt32(attribute.code, 16)}))!);");
                    writer.Write("            ");
                    WriteType(false, attribute.entryType, writer, attribute.description ?? attribute.text);
                    writer.Write("[] list = new ");
                    if (WriteType(false, attribute.entryType, writer, attribute.description ?? attribute.text, true))
                        writer.WriteLine("reader.Count][];");
                    else
                        writer.WriteLine("[reader.Count];");
                    writer.WriteLine("            for (int i = 0; i < reader.Count; i++)");
                    writer.Write("                list[i] = ");
                    long? from = null;
                    long? to = null;
                    if (attribute.min != null && long.TryParse(attribute.min, out long fromVal))
                        from = fromVal;
                    if (attribute.max != null && long.TryParse(attribute.max, out long toVal))
                        to = toVal;
                    if (attribute.lengthSpecified)
                        to = attribute.length;
                    WriteFieldReader(false, false, false, attribute.entryType, "i", from, to, "", "", clusterConfig, writer);
                    writer.Write("            return list");
                    hasDefault = false;
                }
                else
                {
                    if (attribute.type == "epoch_s")
                        writer.Write("TimeUtil.FromEpochSeconds((uint");
                    else
                    {
                        writer.Write('(');
                        WriteType(false, attribute.type, writer, attribute.description ?? attribute.text);
                        if (attribute.isNullable == true || hasDefault)
                            writer.Write('?');
                    }
                    if (HasEnum(clusterConfig, attribute.type) || HasBitmap(clusterConfig, attribute.type))
                        writer.Write(")await GetEnumAttribute(session, " + Convert.ToUInt32(attribute.code, 16));
                    else
                    {
                        writer.Write(")(dynamic?)");
                        if (attribute.isNullable != true && !hasDefault)
                            writer.Write('(');
                        writer.Write("await GetAttribute(session, " + Convert.ToUInt32(attribute.code, 16));
                    }
                    if (attribute.type == "epoch_s")
                        writer.Write(')');
                    else if (attribute.isNullable == true)
                        writer.Write(", true");
                    writer.Write(')');
                    if (!HasEnum(clusterConfig, attribute.type) && !HasBitmap(clusterConfig, attribute.type) && (attribute.isNullable != true && !hasDefault))
                        writer.Write(")!");
                }

                if (hasDefault)
                {
                    if (HasEnum(clusterConfig, attribute.type) || HasBitmap(clusterConfig, attribute.type))
                        writer.WriteLine(" ?? " + GeneratorUtil.SanitizeName(attribute.type) + "." + attribute.@default + ";");
                    else
                        writer.WriteLine(" ?? " + SanitizeDefault(attribute.@default!, attribute.type, attribute.entryType) + ";");
                }
                else
                    writer.WriteLine(";");
                writer.WriteLine("        }");
            }
            if (attribute.writable)
            {
                //if (attribute.access.read) //Uncomment if read is optional
                writer.WriteLine();
                writer.WriteLine($"        /// <summary>\n        /// Set the {GeneratorUtil.FieldNameToComment(attribute.description ?? attribute.text)} attribute\n        /// </summary>");
                writer.Write("        public async Task Set" + (attribute.description ?? attribute.text) + " (SecureSession session, ");
                bool array = "array".Equals(attribute.type, StringComparison.InvariantCultureIgnoreCase);
                WriteType(array, array ? attribute.entryType : attribute.type, writer, attribute.description ?? attribute.text);
                if (attribute.isNullable == true || hasDefault)
                    writer.Write('?');
                writer.Write(" value");
                if (hasDefault)
                {
                    if (HasEnum(clusterConfig, attribute.type) || HasBitmap(clusterConfig, attribute.type))
                        writer.Write(" = " + attribute.type + "." + attribute.@default);
                    else if (!array)
                        writer.Write(" = " + SanitizeDefault(attribute.@default!, attribute.type, attribute.entryType));
                }
                writer.WriteLine(") {");
                writer.Write("            await SetAttribute(session, " + Convert.ToUInt32(attribute.code, 16) + ", value");
                if (hasDefault && array)
                    writer.Write(" ?? " + SanitizeDefault(attribute.@default!, attribute.type, attribute.entryType));
                if (attribute.isNullable == true)
                    writer.Write(", true");
                writer.WriteLine(");");
                writer.WriteLine("        }");
            }
        }

        private static bool WriteType(bool array, string type, TextWriter writer, string name, bool openIndex = false)
        {
            if (array)
            {
                WriteType(false, type!, writer, name);
                if (openIndex)
                    writer.Write("[");
                else
                    writer.Write("[]");
                return true;
            }

            if (name == "MessageID" && type == "octet_string")
                type = name;

            switch (type)
            {
                case "int8u":
                case "INT8U":
                case "enum8":
                case "bitmap8":
                case "tag":
                case "namespace":
                case "fabric_idx":
                case "action_id":
                case "percent":
                case "UnsignedTemperature":
                    writer.Write("byte");
                    break;
                case "int16u":
                case "bitmap16":
                case "enum16":
                case "group_id":
                case "endpoint_no":
                case "vendor_id":
                case "entry_idx":
                    writer.Write("ushort");
                    break;
                case "percent100ths":
                    writer.Write("decimal");
                    break;
                case "devtype_id":
                    writer.Write("DeviceTypeEnum");
                    break;
                case "int24u":
                case "int32u":
                case "bitmap32":
                case "cluster_id":
                case "attrib_id":
                case "field_id":
                case "event-id":
                case "command_id":
                case "trans_id":
                case "data_ver":
                    writer.Write("uint");
                    break;
                case "elapsed_s":
                case "systime_ms":
                case "systime_us":
                    writer.Write("TimeSpan");
                    break;
                case "posix_ms":
                    writer.Write("DateTimeOffset");
                    break;
                case "epoch_s":
                case "epoch_us":
                case "utc":
                    includes.Add("MatterDotNet.Util");
                    writer.Write("DateTime");
                    break;
                case "int40u":
                case "int48u":
                case "int56u":
                case "int64u":
                case "bitmap64":
                case "fabric_id":
                case "node_id":
                case "EUI64":
                case "event_no":
                case "subject_id":
                    writer.Write("ulong");
                    break;
                case "int8s":
                case "SignedTemperature":
                    writer.Write("sbyte");
                    break;
                case "int16s":
                case "temperature":
                case "TemperatureDifference":
                    writer.Write("short");
                    break;
                case "int24s":
                case "int32s":
                    writer.Write("int");
                    break;
                case "int40s":
                case "int48s":
                case "int56s":
                case "int64s":
                case "power_mw":
                case "amperage_ma":
                case "voltage_mv":
                case "energy_mwh":
                    writer.Write("long");
                    break;
                case "single":
                    writer.Write("float");
                    break;
                case "ipadr":
                case "ipv4adr":
                case "ipv6adr":
                    includes.Add("System.Net");
                    writer.Write("IPAddress");
                    break;
                case "hwadr":
                    includes.Add("System.Net.NetworkInformation");
                    writer.Write("PhysicalAddress");
                    break;
                case "MessageID":
                    writer.Write("Guid");
                    break;
                case "octet_string":
                case "long_octet_string":
                case "ipv6pre":
                    if (openIndex)
                        writer.Write("byte[");
                    else
                        writer.Write("byte[]");
                    return true;
                case "char_string":
                case "CHAR_STRING":
                case "long_char_string":
                case "LONG_CHAR_STRING":
                    writer.Write("string");
                    break;
                case "boolean":
                    writer.Write("bool");
                    break;
                case "double":
                    writer.Write("double");
                    break;
                case "tod":
                case "date":
                    throw new NotImplementedException();
                case "status":
                    includes.Add("MatterDotNet.Protocol.Payloads.Status");
                    writer.Write("IMStatusCode");
                    break;
                default:
                    writer.Write(GeneratorUtil.SanitizeName(type));
                    break;
            }
            return false;
        }

        private static string SanitizeDefault(string value, string? type, string? arrayType)
        {
            if (value == "\"")
                return "\"\"";
            if (value == "empty" || ("array".Equals(type, StringComparison.InvariantCultureIgnoreCase) && value == "0"))
            {
                if (type == "array" || type == "ARRAY")
                {
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    sw.Write("Array.Empty<");
                    WriteType(false, arrayType!, sw, string.Empty);
                    sw.Write(">()");
                    return sb.ToString();
                }
                else if (type?.Equals("char_string", StringComparison.InvariantCultureIgnoreCase) == true)
                    return "\"\"";
                return "[]";
            }
            if (value.StartsWith("0x"))
                return value.Split(' ')[0];
            if (value.StartsWith('"'))
                return value;
            if (value == "0" && (type == "epoch_s" || type == "epoch_us" || type == "utc"))
                return "TimeUtil.EPOCH";
            if (value == "0xFFFFFFFFFFFFFFFF" && (type == "epoch_s" || type == "epoch_us" || type == "utc"))
                return "DateTime.MinValue";
            if (type == "elapsed_s")
            {
                if (value == "null")
                    return value;
                if (value.EndsWith('s'))
                    value = value.Substring(0, value.Length - 1);
                return $"TimeSpan.FromSeconds({value})";
            }
            if (type == "boolean")
                return (value == "1" || value.Equals("true", StringComparison.InvariantCultureIgnoreCase)) ? "true" : "false";
            if (type == "status" && value == "SUCCESS")
                return "IMStatusCode.SUCCESS";
            if (type == "int16s" && value == "0x8000")
                return "short.MinValue";
            if (type == "TemperatureDifference" || type == "SignedTemperature" || type == "UnsignedTemperature" || type == "temperature")
            {
                if (value == "null")
                    return value;
                if (value.EndsWith("°C"))
                    value = value.Substring(0, value.Length - 2);
                return value;
            }
            return value.ToLowerInvariant();
        }

        private static bool DefaultValid(string value)
        {
            if (value.Equals("MS", StringComparison.InvariantCultureIgnoreCase))
                return false;
            if (value.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                return false;
            if (value == "-")
                return false;
            if (value == "PhysicalMinLevel" || value == "PhysicalMaxLevel")
                return false;
            if (value == "AbsMinHeatSetpointLimit" || value == "AbsMaxHeatSetpointLimit")
                return false;
            if (value == "AbsMinCoolSetpointLimit" || value == "AbsMaxCoolSetpointLimit")
                return false;
            return !string.IsNullOrEmpty(value);
        }
    }
}
