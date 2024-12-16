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

using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Payloads.Flags;
using MatterDotNet.Protocol.Payloads.OpCodes;
using MatterDotNet.Protocol.Payloads.Status;
using MatterDotNet.Protocol.Sessions;

namespace MatterDotNet.Protocol
{
    public class InteractionManager
    {
        public static async Task<List<AttributeReportIB>> GetAttributes(SecureSession session, ushort endpoint, uint cluster, params uint[] attributes)
        {
            using (Exchange secExchange = session.CreateExchange())
            {
                AttributePathIB[] paths = new AttributePathIB[attributes.Length];
                for (int i = 0; i < paths.Length; i++)
                    paths[i] = new AttributePathIB() { Node = session.InitiatorNodeID, Endpoint = endpoint, Cluster = cluster, Attribute = attributes[i] };
                ReadRequestMessage read = new ReadRequestMessage()
                {
                    InteractionModelRevision = Constants.MATTER_13_REVISION,
                    FabricFiltered = false,
                    AttributeRequests = paths
                };
                Frame readFrame = new Frame(read, (byte)IMOpCodes.ReadRequest);
                readFrame.Flags |= MessageFlags.SourceNodeID;
                readFrame.Message.Protocol = ProtocolType.InteractionModel;
                await secExchange.SendFrame(readFrame);
                List<AttributeReportIB> results = new List<AttributeReportIB>();
                bool more = false;
                do
                {
                    Frame response = await secExchange.Read();
                    if (response.Message.Payload is ReportDataMessage msg)
                    {
                        more = (msg.MoreChunkedMessages == true);
                        if (msg.AttributeReports != null)
                            results.AddRange(msg.AttributeReports);
                        if (more)
                        {
                            var status = new StatusResponseMessage() { InteractionModelRevision = Constants.MATTER_13_REVISION, Status = (byte)IMStatusCode.SUCCESS };
                            Frame statusFrame = new Frame(status, (byte)IMOpCodes.StatusResponse);
                            readFrame.Flags |= MessageFlags.SourceNodeID;
                            readFrame.Message.Protocol = ProtocolType.InteractionModel;
                            await secExchange.SendFrame(statusFrame);
                        }
                    }
                } while (more);
                return results;
            }
        }

        public static async Task<AttributeReportIB> GetAttribute(SecureSession session, ushort endpoint, uint cluster, uint attribute)
        {
            using (Exchange secExchange = session.CreateExchange())
            {
                ReadRequestMessage read = new ReadRequestMessage()
                {
                    InteractionModelRevision = Constants.MATTER_13_REVISION,
                    FabricFiltered = false,
                    AttributeRequests = [new AttributePathIB() { Node = session.InitiatorNodeID, Endpoint = endpoint, Cluster = cluster, Attribute = attribute }]
                };
                Frame readFrame = new Frame(read, (byte)IMOpCodes.ReadRequest);
                readFrame.Flags |= MessageFlags.SourceNodeID;
                readFrame.Message.Protocol = ProtocolType.InteractionModel;
                await secExchange.SendFrame(readFrame);
                while (true)
                {
                    Frame response = await secExchange.Read();
                    if (response.Message.Payload is ReportDataMessage msg)
                    {
                        if (msg.AttributeReports != null)
                            return msg.AttributeReports[0];
                    }
                }
            }
        }

        public static async Task SendCommand(Exchange exchange, ushort endpoint, uint cluster, uint command, TLVPayload? payload = null)
        {
            InvokeRequestMessage run = new InvokeRequestMessage()
            {
                SuppressResponse = false,
                TimedRequest = false,
                InteractionModelRevision = Constants.MATTER_13_REVISION,
                InvokeRequests = [new CommandDataIB() { CommandFields = payload, CommandPath = new CommandPathIB() { Endpoint = endpoint, Cluster = cluster, Command = command } }]
            };
            Frame readFrame = new Frame(run, (byte)IMOpCodes.InvokeRequest);
            readFrame.Flags |= MessageFlags.SourceNodeID;
            readFrame.Message.Protocol = ProtocolType.InteractionModel;
            await exchange.SendFrame(readFrame);
        }

        public static async Task<InvokeResponseIB> ExecCommand(SecureSession secSession, ushort endpoint, uint cluster, uint command, TLVPayload? payload = null)
        {
            using (Exchange exchange = secSession.CreateExchange())
            {
                await SendCommand(exchange, endpoint, cluster, command, payload);
                while (true)
                {
                    Frame response = await exchange.Read();
                    if (response.Message.Payload is InvokeResponseMessage msg)
                        return msg.InvokeResponses[0];
                }
            }
        }
    }
}
