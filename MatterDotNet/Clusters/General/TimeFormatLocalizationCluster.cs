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
//
// WARNING: This file was auto-generated. Do not edit.

using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// Nodes should be expected to be deployed to any and all regions of the world. These global regions may have differing preferences for how dates and times are conveyed. As such, Nodes that visually or audibly convey time information need a mechanism by which they can be configured to use a user’s preferred format.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class TimeFormatLocalization : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x002c;

        /// <summary>
        /// Nodes should be expected to be deployed to any and all regions of the world. These global regions may have differing preferences for how dates and times are conveyed. As such, Nodes that visually or audibly convey time information need a mechanism by which they can be configured to use a user’s preferred format.
        /// </summary>
        [SetsRequiredMembers]
        public TimeFormatLocalization(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected TimeFormatLocalization(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            HourFormat = new ReadWriteAttribute<HourFormatEnum>(cluster, endPoint, 0) {
                Deserialize = x => (HourFormatEnum)DeserializeEnum(x)!
            };
            ActiveCalendarType = new ReadWriteAttribute<CalendarType>(cluster, endPoint, 1) {
                Deserialize = x => (CalendarType)DeserializeEnum(x)!
            };
            SupportedCalendarTypes = new ReadAttribute<CalendarType[]>(cluster, endPoint, 2) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    CalendarType[] list = new CalendarType[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = (CalendarType)reader.GetUShort(i)!.Value;
                    return list;
                }
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// The Node can be configured to use different calendar formats when conveying values to a user.
            /// </summary>
            CalendarFormat = 1,
        }

        /// <summary>
        /// Hour Format
        /// </summary>
        public enum HourFormatEnum : byte {
            /// <summary>
            /// Time conveyed with a 12-hour clock
            /// </summary>
            _12hr = 0x0,
            /// <summary>
            /// Time conveyed with a 24-hour clock
            /// </summary>
            _24hr = 0x1,
            /// <summary>
            /// Use active locale clock
            /// </summary>
            UseActiveLocale = 0xFF,
        }

        /// <summary>
        /// Calendar Type
        /// </summary>
        public enum CalendarType : byte {
            /// <summary>
            /// Dates conveyed using the Buddhist calendar
            /// </summary>
            Buddhist = 0x0,
            /// <summary>
            /// Dates conveyed using the Chinese calendar
            /// </summary>
            Chinese = 0x1,
            /// <summary>
            /// Dates conveyed using the Coptic calendar
            /// </summary>
            Coptic = 0x2,
            /// <summary>
            /// Dates conveyed using the Ethiopian calendar
            /// </summary>
            Ethiopian = 0x3,
            /// <summary>
            /// Dates conveyed using the Gregorian calendar
            /// </summary>
            Gregorian = 0x4,
            /// <summary>
            /// Dates conveyed using the Hebrew calendar
            /// </summary>
            Hebrew = 0x5,
            /// <summary>
            /// Dates conveyed using the Indian calendar
            /// </summary>
            Indian = 0x6,
            /// <summary>
            /// Dates conveyed using the Islamic calendar
            /// </summary>
            Islamic = 0x7,
            /// <summary>
            /// Dates conveyed using the Japanese calendar
            /// </summary>
            Japanese = 0x8,
            /// <summary>
            /// Dates conveyed using the Korean calendar
            /// </summary>
            Korean = 0x9,
            /// <summary>
            /// Dates conveyed using the Persian calendar
            /// </summary>
            Persian = 0xA,
            /// <summary>
            /// Dates conveyed using the Taiwanese calendar
            /// </summary>
            Taiwanese = 0xB,
            /// <summary>
            /// calendar implied from active locale
            /// </summary>
            UseActiveLocale = 0xFF,
        }
        #endregion Enums

        #region Attributes
        /// <summary>
        /// Features supported by this cluster
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task<Feature> GetSupportedFeatures(SecureSession session)
        {
            return (Feature)(byte)(await GetAttribute(session, 0xFFFC))!;
        }

        /// <summary>
        /// Returns true when the feature is supported by the cluster
        /// </summary>
        /// <param name="session"></param>
        /// <param name="feature"></param>
        /// <returns></returns>
        public async Task<bool> Supports(SecureSession session, Feature feature)
        {
            return ((feature & await GetSupportedFeatures(session)) != 0);
        }

        /// <summary>
        /// Hour Format Attribute
        /// </summary>
        public required ReadWriteAttribute<HourFormatEnum> HourFormat { get; init; }

        /// <summary>
        /// Active Calendar Type Attribute
        /// </summary>
        public required ReadWriteAttribute<CalendarType> ActiveCalendarType { get; init; }

        /// <summary>
        /// Supported Calendar Types Attribute
        /// </summary>
        public required ReadAttribute<CalendarType[]> SupportedCalendarTypes { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Time Format Localization";
        }
    }
}