// MatterDotNet Copyright (C) 2024 
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

using MatterDotNet.Messages.CASE;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Payloads.Flags;
using MatterDotNet.Protocol.Payloads.OpCodes;
using MatterDotNet.Protocol.Sessions;
using System.Security.Cryptography;

namespace MatterDotNet.Protocol.Cryptography
{
    public static class CASE
    {
        private static readonly byte[] Resume1MIC_Nonce = [0x4e, 0x43, 0x41, 0x53, 0x45, 0x5f, 0x53, 0x69, 0x67, 0x6d, 0x61, 0x53, 0x31]; /* "NCASE_SigmaS1" */
        private static readonly byte[] TBEData2_Nonce = [0x4e, 0x43, 0x41, 0x53, 0x45, 0x5f, 0x53, 0x69, 0x67, 0x6d, 0x61, 0x32, 0x4e]; /* "NCASE_Sigma2N" */
        private static readonly byte[] TBEData3_Nonce = [0x4e, 0x43, 0x41, 0x53, 0x45, 0x5f, 0x53, 0x69, 0x67, 0x6d, 0x61, 0x33, 0x4e]; /* "NCASE_Sigma3N" */
        private static readonly byte[] Resume2MIC_Nonce = [0x4e, 0x43, 0x41, 0x53, 0x45, 0x5f, 0x53, 0x69, 0x67, 0x6d, 0x61, 0x53, 0x32]; /* "NCASE_SigmaS2" */

        public static Frame GenerateSigma1(byte[] destination)
        {
            var keypair = Crypto.GenerateKeypair();

            Sigma1 sigma1 = new Sigma1()
            {
                InitiatorRandom = RandomNumberGenerator.GetBytes(32),
                InitiatorSessionId = SessionManager.GetAvailableSessionID(),
                DestinationId = destination,
                InitiatorEphPubKey = keypair.Public
            };

            Frame frame = new Frame(sigma1, (byte)SecureOpCodes.CASESigma1);
            frame.Message.Protocol = ProtocolType.SecureChannel;
            return frame;
        }
    }
}
