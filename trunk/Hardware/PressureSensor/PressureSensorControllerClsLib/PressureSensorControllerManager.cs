using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WestDragon.Framework.SerialCommunicationClsLib;

namespace PressureSensorControllerClsLib
{
    /// <summary>
    /// Stage管理类
    /// </summary>
    public class PressureSensorControllerManager
    {
        #region 获取单例
        private static readonly object _lockObj = new object();
        private static volatile PressureSensorControllerManager _instance = null;
        public static PressureSensorControllerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new PressureSensorControllerManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private PressureSensorControllerManager()
        {
            AllPressureSensors = new Dictionary<EnumPressureSensorType, IPressureSensorController>();
            UnionSerialPortEngines = new Dictionary<string, SerialPortController>();
            //Initialize();
        }
        #endregion

        #region 配置信息

        private HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }
        #endregion

        public Dictionary<EnumPressureSensorType, IPressureSensorController> AllPressureSensors { get; set; }
        public Dictionary<string, SerialPortController> UnionSerialPortEngines { get; set; }


        /// <summary>
        /// 当前硬件
        /// </summary>
        IPressureSensorController _currentController = null;

        public void Initialize()
        {
            foreach (var item in _hardwareConfig.PressureSensorControllerConfig)
            {
                if (!AllPressureSensors.ContainsKey(item.PressureSensorFieldPosition))
                {

                    var df = CreatePressureSensorController(item);

                    if (item.RunningType == EnumRunningType.Actual)
                    {
                        if (UnionSerialPortEngines.ContainsKey(item.CommunicatorID))
                        {
                            df.SerialPortEngine = UnionSerialPortEngines[item.CommunicatorID];
                            df.Connect();
                        }

                        if (!UnionSerialPortEngines.ContainsKey(item.CommunicatorID))
                        {
                            df.Connect();
                            UnionSerialPortEngines.Add(item.CommunicatorID, df.SerialPortEngine);
                        }
                    }
                    else
                    {
                        df.Connect();
                    }
                    AllPressureSensors.Add(item.PressureSensorFieldPosition, df);
                }
                else if (!AllPressureSensors[item.PressureSensorFieldPosition].IsConnect)
                {
                    AllPressureSensors[item.PressureSensorFieldPosition].Connect();
                }
            }
            
        }

        public void Shutdown(EnumPressureSensorType PressureSensorPosition)
        {
            if (AllPressureSensors.ContainsKey(PressureSensorPosition))
            {
                AllPressureSensors[PressureSensorPosition].Disconnect();
            }
        }
        public void Shutdown()
        {
            foreach (var item in AllPressureSensors)
            {
                //item.Value.SetIntensity(0);
                item.Value.Disconnect();
            }
        }

        /// <summary>
        /// 获取当前硬件
        /// </summary>
        /// <returns></returns>
        public IPressureSensorController GetCurrentHardware()
        {
            if (_currentController == null)
            {
                throw new NotSupportedException("Stage controller is not initialized.");
            }
            return _currentController;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private IPressureSensorController CreatePressureSensorController(PressureSensorControllerConfig ledConfig)
        {
            IPressureSensorController darkFieldController = null;
            if (ledConfig.RunningType == EnumRunningType.Actual)
            {
                switch (ledConfig.PressureSensorProducer)
                {
                    case EnumPressureSensorProducer.PressureSensor:
                        darkFieldController = new PressureSensor(ledConfig);
                        break;
                    default:
                        darkFieldController = new SimulatePressureSensorController();
                        break;
                }
            }
            else
            {
                darkFieldController = new SimulatePressureSensorController();
            }
            return darkFieldController;
        }


        public IPressureSensorController GetPressureSensorController(EnumPressureSensorType PressureSensorPosition)
        {
            IPressureSensorController darkFieldController = null;
            if (AllPressureSensors.ContainsKey(PressureSensorPosition))
            {
                darkFieldController = AllPressureSensors[PressureSensorPosition];
            }
            return darkFieldController;
        }
    }
}
