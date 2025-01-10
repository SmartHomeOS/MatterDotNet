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
    /// Power Source Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class PowerSourceCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x002F;

        /// <summary>
        /// Power Source Cluster
        /// </summary>
        public PowerSourceCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected PowerSourceCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// A wired power source
            /// </summary>
            Wired = 1,
            /// <summary>
            /// A battery power source
            /// </summary>
            Battery = 2,
            /// <summary>
            /// A rechargeable battery power source
            /// </summary>
            Rechargeable = 4,
            /// <summary>
            /// A replaceable battery power source
            /// </summary>
            Replaceable = 8,
        }

        /// <summary>
        /// Bat Approved Chemistry
        /// </summary>
        public enum BatApprovedChemistryEnum {
            /// <summary>
            /// Cell chemistry is unspecified or unknown
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// Cell chemistry is alkaline
            /// </summary>
            Alkaline = 1,
            /// <summary>
            /// Cell chemistry is lithium carbon fluoride
            /// </summary>
            LithiumCarbonFluoride = 2,
            /// <summary>
            /// Cell chemistry is lithium chromium oxide
            /// </summary>
            LithiumChromiumOxide = 3,
            /// <summary>
            /// Cell chemistry is lithium copper oxide
            /// </summary>
            LithiumCopperOxide = 4,
            /// <summary>
            /// Cell chemistry is lithium iron disulfide
            /// </summary>
            LithiumIronDisulfide = 5,
            /// <summary>
            /// Cell chemistry is lithium manganese dioxide
            /// </summary>
            LithiumManganeseDioxide = 6,
            /// <summary>
            /// Cell chemistry is lithium thionyl chloride
            /// </summary>
            LithiumThionylChloride = 7,
            /// <summary>
            /// Cell chemistry is magnesium
            /// </summary>
            Magnesium = 8,
            /// <summary>
            /// Cell chemistry is mercury oxide
            /// </summary>
            MercuryOxide = 9,
            /// <summary>
            /// Cell chemistry is nickel oxyhydride
            /// </summary>
            NickelOxyhydride = 10,
            /// <summary>
            /// Cell chemistry is silver oxide
            /// </summary>
            SilverOxide = 11,
            /// <summary>
            /// Cell chemistry is zinc air
            /// </summary>
            ZincAir = 12,
            /// <summary>
            /// Cell chemistry is zinc carbon
            /// </summary>
            ZincCarbon = 13,
            /// <summary>
            /// Cell chemistry is zinc chloride
            /// </summary>
            ZincChloride = 14,
            /// <summary>
            /// Cell chemistry is zinc manganese dioxide
            /// </summary>
            ZincManganeseDioxide = 15,
            /// <summary>
            /// Cell chemistry is lead acid
            /// </summary>
            LeadAcid = 16,
            /// <summary>
            /// Cell chemistry is lithium cobalt oxide
            /// </summary>
            LithiumCobaltOxide = 17,
            /// <summary>
            /// Cell chemistry is lithium ion
            /// </summary>
            LithiumIon = 18,
            /// <summary>
            /// Cell chemistry is lithium ion polymer
            /// </summary>
            LithiumIonPolymer = 19,
            /// <summary>
            /// Cell chemistry is lithium iron phosphate
            /// </summary>
            LithiumIronPhosphate = 20,
            /// <summary>
            /// Cell chemistry is lithium sulfur
            /// </summary>
            LithiumSulfur = 21,
            /// <summary>
            /// Cell chemistry is lithium titanate
            /// </summary>
            LithiumTitanate = 22,
            /// <summary>
            /// Cell chemistry is nickel cadmium
            /// </summary>
            NickelCadmium = 23,
            /// <summary>
            /// Cell chemistry is nickel hydrogen
            /// </summary>
            NickelHydrogen = 24,
            /// <summary>
            /// Cell chemistry is nickel iron
            /// </summary>
            NickelIron = 25,
            /// <summary>
            /// Cell chemistry is nickel metal hydride
            /// </summary>
            NickelMetalHydride = 26,
            /// <summary>
            /// Cell chemistry is nickel zinc
            /// </summary>
            NickelZinc = 27,
            /// <summary>
            /// Cell chemistry is silver zinc
            /// </summary>
            SilverZinc = 28,
            /// <summary>
            /// Cell chemistry is sodium ion
            /// </summary>
            SodiumIon = 29,
            /// <summary>
            /// Cell chemistry is sodium sulfur
            /// </summary>
            SodiumSulfur = 30,
            /// <summary>
            /// Cell chemistry is zinc bromide
            /// </summary>
            ZincBromide = 31,
            /// <summary>
            /// Cell chemistry is zinc cerium
            /// </summary>
            ZincCerium = 32,
        }

        /// <summary>
        /// Bat Charge Fault
        /// </summary>
        public enum BatChargeFaultEnum {
            /// <summary>
            /// The Node detects an unspecified fault on this battery source.
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// The Node detects the ambient temperature is above the nominal range for this battery source.
            /// </summary>
            AmbientTooHot = 1,
            /// <summary>
            /// The Node detects the ambient temperature is below the nominal range for this battery source.
            /// </summary>
            AmbientTooCold = 2,
            /// <summary>
            /// The Node detects the temperature of this battery source is above the nominal range.
            /// </summary>
            BatteryTooHot = 3,
            /// <summary>
            /// The Node detects the temperature of this battery source is below the nominal range.
            /// </summary>
            BatteryTooCold = 4,
            /// <summary>
            /// The Node detects this battery source is not present.
            /// </summary>
            BatteryAbsent = 5,
            /// <summary>
            /// The Node detects this battery source is over voltage.
            /// </summary>
            BatteryOverVoltage = 6,
            /// <summary>
            /// The Node detects this battery source is under voltage.
            /// </summary>
            BatteryUnderVoltage = 7,
            /// <summary>
            /// The Node detects the charger for this battery source is over voltage.
            /// </summary>
            ChargerOverVoltage = 8,
            /// <summary>
            /// The Node detects the charger for this battery source is under voltage.
            /// </summary>
            ChargerUnderVoltage = 9,
            /// <summary>
            /// The Node detects a charging safety timeout for this battery source.
            /// </summary>
            SafetyTimeout = 10,
        }

        /// <summary>
        /// Bat Charge Level
        /// </summary>
        public enum BatChargeLevelEnum {
            /// <summary>
            /// Charge level is nominal
            /// </summary>
            OK = 0,
            /// <summary>
            /// Charge level is low, intervention may soon be required.
            /// </summary>
            Warning = 1,
            /// <summary>
            /// Charge level is critical, immediate intervention is required
            /// </summary>
            Critical = 2,
        }

        /// <summary>
        /// Bat Charge State
        /// </summary>
        public enum BatChargeStateEnum {
            /// <summary>
            /// Unable to determine the charging state
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// The battery is charging
            /// </summary>
            IsCharging = 1,
            /// <summary>
            /// The battery is at full charge
            /// </summary>
            IsAtFullCharge = 2,
            /// <summary>
            /// The battery is not charging
            /// </summary>
            IsNotCharging = 3,
        }

        /// <summary>
        /// Bat Common Designation
        /// </summary>
        public enum BatCommonDesignationEnum {
            /// <summary>
            /// Common type is unknown or unspecified
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            AAA = 1,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            AA = 2,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            C = 3,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            D = 4,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _4v5 = 5,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _6v0 = 6,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _9v0 = 7,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _1_2AA = 8,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            AAAA = 9,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A = 10,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            B = 11,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            F = 12,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            N = 13,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            No6 = 14,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SubC = 15,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A23 = 16,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A27 = 17,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            BA5800 = 18,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            Duplex = 19,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _4SR44 = 20,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _523 = 21,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _531 = 22,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _15v0 = 23,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _22v5 = 24,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _30v0 = 25,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _45v0 = 26,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _67v5 = 27,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            J = 28,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            CR123A = 29,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            CR2 = 30,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _2CR5 = 31,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            CR_P2 = 32,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            CR_V3 = 33,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR41 = 34,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR43 = 35,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR44 = 36,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR45 = 37,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR48 = 38,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR54 = 39,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR55 = 40,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR57 = 41,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR58 = 42,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR59 = 43,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR60 = 44,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR63 = 45,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR64 = 46,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR65 = 47,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR66 = 48,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR67 = 49,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR68 = 50,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR69 = 51,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR516 = 52,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR731 = 53,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR712 = 54,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            LR932 = 55,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A5 = 56,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A10 = 57,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A13 = 58,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A312 = 59,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A675 = 60,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            AC41E = 61,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _10180 = 62,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _10280 = 63,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _10440 = 64,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _14250 = 65,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _14430 = 66,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _14500 = 67,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _14650 = 68,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _15270 = 69,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _16340 = 70,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            RCR123A = 71,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _17500 = 72,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _17670 = 73,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _18350 = 74,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _18500 = 75,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _18650 = 76,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _19670 = 77,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _25500 = 78,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _26650 = 79,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _32600 = 80,
        }

        /// <summary>
        /// Bat Fault
        /// </summary>
        public enum BatFaultEnum {
            /// <summary>
            /// The Node detects an unspecified fault on this battery power source.
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// The Node detects the temperature of this battery power source is above ideal operating conditions.
            /// </summary>
            OverTemp = 1,
            /// <summary>
            /// The Node detects the temperature of this battery power source is below ideal operating conditions.
            /// </summary>
            UnderTemp = 2,
        }

        /// <summary>
        /// Bat Replaceability
        /// </summary>
        public enum BatReplaceabilityEnum {
            /// <summary>
            /// The replaceability is unspecified or unknown.
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// The battery is not replaceable.
            /// </summary>
            NotReplaceable = 1,
            /// <summary>
            /// The battery is replaceable by the user or customer.
            /// </summary>
            UserReplaceable = 2,
            /// <summary>
            /// The battery is replaceable by an authorized factory technician.
            /// </summary>
            FactoryReplaceable = 3,
        }

        /// <summary>
        /// Power Source Status
        /// </summary>
        public enum PowerSourceStatusEnum {
            /// <summary>
            /// Indicate the source status is not specified
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// Indicate the source is available and currently supplying power
            /// </summary>
            Active = 1,
            /// <summary>
            /// Indicate the source is available, but is not currently supplying power
            /// </summary>
            Standby = 2,
            /// <summary>
            /// Indicate the source is not currently available to supply power
            /// </summary>
            Unavailable = 3,
        }

        /// <summary>
        /// Wired Current Type
        /// </summary>
        public enum WiredCurrentTypeEnum {
            /// <summary>
            /// Indicates AC current
            /// </summary>
            AC = 0,
            /// <summary>
            /// Indicates DC current
            /// </summary>
            DC = 1,
        }

        /// <summary>
        /// Wired Fault
        /// </summary>
        public enum WiredFaultEnum {
            /// <summary>
            /// The Node detects an unspecified fault on this wired power source.
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// The Node detects the supplied voltage is above maximum supported value for this wired power source.
            /// </summary>
            OverVoltage = 1,
            /// <summary>
            /// The Node detects the supplied voltage is below maximum supported value for this wired power source.
            /// </summary>
            UnderVoltage = 2,
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
        /// Get the Status attribute
        /// </summary>
        public async Task<PowerSourceStatusEnum> GetStatus(SecureSession session) {
            return (PowerSourceStatusEnum)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Get the Order attribute
        /// </summary>
        public async Task<byte> GetOrder(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 1))!;
        }

        /// <summary>
        /// Get the Description attribute
        /// </summary>
        public async Task<string> GetDescription(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 2))!;
        }

        /// <summary>
        /// Get the Wired Assessed Input Voltage attribute
        /// </summary>
        public async Task<uint?> GetWiredAssessedInputVoltage(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 3, true);
        }

        /// <summary>
        /// Get the Wired Assessed Input Frequency attribute
        /// </summary>
        public async Task<ushort?> GetWiredAssessedInputFrequency(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 4, true);
        }

        /// <summary>
        /// Get the Wired Current Type attribute
        /// </summary>
        public async Task<WiredCurrentTypeEnum> GetWiredCurrentType(SecureSession session) {
            return (WiredCurrentTypeEnum)await GetEnumAttribute(session, 5);
        }

        /// <summary>
        /// Get the Wired Assessed Current attribute
        /// </summary>
        public async Task<uint?> GetWiredAssessedCurrent(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 6, true);
        }

        /// <summary>
        /// Get the Wired Nominal Voltage attribute
        /// </summary>
        public async Task<uint> GetWiredNominalVoltage(SecureSession session) {
            return (uint)(dynamic?)(await GetAttribute(session, 7))!;
        }

        /// <summary>
        /// Get the Wired Maximum Current attribute
        /// </summary>
        public async Task<uint> GetWiredMaximumCurrent(SecureSession session) {
            return (uint)(dynamic?)(await GetAttribute(session, 8))!;
        }

        /// <summary>
        /// Get the Wired Present attribute
        /// </summary>
        public async Task<bool> GetWiredPresent(SecureSession session) {
            return (bool)(dynamic?)(await GetAttribute(session, 9))!;
        }

        /// <summary>
        /// Get the Active Wired Faults attribute
        /// </summary>
        public async Task<WiredFaultEnum[]> GetActiveWiredFaults(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 10))!);
            WiredFaultEnum[] list = new WiredFaultEnum[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = (WiredFaultEnum)reader.GetUShort(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Bat Voltage attribute
        /// </summary>
        public async Task<uint?> GetBatVoltage(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 11, true);
        }

        /// <summary>
        /// Get the Bat Percent Remaining attribute
        /// </summary>
        public async Task<byte?> GetBatPercentRemaining(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 12, true);
        }

        /// <summary>
        /// Get the Bat Time Remaining attribute
        /// </summary>
        public async Task<uint?> GetBatTimeRemaining(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 13, true);
        }

        /// <summary>
        /// Get the Bat Charge Level attribute
        /// </summary>
        public async Task<BatChargeLevelEnum> GetBatChargeLevel(SecureSession session) {
            return (BatChargeLevelEnum)await GetEnumAttribute(session, 14);
        }

        /// <summary>
        /// Get the Bat Replacement Needed attribute
        /// </summary>
        public async Task<bool> GetBatReplacementNeeded(SecureSession session) {
            return (bool)(dynamic?)(await GetAttribute(session, 15))!;
        }

        /// <summary>
        /// Get the Bat Replaceability attribute
        /// </summary>
        public async Task<BatReplaceabilityEnum> GetBatReplaceability(SecureSession session) {
            return (BatReplaceabilityEnum)await GetEnumAttribute(session, 16);
        }

        /// <summary>
        /// Get the Bat Present attribute
        /// </summary>
        public async Task<bool> GetBatPresent(SecureSession session) {
            return (bool)(dynamic?)(await GetAttribute(session, 17))!;
        }

        /// <summary>
        /// Get the Active Bat Faults attribute
        /// </summary>
        public async Task<BatFaultEnum[]> GetActiveBatFaults(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 18))!);
            BatFaultEnum[] list = new BatFaultEnum[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = (BatFaultEnum)reader.GetUShort(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Bat Replacement Description attribute
        /// </summary>
        public async Task<string> GetBatReplacementDescription(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 19))!;
        }

        /// <summary>
        /// Get the Bat Common Designation attribute
        /// </summary>
        public async Task<BatCommonDesignationEnum> GetBatCommonDesignation(SecureSession session) {
            return (BatCommonDesignationEnum)await GetEnumAttribute(session, 20);
        }

        /// <summary>
        /// Get the Bat ANSI Designation attribute
        /// </summary>
        public async Task<string> GetBatANSIDesignation(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 21))!;
        }

        /// <summary>
        /// Get the Bat IEC Designation attribute
        /// </summary>
        public async Task<string> GetBatIECDesignation(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 22))!;
        }

        /// <summary>
        /// Get the Bat Approved Chemistry attribute
        /// </summary>
        public async Task<BatApprovedChemistryEnum> GetBatApprovedChemistry(SecureSession session) {
            return (BatApprovedChemistryEnum)await GetEnumAttribute(session, 23);
        }

        /// <summary>
        /// Get the Bat Capacity attribute
        /// </summary>
        public async Task<uint> GetBatCapacity(SecureSession session) {
            return (uint)(dynamic?)(await GetAttribute(session, 24))!;
        }

        /// <summary>
        /// Get the Bat Quantity attribute
        /// </summary>
        public async Task<byte> GetBatQuantity(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 25))!;
        }

        /// <summary>
        /// Get the Bat Charge State attribute
        /// </summary>
        public async Task<BatChargeStateEnum> GetBatChargeState(SecureSession session) {
            return (BatChargeStateEnum)await GetEnumAttribute(session, 26);
        }

        /// <summary>
        /// Get the Bat Time To Full Charge attribute
        /// </summary>
        public async Task<uint?> GetBatTimeToFullCharge(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 27, true);
        }

        /// <summary>
        /// Get the Bat Functional While Charging attribute
        /// </summary>
        public async Task<bool> GetBatFunctionalWhileCharging(SecureSession session) {
            return (bool)(dynamic?)(await GetAttribute(session, 28))!;
        }

        /// <summary>
        /// Get the Bat Charging Current attribute
        /// </summary>
        public async Task<uint?> GetBatChargingCurrent(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 29, true);
        }

        /// <summary>
        /// Get the Active Bat Charge Faults attribute
        /// </summary>
        public async Task<BatChargeFaultEnum[]> GetActiveBatChargeFaults(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 30))!);
            BatChargeFaultEnum[] list = new BatChargeFaultEnum[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = (BatChargeFaultEnum)reader.GetUShort(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Endpoint List attribute
        /// </summary>
        public async Task<ushort[]> GetEndpointList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 31))!);
            ushort[] list = new ushort[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetUShort(i)!.Value;
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Power Source Cluster";
        }
    }
}