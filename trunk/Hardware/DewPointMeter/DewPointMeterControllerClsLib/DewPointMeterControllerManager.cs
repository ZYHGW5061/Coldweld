using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DewPointMeterControllerClsLib
{
    /// <summary>
    /// Stage管理类
    /// </summary>
    public class DewPointMeterControllerManager
    {
        #region 获取单例
        private static readonly object _lockObj = new object();
        private static volatile DewPointMeterControllerManager _instance = null;
        public static DewPointMeterControllerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new DewPointMeterControllerManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private DewPointMeterControllerManager()
        {
            AllDewPointMeters = new Dictionary<EnumDewPointMeterType, IDewPointMeterController>();
            UnionSerialPortEngines = new Dictionary<string, SerialPort>();
            //Initialize();
        }
        #endregion

        #region 配置信息

        private HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }
        #endregion

        public Dictionary<EnumDewPointMeterType, IDewPointMeterController> AllDewPointMeters { get; set; }
        public Dictionary<string, SerialPort> UnionSerialPortEngines { get; set; }


        /// <summary>
        /// 当前硬件
        /// </summary>
        IDewPointMeterController _currentController = null;

        public void Initialize()
        {
            foreach (var item in _hardwareConfig.DewPointMeterControllerConfig)
            {
                if (!AllDewPointMeters.ContainsKey(item.DewPointMeterFieldPosition))
                {

                    var df = CreateDewPointMeterController(item);

                    if (item.RunningType == EnumRunningType.Actual)
                    {
                        if (UnionSerialPortEngines.ContainsKey(item.CommunicatorID))
                        {
                            df.SerialPortEngine = UnionSerialPortEngines[item.CommunicatorID];
                            //df.Connect();
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
                    AllDewPointMeters.Add(item.DewPointMeterFieldPosition, df);
                }
                else if (!AllDewPointMeters[item.DewPointMeterFieldPosition].IsConnect)
                {
                    AllDewPointMeters[item.DewPointMeterFieldPosition].Connect();
                }
            }
            
        }

        public void Shutdown(EnumDewPointMeterType DewPointMeterPosition)
        {
            if (AllDewPointMeters.ContainsKey(DewPointMeterPosition))
            {
                AllDewPointMeters[DewPointMeterPosition].Disconnect();
            }
        }
        public void Shutdown()
        {
            foreach (var item in AllDewPointMeters)
            {
                //item.Value.SetIntensity(0);
                item.Value.Disconnect();
            }
        }

        /// <summary>
        /// 获取当前硬件
        /// </summary>
        /// <returns></returns>
        public IDewPointMeterController GetCurrentHardware()
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
        private IDewPointMeterController CreateDewPointMeterController(DewPointMeterControllerConfig ledConfig)
        {
            IDewPointMeterController darkFieldController = null;
            if (ledConfig.RunningType == EnumRunningType.Actual)
            {
                switch (ledConfig.DewPointMeterProducer)
                {
                    case EnumDewPointMeterProducer.DewPointMeter:
                        darkFieldController = new DewPointMeter(ledConfig);
                        break;
                    default:
                        darkFieldController = new SimulateDewPointMeterController();
                        break;
                }
            }
            else
            {
                darkFieldController = new SimulateDewPointMeterController();
            }
            return darkFieldController;
        }


        public IDewPointMeterController GetDewPointMeterController(EnumDewPointMeterType DewPointMeterPosition)
        {
            IDewPointMeterController darkFieldController = null;
            if (AllDewPointMeters.ContainsKey(DewPointMeterPosition))
            {
                darkFieldController = AllDewPointMeters[DewPointMeterPosition];
            }
            return darkFieldController;
        }
    }
}
