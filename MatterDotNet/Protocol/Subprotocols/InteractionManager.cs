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

using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Payloads.OpCodes;
using MatterDotNet.Protocol.Payloads.Status;
using MatterDotNet.Protocol.Sessions;
using System.Data;

namespace MatterDotNet.Protocol.Subprotocols
{
    internal class InteractionManager
    {
        public static async Task<List<AttributeReportIB>> GetAttributes(SecureSession session, ushort endpoint, uint cluster, params uint[] attributes)
        {
            using (Exchange secExchange = session.CreateExchange())
            {
                AttributePathIB[] paths = new AttributePathIB[attributes.Length];
                for (int i = 0; i < paths.Length; i++)
                    paths[i] = new AttributePathIB() { Endpoint = endpoint, Cluster = cluster, Attribute = attributes[i] };
                ReadRequestMessage read = new ReadRequestMessage()
                {
                    InteractionModelRevision = Constants.MATTER_13_REVISION,
                    FabricFiltered = false,
                    AttributeRequests = paths,
                    EventRequests = [],
                    DataVersionFilters = []
                };
                Frame readFrame = new Frame(read, (byte)IMOpCodes.ReadRequest);
                readFrame.Message.Protocol = ProtocolType.InteractionModel;
                readFrame.SourceNodeID = session.InitiatorNodeID;
                readFrame.DestinationID = session.ResponderNodeID;
                await secExchange.SendFrame(readFrame);
                List<AttributeReportIB> results = new List<AttributeReportIB>();
                bool more = false;
                do
                {
                    Frame response = await secExchange.Read();
                    if (response.Message.Payload is ReportDataMessage msg)
                    {
                        more = msg.MoreChunkedMessages == true;
                        if (msg.AttributeReports != null)
                            results.AddRange(msg.AttributeReports);
                        if (more)
                        {
                            var status = new StatusResponseMessage() { InteractionModelRevision = Constants.MATTER_13_REVISION, Status = (byte)IMStatusCode.SUCCESS };
                            Frame statusFrame = new Frame(status, (byte)IMOpCodes.StatusResponse);
                            readFrame.Message.Protocol = ProtocolType.InteractionModel;
                            await secExchange.SendFrame(statusFrame);
                        }
                    }
                } while (more);
                return results;
            }
        }

        public static async Task<object?> GetAttribute(SecureSession session, ushort endpoint, uint cluster, uint attribute)
        {
            using (Exchange secExchange = session.CreateExchange())
            {
                ReadRequestMessage read = new ReadRequestMessage()
                {
                    InteractionModelRevision = Constants.MATTER_13_REVISION,
                    FabricFiltered = true,
                    AttributeRequests = [new AttributePathIB() { Endpoint = endpoint, Cluster = cluster, Attribute = attribute }]
                };
                Frame readFrame = new Frame(read, (byte)IMOpCodes.ReadRequest);
                readFrame.Message.Protocol = ProtocolType.InteractionModel;
                readFrame.SourceNodeID = session.InitiatorNodeID;
                readFrame.DestinationID = session.ResponderNodeID;
                await secExchange.SendFrame(readFrame);
                while (true)
                {
                    Frame response = await secExchange.Read();
                    if (response.Message.Payload is ReportDataMessage msg)
                    {
                        if (msg.AttributeReports != null && !ValidateResponse(msg.AttributeReports[0], endpoint))
                            throw new IOException("Failed to query attribute: " + (IMStatusCode?)msg.AttributeReports[0].AttributeStatus?.Status.Status);
                        if (msg.MoreChunkedMessages.HasValue && msg.MoreChunkedMessages.Value == true)
                            return await HandleChunked(msg, secExchange, endpoint);
                        if (msg.AttributeReports != null)
                            return GetData(msg.AttributeReports);
                    }
                    else if (response.Message.Payload is StatusResponseMessage status)
                        throw new IOException("Error: " + (IMStatusCode)status.Status);
                }
            }
        }

        private static object? GetData(IList<AttributeReportIB> attributeReports)
        {
            if (attributeReports.Count == 0)
                return null;
            if (attributeReports.Count == 1)
                return attributeReports[0].AttributeData!.Data;
            dynamic[] result = (dynamic[])attributeReports[0].AttributeData!.Data!;
            int offset = result.Length;
            Array.Resize(ref result, attributeReports.Count - 1 + offset);
            for (int i = 1; i < attributeReports.Count; i++)
                result[offset + i - 1] = attributeReports[i].AttributeData!.Data!;
            return result;
        }

