﻿// MatterDotNet Copyright (C) 2025
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

using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using System.Net;

namespace MatterDotNet.Protocol.Connection
{
    internal interface IConnection : IDisposable
    {
        Task SendFrame(Exchange exchange, Frame frame, bool reliable, CancellationToken token);
        Task CloseExchange(Exchange exchange);
        bool Connected { get; }
        EndPoint EndPoint { get; }
    }
}