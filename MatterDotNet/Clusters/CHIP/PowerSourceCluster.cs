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
//
// WARNING: This file was auto-generated. Do not edit.

using MatterDotNet.Attributes;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.CHIP
{
    /// <summary>
    /// This cluster is used to describe the configuration and capabilities of a physical power source that provides power to the Node.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class PowerSource : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x002F;

        /// <summary>
        /// This cluster is used to describe the configuration and capabilities of a physical power source that provides power to the Node.
        /// </summary>
        [SetsRequiredMembers]
        public PowerSource(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected PowerSource(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            Status = new ReadAttribute<PowerSourceStatus>(cluster, endPoint, 0) {
                Deserialize = x => (PowerSourceStatus)DeserializeEnum(x)!
            };
            Order = new ReadAttribute<byte>(cluster, endPoint, 1) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            Description = new ReadAttribute<string>(cluster, endPoint, 2) {
                Deserialize = x => (string)(dynamic?)x!
            };
            WiredAssessedInputVoltage = new ReadAttribute<uint?>(cluster, endPoint, 3, true) {
                Deserialize = x => (uint?)(dynamic?)x
            };
            WiredAssessedInputFrequency = new ReadAttribute<ushort?>(cluster, endPoint, 4, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            WiredCurrentType = new ReadAttribute<WiredCurrentTypeEnum>(cluster, endPoint, 5) {
                Deserialize = x => (WiredCurrentTypeEnum)DeserializeEnum(x)!
            };
            WiredAssessedCurrent = new ReadAttribute<uint?>(cluster, endPoint, 6, true) {
                Deserialize = x => (uint?)(dynamic?)x
            };
            WiredNominalVoltage = new ReadAttribute<uint>(cluster, endPoint, 7) {
                Deserialize = x => (uint)(dynamic?)x!
            };
            WiredMaximumCurrent = new ReadAttribute<uint>(cluster, endPoint, 8) {
                Deserialize = x => (uint)(dynamic?)x!
            };
            WiredPresent = new ReadAttribute<bool>(cluster, endPoint, 9) {
                Deserialize = x => (bool)(dynamic?)x!
            };
            ActiveWiredFaults = new ReadAttribute<WiredFault[]>(cluster, endPoint, 10) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    WiredFault[] list = new WiredFault[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = (WiredFault)reader.GetUShort(i)!.Value;
                    return list;
                }
            };
            BatVoltage = new ReadAttribute<uint?>(cluster, endPoint, 11, true) {
                Deserialize = x => (uint?)(dynamic?)x
            };
            BatPercentRemaining = new ReadAttribute<byte?>(cluster, endPoint, 12, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            BatTimeRemaining = new ReadAttribute<uint?>(cluster, endPoint, 13, true) {
                Deserialize = x => (uint?)(dynamic?)x
            };
            BatChargeLevel = new ReadAttribute<BatChargeLevelEnum>(cluster, endPoint, 14) {
                Deserialize = x => (BatChargeLevelEnum)DeserializeEnum(x)!
            };
            BatReplacementNeeded = new ReadAttribute<bool>(cluster, endPoint, 15) {
                Deserialize = x => (bool)(dynamic?)x!
            };
            BatReplaceability = new ReadAttribute<BatReplaceabilityEnum>(cluster, endPoint, 16) {
                Deserialize = x => (BatReplaceabilityEnum)DeserializeEnum(x)!
            };
            BatPresent = new ReadAttribute<bool>(cluster, endPoint, 17) {
                Deserialize = x => (bool)(dynamic?)x!
            };
            ActiveBatFaults = new ReadAttribute<BatFault[]>(cluster, endPoint, 18) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    BatFault[] list = new BatFault[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = (BatFault)reader.GetUShort(i)!.Value;
                    return list;
                }
            };
            BatReplacementDescription = new ReadAttribute<string>(cluster, endPoint, 19) {
                Deserialize = x => (string)(dynamic?)x!
            };
            BatCommonDesignation = new ReadAttribute<BatCommonDesignationEnum>(cluster, endPoint, 20) {
                Deserialize = x => (BatCommonDesignationEnum)DeserializeEnum(x)!
            };
            BatANSIDesignation = new ReadAttribute<string>(cluster, endPoint, 21) {
                Deserialize = x => (string)(dynamic?)x!
            };
            BatIECDesignation = new ReadAttribute<string>(cluster, endPoint, 22) {
                Deserialize = x => (string)(dynamic?)x!
            };
            BatApprovedChemistry = new ReadAttribute<BatApprovedChemistryEnum>(cluster, endPoint, 23) {
                Deserialize = x => (BatApprovedChemistryEnum)DeserializeEnum(x)!
            };
            BatCapacity = new ReadAttribute<uint>(cluster, endPoint, 24) {
                Deserialize = x => (uint)(dynamic?)x!
            };
            BatQuantity = new ReadAttribute<byte>(cluster, endPoint, 25) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            BatChargeState = new ReadAttribute<BatChargeStateEnum>(cluster, endPoint, 26) {
                Deserialize = x => (BatChargeStateEnum)DeserializeEnum(x)!
            };
            BatTimeToFullCharge = new ReadAttribute<uint?>(cluster, endPoint, 27, true) {
                Deserialize = x => (uint?)(dynamic?)x
            };
            BatFunctionalWhileCharging = new ReadAttribute<bool>(cluster, endPoint, 28) {
                Deserialize = x => (bool)(dynamic?)x!
            };
            BatChargingCurrent = new ReadAttribute<uint?>(cluster, endPoint, 29, true) {
                Deserialize = x => (uint?)(dynamic?)x
            };
            ActiveBatChargeFaults = new ReadAttribute<BatChargeFault[]>(cluster, endPoint, 30) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    BatChargeFault[] list = new BatChargeFault[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = (BatChargeFault)reader.GetUShort(i)!.Value;
                    return list;
                }
            };
            EndpointList = new ReadAttribute<ushort[]>(cluster, endPoint, 31) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ushort[] list = new ushort[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = reader.GetUShort(i)!.Value;
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
        /// Wired Fault
        /// </summary>
        public enum WiredFault : byte {
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

        /// <summary>
        /// Bat Fault
        /// </summary>
        public enum BatFault : byte {
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
        /// Bat Charge Fault
        /// </summary>
        public enum BatChargeFault : byte {
            /// <summary>
            /// The Node detects an unspecified fault on this battery source.
            /// </summary>
            Unspecified = 0x0,
            /// <summary>
            /// The Node detects the ambient temperature is above the nominal range for this battery source.
            /// </summary>
            AmbientTooHot = 0x1,
            /// <summary>
            /// The Node detects the ambient temperature is below the nominal range for this battery source.
            /// </summary>
            AmbientTooCold = 0x2,
            /// <summary>
            /// The Node detects the temperature of this battery source is above the nominal range.
            /// </summary>
            BatteryTooHot = 0x3,
            /// <summary>
            /// The Node detects the temperature of this battery source is below the nominal range.
            /// </summary>
            BatteryTooCold = 0x4,
            /// <summary>
            /// The Node detects this battery source is not present.
            /// </summary>
            BatteryAbsent = 0x5,
            /// <summary>
            /// The Node detects this battery source is over voltage.
            /// </summary>
            BatteryOverVoltage = 0x6,
            /// <summary>
            /// The Node detects this battery source is under voltage.
            /// </summary>
            BatteryUnderVoltage = 0x7,
            /// <summary>
            /// The Node detects the charger for this battery source is over voltage.
            /// </summary>
            ChargerOverVoltage = 0x8,
            /// <summary>
            /// The Node detects the charger for this battery source is under voltage.
            /// </summary>
            ChargerUnderVoltage = 0x9,
            /// <summary>
            /// The Node detects a charging safety timeout for this battery source.
            /// </summary>
            SafetyTimeout = 0xA,
        }

        /// <summary>
        /// Power Source Status
        /// </summary>
        public enum PowerSourceStatus : byte {
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
        public enum WiredCurrentTypeEnum : byte {
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
        /// Bat Charge Level
        /// </summary>
        public enum BatChargeLevelEnum : byte {
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
        /// Bat Replaceability
        /// </summary>
        public enum BatReplaceabilityEnum : byte {
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
        /// Bat Charge State
        /// </summary>
        public enum BatChargeStateEnum : byte {
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
        public enum BatCommonDesignationEnum : ushort {
            /// <summary>
            /// Common type is unknown or unspecified
            /// </summary>
            Unspecified = 0x0,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            AAA = 0x1,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            AA = 0x2,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            C = 0x3,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            D = 0x4,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _4v5 = 0x5,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _6v0 = 0x6,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _9v0 = 0x7,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _1_2AA = 0x8,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            AAAA = 0x9,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A = 0xA,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            B = 0xB,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            F = 0xC,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            N = 0xD,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            No6 = 0xE,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SubC = 0xF,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A23 = 0x10,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A27 = 0x11,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            BA5800 = 0x12,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            Duplex = 0x13,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _4SR44 = 0x14,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _523 = 0x15,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _531 = 0x16,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _15v0 = 0x17,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _22v5 = 0x18,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _30v0 = 0x19,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _45v0 = 0x1A,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _67v5 = 0x1B,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            J = 0x1C,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            CR123A = 0x1D,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            CR2 = 0x1E,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _2CR5 = 0x1F,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            CR_P2 = 0x20,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            CR_V3 = 0x21,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR41 = 0x22,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR43 = 0x23,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR44 = 0x24,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR45 = 0x25,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR48 = 0x26,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR54 = 0x27,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR55 = 0x28,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR57 = 0x29,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR58 = 0x2A,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR59 = 0x2B,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR60 = 0x2C,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR63 = 0x2D,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR64 = 0x2E,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR65 = 0x2F,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR66 = 0x30,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR67 = 0x31,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR68 = 0x32,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR69 = 0x33,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR516 = 0x34,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR731 = 0x35,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            SR712 = 0x36,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            LR932 = 0x37,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A5 = 0x38,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A10 = 0x39,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A13 = 0x3A,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A312 = 0x3B,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            A675 = 0x3C,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            AC41E = 0x3D,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _10180 = 0x3E,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _10280 = 0x3F,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _10440 = 0x40,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _14250 = 0x41,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _14430 = 0x42,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _14500 = 0x43,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _14650 = 0x44,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _15270 = 0x45,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _16340 = 0x46,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            RCR123A = 0x47,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _17500 = 0x48,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _17670 = 0x49,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _18350 = 0x4A,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _18500 = 0x4B,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _18650 = 0x4C,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _19670 = 0x4D,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _25500 = 0x4E,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _26650 = 0x4F,
            /// <summary>
            /// Common type is as specified
            /// </summary>
            _32600 = 0x50,
        }

        /// <summary>
        /// Bat Approved Chemistry
        /// </summary>
        public enum BatApprovedChemistryEnum : ushort {
            /// <summary>
            /// Cell chemistry is unspecified or unknown
            /// </summary>
            Unspecified = 0x0,
            /// <summary>
            /// Cell chemistry is alkaline
            /// </summary>
            Alkaline = 0x1,
            /// <summary>
            /// Cell chemistry is lithium carbon fluoride
            /// </summary>
            LithiumCarbonFluoride = 0x2,
            /// <summary>
            /// Cell chemistry is lithium chromium oxide
            /// </summary>
            LithiumChromiumOxide = 0x3,
            /// <summary>
            /// Cell chemistry is lithium copper oxide
            /// </summary>
            LithiumCopperOxide = 0x4,
            /// <summary>
            /// Cell chemistry is lithium iron disulfide
            /// </summary>
            LithiumIronDisulfide = 0x5,
            /// <summary>
            /// Cell chemistry is lithium manganese dioxide
            /// </summary>
            LithiumManganeseDioxide = 0x6,
            /// <summary>
            /// Cell chemistry is lithium thionyl chloride
            /// </summary>
            LithiumThionylChloride = 0x7,
            /// <summary>
            /// Cell chemistry is magnesium
            /// </summary>
            Magnesium = 0x8,
            /// <summary>
            /// Cell chemistry is mercury oxide
            /// </summary>
            MercuryOxide = 0x9,
            /// <summary>
            /// Cell chemistry is nickel oxyhydride
            /// </summary>
            NickelOxyhydride = 0xA,
            /// <summary>
            /// Cell chemistry is silver oxide
            /// </summary>
            SilverOxide = 0xB,
            /// <summary>
            /// Cell chemistry is zinc air
            /// </summary>
            ZincAir = 0xC,
            /// <summary>
            /// Cell chemistry is zinc carbon
            /// </summary>
            ZincCarbon = 0xD,
            /// <summary>
            /// Cell chemistry is zinc chloride
            /// </summary>
            ZincChloride = 0xE,
            /// <summary>
            /// Cell chemistry is zinc manganese dioxide
            /// </summary>
            ZincManganeseDioxide = 0xF,
            /// <summary>
            /// Cell chemistry is lead acid
            /// </summary>
            LeadAcid = 0x10,
            /// <summary>
            /// Cell chemistry is lithium cobalt oxide
            /// </summary>
            LithiumCobaltOxide = 0x11,
            /// <summary>
            /// Cell chemistry is lithium ion
            /// </summary>
            LithiumIon = 0x12,
            /// <summary>
            /// Cell chemistry is lithium ion polymer
            /// </summary>
            LithiumIonPolymer = 0x13,
            /// <summary>
            /// Cell chemistry is lithium iron phosphate
            /// </summary>
            LithiumIronPhosphate = 0x14,
            /// <summary>
            /// Cell chemistry is lithium sulfur
            /// </summary>
            LithiumSulfur = 0x15,
            /// <summary>
            /// Cell chemistry is lithium titanate
            /// </summary>
            LithiumTitanate = 0x16,
            /// <summary>
            /// Cell chemistry is nickel cadmium
            /// </summary>
            NickelCadmium = 0x17,
            /// <summary>
            /// Cell chemistry is nickel hydrogen
            /// </summary>
            NickelHydrogen = 0x18,
            /// <summary>
            /// Cell chemistry is nickel iron
            /// </summary>
            NickelIron = 0x19,
            /// <summary>
            /// Cell chemistry is nickel metal hydride
            /// </summary>
            NickelMetalHydride = 0x1A,
            /// <summary>
            /// Cell chemistry is nickel zinc
            /// </summary>
            NickelZinc = 0x1B,
            /// <summary>
            /// Cell chemistry is silver zinc
            /// </summary>
            SilverZinc = 0x1C,
            /// <summary>
            /// Cell chemistry is sodium ion
            /// </summary>
            SodiumIon = 0x1D,
            /// <summary>
            /// Cell chemistry is sodium sulfur
            /// </summary>
            SodiumSulfur = 0x1E,
            /// <summary>
            /// Cell chemistry is zinc bromide
            /// </summary>
            ZincBromide = 0x1F,
            /// <summary>
            /// Cell chemistry is zinc cerium
            /// </summary>
            ZincCerium = 0x20,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Wired Fault Change Type
        /// </summary>
        public record WiredFaultChangeType : TLVPayload {
            /// <summary>
            /// Wired Fault Change Type
            /// </summary>
            public WiredFaultChangeType() { }

            /// <summary>
            /// Wired Fault Change Type
            /// </summary>
            [SetsRequiredMembers]
            public WiredFaultChangeType(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                {
                    Current = new WiredFault[reader.GetStruct(0)!.Length];
                    for (int n = 0; n < Current.Length; n++) {
                        Current[n] = (WiredFault)reader.GetUShort(n)!.Value;
                    }
                }
                {
                    Previous = new WiredFault[reader.GetStruct(1)!.Length];
                    for (int n = 0; n < Previous.Length; n++) {
                        Previous[n] = (WiredFault)reader.GetUShort(n)!.Value;
                    }
                }
            }
            public required WiredFault[] Current { get; set; }
            public required WiredFault[] Previous { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    Constrain(Current, 0, 8);
                    writer.StartArray(0);
                    foreach (var item in Current) {
                        writer.WriteUShort(-1, (ushort)item);
                    }
                    writer.EndContainer();
                }
                {
                    Constrain(Previous, 0, 8);
                    writer.StartArray(1);
                    foreach (var item in Previous) {
                        writer.WriteUShort(-1, (ushort)item);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Bat Fault Change Type
        /// </summary>
        public record BatFaultChangeType : TLVPayload {
            /// <summary>
            /// Bat Fault Change Type
            /// </summary>
            public BatFaultChangeType() { }

            /// <summary>
            /// Bat Fault Change Type
            /// </summary>
            [SetsRequiredMembers]
            public BatFaultChangeType(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                {
                    Current = new BatFault[reader.GetStruct(0)!.Length];
                    for (int n = 0; n < Current.Length; n++) {
                        Current[n] = (BatFault)reader.GetUShort(n)!.Value;
                    }
                }
                {
                    Previous = new BatFault[reader.GetStruct(1)!.Length];
                    for (int n = 0; n < Previous.Length; n++) {
                        Previous[n] = (BatFault)reader.GetUShort(n)!.Value;
                    }
                }
            }
            public required BatFault[] Current { get; set; }
            public required BatFault[] Previous { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    Constrain(Current, 0, 8);
                    writer.StartArray(0);
                    foreach (var item in Current) {
                        writer.WriteUShort(-1, (ushort)item);
                    }
                    writer.EndContainer();
                }
                {
                    Constrain(Previous, 0, 8);
                    writer.StartArray(1);
                    foreach (var item in Previous) {
                        writer.WriteUShort(-1, (ushort)item);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Bat Charge Fault Change Type
        /// </summary>
        public record BatChargeFaultChangeType : TLVPayload {
            /// <summary>
            /// Bat Charge Fault Change Type
            /// </summary>
            public BatChargeFaultChangeType() { }

            /// <summary>
            /// Bat Charge Fault Change Type
            /// </summary>
            [SetsRequiredMembers]
            public BatChargeFaultChangeType(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                {
                    Current = new BatChargeFault[reader.GetStruct(0)!.Length];
                    for (int n = 0; n < Current.Length; n++) {
                        Current[n] = (BatChargeFault)reader.GetUShort(n)!.Value;
                    }
                }
                {
                    Previous = new BatChargeFault[reader.GetStruct(1)!.Length];
                    for (int n = 0; n < Previous.Length; n++) {
                        Previous[n] = (BatChargeFault)reader.GetUShort(n)!.Value;
                    }
                }
            }
            public required BatChargeFault[] Current { get; set; }
            public required BatChargeFault[] Previous { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    Constrain(Current, 0, 16);
                    writer.StartArray(0);
                    foreach (var item in Current) {
                        writer.WriteUShort(-1, (ushort)item);
                    }
                    writer.EndContainer();
                }
                {
                    Constrain(Previous, 0, 16);
                    writer.StartArray(1);
                    foreach (var item in Previous) {
                        writer.WriteUShort(-1, (ushort)item);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }
        #endregion Records

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
        /// Status Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<PowerSourceStatus> Status { get; init; }

        /// <summary>
        /// Order Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> Order { get; init; }

        /// <summary>
        /// Description Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> Description { get; init; }

        /// <summary>
        /// Wired Assessed Input Voltage Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> WiredAssessedInputVoltage { get; init; }

        /// <summary>
        /// Wired Assessed Input Frequency Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort?> WiredAssessedInputFrequency { get; init; }

        /// <summary>
        /// Wired Current Type Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<WiredCurrentTypeEnum> WiredCurrentType { get; init; }

        /// <summary>
        /// Wired Assessed Current Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> WiredAssessedCurrent { get; init; }

        /// <summary>
        /// Wired Nominal Voltage Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> WiredNominalVoltage { get; init; }

        /// <summary>
        /// Wired Maximum Current Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> WiredMaximumCurrent { get; init; }

        /// <summary>
        /// Wired Present Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool> WiredPresent { get; init; }

        /// <summary>
        /// Active Wired Faults Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<WiredFault[]> ActiveWiredFaults { get; init; }

        /// <summary>
        /// Bat Voltage Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> BatVoltage { get; init; }

        /// <summary>
        /// Bat Percent Remaining Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> BatPercentRemaining { get; init; }

        /// <summary>
        /// Bat Time Remaining Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> BatTimeRemaining { get; init; }

        /// <summary>
        /// Bat Charge Level Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<BatChargeLevelEnum> BatChargeLevel { get; init; }

        /// <summary>
        /// Bat Replacement Needed Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool> BatReplacementNeeded { get; init; }

        /// <summary>
        /// Bat Replaceability Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<BatReplaceabilityEnum> BatReplaceability { get; init; }

        /// <summary>
        /// Bat Present Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool> BatPresent { get; init; }

        /// <summary>
        /// Active Bat Faults Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<BatFault[]> ActiveBatFaults { get; init; }

        /// <summary>
        /// Bat Replacement Description Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> BatReplacementDescription { get; init; }

        /// <summary>
        /// Bat Common Designation Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<BatCommonDesignationEnum> BatCommonDesignation { get; init; }

        /// <summary>
        /// Bat ANSI Designation Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> BatANSIDesignation { get; init; }

        /// <summary>
        /// Bat IEC Designation Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> BatIECDesignation { get; init; }

        /// <summary>
        /// Bat Approved Chemistry Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<BatApprovedChemistryEnum> BatApprovedChemistry { get; init; }

        /// <summary>
        /// Bat Capacity Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> BatCapacity { get; init; }

        /// <summary>
        /// Bat Quantity Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> BatQuantity { get; init; }

        /// <summary>
        /// Bat Charge State Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<BatChargeStateEnum> BatChargeState { get; init; }

        /// <summary>
        /// Bat Time To Full Charge Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> BatTimeToFullCharge { get; init; }

        /// <summary>
        /// Bat Functional While Charging Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool> BatFunctionalWhileCharging { get; init; }

        /// <summary>
        /// Bat Charging Current Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> BatChargingCurrent { get; init; }

        /// <summary>
        /// Active Bat Charge Faults Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<BatChargeFault[]> ActiveBatChargeFaults { get; init; }

        /// <summary>
        /// Endpoint List Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort[]> EndpointList { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Power Source";
        }
    }
}