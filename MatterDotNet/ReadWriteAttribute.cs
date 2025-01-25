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

using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Data;

namespace MatterDotNet
{
    /// <summary>
    /// Create a Readable and Writable Attribute
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReadWriteAttribute<T> : ReadAttribute<T>
    {
        internal ReadWriteAttribute(uint clusterId, ushort endPoint, ushort attributeId, bool nullable = false) : base(clusterId, endPoint, attributeId, nullable){ }
        
        /// <summary>
        /// Set the attribute to the provided value
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ConstraintException"></exception>
        public async Task Set(SecureSession session, T value)
        {
            if (!nullable && value == null)
                throw new ConstraintException("Attribute " + AttributeId + " was null");
            Value = value;
            await InteractionManager.SetAttribute(session, EndPoint, ClusterId, AttributeId, Value);
        }
    }
}
