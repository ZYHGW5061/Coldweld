using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using WestDragon.Framework.UtilityHelper;

namespace GlobalDataDefineClsLib
{
    [Serializable]
    public enum EnumRunningType { Actual, Simulated }

    [Serializable]
    public enum EnumVisionRunningType 
    { 
        Identification,
        IdentificationButNotLocate,
        Nonidentification,
    }

    [Serializable]
    public enum EnumStageAxis
    {
        [Description("无")]
        None = -1,

        [Description("料盒钩爪Y轴")]
        MaterialboxY = 5,

        [Description("料盒钩爪X轴")]
        MaterialboxX = 4,

        [Description("料盒钩爪Z轴")]
        MaterialboxZ = 2,

        [Description("料盒钩爪Theta轴")]
        MaterialboxT = 9,

        [Description("料盒钩爪开合轴")]
        MaterialboxHook = 7,

        [Description("物料钩子Y轴")]
        MaterialY = 3,

        [Description("物料钩子X轴")]
        MaterialX = 8,

        [Description("物料钩子Z轴")]
        MaterialZ = 1,

        [Description("物料钩子开合轴")]
        MaterialHook = 6,

        [Description("烘箱1轨道")]
        OverTrack1 = 11,

        [Description("烘箱2轨道")]
        OverTrack2 = 12,

        [Description("压机顶升轴")]
        Presslifting = 10,


    }

    [Serializable]
    public enum EnumOvenBoxNum
    {
        [Description("烘箱1")]
        Oven1 = 0,
        [Description("烘箱2")]
        Oven2 = 1,
        [Description("工作箱")]
        Box = 2
    }

    [Serializable]
    public enum EnumMaterialHooktargetNum
    {
        [Description("物料")]
        Material = 0,
        [Description("膜具")]
        MembraneEquipment = 1,
        [Description("料盒")]
        MaterialBox = 2,
    }

    [Serializable]
    public enum TowerLampMothed
    {
        [Description("待机")]
        Standby = 0,
        [Description("运行")]
        Run = 1,
        [Description("警报")]
        Alarm = 2,
        [Description("提示")]
        Remind = 3
    }

    [Serializable]
    public enum EnumRunSpeed
    {
        [Description("低速")]
        Low = 0,
        [Description("中速")]
        Medium = 1,
        [Description("高速")]
        Hight = 2,


    }

    [Serializable]
    public enum EnumOvenBoxState
    {
        [Description("其他")]
        None = -1,
        [Description("烘箱1进料")]
        Oven1In = 0,
        [Description("烘箱1出料")]
        Oven1Out = 1,
        [Description("烘箱1焊接")]
        Oven1Work = 2,
        [Description("烘箱2进料")]
        Oven2In = 3,
        [Description("烘箱2出料")]
        Oven2Out = 4,
        [Description("烘箱2焊接")]
        Oven2Work = 5,
    }

    [Serializable]
    public enum ProcessTaskStatus
    {
        [Description("进行中")]
        InProgress, // 进行中  
        [Description("已完成")]
        Completed,   // 已完成  
        [Description("已暂停")]
        Paused,      // 已暂停  
        [Description("未执行")]
        NotExecuted   // 未执行  
    }


    [Serializable]
    public enum EnumLoginResult
    {
        None = 0, Success = 1, UserNotExist = 2, PasswordWrong = 3
    }
    [Serializable]
    public class RightsInfo
    {
        public RightsInfo() { }

        public int RightsID { get; set; }
        public string RightsType { get; set; }
    }
    [Serializable]
    public class UserInfos
    {
        public UserInfos() { }

        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string usertype { get; set; }
        public string description { get; set; }
    }

    [Serializable]
    public enum EnumSensor
    {
        Oven1InteriorDoorOpen,
        Oven1InteriorDoorClose,
        Oven2InteriorDoorOpen,
        Oven2InteriorDoorClose,
        /// <summary>
        /// 烘箱抽气阀 bool
        /// </summary>
        Oven1BakeOvenBleed,
        /// <summary>
        /// 烘箱排气阀 bool
        /// </summary>
        Oven1BakeOvenExhaust,
        /// <summary>
        /// 烘箱补气阀 bool
        /// </summary>
        Oven1BakeOvenAerate,
        /// <summary>
        /// 烘箱抽气阀 bool
        /// </summary>
        Oven2BakeOvenBleed,
        /// <summary>
        /// 烘箱排气阀 bool
        /// </summary>
        Oven2BakeOvenExhaust,
        /// <summary>
        /// 烘箱补气阀 bool
        /// </summary>
        Oven2BakeOvenAerate,
        /// <summary>
        /// 工作箱抽气阀 bool
        /// </summary>
        BoxBakeOvenBleed,
        /// <summary>
        /// 工作箱排气阀 bool
        /// </summary>
        BoxBakeOvenExhaust,
        /// <summary>
        /// 工作箱补气阀 bool
        /// </summary>
        BoxBakeOvenAerate,

    }


    [Serializable]
    public enum TemperatureRtuAdd
    {
        /// <summary>
        /// 测量数值
        /// </summary>
        PV = 256,
        /// <summary>
        /// 设定值
        /// </summary>
        SV = 257,
        /// <summary>
        /// 控制输出1
        /// </summary>
        OUT1 = 258,
        /// <summary>
        /// 控制输出2
        /// </summary>
        OUT2 = 259,
        /// <summary>
        /// 状态标志
        /// </summary>
        EXE_FLG = 260,
        /// <summary>
        /// 事件输出标志
        /// </summary>
        EV_FLG = 261,
        /// <summary>
        /// 运行SV编号
        /// </summary>
        SV_No = 262,
        /// <summary>
        /// 运行PID编号
        /// </summary>
        EXE_PID = 263,
        /// <summary>
        /// 加热器1电流值
        /// </summary>
        HC1 = 265,
        /// <summary>
        /// 加热器2电流值
        /// </summary>
        HC2 = 266,
        /// <summary>
        /// DI输出状态标志
        /// </summary>
        DI_FLG = 267,
        /// <summary>
        /// 事件锁定输出标志
        /// </summary>
        EV_LAC = 269,
        /// <summary>
        /// 事件延迟ON/OFF标志
        /// </summary>
        EV_ACT = 270,
        /// <summary>
        /// 曲线运行标志
        /// </summary>
        E_PRG = 288,
        /// <summary>
        /// 曲线编号
        /// </summary>
        E_PTN = 289,
        /// <summary>
        /// 曲线数目
        /// </summary>
        E_PRG_Num = 291,
        /// <summary>
        /// 曲线步骤
        /// </summary>
        E_PTN_Num = 292,
        /// <summary>
        /// 曲线执行步骤剩余时间
        /// </summary>
        E_TIM = 293,
        /// <summary>
        /// 曲线执行PID号
        /// </summary>
        E_PID = 294,
        /// <summary>
        /// 执行SV编号
        /// </summary>
        SV_NO = 384,
        /// <summary>
        /// 手动设定时控制输出1设定值
        /// </summary>
        OUT1_Set = 386,
        /// <summary>
        /// 手动设定时控制输出2设定值
        /// </summary>
        OUT2_Set = 387,
        /// <summary>
        /// 自动/手动
        /// </summary>
        AT = 388,
        /// <summary>
        /// 手动/自动
        /// </summary>
        MAN = 389,
        /// <summary>
        /// 手动/自动
        /// </summary>
        COM = 396,
        /// <summary>
        /// 运行
        /// </summary>
        RUN = 400,
        /// <summary>
        /// 保持
        /// </summary>
        HLD = 401,
        /// <summary>
        /// 高级
        /// </summary>
        ADV = 404,
        /// <summary>
        /// 自锁
        /// </summary>
        RST_LACH = 403,
        /// <summary>
        /// 定值控制设定值1
        /// </summary>
        FIX_SV1 = 768,
        /// <summary>
        /// 定值控制设定值2
        /// </summary>
        FIX_SV2 = 769,
        /// <summary>
        /// 定值控制设定值3
        /// </summary>
        FIX_SV3 = 770,
        /// <summary>
        /// 设定值范围下限
        /// </summary>
        SV_L = 778,
        /// <summary>
        /// 设定值范围上限
        /// </summary>
        SV_H = 779,
        /// <summary>
        /// 控制输出1, 比例带1
        /// </summary>
        PB1 = 1024,
        /// <summary>
        /// 控制输出1, 积分时间1
        /// </summary>
        IT1 = 1025,
        /// <summary>
        /// 控制输出1, 微分时间1
        /// </summary>
        DT1 = 1026,
        /// <summary>
        /// 手动设定1
        /// </summary>
        MR1 = 1027,
        /// <summary>
        /// 控制输出1, 时间滞后1
        /// </summary>
        DF1 = 1028,
        /// <summary>
        /// 控制输出1, 输出范围上限1
        /// </summary>
        O11_H = 1029,
        /// <summary>
        /// 控制输出1, 目标值功能1
        /// </summary>
        SF1 = 1030,
        /// <summary>
        /// 控制输出1, 比例带2
        /// </summary>
        IT2 = 1031,
        /// <summary>
        /// 控制输出1, 积分时间2
        /// </summary>
        DT2 = 1032,
        /// <summary>
        /// 控制输出1, 微分时间2
        /// </summary>
        MR2 = 1033,
        /// <summary>
        /// 手动设定2
        /// </summary>
        DF2 = 1034,
        /// <summary>
        /// 控制输出1, 时间滞后2
        /// </summary>
        O12_L = 1035,
        /// <summary>
        /// 控制输出1, 输出范围下限2
        /// </summary>
        O12_H = 1036,
        /// <summary>
        /// 控制输出1, 输出范围上限2
        /// </summary>
        SF2 = 1037,
        /// <summary>
        /// 控制输出1, 目标值功能2
        /// </summary>
        PB3 = 1038,
        /// <summary>
        /// 控制输出1, 比例带3
        /// </summary>
        IT3 = 1039,
        /// <summary>
        /// 控制输出1, 积分时间3
        /// </summary>
        DT3 = 1040,
        /// <summary>
        /// 控制输出1, 微分时间3
        /// </summary>
        MR3 = 1041,
        /// <summary>
        /// 手动设定3
        /// </summary>
        DF3 = 1042,
        /// <summary>
        /// 控制输出1, 时间滞后3
        /// </summary>
        O13_L = 1043,
        /// <summary>
        /// 控制输出1, 输出范围下限3
        /// </summary>
        O13_H = 1044,
        /// <summary>
        /// 控制输出1, 输出范围上限3
        /// </summary>
        SF3 = 1045,
        /// <summary>
        /// 控制输出1, 目标值功能3
        /// </summary>
        ACTMD = 1046,
        /// <summary>
        /// 控制输出1, 输出特性
        /// </summary>
        O1_CYC = 1047,
        /// <summary>
        /// 控制输出1, 比例周期
        /// </summary>
        O2_CYC = 1536,
        /// <summary>
        /// 控制输出2, 输出特性
        /// </summary>
        ACTMD2 = 1537,
        /// <summary>
        /// 软启动设定数据1
        /// </summary>
        SOFTD1 = 1538,
        /// <summary>
        /// 软启动设定数据2
        /// </summary>
        SOFTD2 = 1540,
        /// <summary>
        /// 按键锁
        /// </summary>
        KLOCK = 1543,
        /// <summary>
        /// PV增益系数
        /// </summary>
        PV_G = 1546,
        /// <summary>
        /// PV偏移
        /// </summary>
        PV_B = 1547,
        /// <summary>
        /// PV滤波
        /// </summary>
        PV_F = 1553,
        /// <summary>
        /// 输入单位
        /// </summary>
        UNIT = 1792,
        /// <summary>
        /// 测量范围代码
        /// </summary>
        RANGE = 1793,
        /// <summary>
        /// 小数点
        /// </summary>
        DP = 1794,
        /// <summary>
        /// 输入修正值1
        /// </summary>
        SC_L = 1796,
        /// <summary>
        /// 输入修正值2
        /// </summary>
        SC_H = 1797,
        /// <summary>
        /// 程序模式
        /// </summary>
        PRG_MD = 1799,
        /// <summary>
        /// 曲线起始编号
        /// </summary>
        ST_PTN = 1800,
        /// <summary>
        /// 曲线段数
        /// </summary>
        PTN_CNT = 1801,
        /// <summary>
        /// 时间模式
        /// </summary>
        TIM_MOD = 2048,
    }

    [Serializable]
    public enum TurboMolecularPumpAdd
    {
        /// <summary>
        /// 输出频率
        /// </summary>
        OutputFrequency = 4096,
        /// <summary>
        /// 输出电压
        /// </summary>
        Outputvoltage = 4097,
        /// <summary>
        /// 输出电流
        /// </summary>
        Outputcurrent = 4098,
        /// <summary>
        /// 分子泵状态
        /// </summary>
        TurboMolecularPumpstatus = 4099,
        /// <summary>
        /// 命令
        /// </summary>
        Command = 8192,
        /// <summary>
        /// 锁定参数
        /// </summary>
        LockParameters = 8193,

    }

    [Serializable]
    public enum VacuumGaugeAdd
    {
        /// <summary>
        /// 真空度
        /// </summary>
        Vacuumdegree = 107,

    }

    [Serializable]
    public enum DewPointMeterAdd
    {
        /// <summary>
        /// 露点
        /// </summary>
        DewPoint = 0,

    }


