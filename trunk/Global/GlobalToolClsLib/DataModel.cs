﻿using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalToolClsLib
{
    public class DataModel: INotifyPropertyChanged
    {
        private static readonly Lazy<DataModel> instance = new Lazy<DataModel>(() => new DataModel());
        public static DataModel Instance => instance.Value;
        private DataModel() 
        {
            materialMat = new List<List<EnumMaterialproperties>>();
            materialBoxMapLog = new EnumReturnMaterialBoxproperties();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        #region 内部参数

        private int weldMaterialNumber = 0;
        private int pressWorkNumber = 0;
        private int equipmentOperatingTime = 0;

        private bool run = false;
        private bool error = false;

        private bool stageAxisIsconnect = true;
        private bool stageIOIsconnect = true;
        private bool cameraIsconnect = true;
        private bool temperatureIsconnect = true;
        private bool vacuumIsconnect = true;
        private bool turboMolecularPumpIsconnect = true;


        private EnumOvenBoxState state = EnumOvenBoxState.None;

        private bool linkstatusofmodules = true;

        private bool ovenBox1Heating = false;
        private bool ovenBox2Heating = false;
        private bool ovenBox1OutRemind = false;
        private bool ovenBox2OutRemind = false;
        private bool ovenBox1InRemind = false;
        private bool ovenBox2InRemind = false;

        private string joblogText;
        private List<List<EnumMaterialproperties>> materialMat;
        private EnumReturnMaterialBoxproperties materialBoxMapLog;

        private int ovennum = 0;
        private int materialboxnum = 0;
        private int materialnum = 0;
        private int materialrow = 0;
        private int materialcol = 0;


        private bool towerYellowLight = false;
        private bool towerGreenLight = false;
        private bool towerRedLight = false;

        #region 烘箱1

        /// <summary>
        /// 烘箱补气阀 bool
        /// </summary>
        private bool  bakeOvenAerate = false;

        /// <summary>
        /// 烘箱粗抽阀 bool
        /// </summary>
        private bool  bakeOvenCoarseExtractionValve = false;
        /// <summary>
        /// 烘箱前级阀 bool
        /// </summary>
        private bool  bakeOvenFrontStageValve = false;
        /// <summary>
        /// 烘箱插板阀 bool
        /// </summary>
        private bool  bakeOvenPlugInValve = false;
        /// <summary>
        /// 烘箱机械泵 bool
        /// </summary>
        private bool  bakeOvenMechanicalPump = false;


        /// <summary>
        /// 烘箱内门升降 bool 按1松0
        /// </summary>
        private bool  bakeOvenInnerdoorUpDown = false;
        

        #endregion

        #region 烘箱2

        /// <summary>
        /// 烘箱补气阀 bool
        /// </summary>
        private bool  bakeOven2Aerate = false;

        /// <summary>
        /// 烘箱2粗抽阀 bool
        /// </summary>
        private bool  bakeOven2CoarseExtractionValve = false;
        /// <summary>
        /// 烘箱2前级阀 bool
        /// </summary>
        private bool  bakeOven2FrontStageValve = false;
        /// <summary>
        /// 烘箱2插板阀 bool
        /// </summary>
        private bool  bakeOven2PlugInValve = false;
        /// <summary>
        /// 烘箱2机械泵 bool
        /// </summary>
        private bool  bakeOven2MechanicalPump = false;

        /// <summary>
        /// 烘箱内门升 bool 按1松0
        /// </summary>
        private bool  bakeOven2InnerdoorUpDown = false;

        #endregion


        #region 箱体

        /// <summary>
        /// 箱体补气阀 short
        /// </summary>
        private bool  boxAerate = false;

        /// <summary>
        /// 方舱粗抽阀 bool
        /// </summary>
        private bool  boxCoarseExtractionValve = false;
        /// <summary>
        /// 方舱前级阀 bool
        /// </summary>
        private bool  boxFrontStageValve = false;
        /// <summary>
        /// 方舱插板阀 bool
        /// </summary>
        private bool  boxPlugInValve = false;
        /// <summary>
        /// 方舱机械泵 bool
        /// </summary>
        private bool  boxMechanicalPump = false;


        /// <summary>
        /// 压机压合分离 bool
        /// </summary>
        private bool pressPressingDivide = false;

        /// <summary>
        /// 压缩机启动 bool
        /// </summary>
        private bool compressorStartup = false;

        /// <summary>
        /// 压缩机停止 bool
        /// </summary>
        private bool compressorStops = false;

        /// <summary>
        /// 冷凝泵 bool
        /// </summary>
        private bool condenserPump = false;


        #endregion

        #region 电机

        /// <summary>
        /// 电机抱闸1
        /// </summary>
        private bool motorprivateBrake = false;
        /// <summary>
        /// 电机抱闸2
        /// </summary>
        private bool motorprivateBrake1 = false;

        #endregion



        private bool bakeOvenPlugInValveOpenstatus = false;
        private bool bakeOvenPlugInValveClosestatus = false;
        private bool bakeOvenInnerdoorOpenstatus = false;
        private bool bakeOvenInnerdoorClosestatus = false;
        private bool bakeOvenOuterdoorClosestatus = false;
        private bool bakeOvenPressureSensor = false;
        private bool bakeOvenAutoHeat = false;
        private float bakeOvenVacuum = 0;
        private float bakeOvenPressure = 0;
        private float bakeOvenDowntemp = 0;

        private bool bakeOven2PlugInValveOpenstatus = false;
        private bool bakeOven2PlugInValveClosestatus = false;
        private bool bakeOven2InnerdoorOpenstatus = false;
        private bool bakeOven2InnerdoorClosestatus = false;
        private bool bakeOven2OuterdoorClosestatus = false;
        private bool bakeOven2PressureSensor = false;
        private bool bakeOven2AutoHeat = false;
        private float bakeOven2Vacuum = 0;
        private float bakeOven2Pressure = 0;
        private float bakeOven2Downtemp = 0;

        private bool boxPlugInValveOpenstatus = false;
        private bool boxPlugInValveClosestatus = false;
        private bool boxOuterdoorClosestatus = false;
        private bool condenserPumpSignal1 = false;
        private bool condenserPumpSignal2 = false;
        private bool condenserPumpSignal3 = false;
        private bool condenserPumpSignal4 = false;
        private bool condenserPumpSignal5 = false;
        private bool condenserPumpSignal6 = false;
        private bool thermalRelay = false;
        private bool boxPressureSensor = false;
        private float boxVacuum = 0;
        private float boxDewPoint = 0;
        private float boxPressure = 0;
        private bool pressIsPress = false;
        private bool pressIsDivide = false;



        private int heatPreservationResidueMinute = 0;
        private int heatPreservationResidueMinute2 = 0;

        private float ovenBox1OutputFrequency = 0;
        private float ovenBox1OutputVoltage = 0;
        private float ovenBox1OutputCurrent = 0;
        private bool ovenBox1Standbymode = false;
        private bool ovenBox1Function = false;
        private bool ovenBox1err = false;
        private bool ovenBox1OC = false;
        private bool ovenBox1OE = false;
        private bool ovenBox1Retain = false;
        private bool ovenBox1RLU = false;
        private bool ovenBox1OL2 = false;
        private bool ovenBox1SL = false;
        private bool ovenBox1ESP = false;
        private bool ovenBox1LU = false;
        private bool ovenBox1OH = false;

        private float ovenBox2OutputFrequency = 0;
        private float ovenBox2OutputVoltage = 0;
        private float ovenBox2OutputCurrent = 0;
        private bool ovenBox2Standbymode = false;
        private bool ovenBox2Function = false;
        private bool ovenBox2err = false;
        private bool ovenBox2OC = false;
        private bool ovenBox2OE = false;
        private bool ovenBox2Retain = false;
        private bool ovenBox2RLU = false;
        private bool ovenBox2OL2 = false;
        private bool ovenBox2SL = false;
        private bool ovenBox2ESP = false;
        private bool ovenBox2LU = false;
        private bool ovenBox2OH = false;




        #endregion


        #region 全局参数

        /// <summary>
        /// 已焊接物料个数
        /// </summary>
        public int WeldMaterialNumber
        {
            get { return weldMaterialNumber; }
            set
            {
                if (weldMaterialNumber != value)
                {
                    weldMaterialNumber = value;
                    OnPropertyChanged(nameof(WeldMaterialNumber));
                }
            }
        }

        /// <summary>
        /// 压机工作次数
        /// </summary>
        public int PressWorkNumber
        {
            get { return pressWorkNumber; }
            set
            {
                if (pressWorkNumber != value)
                {
                    pressWorkNumber = value;
                    OnPropertyChanged(nameof(PressWorkNumber));
                }
            }
        }

        /// <summary>
        /// 设备运行时间 分钟
        /// </summary>
        public int EquipmentOperatingTime
        {
            get { return equipmentOperatingTime; }
            set
            {
                if (equipmentOperatingTime != value)
                {
                    equipmentOperatingTime = value;
                    OnPropertyChanged(nameof(EquipmentOperatingTime));
                }
            }
        }


        /// <summary>
        /// 运行状态
        /// </summary>
        public bool Run
        {
            get { return run; }
            set
            {
                if (run != value)
                {
                    run = value;
                    OnPropertyChanged(nameof(Run));
                }
            }
        }

        /// <summary>
        /// 错误状态
        /// </summary>
        public bool Error
        {
            get { return error; }
            set
            {
                if (error != value)
                {
                    error = value;
                    OnPropertyChanged(nameof(Error));
                }
            }
        }




        /// <summary>
        /// 轴连接状态
        /// </summary>
        public bool StageAxisIsconnect
        {
            get { return stageAxisIsconnect; }
            set
            {
                if (stageAxisIsconnect != value)
                {
                    stageAxisIsconnect = value;
                    OnPropertyChanged(nameof(StageAxisIsconnect));
                }
            }
        }
        /// <summary>
        /// 轴IO模块连接状态
        /// </summary>
        public bool StageIOIsconnect
        {
            get { return stageIOIsconnect; }
            set
            {
                if (stageIOIsconnect != value)
                {
                    stageIOIsconnect = value;
                    OnPropertyChanged(nameof(StageIOIsconnect));
                }
            }
        }

        /// <summary>
        /// 相机连接状态
        /// </summary>
        public bool CameraIsconnect
        {
            get { return cameraIsconnect; }
            set
            {
                if (cameraIsconnect != value)
                {
                    cameraIsconnect = value;
                    OnPropertyChanged(nameof(CameraIsconnect));
                }
            }
        }

        /// <summary>
        /// 温控表连接状态
        /// </summary>
        public bool TemperatureIsconnect
        {
            get { return temperatureIsconnect; }
            set
            {
                if (temperatureIsconnect != value)
                {
                    temperatureIsconnect = value;
                    OnPropertyChanged(nameof(TemperatureIsconnect));
                }
            }
        }

        /// <summary>
        /// 真空计连接状态
        /// </summary>
        public bool VacuumIsconnect
        {
            get { return vacuumIsconnect; }
            set
            {
                if (vacuumIsconnect != value)
                {
                    vacuumIsconnect = value;
                    OnPropertyChanged(nameof(VacuumIsconnect));
                }
            }
        }

        /// <summary>
        /// 分子泵连接状态
        /// </summary>
        public bool TurboMolecularPumpIsconnect
        {
            get { return turboMolecularPumpIsconnect; }
            set
            {
                if (turboMolecularPumpIsconnect != value)
                {
                    turboMolecularPumpIsconnect = value;
                    OnPropertyChanged(nameof(TurboMolecularPumpIsconnect));
                }
            }
        }





        /// <summary>
        /// IO模块链接状态
        /// </summary>
        public bool Linkstatusofmodules
        {
            get { return linkstatusofmodules; }
            set
            {
                if (linkstatusofmodules != value)
                {
                    linkstatusofmodules = value;
                    OnPropertyChanged(nameof(Linkstatusofmodules));
                }
            }
        }


        public EnumOvenBoxState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                }
            }
        }

        public bool OvenBox1Heating
        {
            get { return ovenBox1Heating; }
            set
            {
                if (ovenBox1Heating != value)
                {
                    ovenBox1Heating = value;
                }
            }
        }
        public bool OvenBox2Heating
        {
            get { return ovenBox2Heating; }
            set
            {
                if (ovenBox2Heating != value)
                {
                    ovenBox2Heating = value;
                }
            }
        }

        public bool OvenBox1OutRemind
        {
            get { return ovenBox1OutRemind; }
            set
            {
                if (ovenBox1OutRemind != value)
                {
                    ovenBox1OutRemind = value;
                }
            }
        }
        public bool OvenBox2OutRemind
        {
            get { return ovenBox2OutRemind; }
            set
            {
                if (ovenBox2OutRemind != value)
                {
                    ovenBox2OutRemind = value;
                }
            }
        }

        public bool OvenBox1InRemind
        {
            get { return ovenBox1InRemind; }
            set
            {
                if (ovenBox1InRemind != value)
                {
                    ovenBox1InRemind = value;
                }
            }
        }
        public bool OvenBox2InRemind
        {
            get { return ovenBox2InRemind; }
            set
            {
                if (ovenBox2InRemind != value)
                {
                    ovenBox2InRemind = value;
                }
            }
        }



        public string JobLogText
        {
            get { return joblogText; }
            set
            {
                if (joblogText != value)
                {
                    joblogText = value;
                    OnPropertyChanged(nameof(JobLogText));
                }
            }
        }

        public int Ovennum
        {
            get { return ovennum; }
            set
            {
                if (ovennum != value)
                {
                    ovennum = value;
                    OnPropertyChanged(nameof(Ovennum));
                }
            }
        }
        public int Materialboxnum
        {
            get { return materialboxnum; }
            set
            {
                if (materialboxnum != value)
                {
                    materialboxnum = value;
                    OnPropertyChanged(nameof(Materialboxnum));
                }
            }
        }
        public int Materialnum
        {
            get { return materialnum; }
            set
            {
                if (materialnum != value)
                {
                    materialnum = value;
                    OnPropertyChanged(nameof(Materialnum));
                }
            }
        }
        public int Materialrow
        {
            get { return materialrow; }
            set
            {
                if (materialrow != value)
                {
                    materialrow = value;
                    OnPropertyChanged(nameof(Materialrow));
                }
            }
        }
        public int Materialcol
        {
            get { return materialcol; }
            set
            {
                if (materialcol != value)
                {
                    materialcol = value;
                    OnPropertyChanged(nameof(Materialcol));
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
        public EnumReturnMaterialBoxproperties MaterialBoxMapLog
        {
            get { return materialBoxMapLog; }
            set
            {
                if (materialBoxMapLog != value)
                {
                    if (materialBoxMapLog != null)
                    {
                        (materialBoxMapLog as INotifyPropertyChanged).PropertyChanged -= OnInnerPropertyChanged;
                    }

                    materialBoxMapLog = value;
                    OnPropertyChanged(nameof(MaterialBoxMapLog));

                    if (materialBoxMapLog != null)
                    {
                        (materialBoxMapLog as INotifyPropertyChanged).PropertyChanged += OnInnerPropertyChanged;
                    }
                }
            }
        }



        //IO
        /// <summary>
        /// 塔灯黄灯
        /// </summary>
        public bool TowerYellowLight
        {
            get { return towerYellowLight; }
            set
            {
                if (towerYellowLight != value)
                {
                    towerYellowLight = value;
                    OnPropertyChanged(nameof(TowerYellowLight));
                }
            }
        }
        /// <summary>
        /// 塔灯绿灯
        /// </summary>
        public bool TowerGreenLight
        {
            get { return towerGreenLight; }
            set
            {
                if (towerGreenLight != value)
                {
                    towerGreenLight = value;
                    OnPropertyChanged(nameof(TowerGreenLight));
                }
            }
        }
        /// <summary>
        /// 塔灯红灯
        /// </summary>
        public bool TowerRedLight
        {
            get { return towerRedLight; }
            set
            {
                if (towerRedLight != value)
                {
                    towerRedLight = value;
                    OnPropertyChanged(nameof(TowerRedLight));
                }
            }
        }

        #region 烘箱1

        /// <summary>
        /// 烘箱补气阀 bool
        /// </summary>
        public bool BakeOvenAerate{get { return bakeOvenAerate; }set{if (bakeOvenAerate != value){ bakeOvenAerate = value;OnPropertyChanged(nameof(BakeOvenAerate));}}}

        /// <summary>
        /// 烘箱粗抽阀 bool
        /// </summary>
        public bool BakeOvenCoarseExtractionValve{get { return bakeOvenCoarseExtractionValve; }set{if (bakeOvenCoarseExtractionValve != value){ bakeOvenCoarseExtractionValve = value;OnPropertyChanged(nameof(BakeOvenCoarseExtractionValve));}}}
        /// <summary>
        /// 烘箱前级阀 bool
        /// </summary>
        public bool BakeOvenFrontStageValve{get { return bakeOvenFrontStageValve; }set{if (bakeOvenFrontStageValve != value){ bakeOvenFrontStageValve = value;OnPropertyChanged(nameof(BakeOvenFrontStageValve));}}}
        /// <summary>
        /// 烘箱插板阀 bool
        /// </summary>
        public bool BakeOvenPlugInValve{get { return bakeOvenPlugInValve; }set{if (bakeOvenPlugInValve != value){ bakeOvenPlugInValve = value;OnPropertyChanged(nameof(BakeOvenPlugInValve));}}}
        /// <summary>
        /// 烘箱机械泵 bool
        /// </summary>
        public bool BakeOvenMechanicalPump{get { return bakeOvenMechanicalPump; }set{if (bakeOvenMechanicalPump != value){ bakeOvenMechanicalPump = value;OnPropertyChanged(nameof(BakeOvenMechanicalPump));}}}


        /// <summary>
        /// 烘箱内门升降 bool 按1松0
        /// </summary>
        public bool BakeOvenInnerdoorUpDown{get { return bakeOvenInnerdoorUpDown; }set{if (bakeOvenInnerdoorUpDown != value){ bakeOvenInnerdoorUpDown = value;OnPropertyChanged(nameof(BakeOvenInnerdoorUpDown));}}}


        #endregion

        #region 烘箱2

        /// <summary>
        /// 烘箱2补气阀 bool
        /// </summary>
        public bool BakeOven2Aerate{get { return bakeOven2Aerate; }set{if (bakeOven2Aerate != value){ bakeOven2Aerate = value;OnPropertyChanged(nameof(BakeOven2Aerate));}}}

        /// <summary>
        /// 烘箱2粗抽阀 bool
        /// </summary>
        public bool BakeOven2CoarseExtractionValve{get { return bakeOven2CoarseExtractionValve; }set{if (bakeOven2CoarseExtractionValve != value){ bakeOven2CoarseExtractionValve = value;OnPropertyChanged(nameof(BakeOven2CoarseExtractionValve));}}}
        /// <summary>
        /// 烘箱2前级阀 bool
        /// </summary>
        public bool BakeOven2FrontStageValve{get { return bakeOven2FrontStageValve; }set{if (bakeOven2FrontStageValve != value){ bakeOven2FrontStageValve = value;OnPropertyChanged(nameof(BakeOven2FrontStageValve));}}}
        /// <summary>
        /// 烘箱2插板阀 bool
        /// </summary>
        public bool BakeOven2PlugInValve{get { return bakeOven2PlugInValve; }set{if (bakeOven2PlugInValve != value){ bakeOven2PlugInValve = value;OnPropertyChanged(nameof(BakeOven2PlugInValve));}}}
        /// <summary>
        /// 烘箱2机械泵 bool
        /// </summary>
        public bool BakeOven2MechanicalPump{get { return bakeOven2MechanicalPump; }set{if (bakeOven2MechanicalPump != value){ bakeOven2MechanicalPump = value;OnPropertyChanged(nameof(BakeOven2MechanicalPump));}}}

        /// <summary>
        /// 烘箱内门升 bool 按1松0
        /// </summary>
        public bool BakeOven2InnerdoorUpDown{get { return bakeOven2InnerdoorUpDown; }set{if (bakeOven2InnerdoorUpDown != value){ bakeOven2InnerdoorUpDown = value;OnPropertyChanged(nameof(BakeOven2InnerdoorUpDown));}}}

        #endregion


        #region 箱体

        /// <summary>
        /// 箱体补气阀 short
        /// </summary>
        public bool BoxAerate{get { return boxAerate; }set{if (boxAerate != value){ boxAerate = value;OnPropertyChanged(nameof(BoxAerate));}}}

        /// <summary>
        /// 方舱粗抽阀 bool
        /// </summary>
        public bool BoxCoarseExtractionValve{get { return boxCoarseExtractionValve; }set{if (boxCoarseExtractionValve != value){ boxCoarseExtractionValve = value;OnPropertyChanged(nameof(BoxCoarseExtractionValve));}}}
        /// <summary>
        /// 方舱前级阀 bool
        /// </summary>
        public bool BoxFrontStageValve{get { return boxFrontStageValve; }set{if (boxFrontStageValve != value){ boxFrontStageValve = value;OnPropertyChanged(nameof(BoxFrontStageValve));}}}
        /// <summary>
        /// 方舱插板阀 bool
        /// </summary>
        public bool BoxPlugInValve{get { return boxPlugInValve; }set{if (boxPlugInValve != value){ boxPlugInValve = value;OnPropertyChanged(nameof(BoxPlugInValve));}}}
        /// <summary>
        /// 方舱机械泵 bool
        /// </summary>
        public bool BoxMechanicalPump{get { return boxMechanicalPump; }set{if (boxMechanicalPump != value){ boxMechanicalPump = value;OnPropertyChanged(nameof(BoxMechanicalPump));}}}


        /// <summary>
        /// 压机压合分离 bool
        /// </summary>
        public bool PressPressingDivide{get { return pressPressingDivide; }set{if (pressPressingDivide != value){ pressPressingDivide = value;OnPropertyChanged(nameof(PressPressingDivide));}}}

        /// <summary>
        /// 压缩机启动 bool
        /// </summary>
        public bool CompressorStartup{get { return compressorStartup; }set{if (compressorStartup != value){ compressorStartup = value;OnPropertyChanged(nameof(CompressorStartup));}}}

        /// <summary>
        /// 压缩机停止 bool
        /// </summary>
        public bool CompressorStops{get { return compressorStops; }set{if (compressorStops != value){ compressorStops = value;OnPropertyChanged(nameof(CompressorStops));}}}

        /// <summary>
        /// 冷凝泵 bool
        /// </summary>
        public bool CondenserPump{get { return condenserPump; }set{if (condenserPump != value){ condenserPump = value;OnPropertyChanged(nameof(CondenserPump));}}}


        #endregion

        #region 电机

        /// <summary>
        /// 电机抱闸1
        /// </summary>
        public bool MotorBrake { get { return motorprivateBrake; }set{if (motorprivateBrake != value){ motorprivateBrake = value;OnPropertyChanged(nameof(MotorBrake));}}}
        /// <summary>
        /// 电机抱闸2
        /// </summary>
        public bool MotorBrake1 { get { return motorprivateBrake1; }set{if (motorprivateBrake1 != value){ motorprivateBrake1 = value;OnPropertyChanged(nameof(MotorBrake1));}}}

        #endregion




        /// <summary>
        /// 烘箱插板阀打开状态 bool
        /// </summary>
        public bool BakeOvenPlugInValveOpenstatus{get { return bakeOvenPlugInValveOpenstatus; }set
            {
                if (bakeOvenPlugInValveOpenstatus != value)
                {
                    bakeOvenPlugInValveOpenstatus = value;
                    OnPropertyChanged(nameof(BakeOvenPlugInValveOpenstatus));
                }
            }}
        /// <summary>
        /// 烘箱插板阀关闭状态 bool
        /// </summary>
        public bool BakeOvenPlugInValveClosestatus
        {
            get { return bakeOvenPlugInValveClosestatus; }
            set
            {
                if (bakeOvenPlugInValveClosestatus != value)
                {
                    bakeOvenPlugInValveClosestatus = value;
                    OnPropertyChanged(nameof(BakeOvenPlugInValveClosestatus));
                }
            }
        }
        /// <summary>
        /// 烘箱内门打开状态 bool
        /// </summary>
        public bool BakeOvenInnerdoorOpenstatus
        {
            get { return bakeOvenInnerdoorOpenstatus; }
            set
            {
                if (bakeOvenInnerdoorOpenstatus != value)
                {
                    bakeOvenInnerdoorOpenstatus = value;
                    OnPropertyChanged(nameof(BakeOvenInnerdoorOpenstatus));
                }
            }
        }
        /// <summary>
        /// 烘箱内门关闭状态 bool
        /// </summary>
        public bool BakeOvenInnerdoorClosestatus
        {
            get { return bakeOvenInnerdoorClosestatus; }
            set
            {
                if (bakeOvenInnerdoorClosestatus != value)
                {
                    bakeOvenInnerdoorClosestatus = value;
                    OnPropertyChanged(nameof(BakeOvenInnerdoorClosestatus));
                }
            }
        }
        /// <summary>
        /// 烘箱外门关闭状态 bool
        /// </summary>
        public bool BakeOvenOuterdoorClosestatus
        {
            get { return bakeOvenOuterdoorClosestatus; }
            set
            {
                if (bakeOvenOuterdoorClosestatus != value)
                {
                    bakeOvenOuterdoorClosestatus = value;
                    OnPropertyChanged(nameof(BakeOvenOuterdoorClosestatus));
                }
            }
        }
        /// <summary>
        /// 烘箱1压力传感器到达常压 bool
        /// </summary>
        public bool BakeOvenPressureSensor
        {
            get { return bakeOvenPressureSensor; }
            set
            {
                if (bakeOvenPressureSensor != value)
                {
                    bakeOvenPressureSensor = value;
                    OnPropertyChanged(nameof(BakeOvenPressureSensor));
                }
            }
        }

        /// <summary>
        /// 烘箱加热状态 bool
        /// </summary>
        public bool BakeOvenAutoHeat
        {
            get { return bakeOvenAutoHeat; }
            set
            {
                if (bakeOvenAutoHeat != value)
                {
                    bakeOvenAutoHeat = value;
                    OnPropertyChanged(nameof(BakeOvenAutoHeat));
                }
            }
        }

        /// <summary>
        /// 烘箱真空度 float
        /// </summary>
        public float BakeOvenVacuum
        {
            get { return bakeOvenVacuum; }
            set
            {
                if (bakeOvenVacuum != value)
                {
                    bakeOvenVacuum = value;
                    OnPropertyChanged(nameof(BakeOvenVacuum));
                }
            }
        }

        /// <summary>
        /// 烘箱压力 float
        /// </summary>
        public float BakeOvenPressure
        {
            get { return bakeOvenPressure; }
            set
            {
                if (bakeOvenPressure != value)
                {
                    bakeOvenPressure = value;
                    OnPropertyChanged(nameof(BakeOvenPressure));
                }
            }
        }

        /// <summary>
        /// 烘箱下板温度 float
        /// </summary>
        public float BakeOvenDowntemp
        {
            get { return bakeOvenDowntemp; }
            set
            {
                if (bakeOvenDowntemp != value)
                {
                    bakeOvenDowntemp = value;
                    OnPropertyChanged(nameof(BakeOvenDowntemp));
                }
            }
        }


        /// <summary>
        /// 烘箱2插板阀打开状态 bool
        /// </summary>
        public bool BakeOven2PlugInValveOpenstatus
        {
            get { return bakeOven2PlugInValveOpenstatus; }
            set
            {
                if (bakeOven2PlugInValveOpenstatus != value)
                {
                    bakeOven2PlugInValveOpenstatus = value;
                    OnPropertyChanged(nameof(BakeOven2PlugInValveOpenstatus));
                }
            }
        }
        /// <summary>
        /// 烘箱2插板阀关闭状态 bool
        /// </summary>
        public bool BakeOven2PlugInValveClosestatus
        {
            get { return bakeOven2PlugInValveClosestatus; }
            set
            {
                if (bakeOven2PlugInValveClosestatus != value)
                {
                    bakeOven2PlugInValveClosestatus = value;
                    OnPropertyChanged(nameof(BakeOven2PlugInValveClosestatus));
                }
            }
        }
        /// <summary>
        /// 烘箱2内门打开状态 bool
        /// </summary>
        public bool BakeOven2InnerdoorOpenstatus
        {
            get { return bakeOven2InnerdoorOpenstatus; }
            set
            {
                if (bakeOven2InnerdoorOpenstatus != value)
                {
                    bakeOven2InnerdoorOpenstatus = value;
                    OnPropertyChanged(nameof(BakeOven2InnerdoorOpenstatus));
                }
            }
        }
        /// <summary>
        /// 烘箱2内门关闭状态 bool
        /// </summary>
        public bool BakeOven2InnerdoorClosestatus
        {
            get { return bakeOven2InnerdoorClosestatus; }
            set
            {
                if (bakeOven2InnerdoorClosestatus != value)
                {
                    bakeOven2InnerdoorClosestatus = value;
                    OnPropertyChanged(nameof(BakeOven2InnerdoorClosestatus));
                }
            }
        }
        /// <summary>
        /// 烘箱2外门关闭状态 bool
        /// </summary>
        public bool BakeOven2OuterdoorClosestatus
        {
            get { return bakeOven2OuterdoorClosestatus; }
            set
            {
                if (bakeOven2OuterdoorClosestatus != value)
                {
                    bakeOven2OuterdoorClosestatus = value;
                    OnPropertyChanged(nameof(BakeOven2OuterdoorClosestatus));
                }
            }
        }

        /// <summary>
        /// 烘箱2压力传感器到达常压 bool
        /// </summary>
        public bool BakeOven2PressureSensor
        {
            get { return bakeOven2PressureSensor; }
            set
            {
                if (bakeOven2PressureSensor != value)
                {
                    bakeOven2PressureSensor = value;
                    OnPropertyChanged(nameof(BakeOven2PressureSensor));
                }
            }
        }

        /// <summary>
        /// 烘箱2加热状态 bool
        /// </summary>
        public bool BakeOven2AutoHeat
        {
            get { return bakeOven2AutoHeat; }
            set
            {
                if (bakeOven2AutoHeat != value)
                {
                    bakeOven2AutoHeat = value;
                    OnPropertyChanged(nameof(BakeOven2AutoHeat));
                }
            }
        }

        /// <summary>
        /// 烘箱2真空度 float
        /// </summary>
        public float BakeOven2Vacuum
        {
            get { return bakeOven2Vacuum; }
            set
            {
                if (bakeOven2Vacuum != value)
                {
                    bakeOven2Vacuum = value;
                    OnPropertyChanged(nameof(BakeOven2Vacuum));
                }
            }
        }

        /// <summary>
        /// 烘箱2压力 float
        /// </summary>
        public float BakeOven2Pressure
        {
            get { return bakeOven2Pressure; }
            set
            {
                if (bakeOven2Pressure != value)
                {
                    bakeOven2Pressure = value;
                    OnPropertyChanged(nameof(BakeOven2Pressure));
                }
            }
        }

        /// <summary>
        /// 烘箱2下板温度 float
        /// </summary>
        public float BakeOven2Downtemp
        {
            get { return bakeOven2Downtemp; }
            set
            {
                if (bakeOven2Downtemp != value)
                {
                    bakeOven2Downtemp = value;
                    OnPropertyChanged(nameof(BakeOven2Downtemp));
                }
            }
        }


        /// <summary>
        /// 方舱插板阀打开状态 bool
        /// </summary>
        public bool BoxPlugInValveOpenstatus
        {
            get { return boxPlugInValveOpenstatus; }
            set
            {
                if (boxPlugInValveOpenstatus != value)
                {
                    boxPlugInValveOpenstatus = value;
                    OnPropertyChanged(nameof(BoxPlugInValveOpenstatus));
                }
            }
        }
        /// <summary>
        /// 方舱插板阀关闭状态 bool
        /// </summary>
        public bool BoxPlugInValveClosestatus
        {
            get { return boxPlugInValveClosestatus; }
            set
            {
                if (boxPlugInValveClosestatus != value)
                {
                    boxPlugInValveClosestatus = value;
                    OnPropertyChanged(nameof(BoxPlugInValveClosestatus));
                }
            }
        }
        /// <summary>
        /// 方舱外门关闭状态 bool
        /// </summary>
        public bool BoxOuterdoorClosetatus
        {
            get { return boxOuterdoorClosestatus; }
            set
            {
                if (boxOuterdoorClosestatus != value)
                {
                    boxOuterdoorClosestatus = value;
                    OnPropertyChanged(nameof(BoxOuterdoorClosetatus));
                }
            }
        }

        /// <summary>
        /// 冷凝泵到达10pa bool
        /// </summary>
        public bool CondenserPumpSignal1
        {
            get { return condenserPumpSignal1; }
            set
            {
                if (condenserPumpSignal1 != value)
                {
                    condenserPumpSignal1 = value;
                    OnPropertyChanged(nameof(CondenserPumpSignal1));
                }
            }
        }
        /// <summary>
        /// 冷凝泵到达20K bool
        /// </summary>
        public bool CondenserPumpSignal2
        {
            get { return condenserPumpSignal2; }
            set
            {
                if (condenserPumpSignal2 != value)
                {
                    condenserPumpSignal2 = value;
                    OnPropertyChanged(nameof(CondenserPumpSignal2));
                }
            }
        }
        /// <summary>
        /// 冷凝泵到达室温 bool
        /// </summary>
        public bool CondenserPumpSignal3
        {
            get { return condenserPumpSignal3; }
            set
            {
                if (condenserPumpSignal3 != value)
                {
                    condenserPumpSignal3 = value;
                    OnPropertyChanged(nameof(CondenserPumpSignal3));
                }
            }
        }
        /// <summary>
        /// 皮拉尼到达40Pa bool
        /// </summary>
        public bool CondenserPumpSignal4
        {
            get { return condenserPumpSignal4; }
            set
            {
                if (condenserPumpSignal4 != value)
                {
                    condenserPumpSignal4 = value;
                    OnPropertyChanged(nameof(CondenserPumpSignal4));
                }
            }
        }
        /// <summary>
        /// 皮拉尼到达50Pa bool
        /// </summary>
        public bool CondenserPumpSignal5
        {
            get { return condenserPumpSignal5; }
            set
            {
                if (condenserPumpSignal5 != value)
                {
                    condenserPumpSignal5 = value;
                    OnPropertyChanged(nameof(CondenserPumpSignal5));
                }
            }
        }
        /// <summary>
        /// 冷凝泵到大气 bool
        /// </summary>
        public bool CondenserPumpSignal6
        {
            get { return condenserPumpSignal6; }
            set
            {
                if (condenserPumpSignal6 != value)
                {
                    condenserPumpSignal6 = value;
                    OnPropertyChanged(nameof(CondenserPumpSignal6));
                }
            }
        }
        /// <summary>
        /// 热继电器
        /// </summary>
        public bool ThermalRelay
        {
            get { return thermalRelay; }
            set
            {
                if (thermalRelay != value)
                {
                    thermalRelay = value;
                    OnPropertyChanged(nameof(ThermalRelay));
                }
            }
        }


        /// <summary>
        /// 方舱压力传感器到达常压 bool
        /// </summary>
        public bool BoxPressureSensor
        {
            get { return boxPressureSensor; }
            set
            {
                if (boxPressureSensor != value)
                {
                    boxPressureSensor = value;
                    OnPropertyChanged(nameof(BoxPressureSensor));
                }
            }
        }

        /// <summary>
        /// 方舱真空度 float
        /// </summary>
        public float BoxVacuum
        {
            get { return boxVacuum; }
            set
            {
                if (boxVacuum != value)
                {
                    boxVacuum = value;
                    OnPropertyChanged(nameof(BoxVacuum));
                }
            }
        }

        /// <summary>
        /// 方舱露点 float
        /// </summary>
        public float BoxDewPoint
        {
            get { return boxDewPoint; }
            set
            {
                if (boxDewPoint != value)
                {
                    boxDewPoint = value;
                    OnPropertyChanged(nameof(BoxDewPoint));
                }
            }
        }


        /// <summary>
        /// 方舱压力 float
        /// </summary>
        public float BoxPressure
        {
            get { return boxPressure; }
            set
            {
                if (boxPressure != value)
                {
                    boxPressure = value;
                    OnPropertyChanged(nameof(BoxPressure));
                }
            }
        }

        /// <summary>
        /// 压机压合到位 bool
        /// </summary>
        public bool PressIsPress
        {
            get { return pressIsPress; }
            set
            {
                if (pressIsPress != value)
                {
                    pressIsPress = value;
                    OnPropertyChanged(nameof(PressIsPress));
                }
            }
        }
        /// <summary>
        /// 压机分开到位 bool
        /// </summary>
        public bool PressIsDivide
        {
            get { return pressIsDivide; }
            set
            {
                if (pressIsDivide != value)
                {
                    pressIsDivide = value;
                    OnPropertyChanged(nameof(PressIsDivide));
                }
            }
        }

        /// <summary>
        /// 烘箱1保温剩余时间
        /// </summary>
        public int HeatPreservationResidueMinute
        {
            get { return heatPreservationResidueMinute; }
            set
            {
                if (heatPreservationResidueMinute != value)
                {
                    heatPreservationResidueMinute = value;
                    OnPropertyChanged(nameof(HeatPreservationResidueMinute));
                }
            }
        }

        /// <summary>
        /// 烘箱2保温剩余时间
        /// </summary>
        public int HeatPreservationResidueMinute2
        {
            get { return heatPreservationResidueMinute2; }
            set
            {
                if (heatPreservationResidueMinute2 != value)
                {
                    heatPreservationResidueMinute2 = value;
                    OnPropertyChanged(nameof(HeatPreservationResidueMinute2));
                }
            }
        }



        /// <summary>
        /// 烘箱1分子泵输出频率 float
        /// </summary>
        public float OvenBox1OutputFrequency
        {
            get { return ovenBox1OutputFrequency; }
            set
            {
                if (ovenBox1OutputFrequency != value)
                {
                    ovenBox1OutputFrequency = value;
                    OnPropertyChanged(nameof(OvenBox1OutputFrequency));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵输出电压 float
        /// </summary>
        public float OvenBox1OutputVoltage
        {
            get { return ovenBox1OutputVoltage; }
            set
            {
                if (ovenBox1OutputVoltage != value)
                {
                    ovenBox1OutputVoltage = value;
                    OnPropertyChanged(nameof(OvenBox1OutputVoltage));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵输出电流 float
        /// </summary>
        public float OvenBox1OutputCurrent
        {
            get { return ovenBox1OutputCurrent; }
            set
            {
                if (ovenBox1OutputCurrent != value)
                {
                    ovenBox1OutputCurrent = value;
                    OnPropertyChanged(nameof(OvenBox1OutputCurrent));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵待机状态 bool
        /// </summary>
        public bool OvenBox1Standbymode
        { 
            get { return ovenBox1Standbymode; }
            set
            {
                if (ovenBox1Standbymode != value)
                {
                    ovenBox1Standbymode = value;
                    OnPropertyChanged(nameof(OvenBox1Standbymode));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵运行状态 bool
        /// </summary>
        public bool OvenBox1Function
        {
            get { return ovenBox1Function; }
            set
            {
                if (ovenBox1Function != value)
                {
                    ovenBox1Function = value;
                    OnPropertyChanged(nameof(OvenBox1Function));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵错误状态 bool
        /// </summary>
        public bool OvenBox1err
        {
            get { return ovenBox1err; }
            set
            {
                if (ovenBox1err != value)
                {
                    ovenBox1err = value;
                    OnPropertyChanged(nameof(OvenBox1err));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵短路保护状态 bool
        /// </summary>
        public bool OvenBox1OC
        {
            get { return ovenBox1OC; }
            set
            {
                if (ovenBox1OC != value)
                {
                    ovenBox1OC = value;
                    OnPropertyChanged(nameof(OvenBox1OC));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵直流过压状态 bool
        /// </summary>
        public bool OvenBox1OE
        {
            get { return ovenBox1OE; }
            set
            {
                if (ovenBox1OE != value)
                {
                    ovenBox1OE = value;
                    OnPropertyChanged(nameof(OvenBox1OE));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵保持状态 bool
        /// </summary>
        public bool OvenBox1Retain
        {
            get { return ovenBox1Retain; }
            set
            {
                if (ovenBox1Retain != value)
                {
                    ovenBox1Retain = value;
                    OnPropertyChanged(nameof(OvenBox1Retain));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵RLU状态 bool
        /// </summary>
        public bool OvenBox1RLU
        {
            get { return ovenBox1RLU; }
            set
            {
                if (ovenBox1RLU != value)
                {
                    ovenBox1RLU = value;
                    OnPropertyChanged(nameof(OvenBox1RLU));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵过流状态 bool
        /// </summary>
        public bool OvenBox1OL2
        {
            get { return ovenBox1OL2; }
            set
            {
                if (ovenBox1OL2 != value)
                {
                    ovenBox1OL2 = value;
                    OnPropertyChanged(nameof(OvenBox1OL2));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵过载状态 bool
        /// </summary>
        public bool OvenBox1SL
        {
            get { return ovenBox1SL; }
            set
            {
                if (ovenBox1SL != value)
                {
                    ovenBox1SL = value;
                    OnPropertyChanged(nameof(OvenBox1SL));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵过热状态 bool
        /// </summary>
        public bool OvenBox1ESP
        {
            get { return ovenBox1ESP; }
            set
            {
                if (ovenBox1ESP != value)
                {
                    ovenBox1ESP = value;
                    OnPropertyChanged(nameof(OvenBox1ESP));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵欠压状态 bool
        /// </summary>
        public bool OvenBox1LU
        {
            get { return ovenBox1LU; }
            set
            {
                if (ovenBox1LU != value)
                {
                    ovenBox1LU = value;
                    OnPropertyChanged(nameof(OvenBox1LU));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵控制器过热状态 bool
        /// </summary>
        public bool OvenBox1OH
        {
            get { return ovenBox1OH; }
            set
            {
                if (ovenBox1OH != value)
                {
                    ovenBox1OH = value;
                    OnPropertyChanged(nameof(OvenBox1OH));
                }
            }
        }



        /// <summary>
        /// 烘箱1分子泵输出频率 float
        /// </summary>
        public float OvenBox2OutputFrequency
        {
            get { return ovenBox2OutputFrequency; }
            set
            {
                if (ovenBox2OutputFrequency != value)
                {
                    ovenBox2OutputFrequency = value;
                    OnPropertyChanged(nameof(OvenBox2OutputFrequency));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵输出电压 float
        /// </summary>
        public float OvenBox2OutputVoltage
        {
            get { return ovenBox2OutputVoltage; }
            set
            {
                if (ovenBox2OutputVoltage != value)
                {
                    ovenBox2OutputVoltage = value;
                    OnPropertyChanged(nameof(OvenBox2OutputVoltage));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵输出电流 float
        /// </summary>
        public float OvenBox2OutputCurrent
        {
            get { return ovenBox2OutputCurrent; }
            set
            {
                if (ovenBox2OutputCurrent != value)
                {
                    ovenBox2OutputCurrent = value;
                    OnPropertyChanged(nameof(OvenBox2OutputCurrent));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵待机状态 bool
        /// </summary>
        public bool OvenBox2Standbymode
        {
            get { return ovenBox2Standbymode; }
            set
            {
                if (ovenBox2Standbymode != value)
                {
                    ovenBox2Standbymode = value;
                    OnPropertyChanged(nameof(OvenBox2Standbymode));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵运行状态 bool
        /// </summary>
        public bool OvenBox2Function
        {
            get { return ovenBox2Function; }
            set
            {
                if (ovenBox2Function != value)
                {
                    ovenBox2Function = value;
                    OnPropertyChanged(nameof(OvenBox2Function));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵错误状态 bool
        /// </summary>
        public bool OvenBox2err
        {
            get { return ovenBox2err; }
            set
            {
                if (ovenBox2err != value)
                {
                    ovenBox2err = value;
                    OnPropertyChanged(nameof(OvenBox2err));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵短路保护状态 bool
        /// </summary>
        public bool OvenBox2OC
        {
            get { return ovenBox2OC; }
            set
            {
                if (ovenBox2OC != value)
                {
                    ovenBox2OC = value;
                    OnPropertyChanged(nameof(OvenBox2OC));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵直流过压状态 bool
        /// </summary>
        public bool OvenBox2OE
        {
            get { return ovenBox2OE; }
            set
            {
                if (ovenBox2OE != value)
                {
                    ovenBox2OE = value;
                    OnPropertyChanged(nameof(OvenBox2OE));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵保持状态 bool
        /// </summary>
        public bool OvenBox2Retain
        {
            get { return ovenBox2Retain; }
            set
            {
                if (ovenBox2Retain != value)
                {
                    ovenBox2Retain = value;
                    OnPropertyChanged(nameof(OvenBox2Retain));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵RLU状态 bool
        /// </summary>
        public bool OvenBox2RLU
        {
            get { return ovenBox2RLU; }
            set
            {
                if (ovenBox2RLU != value)
                {
                    ovenBox2RLU = value;
                    OnPropertyChanged(nameof(OvenBox2RLU));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵过流状态 bool
        /// </summary>
        public bool OvenBox2OL2
        {
            get { return ovenBox2OL2; }
            set
            {
                if (ovenBox2OL2 != value)
                {
                    ovenBox2OL2 = value;
                    OnPropertyChanged(nameof(OvenBox2OL2));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵过载状态 bool
        /// </summary>
        public bool OvenBox2SL
        {
            get { return ovenBox2SL; }
            set
            {
                if (ovenBox2SL != value)
                {
                    ovenBox2SL = value;
                    OnPropertyChanged(nameof(OvenBox2SL));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵过热状态 bool
        /// </summary>
        public bool OvenBox2ESP
        {
            get { return ovenBox2ESP; }
            set
            {
                if (ovenBox2ESP != value)
                {
                    ovenBox2ESP = value;
                    OnPropertyChanged(nameof(OvenBox2ESP));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵欠压状态 bool
        /// </summary>
        public bool OvenBox2LU
        {
            get { return ovenBox2LU; }
            set
            {
                if (ovenBox2LU != value)
                {
                    ovenBox2LU = value;
                    OnPropertyChanged(nameof(OvenBox2LU));
                }
            }
        }
        /// <summary>
        /// 烘箱1分子泵控制器过热状态 bool
        /// </summary>
        public bool OvenBox2OH
        {
            get { return ovenBox2OH; }
            set
            {
                if (ovenBox2OH != value)
                {
                    ovenBox2OH = value;
                    OnPropertyChanged(nameof(OvenBox2OH));
                }
            }
        }



        #endregion

        #region 订阅事件

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OnInnerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged($"MaterialMapLog.{e.PropertyName}");
        }

        #endregion


        #region IO锁定

        public bool IOlock(EnumBoardcardDefineOutputIO iO, bool En = true)
        {
            if(iO == EnumBoardcardDefineOutputIO.BakeOvenPlugInValve && En)
            {
                if(BakeOvenInnerdoorOpenstatus)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            if (iO == EnumBoardcardDefineOutputIO.BakeOvenInnerdoorUpDown && En)
            {
                if (BakeOvenPlugInValveOpenstatus)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            if (iO == EnumBoardcardDefineOutputIO.BakeOven2PlugInValve && En)
            {
                if (BakeOven2InnerdoorOpenstatus)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            if (iO == EnumBoardcardDefineOutputIO.BakeOven2InnerdoorUpDown && En)
            {
                if (BakeOven2PlugInValveOpenstatus)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            if(iO == EnumBoardcardDefineOutputIO.CondenserPump && En)
            {
                if(!CondenserPumpSignal4)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


            return false;
        }

        public bool TurboMolecularPumpRun(int OvenNum = 1)
        {
            if(OvenNum == 1)
            {
                if (BakeOvenVacuum < 10 && BakeOvenPlugInValveOpenstatus && !BakeOvenCoarseExtractionValve && BakeOvenFrontStageValve && BakeOvenMechanicalPump)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if(OvenNum == 2)
            {
                if (BakeOven2Vacuum < 10 && BakeOven2PlugInValveOpenstatus && !BakeOven2CoarseExtractionValve && BakeOven2FrontStageValve && BakeOven2MechanicalPump)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return true;
        }

        #endregion

    }
}
