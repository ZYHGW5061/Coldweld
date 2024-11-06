using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.UtilityHelper;

namespace ConfigurationClsLib
{
    [Serializable]
    [XmlRoot("HardwareConfiguration")]
    public class HardwareConfiguration
    {
        #region member
        private static string _configPath = Path.Combine($@"{System.Environment.CurrentDirectory}\Config", "HardwareConfiguration.xml");
        private static readonly object _lockObj = new object();
        private static volatile HardwareConfiguration _instance = LoadConfig();
        public static HardwareConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HardwareConfiguration();
                        }
                    }
                }
                return _instance;
            }
        }
        private HardwareConfiguration()
        {

        }


        //public static HardwareConfiguration _config = LoadConfig();
        private static HardwareConfiguration LoadConfig()
        {
            HardwareConfiguration ret = new HardwareConfiguration();
            try
            {
                ret=XmlSerializeHelper.XmlDeserializeFromFile<HardwareConfiguration>(_configPath, Encoding.UTF8);
            }
            catch(Exception ex)
            {
                LogRecorder.RecordLog(EnumLogContentType.Error, "Load  HardwareConfiguration Error.",ex);
            }
            return ret;
        }
        public void SaveConfig()
        {
            XmlSerializeHelper.XmlSerializeToFile(this, _configPath, Encoding.UTF8);
        }
        #endregion
        # region XMLConfig


        [XmlElement("Stage")]
        public StageConfig StageConfig { get; set; }

        [XmlArray("Cameras"), XmlArrayItem(typeof(CameraConfig))]
        public List<CameraConfig> CameraConfigList { get; set; }

        [XmlArray("RingLightControllers"), XmlArrayItem(typeof(LEDConfig))]
        public List<LEDConfig> RingLightControllerConfigList { get; set; }

        [XmlArray("DirectLightControllers"), XmlArrayItem(typeof(LEDConfig))]
        public List<LEDConfig> DirectLightControllerConfigList { get; set; }

        [XmlArray("Lenses"), XmlArrayItem(typeof(LensConfig))]
        public List<LensConfig> LensList { get; set; }
        [XmlElement("PowerController")]
        public PowerControllerConfig PowerControllerConfig { get; set; }
        [XmlElement("LaserSensorController")]
        public LaserSensorControllerConfig LaserSensorControllerConfig { get; set; }
        [XmlElement("DynamometerController")]
        public DynamometerControllerConfig DynamometerControllerConfig { get; set; }
        [XmlElement("JoyStickController")]
        public JoyStickControllerConfig JoyStickControllerConfig { get; set; }

        [XmlArray("TemperatureControllers"), XmlArrayItem(typeof(TemperatureControllerConfig))]
        public List<TemperatureControllerConfig> TemperatureControllerConfig { get; set; }

        [XmlArray("TurboMolecularPumpControllers"), XmlArrayItem(typeof(TurboMolecularPumpControllerConfig))]
        public List<TurboMolecularPumpControllerConfig> TurboMolecularPumpControllerConfig { get; set; }

        [XmlArray("VacuumGaugeControllers"), XmlArrayItem(typeof(VacuumGaugeControllerConfig))]
        public List<VacuumGaugeControllerConfig> VacuumGaugeControllerConfig { get; set; }

        [XmlArray("DewPointMeterControllers"), XmlArrayItem(typeof(DewPointMeterControllerConfig))]
        public List<DewPointMeterControllerConfig> DewPointMeterControllerConfig { get; set; }

        [XmlArray("PressureSensorControllers"), XmlArrayItem(typeof(PressureSensorControllerConfig))]
        public List<PressureSensorControllerConfig> PressureSensorControllerConfig { get; set; }

        #endregion



        #region 外部变量

        [XmlIgnore]
        public LEDConfig TrackRingLightConfig
        {
            get { return RingLightControllerConfigList.FirstOrDefault(i => i.LightFieldPosition == EnumLightSourceType.TrackRingField); }
        }
        [XmlIgnore]
        public LEDConfig WeldRingLightConfig
        {
            get { return RingLightControllerConfigList.FirstOrDefault(i => i.LightFieldPosition == EnumLightSourceType.WeldRingField); }
        }

        
        #endregion 外部变量
    }

    #region Serial Port
    [Serializable]
    public class SerialPortConfig
    {
        [XmlAttribute("Port")]
        public int Port { get; set; }

        [XmlAttribute("StopBits")]
        public StopBits StopBits { get; set; }

        [XmlAttribute("Parity")]
        public Parity Parity { get; set; }

        [XmlAttribute("DataBits")]
        public int DataBits { get; set; }

        [XmlAttribute("BaudRate")]
        public int BaudRate { get; set; }

        [XmlAttribute("DeviceAddress")]
        public int DeviceAddress { get; set; }
    }
    #endregion



    [Serializable]
    [XmlType("EthernetConfig")]
    public class EthernetConfig
    {
        [XmlAttribute("IP")]
        public string IP { get; set; }
        [XmlAttribute("Port")]
        public int Port { get; set; }
    }


    #region Stage
    [Serializable]
    public class StageConfig
    {
        [XmlAttribute("RunningType")]
        public EnumRunningType RunningType { get; set; }

        [XmlElement("RemoteAddress")]
        public string RemoteAddress { get; set; }

        [XmlElement("LoadWaferPosition")]
        public BaseCoordinateConfig LoadWaferPosition { get; set; }

        [XmlElement("MovingSpeed")]
        public StageSpeedConfig MovingSpeed { get; set; }

        [XmlElement("ReviewSpeed")]
        public StageSpeedConfig ReviewSpeed { get; set; }

        [XmlElement("AreaCaptureFPS")]
        public int AreaCaptureFPS { get; set; }

        [XmlElement("DriftSpeed")]
        public double DriftSpeed { get; set; }

        [XmlElement("PreProcessPositionY")]
        public int PreProcessPositionY { get; set; }

        [XmlArray("Axises"), XmlArrayItem(typeof(AxisConfig))]
        public List<AxisConfig> AxisConfigList { get; set; }

        public AxisConfig GetAixsConfigByType(EnumStageAxis axisType)
        {
            if (AxisConfigList != null)
            {
                return AxisConfigList.Find(n => n.Type == axisType);
            }
            return null;
        }
    }

    

    [Serializable]
    public class StageSpeedConfig
    {
        [XmlAttribute("X")]
        public double X { get; set; }

        [XmlAttribute("Y")]
        public double Y { get; set; }
    }


    [XmlType("Axis")]
    public class AxisConfig
    {
        [XmlAttribute("Type")]
        public EnumStageAxis Type { get; set; }

        [XmlAttribute("Index")]
        public int Index { get; set; }




        [XmlAttribute("AxisSpeed")]
        public double AxisSpeed { get; set; }
        [XmlAttribute("MaxAxisSpeed")]
        public double MaxAxisSpeed { get; set; }
        [XmlAttribute("Deceleration")]
        public double Deceleration { get; set; }

        [XmlAttribute("Acceleration")]
        public double Acceleration { get; set; }

        [XmlAttribute("SoftRightLimit")]
        public double SoftRightLimit { get; set; }

        [XmlAttribute("SoftLeftLimit")]
        public double SoftLeftLimit { get; set; }

        [XmlAttribute("Smooth")]
        public double Smooth { get; set; }
        [XmlAttribute("SmoothTime")]
        public int SmoothTime { get; set; }

        [XmlAttribute("CirclePulse")]
        public int CirclePulse { get; set; }
        [XmlAttribute("Lead")]
        public double Lead { get; set; }

    }

    #endregion Stage

    #region Camera
    [XmlType("Camera")]
    public class CameraConfig
    {
        [XmlAttribute("RunningType")]
        public EnumRunningType RunningType { get; set; }
        [XmlAttribute("IP")]
        public string IP { get; set; }
        [XmlAttribute("Port")]
        public int Port { get; set; }
        [XmlAttribute("CameraPort")]
        public string CameraPort { get; set; }
        [XmlElement("CameraProducer")]
        public EnumCameraProducer CameraProducer { get; set; }

        [XmlElement("CameraName")]
        public string CameraName { get; set; }
        [XmlElement("CameraType")]
        public EnumCameraType CameraType { get; set; }

        [XmlElement("ImageType")]
        public EnumImageData ImageType { get; set; }

        [XmlElement("DMAIndex")]
        public int DMAIndex { get; set; }

        [XmlElement("TapGeometry")]
        public string TapGeometry { get; set; }

        [XmlElement("SerialNumber")]
        public string SerialNumber { get; set; }

        [XmlElement("WidthPixelSize")]
        public float WidthPixelSize { get; set; }
        [XmlElement("HeightPixelSize")]
        public float HeightPixelSize { get; set; }
        [XmlElement("Angle")]
        public float Angle { get; set; }
        [XmlElement("CameraInstallationAngle")]
        public float CameraInstallationAngle { get; set; }

        [XmlElement("ImageSizeWidth")]
        public int ImageSizeWidth { get; set; }

        [XmlElement("ImageSizeHeight")]
        public int ImageSizeHeight { get; set; }

        [XmlElement("ExposureTime")]
        public int ExposureTime { get; set; }

        [XmlElement("CameraWindowImageScale")]
        public float CameraWindowImageScale { get; set; }
    }

    #endregion Camera

    #region LightSourceControllers

    [Serializable]
    [XmlType("LEDController")]
    public class LEDConfig
    {
        public LEDConfig()
        {
            CommunicatorID = "";

        }
        [XmlAttribute("RunningType")]
        public EnumRunningType RunningType { get; set; }

        [XmlElement("LightProducer")]
        public EnumLightProducer LightProducer { get; set; }
        [XmlElement("LightFieldPosition")]
        public EnumLightSourceType LightFieldPosition { get; set; }
        [XmlElement("CommunicationType")]
        public EnumCommunicationType CommunicationType { get; set; }

        [XmlElement("LightSourceName")]
        public string LightSourceName { get; set; }

        [XmlElement("IPAddress")]
        public string IPAddress { get; set; }

        [XmlElement("Port")]
        public int Port { get; set; }

        [XmlElement("SerialCommunicator")]
        public SerialPortConfig SerialCommunicator { get; set; }
        [XmlElement("CommunicatorID")]
        public string CommunicatorID { get; set; }
        [XmlElement("ChannelNumber")]
        public int ChannelNumber { get; set; }

        [XmlElement("MinIntensity")]
        public float MinIntensity { get; set; }

        [XmlElement("MaxIntensity")]
        public float MaxIntensity { get; set; }

        [XmlElement("ExposureMonitor")]
        public ExposureMonitorConfig ExposureMonitor { get; set; }
    }



    [Serializable]
    public class ExposureMonitorConfig
    {
        [XmlAttribute("ExposureThreshold"), DataMember(Order = 1)]
        public long ExposureThreshold { get; set; }

        [XmlAttribute("CurExposureCounter"), DataMember(Order = 2)]
        public long CurExposureCounter { get; set; }

        [XmlAttribute("Enabled"), DataMember(Order = 3)]
        public bool Enabled { get; set; }
    }
    [Serializable, XmlType("Lens")]
    public class LensConfig
    {
        [XmlAttribute("UserCamera")]
        public EnumCameraType UserCamera { get; set; }

        [XmlAttribute("CoarseFocusValue")]
        public double CoarseFocusValue { get; set; }

        [XmlAttribute("CalibratedMag")]
        public double CalibratedMagnification { get; set; }

        [XmlAttribute("StandardCalibratedMag")]
        public double StandardCalibratedMag { get; set; }
    }

    [Serializable]
    public class JoyStickControllerConfig
    {
        [XmlAttribute("RunningType")]
        public EnumRunningType RunningType { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }
    }
    [Serializable]
    [XmlType("PowerController")]
    public class PowerControllerConfig
    {
        [XmlAttribute("RunningType")]
        public EnumRunningType RunningType { get; set; }

        [XmlElement("ControllerProducer")]
        public EnumPowerControllerProducer ControllerProducer { get; set; }

        [XmlElement("SerialCommunicator")]
        public SerialPortConfig SerialCommunicator { get; set; }
    }

    [Serializable]
    public enum EnumPowerControllerProducer { None, Default }
    #endregion

    [Serializable]
    [XmlType("LaserSensorController")]
    public class LaserSensorControllerConfig
    {
        [XmlAttribute("RunningType")]
        public EnumRunningType RunningType { get; set; }

        [XmlElement("SerialCommunicator")]
        public SerialPortConfig SerialCommunicator { get; set; }
    }
    [Serializable]
    [XmlType("DynamometerController")]
    public class DynamometerControllerConfig
    {
        [XmlAttribute("RunningType")]
        public EnumRunningType RunningType { get; set; }

        [XmlElement("SerialCommunicator")]
        public SerialPortConfig SerialCommunicator { get; set; }
    }

    [Serializable]
    [XmlType("TemperatureController")]
    public class TemperatureControllerConfig
    {
        [XmlAttribute("RunningType")]
        public EnumRunningType RunningType { get; set; }

        [XmlElement("TemperatureProducer")]
        public EnumTemperatureProducer TemperatureProducer { get; set; }
        [XmlElement("TemperatureFieldPosition")]
        public EnumTemperatureType TemperatureFieldPosition { get; set; }
        [XmlElement("CommunicationType")]
        public EnumCommunicationType CommunicationType { get; set; }

        [XmlElement("TemperatureName")]
        public string TemperatureName { get; set; }

        [XmlElement("IPAddress")]
        public string IPAddress { get; set; }

        [XmlElement("Port")]
        public int Port { get; set; }

        [XmlElement("SerialCommunicator")]
        public SerialPortConfig SerialCommunicator { get; set; }
        [XmlElement("CommunicatorID")]
        public string CommunicatorID { get; set; }
        [XmlElement("ChannelNumber")]
        public int ChannelNumber { get; set; }

        [XmlElement("MinIntensity")]
        public float MinIntensity { get; set; }

        [XmlElement("MaxIntensity")]
        public float MaxIntensity { get; set; }

        [XmlElement("ExposureMonitor")]
        public ExposureMonitorConfig ExposureMonitor { get; set; }
    }

    [XmlType("TurboMolecularPumpController")]
    public class TurboMolecularPumpControllerConfig
    {
        [XmlAttribute("RunningType")]
        public EnumRunningType RunningType { get; set; }

        [XmlElement("TurboMolecularPumpProducer")]
        public EnumTurboMolecularPumpProducer TurboMolecularPumpProducer { get; set; }
        [XmlElement("TurboMolecularPumpFieldPosition")]
        public EnumTurboMolecularPumpType TurboMolecularPumpFieldPosition { get; set; }
        [XmlElement("CommunicationType")]
        public EnumCommunicationType CommunicationType { get; set; }

        [XmlElement("TurboMolecularPumpName")]
        public string TurboMolecularPumpName { get; set; }

        [XmlElement("IPAddress")]
        public string IPAddress { get; set; }

        [XmlElement("Port")]
        public int Port { get; set; }

        [XmlElement("SerialCommunicator")]
        public SerialPortConfig SerialCommunicator { get; set; }
        [XmlElement("CommunicatorID")]
        public string CommunicatorID { get; set; }
        [XmlElement("ChannelNumber")]
        public int ChannelNumber { get; set; }

        [XmlElement("MinIntensity")]
        public float MinIntensity { get; set; }

        [XmlElement("MaxIntensity")]
        public float MaxIntensity { get; set; }

        [XmlElement("ExposureMonitor")]
        public ExposureMonitorConfig ExposureMonitor { get; set; }
    }


    [XmlType("VacuumGaugeController")]
    public class VacuumGaugeControllerConfig
    {
        [XmlAttribute("RunningType")]
        public EnumRunningType RunningType { get; set; }

        [XmlElement("VacuumGaugeProducer")]
        public EnumVacuumGaugeProducer VacuumGaugeProducer { get; set; }
        [XmlElement("VacuumGaugeFieldPosition")]
        public EnumVacuumGaugeType VacuumGaugeFieldPosition { get; set; }
        [XmlElement("CommunicationType")]
        public EnumCommunicationType CommunicationType { get; set; }

        [XmlElement("VacuumGaugeName")]
        public string VacuumGaugeName { get; set; }

        [XmlElement("IPAddress")]
        public string IPAddress { get; set; }

        [XmlElement("Port")]
        public int Port { get; set; }

        [XmlElement("SerialCommunicator")]
        public SerialPortConfig SerialCommunicator { get; set; }
        [XmlElement("CommunicatorID")]
        public string CommunicatorID { get; set; }
        [XmlElement("ChannelNumber")]
        public int ChannelNumber { get; set; }

        [XmlElement("MinIntensity")]
        public float MinIntensity { get; set; }

        [XmlElement("MaxIntensity")]
        public float MaxIntensity { get; set; }

        [XmlElement("ExposureMonitor")]
        public ExposureMonitorConfig ExposureMonitor { get; set; }
    }


    [XmlType("DewPointMeterController")]
    public class DewPointMeterControllerConfig
    {
        [XmlAttribute("RunningType")]
        public EnumRunningType RunningType { get; set; }

        [XmlElement("DewPointMeterProducer")]
        public EnumDewPointMeterProducer DewPointMeterProducer { get; set; }
        [XmlElement("DewPointMeterFieldPosition")]
        public EnumDewPointMeterType DewPointMeterFieldPosition { get; set; }
        [XmlElement("CommunicationType")]
        public EnumCommunicationType CommunicationType { get; set; }

        [XmlElement("DewPointMeterName")]
        public string DewPointMeterName { get; set; }

        [XmlElement("IPAddress")]
        public string IPAddress { get; set; }

        [XmlElement("Port")]
        public int Port { get; set; }

        [XmlElement("SerialCommunicator")]
        public SerialPortConfig SerialCommunicator { get; set; }
        [XmlElement("CommunicatorID")]
        public string CommunicatorID { get; set; }
        [XmlElement("ChannelNumber")]
        public int ChannelNumber { get; set; }

        [XmlElement("MinIntensity")]
        public float MinIntensity { get; set; }

        [XmlElement("MaxIntensity")]
        public float MaxIntensity { get; set; }

        [XmlElement("ExposureMonitor")]
        public ExposureMonitorConfig ExposureMonitor { get; set; }
    }


    [XmlType("PressureSensorController")]
    public class PressureSensorControllerConfig
    {
        [XmlAttribute("RunningType")]
        public EnumRunningType RunningType { get; set; }

        [XmlElement("PressureSensorProducer")]
        public EnumPressureSensorProducer PressureSensorProducer { get; set; }
        [XmlElement("PressureSensorFieldPosition")]
        public EnumPressureSensorType PressureSensorFieldPosition { get; set; }
        [XmlElement("CommunicationType")]
        public EnumCommunicationType CommunicationType { get; set; }

        [XmlElement("PressureSensorName")]
        public string PressureSensorName { get; set; }

        [XmlElement("IPAddress")]
        public string IPAddress { get; set; }

        [XmlElement("Port")]
        public int Port { get; set; }

        [XmlElement("SerialCommunicator")]
        public SerialPortConfig SerialCommunicator { get; set; }
        [XmlElement("CommunicatorID")]
        public string CommunicatorID { get; set; }
        [XmlElement("ChannelNumber")]
        public int ChannelNumber { get; set; }

        [XmlElement("MinIntensity")]
        public float MinIntensity { get; set; }

        [XmlElement("MaxIntensity")]
        public float MaxIntensity { get; set; }

        [XmlElement("ExposureMonitor")]
        public ExposureMonitorConfig ExposureMonitor { get; set; }
    }


}