    [Serializable]
    public enum EnumStageSystem
    {
        [Description("料盒钩爪")]
        MaterialboxHook,
        [Description("物料钩爪")]
        MaterialHook,
        [Description("烘箱1")]
        OverTrack1,
        [Description("烘箱2")]
        OverTrack2,
        [Description("焊台")]
        Weld
    }
    [Serializable]
    public enum EnumSystemAxis
    {
        XY,
        Z,
        Hook,
        Track,
        Focus,
        Theta,
        X
    }
    [Serializable]
    public enum EnumFindBondPositionMethod
    {
        NoAccuracy,
        Accuracy,
    }
    [Serializable]
    public enum EnumCameraProducer { HIK, DaHeng }
    [Serializable]
    public enum EnumCameraType
    {
        None = 0,
        TrackCamera = 1,
        WeldCamera = 2,
    }
    [Serializable]
    public enum EnumImageData
    {
        Grey, RGB
    }
    [Serializable]
    public enum EnumLightProducer { HIK, OPT, ZHIYUE }
    [Serializable]
    public enum EnumLightSourceType
    {
        TrackRingField, WeldRingField
    }
    [Serializable]
    public enum EnumTemperatureProducer { SHIMADEN, OMRON }

    [Serializable]
    public enum EnumTemperatureType
    {
        OvenBox1Low,OvenBox1Up, OvenBox2Low, OvenBox2Up,
    }
    [Serializable]
    public enum EnumTurboMolecularPumpProducer { TurboMolecularPump }

    [Serializable]
    public enum EnumTurboMolecularPumpType
    {
        OvenBox1, OvenBox2,Box,Done
    }
    [Serializable]
    public enum EnumVacuumGaugeProducer { VacuumGauge }

    [Serializable]
    public enum EnumVacuumGaugeType
    {
        OvenBox1, OvenBox2, Box
    }

    [Serializable]
    public enum EnumDewPointMeterProducer { DewPointMeter }
    [Serializable]
    public enum EnumDewPointMeterType
    {
        Box
    }
    [Serializable]
    public enum EnumPressureSensorProducer { PressureSensor }

    [Serializable]
    public enum EnumPressureSensorType
    {
        OvenBox1, OvenBox2, Box
    }
    [Serializable]
    public enum EnumCommunicationType { SerialPort, Ethernet }
    public enum EnumRecogniseResulSaveOption
    {
        NotSave = 0, SaveOK = 1, SaveNG = 2, AllSave = 3, SaveVacuum = 4
    }
    /// <summary>
    /// Recipe根节点步骤
    /// </summary>
    public enum EnumRecipeRootStep 
    { 
        None,
        Submount,
        Dispenser, 
        BondPosition, 
        Component 
    }
    /// <summary>
    /// Recipe步骤
    /// </summary>
    [Serializable]
    public enum EnumRecipeStep
    {
        Create,
        Configuration,

        Submount_InformationSettings,
        Submount_PositionSettings,
        Submount_MaterialMap,
        Submount_PPSettings,
        Submount_Accuracy,

        BondPosition,

        Component_InformationSettings,
        Component_PositionSettings,
        Component_MaterialMap,
        Component_PPSettings,
        Component_Accuracy,

        EutecticSettings,

        ProcessList,
        BlankingSetting,

        None
    }

    /// <summary>
    /// Recipe步骤
    /// </summary>
    [Serializable]
    public enum EnumDefinedSingleStep
    {
        CHipPPPick,
        ChipPPPlace,
        SubmountPPPick,
        SubmountPPPlace,

        ComponentVisionPosition,
        ComponentUplookingAccuracy,

        SubmountVisionPosition,
        SubmountUplookingAccuracy,

        SubmountVisionPositionBeforeBond,

        Bond,
        Eutectic,

        SubmountVisionPositionBeforeBlanking,

        None
    }
    [Serializable]
    public enum EnumDefineSetupRecipeComponentPositionStep
    {
        None,
        SetWorkHeight,
        SetComponentLeftUpperCorner,
        SetComponentRightUpperCorner,
        SetComponentRightLowerCorner,
        SetComponentLeftLowerCorner,
        VisionPosition
    }

    


    [Serializable]
    public enum EnumDefineSetupRecipeComponentMapStep
    {
        None,
        SetFirstComponentPos,
        SetColumnMap,   //包括列数和列间距
        SetRowMap,    //包括行数和行间距
        SetDeterminWaferRangeFirstPoint,
        SetDeterminWaferRangeSecondPoint,
        SetDeterminWaferRangeThirdPoint
    }
    [Serializable]
    public enum EnumDefineSetupRecipeComponentAccuracyStep
    {
        None,
        SetComponentLeftUpperCorner,
        SetComponentRightUpperCorner,
        SetComponentRightLowerCorner,
        SetComponentLeftLowerCorner,
        EdgeSearch,
        LeftUpperEdgeSearch,
        RighLowerEdgeSearch
    }

    [Serializable]
    public enum EnumDefineSetupRecipeSubmountPositionStep
    {
        None,
        SetWorkHeight,
        SetSubmountLeftUpperCorner,
        SetSubmountRightUpperCorner,
        SetSubmountRightLowerCorner,
        SetSubmountLeftLowerCorner,
        VisionPosition
    }


    [Serializable]
    public enum EnumDefineSetupRecipeSubmountMapStep
    {
        None,
        SetFirstSubmountPos,
        SetColumnMap,   //包括列数和列间距
        SetRowMap,    //包括行数和行间距
        SetDeterminWaferRangeFirstPoint,
        SetDeterminWaferRangeSecondPoint,
        SetDeterminWaferRangeThirdPoint
    }

    [Serializable]
    public enum EnumDefineSetupRecipeSubmountAccuracyStep
    {
        None,
        SetSubmountLeftUpperCorner,
        SetSubmountRightUpperCorner,
        SetSubmountRightLowerCorner,
        SetSubmountLeftLowerCorner,
        EdgeSearch,
        LeftUpperEdgeSearch,
        RighLowerEdgeSearch
    }

    [Serializable]
    public enum EnumDefineSetupRecipeBondPositionStep
    {
        None,
        SetPPHeight,
        SetSubmountLeftUpperCorner,
        SetSubmountRightUpperCorner,
        SetSubmountRightLowerCorner,
        SetSubmountLeftLowerCorner,
        VisionPosition,
        SetBondPosition
    }
    [Serializable]
    public enum EnumDefineSetupRecipeBlankingSettingsStep
    {
        None,
        SetPickParameters,
        BlankingMethod
    }
    [Serializable]
    public enum EnumDefinePPAlignStep
    {
        None,
        PositionCenterFirst,
        PositionCenterSecond
    }
    [Serializable]
    public enum EnumHardwareType
    {
        Stage,
        Camera,
        Light,
        ControlBoard,
        PowerController,
        LaserSensor,
        Dynamometer,
        TemperatureController,
        TurboMolecularPumpController,
        VacuumGaugeController,
        DewPointMeterController,
        PressureSensorController,

    }
    [Serializable]
    public enum EnumCarrierType
    {
        [Description("蓝膜")]
        Wafer,
        [Description("华夫盒")]
        WafflePack,
        [Description("蓝膜华夫盒")]
        WaferWafflePack
    }
    [Serializable]
    public enum EnumBlankingMethod
    {
        OriginalRoad,
        BlankingArea
    }
    /// <summary>
    /// 加热段参数
    /// </summary>
    [Serializable]
    public class HeatSegmentParam
    {
        public int segmentNo { get; set; }

        public double TimespanMinite { get; set; }

        public double TargetTemprerature { get; set; }

    }
    [Serializable]
    public enum EnumUsedPP
    {
        ChipPP,
        SubmountPP
    }
    [Serializable]
    public enum EnumVisionPositioningMethod
    {
        PatternSearch,
        CircleSearch,
        EdgeSearch
    }
    [Serializable]
    public enum EnumAccuracyMethod
    {
        None,
        UplookingCamera,
        CalibrationTable
    }

    [Serializable]
    public enum EnumMaterialBoxstate
    {
        Unwelded,
        Welded,
        Jumping,
    }

    [Serializable]
    public enum EnumMaterialBoxLocationNumber
    {
        Oven1,
        Oven2,
        OutOvenArea,
        FreeArea1,
        WeldArea,
        FreeArea2,

    }

    [Serializable]
    public enum EnumMaterialstate
    {
        Unwelded,
        Welded,
        Jumping,
        Totheweldingstation
    }


    [Serializable]
    public class EnumMaterialBoxproperties
    {
        public EnumMaterialBoxproperties()
        {
            MaterialBoxPosition = new XYZTCoordinateConfig();
            MaterialBoxstate = EnumMaterialBoxstate.Unwelded;
            MaterialBoxLocationNumber = EnumMaterialBoxLocationNumber.Oven1;
        }

        /// <summary>
        /// 料盒位置
        /// </summary>
        [XmlElement("MaterialBoxPosition")]
        public XYZTCoordinateConfig MaterialBoxPosition { get; set; }

        /// <summary>
        /// 料盒状态
        /// </summary>
        [XmlElement("MaterialBoxstate")]
        public EnumMaterialBoxstate MaterialBoxstate;


        /// <summary>
        /// 料盒所在位置编号
        /// </summary>
        [XmlElement("MaterialBoxLocationNumber")]
        public EnumMaterialBoxLocationNumber MaterialBoxLocationNumber { get; set; }


    }

    [Serializable]
    public class EnumReturnMaterialBoxproperties
    {
        public EnumReturnMaterialBoxproperties()
        {
            MaterialBoxMat = new List<EnumMaterialBoxproperties>();
            MaterialBoxMat.Add(
                new EnumMaterialBoxproperties()
                {
                    MaterialBoxLocationNumber = EnumMaterialBoxLocationNumber.Oven1,
                    MaterialBoxstate = EnumMaterialBoxstate.Unwelded
                }
                );
            MaterialBoxMat.Add(
                new EnumMaterialBoxproperties()
                {
                    MaterialBoxLocationNumber = EnumMaterialBoxLocationNumber.Oven1,
                    MaterialBoxstate = EnumMaterialBoxstate.Unwelded
                }
                );
        }
        public int Re { get; set; }
        public List<EnumMaterialBoxproperties> MaterialBoxMat { get; set; }
    }

    [Serializable]
    public class EnumMaterialproperties
    {
        public EnumMaterialproperties()
        {
            MaterialPosition = new XYZTCoordinateConfig();
            Materialstate = EnumMaterialstate.Unwelded;
        }

        /// <summary>
        /// 物料位置
        /// </summary>
        [XmlElement("MaterialPosition")]
        public XYZTCoordinateConfig MaterialPosition { get; set; }

        /// <summary>
        /// 物料状态
        /// </summary>
        [XmlElement("Materialstate")]
        public EnumMaterialstate Materialstate;


        /// <summary>
        /// 物料行数
        /// </summary>
        [XmlElement("MaterialRowNumber")]
        public int MaterialRowNumber { get; set; }


        /// <summary>
        /// 物料行数
        /// </summary>
        [XmlElement("MaterialColNumber")]
        public int MaterialColNumber { get; set; }
    }

    [Serializable]
    public class EnumReturnMaterialproperties : INotifyPropertyChanged
    {
        private static readonly Lazy<EnumReturnMaterialproperties> instance = new Lazy<EnumReturnMaterialproperties>(() => new EnumReturnMaterialproperties());

        public static EnumReturnMaterialproperties Instance => instance.Value;

        public EnumReturnMaterialproperties()
        {
            materialMat = new List<List<EnumMaterialproperties>>();
            MaterialMat = new List<List<EnumMaterialproperties>>();
        }
        private int re;
        private List<List<EnumMaterialproperties>> materialMat;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Re
        {
            get { return re; }
            set
            {
                if (re != value)
                {
                    re = value;
                    OnPropertyChanged(nameof(Re));
                }
            }
        }

