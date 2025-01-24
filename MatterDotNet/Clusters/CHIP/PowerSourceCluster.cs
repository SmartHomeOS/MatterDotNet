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
        public PowerSource(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected PowerSource(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum WiredCurrentType : byte {
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
        public enum BatChargeLevel : byte {
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
        public enum BatReplaceability : byte {
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
        public enum BatChargeState : byte {
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
        public enum BatCommonDesignation : ushort {
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
        public enum BatApprovedChemistry : ushort {
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
        /// Get the Status attribute
        /// </summary>
        public async Task<PowerSourceStatus> GetStatus(SecureSession session) {
            return (PowerSourceStatus)await GetEnumAttribute(session, 0);
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
        public async Task<WiredCurrentType> GetWiredCurrentType(SecureSession session) {
            return (WiredCurrentType)await GetEnumAttribute(session, 5);
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
        public async Task<WiredFault[]> GetActiveWiredFaults(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 10))!);
            WiredFault[] list = new WiredFault[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = (WiredFault)reader.GetUShort(i)!.Value;
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
        public async Task<BatChargeLevel> GetBatChargeLevel(SecureSession session) {
            return (BatChargeLevel)await GetEnumAttribute(session, 14);
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
        public async Task<BatReplaceability> GetBatReplaceability(SecureSession session) {
            return (BatReplaceability)await GetEnumAttribute(session, 16);
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
        public async Task<BatFault[]> GetActiveBatFaults(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 18))!);
            BatFault[] list = new BatFault[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = (BatFault)reader.GetUShort(i)!.Value;
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
        public async Task<BatCommonDesignation> GetBatCommonDesignation(SecureSession session) {
            return (BatCommonDesignation)await GetEnumAttribute(session, 20);
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
        public async Task<BatApprovedChemistry> GetBatApprovedChemistry(SecureSession session) {
            return (BatApprovedChemistry)await GetEnumAttribute(session, 23);
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
        public async Task<BatChargeState> GetBatChargeState(SecureSession session) {
            return (BatChargeState)await GetEnumAttribute(session, 26);
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
        public async Task<BatChargeFault[]> GetActiveBatChargeFaults(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 30))!);
            BatChargeFault[] list = new BatChargeFault[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = (BatChargeFault)reader.GetUShort(i)!.Value;
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
            return "Power Source";
        }
    }
}