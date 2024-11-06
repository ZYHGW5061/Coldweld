using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StageManagerClsLib;
using CameraControllerClsLib;
using LightControllerManagerClsLib;
using LightControllerClsLib;
using StageControllerClsLib;
using PowerControllerManagerClsLib;
using BoardCardControllerClsLib;
using LaserSensorManagerClsLib;
using DynamometerManagerClsLib;
using LaserSensorControllerClsLib;
using JoyStickManagerClsLib;
using IJoyStickControllerClsLib;using System.IO.Ports;
using TemperatureControllerClsLib;
using IOUtilityClsLib;
using TurboMolecularPumpControllerClsLib;
using VacuumGaugeControllerClsLib;
using PressureSensorControllerClsLib;
using DewPointMeterControllerClsLib;

namespace HardwareManagerClsLib
{
    public class HardwareManager
    {
        private static readonly object _lockObj = new object();
        private static volatile HardwareManager _instance = null;
        public SerialPort PowerControl = new SerialPort();
        public static HardwareManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HardwareManager();
                        }
                    }
                }
                return _instance;
            }
        }


        List<SerialPort> serials = new List<SerialPort>();


        private HardwareManager()
        {

        }
        public void Initialize()
        {
        }
        protected HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }
        //private CameraManager _cameraManager
        //{
        //    get { return CameraManager.Instance; }
        //}
        private StageManager _stageManager
        {
            get { return StageManager.Instance; }
        }

        private BoardCardManager _boardCardManager
        {
            get { return BoardCardManager.Instance; }
        }


        private CameraManager _cameraManager
        {
            get { return CameraManager.Instance; }
        }
        private LightControllerManager _LightControllerManager
        {
            get { return LightControllerManager.Instance; }
        }
        

        public ILightSourceController TrackRingLightController
        {
            get
            {
                return _LightControllerManager.GetLightController(EnumLightSourceType.TrackRingField);
            }
        }
        public ILightSourceController WeldRingLightController
        {
            get
            {
                return _LightControllerManager.GetLightController(EnumLightSourceType.WeldRingField);
            }
        }
       

        /// <summary>
        /// 
        /// </summary>
        public ICameraController TrackCamera
        {
            get { return _cameraManager.GetCameraByID(EnumCameraType.TrackCamera); }
        }
        public ICameraController WeldCamera
        {
            get { return _cameraManager.GetCameraByID(EnumCameraType.WeldCamera); }
        }

        public IJobStickController JobStick
        {
            get { return JoyStickManager.Instance.GetCurrentController(); }
        }

        public IPowerController PowerController
        {
            get { return PowerControllerManager.Instance.GetCurrentHardware(); }
        }

        

        private TemperatureControllerManager _TemperatureControllerManager
        {
            get { return TemperatureControllerManager.Instance; }
        }

        public ITemperatureController OvenBox1LowTemperatureController
        {
            get { return _TemperatureControllerManager.GetTemperatureController( EnumTemperatureType.OvenBox1Low); }
        }

        public ITemperatureController OvenBox1UpTemperatureController
        {
            get { return _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Up); }
        }

        public ITemperatureController OvenBox2LowTemperatureController
        {
            get { return _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low); }
        }

        public ITemperatureController OvenBox2UpTemperatureController
        {
            get { return _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low); }
        }

        /// <summary>
        /// 根据硬件类型连接硬件的
        /// </summary>
        /// <param name="type"></param>
        public void ConnectHardware(EnumHardwareType type)
        {
            switch (type)
            {
                case EnumHardwareType.Stage:
                    _stageManager.Initialize();
                    //_boardCardManager.Initialize();
                    //JoyStickManager.Instance.Initialize();
                    IOUtilityHelper.Instance.Start();
                    //IOUtilityHelper.Instance.TurnonTowerYellowLight();
                    break;
                case EnumHardwareType.Camera:
                    _cameraManager.InitializeCameras();

                    break;
                case EnumHardwareType.Light:
                    _LightControllerManager.Initialize();
                    break;
                case EnumHardwareType.ControlBoard:
                    //PLCControllerManager.Instance.Initialize();
                    break;
                case EnumHardwareType.PowerController:
                    PowerControllerManager.Instance.Initialize();
                    break;
                case EnumHardwareType.TemperatureController:
                    TemperatureControllerManager.Instance.Initialize();
                    break;
                case EnumHardwareType.TurboMolecularPumpController:
                    TurboMolecularPumpControllerManager.Instance.Initialize();
                    break;
                case EnumHardwareType.VacuumGaugeController:
                    VacuumGaugeControllerManager.Instance.Initialize();
                    break;
                case EnumHardwareType.DewPointMeterController:
                    DewPointMeterControllerManager.Instance.Initialize();
                    break;
                case EnumHardwareType.PressureSensorController:
                    PressureSensorControllerManager.Instance.Initialize();
                    break;
                //case EnumHardwareType.LaserSensor:
                //    LaserSensorManager.Instance.Initialize(PowerControl);
                //    break;
                //case EnumHardwareType.Dynamometer:
                //    DynamometerManager.Instance.Initialize(PowerControl);
                //    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 根据设备类型断开设备连接
        /// </summary>
        /// <param name="type"></param>
        public void DisconnectHardware(EnumHardwareType type)
        {
            try
            {
                switch (type)
                {
                    case EnumHardwareType.Stage:
                        IOUtilityHelper.Instance.Stop();
                            _stageManager.Shutdown();
                        break;
                    case EnumHardwareType.Camera:
                            _cameraManager.Shutdown();
                        break;
                    case EnumHardwareType.Light:
                        _LightControllerManager.Shutdown();
                        break;
                    case EnumHardwareType.ControlBoard:
                        //PLCControllerManager.Instance.Shutdown();
                        break;
                    case EnumHardwareType.PowerController:
                        PowerControllerManager.Instance.Shutdown();
                        break;
                    case EnumHardwareType.TemperatureController:
                        TemperatureControllerManager.Instance.Shutdown();
                        break;
                    case EnumHardwareType.TurboMolecularPumpController:
                        TurboMolecularPumpControllerManager.Instance.Shutdown();
                        break;
                    case EnumHardwareType.VacuumGaugeController:
                        VacuumGaugeControllerManager.Instance.Shutdown();
                        break;
                    case EnumHardwareType.DewPointMeterController:
                        DewPointMeterControllerManager.Instance.Shutdown();
                        break;
                    case EnumHardwareType.PressureSensorController:
                        PressureSensorControllerManager.Instance.Shutdown();
                        break;
                    default:
                        break;
                }
            }
            catch { }
        }

        /// <summary>
        /// 断开连接所有硬件
        /// </summary>
        public void DisconnectHardwares()
        {
            DisconnectHardware(EnumHardwareType.ControlBoard);

            DisconnectHardware(EnumHardwareType.Stage);

            DisconnectHardware(EnumHardwareType.Light);

            DisconnectHardware(EnumHardwareType.Camera);

            DisconnectHardware(EnumHardwareType.PowerController);

            DisconnectHardware(EnumHardwareType.TemperatureController);

            DisconnectHardware(EnumHardwareType.TurboMolecularPumpController);

            DisconnectHardware(EnumHardwareType.VacuumGaugeController);

            DisconnectHardware(EnumHardwareType.DewPointMeterController);

            DisconnectHardware(EnumHardwareType.PressureSensorController);

        }
        /// <summary>
        /// Stage运动平台
        /// </summary>
        public IStageController Stage
        {
            get { return StageManager.Instance.GetCurrentController(); }
        }
        
        //public IPowerController PowerController
        //{
        //    get { return PowerControllerManager.Instance.GetCurrentHardware(); }
        //}
        public CameraConfig GetCameraParametersByCameraType(string type)
        {
            CameraConfig ret = null;
            if (HardwareConfiguration.Instance.CameraConfigList.Any(i => i.CameraType == (EnumCameraType)Enum.Parse(typeof(EnumCameraType), type)))
            {
                ret = HardwareConfiguration.Instance.CameraConfigList.FirstOrDefault(i => i.CameraType == (EnumCameraType)Enum.Parse(typeof(EnumCameraType), type));
            }
            return ret;
        }
        public LensConfig CurrentLensConfig
        {
            get
            {
                LensConfig ret = null;
                //if (HardwareConfiguration.Instance.LensList.Any(i => i.UserCamera == _cameraManager.CurrentCameraType))
                //{
                //    ret = HardwareConfiguration.Instance.LensList.FirstOrDefault(i => i.UserCamera == _cameraManager.CurrentCameraType);
                //}
                return ret;
            }
        }
        /// <summary>
        /// 检测Camera的有效性
        /// </summary>
        /// <returns></returns>
        public bool CheckCameraEngineValid()
        {
            if (TrackCamera != null && WeldCamera != null)
            {
                return TrackCamera.IsConnect & WeldCamera.IsConnect;
            }
            return true;
        }
        public bool CheckPLCEngineValid()
        {
            return true;
        }
        public bool CheckStageEngineValid()
        {
            return true;
        }
        public bool CheckPowerControllerValid()
        {
            //if (PowerController != null)
            //{
            //    return PowerController.IsConnect;
            //}
            return true;
        }
        public bool CheckLaserSensorControllerValid()
        {
            //if (LaserSensorManager.Instance != null)
            //{
            //    return LaserSensorManager.Instance.IsConnect;
            //}
            return true;
        }

        public bool CheckDynamometerControllerValid()
        {
            //if (LaserSensorManager.Instance != null)
            //{
            //    return LaserSensorManager.Instance.IsConnect;
            //}
            return true;
        }
        public bool CheckTemperatureControllerValid()
        {
            //if (PowerController != null)
            //{
            //    return PowerController.IsConnect;
            //}
            return true;
        }

        public bool CheckTurboMolecularPumpControllerValid()
        {
            //if (PowerController != null)
            //{
            //    return PowerController.IsConnect;
            //}
            return true;
        }

        public bool CheckVacuumGaugeControllerValid()
        {
            //if (PowerController != null)
            //{
            //    return PowerController.IsConnect;
            //}
            return true;
        }

        public bool CheckDewPointMeterControllerValid()
        {
            //if (PowerController != null)
            //{
            //    return PowerController.IsConnect;
            //}
            return true;
        }

        public bool CheckPressureSensorControllerValid()
        {
            //if (PowerController != null)
            //{
            //    return PowerController.IsConnect;
            //}
            return true;
        }

    }
}
