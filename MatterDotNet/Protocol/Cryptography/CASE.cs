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

using MatterDotNet.Messages;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using System.Security.Cryptography;

namespace MatterDotNet.Protocol.Cryptography
{
    internal class CASE
    {
        private static readonly byte[] TBEData1_Nonce = [0x4e, 0x43, 0x41, 0x53, 0x45, 0x5f, 0x53, 0x69, 0x67, 0x6d, 0x61, 0x53, 0x31]; /* "NCASE_SigmaS1" */
        private static readonly byte[] TBEData2_Nonce = [0x4e, 0x43, 0x41, 0x53, 0x45, 0x5f, 0x53, 0x69, 0x67, 0x6d, 0x61, 0x32, 0x4e]; /* "NCASE_Sigma2N" */
        private static readonly byte[] TBEData3_Nonce = [0x4e, 0x43, 0x41, 0x53, 0x45, 0x5f, 0x53, 0x69, 0x67, 0x6d, 0x61, 0x33, 0x4e]; /* "NCASE_Sigma3N" */
        private static readonly byte[] Resume1MIC_Nonce = [0x4e, 0x43, 0x41, 0x53, 0x45, 0x5f, 0x53, 0x69, 0x67, 0x6d, 0x61, 0x53, 0x31]; /* "NCASE_SigmaR1" */

        public static Frame GenerateSigma1(ulong destination)
        {
            var keypair = Crypto.GenerateKeypair();

            Sigma1 sigma1 = new Sigma1();
            sigma1.initiatorRandom = RandomNumberGenerator.GetBytes(32);
            sigma1.initiatorSessionId = SessionManager.GetAvailableSessionID();
            sigma1.destinationId = destination;
            sigma1.initiatorEphPubKey = keypair.Public;

            Frame frame = new Frame(sigma1);
            frame.Message.Flags |= ExchangeFlags.Initiator;
            frame.Message.OpCode = (byte)SecureOpCodes.CASESigma1;
            frame.Message.Protocol = ProtocolType.SecureChannel;
            return frame;
        }
    }
}