        private static async Task<object?> HandleChunked(ReportDataMessage first, Exchange secExchange, ushort endpoint)
        {
            List<AttributeReportIB> attributes = new List<AttributeReportIB>();
            attributes.AddRange(first.AttributeReports!);
            for (int i = 0; i < 1024; i++) //Infinite loop protection
            {
                Frame response = await secExchange.Read();
                if (response.Message.Payload is ReportDataMessage msg)
                {
                    if (msg.AttributeReports != null && !ValidateResponse(msg.AttributeReports[0], endpoint))
                        throw new IOException("Failed to query attribute: " + (IMStatusCode?)msg.AttributeReports[0].AttributeStatus?.Status.Status);
                    if (msg.AttributeReports != null)
                        attributes.AddRange(msg.AttributeReports);
                    if (!msg.MoreChunkedMessages.HasValue || msg.MoreChunkedMessages.Value == false)
                    {
                        if (!msg.SuppressResponse.HasValue || msg.SuppressResponse.Value == false)
                            await SendStatus(IMStatusCode.SUCCESS, secExchange);
                        break;
                    }
                    else
                        await SendStatus(IMStatusCode.SUCCESS, secExchange);
                }
                else if (response.Message.Payload is StatusResponseMessage status)
                    throw new IOException("Error: " + (IMStatusCode)status.Status);
            }
            return GetData(attributes);
        }

        private static async Task SendStatus(IMStatusCode status, Exchange exchange)
        {
            await exchange.SendFrame(new Frame(new StatusPayload(GeneralCode.SUCCESS, 0, ProtocolType.SecureChannel, (ushort)status), (byte)SecureOpCodes.StatusReport));
        }

        public static Task SendCommand(SecureSession session, ushort endpoint, uint cluster, uint command, bool timed, TLVPayload? payload = null)
        {
            using (Exchange exchange = session.CreateExchange())
                return SendCommand(exchange, endpoint, cluster, command, timed, null, payload);
        }

        public static async Task SendCommand(Exchange exchange, ushort endpoint, uint cluster, uint command, bool timed, ushort? refNum, TLVPayload? payload = null)
        {
            InvokeRequestMessage run = new InvokeRequestMessage()
            {
                SuppressResponse = false,
                TimedRequest = timed,
                InteractionModelRevision = Constants.MATTER_13_REVISION,
                InvokeRequests = [new CommandDataIB() { CommandFields = payload, CommandRef = refNum, CommandPath = new CommandPathIB() { Endpoint = endpoint, Cluster = cluster, Command = command } }]
            };
            Frame invokeFrame = new Frame(run, (byte)IMOpCodes.InvokeRequest);
            invokeFrame.Message.Protocol = ProtocolType.InteractionModel;
            invokeFrame.SourceNodeID = exchange.Session.InitiatorNodeID;
            invokeFrame.DestinationID = exchange.Session.ResponderNodeID;
            await exchange.SendFrame(invokeFrame);
        }

        public static async Task StartTimed(Exchange exchange, ushort timeout)
        {
            TimedRequestMessage time = new TimedRequestMessage()
            {
                Timeout = timeout,
                InteractionModelRevision = Constants.MATTER_13_REVISION,
            };
            Frame invokeFrame = new Frame(time, (byte)IMOpCodes.TimedRequest);
            invokeFrame.Message.Protocol = ProtocolType.InteractionModel;
            invokeFrame.SourceNodeID = exchange.Session.InitiatorNodeID;
            invokeFrame.DestinationID = exchange.Session.ResponderNodeID;
            await exchange.SendFrame(invokeFrame);
            while (true)
            {
                Frame response = await exchange.Read();
                if (response.Message.Payload is StatusResponseMessage status)
                {
                    if ((IMStatusCode)status.Status == IMStatusCode.SUCCESS)
                        return;
                    throw new IOException("Error: " + (IMStatusCode)status.Status);
                }
            }
        }

        public static async Task<InvokeResponseIB> ExecCommand(SecureSession secSession, ushort endpoint, uint cluster, uint command, TLVPayload? payload = null)
        {
            using (Exchange exchange = secSession.CreateExchange())
            {
                ushort refNum = (ushort)Random.Shared.Next();
                await SendCommand(exchange, endpoint, cluster, command, false, refNum, payload);
                while (true)
                {
                    Frame response = await exchange.Read();
                    if (response.Message.Payload is InvokeResponseMessage msg)
                    {
                        if (msg.InvokeResponses[0].Status == null || !msg.InvokeResponses[0].Status!.CommandRef.HasValue || msg.InvokeResponses[0].Status!.CommandRef!.Value == refNum)
                            return msg.InvokeResponses[0];
                    }
                    else if (response.Message.Payload is StatusResponseMessage status)
                        throw new IOException("Error: " + (IMStatusCode)status.Status);
                }
            }
        }

        public static async Task<InvokeResponseIB> ExecTimedCommand(SecureSession secSession, ushort endpoint, uint cluster, uint command, ushort timeoutMS, TLVPayload? payload = null)
        {
            using (Exchange exchange = secSession.CreateExchange())
            {
                await StartTimed(exchange, timeoutMS);
                await SendCommand(exchange, endpoint, cluster, command, true, null, payload);
                while (true)
                {
                    Frame response = await exchange.Read();
                    if (response.Message.Payload is InvokeResponseMessage msg)
                        return msg.InvokeResponses[0];
                    else if (response.Message.Payload is StatusResponseMessage status)
                        throw new IOException("Error: " + (IMStatusCode)status.Status);
                }
            }
        }

