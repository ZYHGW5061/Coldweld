using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using StageControllerClsLib;


namespace StageManagerClsLib
{
    /// <summary>
    /// Stage管理类
    /// </summary>
    public class StageManager
    {
        #region 获取单例
        private static readonly object _lockObj = new object();
        private static volatile StageManager _instance = null;
        public static StageManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new StageManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private StageManager()
        {
        }
        #endregion

        /// <summary>
        /// 当前硬件
        /// </summary>
        IStageController _currentStageController = null;
        private HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }
        /// <summary>
        /// 初始化,创建Stage控制对象及硬件连接
        /// </summary>
        public void Initialize()
        {
            var runningType = _hardwareConfig.StageConfig.RunningType;
            if (runningType == EnumRunningType.Actual)
            {

                TPGJStageInfo stageInfo = new TPGJStageInfo();
                stageInfo.AxisControllerDic = new Dictionary<EnumStageAxis, ISingleAxisController>();
                stageInfo.AxisControllerDic.Add(EnumStageAxis.MaterialboxX, new MaterialboxXSingleAxisController());
                stageInfo.AxisControllerDic.Add(EnumStageAxis.MaterialboxY, new MaterialboxYSingleAxisController());
                stageInfo.AxisControllerDic.Add(EnumStageAxis.MaterialboxZ, new MaterialboxZSingleAxisController());
                stageInfo.AxisControllerDic.Add(EnumStageAxis.MaterialboxT, new MaterialboxTSingleAxisController());
                stageInfo.AxisControllerDic.Add(EnumStageAxis.MaterialboxHook, new MaterialboxHookSingleAxisController());
                stageInfo.AxisControllerDic.Add(EnumStageAxis.MaterialX, new MaterialXSingleAxisController());
                stageInfo.AxisControllerDic.Add(EnumStageAxis.MaterialY, new MaterialYSingleAxisController());
                stageInfo.AxisControllerDic.Add(EnumStageAxis.MaterialZ, new MaterialZSingleAxisController());
                stageInfo.AxisControllerDic.Add(EnumStageAxis.MaterialHook, new MaterialHookSingleAxisController());
                stageInfo.AxisControllerDic.Add(EnumStageAxis.OverTrack1, new OverTrack1SingleAxisController());
                stageInfo.AxisControllerDic.Add(EnumStageAxis.OverTrack2, new OverTrack2SingleAxisController());
                stageInfo.AxisControllerDic.Add(EnumStageAxis.Presslifting, new PressliftingSingleAxisController());
                //添加其他轴

                StageCore.Instance.StageInfo = stageInfo;
                _currentStageController = new TPGJStageController();
                //(_currentStageController as HCFAStageController).StageInfo = stageInfo;
            }
            else
            {
                _currentStageController = new SimulateStageController();
            }

            if (!_currentStageController.IsConnect)
                _currentStageController.Connect();
            _currentStageController.CheckHomeDone();
            _currentStageController.InitialzeAllAxisParameter();
            _currentStageController.InitialzeAllIO();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Shutdown()
        {

            if (_currentStageController != null)
            {
                _currentStageController.Stop();
                _currentStageController.Disconnect();
                _currentStageController = null;
            }
        }

        public void ReconnectAxis()
        {
            if (!_currentStageController.IsConnect)
                _currentStageController.Connect();
            _currentStageController.CheckHomeDone();
            _currentStageController.InitialzeAllAxisParameter();
        }

        public void ReconnectIO()
        {
            if (!_currentStageController.IsConnect)
                _currentStageController.Connect();
            _currentStageController.InitialzeAllIO();
        }

        /// <summary>
        /// 获取当前控制器
        /// </summary>
        /// <returns></returns>
        public IStageController GetCurrentController()
        {
            if (_currentStageController == null)
            {
            }
            return _currentStageController;
        }
    }
}
