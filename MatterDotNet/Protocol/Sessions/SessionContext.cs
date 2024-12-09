﻿// MatterDotNet Copyright (C) 2024 
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

using MatterDotNet.Protocol.Connection;
using System.Collections.Concurrent;

namespace MatterDotNet.Protocol.Sessions
{
    public class SessionContext
    {
        public bool Initiator { get; init; }
        public ulong InitiatorNodeID { get; init; }
        public ushort LocalSessionID { get; init; }
        internal IConnection Connection { get; init; }

        private ConcurrentDictionary<ushort, Exchange> exchanges = new ConcurrentDictionary<ushort, Exchange>();

        internal SessionContext(IConnection connection, bool initiator, ulong initiatorNodeId, ushort localSessionId)
        {
            Connection = connection;
            Initiator = initiator;
            InitiatorNodeID = initiatorNodeId;
            LocalSessionID = localSessionId;
        }

        public Exchange CreateExchange()
        {
            if (exchanges.Count >= 5)
                throw new InvalidOperationException("5 Exchanges are already open");
            ushort exchangeId;
            do
            {
                exchangeId = (ushort)Random.Shared.Next(1, ushort.MaxValue);
            }
            while (exchanges.ContainsKey(exchangeId));
            return new Exchange(this, exchangeId);
        }

        internal void DeleteExchange(Exchange exchange)
        {
            exchanges.TryRemove(exchange.ID, out _);
        }
    }
}