using IJoyStickControllerClsLib;
using JoyStickControllerClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoyStickManagerClsLib
{
    public class JoyStickManager
    {
        #region 获取单例
        private static readonly object _lockObj = new object();
        private static volatile JoyStickManager _instance = null;
        public static JoyStickManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new JoyStickManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private JoyStickManager()
        {
            //Initialize();
        }
        #endregion
        /// <summary>
        /// 硬件配置
        /// </summary>
        //protected HardwareConfiguration _hardwareConfig
        //{
        //    get { return HardwareConfiguration.Instance; }
        //}
        IJobStickController _jobStickController;
        public void Initialize()
        {
            //if(_hardwareConfig.JoyStickControllerConfig.RunningType== EnumRunningType.Actual)
            //{
            //    _jobStickController = new JoyStickController(_hardwareConfig.JoyStickControllerConfig.Name);
            //    _jobStickController.Connect();
            //}
            //else
            //{
            //    _jobStickController = new SimulatedJoyStickController();
            //}

            _jobStickController = new JoyStickController("Controller (BEITONG A1T XINPUT GAMEPAD)");
            _jobStickController.Connect();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Shutdown()
        {

        }
        public IJobStickController GetCurrentController()
        {
            if (_jobStickController == null)
            {
                //throw new NotSupportedException("JoyStick controller is not initialized.");
            }
            return _jobStickController;
        }
    }
}
