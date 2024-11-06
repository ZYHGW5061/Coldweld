using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GlobalDataDefineClsLib
{
    public static class GlobalParameterSetting
    {
        public const string SYSTEM_DEBUG_LOGGER_ID = "SystemGlobalLogger";
        public const double GOLDEN_POINT = 0.618;
        public const string RIGHT_BRACE = "}";
        public const string LEFT_BRACE = "{";
        public const string RIGHT_SQUARE_BRACKET = "]";
        public const string RIGHT_ANGLE_BRACKET = ">";
        public const string LEFT_ANGLE_BRACKET = "<";
        public const string LEFT_SQUARE_BRACKET = "[";
        public const string LEFT_PARENTTHESIS = "(";
        public const string SYSTEM_MONITOR_LOGGER_ID = "SystemMonitorLogger";
        public const string HARDWARE_PLC_LOGGER_ID = "HardwarePLCLogger";
        public const string SYSTEM_PLC_LOGGER_ID = "SystemPLCLogger";
        public const string SYSTEM_OPERATION_DBLOGGER_ID = "UserOperationLogger";
        public const string RIGHT_PARENTTHESIS = ")";
        public static readonly string CONFIG_FILE_FULLNAME;
        public static readonly string SYSTEM_STARTUP_INFO_FILE_FULLNAME;
        public static readonly string WAFERJOB_LOG_FILE_DEFAULT_DIR;
        public static readonly string LOG_FILE_OPERATION_DIR;
        public static readonly string LOG_FILE_EXCEPTION_DIR;
        public static readonly string CONFIG_FILE_FULLNAME_MASTERCONTROL;
        public static readonly string CHANNEL_CONFIG_FILE_DEFAULT_DIR;
        public static readonly string BUSINESS_GOLDEN_TEMP_DIR;
        public static readonly string CONFIG_FILE_DEFAULT_DIR;
        public static readonly string JOB_REPORT_TEMPLATE_PATH_HISTORY;
        public static readonly string JOB_REPORT_TEMPLATE_PATH;
        public static readonly string JOB_REPORT_IMAGE_PATH;
        public static readonly string DEBUG_OUTPUT_DIR;
        public static readonly string SYSTEM_DEFAULT_DIR;
        public static readonly string CONFIG_FILE_DEFAULT_DIR_MASTERCONTROL;
        public static bool ENABLE_SAVE_DEFECT_RAWDATA;
        public static bool ENABLE_SAVE_RAWDATA;
        public static Bitmap MAP_NAVIGATION;


        //public static readonly Dictionary<EnumWaferMissionState, Color> WaferMissionStateColorDic = new Dictionary<EnumWaferMissionState, Color>()
        //{
        //    {EnumWaferMissionState.Unknow,Color.SandyBrown},
        //    {EnumWaferMissionState.NoWafer,Color.LightGray},
        //    {EnumWaferMissionState.ErrorWafer, Color.Red},
        //    {EnumWaferMissionState.Unselect, Color.White},
        //    {EnumWaferMissionState.Selected, Color.LawnGreen},
        //    {EnumWaferMissionState.PreAlign, Color.SkyBlue},
        //    {EnumWaferMissionState.Aligning, Color.Cyan},
        //    {EnumWaferMissionState.Aligned, Color.Cyan},
        //    {EnumWaferMissionState.Measuring, Color.Yellow},
        //    {EnumWaferMissionState.Measured, Color.DarkOrange},
        //    {EnumWaferMissionState.CompletePass, Color.Green},
        //    {EnumWaferMissionState.CompleteNotPass, Color.Brown}
        //};
    }
    /// <summary>
    /// Lid的属性，检测Lid，边缘Lid，非检测Lid(Shell同样适用)
    /// </summary>
    [Serializable]
    public enum MaterialProperties { Testable, Edge, NonTestable }
    /// <summary>
    /// Lid的属性，检测Lid，边缘Lid，非检测Lid(Shell同样适用)
    /// </summary>
    [Serializable]
    public enum MaterialProcessResult { None = 0, NotProcess = 1,RecognisePass=2, LSSuccess = 4,SSSuccess=5,PointWeldFinished=3, Fail=8, Abandoned = 7 }
    /// <summary>
    /// 自动聚焦的方法
    /// </summary>
    [Serializable]
    public enum EnumAutoFocusMethod { Normal, Fast, CurveFit, Maximum }
    /// <summary>
    /// 自动聚焦的方法
    /// </summary>
    [Serializable]
    public enum EnumImageSharpnessMode { MeanStdDev, Laplance, Sobel }
    /// <summary>
    /// 物料参数
    /// </summary>

    [Serializable]
    /// <summary>
    /// 物料类型
    /// </summary>
    public enum EnumMaterialType
    {
        [Description("基底")]
        Substrate,

        [Description("芯片")]
        Chip
    }

    [Serializable]
    public class MaterialSettings
    {
        public MaterialSettings()
        {
            MarkPoint = new XYZTCoordinateConfig();
            PositionComponentVisionParameters = new VisionParameters();
            AccuracyComponentPositionVisionParameters = new VisionParameters();

            PositionSubmountVisionParameters = new VisionParameters();

            AccuracySubmountPositionVisionParameters = new VisionParameters();

            //PositionBeforeBondVisionParameters = new VisionParameters();
            PositionSubmountBeforeBlankingVisionParameters = new VisionParameters();

            FirstComponentLocation = new XYZTCoordinateConfig();
            FirstSubmountLocation = new XYZTCoordinateConfig();
            ComponentMapInfos = new List<MaterialMapInformation>();
            SubmountMapInfos = new List<MaterialMapInformation>();

            PPSettings = new PPWorkParameters();
            PPSettingsForBlanking = new PPWorkParameters();

            WaferEdgePos1 = new XYZTCoordinateConfig();
            WaferEdgePos2 = new XYZTCoordinateConfig();
            WaferEdgePos3 = new XYZTCoordinateConfig();

        }
        #region 
        [XmlElement("IsMaterialInfoSettingsComplete")]
        public bool IsMaterialInfoSettingsComplete { get; set; }
        [XmlElement("IsMaterialPositionSettingsComplete")]
        public bool IsMaterialPositionSettingsComplete { get; set; }
        [XmlElement("IsMaterialPPSettingsComplete")]
        public bool IsMaterialPPSettingsComplete { get; set; }
        [XmlElement("IsMaterialMapSettingsComplete")]
        public bool IsMaterialMapSettingsComplete { get; set; }
        [XmlElement("IsMaterialAccuracySettingsComplete")]
        public bool IsMaterialAccuracySettingsComplete { get; set; }
        [XmlElement("MarkPoint")]
        public XYZTCoordinateConfig MarkPoint { get; set; }
        //按系统坐标系记录
        [XmlElement("FirstComponentLocation")]
        //按系统坐标系记录
        public XYZTCoordinateConfig FirstComponentLocation { get; set; }
        [XmlElement("FirstSubmountLocation")]
        public XYZTCoordinateConfig FirstSubmountLocation { get; set; }
        
        [XmlElement("Name")]
        //物料名称
        public string Name { get; set; }
        [XmlElement("MaterialType")]
        public EnumMaterialType MaterialType { get; set; }

        [XmlElement("WidthMM")]
        //物料横宽
        public float WidthMM { get; set; }

        [XmlElement("HeightMM")]
        //物料竖高
        public float HeightMM { get; set; }

        [XmlElement("PitchColumnMM")]
        //列间距
        public float PitchColumnMM { get; set; }

        [XmlElement("PitchRowMM")]
        //行间距
        public float PitchRowMM { get; set; }

        [XmlElement("ThicknessMM")]
        //物料厚度
        public float ThicknessMM { get; set; }
        [XmlElement("RowCount")]
        public int RowCount { get; set; }
        [XmlElement("ColumnCount")]
        public int ColumnCount { get; set; }
        [XmlElement("OrigionAngle")]
        public float OrigionAngle { get; set; }

        [XmlElement("CarrierType")]
        //容器类型
        public EnumCarrierType CarrierType { get; set; }

        [XmlElement("CarrierThicknessMM")]
        //容器厚度
        public float CarrierThicknessMM { get; set; }

        [XmlElement("RelatedESToolName")]
        public string RelatedESToolName { get; set; }
        [XmlElement("RelatedPPToolName")]
        public string RelatedPPToolName { get; set; }
        /// <summary>
        /// Chip吸嘴吸取时bondz的高度
        /// </summary>
        [XmlElement("ChipPPPickPos")]
        public float ChipPPPickPos { get; set; }

        /// <summary>
        /// Chip吸嘴放置时bondz的高度
        /// </summary>
        [XmlElement("ChipPPPlacePos")]
        public float ChipPPPlacePos { get; set; }
        [XmlElement("PPSettings")]
        public PPWorkParameters PPSettings { get; set; }
        [XmlElement("PPSettingsForBlanking")]
        public PPWorkParameters PPSettingsForBlanking { get; set; }
        /// <summary>
        /// Submount吸嘴吸取时bondz的高度
        /// </summary>
        [XmlElement("SubmountPPPickPos")]
        public float SubmountPPPickPos { get; set; }
        /// <summary>
        /// Submount吸嘴放置时bondz的高度
        /// </summary>
        [XmlElement("SubmountPPPlacePos")]
        public float SubmountPPPlacePos { get; set; }
        /// <summary>
        /// Submount吸嘴下料吸取时bondz的高度
        /// </summary>
        [XmlElement("SubmountPPPickPosForBlanking")]
        public float SubmountPPPickPosForBlanking { get; set; }
        /// <summary>
        /// Submount吸嘴下料放置时bondz的高度
        /// </summary>
        [XmlElement("SubmountPPPlacePosForBlanking")]
        public float SubmountPPPlacePosForBlanking { get; set; }

        [XmlElement("WaferEdgePos1")]
        public XYZTCoordinateConfig WaferEdgePos1 { get; set; }

        [XmlElement("WaferEdgePos2")]
        public XYZTCoordinateConfig WaferEdgePos2 { get; set; }

        [XmlElement("WaferEdgePos3")]
        public XYZTCoordinateConfig WaferEdgePos3 { get; set; }

        [XmlElement("WaferThickness")]
        //蓝膜厚度
        public float WaferThickness { get; set; }

        /// <summary>
        /// 视觉定位芯片时的参数
        /// </summary>
        [XmlElement("PositionComponentVisionParameters")]
        public VisionParameters PositionComponentVisionParameters { get; set; }
        /// <summary>
        /// 芯片二次定位的视觉参数
        /// </summary>
        [XmlElement("AccuracyComponentPositionVisionParameters")]
        public VisionParameters AccuracyComponentPositionVisionParameters { get; set; }
        /// <summary>
        /// 衬底视觉定位芯片时的参数
        /// </summary>
        [XmlElement("PositionSubmountVisionParameters")]
        public VisionParameters PositionSubmountVisionParameters { get; set; }
        /// <summary>
        /// 衬底二次定位时的视觉参数
        /// </summary>

        [XmlElement("AccuracySubmountPositionVisionParameters")]
        public VisionParameters AccuracySubmountPositionVisionParameters { get; set; }

        /// <summary>
        /// 退料时衬底的视觉定位参数
        /// </summary>

        [XmlElement("PositionSubmountBeforeBlankingVisionParameters")]
        public VisionParameters PositionSubmountBeforeBlankingVisionParameters { get; set; }



        //Bond头相机工作高度
        //wafer相机工作高度
        //仰视相机工作高度
        //芯片吸嘴工作高度
        //substrate吸嘴工作高度
        //顶针座高度
        //顶针高度
        #endregion

        [XmlIgnore]
        public List<MaterialMapInformation> ComponentMapInfos { get; set; }
        [XmlIgnore]
        public List<MaterialMapInformation> SubmountMapInfos { get; set; }


    }
    /// <summary>
    /// 下料时的参数,下料的吸嘴复用衬底的吸嘴，只是高度不同
    /// </summary>
    [Serializable]
    public class BlankingParameters
    {
        [XmlElement("IsCompleted")]
        public bool IsCompleted { get; set; }
        [XmlElement("BlankingMethod")]
        public EnumBlankingMethod BlankingMethod { get; set; }

        [XmlElement("PickPosition")]
        public XYZTCoordinateConfig PickPosition { get; set; }

        [XmlElement("PlacePositionForFirstSumbount")]
        public XYZTCoordinateConfig PlacePositionForFirstSumbount { get; set; }
        public BlankingParameters()
        {
            PickPosition = new XYZTCoordinateConfig();
            PlacePositionForFirstSumbount = new XYZTCoordinateConfig();
        }
    }
    /// <summary>
    /// 视觉参数类
    /// </summary>
    [Serializable]
    public class VisionParameters
    {
        [XmlElement("IsActive")]
        public bool IsActive { get; set; }
        [XmlElement("IsCompleted")]
        public bool IsCompleted { get; set; }
        [XmlElement("VisionPositionUsedCamera")]
        public EnumCameraType VisionPositionUsedCamera { get; set; }

        [XmlElement("VisionPositionMethod")]
        public EnumVisionPositioningMethod VisionPositionMethod { get; set; }
        [XmlElement("AccuracyMethod")]
        public EnumAccuracyMethod AccuracyMethod { get; set; }
        [XmlElement("AccuracyVisionPositionMethod")]
        public EnumVisionPositioningMethod AccuracyVisionPositionMethod { get; set; }
        /// <summary>
        /// 系统坐标系
        /// </summary>
        [XmlElement("PatternOffsetWithMaterialCenter")]
        public XYZTCoordinateConfig PatternOffsetWithMaterialCenter { get; set; }


        [XmlArray("ShapeMatchConfigs"), XmlArrayItem(typeof(MatchIdentificationParam))]
        public List<MatchIdentificationParam> ShapeMatchParameters { get; set; }
        [XmlArray("CircleSearchConfigs"), XmlArrayItem(typeof(CircleFindIdentificationParam))]
        public List<CircleFindIdentificationParam> CircleSearchParameters { get; set; }
        [XmlArray("EdgeSearchConfigs"), XmlArrayItem(typeof(LineFindIdentificationParam))]
        public List<LineFindIdentificationParam> LineSearchParams { get; set; }
        public VisionParameters()
        {
            ShapeMatchParameters = new List<MatchIdentificationParam>();
            CircleSearchParameters = new List<CircleFindIdentificationParam>();
            LineSearchParams = new List<LineFindIdentificationParam>();
            PatternOffsetWithMaterialCenter = new XYZTCoordinateConfig();
        }
    }
    [Serializable]
    public class BondingPositionSettings
    {
        public BondingPositionSettings()
        {
            BondPositionWithPatternOffset = new XYZTCoordinateConfig();
            BondPositionWithMaterialCenterOffset = new XYZTCoordinateConfig();
            PositionBeforeBondVisionParameters = new VisionParameters();
        }
        [XmlElement("IsComplete")]
        public bool IsComplete { get; set; }
        [XmlElement("FindBondPositionMethod")]
        public EnumFindBondPositionMethod FindBondPositionMethod { get; set; }
        /// <summary>
        /// 贴片前的视觉参数
        /// </summary>

        [XmlElement("PositionBeforeBondVisionParameters")]
        public VisionParameters PositionBeforeBondVisionParameters { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("PPWorkHeight")]
        public float PPWorkHeight { get; set; }
        /// <summary>
        /// 贴装位置和识别特征的偏移，精准贴装模式下使用此参数
        /// </summary>
        [XmlElement("BondPositionWithPatternOffset")]
        public XYZTCoordinateConfig BondPositionWithPatternOffset { get; set; }
        /// <summary>
        /// 贴装位置和物料中心的偏移，非精准贴装模式下使用此参数
        /// </summary>
        [XmlElement("BondPositionWithMaterialCenterOffset")]
        public XYZTCoordinateConfig BondPositionWithMaterialCenterOffset { get; set; }
    }

    [Serializable]
    public class EutecticParameters
    {
        [XmlElement("IsCompleted")]
        public bool IsCompleted { get; set; }

        [XmlElement("EutecticTimeMs")]
        public float EutecticTimeMs { get; set; }
        [XmlElement("EutecticPress")]
        public float EutecticPress { get; set; }
        [XmlElement("HeatSegmentParam")]
        public List<HeatSegmentParam> HeatSegmentParam { get; set; }
        public EutecticParameters()
        {
            HeatSegmentParam = new List<HeatSegmentParam>();
        }
    }
}
