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

namespace MatterDotNet.Protocol.Payloads.OpCodes
{
    internal enum SecureOpCodes
    {
        MsgCounterSyncReq = 0x00,
        MsgCounterSyncRsp = 0x01,
        MRPStandaloneAcknowledgement = 0x10,
        PBKDFParamRequest = 0x20,
        PBKDFParamResponse = 0x21,
        PASEPake1 = 0x22,
        PASEPake2 = 0x23,
        PASEPake3 = 0x24,
        CASESigma1 = 0x30,
        CASESigma2 = 0x31,
        CASESigma3 = 0x32,
        CASESigma2_Resume = 0x33,
        StatusReport = 0x40,
        ICDCheckInMessage = 0x50
    }
}
