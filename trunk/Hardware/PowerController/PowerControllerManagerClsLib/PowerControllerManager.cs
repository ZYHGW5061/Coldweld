using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PowerControllerManagerClsLib
{
    /// <summary>
    /// Stage管理类
    /// </summary>
    public class PowerControllerManager
    {
        #region 获取单例
        private static readonly object _lockObj = new object();
        private static volatile PowerControllerManager _instance = null;
        public static PowerControllerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new PowerControllerManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private PowerControllerManager()
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
        IPowerController _currentController = null;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            var runningType = _hardwareConfig.PowerControllerConfig.RunningType;
            if (runningType == EnumRunningType.Actual)
            {
                _currentController = new JingYuanPowerControl(_hardwareConfig.PowerControllerConfig);
            }
            else
            {
                _currentController = new SimulatedPowerController();
            }

            if (!_currentController.IsConnect)
                _currentController.Connect();
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
        public IPowerController GetCurrentHardware()
        {
            if (_currentController == null)
            {
                throw new NotSupportedException("Stage controller is not initialized.");
            }
            return _currentController;
        }
    }
}
