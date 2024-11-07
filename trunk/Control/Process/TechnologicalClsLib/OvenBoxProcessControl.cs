using BoardCardControllerClsLib;
using CommonPanelClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TemperatureControllerClsLib;
using TurboMolecularPumpControllerClsLib;
using VacuumGaugeControllerClsLib;

namespace TechnologicalClsLib
{
    public class OvenBoxProcessControl
    {
        #region Private File

        private static readonly object _lockObj = new object();
        private static volatile OvenBoxProcessControl _instance = null;

        IBoardCardController _boardCardController;

        public static OvenBoxProcessControl Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new OvenBoxProcessControl();
                        }
                    }
                }
                return _instance;
            }
        }

        

        private OvenBoxProcessControl()
        {
            _boardCardController = BoardCardManager.Instance.GetCurrentController();
        }


        private TemperatureControllerManager _TemperatureControllerManager
        {
            get { return TemperatureControllerManager.Instance; }
        }

        private VacuumGaugeControllerManager _VacuumGaugeControllerManager
        {
            get { return VacuumGaugeControllerManager.Instance; }
        }

        private TurboMolecularPumpControllerManager _TurboMolecularPumpControllerManager
        {
            get { return TurboMolecularPumpControllerManager.Instance; }
        }



        private Thread heatthd;
        private Thread heatthd2;

        private Thread Doorthd;

        private Thread Weldthd;

        private Thread OvenBox1VacuumPumpingthd;

        private Thread OvenBox2VacuumPumpingthd;

        private Thread BoxVacuumPumpingthd;

        private Thread Oven1OutRemindthd;

        private Thread Oven2OutRemindthd;

        private Thread Oven1InRemindthd;

        private Thread Oven2InRemindthd;

        private Thread towerLampthd;

        private bool StopHeat = false;
        private bool StopHeat2 = false;

        private bool StopDoor = false;

        private bool Stopweld = false;

        private bool StopOvenBox1VacuumPumpingthd = false;

        private bool StopOvenBox2VacuumPumpingthd = false;

        private bool StopBoxVacuumPumpingthd = false;

        private bool StoptowerLampthd = false;


        //private StageCore stage = StageControllerClsLib.StageCore.Instance;
        ///// <summary>
        ///// 定位系统
        ///// </summary>
        //private PositioningSystem _positioningSystem
        //{
        //    get { return PositioningSystem.Instance; }
        //}

        //private CameraConfig _TrackCameraConfig
        //{
        //    get { return CameraManager.Instance.GetCameraConfigByID(EnumCameraType.TrackCamera); }
        //}
        //private CameraConfig _WeldCameraConfig
        //{
        //    get { return CameraManager.Instance.GetCameraConfigByID(EnumCameraType.WeldCamera); }
        //}

        //private SystemConfiguration _systemConfig
        //{
        //    get { return SystemConfiguration.Instance; }
        //}

        //private VisionControlAppClsLib.VisualControlManager _VisualManager
        //{
        //    get { return VisionControlAppClsLib.VisualControlManager.Instance; }
        //}
        //public VisualControlApplications TrackCameraVisual
        //{
        //    get { return _VisualManager.GetCameraByID(EnumCameraType.TrackCamera); }
        //}
        //public VisualControlApplications WeldCameraVisual
        //{
        //    get { return _VisualManager.GetCameraByID(EnumCameraType.WeldCamera); }
        //}


        //VisualControlForm VForm;

        #endregion

        #region Public File

        public EnumOvenBoxStates OvenBox1state { get; set; } = new EnumOvenBoxStates();
        public EnumOvenBoxStates OvenBox2state { get; set; } = new EnumOvenBoxStates();
        public bool IsConnected 
        {
            get 
            {
                return _boardCardController.IsConnect;
            }
        }


        

        #endregion


        #region Private Mothed

        private EnumOvenBoxStates ReadAllOvenBoxStatesMothed(EnumOvenBoxNum num)
        {
            try
            {
                EnumOvenBoxStates OvenBoxstate = new EnumOvenBoxStates();
                if (num == EnumOvenBoxNum.Oven1)
                {
                    //OvenBoxstate
                }
                else if(num == EnumOvenBoxNum.Oven2)
                {

                }

                return OvenBoxstate;
            }
            catch(Exception ex)
            {
                return null;
            }
            

        }

        private bool ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO name)
        {
            try
            {
                var ret = false;
                var status = 0;
                _boardCardController.IO_ReadInput_2(11, (int)name, out status);
                ret = status == 1;
                return ret;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private float ReadfloatOvenBoxStatesMothed(EnumBoardcardDefineInputIO name)
        {
            try
            {
                

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }


        }

        private short ReadshortOvenBoxStatesMothed(EnumBoardcardDefineInputIO name)
        {
            try
            {
                EnumOvenBoxStates OvenBoxstate = new EnumOvenBoxStates();

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }


        }

        private bool ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO name)
        {
            try
            {
                var ret = false;
                var status = 0;
                _boardCardController.IO_ReadOutput_2(11, (int)name, out status);
                ret = status == 1;
                return ret;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private float ReadfloatOvenBoxStatesMothed(EnumBoardcardDefineOutputIO name)
        {
            try
            {


                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }


        }

        private short ReadshortOvenBoxStatesMothed(EnumBoardcardDefineOutputIO name)
        {
            try
            {
                EnumOvenBoxStates OvenBoxstate = new EnumOvenBoxStates();

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }


        }


        private int WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO name, bool value)
        {
            try
            {
                int V = 0;
                if(value == false)
                {
                    V = 0;
                }
                else
                {
                    V = 1;
                }

                //_boardCardController.IO_WriteOutPut(11, (short)name, V);
                bool Done = DataModel.Instance.IOlock(name);

                if(Done)
                {
                    return -2;
                }

                _boardCardController.IO_WriteOutPut_2(11, (short)name, V);

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }


        }

        private int WritefloatOvenBoxStatesMothed(EnumBoardcardDefineOutputIO name, float value)
        {
            try
            {

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }


        }

        private int WriteshortOvenBoxStatesMothed(EnumBoardcardDefineOutputIO name, short value)
        {
            try
            {

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }


        }


        private void ManualHeatMothed(EnumOvenBoxNum num, int HeatTargetTemperature, int HeatPreservationMinute, int OverTemperatureThreshold)
        {
            
            //IOManager.Instance.ChangeIOValue("HeatPreservationResidueMinute", HeatPreservationMinute);
            DataModel.Instance.HeatPreservationResidueMinute = HeatPreservationMinute;

            if (num == EnumOvenBoxNum.Oven1)
            {
                

                if (_TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).IsConnect)
                {
                    DataModel.Instance.OvenBox1Heating = true;

                    _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Write(TemperatureRtuAdd.FIX_SV1, (int)HeatTargetTemperature * 10);

                    _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Write(TemperatureRtuAdd.RUN, 1);

                    DataModel.Instance.OvenBox1Heating = true;


                    Stopwatch stopwatch = new Stopwatch();

                    while (!StopHeat)
                    {
                        //float temperatureLow = _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Read(TemperatureRtuAdd.PV);
                        DataModel.Instance.OvenBox1Heating = true;
                        float temperatureLow = DataModel.Instance.BakeOvenDowntemp;
                        if (temperatureLow > (HeatTargetTemperature - 1))
                        {
                            break;
                        }
                        Thread.Sleep(200);
                    }

                    if(StopHeat)
                    {
                        _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Write(TemperatureRtuAdd.RUN, 0);

                        //IOManager.Instance.ChangeIOValue("HeatPreservationResidueMinute", 0);
                        DataModel.Instance.HeatPreservationResidueMinute = 0;

                        StopHeat = false;

                        DataModel.Instance.OvenBox1Heating = false;

                        return;
                    }


                    //float temperatureLow1 = _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Read(TemperatureRtuAdd.PV);
                    float temperatureLow1 = DataModel.Instance.BakeOvenDowntemp;
                    if (temperatureLow1 > (HeatTargetTemperature - 1))
                    {
                        stopwatch.Start();
                    }

                    while (!StopHeat)
                    {
                        if ((stopwatch.Elapsed.TotalSeconds / 60) > HeatPreservationMinute)
                        {
                            stopwatch.Stop();

                            break;
                        }

                        //IOManager.Instance.ChangeIOValue("HeatPreservationResidueMinute", HeatPreservationMinute - (stopwatch.Elapsed.TotalSeconds / 60));
                        DataModel.Instance.HeatPreservationResidueMinute = (int)(HeatPreservationMinute - (stopwatch.Elapsed.TotalSeconds / 60));

                        Thread.Sleep(500);

                    }

                    if(stopwatch.IsRunning)
                    {
                        stopwatch.Stop();
                    }

                    _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Write(TemperatureRtuAdd.RUN, 0);

                    //IOManager.Instance.ChangeIOValue("HeatPreservationResidueMinute", 0);
                    DataModel.Instance.HeatPreservationResidueMinute = 0;

                    StopHeat = false;

                    DataModel.Instance.OvenBox1Heating = false;
                }
            }
        }

        private void ManualHeatMothed2(EnumOvenBoxNum num, int HeatTargetTemperature, int HeatPreservationMinute, int OverTemperatureThreshold)
        {
            //IOManager.Instance.ChangeIOValue("HeatPreservationResidueMinute", HeatPreservationMinute);
            DataModel.Instance.HeatPreservationResidueMinute2 = HeatPreservationMinute;
            if (num == EnumOvenBoxNum.Oven2)
            {
                DataModel.Instance.OvenBox2Heating = true;

                if (_TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).IsConnect)
                {
                    _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Write(TemperatureRtuAdd.FIX_SV1, (int)HeatTargetTemperature * 10);

                    _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Write(TemperatureRtuAdd.RUN, 1);

                    DataModel.Instance.OvenBox2Heating = true;

                    Stopwatch stopwatch = new Stopwatch();

                    while (!StopHeat2)
                    {
                        //float temperatureLow = _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Read(TemperatureRtuAdd.PV);

                        float temperatureLow = DataModel.Instance.BakeOven2Downtemp;
                        if (temperatureLow > (HeatTargetTemperature - 1))
                        {
                            break;
                        }
                        Thread.Sleep(200);
                    }

                    if (StopHeat2)
                    {
                        _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Write(TemperatureRtuAdd.RUN, 0);

                        //IOManager.Instance.ChangeIOValue("HeatPreservationResidueMinute", 0);
                        DataModel.Instance.HeatPreservationResidueMinute2 = 0;

                        StopHeat2 = false;

                        DataModel.Instance.OvenBox2Heating = false;

                        return;
                    }


                    //float temperatureLow1 = _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Read(TemperatureRtuAdd.PV);
                    float temperatureLow1 = DataModel.Instance.BakeOven2Downtemp;
                    if (temperatureLow1 > (HeatTargetTemperature - 1))
                    {
                        stopwatch.Start();
                    }

                    while (!StopHeat2)
                    {
                        if ((stopwatch.Elapsed.TotalSeconds / 60) > HeatPreservationMinute)
                        {
                            stopwatch.Stop();

                            break;
                        }

                        //IOManager.Instance.ChangeIOValue("HeatPreservationResidueMinute", HeatPreservationMinute - (stopwatch.Elapsed.TotalSeconds / 60));
                        DataModel.Instance.HeatPreservationResidueMinute2 = (int)(HeatPreservationMinute - (stopwatch.Elapsed.TotalSeconds / 60));

                        Thread.Sleep(500);

                    }

                    if (stopwatch.IsRunning)
                    {
                        stopwatch.Stop();
                    }

                    _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Write(TemperatureRtuAdd.RUN, 0);

                    //IOManager.Instance.ChangeIOValue("HeatPreservationResidueMinute", 0);
                    DataModel.Instance.HeatPreservationResidueMinute2 = 0;

                    DataModel.Instance.OvenBox2Heating = false;

                    StopHeat2 = false;
                }
            }

        }


        private void AutoHeatMothed(EnumOvenBoxNum num, int HeatTargetTemperature, int HeatPreservationMinute, int OverTemperatureThreshold, int BakeOvenPFUpPressure, int BakeOvenPFDownPressure, int BakeOvenPFnum, int BakeOvenPFinterval)
        {
            IOManager.Instance.ChangeIOValue("HeatPreservationResidueMinute", HeatPreservationMinute);

            if (num == EnumOvenBoxNum.Oven1)
            {
            }
            else if (num == EnumOvenBoxNum.Oven2)
            {
            }


        }


        private void PurgeMothed(EnumOvenBoxNum num, int BakeOvenPFUpPressure, int BakeOvenPFDownPressure,int BakeOvenPFnum, int BakeOvenPFinterval)
        {
            if(num == EnumOvenBoxNum.Oven1)
            {
               

            }
            else if(num == EnumOvenBoxNum.Oven2)
            {
                
            }
        }

        private void BoxPurgeMothed(int BoxPFUpPressure, int BoxPFDownPressure, int BoxPFnum, int BoxPFinterval)
        {
            
        }


        private void OvenBox1VacuumPumpingMothed()
        {
            //抽真空前，需要关闭烘箱内门插板阀和烘箱外门
            bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOvenInnerdoorClosestatus);
            bool Done2 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOvenOuterdoorClosestatus);
            Done1 = DataModel.Instance.BakeOvenInnerdoorClosestatus;
            Done2 = DataModel.Instance.BakeOvenOuterdoorClosestatus;

            if (Done1 && Done2 && !StopOvenBox1VacuumPumpingthd)
            {
                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenFrontStageValve);
                Done1 = DataModel.Instance.BakeOvenFrontStageValve;
                if (Done1 && !StopOvenBox1VacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenFrontStageValve, false);//关闭前级阀
                }
                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenPlugInValve);
                Done1 = DataModel.Instance.BakeOvenPlugInValve;
                //Done2 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOvenPlugInValveClosestatus);
                Done2 = DataModel.Instance.BakeOvenPlugInValveClosestatus;
                if ((Done1 || !Done2) && !StopOvenBox1VacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenPlugInValve, false);//关闭插板阀
                    
                    while (!StopOvenBox1VacuumPumpingthd)
                    {
                        //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOvenPlugInValveClosestatus);
                        Done1 = DataModel.Instance.BakeOvenPlugInValveClosestatus;
                        if (Done1)
                        {
                            break;
                        }
                        Thread.Sleep(500);
                    }
                }
                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenMechanicalPump);
                Done1 = DataModel.Instance.BakeOvenMechanicalPump;
                if (!Done1 && !StopOvenBox1VacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenMechanicalPump, true);//打开机械泵
                }

                //float Vacuum = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox1).ReadVacuum();
                float Vacuum = DataModel.Instance.BakeOvenVacuum;
                if (Vacuum > 10)
                {
                    //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenCoarseExtractionValve);
                    Done1 = DataModel.Instance.BakeOvenCoarseExtractionValve;
                    if (!Done1 && !StopOvenBox1VacuumPumpingthd)
                    {
                        WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenCoarseExtractionValve, true);//打开粗抽阀
                    }
                }
                else if(Vacuum < 0)
                {
                    while (!StopOvenBox1VacuumPumpingthd)
                    {
                        //Vacuum = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox1).ReadVacuum();
                        Vacuum = DataModel.Instance.BakeOvenVacuum;
                        if (Vacuum < 10 && Vacuum > 0)
                        {
                            break;
                        }
                        if (Vacuum > 10)
                        {
                            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenCoarseExtractionValve);
                            Done1 = DataModel.Instance.BakeOvenCoarseExtractionValve;
                            if (!Done1)
                            {
                                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenCoarseExtractionValve, true);//打开粗抽阀
                            }
                            break;
                        }
                        Thread.Sleep(500);
                    }
                }

                while (!StopOvenBox1VacuumPumpingthd)
                {
                    //Vacuum = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox1).ReadVacuum();
                    Vacuum = DataModel.Instance.BakeOvenVacuum;
                    if (Vacuum < 10 && Vacuum > 0)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }
                if (StopOvenBox1VacuumPumpingthd)
                {
                    StopOvenBox1VacuumPumpingMothed();
                    return;
                }

                Done1 = DataModel.Instance.BakeOvenCoarseExtractionValve;
                if (Done1)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenCoarseExtractionValve, false);//关闭粗抽阀
                }

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenFrontStageValve);
                Done1 = DataModel.Instance.BakeOvenFrontStageValve;
                if (!Done1)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenFrontStageValve, true);//打开前级阀
                }
                

                int T = 0;
                while(!StopOvenBox1VacuumPumpingthd)
                {
                    Thread.Sleep(500);
                    T++;
                    if(T>120)
                    {
                        break;
                    }
                }

                if(StopOvenBox1VacuumPumpingthd)
                {
                    StopOvenBox1VacuumPumpingMothed();
                    return;
                }


                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenPlugInValve);
                //Done2 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOvenPlugInValveOpenstatus);
                Done1 = DataModel.Instance.BakeOvenPlugInValve;
                Done2 = DataModel.Instance.BakeOvenPlugInValveOpenstatus;
                if ((!Done1 || !Done2) && !StopOvenBox1VacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenPlugInValve, true);//打开插板阀

                    while (!StopOvenBox1VacuumPumpingthd)
                    {
                        //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOvenPlugInValveOpenstatus);
                        Done1 = DataModel.Instance.BakeOvenPlugInValveOpenstatus;
                        if (Done1)
                        {
                            break;
                        }
                        Thread.Sleep(500);
                    }
                }


                T = 0;
                while (!StopOvenBox1VacuumPumpingthd)
                {
                    Thread.Sleep(500);
                    T++;
                    if (T > 120)
                    {
                        break;
                    }
                }

                while (!StopOvenBox1VacuumPumpingthd)
                {
                    //Vacuum = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox1).ReadVacuum();
                    Vacuum = DataModel.Instance.BakeOvenVacuum;
                    if (Vacuum < 10 && Vacuum>0)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }
                if (StopOvenBox1VacuumPumpingthd)
                {
                    StopOvenBox1VacuumPumpingMothed();
                    return;
                }

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenCoarseExtractionValve);
                Done1 = DataModel.Instance.BakeOvenCoarseExtractionValve;
                if (Done1 && !StopOvenBox1VacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenCoarseExtractionValve, false);//关闭粗抽阀
                }
                if (StopOvenBox1VacuumPumpingthd)
                {
                    StopOvenBox1VacuumPumpingMothed();
                    return;
                }

                //TurboMolecularPumpstatus Value = _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).ReadStatus();
                //if (Value == null)
                //{
                //    while(!StopOvenBox1VacuumPumpingthd)
                //    {
                //        Value = _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).ReadStatus();
                //        if(Value != null)
                //        {
                //            break;
                //        }
                //        Thread.Sleep(500);
                //    }
                //    if (StopOvenBox1VacuumPumpingthd)
                //    {
                //        StopOvenBox1VacuumPumpingMothed();
                //        return;
                //    }
                //}
                //if (!Value.err)
                //{
                //    if(!Value.Function)
                //    {
                //        _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).Function();
                //    }
                //}

                while (!StopOvenBox1VacuumPumpingthd)
                {
                    if (!DataModel.Instance.OvenBox1err)
                    {
                        if (!DataModel.Instance.OvenBox1Function)
                        {
                            _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).Function();
                            break;
                        }
                    }

                    Thread.Sleep(500);

                }


                while (!StopOvenBox1VacuumPumpingthd)
                {
                    
                    
                    Thread.Sleep(500);
                    
                }
                if (StopOvenBox1VacuumPumpingthd)
                {
                    StopOvenBox1VacuumPumpingMothed();
                    return;
                }

            }

        }

        private void OvenBox2VacuumPumpingMothed()
        {
            //抽真空前，需要关闭烘箱内门插板阀和烘箱外门
            bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOven2InnerdoorClosestatus);
            bool Done2 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOven2OuterdoorClosestatus);
            Done1 = DataModel.Instance.BakeOven2InnerdoorClosestatus;
            Done2 = DataModel.Instance.BakeOven2OuterdoorClosestatus;

            if (Done1 && Done2 && !StopOvenBox2VacuumPumpingthd)
            {
                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2FrontStageValve);
                Done1 = DataModel.Instance.BakeOven2FrontStageValve;
                if (Done1 && !StopOvenBox2VacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2FrontStageValve, false);//关闭前级阀
                }
                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2PlugInValve);
                //Done2 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOven2PlugInValveClosestatus);
                Done1 = DataModel.Instance.BakeOven2PlugInValve;
                Done2 = DataModel.Instance.BakeOven2PlugInValveClosestatus;
                if ((Done1 || !Done2) && !StopOvenBox2VacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2PlugInValve, false);//关闭插板阀

                    while (!StopOvenBox1VacuumPumpingthd)
                    {
                        //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOven2PlugInValveClosestatus);
                        Done1 = DataModel.Instance.BakeOven2PlugInValveClosestatus;
                        if (Done1)
                        {
                            break;
                        }
                        Thread.Sleep(500);
                    }
                }
                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2MechanicalPump);
                Done1 = DataModel.Instance.BakeOven2MechanicalPump;
                if (!Done1 && !StopOvenBox2VacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2MechanicalPump, true);//打开机械泵
                }

                //float Vacuum = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox2).ReadVacuum();
                float Vacuum = DataModel.Instance.BakeOven2Vacuum;
                if (Vacuum > 10)
                {
                    //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2CoarseExtractionValve);
                    Done1 = DataModel.Instance.BakeOven2CoarseExtractionValve;
                    if (!Done1 && !StopOvenBox2VacuumPumpingthd)
                    {
                        WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2CoarseExtractionValve, true);//打开粗抽阀
                    }
                }
                else if (Vacuum < 0)
                {
                    while (!StopOvenBox2VacuumPumpingthd)
                    {
                        //Vacuum = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox2).ReadVacuum();
                        Vacuum = DataModel.Instance.BakeOven2Vacuum;
                        if (Vacuum < 10 && Vacuum > 0)
                        {
                            break;
                        }
                        if (Vacuum > 10)
                        {
                            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2CoarseExtractionValve);
                            Done1 = DataModel.Instance.BakeOven2CoarseExtractionValve;
                            if (!Done1)
                            {
                                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2CoarseExtractionValve, true);//打开粗抽阀
                            }
                            break;
                        }
                        Thread.Sleep(500);
                    }
                }

                while (!StopOvenBox2VacuumPumpingthd)
                {
                    //Vacuum = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox2).ReadVacuum();
                    Vacuum = DataModel.Instance.BakeOven2Vacuum;
                    if (Vacuum < 10 && Vacuum > 0)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }
                if (StopOvenBox2VacuumPumpingthd)
                {
                    StopOvenBox2VacuumPumpingMothed();
                    return;
                }

                Done1 = DataModel.Instance.BakeOven2CoarseExtractionValve;
                if (Done1)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2CoarseExtractionValve, false);//关闭粗抽阀
                }

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2FrontStageValve);
                Done1 = DataModel.Instance.BakeOven2FrontStageValve;
                if (!Done1 && !StopOvenBox2VacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2FrontStageValve, true);//打开前级阀
                }


                int T = 0;
                while (!StopOvenBox2VacuumPumpingthd)
                {
                    Thread.Sleep(500);
                    T++;
                    if (T > 120)
                    {
                        break;
                    }
                }

                if (StopOvenBox2VacuumPumpingthd)
                {
                    StopOvenBox2VacuumPumpingMothed();
                    return;
                }

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2PlugInValve);
                //Done2 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOven2PlugInValveOpenstatus);
                Done1 = DataModel.Instance.BakeOven2PlugInValve;
                Done2 = DataModel.Instance.BakeOven2PlugInValveOpenstatus;
                if ((!Done1 || !Done2) && !StopOvenBox2VacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2PlugInValve, true);//打开插板阀

                    while (!StopOvenBox1VacuumPumpingthd)
                    {
                        //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOven2PlugInValveOpenstatus);
                        Done1 = DataModel.Instance.BakeOven2PlugInValveOpenstatus;
                        if (Done1)
                        {
                            break;
                        }
                        Thread.Sleep(500);
                    }
                }


                T = 0;
                while (!StopOvenBox2VacuumPumpingthd)
                {
                    Thread.Sleep(500);
                    T++;
                    if (T > 120)
                    {
                        break;
                    }
                }

                while (!StopOvenBox2VacuumPumpingthd)
                {
                    //Vacuum = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox2).ReadVacuum();
                    Vacuum = DataModel.Instance.BakeOven2Vacuum;
                    if (Vacuum < 10 && Vacuum > 0)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }
                if (StopOvenBox2VacuumPumpingthd)
                {
                    StopOvenBox2VacuumPumpingMothed();
                    return;
                }

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2CoarseExtractionValve);
                Done1 = DataModel.Instance.BakeOven2CoarseExtractionValve;
                if (Done1 && !StopOvenBox2VacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2CoarseExtractionValve, false);//关闭粗抽阀
                }
                if (StopOvenBox2VacuumPumpingthd)
                {
                    StopOvenBox2VacuumPumpingMothed();
                    return;
                }

                //TurboMolecularPumpstatus Value = _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).ReadStatus();
                //if (Value == null)
                //{
                //    while (!StopOvenBox2VacuumPumpingthd)
                //    {
                //        Value = _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).ReadStatus();
                //        if (Value != null)
                //        {
                //            break;
                //        }
                //        Thread.Sleep(500);
                //    }
                //    if (StopOvenBox2VacuumPumpingthd)
                //    {
                //        StopOvenBox2VacuumPumpingMothed();
                //        return;
                //    }
                //}
                //if (!Value.err)
                //{
                //    if (!Value.Function)
                //    {
                //        _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).Function();
                //    }
                //}

                while (!StopOvenBox2VacuumPumpingthd)
                {
                    if (!DataModel.Instance.OvenBox2err)
                    {
                        if (!DataModel.Instance.OvenBox2Function)
                        {
                            _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).Function();
                            Thread.Sleep(500);

                            if (!DataModel.Instance.OvenBox2Function)
                            {
                                
                            }
                            else
                            {
                                break;
                            }

                            
                        }
                    }

                    Thread.Sleep(500);

                }


                while (!StopOvenBox2VacuumPumpingthd)
                {


                    Thread.Sleep(500);

                }
                if (StopOvenBox2VacuumPumpingthd)
                {
                    StopOvenBox2VacuumPumpingMothed();
                    return;
                }

            }

        }

        private void BoxVacuumPumpingMothed()
        {
            //抽真空前，需要关闭烘箱内门插板阀和方舱外门
            bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOvenInnerdoorClosestatus);
            bool Done2 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOven2InnerdoorClosestatus);
            bool Done3 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BoxOuterdoorClosetatus);

            Done1 = DataModel.Instance.BakeOvenInnerdoorClosestatus;
            Done2 = DataModel.Instance.BakeOven2InnerdoorClosestatus;
            Done3 = DataModel.Instance.BoxOuterdoorClosetatus;


            if (Done1 && Done2 && Done3 && !StopBoxVacuumPumpingthd)
            {
                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxFrontStageValve);
                Done1 = DataModel.Instance.BoxFrontStageValve;
                if (Done1 && !StopBoxVacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxFrontStageValve, false);//关闭前级阀
                }
                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxPlugInValve);
                //Done2 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BoxPlugInValveClosestatus);
                Done1 = DataModel.Instance.BoxPlugInValve;
                Done2 = DataModel.Instance.BoxPlugInValveClosestatus;
                if ((Done1 || !Done2) && !StopBoxVacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxPlugInValve, false);//关闭插板阀

                    while (!StopOvenBox1VacuumPumpingthd)
                    {
                        //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BoxPlugInValveClosestatus);
                        Done1 = DataModel.Instance.BoxPlugInValveClosestatus;
                        if (Done1)
                        {
                            break;
                        }
                        Thread.Sleep(500);
                    }
                }

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxMechanicalPump);
                Done1 = DataModel.Instance.BoxMechanicalPump;
                if (!Done1 && !StopBoxVacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxMechanicalPump, true);//打开机械泵
                }

                //float Vacuum = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.Box).ReadVacuum();
                float Vacuum = DataModel.Instance.BoxVacuum;
                if (Vacuum > 10)
                {
                    //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxCoarseExtractionValve);
                    Done1 = DataModel.Instance.BoxCoarseExtractionValve;
                    if (!Done1 && !StopBoxVacuumPumpingthd)
                    {
                        WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxCoarseExtractionValve, true);//打开粗抽阀
                    }
                }
                else if (Vacuum < 0)
                {
                    while (!StopBoxVacuumPumpingthd)
                    {
                        //Vacuum = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.Box).ReadVacuum();
                        Vacuum = DataModel.Instance.BoxVacuum;
                        if (Vacuum < 10 && Vacuum > 0)
                        {
                            break;
                        }
                        if (Vacuum > 10)
                        {
                            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxCoarseExtractionValve);
                            Done1 = DataModel.Instance.BoxCoarseExtractionValve;
                            if (!Done1 && !StopBoxVacuumPumpingthd)
                            {
                                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxCoarseExtractionValve, true);//打开粗抽阀
                            }
                            break;
                        }
                        Thread.Sleep(500);
                    }
                }

                while (!StopBoxVacuumPumpingthd)
                {
                    //Vacuum = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.Box).ReadVacuum();
                    Vacuum = DataModel.Instance.BoxVacuum;
                    if (Vacuum <= 10 && Vacuum > 0)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }
                if (StopBoxVacuumPumpingthd)
                {
                    StopBoxVacuumPumpingMothed();
                    return;
                }

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxCoarseExtractionValve);
                Done1 = DataModel.Instance.BoxCoarseExtractionValve;
                if (Done1 && !StopBoxVacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxCoarseExtractionValve, false);//关闭粗抽阀
                }

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxFrontStageValve);
                Done1 = DataModel.Instance.BoxFrontStageValve;
                if (!Done1 && !StopBoxVacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxFrontStageValve, true);//打开前级阀
                }


                int T = 0;
                while (!StopBoxVacuumPumpingthd)
                {
                    Thread.Sleep(500);
                    T++;
                    if (T > 120)
                    {
                        break;
                    }
                }

                if (StopBoxVacuumPumpingthd)
                {
                    StopBoxVacuumPumpingMothed();
                    return;
                }

                //冷凝泵达到40pa

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.CondenserPumpSignal4);
                Done1 = DataModel.Instance.CondenserPumpSignal4;
                while (!StopBoxVacuumPumpingthd)
                {
                    //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.CondenserPumpSignal4);
                    Done1 = DataModel.Instance.CondenserPumpSignal4;
                    if (Done1)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }

                if (StopBoxVacuumPumpingthd)
                {
                    StopBoxVacuumPumpingMothed();
                    return;
                }


                //前级阀关闭
                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxFrontStageValve);
                Done1 = DataModel.Instance.BoxFrontStageValve;
                if (Done1 && !StopBoxVacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxFrontStageValve, false);//关闭前级阀
                }


                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxCoarseExtractionValve);
                Done1 = DataModel.Instance.BoxCoarseExtractionValve;
                if (!Done1 && !StopBoxVacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxCoarseExtractionValve, true);//打开粗抽阀
                }

                if (StopBoxVacuumPumpingthd)
                {
                    StopBoxVacuumPumpingMothed();
                    return;
                }

                //压缩机打开
                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CompressorStops); 
                Done1 = DataModel.Instance.CompressorStops;
                if (!Done1 && !StopBoxVacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CompressorStops, true);//打开压缩机
                    Thread.Sleep(500);
                }
                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CompressorStartup);
                Done1 = DataModel.Instance.CompressorStartup;
                if (!Done1 && !StopBoxVacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CompressorStartup, true);//打开压缩机
                    Thread.Sleep(500);
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CompressorStartup, false);//打开压缩机
                }

                T = 0;
                while (!StopBoxVacuumPumpingthd)
                {
                    Thread.Sleep(500);
                    T++;
                    if (T > 60)
                    {
                        break;
                    }
                }

                if (StopBoxVacuumPumpingthd)
                {
                    StopBoxVacuumPumpingMothed();
                    return;
                }

                //皮拉尼达到40pa

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.CondenserPumpSignal4);
                Done1 = DataModel.Instance.CondenserPumpSignal4;
                while (!StopBoxVacuumPumpingthd)
                {
                    //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.CondenserPumpSignal4);
                    Done1 = DataModel.Instance.CondenserPumpSignal4;
                    if (Done1)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }

                if (StopBoxVacuumPumpingthd)
                {
                    StopBoxVacuumPumpingMothed();
                    return;
                }

                //打开冷凝泵
                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CondenserPump);
                Done1 = DataModel.Instance.CondenserPump;
                if (!Done1 && !StopBoxVacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CondenserPump, true);//打开冷凝泵
                }

                if (StopBoxVacuumPumpingthd)
                {
                    StopBoxVacuumPumpingMothed();
                    return;
                }

                //冷凝泵温度达到20K

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.CondenserPumpSignal2);
                Done1 = DataModel.Instance.CondenserPumpSignal2;
                while (!StopBoxVacuumPumpingthd)
                {
                    //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.CondenserPumpSignal2);
                    Done1 = DataModel.Instance.CondenserPumpSignal2;
                    if (Done1)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }

                if (StopBoxVacuumPumpingthd)
                {
                    StopBoxVacuumPumpingMothed();
                    return;
                }

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxCoarseExtractionValve);
                

                while (!StopBoxVacuumPumpingthd)
                {
                    //Vacuum = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.Box).ReadVacuum();
                    Vacuum = DataModel.Instance.BoxVacuum;
                    if (Vacuum < 10 && Vacuum > 0)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }
                if (StopBoxVacuumPumpingthd)
                {
                    StopBoxVacuumPumpingMothed();
                    return;
                }

                Done1 = DataModel.Instance.BoxCoarseExtractionValve;
                if (Done1 && !StopBoxVacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxCoarseExtractionValve, false);//关闭粗抽阀
                }

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxPlugInValve);
                //Done2 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BoxPlugInValveOpenstatus);
                Done1 = DataModel.Instance.BoxPlugInValve;
                Done2 = DataModel.Instance.BoxPlugInValveOpenstatus;
                if ((!Done1 || !Done2) && !StopBoxVacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxPlugInValve, true);//打开插板阀

                    while (!StopOvenBox1VacuumPumpingthd)
                    {
                        //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BoxPlugInValveOpenstatus);
                        Done1 = DataModel.Instance.BoxPlugInValveOpenstatus;
                        if (Done1)
                        {
                            break;
                        }
                        Thread.Sleep(500);
                    }
                }

                if (StopBoxVacuumPumpingthd)
                {
                    StopBoxVacuumPumpingMothed();
                    return;
                }

                
                T = 0;
                while (!StopBoxVacuumPumpingthd)
                {
                    Thread.Sleep(500);
                    T++;
                    if (T > 120)
                    {
                        break;
                    }
                }

                if (StopBoxVacuumPumpingthd)
                {
                    StopBoxVacuumPumpingMothed();
                    return;
                }

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxMechanicalPump);
                Done1 = DataModel.Instance.BoxMechanicalPump;
                if (Done1 && !StopBoxVacuumPumpingthd)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxMechanicalPump, false);//关闭机械泵
                }


                while (!StopBoxVacuumPumpingthd)
                {


                    Thread.Sleep(500);

                }
                if (StopBoxVacuumPumpingthd)
                {
                    StopBoxVacuumPumpingMothed();
                    return;
                }

            }

        }

        private void StopOvenBox1VacuumPumpingMothed()
        {

            bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenPlugInValve);
            bool Done2 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOvenPlugInValveClosestatus);
            Done1 = DataModel.Instance.BakeOvenPlugInValve;
            Done2 = DataModel.Instance.BakeOvenPlugInValveClosestatus;
            if (Done1 || !Done2)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenPlugInValve, false);//关闭插板阀

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOvenPlugInValveClosestatus);
                Done1 = DataModel.Instance.BakeOvenPlugInValveClosestatus;
                while (!Done1)
                {
                    //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOvenPlugInValveClosestatus);
                    Done1 = DataModel.Instance.BakeOvenPlugInValveClosestatus;
                    if (Done1)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }
            }

            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenCoarseExtractionValve);
            Done1 = DataModel.Instance.BakeOvenCoarseExtractionValve;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenCoarseExtractionValve, false);//关闭粗抽阀
            }

            //TurboMolecularPumpstatus Value = _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).ReadStatus();
            //while (Value == null)
            //{
            //    Value = _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).ReadStatus();
            //    if (Value != null)
            //    {
            //        break;
            //    }
            //    Thread.Sleep(500);
            //}
            //if (!Value.err)
            //{
            //    if (Value.Function)
            //    {
            //        _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).SlowShutdown();
            //        while (Value == null || !Value.Standbymode)
            //        {
            //            Value = _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).ReadStatus();
            //            if (Value != null && Value.Standbymode)
            //            {
            //                break;
            //            }
            //            Thread.Sleep(500);
            //        }
            //    }
            //}

            if (!DataModel.Instance.OvenBox1err)
            {
                if (DataModel.Instance.OvenBox1Function)
                {
                    _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).SlowShutdown();
                }
            }

            while (!DataModel.Instance.OvenBox1Standbymode)
            {


                Thread.Sleep(500);
            }

            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenFrontStageValve);
            Done1 = DataModel.Instance.BakeOvenFrontStageValve;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenFrontStageValve, false);//关闭前级阀
            }

            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenMechanicalPump);
            Done1 = DataModel.Instance.BakeOvenMechanicalPump;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenMechanicalPump, false);//关闭机械泵
            }

        }

        private void StopOvenBox2VacuumPumpingMothed()
        {

            bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2PlugInValve);
            bool Done2 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOven2PlugInValveClosestatus);
            Done1 = DataModel.Instance.BakeOven2PlugInValve;
            Done2 = DataModel.Instance.BakeOven2PlugInValveClosestatus;
            if (Done1 || !Done2)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2PlugInValve, false);//关闭插板阀

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOven2PlugInValveClosestatus);
                Done1 = DataModel.Instance.BakeOven2PlugInValveClosestatus;
                while (!Done1)
                {
                    //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOven2PlugInValveClosestatus);
                    Done1 = DataModel.Instance.BakeOven2PlugInValveClosestatus;
                    if (Done1)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }
            }

            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2CoarseExtractionValve);
            Done1 = DataModel.Instance.BakeOven2CoarseExtractionValve;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2CoarseExtractionValve, false);//关闭粗抽阀
            }

            //TurboMolecularPumpstatus Value = _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).ReadStatus();
            //while (Value == null)
            //{
            //    Value = _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).ReadStatus();
            //    if (Value != null)
            //    {
            //        break;
            //    }
            //    Thread.Sleep(500);
            //}
            //if (!Value.err)
            //{
            //    if (Value.Function)
            //    {
            //        _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).SlowShutdown();
            //        while (Value == null || !Value.Standbymode)
            //        {
            //            Value = _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).ReadStatus();
            //            if (Value != null && Value.Standbymode)
            //            {
            //                break;
            //            }
            //            Thread.Sleep(500);
            //        }
            //    }
            //}

            if (!DataModel.Instance.OvenBox2err)
            {
                if (DataModel.Instance.OvenBox2Function)
                {
                    _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).SlowShutdown();
                }
            }

            while (!DataModel.Instance.OvenBox2Standbymode)
            {


                Thread.Sleep(500);
            }

            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2FrontStageValve);
            Done1 = DataModel.Instance.BakeOven2FrontStageValve;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2FrontStageValve, false);//关闭前级阀
            }

            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2MechanicalPump);
            Done1 = DataModel.Instance.BakeOven2MechanicalPump;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2MechanicalPump, false);//关闭机械泵
            }

        }

        private void StopBoxVacuumPumpingMothed()
        {
            bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxPlugInValve);
            bool Done2 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BoxPlugInValveClosestatus);
            Done1 = DataModel.Instance.BoxPlugInValve;
            Done2 = DataModel.Instance.BoxPlugInValveClosestatus;
            if (Done1 || !Done2)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxPlugInValve, false);//关闭插板阀

                //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BoxPlugInValveClosestatus);
                Done1 = DataModel.Instance.BoxPlugInValveClosestatus;
                while (!Done1)
                {
                    //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BoxPlugInValveClosestatus);
                    Done1 = DataModel.Instance.BoxPlugInValveClosestatus;
                    if (Done1)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }
            }

            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenCoarseExtractionValve);
            Done1 = DataModel.Instance.BoxCoarseExtractionValve;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxCoarseExtractionValve, false);//关闭粗抽阀
            }

            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenFrontStageValve);
            Done1 = DataModel.Instance.BoxFrontStageValve;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxFrontStageValve, false);//关闭前级阀
            }

            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenMechanicalPump);
            Done1 = DataModel.Instance.BoxMechanicalPump;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxMechanicalPump, false);//关闭机械泵
            }

            Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CondenserPump);
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CondenserPump, false);//关闭冷凝泵
            }

            //压缩机关闭
            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CompressorStartup);
            Done1 = DataModel.Instance.CompressorStartup;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CompressorStartup, false);//关闭压缩机
                Thread.Sleep(500);
            }
            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CompressorStops);
            Done1 = DataModel.Instance.CompressorStops;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CompressorStops, false);//关闭压缩机

                Thread.Sleep(500);

                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.CompressorStops, true);//关闭压缩机
            }
            
        }

        private void TowerLampAlarmMothed()
        {
            bool Done1 = DataModel.Instance.TowerGreenLight;
            
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerGreenLight, false);//关闭绿灯
            }
            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerYellowLight);
            Done1 = DataModel.Instance.TowerYellowLight;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerYellowLight, false);//关闭黄灯
            }
            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight);
            Done1 = DataModel.Instance.TowerRedLight;
            if (!Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight, true);//打开红灯
            }

            while (!StoptowerLampthd)
            {
                Thread.Sleep(250);

                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight, false);//关闭红灯

                Thread.Sleep(250);

                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight, true);//打开红灯
            }
            Thread.Sleep(250);
            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight);
            Done1 = DataModel.Instance.TowerRedLight;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight, false);//关闭红灯
            }

        }

        private void TowerLampRemindMothed()
        {
            //bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerGreenLight);
            bool Done1 = DataModel.Instance.TowerGreenLight;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerGreenLight, false);//关闭绿灯
            }
            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerYellowLight);
            Done1 = DataModel.Instance.TowerYellowLight;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerYellowLight, false);//关闭黄灯
            }
            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight);
            Done1 = DataModel.Instance.TowerRedLight;
            if (!Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight, true);//打开红灯
            }

            while (!StoptowerLampthd)
            {
                Thread.Sleep(250);

                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight, false);//关闭红灯

                Thread.Sleep(750);

                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight, true);//打开红灯
            }
            Thread.Sleep(250);
            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight);
            Done1 = DataModel.Instance.TowerRedLight;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight, false);//关闭红灯
            }
        }

        private void TowerLampRunMothed()
        {

            //bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerYellowLight);
            bool Done1 = DataModel.Instance.TowerYellowLight;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerYellowLight, false);//关闭黄灯
            }
            Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight);
            Done1 = DataModel.Instance.TowerRedLight;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight, false);//关闭红灯
            }
            Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerGreenLight);
            Done1 = DataModel.Instance.TowerGreenLight;
            if (!Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerGreenLight, true);//打开绿灯
            }
        }

        private void TowerLampStandbyMothed()
        {
            //bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight);
            bool Done1 = DataModel.Instance.TowerRedLight;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerRedLight, false);//关闭红灯
            }
            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerGreenLight);
            Done1 = DataModel.Instance.TowerGreenLight;
            if (Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerGreenLight, false);//关闭绿灯
            }
            //Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerYellowLight);
            Done1 = DataModel.Instance.TowerYellowLight;
            if (!Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.TowerYellowLight, true);//打开黄灯
            }
        }

        private void MaterialBoxOutofoven1RemindMothed()
        {
            if(OvenBox1VacuumPumpingthd != null && OvenBox1VacuumPumpingthd.IsAlive)
            {
                StopOvenBox1VacuumPumping();
            }
            else
            {
                StopOvenBox1VacuumPumpingMothed();
            }
            //bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenAerate);
            bool Done1 = DataModel.Instance.BakeOvenAerate;
            if (!Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenAerate, true);//打开补气阀
            }

            bool pressure = false;
            while (!pressure)
            {
                pressure = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOvenPressureSensor);
                Thread.Sleep(100);
            }
            WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenAerate, false);//关闭补气阀
            DataModel.Instance.OvenBox1OutRemind = true;

            TowerLamp(TowerLampMothed.Remind);
            WarningBox.FormShow("焊接完成", "烘箱B的物料焊接完毕，请从烘箱A中取料", "提示");
            

            DataModel.Instance.OvenBox1OutRemind = false;

            if(DataModel.Instance.OvenBox1InRemind)
            {
                if (WarningBox.FormShow("进料提示", "请将新料盘放进烘箱A中", "提示") == 1)
                {
                    DataModel.Instance.OvenBox1InRemind = true;
                }
                else
                {
                    DataModel.Instance.OvenBox1InRemind = false;
                }
            }
            TowerLamp(TowerLampMothed.Standby);

        }

        private void MaterialBoxOutofoven2RemindMothed()
        {
            if (OvenBox2VacuumPumpingthd != null && OvenBox2VacuumPumpingthd.IsAlive)
            {
                StopOvenBox2VacuumPumping();
            }
            else
            {
                StopOvenBox2VacuumPumpingMothed();
            }
            //bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2Aerate);
            bool Done1 = DataModel.Instance.BakeOven2Aerate;
            if (!Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2Aerate, true);//打开补气阀
            }

            bool pressure = false;
            while (!pressure)
            {
                pressure = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOven2PressureSensor);
                Thread.Sleep(100);
            }
            WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2Aerate, false);//关闭补气阀
            DataModel.Instance.OvenBox2OutRemind = true;

            TowerLamp(TowerLampMothed.Remind);
            WarningBox.FormShow("焊接完成", "烘箱A的物料焊接完毕，请从烘箱B中取料", "提示");
            

            DataModel.Instance.OvenBox2OutRemind = false;

            if (DataModel.Instance.OvenBox2InRemind)
            {
                if (WarningBox.FormShow("进料提示", "请将料盘放进烘箱B中", "提示") == 1)
                {
                    DataModel.Instance.OvenBox2InRemind = true;
                }
                else
                {
                    DataModel.Instance.OvenBox2InRemind = false;
                }
            }
            TowerLamp(TowerLampMothed.Standby);

        }

        private void MaterialBoxIntooven1RemindMothed()
        {
            if(WarningBox.FormShow("进料提示", "是否已经将料盘放进烘箱A中", "提示") == 1)
            {
                DataModel.Instance.OvenBox1InRemind = true;
            }
            else
            {
                if (OvenBox1VacuumPumpingthd != null && OvenBox1VacuumPumpingthd.IsAlive)
                {
                    StopOvenBox1VacuumPumping();
                }
                else
                {
                    StopOvenBox1VacuumPumpingMothed();
                }
                //bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenAerate);
                bool Done1 = DataModel.Instance.BakeOvenAerate;
                if (!Done1)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenAerate, true);//打开补气阀
                }

                bool pressure = false;
                while (!pressure)
                {
                    pressure = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOvenPressureSensor);
                    Thread.Sleep(100);
                }
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenAerate, false);//关闭补气阀

                TowerLamp(TowerLampMothed.Remind);

                if (WarningBox.FormShow("进料提示", "请将料盘放进烘箱A中", "提示") == 1)
                {
                    DataModel.Instance.OvenBox1InRemind = true;
                }
                else
                {
                    DataModel.Instance.OvenBox1InRemind = false;
                }

                TowerLamp(TowerLampMothed.Standby);
            }

            

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱1外部进料完成";
        }

        private void MaterialBoxIntooven2RemindMothed()
        {

            if (WarningBox.FormShow("进料提示", "是否已经将料盘放进烘箱B中", "提示") == 1)
            {
                DataModel.Instance.OvenBox2InRemind = true;
            }
            else
            {
                if (OvenBox2VacuumPumpingthd != null && OvenBox2VacuumPumpingthd.IsAlive)
                {
                    StopOvenBox2VacuumPumping();
                }
                else
                {
                    StopOvenBox2VacuumPumpingMothed();
                }
                //bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2Aerate);
                bool Done1 = DataModel.Instance.BakeOven2Aerate;
                if (!Done1)
                {
                    WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2Aerate, true);//打开补气阀
                }

                bool pressure = false;
                while (!pressure)
                {
                    pressure = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOven2PressureSensor);
                    Thread.Sleep(100);
                }
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2Aerate, false);//关闭补气阀

                TowerLamp(TowerLampMothed.Remind);

                if (WarningBox.FormShow("进料提示", "请将料盘放进烘箱B中", "提示") == 1)
                {
                    DataModel.Instance.OvenBox2InRemind = true;
                }
                else
                {
                    DataModel.Instance.OvenBox2InRemind = false;
                }

                TowerLamp(TowerLampMothed.Standby);
            }

            

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱2外部进料完成";

        }

        public void MaterialBoxSupplyValve1()
        {
            if (OvenBox1VacuumPumpingthd != null && OvenBox1VacuumPumpingthd.IsAlive)
            {
                StopOvenBox1VacuumPumping();
            }
            else
            {
                StopOvenBox1VacuumPumpingMothed();
            }
            //bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenAerate);
            bool Done1 = DataModel.Instance.BakeOvenAerate;
            if (!Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenAerate, true);//打开补气阀
            }

            bool pressure = false;
            while (!pressure)
            {
                pressure = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOvenPressureSensor);
                Thread.Sleep(100);
            }
            WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOvenAerate, false);//关闭补气阀
            

        }

        public void MaterialBoxSupplyValve2()
        {
            if (OvenBox2VacuumPumpingthd != null && OvenBox2VacuumPumpingthd.IsAlive)
            {
                StopOvenBox2VacuumPumping();
            }
            else
            {
                StopOvenBox2VacuumPumpingMothed();
            }
            //bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2Aerate);
            bool Done1 = DataModel.Instance.BakeOven2Aerate;
            if (!Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2Aerate, true);//打开补气阀
            }

            bool pressure = false;
            while (!pressure)
            {
                pressure = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BakeOven2PressureSensor);
                Thread.Sleep(100);
            }
            WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BakeOven2Aerate, false);//关闭补气阀


        }

        public void MaterialBoxSupplyValve3()
        {
            if (BoxVacuumPumpingthd != null && BoxVacuumPumpingthd.IsAlive)
            {
                StopBoxVacuumPumping();
            }
            else
            {
                StopBoxVacuumPumpingMothed();
            }
            //bool Done1 = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxAerate);
            bool Done1 = DataModel.Instance.BoxAerate;
            if (!Done1)
            {
                WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxAerate, true);//打开补气阀
            }

            bool pressure = false;
            while (!pressure)
            {
                pressure = ReadBoolOvenBoxStatesMothed(EnumBoardcardDefineInputIO.BoxPressureSensor);
                Thread.Sleep(100);
            }
            WriteBoolOvenBoxStatesMothed(EnumBoardcardDefineOutputIO.BoxAerate, false);//关闭补气阀


        }



        private void OpenOvenboxInnerDoorMothed(EnumOvenBoxNum num)
        {
            if(num == EnumOvenBoxNum.Oven1)
            {
                bool Done1 = DataModel.Instance.BakeOvenInnerdoorOpenstatus;
                if (!Done1)
                {
                    Done1 = DataModel.Instance.BakeOvenPlugInValveClosestatus;
                    if (!Done1)
                    {
                        Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenPlugInValve, false);

                        while (!StopDoor)
                        {
                            //bool Done = Read<bool>(EnumBoardcardDefineInputIO.BakeOvenPlugInValveClosestatus);

                            Done1 = DataModel.Instance.BakeOvenPlugInValveClosestatus;
                            if (Done1)
                            {
                                break;
                            }

                            Thread.Sleep(100);
                        }
                    }

                    Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenInnerdoorUpDown, true);

                    while(!StopDoor)
                    {
                        //bool Done = Read<bool>(EnumBoardcardDefineInputIO.BakeOvenInnerdoorOpenstatus);

                        Done1 = DataModel.Instance.BakeOvenInnerdoorOpenstatus;
                        if (Done1)
                        {
                            //Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenInnerdoorUp, false);

                            StopDoor = false;

                            break;
                        }

                        Thread.Sleep(100);
                    }
                    if (StopDoor)
                    {
                        //Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenInnerdoorUp, false);

                        StopDoor = false;
                    }
                }
                
            }
            else if (num == EnumOvenBoxNum.Oven2)
            {
                bool Done1 = DataModel.Instance.BakeOven2InnerdoorOpenstatus;
                if (!Done1)
                {
                    Done1 = DataModel.Instance.BakeOven2PlugInValveClosestatus;
                    if (!Done1)
                    {
                        Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2PlugInValve, false);

                        while (!StopDoor)
                        {
                            //bool Done = Read<bool>(EnumBoardcardDefineInputIO.BakeOven2PlugInValveClosestatus);

                            Done1 = DataModel.Instance.BakeOven2PlugInValveClosestatus;
                            if (Done1)
                            {
                                break;
                            }

                            Thread.Sleep(100);
                        }
                    }

                    Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2InnerdoorUpDown, true);

                    while (!StopDoor)
                    {
                        //bool Done = Read<bool>(EnumBoardcardDefineInputIO.BakeOven2InnerdoorOpenstatus);

                        Done1 = DataModel.Instance.BakeOven2InnerdoorOpenstatus;

                        if (Done1)
                        {
                            //Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2InnerdoorUp, false);

                            StopDoor = false;

                            break;
                        }

                        Thread.Sleep(100);
                    }
                    if (StopDoor)
                    {
                        //Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2InnerdoorUp, false);

                        StopDoor = false;
                    }
                }
                
            }
        }

        private void CloseOvenboxInnerDoorMothed(EnumOvenBoxNum num)
        {
            if (num == EnumOvenBoxNum.Oven1)
            {
                bool Done1 = DataModel.Instance.BakeOvenInnerdoorClosestatus;
                if (!Done1)
                {
                    Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenInnerdoorUpDown, false);

                    while (!StopDoor)
                    {
                        //bool Done = Read<bool>(EnumBoardcardDefineInputIO.BakeOvenInnerdoorClosestatus);

                        Done1 = DataModel.Instance.BakeOvenInnerdoorClosestatus;

                        if (Done1)
                        {

                            StopDoor = false;

                            break;
                        }

                        Thread.Sleep(200);
                    }

                    if(DataModel.Instance.BakeOvenVacuum < 10)
                    {

                        Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenPlugInValve, true);

                        while (!StopDoor)
                        {
                            //bool Done = Read<bool>(EnumBoardcardDefineInputIO.BakeOvenInnerdoorOpenstatus);

                            Done1 = DataModel.Instance.BakeOvenPlugInValveOpenstatus;
                            if (Done1)
                            {
                                //Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenInnerdoorUp, false);

                                StopDoor = false;

                                break;
                            }

                            Thread.Sleep(100);
                        }
                    }
                    if (StopDoor)
                    {

                        StopDoor = false;
                    }
                }

            }
            else if (num == EnumOvenBoxNum.Oven2)
            {
                bool Done1 = DataModel.Instance.BakeOven2InnerdoorClosestatus;
                if (!Done1)
                {
                    Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2InnerdoorUpDown, false);

                    while (!StopDoor)
                    {
                        //bool Done = Read<bool>(EnumBoardcardDefineInputIO.BakeOven2InnerdoorClosestatus);

                        Done1 = DataModel.Instance.BakeOven2InnerdoorClosestatus;

                        if (Done1)
                        {

                            StopDoor = false;

                            break;
                        }

                        Thread.Sleep(200);
                    }
                    if (DataModel.Instance.BakeOven2Vacuum < 10)
                    {

                        Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2PlugInValve, true);

                        while (!StopDoor)
                        {
                            //bool Done = Read<bool>(EnumBoardcardDefineInputIO.BakeOvenInnerdoorOpenstatus);

                            Done1 = DataModel.Instance.BakeOven2PlugInValveOpenstatus;
                            if (Done1)
                            {
                                //Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenInnerdoorUp, false);

                                StopDoor = false;

                                break;
                            }

                            Thread.Sleep(100);
                        }
                    }
                    if (StopDoor)
                    {

                        StopDoor = false;
                    }
                }

            }
        }


        private void WeldMothed(int weldtime)
        {

            if (!DataModel.Instance.PressIsPress && !Stopweld)
            {
                Write<bool>(EnumBoardcardDefineOutputIO.PressPressingDivide, true);

                while (!Stopweld)
                {

                    if (DataModel.Instance.PressIsPress)
                    {
                        break;
                    }

                    Thread.Sleep(100);
                }

            }

            int T = 0;

            while (!Stopweld)
            {

                
                if(T > weldtime)
                {
                    break;
                }
                T++;
                Thread.Sleep(1000);
            }

            if (Stopweld)
            {
                //Write<bool>(EnumBoardcardDefineOutputIO.PressDivide, false);

                Stopweld = false;

                return;
            }


            Write<bool>(EnumBoardcardDefineOutputIO.PressPressingDivide, false);

            while (!Stopweld)
            {

                if (DataModel.Instance.PressIsDivide)
                {
                    break;
                }

                Thread.Sleep(100);
            }

            if (Stopweld)
            {
                //Write<bool>(EnumBoardcardDefineOutputIO.PressDivide, false);

                Stopweld = false;

                return;
            }


        }


        private void RestWeldMothed()
        {
            Write<bool>(EnumBoardcardDefineOutputIO.PressPressingDivide, false);

            while(!Stopweld)
            {
                
                if (DataModel.Instance.PressIsDivide)
                {
                    break;
                }

                Thread.Sleep(100);
            }

            if (Stopweld)
            {
                //Write<bool>(EnumBoardcardDefineOutputIO.PressDivide, false);

                Stopweld = false;

                return;
            }

            
        }


        #endregion



        #region Public Mothed

        public EnumOvenBoxStates ReadAllOvenBoxStates(EnumOvenBoxNum num)
        {
            return ReadAllOvenBoxStatesMothed(num);
        }

        public bool ReadBoolOvenBoxStates(EnumBoardcardDefineInputIO name)
        {
            return ReadBoolOvenBoxStatesMothed(name);
        }
        public float ReadfloatOvenBoxStates(EnumBoardcardDefineInputIO name)
        {
            return ReadfloatOvenBoxStatesMothed(name);
        }
        public short ReadshortOvenBoxStates(EnumBoardcardDefineInputIO name)
        {
            return ReadshortOvenBoxStatesMothed(name);
        }

        public bool ReadBoolOvenBoxStates(EnumBoardcardDefineOutputIO name)
        {
            return ReadBoolOvenBoxStatesMothed(name);
        }
        public float ReadfloatOvenBoxStates(EnumBoardcardDefineOutputIO name)
        {
            return ReadfloatOvenBoxStatesMothed(name);
        }
        public short ReadshortOvenBoxStates(EnumBoardcardDefineOutputIO name)
        {
            return ReadshortOvenBoxStatesMothed(name);
        }


        public int WriteBoolOvenBoxStates(EnumBoardcardDefineOutputIO name, bool value)
        {
            return WriteBoolOvenBoxStatesMothed(name, value);
        }
        public int WritefloatOvenBoxStates(EnumBoardcardDefineOutputIO name, float value)
        {
            return WritefloatOvenBoxStatesMothed(name, value);
        }
        public int WriteshortOvenBoxStates(EnumBoardcardDefineOutputIO name, short value)
        {
            return WriteshortOvenBoxStatesMothed(name, value);
        }





        /// <summary>
        /// 读取节点
        /// </summary>
        /// <typeparam name="T"> bool int float</typeparam>
        /// <param name="Note"> 节点</param>
        /// <returns></returns>
        public T Read<T>(EnumBoardcardDefineInputIO name)
        {
            try
            {
                Type t = typeof(T);
                if(t == typeof(bool))
                {
                    bool V = ReadBoolOvenBoxStates(name);


                    return (T)Convert.ChangeType("" + V, typeof(T));
                }
                else if (t == typeof(float))
                {
                    float V = ReadfloatOvenBoxStates(name);


                    return (T)Convert.ChangeType("" + V, typeof(T));
                }
                else if (t == typeof(short))
                {
                    short V = ReadshortOvenBoxStates(name);


                    return (T)Convert.ChangeType("" + V, typeof(T));
                }


                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public T Read<T>(EnumBoardcardDefineOutputIO name)
        {
            try
            {
                Type t = typeof(T);
                if (t == typeof(bool))
                {

                    bool V = ReadBoolOvenBoxStates(name);


                    return (T)Convert.ChangeType("" + V, typeof(T));
                }
                else if (t == typeof(float))
                {
                    float V = ReadfloatOvenBoxStates(name);


                    return (T)Convert.ChangeType("" + V, typeof(T));
                }
                else if (t == typeof(short))
                {
                    short V = ReadshortOvenBoxStates(name);


                    return (T)Convert.ChangeType("" + V, typeof(T));
                }

                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }



        public void Write<T>(EnumBoardcardDefineOutputIO name, T Data)
        {
            try
            {
                Type t = typeof(T);

                if (t == typeof(bool))
                {

                    WriteBoolOvenBoxStates(name, Convert.ToBoolean(Data));

                }
                else if (t == typeof(float))
                {
                    WritefloatOvenBoxStates(name, Convert.ToSingle(Data));
                }
                else if (t == typeof(short))
                {
                    WriteshortOvenBoxStates(name, Convert.ToInt16(Data));
                }
            }
            catch (Exception ex)
            {

            }
        }



        public void OvenBox1VacuumPumping()
        {
            try
            {
                if (OvenBox1VacuumPumpingthd != null && OvenBox1VacuumPumpingthd.IsAlive)
                {
                    return;
                }

                OvenBox1VacuumPumpingthd = new Thread(() => OvenBox1VacuumPumpingMothed());

                StopOvenBox1VacuumPumpingthd = false;

                OvenBox1VacuumPumpingthd.Start();

                //OvenBox1VacuumPumpingthd.Join();

            }
            catch (Exception ex)
            {

            }

        }

        public void OvenBox2VacuumPumping()
        {
            try
            {
                if (OvenBox2VacuumPumpingthd != null && OvenBox2VacuumPumpingthd.IsAlive)
                {
                    return;
                }

                OvenBox2VacuumPumpingthd = new Thread(() => OvenBox2VacuumPumpingMothed());

                StopOvenBox2VacuumPumpingthd = false;

                OvenBox2VacuumPumpingthd.Start();

                //OvenBox2VacuumPumpingthd.Join();

            }
            catch (Exception ex)
            {

            }

        }

        public void BoxVacuumPumping()
        {
            try
            {
                if (BoxVacuumPumpingthd != null && BoxVacuumPumpingthd.IsAlive)
                {
                    return;
                }

                BoxVacuumPumpingthd = new Thread(() => BoxVacuumPumpingMothed());

                StopBoxVacuumPumpingthd = false;

                BoxVacuumPumpingthd.Start();

                //BoxVacuumPumpingthd.Join();

            }
            catch (Exception ex)
            {

            }

        }

        public void StopOvenBox1VacuumPumping()
        {
            if (OvenBox1VacuumPumpingthd != null && OvenBox1VacuumPumpingthd.IsAlive)
            {
                StopOvenBox1VacuumPumpingthd = true;

                OvenBox1VacuumPumpingthd.Join();
            }
            else
            {
                StopOvenBox1VacuumPumpingMothed();
            }

        }

        public void StopOvenBox2VacuumPumping()
        {
            if (OvenBox2VacuumPumpingthd != null && OvenBox2VacuumPumpingthd.IsAlive)
            {
                StopOvenBox2VacuumPumpingthd = true;

                OvenBox2VacuumPumpingthd.Join();
            }
            else
            {
                StopOvenBox2VacuumPumpingMothed();
            }

        }

        public void StopBoxVacuumPumping()
        {
            if (BoxVacuumPumpingthd != null && BoxVacuumPumpingthd.IsAlive)
            {
                StopBoxVacuumPumpingthd = true;

                BoxVacuumPumpingthd.Join();
            }
            else
            {
                StopBoxVacuumPumpingMothed();
            }

        }

        public void MaterialBoxOutofoven1Remind()
        {
            try
            {
                if (Oven1OutRemindthd != null && Oven1OutRemindthd.IsAlive)
                {
                    return;
                }

                Oven1OutRemindthd = new Thread(() => MaterialBoxOutofoven1RemindMothed());

                Oven1OutRemindthd.Start();

                //OvenBox1VacuumPumpingthd.Join();

            }
            catch (Exception ex)
            {

            }
        }

        public void MaterialBoxOutofoven2Remind()
        {
            try
            {
                if (Oven2OutRemindthd != null && Oven2OutRemindthd.IsAlive)
                {
                    return;
                }

                Oven2OutRemindthd = new Thread(() => MaterialBoxOutofoven2RemindMothed());

                Oven2OutRemindthd.Start();

                //OvenBox1VacuumPumpingthd.Join();

            }
            catch (Exception ex)
            {

            }
        }

        public void MaterialBoxIntooven1Remind()
        {
            try
            {
                if (Oven1InRemindthd != null && Oven1InRemindthd.IsAlive)
                {
                    return;
                }

                Oven1InRemindthd = new Thread(() => MaterialBoxIntooven1RemindMothed());

                Oven1InRemindthd.Start();

                //OvenBox1VacuumPumpingthd.Join();

            }
            catch (Exception ex)
            {

            }
        }

        public void MaterialBoxIntooven2Remind()
        {
            try
            {
                if (Oven2InRemindthd != null && Oven2InRemindthd.IsAlive)
                {
                    return;
                }

                Oven2InRemindthd = new Thread(() => MaterialBoxIntooven2RemindMothed());

                Oven2InRemindthd.Start();

                //OvenBox1VacuumPumpingthd.Join();

            }
            catch (Exception ex)
            {

            }
        }

        public void TowerLamp(TowerLampMothed mothed)
        {
            try
            {
                if(mothed == TowerLampMothed.Standby)
                {
                    if (towerLampthd != null && towerLampthd.IsAlive)
                    {
                        StoptowerLampthd = true;

                        towerLampthd.Join();
                    }
                    TowerLampStandbyMothed();
                }
                else if (mothed == TowerLampMothed.Run)
                {
                    if (towerLampthd != null && towerLampthd.IsAlive)
                    {
                        StoptowerLampthd = true;

                        towerLampthd.Join();
                    }
                    TowerLampRunMothed();
                }
                else if (mothed == TowerLampMothed.Alarm)
                {
                    if (towerLampthd != null && towerLampthd.IsAlive)
                    {
                        StoptowerLampthd = true;

                        towerLampthd.Join();
                    }
                    towerLampthd = new Thread(TowerLampAlarmMothed);

                    StoptowerLampthd = false;

                    towerLampthd.Start();
                }
                else if (mothed == TowerLampMothed.Remind)
                {
                    if (towerLampthd != null && towerLampthd.IsAlive)
                    {
                        StoptowerLampthd = true;

                        towerLampthd.Join();
                    }
                    towerLampthd = new Thread(TowerLampRemindMothed);

                    StoptowerLampthd = false;

                    towerLampthd.Start();
                }

            }
            catch (Exception ex)
            {

            }
        }



        public void BoxPurge(int BakeOvenPFUpPressure, int BakeOvenPFDownPressure, int BakeOvenPFnum, int BakeOvenPFinterval)
        {
            try
            {
                if (heatthd != null && heatthd.IsAlive)
                {
                    return;
                }

                heatthd = new Thread(() => BoxPurgeMothed(BakeOvenPFUpPressure, BakeOvenPFDownPressure, BakeOvenPFnum, BakeOvenPFinterval));

                StopHeat = false;

                heatthd.Start();

                heatthd.Join();

            }
            catch (Exception ex)
            {

            }
        }

        public void StopBoxPurge()
        {
            if (heatthd != null && heatthd.IsAlive)
            {
                StopHeat = true;

                heatthd.Join();
            }
        }


        public void Purge(EnumOvenBoxNum num, int BakeOvenPFUpPressure, int BakeOvenPFDownPressure, int BakeOvenPFnum, int BakeOvenPFinterval)
        {
            try
            {
                if (heatthd != null && heatthd.IsAlive)
                {
                    return;
                }

                heatthd = new Thread(() => PurgeMothed(num, BakeOvenPFUpPressure, BakeOvenPFDownPressure, BakeOvenPFnum, BakeOvenPFinterval));

                StopHeat = false;

                heatthd.Start();

                heatthd.Join();
            }
            catch (Exception ex)
            {

            }
        }

        public void StopPurge()
        {
            if (heatthd != null && heatthd.IsAlive)
            {
                StopHeat = true;

                heatthd.Join();
            }
        }

        public void ManualHeat(EnumOvenBoxNum num, int HeatTargetTemperature, int HeatPreservationMinute, int OverTemperatureThreshold)
        {

            try
            {
                if(heatthd != null &&  heatthd.IsAlive)
                {
                    return;
                }

                heatthd = new Thread(() => ManualHeatMothed(num, HeatTargetTemperature, HeatPreservationMinute, OverTemperatureThreshold));

                StopHeat = false;

                heatthd.Start();

                //heatthd.Join();

                
            }
            catch(Exception ex)
            {

            }
        }

        public void StopManualHeat()
        {
            if (heatthd != null && heatthd.IsAlive)
            {
                StopHeat = true;

                //heatthd.Join();

                DataModel.Instance.OvenBox1Heating = false;
            }
            else
            {
                _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Write(TemperatureRtuAdd.RUN, 0);

                _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Write(TemperatureRtuAdd.AT, 0);
            }
        }

        public void ManualHeat2(EnumOvenBoxNum num, int HeatTargetTemperature, int HeatPreservationMinute, int OverTemperatureThreshold)
        {

            try
            {
                if (heatthd2 != null && heatthd2.IsAlive)
                {
                    return;
                }

                heatthd2 = new Thread(() => ManualHeatMothed2(num, HeatTargetTemperature, HeatPreservationMinute, OverTemperatureThreshold));

                StopHeat2 = false;

                heatthd2.Start();

                //heatthd2.Join();

                
            }
            catch (Exception ex)
            {

            }
        }

        public void StopManualHeat2()
        {
            if (heatthd2 != null && heatthd2.IsAlive)
            {
                StopHeat2 = true;

                //heatthd2.Join();

                DataModel.Instance.OvenBox2Heating = false;
            }
            else
            {
                _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Write(TemperatureRtuAdd.RUN, 0);

                _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Write(TemperatureRtuAdd.AT, 0);
            }
        }

        public void AutoHeat(EnumOvenBoxNum num, int HeatTargetTemperature, int HeatPreservationMinute, int OverTemperatureThreshold, int BakeOvenPFUpPressure, int BakeOvenPFDownPressure, int BakeOvenPFnum, int BakeOvenPFinterval)
        {
            try
            {
                if (heatthd != null && heatthd.IsAlive)
                {
                    return;
                }

                heatthd = new Thread(() => AutoHeatMothed(num, HeatTargetTemperature, HeatPreservationMinute, OverTemperatureThreshold, BakeOvenPFUpPressure, BakeOvenPFDownPressure, BakeOvenPFnum, BakeOvenPFinterval));

                StopHeat = false;

                heatthd.Start();

                heatthd.Join();
            }
            catch (Exception ex)
            {

            }
        }


        public void StopAutoHeat()
        {
            if (heatthd != null && heatthd.IsAlive)
            {
                StopHeat = true;

                heatthd.Join();
            }
        }


        public void OpenOvenboxInnerDoor(EnumOvenBoxNum num)
        {
            if (Doorthd != null && Doorthd.IsAlive)
            {
                return;
            }

            Doorthd = new Thread(() => OpenOvenboxInnerDoorMothed(num));

            StopDoor = false;

            Doorthd.Start();

            Doorthd.Join();
        }

        public void StopOpenOvenboxInnerDoor(EnumOvenBoxNum num)
        {
            if (Doorthd != null && heatthd.IsAlive)
            {
                StopDoor = true;

                Doorthd.Join();
            }
        }

        public void CloseOvenboxInnerDoor(EnumOvenBoxNum num)
        {
            if (Doorthd != null && Doorthd.IsAlive)
            {
                return;
            }

            Doorthd = new Thread(() => CloseOvenboxInnerDoorMothed(num));

            StopDoor = false;

            Doorthd.Start();

            Doorthd.Join();
        }

        public void StopCloseOvenboxInnerDoor(EnumOvenBoxNum num)
        {
            if (Doorthd != null && Doorthd.IsAlive)
            {
                StopDoor = true;

                Doorthd.Join();
            }
        }


        public void Weld(int weldtime)
        {
            if (Weldthd != null && Weldthd.IsAlive)
            {
                return;
            }

            Weldthd = new Thread(() => WeldMothed(weldtime));

            Stopweld = false;

            Weldthd.Start();

            Weldthd.Join();
        }

        public void StopWeld()
        {
            if (Weldthd != null && Weldthd.IsAlive)
            {
                Stopweld = true;

                Weldthd.Join();
            }
        }

        public void RestWeld()
        {
            if (Weldthd != null && Weldthd.IsAlive)
            {
                return;
            }

            Weldthd = new Thread(() => RestWeldMothed());

            Stopweld = false;

            Weldthd.Start();

            Weldthd.Join();
        }

        public void StopRestWeld()
        {
            if (Weldthd != null && Weldthd.IsAlive)
            {
                Stopweld = true;

                Weldthd.Join();
            }
        }


        #endregion



    }

}
