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
//
// WARNING: This file was auto-generated. Do not edit.


using MatterDotNet.Clusters.Application;
using MatterDotNet.Clusters.Utility;
using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Payloads.Status;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Data;

namespace MatterDotNet.Clusters
{
    /// <summary>
    /// The base class for all clusters
    /// </summary>
    public abstract class ClusterBase
    {
        /// <summary>
        /// Creates a new instance of the cluster
        /// </summary>
        /// <param name="cluster"></param>
        /// <param name="endPoint"></param>
        public ClusterBase(uint cluster, ushort endPoint)
        {
            this.cluster = cluster;
            this.endPoint = endPoint;
        }

        /// <summary>
        /// End point number
        /// </summary>
        protected readonly ushort endPoint;

        /// <summary>
        /// Cluster ID
        /// </summary>
        protected readonly uint cluster;

        /// <summary>
        /// Gets an optional field from an Invoke Response
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="fieldNumber"></param>
        /// <returns></returns>
        protected static object? GetOptionalField(InvokeResponseIB resp, int fieldNumber)
        {
            object?[] fields = (object?[])resp.Command!.CommandFields!;
            if (fieldNumber >= fields.Length)
                return null;
            return fields[fieldNumber];
        }

        /// <summary>
        /// Gets a required field from an Invoke Response
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="fieldNumber"></param>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        protected static object GetField(InvokeResponseIB resp, int fieldNumber)
        {
            object[] fields = (object[])resp.Command!.CommandFields!;
            if (fieldNumber >= fields.Length)
                throw new DataException("Field " + fieldNumber + " is missing");
            return fields[fieldNumber]!;
        }

        /// <summary>
        /// Gets an attribute with the given ID or throws an appropriate exception
        /// </summary>
        /// <param name="session"></param>
        /// <param name="attribute"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ConstraintException"></exception>
        protected async Task<object?> GetAttribute(SecureSession session, ushort attribute, bool nullable = false)
        {
            AttributeReportIB report = await InteractionManager.GetAttribute(session, endPoint, cluster, attribute);
            if (!ValidateResponse(report))
                throw new IOException("Failed to query feature map");
            if (!nullable && report.AttributeData!.Data == null)
                throw new ConstraintException("Attribute " + attribute + " was null");
            return report.AttributeData!.Data;
        }

        /// <summary>
        /// Sets an attribute with the given ID to the given value
        /// </summary>
        /// <param name="session"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        /// <exception cref="ConstraintException"></exception>
        /// <exception cref="IOException"></exception>
        protected async Task SetAttribute(SecureSession session, ushort attribute, object? value, bool nullable = false)
        {
            if (!nullable && value == null)
                throw new ConstraintException("Attribute " + attribute + " was null");
            AttributeStatusIB result = await InteractionManager.SetAttribute(session, endPoint, cluster, attribute, value);
            if (!ValidateResponse(result))
                throw new IOException("Failed to query feature map");
        }

        /// <summary>
        /// Gets an enum attribute with the given ID or throws an appropriate exception
        /// </summary>
        /// <param name="session"></param>
        /// <param name="attribute"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        protected async Task<uint?> GetEnumAttribute(SecureSession session, ushort attribute, bool nullable = false)
        {
            object? value = await GetAttribute(session, attribute, nullable);
            if (value is byte byteVal)
                return byteVal;
            if (value is ushort shortVal)
                return shortVal;
            return (uint?)value!;
        }

        /// <summary>
        /// Validates a response and throws an exception if it's an error status
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        protected bool ValidateResponse(InvokeResponseIB resp)
        {
            if (resp.Status == null)
            {
                if (resp.Command?.CommandFields != null)
                    return true;
                throw new InvalidDataException("Response received without status");
            }
            return ValidateStatus((IMStatusCode)resp.Status.Status.Status);
        }

        /// <summary>
        /// Validates a response and throws an exception if it's an error status
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        protected bool ValidateResponse(AttributeStatusIB resp)
        {
            if (resp.Status == null)
                throw new InvalidDataException("Response received without status");

            return ValidateStatus((IMStatusCode)resp.Status.Status);
        }

        /// <summary>
        /// Validates a response and throws an exception if it's an error status
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        protected bool ValidateResponse(AttributeReportIB resp)
        {
            if (resp.AttributeStatus == null)
            {
                if (resp.AttributeData != null)
                    return true;
                throw new InvalidDataException("Response received without status");
            }
            return ValidateStatus((IMStatusCode)resp.AttributeStatus.Status.Status);
        }

        private bool ValidateStatus(IMStatusCode status)
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

        /// <summary>
        /// Returns the human readable name for the cluster
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.GetType().Name;
        }

