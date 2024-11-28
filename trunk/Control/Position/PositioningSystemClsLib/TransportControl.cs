using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using RecipeClsLib;
using StageControllerClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechnologicalClsLib;
using WestDragon.Framework.UtilityHelper;

namespace PositioningSystemClsLib
{
    public class TransportControl
    {

        #region private file


        private static volatile TransportControl _instance = new TransportControl();
        private static readonly object _lockObj = new object();
        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }
        /// <summary>
        /// 获取单例对象
        /// </summary>
        /// <returns></returns>
        public static TransportControl Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new TransportControl();
                        }
                    }
                }
                return _instance;
            }
        }

        private StageCore stage = StageControllerClsLib.StageCore.Instance;
        /// <summary>
        /// 定位系统
        /// </summary>
        private PositioningSystem _positioningSystem
        {
            get { return PositioningSystem.Instance; }
        }

        private OvenBoxProcessControl _plc
        {
            get { return OvenBoxProcessControl.Instance; }
        }


        /// <summary>
        /// 硬件配置处理器
        /// </summary>
        private HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }




        #endregion



        #region public file

        /// <summary>
        /// 搬送状态
        /// </summary>
        public int TransportStatus { get; set; } = -1;

        private TransportRecipe _transportRecipe;
        public TransportRecipe TransportRecipe
        {
            get { return _transportRecipe; }
            set { _transportRecipe = value; }
        }

        public int Ovennum { get; set; } = 0;
        public int Ovenlayer { get; set; } = 0;


        public double Oven1Vacuum { get; set; } = 0;
        public double Oven2Vacuum { get; set; } = 0;
        public double BoxVacuum { get; set; } = 0;
        public double VacuumD { get; set; } = 10;
        public double VacuumC { get; set; } = 0.0001;

        public int singleDelay { get; set; } = 500;
        public int Delay { get; set; } = 120000;

        #endregion


        #region private mothd

        /// <summary>
        /// 料盒勾爪到目标位置
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="T"></param>
        private void MaterialboxHookXYZTAbsoluteMove(double X, double Y, double Z, double T, int mode = 0)
        {
            double MaterialboxX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
            double MaterialboxY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
            double MaterialboxZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
            double MaterialboxT = ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

            if(Z>7)
            {
                Z = 7;
            }

            if(mode == 0)
            {
                if(Math.Abs(MaterialboxX - X) < 2 && Math.Abs(MaterialboxY - Y) < 2 && Math.Abs(MaterialboxT - T) < 2)
                {
                    AxisAbsoluteMove(EnumStageAxis.MaterialboxY, Y);

                    AxisAbsoluteMove(EnumStageAxis.MaterialboxX, X);

                    AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, Z);
                }
                else
                {
                    {
                        //先到旋转位置
                        if (((MaterialboxY < SystemConfiguration.Instance.PositioningConfig.MaterialBoxZcannotmovedLocation1.Y)) &&
                            (Math.Abs(MaterialboxT - SystemConfiguration.Instance.PositioningConfig.MaterialBoxZcannotmovedLocation1.Theta) < 70))//料盘勾爪在物料勾爪下方时，需要先移动XY，不能先动Z
                        {
                            AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxZinMaterialMaxZ);
                            if (((MaterialboxX > SystemConfiguration.Instance.PositioningConfig.MaterialBoxYcannotmovedLocation1.X)) && Math.Abs(MaterialboxT - (-1)) < 10)
                            {
                                

                                AxisAbsoluteMove(EnumStageAxis.MaterialboxX, -91.530f);

                                AxisAbsoluteMove(EnumStageAxis.MaterialboxY, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.Y);

                                AxisAbsoluteMove(EnumStageAxis.MaterialboxX, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.X);

                                AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.Z);
                            }
                            else
                            {
                                AxisAbsoluteMove(EnumStageAxis.MaterialboxX, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.X);

                                AxisAbsoluteMove(EnumStageAxis.MaterialboxY, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.Y);

                                AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.Z);
                            }
                            

                        }
                        else//料盘勾爪不在物料勾爪下方，可以先动XY
                        {
                            if (((MaterialboxY > SystemConfiguration.Instance.PositioningConfig.MaterialBoxXcannotmovedLocation1.Y)) &&
                            (Math.Abs(MaterialboxT - SystemConfiguration.Instance.PositioningConfig.MaterialBoxXcannotmovedLocation1.Theta) < 10))//料盘勾爪在烘箱A中，此时不能先动X轴
                            {
                                AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxZinOvenBoxMaxZ);

                                AxisAbsoluteMove(EnumStageAxis.MaterialboxY, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.Y);

                                AxisAbsoluteMove(EnumStageAxis.MaterialboxX, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.X);

                                AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.Z);
                            }
                            else if (((MaterialboxX > SystemConfiguration.Instance.PositioningConfig.MaterialBoxYcannotmovedLocation1.X)) &&
                            (Math.Abs(MaterialboxT - SystemConfiguration.Instance.PositioningConfig.MaterialBoxYcannotmovedLocation1.Theta) < 10))//料盘勾爪在烘箱B中，此时不能先动Y轴
                            {
                                AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxZinOvenBoxMaxZ);

                                AxisAbsoluteMove(EnumStageAxis.MaterialboxX, -175.412f);

                                AxisAbsoluteMove(EnumStageAxis.MaterialboxY, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.Y);

                                AxisAbsoluteMove(EnumStageAxis.MaterialboxX, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.X);



                                AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.Z);
                            }
                            else
                            {
                                if(((MaterialboxX > SystemConfiguration.Instance.PositioningConfig.MaterialBoxYcannotmovedLocation1.X)) && Math.Abs(MaterialboxT - (-1)) < 10)
                                {
                                    AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.Z);

                                    AxisAbsoluteMove(EnumStageAxis.MaterialboxX, -91.530f);

                                    AxisAbsoluteMove(EnumStageAxis.MaterialboxY, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.Y);

                                    AxisAbsoluteMove(EnumStageAxis.MaterialboxX, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.X);
                                }
                                else
                                {
                                    AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.Z);

                                    AxisAbsoluteMove(EnumStageAxis.MaterialboxX, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.X);

                                    AxisAbsoluteMove(EnumStageAxis.MaterialboxY, SystemConfiguration.Instance.PositioningConfig.MaterialBoxRotatablePositionLocation.Y);
                                }

                                
                            }
                        }

                        //先转角度
                        AxisAbsoluteMove(EnumStageAxis.MaterialboxT, T);

                        //到目标位置

                        if (((Y > SystemConfiguration.Instance.PositioningConfig.MaterialBoxXcannotmovedLocation1.Y)) &&
                        (Math.Abs(T - SystemConfiguration.Instance.PositioningConfig.MaterialBoxXcannotmovedLocation1.Theta) < 10))//料盘要进入烘箱A中
                        {
                            AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxZinOvenBoxMaxZ);

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxX, X);

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxY, Y);

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, Z);
                        }
                        else if (((X > SystemConfiguration.Instance.PositioningConfig.MaterialBoxYcannotmovedLocation1.X)) &&
                    (Math.Abs(T - SystemConfiguration.Instance.PositioningConfig.MaterialBoxYcannotmovedLocation1.Theta) < 10))//料盘要进入烘箱B中
                        {
                            AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxZinOvenBoxMaxZ);

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxX, -175.412f);

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxY, Y);

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxX, X);

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, Z);
                        }
                        else if (((MaterialboxY < SystemConfiguration.Instance.PositioningConfig.MaterialBoxZcannotmovedLocation1.Y)) &&
                            (Math.Abs(MaterialboxT - SystemConfiguration.Instance.PositioningConfig.MaterialBoxZcannotmovedLocation1.Theta) < 70))//料盘勾爪在物料勾爪下方时，需要先移动XY，不能先动Z
                        {
                            AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxZinMaterialMaxZ);

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxY, Y);

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxX, X);

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, Z);
                        }
                        else
                        {
                            

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxZinMaterialMaxZ);

                            if (X > SystemConfiguration.Instance.PositioningConfig.MaterialBoxYcannotmovedLocation1.X && Math.Abs(T - SystemConfiguration.Instance.PositioningConfig.MaterialBoxZcannotmovedLocation1.Theta) < 70)
                            {
                                AxisAbsoluteMove(EnumStageAxis.MaterialboxX, -91.530f);
                            }

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxY, Y);

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxX, X);

                            AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, Z);
                        }

                    }

                }
                //if (Math.Abs(MaterialboxT - T) > 10)//需要旋转大角度的情况,需要先到旋转角度位置，将角度旋转之后再到达目标位置

            }
            else if(mode == 1)
            {
                if (Math.Abs(MaterialboxX - X) < 2 && Math.Abs(MaterialboxY - Y) < 2 && Math.Abs(MaterialboxT - T) < 2)
                {
                    AxisAbsoluteMove(EnumStageAxis.MaterialboxY, Y);

                    AxisAbsoluteMove(EnumStageAxis.MaterialboxX, X);

                    AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, Z);
                }
                else
                {
                    if (((Y > SystemConfiguration.Instance.PositioningConfig.MaterialBoxXcannotmovedLocation1.Y)) &&
                    (Math.Abs(T - SystemConfiguration.Instance.PositioningConfig.MaterialBoxXcannotmovedLocation1.Theta) < 10))//料盘要进入烘箱A中
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxZinOvenBoxMaxZ);

                        AxisAbsoluteMove(EnumStageAxis.MaterialboxX, X);

                        AxisAbsoluteMove(EnumStageAxis.MaterialboxY, Y);

                        AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, Z);
                    }
                    else if (((X > SystemConfiguration.Instance.PositioningConfig.MaterialBoxYcannotmovedLocation1.X)) &&
                (Math.Abs(T - SystemConfiguration.Instance.PositioningConfig.MaterialBoxYcannotmovedLocation1.Theta) < 10))//料盘要进入烘箱B中
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxZinOvenBoxMaxZ);

                        AxisAbsoluteMove(EnumStageAxis.MaterialboxX, SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.X);

                        AxisAbsoluteMove(EnumStageAxis.MaterialboxY, Y);

                        AxisAbsoluteMove(EnumStageAxis.MaterialboxX, X);

                        AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, Z);
                    }
                    else if (((MaterialboxY < SystemConfiguration.Instance.PositioningConfig.MaterialBoxZcannotmovedLocation1.Y)) &&
                        (Math.Abs(MaterialboxT - SystemConfiguration.Instance.PositioningConfig.MaterialBoxZcannotmovedLocation1.Theta) < 70))//料盘勾爪在物料勾爪下方时，需要先移动XY，不能先动Z
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxZinMaterialMaxZ);

                        AxisAbsoluteMove(EnumStageAxis.MaterialboxY, Y);

                        AxisAbsoluteMove(EnumStageAxis.MaterialboxX, X);

                        AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, Z);
                    }
                    else
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, Z);

                        AxisAbsoluteMove(EnumStageAxis.MaterialboxY, Y);

                        AxisAbsoluteMove(EnumStageAxis.MaterialboxX, X);
                    }
                }
                
            }
        }

        /// <summary>
        /// 物料勾爪到目标位置
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        private void MaterialHookXYZAbsoluteMove(double X, double Y, double Z)
        {
            double MaterialX = ReadCurrentAxisposition(EnumStageAxis.MaterialX);
            double MaterialY = ReadCurrentAxisposition(EnumStageAxis.MaterialY);
            double MaterialZ = ReadCurrentAxisposition(EnumStageAxis.MaterialZ);

            if ((MaterialY < SystemConfiguration.Instance.PositioningConfig.MaterialXcannotmovedLocation1.Y))//物料勾爪靠近柱子，不能先移动X
            {
                if((Z < SystemConfiguration.Instance.PositioningConfig.MaterialZMinZ) || (Z > SystemConfiguration.Instance.PositioningConfig.MaterialZMaxZ)) //物料勾爪在压机中间，先将Z移动到最合适位置
                {
                    AxisAbsoluteMove(EnumStageAxis.MaterialZ, SystemConfiguration.Instance.PositioningConfig.MaterialZmostsuitableZ);

                    if (Y < SystemConfiguration.Instance.PositioningConfig.MaterialXcannotmovedLocation1.Y)//目标位置在柱子附近
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialX, X);

                        AxisAbsoluteMove(EnumStageAxis.MaterialY, Y);
                    }
                    else
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialY, Y);

                        AxisAbsoluteMove(EnumStageAxis.MaterialX, X);
                    }

                    AxisAbsoluteMove(EnumStageAxis.MaterialZ, Z);
                }
                else
                {
                    AxisAbsoluteMove(EnumStageAxis.MaterialZ, Z);

                    if (Y < SystemConfiguration.Instance.PositioningConfig.MaterialXcannotmovedLocation1.Y)//目标位置在柱子附近
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialX, X);

                        AxisAbsoluteMove(EnumStageAxis.MaterialY, Y);
                    }
                    else
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialY, Y);

                        AxisAbsoluteMove(EnumStageAxis.MaterialX, X);
                    }
                }

            }
            else//物料勾爪远离柱子
            {
                if ((Z < SystemConfiguration.Instance.PositioningConfig.MaterialZMinZ) || (Z > SystemConfiguration.Instance.PositioningConfig.MaterialZMaxZ)) //物料勾爪在压机中间，先将Z移动到最合适位置
                {
                    AxisAbsoluteMove(EnumStageAxis.MaterialZ, SystemConfiguration.Instance.PositioningConfig.MaterialZmostsuitableZ);

                    if (Y < SystemConfiguration.Instance.PositioningConfig.MaterialXcannotmovedLocation1.Y)//目标位置在柱子附近
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialX, X);

                        AxisAbsoluteMove(EnumStageAxis.MaterialY, Y);
                    }
                    else
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialY, Y);

                        AxisAbsoluteMove(EnumStageAxis.MaterialX, X);
                    }

                    AxisAbsoluteMove(EnumStageAxis.MaterialZ, Z);
                }
                else 
                {
                    AxisAbsoluteMove(EnumStageAxis.MaterialZ, Z);

                    if (Y < SystemConfiguration.Instance.PositioningConfig.MaterialXcannotmovedLocation1.Y)//目标位置在柱子附近
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialX, X);

                        AxisAbsoluteMove(EnumStageAxis.MaterialY, Y);
                    }
                    else
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialY, Y);

                        AxisAbsoluteMove(EnumStageAxis.MaterialX, X);
                    }
                }
            }




        }


        /// <summary>
        /// 料盒钩爪移动
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        private void MaterialboxHookXYZAbsoluteMove(double X, double Y, double Z)
        {
            //stage.AbloluteMoveSync(new EnumStageAxis[3] { EnumStageAxis.BondX, EnumStageAxis.BondY, EnumStageAxis.BondZ }, new double[3] { X, Y, Z });
            stage.ClrAlarm(EnumStageAxis.MaterialboxZ);
            stage.AbloluteMoveSync(EnumStageAxis.MaterialboxZ, Z);

            stage.ClrAlarm(EnumStageAxis.MaterialboxX);
            stage.AbloluteMoveSync(EnumStageAxis.MaterialboxX, X);

            stage.ClrAlarm(EnumStageAxis.MaterialboxY);
            stage.AbloluteMoveSync(EnumStageAxis.MaterialboxY, Y);


            //EnumStageAxis[] multiAxis = new EnumStageAxis[2];
            //multiAxis[0] = EnumStageAxis.MaterialboxX;
            //multiAxis[1] = EnumStageAxis.MaterialboxY;

            //double[] target1 = new double[2];

            //target1[0] = X;
            //target1[1] = Y;

            //_positioningSystem.MoveAixsToStageCoord(multiAxis, target1, EnumCoordSetType.Absolute);
        }

        ///// <summary>
        ///// 物料钩爪移动
        ///// </summary>
        ///// <param name="X"></param>
        ///// <param name="Y"></param>
        ///// <param name="Z"></param>
        //private void MaterialHookXYZAbsoluteMove(double X, double Y, double Z)
        //{
        //    //stage.AbloluteMoveSync(new EnumStageAxis[3] { EnumStageAxis.BondX, EnumStageAxis.BondY, EnumStageAxis.BondZ }, new double[3] { X, Y, Z });
        //    stage.ClrAlarm(EnumStageAxis.MaterialZ);
        //    stage.AbloluteMoveSync(EnumStageAxis.MaterialZ, Z);

        //    stage.ClrAlarm(EnumStageAxis.MaterialX);
        //    stage.AbloluteMoveSync(EnumStageAxis.MaterialX, X);

        //    stage.ClrAlarm(EnumStageAxis.MaterialY);
        //    stage.AbloluteMoveSync(EnumStageAxis.MaterialY, Y);


        //    //EnumStageAxis[] multiAxis = new EnumStageAxis[2];
        //    //multiAxis[0] = EnumStageAxis.MaterialX;
        //    //multiAxis[1] = EnumStageAxis.MaterialY;

        //    //double[] target1 = new double[2];

        //    //target1[0] = X;
        //    //target1[1] = Y;

        //    //_positioningSystem.MoveAixsToStageCoord(multiAxis, target1, EnumCoordSetType.Absolute);
        //}


        private bool AxisAbsoluteMove(EnumStageAxis axis, double target)
        {
            stage.ClrAlarm(axis);
            stage.AbloluteMoveSync(axis, target);

            double position = stage.GetCurrentPosition(axis);
            int T = 0;
            if (Math.Abs(position - target) > 0.5 || T < 900)
            {
                while (Math.Abs(position - target) > 0.5)
                {
                    position = stage.GetCurrentPosition(axis);
                    if (Math.Abs(position - target) < 0.5)
                    {
                        break;
                    }
                    T++;
                    Thread.Sleep(100);
                }
            }
            if (Math.Abs(position - target) > 0.5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void AxisRelativeMove(EnumStageAxis axis, double target)
        {
            stage.RelativeMoveSync(axis, (float)target);
        }

        public double ReadCurrentAxisposition(EnumStageAxis axis)
        {

            double position = stage.GetCurrentPosition(axis);
            //double position = 2;
            return position;
        }

        private void AxisHome(EnumStageAxis axis)
        {
            //stage.Home(axis);
        }

        private void SetAxisSpeed(EnumStageAxis axis, float speed)
        {
            stage.SetAxisSpeed(axis, speed);
        }


        #endregion


        #region public mothd

        /// <summary>
        /// 烘箱1抽真空
        /// </summary>
        /// <returns></returns>
        public int OvenBox1VacuumPumping()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    _plc.OvenBox1VacuumPumping();

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 烘箱1停止抽真空
        /// </summary>
        /// <returns></returns>
        public int StopOvenBox1VacuumPumping()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    _plc.StopOvenBox1VacuumPumping();

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 烘箱2抽真空
        /// </summary>
        /// <returns></returns>
        public int OvenBox2VacuumPumping()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    _plc.OvenBox2VacuumPumping();

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 烘箱2停止抽真空
        /// </summary>
        /// <returns></returns>
        public int StopOvenBox2VacuumPumping()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    _plc.StopOvenBox2VacuumPumping();

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 方舱抽真空
        /// </summary>
        /// <returns></returns>
        public int BoxVacuumPumping()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    _plc.BoxVacuumPumping();

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 方舱停止抽真空
        /// </summary>
        /// <returns></returns>
        public int Box1VacuumPumping()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    _plc.StopBoxVacuumPumping();

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 设置传送配方
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public int SetTransportParam(TransportRecipe recipe)
        {
            if (recipe != null)
            {
                try
                {
                    _transportRecipe = recipe;
                    return 0;
                }
                catch (Exception)
                {
                    _transportRecipe = null;
                    return -1;
                }
            }
            else
            {
                _transportRecipe = null;
            }

            return -1;
        }

        /// <summary>
        /// 获取当前传送配方
        /// </summary>
        /// <returns></returns>
        public TransportRecipe GetTransportParam()
        {

            return _transportRecipe;
        }

        /// <summary>
        /// 读取烘箱压力
        /// </summary>
        /// <param name="OverBoxNum"></param>
        /// <returns></returns>
        public double ReadOvenVacuum(EnumOvenBoxNum OverBoxNum)
        {
            if(OverBoxNum == EnumOvenBoxNum.Oven1)
            {
                //return _plc.Read<int>(EnumBoardcardDefineInputIO.BakeOvenPressure);
            }
            else if (OverBoxNum == EnumOvenBoxNum.Oven2)
            {
                //return _plc.Read<int>(EnumBoardcardDefineInputIO.BakeOven2Pressure);
            }
            return 0;
        }

        /// <summary>
        /// 读取工作箱压力
        /// </summary>
        /// <returns></returns>
        public double ReadBoxVacuum()
        {
            //return _plc.Read<int>(EnumBoardcardDefineInputIO.BoxPressure);
            return 0;
        }

        /// <summary>
        /// 读取传感器信号
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns></returns>
        public bool Readsensor(EnumSensor sensor)
        {
            return true;
            if (sensor == EnumSensor.Oven1InteriorDoorClose)
            {
                return _plc.Read<bool>(EnumBoardcardDefineInputIO.BakeOvenInnerdoorClosestatus);
            }
            else if (sensor == EnumSensor.Oven1InteriorDoorOpen)
            {
                return _plc.Read<bool>(EnumBoardcardDefineInputIO.BakeOvenInnerdoorOpenstatus);
            }
            else if (sensor == EnumSensor.Oven2InteriorDoorClose)
            {
                return _plc.Read<bool>(EnumBoardcardDefineInputIO.BakeOven2InnerdoorClosestatus);
            }
            else if (sensor == EnumSensor.Oven2InteriorDoorOpen)
            {
                return _plc.Read<bool>(EnumBoardcardDefineInputIO.BakeOven2InnerdoorOpenstatus);
            }
            else if (sensor == EnumSensor.Oven1BakeOvenAerate)
            {
                return _plc.Read<bool>(EnumBoardcardDefineOutputIO.BakeOvenAerate);
            }
            else if (sensor == EnumSensor.Oven2BakeOvenAerate)
            {
                return _plc.Read<bool>(EnumBoardcardDefineOutputIO.BakeOven2Aerate);
            }
            else if (sensor == EnumSensor.BoxBakeOvenAerate)
            {
                return _plc.Read<bool>(EnumBoardcardDefineOutputIO.BoxAerate);
            }

            return false;
        }

        /// <summary>
        /// 打开烘箱补气阀
        /// </summary>
        /// <param name="OverBoxNum"></param>
        /// <returns></returns>
        public int OpenOvenBoxAerates(EnumOvenBoxNum OverBoxNum)
        {
            if (_transportRecipe != null)
            {
                try
                {
                    if(OverBoxNum == EnumOvenBoxNum.Oven1)
                    {
                        _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenAerate, true);
                    }
                    else if (OverBoxNum == EnumOvenBoxNum.Oven2)
                    {
                        _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2Aerate, true);
                    }
                    else if(OverBoxNum == EnumOvenBoxNum.Box)
                    {
                        _plc.Write<bool>(EnumBoardcardDefineOutputIO.BoxAerate, true);
                    }

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 关闭烘箱补气阀
        /// </summary>
        /// <param name="OverBoxNum"></param>
        /// <returns></returns>
        public int CloseOvenBoxAerates(EnumOvenBoxNum OverBoxNum)
        {
            if (_transportRecipe != null)
            {
                try
                {
                    if (OverBoxNum == EnumOvenBoxNum.Oven1)
                    {
                        _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenAerate, false);
                    }
                    else if (OverBoxNum == EnumOvenBoxNum.Oven2)
                    {
                        _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2Aerate, false);
                    }
                    else if (OverBoxNum == EnumOvenBoxNum.Box)
                    {
                        _plc.Write<bool>(EnumBoardcardDefineOutputIO.BoxAerate, false);
                    }

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// 打开烘箱内门
        /// </summary>
        /// <param name="OverBoxNum"></param>
        /// <returns></returns>
        public int OpenOvenBoxInteriorDoor(EnumOvenBoxNum OverBoxNum)
        {
            if (_transportRecipe != null)
            {
                try
                {
                    _plc.OpenOvenboxInnerDoor(OverBoxNum);

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 关闭烘箱内门
        /// </summary>
        /// <param name="OverBoxNum"></param>
        /// <returns></returns>
        public int CloseOvenBoxInteriorDoor(EnumOvenBoxNum OverBoxNum)
        {
            if (_transportRecipe != null)
            {
                try
                {
                    _plc.CloseOvenboxInnerDoor(OverBoxNum);

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// 料盘钩爪到避让位置1（只能在空闲位置时，或者不影响物料勾爪时进行移动）
        /// </summary>
        /// <returns></returns>
        public int MaterialboxHooktoAvoidPositionAction()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    double MaterialboxX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                    double MaterialboxY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                    double MaterialboxZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                    double MaterialboxT = ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                    if (((MaterialboxY < SystemConfiguration.Instance.PositioningConfig.MaterialBoxZcannotmovedLocation1.Y)) &&
                        (Math.Abs(MaterialboxT - SystemConfiguration.Instance.PositioningConfig.MaterialBoxZcannotmovedLocation1.Theta) < 70))//料盘勾爪在物料勾爪下方时，不能移动
                    {
                        return -1;

                    }
                    else//料盘勾爪不在物料勾爪下方，可以先动XY
                    {
                        if (((MaterialboxY > SystemConfiguration.Instance.PositioningConfig.MaterialBoxXcannotmovedLocation1.Y)) &&
                        (Math.Abs(MaterialboxT - SystemConfiguration.Instance.PositioningConfig.MaterialBoxXcannotmovedLocation1.Theta) < 10))//料盘勾爪在烘箱A中，不能移动
                        {
                            return -1;
                        }
                        else if (((MaterialboxX > SystemConfiguration.Instance.PositioningConfig.MaterialBoxYcannotmovedLocation1.X)) &&
                        (Math.Abs(MaterialboxT - SystemConfiguration.Instance.PositioningConfig.MaterialBoxYcannotmovedLocation1.Theta) < 10))//料盘勾爪在烘箱B中，不能移动
                        {
                            return -1;
                        }
                    }

                    AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookAvoidLocation.Z);

                    AxisAbsoluteMove(EnumStageAxis.MaterialboxT, SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookAvoidLocation.Theta);

                    AxisAbsoluteMove(EnumStageAxis.MaterialboxX, SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookAvoidLocation.X);

                    AxisAbsoluteMove(EnumStageAxis.MaterialboxY, SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookAvoidLocation.Y);


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// 料盘钩爪到空闲位置
        /// </summary>
        /// <returns></returns>
        public int MaterialboxHooktoSafePositionAction(int mode = 0)
        {
            if (_transportRecipe != null)
            {
                try
                {
                    AxisAbsoluteMove(EnumStageAxis.MaterialboxHook, _transportRecipe.MaterialboxHookOpen);

                    //AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, _transportRecipe.MaterialboxHookSafePosition.Z);

                    //AxisAbsoluteMove(EnumStageAxis.MaterialboxT, _transportRecipe.MaterialboxHookSafePosition.Theta);

                    //MaterialboxHookXYZAbsoluteMove(_transportRecipe.MaterialboxHookSafePosition.X, _transportRecipe.MaterialboxHookSafePosition.Y, _transportRecipe.MaterialboxHookSafePosition.Z);

                    MaterialboxHookXYZTAbsoluteMove(SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.X, SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.Y,
                        SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.Z, SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.Theta, mode);

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }




        /// <summary>
        /// 料盒出烘箱
        /// </summary>
        /// <param name="OverBoxNum"></param>
        /// <returns></returns>
        public int MaterialboxOutofovenAction(EnumOvenBoxNum OverBoxNum)
        {

            if (_transportRecipe != null)
            {
                try
                {
                    if (OverBoxNum == EnumOvenBoxNum.Oven1)
                    {
                        AxisAbsoluteMove(EnumStageAxis.OverTrack1, _transportRecipe.OverTrackMaterialboxOutofoven);
                    }
                    else if (OverBoxNum == EnumOvenBoxNum.Oven2)
                    {
                        AxisAbsoluteMove(EnumStageAxis.OverTrack2, _transportRecipe.OverTrack2MaterialboxOutofoven);
                    }


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 料盒钩爪到烘箱出料盒的上方
        /// </summary>
        /// <param name="OverBoxNum"></param>
        /// <returns></returns>
        public int MaterialboxHooktoMaterialboxAction(EnumOvenBoxNum OverBoxNum)
        {

            if (_transportRecipe != null)
            {
                try
                {
                    if (_transportRecipe.MaterialboxHookUp < 12)
                    {
                        _transportRecipe.MaterialboxHookUp = 12;
                    }
                    if (_transportRecipe.MaterialboxHookUp2 < 12)
                    {
                        _transportRecipe.MaterialboxHookUp2 = 12;
                    }

                    //AxisAbsoluteMove(EnumStageAxis.MaterialboxHook, _transportRecipe.MaterialboxHookOpen);

                    //AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, _transportRecipe.MaterialboxHookSafePosition.Z);

                    double MaterialboxHookX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                    double MaterialboxHookY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                    double MaterialboxHookZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                    double MaterialboxHookT = ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                    if (OverBoxNum == EnumOvenBoxNum.Oven1)
                    {
                        //AxisAbsoluteMove(EnumStageAxis.MaterialboxT, _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Theta);

                        //MaterialboxHookXYZAbsoluteMove(_transportRecipe.MaterialboxHooktoMaterialboxPosition1.X, _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Y, _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Z + _transportRecipe.MaterialboxHookUp);

                        MaterialboxHookXYZTAbsoluteMove(_transportRecipe.MaterialboxHooktoMaterialboxPosition1.X, _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Y,
                        _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Z + _transportRecipe.MaterialboxHookUp, _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Theta);

                        MaterialboxHookX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                        MaterialboxHookY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                        MaterialboxHookZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                        MaterialboxHookT = ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                        if (MaterialboxHookX != _transportRecipe.MaterialboxHooktoMaterialboxPosition1.X || MaterialboxHookY != _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Y
                            || MaterialboxHookZ != _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Z + _transportRecipe.MaterialboxHookUp || MaterialboxHookT != _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Theta)
                        {
                            int T = 0;
                            while (T > 300)
                            {
                                MaterialboxHookX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                                MaterialboxHookY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                                MaterialboxHookZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                                MaterialboxHookT = ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                                if (Math.Abs(MaterialboxHookX - _transportRecipe.MaterialboxHooktoMaterialboxPosition1.X) < 0.5 && Math.Abs(MaterialboxHookY - _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Y) < 0.5
                                    && Math.Abs(MaterialboxHookZ - _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Z - _transportRecipe.MaterialboxHookUp) < 1.5 && Math.Abs(MaterialboxHookT - _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Theta) < 0.5)
                                {
                                    break;
                                }


                                Thread.Sleep(100);
                            }
                        }

                        if (!(Math.Abs(MaterialboxHookX - _transportRecipe.MaterialboxHooktoMaterialboxPosition1.X) < 0.5 && Math.Abs(MaterialboxHookY - _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Y) < 0.5
                                    && Math.Abs(MaterialboxHookZ - _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Z - _transportRecipe.MaterialboxHookUp) < 1.5 && Math.Abs(MaterialboxHookT - _transportRecipe.MaterialboxHooktoMaterialboxPosition1.Theta) < 0.5))
                        {
                            return -1;
                        }

                    }
                    else if (OverBoxNum == EnumOvenBoxNum.Oven2)
                    {
                        //AxisAbsoluteMove(EnumStageAxis.MaterialboxT, _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Theta);

                        //MaterialboxHookXYZAbsoluteMove(_transportRecipe.MaterialboxHooktoMaterialboxPosition2.X, _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Y, _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Z + _transportRecipe.MaterialboxHookUp2);

                        MaterialboxHookXYZTAbsoluteMove(_transportRecipe.MaterialboxHooktoMaterialboxPosition2.X, _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Y,
                        _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Z + _transportRecipe.MaterialboxHookUp2, _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Theta);


                        MaterialboxHookX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                        MaterialboxHookY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                        MaterialboxHookZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                        MaterialboxHookT = ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                        if (MaterialboxHookX != _transportRecipe.MaterialboxHooktoMaterialboxPosition2.X || MaterialboxHookY != _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Y
                            || MaterialboxHookZ != _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Z + _transportRecipe.MaterialboxHookUp2 || MaterialboxHookT != _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Theta)
                        {
                            int T = 0;
                            while (T > 300)
                            {
                                MaterialboxHookX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                                MaterialboxHookY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                                MaterialboxHookZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                                MaterialboxHookT = ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                                if (Math.Abs(MaterialboxHookX - _transportRecipe.MaterialboxHooktoMaterialboxPosition2.X) < 0.5 && Math.Abs(MaterialboxHookY - _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Y) < 0.5
                                    && Math.Abs(MaterialboxHookZ - _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Z - _transportRecipe.MaterialboxHookUp2) < 1.5 && Math.Abs(MaterialboxHookT - _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Theta) < 0.5)
                                {
                                    break;
                                }


                                Thread.Sleep(100);
                            }
                        }

                        if (!(Math.Abs(MaterialboxHookX - _transportRecipe.MaterialboxHooktoMaterialboxPosition2.X) < 0.5 && Math.Abs(MaterialboxHookY - _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Y) < 0.5
                                    && Math.Abs(MaterialboxHookZ - _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Z - _transportRecipe.MaterialboxHookUp2) < 1.5 && Math.Abs(MaterialboxHookT - _transportRecipe.MaterialboxHooktoMaterialboxPosition2.Theta) < 0.5))
                        {
                            return -1;
                        }


                    }





                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 料盒钩爪拾取料盒
        /// </summary>
        /// <param name="TargetZ"></param>
        /// <param name="HookUp"></param>
        /// <returns></returns>
        public int MaterialboxHookPickupMaterialboxAction(double TargetZ, double HookUp)
        {
            if (_transportRecipe != null)
            {
                try
                {
                    double MaterialboxX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                    double MaterialboxY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                    double MaterialboxZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                    double MaterialboxT = ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                    AxisAbsoluteMove(EnumStageAxis.MaterialboxHook, _transportRecipe.MaterialboxHookOpen);

                    AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, TargetZ);

                    AxisAbsoluteMove(EnumStageAxis.MaterialboxHook, _transportRecipe.MaterialboxHookClose);


                    if (((MaterialboxY > SystemConfiguration.Instance.PositioningConfig.MaterialBoxXcannotmovedLocation1.Y)) &&
                    (Math.Abs(MaterialboxT - SystemConfiguration.Instance.PositioningConfig.MaterialBoxXcannotmovedLocation1.Theta) < 10))//料盘勾爪在烘箱A中，此时不能先动X轴
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxZinOvenBoxMaxZ);
                    }
                    else if (((MaterialboxX > SystemConfiguration.Instance.PositioningConfig.MaterialBoxYcannotmovedLocation1.X)) &&
                    (Math.Abs(MaterialboxT - SystemConfiguration.Instance.PositioningConfig.MaterialBoxYcannotmovedLocation1.Theta) < 10))//料盘勾爪在烘箱B中，此时不能先动Y轴
                    {
                        AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, SystemConfiguration.Instance.PositioningConfig.MaterialBoxZinOvenBoxMaxZ);
                    }
                    else
                    {
                        AxisRelativeMove(EnumStageAxis.MaterialboxZ, HookUp);
                    }
                    


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 料盒到目标位置
        /// </summary>
        /// <param name="Target"></param>
        /// <returns></returns>
        public int MaterialboxHooktoTargetPositionAction(XYZTCoordinateConfig Target, double HookUp)
        {

            if (_transportRecipe != null)
            {
                try
                {
                    //AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, Target.Z + HookUp);

                    //double MaterialboxHookX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                    //double MaterialboxHookY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);


                    //MaterialboxHookXYZAbsoluteMove(Target.X, Target.Y, Target.Z + HookUp);

                    //AxisAbsoluteMove(EnumStageAxis.MaterialboxT, Target.Theta);


                    MaterialboxHookXYZTAbsoluteMove(Target.X, Target.Y, Target.Z + HookUp, Target.Theta);

                    double MaterialboxHookX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                    double MaterialboxHookY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                    double MaterialboxHookZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                    double MaterialboxHookT = ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                    if (MaterialboxHookX != Target.X || MaterialboxHookY != Target.Y || MaterialboxHookZ != Target.Z + HookUp || MaterialboxHookT != Target.Theta)
                    {
                        int T = 0;
                        while (T > 900)
                        {
                            MaterialboxHookX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                            MaterialboxHookY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                            MaterialboxHookZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                            MaterialboxHookT = ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                            if (Math.Abs(MaterialboxHookX - Target.X) < 0.5 && Math.Abs(MaterialboxHookY - Target.Y) < 0.5
                                && Math.Abs(MaterialboxHookZ - Target.Z - HookUp) < 1.5 && Math.Abs(MaterialboxHookT - Target.Theta) < 0.5)
                            {
                                break;
                            }


                            Thread.Sleep(100);
                        }
                    }

                    if (!(Math.Abs(MaterialboxHookX - Target.X) < 0.5 && Math.Abs(MaterialboxHookY - Target.Y) < 0.5
                                && Math.Abs(MaterialboxHookZ - Target.Z - HookUp) < 1.5 && Math.Abs(MaterialboxHookT - Target.Theta) < 0.5))
                    {
                        return -1;
                    }


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 料盒到焊接位置
        /// </summary>
        /// <returns></returns>
        public int MaterialboxHooktoWeldPositionAction()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, _transportRecipe.MaterialboxHooktoWeldPosition.Z);

                    double MaterialboxHookX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                    double MaterialboxHookY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);


                    MaterialboxHookXYZAbsoluteMove(_transportRecipe.MaterialboxHooktoWeldPosition.X, _transportRecipe.MaterialboxHooktoWeldPosition.Y, _transportRecipe.MaterialboxHooktoWeldPosition.Z);

                    AxisAbsoluteMove(EnumStageAxis.MaterialboxT, _transportRecipe.MaterialboxHooktoWeldPosition.Theta);

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 料盒钩爪放下料盒
        /// </summary>
        /// <param name="TargetZ"></param>
        /// <param name="HookUp"></param>
        /// <returns></returns>
        public int MaterialboxHookPutdownMaterialboxAction(double TargetZ, double HookUp)
        {
            if (_transportRecipe != null)
            {
                try
                {
                    AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, TargetZ);

                    AxisAbsoluteMove(EnumStageAxis.MaterialboxHook, _transportRecipe.MaterialboxHookOpen);

                    AxisRelativeMove(EnumStageAxis.MaterialboxZ, HookUp);


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        


        /// <summary>
        /// 料盒进烘箱
        /// </summary>
        /// <param name="OverBoxNum"></param>
        /// <returns></returns>
        public int MaterialboxInofovenAction(EnumOvenBoxNum OverBoxNum)
        {
            if (_transportRecipe != null)
            {
                try
                {
                    if (OverBoxNum == EnumOvenBoxNum.Oven1)
                    {
                        AxisAbsoluteMove(EnumStageAxis.OverTrack1, _transportRecipe.OverTrack1MaterialboxInofoven);
                    }
                    else if (OverBoxNum == EnumOvenBoxNum.Oven2)
                    {
                        AxisAbsoluteMove(EnumStageAxis.OverTrack2, _transportRecipe.OverTrack2MaterialboxInofoven);
                    }



                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }



        /// <summary>
        /// 料盒出烘箱（腔体外）提醒,并判断是否进料
        /// </summary>
        /// <param name="OverBoxNum"></param>
        /// <returns></returns>
        public int MaterialBoxOutofovenRemindAction(EnumOvenBoxNum OverBoxNum)
        {

            if (_transportRecipe != null)
            {
                try
                {
                    if (OverBoxNum == EnumOvenBoxNum.Oven1)
                    {
                        _plc.MaterialBoxOutofoven1Remind();
                    }
                    else if (OverBoxNum == EnumOvenBoxNum.Oven2)
                    {
                        _plc.MaterialBoxOutofoven2Remind();
                    }


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 料盒进烘箱（腔体外）提醒,并判断是否进料
        /// </summary>
        /// <param name="OverBoxNum"></param>
        /// <returns></returns>
        public int MaterialBoxIntoovenRemindAction(EnumOvenBoxNum OverBoxNum)
        {

            if (_transportRecipe != null)
            {
                try
                {
                    if (OverBoxNum == EnumOvenBoxNum.Oven1)
                    {
                        _plc.MaterialBoxIntooven1Remind();
                    }
                    else if (OverBoxNum == EnumOvenBoxNum.Oven2)
                    {
                        _plc.MaterialBoxIntooven2Remind();
                    }


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }



        /// <summary>
        /// 物料钩爪到避让位置1（只能在空闲位置时，或者不影响物料勾爪时进行移动）
        /// </summary>
        /// <returns></returns>
        public int MaterialHooktoAvoidPositionAction()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    double MaterialX = ReadCurrentAxisposition(EnumStageAxis.MaterialX);
                    double MaterialY = ReadCurrentAxisposition(EnumStageAxis.MaterialY);
                    double MaterialZ = ReadCurrentAxisposition(EnumStageAxis.MaterialZ);

                    if ((MaterialY < SystemConfiguration.Instance.PositioningConfig.MaterialXcannotmovedLocation1.Y))//物料勾爪靠近柱子，不能移动
                    {
                        if(Math.Abs(MaterialX - SystemConfiguration.Instance.PositioningConfig.MaterialhookAvoidLocation.X) < 0.5 &&
                            Math.Abs(MaterialY - SystemConfiguration.Instance.PositioningConfig.MaterialhookAvoidLocation.Y) < 0.5 &&
                            Math.Abs(MaterialZ - SystemConfiguration.Instance.PositioningConfig.MaterialhookAvoidLocation.Z) < 0.5)
                        {
                            return 0;
                        }

                        return -1;

                    }


                    AxisAbsoluteMove(EnumStageAxis.MaterialZ, SystemConfiguration.Instance.PositioningConfig.MaterialhookAvoidLocation.Z);

                    AxisAbsoluteMove(EnumStageAxis.MaterialX, SystemConfiguration.Instance.PositioningConfig.MaterialhookAvoidLocation.X);

                    AxisAbsoluteMove(EnumStageAxis.MaterialY, SystemConfiguration.Instance.PositioningConfig.MaterialhookAvoidLocation.Y);


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 物料钩爪到空闲位置
        /// </summary>
        /// <returns></returns>
        public int MaterialHooktoSafePositionAction()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    AxisAbsoluteMove(EnumStageAxis.MaterialHook, _transportRecipe.MaterialHookOpen);

                    //AxisAbsoluteMove(EnumStageAxis.MaterialZ, _transportRecipe.MaterialHookSafePosition.Z);

                    MaterialHookXYZAbsoluteMove(SystemConfiguration.Instance.PositioningConfig.MaterialhookSafeLocation.X, SystemConfiguration.Instance.PositioningConfig.MaterialhookSafeLocation.Y, SystemConfiguration.Instance.PositioningConfig.MaterialhookSafeLocation.Z);


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 物料钩爪到物料上方
        /// </summary>
        /// <param name="MaterialPosition"></param>
        /// <returns></returns>
        public int MaterialHooktoMaterialAction(XYZTCoordinateConfig MaterialPosition, double HookUp)
        {
            if (_transportRecipe != null)
            {
                try
                {
                    //AxisAbsoluteMove(EnumStageAxis.MaterialHook, _transportRecipe.MaterialboxHookOpen);

                    //AxisAbsoluteMove(EnumStageAxis.MaterialZ, MaterialPosition.Z + HookUp);

                    //double MaterialboxHookX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                    //double MaterialboxHookY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);

                    MaterialHookXYZAbsoluteMove(MaterialPosition.X, MaterialPosition.Y, MaterialPosition.Z + HookUp);


                    double MaterialboxX = ReadCurrentAxisposition(EnumStageAxis.MaterialX);
                    double MaterialboxY = ReadCurrentAxisposition(EnumStageAxis.MaterialY);
                    double MaterialboxZ = ReadCurrentAxisposition(EnumStageAxis.MaterialZ);

                    if (MaterialboxX != MaterialPosition.X || MaterialboxY != MaterialPosition.Y || MaterialboxZ != MaterialPosition.Z + HookUp)
                    {
                        int T = 0;
                        while (T > 300)
                        {
                            MaterialboxX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                            MaterialboxY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                            MaterialboxZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);

                            if (Math.Abs(MaterialboxX - MaterialPosition.X) < 0.5 && Math.Abs(MaterialboxY - MaterialPosition.Y) < 0.5
                                && Math.Abs(MaterialboxZ - MaterialPosition.Z - HookUp) < 0.5)
                            {
                                break;
                            }


                            Thread.Sleep(100);
                        }
                    }

                    if (!(Math.Abs(MaterialboxX - MaterialPosition.X) < 0.5 && Math.Abs(MaterialboxY - MaterialPosition.Y) < 0.5
                                && Math.Abs(MaterialboxZ - MaterialPosition.Z - HookUp) < 0.5))
                    {
                        return -1;
                    }


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 物料钩爪拾取物料
        /// </summary>
        /// <param name="TargetZ"></param>
        /// <returns></returns>
        public int MaterialHookPickupMaterialAction(double TargetZ, double HookUp)
        {
            if (_transportRecipe != null)
            {
                try
                {
                    AxisAbsoluteMove(EnumStageAxis.MaterialHook, _transportRecipe.MaterialHookOpen);

                    AxisAbsoluteMove(EnumStageAxis.MaterialZ, TargetZ);

                    AxisAbsoluteMove(EnumStageAxis.MaterialHook, _transportRecipe.MaterialHookClose);

                    AxisRelativeMove(EnumStageAxis.MaterialZ, HookUp);


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 物料钩爪到目标位置
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public int MaterialHooktoTargetPositionAction(XYZTCoordinateConfig Target, double Hookup)
        {
            if (_transportRecipe != null)
            {
                try
                {
                    //AxisAbsoluteMove(EnumStageAxis.MaterialZ, Target.Z + Hookup);




                    MaterialHookXYZAbsoluteMove(Target.X, Target.Y, Target.Z + Hookup);

                    double MaterialboxX = ReadCurrentAxisposition(EnumStageAxis.MaterialX);
                    double MaterialboxY = ReadCurrentAxisposition(EnumStageAxis.MaterialY);
                    double MaterialboxZ = ReadCurrentAxisposition(EnumStageAxis.MaterialZ);

                    if (MaterialboxX != Target.X || MaterialboxY != Target.Y || MaterialboxZ != Target.Z + Hookup)
                    {
                        int T = 0;
                        while (T > 300)
                        {
                            MaterialboxX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                            MaterialboxY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                            MaterialboxZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);

                            if (Math.Abs(MaterialboxX - Target.X) < 0.5 && Math.Abs(MaterialboxY - Target.Y) < 0.5
                                && Math.Abs(MaterialboxZ - Target.Z - Hookup) < 0.5)
                            {
                                break;
                            }


                            Thread.Sleep(100);
                        }
                    }

                    if (!(Math.Abs(MaterialboxX - Target.X) < 0.5 && Math.Abs(MaterialboxY - Target.Y) < 0.5
                                && Math.Abs(MaterialboxZ - Target.Z - Hookup) < 0.5))
                    {
                        return -1;
                    }

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 物料钩爪放下物料
        /// </summary>
        /// <param name="TargetZ"></param>
        /// <returns></returns>
        public int MaterialHookPutdownMaterialAction(double TargetZ, double Hookup)
        {
            if (_transportRecipe != null)
            {
                try
                {
                    AxisAbsoluteMove(EnumStageAxis.MaterialZ, TargetZ);

                    AxisAbsoluteMove(EnumStageAxis.MaterialHook, _transportRecipe.MaterialHookOpen);

                    AxisRelativeMove(EnumStageAxis.MaterialZ, Hookup);


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 顶升顶起物料
        /// </summary>
        /// <returns></returns>
        public int PressliftingLiftUpMaterialAction()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    AxisAbsoluteMove(EnumStageAxis.Presslifting, _transportRecipe.PressliftingWorkPosition);


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 顶升降下物料
        /// </summary>
        /// <returns></returns>
        public int PressliftingPutdownMaterialAction()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    AxisAbsoluteMove(EnumStageAxis.Presslifting, _transportRecipe.PressliftingSafePosition);


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 焊接物料
        /// </summary>
        /// <returns></returns>
        public int WeldMaterialAction()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    _plc.Weld((int)TransportRecipe.WeldTime);


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// 焊接复位
        /// </summary>
        /// <returns></returns>
        public int WeldResetAction()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    _plc.RestWeld();


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// 停止焊接
        /// </summary>
        /// <returns></returns>
        public int StopWeldMaterialAction()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    _plc.StopWeld();


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// 停止复位
        /// </summary>
        /// <returns></returns>
        public int StopWeldResetAction()
        {
            if (_transportRecipe != null)
            {
                try
                {
                    _plc.StopRestWeld();


                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }

        }


        public int SetRunSpeed(EnumRunSpeed speed)
        {
            try
            {
                if(speed == EnumRunSpeed.Low)
                {
                    AxisConfig axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxX);
                    SetAxisSpeed(EnumStageAxis.MaterialboxX, (float)axisConfig.LowAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxY);
                    SetAxisSpeed(EnumStageAxis.MaterialboxY, (float)axisConfig.LowAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxZ);
                    SetAxisSpeed(EnumStageAxis.MaterialboxZ, (float)axisConfig.LowAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxT);
                    SetAxisSpeed(EnumStageAxis.MaterialboxT, (float)axisConfig.LowAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxHook);
                    SetAxisSpeed(EnumStageAxis.MaterialboxHook, (float)axisConfig.LowAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialX);
                    SetAxisSpeed(EnumStageAxis.MaterialX, (float)axisConfig.LowAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialY);
                    SetAxisSpeed(EnumStageAxis.MaterialY, (float)axisConfig.LowAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialZ);
                    SetAxisSpeed(EnumStageAxis.MaterialZ, (float)axisConfig.LowAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialHook);
                    SetAxisSpeed(EnumStageAxis.MaterialHook, (float)axisConfig.LowAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.OverTrack1);
                    SetAxisSpeed(EnumStageAxis.OverTrack1, (float)axisConfig.LowAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.OverTrack2);
                    SetAxisSpeed(EnumStageAxis.OverTrack2, (float)axisConfig.LowAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.Presslifting);
                    SetAxisSpeed(EnumStageAxis.Presslifting, (float)axisConfig.LowAxisSpeed);


                }
                else if (speed == EnumRunSpeed.Medium)
                {
                    AxisConfig axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxX);
                    SetAxisSpeed(EnumStageAxis.MaterialboxX, (float)axisConfig.MediumAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxY);
                    SetAxisSpeed(EnumStageAxis.MaterialboxY, (float)axisConfig.MediumAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxZ);
                    SetAxisSpeed(EnumStageAxis.MaterialboxZ, (float)axisConfig.MediumAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxT);
                    SetAxisSpeed(EnumStageAxis.MaterialboxT, (float)axisConfig.MediumAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxHook);
                    SetAxisSpeed(EnumStageAxis.MaterialboxHook, (float)axisConfig.MediumAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialX);
                    SetAxisSpeed(EnumStageAxis.MaterialX, (float)axisConfig.MediumAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialY);
                    SetAxisSpeed(EnumStageAxis.MaterialY, (float)axisConfig.MediumAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialZ);
                    SetAxisSpeed(EnumStageAxis.MaterialZ, (float)axisConfig.MediumAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialHook);
                    SetAxisSpeed(EnumStageAxis.MaterialHook, (float)axisConfig.MediumAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.OverTrack1);
                    SetAxisSpeed(EnumStageAxis.OverTrack1, (float)axisConfig.MediumAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.OverTrack2);
                    SetAxisSpeed(EnumStageAxis.OverTrack2, (float)axisConfig.MediumAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.Presslifting);
                    SetAxisSpeed(EnumStageAxis.Presslifting, (float)axisConfig.MediumAxisSpeed);



                }
                else if (speed == EnumRunSpeed.Hight)
                {
                    AxisConfig axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxX);
                    SetAxisSpeed(EnumStageAxis.MaterialboxX, (float)axisConfig.HighAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxY);
                    SetAxisSpeed(EnumStageAxis.MaterialboxY, (float)axisConfig.HighAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxZ);
                    SetAxisSpeed(EnumStageAxis.MaterialboxZ, (float)axisConfig.HighAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxT);
                    SetAxisSpeed(EnumStageAxis.MaterialboxT, (float)axisConfig.HighAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxHook);
                    SetAxisSpeed(EnumStageAxis.MaterialboxHook, (float)axisConfig.HighAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialX);
                    SetAxisSpeed(EnumStageAxis.MaterialX, (float)axisConfig.HighAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialY);
                    SetAxisSpeed(EnumStageAxis.MaterialY, (float)axisConfig.HighAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialZ);
                    SetAxisSpeed(EnumStageAxis.MaterialZ, (float)axisConfig.HighAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialHook);
                    SetAxisSpeed(EnumStageAxis.MaterialHook, (float)axisConfig.HighAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.OverTrack1);
                    SetAxisSpeed(EnumStageAxis.OverTrack1, (float)axisConfig.HighAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.OverTrack2);
                    SetAxisSpeed(EnumStageAxis.OverTrack2, (float)axisConfig.HighAxisSpeed);

                    axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.Presslifting);
                    SetAxisSpeed(EnumStageAxis.Presslifting, (float)axisConfig.HighAxisSpeed);



                }

                return 0;
            }
            catch
            {
                return -1;
            }
        }


        #endregion

    }
}
