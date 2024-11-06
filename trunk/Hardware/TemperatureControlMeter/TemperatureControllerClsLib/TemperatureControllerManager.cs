using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WestDragon.Framework.SerialCommunicationClsLib;

namespace TemperatureControllerClsLib
{
    /// <summary>
    /// Stage管理类
    /// </summary>
    public class TemperatureControllerManager
    {
        #region 获取单例
        private static readonly object _lockObj = new object();
        private static volatile TemperatureControllerManager _instance = null;
        public static TemperatureControllerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new TemperatureControllerManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private TemperatureControllerManager()
        {
            AllTemperatures = new Dictionary<EnumTemperatureType, ITemperatureController>();
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

        public Dictionary<EnumTemperatureType, ITemperatureController> AllTemperatures { get; set; }
        public Dictionary<string, SerialPort> UnionSerialPortEngines { get; set; }


        /// <summary>
        /// 当前硬件
        /// </summary>
        ITemperatureController _currentController = null;

        public void Initialize()
        {
            foreach (var item in _hardwareConfig.TemperatureControllerConfig)
            {
                if (!AllTemperatures.ContainsKey(item.TemperatureFieldPosition))
                {

                    var df = CreateTemperatureController(item);

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
                    AllTemperatures.Add(item.TemperatureFieldPosition, df);
                }
                else if (!AllTemperatures[item.TemperatureFieldPosition].IsConnect)
                {
                    AllTemperatures[item.TemperatureFieldPosition].Connect();
                }
            }
            
        }

        public void Shutdown(EnumTemperatureType TemperaturePosition)
        {
            if (AllTemperatures.ContainsKey(TemperaturePosition))
            {
                AllTemperatures[TemperaturePosition].Disconnect();
            }
        }
        public void Shutdown()
        {
            foreach (var item in AllTemperatures)
            {
                //item.Value.SetIntensity(0);
                item.Value.Disconnect();
            }
        }

        /// <summary>
        /// 获取当前硬件
        /// </summary>
        /// <returns></returns>
        public ITemperatureController GetCurrentHardware()
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
        private ITemperatureController CreateTemperatureController(TemperatureControllerConfig ledConfig)
        {
            ITemperatureController darkFieldController = null;
            if (ledConfig.RunningType == EnumRunningType.Actual)
            {
                switch (ledConfig.TemperatureProducer)
                {
                    case EnumTemperatureProducer.SHIMADEN:
                        darkFieldController = new SHIMADENRTU(ledConfig);
                        break;
                    case EnumTemperatureProducer.OMRON:
                        //darkFieldController = new OPTLightController(ledConfig);
                        break;
                    default:
                        darkFieldController = new SimulateTemperatureController();
                        break;
                }
            }
            else
            {
                darkFieldController = new SimulateTemperatureController();
            }
            return darkFieldController;
        }


        public ITemperatureController GetTemperatureController(EnumTemperatureType TemperaturePosition)
        {
            ITemperatureController darkFieldController = null;
            if (AllTemperatures.ContainsKey(TemperaturePosition))
            {
                darkFieldController = AllTemperatures[TemperaturePosition];
            }
            return darkFieldController;
        }
    }
}
