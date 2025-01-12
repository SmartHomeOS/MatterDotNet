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
//
// WARNING: This file was auto-generated. Do not edit.


using MatterDotNet.Clusters.Application;
using MatterDotNet.Clusters.Utility;
using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Payloads;
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

        public async Task<List<ushort>?> GetAttributeList(SecureSession session)
        {
            object ret = (await GetAttribute(session, 0xFFFB))!;
            return (List<ushort>?)ret;
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

        protected static T[]? GetOptionalArrayField<T>(InvokeResponseIB resp, int fieldNumber) where T : TLVPayload
        {
            if (fieldNumber >= ((object[])resp.Command!.CommandFields!).Length)
                return null;
            return GetArrayField<T>(resp, fieldNumber);
        }

        /// <summary>
        /// Gets a required field from an Invoke Response
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="fieldNumber"></param>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        protected static T[] GetArrayField<T>(InvokeResponseIB resp, int fieldNumber) where T : TLVPayload
        {
            object[] fields = (object[])resp.Command!.CommandFields!;
            if (fieldNumber >= fields.Length)
                throw new DataException("Field " + fieldNumber + " is missing");
            object[] array = (object[])fields[fieldNumber]!;
            T[] ret = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
                ret[i] = ((T)Activator.CreateInstance(typeof(T), [array[i]])!);
            return ret;
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
            object? data = await InteractionManager.GetAttribute(session, endPoint, cluster, attribute);
            if (!nullable && data == null)
                throw new ConstraintException("Attribute " + attribute + " was null");
            return data;
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
            await InteractionManager.SetAttribute(session, endPoint, cluster, attribute, value);
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
            return InteractionManager.ValidateResponse(resp, endPoint);
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
                case ChannelCluster.CLUSTER_ID:
                    return new ChannelCluster(endPoint);
                case ColorControlCluster.CLUSTER_ID:
                    return new ColorControlCluster(endPoint);
                case ConcentrationMeasurementClusters.CLUSTER_ID:
                    return new ConcentrationMeasurementClusters(endPoint);
                case ContentAppObserverCluster.CLUSTER_ID:
                    return new ContentAppObserverCluster(endPoint);
                case ContentControlCluster.CLUSTER_ID:
                    return new ContentControlCluster(endPoint);
                case ContentLauncherCluster.CLUSTER_ID:
                    return new ContentLauncherCluster(endPoint);
                case DescriptorCluster.CLUSTER_ID:
                    return new DescriptorCluster(endPoint);
                case DeviceEnergyManagementCluster.CLUSTER_ID:
                    return new DeviceEnergyManagementCluster(endPoint);
                case DiagnosticLogsCluster.CLUSTER_ID:
                    return new DiagnosticLogsCluster(endPoint);
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
                case DishwasherAlarmCluster.CLUSTER_ID:
                    return new DishwasherAlarmCluster(endPoint);
                case DoorLockCluster.CLUSTER_ID:
                    return new DoorLockCluster(endPoint);
                case ElectricalEnergyMeasurementCluster.CLUSTER_ID:
                    return new ElectricalEnergyMeasurementCluster(endPoint);
                case ElectricalPowerMeasurementCluster.CLUSTER_ID:
                    return new ElectricalPowerMeasurementCluster(endPoint);
                case EnergyEVSECluster.CLUSTER_ID:
                    return new EnergyEVSECluster(endPoint);
                case EnergyPreferenceCluster.CLUSTER_ID:
                    return new EnergyPreferenceCluster(endPoint);
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
                case ICDManagementCluster.CLUSTER_ID:
                    return new ICDManagementCluster(endPoint);
                case IdentifyCluster.CLUSTER_ID:
                    return new IdentifyCluster(endPoint);
                case IlluminanceMeasurementCluster.CLUSTER_ID:
                    return new IlluminanceMeasurementCluster(endPoint);
                case FixedLabelCluster.CLUSTER_ID:
                    return new FixedLabelCluster(endPoint);
                case UserLabelCluster.CLUSTER_ID:
                    return new UserLabelCluster(endPoint);
                case LaundryDryerControlsCluster.CLUSTER_ID:
                    return new LaundryDryerControlsCluster(endPoint);
                case LaundryWasherControlsCluster.CLUSTER_ID:
                    return new LaundryWasherControlsCluster(endPoint);
                case LevelControlCluster.CLUSTER_ID:
                    return new LevelControlCluster(endPoint);
                case LocalizationConfigurationCluster.CLUSTER_ID:
                    return new LocalizationConfigurationCluster(endPoint);
                case TimeFormatLocalizationCluster.CLUSTER_ID:
                    return new TimeFormatLocalizationCluster(endPoint);
                case UnitLocalizationCluster.CLUSTER_ID:
                    return new UnitLocalizationCluster(endPoint);
                case LowPowerCluster.CLUSTER_ID:
                    return new LowPowerCluster(endPoint);
                case MediaInputCluster.CLUSTER_ID:
                    return new MediaInputCluster(endPoint);
                case MediaPlaybackCluster.CLUSTER_ID:
                    return new MediaPlaybackCluster(endPoint);
                case NetworkCommissioningCluster.CLUSTER_ID:
                    return new NetworkCommissioningCluster(endPoint);
                case OccupancySensingCluster.CLUSTER_ID:
                    return new OccupancySensingCluster(endPoint);
                case On_OffCluster.CLUSTER_ID:
                    return new On_OffCluster(endPoint);
                case NodeOperationalCredentialsCluster.CLUSTER_ID:
                    return new NodeOperationalCredentialsCluster(endPoint);
                case OTASoftwareUpdateProviderCluster.CLUSTER_ID:
                    return new OTASoftwareUpdateProviderCluster(endPoint);
                case OTASoftwareUpdateRequestorCluster.CLUSTER_ID:
                    return new OTASoftwareUpdateRequestorCluster(endPoint);
                case PressureMeasurementCluster.CLUSTER_ID:
                    return new PressureMeasurementCluster(endPoint);
                case ProxyConfigurationCluster.CLUSTER_ID:
                    return new ProxyConfigurationCluster(endPoint);
                case ProxyDiscoveryCluster.CLUSTER_ID:
                    return new ProxyDiscoveryCluster(endPoint);
                case RefrigeratorAlarmCluster.CLUSTER_ID:
                    return new RefrigeratorAlarmCluster(endPoint);
                case ResourceMonitoringClusters.CLUSTER_ID:
                    return new ResourceMonitoringClusters(endPoint);
                case ScenesManagementCluster.CLUSTER_ID:
                    return new ScenesManagementCluster(endPoint);
                case SmokeCOAlarmCluster.CLUSTER_ID:
                    return new SmokeCOAlarmCluster(endPoint);
                case SwitchCluster.CLUSTER_ID:
                    return new SwitchCluster(endPoint);
                case TemperatureControlCluster.CLUSTER_ID:
                    return new TemperatureControlCluster(endPoint);
                case TemperatureMeasurementCluster.CLUSTER_ID:
                    return new TemperatureMeasurementCluster(endPoint);
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