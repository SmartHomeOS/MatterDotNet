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
//
// WARNING: This file was auto-generated. Do not edit.


using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Payloads.Status;
using System.Data;

namespace MatterDotNet.Clusters
{
    public abstract class ClusterBase
    {
        public ClusterBase(ushort endPoint)
        {
            this.endPoint = endPoint;
        }

        protected readonly ushort endPoint;

        protected object? GetOptionalField(InvokeResponseIB resp, int fieldNumber)
        {
            object?[] fields = (object?[])resp.Command!.CommandFields!;
            if (fieldNumber >= fields.Length)
                return null;
            return fields[fieldNumber];
        }
        protected object GetField(InvokeResponseIB resp, int fieldNumber)
        {
            object[] fields = (object[])resp.Command!.CommandFields!;
            if (fieldNumber >= fields.Length)
                throw new DataException("Field " + fieldNumber + " is missing");
            return fields[fieldNumber]!;
        }

        protected bool validateResponse(InvokeResponseIB resp)
        {
            if (resp.Status == null)
            {
                if (resp.Command?.CommandFields != null)
                    return true;
                throw new InvalidDataException("Response received without status");
            }
            switch ((IMStatusCode)resp.Status.Status.Status)
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