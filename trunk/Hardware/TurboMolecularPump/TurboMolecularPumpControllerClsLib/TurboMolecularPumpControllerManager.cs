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

namespace TurboMolecularPumpControllerClsLib
{
    /// <summary>
    /// Stage管理类
    /// </summary>
    public class TurboMolecularPumpControllerManager
    {
        #region 获取单例
        private static readonly object _lockObj = new object();
        private static volatile TurboMolecularPumpControllerManager _instance = null;
        public static TurboMolecularPumpControllerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new TurboMolecularPumpControllerManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private TurboMolecularPumpControllerManager()
        {
            AllTurboMolecularPumps = new Dictionary<EnumTurboMolecularPumpType, ITurboMolecularPumpController>();
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

        public Dictionary<EnumTurboMolecularPumpType, ITurboMolecularPumpController> AllTurboMolecularPumps { get; set; }
        public Dictionary<string, SerialPort> UnionSerialPortEngines { get; set; }


        /// <summary>
        /// 当前硬件
        /// </summary>
        ITurboMolecularPumpController _currentController = null;

        public void Initialize()
        {
            foreach (var item in _hardwareConfig.TurboMolecularPumpControllerConfig)
            {
                if (!AllTurboMolecularPumps.ContainsKey(item.TurboMolecularPumpFieldPosition))
                {

                    var df = CreateTurboMolecularPumpController(item);

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
                    AllTurboMolecularPumps.Add(item.TurboMolecularPumpFieldPosition, df);
                }
                else if (!AllTurboMolecularPumps[item.TurboMolecularPumpFieldPosition].IsConnect)
                {
                    AllTurboMolecularPumps[item.TurboMolecularPumpFieldPosition].Connect();
                }
            }
            
        }

        public void Shutdown(EnumTurboMolecularPumpType TurboMolecularPumpPosition)
        {
            if (AllTurboMolecularPumps.ContainsKey(TurboMolecularPumpPosition))
            {
                AllTurboMolecularPumps[TurboMolecularPumpPosition].Disconnect();
            }
        }
        public void Shutdown()
        {
            foreach (var item in AllTurboMolecularPumps)
            {
                //item.Value.SetIntensity(0);
                item.Value.Disconnect();
            }
        }

        /// <summary>
        /// 获取当前硬件
        /// </summary>
        /// <returns></returns>
        public ITurboMolecularPumpController GetCurrentHardware()
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
        private ITurboMolecularPumpController CreateTurboMolecularPumpController(TurboMolecularPumpControllerConfig ledConfig)
        {
            ITurboMolecularPumpController darkFieldController = null;
            if (ledConfig.RunningType == EnumRunningType.Actual)
            {
                switch (ledConfig.TurboMolecularPumpProducer)
                {
                    case EnumTurboMolecularPumpProducer.TurboMolecularPump:
                        darkFieldController = new TurboMolecularPump(ledConfig);
                        break;
                    default:
                        darkFieldController = new SimulateTurboMolecularPumpController();
                        break;
                }
            }
            else
            {
                darkFieldController = new SimulateTurboMolecularPumpController();
            }
            return darkFieldController;
        }


        public ITurboMolecularPumpController GetTurboMolecularPumpController(EnumTurboMolecularPumpType TurboMolecularPumpPosition)
        {
            ITurboMolecularPumpController darkFieldController = null;
            if (AllTurboMolecularPumps.ContainsKey(TurboMolecularPumpPosition))
            {
                darkFieldController = AllTurboMolecularPumps[TurboMolecularPumpPosition];
            }
            return darkFieldController;
        }
    }
}
