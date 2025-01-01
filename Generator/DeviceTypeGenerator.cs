﻿// MatterDotNet Copyright (C) 2025
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
    public class DeviceTypeGenerator
    {
        private static HashSet<string> includes = new HashSet<string>();
        public static void Generate()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(DeviceType));
            IEnumerable<string> deviceTypeXMLs = Directory.EnumerateFiles("..\\..\\..\\DeviceTypes");
            StreamWriter deviceTypeEnum = new StreamWriter("outputs\\DeviceType.cs");
            deviceTypeEnum.NewLine = "\n";
            deviceTypeEnum.WriteLine("// MatterDotNet Copyright (C) 2025 \n//\n// This program is free software: you can redistribute it and/or modify\n// it under the terms of the GNU Affero General Public License as published by\n// the Free Software Foundation, either version 3 of the License, or any later version.\n// This program is distributed in the hope that it will be useful,\n// but WITHOUT ANY WARRANTY, without even the implied warranty of\n// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.\n// See the GNU Affero General Public License for more details.\n// You should have received a copy of the GNU Affero General Public License\n// along with this program.  If not, see <http://www.gnu.org/licenses/>.\n//\n// WARNING: This file was auto-generated. Do not edit.\n");
            deviceTypeEnum.WriteLine("namespace MatterDotNet\n{\n    /// <summary>\n    /// Matter Device Type\n    /// </summary>\n    public enum DeviceTypeEnum : uint\n    {");
            foreach (string deviceTypeXML in deviceTypeXMLs)
            {
                includes.Clear();
                if (deviceTypeXML.EndsWith(".xml"))
                {
                    Console.WriteLine("Generating " + deviceTypeXML + "...");
                    DeviceType? deviceTypeObject = deserializer.Deserialize(File.OpenRead(deviceTypeXML)) as DeviceType;
                    if (deviceTypeObject == null)
                        throw new IOException("Failed to parse device type " + deviceTypeXML);
                    if (!string.IsNullOrEmpty(deviceTypeObject.id))
                    {
                        deviceTypeEnum.WriteLine($"        /// <summary>\n        /// {deviceTypeObject.name} Type\n        /// </summary>");
                        deviceTypeEnum.WriteLine($"        {deviceTypeObject.name.Replace(" ", "").Replace('/', '_').Replace("-", "")} = {deviceTypeObject.id},");
                    }
                }
            }
            deviceTypeEnum.WriteLine("    }\n}");
            deviceTypeEnum.Close();
        }
    }
}