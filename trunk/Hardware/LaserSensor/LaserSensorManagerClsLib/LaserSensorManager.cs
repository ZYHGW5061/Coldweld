using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using LaserSensorControllerClsLib;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserSensorManagerClsLib
{
    public class LaserSensorManager
    {
        #region 获取单例
        private static readonly object _lockObj = new object();
        private static volatile LaserSensorManager _instance = null;
        public static LaserSensorManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new LaserSensorManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private LaserSensorManager()
        {
            //Initialize();
        }
        #endregion

        #region 配置信息

        private HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }
        #endregion

        /// <summary>
        /// 当前硬件
        /// </summary>
        ILaserSensorController _currentController = null;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            var runningType = _hardwareConfig.LaserSensorControllerConfig.RunningType;
            if (runningType == EnumRunningType.Actual)
            {
                _currentController = new BOJKESensor(_hardwareConfig.LaserSensorControllerConfig);
            }
            else
            {
                _currentController = new SimulatedLaserSensorController();
            }

            if (!_currentController.IsConnect)
                _currentController.Connect();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize(SerialPort serial)
        {
            var runningType = _hardwareConfig.LaserSensorControllerConfig.RunningType;
            if (runningType == EnumRunningType.Actual)
            {
                _currentController = new BOJKESensor(_hardwareConfig.LaserSensorControllerConfig);
            }
            else
            {
                _currentController = new SimulatedLaserSensorController();
            }

            if (!_currentController.IsConnect)
                _currentController.Connect(serial);
        }


        /// <summary>
        /// 关闭
        /// </summary>
        public void Shutdown()
        {
            //if (_currentStageController != null && _currentStageController.IsConnected)
            //{
            //    _currentStageController.Stop();
            //    _currentStageController.Disconnect();
            //    _currentStageController = null;
            //}
            if (_currentController != null)
            {
                _currentController.Disconnect();
                _currentController = null;
            }
        }

        /// <summary>
        /// 获取当前硬件
        /// </summary>
        /// <returns></returns>
        public ILaserSensorController GetCurrentHardware()
        {
            if (_currentController == null)
            {
                throw new NotSupportedException("Stage controller is not initialized.");
            }
            return _currentController;
        }


    }
}
