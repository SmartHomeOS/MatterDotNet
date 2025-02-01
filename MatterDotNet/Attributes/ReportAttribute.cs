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

namespace MatterDotNet.Attributes
{
    /// <summary>
    /// Create a read-only attribute
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReportAttribute<T> : ReadAttribute<T>, IReportAttribute
    {
        /// <summary>
        /// Signature for attribute changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public delegate Task NodeEventHandler(ReadAttribute<T> sender, T oldValue, T newValue);

        /// <summary>
        /// The attribute has been updated
        /// </summary>
        public event NodeEventHandler? Updated;

        /// <summary>
        /// Create a read-only attribute
        /// </summary>
        /// <param name="clusterId"></param>
        /// <param name="endPoint"></param>
        /// <param name="attributeId"></param>
        /// <param name="nullable"></param>
        internal ReportAttribute(uint clusterId, ushort endPoint, ushort attributeId, bool nullable = false) : base(clusterId, endPoint, attributeId, nullable) { }

        void IReportAttribute.Notify(object? data)
        {
            if (data == null)
            {
                if (nullable && Value != null)
                {
                    T update = (dynamic)null!;
                    Updated?.Invoke(this, Value, update);
                    Value = update;
                }
            }
            else
            {
                T update = Deserialize(data);
                if (update != null && !update.Equals(Value))
                {
                    Updated?.Invoke(this, Value, update);
                    Value = update;
                }
            }
        }

        public async Task Subscribe(SecureSession session, TimeSpan minReporting, TimeSpan maxReporting, CancellationToken token = default)
        {
            using (Exchange exchange = session.CreateExchange())
                await InteractionManager.Subscribe(exchange, this, minReporting, maxReporting, token);
        }
    }
}
