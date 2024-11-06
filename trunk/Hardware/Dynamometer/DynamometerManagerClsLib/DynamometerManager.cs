using ConfigurationClsLib;
using DynamometerControllerClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamometerManagerClsLib
{
    public class DynamometerManager
    {
        #region 获取单例
        private static readonly object _lockObj = new object();
        private static volatile DynamometerManager _instance = null;
        public static DynamometerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new DynamometerManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private DynamometerManager()
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
        IDynamometerController _currentController = null;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            var runningType = _hardwareConfig.DynamometerControllerConfig.RunningType;
            if (runningType == EnumRunningType.Actual)
            {
                _currentController = new JNSENSORDynamometer(_hardwareConfig.DynamometerControllerConfig);
            }
            else
            {
                _currentController = new SimulatedDynamometer();
            }

            if (!_currentController.IsConnect)
                _currentController.Connect();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize(SerialPort serial)
        {
            var runningType = _hardwareConfig.DynamometerControllerConfig.RunningType;
            if (runningType == EnumRunningType.Actual)
            {
                _currentController = new JNSENSORDynamometer(_hardwareConfig.DynamometerControllerConfig);
            }
            else
            {
                _currentController = new SimulatedDynamometer();
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
        public IDynamometerController GetCurrentHardware()
        {
            if (_currentController == null)
            {
                throw new NotSupportedException("Stage controller is not initialized.");
            }
            return _currentController;
        }

    }
}
