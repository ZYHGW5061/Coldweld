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

namespace VacuumGaugeControllerClsLib
{
    /// <summary>
    /// Stage管理类
    /// </summary>
    public class VacuumGaugeControllerManager
    {
        #region 获取单例
        private static readonly object _lockObj = new object();
        private static volatile VacuumGaugeControllerManager _instance = null;
        public static VacuumGaugeControllerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new VacuumGaugeControllerManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private VacuumGaugeControllerManager()
        {
            AllVacuumGauges = new Dictionary<EnumVacuumGaugeType, IVacuumGaugeController>();
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

        public Dictionary<EnumVacuumGaugeType, IVacuumGaugeController> AllVacuumGauges { get; set; }
        public Dictionary<string, SerialPort> UnionSerialPortEngines { get; set; }


        /// <summary>
        /// 当前硬件
        /// </summary>
        IVacuumGaugeController _currentController = null;

        public void Initialize()
        {
            foreach (var item in _hardwareConfig.VacuumGaugeControllerConfig)
            {
                if (!AllVacuumGauges.ContainsKey(item.VacuumGaugeFieldPosition))
                {

                    var df = CreateVacuumGaugeController(item);

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
                    AllVacuumGauges.Add(item.VacuumGaugeFieldPosition, df);
                }
                else if (!AllVacuumGauges[item.VacuumGaugeFieldPosition].IsConnect)
                {
                    AllVacuumGauges[item.VacuumGaugeFieldPosition].Connect();
                }
            }
            
        }

        public void Shutdown(EnumVacuumGaugeType VacuumGaugePosition)
        {
            if (AllVacuumGauges.ContainsKey(VacuumGaugePosition))
            {
                AllVacuumGauges[VacuumGaugePosition].Disconnect();
            }
        }
        public void Shutdown()
        {
            foreach (var item in AllVacuumGauges)
            {
                //item.Value.SetIntensity(0);
                item.Value.Disconnect();
            }
        }

        /// <summary>
        /// 获取当前硬件
        /// </summary>
        /// <returns></returns>
        public IVacuumGaugeController GetCurrentHardware()
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
        private IVacuumGaugeController CreateVacuumGaugeController(VacuumGaugeControllerConfig ledConfig)
        {
            IVacuumGaugeController darkFieldController = null;
            if (ledConfig.RunningType == EnumRunningType.Actual)
            {
                switch (ledConfig.VacuumGaugeProducer)
                {
                    case EnumVacuumGaugeProducer.VacuumGauge:
                        darkFieldController = new VacuumGauge(ledConfig);
                        break;
                    default:
                        darkFieldController = new SimulateVacuumGaugeController();
                        break;
                }
            }
            else
            {
                darkFieldController = new SimulateVacuumGaugeController();
            }
            return darkFieldController;
        }


        public IVacuumGaugeController GetVacuumGaugeController(EnumVacuumGaugeType VacuumGaugePosition)
        {
            IVacuumGaugeController darkFieldController = null;
            if (AllVacuumGauges.ContainsKey(VacuumGaugePosition))
            {
                darkFieldController = AllVacuumGauges[VacuumGaugePosition];
            }
            return darkFieldController;
        }
    }
}