        internal static async Task SetAttribute(SecureSession session, ushort endPoint, uint cluster, ushort attribute, object? value)
        {
            using (Exchange secExchange = session.CreateExchange())
            {
                WriteRequestMessage write = new WriteRequestMessage()
                {
                    InteractionModelRevision = Constants.MATTER_13_REVISION,
                    WriteRequests = [ new AttributeDataIB() {
                                        Path = new AttributePathIB() { Node = session.ResponderNodeID, Endpoint = endPoint, Cluster = cluster, Attribute = attribute },
                                        Data = value
                                      }
                                    ],
                    TimedRequest = false,
                    MoreChunkedMessages = false
                };
                Frame readFrame = new Frame(write, (byte)IMOpCodes.WriteRequest);
                readFrame.Message.Protocol = ProtocolType.InteractionModel;
                readFrame.SourceNodeID = session.InitiatorNodeID;
                readFrame.DestinationID = session.ResponderNodeID;
                await secExchange.SendFrame(readFrame);
                while (true)
                {
                    Frame response = await secExchange.Read();
                    if (response.Message.Payload is WriteResponseMessage msg)
                    {
                        if (msg.WriteResponses != null && !ValidateResponse(msg.WriteResponses[0], endPoint))
                                throw new IOException($"Failed to set attribute {attribute} on cluster {cluster}@{endPoint}");
                    }
                    else if (response.Message.Payload is StatusResponseMessage status)
                        throw new IOException("Error: " + (IMStatusCode)status.Status);
                }
            }
        }

        /// <summary>
        /// Validates a response and throws an exception if it's an error status
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        internal static bool ValidateResponse(InvokeResponseIB resp, ushort endPoint)
        {
            if (resp.Status == null)
            {
                if (resp.Command?.CommandFields != null)
                    return true;
                throw new InvalidDataException("Response received without status");
            }
            return ValidateStatus((IMStatusCode)resp.Status.Status.Status, endPoint);
        }

        /// <summary>
        /// Validates a response and throws an exception if it's an error status
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        private static bool ValidateResponse(AttributeStatusIB resp, ushort endPoint)
        {
            if (resp.Status == null)
                throw new InvalidDataException("Response received without status");

            return ValidateStatus((IMStatusCode)resp.Status.Status, endPoint);
        }

        /// <summary>
        /// Validates a response and throws an exception if it's an error status
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        private static bool ValidateResponse(AttributeReportIB resp, ushort endPoint)
        {
            if (resp.AttributeStatus == null)
            {
                if (resp.AttributeData != null)
                    return true;
                throw new InvalidDataException("Response received without status");
            }
            return ValidateStatus((IMStatusCode)resp.AttributeStatus.Status.Status, endPoint);
        }

        private static bool ValidateStatus(IMStatusCode status, ushort endPoint)
        {
            switch (status)
            {
                case IMStatusCode.SUCCESS:
                    return true;
                case IMStatusCode.FAILURE:
                    return false;
                case IMStatusCode.UNSUPPORTED_ACCESS:
                    throw new UnauthorizedAccessException("Unsupported / Unauthorized Access");
                case IMStatusCode.UNSUPPORTED_ENDPOINT:
                    throw new InvalidOperationException("Endpoint " + endPoint + " is not supported");
                case IMStatusCode.INVALID_ACTION:
                    throw new DataException("Invalid Action");
                case IMStatusCode.UNSUPPORTED_COMMAND:
                    throw new DataException("Command ID not supported on this cluster");
                case IMStatusCode.INVALID_COMMAND:
                    throw new DataException("Invalid Command Payload");
                case IMStatusCode.CONSTRAINT_ERROR:
                    throw new DataException("Data constraint violated");
                case IMStatusCode.RESOURCE_EXHAUSTED:
                    throw new InsufficientMemoryException("Resource exhausted");
                case IMStatusCode.DATA_VERSION_MISMATCH:
                    throw new DataException("Data version mismatch");
                case IMStatusCode.TIMEOUT:
                    throw new TimeoutException();
                case IMStatusCode.BUSY:
                    throw new IOException("Resource Busy");
                case IMStatusCode.UNSUPPORTED_CLUSTER:
                    throw new DataException("Unsupported Cluster");
                case IMStatusCode.FAILSAFE_REQUIRED:
                    throw new InvalidOperationException("Failsafe required");
                case IMStatusCode.INVALID_IN_STATE:
                    throw new InvalidOperationException("The received request cannot be handled due to the current operational state of the device");
                default:
                    return false;
            }
        }
    }
}