        public List<List<EnumMaterialproperties>> MaterialMat
        {
            get { return materialMat; }
            set
            {
                if (materialMat != value)
                {
                    materialMat = value;
                    OnPropertyChanged(nameof(MaterialMat));
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public EnumReturnMaterialproperties()
        //{
        //    MaterialMat = new List<List<EnumMaterialproperties>>();
        //}
        //public int Re { get; set; }
        //public List<List<EnumMaterialproperties>> MaterialMat { get; set; }
    }

    [Serializable]
    public class EnumTrainsportMaterialParam
    {
        public EnumTrainsportMaterialParam()
        {
            MaterialSize = new XYZTCoordinateConfig();
            TrackCameraIdentifyMaterialMatch = new MatchIdentificationParam();
            WeldCameraIdentifyMaterialMatch = new MatchIdentificationParam();
        }

        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 物料宽度、长度、厚度
        /// </summary>
        [XmlElement("MaterialSize")]
        public XYZTCoordinateConfig MaterialSize { get; set; }

        /// <summary>
        /// 搬送相机识别物料
        /// </summary>
        [XmlElement("TrackCameraIdentifyMaterialMatch")]
        public MatchIdentificationParam TrackCameraIdentifyMaterialMatch { get; set; }

        /// <summary>
        /// 焊接相机识别物料
        /// </summary>
        [XmlElement("WeldCameraIdentifyMaterialMatch")]
        public MatchIdentificationParam WeldCameraIdentifyMaterialMatch { get; set; }

        /// <summary>
        /// 焊接相机识别物料
        /// </summary>
        [XmlArray("WeldCameraIdentifyMaterialMatchs"), XmlArrayItem(typeof(MatchIdentificationParam))]
        public List<MatchIdentificationParam> WeldCameraIdentifyMaterialMatchs { get; set; }

    }


    [Serializable]
    public class EnumTrainsportMaterialboxParam
    {
        public EnumTrainsportMaterialboxParam()
        {
            MaterialParam = new EnumTrainsportMaterialParam();
            MaterialboxSize = new XYZTCoordinateConfig();
            MaterialboxCenter = new XYZTCoordinateConfig();
            TrackCameraIdentifyMaterialBoxMatch = new MatchIdentificationParam();

            Iswelded = false;

            MaterialRowNumber = 15;
            MaterialColNumber = 15;

            MaterialMat = new List<List<EnumMaterialproperties>>();

            //for (int i = 0; i < MaterialRowNumber; i++)
            //{
            //    List<EnumMaterialproperties> row = new List<EnumMaterialproperties>();
            //    for (int j = 0; j < MaterialColNumber; j++)
            //    {
            //        row.Add(new EnumMaterialproperties()
            //        {
            //            Materialstate = EnumMaterialstate.Unwelded,
            //            MaterialPosition = new XYZTCoordinateConfig(),
            //            MaterialRowNumber = i,
            //            MaterialColNumber = j,

            //        });
            //    }
            //    MaterialMat.Add(row);
            //}

        }

        public void InitMaterialMat()
        {
            if(MaterialMat?.Count == MaterialRowNumber && MaterialMat[0]?.Count == MaterialColNumber)
            {
                return;
            }

            MaterialMat = new List<List<EnumMaterialproperties>>();

            for (int i = 0; i < this.MaterialRowNumber; i++) // 添加3行  
            {
                List<EnumMaterialproperties> row = new List<EnumMaterialproperties>();
                for (int j = 0; j < this.MaterialColNumber; j++) // 每行添加4列  
                {
                    row.Add(new EnumMaterialproperties()
                    {
                        Materialstate = EnumMaterialstate.Unwelded,
                        MaterialPosition = new XYZTCoordinateConfig(),
                        MaterialRowNumber = i,
                        MaterialColNumber = j,

                    });
                }
                MaterialMat.Add(row);
            }
        }

        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 物料参数
        /// </summary>
        [XmlElement("MaterialParam")]
        public EnumTrainsportMaterialParam MaterialParam { get; set; }

        /// <summary>
        /// 料盒宽度、长度、厚度
        /// </summary>
        [XmlElement("MaterialboxSize")]
        public XYZTCoordinateConfig MaterialboxSize { get; set; }

        /// <summary>
        /// 料盒焊接时中心坐标
        /// </summary>
        [XmlElement("MaterialboxCenter")]
        public XYZTCoordinateConfig MaterialboxCenter { get; set; }

        /// <summary>
        /// 物料个数
        /// </summary>
        [XmlElement("MaterialNumber")]
        public int MaterialNumber { get; set; }
        /// <summary>
        /// 物料行数
        /// </summary>
        [XmlElement("MaterialRowNumber")]
        public int MaterialRowNumber { get; set; }
        /// <summary>
        /// 物料列数
        /// </summary>
        [XmlElement("MaterialColNumber")]
        public int MaterialColNumber { get; set; }

        /// <summary>
        /// 物料矩阵
        /// </summary>
        [XmlArray("MaterialMat"), XmlArrayItem(typeof(List<EnumMaterialproperties>))]
        public List<List<EnumMaterialproperties>> MaterialMat { get; set; }


        /// <summary>
        /// 物料行间距
        /// </summary>
        [XmlElement("MaterialRowinterval")]
        public float MaterialRowinterval { get; set; }
        /// <summary>
        /// 物料列间距
        /// </summary>
        [XmlElement("MaterialColinterval")]
        public float MaterialColinterval { get; set; }

        /// <summary>
        /// 已经焊接
        /// </summary>
        [XmlElement("Iswelded")]
        public bool Iswelded { get; set; }


        /// <summary>
        /// 搬送相机识别料盒
        /// </summary>
        [XmlElement("TrackCameraIdentifyMaterialBoxMatch")]
        public MatchIdentificationParam TrackCameraIdentifyMaterialBoxMatch { get; set; }


        public void SetMaterialMat(XYZTCoordinateConfig Firstxyz, XYZTCoordinateConfig Centerxyzt, XYZTCoordinateConfig Centerxyztoffset,
            double MaterialArrayQxX = 0.015799999999984, double MaterialArrayQxY = -19.999300000000005,
            double MaterialArrayQyX = -19.999899999999997, double MaterialArrayQyY = -0.137999999999977,
            double MaterialArrayQzX = 0.062099999999996, double MaterialArrayQzY = 0.191399999999998)
        {
            double Xoffset = Centerxyztoffset.X - Centerxyzt.X;
            double Yoffset = Centerxyztoffset.Y - Centerxyzt.Y;
            double Zoffset = (Centerxyztoffset.Z - Centerxyzt.Z) / MaterialColinterval / 2;
            double Toffset = Centerxyztoffset.Theta - Centerxyzt.Theta;

            double angleInRadians = Toffset * (Math.PI / 180.0);

            #region 241206前固定位置补偿版本，最下角为起点


            //double XBC = 0.14 / MaterialColNumber / 15.0f * 7.0f;
            //double YBC = 0.25 / MaterialRowNumber / 15.0f * 7.0f;
            //double ZBC = -0.15 / MaterialRowNumber / 15.0f * 7.0f;

            //double XiB = MaterialRowinterval / (20) * (19.9952);
            //double XjB = MaterialRowinterval / (20) * (-0.1133);
            //double YiB = MaterialRowinterval / (20) * (0.1419);
            //double YjB = MaterialRowinterval / (20) * (19.9515);
            //double ZiB = MaterialRowinterval / (20) * (-0.1628);
            //double ZjB = MaterialRowinterval / (20) * (-0.0378);

            //for (int i = 0; i < MaterialRowNumber; i++)
            //{
            //    for (int j = 0; j < MaterialColNumber; j++)
            //    {
            //        double X0 = Firstxyz.X + i * XiB + j * XjB + i * j * XBC;

            //        double Y0 = Firstxyz.Y + i * YiB + j * YjB + j * i * YBC;

            //        double Z0 = Firstxyz.Z + i * ZiB + j * ZjB + j * i * ZBC;

            //        XYZTCoordinateConfig xyz = new XYZTCoordinateConfig()
            //        {
            //            X = X0,
            //            Y = Y0,
            //            Z = Z0,
            //        };

            //        MaterialMat[i][j].MaterialPosition = xyz;
            //    }
            //}

            #endregion

            #region 最初的位置加角度补偿版本


            //for (int i = 0; i < MaterialRowNumber; i++)
            //{
            //    for (int j = 0; j < MaterialColNumber; j++)
            //    {
            //        double X = Firstxyz.X + j * MaterialColinterval;
            //        double Y = Firstxyz.Y + i * MaterialRowinterval;

            //        double Z = i * MaterialRowinterval * Zoffset + j * MaterialColinterval * Zoffset;

            //        double xNew1 = Math.Cos(angleInRadians) * (X - Centerxyzt.X) - Math.Sin(angleInRadians) * (Y - Centerxyzt.Y) + Centerxyzt.X;
            //        double yNew1 = Math.Sin(angleInRadians) * (X - Centerxyzt.X) + Math.Cos(angleInRadians) * (Y - Centerxyzt.Y) + Centerxyzt.Y;

            //        XYZTCoordinateConfig xyz = new XYZTCoordinateConfig()
            //        {
            //            X = xNew1 + Xoffset,
            //            Y = yNew1 + Yoffset,
            //            Z = Firstxyz.Z + Z,
            //        };

            //        MaterialMat[i][j].MaterialPosition = xyz;

            //    }
            //}


            #endregion


            #region 241206大气下中心旋转补偿

            //for (int i = 0; i < MaterialRowNumber; i++)
            //{
            //    for (int j = 0; j < MaterialColNumber; j++)
            //    {
            //        double X = 7 - i;
            //        double Y = 7 - j;
            //        double Qx = 147.4335 + X * 0.015799999999984 + Y * -19.999300000000005;
            //        double Qy = -160.4275 + X * -19.999899999999997 + Y * -0.137999999999977;
            //        double Qz = -42.8473 + X * 0.061099999999996 + Y * 0.101399999999998;

            //        double X0 = Qx;

            //        double Y0 = Qy;

            //        double Z0 = Qz;

            //        XYZTCoordinateConfig xyz = new XYZTCoordinateConfig()
            //        {
            //            X = X0,
            //            Y = Y0,
            //            Z = Z0,
            //        };

            //        MaterialMat[i][j].MaterialPosition = xyz;
            //    }
            //}

            #endregion


            #region 241206真空下中心旋转补偿

            //for (int i = 0; i < MaterialRowNumber; i++)
            //{
            //    for (int j = 0; j < MaterialColNumber; j++)
            //    {
            //        double X = 7 - i;
            //        double Y = 7 - j;
            //        double Qx = 147.6465 + X * 0.015799999999984 + Y * -19.999300000000005;
            //        double Qy = -160.4275 + X * -19.999899999999997 + Y * -0.137999999999977;
            //        double Qz = -43.5 + X * 0.062099999999996 + Y * 0.191399999999998;

            //        double X0 = Qx;

            //        double Y0 = Qy;

            //        double Z0 = Qz;

            //        XYZTCoordinateConfig xyz = new XYZTCoordinateConfig()
            //        {
            //            X = X0,
            //            Y = Y0,
            //            Z = Z0,
            //        };

            //        MaterialMat[i][j].MaterialPosition = xyz;
            //    }
            //}

            #endregion

            #region 241216真空下中心旋转补偿，参数可设置

            if((MaterialRowNumber % 2 != 0) && (MaterialColNumber % 2 != 0))
            {
                for (int i = 0; i < MaterialRowNumber; i++)
                {
                    for (int j = 0; j < MaterialColNumber; j++)
                    {
                        double X = (MaterialRowNumber - 1) / 2 - i;
                        double Y = (MaterialColNumber - 1) / 2 - j;
                        double Qx = Firstxyz.X + X * MaterialArrayQxX + Y * MaterialArrayQxY ;
                        double Qy = Firstxyz.Y + X * MaterialArrayQyX + Y * MaterialArrayQyY;
                        double Qz = Firstxyz.Z + X * MaterialArrayQzX + Y * MaterialArrayQzY;

                        double X0 = Qx;

                        double Y0 = Qy;

                        double Z0 = Qz;

                        XYZTCoordinateConfig xyz = new XYZTCoordinateConfig()
                        {
                            X = X0,
                            Y = Y0,
                            Z = Z0,
                        };

                        MaterialMat[i][j].MaterialPosition = xyz;
                    }
                }

            }


            #endregion



        }

    }




    [Serializable]
    public class EnumOverBoxParam
    {
        public EnumOverBoxParam()
        {
            MaterialboxParam = new List<EnumTrainsportMaterialboxParam>();
            //MaterialboxParam.Add(new EnumTrainsportMaterialboxParam());
            //MaterialboxParam.Add(new EnumTrainsportMaterialboxParam());
        }

        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 料盒参数
        /// </summary>
        [XmlArray("MaterialboxParam"), XmlArrayItem(typeof(EnumTrainsportMaterialboxParam))]
        public List<EnumTrainsportMaterialboxParam> MaterialboxParam { get; set; }


        /// <summary>
        /// 料盒层数
        /// </summary>
        [XmlElement("OverBoxMaterialBoxLayerNumber")]
        public int OverBoxMaterialBoxLayerNumber { get; set; }

        /// <summary>
        /// 放料盘的次数
        /// </summary>
        [XmlElement("OverBoxMaterialBoxGetInNumber")]
        public int OverBoxMaterialBoxGetInNumber { get; set; }

    }



    [Serializable]
    public class ShapeMatchParameters
    {
        public ShapeMatchParameters()
        {
            MaskSetting = new List<RecogniseMaskSetting>();
            BondTablePositionOfCreatePattern = new XYZTCoordinateConfig();
            WaferTablePositionOfCreatePattern = new XYZTCoordinateConfig();
            PositionOfMaterialCenter = new XYZTCoordinateConfig();
            RingLightSetting = new OpticalSettings();
            DirectLightSetting = new OpticalSettings();
            ROILeftUpperPoint = new PointF();
        }
        [XmlElement("Name")]
        public string Name { get; set; }


        [XmlElement("CameraZWorkPosition")]
        public float CameraZWorkPosition { get; set; }
        [XmlElement("OrigionAngle")]
        public float OrigionAngle { get; set; }
        /// <summary>
        /// 创建模板时BondTable的位置
        /// </summary>
        [XmlElement("BondTablePositionOfCreatePattern")]
        public XYZTCoordinateConfig BondTablePositionOfCreatePattern { get; set; }
        /// <summary>
        /// 创建模板时物料中心的系统坐标位
        /// </summary>
        [XmlIgnore]
        public XYZTCoordinateConfig PositionOfMaterialCenter { get; set; }
        /// <summary>
        /// 创建模板时WaferTable的位置
        /// </summary>
        [XmlElement("WaferTablePositionOfCreatePattern")]
        public XYZTCoordinateConfig WaferTablePositionOfCreatePattern { get; set; }

        [XmlElement("RingLightSetting")]
        public OpticalSettings RingLightSetting { get; set; }
        [XmlElement("DirectLightSetting")]
        public OpticalSettings DirectLightSetting { get; set; }
        [XmlElement("ScoreThreshold")]
        public double ScoreThreshold { get; set; }
        [XmlElement("AcceptAngleThreshold")]
        public double AcceptAngleThreshold { get; set; }




        /// <summary>
        /// 
        /// </summary>
        [XmlElement("TrainFileFullName")]
        public string TrainFileFullName { get; set; }
        [XmlElement("TemplateFileFullName")]
        public string TemplateFileFullName { get; set; }
        [XmlElement("TemplateRegionLeftUpperPoint")]
        public PointF TemplateRegionLeftUpperPoint { get; set; }
        [XmlElement("TemplateRegionWidth")]
        public float TemplateRegionWidth { get; set; }
        [XmlElement("TemplateRegionHeight")]
        public float TemplateRegionHeight { get; set; }

        [XmlElement("ROILeftUpperPoint")]
        public PointF ROILeftUpperPoint { get; set; }
        [XmlElement("ROIWidth")]
        public float ROIWidth { get; set; }
        [XmlElement("ROIHeight")]
        public float ROIHeight { get; set; }
        [XmlArray("MaskSetting"), XmlArrayItem(typeof(RecogniseMaskSetting))]
        public List<RecogniseMaskSetting> MaskSetting { get; set; }

    }
    [Serializable]
    [XmlType("Mask")]
    public class RecogniseMaskSetting
    {
        [XmlElement("MaskRegionLeftUpperPoint")]
        public PointF MaskRegionLeftUpperPoint { get; set; }
        [XmlElement("MaskRegionWidth")]
        public float MaskRegionWidth { get; set; }
        [XmlElement("MaskRegionHeight")]
        public float MaskRegionHeight { get; set; }
        public RecogniseMaskSetting()
        {
            MaskRegionLeftUpperPoint = new PointF();
        }
    }

    [Serializable]
    public class CircleSearchParameters
    {
        public CircleSearchParameters()
        {
            MaskSetting = new List<RecogniseMaskSetting>();
            BondTablePosition = new XYZTCoordinateConfig();
            WaferTablePosition = new XYZTCoordinateConfig();
        }
        [XmlElement("Name")]
        public string Name { get; set; }

        //[XmlElement("UsedCamera")]
        //public EnumCameraType UsedCamera { get; set; }
        [XmlElement("BondTablePosition")]
        public XYZTCoordinateConfig BondTablePosition { get; set; }
        [XmlElement("WaferTablePosition")]
        public XYZTCoordinateConfig WaferTablePosition { get; set; }
        [XmlElement("CameraZWorkPosition")]
        public float CameraZWorkPosition { get; set; }
        [XmlElement("RingLightSetting")]
        public OpticalSettings RingLightSetting { get; set; }
        [XmlElement("DirectLightSetting")]
        public OpticalSettings DirectLightSetting { get; set; }
        [XmlElement("ScoreThreshold")]
        public double ScoreThreshold { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("TrainFileFullName")]
        public string TrainFileFullName { get; set; }
        [XmlElement("TemplateFileFullName")]
        public string TemplateFileFullName { get; set; }
        [XmlElement("TemplateRegionLeftUpperPoint")]
        public PointF TemplateRegionLeftUpperPoint { get; set; }
        [XmlElement("TemplateRegionWidth")]
        public float TemplateRegionWidth { get; set; }
        [XmlElement("TemplateRegionHeight")]
        public float TemplateRegionHeight { get; set; }

        [XmlElement("ROILeftUpperPoint")]
        public PointF ROILeftUpperPoint { get; set; }
        [XmlElement("ROIWidth")]
        public float ROIWidth { get; set; }
        [XmlElement("ROIHeight")]
        public float ROIHeight { get; set; }
        [XmlArray("MaskSetting"), XmlArrayItem(typeof(RecogniseMaskSetting))]
        public List<RecogniseMaskSetting> MaskSetting { get; set; }
    }
    [Serializable]
    public class EdgeSearchParameters
    {
        public EdgeSearchParameters()
        {
            MaskSetting = new List<RecogniseMaskSetting>();
            BondTablePosition = new XYZTCoordinateConfig();
            WaferTablePosition = new XYZTCoordinateConfig();
            RingLightSetting = new OpticalSettings();
            DirectLightSetting = new OpticalSettings();
        }
        [XmlElement("Name")]
        public string Name { get; set; }

        //[XmlElement("UsedCamera")]
        //public EnumCameraType UsedCamera { get; set; }
        [XmlElement("BondTablePosition")]
        public XYZTCoordinateConfig BondTablePosition { get; set; }
        [XmlElement("WaferTablePosition")]
        public XYZTCoordinateConfig WaferTablePosition { get; set; }
        [XmlElement("CameraZWorkPosition")]
        public float CameraZWorkPosition { get; set; }
        [XmlElement("OrigionAngle")]
        public float OrigionAngle { get; set; }
        [XmlElement("RingLightSetting")]
        public OpticalSettings RingLightSetting { get; set; }
        [XmlElement("DirectLightSetting")]
        public OpticalSettings DirectLightSetting { get; set; }
        [XmlElement("ScoreThreshold")]
        public double ScoreThreshold { get; set; }
        [XmlElement("AcceptAngleThreshold")]
        public double AcceptAngleThreshold { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("TrainFileFullName")]
        public string TrainFileFullName { get; set; }
        [XmlElement("TemplateFileFullName")]
        public string TemplateFileFullName { get; set; }
        [XmlElement("UpEdgeSearchTemplateFileFullName")]
        public string UpEdgeSearchTemplateFileFullName { get; set; }
        [XmlElement("RightEdgeSearchTemplateFileFullName")]
        public string RightEdgeSearchTemplateFileFullName { get; set; }
        [XmlElement("DownEdgeSearchTemplateFileFullName")]
        public string DownEdgeSearchTemplateFileFullName { get; set; }
        [XmlElement("LeftEdgeSearchTemplateFileFullName")]
        public string LeftEdgeSearchTemplateFileFullName { get; set; }
        [XmlElement("TemplateRegionLeftUpperPoint")]
        public PointF TemplateRegionLeftUpperPoint { get; set; }
        [XmlElement("TemplateRegionWidth")]
        public float TemplateRegionWidth { get; set; }
        [XmlElement("TemplateRegionHeight")]
        public float TemplateRegionHeight { get; set; }

        [XmlElement("ROILeftUpperPoint")]
        public PointF ROILeftUpperPoint { get; set; }
        [XmlElement("ROIWidth")]
        public float ROIWidth { get; set; }
        [XmlElement("ROIHeight")]
        public float ROIHeight { get; set; }
        [XmlArray("MaskSetting"), XmlArrayItem(typeof(RecogniseMaskSetting))]
        public List<RecogniseMaskSetting> MaskSetting { get; set; }
    }
    [Serializable]
    public class OpticalSettings
    {
        [XmlAttribute("Active")]
        public bool Active { get; set; }
        [XmlAttribute("LightColor")]
        public EnumLightColor LightColor { get; set; }

        [XmlAttribute("Brightness")]
        public float Brightness { get; set; }


    }
    /// <summary>
    /// 自定义回调动作抽象基类
    /// 用于向特定的过程返回数据并进行自定义操作。
    /// </summary>
    public abstract class ACustomActionBaseClass { }

    [Serializable]
    public enum EnumRecipeType { INVALID = 0, Bonder = 1, Heat = 2, Transport = 3, Material = 4, MaterialBox = 5 }
    /// <summary>
    /// 用于保存Job分析结果的类
    /// </summary>
    [Serializable]
    public sealed class JobResult
    {
        /// <summary>
        /// 最终结果
        /// </summary>
        //public List<ProcessResult> LidFinalResults { get; set; }
        //public List<ProcessResult> ShellFinalResults { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProcessJobName { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public JobResult()
        {
            //LidFinalResults = new List<ProcessResult>();
            //ShellFinalResults = new List<ProcessResult>();
        }
    }
    [Serializable]
    public enum EnumTransferAction
    {
        /// <summary>
        /// 料盒钩爪到空闲位置
        /// </summary>
        MaterialboxHooktoSafePosition,
        /// <summary>
        /// 料盒出烘箱
        /// </summary>
        MaterialboxOutofoven,
        /// <summary>
        /// 料盒钩爪到料盒上方
        /// </summary>
        MaterialboxHooktoMaterialbox,
        /// <summary>
        /// 料盒钩爪拾起料盒
        /// </summary>
        MaterialboxHookPickupMaterialbox,
        /// <summary>
        /// 料盒钩爪到目标位置
        /// </summary>
        MaterialboxHooktoTargetPosition,
        /// <summary>
        /// 料盒钩爪放下料盒
        /// </summary>
        MaterialboxHookPutdownMaterialbox,
        /// <summary>
        /// 料盒进烘箱
        /// </summary>
        MaterialboxInofoven,

        /// <summary>
        /// 物料钩爪到空闲位置
        /// </summary>
        MaterialHooktoSafePosition,
        /// <summary>
        /// 物料钩爪到物料上方
        /// </summary>
        MaterialHooktoMaterial,
        /// <summary>
        /// 物料钩爪拾起物料
        /// </summary>
        MaterialHookPickupMaterial,
        /// <summary>
        /// 物料钩爪到目标位置
        /// </summary>
        MaterialHooktoTargetPosition,
        /// <summary>
        /// 物料钩爪放下物料
        /// </summary>
        MaterialHookPutdownMaterial,




    }
    [Serializable]
    public enum EnumTransferStatus
    {
        TransferCompleted, //整盒传输完成
        StripTransferCompleted, //整盒传输完成
        Transfering,       //正在传输
        ExceptionAborted,  //传输异常停止
        UserAbort,         //用户停止传输
        StripCleared,      //清片完成
        StripLoaded,        //一键上片完成
        /// <summary>
        /// 料盒在buffer
        /// </summary>
        MaterialboxinBuffer,
        /// <summary>
        /// 料盒在交换箱
        /// </summary>
        MaterialboxinExchangebox,
        /// <summary>
        /// 料盒在料盒轨道
        /// </summary>
        MaterialboxinMaterialboxTrack,
        /// <summary>
        /// 料盒在烘箱
        /// </summary>
        MaterialboxinOven,
        /// <summary>
        /// 料条在料条轨道
        /// </summary>
        MaterialstripinMaterialstripTrack,
        /// <summary>
        /// 料条在焊台
        /// </summary>
        MaterialstripinWeld,
        /// <summary>
        /// 无料盒无料条
        /// </summary>
        NoMateralboxNoMateralstrip
    }
    [Serializable]
    public enum EnumProcessingState
    {
        INIT = 0,
        IDLE = 2,
        SETUP = 3,
        READY = 4,
        EXECUTING = 5,
        PAUSE = 6
    }
    [Serializable]
    public enum EnumProcessJobState
    {
        Created = 255,
        Queued = 0,
        SettingUp = 1,
        WaitingForStart = 2,
        Processing = 3,
        ProcessComplete = 4,
        Pausing = 6,
        Paused = 7,
        Stopping = 8,
        Aborting = 9,
        Stopped = 10,
        Aborted = 11
    }
    [Serializable]
    public enum EnumBondJobStatus
    {
        Idle = 0,
        Running = 1,
        UserAbort = 2,
        ProcessCompleted = 3,
        ProcessError = 4,
        TransferError = 5,
        TransferCompleted = 6,
        VisionFailed = 7,
        NeedManualLoadSubstrate,
        NeedManualFindComponent
    }
    /// <summary>
    /// 支持权重对象的随机对象接口
    /// </summary>
    public interface IRandomObject
    {
        /// <summary>
        /// 权重
        /// </summary>
        int Weight
        {
            set;
            get;
        }
    }
    [Serializable]
    public class MaterialMapInformation : ACloneable<MaterialMapInformation>, IComparable, IEqualityComparer<MaterialMapInformation>, IRandomObject
    {
        #region IRandomObject
        /// <summary>
        /// 权重
        /// </summary>
        [XmlAttribute("Weight")]
        public int Weight { set; get; }
        #endregion

        /// <summary>
        /// Wafer坐标系的坐标值
        /// </summary>
        public PointF MaterialLocation;
        /// <summary>
        /// 索引坐标。
        /// </summary>
        public Point MaterialCoordIndex;

        /// <summary>
        /// 编号
        /// </summary>
        [XmlAttribute("MaterialNumber")]
        public int MaterialNumber;

        /// <summary>
        /// material是否存在
        /// </summary>
        [XmlAttribute("IsMaterialExist")]
        public bool IsMaterialExist;

        /// <summary>
        /// 是否是Mark
        /// </summary>
        [XmlAttribute("IsMark")]
        public bool IsMark;

        /// <summary>
        /// 属性
        /// </summary>
        [XmlAttribute("Properties")]
        public MaterialProperties Properties { get; set; }
        [XmlIgnore]
        MaterialProcessResult ProcessResult { get; set; }

        /// <summary>
        /// 是否需要检测
        /// </summary>
        [XmlAttribute("IsProcess")]
        public bool IsProcess;

        /// <summary>
        /// 构造函数。
        /// </summary>
        public MaterialMapInformation()
        {
            IsMaterialExist = true;
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="number"></param>
        /// <param name="coordIndex"></param>
        /// <param name="location"></param>
        /// <param name="patternAngle"></param>
        /// <param name="toBePorcessed"></param>
        public MaterialMapInformation(int number, Point coordIndex, PointF location, double patternAngle, bool toBePorcessed)
        {
            IsMaterialExist = true;
            MaterialNumber = number;
            MaterialCoordIndex = coordIndex;
            MaterialLocation = location;
        }

        /// <summary>
        /// 对象比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            try
            {
                int positive = 0;
                if (obj is MaterialMapInformation)
                {
                    MaterialMapInformation compareObject = obj as MaterialMapInformation;
                    if (this.MaterialCoordIndex.X < compareObject.MaterialCoordIndex.X)
                    {
                        positive = this.MaterialCoordIndex.Y <= compareObject.MaterialCoordIndex.Y ? -1 : 1;
                    }
                    else if (this.MaterialCoordIndex.X > compareObject.MaterialCoordIndex.X)
                    {
                        positive = this.MaterialCoordIndex.Y < compareObject.MaterialCoordIndex.Y ? -1 : 1;
                    }
                    else
                    {
                        positive = this.MaterialCoordIndex.Y.CompareTo(compareObject.MaterialCoordIndex.Y);
                    }
                }
                return -positive;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + " <- " + ex.TargetSite.Name);
                return 0;
            }
        }

        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(MaterialMapInformation obj)
        {
            return obj.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public bool Equals(MaterialMapInformation obj1, MaterialMapInformation obj2)
        {
            return obj1.MaterialCoordIndex.Equals(obj2.MaterialCoordIndex);
        }
    }
    /// <summary>
    /// 机台工艺检测状态
    /// </summary>
    [Serializable]
    public enum EnumProcessStatus
    {
        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownError,
        /// <summary>
        /// 空闲状态
        /// </summary>
        Idle,
        /// <summary>
        /// 运行中
        /// </summary>
        Running,
        /// <summary>
        /// 用户停止
        /// </summary>
        Aborted,
        /// <summary>
        ///
        /// </summary>
        Completed,
        /// <summary>
        /// 暂停
        /// </summary>
        Paused,
        /// <summary>
        /// 
        /// </summary>
        WaferInspectError,
        /// <summary>
        /// Wafer传送异常
        /// </summary>
        WaferTransferError,
        /// <summary>
        /// 这里指一盒扫描完成，指一次循环跑片
        /// </summary>
        TransferCompleted
    }
    [Serializable]
    public enum EnumLightColor
    {
        Red,
        Green,
        Blue,
        White
    }


    [Serializable]
    public class EnumPosition
    {
        public EnumOvenBoxNum Num { get; set; }

        public int MaterialBoxNum { get; set; } = 0;

        public int MaterialNum { get; set; } = 0;
        public int MaterialRow { get; set; } = 0;
        public int MaterialCol { get; set; } = 0;

        public EnumPosition()
        {


        }
        public EnumPosition(EnumOvenBoxNum Num, int MaterialBoxNum)
        {
            this.Num = Num;
            this.MaterialBoxNum = MaterialBoxNum;
        }

    }


    [Serializable]
    public class EnumOvenStates
    {
        public EnumOvenBoxState State { get; set; }

        public EnumOvenStates()
        {


        }
        public EnumOvenStates(EnumOvenBoxState State)
        {
            this.State = State;
        }

    }

    [Serializable]
    public class EnumMoveSpeedParameter
    {
        public int code { get; set; }

        public EnumRunSpeed speed { get; set; }

        public EnumMoveSpeedParameter()
        {


        }
        public EnumMoveSpeedParameter(int code, EnumRunSpeed speed = EnumRunSpeed.Medium)
        {
            this.code = code;
            this.speed = speed;
        }

    }

    [Serializable]
    public class EnumParameter
    {
        public EnumOvenBoxNum Num { get; set; }

        public int code { get; set; }

        public bool OvenBoxInRemind { get; set; }

        public EnumParameter()
        {


        }
        public EnumParameter(EnumOvenBoxNum Num, int code, bool OvenBoxInRemind = false)
        {
            this.Num = Num;
            this.code = code;
            this.OvenBoxInRemind = OvenBoxInRemind;
        }

    }

    [Serializable]
    public class PutdownPosition
    {
        public XYZTCoordinateConfig XYZT { get; set; }

        public float UpPostion { get; set; }
        public int code { get; set; }

        public PutdownPosition()
        {


        }
        public PutdownPosition(XYZTCoordinateConfig XYZT, float UpPostion, int code)
        {
            this.XYZT = XYZT;
            this.UpPostion = UpPostion;
            this.code = code;
        }

    }

    [Serializable]
    public class ProcessTargetPositionParam
    {
        public XYZTCoordinateConfig XYZT { get; set; }

        public float UpPostion { get; set; }
        public int code { get; set; }

        public EnumMaterialHooktargetNum target { get; set; }

        public int PostionNum { get; set; }

        public int rowNum { get; set; }

        public int columnNum { get; set; }

        public ProcessTargetPositionParam()
        {


        }
        public ProcessTargetPositionParam(XYZTCoordinateConfig XYZT, float UpPostion, int code)
        {
            this.XYZT = XYZT;
            this.UpPostion = UpPostion;
            this.code = code;
            this.PostionNum = PostionNum;
            this.target = EnumMaterialHooktargetNum.MembraneEquipment;
        }
        public ProcessTargetPositionParam(XYZTCoordinateConfig XYZT, float UpPostion, int code, int PostionNum)
        {
            this.XYZT = XYZT;
            this.UpPostion = UpPostion;
            this.code = code;
            this.PostionNum = PostionNum;
            this.target = EnumMaterialHooktargetNum.MembraneEquipment;
        }
        public ProcessTargetPositionParam(XYZTCoordinateConfig XYZT, float UpPostion, int code, int rowNum, int columnNum)
        {
            this.XYZT = XYZT;
            this.UpPostion = UpPostion;
            this.code = code;
            this.rowNum = rowNum;
            this.columnNum = columnNum;
            this.target = EnumMaterialHooktargetNum.Material;
        }

    }

    [Serializable]
    public class ProcessTrainsportMaterialboxParam
    {
        public EnumTrainsportMaterialboxParam param { get; set; }
        public int code { get; set; }

        public ProcessTrainsportMaterialboxParam()
        {


        }
        public ProcessTrainsportMaterialboxParam(EnumTrainsportMaterialboxParam param, int code)
        {
            this.param = param;
            this.code = code;
        }

    }


    [Serializable]
    public class ESToolSettings
    {
        public ESToolSettings()
        {
            NeedleCenter = new XYZTCoordinateConfig();
        }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("NeedleName")]
        public string NeedleName { get; set; }
        [XmlElement("ESBasePosition")]
        public float ESBasePosition { get; set; }

        [XmlElement("NeedleZeorPosition")]
        public float NeedleZeorPosition { get; set; }
        [XmlElement("NeedleCenter")]
        public XYZTCoordinateConfig NeedleCenter { get; set; }

    }
    [Serializable]
    public class PPToolSettings
    {
        public PPToolSettings()
        {
            ChipPPPosCompensateCoordinate1 = new XYZTCoordinateConfig();
            ChipPPPosCompensateCoordinate2 = new XYZTCoordinateConfig();
            PPESAltimetryParameter = new PPESAltimetryParameters();

        }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("PPName")]
        public string PPName { get; set; }
        /// <summary>
        /// 在Mark处的测高值
        /// </summary>
        [XmlElement("AltimetryOnMark")]
        public float AltimetryOnMark { get; set; }

        /// <summary>
        /// 计算吸嘴旋转补偿XY公式的坐标1，一般在T0度纪录
        /// </summary>
        [XmlElement("PPosCompensateCoordinate1")]
        public XYZTCoordinateConfig ChipPPPosCompensateCoordinate1 { get; set; }
        /// <summary>
        /// 计算吸嘴旋转补偿XY公式的坐标2，一般在T180度纪录
        /// </summary>
        [XmlElement("PPosCompensateCoordinate2")]
        public XYZTCoordinateConfig ChipPPPosCompensateCoordinate2 { get; set; }
        /// <summary>
        /// 吸嘴和ES测高时的参数
        /// </summary>
        [XmlElement("PPESAltimetryParameter")]
        public PPESAltimetryParameters PPESAltimetryParameter { get; set; }
    }
    [Serializable]
    public class PPESAltimetryParameters
    {
        public PPESAltimetryParameters()
        {
        }
        [XmlElement("PPName")]
        public string PPName { get; set; }

