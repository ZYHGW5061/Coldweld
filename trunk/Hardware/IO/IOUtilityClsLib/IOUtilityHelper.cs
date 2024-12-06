using BoardCardControllerClsLib;
using CameraControllerClsLib;
using ConfigurationClsLib;
using DewPointMeterControllerClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TemperatureControllerClsLib;
using TurboMolecularPumpControllerClsLib;
using VacuumGaugeControllerClsLib;
using WestDragon.Framework.BaseLoggerClsLib;

namespace IOUtilityClsLib
{
    public class IOUtilityHelper
    {
        private static readonly object _lockObj = new object();
        private static volatile IOUtilityHelper _instance = null;
        private bool _enablePollingIO;
        private bool _enablePollingIO2;
        private bool _enablePollingIO3;
        private bool _enablePollingIO4;
        private bool _enablePollingIO5;
        private bool _enablePollingIO6;
        IBoardCardController _boardCardController;
        private TemperatureControllerManager _TemperatureControllerManager
        {
            get { return TemperatureControllerManager.Instance; }
        }

        private VacuumGaugeControllerManager _VacuumGaugeControllerManager
        {
            get { return VacuumGaugeControllerManager.Instance; }
        }

        private DewPointMeterControllerManager _DewPointMeterControllerManager
        {
            get { return DewPointMeterControllerManager.Instance; }
        }

        private TurboMolecularPumpControllerManager _TurboMolecularPumpControllerManager
        {
            get { return TurboMolecularPumpControllerManager.Instance; }
        }

        private CameraManager _CameraManager
        {
            get { return CameraManager.Instance; }
        }

