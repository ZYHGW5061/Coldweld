using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.IOManageClsLib;
using WestDragon.Framework.UtilityHelper;

namespace GlobalToolClsLib
{
    public class IOManager
    {
        private static readonly object _lockObj = new object();
        private static volatile IOManager _instance = null;
        public static IOManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new IOManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private IOManager()
        {
            //Initialize();
        }
        public void Initialize()
        {
            var fullFilePath = @"Config\DefaultIOs.xml";
            var ios = XmlSerializeHelper.XmlDeserializeFromFile<List<IOChannel>>(fullFilePath, Encoding.UTF8);
            foreach (var item in ios)
            {
                IOChannelManager.GetHandler().Add(new IOChannel(item.ChannelName, item.ChannelValue, item.Category, item.Description));
            }
        }
        public void ChangeIOValue(string ioName, object value)
        {
            IOChannelManager.GetHandler().ChangeIOValue(ioName, value);
        }
        public T GetIOValue<T>(string ioName)
        {
            return IOChannelManager.GetHandler().GetIOValue<T>(ioName);
        }
        public void RegisterIOChannelChangedEvent(string ioName, Action<string,object,object> action)
        {
            IOChannelManager.GetHandler().RegisterIOChannelEvt(ioName, action);
        }
        public void UnregisterIOChannelChangedEvent(string ioName, Action<string, object, object> action)
        {
            IOChannelManager.GetHandler().UnregisterIOChannelEvt(ioName, action);
        }

        public EnumStageAxis GetAxisByIOName(string ioName)
        {
            var ret = EnumStageAxis.None;
            switch (ioName)
            {
                case "Stage.MaterialboxXPosition":
                    ret = EnumStageAxis.MaterialboxX;
                    break;
                case "Stage.MaterialboxYPosition":
                    ret = EnumStageAxis.MaterialboxY;
                    break;
                case "Stage.MaterialboxZPosition":
                    ret = EnumStageAxis.MaterialboxZ;
                    break;
                case "Stage.MaterialboxTPosition":
                    ret = EnumStageAxis.MaterialboxT;
                    break;
                case "Stage.MaterialboxHookPosition":
                    ret = EnumStageAxis.MaterialboxHook;
                    break;
                case "Stage.MaterialXPosition":
                    ret = EnumStageAxis.MaterialX;
                    break;
                case "Stage.MaterialYPosition":
                    ret = EnumStageAxis.MaterialY;
                    break;
                case "Stage.MaterialZPosition":
                    ret = EnumStageAxis.MaterialZ;
                    break;
                case "Stage.MaterialHookPosition":
                    ret = EnumStageAxis.MaterialHook;
                    break;
                case "Stage.OverTrack1Position":
                    ret = EnumStageAxis.OverTrack1;
                    break;
                case "Stage.OverTrack2Position":
                    ret = EnumStageAxis.OverTrack2;
                    break;
                case "Stage.PressliftingPosition":
                    ret = EnumStageAxis.Presslifting;
                    break;
                default:
                    break;
            }
            return ret;
        }

    }
}
