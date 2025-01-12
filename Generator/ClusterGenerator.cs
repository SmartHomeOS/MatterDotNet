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
            StreamWriter clusterBase = new StreamWriter("outputs\\ClusterBase.cs");
            foreach (string clusterxml in clusterxmls)
            {
                includes.Clear();
                if (clusterxml.EndsWith(".xml"))
                {
                    Console.WriteLine("Generating " + clusterxml + "...");
                    Cluster? cluster = deserializer.Deserialize(File.OpenRead(clusterxml)) as Cluster;
                    if (cluster == null)
                        throw new IOException("Failed to parse cluster " + clusterxml);
                    if (!string.IsNullOrEmpty(cluster.clusterIds.clusterId.id))
                        clusterBase.WriteLine($"                case {GeneratorUtil.SanitizeClassName(cluster.name)}.CLUSTER_ID:\n                    return new {GeneratorUtil.SanitizeClassName(cluster.name)}(endPoint);");
                    WriteClass(cluster);
                }
            }
            clusterBase.Close();
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
            includes.Add("MatterDotNet.Protocol.Sessions");
            writer.WriteLine();
            writer.WriteLine($"namespace MatterDotNet.Clusters.{GeneratorUtil.SanitizeName(cluster.classification.role) ?? "Misc"}\n{{");
            writer.WriteLine("    /// <summary>");
            writer.WriteLine($"    /// {cluster.name}");
            writer.WriteLine("    /// </summary>");
            if (!string.IsNullOrEmpty(cluster.clusterIds.clusterId.id))
                writer.WriteLine($"    [ClusterRevision(CLUSTER_ID, {cluster.revision})]");
            writer.WriteLine("    public class " + GeneratorUtil.SanitizeClassName(cluster.name) + (string.IsNullOrEmpty(cluster.classification.baseCluster) ? " : ClusterBase" : $" : {GeneratorUtil.SanitizeClassName(cluster.classification.baseCluster)}Cluster"));
            writer.WriteLine("    {");
            if (!string.IsNullOrEmpty(cluster.clusterIds.clusterId.id))
                writer.WriteLine("        internal const uint CLUSTER_ID = " + cluster.clusterIds.clusterId.id + ";");
            writer.WriteLine();
            writer.WriteLine("        /// <summary>");
            writer.WriteLine($"        /// {cluster.name}");
            writer.WriteLine("        /// </summary>");
            if (string.IsNullOrEmpty(cluster.clusterIds.clusterId.id))
                writer.WriteLine("        public " + GeneratorUtil.SanitizeClassName(cluster.name) + "(uint cluster, ushort endPoint) : base(cluster, endPoint) { }");
            else
                writer.WriteLine("        public " + GeneratorUtil.SanitizeClassName(cluster.name) + "(ushort endPoint) : base(CLUSTER_ID, endPoint) { }");
            if (string.IsNullOrEmpty(cluster.classification.baseCluster) && !string.IsNullOrEmpty(cluster.clusterIds.clusterId.id))
                writer.WriteLine($"        /// <inheritdoc />\n        protected {GeneratorUtil.SanitizeClassName(cluster.name)}(uint cluster, ushort endPoint) : base(cluster, endPoint) {{ }}");
            writer.WriteLine();
            if (cluster.dataTypes?.@enum != null || cluster.dataTypes?.bitmap != null || (cluster.features != null && cluster.features.Length > 0))
            {
                writer.WriteLine("        #region Enums");
                bool firstEnum=true;
                if (cluster.features != null && cluster.features.Length > 0)
                {
                    firstEnum = false;
                    WriteFeatures(cluster.features, writer);
                }
                if (cluster.dataTypes?.@enum != null)
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
                if (cluster.dataTypes?.bitmap != null)
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
            if (cluster.dataTypes?.@struct != null)
            {
                includes.Add("System.Diagnostics.CodeAnalysis");
                includes.Add("MatterDotNet.Protocol.Payloads");
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
                    WriteRecord(structType, cluster, writer);
                }
                writer.WriteLine("        #endregion Records");
                writer.WriteLine();
            }
            if (cluster.commands != null && cluster.commands.Length > 0)
            {
                includes.Add("MatterDotNet.Messages.InteractionModel");
                includes.Add("MatterDotNet.Protocol.Subprotocols");
                writer.WriteLine("        #region Payloads");
                bool firstPayload = true;
                foreach (var command in cluster.commands)
                {
                    if (command.field == null)
                        continue;
                    includes.Add("MatterDotNet.Protocol.Payloads");
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

            if ((cluster.attributes != null && cluster.attributes.Length > 0) || (cluster.features != null && cluster.features.Length > 0))
            {
                writer.WriteLine("        #region Attributes");
                bool firstAttribute = true;
                if (cluster.features != null && cluster.features.Length > 0)
                {
                    firstAttribute = false;
                    WriteFeatureFunctions(writer);
                }
                if (cluster.attributes != null && cluster.attributes.Length > 0)
                {
                    foreach (var attribute in cluster.attributes)
                    {
                        if (attribute.type != null && attribute?.mandatoryConform?.condition?.name != "Zigbee")
                        {
                            if (firstAttribute)
                                firstAttribute = false;
                            else
                                writer.WriteLine();
                            WriteAttribute(cluster, attribute, writer);
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

        private static void WriteStruct(clusterCommand command, bool toServer, TextWriter writer, Cluster cluster)
        {
            if (!toServer)
                writer.WriteLine($"        /// <summary>\n        /// {GeneratorUtil.FieldNameToComment(command.name)} - Reply from server\n        /// </summary>");
           writer.WriteLine($"        {(toServer ? "private record" : "public struct")} " + GeneratorUtil.SanitizeName(command.name) + (toServer ? "Payload : TLVPayload {" : "() {"));
            foreach (clusterCommandField field in command.field)
            {
                if (field.type == null || field.disallowConform != null) //Reserved/removed fields
                    continue;
                if (field.type == "enum8" && field.name == "Status")
                    field.type = "status";
                writer.Write("            public ");
                bool optional = field.mandatoryConform == null;
                if (!optional && (field.mandatoryConform!.feature != null || field.mandatoryConform.orTerm != null))
                    optional = true;
                if (!optional)
                    writer.Write("required ");
                WriteType(field.type, field.entry?.type, writer);
                if (optional || field.quality?.nullable == true)
                    writer.Write("?");
                if (field.name == GeneratorUtil.SanitizeName(command.name))
                    writer.Write(" " + GeneratorUtil.SanitizeName(field.name) + "Field { get; set; }");
                else
                    writer.Write(" " + GeneratorUtil.SanitizeName(field.name) + " { get; set; }");
                if (field.@default != null && DefaultValid(field.@default))
                {
                    if (field.type.EndsWith("Enum") && field.@default != "0")
                        writer.WriteLine(" = " + field.type + "." + field.@default + ";");
                    else
                        writer.WriteLine(" = " + SanitizeDefault(field.@default, field.type, field.entry?.type) + ";");
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
                    long? from = null;
                    long? to = null;
                    if (field.type == "max 254")
                            to = 254;
                    if (field.constraint != null)
                    {
                        switch (field.constraint.type)
                        {
                            case "min":
                            case "minCount":
                            case "minLength":
                                if (long.TryParse(field.constraint.value, out long fromVal))
                                    from = fromVal;
                                break;
                            case "max":
                            case "maxCount":
                            case "maxLength":
                                if (field.constraint.value == "RESP_MAX Constant Type")
                                    to = Constants.RESP_MAX;
                                else if (long.TryParse(field.constraint.value, out long toVal))
                                    to = toVal;
                                break;
                            case "between":
                            case "countBetween":
                            case "lengthBetween":
                                if (long.TryParse(field.constraint.to, out long parsedTo))
                                    to = parsedTo;
                                if (long.TryParse(field.constraint.from, out long parsedFrom))
                                    from = parsedFrom;
                                break;
                            case "allowed":
                                if (long.TryParse(field.constraint.value, out long parsedAllowed))
                                    to = parsedAllowed;
                                break;
                            case "desc":
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    bool optional = field.mandatoryConform == null;
                    if (!optional && (field.mandatoryConform!.feature != null || field.mandatoryConform.orTerm != null))
                        optional = true;
                    WriteStructType(optional, field.quality?.nullable == true, field.type, field.entry?.type, field.id, from, to, (field.name == GeneratorUtil.SanitizeName(command.name) ? field.name + "Field" : field.name), cluster, writer);
                }
                writer.WriteLine("                writer.EndContainer();");
                writer.WriteLine("            }");
            }
            writer.WriteLine("        }");
        }

        private static void WriteStructType(bool optional, bool nullable, string type, string? entryType, int id, long? from, long? to, string name, Cluster cluster, TextWriter writer)
        {
            bool unsigned = false;
            string totalIndent = "                ";
            if (optional)
            {
                writer.WriteLine($"{totalIndent}if ({name} != null)");
                if (type != "list" && type != "array")
                    totalIndent += "    ";
            }
            switch (type)
            {
                case "list": //Actually an array
                    if (nullable)
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
                    return;
                case "bool":
                    writer.WriteLine($"{totalIndent}writer.WriteBool({id}, {name});");
                    return;
                case "int8":
                case "SignedTemperature":
                    writer.Write($"{totalIndent}writer.WriteSByte({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", sbyte.MaxValue");
                    break;
                case "int16":
                case "temperature":
                case "TemperatureDifference":
                    writer.Write($"{totalIndent}writer.WriteShort({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", short.MaxValue");
                    break;
                case "int24":
                case "int32":
                    writer.Write($"{totalIndent}writer.WriteInt({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", int.MaxValue");
                    break;
                case "int40":
                case "int48":
                case "int56":
                case "int64":
                case "power-mW":
                case "amperage-mA":
                case "voltage-mV":
                case "energy-mWh":
                    writer.Write($"{totalIndent}writer.WriteLong({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", long.MaxValue");
                    break;
                case "max 254":
                case "uint8":
                case "enum8":
                case "map8":
                case "tag":
                case "namespace":
                case "fabric-idx":
                case "action-id":
                case "percent":
                case "UnsignedTemperature":
                    writer.Write($"{totalIndent}writer.WriteByte({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", byte.MaxValue");
                    unsigned = true;
                    break;
                case "uint16":
                case "map16":
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
                    unsigned = true;
                    break;
                case "uint24":
                case "uint32":
                case "map32":
                case "cluster-id":
                case "attribute-id":
                case "field-id":
                case "event-id":
                case "command-id":
                case "trans-id":
                case "data-ver":
                case "AlarmBitmap":
                    writer.Write($"{totalIndent}writer.WriteUInt({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", uint.MaxValue");
                    unsigned = true;
                    break;
                case "epoch-s":
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
                case "elapsed-s":
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
                case "epoch-us":
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
                case "ref_DataTypeSystemTimeUs":
                case "ref_DataTypeSystemTimeMs":
                case "systime-ms":
                case "systime-us":
                    if (nullable && !optional)
                        writer.Write($"{totalIndent}if (!{name}.HasValue)\n{totalIndent}    writer.WriteNull({id});\n{totalIndent}else\n    ");
                    writer.Write($"{totalIndent}writer.WriteULong({id}, (ulong){name}");
                    if (nullable || optional)
                        writer.Write("!.Value");
                    if (type == "ref_DataTypeSystemTimeMs" || type == "systime-ms")
                        writer.Write(".TotalMilliseconds");
                    else
                        writer.Write(".TotalMicroseconds");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null && from != 0)
                        writer.Write(", ulong.MaxValue");
                    unsigned = true;
                    break;
                case "ref_DataTypePosixMs":
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
                case "uint40":
                case "uint48":
                case "uint56":
                case "uint64":
                case "map64":
                case "fabric-id":
                case "node-id":
                case "EUI64":
                case "event-no":
                case "SubjectID":
                case "subject-id":
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
                case "ref_IpAdr":
                case "ref_Ipv4Adr":
                case "ref_Ipv6Adr":
                case "ipv6adr":
                    if (nullable && !optional)
                        writer.Write($"{totalIndent}if ({name} == null)\n{totalIndent}    writer.WriteNull({id});\n{totalIndent}else\n    ");
                    writer.WriteLine($"{totalIndent}writer.WriteBytes({id}, {name}.GetAddressBytes());");
                    return;
                case "Hardware Address":
                case "hwadr":
                    if (nullable && !optional)
                        writer.Write($"{totalIndent}if ({name} == null)\n{totalIndent}    writer.WriteNull({id});\n{totalIndent}else\n    ");
                    writer.WriteLine($"{totalIndent}writer.WriteBytes({id}, {name}.GetAddressBytes());");
                    return;
                case "devtype-id":
                    writer.WriteLine($"{totalIndent}writer.WriteUInt({id}, {(nullable ? "(uint?)" : "(uint)")}{name});");
                    return;
                case "MessageID":
                    if (nullable && !optional)
                        writer.Write($"{totalIndent}if ({name} == null)\n{totalIndent}    writer.WriteNull({id});\n{totalIndent}else\n    ");
                    writer.WriteLine($"{totalIndent}writer.WriteBytes({id}, {name}.ToByteArray());");
                    return;
                case "octstr":
                case "ipv6pre":
                    writer.Write($"{totalIndent}writer.WriteBytes({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", int.MaxValue");
                    break;
                case "string":
                    writer.Write($"{totalIndent}writer.WriteString({id}, {name}");
                    if (to != null)
                        writer.Write($", {to.Value}");
                    else if (from != null)
                        writer.Write(", int.MaxValue");
                    break;
                default:
                    if (type == "status")
                        writer.WriteLine($"{totalIndent}writer.WriteByte({id}, {(nullable ? "(byte?)" : "(byte)")}{name});");
                    else if (HasEnum(cluster, type) || HasBitmap(cluster, type) || type == "ref_CharacteristicEnum" || type == "ref_MeasurementTypeEnum")
                        writer.WriteLine($"{totalIndent}writer.WriteUShort({id}, {(nullable ? "(ushort?)" : "(ushort)")}{name});");
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

        private static void WriteFieldReader(bool optional, bool nullable, string type, string? entryType, string id, long? from, long? to, string name, string structName, Cluster cluster, TextWriter writer)
        {
            string totalIndent = "                ";
            if (id != "-1" && type != "list" && id != "i")
            {
                if (name == GeneratorUtil.SanitizeName(structName))
                    writer.Write($"{totalIndent}{name}Field = ");
                else
                    writer.Write($"{totalIndent}{name} = ");
            }
            bool extraClose = false;
            switch (type)
            {
                case "list":
                    writer.WriteLine($"{totalIndent}{{");
                    writer.Write($"{totalIndent}    {name} = new ");
                    WriteType(entryType!, null, writer);
                    writer.WriteLine($"[((object[])fields[{id}]).Length];");
                    writer.WriteLine($"{totalIndent}    for (int i = 0; i < {name}.Length; i++) {{");
                    writer.Write($"{totalIndent}        {name}[i] = ");
                    WriteFieldReader(false, false, entryType!, null, "-1", null, null, $"fields[{id}]", structName, cluster, writer);
                    writer.WriteLine($"{totalIndent}    }}");
                    writer.WriteLine($"{totalIndent}}}");
                    return;
                case "bool":
                    writer.Write($"reader.GetBool({id}");
                    break;
                case "int8":
                case "SignedTemperature":
                    writer.Write($"reader.GetSByte({id}");
                    break;
                case "int16":
                case "temperature":
                case "TemperatureDifference":
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
                case "voltage-mV":
                case "energy-mWh":
                    writer.Write($"reader.GetLong({id}");
                    break;
                case "max 254":
                case "uint8":
                case "enum8":
                case "map8":
                case "tag":
                case "namespace":
                case "fabric-idx":
                case "action-id":
                case "percent":
                case "UnsignedTemperature":
                    writer.Write($"reader.GetByte({id}");
                    break;
                case "uint16":
                case "enum16":
                case "map16":
                case "group-id":
                case "endpoint-no":
                case "vendor-id":
                case "entry-idx":
                case "percent100ths":
                    writer.Write($"reader.GetUShort({id}");
                    break;
                case "uint24":
                case "uint32":
                case "map32":
                case "cluster-id":
                case "attribute-id":
                case "field-id":
                case "event-id":
                case "command-id":
                case "trans-id":
                case "data-ver":
                case "AlarmBitmap":
                    writer.Write($"reader.GetUInt({id}");
                    break;
                case "uint40":
                case "uint48":
                case "uint56":
                case "uint64":
                case "map64":
                case "fabric-id":
                case "node-id":
                case "EUI64":
                case "event-no":
                case "SubjectID":
                case "subject-id":
                    writer.Write($"reader.GetULong({id}");
                    break;
                case "single":
                    writer.Write($"reader.GetFloat({id}");
                    break;
                case "double":
                    writer.Write($"reader.GetDouble({id}");
                    break;
                case "ref_IpAdr":
                    writer.WriteLine($"new IPAddress(reader.GetBytes({id}, {(optional ? "true" : "false")}, 16, 4)!);;");
                    return;
                case "ref_Ipv4Adr":
                    writer.WriteLine($"new IPAddress(reader.GetBytes({id}, {(optional ? "true" : "false")}, 4, 4)!);;");
                    return;
                case "ref_Ipv6Adr":
                case "ipv6adr":
                    writer.WriteLine($"new IPAddress(reader.GetBytes({id}, {(optional ? "true" : "false")}, 16, 16)!);;");
                    return;
                case "Hardware Address":
                case "hwadr":
                    writer.WriteLine($"new PhysicalAddress(reader.GetBytes({id}, {(optional ? "true" : "false")}, 8, 6)!);");
                    return;
                case "MessageID":
                    writer.WriteLine($"new Guid(reader.GetBytes({id}, {(optional ? "true" : "false")}, 16, 16)!);");
                    return;
                case "epoch-s":
                case "utc":
                    includes.Add("MatterDotNet.Util");
                    writer.Write($"TimeUtil.FromEpochSeconds(reader.GetUInt({id}");
                    extraClose = true;
                    break;
                case "elapsed-s":
                    writer.Write($"TimeSpan.FromSeconds(reader.GetUInt({id}");
                    extraClose = true;
                    break;
                case "epoch-us":
                    includes.Add("MatterDotNet.Util");
                    writer.Write($"TimeUtil.FromEpochUS(reader.GetULong({id}");
                    extraClose = true;
                    break;
                case "ref_DataTypeSystemTimeUs":
                case "systime-us":
                    writer.Write($"TimeUtil.FromMicros(reader.GetULong({id}");
                    extraClose = true;
                    break;
                case "ref_DataTypeSystemTimeMs":
                case "systime-ms":
                    writer.Write($"TimeUtil.FromMillis(reader.GetULong({id}");
                    extraClose = true;
                    break;
                case "ref_DataTypePosixMs":
                    writer.Write($"DateTimeOffset.FromUnixTimeMilliseconds(reader.GetULong({id}");
                    extraClose = true;
                    break;
                case "devtype-id":
                    writer.Write($"(DeviceTypeEnum)reader.GetUInt({id}");
                    if (optional)
                        writer.Write(", true");
                    writer.WriteLine(")!.Value;");
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
                    writer.WriteLine(';');
                    return;
                default:
                    if (HasEnum(cluster, type) || HasBitmap(cluster, type) || type == "status" || type == "ref_CharacteristicEnum" || type == "ref_MeasurementTypeEnum")
                    {
                        if (type == "ref_CharacteristicEnum")
                            type = "MediaPlaybackCluster.CharacteristicEnum";
                        else if (type == "ref_MeasurementTypeEnum")
                            type = "MeasurementType";

                        if (type == "status")
                            writer.Write($"(IMStatusCode)reader.GetByte({id}");
                        else
                            writer.Write($"({GeneratorUtil.SanitizeName(type)}");
                        if (optional)
                            writer.Write('?');
                        writer.Write($")reader.GetUShort({id}");
                        if (optional)
                            writer.WriteLine(", true);");
                        else
                            writer.WriteLine(")!.Value;");
                    }
                    else {
                        writer.Write($"new ");
                        WriteType(type, entryType, writer);
                        if (id == "i")
                            writer.WriteLine("(reader.GetStruct(i)!);");
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

        private static bool HasEnum(Cluster cluster, string name)
        {
            if (cluster.dataTypes?.@enum == null)
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
            if (cluster.dataTypes?.@enum == null)
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
            if (cluster.dataTypes?.bitmap == null)
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
            if (cluster.dataTypes?.bitmap == null)
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
            if (cluster.dataTypes?.@struct == null)
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
                            if (field.optionalConform != null || (field.mandatoryConform != null && (field.mandatoryConform.feature != null || field.mandatoryConform.orTerm != null)))
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
                            writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, " + cmd.id + ", requestFields);");
                        else
                            writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, " + cmd.id + ", requestFields);");
                    }
                    else
                    {
                        if (cmd.response == "N")
                        {
                            if (cmd.access.timed)
                                writer.WriteLine("            await InteractionManager.SendTimedCommand(session, endPoint, cluster, commandTimeoutMS, " + cmd.id + ");");
                            else
                                writer.WriteLine("            await InteractionManager.SendCommand(session, endPoint, cluster, " + cmd.id + ");");
                        }
                        else
                        {
                            if (cmd.access.timed)
                                writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, " + cmd.id + ");");
                            else
                                writer.WriteLine("            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, " + cmd.id + ");");
                        }
                    }
                    if (response == null || response.field == null)
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
                            if (field.type == "list" && field.entry != null && HasStruct(cluster, field.entry.type))
                            {
                                writer.Write("                " + GeneratorUtil.SanitizeName(field.name) + " = ");
                                if (field.optionalConform != null || (field.mandatoryConform != null && (field.mandatoryConform.feature != null || field.mandatoryConform.orTerm != null)))
                                    writer.Write("GetOptionalArrayField<");
                                else
                                    writer.Write("GetArrayField<");
                                WriteType(field.entry.type, null, writer);
                                writer.WriteLine($">(resp, {field.id}),");
                            }
                            else
                            {
                                writer.Write("                " + GeneratorUtil.SanitizeName(field.name) + " = ");
                                if (field.type == "ref_DataTypePosixMs" && field.quality?.nullable == true)
                                    writer.Write($"GetField(resp, {field.id}) != null ? ");
                                else
                                    writer.Write('(');
                                WriteType(field.type, field.entry?.type, writer);
                                if (field.optionalConform != null || (field.mandatoryConform != null && (field.mandatoryConform.feature != null || field.mandatoryConform.orTerm != null)))
                                {
                                    if (HasEnum(cluster, field.type) || HasBitmap(cluster, field.type))
                                        writer.Write("?)(byte");
                                    writer.WriteLine($"?)GetOptionalField(resp, {field.id}),");
                                }
                                else
                                {
                                    if (field.type == "elapsed-s")
                                        writer.WriteLine($".FromSeconds((uint)GetField(resp, {field.id}))),");
                                    else if (field.type == "ref_DataTypeSystemTimeUs" || field.type == "systime-us")
                                        writer.WriteLine($".FromMicroseconds((ulong)GetField(resp, {field.id}))),");
                                    else if (field.type == "ref_DataTypeSystemTimeMs" || field.type == "systime-ms")
                                        writer.WriteLine($".FromMilliseconds((ulong)GetField(resp, {field.id}))),");
                                    else if (field.type == "ref_DataTypePosixMs")
                                    {
                                        writer.Write($".FromUnixTimeMilliseconds((long)(ulong)GetField(resp, {field.id}))");
                                        if (field.quality?.nullable == true)
                                            writer.WriteLine(" : null,");
                                        else
                                            writer.WriteLine("),");
                                    }
                                    else
                                    {
                                        if (field.quality?.nullable == true)
                                            writer.Write('?');
                                        if (HasEnum(cluster, field.type) || HasBitmap(cluster, field.type) || field.type == "status")
                                            writer.Write(")(byte");
                                        writer.WriteLine($")GetField(resp, {field.id}),");
                                    }
                                }
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

        private static void WriteRecord(clusterDataTypesStruct structType, Cluster cluster, TextWriter writer)
        {
            writer.WriteLine($"        /// <summary>\n        /// {GeneratorUtil.FieldNameToComment(structType.name)}\n        /// </summary>");
            writer.WriteLine("        public record " + GeneratorUtil.SanitizeName(structType.name) + " : TLVPayload {");
            writer.WriteLine($"            /// <summary>\n            /// {GeneratorUtil.FieldNameToComment(structType.name)}\n            /// </summary>");
            writer.WriteLine($"            public {GeneratorUtil.SanitizeName(structType.name)}() {{ }}\n");
            writer.WriteLine($"            /// <summary>\n            /// {GeneratorUtil.FieldNameToComment(structType.name)}\n            /// </summary>");
            writer.WriteLine($"            [SetsRequiredMembers]");
            writer.WriteLine($"            public {GeneratorUtil.SanitizeName(structType.name)}(object[] fields) {{");
            writer.WriteLine("                FieldReader reader = new FieldReader(fields);");
            foreach (clusterDataTypesStructField field in structType.field)
            {
                long? to = null;
                long? from = null;
                if (field.type == null)
                    continue;
                if (field.constraint?.toSpecified == true && long.TryParse(field.constraint.to, out long toVal))
                    to = toVal;
                if (field.constraint?.fromSpecified == true && long.TryParse(field.constraint.from, out long fromVal))
                    from = fromVal;
                bool optional = field.mandatoryConform == null;
                if (!optional && (field.mandatoryConform!.feature != null || field.mandatoryConform.orTerm != null))
                    optional = true;

                WriteFieldReader(optional, field.quality?.nullable == true, field.type, field.entry?.type, field.id.ToString(), from, to, field.name, structType.name, cluster, writer);
            }
            writer.WriteLine("            }");
            foreach (clusterDataTypesStructField field in structType.field)
            {
                if (field.type == null)
                    continue;
                bool hasDefault = field.@default != null && DefaultValid(field.@default);
                if (hasDefault && HasEnum(cluster, field.type) && !HasEnumValue(cluster, field.type, field.@default!))
                    hasDefault = false;
                if (hasDefault && HasBitmap(cluster, field.type) && !HasBitmapValue(cluster, field.type, field.@default!))
                    hasDefault = false;
                writer.Write("            public ");
                if (field.mandatoryConform != null)
                    writer.Write("required ");
                WriteType(field.type, field.entry?.type, writer);
                if (field.mandatoryConform == null || field.quality?.nullable == true || (field.mandatoryConform != null && (field.mandatoryConform!.feature != null || field.mandatoryConform.orTerm != null)))
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
                        writer.WriteLine(" = " + SanitizeDefault(field.@default!, field.type, field.entry?.type) + ";");
                }
                else
                    writer.WriteLine();
            }
            writer.WriteLine("            internal override void Serialize(TLVWriter writer, long structNumber = -1) {");
            writer.WriteLine("                writer.StartStructure(structNumber);");
            foreach (clusterDataTypesStructField field in structType.field)
            {
                long? from = null;
                long? to = null;
                if (field.type == null)
                    continue;
                if (field.constraint != null)
                {
                    switch (field.constraint.type)
                    {
                        case "min":
                        case "minCount":
                        case "minLength":
                            if (long.TryParse(field.constraint.value, out long fromVal))
                                from = fromVal;
                            break;
                        case "max":
                        case "maxCount":
                        case "maxLength":
                            if (long.TryParse(field.constraint.value, out long toVal))
                                to = toVal;
                            break;
                        case "between":
                        case "lengthBetween":
                            if (long.TryParse(field.constraint.from, out long parsedFrom))
                                from = parsedFrom;
                            if (long.TryParse(field.constraint.to, out long parsedTo))
                                to = parsedTo;
                            break;
                        case "desc":
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                bool optional = field.mandatoryConform == null;
                if (!optional && (field.mandatoryConform!.feature != null || field.mandatoryConform.orTerm != null))
                    optional = true;
                WriteStructType(optional, field.quality?.nullable == true, field.type, field.entry?.type, field.id, from, to, (field.name == GeneratorUtil.SanitizeName(structType.name) ? field.name + "Field" : field.name), cluster, writer);
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
                if (!string.IsNullOrWhiteSpace(item.value))
                {
                    if (item.summary != null)
                        writer.WriteLine("            /// <summary>\n            /// " + GeneratorUtil.SanitizeComment(item.summary) + "\n            /// </summary>");
                    writer.WriteLine("            " + GeneratorUtil.SanitizeName(item.name) + " = " + item.value + ",");
                }
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
            writer.WriteLine("            /// <summary>\n            /// Nothing Set\n            /// </summary>");
            writer.WriteLine("            None = 0,");
            foreach (clusterDataTypesBitfieldItem item in enumType.bitfield)
            {
                if (item.summary != null)
                    writer.WriteLine("            /// <summary>\n            /// " + GeneratorUtil.SanitizeComment(item.summary) + "\n            /// </summary>");
                writer.WriteLine("            " + GeneratorUtil.SanitizeName(item.name) + " = " + (1<<item.bit) + ",");
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
                if (item.summary != null)
                    writer.WriteLine("            /// <summary>\n            /// " + GeneratorUtil.SanitizeComment(item.summary) + "\n            /// </summary>");
                writer.WriteLine("            " + GeneratorUtil.SanitizeName(item.name) + " = " + (1 << item.bit) + ",");
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
            if (attribute?.access?.read == true)
            {
                writer.WriteLine($"        /// <summary>\n        /// Get the {GeneratorUtil.FieldNameToComment(attribute.name)} attribute\n        /// </summary>");
                writer.Write("        public async Task<");
                WriteType(attribute.type, attribute.entry?.type, writer);
                if (attribute.quality?.nullable == true)
                    writer.Write('?');
                writer.WriteLine("> Get" + attribute.name + "(SecureSession session) {");
                if (attribute.type != "list")
                    writer.Write("            return ");
                if (HasStruct(cluster, attribute.type))
                    writer.Write($"new {GeneratorUtil.SanitizeName(attribute.type)}((object[])(await GetAttribute(session, " + Convert.ToUInt16(attribute.id, 16) + "))!)");
                else if (attribute.type == "list")
                {
                    writer.WriteLine($"            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, {Convert.ToUInt16(attribute.id, 16)}))!);");
                    writer.Write("            ");
                    WriteType(attribute.entry!.type, null, writer);
                    writer.Write("[] list = new ");
                    if (WriteType(attribute.entry!.type, null, writer, true))
                        writer.WriteLine("reader.Count][];");
                    else
                        writer.WriteLine("[reader.Count];");
                    writer.WriteLine("            for (int i = 0; i < reader.Count; i++)");
                    writer.Write("                list[i] = ");
                    long? from = null;
                    long? to = null;
                    if (attribute.constraint?.fromSpecified == true && long.TryParse(attribute.constraint.from, out long fromVal))
                        from = fromVal;
                    if (attribute.constraint?.toSpecified == true && long.TryParse(attribute.constraint.to, out long toVal))
                        to = toVal;
                    WriteFieldReader(false, false, attribute.entry!.type, null, "i", from, to, "", "", cluster, writer);
                    writer.Write("            return list");
                    hasDefault = false;
                }
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
                    if (!HasEnum(cluster, attribute.type) && !HasBitmap(cluster, attribute.type) && (attribute.quality?.nullable != true && !hasDefault))
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
            if (attribute?.access?.write == "true" || attribute?.access?.write == "optional")
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

        private static bool WriteType(string type, string? entryType, TextWriter writer, bool openIndex = false)
        {
            switch (type)
            {
                case "max 254":
                case "uint8":
                case "enum8":
                case "map8":
                case "tag":
                case "namespace":
                case "fabric-idx":
                case "action-id":
                case "percent":
                case "UnsignedTemperature":
                    writer.Write("byte");
                    break;
                case "uint16":
                case "map16":
                case "enum16":
                case "group-id":
                case "endpoint-no":
                case "vendor-id":
                case "entry-idx":
                case "percent100ths":
                    writer.Write("ushort");
                    break;
                case "devtype-id":
                    writer.Write("DeviceTypeEnum");
                    break;
                case "uint24":
                case "uint32":
                case "map32":
                case "cluster-id":
                case "attribute-id":
                case "field-id":
                case "event-id":
                case "command-id":
                case "trans-id":
                case "data-ver":
                case "AlarmBitmap":
                    writer.Write("uint");
                    break;
                case "elapsed-s":
                case "systime-ms":
                case "systime-us":
                case "ref_DataTypeSystemTimeUs":
                case "ref_DataTypeSystemTimeMs":
                    writer.Write("TimeSpan");
                    break;
                case "ref_DataTypePosixMs":
                    writer.Write("DateTimeOffset");
                    break;
                case "epoch-s":
                case "epoch-us":
                case "utc":
                    includes.Add("MatterDotNet.Util");
                    writer.Write("DateTime");
                    break;
                case "uint40":
                case "uint48":
                case "uint56":
                case "uint64":
                case "map64":
                case "fabric-id":
                case "node-id":
                case "EUI64":
                case "event-no":
                case "SubjectID":
                case "subject-id":
                    writer.Write("ulong");
                    break;
                case "int8":
                case "SignedTemperature":
                    writer.Write("sbyte");
                    break;
                case "int16":
                case "temperature":
                case "TemperatureDifference":
                    writer.Write("short");
                    break;
                case "int24":
                case "int32":
                    writer.Write("int");
                    break;
                case "int40":
                case "int48":
                case "int56":
                case "int64":
                case "power-mW":
                case "amperage-mA":
                case "voltage-mV":
                case "energy-mWh":
                    writer.Write("long");
                    break;
                case "list": //Really an array
                    WriteType(entryType!, null, writer);
                    if (openIndex)
                        writer.Write("[");
                    else
                        writer.Write("[]");
                    return true;
                case "single":
                    writer.Write("float");
                    break;
                case "ref_IpAdr":
                case "ref_Ipv4Adr":
                case "ref_Ipv6Adr":
                case "ipv6adr":
                    includes.Add("System.Net");
                    writer.Write("IPAddress");
                    break;
                case "Hardware Address":
                case "hwadr": //This is the most non-standard standard i've ever worked with
                    includes.Add("System.Net.NetworkInformation");
                    writer.Write("PhysicalAddress");
                    break;
                case "MessageID":
                    writer.Write("Guid");
                    break;
                case "octstr":
                case "ipv6pre":
                    if (openIndex)
                        writer.Write("byte[");
                    else
                        writer.Write("byte[]");
                    return true;
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
                case "ref_MeasurementAccuracyStruct":
                    includes.Add("MatterDotNet.Messages");
                    writer.Write("MeasurementAccuracy");
                    break;
                case "ref_MeasurementTypeEnum":
                    includes.Add("MatterDotNet.Messages");
                    writer.Write("MeasurementType");
                    break;
                case "ref_OccupancyBitmap":
                    writer.Write("OccupancySensingCluster.OccupancyBitmap");
                    break;
                case "tod":
                case "date":
                    throw new NotImplementedException();
                case "ref_AdditionalInfoStruct":
                    writer.Write("ContentLauncherCluster.AdditionalInfo");
                    break;
                case "ref_CharacteristicEnum":
                    writer.Write("MediaPlaybackCluster.CharacteristicEnum");
                    break;
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

        private static string SanitizeDefault(string value, string? type, string? listType)
        {
            if (value == "\"")
                return "\"\"";
            if (value == "empty" || (type == "list" && value == "0"))
            {
                if (type == "list")
                {
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    sw.Write("Array.Empty<");
                    WriteType(listType!, null, sw);
                    sw.Write(">()");
                    return sb.ToString();
                }
                else if (type?.Equals("string", StringComparison.InvariantCultureIgnoreCase) == true)
                    return "\"\"";
                return "[]";
            }
            if (value.StartsWith("0x"))
                return value.Split(' ')[0];
            if (value.StartsWith('"'))
                return value;
            if (value == "0" && (type == "epoch-s" || type == "epoch-us" || type == "utc"))
                return "TimeUtil.EPOCH";
            if (type == "elapsed-s")
            {
                if (value == "null")
                    return value;
                if (value.EndsWith('s'))
                    value = value.Substring(0, value.Length - 1);
                return $"TimeSpan.FromSeconds({value})";
            }
            if (type == "bool")
                return (value == "1" || value.Equals("true", StringComparison.InvariantCultureIgnoreCase)) ? "true" : "false";
            if (type == "status" && value == "SUCCESS")
                return "IMStatusCode.SUCCESS";
            if (type == "TemperatureDifference" || type == "SignedTemperature" || type == "UnsignedTemperature" || type == "temperature")
            {
                if (value == "null")
                    return value;
                if (value.EndsWith("°C"))
                    value = value.Substring(0, value.Length - 2);
                double temp = double.Parse(value);
                if (type == "temperature" || type == "TemperatureDifference")
                    return ((int)(temp * 100)).ToString();
                else
                    return ((int)(temp * 10)).ToString();
            }
            if (type == "ref_OccupancyBitmap")
                return "(OccupancySensingCluster.OccupancyBitmap)" + value;
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
            return true;
        }
    }
}
