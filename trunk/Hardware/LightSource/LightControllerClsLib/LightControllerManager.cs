using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightControllerClsLib
{
    public class LightControllerManager
    {
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
            AllFields = new Dictionary<EnumLightSourceType, LightController>();
            //Initialize();
        }

        public Dictionary<EnumLightSourceType, LightController> AllFields { get; set; }
        #region 配置信息
        /// <summary>
        /// 获取配置文件处理器
        /// </summary>
        private HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }
        #endregion

        public void Initialize()
        {
            foreach (var item in _hardwareConfig.BrightFieldControllerConfigList)
            {
                if (!AllFields.ContainsKey(item.LightFieldPosition))
                {
                    var df = CreateBrightFieldController(item);
                    //df.Connect();
                    AllFields.Add(item.LightFieldPosition, df);
                }
                //else if (!AllFields[item.LightFieldPosition].IsConnected)
                //{
                //    AllFields[item.LightFieldPosition].Connect();
                //}
            }
            //var df = CreateBrightFieldController(_hardwareConfig.BrightFieldControllerConfigList);
            //if (!IsConnected())
            //{
            //    LightConnect();
            //}
        }

        private LightController CreateBrightFieldController(LEDConfig ledConfig)
        {
            //LightController fieldController = null;
            //if (ledConfig.RunningType == EnumRunningType.Actual)
            //{
            //    switch (ledConfig.LightProducer)
            //    {
            //        case EnumLightProducer.HIK:
            //            fieldController = new LightController(ledConfig);
            //            break;
            //        //case EnumLightProducer.OPT:
            //        //    fieldController = new OPTLightController(ledConfig);
            //        //    break;
            //        default:
            //            fieldController = new SimulatedLightController();
            //            break;
            //    }
            //}
            //else
            //{
            //    fieldController = new SimulatedLightController();
            //}
            //return fieldController;
            return null;
        }
        public LightController GetBrightFieldController(EnumLightSourceType lightPosition)
        {
            LightController darkFieldController = null;
            if (AllFields.ContainsKey(lightPosition))
            {
                darkFieldController = AllFields[lightPosition];
            }
            return darkFieldController;
        }

    }
}
