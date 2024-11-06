using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModelConfigClsLib.Entity
{
    //物料配置类
    [XmlRoot("ComponentConfig")]
    public class ComponentConfig
    {
        //模块名称
        [XmlElement("ConfigName")]
        public string ConfigName { get; set; }

        //物料类型
        [XmlElement("ComponentType")]
        public EnumComponentType ComponentType { get; set; }

        //行数
        [XmlElement("RowsNum")]
        public int RowsNum { get; set; }

        //列数
        [XmlElement("ColsNum")]
        public int ColsNum { get; set; }

        //物料高度 mm
        [XmlElement("Height")]
        public float Height { get; set; }

        //物料行间距 mm
        [XmlElement("LineSpace")]
        public float LineSpace { get; set; }

        //物料列间距 mm
        [XmlElement("ColSpace")]
        public float ColSpace { get; set; }

        //物料长度
        [XmlElement("Length")]
        public float Length { get; set; }

        //物料宽度
        [XmlElement("Width")]
        public float Width { get; set; }

        //物料容器类型
        [XmlElement("ContainerType")]
        public EnumContainerType ContainerType { get; set; }

        //物料吸取顺序
        [XmlElement("PickupOrder")]
        public EnumPickupOrder PickupOrder { get; set; }

        //物料特征图片file
        [XmlElement("FeatureImg")]
        public string FeatureImg { get; set; }

        //顶针上升距离
        [XmlElement("NeedleRisingDistance")]
        public float NeedleRisingDistance { get; set; }

        //蓝膜厚度
        [XmlElement("WaferHeight")]
        public float WaferHeight { get; set; }

        //是否二次校准
        [XmlElement("IsSecondCalibration")]
        public Boolean IsSecondCalibration { get; set; }

        //二次校准形式
        [XmlElement("SecondCalibrationType")]
        public EnumSecondCalibrationType SecondCalibrationType { get; set; }

        //二次校准模板
        [XmlElement("SecondCalibrationModel")]
        public EnumSecondCalibrationModel SecondCalibrationModel { get; set; }

        //结束形式 
        [XmlElement("FinishAction")]
        public EnumFinishAction FinishAction { get; set; }
    }

    public enum EnumComponentType
    {
        [Display(Name = "基底")]
        Substrate,

        [Display(Name = "芯片")]
        Chip
    }

    public enum EnumContainerType
    {
        [Display(Name = "华夫盒")]
        GelPack,
        [Display(Name = "晶圆华夫盒")]
        waferGelPack, 
        [Display(Name = "蓝膜")]
        Wafer
    }

    public enum EnumPickupOrder
    {
        [Display(Name = "左起蛇形")]
        LeftSnakeShape,
        [Display(Name = "右起蛇形")]
        RightSnakeShape
    }

    public enum EnumSecondCalibrationType
    {
        [Display(Name = "仰视相机校准")]
        LookupCamera,
        [Display(Name = "校准平台")]
        CalibrationTable
    }

    public enum EnumSecondCalibrationModel
    {
        [Display(Name = "图案")]
        Pattern,
        [Display(Name = "边框")]
        Frame
    }

    public enum EnumFinishAction
    {
        [Display(Name = "抛料")]
        ThrowOut,
        [Display(Name = "放回")]
        PutBack
    }

    public enum BondHeadType
    {
        [Display(Name = "芯片Bond头")]
        pp1,
        [Display(Name = "基底Bond头")]
        pp2
    }
}
