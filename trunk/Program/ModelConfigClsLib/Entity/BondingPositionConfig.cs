using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModelConfigClsLib.Entity
{
    //贴片位置配置类
    [XmlRoot("BondingPositionConfig")]
    public class BondingPositionConfig
    {
        //模块名称
        [XmlElement("ConfigName")]
        public string ConfigName { get; set; }

        //基底模块名称 - 对应某个ComponentConfig的名称
        [XmlElement("SubstrateName")]
        public string SubstrateName { get; set; }

        //贴片位置X轴偏移量
        public float OffsetX { get; set; }

        //贴片位置Y轴偏移量
        public float OffsetY { get; set; }

        //贴片位置角度偏移量
        public float OffsetAngle { get; set; }
    }
}
