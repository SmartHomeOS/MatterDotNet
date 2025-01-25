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

using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Closures
{
    /// <summary>
    /// An interface to a generic way to secure a door
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 7)]
    public class DoorLock : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0101;

        /// <summary>
        /// An interface to a generic way to secure a door
        /// </summary>
        [SetsRequiredMembers]
        public DoorLock(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected DoorLock(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            LockState = new ReadAttribute<LockStateEnum?>(cluster, endPoint, 0, true) {
                Deserialize = x => (LockStateEnum?)DeserializeEnum(x)
            };
            LockType = new ReadAttribute<LockTypeEnum>(cluster, endPoint, 1) {
                Deserialize = x => (LockTypeEnum)DeserializeEnum(x)!
            };
            ActuatorEnabled = new ReadAttribute<bool>(cluster, endPoint, 2) {
                Deserialize = x => (bool)(dynamic?)x!
            };
            DoorState = new ReadAttribute<DoorStateEnum?>(cluster, endPoint, 3, true) {
                Deserialize = x => (DoorStateEnum?)DeserializeEnum(x)
            };
            DoorOpenEvents = new ReadWriteAttribute<uint>(cluster, endPoint, 4) {
                Deserialize = x => (uint)(dynamic?)x!
            };
            DoorClosedEvents = new ReadWriteAttribute<uint>(cluster, endPoint, 5) {
                Deserialize = x => (uint)(dynamic?)x!
            };
            OpenPeriod = new ReadWriteAttribute<ushort>(cluster, endPoint, 6) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            NumberOfTotalUsersSupported = new ReadAttribute<ushort>(cluster, endPoint, 23) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0

            };
            NumberOfPINUsersSupported = new ReadAttribute<ushort>(cluster, endPoint, 24) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0

            };
            NumberOfRFIDUsersSupported = new ReadAttribute<ushort>(cluster, endPoint, 25) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0

            };
            NumberOfWeekDaySchedulesSupportedPerUser = new ReadAttribute<byte>(cluster, endPoint, 32) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            NumberOfYearDaySchedulesSupportedPerUser = new ReadAttribute<byte>(cluster, endPoint, 33) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            NumberOfHolidaySchedulesSupported = new ReadAttribute<byte>(cluster, endPoint, 34) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            MaxPINCodeLength = new ReadAttribute<byte>(cluster, endPoint, 35) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            MinPINCodeLength = new ReadAttribute<byte>(cluster, endPoint, 36) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            MaxRFIDCodeLength = new ReadAttribute<byte>(cluster, endPoint, 37) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            MinRFIDCodeLength = new ReadAttribute<byte>(cluster, endPoint, 38) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            CredentialRulesSupport = new ReadAttribute<CredentialRuleMask>(cluster, endPoint, 39) {
                Deserialize = x => (CredentialRuleMask)DeserializeEnum(x)!
            };
            NumberOfCredentialsSupportedPerUser = new ReadAttribute<byte>(cluster, endPoint, 40) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            Language = new ReadWriteAttribute<string>(cluster, endPoint, 51) {
                Deserialize = x => (string)(dynamic?)x!
            };
            LEDSettings = new ReadWriteAttribute<byte>(cluster, endPoint, 52) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            AutoRelockTime = new ReadWriteAttribute<uint>(cluster, endPoint, 53) {
                Deserialize = x => (uint)(dynamic?)x!
            };
            SoundVolume = new ReadWriteAttribute<byte>(cluster, endPoint, 54) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            OperatingMode = new ReadWriteAttribute<OperatingModeEnum>(cluster, endPoint, 55) {
                Deserialize = x => (OperatingModeEnum)DeserializeEnum(x)!
            };
            SupportedOperatingModes = new ReadAttribute<SupportedOperatingModesBitmap>(cluster, endPoint, 56) {
                Deserialize = x => (SupportedOperatingModesBitmap)DeserializeEnum(x)!
            };
            DefaultConfigurationRegister = new ReadAttribute<DefaultConfigurationRegisterBitmap>(cluster, endPoint, 57) {
                Deserialize = x => (DefaultConfigurationRegisterBitmap)DeserializeEnum(x)!
            };
            EnableLocalProgramming = new ReadWriteAttribute<bool>(cluster, endPoint, 64) {
                Deserialize = x => (bool?)(dynamic?)x ?? true

            };
            EnableOneTouchLocking = new ReadWriteAttribute<bool>(cluster, endPoint, 65) {
                Deserialize = x => (bool?)(dynamic?)x ?? false

            };
            EnableInsideStatusLED = new ReadWriteAttribute<bool>(cluster, endPoint, 66) {
                Deserialize = x => (bool?)(dynamic?)x ?? false

            };
            EnablePrivacyModeButton = new ReadWriteAttribute<bool>(cluster, endPoint, 67) {
                Deserialize = x => (bool?)(dynamic?)x ?? false

            };
            LocalProgrammingFeatures = new ReadWriteAttribute<LocalProgrammingFeaturesBitmap>(cluster, endPoint, 68) {
                Deserialize = x => (LocalProgrammingFeaturesBitmap)DeserializeEnum(x)!
            };
            WrongCodeEntryLimit = new ReadWriteAttribute<byte>(cluster, endPoint, 72) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            UserCodeTemporaryDisableTime = new ReadWriteAttribute<byte>(cluster, endPoint, 73) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            SendPINOverTheAir = new ReadWriteAttribute<bool>(cluster, endPoint, 80) {
                Deserialize = x => (bool?)(dynamic?)x ?? false

            };
            RequirePINforRemoteOperation = new ReadWriteAttribute<bool>(cluster, endPoint, 81) {
                Deserialize = x => (bool?)(dynamic?)x ?? false

            };
            ExpiringUserTimeout = new ReadWriteAttribute<ushort>(cluster, endPoint, 83) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            AliroReaderVerificationKey = new ReadAttribute<byte[]?>(cluster, endPoint, 296, true) {
                Deserialize = x => (byte[]?)(dynamic?)x
            };
            AliroReaderGroupIdentifier = new ReadAttribute<byte[]?>(cluster, endPoint, 297, true) {
                Deserialize = x => (byte[]?)(dynamic?)x
            };
            AliroReaderGroupSubIdentifier = new ReadAttribute<byte[]>(cluster, endPoint, 304) {
                Deserialize = x => (byte[])(dynamic?)x!
            };
            AliroExpeditedTransactionSupportedProtocolVersions = new ReadAttribute<byte[][]>(cluster, endPoint, 305) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    byte[][] list = new byte[reader.Count][];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = reader.GetBytes(i, false, 16)!;
                    return list;
                }
            };
            AliroGroupResolvingKey = new ReadAttribute<byte[]?>(cluster, endPoint, 306, true) {
                Deserialize = x => (byte[]?)(dynamic?)x
            };
            AliroSupportedBLEUWBProtocolVersions = new ReadAttribute<byte[][]>(cluster, endPoint, 307) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    byte[][] list = new byte[reader.Count][];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = reader.GetBytes(i, false, 16)!;
                    return list;
                }
            };
            AliroBLEAdvertisingVersion = new ReadAttribute<byte>(cluster, endPoint, 308) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            NumberOfAliroCredentialIssuerKeysSupported = new ReadAttribute<ushort>(cluster, endPoint, 309) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            NumberOfAliroEndpointKeysSupported = new ReadAttribute<ushort>(cluster, endPoint, 310) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Lock supports PIN credentials (via keypad, or over-the-air)
            /// </summary>
            PINCredential = 1,
            /// <summary>
            /// Lock supports RFID credentials
            /// </summary>
            RFIDCredential = 2,
            /// <summary>
            /// Lock supports finger related credentials (fingerprint, finger vein)
            /// </summary>
            FingerCredentials = 4,
            /// <summary>
            /// Lock supports local/on-lock logging when Events are not supported
            /// </summary>
            Logging = 8,
            /// <summary>
            /// Lock supports week day user access schedules
            /// </summary>
            WeekDayAccessSchedules = 16,
            /// <summary>
            /// Lock supports a door position sensor that indicates door's state
            /// </summary>
            DoorPositionSensor = 32,
            /// <summary>
            /// Lock supports face related credentials (face, iris, retina)
            /// </summary>
            FaceCredentials = 64,
            /// <summary>
            /// PIN codes over-the-air supported for lock/unlock operations
            /// </summary>
            CredentialsOverTheAirAccess = 128,
            /// <summary>
            /// Lock supports the user commands and database
            /// </summary>
            User = 256,
            /// <summary>
            /// Operation and Programming Notifications
            /// </summary>
            Notification = 512,
            /// <summary>
            /// Lock supports year day user access schedules
            /// </summary>
            YearDayAccessSchedules = 1024,
            /// <summary>
            /// Lock supports holiday schedules
            /// </summary>
            HolidaySchedules = 2048,
            /// <summary>
            /// Lock supports unbolting
            /// </summary>
            Unbolt = 4096,
            /// <summary>
            /// AliroProvisioning
            /// </summary>
            AliroProvisioning = 8192,
            /// <summary>
            /// AliroBLEUWB
            /// </summary>
            AliroBLEUWB = 16384,
        }

        /// <summary>
        /// Alarm Code
        /// </summary>
        public enum AlarmCode : byte {
            /// <summary>
            /// Locking Mechanism Jammed
            /// </summary>
            LockJammed = 0,
            /// <summary>
            /// Lock Reset to Factory Defaults
            /// </summary>
            LockFactoryReset = 1,
            /// <summary>
            /// Lock Radio Power Cycled
            /// </summary>
            LockRadioPowerCycled = 3,
            /// <summary>
            /// Tamper Alarm - wrong code entry limit
            /// </summary>
            WrongCodeEntryLimit = 4,
            /// <summary>
            /// Tamper Alarm - front escutcheon removed from main
            /// </summary>
            FrontEsceutcheonRemoved = 5,
            /// <summary>
            /// Forced Door Open under Door Locked Condition
            /// </summary>
            DoorForcedOpen = 6,
            /// <summary>
            /// Door ajar
            /// </summary>
            DoorAjar = 7,
            /// <summary>
            /// Force User SOS alarm
            /// </summary>
            ForcedUser = 8,
        }

        /// <summary>
        /// Credential Rule
        /// </summary>
        public enum CredentialRule : byte {
            /// <summary>
            /// Only one credential is required for lock operation
            /// </summary>
            Single = 0,
            /// <summary>
            /// Any two credentials are required for lock operation
            /// </summary>
            Dual = 1,
            /// <summary>
            /// Any three credentials are required for lock operation
            /// </summary>
            Tri = 2,
        }

        /// <summary>
        /// Credential Type
        /// </summary>
        public enum CredentialType : byte {
            /// <summary>
            /// Programming PIN code credential type
            /// </summary>
            ProgrammingPIN = 0,
            /// <summary>
            /// PIN code credential type
            /// </summary>
            PIN = 1,
            /// <summary>
            /// RFID identifier credential type
            /// </summary>
            RFID = 2,
            /// <summary>
            /// Fingerprint identifier credential type
            /// </summary>
            Fingerprint = 3,
            /// <summary>
            /// Finger vein identifier credential type
            /// </summary>
            FingerVein = 4,
            /// <summary>
            /// Face identifier credential type
            /// </summary>
            Face = 5,
            /// <summary>
            /// A Credential Issuer public key as defined in ref_Aliro
            /// </summary>
            AliroCredentialIssuerKey = 6,
            /// <summary>
            /// An Endpoint public key as defined in ref_Aliro which can be evicted if space is needed for another endpoint key
            /// </summary>
            AliroEvictableEndpointKey = 7,
            /// <summary>
            /// An Endpoint public key as defined in ref_Aliro which cannot be evicted if space is needed for another endpoint key
            /// </summary>
            AliroNonEvictableEndpointKey = 8,
        }

        /// <summary>
        /// Data Operation Type
        /// </summary>
        public enum DataOperationType : byte {
            /// <summary>
            /// Data is being added or was added
            /// </summary>
            Add = 0,
            /// <summary>
            /// Data is being cleared or was cleared
            /// </summary>
            Clear = 1,
            /// <summary>
            /// Data is being modified or was modified
            /// </summary>
            Modify = 2,
        }

        /// <summary>
        /// Door State
        /// </summary>
        public enum DoorStateEnum : byte {
            /// <summary>
            /// Door state is open
            /// </summary>
            DoorOpen = 0,
            /// <summary>
            /// Door state is closed
            /// </summary>
            DoorClosed = 1,
            /// <summary>
            /// Door state is jammed
            /// </summary>
            DoorJammed = 2,
            /// <summary>
            /// Door state is currently forced open
            /// </summary>
            DoorForcedOpen = 3,
            /// <summary>
            /// Door state is invalid for unspecified reason
            /// </summary>
            DoorUnspecifiedError = 4,
            /// <summary>
            /// Door state is ajar
            /// </summary>
            DoorAjar = 5,
        }

        /// <summary>
        /// Lock Data Type
        /// </summary>
        public enum LockDataType : byte {
            /// <summary>
            /// Unspecified or manufacturer specific lock user data added, cleared, or modified.
            /// </summary>
            Unspecified = 0x0,
            /// <summary>
            /// Lock programming PIN code was added, cleared, or modified.
            /// </summary>
            ProgrammingCode = 0x1,
            /// <summary>
            /// Lock user index was added, cleared, or modified.
            /// </summary>
            UserIndex = 0x2,
            /// <summary>
            /// Lock user week day schedule was added, cleared, or modified.
            /// </summary>
            WeekDaySchedule = 0x3,
            /// <summary>
            /// Lock user year day schedule was added, cleared, or modified.
            /// </summary>
            YearDaySchedule = 0x4,
            /// <summary>
            /// Lock holiday schedule was added, cleared, or modified.
            /// </summary>
            HolidaySchedule = 0x5,
            /// <summary>
            /// Lock user PIN code was added, cleared, or modified.
            /// </summary>
            PIN = 0x6,
            /// <summary>
            /// Lock user RFID code was added, cleared, or modified.
            /// </summary>
            RFID = 0x7,
            /// <summary>
            /// Lock user fingerprint was added, cleared, or modified.
            /// </summary>
            Fingerprint = 0x8,
            /// <summary>
            /// Lock user finger-vein information was added, cleared, or modified.
            /// </summary>
            FingerVein = 0x9,
            /// <summary>
            /// Lock user face information was added, cleared, or modified.
            /// </summary>
            Face = 0xA,
            /// <summary>
            /// An Aliro credential issuer key credential was added, cleared, or modified.
            /// </summary>
            AliroCredentialIssuerKey = 0xB,
            /// <summary>
            /// An Aliro endpoint key credential which can be evicted credential was added, cleared, or modified.
            /// </summary>
            AliroEvictableEndpointKey = 0xC,
            /// <summary>
            /// An Aliro endpoint key credential which cannot be evicted was added, cleared, or modified.
            /// </summary>
            AliroNonEvictableEndpointKey = 0xD,
        }

        /// <summary>
        /// Lock Operation Type
        /// </summary>
        public enum LockOperationType : byte {
            /// <summary>
            /// Lock operation
            /// </summary>
            Lock = 0,
            /// <summary>
            /// Unlock operation
            /// </summary>
            Unlock = 1,
            /// <summary>
            /// Triggered by keypad entry for user with User Type set to Non Access User
            /// </summary>
            NonAccessUserEvent = 2,
            /// <summary>
            /// Triggered by using a user with UserType set to Forced User
            /// </summary>
            ForcedUserEvent = 3,
            /// <summary>
            /// Unlatch operation
            /// </summary>
            Unlatch = 4,
        }

        /// <summary>
        /// Operation Error
        /// </summary>
        public enum OperationError : byte {
            /// <summary>
            /// Lock/unlock error caused by unknown or unspecified source
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// Lock/unlock error caused by invalid PIN, RFID, fingerprint or other credential
            /// </summary>
            InvalidCredential = 1,
            /// <summary>
            /// Lock/unlock error caused by disabled USER or credential
            /// </summary>
            DisabledUserDenied = 2,
            /// <summary>
            /// Lock/unlock error caused by schedule restriction
            /// </summary>
            Restricted = 3,
            /// <summary>
            /// Lock/unlock error caused by insufficient battery power left to safely actuate the lock
            /// </summary>
            InsufficientBattery = 4,
        }

        /// <summary>
        /// Operating Mode
        /// </summary>
        public enum OperatingModeEnum : byte {
            /// <summary>
            /// 
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 
            /// </summary>
            Vacation = 1,
            /// <summary>
            /// 
            /// </summary>
            Privacy = 2,
            /// <summary>
            /// 
            /// </summary>
            NoRemoteLockUnlock = 3,
            /// <summary>
            /// 
            /// </summary>
            Passage = 4,
        }

        /// <summary>
        /// Operation Source
        /// </summary>
        public enum OperationSource : byte {
            /// <summary>
            /// Lock/unlock operation came from unspecified source
            /// </summary>
            Unspecified = 0x0,
            /// <summary>
            /// Lock/unlock operation came from manual operation (key, thumbturn, handle, etc).
            /// </summary>
            Manual = 0x1,
            /// <summary>
            /// Lock/unlock operation came from proprietary remote source (e.g. vendor app/cloud)
            /// </summary>
            ProprietaryRemote = 0x2,
            /// <summary>
            /// Lock/unlock operation came from keypad
            /// </summary>
            Keypad = 0x3,
            /// <summary>
            /// Lock/unlock operation came from lock automatically (e.g. relock timer)
            /// </summary>
            Auto = 0x4,
            /// <summary>
            /// Lock/unlock operation came from lock button (e.g. one touch or button)
            /// </summary>
            Button = 0x5,
            /// <summary>
            /// Lock/unlock operation came from lock due to a schedule
            /// </summary>
            Schedule = 0x6,
            /// <summary>
            /// Lock/unlock operation came from remote node
            /// </summary>
            Remote = 0x7,
            /// <summary>
            /// Lock/unlock operation came from RFID card
            /// </summary>
            RFID = 0x8,
            /// <summary>
            /// Lock/unlock operation came from biometric source (e.g. face, fingerprint/fingervein)
            /// </summary>
            Biometric = 0x9,
            /// <summary>
            /// Lock/unlock operation came from an interaction defined in ref_Aliro, or user change operation was a step-up credential provisioning as defined in ref_Aliro
            /// </summary>
            Aliro = 0xA,
        }

        /// <summary>
        /// User Status
        /// </summary>
        public enum UserStatus : byte {
            /// <summary>
            /// The user ID is available
            /// </summary>
            Available = 0,
            /// <summary>
            /// The user ID is occupied and enabled
            /// </summary>
            OccupiedEnabled = 1,
            /// <summary>
            /// The user ID is occupied and disabled
            /// </summary>
            OccupiedDisabled = 3,
        }

        /// <summary>
        /// User Type
        /// </summary>
        public enum UserType : byte {
            /// <summary>
            /// The user ID type is unrestricted
            /// </summary>
            UnrestrictedUser = 0,
            /// <summary>
            /// The user ID type is schedule
            /// </summary>
            YearDayScheduleUser = 1,
            /// <summary>
            /// The user ID type is schedule
            /// </summary>
            WeekDayScheduleUser = 2,
            /// <summary>
            /// The user ID type is programming
            /// </summary>
            ProgrammingUser = 3,
            /// <summary>
            /// The user ID type is non access
            /// </summary>
            NonAccessUser = 4,
            /// <summary>
            /// The user ID type is forced
            /// </summary>
            ForcedUser = 5,
            /// <summary>
            /// The user ID type is disposable
            /// </summary>
            DisposableUser = 6,
            /// <summary>
            /// The user ID type is expiring
            /// </summary>
            ExpiringUser = 7,
            /// <summary>
            /// The user ID type is schedule restricted
            /// </summary>
            ScheduleRestrictedUser = 8,
            /// <summary>
            /// The user ID type is remote only
            /// </summary>
            RemoteOnlyUser = 9,
        }

        /// <summary>
        /// Lock State
        /// </summary>
        public enum LockStateEnum : byte {
            /// <summary>
            /// Lock state is not fully locked
            /// </summary>
            NotFullyLocked = 0,
            /// <summary>
            /// Lock state is fully locked
            /// </summary>
            Locked = 1,
            /// <summary>
            /// Lock state is fully unlocked
            /// </summary>
            Unlocked = 2,
            /// <summary>
            /// Lock state is fully unlocked and the latch is pulled
            /// </summary>
            Unlatched = 3,
        }

        /// <summary>
        /// Lock Type
        /// </summary>
        public enum LockTypeEnum : byte {
            /// <summary>
            /// Physical lock type is dead bolt
            /// </summary>
            DeadBolt = 0x0,
            /// <summary>
            /// Physical lock type is magnetic
            /// </summary>
            Magnetic = 0x1,
            /// <summary>
            /// Physical lock type is other
            /// </summary>
            Other = 0x2,
            /// <summary>
            /// Physical lock type is mortise
            /// </summary>
            Mortise = 0x3,
            /// <summary>
            /// Physical lock type is rim
            /// </summary>
            Rim = 0x4,
            /// <summary>
            /// Physical lock type is latch bolt
            /// </summary>
            LatchBolt = 0x5,
            /// <summary>
            /// Physical lock type is cylindrical lock
            /// </summary>
            CylindricalLock = 0x6,
            /// <summary>
            /// Physical lock type is tubular lock
            /// </summary>
            TubularLock = 0x7,
            /// <summary>
            /// Physical lock type is interconnected lock
            /// </summary>
            InterconnectedLock = 0x8,
            /// <summary>
            /// Physical lock type is dead latch
            /// </summary>
            DeadLatch = 0x9,
            /// <summary>
            /// Physical lock type is door furniture
            /// </summary>
            DoorFurniture = 0xA,
            /// <summary>
            /// Physical lock type is euro cylinder
            /// </summary>
            Eurocylinder = 0xB,
        }

        /// <summary>
        /// Status
        /// </summary>
        public enum Status : byte {
            Success = 0x0,
            Failure = 0x1,
            Duplicate = 0x2,
            Occupied = 0x3,
            InvalidField = 0x85,
            ResourceExhausted = 0x89,
            NotFound = 0x8B,
        }

        /// <summary>
        /// Door Lock Set Pin Or Id Status
        /// </summary>
        public enum DoorLockSetPinOrIdStatus : byte {
            Success = 0,
            GeneralFailure = 1,
            MemoryFull = 2,
            DuplicateCodeError = 3,
        }

        /// <summary>
        /// Door Lock Operation Event Code
        /// </summary>
        public enum DoorLockOperationEventCode : byte {
            UnknownOrMfgSpecific = 0x0,
            Lock = 0x1,
            Unlock = 0x2,
            LockInvalidPinOrId = 0x3,
            LockInvalidSchedule = 0x4,
            UnlockInvalidPinOrId = 0x5,
            UnlockInvalidSchedule = 0x6,
            OneTouchLock = 0x7,
            KeyLock = 0x8,
            KeyUnlock = 0x9,
            AutoLock = 0xA,
            ScheduleLock = 0xB,
            ScheduleUnlock = 0xC,
            ManualLock = 0xD,
            ManualUnlock = 0xE,
        }

        /// <summary>
        /// Door Lock Programming Event Code
        /// </summary>
        public enum DoorLockProgrammingEventCode : byte {
            UnknownOrMfgSpecific = 0,
            MasterCodeChanged = 1,
            PinAdded = 2,
            PinDeleted = 3,
            PinChanged = 4,
            IdAdded = 5,
            IdDeleted = 6,
        }

        /// <summary>
        /// Door Lock User Status
        /// </summary>
        public enum DoorLockUserStatus : byte {
            Available = 0x0,
            OccupiedEnabled = 0x1,
            OccupiedDisabled = 0x3,
            NotSupported = 0xFF,
        }

        /// <summary>
        /// Door Lock User Type
        /// </summary>
        public enum DoorLockUserType : byte {
            Unrestricted = 0x0,
            YearDayScheduleUser = 0x1,
            WeekDayScheduleUser = 0x2,
            MasterUser = 0x3,
            NonAccessUser = 0x4,
            NotSupported = 0xFF,
        }

        /// <summary>
        /// Credential Rule Mask
        /// </summary>
        [Flags]
        public enum CredentialRuleMask : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Only one credential is required for lock operation
            /// </summary>
            Single = 0x01,
            /// <summary>
            /// Any two credentials are required for lock operation
            /// </summary>
            Dual = 0x02,
            /// <summary>
            /// Any three credentials are required for lock operation
            /// </summary>
            Tri = 0x04,
        }

        /// <summary>
        /// Days Mask Map
        /// </summary>
        [Flags]
        public enum DaysMaskMap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Schedule is applied on Sunday
            /// </summary>
            Sunday = 0x01,
            /// <summary>
            /// Schedule is applied on Monday
            /// </summary>
            Monday = 0x02,
            /// <summary>
            /// Schedule is applied on Tuesday
            /// </summary>
            Tuesday = 0x04,
            /// <summary>
            /// Schedule is applied on Wednesday
            /// </summary>
            Wednesday = 0x08,
            /// <summary>
            /// Schedule is applied on Thursday
            /// </summary>
            Thursday = 0x10,
            /// <summary>
            /// Schedule is applied on Friday
            /// </summary>
            Friday = 0x20,
            /// <summary>
            /// Schedule is applied on Saturday
            /// </summary>
            Saturday = 0x40,
        }

        /// <summary>
        /// Credential Rules Support
        /// </summary>
        [Flags]
        public enum CredentialRulesSupportBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Only one credential is required for lock operation
            /// </summary>
            Single = 0x01,
            /// <summary>
            /// Any two credentials are required for lock operation
            /// </summary>
            Dual = 0x02,
            /// <summary>
            /// Any three credentials are required for lock operation
            /// </summary>
            Tri = 0x04,
        }

        /// <summary>
        /// Supported Operating Modes
        /// </summary>
        [Flags]
        public enum SupportedOperatingModesBitmap : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Normal operation mode
            /// </summary>
            Normal = 0x0001,
            /// <summary>
            /// Vacation operation mode
            /// </summary>
            Vacation = 0x0002,
            /// <summary>
            /// Privacy operation mode
            /// </summary>
            Privacy = 0x0004,
            /// <summary>
            /// No remote lock and unlock operation mode
            /// </summary>
            NoRemoteLockUnlock = 0x0008,
            /// <summary>
            /// Passage operation mode
            /// </summary>
            Passage = 0x0010,
        }

        /// <summary>
        /// Default Configuration Register
        /// </summary>
        [Flags]
        public enum DefaultConfigurationRegisterBitmap : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// The state of local programming functionality
            /// </summary>
            EnableLocalProgrammingEnabled = 0x0001,
            /// <summary>
            /// The state of the keypad interface
            /// </summary>
            KeypadInterfaceDefaultAccessEnabled = 0x0002,
            /// <summary>
            /// The state of the remote interface
            /// </summary>
            RemoteInterfaceDefaultAccessIsEnabled = 0x0004,
            /// <summary>
            /// Sound volume is set to Silent value
            /// </summary>
            SoundEnabled = 0x0020,
            /// <summary>
            /// Auto relock time it set to 0
            /// </summary>
            AutoRelockTimeSet = 0x0040,
            /// <summary>
            /// LEDs is disabled
            /// </summary>
            LEDSettingsSet = 0x0080,
        }

        /// <summary>
        /// Local Programming Features
        /// </summary>
        [Flags]
        public enum LocalProgrammingFeaturesBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// The state of the ability to add users, credentials or schedules on the device
            /// </summary>
            AddUsersCredentialsSchedulesLocally = 0x01,
            /// <summary>
            /// The state of the ability to modify users, credentials or schedules on the device
            /// </summary>
            ModifyUsersCredentialsSchedulesLocally = 0x02,
            /// <summary>
            /// The state of the ability to clear users, credentials or schedules on the device
            /// </summary>
            ClearUsersCredentialsSchedulesLocally = 0x04,
            /// <summary>
            /// The state of the ability to adjust settings on the device
            /// </summary>
            AdjustLockSettingsLocally = 0x08,
        }

        /// <summary>
        /// Keypad Operation Event Mask
        /// </summary>
        [Flags]
        public enum KeypadOperationEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            Unknown = 0x0001,
            Lock = 0x0002,
            Unlock = 0x0004,
            LockInvalidPIN = 0x0008,
            LockInvalidSchedule = 0x0010,
            UnlockInvalidCode = 0x0020,
            UnlockInvalidSchedule = 0x0040,
            NonAccessUserOpEvent = 0x0080,
        }

        /// <summary>
        /// Remote Operation Event Mask
        /// </summary>
        [Flags]
        public enum RemoteOperationEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            Unknown = 0x0001,
            Lock = 0x0002,
            Unlock = 0x0004,
            LockInvalidCode = 0x0008,
            LockInvalidSchedule = 0x0010,
            UnlockInvalidCode = 0x0020,
            UnlockInvalidSchedule = 0x0040,
        }

        /// <summary>
        /// Manual Operation Event Mask
        /// </summary>
        [Flags]
        public enum ManualOperationEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            Unknown = 0x00001,
            ThumbturnLock = 0x00002,
            ThumbturnUnlock = 0x00004,
            OneTouchLock = 0x00008,
            KeyLock = 0x00010,
            KeyUnlock = 0x00020,
            AutoLock = 0x00040,
            ScheduleLock = 0x00080,
            ScheduleUnlock = 0x00100,
            ManualLock = 0x00200,
            ManualUnlock = 0x00400,
        }

        /// <summary>
        /// RFID Operation Event Mask
        /// </summary>
        [Flags]
        public enum RFIDOperationEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            Unknown = 0x0001,
            Lock = 0x0002,
            Unlock = 0x0004,
            LockInvalidRFID = 0x0008,
            LockInvalidSchedule = 0x0010,
            UnlockInvalidRFID = 0x0020,
            UnlockInvalidSchedule = 0x0040,
        }

        /// <summary>
        /// Keypad Programming Event Mask
        /// </summary>
        [Flags]
        public enum KeypadProgrammingEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            Unknown = 0x0001,
            ProgrammingPINChanged = 0x0002,
            PINAdded = 0x0004,
            PINCleared = 0x0008,
            PINChanged = 0x0010,
        }

        /// <summary>
        /// Remote Programming Event Mask
        /// </summary>
        [Flags]
        public enum RemoteProgrammingEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            Unknown = 0x0001,
            ProgrammingPINChanged = 0x0002,
            PINAdded = 0x0004,
            PINCleared = 0x0008,
            PINChanged = 0x0010,
            RFIDCodeAdded = 0x0020,
            RFIDCodeCleared = 0x0040,
        }

        /// <summary>
        /// RFID Programming Event Mask
        /// </summary>
        [Flags]
        public enum RFIDProgrammingEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            Unknown = 0x0001,
            RFIDCodeAdded = 0x0020,
            RFIDCodeCleared = 0x0040,
        }

        /// <summary>
        /// Door Lock Day Of Week
        /// </summary>
        [Flags]
        public enum DoorLockDayOfWeek : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Sunday
            /// </summary>
            Sunday = 0x01,
            /// <summary>
            /// Monday
            /// </summary>
            Monday = 0x02,
            /// <summary>
            /// Tuesday
            /// </summary>
            Tuesday = 0x04,
            /// <summary>
            /// Wednesday
            /// </summary>
            Wednesday = 0x08,
            /// <summary>
            /// Thursday
            /// </summary>
            Thursday = 0x10,
            /// <summary>
            /// Friday
            /// </summary>
            Friday = 0x20,
            /// <summary>
            /// Saturday
            /// </summary>
            Saturday = 0x40,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Credential
        /// </summary>
        public record Credential : TLVPayload {
            /// <summary>
            /// Credential
            /// </summary>
            public Credential() { }

            /// <summary>
            /// Credential
            /// </summary>
            [SetsRequiredMembers]
            public Credential(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                CredentialType = (CredentialType)reader.GetUShort(0)!.Value;
                CredentialIndex = reader.GetUShort(1)!.Value;
            }
            public required CredentialType CredentialType { get; set; }
            public required ushort CredentialIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)CredentialType);
                writer.WriteUShort(1, CredentialIndex);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record LockDoorPayload : TLVPayload {
            public byte[]? PINCode { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (PINCode != null)
                    writer.WriteBytes(0, PINCode);
                writer.EndContainer();
            }
        }

        private record UnlockDoorPayload : TLVPayload {
            public byte[]? PINCode { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (PINCode != null)
                    writer.WriteBytes(0, PINCode);
                writer.EndContainer();
            }
        }

        private record UnlockWithTimeoutPayload : TLVPayload {
            public required ushort Timeout { get; set; }
            public byte[]? PINCode { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, Timeout);
                if (PINCode != null)
                    writer.WriteBytes(1, PINCode);
                writer.EndContainer();
            }
        }

        private record SetWeekDaySchedulePayload : TLVPayload {
            public required byte WeekDayIndex { get; set; }
            public required ushort UserIndex { get; set; }
            public required DaysMaskMap DaysMask { get; set; }
            public required byte StartHour { get; set; }
            public required byte StartMinute { get; set; }
            public required byte EndHour { get; set; }
            public required byte EndMinute { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, WeekDayIndex);
                writer.WriteUShort(1, UserIndex);
                writer.WriteUInt(2, (uint)DaysMask);
                writer.WriteByte(3, StartHour);
                writer.WriteByte(4, StartMinute);
                writer.WriteByte(5, EndHour);
                writer.WriteByte(6, EndMinute);
                writer.EndContainer();
            }
        }

        private record GetWeekDaySchedulePayload : TLVPayload {
            public required byte WeekDayIndex { get; set; }
            public required ushort UserIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, WeekDayIndex);
                writer.WriteUShort(1, UserIndex);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Week Day Schedule Response - Reply from server
        /// </summary>
        public struct GetWeekDayScheduleResponse() {
            public required byte WeekDayIndex { get; set; }
            public required ushort UserIndex { get; set; }
            public required Status Status { get; set; }
            public DaysMaskMap? DaysMask { get; set; }
            public byte? StartHour { get; set; }
            public byte? StartMinute { get; set; }
            public byte? EndHour { get; set; }
            public byte? EndMinute { get; set; }
        }

        private record ClearWeekDaySchedulePayload : TLVPayload {
            public required byte WeekDayIndex { get; set; }
            public required ushort UserIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, WeekDayIndex);
                writer.WriteUShort(1, UserIndex);
                writer.EndContainer();
            }
        }

        private record SetYearDaySchedulePayload : TLVPayload {
            public required byte YearDayIndex { get; set; }
            public required ushort UserIndex { get; set; }
            public required DateTime LocalStartTime { get; set; }
            public required DateTime LocalEndTime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, YearDayIndex);
                writer.WriteUShort(1, UserIndex);
                writer.WriteUInt(2, TimeUtil.ToEpochSeconds(LocalStartTime));
                writer.WriteUInt(3, TimeUtil.ToEpochSeconds(LocalEndTime));
                writer.EndContainer();
            }
        }

        private record GetYearDaySchedulePayload : TLVPayload {
            public required byte YearDayIndex { get; set; }
            public required ushort UserIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, YearDayIndex);
                writer.WriteUShort(1, UserIndex);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Year Day Schedule Response - Reply from server
        /// </summary>
        public struct GetYearDayScheduleResponse() {
            public required byte YearDayIndex { get; set; }
            public required ushort UserIndex { get; set; }
            public required Status Status { get; set; }
            public DateTime? LocalStartTime { get; set; }
            public DateTime? LocalEndTime { get; set; }
        }

        private record ClearYearDaySchedulePayload : TLVPayload {
            public required byte YearDayIndex { get; set; }
            public required ushort UserIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, YearDayIndex);
                writer.WriteUShort(1, UserIndex);
                writer.EndContainer();
            }
        }

        private record SetHolidaySchedulePayload : TLVPayload {
            public required byte HolidayIndex { get; set; }
            public required DateTime LocalStartTime { get; set; }
            public required DateTime LocalEndTime { get; set; }
            public required OperatingModeEnum OperatingMode { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, HolidayIndex);
                writer.WriteUInt(1, TimeUtil.ToEpochSeconds(LocalStartTime));
                writer.WriteUInt(2, TimeUtil.ToEpochSeconds(LocalEndTime));
                writer.WriteUShort(3, (ushort)OperatingMode);
                writer.EndContainer();
            }
        }

        private record GetHolidaySchedulePayload : TLVPayload {
            public required byte HolidayIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, HolidayIndex);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Holiday Schedule Response - Reply from server
        /// </summary>
        public struct GetHolidayScheduleResponse() {
            public required byte HolidayIndex { get; set; }
            public required Status Status { get; set; }
            public DateTime? LocalStartTime { get; set; }
            public DateTime? LocalEndTime { get; set; }
            public OperatingModeEnum? OperatingMode { get; set; }
        }

        private record ClearHolidaySchedulePayload : TLVPayload {
            public required byte HolidayIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, HolidayIndex);
                writer.EndContainer();
            }
        }

        private record SetUserPayload : TLVPayload {
            public required DataOperationType OperationType { get; set; }
            public required ushort UserIndex { get; set; }
            public required string? UserName { get; set; }
            public required uint? UserUniqueID { get; set; }
            public required UserStatus? UserStatus { get; set; }
            public required UserType? UserType { get; set; }
            public required CredentialRule? CredentialRule { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)OperationType);
                writer.WriteUShort(1, UserIndex);
                writer.WriteString(2, UserName);
                writer.WriteUInt(3, UserUniqueID);
                writer.WriteUShort(4, (ushort?)UserStatus);
                writer.WriteUShort(5, (ushort?)UserType);
                writer.WriteUShort(6, (ushort?)CredentialRule);
                writer.EndContainer();
            }
        }

        private record GetUserPayload : TLVPayload {
            public required ushort UserIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, UserIndex);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get User Response - Reply from server
        /// </summary>
        public struct GetUserResponse() {
            public required ushort UserIndex { get; set; }
            public required string? UserName { get; set; }
            public required uint? UserUniqueID { get; set; }
            public required UserStatus? UserStatus { get; set; }
            public required UserType? UserType { get; set; }
            public required CredentialRule? CredentialRule { get; set; }
            public required Credential[]? Credentials { get; set; }
            public required byte? CreatorFabricIndex { get; set; }
            public required byte? LastModifiedFabricIndex { get; set; }
            public required ushort? NextUserIndex { get; set; }
        }

        private record ClearUserPayload : TLVPayload {
            public required ushort UserIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, UserIndex);
                writer.EndContainer();
            }
        }

        private record SetCredentialPayload : TLVPayload {
            public required DataOperationType OperationType { get; set; }
            public required Credential Credential { get; set; }
            public required byte[] CredentialData { get; set; }
            public required ushort? UserIndex { get; set; }
            public required UserStatus? UserStatus { get; set; }
            public required UserType? UserType { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)OperationType);
                Credential.Serialize(writer, 1);
                writer.WriteBytes(2, CredentialData);
                writer.WriteUShort(3, UserIndex);
                writer.WriteUShort(4, (ushort?)UserStatus);
                writer.WriteUShort(5, (ushort?)UserType);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Set Credential Response - Reply from server
        /// </summary>
        public struct SetCredentialResponse() {
            public required Status Status { get; set; }
            public required ushort? UserIndex { get; set; }
            public required ushort? NextCredentialIndex { get; set; }
        }

        private record GetCredentialStatusPayload : TLVPayload {
            public required Credential Credential { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                Credential.Serialize(writer, 0);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Credential Status Response - Reply from server
        /// </summary>
        public struct GetCredentialStatusResponse() {
            public required bool CredentialExists { get; set; }
            public required ushort? UserIndex { get; set; }
            public required byte? CreatorFabricIndex { get; set; }
            public required byte? LastModifiedFabricIndex { get; set; }
            public required ushort? NextCredentialIndex { get; set; }
            public byte[]? CredentialData { get; set; }
        }

        private record ClearCredentialPayload : TLVPayload {
            public required Credential? Credential { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (Credential == null)
                    writer.WriteNull(0);
                else
                    Credential.Serialize(writer, 0);
                writer.EndContainer();
            }
        }

        private record UnboltDoorPayload : TLVPayload {
            public byte[]? PINCode { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (PINCode != null)
                    writer.WriteBytes(0, PINCode);
                writer.EndContainer();
            }
        }

        private record SetAliroReaderConfigPayload : TLVPayload {
            public required byte[] SigningKey { get; set; }
            public required byte[] VerificationKey { get; set; }
            public required byte[] GroupIdentifier { get; set; }
            public byte[]? GroupResolvingKey { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, SigningKey, 32);
                writer.WriteBytes(1, VerificationKey, 65);
                writer.WriteBytes(2, GroupIdentifier, 16);
                if (GroupResolvingKey != null)
                    writer.WriteBytes(3, GroupResolvingKey, 16);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Lock Door
        /// </summary>
        public async Task<bool> LockDoor(SecureSession session, ushort commandTimeoutMS, byte[]? pINCode) {
            LockDoorPayload requestFields = new LockDoorPayload() {
                PINCode = pINCode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Unlock Door
        /// </summary>
        public async Task<bool> UnlockDoor(SecureSession session, ushort commandTimeoutMS, byte[]? pINCode) {
            UnlockDoorPayload requestFields = new UnlockDoorPayload() {
                PINCode = pINCode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 1, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Unlock With Timeout
        /// </summary>
        public async Task<bool> UnlockWithTimeout(SecureSession session, ushort commandTimeoutMS, ushort timeout, byte[]? pINCode) {
            UnlockWithTimeoutPayload requestFields = new UnlockWithTimeoutPayload() {
                Timeout = timeout,
                PINCode = pINCode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 3, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Week Day Schedule
        /// </summary>
        public async Task<bool> SetWeekDaySchedule(SecureSession session, byte weekDayIndex, ushort userIndex, DaysMaskMap daysMask, byte startHour, byte startMinute, byte endHour, byte endMinute) {
            SetWeekDaySchedulePayload requestFields = new SetWeekDaySchedulePayload() {
                WeekDayIndex = weekDayIndex,
                UserIndex = userIndex,
                DaysMask = daysMask,
                StartHour = startHour,
                StartMinute = startMinute,
                EndHour = endHour,
                EndMinute = endMinute,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 11, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Week Day Schedule
        /// </summary>
        public async Task<GetWeekDayScheduleResponse?> GetWeekDaySchedule(SecureSession session, byte weekDayIndex, ushort userIndex) {
            GetWeekDaySchedulePayload requestFields = new GetWeekDaySchedulePayload() {
                WeekDayIndex = weekDayIndex,
                UserIndex = userIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 12, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetWeekDayScheduleResponse() {
                WeekDayIndex = (byte)GetField(resp, 0),
                UserIndex = (ushort)GetField(resp, 1),
                Status = (Status)(byte)GetField(resp, 2),
                DaysMask = (DaysMaskMap?)(byte?)GetOptionalField(resp, 3),
                StartHour = (byte?)GetOptionalField(resp, 4),
                StartMinute = (byte?)GetOptionalField(resp, 5),
                EndHour = (byte?)GetOptionalField(resp, 6),
                EndMinute = (byte?)GetOptionalField(resp, 7),
            };
        }

        /// <summary>
        /// Clear Week Day Schedule
        /// </summary>
        public async Task<bool> ClearWeekDaySchedule(SecureSession session, byte weekDayIndex, ushort userIndex) {
            ClearWeekDaySchedulePayload requestFields = new ClearWeekDaySchedulePayload() {
                WeekDayIndex = weekDayIndex,
                UserIndex = userIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 13, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Year Day Schedule
        /// </summary>
        public async Task<bool> SetYearDaySchedule(SecureSession session, byte yearDayIndex, ushort userIndex, DateTime localStartTime, DateTime localEndTime) {
            SetYearDaySchedulePayload requestFields = new SetYearDaySchedulePayload() {
                YearDayIndex = yearDayIndex,
                UserIndex = userIndex,
                LocalStartTime = localStartTime,
                LocalEndTime = localEndTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 14, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Year Day Schedule
        /// </summary>
        public async Task<GetYearDayScheduleResponse?> GetYearDaySchedule(SecureSession session, byte yearDayIndex, ushort userIndex) {
            GetYearDaySchedulePayload requestFields = new GetYearDaySchedulePayload() {
                YearDayIndex = yearDayIndex,
                UserIndex = userIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 15, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetYearDayScheduleResponse() {
                YearDayIndex = (byte)GetField(resp, 0),
                UserIndex = (ushort)GetField(resp, 1),
                Status = (Status)(byte)GetField(resp, 2),
                LocalStartTime = (DateTime?)GetOptionalField(resp, 3),
                LocalEndTime = (DateTime?)GetOptionalField(resp, 4),
            };
        }

        /// <summary>
        /// Clear Year Day Schedule
        /// </summary>
        public async Task<bool> ClearYearDaySchedule(SecureSession session, byte yearDayIndex, ushort userIndex) {
            ClearYearDaySchedulePayload requestFields = new ClearYearDaySchedulePayload() {
                YearDayIndex = yearDayIndex,
                UserIndex = userIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 16, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Holiday Schedule
        /// </summary>
        public async Task<bool> SetHolidaySchedule(SecureSession session, byte holidayIndex, DateTime localStartTime, DateTime localEndTime, OperatingModeEnum operatingMode) {
            SetHolidaySchedulePayload requestFields = new SetHolidaySchedulePayload() {
                HolidayIndex = holidayIndex,
                LocalStartTime = localStartTime,
                LocalEndTime = localEndTime,
                OperatingMode = operatingMode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 17, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Holiday Schedule
        /// </summary>
        public async Task<GetHolidayScheduleResponse?> GetHolidaySchedule(SecureSession session, byte holidayIndex) {
            GetHolidaySchedulePayload requestFields = new GetHolidaySchedulePayload() {
                HolidayIndex = holidayIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 18, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetHolidayScheduleResponse() {
                HolidayIndex = (byte)GetField(resp, 0),
                Status = (Status)(byte)GetField(resp, 1),
                LocalStartTime = (DateTime?)GetOptionalField(resp, 2),
                LocalEndTime = (DateTime?)GetOptionalField(resp, 3),
                OperatingMode = (OperatingModeEnum?)(byte?)GetOptionalField(resp, 4),
            };
        }

        /// <summary>
        /// Clear Holiday Schedule
        /// </summary>
        public async Task<bool> ClearHolidaySchedule(SecureSession session, byte holidayIndex) {
            ClearHolidaySchedulePayload requestFields = new ClearHolidaySchedulePayload() {
                HolidayIndex = holidayIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 19, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set User
        /// </summary>
        public async Task<bool> SetUser(SecureSession session, ushort commandTimeoutMS, DataOperationType operationType, ushort userIndex, string? userName, uint? userUniqueID, UserStatus? userStatus, UserType? userType, CredentialRule? credentialRule) {
            SetUserPayload requestFields = new SetUserPayload() {
                OperationType = operationType,
                UserIndex = userIndex,
                UserName = userName,
                UserUniqueID = userUniqueID,
                UserStatus = userStatus,
                UserType = userType,
                CredentialRule = credentialRule,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 26, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get User
        /// </summary>
        public async Task<GetUserResponse?> GetUser(SecureSession session, ushort userIndex) {
            GetUserPayload requestFields = new GetUserPayload() {
                UserIndex = userIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 27, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetUserResponse() {
                UserIndex = (ushort)GetField(resp, 0),
                UserName = (string?)GetField(resp, 1),
                UserUniqueID = (uint?)GetField(resp, 2),
                UserStatus = (UserStatus?)(byte)GetField(resp, 3),
                UserType = (UserType?)(byte)GetField(resp, 4),
                CredentialRule = (CredentialRule?)(byte)GetField(resp, 5),
                Credentials = GetOptionalArrayField<Credential>(resp, 6),
                CreatorFabricIndex = (byte?)GetField(resp, 7),
                LastModifiedFabricIndex = (byte?)GetField(resp, 8),
                NextUserIndex = (ushort?)GetField(resp, 9),
            };
        }

        /// <summary>
        /// Clear User
        /// </summary>
        public async Task<bool> ClearUser(SecureSession session, ushort commandTimeoutMS, ushort userIndex) {
            ClearUserPayload requestFields = new ClearUserPayload() {
                UserIndex = userIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 29, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Credential
        /// </summary>
        public async Task<SetCredentialResponse?> SetCredential(SecureSession session, ushort commandTimeoutMS, DataOperationType operationType, Credential credential, byte[] credentialData, ushort? userIndex, UserStatus? userStatus, UserType? userType) {
            SetCredentialPayload requestFields = new SetCredentialPayload() {
                OperationType = operationType,
                Credential = credential,
                CredentialData = credentialData,
                UserIndex = userIndex,
                UserStatus = userStatus,
                UserType = userType,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 34, commandTimeoutMS, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new SetCredentialResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                UserIndex = (ushort?)GetField(resp, 1),
                NextCredentialIndex = (ushort?)GetField(resp, 2),
            };
        }

        /// <summary>
        /// Get Credential Status
        /// </summary>
        public async Task<GetCredentialStatusResponse?> GetCredentialStatus(SecureSession session, Credential credential) {
            GetCredentialStatusPayload requestFields = new GetCredentialStatusPayload() {
                Credential = credential,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 36, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetCredentialStatusResponse() {
                CredentialExists = (bool)GetField(resp, 0),
                UserIndex = (ushort?)GetField(resp, 1),
                CreatorFabricIndex = (byte?)GetField(resp, 2),
                LastModifiedFabricIndex = (byte?)GetField(resp, 3),
                NextCredentialIndex = (ushort?)GetField(resp, 4),
                CredentialData = (byte[]?)GetOptionalField(resp, 5),
            };
        }

        /// <summary>
        /// Clear Credential
        /// </summary>
        public async Task<bool> ClearCredential(SecureSession session, ushort commandTimeoutMS, Credential? credential) {
            ClearCredentialPayload requestFields = new ClearCredentialPayload() {
                Credential = credential,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 38, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Unbolt Door
        /// </summary>
        public async Task<bool> UnboltDoor(SecureSession session, ushort commandTimeoutMS, byte[]? pINCode) {
            UnboltDoorPayload requestFields = new UnboltDoorPayload() {
                PINCode = pINCode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 39, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Aliro Reader Config
        /// </summary>
        public async Task<bool> SetAliroReaderConfig(SecureSession session, ushort commandTimeoutMS, byte[] signingKey, byte[] verificationKey, byte[] groupIdentifier, byte[]? groupResolvingKey) {
            SetAliroReaderConfigPayload requestFields = new SetAliroReaderConfigPayload() {
                SigningKey = signingKey,
                VerificationKey = verificationKey,
                GroupIdentifier = groupIdentifier,
                GroupResolvingKey = groupResolvingKey,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 40, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Clear Aliro Reader Config
        /// </summary>
        public async Task<bool> ClearAliroReaderConfig(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 41, commandTimeoutMS);
            return ValidateResponse(resp);
        }
        #endregion Commands

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
        /// Lock State Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<LockStateEnum?> LockState { get; init; }

        /// <summary>
        /// Lock Type Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<LockTypeEnum> LockType { get; init; }

        /// <summary>
        /// Actuator Enabled Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool> ActuatorEnabled { get; init; }

        /// <summary>
        /// Door State Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<DoorStateEnum?> DoorState { get; init; }

        /// <summary>
        /// Door Open Events Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<uint> DoorOpenEvents { get; init; }

        /// <summary>
        /// Door Closed Events Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<uint> DoorClosedEvents { get; init; }

        /// <summary>
        /// Open Period Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> OpenPeriod { get; init; }

        /// <summary>
        /// Number Of Total Users Supported Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> NumberOfTotalUsersSupported { get; init; }

        /// <summary>
        /// Number Of PIN Users Supported Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> NumberOfPINUsersSupported { get; init; }

        /// <summary>
        /// Number Of RFID Users Supported Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> NumberOfRFIDUsersSupported { get; init; }

        /// <summary>
        /// Number Of Week Day Schedules Supported Per User Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfWeekDaySchedulesSupportedPerUser { get; init; }

        /// <summary>
        /// Number Of Year Day Schedules Supported Per User Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfYearDaySchedulesSupportedPerUser { get; init; }

        /// <summary>
        /// Number Of Holiday Schedules Supported Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfHolidaySchedulesSupported { get; init; }

        /// <summary>
        /// Max PIN Code Length Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> MaxPINCodeLength { get; init; }

        /// <summary>
        /// Min PIN Code Length Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> MinPINCodeLength { get; init; }

        /// <summary>
        /// Max RFID Code Length Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> MaxRFIDCodeLength { get; init; }

        /// <summary>
        /// Min RFID Code Length Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> MinRFIDCodeLength { get; init; }

        /// <summary>
        /// Credential Rules Support Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<CredentialRuleMask> CredentialRulesSupport { get; init; }

        /// <summary>
        /// Number Of Credentials Supported Per User Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfCredentialsSupportedPerUser { get; init; }

        /// <summary>
        /// Language Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<string> Language { get; init; }

        /// <summary>
        /// LED Settings Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte> LEDSettings { get; init; }

        /// <summary>
        /// Auto Relock Time Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<uint> AutoRelockTime { get; init; }

        /// <summary>
        /// Sound Volume Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte> SoundVolume { get; init; }

        /// <summary>
        /// Operating Mode Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<OperatingModeEnum> OperatingMode { get; init; }

        /// <summary>
        /// Supported Operating Modes Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<SupportedOperatingModesBitmap> SupportedOperatingModes { get; init; }

        /// <summary>
        /// Default Configuration Register Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<DefaultConfigurationRegisterBitmap> DefaultConfigurationRegister { get; init; }

        /// <summary>
        /// Enable Local Programming Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<bool> EnableLocalProgramming { get; init; }

        /// <summary>
        /// Enable One Touch Locking Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<bool> EnableOneTouchLocking { get; init; }

        /// <summary>
        /// Enable Inside Status LED Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<bool> EnableInsideStatusLED { get; init; }

        /// <summary>
        /// Enable Privacy Mode Button Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<bool> EnablePrivacyModeButton { get; init; }

        /// <summary>
        /// Local Programming Features Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<LocalProgrammingFeaturesBitmap> LocalProgrammingFeatures { get; init; }

        /// <summary>
        /// Wrong Code Entry Limit Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte> WrongCodeEntryLimit { get; init; }

        /// <summary>
        /// User Code Temporary Disable Time Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte> UserCodeTemporaryDisableTime { get; init; }

        /// <summary>
        /// Send PIN Over The Air Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<bool> SendPINOverTheAir { get; init; }

        /// <summary>
        /// Require PI Nfor Remote Operation Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<bool> RequirePINforRemoteOperation { get; init; }

        /// <summary>
        /// Expiring User Timeout Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> ExpiringUserTimeout { get; init; }

        /// <summary>
        /// Aliro Reader Verification Key Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte[]?> AliroReaderVerificationKey { get; init; }

        /// <summary>
        /// Aliro Reader Group Identifier Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte[]?> AliroReaderGroupIdentifier { get; init; }

        /// <summary>
        /// Aliro Reader Group Sub Identifier Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte[]> AliroReaderGroupSubIdentifier { get; init; }

        /// <summary>
        /// Aliro Expedited Transaction Supported Protocol Versions Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte[][]> AliroExpeditedTransactionSupportedProtocolVersions { get; init; }

        /// <summary>
        /// Aliro Group Resolving Key Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte[]?> AliroGroupResolvingKey { get; init; }

        /// <summary>
        /// Aliro Supported BLEUWB Protocol Versions Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte[][]> AliroSupportedBLEUWBProtocolVersions { get; init; }

        /// <summary>
        /// Aliro BLE Advertising Version Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> AliroBLEAdvertisingVersion { get; init; }

        /// <summary>
        /// Number Of Aliro Credential Issuer Keys Supported Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> NumberOfAliroCredentialIssuerKeysSupported { get; init; }

        /// <summary>
        /// Number Of Aliro Endpoint Keys Supported Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> NumberOfAliroEndpointKeysSupported { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Door Lock";
        }
    }
}