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

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Time Format Localization Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class TimeFormatLocalizationCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x002C;

        /// <summary>
        /// Time Format Localization Cluster
        /// </summary>
        public TimeFormatLocalizationCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected TimeFormatLocalizationCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        /// Calendar Type
        /// </summary>
        public enum CalendarTypeEnum {
            /// <summary>
            /// Dates conveyed using the Buddhist calendar
            /// </summary>
            Buddhist = 0,
            /// <summary>
            /// Dates conveyed using the Chinese calendar
            /// </summary>
            Chinese = 1,
            /// <summary>
            /// Dates conveyed using the Coptic calendar
            /// </summary>
            Coptic = 2,
            /// <summary>
            /// Dates conveyed using the Ethiopian calendar
            /// </summary>
            Ethiopian = 3,
            /// <summary>
            /// Dates conveyed using the Gregorian calendar
            /// </summary>
            Gregorian = 4,
            /// <summary>
            /// Dates conveyed using the Hebrew calendar
            /// </summary>
            Hebrew = 5,
            /// <summary>
            /// Dates conveyed using the Indian calendar
            /// </summary>
            Indian = 6,
            /// <summary>
            /// Dates conveyed using the Islamic calendar
            /// </summary>
            Islamic = 7,
            /// <summary>
            /// Dates conveyed using the Japanese calendar
            /// </summary>
            Japanese = 8,
            /// <summary>
            /// Dates conveyed using the Korean calendar
            /// </summary>
            Korean = 9,
            /// <summary>
            /// Dates conveyed using the Persian calendar
            /// </summary>
            Persian = 10,
            /// <summary>
            /// Dates conveyed using the Taiwanese calendar
            /// </summary>
            Taiwanese = 11,
            /// <summary>
            /// calendar implied from active locale
            /// </summary>
            UseActiveLocale = 255,
        }

        /// <summary>
        /// Hour Format
        /// </summary>
        public enum HourFormatEnum {
            /// <summary>
            /// Time conveyed with a 12-hour clock
            /// </summary>
            _12hr = 0,
            /// <summary>
            /// Time conveyed with a 24-hour clock
            /// </summary>
            _24hr = 1,
            /// <summary>
            /// Use active locale clock
            /// </summary>
            UseActiveLocale = 255,
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
        /// Get the Hour Format attribute
        /// </summary>
        public async Task<HourFormatEnum> GetHourFormat(SecureSession session) {
            return (HourFormatEnum)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Set the Hour Format attribute
        /// </summary>
        public async Task SetHourFormat (SecureSession session, HourFormatEnum value) {
            await SetAttribute(session, 0, value);
        }

        /// <summary>
        /// Get the Active Calendar Type attribute
        /// </summary>
        public async Task<CalendarTypeEnum> GetActiveCalendarType(SecureSession session) {
            return (CalendarTypeEnum)await GetEnumAttribute(session, 1);
        }

        /// <summary>
        /// Set the Active Calendar Type attribute
        /// </summary>
        public async Task SetActiveCalendarType (SecureSession session, CalendarTypeEnum value) {
            await SetAttribute(session, 1, value);
        }

        /// <summary>
        /// Get the Supported Calendar Types attribute
        /// </summary>
        public async Task<List<CalendarTypeEnum>> GetSupportedCalendarTypes(SecureSession session) {
            List<CalendarTypeEnum> list = new List<CalendarTypeEnum>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 2))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add((CalendarTypeEnum)reader.GetUShort(i)!.Value);
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Time Format Localization Cluster";
        }
    }
}