        [XmlElement("PPPosition")]
        public float PPPosition { get; set; }
        [XmlElement("ESPosition")]
        public float ESPosition { get; set; }
    }
    [Serializable]
    public class BaseCoordinateConfig
    {
        [XmlAttribute("X"), DataMember(Order = 1)]
        public double X { get; set; }

        [XmlAttribute("Y"), DataMember(Order = 2)]
        public double Y { get; set; }
    }

    [Serializable]
    public class XYThetaCoordinateConfig : BaseCoordinateConfig
    {
        [XmlAttribute("Theta")]
        public float Theta { get; set; }
    }

    [Serializable]
    public class XYZCoordinateConfig : BaseCoordinateConfig
    {
        [XmlAttribute("Z")]
        public double Z { get; set; }
    }

    [Serializable]
    public class XYZTCoordinateConfig : BaseCoordinateConfig
    {
        [XmlAttribute("Z")]
        public double Z { get; set; }

        [XmlAttribute("Theta")]
        public double Theta { get; set; }
    }

    [Serializable]
    public class XYZTOffsetConfig : BaseCoordinateConfig
    {
        [XmlAttribute("Z")]
        public double Z { get; set; }

        [XmlAttribute("Theta")]
        public double Theta { get; set; }
    }

    [Serializable]
    public class MatchIdentificationParam
    {
        public MatchIdentificationParam()
        {
            //Templatexml = "";
            //Runxml = "";
            TemplateRoi = new RectangleFV();
            SearchRoi = new RectangleFV();
            MaskSetting = new List<RecogniseMaskSetting>();
            BondTablePositionOfCreatePattern = new XYZTCoordinateConfig();
            PositionOfMaterialCenter = new XYZTCoordinateConfig();
            WaferTablePositionOfCreatePattern = new XYZTCoordinateConfig();
            //Templateresult = new MatchTemplateResult();
        }

