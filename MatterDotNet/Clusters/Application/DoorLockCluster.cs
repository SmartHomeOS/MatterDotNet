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
using MatterDotNet.Protocol.Payloads.Status;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Door Lock Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 7)]
    public class DoorLockCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0101;

        /// <summary>
        /// Door Lock Cluster
        /// </summary>
        public DoorLockCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected DoorLockCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
            CredentialOverTheAirAccess = 128,
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
            Unbolting = 4096,
        }

        /// <summary>
        /// Alarm Code
        /// </summary>
        public enum AlarmCodeEnum {
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
        public enum CredentialRuleEnum {
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
        public enum CredentialTypeEnum {
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
        }

        /// <summary>
        /// Data Operation Type
        /// </summary>
        public enum DataOperationTypeEnum {
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
        public enum DoorStateEnum {
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
        /// Event Source
        /// </summary>
        public enum EventSourceEnum {
            /// <summary>
            /// Event source is keypad
            /// </summary>
            Keypad = 0,
            /// <summary>
            /// Event source is remote
            /// </summary>
            Remote = 1,
            /// <summary>
            /// Event source is manual
            /// </summary>
            Manual = 2,
            /// <summary>
            /// Event source is RFID
            /// </summary>
            RFID = 3,
            /// <summary>
            /// Event source is unknown
            /// </summary>
            Indeterminate = 255,
        }

        /// <summary>
        /// Event Type
        /// </summary>
        public enum EventTypeEnum {
            /// <summary>
            /// Event type is operation
            /// </summary>
            Operation = 0,
            /// <summary>
            /// Event type is programming
            /// </summary>
            Programming = 1,
            /// <summary>
            /// Event type is alarm
            /// </summary>
            Alarm = 2,
        }

        /// <summary>
        /// LED Setting
        /// </summary>
        public enum LEDSettingEnum {
            /// <summary>
            /// Never use LED for signalization
            /// </summary>
            NoLEDSignal = 0,
            /// <summary>
            /// Use LED signalization except for access allowed events
            /// </summary>
            NoLEDSignalAccessAllowed = 1,
            /// <summary>
            /// Use LED signalization for all events
            /// </summary>
            LEDSignalAll = 2,
        }

        /// <summary>
        /// Lock Data Type
        /// </summary>
        public enum LockDataTypeEnum {
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
        }

        /// <summary>
        /// Lock Operation Type
        /// </summary>
        public enum LockOperationTypeEnum {
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
        /// Lock State
        /// </summary>
        public enum LockStateEnum {
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
        public enum LockTypeEnum {
            /// <summary>
            /// Physical lock type is dead bolt
            /// </summary>
            DeadBolt = 0,
            /// <summary>
            /// Physical lock type is magnetic
            /// </summary>
            Magnetic = 1,
            /// <summary>
            /// Physical lock type is other
            /// </summary>
            Other = 2,
            /// <summary>
            /// Physical lock type is mortise
            /// </summary>
            Mortise = 3,
            /// <summary>
            /// Physical lock type is rim
            /// </summary>
            Rim = 4,
            /// <summary>
            /// Physical lock type is latch bolt
            /// </summary>
            LatchBolt = 5,
            /// <summary>
            /// Physical lock type is cylindrical lock
            /// </summary>
            CylindricalLock = 6,
            /// <summary>
            /// Physical lock type is tubular lock
            /// </summary>
            TubularLock = 7,
            /// <summary>
            /// Physical lock type is interconnected lock
            /// </summary>
            InterconnectedLock = 8,
            /// <summary>
            /// Physical lock type is dead latch
            /// </summary>
            DeadLatch = 9,
            /// <summary>
            /// Physical lock type is door furniture
            /// </summary>
            DoorFurniture = 10,
            /// <summary>
            /// Physical lock type is euro cylinder
            /// </summary>
            Eurocylinder = 11,
        }

        /// <summary>
        /// Operating Mode
        /// </summary>
        public enum OperatingModeEnum {
            Normal = 0,
            Vacation = 1,
            Privacy = 2,
            NoRemoteLockUnlock = 3,
            Passage = 4,
        }

        /// <summary>
        /// Operation Error
        /// </summary>
        public enum OperationErrorEnum {
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
        /// Operation Source
        /// </summary>
        public enum OperationSourceEnum {
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
        }

        /// <summary>
        /// Sound Volume
        /// </summary>
        public enum SoundVolumeEnum {
            /// <summary>
            /// Silent Mode
            /// </summary>
            Silent = 0,
            /// <summary>
            /// Low Volume
            /// </summary>
            Low = 1,
            /// <summary>
            /// High Volume
            /// </summary>
            High = 2,
            /// <summary>
            /// Medium Volume
            /// </summary>
            Medium = 3,
        }

        /// <summary>
        /// User Status
        /// </summary>
        public enum UserStatusEnum {
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
        public enum UserTypeEnum {
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
        /// Alarm Mask Bitmap
        /// </summary>
        [Flags]
        public enum AlarmMaskBitmap {
            /// <summary>
            /// Locking Mechanism Jammed
            /// </summary>
            LockJammed = 1,
            /// <summary>
            /// Lock Reset to Factory Defaults
            /// </summary>
            LockFactoryReset = 2,
            /// <summary>
            /// Reserved
            /// </summary>
            N_A = 4,
            /// <summary>
            /// RF Module Power Cycled
            /// </summary>
            LockRadioPowerCycled = 8,
            /// <summary>
            /// Tamper Alarm - wrong code entry limit
            /// </summary>
            WrongCodeEntryLimit = 16,
            /// <summary>
            /// Tamper Alarm - front escutcheon removed from main
            /// </summary>
            FrontEscutcheonRemoved = 32,
            /// <summary>
            /// Forced Door Open under Door Locked Condition
            /// </summary>
            DoorForcedOpen = 64,
        }

        /// <summary>
        /// Configuration Register Bitmap
        /// </summary>
        [Flags]
        public enum ConfigurationRegisterBitmap {
            /// <summary>
            /// The state of local programming functionality
            /// </summary>
            LocalProgramming = 1,
            /// <summary>
            /// The state of the keypad interface
            /// </summary>
            KeypadInterface = 2,
            /// <summary>
            /// The state of the remote interface
            /// </summary>
            RemoteInterface = 4,
            /// <summary>
            /// Sound volume is set to Silent value
            /// </summary>
            SoundVolume = 32,
            /// <summary>
            /// Auto relock time it set to 0
            /// </summary>
            AutoRelockTime = 64,
            /// <summary>
            /// LEDs is disabled
            /// </summary>
            LEDSettings = 128,
        }

        /// <summary>
        /// Credential Rules Bitmap
        /// </summary>
        [Flags]
        public enum CredentialRulesBitmap {
            /// <summary>
            /// Only one credential is required for lock operation
            /// </summary>
            Single = 1,
            /// <summary>
            /// Any two credentials are required for lock operation
            /// </summary>
            Dual = 2,
            /// <summary>
            /// Any three credentials are required for lock operation
            /// </summary>
            Tri = 4,
        }

        /// <summary>
        /// Days Mask Bitmap
        /// </summary>
        [Flags]
        public enum DaysMaskBitmap {
            /// <summary>
            /// Schedule is applied on Sunday
            /// </summary>
            Sunday = 1,
            /// <summary>
            /// Schedule is applied on Monday
            /// </summary>
            Monday = 2,
            /// <summary>
            /// Schedule is applied on Tuesday
            /// </summary>
            Tuesday = 4,
            /// <summary>
            /// Schedule is applied on Wednesday
            /// </summary>
            Wednesday = 8,
            /// <summary>
            /// Schedule is applied on Thursday
            /// </summary>
            Thursday = 16,
            /// <summary>
            /// Schedule is applied on Friday
            /// </summary>
            Friday = 32,
            /// <summary>
            /// Schedule is applied on Saturday
            /// </summary>
            Saturday = 64,
        }

        /// <summary>
        /// Event Mask Bitmap
        /// </summary>
        [Flags]
        public enum EventMaskBitmap {
            /// <summary>
            /// State of bit 0
            /// </summary>
            Bit0 = 1,
            /// <summary>
            /// State of bit 1
            /// </summary>
            Bit1 = 2,
            /// <summary>
            /// State of bit 2
            /// </summary>
            Bit2 = 4,
            /// <summary>
            /// State of bit 3
            /// </summary>
            Bit3 = 8,
            /// <summary>
            /// State of bit 4
            /// </summary>
            Bit4 = 16,
            /// <summary>
            /// State of bit 5
            /// </summary>
            Bit5 = 32,
            /// <summary>
            /// State of bit 6
            /// </summary>
            Bit6 = 64,
            /// <summary>
            /// State of bit 7
            /// </summary>
            Bit7 = 128,
            /// <summary>
            /// State of bit 8
            /// </summary>
            Bit8 = 256,
            /// <summary>
            /// State of bit 9
            /// </summary>
            Bit9 = 512,
            /// <summary>
            /// State of bit 10
            /// </summary>
            Bit10 = 1024,
            /// <summary>
            /// State of bit 11
            /// </summary>
            Bit11 = 2048,
            /// <summary>
            /// State of bit 12
            /// </summary>
            Bit12 = 4096,
            /// <summary>
            /// State of bit 13
            /// </summary>
            Bit13 = 8192,
            /// <summary>
            /// State of bit 14
            /// </summary>
            Bit14 = 16384,
            /// <summary>
            /// State of bit 15
            /// </summary>
            Bit15 = 32768,
        }

        /// <summary>
        /// Local Programming Features Bitmap
        /// </summary>
        [Flags]
        public enum LocalProgrammingFeaturesBitmap {
            /// <summary>
            /// The state of the ability to add users, credentials or schedules on the device
            /// </summary>
            AddUsersCredentialsSchedules = 1,
            /// <summary>
            /// The state of the ability to modify users, credentials or schedules on the device
            /// </summary>
            ModifyUsersCredentialsSchedules = 2,
            /// <summary>
            /// The state of the ability to clear users, credentials or schedules on the device
            /// </summary>
            ClearUsersCredentialsSchedules = 4,
            /// <summary>
            /// The state of the ability to adjust settings on the device
            /// </summary>
            AdjustSettings = 8,
        }

        /// <summary>
        /// Operating Modes Bitmap
        /// </summary>
        [Flags]
        public enum OperatingModesBitmap {
            /// <summary>
            /// Normal operation mode
            /// </summary>
            Normal = 1,
            /// <summary>
            /// Vacation operation mode
            /// </summary>
            Vacation = 2,
            /// <summary>
            /// Privacy operation mode
            /// </summary>
            Privacy = 4,
            /// <summary>
            /// No remote lock and unlock operation mode
            /// </summary>
            NoRemoteLockUnlock = 8,
            /// <summary>
            /// Passage operation mode
            /// </summary>
            Passage = 16,
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
                CredentialType = (CredentialTypeEnum)reader.GetUShort(0)!.Value;
                CredentialIndex = reader.GetUShort(1)!.Value;
            }
            public required CredentialTypeEnum CredentialType { get; set; }
            public required ushort CredentialIndex { get; set; } = 0;
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

        private record GetLogRecordPayload : TLVPayload {
            public required ushort LogIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, LogIndex);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Log Record Response - Reply from server
        /// </summary>
        public struct GetLogRecordResponse() {
            public required ushort LogEntryID { get; set; }
            public required DateTime Timestamp { get; set; }
            public required EventTypeEnum EventType { get; set; }
            public required EventSourceEnum Source { get; set; }
            public required byte EventID { get; set; }
            public required ushort UserID { get; set; }
            public required byte[] PIN { get; set; }
        }

        private record SetPINCodePayload : TLVPayload {
            public required ushort UserID { get; set; }
            public required UserStatusEnum? UserStatus { get; set; } = UserStatusEnum.OccupiedEnabled;
            public required UserTypeEnum? UserType { get; set; } = UserTypeEnum.UnrestrictedUser;
            public required byte[] PIN { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, UserID);
                writer.WriteUShort(1, (ushort?)UserStatus);
                writer.WriteUShort(2, (ushort?)UserType);
                writer.WriteBytes(3, PIN);
                writer.EndContainer();
            }
        }

        private record GetPINCodePayload : TLVPayload {
            public required ushort UserID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, UserID);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get PIN Code Response - Reply from server
        /// </summary>
        public struct GetPINCodeResponse() {
            public required ushort UserID { get; set; }
            public required UserStatusEnum? UserStatus { get; set; } = UserStatusEnum.Available;
            public required UserTypeEnum? UserType { get; set; }
            public required byte[]? PINCode { get; set; } = [];
        }

        private record ClearPINCodePayload : TLVPayload {
            public required ushort PINSlotIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, PINSlotIndex, ushort.MaxValue, 1);
                writer.EndContainer();
            }
        }

        private record SetUserStatusPayload : TLVPayload {
            public required ushort UserID { get; set; }
            public required UserStatusEnum UserStatus { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, UserID);
                writer.WriteUShort(1, (ushort)UserStatus);
                writer.EndContainer();
            }
        }

        private record GetUserStatusPayload : TLVPayload {
            public required ushort UserID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, UserID);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get User Status Response - Reply from server
        /// </summary>
        public struct GetUserStatusResponse() {
            public required ushort UserID { get; set; }
            public required UserStatusEnum UserStatus { get; set; }
        }

        private record SetWeekDaySchedulePayload : TLVPayload {
            public required byte WeekDayIndex { get; set; }
            public required ushort UserIndex { get; set; }
            public required DaysMaskBitmap DaysMask { get; set; }
            public required byte StartHour { get; set; }
            public required byte StartMinute { get; set; }
            public required byte EndHour { get; set; }
            public required byte EndMinute { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, WeekDayIndex, byte.MaxValue, 1);
                writer.WriteUShort(1, UserIndex, ushort.MaxValue, 1);
                writer.WriteUShort(2, (ushort)DaysMask);
                writer.WriteByte(3, StartHour, 23);
                writer.WriteByte(4, StartMinute, 59);
                writer.WriteByte(5, EndHour, 23);
                writer.WriteByte(6, EndMinute, 59);
                writer.EndContainer();
            }
        }

        private record GetWeekDaySchedulePayload : TLVPayload {
            public required byte WeekDayIndex { get; set; }
            public required ushort UserIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, WeekDayIndex, byte.MaxValue, 1);
                writer.WriteUShort(1, UserIndex, ushort.MaxValue, 1);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Week Day Schedule Response - Reply from server
        /// </summary>
        public struct GetWeekDayScheduleResponse() {
            public required byte WeekDayIndex { get; set; }
            public required ushort UserIndex { get; set; }
            public required IMStatusCode Status { get; set; } = IMStatusCode.SUCCESS;
            public DaysMaskBitmap? DaysMask { get; set; }
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
                writer.WriteByte(0, WeekDayIndex, byte.MaxValue, 1);
                writer.WriteUShort(1, UserIndex, ushort.MaxValue, 1);
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
                writer.WriteByte(0, YearDayIndex, byte.MaxValue, 1);
                writer.WriteUShort(1, UserIndex, ushort.MaxValue, 1);
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
                writer.WriteByte(0, YearDayIndex, byte.MaxValue, 1);
                writer.WriteUShort(1, UserIndex, ushort.MaxValue, 1);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Year Day Schedule Response - Reply from server
        /// </summary>
        public struct GetYearDayScheduleResponse() {
            public required byte YearDayIndex { get; set; }
            public required ushort UserIndex { get; set; }
            public required IMStatusCode Status { get; set; } = IMStatusCode.SUCCESS;
            public DateTime? LocalStartTime { get; set; }
            public DateTime? LocalEndTime { get; set; }
        }

        private record ClearYearDaySchedulePayload : TLVPayload {
            public required byte YearDayIndex { get; set; }
            public required ushort UserIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, YearDayIndex, byte.MaxValue, 1);
                writer.WriteUShort(1, UserIndex, ushort.MaxValue, 1);
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
                writer.WriteByte(0, HolidayIndex, byte.MaxValue, 1);
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
                writer.WriteByte(0, HolidayIndex, byte.MaxValue, 1);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Holiday Schedule Response - Reply from server
        /// </summary>
        public struct GetHolidayScheduleResponse() {
            public required byte HolidayIndex { get; set; }
            public required IMStatusCode Status { get; set; } = IMStatusCode.SUCCESS;
            public DateTime? LocalStartTime { get; set; }
            public DateTime? LocalEndTime { get; set; }
            public OperatingModeEnum? OperatingMode { get; set; }
        }

        private record ClearHolidaySchedulePayload : TLVPayload {
            public required byte HolidayIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, HolidayIndex, byte.MaxValue, 1);
                writer.EndContainer();
            }
        }

        private record SetUserTypePayload : TLVPayload {
            public required ushort UserID { get; set; }
            public required UserTypeEnum UserType { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, UserID);
                writer.WriteUShort(1, (ushort)UserType);
                writer.EndContainer();
            }
        }

        private record GetUserTypePayload : TLVPayload {
            public required ushort UserID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, UserID);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get User Type Response - Reply from server
        /// </summary>
        public struct GetUserTypeResponse() {
            public required ushort UserID { get; set; }
            public required UserTypeEnum UserType { get; set; }
        }

        private record SetRFIDCodePayload : TLVPayload {
            public required ushort UserID { get; set; }
            public required UserStatusEnum? UserStatus { get; set; } = UserStatusEnum.OccupiedEnabled;
            public required UserTypeEnum? UserType { get; set; } = UserTypeEnum.UnrestrictedUser;
            public required byte[] RFIDCode { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, UserID);
                writer.WriteUShort(1, (ushort?)UserStatus);
                writer.WriteUShort(2, (ushort?)UserType);
                writer.WriteBytes(3, RFIDCode);
                writer.EndContainer();
            }
        }

        private record GetRFIDCodePayload : TLVPayload {
            public required ushort UserID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, UserID);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get RFID Code Response - Reply from server
        /// </summary>
        public struct GetRFIDCodeResponse() {
            public required ushort UserID { get; set; }
            public required UserStatusEnum? UserStatus { get; set; } = UserStatusEnum.Available;
            public required UserTypeEnum? UserType { get; set; }
            public required byte[]? RFIDCode { get; set; } = [];
        }

        private record ClearRFIDCodePayload : TLVPayload {
            public required ushort RFIDSlotIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, RFIDSlotIndex, ushort.MaxValue, 1);
                writer.EndContainer();
            }
        }

        private record SetUserPayload : TLVPayload {
            public required DataOperationTypeEnum OperationType { get; set; }
            public required ushort UserIndex { get; set; }
            public required string? UserName { get; set; } = "";
            public required uint? UserUniqueID { get; set; } = 0xFFFFFFFF;
            public required UserStatusEnum? UserStatus { get; set; } = UserStatusEnum.OccupiedEnabled;
            public required UserTypeEnum? UserType { get; set; } = UserTypeEnum.UnrestrictedUser;
            public required CredentialRuleEnum? CredentialRule { get; set; } = CredentialRuleEnum.Single;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)OperationType);
                writer.WriteUShort(1, UserIndex, ushort.MaxValue, 1);
                writer.WriteString(2, UserName, 10);
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
                writer.WriteUShort(0, UserIndex, ushort.MaxValue, 1);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get User Response - Reply from server
        /// </summary>
        public struct GetUserResponse() {
            public required ushort UserIndex { get; set; }
            public required string? UserName { get; set; } = "";
            public required uint? UserUniqueID { get; set; } = 0;
            public required UserStatusEnum? UserStatus { get; set; } = UserStatusEnum.Available;
            public required UserTypeEnum? UserType { get; set; } = UserTypeEnum.UnrestrictedUser;
            public required CredentialRuleEnum? CredentialRule { get; set; } = CredentialRuleEnum.Single;
            public required Credential[]? Credentials { get; set; }
            public required byte? CreatorFabricIndex { get; set; }
            public required byte? LastModifiedFabricIndex { get; set; }
            public required ushort? NextUserIndex { get; set; }
        }

        private record ClearUserPayload : TLVPayload {
            public required ushort UserIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, UserIndex, ushort.MaxValue, 1);
                writer.EndContainer();
            }
        }

        private record SetCredentialPayload : TLVPayload {
            public required DataOperationTypeEnum OperationType { get; set; }
            public required Credential Credential { get; set; }
            public required byte[] CredentialData { get; set; }
            public required ushort? UserIndex { get; set; }
            public required UserStatusEnum? UserStatus { get; set; } = UserStatusEnum.OccupiedEnabled;
            public required UserTypeEnum? UserType { get; set; } = UserTypeEnum.UnrestrictedUser;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)OperationType);
                Credential.Serialize(writer, 1);
                writer.WriteBytes(2, CredentialData);
                writer.WriteUShort(3, UserIndex, ushort.MaxValue, 1);
                writer.WriteUShort(4, (ushort?)UserStatus);
                writer.WriteUShort(5, (ushort?)UserType);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Set Credential Response - Reply from server
        /// </summary>
        public struct SetCredentialResponse() {
            public required IMStatusCode Status { get; set; }
            public required ushort? UserIndex { get; set; } = 0;
            public ushort? NextCredentialIndex { get; set; }
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
            public ushort? NextCredentialIndex { get; set; }
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
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Lock Door
        /// </summary>
        public async Task<bool> LockDoor(SecureSession session, ushort commandTimeoutMS, byte[]? PINCode) {
            LockDoorPayload requestFields = new LockDoorPayload() {
                PINCode = PINCode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Unlock Door
        /// </summary>
        public async Task<bool> UnlockDoor(SecureSession session, ushort commandTimeoutMS, byte[]? PINCode) {
            UnlockDoorPayload requestFields = new UnlockDoorPayload() {
                PINCode = PINCode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x01, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Toggle
        /// </summary>
        public async Task<bool> Toggle(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x02);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Unlock With Timeout
        /// </summary>
        public async Task<bool> UnlockWithTimeout(SecureSession session, ushort commandTimeoutMS, ushort Timeout, byte[]? PINCode) {
            UnlockWithTimeoutPayload requestFields = new UnlockWithTimeoutPayload() {
                Timeout = Timeout,
                PINCode = PINCode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x03, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Log Record
        /// </summary>
        public async Task<GetLogRecordResponse?> GetLogRecord(SecureSession session, ushort LogIndex) {
            GetLogRecordPayload requestFields = new GetLogRecordPayload() {
                LogIndex = LogIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetLogRecordResponse() {
                LogEntryID = (ushort)GetField(resp, 0),
                Timestamp = (DateTime)GetField(resp, 1),
                EventType = (EventTypeEnum)(byte)GetField(resp, 2),
                Source = (EventSourceEnum)(byte)GetField(resp, 3),
                EventID = (byte)GetField(resp, 4),
                UserID = (ushort)GetField(resp, 5),
                PIN = (byte[])GetField(resp, 6),
            };
        }

        /// <summary>
        /// Set PIN Code
        /// </summary>
        public async Task<bool> SetPINCode(SecureSession session, ushort commandTimeoutMS, ushort UserID, UserStatusEnum UserStatus, UserTypeEnum UserType, byte[] PIN) {
            SetPINCodePayload requestFields = new SetPINCodePayload() {
                UserID = UserID,
                UserStatus = UserStatus,
                UserType = UserType,
                PIN = PIN,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get PIN Code
        /// </summary>
        public async Task<GetPINCodeResponse?> GetPINCode(SecureSession session, ushort UserID) {
            GetPINCodePayload requestFields = new GetPINCodePayload() {
                UserID = UserID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetPINCodeResponse() {
                UserID = (ushort)GetField(resp, 0),
                UserStatus = (UserStatusEnum?)(byte)GetField(resp, 1),
                UserType = (UserTypeEnum?)(byte)GetField(resp, 2),
                PINCode = (byte[]?)GetField(resp, 3),
            };
        }

        /// <summary>
        /// Clear PIN Code
        /// </summary>
        public async Task<bool> ClearPINCode(SecureSession session, ushort commandTimeoutMS, ushort PINSlotIndex) {
            ClearPINCodePayload requestFields = new ClearPINCodePayload() {
                PINSlotIndex = PINSlotIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x07, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Clear All PIN Codes
        /// </summary>
        public async Task<bool> ClearAllPINCodes(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x08);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set User Status
        /// </summary>
        public async Task<bool> SetUserStatus(SecureSession session, ushort UserID, UserStatusEnum UserStatus) {
            SetUserStatusPayload requestFields = new SetUserStatusPayload() {
                UserID = UserID,
                UserStatus = UserStatus,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x09, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get User Status
        /// </summary>
        public async Task<GetUserStatusResponse?> GetUserStatus(SecureSession session, ushort UserID) {
            GetUserStatusPayload requestFields = new GetUserStatusPayload() {
                UserID = UserID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0A, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetUserStatusResponse() {
                UserID = (ushort)GetField(resp, 0),
                UserStatus = (UserStatusEnum)(byte)GetField(resp, 1),
            };
        }

        /// <summary>
        /// Set Week Day Schedule
        /// </summary>
        public async Task<bool> SetWeekDaySchedule(SecureSession session, byte WeekDayIndex, ushort UserIndex, DaysMaskBitmap DaysMask, byte StartHour, byte StartMinute, byte EndHour, byte EndMinute) {
            SetWeekDaySchedulePayload requestFields = new SetWeekDaySchedulePayload() {
                WeekDayIndex = WeekDayIndex,
                UserIndex = UserIndex,
                DaysMask = DaysMask,
                StartHour = StartHour,
                StartMinute = StartMinute,
                EndHour = EndHour,
                EndMinute = EndMinute,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0B, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Week Day Schedule
        /// </summary>
        public async Task<GetWeekDayScheduleResponse?> GetWeekDaySchedule(SecureSession session, byte WeekDayIndex, ushort UserIndex) {
            GetWeekDaySchedulePayload requestFields = new GetWeekDaySchedulePayload() {
                WeekDayIndex = WeekDayIndex,
                UserIndex = UserIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0C, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetWeekDayScheduleResponse() {
                WeekDayIndex = (byte)GetField(resp, 0),
                UserIndex = (ushort)GetField(resp, 1),
                Status = (IMStatusCode)(byte)GetField(resp, 2),
                DaysMask = (DaysMaskBitmap?)(byte?)GetOptionalField(resp, 3),
                StartHour = (byte?)GetOptionalField(resp, 4),
                StartMinute = (byte?)GetOptionalField(resp, 5),
                EndHour = (byte?)GetOptionalField(resp, 6),
                EndMinute = (byte?)GetOptionalField(resp, 7),
            };
        }

        /// <summary>
        /// Clear Week Day Schedule
        /// </summary>
        public async Task<bool> ClearWeekDaySchedule(SecureSession session, byte WeekDayIndex, ushort UserIndex) {
            ClearWeekDaySchedulePayload requestFields = new ClearWeekDaySchedulePayload() {
                WeekDayIndex = WeekDayIndex,
                UserIndex = UserIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0D, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Year Day Schedule
        /// </summary>
        public async Task<bool> SetYearDaySchedule(SecureSession session, byte YearDayIndex, ushort UserIndex, DateTime LocalStartTime, DateTime LocalEndTime) {
            SetYearDaySchedulePayload requestFields = new SetYearDaySchedulePayload() {
                YearDayIndex = YearDayIndex,
                UserIndex = UserIndex,
                LocalStartTime = LocalStartTime,
                LocalEndTime = LocalEndTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0E, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Year Day Schedule
        /// </summary>
        public async Task<GetYearDayScheduleResponse?> GetYearDaySchedule(SecureSession session, byte YearDayIndex, ushort UserIndex) {
            GetYearDaySchedulePayload requestFields = new GetYearDaySchedulePayload() {
                YearDayIndex = YearDayIndex,
                UserIndex = UserIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0F, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetYearDayScheduleResponse() {
                YearDayIndex = (byte)GetField(resp, 0),
                UserIndex = (ushort)GetField(resp, 1),
                Status = (IMStatusCode)(byte)GetField(resp, 2),
                LocalStartTime = (DateTime?)GetOptionalField(resp, 2),
                LocalEndTime = (DateTime?)GetOptionalField(resp, 3),
            };
        }

        /// <summary>
        /// Clear Year Day Schedule
        /// </summary>
        public async Task<bool> ClearYearDaySchedule(SecureSession session, byte YearDayIndex, ushort UserIndex) {
            ClearYearDaySchedulePayload requestFields = new ClearYearDaySchedulePayload() {
                YearDayIndex = YearDayIndex,
                UserIndex = UserIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x10, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Holiday Schedule
        /// </summary>
        public async Task<bool> SetHolidaySchedule(SecureSession session, byte HolidayIndex, DateTime LocalStartTime, DateTime LocalEndTime, OperatingModeEnum OperatingMode) {
            SetHolidaySchedulePayload requestFields = new SetHolidaySchedulePayload() {
                HolidayIndex = HolidayIndex,
                LocalStartTime = LocalStartTime,
                LocalEndTime = LocalEndTime,
                OperatingMode = OperatingMode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x11, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Holiday Schedule
        /// </summary>
        public async Task<GetHolidayScheduleResponse?> GetHolidaySchedule(SecureSession session, byte HolidayIndex) {
            GetHolidaySchedulePayload requestFields = new GetHolidaySchedulePayload() {
                HolidayIndex = HolidayIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x12, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetHolidayScheduleResponse() {
                HolidayIndex = (byte)GetField(resp, 0),
                Status = (IMStatusCode)(byte)GetField(resp, 1),
                LocalStartTime = (DateTime?)GetOptionalField(resp, 2),
                LocalEndTime = (DateTime?)GetOptionalField(resp, 3),
                OperatingMode = (OperatingModeEnum?)(byte?)GetOptionalField(resp, 4),
            };
        }

        /// <summary>
        /// Clear Holiday Schedule
        /// </summary>
        public async Task<bool> ClearHolidaySchedule(SecureSession session, byte HolidayIndex) {
            ClearHolidaySchedulePayload requestFields = new ClearHolidaySchedulePayload() {
                HolidayIndex = HolidayIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x13, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set User Type
        /// </summary>
        public async Task<bool> SetUserType(SecureSession session, ushort UserID, UserTypeEnum UserType) {
            SetUserTypePayload requestFields = new SetUserTypePayload() {
                UserID = UserID,
                UserType = UserType,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x14, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get User Type
        /// </summary>
        public async Task<GetUserTypeResponse?> GetUserType(SecureSession session, ushort UserID) {
            GetUserTypePayload requestFields = new GetUserTypePayload() {
                UserID = UserID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x15, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetUserTypeResponse() {
                UserID = (ushort)GetField(resp, 0),
                UserType = (UserTypeEnum)(byte)GetField(resp, 1),
            };
        }

        /// <summary>
        /// Set RFID Code
        /// </summary>
        public async Task<bool> SetRFIDCode(SecureSession session, ushort commandTimeoutMS, ushort UserID, UserStatusEnum UserStatus, UserTypeEnum UserType, byte[] RFIDCode) {
            SetRFIDCodePayload requestFields = new SetRFIDCodePayload() {
                UserID = UserID,
                UserStatus = UserStatus,
                UserType = UserType,
                RFIDCode = RFIDCode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x16, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get RFID Code
        /// </summary>
        public async Task<GetRFIDCodeResponse?> GetRFIDCode(SecureSession session, ushort UserID) {
            GetRFIDCodePayload requestFields = new GetRFIDCodePayload() {
                UserID = UserID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x17, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetRFIDCodeResponse() {
                UserID = (ushort)GetField(resp, 0),
                UserStatus = (UserStatusEnum?)(byte)GetField(resp, 1),
                UserType = (UserTypeEnum?)(byte)GetField(resp, 2),
                RFIDCode = (byte[]?)GetField(resp, 3),
            };
        }

        /// <summary>
        /// Clear RFID Code
        /// </summary>
        public async Task<bool> ClearRFIDCode(SecureSession session, ushort commandTimeoutMS, ushort RFIDSlotIndex) {
            ClearRFIDCodePayload requestFields = new ClearRFIDCodePayload() {
                RFIDSlotIndex = RFIDSlotIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x18, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Clear All RFID Codes
        /// </summary>
        public async Task<bool> ClearAllRFIDCodes(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x19);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set User
        /// </summary>
        public async Task<bool> SetUser(SecureSession session, ushort commandTimeoutMS, DataOperationTypeEnum OperationType, ushort UserIndex, string UserName, uint UserUniqueID, UserStatusEnum UserStatus, UserTypeEnum UserType, CredentialRuleEnum CredentialRule) {
            SetUserPayload requestFields = new SetUserPayload() {
                OperationType = OperationType,
                UserIndex = UserIndex,
                UserName = UserName,
                UserUniqueID = UserUniqueID,
                UserStatus = UserStatus,
                UserType = UserType,
                CredentialRule = CredentialRule,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x1A, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get User
        /// </summary>
        public async Task<GetUserResponse?> GetUser(SecureSession session, ushort UserIndex) {
            GetUserPayload requestFields = new GetUserPayload() {
                UserIndex = UserIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x1B, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetUserResponse() {
                UserIndex = (ushort)GetField(resp, 0),
                UserName = (string?)GetField(resp, 1),
                UserUniqueID = (uint?)GetField(resp, 2),
                UserStatus = (UserStatusEnum?)(byte)GetField(resp, 3),
                UserType = (UserTypeEnum?)(byte)GetField(resp, 4),
                CredentialRule = (CredentialRuleEnum?)(byte)GetField(resp, 5),
                Credentials = GetArrayField<Credential>(resp, 6),
                CreatorFabricIndex = (byte?)GetField(resp, 7),
                LastModifiedFabricIndex = (byte?)GetField(resp, 8),
                NextUserIndex = (ushort?)GetField(resp, 9),
            };
        }

        /// <summary>
        /// Clear User
        /// </summary>
        public async Task<bool> ClearUser(SecureSession session, ushort commandTimeoutMS, ushort UserIndex) {
            ClearUserPayload requestFields = new ClearUserPayload() {
                UserIndex = UserIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x1D, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Credential
        /// </summary>
        public async Task<SetCredentialResponse?> SetCredential(SecureSession session, ushort commandTimeoutMS, DataOperationTypeEnum OperationType, Credential Credential, byte[] CredentialData, ushort UserIndex, UserStatusEnum UserStatus, UserTypeEnum UserType) {
            SetCredentialPayload requestFields = new SetCredentialPayload() {
                OperationType = OperationType,
                Credential = Credential,
                CredentialData = CredentialData,
                UserIndex = UserIndex,
                UserStatus = UserStatus,
                UserType = UserType,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x22, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new SetCredentialResponse() {
                Status = (IMStatusCode)(byte)GetField(resp, 0),
                UserIndex = (ushort?)GetField(resp, 1),
                NextCredentialIndex = (ushort?)GetOptionalField(resp, 2),
            };
        }

        /// <summary>
        /// Get Credential Status
        /// </summary>
        public async Task<GetCredentialStatusResponse?> GetCredentialStatus(SecureSession session, Credential Credential) {
            GetCredentialStatusPayload requestFields = new GetCredentialStatusPayload() {
                Credential = Credential,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x24, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetCredentialStatusResponse() {
                CredentialExists = (bool)GetField(resp, 0),
                UserIndex = (ushort?)GetField(resp, 1),
                CreatorFabricIndex = (byte?)GetField(resp, 2),
                LastModifiedFabricIndex = (byte?)GetField(resp, 3),
                NextCredentialIndex = (ushort?)GetOptionalField(resp, 4),
            };
        }

        /// <summary>
        /// Clear Credential
        /// </summary>
        public async Task<bool> ClearCredential(SecureSession session, ushort commandTimeoutMS, Credential Credential) {
            ClearCredentialPayload requestFields = new ClearCredentialPayload() {
                Credential = Credential,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x26, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Unbolt Door
        /// </summary>
        public async Task<bool> UnboltDoor(SecureSession session, ushort commandTimeoutMS, byte[]? PINCode) {
            UnboltDoorPayload requestFields = new UnboltDoorPayload() {
                PINCode = PINCode,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x27, requestFields);
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
        public async Task<LockStateEnum?> GetLockState(SecureSession session) {
            return (LockStateEnum?)await GetEnumAttribute(session, 0, true);
        }

        /// <summary>
        /// Get the Lock Type attribute
        /// </summary>
        public async Task<LockTypeEnum> GetLockType(SecureSession session) {
            return (LockTypeEnum)await GetEnumAttribute(session, 1);
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
        public async Task<DoorStateEnum?> GetDoorState(SecureSession session) {
            return (DoorStateEnum?)await GetEnumAttribute(session, 3, true);
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
        /// Get the Number Of Log Records Supported attribute
        /// </summary>
        public async Task<ushort> GetNumberOfLogRecordsSupported(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16) ?? 0;
        }

        /// <summary>
        /// Get the Number Of Total Users Supported attribute
        /// </summary>
        public async Task<ushort> GetNumberOfTotalUsersSupported(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 17) ?? 0;
        }

        /// <summary>
        /// Get the Number Of PIN Users Supported attribute
        /// </summary>
        public async Task<ushort> GetNumberOfPINUsersSupported(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 18) ?? 0;
        }

        /// <summary>
        /// Get the Number Of RFID Users Supported attribute
        /// </summary>
        public async Task<ushort> GetNumberOfRFIDUsersSupported(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 19) ?? 0;
        }

        /// <summary>
        /// Get the Number Of Week Day Schedules Supported Per User attribute
        /// </summary>
        public async Task<byte> GetNumberOfWeekDaySchedulesSupportedPerUser(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 20) ?? 0;
        }

        /// <summary>
        /// Get the Number Of Year Day Schedules Supported Per User attribute
        /// </summary>
        public async Task<byte> GetNumberOfYearDaySchedulesSupportedPerUser(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 21) ?? 0;
        }

        /// <summary>
        /// Get the Number Of Holiday Schedules Supported attribute
        /// </summary>
        public async Task<byte> GetNumberOfHolidaySchedulesSupported(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 22) ?? 0;
        }

        /// <summary>
        /// Get the Max PIN Code Length attribute
        /// </summary>
        public async Task<byte> GetMaxPINCodeLength(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 23))!;
        }

        /// <summary>
        /// Get the Min PIN Code Length attribute
        /// </summary>
        public async Task<byte> GetMinPINCodeLength(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 24))!;
        }

        /// <summary>
        /// Get the Max RFID Code Length attribute
        /// </summary>
        public async Task<byte> GetMaxRFIDCodeLength(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 25))!;
        }

        /// <summary>
        /// Get the Min RFID Code Length attribute
        /// </summary>
        public async Task<byte> GetMinRFIDCodeLength(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 26))!;
        }

        /// <summary>
        /// Get the Credential Rules Support attribute
        /// </summary>
        public async Task<CredentialRulesBitmap> GetCredentialRulesSupport(SecureSession session) {
            return (CredentialRulesBitmap)await GetEnumAttribute(session, 27);
        }

        /// <summary>
        /// Get the Number Of Credentials Supported Per User attribute
        /// </summary>
        public async Task<byte> GetNumberOfCredentialsSupportedPerUser(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 28) ?? 0;
        }

        /// <summary>
        /// Get the Enable Logging attribute
        /// </summary>
        public async Task<bool> GetEnableLogging(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 32) ?? false;
        }

        /// <summary>
        /// Set the Enable Logging attribute
        /// </summary>
        public async Task SetEnableLogging (SecureSession session, bool? value = false) {
            await SetAttribute(session, 32, value);
        }

        /// <summary>
        /// Get the Language attribute
        /// </summary>
        public async Task<string> GetLanguage(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 33))!;
        }

        /// <summary>
        /// Set the Language attribute
        /// </summary>
        public async Task SetLanguage (SecureSession session, string value) {
            await SetAttribute(session, 33, value);
        }

        /// <summary>
        /// Get the LED Settings attribute
        /// </summary>
        public async Task<LEDSettingEnum> GetLEDSettings(SecureSession session) {
            return (LEDSettingEnum)await GetEnumAttribute(session, 34);
        }

        /// <summary>
        /// Set the LED Settings attribute
        /// </summary>
        public async Task SetLEDSettings (SecureSession session, LEDSettingEnum value) {
            await SetAttribute(session, 34, value);
        }

        /// <summary>
        /// Get the Auto Relock Time attribute
        /// </summary>
        public async Task<uint> GetAutoRelockTime(SecureSession session) {
            return (uint)(dynamic?)(await GetAttribute(session, 35))!;
        }

        /// <summary>
        /// Set the Auto Relock Time attribute
        /// </summary>
        public async Task SetAutoRelockTime (SecureSession session, uint value) {
            await SetAttribute(session, 35, value);
        }

        /// <summary>
        /// Get the Sound Volume attribute
        /// </summary>
        public async Task<SoundVolumeEnum> GetSoundVolume(SecureSession session) {
            return (SoundVolumeEnum)await GetEnumAttribute(session, 36);
        }

        /// <summary>
        /// Set the Sound Volume attribute
        /// </summary>
        public async Task SetSoundVolume (SecureSession session, SoundVolumeEnum value) {
            await SetAttribute(session, 36, value);
        }

        /// <summary>
        /// Get the Operating Mode attribute
        /// </summary>
        public async Task<OperatingModeEnum> GetOperatingMode(SecureSession session) {
            return (OperatingModeEnum)await GetEnumAttribute(session, 37);
        }

        /// <summary>
        /// Set the Operating Mode attribute
        /// </summary>
        public async Task SetOperatingMode (SecureSession session, OperatingModeEnum value) {
            await SetAttribute(session, 37, value);
        }

        /// <summary>
        /// Get the Supported Operating Modes attribute
        /// </summary>
        public async Task<OperatingModesBitmap> GetSupportedOperatingModes(SecureSession session) {
            return (OperatingModesBitmap)await GetEnumAttribute(session, 38);
        }

        /// <summary>
        /// Get the Default Configuration Register attribute
        /// </summary>
        public async Task<ConfigurationRegisterBitmap> GetDefaultConfigurationRegister(SecureSession session) {
            return (ConfigurationRegisterBitmap)await GetEnumAttribute(session, 39);
        }

        /// <summary>
        /// Get the Enable Local Programming attribute
        /// </summary>
        public async Task<bool> GetEnableLocalProgramming(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 40) ?? true;
        }

        /// <summary>
        /// Set the Enable Local Programming attribute
        /// </summary>
        public async Task SetEnableLocalProgramming (SecureSession session, bool? value = true) {
            await SetAttribute(session, 40, value);
        }

        /// <summary>
        /// Get the Enable One Touch Locking attribute
        /// </summary>
        public async Task<bool> GetEnableOneTouchLocking(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 41) ?? false;
        }

        /// <summary>
        /// Set the Enable One Touch Locking attribute
        /// </summary>
        public async Task SetEnableOneTouchLocking (SecureSession session, bool? value = false) {
            await SetAttribute(session, 41, value);
        }

        /// <summary>
        /// Get the Enable Inside Status LED attribute
        /// </summary>
        public async Task<bool> GetEnableInsideStatusLED(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 42) ?? false;
        }

        /// <summary>
        /// Set the Enable Inside Status LED attribute
        /// </summary>
        public async Task SetEnableInsideStatusLED (SecureSession session, bool? value = false) {
            await SetAttribute(session, 42, value);
        }

        /// <summary>
        /// Get the Enable Privacy Mode Button attribute
        /// </summary>
        public async Task<bool> GetEnablePrivacyModeButton(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 43) ?? false;
        }

        /// <summary>
        /// Set the Enable Privacy Mode Button attribute
        /// </summary>
        public async Task SetEnablePrivacyModeButton (SecureSession session, bool? value = false) {
            await SetAttribute(session, 43, value);
        }

        /// <summary>
        /// Get the Local Programming Features attribute
        /// </summary>
        public async Task<LocalProgrammingFeaturesBitmap> GetLocalProgrammingFeatures(SecureSession session) {
            return (LocalProgrammingFeaturesBitmap)await GetEnumAttribute(session, 44);
        }

        /// <summary>
        /// Set the Local Programming Features attribute
        /// </summary>
        public async Task SetLocalProgrammingFeatures (SecureSession session, LocalProgrammingFeaturesBitmap value) {
            await SetAttribute(session, 44, value);
        }

        /// <summary>
        /// Get the Wrong Code Entry Limit attribute
        /// </summary>
        public async Task<byte> GetWrongCodeEntryLimit(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 48))!;
        }

        /// <summary>
        /// Set the Wrong Code Entry Limit attribute
        /// </summary>
        public async Task SetWrongCodeEntryLimit (SecureSession session, byte value) {
            await SetAttribute(session, 48, value);
        }

        /// <summary>
        /// Get the User Code Temporary Disable Time attribute
        /// </summary>
        public async Task<byte> GetUserCodeTemporaryDisableTime(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 49))!;
        }

        /// <summary>
        /// Set the User Code Temporary Disable Time attribute
        /// </summary>
        public async Task SetUserCodeTemporaryDisableTime (SecureSession session, byte value) {
            await SetAttribute(session, 49, value);
        }

        /// <summary>
        /// Get the Send PIN Over The Air attribute
        /// </summary>
        public async Task<bool> GetSendPINOverTheAir(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 50) ?? false;
        }

        /// <summary>
        /// Set the Send PIN Over The Air attribute
        /// </summary>
        public async Task SetSendPINOverTheAir (SecureSession session, bool? value = false) {
            await SetAttribute(session, 50, value);
        }

        /// <summary>
        /// Get the Require PI Nfor Remote Operation attribute
        /// </summary>
        public async Task<bool> GetRequirePINforRemoteOperation(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 51) ?? false;
        }

        /// <summary>
        /// Set the Require PI Nfor Remote Operation attribute
        /// </summary>
        public async Task SetRequirePINforRemoteOperation (SecureSession session, bool? value = false) {
            await SetAttribute(session, 51, value);
        }

        /// <summary>
        /// Get the Expiring User Timeout attribute
        /// </summary>
        public async Task<ushort> GetExpiringUserTimeout(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 53))!;
        }

        /// <summary>
        /// Set the Expiring User Timeout attribute
        /// </summary>
        public async Task SetExpiringUserTimeout (SecureSession session, ushort value) {
            await SetAttribute(session, 53, value);
        }

        /// <summary>
        /// Get the Alarm Mask attribute
        /// </summary>
        public async Task<AlarmMaskBitmap> GetAlarmMask(SecureSession session) {
            return (AlarmMaskBitmap)await GetEnumAttribute(session, 64);
        }

        /// <summary>
        /// Set the Alarm Mask attribute
        /// </summary>
        public async Task SetAlarmMask (SecureSession session, AlarmMaskBitmap value) {
            await SetAttribute(session, 64, value);
        }

        /// <summary>
        /// Get the Keypad Operation Event Mask attribute
        /// </summary>
        public async Task<EventMaskBitmap> GetKeypadOperationEventMask(SecureSession session) {
            return (EventMaskBitmap)await GetEnumAttribute(session, 65);
        }

        /// <summary>
        /// Set the Keypad Operation Event Mask attribute
        /// </summary>
        public async Task SetKeypadOperationEventMask (SecureSession session, EventMaskBitmap value) {
            await SetAttribute(session, 65, value);
        }

        /// <summary>
        /// Get the Remote Operation Event Mask attribute
        /// </summary>
        public async Task<EventMaskBitmap> GetRemoteOperationEventMask(SecureSession session) {
            return (EventMaskBitmap)await GetEnumAttribute(session, 66);
        }

        /// <summary>
        /// Set the Remote Operation Event Mask attribute
        /// </summary>
        public async Task SetRemoteOperationEventMask (SecureSession session, EventMaskBitmap value) {
            await SetAttribute(session, 66, value);
        }

        /// <summary>
        /// Get the Manual Operation Event Mask attribute
        /// </summary>
        public async Task<EventMaskBitmap> GetManualOperationEventMask(SecureSession session) {
            return (EventMaskBitmap)await GetEnumAttribute(session, 67);
        }

        /// <summary>
        /// Set the Manual Operation Event Mask attribute
        /// </summary>
        public async Task SetManualOperationEventMask (SecureSession session, EventMaskBitmap value) {
            await SetAttribute(session, 67, value);
        }

        /// <summary>
        /// Get the RFID Operation Event Mask attribute
        /// </summary>
        public async Task<EventMaskBitmap> GetRFIDOperationEventMask(SecureSession session) {
            return (EventMaskBitmap)await GetEnumAttribute(session, 68);
        }

        /// <summary>
        /// Set the RFID Operation Event Mask attribute
        /// </summary>
        public async Task SetRFIDOperationEventMask (SecureSession session, EventMaskBitmap value) {
            await SetAttribute(session, 68, value);
        }

        /// <summary>
        /// Get the Keypad Programming Event Mask attribute
        /// </summary>
        public async Task<EventMaskBitmap> GetKeypadProgrammingEventMask(SecureSession session) {
            return (EventMaskBitmap)await GetEnumAttribute(session, 69);
        }

        /// <summary>
        /// Set the Keypad Programming Event Mask attribute
        /// </summary>
        public async Task SetKeypadProgrammingEventMask (SecureSession session, EventMaskBitmap value) {
            await SetAttribute(session, 69, value);
        }

        /// <summary>
        /// Get the Remote Programming Event Mask attribute
        /// </summary>
        public async Task<EventMaskBitmap> GetRemoteProgrammingEventMask(SecureSession session) {
            return (EventMaskBitmap)await GetEnumAttribute(session, 70);
        }

        /// <summary>
        /// Set the Remote Programming Event Mask attribute
        /// </summary>
        public async Task SetRemoteProgrammingEventMask (SecureSession session, EventMaskBitmap value) {
            await SetAttribute(session, 70, value);
        }

        /// <summary>
        /// Get the RFID Programming Event Mask attribute
        /// </summary>
        public async Task<EventMaskBitmap> GetRFIDProgrammingEventMask(SecureSession session) {
            return (EventMaskBitmap)await GetEnumAttribute(session, 71);
        }

        /// <summary>
        /// Set the RFID Programming Event Mask attribute
        /// </summary>
        public async Task SetRFIDProgrammingEventMask (SecureSession session, EventMaskBitmap value) {
            await SetAttribute(session, 71, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Door Lock Cluster";
        }
    }
}