        private Dictionary<EnumBoardcardDefineInputIO, string> _inputIOList;
        private Dictionary<EnumBoardcardDefineOutputIO, string> _outputIOList;
        public static IOUtilityHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new IOUtilityHelper();
                        }
                    }
                }
                return _instance;
            }
        }

        private IOUtilityHelper()
        {
            _inputIOList = new Dictionary<EnumBoardcardDefineInputIO, string>();

            //_inputIOList.Add(EnumBoardcardDefineInputIO.BakeOvenInnerdoorOpenstatus, "BakeOvenInnerdoorOpenstatus");
            //_inputIOList.Add(EnumBoardcardDefineInputIO.BakeOvenInnerdoorClosestatus, "BakeOvenInnerdoorClosestatus");
            //_inputIOList.Add(EnumBoardcardDefineInputIO.BakeOvenOuterdoorClosestatus, "BakeOvenOuterdoorClosestatus");


            //_inputIOList.Add(EnumBoardcardDefineInputIO.BakeOven2InnerdoorOpenstatus, "BakeOven2InnerdoorOpenstatus");
            //_inputIOList.Add(EnumBoardcardDefineInputIO.BakeOven2InnerdoorClosestatus, "BakeOven2InnerdoorClosestatus");
            //_inputIOList.Add(EnumBoardcardDefineInputIO.BakeOven2OuterdoorClosestatus, "BakeOven2OuterdoorClosestatus");

            //_inputIOList.Add(EnumBoardcardDefineInputIO.BoxOuterdoorClosetatus, "BoxOuterdoorCloseSta");

            //_inputIOList.Add(EnumBoardcardDefineInputIO.PressIsPress, "PressIsPress");
            //_inputIOList.Add(EnumBoardcardDefineInputIO.PressIsDivide, "PressIsDivide");


            //_outputIOList = new Dictionary<EnumBoardcardDefineOutputIO, string>();
            //_outputIOList.Add(EnumBoardcardDefineOutputIO.TowerRedLight, "TowerRedLight");
            //_outputIOList.Add(EnumBoardcardDefineOutputIO.TowerYellowLight, "TowerYellowLight");
            //_outputIOList.Add(EnumBoardcardDefineOutputIO.TowerGreenLight, "TowerGreenLight");

            //// Adding remaining IO from enum EnumBoardcardDefineOutputIO

            //_outputIOList.Add(EnumBoardcardDefineOutputIO.BakeOvenAerate, "BakeOvenAerate");


            ////烘箱2
            //_outputIOList.Add(EnumBoardcardDefineOutputIO.BakeOven2Aerate, "BakeOven2Aerate");


            //_outputIOList.Add(EnumBoardcardDefineOutputIO.BoxAerate, "BoxAerate");


            //_outputIOList.Add(EnumBoardcardDefineOutputIO.MotorBrake, "MotorBrake");

            _boardCardController = BoardCardManager.Instance.GetCurrentController();
        }
        public void Start()
        {

            Thread.Sleep(6000);

            DataModel.Instance.WeldMaterialNumber = SystemConfiguration.Instance.StatisticalDataConfig.WeldMaterialNumber;
            DataModel.Instance.PressWorkNumber = SystemConfiguration.Instance.StatisticalDataConfig.PressWorkNumber;
            DataModel.Instance.EquipmentOperatingTime = SystemConfiguration.Instance.StatisticalDataConfig.EquipmentOperatingTime;

            SQLiteProgram.Instance.Init();

            Thread.Sleep(1000);

            _enablePollingIO = true;
            Task.Run(new Action(ReadIOTask));

            _enablePollingIO2 = true;
            Task.Run(new Action(ReadIOTask2));

            _enablePollingIO3 = true;
            Task.Run(new Action(ReadIOTask3));

            _enablePollingIO4 = true;
            Task.Run(new Action(ReadIOTask4));

            

            _enablePollingIO5 = true;
            Task.Run(new Action(RecordData));

            _enablePollingIO6 = true;
            Task.Run(new Action(ReadIOTask6));

        }

        public void Stop()
        {
            _enablePollingIO = false;
            _enablePollingIO2 = false;
            _enablePollingIO3 = false;
            _enablePollingIO4 = false;
            _enablePollingIO5 = false;
            _enablePollingIO6 = false;
        }

        public bool IsTowerRedLightOn()
        {
            if (_boardCardController == null)
            {
                return false;
            }
            var ret = false;
            var status = 0;
            _boardCardController.IO_ReadOutput_2(11, (int)EnumBoardcardDefineOutputIO.TowerRedLight, out status);
            ret = status == 1;
            return ret;
        }
        /// <summary>
        /// 亮红灯
        /// </summary>
        public void TurnonTowerRedLight()
        {
            if (_boardCardController == null)
            {
                return;
            }
            _boardCardController.IO_WriteOutPut_2(11, (int)EnumBoardcardDefineOutputIO.TowerRedLight, 1);


        }
        /// <summary>
        /// 灭红灯
        /// </summary>
        public void TurnoffTowerRedLight()
        {
            if (_boardCardController == null)
            {
                return;
            }
            _boardCardController.IO_WriteOutPut_2(11, (int)EnumBoardcardDefineOutputIO.TowerRedLight, 0);
        }

        public bool IsTowerYellowLightOn()
        {
            var ret = false;
            var status = 0;
            _boardCardController.IO_ReadOutput_2(11, (int)EnumBoardcardDefineOutputIO.TowerYellowLight, out status);
            ret = status == 1;
            return ret;
        }
        /// <summary>
        /// 亮黄灯
        /// </summary>
        public void TurnonTowerYellowLight()
        {
            if(_boardCardController == null)
            {
                return;
            }
            _boardCardController.IO_WriteOutPut_2(11, (int)EnumBoardcardDefineOutputIO.TowerYellowLight, 1);
        }
        /// <summary>
        /// 灭黄灯
        /// </summary>
        public void TurnoffTowerYellowLight()
        {
            if (_boardCardController == null)
            {
                return;
            }
            _boardCardController.IO_WriteOutPut_2(11, (int)EnumBoardcardDefineOutputIO.TowerYellowLight, 0);
        }
        public bool IsTowerGreenLightOn()
        {
            if (_boardCardController == null)
            {
                return false;
            }
            var ret = false;
            var status = 0;
            _boardCardController.IO_ReadOutput_2(11, (int)EnumBoardcardDefineOutputIO.TowerGreenLight, out status);
            ret = status == 1;
            return ret;
        }
        /// <summary>
        /// 亮绿灯
        /// </summary>
        public void TurnonTowerGreenLight()
        {
            if (_boardCardController == null)
            {
                return;
            }
            _boardCardController.IO_WriteOutPut_2(11, (int)EnumBoardcardDefineOutputIO.TowerGreenLight, 1);
        }
        /// <summary>
        /// 灭绿灯
        /// </summary>
        public void TurnoffTowerGreenLight()
        {
            if (_boardCardController == null)
            {
                return;
            }
            _boardCardController.IO_WriteOutPut_2(11, (int)EnumBoardcardDefineOutputIO.TowerGreenLight, 0);
        }




        /// <summary>
        /// 读取IO状态的任务
        /// </summary>
        private void ReadIOTask()
        {
            while (_enablePollingIO)
            {
                if (_boardCardController != null)
                {
                    List<int> data = new List<int>(); ;
                    _boardCardController.IO_ReadAllInput_2(11, out data);
                    if (data.Count > 0)
                    {
                        ParseDataAndUpdateInputIOValue(data);
                    }
                    _boardCardController.IO_ReadAllOutput_2(11, out data);
                    if (data.Count > 0)
                    {
                        ParseDataAndUpdateOutputIOValue(data);
                    }
                }

                DataModel.Instance.StageAxisIsconnect = _boardCardController.IsConnect;
                Thread.Sleep(10);

                DataModel.Instance.StageIOIsconnect = _boardCardController.IO_GetGLinkCommStatus();
                Thread.Sleep(10);

            }
        }

        private void ReadIOTask2()
        {
            while (_enablePollingIO2)
            {
                ParseDataAndUpdateInputIOIntValueTemperature();


                Thread.Sleep(200);
            }
        }
        private void ReadIOTask3()
        {
            while (_enablePollingIO3)
            {
                ParseDataAndUpdateInputIOIntValueVacuumGauge();


                Thread.Sleep(200);
            }
        }
        private void ReadIOTask4()
        {
            while (_enablePollingIO4)
            {
                ParseDataAndUpdateInputIOIntValueTurboMolecularPump();


                Thread.Sleep(200);
            }
        }

        private void RecordData()
        {
            int I = 0;
            while (_enablePollingIO5)
            {
                Thread.Sleep(1000);
                //if(SystemConfiguration.Instance.JobConfig.RecogniseResulSaveOption == EnumRecogniseResulSaveOption.AllSave || SystemConfiguration.Instance.JobConfig.RecogniseResulSaveOption == EnumRecogniseResulSaveOption.SaveVacuum)
                //{
                //    LogRecorder.RecordData1Log(EnumLogContentType.Info, "烘箱A真空度：" + DataModel.Instance.BakeOvenVacuum);
                //    LogRecorder.RecordData2Log(EnumLogContentType.Info, "烘箱B真空度：" + DataModel.Instance.BakeOven2Vacuum);
                //    LogRecorder.RecordData3Log(EnumLogContentType.Info, "方舱真空度：" + DataModel.Instance.BoxVacuum);



                //}

                I++;
                if(I > 30)
                {
                    string tablename = "VacuumsData";
                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string currentTime = DateTime.Now.ToString("HH:mm:ss");
                    float vacuum1 = DataModel.Instance.BakeOvenVacuum;
                    float vacuum2 = DataModel.Instance.BakeOven2Vacuum;
                    float vacuum3 = DataModel.Instance.BoxVacuum;

                    Dictionary<string, SQLData> tableDictionarys_0 = new Dictionary<string, SQLData>
                {
                    { "Date", new SQLData(currentDate, SQLDataType.STRING) },
                    { "Time", new SQLData(currentTime, SQLDataType.STRING) },
                    { "Vacuum1", new SQLData(vacuum1, SQLDataType.FLOAT) },
                    { "Vacuum2", new SQLData(vacuum2, SQLDataType.FLOAT) },
                    { "Vacuum3", new SQLData(vacuum3, SQLDataType.FLOAT) },
                };

                    SQLiteProgram.Instance.AddData(tablename, tableDictionarys_0);


                    string tablename2 = "TemperatureData";
                    float temp1 = DataModel.Instance.BakeOvenDowntemp;
                    float temp2 = DataModel.Instance.BakeOven2Downtemp;

                    Dictionary<string, SQLData> tableDictionarys_1 = new Dictionary<string, SQLData>
                {
                    { "Date", new SQLData(currentDate, SQLDataType.STRING) },
                    { "Time", new SQLData(currentTime, SQLDataType.STRING) },
                    { "Temperature1", new SQLData(temp1, SQLDataType.FLOAT) },
                    { "Temperature2", new SQLData(temp2, SQLDataType.FLOAT) },
                };

                    SQLiteProgram.Instance.AddData(tablename2, tableDictionarys_1);


                    I = 0;
                }


            }

        }

        private void ReadIOTask6()
        {
            DateTime startTime = DateTime.Now;
            while (_enablePollingIO6)
            {
                DateTime time = DateTime.Now;
                DataModel.Instance.Sysdatetime = time.ToShortDateString() + "  " + time.ToLongTimeString();

                if ((time - startTime).TotalMinutes >= 1)
                {
                    DataModel.Instance.EquipmentOperatingTime++;
                    startTime = DateTime.Now;
                }

                Thread.Sleep(1000);
            }
        }


        /// <summary>
        /// 解析字符串并更新该IO点Value值
        /// </summary>
        internal void ParseDataAndUpdateInputIOValue(List<int> msg)
        {
            DataModel.Instance.BakeOvenPlugInValveOpenstatus = msg[(int)EnumBoardcardDefineInputIO.BakeOvenPlugInValveOpenstatus] == 1 ? true : false;
            DataModel.Instance.BakeOvenPlugInValveClosestatus = msg[(int)EnumBoardcardDefineInputIO.BakeOvenPlugInValveClosestatus] == 1 ? true : false;
            DataModel.Instance.BakeOvenInnerdoorOpenstatus = msg[(int)EnumBoardcardDefineInputIO.BakeOvenInnerdoorOpenstatus] == 1 ? true : false;
            DataModel.Instance.BakeOvenInnerdoorClosestatus = msg[(int)EnumBoardcardDefineInputIO.BakeOvenInnerdoorClosestatus] == 1 ? true : false;
            DataModel.Instance.BakeOvenOuterdoorClosestatus = msg[(int)EnumBoardcardDefineInputIO.BakeOvenOuterdoorClosestatus] == 1 ? true : false;
            DataModel.Instance.BakeOvenPressureSensor = msg[(int)EnumBoardcardDefineInputIO.BakeOvenPressureSensor] == 1 ? true : false;

            DataModel.Instance.BakeOven2PlugInValveOpenstatus = msg[(int)EnumBoardcardDefineInputIO.BakeOven2PlugInValveOpenstatus] == 1 ? true : false;
            DataModel.Instance.BakeOven2PlugInValveClosestatus = msg[(int)EnumBoardcardDefineInputIO.BakeOven2PlugInValveClosestatus] == 1 ? true : false;
            DataModel.Instance.BakeOven2InnerdoorOpenstatus = msg[(int)EnumBoardcardDefineInputIO.BakeOven2InnerdoorOpenstatus] == 1 ? true : false;
            DataModel.Instance.BakeOven2InnerdoorClosestatus = msg[(int)EnumBoardcardDefineInputIO.BakeOven2InnerdoorClosestatus] == 1 ? true : false;
            DataModel.Instance.BakeOven2OuterdoorClosestatus = msg[(int)EnumBoardcardDefineInputIO.BakeOven2OuterdoorClosestatus] == 1 ? true : false;
            DataModel.Instance.BakeOven2PressureSensor = msg[(int)EnumBoardcardDefineInputIO.BakeOven2PressureSensor] == 1 ? true : false;

            DataModel.Instance.BoxPlugInValveOpenstatus = msg[(int)EnumBoardcardDefineInputIO.BoxPlugInValveOpenstatus] == 1 ? true : false;
            DataModel.Instance.BoxPlugInValveClosestatus = msg[(int)EnumBoardcardDefineInputIO.BoxPlugInValveClosestatus] == 1 ? true : false;
            DataModel.Instance.BoxOuterdoorClosetatus = msg[(int)EnumBoardcardDefineInputIO.BoxOuterdoorClosetatus] == 1 ? true : false;
            DataModel.Instance.PressIsPress = msg[(int)EnumBoardcardDefineInputIO.PressIsPress] == 1 ? true : false;
            DataModel.Instance.PressIsDivide = msg[(int)EnumBoardcardDefineInputIO.PressIsDivide] == 1 ? true : false;
            DataModel.Instance.CondenserPumpSignal1 = msg[(int)EnumBoardcardDefineInputIO.CondenserPumpSignal1] == 1 ? true : false;
            DataModel.Instance.CondenserPumpSignal2 = msg[(int)EnumBoardcardDefineInputIO.CondenserPumpSignal2] == 1 ? true : false;
            DataModel.Instance.CondenserPumpSignal3 = msg[(int)EnumBoardcardDefineInputIO.CondenserPumpSignal3] == 1 ? true : false;
            DataModel.Instance.CondenserPumpSignal4 = msg[(int)EnumBoardcardDefineInputIO.CondenserPumpSignal4] == 1 ? true : false;
            DataModel.Instance.CondenserPumpSignal5 = msg[(int)EnumBoardcardDefineInputIO.CondenserPumpSignal5] == 1 ? true : false;
            DataModel.Instance.CondenserPumpSignal6 = msg[(int)EnumBoardcardDefineInputIO.CondenserPumpSignal6] == 1 ? true : false;
            DataModel.Instance.CompressorAlarm = msg[(int)EnumBoardcardDefineInputIO.CompressorAlarm] == 1 ? true : false;
            DataModel.Instance.CondenserStar = msg[(int)EnumBoardcardDefineInputIO.CondenserStar] == 1 ? true : false;
            DataModel.Instance.ThermalRelay = msg[(int)EnumBoardcardDefineInputIO.ThermalRelay] == 1 ? true : false;
            DataModel.Instance.BoxPressureSensor = msg[(int)EnumBoardcardDefineInputIO.BoxPressureSensor] == 1 ? true : false;


            Thread.Sleep(20);
        }
        internal void ParseDataAndUpdateOutputIOValue(List<int> msg)
        {

            DataModel.Instance.TowerYellowLight = msg[(int)EnumBoardcardDefineOutputIO.TowerYellowLight] == 1 ? true : false;
            DataModel.Instance.TowerGreenLight = msg[(int)EnumBoardcardDefineOutputIO.TowerGreenLight] == 1 ? true : false;
            DataModel.Instance.TowerRedLight = msg[(int)EnumBoardcardDefineOutputIO.TowerRedLight] == 1 ? true : false;

            DataModel.Instance.BakeOvenAerate = msg[(int)EnumBoardcardDefineOutputIO.BakeOvenAerate] == 1 ? true : false;
            DataModel.Instance.BakeOvenCoarseExtractionValve = msg[(int)EnumBoardcardDefineOutputIO.BakeOvenCoarseExtractionValve] == 1 ? true : false;
            DataModel.Instance.BakeOvenFrontStageValve = msg[(int)EnumBoardcardDefineOutputIO.BakeOvenFrontStageValve] == 1 ? true : false;
            DataModel.Instance.BakeOvenPlugInValve = msg[(int)EnumBoardcardDefineOutputIO.BakeOvenPlugInValve] == 1 ? true : false;
            DataModel.Instance.BakeOvenMechanicalPump = msg[(int)EnumBoardcardDefineOutputIO.BakeOvenMechanicalPump] == 1 ? true : false;
            DataModel.Instance.BakeOvenInnerdoorUpDown = msg[(int)EnumBoardcardDefineOutputIO.BakeOvenInnerdoorUpDown] == 1 ? true : false;

            DataModel.Instance.BakeOven2Aerate = msg[(int)EnumBoardcardDefineOutputIO.BakeOven2Aerate] == 1 ? true : false;
            DataModel.Instance.BakeOven2CoarseExtractionValve = msg[(int)EnumBoardcardDefineOutputIO.BakeOven2CoarseExtractionValve] == 1 ? true : false;
            DataModel.Instance.BakeOven2FrontStageValve = msg[(int)EnumBoardcardDefineOutputIO.BakeOven2FrontStageValve] == 1 ? true : false;
            DataModel.Instance.BakeOven2PlugInValve = msg[(int)EnumBoardcardDefineOutputIO.BakeOven2PlugInValve] == 1 ? true : false;
            DataModel.Instance.BakeOven2MechanicalPump = msg[(int)EnumBoardcardDefineOutputIO.BakeOven2MechanicalPump] == 1 ? true : false;
            DataModel.Instance.BakeOven2InnerdoorUpDown = msg[(int)EnumBoardcardDefineOutputIO.BakeOven2InnerdoorUpDown] == 1 ? true : false;

            DataModel.Instance.BoxAerate = msg[(int)EnumBoardcardDefineOutputIO.BoxAerate] == 1 ? true : false;
            DataModel.Instance.BoxCoarseExtractionValve = msg[(int)EnumBoardcardDefineOutputIO.BoxCoarseExtractionValve] == 1 ? true : false;
            DataModel.Instance.BoxFrontStageValve = msg[(int)EnumBoardcardDefineOutputIO.BoxFrontStageValve] == 1 ? true : false;
            DataModel.Instance.BoxPlugInValve = msg[(int)EnumBoardcardDefineOutputIO.BoxPlugInValve] == 1 ? true : false;
            DataModel.Instance.BoxMechanicalPump = msg[(int)EnumBoardcardDefineOutputIO.BoxMechanicalPump] == 1 ? true : false;
            DataModel.Instance.PressPressingDivide = msg[(int)EnumBoardcardDefineOutputIO.PressPressingDivide] == 1 ? true : false;

            DataModel.Instance.CompressorStartup = msg[(int)EnumBoardcardDefineOutputIO.CompressorStartup] == 1 ? true : false;
            DataModel.Instance.CompressorStops = msg[(int)EnumBoardcardDefineOutputIO.CompressorStops] == 1 ? true : false;
            DataModel.Instance.CondenserPump = msg[(int)EnumBoardcardDefineOutputIO.CondenserPump] == 1 ? true : false;
            DataModel.Instance.ReductionIN = msg[(int)EnumBoardcardDefineOutputIO.ReductionIN] == 1 ? true : false;
            DataModel.Instance.ReductionOUT = msg[(int)EnumBoardcardDefineOutputIO.ReductionOUT] == 1 ? true : false;
            DataModel.Instance.CondenserPumpHeat = msg[(int)EnumBoardcardDefineOutputIO.CondenserPumpHeat] == 1 ? true : false;

            DataModel.Instance.MotorBrake = msg[(int)EnumBoardcardDefineOutputIO.MotorBrake] == 1 ? true : false;
            DataModel.Instance.MotorBrake1 = msg[(int)EnumBoardcardDefineOutputIO.MotorBrake1] == 1 ? true : false;

            Thread.Sleep(20);

        }


        internal void ParseDataAndUpdateInputIOIntValueTemperature()
        { 


            if(_CameraManager.AllCamera.Count > 0)
            {
                bool Camera1connect = true, Camera2connect = true;

                if(!_CameraManager.GetCameraByID( EnumCameraType.TrackCamera).IsConnect)
                {
                    Camera1connect = false;
                }
                Thread.Sleep(50);

                if (!_CameraManager.GetCameraByID(EnumCameraType.WeldCamera).IsConnect)
                {
                    Camera2connect = false;
                }
                Thread.Sleep(50);

                if (Camera1connect && Camera2connect)
                {
                    DataModel.Instance.CameraIsconnect = true;
                }
                else
                {
                    DataModel.Instance.CameraIsconnect = false;
                }

            }


            if (_TemperatureControllerManager.AllTemperatures.Count > 0)
            {
                if (!DataModel.Instance.TemperatureIsWriting)
                {
                    DataModel.Instance.TemperatureIsReading = true;

                    bool To1connect = true, To2connect = true;

                    if (_TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).IsConnect)
                    {
                        int data = -1000;
                        float BakeOvenDowntemp = -1000;
                        bool ret = _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Read(TemperatureRtuAdd.PV, ref data);
                        if (ret && data > -1000)
                        {
                            BakeOvenDowntemp = data / 10;
                            DataModel.Instance.BakeOvenDowntemp = BakeOvenDowntemp;
                        }


                        Thread.Sleep(50);

                        //float BakeOvenDowntemp = _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Read(TemperatureRtuAdd.PV);
                        //IOManager.Instance.ChangeIOValue("BakeOvenDowntemp", BakeOvenDowntemp);

                        data = -1000;
                        ret = (_TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Read(TemperatureRtuAdd.EV_FLG, ref data));
                        if (ret && data > -1)
                        {
                            bool BakeOvenAutoHeatstatus = data > 0;
                            DataModel.Instance.BakeOvenAutoHeat = BakeOvenAutoHeatstatus;
                        }


                        Thread.Sleep(50);
                    }
                    else
                    {
                        To1connect = false;
                    }

                    if (_TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).IsConnect)
                    {
                        int data = -1000;
                        float BakeOvenDowntemp = -1000;
                        bool ret = _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Read(TemperatureRtuAdd.PV, ref data);
                        if (ret && data > -1000)
                        {
                            BakeOvenDowntemp = data / 10;
                            DataModel.Instance.BakeOven2Downtemp = BakeOvenDowntemp;
                        }


                        Thread.Sleep(50);

                        //float BakeOvenDowntemp = _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Read(TemperatureRtuAdd.PV);
                        //IOManager.Instance.ChangeIOValue("BakeOvenDowntemp", BakeOvenDowntemp);

                        data = -1000;
                        ret = (_TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Read(TemperatureRtuAdd.EV_FLG, ref data));
                        if (ret && data > -1)
                        {
                            bool BakeOvenAutoHeatstatus = data > 0;
                            DataModel.Instance.BakeOven2AutoHeat = BakeOvenAutoHeatstatus;
                        }


                        Thread.Sleep(50);
                    }
                    else
                    {
                        To2connect = false;
                    }

                    if (To1connect && To2connect)
                    {
                        DataModel.Instance.TemperatureIsconnect = true;
                    }
                    else
                    {
                        DataModel.Instance.TemperatureIsconnect = false;
                    }


                    DataModel.Instance.TemperatureIsReading = false;
                }
            }


        }

        internal void ParseDataAndUpdateInputIOIntValueVacuumGauge()
        {

            if (_VacuumGaugeControllerManager.AllVacuumGauges.Count > 0)
            {
                if (!DataModel.Instance.VacuumIsWriting)
                {
                    DataModel.Instance.VacuumIsReading = true;
                    bool Vo1connect = true, Vo2connect = true, Vo3connect = true;

                    if (_VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox1).IsConnect)
                    {
                        float Vacuum1 = -1;
                        bool ret = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox1).ReadVacuum(ref Vacuum1);
                        if (ret && Vacuum1 > 0)
                        {
                            DataModel.Instance.BakeOvenVacuum = Vacuum1;
                        }


                        Thread.Sleep(50);
                    }
                    else
                    {
                        Vo1connect = false;
                    }

                    if (_VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox2).IsConnect)
                    {

                        float Vacuum2 = 0;
                        bool ret = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox2).ReadVacuum(ref Vacuum2);
                        if (ret && Vacuum2 > 0)
                        {
                            DataModel.Instance.BakeOven2Vacuum = Vacuum2;
                        }

                        Thread.Sleep(50);
                    }
                    else
                    {
                        Vo2connect = false;
                    }

                    if (_VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.Box).IsConnect)
                    {

                        float Vacuum3 = 0;
                        bool ret = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.Box).ReadVacuum(ref Vacuum3);
                        if (ret && Vacuum3 > 0)
                        {
                            DataModel.Instance.BoxVacuum = Vacuum3;
                        }

                        Thread.Sleep(50);
                    }
                    else
                    {
                        Vo3connect = false;
                    }

                    if (Vo1connect && Vo2connect && Vo3connect)
                    {
                        DataModel.Instance.VacuumIsconnect = true;
                    }
                    else
                    {
                        DataModel.Instance.VacuumIsconnect = false;
                    }

                    DataModel.Instance.VacuumIsReading = false;
                }


            }


        }

        internal void ParseDataAndUpdateInputIOIntValueTurboMolecularPump()
        {

            if (_TurboMolecularPumpControllerManager.AllTurboMolecularPumps.Count > 0)
            {
                if (!DataModel.Instance.TurboMolecularPumpIsWriting)
                {
                    DataModel.Instance.TurboMolecularPumpIsReading = true;

                    bool Tuo1connect = true, Tuo2connect = true;

                    if (_TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).IsConnect)
                    {
                        TurboMolecularPumpstatus Value = null;
                        bool ret = _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).ReadStatus(ref Value);
                        if (Value == null || !ret)
                        {

                        }
                        else
                        {
                            DataModel.Instance.OvenBox1OutputFrequency = Value.OutputFrequency;
                            DataModel.Instance.OvenBox1OutputVoltage = Value.OutputVoltage;
                            DataModel.Instance.OvenBox1OutputCurrent = Value.OutputCurrent;
                            DataModel.Instance.OvenBox1Standbymode = Value.Standbymode;
                            DataModel.Instance.OvenBox1Function = Value.Function;
                            DataModel.Instance.OvenBox1err = Value.err;
                            DataModel.Instance.OvenBox1OC = Value.OC;
                            DataModel.Instance.OvenBox1OE = Value.OE;
                            DataModel.Instance.OvenBox1Retain = Value.Retain;
                            DataModel.Instance.OvenBox1RLU = Value.RLU;
                            DataModel.Instance.OvenBox1OL2 = Value.OL2;
                            DataModel.Instance.OvenBox1SL = Value.SL;
                            DataModel.Instance.OvenBox1ESP = Value.ESP;
                            DataModel.Instance.OvenBox1LU = Value.LU;
                            DataModel.Instance.OvenBox1OH = Value.OH;
                        }
                    }
                    else
                    {
                        Tuo1connect = false;
                    }

                    if (_TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).IsConnect)
                    {
                        TurboMolecularPumpstatus Value = null;
                        bool ret = _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).ReadStatus(ref Value);
                        if (Value == null || !ret)
                        {

                        }
                        else
                        {
                            DataModel.Instance.OvenBox2OutputFrequency = Value.OutputFrequency;
                            DataModel.Instance.OvenBox2OutputVoltage = Value.OutputVoltage;
                            DataModel.Instance.OvenBox2OutputCurrent = Value.OutputCurrent;
                            DataModel.Instance.OvenBox2Standbymode = Value.Standbymode;
                            DataModel.Instance.OvenBox2Function = Value.Function;
                            DataModel.Instance.OvenBox2err = Value.err;
                            DataModel.Instance.OvenBox2OC = Value.OC;
                            DataModel.Instance.OvenBox2OE = Value.OE;
                            DataModel.Instance.OvenBox2Retain = Value.Retain;
                            DataModel.Instance.OvenBox2RLU = Value.RLU;
                            DataModel.Instance.OvenBox2OL2 = Value.OL2;
                            DataModel.Instance.OvenBox2SL = Value.SL;
                            DataModel.Instance.OvenBox2ESP = Value.ESP;
                            DataModel.Instance.OvenBox2LU = Value.LU;
                            DataModel.Instance.OvenBox2OH = Value.OH;
                        }

                    }
                    else
                    {
                        Tuo2connect = false;
                    }


                    if (Tuo1connect && Tuo2connect)
                    {
                        DataModel.Instance.TurboMolecularPumpIsconnect = true;
                    }
                    else
                    {
                        DataModel.Instance.TurboMolecularPumpIsconnect = false;
                    }


                    DataModel.Instance.TurboMolecularPumpIsReading = false;
                }


            }

            if (_DewPointMeterControllerManager.AllDewPointMeters.Count > 0)
            {

                if (_DewPointMeterControllerManager.GetDewPointMeterController(EnumDewPointMeterType.Box).IsConnect)
                {
                    float DewPoint = _DewPointMeterControllerManager.GetDewPointMeterController(EnumDewPointMeterType.Box).ReadDewPoint();
                    DataModel.Instance.BoxDewPoint = DewPoint;

                    Thread.Sleep(50);
                }


            }



        }

    }


}