        [XmlElement("RingLightintensity")]
        public int RingLightintensity { get; set; }
        [XmlElement("DirectLightintensity")]
        public int DirectLightintensity { get; set; }
        [XmlElement("Templatexml")]
        public string Templatexml { get; set; }
        [XmlElement("TemplateParamxml")]
        public string TemplateParamxml { get; set; }
        [XmlElement("Runxml")]
        public string Runxml { get; set; }

        [XmlElement("Score")]
        public float Score { get; set; }

        [XmlElement("MinAngle")]
        public int MinAngle { get; set; }

        [XmlElement("MaxAngle")]
        public int MaxAngle { get; set; }

        [XmlElement("TemplateRoi")]
        public RectangleFV TemplateRoi { get; set; }

        [XmlElement("SearchRoi")]
        public RectangleFV SearchRoi { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }


        [XmlElement("CameraZWorkPosition")]
        public float CameraZWorkPosition { get; set; }
        [XmlElement("OrigionAngle")]
        public float OrigionAngle { get; set; }
        /// <summary>
        /// 创建模板时BondTable的位置
        /// </summary>
        [XmlElement("BondTablePositionOfCreatePattern")]
        public XYZTCoordinateConfig BondTablePositionOfCreatePattern { get; set; }
        /// <summary>
        /// 创建模板时物料中心的系统坐标位
        /// </summary>
        [XmlIgnore]
        public XYZTCoordinateConfig PositionOfMaterialCenter { get; set; }
        /// <summary>
        /// 创建模板时WaferTable的位置
        /// </summary>
        [XmlElement("WaferTablePositionOfCreatePattern")]
        public XYZTCoordinateConfig WaferTablePositionOfCreatePattern { get; set; }

        [XmlArray("MaskSetting"), XmlArrayItem(typeof(RecogniseMaskSetting))]
        public List<RecogniseMaskSetting> MaskSetting { get; set; }
        //[XmlElement("Templateresult")]
        //public MatchTemplateResult Templateresult { get; set; }

        /// <summary>
        /// 物料行数
        /// </summary>
        [XmlElement("MaterialRow")]
        public int MaterialRow { get; set; } = 0;
        /// <summary>
        /// 物料列数
        /// </summary>
        [XmlElement("MaterialCol")]
        public int MaterialCol { get; set; } = 0;

    }

    [Serializable]
    public class LineFindIdentificationParam
    {
        public LineFindIdentificationParam()
        {
            //UpEdgefilepath = "";
            //DownEdgefilepath = "";
            //LeftEdgefilepath = "";
            //RightEdgefilepath = "";
            UpEdgeRoi = new RectangleFV();
            DownEdgeRoi = new RectangleFV();
            LeftEdgeRoi = new RectangleFV();
            RightEdgeRoi = new RectangleFV();
            MaskSetting = new List<RecogniseMaskSetting>();
            BondTablePosition = new XYZTCoordinateConfig();
            WaferTablePosition = new XYZTCoordinateConfig();
        }
        [XmlElement("RingLightintensity")]
        public int RingLightintensity { get; set; }
        [XmlElement("DirectLightintensity")]
        public int DirectLightintensity { get; set; }

        [XmlElement("UpEdgefilepath")]
        public string UpEdgefilepath { get; set; }
        [XmlElement("DownEdgefilepath")]
        public string DownEdgefilepath { get; set; }
        [XmlElement("LeftEdgefilepath")]
        public string LeftEdgefilepath { get; set; }
        [XmlElement("RightEdgefilepath")]
        public string RightEdgefilepath { get; set; }


        [XmlElement("UpEdgeScore")]
        public int UpEdgeScore { get; set; }
        [XmlElement("DownEdgeScore")]
        public int DownEdgeScore { get; set; }
        [XmlElement("LeftEdgeScore")]
        public int LeftEdgeScore { get; set; }
        [XmlElement("RightEdgeScore")]
        public int RightEdgeScore { get; set; }


        [XmlElement("UpEdgeRoi")]
        public RectangleFV UpEdgeRoi { get; set; }
        [XmlElement("DownEdgeRoi")]
        public RectangleFV DownEdgeRoi { get; set; }
        [XmlElement("LeftEdgeRoi")]
        public RectangleFV LeftEdgeRoi { get; set; }

