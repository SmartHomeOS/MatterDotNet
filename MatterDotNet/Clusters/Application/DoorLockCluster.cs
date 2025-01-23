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
        public DoorLock(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected DoorLock(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum DoorState : byte {
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
            Unspecified = 0,
            /// <summary>
            /// Lock programming PIN code was added, cleared, or modified.
            /// </summary>
            ProgrammingCode = 1,
            /// <summary>
            /// Lock user index was added, cleared, or modified.
            /// </summary>
            UserIndex = 2,
            /// <summary>
            /// Lock user week day schedule was added, cleared, or modified.
            /// </summary>
            WeekDaySchedule = 3,
            /// <summary>
            /// Lock user year day schedule was added, cleared, or modified.
            /// </summary>
            YearDaySchedule = 4,
            /// <summary>
            /// Lock holiday schedule was added, cleared, or modified.
            /// </summary>
            HolidaySchedule = 5,
            /// <summary>
            /// Lock user PIN code was added, cleared, or modified.
            /// </summary>
            PIN = 6,
            /// <summary>
            /// Lock user RFID code was added, cleared, or modified.
            /// </summary>
            RFID = 7,
            /// <summary>
            /// Lock user fingerprint was added, cleared, or modified.
            /// </summary>
            Fingerprint = 8,
            /// <summary>
            /// Lock user finger-vein information was added, cleared, or modified.
            /// </summary>
            FingerVein = 9,
            /// <summary>
            /// Lock user face information was added, cleared, or modified.
            /// </summary>
            Face = 10,
            /// <summary>
            /// An Aliro credential issuer key credential was added, cleared, or modified.
            /// </summary>
            AliroCredentialIssuerKey = 11,
            /// <summary>
            /// An Aliro endpoint key credential which can be evicted credential was added, cleared, or modified.
            /// </summary>
            AliroEvictableEndpointKey = 12,
            /// <summary>
            /// An Aliro endpoint key credential which cannot be evicted was added, cleared, or modified.
            /// </summary>
            AliroNonEvictableEndpointKey = 13,
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
        public enum OperatingMode : byte {
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
            Unspecified = 0,
            /// <summary>
            /// Lock/unlock operation came from manual operation (key, thumbturn, handle, etc).
            /// </summary>
            Manual = 1,
            /// <summary>
            /// Lock/unlock operation came from proprietary remote source (e.g. vendor app/cloud)
            /// </summary>
            ProprietaryRemote = 2,
            /// <summary>
            /// Lock/unlock operation came from keypad
            /// </summary>
            Keypad = 3,
            /// <summary>
            /// Lock/unlock operation came from lock automatically (e.g. relock timer)
            /// </summary>
            Auto = 4,
            /// <summary>
            /// Lock/unlock operation came from lock button (e.g. one touch or button)
            /// </summary>
            Button = 5,
            /// <summary>
            /// Lock/unlock operation came from lock due to a schedule
            /// </summary>
            Schedule = 6,
            /// <summary>
            /// Lock/unlock operation came from remote node
            /// </summary>
            Remote = 7,
            /// <summary>
            /// Lock/unlock operation came from RFID card
            /// </summary>
            RFID = 8,
            /// <summary>
            /// Lock/unlock operation came from biometric source (e.g. face, fingerprint/fingervein)
            /// </summary>
            Biometric = 9,
            /// <summary>
            /// Lock/unlock operation came from an interaction defined in ref_Aliro, or user change operation was a step-up credential provisioning as defined in ref_Aliro
            /// </summary>
            Aliro = 10,
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
        /// Dl Lock State
        /// </summary>
        public enum DlLockState : byte {
            NotFullyLocked = 0,
            Locked = 1,
            Unlocked = 2,
            Unlatched = 3,
        }

        /// <summary>
        /// Dl Lock Type
        /// </summary>
        public enum DlLockType : byte {
            DeadBolt = 0,
            Magnetic = 1,
            Other = 2,
            Mortise = 3,
            Rim = 4,
            LatchBolt = 5,
            CylindricalLock = 6,
            TubularLock = 7,
            InterconnectedLock = 8,
            DeadLatch = 9,
            DoorFurniture = 10,
            Eurocylinder = 11,
        }

        /// <summary>
        /// Dl Status
        /// </summary>
        public enum DlStatus : byte {
            Success = 0x00,
            Failure = 0x01,
            Duplicate = 0x02,
            Occupied = 0x03,
            InvalidField = 0x85,
            ResourceExhausted = 0x89,
            NotFound = 0x8B,
        }

        /// <summary>
        /// Door Lock Set Pin Or Id Status
        /// </summary>
        public enum DoorLockSetPinOrIdStatus : byte {
            Success = 0x00,
            GeneralFailure = 0x01,
            MemoryFull = 0x02,
            DuplicateCodeError = 0x03,
        }

        /// <summary>
        /// Door Lock Operation Event Code
        /// </summary>
        public enum DoorLockOperationEventCode : byte {
            UnknownOrMfgSpecific = 0x00,
            Lock = 0x01,
            Unlock = 0x02,
            LockInvalidPinOrId = 0x03,
            LockInvalidSchedule = 0x04,
            UnlockInvalidPinOrId = 0x05,
            UnlockInvalidSchedule = 0x06,
            OneTouchLock = 0x07,
            KeyLock = 0x08,
            KeyUnlock = 0x09,
            AutoLock = 0x0A,
            ScheduleLock = 0x0B,
            ScheduleUnlock = 0x0C,
            ManualLock = 0x0D,
            ManualUnlock = 0x0E,
        }

        /// <summary>
        /// Door Lock Programming Event Code
        /// </summary>
        public enum DoorLockProgrammingEventCode : byte {
            UnknownOrMfgSpecific = 0x00,
            MasterCodeChanged = 0x01,
            PinAdded = 0x02,
            PinDeleted = 0x03,
            PinChanged = 0x04,
            IdAdded = 0x05,
            IdDeleted = 0x06,
        }

        /// <summary>
        /// Door Lock User Status
        /// </summary>
        public enum DoorLockUserStatus : byte {
            Available = 0x00,
            OccupiedEnabled = 0x01,
            OccupiedDisabled = 0x03,
            NotSupported = 0xFF,
        }

        /// <summary>
        /// Door Lock User Type
        /// </summary>
        public enum DoorLockUserType : byte {
            Unrestricted = 0x00,
            YearDayScheduleUser = 0x01,
            WeekDayScheduleUser = 0x02,
            MasterUser = 0x03,
            NonAccessUser = 0x04,
            NotSupported = 0xFF,
        }

        /// <summary>
        /// Dl Credential Rule Mask
        /// </summary>
        [Flags]
        public enum DlCredentialRuleMask : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Single = 0x01,
            Dual = 0x02,
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
            None = 0,
            Sunday = 0x01,
            Monday = 0x02,
            Tuesday = 0x04,
            Wednesday = 0x08,
            Thursday = 0x10,
            Friday = 0x20,
            Saturday = 0x40,
        }

        /// <summary>
        /// Dl Credential Rules Support
        /// </summary>
        [Flags]
        public enum DlCredentialRulesSupport : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Single = 0x01,
            Dual = 0x02,
            Tri = 0x04,
        }

        /// <summary>
        /// Dl Supported Operating Modes
        /// </summary>
        [Flags]
        public enum DlSupportedOperatingModes : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Normal = 0x01,
            Vacation = 0x02,
            Privacy = 0x04,
            NoRemoteLockUnlock = 0x08,
            Passage = 0x10,
        }

        /// <summary>
        /// Dl Default Configuration Register
        /// </summary>
        [Flags]
        public enum DlDefaultConfigurationRegister : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            EnableLocalProgrammingEnabled = 0x01,
            KeypadInterfaceDefaultAccessEnabled = 0x02,
            RemoteInterfaceDefaultAccessIsEnabled = 0x04,
            SoundEnabled = 0x20,
            AutoRelockTimeSet = 0x40,
            LEDSettingsSet = 0x80,
        }

        /// <summary>
        /// Dl Local Programming Features
        /// </summary>
        [Flags]
        public enum DlLocalProgrammingFeatures : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            AddUsersCredentialsSchedulesLocally = 0x01,
            ModifyUsersCredentialsSchedulesLocally = 0x02,
            ClearUsersCredentialsSchedulesLocally = 0x04,
            AdjustLockSettingsLocally = 0x08,
        }

        /// <summary>
        /// Dl Keypad Operation Event Mask
        /// </summary>
        [Flags]
        public enum DlKeypadOperationEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Unknown = 0x01,
            Lock = 0x02,
            Unlock = 0x04,
            LockInvalidPIN = 0x08,
            LockInvalidSchedule = 0x10,
            UnlockInvalidCode = 0x20,
            UnlockInvalidSchedule = 0x40,
            NonAccessUserOpEvent = 0x80,
        }

        /// <summary>
        /// Dl Remote Operation Event Mask
        /// </summary>
        [Flags]
        public enum DlRemoteOperationEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Unknown = 0x01,
            Lock = 0x02,
            Unlock = 0x04,
            LockInvalidCode = 0x08,
            LockInvalidSchedule = 0x10,
            UnlockInvalidCode = 0x20,
            UnlockInvalidSchedule = 0x40,
        }

        /// <summary>
        /// Dl Manual Operation Event Mask
        /// </summary>
        [Flags]
        public enum DlManualOperationEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Unknown = 0x001,
            ThumbturnLock = 0x002,
            ThumbturnUnlock = 0x004,
            OneTouchLock = 0x008,
            KeyLock = 0x010,
            KeyUnlock = 0x020,
            AutoLock = 0x040,
            ScheduleLock = 0x080,
            ScheduleUnlock = 0x100,
            ManualLock = 0x200,
            ManualUnlock = 0x400,
        }

        /// <summary>
        /// Dl RFID Operation Event Mask
        /// </summary>
        [Flags]
        public enum DlRFIDOperationEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Unknown = 0x01,
            Lock = 0x02,
            Unlock = 0x04,
            LockInvalidRFID = 0x08,
            LockInvalidSchedule = 0x10,
            UnlockInvalidRFID = 0x20,
            UnlockInvalidSchedule = 0x40,
        }

        /// <summary>
        /// Dl Keypad Programming Event Mask
        /// </summary>
        [Flags]
        public enum DlKeypadProgrammingEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Unknown = 0x01,
            ProgrammingPINChanged = 0x02,
            PINAdded = 0x04,
            PINCleared = 0x08,
            PINChanged = 0x10,
        }

        /// <summary>
        /// Dl Remote Programming Event Mask
        /// </summary>
        [Flags]
        public enum DlRemoteProgrammingEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Unknown = 0x01,
            ProgrammingPINChanged = 0x02,
            PINAdded = 0x04,
            PINCleared = 0x08,
            PINChanged = 0x10,
            RFIDCodeAdded = 0x20,
            RFIDCodeCleared = 0x40,
        }

        /// <summary>
        /// Dl RFID Programming Event Mask
        /// </summary>
        [Flags]
        public enum DlRFIDProgrammingEventMask : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Unknown = 0x01,
            RFIDCodeAdded = 0x20,
            RFIDCodeCleared = 0x40,
        }

        /// <summary>
        /// Door Lock Day Of Week
        /// </summary>
        [Flags]
        public enum DoorLockDayOfWeek : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Sunday = 0x01,
            Monday = 0x02,
            Tuesday = 0x04,
            Wednesday = 0x08,
            Thursday = 0x10,
            Friday = 0x20,
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
            public required DlStatus Status { get; set; }
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
            public required DlStatus Status { get; set; }
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
            public required OperatingMode OperatingMode { get; set; }
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
            public required DlStatus Status { get; set; }
            public DateTime? LocalStartTime { get; set; }
            public DateTime? LocalEndTime { get; set; }
            public OperatingMode? OperatingMode { get; set; }
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
            public required DlStatus Status { get; set; }
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
                Status = (DlStatus)(byte)GetField(resp, 2),
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
                Status = (DlStatus)(byte)GetField(resp, 2),
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
        public async Task<bool> SetHolidaySchedule(SecureSession session, byte holidayIndex, DateTime localStartTime, DateTime localEndTime, OperatingMode operatingMode) {
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
                Status = (DlStatus)(byte)GetField(resp, 1),
                LocalStartTime = (DateTime?)GetOptionalField(resp, 2),
                LocalEndTime = (DateTime?)GetOptionalField(resp, 3),
                OperatingMode = (OperatingMode?)(byte?)GetOptionalField(resp, 4),
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
                Status = (DlStatus)(byte)GetField(resp, 0),
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
        /// Get the Lock State attribute
        /// </summary>
        public async Task<DlLockState?> GetLockState(SecureSession session) {
            return (DlLockState?)await GetEnumAttribute(session, 0, true);
        }

        /// <summary>
        /// Get the Lock Type attribute
        /// </summary>
        public async Task<DlLockType> GetLockType(SecureSession session) {
            return (DlLockType)await GetEnumAttribute(session, 1);
        }

        /// <summary>
        /// Get the Actuator Enabled attribute
        /// </summary>
        public async Task<bool> GetActuatorEnabled(SecureSession session) {
            return (bool)(dynamic?)(await GetAttribute(session, 2))!;
        }

        /// <summary>
        /// Get the Door State attribute
        /// </summary>
        public async Task<DoorState?> GetDoorState(SecureSession session) {
            return (DoorState?)await GetEnumAttribute(session, 3, true);
        }

        /// <summary>
        /// Get the Door Open Events attribute
        /// </summary>
        public async Task<uint> GetDoorOpenEvents(SecureSession session) {
            return (uint)(dynamic?)(await GetAttribute(session, 4))!;
        }

        /// <summary>
        /// Set the Door Open Events attribute
        /// </summary>
        public async Task SetDoorOpenEvents (SecureSession session, uint value) {
            await SetAttribute(session, 4, value);
        }

        /// <summary>
        /// Get the Door Closed Events attribute
        /// </summary>
        public async Task<uint> GetDoorClosedEvents(SecureSession session) {
            return (uint)(dynamic?)(await GetAttribute(session, 5))!;
        }

        /// <summary>
        /// Set the Door Closed Events attribute
        /// </summary>
        public async Task SetDoorClosedEvents (SecureSession session, uint value) {
            await SetAttribute(session, 5, value);
        }

        /// <summary>
        /// Get the Open Period attribute
        /// </summary>
        public async Task<ushort> GetOpenPeriod(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 6))!;
        }

        /// <summary>
        /// Set the Open Period attribute
        /// </summary>
        public async Task SetOpenPeriod (SecureSession session, ushort value) {
            await SetAttribute(session, 6, value);
        }

        /// <summary>
        /// Get the Number Of Total Users Supported attribute
        /// </summary>
        public async Task<ushort> GetNumberOfTotalUsersSupported(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 23) ?? 0;
        }

        /// <summary>
        /// Get the Number Of PIN Users Supported attribute
        /// </summary>
        public async Task<ushort> GetNumberOfPINUsersSupported(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 24) ?? 0;
        }

        /// <summary>
        /// Get the Number Of RFID Users Supported attribute
        /// </summary>
        public async Task<ushort> GetNumberOfRFIDUsersSupported(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 25) ?? 0;
        }

        /// <summary>
        /// Get the Number Of Week Day Schedules Supported Per User attribute
        /// </summary>
        public async Task<byte> GetNumberOfWeekDaySchedulesSupportedPerUser(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 32) ?? 0;
        }

        /// <summary>
        /// Get the Number Of Year Day Schedules Supported Per User attribute
        /// </summary>
        public async Task<byte> GetNumberOfYearDaySchedulesSupportedPerUser(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 33) ?? 0;
        }

        /// <summary>
        /// Get the Number Of Holiday Schedules Supported attribute
        /// </summary>
        public async Task<byte> GetNumberOfHolidaySchedulesSupported(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 34) ?? 0;
        }

        /// <summary>
        /// Get the Max PIN Code Length attribute
        /// </summary>
        public async Task<byte> GetMaxPINCodeLength(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 35))!;
        }

        /// <summary>
        /// Get the Min PIN Code Length attribute
        /// </summary>
        public async Task<byte> GetMinPINCodeLength(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 36))!;
        }

        /// <summary>
        /// Get the Max RFID Code Length attribute
        /// </summary>
        public async Task<byte> GetMaxRFIDCodeLength(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 37))!;
        }

        /// <summary>
        /// Get the Min RFID Code Length attribute
        /// </summary>
        public async Task<byte> GetMinRFIDCodeLength(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 38))!;
        }

        /// <summary>
        /// Get the Credential Rules Support attribute
        /// </summary>
        public async Task<DlCredentialRuleMask> GetCredentialRulesSupport(SecureSession session) {
            return (DlCredentialRuleMask)await GetEnumAttribute(session, 39);
        }

        /// <summary>
        /// Get the Number Of Credentials Supported Per User attribute
        /// </summary>
        public async Task<byte> GetNumberOfCredentialsSupportedPerUser(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 40) ?? 0;
        }

        /// <summary>
        /// Get the Language attribute
        /// </summary>
        public async Task<string> GetLanguage(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 51))!;
        }

        /// <summary>
        /// Set the Language attribute
        /// </summary>
        public async Task SetLanguage (SecureSession session, string value) {
            await SetAttribute(session, 51, value);
        }

        /// <summary>
        /// Get the LED Settings attribute
        /// </summary>
        public async Task<byte> GetLEDSettings(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 52) ?? 0;
        }

        /// <summary>
        /// Set the LED Settings attribute
        /// </summary>
        public async Task SetLEDSettings (SecureSession session, byte? value = 0) {
            await SetAttribute(session, 52, value);
        }

        /// <summary>
        /// Get the Auto Relock Time attribute
        /// </summary>
        public async Task<uint> GetAutoRelockTime(SecureSession session) {
            return (uint)(dynamic?)(await GetAttribute(session, 53))!;
        }

        /// <summary>
        /// Set the Auto Relock Time attribute
        /// </summary>
        public async Task SetAutoRelockTime (SecureSession session, uint value) {
            await SetAttribute(session, 53, value);
        }

        /// <summary>
        /// Get the Sound Volume attribute
        /// </summary>
        public async Task<byte> GetSoundVolume(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 54) ?? 0;
        }

        /// <summary>
        /// Set the Sound Volume attribute
        /// </summary>
        public async Task SetSoundVolume (SecureSession session, byte? value = 0) {
            await SetAttribute(session, 54, value);
        }

        /// <summary>
        /// Get the Operating Mode attribute
        /// </summary>
        public async Task<OperatingMode> GetOperatingMode(SecureSession session) {
            return (OperatingMode)await GetEnumAttribute(session, 55);
        }

        /// <summary>
        /// Set the Operating Mode attribute
        /// </summary>
        public async Task SetOperatingMode (SecureSession session, OperatingMode value) {
            await SetAttribute(session, 55, value);
        }

        /// <summary>
        /// Get the Supported Operating Modes attribute
        /// </summary>
        public async Task<DlSupportedOperatingModes> GetSupportedOperatingModes(SecureSession session) {
            return (DlSupportedOperatingModes)await GetEnumAttribute(session, 56);
        }

        /// <summary>
        /// Get the Default Configuration Register attribute
        /// </summary>
        public async Task<DlDefaultConfigurationRegister> GetDefaultConfigurationRegister(SecureSession session) {
            return (DlDefaultConfigurationRegister)await GetEnumAttribute(session, 57);
        }

        /// <summary>
        /// Get the Enable Local Programming attribute
        /// </summary>
        public async Task<bool> GetEnableLocalProgramming(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 64) ?? true;
        }

        /// <summary>
        /// Set the Enable Local Programming attribute
        /// </summary>
        public async Task SetEnableLocalProgramming (SecureSession session, bool? value = true) {
            await SetAttribute(session, 64, value);
        }

        /// <summary>
        /// Get the Enable One Touch Locking attribute
        /// </summary>
        public async Task<bool> GetEnableOneTouchLocking(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 65) ?? false;
        }

        /// <summary>
        /// Set the Enable One Touch Locking attribute
        /// </summary>
        public async Task SetEnableOneTouchLocking (SecureSession session, bool? value = false) {
            await SetAttribute(session, 65, value);
        }

        /// <summary>
        /// Get the Enable Inside Status LED attribute
        /// </summary>
        public async Task<bool> GetEnableInsideStatusLED(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 66) ?? false;
        }

        /// <summary>
        /// Set the Enable Inside Status LED attribute
        /// </summary>
        public async Task SetEnableInsideStatusLED (SecureSession session, bool? value = false) {
            await SetAttribute(session, 66, value);
        }

        /// <summary>
        /// Get the Enable Privacy Mode Button attribute
        /// </summary>
        public async Task<bool> GetEnablePrivacyModeButton(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 67) ?? false;
        }

        /// <summary>
        /// Set the Enable Privacy Mode Button attribute
        /// </summary>
        public async Task SetEnablePrivacyModeButton (SecureSession session, bool? value = false) {
            await SetAttribute(session, 67, value);
        }

        /// <summary>
        /// Get the Local Programming Features attribute
        /// </summary>
        public async Task<DlLocalProgrammingFeatures> GetLocalProgrammingFeatures(SecureSession session) {
            return (DlLocalProgrammingFeatures)await GetEnumAttribute(session, 68);
        }

        /// <summary>
        /// Set the Local Programming Features attribute
        /// </summary>
        public async Task SetLocalProgrammingFeatures (SecureSession session, DlLocalProgrammingFeatures value) {
            await SetAttribute(session, 68, value);
        }

        /// <summary>
        /// Get the Wrong Code Entry Limit attribute
        /// </summary>
        public async Task<byte> GetWrongCodeEntryLimit(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 72))!;
        }

        /// <summary>
        /// Set the Wrong Code Entry Limit attribute
        /// </summary>
        public async Task SetWrongCodeEntryLimit (SecureSession session, byte value) {
            await SetAttribute(session, 72, value);
        }

        /// <summary>
        /// Get the User Code Temporary Disable Time attribute
        /// </summary>
        public async Task<byte> GetUserCodeTemporaryDisableTime(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 73))!;
        }

        /// <summary>
        /// Set the User Code Temporary Disable Time attribute
        /// </summary>
        public async Task SetUserCodeTemporaryDisableTime (SecureSession session, byte value) {
            await SetAttribute(session, 73, value);
        }

        /// <summary>
        /// Get the Send PIN Over The Air attribute
        /// </summary>
        public async Task<bool> GetSendPINOverTheAir(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 80) ?? false;
        }

        /// <summary>
        /// Set the Send PIN Over The Air attribute
        /// </summary>
        public async Task SetSendPINOverTheAir (SecureSession session, bool? value = false) {
            await SetAttribute(session, 80, value);
        }

        /// <summary>
        /// Get the Require PI Nfor Remote Operation attribute
        /// </summary>
        public async Task<bool> GetRequirePINforRemoteOperation(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 81) ?? false;
        }

        /// <summary>
        /// Set the Require PI Nfor Remote Operation attribute
        /// </summary>
        public async Task SetRequirePINforRemoteOperation (SecureSession session, bool? value = false) {
            await SetAttribute(session, 81, value);
        }

        /// <summary>
        /// Get the Expiring User Timeout attribute
        /// </summary>
        public async Task<ushort> GetExpiringUserTimeout(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 83))!;
        }

        /// <summary>
        /// Set the Expiring User Timeout attribute
        /// </summary>
        public async Task SetExpiringUserTimeout (SecureSession session, ushort value) {
            await SetAttribute(session, 83, value);
        }

        /// <summary>
        /// Get the Aliro Reader Verification Key attribute
        /// </summary>
        public async Task<byte[]?> GetAliroReaderVerificationKey(SecureSession session) {
            return (byte[]?)(dynamic?)await GetAttribute(session, 296, true);
        }

        /// <summary>
        /// Get the Aliro Reader Group Identifier attribute
        /// </summary>
        public async Task<byte[]?> GetAliroReaderGroupIdentifier(SecureSession session) {
            return (byte[]?)(dynamic?)await GetAttribute(session, 297, true);
        }

        /// <summary>
        /// Get the Aliro Reader Group Sub Identifier attribute
        /// </summary>
        public async Task<byte[]> GetAliroReaderGroupSubIdentifier(SecureSession session) {
            return (byte[])(dynamic?)(await GetAttribute(session, 304))!;
        }

        /// <summary>
        /// Get the Aliro Expedited Transaction Supported Protocol Versions attribute
        /// </summary>
        public async Task<byte[][]> GetAliroExpeditedTransactionSupportedProtocolVersions(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 305))!);
            byte[][] list = new byte[reader.Count][];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetBytes(i, false, 16)!;
            return list;
        }

        /// <summary>
        /// Get the Aliro Group Resolving Key attribute
        /// </summary>
        public async Task<byte[]?> GetAliroGroupResolvingKey(SecureSession session) {
            return (byte[]?)(dynamic?)await GetAttribute(session, 306, true);
        }

        /// <summary>
        /// Get the Aliro Supported BLEUWB Protocol Versions attribute
        /// </summary>
        public async Task<byte[][]> GetAliroSupportedBLEUWBProtocolVersions(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 307))!);
            byte[][] list = new byte[reader.Count][];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetBytes(i, false, 16)!;
            return list;
        }

        /// <summary>
        /// Get the Aliro BLE Advertising Version attribute
        /// </summary>
        public async Task<byte> GetAliroBLEAdvertisingVersion(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 308))!;
        }

        /// <summary>
        /// Get the Number Of Aliro Credential Issuer Keys Supported attribute
        /// </summary>
        public async Task<ushort> GetNumberOfAliroCredentialIssuerKeysSupported(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 309))!;
        }

        /// <summary>
        /// Get the Number Of Aliro Endpoint Keys Supported attribute
        /// </summary>
        public async Task<ushort> GetNumberOfAliroEndpointKeysSupported(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 310))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Door Lock";
        }
    }
}