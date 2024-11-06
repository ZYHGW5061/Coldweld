using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using LightControllerClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using WestDragon.Framework.SerialCommunicationClsLib;

namespace LightControllerManagerClsLib
{
    public class LightControllerManager
    {
        #region 获取单例
        private static readonly object _lockObj = new object();
        private static volatile LightControllerManager _instance = null;
        public static LightControllerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new LightControllerManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private LightControllerManager()
        {
            AllLights = new Dictionary<EnumLightSourceType, ILightSourceController>();
            UnionSerialPortEngines = new Dictionary<string, SerialPortController>();
            //Initialize();
        }
        #endregion

        #region 配置信息
        /// <summary>
        /// 获取配置文件处理器
        /// </summary>
        private HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }
        #endregion


        public Dictionary<EnumLightSourceType, ILightSourceController> AllLights { get; set; }
        public Dictionary<string, SerialPortController>  UnionSerialPortEngines { get; set; }

        private bool _isInitialized = false;
        private ILightSourceController CreateLightController(LEDConfig ledConfig)
        {
            ILightSourceController darkFieldController = null;
            if (ledConfig.RunningType == EnumRunningType.Actual)
            {
                switch (ledConfig.LightProducer)
                {
                    case EnumLightProducer.HIK:
                        darkFieldController = new HIKLightController(ledConfig);
                        break;
                    case EnumLightProducer.ZHIYUE:
                        darkFieldController = new ZHIYUELightController(ledConfig);
                        break;
                    case EnumLightProducer.OPT:
                        //darkFieldController = new OPTLightController(ledConfig);
                        break;
                    default:
                        darkFieldController = new SimulatedLightController();
                        break;
                }
            }
            else
            {
                darkFieldController = new SimulatedLightController();
            }
            return darkFieldController;
        }
        public ILightSourceController GetLightController(EnumLightSourceType lightPosition)
        {
            ILightSourceController darkFieldController = null;
            if(AllLights.ContainsKey(lightPosition))
            {
                darkFieldController = AllLights[lightPosition];
            }
            return darkFieldController;
        }
        
        public void Initialize(EnumLightSourceType lightPosition)
        {
            if (!AllLights.ContainsKey(lightPosition))
            {
                var df = CreateLightController(_hardwareConfig.RingLightControllerConfigList.Where(i => i.LightFieldPosition == lightPosition).FirstOrDefault());
                df.Connect();
                AllLights.Add(lightPosition, df);
            }
            else if (!AllLights[lightPosition].IsConnected)
            {
                AllLights[lightPosition].Connect();
            }
        }
        public void Initialize()
        {
            foreach (var item in _hardwareConfig.RingLightControllerConfigList)
            {
                if (!AllLights.ContainsKey(item.LightFieldPosition))
                {
                   
                    var df = CreateLightController(item);

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
                    AllLights.Add(item.LightFieldPosition, df);
                }
                else if (!AllLights[item.LightFieldPosition].IsConnected)
                {
                    AllLights[item.LightFieldPosition].Connect();
                }
            }
            foreach (var item in _hardwareConfig.DirectLightControllerConfigList)
            {
                if (!AllLights.ContainsKey(item.LightFieldPosition))
                {

                    var df = CreateLightController(item);

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
                    AllLights.Add(item.LightFieldPosition, df);
                }
                else if (!AllLights[item.LightFieldPosition].IsConnected)
                {
                    AllLights[item.LightFieldPosition].Connect();
                }
            }
        }

        public void Shutdown(EnumLightSourceType lightPosition)
        {
            if (AllLights.ContainsKey(lightPosition))
            {
                AllLights[lightPosition].SetIntensity(0);
                AllLights[lightPosition].Disconnect();
            }
        }
        public void Shutdown()
        {
            foreach (var item in AllLights)
            {
                item.Value.SetIntensity(0);

                //item.Value.Disconnect();
            }
            foreach (var item in AllLights)
            {
                //item.Value.SetIntensity(0);
                item.Value.Disconnect();
            }
        }
    }
}