        [XmlElement("RightEdgeRoi")]
        public RectangleFV RightEdgeRoi { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        //[XmlElement("UsedCamera")]
        //public EnumCameraType UsedCamera { get; set; }
        [XmlElement("BondTablePosition")]
        public XYZTCoordinateConfig BondTablePosition { get; set; }
        [XmlElement("WaferTablePosition")]
        public XYZTCoordinateConfig WaferTablePosition { get; set; }
        [XmlElement("CameraZWorkPosition")]
        public float CameraZWorkPosition { get; set; }
        [XmlElement("OrigionAngle")]
        public float OrigionAngle { get; set; }
        [XmlArray("MaskSetting"), XmlArrayItem(typeof(RecogniseMaskSetting))]
        public List<RecogniseMaskSetting> MaskSetting { get; set; }
    }

    [Serializable]
    public class CircleFindIdentificationParam
    {
        public CircleFindIdentificationParam()
        {
            //CircleFindTemplatefilepath = "";
            TemplateRoiCenter = new PointF();
            SearchRoi = new RectangleFV();
            MaskSetting = new List<RecogniseMaskSetting>();
            BondTablePosition = new XYZTCoordinateConfig();
            WaferTablePosition = new XYZTCoordinateConfig();
        }
        [XmlElement("RingLightintensity")]
        public int RingLightintensity { get; set; }
        [XmlElement("DirectLightintensity")]
        public int DirectLightintensity { get; set; }
        [XmlElement("CircleFindTemplatefilepath")]
        public string CircleFindTemplatefilepath { get; set; }

        [XmlElement("Score")]
        public int Score { get; set; }

        [XmlElement("MinR")]
        public int MinR { get; set; }

        [XmlElement("MaxR")]
        public int MaxR { get; set; }

        [XmlElement("TemplateRoiCenter")]
        public PointF TemplateRoiCenter { get; set; }
        [XmlElement("TemplateRoiR")]
        public float TemplateRoiR { get; set; }

        [XmlElement("SearchRoi")]
        public RectangleFV SearchRoi { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        //[XmlElement("UsedCamera")]
        //public EnumCameraType UsedCamera { get; set; }
        [XmlElement("BondTablePosition")]
        public XYZTCoordinateConfig BondTablePosition { get; set; }
        [XmlElement("WaferTablePosition")]
        public XYZTCoordinateConfig WaferTablePosition { get; set; }
        [XmlElement("CameraZWorkPosition")]
        public float CameraZWorkPosition { get; set; }
        [XmlElement("OrigionAngle")]
        public float OrigionAngle { get; set; }
        [XmlArray("MaskSetting"), XmlArrayItem(typeof(RecogniseMaskSetting))]
        public List<RecogniseMaskSetting> MaskSetting { get; set; }
    }

    [Serializable]
    public class EnumOvenBoxStates
    {
        #region 烘箱

        /// <summary>
        /// 烘箱抽气阀状态 bool
        /// </summary>
        public bool BakeOvenBleedstatus { get; set; }
        /// <summary>
        /// 烘箱排气阀状态 bool
        /// </summary>
        public bool BakeOvenExhauststatus { get; set; }
        /// <summary>
        /// 烘箱补气阀状态 bool
        /// </summary>
        public bool BakeOvenAeratestatus { get; set; }
        /// <summary>
        /// 烘箱内门压紧状态 bool
        /// </summary>
        public bool BakeOvenInnerdoor1Pressstatus { get; set; }
        /// <summary>
        /// 烘箱外门压紧状态 bool
        /// </summary>
        public bool BakeOvenOuterdoor1Pressstatus { get; set; }
        /// <summary>
        /// 烘箱内门松开状态 bool
        /// </summary>
        public bool BakeOvenInnerdoor1Releasestatus { get; set; }
        /// <summary>
        /// 烘箱外门松开状态 bool
        /// </summary>
        public bool BakeOvenOuterdoor1Releasestatus { get; set; }
        /// <summary>
        /// 烘箱内门升状态 bool
        /// </summary>
        public bool BakeOvenInnerdoor2Upstatus { get; set; }
        /// <summary>
        /// 烘箱外门升状态 bool
        /// </summary>
        public bool BakeOvenOuterdoor2Upstatus { get; set; }
        /// <summary>
        /// 烘箱内门降状态 bool
        /// </summary>
        public bool BakeOvenInnerdoor2Downstatus { get; set; }
        /// <summary>
        /// 烘箱外门降状态 bool
        /// </summary>
        public bool BakeOvenOuterdoor2Downstatus { get; set; }




        /// <summary>
        /// 烘箱外门开到位
        /// </summary>
        public bool BakeOvenOuterdoorUpSta { get; set; }
        /// <summary>
        /// 烘箱外门关到位
        /// </summary>
        public bool BakeOvenOuterdoorDownSta { get; set; }
        /// <summary>
        /// 烘箱外门压紧到位
        /// </summary>
        public bool BakeOvenOuterdoorPressSta { get; set; }
        /// <summary>
        /// 烘箱外门松开到位
        /// </summary>
        public bool BakeOvenOuterdoorReleaseSta { get; set; }
        /// <summary>
        /// 烘箱内门开到位
        /// </summary>
        public bool BakeOvenInnerdoorUpSta { get; set; }
        /// <summary>
        /// 烘箱内门关到位
        /// </summary>
        public bool BakeOvenInnerdoorDownSta { get; set; }
        /// <summary>
        /// 烘箱内门压紧到位
        /// </summary>
        public bool BakeOvenInnerdoorPressSta { get; set; }
        /// <summary>
        /// 烘箱内门松开到位
        /// </summary>
        public bool BakeOvenInnerdoorReleaseSta { get; set; }


        /// <summary>
        /// 烘箱压力 float
        /// </summary>
        public float BakeOvenPressure { get; set; }
        /// <summary>
        /// 烘箱真空 float 
        /// </summary>
        public float BakeOvenVacuum { get; set; }
        /// <summary>
        /// 烘箱上板温度 float 
        /// </summary>
        public float BakeOvenUPtemp { get; set; }
        /// <summary>
        /// 烘箱下板温度 float 
        /// </summary>
        public float BakeOvenDowntemp { get; set; }
        /// <summary>
        /// 烘箱加热目标温度 float 
        /// </summary>
        public float BakeOvenTargettemp { get; set; }
        /// <summary>
        /// 烘箱加热报警温度 float 
        /// </summary>
        public float BakeOvenAlarmtemp { get; set; }
        /// <summary>
        /// 烘箱保温时间分钟 short 分钟
        /// </summary>
        public short BakeOvenHoldingTimeM { get; set; }
        /// <summary>
        /// 烘箱已保温时间分钟 short 分钟
        /// </summary>
        public short BakeOvenPassedTimeM { get; set; }
        /// <summary>
        /// 烘箱报警压力 float
        /// </summary>
        public float BakeOvenAlarmPressure { get; set; }
        /// <summary>
        /// 烘箱上板P
        /// </summary>
        public float BakeOvenUpHeatPID_P { get; set; }
        /// <summary>
        /// 烘箱上板I
        /// </summary>
        public float BakeOvenUpHeatPID_I { get; set; }
        /// <summary>
        /// 烘箱上板D
        /// </summary>
        public float BakeOvenUpHeatPID_D { get; set; }
        /// <summary>
        /// 烘箱上板P
        /// </summary>
        public float BakeOvenDownHeatPID_P { get; set; }
        /// <summary>
        /// 烘箱上板I
        /// </summary>
        public float BakeOvenDownHeatPID_I { get; set; }
        /// <summary>
        /// 烘箱上板D
        /// </summary>
        public float BakeOvenDownHeatPID_D { get; set; }


        /// <summary>
        /// 烘箱抽充压力上限 float 
        /// </summary>
        public float BakeOvenPFUpPressure { get; set; }
        /// <summary>
        /// 烘箱抽充压力下限 float 
        /// </summary>
        public float BakeOvenPFDownPressure { get; set; }
        /// <summary>
        /// 烘箱抽充次数 short 
        /// </summary>
        public float BakeOvenPFnum { get; set; }
        /// <summary>
        /// 烘箱抽充已完成次数 short 
        /// </summary>
        public float BakeOvenPFCompletednum { get; set; }
        /// <summary>
        /// 烘箱抽充时间间隔 short 秒
        /// </summary>
        public float BakeOvenPFinterval { get; set; }


        #endregion

    }

    [Serializable]
    public class EnumBoxStates
    {

        #region 箱体

        /// <summary>
        /// 箱体抽气阀状态 bool
        /// </summary>
        public bool BoxBleedstatus { get; set; }
        /// <summary>
        /// 箱体排气阀状态 short
        /// </summary>
        public bool BoxExhauststatus { get; set; }
        /// <summary>
        /// 箱体补气阀状态 short
        /// </summary>
        public bool BoxAeratestatus { get; set; }
        /// 箱体外门电缸压紧状态 bool
        /// </summary>
        public bool BoxOuterdoorElePressstatus { get; set; }
        /// <summary>
        /// 箱体外门电缸打开状态 bool
        /// </summary>
        public bool BoxOuterdoorEleReleasestatus { get; set; }


        /// <summary>
        /// 箱体外门压紧到位
        /// </summary>
        public bool BoxOuterdoorPressSta { get; set; }
        /// <summary>
        /// 箱体外门松开到位
        /// </summary>
        public bool BoxOuterdoorReleaseSta { get; set; }
        /// <summary>
        /// 箱体外门关闭到位
        /// </summary>
        public bool BoxOuterdoorCloseSta { get; set; }
        /// <summary>
        /// 箱体外门打开到位
        /// </summary>
        public bool BoxOuterdoorOpenSta { get; set; }


        /// <summary>
        /// 箱体压力 float 
        /// </summary>
        public bool BoxPressure { get; set; }
        /// <summary>
        /// 箱体真空 float 
        /// </summary>
        public bool BoxVacuum { get; set; }
        /// <summary>
        /// 箱体抽充压力上限 float 
        /// </summary>
        public bool BoxPFUpPressure { get; set; }
        /// <summary>
        /// 箱体抽充压力下限 float 
        /// </summary>
        public bool BoxPFDownPressure { get; set; }
        /// <summary>
        /// 箱体抽充次数 short 
        /// </summary>
        public bool BoxPFnum { get; set; }
        /// <summary>
        /// 箱体抽充已完成次数 short 
        /// </summary>
        public bool BoxPFCompletednum { get; set; }
        /// <summary>
        /// 箱体抽充时间间隔 short 秒
        /// </summary>
        public bool BoxPFinterval { get; set; }
        /// <summary>
        /// 箱体报警压力 float
        /// </summary>
        public bool BoxAlarmPressure { get; set; }



        #endregion



    }





    [Serializable]
    public enum EnumBoardcardDefineOutputIO
    {
        Undefine = -1,

        /// <summary>
        /// 塔灯 黄灯
        /// </summary>
        TowerYellowLight = 26,
        /// <summary>
        /// 塔灯 绿灯
        /// </summary>
        TowerGreenLight = 27,
        /// <summary>
        /// 塔灯 红灯
        /// </summary>
        TowerRedLight = 25,

        #region 烘箱1

        /// <summary>
        /// 烘箱补气阀 bool
        /// </summary>
        BakeOvenAerate = 22,

        /// <summary>
        /// 烘箱粗抽阀 bool
        /// </summary>
        BakeOvenCoarseExtractionValve = 8,
        /// <summary>
        /// 烘箱前级阀 bool
        /// </summary>
        BakeOvenFrontStageValve = 9,
        /// <summary>
        /// 烘箱插板阀 bool
        /// </summary>
        BakeOvenPlugInValve = 19,
        /// <summary>
        /// 烘箱机械泵 bool
        /// </summary>
        BakeOvenMechanicalPump = 12,


        /// <summary>
        /// 烘箱内门升降 bool 按1松0
        /// </summary>
        BakeOvenInnerdoorUpDown = 16,
        

        #endregion

        #region 烘箱2

        /// <summary>
        /// 烘箱补气阀 bool
        /// </summary>
        BakeOven2Aerate = 23,

        /// <summary>
        /// 烘箱2粗抽阀 bool
        /// </summary>
        BakeOven2CoarseExtractionValve = 10,
        /// <summary>
        /// 烘箱2前级阀 bool
        /// </summary>
        BakeOven2FrontStageValve = 11,
        /// <summary>
        /// 烘箱2插板阀 bool
        /// </summary>
        BakeOven2PlugInValve = 20,
        /// <summary>
        /// 烘箱2机械泵 bool
        /// </summary>
        BakeOven2MechanicalPump = 13,

        /// <summary>
        /// 烘箱内门升 bool 按1松0
        /// </summary>
        BakeOven2InnerdoorUpDown = 17,

        #endregion


        #region 箱体

        /// <summary>
        /// 箱体补气阀 short
        /// </summary>
        BoxAerate = 24,

        /// <summary>
        /// 方舱粗抽阀 bool
        /// </summary>
        BoxCoarseExtractionValve = 2,
        /// <summary>
        /// 方舱前级阀 bool
        /// </summary>
        BoxFrontStageValve = 3,
        /// <summary>
        /// 方舱插板阀 bool
        /// </summary>
        BoxPlugInValve = 21,
        /// <summary>
        /// 方舱机械泵 bool
        /// </summary>
        BoxMechanicalPump = 14,


        /// <summary>
        /// 压机压合分离 bool
        /// </summary>
        PressPressingDivide = 18,

        /// <summary>
        /// 压缩机启动 bool
        /// </summary>
        CompressorStartup = 4,

