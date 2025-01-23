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
using System.Diagnostics.Metrics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Generator
{
    public class CommentGenerator
    {

        public static void Generate()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Cluster));
            IEnumerable<string> clusterxmls = Directory.EnumerateFiles("..\\..\\..\\CommentSrc");
            StreamWriter output = new StreamWriter("outputs\\comments.xml");
            output.WriteLine("<?xml version=\"1.0\"?>\n<comments>");
            foreach (string clusterxml in clusterxmls)
            {
                if (clusterxml.EndsWith(".xml"))
                {
                    Cluster? cluster = deserializer.Deserialize(File.OpenRead(clusterxml)) as Cluster;
                    if (cluster == null)
                        throw new IOException("Failed to parse cluster " + clusterxml);
                    if (cluster.dataTypes?.bitmap != null)
                    {
                        foreach (var bitmap in cluster.dataTypes.bitmap)
                        {
                            output.WriteLine($"  <bitmap name=\"{cluster.clusterIds.clusterId.id}.{bitmap.name.Replace("Bitmap", "")}\">");
                            bool bitmap16 = bitmap.bitfield.Any(b => b.bit > 7);
                            foreach (var item in bitmap.bitfield)
                            {
                                int value = 1 << item.bit;
                                if (item.to != null && item.from != null)
                                {
                                    value = 0;
                                    int from = Convert.ToInt32(item.from, 16);
                                    int to = Convert.ToInt32(item.to, 16);
                                    for (int i = Math.Min(from, to); i <= Math.Max(from, to); i++)
                                        value |= (1 << i);
                                }
                                string strVal = bitmap16 ? $"0x{value:X4}" : $"0x{value:X2}";
                                output.WriteLine($"    <item value=\"{strVal}\" comment=\"{GeneratorUtil.SanitizeComment(item.summary)?.Replace("\"", "&quot;").Replace(">", "&gt;").Replace("<", "&lt;")}\" />");
                            }
                            output.WriteLine($"  </bitmap>");
                        }
                    }
                    if (cluster.dataTypes?.@enum != null)
                    {
                        foreach (var @enum in cluster.dataTypes.@enum)
                        {
                            if (@enum.item != null)
                            {
                                output.WriteLine($"  <enum name=\"{cluster.clusterIds.clusterId.id}.{@enum.name.Replace("Enum", "")}\">");
                                foreach (var item in @enum.item)
                                {
                                    if (item.value != null)
                                    {
                                        string value = item.value;
                                        if (!value.StartsWith("0x"))
                                            value = "0x" + int.Parse(value).ToString("x2");
                                        output.WriteLine($"    <item value=\"{item.value}\" comment=\"{GeneratorUtil.SanitizeComment(item.summary)?.Replace("\"", "&quot;").Replace(">", "&gt;").Replace("<", "&lt;")}\" />");
                                    }
                                }
                                output.WriteLine($"  </enum>");
                            }
                        }
                    }
                }
            }
            output.WriteLine("</comments>");
            output.Flush();
            output.Close();
        }
    }
}
