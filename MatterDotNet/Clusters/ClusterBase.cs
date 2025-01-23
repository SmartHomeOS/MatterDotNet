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


using MatterDotNet.Clusters.Appliances;
using MatterDotNet.Clusters.CHIP;
using MatterDotNet.Clusters.Closures;
using MatterDotNet.Clusters.EnergyManagement;
using MatterDotNet.Clusters.General;
using MatterDotNet.Clusters.HVAC;
using MatterDotNet.Clusters.Lighting;
using MatterDotNet.Clusters.MeasurementAndSensing;
using MatterDotNet.Clusters.Media;
using MatterDotNet.Clusters.Misc;
using MatterDotNet.Clusters.NetworkInfrastructure;
using MatterDotNet.Clusters.Robots;
using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using Microsoft.VisualBasic;
using System.Data;
using System.Diagnostics;
using System.Threading.Channels;

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
                case AccessControl.CLUSTER_ID:
                    return new AccessControl(endPoint);
                case AccountLogin.CLUSTER_ID:
                    return new AccountLogin(endPoint);
                case Actions.CLUSTER_ID:
                    return new Actions(endPoint);
                case AdministratorCommissioning.CLUSTER_ID:
                    return new AdministratorCommissioning(endPoint);
                case AirQuality.CLUSTER_ID:
                    return new AirQuality(endPoint);
                case ApplicationBasic.CLUSTER_ID:
                    return new ApplicationBasic(endPoint);
                case ApplicationLauncher.CLUSTER_ID:
                    return new ApplicationLauncher(endPoint);
                case AudioOutput.CLUSTER_ID:
                    return new AudioOutput(endPoint);
                case BallastConfiguration.CLUSTER_ID:
                    return new BallastConfiguration(endPoint);
                case BasicInformation.CLUSTER_ID:
                    return new BasicInformation(endPoint);
                case Binding.CLUSTER_ID:
                    return new Binding(endPoint);
                case BooleanState.CLUSTER_ID:
                    return new BooleanState(endPoint);
                case BooleanStateConfiguration.CLUSTER_ID:
                    return new BooleanStateConfiguration(endPoint);
                case BridgedDeviceBasicInformation.CLUSTER_ID:
                    return new BridgedDeviceBasicInformation(endPoint);
                case Media.Channel.CLUSTER_ID:
                    return new Media.Channel(endPoint);
                case Chime.CLUSTER_ID:
                    return new Chime(endPoint);
                case OTASoftwareUpdateProvider.CLUSTER_ID:
                    return new OTASoftwareUpdateProvider(endPoint);
                case OTASoftwareUpdateRequestor.CLUSTER_ID:
                    return new OTASoftwareUpdateProvider(endPoint);
                case ColorControl.CLUSTER_ID:
                    return new ColorControl(endPoint);
                case CommissionerControl.CLUSTER_ID:
                    return new CommissionerControl(endPoint);
                case CarbonMonoxideConcentrationMeasurement.CLUSTER_ID:
                    return new CarbonMonoxideConcentrationMeasurement(endPoint);
                case CarbonDioxideConcentrationMeasurement.CLUSTER_ID:
                    return new CarbonDioxideConcentrationMeasurement(endPoint);
                case NitrogenDioxideConcentrationMeasurement.CLUSTER_ID:
                    return new NitrogenDioxideConcentrationMeasurement(endPoint);
                case OzoneConcentrationMeasurement.CLUSTER_ID:
                    return new OzoneConcentrationMeasurement(endPoint);
                case PM2_5ConcentrationMeasurement.CLUSTER_ID:
                    return new PM2_5ConcentrationMeasurement(endPoint);
                case FormaldehydeConcentrationMeasurement.CLUSTER_ID:
                    return new FormaldehydeConcentrationMeasurement(endPoint);
                case PM1ConcentrationMeasurement.CLUSTER_ID:
                    return new PM1ConcentrationMeasurement(endPoint);
                case PM10ConcentrationMeasurement.CLUSTER_ID:
                    return new PM10ConcentrationMeasurement(endPoint);
                case TotalVolatileOrganicCompoundsConcentrationMeasurement.CLUSTER_ID:
                    return new TotalVolatileOrganicCompoundsConcentrationMeasurement(endPoint);
                case RadonConcentrationMeasurement.CLUSTER_ID:
                    return new RadonConcentrationMeasurement(endPoint);
                case ContentAppObserver.CLUSTER_ID:
                    return new ContentAppObserver(endPoint);
                case ContentControl.CLUSTER_ID:
                    return new ContentControl(endPoint);
                case ContentLauncher.CLUSTER_ID:
                    return new ContentLauncher(endPoint);
                case Descriptor.CLUSTER_ID:
                    return new Descriptor(endPoint);
                case DeviceEnergyManagement.CLUSTER_ID:
                    return new DeviceEnergyManagement(endPoint);
                case DeviceEnergyManagementMode.CLUSTER_ID:
                    return new DeviceEnergyManagementMode(endPoint);
                case DiagnosticLogs.CLUSTER_ID:
                    return new DiagnosticLogs(endPoint);
                case DishwasherAlarm.CLUSTER_ID:
                    return new DishwasherAlarm(endPoint);
                case DishwasherMode.CLUSTER_ID:
                    return new DishwasherMode(endPoint);
                case DoorLock.CLUSTER_ID:
                    return new DoorLock(endPoint);
                case DemandResponseLoadControl.CLUSTER_ID:
                    return new DemandResponseLoadControl(endPoint);
                case EcosystemInformation.CLUSTER_ID:
                    return new EcosystemInformation(endPoint);
                case ElectricalEnergyMeasurement.CLUSTER_ID:
                    return new ElectricalEnergyMeasurement(endPoint);
                case ElectricalPowerMeasurement.CLUSTER_ID:
                    return new ElectricalPowerMeasurement(endPoint);
                case EnergyEVSE.CLUSTER_ID:
                    return new EnergyEVSE(endPoint);
                case EnergyEVSEMode.CLUSTER_ID:
                    return new EnergyEVSEMode(endPoint);
                case EnergyPreference.CLUSTER_ID:
                    return new EnergyPreference(endPoint);
                case EthernetNetworkDiagnostics.CLUSTER_ID:
                    return new EthernetNetworkDiagnostics(endPoint);
                case FanControl.CLUSTER_ID:
                    return new FanControl(endPoint);
                case FaultInjection.CLUSTER_ID:
                    return new FaultInjection(endPoint);
                case FixedLabel.CLUSTER_ID:
                    return new FixedLabel(endPoint);
                case FlowMeasurement.CLUSTER_ID:
                    return new FlowMeasurement(endPoint);
                case GeneralCommissioning.CLUSTER_ID:
                    return new GeneralCommissioning(endPoint);
                case GeneralDiagnostics.CLUSTER_ID:
                    return new GeneralDiagnostics(endPoint);
                case GroupKeyManagement.CLUSTER_ID:
                    return new GroupKeyManagement(endPoint);
                case Groups.CLUSTER_ID:
                    return new Groups(endPoint);
                case ICDManagement.CLUSTER_ID:
                    return new ICDManagement(endPoint);
                case Identify.CLUSTER_ID:
                    return new Identify(endPoint);
                case IlluminanceMeasurement.CLUSTER_ID:
                    return new IlluminanceMeasurement(endPoint);
                case KeypadInput.CLUSTER_ID:
                    return new KeypadInput(endPoint);
                case LaundryDryerControls.CLUSTER_ID:
                    return new LaundryDryerControls(endPoint);
                case LaundryWasherMode.CLUSTER_ID:
                    return new LaundryWasherMode(endPoint);
                case LevelControl.CLUSTER_ID:
                    return new LevelControl(endPoint);
                case LocalizationConfiguration.CLUSTER_ID:
                    return new LocalizationConfiguration(endPoint);
                case LowPower.CLUSTER_ID:
                    return new LowPower(endPoint);
                case MediaInput.CLUSTER_ID:
                    return new MediaInput(endPoint);
                case MediaPlayback.CLUSTER_ID:
                    return new MediaPlayback(endPoint);
                case General.Messages.CLUSTER_ID:
                    return new General.Messages(endPoint);
                case MicrowaveOvenControl.CLUSTER_ID:
                    return new MicrowaveOvenControl(endPoint);
                case MicrowaveOvenMode.CLUSTER_ID:
                    return new MicrowaveOvenMode(endPoint);
                case ModeSelect.CLUSTER_ID:
                    return new ModeSelect(endPoint);
                case NetworkCommissioning.CLUSTER_ID:
                    return new NetworkCommissioning(endPoint);
                case OccupancySensing.CLUSTER_ID:
                    return new OccupancySensing(endPoint);
                case On_Off.CLUSTER_ID:
                    return new On_Off(endPoint);
                case OperationalCredentials.CLUSTER_ID:
                    return new OperationalCredentials(endPoint);
                case OperationalState.CLUSTER_ID:
                    return new OperationalState(endPoint);
                case OvenCavityOperationalState.CLUSTER_ID:
                    return new OvenCavityOperationalState(endPoint);
                case RVCOperationalState.CLUSTER_ID:
                    return new RVCOperationalState(endPoint);
                case OvenMode.CLUSTER_ID:
                    return new OvenMode(endPoint);
                case PowerSource.CLUSTER_ID:
                    return new PowerSource(endPoint);
                case PowerSourceConfiguration.CLUSTER_ID:
                    return new PowerSourceConfiguration(endPoint);
                case PowerTopology.CLUSTER_ID:
                    return new PowerTopology(endPoint);
                case PressureMeasurement.CLUSTER_ID:
                    return new PressureMeasurement(endPoint);
                case ProxyConfiguration.CLUSTER_ID:
                    return new ProxyConfiguration(endPoint);
                case ProxyDiscovery.CLUSTER_ID:
                    return new ProxyDiscovery(endPoint);
                case ProxyValid.CLUSTER_ID:
                    return new ProxyValid(endPoint);
                case PumpConfigurationandControl.CLUSTER_ID:
                    return new PumpConfigurationandControl(endPoint);
                case PulseWidthModulation.CLUSTER_ID:
                    return new PulseWidthModulation(endPoint);
                case RefrigeratorAlarm.CLUSTER_ID:
                    return new RefrigeratorAlarm(endPoint);
                case RefrigeratorAndTemperatureControlledCabinetMode.CLUSTER_ID:
                    return new RefrigeratorAndTemperatureControlledCabinetMode(endPoint);
                case RelativeHumidityMeasurement.CLUSTER_ID:
                    return new RelativeHumidityMeasurement(endPoint);
                case HEPAFilterMonitoring.CLUSTER_ID:
                    return new HEPAFilterMonitoring(endPoint);
                case ActivatedCarbonFilterMonitoring.CLUSTER_ID:
                    return new ActivatedCarbonFilterMonitoring(endPoint);
                case RVCCleanMode.CLUSTER_ID:
                    return new RVCCleanMode(endPoint);
                case RVCRunMode.CLUSTER_ID:
                    return new RVCRunMode(endPoint);
                case ScenesManagement.CLUSTER_ID:
                    return new ScenesManagement(endPoint);
                case ServiceArea.CLUSTER_ID:
                    return new ServiceArea(endPoint);
                case SmokeCOAlarm.CLUSTER_ID:
                    return new SmokeCOAlarm(endPoint);
                case SoftwareDiagnostics.CLUSTER_ID:
                    return new SoftwareDiagnostics(endPoint);
                case CHIP.Switch.CLUSTER_ID:
                    return new CHIP.Switch(endPoint);
                case TargetNavigator.CLUSTER_ID:
                    return new TargetNavigator(endPoint);
                case TemperatureControl.CLUSTER_ID:
                    return new TemperatureControl(endPoint);
                case TemperatureMeasurement.CLUSTER_ID:
                    return new TemperatureMeasurement(endPoint);
                case Thermostat.CLUSTER_ID:
                    return new Thermostat(endPoint);
                case ThermostatUserInterfaceConfiguration.CLUSTER_ID:
                    return new ThermostatUserInterfaceConfiguration(endPoint);
                case ThreadBorderRouterManagement.CLUSTER_ID:
                    return new ThreadBorderRouterManagement(endPoint);
                case ThreadNetworkDiagnostics.CLUSTER_ID:
                    return new ThreadNetworkDiagnostics(endPoint);
                case ThreadNetworkDirectory.CLUSTER_ID:
                    return new ThreadNetworkDirectory(endPoint);
                case TimeFormatLocalization.CLUSTER_ID:
                    return new TimeFormatLocalization(endPoint);
                case TimeSynchronization.CLUSTER_ID:
                    return new TimeSynchronization(endPoint);
                case General.Timer.CLUSTER_ID:
                    return new General.Timer(endPoint);
                case UnitLocalization.CLUSTER_ID:
                    return new UnitLocalization(endPoint);
                case UserLabel.CLUSTER_ID:
                    return new UserLabel(endPoint);
                case ValveConfigurationandControl.CLUSTER_ID:
                    return new ValveConfigurationandControl(endPoint);
                case WakeonLAN.CLUSTER_ID:
                    return new WakeonLAN(endPoint);
                case LaundryWasherControls.CLUSTER_ID:
                    return new LaundryWasherControls(endPoint);
                case WaterHeaterManagement.CLUSTER_ID:
                    return new WaterHeaterManagement(endPoint);
                case WaterHeaterMode.CLUSTER_ID:
                    return new WaterHeaterMode(endPoint);
                case WebRTCTransportProvider.CLUSTER_ID:
                    return new WebRTCTransportProvider(endPoint);
                case WiFiNetworkDiagnostics.CLUSTER_ID:
                    return new WiFiNetworkDiagnostics(endPoint);
                case WiFiNetworkManagement.CLUSTER_ID:
                    return new WiFiNetworkManagement(endPoint);
                case WindowCovering.CLUSTER_ID:
                    return new WindowCovering(endPoint);
                default:
                    return new UnknownCluster(clusterId, endPoint);
            }
        }
    }
}