        /// <summary>
        /// 压缩机停止 bool
        /// </summary>
        CompressorStops = 5,

        /// <summary>
        /// 冷凝泵 bool
        /// </summary>
        CondenserPump = 6,

        /// <summary>
        /// 还原IN bool
        /// </summary>
        ReductionIN = 29,
        /// <summary>
        /// 还原OUT bool
        /// </summary>
        ReductionOUT = 28,
        /// <summary>
        /// 冷凝泵加热 bool
        /// </summary>
        CondenserPumpHeat = 15,


        #endregion

        #region 电机

        /// <summary>
        /// 电机抱闸1
        /// </summary>
        MotorBrake = 0,
        /// <summary>
        /// 电机抱闸2
        /// </summary>
        MotorBrake1 = 1,

        #endregion


    }
    [Serializable]
    public enum EnumBoardcardDefineInputIO
    {
        #region 烘箱

        /// <summary>
        /// 烘箱插板阀打开状态 bool
        /// </summary>
        BakeOvenPlugInValveOpenstatus = 2,
        /// <summary>
        /// 烘箱插板阀关闭状态 bool
        /// </summary>
        BakeOvenPlugInValveClosestatus = 3,

        /// <summary>
        /// 烘箱内门打开状态 bool
        /// </summary>
        BakeOvenInnerdoorOpenstatus = 9,
        /// <summary>
        /// 烘箱内门关闭状态 bool
        /// </summary>
        BakeOvenInnerdoorClosestatus = 8,
        /// <summary>
        /// 烘箱外门关闭状态 bool
        /// </summary>
        BakeOvenOuterdoorClosestatus = 12,

        /// <summary>
        /// 烘箱1压力传感器到达常压
        /// </summary>
        BakeOvenPressureSensor = 22,

        #endregion

        #region 烘箱2


        /// <summary>
        /// 烘箱2插板阀打开状态 bool
        /// </summary>
        BakeOven2PlugInValveOpenstatus = 4,
        /// <summary>
        /// 烘箱2插板阀关闭状态 bool
        /// </summary>
        BakeOven2PlugInValveClosestatus = 5,

        /// <summary>
        /// 烘箱内门打开状态 bool
        /// </summary>
        BakeOven2InnerdoorOpenstatus = 11,
        /// <summary>
        /// 烘箱内门关闭状态 bool
        /// </summary>
        BakeOven2InnerdoorClosestatus = 10,
        /// <summary>
        /// 烘箱外门关闭状态 bool
        /// </summary>
        BakeOven2OuterdoorClosestatus = 13,

        /// <summary>
        /// 烘箱2压力传感器到达常压
        /// </summary>
        BakeOven2PressureSensor = 21,

        #endregion


        #region 箱体

        /// <summary>
        /// 主腔体插板阀打开状态 bool
        /// </summary>
        BoxPlugInValveOpenstatus = 6,
        /// <summary>
        /// 主腔体插板阀关闭状态 bool
        /// </summary>
        BoxPlugInValveClosestatus = 7,

        /// <summary>
        /// 箱体外门关闭到位
        /// </summary>
        BoxOuterdoorClosetatus = 14,

        /// <summary>
        /// 压缩机报警
        /// </summary>
        CompressorAlarm = 18,

        /// <summary>
        /// 冷凝泵启动
        /// </summary>
        CondenserStar = 17,


        /// <summary>
        /// 压机压合到位 bool
        /// </summary>
        PressIsPress = 1,
        /// <summary>
        /// 压机分开到位 bool
        /// </summary>
        PressIsDivide = 0,


        /// <summary>
        /// 冷凝泵到达10pa
        /// </summary>
        CondenserPumpSignal1 = 20,
        /// <summary>
        /// 冷凝泵温度到达20K
        /// </summary>
        CondenserPumpSignal2 = 19,
        /// <summary>
        /// 冷凝泵温度到达室温
        /// </summary>
        CondenserPumpSignal3 = 24,
        /// <summary>
        /// 皮拉尼达到40Pa
        /// </summary>
        CondenserPumpSignal4 = 26,
        /// <summary>
        /// 皮拉尼达到50Pa
        /// </summary>
        CondenserPumpSignal5 = 27,
        /// <summary>
        /// 冷凝泵到大气
        /// </summary>
        CondenserPumpSignal6 = 28,
        /// <summary>
        /// 热继电器
        /// </summary>
        ThermalRelay = 25,

        /// <summary>
        /// 主腔体压力传感器到达常压
        /// </summary>
        BoxPressureSensor = 23,

        #endregion


    }


    public enum EnumProductStepType
    {
        [Description("无")]
        None,

        [Description("传输")]
        Translate,

        [Description("共晶")]
        Eutectic
    }

    public enum EnumTransSrc
    {
        [Description("无")]
        None,

        [Description("物料")]
        Component,

        [Description("校准平台")]
        CalibrationTable,

        [Description("共晶平台")]
        EutecticTable
    }

    public enum EnumTransDest
    {
        [Description("无")]
        None,

        [Description("校准平台")]
        CalibrationTable,

        [Description("共晶平台")]
        EutecticTable,

        [Description("成品区")]
        FinishedProduct
    }


    [Serializable]
    public class PowerParam
    {
        [XmlAttribute("T1")]
        public int T1 { get; set; }
        [XmlAttribute("T2")]
        public int T2 { get; set; }
        [XmlAttribute("T3")]
        public int T3 { get; set; }
        [XmlAttribute("T4")]
        public int T4 { get; set; }
        [XmlAttribute("T5")]
        public int T5 { get; set; }
        [XmlAttribute("t1")]
        public int t1 { get; set; }
        [XmlAttribute("t2")]
        public int t2 { get; set; }
        [XmlAttribute("t3")]
        public int t3 { get; set; }
        [XmlAttribute("t4")]
        public int t4 { get; set; }
        [XmlAttribute("t5")]
        public int t5 { get; set; }
        [XmlAttribute("t6")]
        public int t6 { get; set; }

        [XmlAttribute("H1")]
        public int H1 { get; set; }
        [XmlAttribute("H2")]
        public int H2 { get; set; }
        [XmlAttribute("H3")]
        public int H3 { get; set; }
        [XmlAttribute("H4")]
        public int H4 { get; set; }

        [XmlAttribute("L1")]
        public int L1 { get; set; }
        [XmlAttribute("L2")]
        public int L2 { get; set; }
        [XmlAttribute("L3")]
        public int L3 { get; set; }
        [XmlAttribute("L4")]
        public int L4 { get; set; }



        [XmlAttribute("G1")]
        public int G1 { get; set; }
        [XmlAttribute("G2")]
        public int G2 { get; set; }
        [XmlAttribute("G3")]
        public int G3 { get; set; }
        [XmlAttribute("G4")]
        public int G4 { get; set; }



        [XmlAttribute("t0")]
        public int t0 { get; set; }

        [XmlAttribute("T0")]
        public int T0 { get; set; }



        [XmlAttribute("MH")]
        public int MH { get; set; }
        [XmlAttribute("ML")]
        public int ML { get; set; }

        [XmlAttribute("HD")]
        public int HD { get; set; }

        [XmlAttribute("CNTLLimit")]
        public int CNTLLimit { get; set; }

        [XmlAttribute("MP")]
        public int MP { get; set; }

        [XmlAttribute("TC")]
        public int TC { get; set; }

        [XmlAttribute("IOUT")]
        public int IOUT { get; set; }

        [XmlAttribute("OPMD")]
        public int OPMD { get; set; }

        [XmlAttribute("DM")]
        public int DM { get; set; }

        [XmlAttribute("GP")]
        public int GP { get; set; }

        [XmlAttribute("DN")]
        public int DN { get; set; }

        [XmlAttribute("ERRC")]
        public int ERRC { get; set; }


        [XmlAttribute("CNTH")]
        public int CNTH { get; set; }
        [XmlAttribute("CNTL")]
        public int CNTL { get; set; }

        [XmlAttribute("TM1")]
        public int TM1 { get; set; }
        [XmlAttribute("TM2")]
        public int TM2 { get; set; }
        [XmlAttribute("TM3")]
        public int TM3 { get; set; }
        [XmlAttribute("TM4")]
        public int TM4 { get; set; }
        [XmlAttribute("TMC")]
        public int TMC { get; set; }
        [XmlAttribute("DataTime")]
        public int DataTime { get; set; }
        [XmlAttribute("tcys")]
        public int tcys { get; set; }
        [XmlAttribute("IM1")]
        public int IM1 { get; set; }
        [XmlAttribute("IM2")]
        public int IM2 { get; set; }
        [XmlAttribute("IM3")]
        public int IM3 { get; set; }
        [XmlAttribute("IM4")]
        public int IM4 { get; set; }
        [XmlAttribute("UM1")]
        public int UM1 { get; set; }
        [XmlAttribute("UM2")]
        public int UM2 { get; set; }
        [XmlAttribute("UM3")]
        public int UM3 { get; set; }
        [XmlAttribute("UM4")]
        public int UM4 { get; set; }

        [XmlAttribute("TData1")]
        public int TData1 { get; set; }



    }

    [Serializable]
    public class RectangleFV
    {
        [XmlAttribute("X")]
        public float X { get; set; }
        [XmlAttribute("Y")]
        public float Y { get; set; }
        [XmlAttribute("Width")]
        public float Width { get; set; }
        [XmlAttribute("Height")]
        public float Height { get; set; }

    }

    [Serializable]
    public class TurboMolecularPumpstatus
    {
        /// <summary>
        /// 输出频率
        /// </summary>
        public float OutputFrequency { get; set; }
        /// <summary>
        /// 输出电压
        /// </summary>
        public float OutputVoltage { get; set; }
        /// <summary>
        /// 输出电流
        /// </summary>
        public float OutputCurrent { get; set; }

        /// <summary>
        /// 待机
        /// </summary>
        public bool Standbymode { get; set; }
        /// <summary>
        /// 运行
        /// </summary>
        public bool Function { get; set; }
        /// <summary>
        /// 操作错误
        /// </summary>
        public bool err { get; set; }
        /// <summary>
        /// 短路保护
        /// </summary>
        public bool OC { get; set; }
        /// <summary>
        /// 直流过压
        /// </summary>
        public bool OE { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public bool Retain { get; set; }
        /// <summary>
        /// RLU
        /// </summary>
        public bool RLU { get; set; }
        /// <summary>
        /// 过流
        /// </summary>
        public bool OL2 { get; set; }
        /// <summary>
        /// 分子泵过载
        /// </summary>
        public bool SL { get; set; }
        /// <summary>
        /// 分子泵过热
        /// </summary>
        public bool ESP { get; set; }
        /// <summary>
        /// 欠压
        /// </summary>
        public bool LU { get; set; }
        /// <summary>
        /// 分子泵驱动控制器过热
        /// </summary>
        public bool OH { get; set; }



    }



    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PPWorkParameters
    {
        /// <summary>
        /// 使用的吸嘴
        /// </summary>
        [XmlElement("UsedPP")]
        public EnumUsedPP UsedPP;
        /// <summary>
        /// 工作高度，不包括压力偏移和其他偏移
        /// </summary>
        [XmlElement("WorkHeight")]
        public float WorkHeight { get; set; }
        /// <summary>
        /// 压力偏移
        /// </summary>

        [XmlElement("PickupStress")]
        public float PickupStress { get; set; }
        /// <summary>
        /// 是否需要顶针
        /// </summary>
        [XmlElement("IsUseNeedle")]
        public bool IsUseNeedle { get; set; }
        /// <summary>
        /// 顶针上升高度
        /// </summary>
        [XmlElement("NeedleUpHeight")]
        public float NeedleUpHeight { get; set; }
        /// <summary>
        /// 顶针上升速度
        /// </summary>
        [XmlElement("NeedleSpeed")]
        public float NeedleSpeed { get; set; }

        //[XmlElement("PickedDelayMS")]
        //public float DelayMSAfterPicked { get; set; }
        /// <summary>
        /// 开真空后等待的延时，延时过后才开始上升
        /// </summary>

        [XmlElement("DelayMSForVaccum")]
        public float DelayMSForVaccum { get; set; }
        /// <summary>
        /// 拾取前慢速移动的距离
        /// </summary>

        [XmlElement("SlowTravelBeforePickupMM")]
        public float SlowTravelBeforePickupMM { get; set; }
        /// <summary>
        /// 拾取前慢速移动的速度
        /// </summary>

        [XmlElement("SlowSpeedBeforePickup")]
        public float SlowSpeedBeforePickup { get; set; }
        /// <summary>
        /// 拾取后慢速移动的距离
        /// </summary>

        [XmlElement("SlowTravelAfterPickupMM")]
        public float SlowTravelAfterPickupMM { get; set; }
        /// <summary>
        /// 拾取前慢速移动的速度
        /// </summary>

        [XmlElement("SlowSpeedAfterPickup")]
        public float SlowSpeedAfterPickup { get; set; }
        /// <summary>
        /// 拾取后上升的高度
        /// </summary>

        [XmlElement("UpDistanceMMAfterPicked")]
        public float UpDistanceMMAfterPicked { get; set; }
        public PPWorkParameters()
        {

        }
    }



    public class ScorePoint
    {
        public PointF Point;
        public int Score;
        public ScorePoint()
        {
            Point = new PointF();
        }

        public ScorePoint(PointF point, int Score)
        {
            this.Point = point;
            this.Score = Score;
        }
    }

    public class RectangleFA
    {
        public PointF Benchmark;
        public PointF Center;
        public float Width;
        public float Height;
        public float Angle;
        public PointF LeftTop;
        public PointF LeftBottom;
        public PointF RightTop;
        public PointF RightBottom;

