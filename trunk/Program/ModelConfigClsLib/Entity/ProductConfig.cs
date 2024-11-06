using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModelConfigClsLib.Entity
{
    //生产类配置文件
    [XmlRoot("ProductConfig")]
    public class ProductConfig
    {
        //模块名称
        [XmlElement("ProductName")]
        public string ProductName { get; set; }

        //步骤列表
        [XmlArray("ProductSteps")]
        [XmlArrayItem("Step")]
        public List<ProductStep> ProductSteps { get; set; }

        //下料方式类型
        [XmlElement("BlankType")]
        public EnumBlankType BlankType { get;set; }
    }

    //生产步骤
    public class ProductStep
    {
        [XmlElement("StepName")]
        public string StepName { get; set; }

        [XmlElement("ProductStepType")]
        public EnumProductStepType productStepType { get; set; }

        [XmlElement("ComponentName")]
        public string ComponentName { get; set; }

        [XmlElement("BondingPositionName")]
        public string BondingPositionName { get; set; }

        [XmlElement("EutecticName")]
        public string EutecticName { get; set; }

        /*
        //传输源区域
        [XmlElement("EnumTransSrc")]
        public EnumTransSrc TransSrc { get; set; }

        //传输目标区域
        [XmlElement("EnumTransDest")]
        public EnumTransDest TransDest { get; set; }
        */
    }

    public enum EnumBlankType
    {
        [Description("放回基底盘")]
        SubstratePos,

        [Description("回料区")]
        FinishPos
    }



}
