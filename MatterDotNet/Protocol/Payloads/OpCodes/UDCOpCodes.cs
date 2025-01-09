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
    /// <summary>
    /// User Directed Commissioning (UDC) OpCodes
    /// </summary>
    internal enum UDCOpCodes
    {
        /// <summary>
        /// The Identification Declaration message provides the DNSSD Instance Name of the commissionee requesting commissioning to the commissioner selected by the user. 
        /// It can also include information relating to the requested commissioning session.
        /// </summary>
        IdentificationDeclaration = 0,
        /// <summary>
        /// The Commissioner Declaration message provides information to a commissionee from the commissioner indicating its pre-commissioning state. 
        /// This information can be used by the commissionee to simplify the Passcode entry flow for the user.
        /// </summary>
        CommissionerDeclaration = 1,
    }
}