        public RectangleFA()
        {

        }

        public RectangleFA(PointF Benchmark, PointF Center, float Width, float Height, float Angle)
        {
            this.Benchmark = Benchmark;
            this.Center = Center;
            this.Width = Width;
            this.Height = Height;
            this.Angle = Angle;

            double angleRadians = Angle * Math.PI / 180.0;

            float halfWidth = Width / 2;
            float halfHeight = Height / 2;

            PointF[] points = new PointF[4];
            points[0] = new PointF(Center.X - halfWidth, Center.Y - halfHeight); // 左上角
            points[1] = new PointF(Center.X + halfWidth, Center.Y - halfHeight); // 右上角
            points[2] = new PointF(Center.X + halfWidth, Center.Y + halfHeight); // 右下角
            points[3] = new PointF(Center.X - halfWidth, Center.Y + halfHeight); // 左下角

            PointF[] rotatedPoints = new PointF[4];
            for (int i = 0; i < 4; i++)
            {
                rotatedPoints[i] = new PointF(
                    Center.X + (float)((points[i].X - Center.X) * Math.Cos(angleRadians) - (points[i].Y - Center.Y) * Math.Sin(angleRadians)),
                    Center.Y + (float)((points[i].X - Center.X) * Math.Sin(angleRadians) + (points[i].Y - Center.Y) * Math.Cos(angleRadians)));
            }

            this.LeftTop = rotatedPoints[0];
            this.LeftBottom = rotatedPoints[3];
            this.RightTop = rotatedPoints[1];
            this.RightBottom = rotatedPoints[2];

        }

        public RectangleFA(PointF LeftTop, PointF LeftBottom, PointF RightTop, PointF RightBottom)
        {
            this.LeftTop = LeftTop;
            this.LeftBottom = LeftBottom;
            this.RightTop = RightTop;
            this.RightBottom = RightBottom;

            this.Center = new PointF((LeftTop.X + RightBottom.X + RightTop.X + LeftBottom.X) / 4, (LeftTop.Y + RightBottom.Y + RightTop.Y + LeftBottom.Y) / 4);

            float width1 = Distance(LeftTop, RightTop);
            float width2 = Distance(LeftBottom, RightBottom);
            this.Width = (width1 + width2) / 2;

            float height1 = Distance(LeftTop, LeftBottom);
            float height2 = Distance(RightTop, RightBottom);
            this.Height = (height1 + height2) / 2;

            float deltaX = RightTop.X - LeftTop.X;
            float deltaY = RightTop.Y - LeftTop.Y;

            float deltaX2 = RightBottom.X - LeftBottom.X;
            float deltaY2 = RightBottom.Y - LeftBottom.Y;

            float A1 = (float)(Math.Atan2(deltaY, deltaX) * (180.0 / Math.PI));
            float A2 = (float)(Math.Atan2(deltaY2, deltaX2) * (180.0 / Math.PI));
            this.Angle = (A1 + A2) / 2;
        }

        private float Distance(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }

    public class ROI
    {
        public RectangleF Shape;
        public bool Sign;
        public ROI()
        {

        }
        public ROI(RectangleF Shape, bool Sign)
        {
            this.Shape = Shape;
            this.Sign = Sign;
        }
    }

    public class Line
    {
        public PointF point1;
        public PointF point2;

        public Line()
        {

        }
        public Line(PointF point1, PointF point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

    }

    public class MatchTemplateResult
    {
        public PointF Center;
        public float Angle;
        public PointF Deviation;
        public List<ScorePoint> Points;

        public MatchTemplateResult()
        {
            //Points = new List<ScorePoint>();
        }
        public MatchTemplateResult(PointF Center, float Angle, PointF Deviation,
            List<ScorePoint> Points)
        {
            this.Center = Center;
            this.Angle = Angle;
            this.Deviation = Deviation;
            this.Points = Points;
        }
    }

    public class MatchResult
    {
        public RectangleFA MatchBox;
        public ROI SearchBox;
        public float Score;
        public float Scale;
        public float ScaleX;
        public float ScaleY;
        public List<ScorePoint> Points;
        public bool IsOk = false;

        public MatchResult()
        {

        }
        public MatchResult(RectangleFA MatchBox, ROI SearchBox,
            float Score, float Scale, float ScaleX, float ScaleY,
            List<ScorePoint> Points)
        {
            this.MatchBox = MatchBox;
            this.SearchBox = SearchBox;
            this.Score = Score;
            this.Scale = Scale;
            this.ScaleX = ScaleX;
            this.ScaleY = ScaleY;
            this.Points = Points;
        }

    }

    public enum MatchParas
    {
        /// <summary>
        /// 金字塔模板匹配最大匹配个数 int [1-1000] 1
        /// </summary>
        MaxMatchNum,
        /// <summary>
        /// 金字塔模板匹配匹配起始角度 int [-180-180] -180
        /// </summary>
        AngleStart,
        /// <summary>
        /// 金字塔模板匹配匹配终止角度 int [-180-180] 180
        /// </summary>
        AngleEnd,
        /// <summary>
        /// 金字塔模板匹配匹配噪点标记(是否考虑噪点) int [0-1] 0
        /// </summary>
        SpotterFlag,
        /// <summary>
        /// 金字塔模板匹配最大重叠率 int [1-100] 40
        /// </summary>
        MaxOverlap,
        /// <summary>
        /// 金字塔模板匹配匹配延拓阈值 int [0-90] 0
        /// </summary>
        MatchExtentRate,
        /// <summary>
        /// 金字塔模板匹配匹配速度阈值开关 int [0-1] 0
        /// </summary>
        RoughFlag,
        /// <summary>
        /// 金字塔模板匹配匹配速度阈值 int [0-100] 50
        /// </summary>
        RoughThreshold,
        /// <summary>
        /// 金字塔模板匹配超时控制 int [0-10000] 0
        /// </summary>
        TimeOut,
        /// <summary>
        /// 金字塔模板匹配边缘阈值 int [1-255] 40
        /// </summary>
        MatchThresholdHigh,
        /// <summary>
        /// 金字塔模板匹配重要点得分阈值 int [1-100] 50
        /// </summary>
        ImportantScoreThreshold,
        /// <summary>
        /// 金字塔模板匹配匹配极性(是否考虑极性) Enum ["No" "Yes"] "Yes"
        /// </summary>
        Polarity,
        /// <summary>
        /// 金字塔模板匹配排序类型 Enum ["None" "Score" "Angle" "X" "Y" "XY" "YX"] "Score"
        /// </summary>
        SortType,
        /// <summary>
        /// 金字塔模板匹配边缘阈值标志 Enum ["Auto" "Model" "Manual"] "Auto"
        /// </summary>
        MatchThresholdFlag,
        /// <summary>
        /// 金字塔模板匹配最小匹配分数 float [0.0-1.0] 0.5
        /// </summary>
        MinScore,
        /// <summary>
        /// 金字塔模板匹配匹配起始尺度 float [0.1-10.0] 1
        /// </summary>
        ScaleStart,
        /// <summary>
        /// 金字塔模板匹配匹配终止尺度 float [0.1-10.0] 1
        /// </summary>
        ScaleEnd,
        /// <summary>
        /// 金字塔模板匹配匹配起始X尺度 float [0.1-10.0] 1
        /// </summary>
        ScaleXStart,
        /// <summary>
        /// 金字塔模板匹配匹配终止X尺度 float [0.1-10.0] 1
        /// </summary>
        ScaleXEnd,

    }


    public class LineResult
    {
        public PointF Startpoint;
        public PointF Endpoint;
        public float Angle;
        public ROI SearchBox;
        public float Straightness;
        public int pointnumber;
        public List<ScorePoint> Points;
        public bool IsOk = false;

        public LineResult()
        {

        }
        public LineResult(PointF Startpoint, PointF Endpoint, ROI SearchBox,
            float Angle, float Straightness, int pointnumber, List<ScorePoint> Points)
        {
            this.Startpoint = Startpoint;
            this.Endpoint = Endpoint;
            this.SearchBox = SearchBox;
            this.Angle = Angle;
            this.Straightness = Straightness;
            this.pointnumber = pointnumber;
            this.Points = Points;
        }

    }


    public enum LineFindParas
    {
        /// <summary>
        /// 边缘阈值 int [1-255] 5
        /// </summary>
        EdgeStrength,
        /// <summary>
        /// 卡尺数量 int [2-1000] 30
        /// </summary>
        RayNum,
        /// <summary>
        /// 卡尺宽度(参数范围同时和能力集有关,即Min(AbilityWidth/Height, Value)) int [1-500] 5
        /// </summary>
        RegionWidth,
        /// <summary>
        /// 剔除距离 int [1-1000] 5
        /// </summary>
        RejectDist,
        /// <summary>
        /// 滤波核半宽 int [1-50] 2
        /// </summary>
        KernelSize,
        /// <summary>
        /// 忽略点数 int [0-998] 0
        /// </summary>
        RejectNum,
        /// <summary>
        /// 点边距离 int [0-100] 0
        /// </summary>
        P2BoxDist,
        /// <summary>
        /// 直线度 int [0-100] 0
        /// </summary>
        LineRate,
        /// <summary>
        /// 边缘极性 Enum ["BlackToWhite" "WhiteToBlack" "Both"] "Both"
        /// </summary>
        EdgePolarity,
        /// <summary>
        /// 查找模式 Enum ["Best" "First" "Last" "Manual"] "Best"
        /// </summary>
        LineFindMode,
        /// <summary>
        /// 搜索方向 Enum ["UpToDown" "LeftToRight"] "UpToDown"
        /// </summary>
        FindOrient,
        /// <summary>
        /// 拟合方式 Enum ["LS" "Huber" "Tukey" ] "Huber"
        /// </summary>
        FitFun,
        /// <summary>
        /// 初始拟合类型 Enum ["ALS" "LLS" ] "LLS"
        /// </summary>
        FitInitType,

    }


    public class CircleResult
    {
        public PointF CircleCenter;
        public float CircleRadius;
        public PointF ArcCenter;
        public float ArcOuterRadius;
        public float ArcStartAngle;
        public float ArcAngleRange;
        public ROI SearchBox;
        public float Circleness;

        public int pointnumber;
        public List<ScorePoint> Points;

        public bool IsOk = false;

        public CircleResult()
        {

        }
        public CircleResult(PointF CircleCenter, float CircleRadius, PointF ArcCenter,
            float ArcOuterRadius, float ArcStartAngle, float ArcAngleRange, float Circleness,
            ROI SearchBox, int pointnumber, List<ScorePoint> Points)
        {
            this.CircleCenter = CircleCenter;
            this.CircleRadius = CircleRadius;
            this.ArcCenter = ArcCenter;
            this.ArcOuterRadius = ArcOuterRadius;
            this.ArcStartAngle = ArcStartAngle;
            this.ArcAngleRange = ArcAngleRange;
            this.Circleness = Circleness;
            this.SearchBox = SearchBox;
            this.pointnumber = pointnumber;
            this.Points = Points;
        }

    }


    public enum CircleFindParas
    {
        /// <summary>
        /// 圆最小半径 int [1-10000] 10
        /// </summary>
        MinRadius,
        /// <summary>
        /// 圆最大半径 int [1-10000] 100
        /// </summary>
        MaxRadius,
        /// <summary>
        /// 边缘阈值 int [0-255] 5
        /// </summary>
        EdgeThresh,
        /// <summary>
        /// 卡尺数量 int [3-1000] 30
        /// </summary>
        RadNum,
        /// <summary>
        /// 圆环起始角度 int [-180-180] 0
        /// </summary>
        StartAngle,
        /// <summary>
        /// 圆环角度范围 int [1-360] 360
        /// </summary>
        AngleExtend,
        /// <summary>
        /// 下采样系数 int [1-8] 8
        /// </summary>
        CCDSampleScale,
        /// <summary>
        /// 圆定位敏感度 int [1-1000] 10
        /// </summary>
        CCDCircleThresh,
        /// <summary>
        /// 滤波核半宽 int [1-50] 2
        /// </summary>
        EdgeWidth,
        /// <summary>
        /// 卡尺宽度(参数范围同时和能力集有关,即Min(AbilityWidth/Height, Value)) int [1-500] 5
        /// </summary>
        ProLength,
        /// <summary>
        /// 忽略点数 int [0-998] 0
        /// </summary>
        RejectNum,
        /// <summary>
        /// 剔除距离 int [1-15000] 5
        /// </summary>
        RejectDist,
        /// <summary>
        /// 圆度 int [0-100] 0
        /// </summary>
        CircleRate,
        /// <summary>
        /// 圆面积度 int [0-100] 0
        /// </summary>
        AreaRate,
        /// <summary>
        /// 边缘极性 Enum ["BlackToWhite" "WhiteToBlack" "Both"] "BlackToWhite"
        /// </summary>
        EdgePolarity,
        /// <summary>
        /// 查找模式 Enum ["Best" "Largest" "Smallest" "Manual"] "Smallest"
        /// </summary>
        CircleFindMode,
        /// <summary>
        /// 边缘扫描方向 Enum ["InnerToOuter" "OuterToInner"] "InnerToOuter"
        /// </summary>
        EdgeScanOrient,
        /// <summary>
        /// 拟合方式 Enum ["LS" "Huber" "Tukey" ] "Huber"
        /// </summary>
        FitFun,
        /// <summary>
        /// 初始拟合类型 Enum ["ALS" "LLS" ] "LLS"
        /// </summary>
        InitType,
    }



}