        /// <summary>
        /// Create a cluster for the given cluster ID and end point
        /// </summary>
        /// <param name="clusterId"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static ClusterBase Create(uint clusterId, ushort endPoint)
        {
            switch (clusterId)
            {
                case AccessControlCluster.CLUSTER_ID:
                    return new AccessControlCluster(endPoint);
                case AdministratorCommissioningCluster.CLUSTER_ID:
                    return new AdministratorCommissioningCluster(endPoint);
                case AirQualityCluster.CLUSTER_ID:
                    return new AirQualityCluster(endPoint);
                case ApplicationBasicCluster.CLUSTER_ID:
                    return new ApplicationBasicCluster(endPoint);
                case ApplicationLauncherCluster.CLUSTER_ID:
                    return new ApplicationLauncherCluster(endPoint);
                case AudioOutputCluster.CLUSTER_ID:
                    return new AudioOutputCluster(endPoint);
                case BallastConfigurationCluster.CLUSTER_ID:
                    return new BallastConfigurationCluster(endPoint);
                case BasicInformationCluster.CLUSTER_ID:
                    return new BasicInformationCluster(endPoint);
                case BindingCluster.CLUSTER_ID:
                    return new BindingCluster(endPoint);
                case BooleanStateCluster.CLUSTER_ID:
                    return new BooleanStateCluster(endPoint);
                case BooleanStateConfigurationCluster.CLUSTER_ID:
                    return new BooleanStateConfigurationCluster(endPoint);
                case ActionsCluster.CLUSTER_ID:
                    return new ActionsCluster(endPoint);
                case BridgedDeviceBasicInformationCluster.CLUSTER_ID:
                    return new BridgedDeviceBasicInformationCluster(endPoint);
                case ColorControlCluster.CLUSTER_ID:
                    return new ColorControlCluster(endPoint);
                case ConcentrationMeasurementClusters.CLUSTER_ID:
                    return new ConcentrationMeasurementClusters(endPoint);
                case DescriptorCluster.CLUSTER_ID:
                    return new DescriptorCluster(endPoint);
                case EthernetNetworkDiagnosticsCluster.CLUSTER_ID:
                    return new EthernetNetworkDiagnosticsCluster(endPoint);
                case GeneralDiagnosticsCluster.CLUSTER_ID:
                    return new GeneralDiagnosticsCluster(endPoint);
                case SoftwareDiagnosticsCluster.CLUSTER_ID:
                    return new SoftwareDiagnosticsCluster(endPoint);
                case ThreadNetworkDiagnosticsCluster.CLUSTER_ID:
                    return new ThreadNetworkDiagnosticsCluster(endPoint);
                case WiFiNetworkDiagnosticsCluster.CLUSTER_ID:
                    return new WiFiNetworkDiagnosticsCluster(endPoint);
                case FanControlCluster.CLUSTER_ID:
                    return new FanControlCluster(endPoint);
                case FlowMeasurementCluster.CLUSTER_ID:
                    return new FlowMeasurementCluster(endPoint);
                case GeneralCommissioningCluster.CLUSTER_ID:
                    return new GeneralCommissioningCluster(endPoint);
                case GroupKeyManagementCluster.CLUSTER_ID:
                    return new GroupKeyManagementCluster(endPoint);
                case GroupsCluster.CLUSTER_ID:
                    return new GroupsCluster(endPoint);
                case IdentifyCluster.CLUSTER_ID:
                    return new IdentifyCluster(endPoint);
                case IlluminanceMeasurementCluster.CLUSTER_ID:
                    return new IlluminanceMeasurementCluster(endPoint);
                case FixedLabelCluster.CLUSTER_ID:
                    return new FixedLabelCluster(endPoint);
                case UserLabelCluster.CLUSTER_ID:
                    return new UserLabelCluster(endPoint);
                case LevelControlCluster.CLUSTER_ID:
                    return new LevelControlCluster(endPoint);
                case LowPowerCluster.CLUSTER_ID:
                    return new LowPowerCluster(endPoint);
                case NetworkCommissioningCluster.CLUSTER_ID:
                    return new NetworkCommissioningCluster(endPoint);
                case OccupancySensingCluster.CLUSTER_ID:
                    return new OccupancySensingCluster(endPoint);
                case On_OffCluster.CLUSTER_ID:
                    return new On_OffCluster(endPoint);
                case NodeOperationalCredentialsCluster.CLUSTER_ID:
                    return new NodeOperationalCredentialsCluster(endPoint);
                case SwitchCluster.CLUSTER_ID:
                    return new SwitchCluster(endPoint);
                case TimeSynchronizationCluster.CLUSTER_ID:
                    return new TimeSynchronizationCluster(endPoint);
                case WakeOnLANCluster.CLUSTER_ID:
                    return new WakeOnLANCluster(endPoint);
                case WaterContentMeasurementClusters.CLUSTER_ID:
                    return new WaterContentMeasurementClusters(endPoint);
                default:
                    return new UnknownCluster(clusterId, endPoint);
            }
        }
    }
}