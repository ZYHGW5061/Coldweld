using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModelConfigClsLib.Entity
{
    //共晶模块配置类
    [XmlRoot("EutecticConfig")]
    public class EutecticConfig
    {
        //模块名称
        [XmlElement("ConfigName")]
        public string ConfigName { get; set; }

        //底温
        [XmlElement("BaseTemp")]
        public float BaseTemp { get; set; }

        //升温温度
        [XmlElement("RiseTemp")]
        public float RiseTemp { get; set; }

        //升温时长 s
        [XmlElement("RisingTime")]
        public int RisingTime { get; set; }

        //保温时长 s
        [XmlElement("HoldingTime")]
        public int HoldingTime { get; set; }

        //冷却时长 s
        [XmlElement("CoolDownTime")]
        public int CoolDownTime { get; set; }
    }
}
