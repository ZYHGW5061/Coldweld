using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using HardwareManagerClsLib;
using JoyStickControllerClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PositioningSystemClsLib
{
    public class JoyStickControl
    {
        private static volatile JoyStickControl _instance = new JoyStickControl();
        private static readonly object _lockObj = new object();
        /// <summary>
        /// 获取单例对象
        /// </summary>
        /// <returns></returns>
        public static JoyStickControl Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new JoyStickControl();
                        }
                    }
                }
                return _instance;
            }
        }



        bool HeadMode = true;// true 机头模式 false 传送模式

        JoyStickController _joystickOBJ = null;
        int Mode = 1;//轴组
        bool YGMode = false;//摇杆
        EnumStageAxis Axis1 = EnumStageAxis.None;//摇杆水平轴号
        EnumStageAxis Axis2 = EnumStageAxis.None;//摇杆垂直轴号
        public float Lowspeed = 3;//低速
        public float Highspeed = 20;//高速

        public float[] Speeds = new float[11];

        private HardwareConfiguration _hardwareConfig { get { return HardwareConfiguration.Instance; } }
        protected PositioningSystem _positionSystem
        {
            get
            {
                return PositioningSystem.Instance;
            }
        }


        private JoyStickControl()
        {
            _joystickOBJ = (JoyStickController)HardwareManager.Instance.JobStick;


            _joystickOBJ.JoystickMoveEvent += _joystickOBJ_JoystickMoveEvent;
            _joystickOBJ.JoystickButtonEvent += _joystickOBJ_JoystickButtonEvent;

            AxisConfig axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxX);
            Speeds[0] = (float)axisConfig.MaxAxisSpeed / 2;
            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxY);
            Speeds[1] = (float)axisConfig.MaxAxisSpeed / 2;
            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxZ);
            Speeds[2] = (float)axisConfig.MaxAxisSpeed / 2;
            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxT);
            Speeds[3] = (float)axisConfig.MaxAxisSpeed / 2;
            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxHook);
            Speeds[4] = (float)axisConfig.MaxAxisSpeed / 2;

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialX);
            Speeds[5] = (float)axisConfig.MaxAxisSpeed / 2;
            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialY);
            Speeds[6] = (float)axisConfig.MaxAxisSpeed / 2;
            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialZ);
            Speeds[7] = (float)axisConfig.MaxAxisSpeed / 2;
            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialHook);
            Speeds[8] = (float)axisConfig.MaxAxisSpeed / 2;

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.OverTrack1);
            Speeds[9] = (float)axisConfig.MaxAxisSpeed / 2;
            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.OverTrack2);
            Speeds[10] = (float)axisConfig.MaxAxisSpeed / 2;

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.Presslifting);
            Speeds[11] = (float)axisConfig.MaxAxisSpeed / 2;
        }

        public void JoyStickEnable(bool Enable)
        {
            _joystickOBJ.HandleEnable(Enable);

            foreach (EnumStageAxis axis in Enum.GetValues(typeof(EnumStageAxis)))
            {
                if (axis != EnumStageAxis.None)
                {
                    _positionSystem.StopJogPositive(axis);
                    Thread.Sleep(3);
                    _positionSystem.StopJogNegative(axis);
                    Thread.Sleep(3);
                }

            }
        }


        /// <summary>
        /// 手柄摇杆响应事件
        /// </summary>
        /// <param name="JoyX">左右</param>
        /// <param name="JoyY">上下</param>
        private void _joystickOBJ_JoystickMoveEvent(int JoyX, int JoyY)
        {
            try
            {


                if (YGMode == false)
                {
                    float ValueX = Math.Abs((float)(JoyX - 32767.0f) / 32767.0f * 30);
                    float ValueY = Math.Abs((float)(JoyY - 32767.0f) / 32767.0f * 30);
                    if ((int)Axis1 > -1 && (int)Axis1 < 12)
                    {
                        bool directionX = false;// false反转 true正转
                        switch (Axis1)
                        {
                            case EnumStageAxis.MaterialboxX:
                                ValueX = Math.Abs((float)(JoyX - 32767.0f) / 32767.0f * Speeds[0]);
                                directionX = true;
                                break;
                            case EnumStageAxis.MaterialX:
                                ValueX = Math.Abs((float)(JoyX - 32767.0f) / 32767.0f * Speeds[5]);
                                directionX = false;
                                break;
                            case EnumStageAxis.MaterialboxHook:
                                ValueX = Math.Abs((float)(JoyX - 32767.0f) / 32767.0f * Speeds[4]);
                                directionX = false;
                                break;
                            case EnumStageAxis.MaterialHook:
                                ValueX = Math.Abs((float)(JoyX - 32767.0f) / 32767.0f * Speeds[8]);
                                directionX = false;
                                break;
                            case EnumStageAxis.OverTrack1:
                                ValueX = Math.Abs((float)(JoyX - 32767.0f) / 32767.0f * Speeds[9]);
                                directionX = false;
                                break;
                            case EnumStageAxis.OverTrack2:
                                ValueX = Math.Abs((float)(JoyX - 32767.0f) / 32767.0f * Speeds[10]);
                                directionX = true;
                                break;
                        }



                        if (JoyX < 31500)
                        {
                            if (directionX)
                            {
                                _positionSystem.JogPositive(Axis1, ValueX);
                            }
                            else
                            {
                                _positionSystem.JogNegative(Axis1, ValueX);
                            }
                        }
                        if (JoyX >= 31500 && JoyX <= 34034)
                        {
                            _positionSystem.StopJogPositive(Axis1);
                            Thread.Sleep(3);
                            _positionSystem.StopJogNegative(Axis1);
                        }
                        if (JoyX > 34034)
                        {
                            if (directionX)
                            {
                                _positionSystem.JogNegative(Axis1, ValueX);
                            }
                            else
                            {
                                _positionSystem.JogPositive(Axis1, ValueX);
                            }
                        }
                    }

                    if ((int)Axis2 > -1 && (int)Axis2 < 19)
                    {
                        bool directionY = false;// false反转 true正转
                        switch (Axis2)
                        {
                            case EnumStageAxis.MaterialboxY:
                                ValueY = Math.Abs((float)(JoyY - 32767.0f) / 32767.0f * Speeds[1]);
                                directionY = true;
                                break;
                            case EnumStageAxis.MaterialboxZ:
                                ValueY = Math.Abs((float)(JoyY - 32767.0f) / 32767.0f * Speeds[2]);
                                directionY = true;
                                break;
                            case EnumStageAxis.MaterialY:
                                ValueY = Math.Abs((float)(JoyY - 32767.0f) / 32767.0f * Speeds[6]);
                                directionY = true;
                                break;
                            case EnumStageAxis.MaterialZ:
                                ValueY = Math.Abs((float)(JoyY - 32767.0f) / 32767.0f * Speeds[7]);
                                directionY = true;
                                break;
                            case EnumStageAxis.Presslifting:
                                ValueY = Math.Abs((float)(JoyY - 32767.0f) / 32767.0f * Speeds[11]);
                                directionY = true;
                                break;
                        }

                        if (JoyY < 31500)
                        {
                            if (directionY)
                            {
                                _positionSystem.JogPositive(Axis2, ValueY);
                            }
                            else
                            {
                                _positionSystem.JogNegative(Axis2, ValueY);
                            }
                        }
                        if (JoyY >= 31500 && JoyY <= 34034)
                        {
                            _positionSystem.StopJogPositive(Axis2);
                            Thread.Sleep(3);
                            _positionSystem.StopJogNegative(Axis2);
                        }
                        if (JoyY > 34034)
                        {
                            if (directionY)
                            {
                                _positionSystem.JogNegative(Axis2, ValueY);
                            }
                            else
                            {
                                _positionSystem.JogPositive(Axis2, ValueY);
                            }

                        }
                    }
                }
                else
                {
                    if (JoyX >= 31500 && JoyX <= 34034)
                    {
                        _positionSystem.StopJogPositive(Axis1);
                        Thread.Sleep(3);
                        _positionSystem.StopJogNegative(Axis1);
                    }


                    if (JoyY >= 31500 && JoyY <= 34034)
                    {
                        _positionSystem.StopJogPositive(Axis2);
                        Thread.Sleep(3);
                        _positionSystem.StopJogNegative(Axis2);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 手柄按键响应事件
        /// </summary>
        /// <param name="KeyNum">1-12</param>
        private void _joystickOBJ_JoystickButtonEvent(int KeyNum)
        {
            try
            {

                //模式切换
                if (KeyNum > -1 && KeyNum < 11)
                {
                    Mode = KeyNum;

                    if (HeadMode)
                    {
                        switch (Mode)
                        {
                            case (int)Axisgroup.MaterialboxHook:
                                YGMode = false;
                                Axis1 = EnumStageAxis.MaterialboxX;
                                Axis2 = EnumStageAxis.MaterialboxY;
                                //stageSystem = EnumStageSystem.ShellTable;
                                //systemAxis = EnumSystemAxis.XY;
                                break;
                            case (int)Axisgroup.MaterialboxHookZ:
                                YGMode = false;
                                Axis1 = EnumStageAxis.None;
                                Axis2 = EnumStageAxis.MaterialboxZ;
                                //stageSystem = EnumStageSystem.LidTable;
                                //systemAxis = EnumSystemAxis.XY;
                                break;
                            case (int)Axisgroup.MaterialHook:
                                YGMode = false;
                                Axis1 = EnumStageAxis.MaterialX;
                                Axis2 = EnumStageAxis.MaterialY;
                                //stageSystem = EnumStageSystem.PP;
                                //systemAxis = EnumSystemAxis.YZ;
                                break;
                            case (int)Axisgroup.MaterialHookZ:
                                YGMode = false;
                                Axis1 = EnumStageAxis.None;
                                Axis2 = EnumStageAxis.MaterialZ;
                                //stageSystem = EnumStageSystem.Head;
                                //systemAxis = EnumSystemAxis.XZ;
                                break;
                            case (int)Axisgroup.MaterialboxHookGrab:
                                YGMode = false;
                                Axis1 = EnumStageAxis.MaterialboxHook;
                                Axis2 = EnumStageAxis.None;
                                //stageSystem = EnumStageSystem.Head;
                                //systemAxis = EnumSystemAxis.XZ;
                                break;
                            case (int)Axisgroup.MaterialHookGrab:
                                YGMode = false;
                                Axis1 = EnumStageAxis.MaterialHook;
                                Axis2 = EnumStageAxis.Presslifting;
                                //stageSystem = EnumStageSystem.Head;
                                //systemAxis = EnumSystemAxis.XZ;
                                break;
                            case (int)Axisgroup.OverTrack1:
                                YGMode = false;
                                Axis1 = EnumStageAxis.OverTrack1;
                                //stageSystem = EnumStageSystem.None;
                                ////systemAxis = EnumSystemAxis.Z;
                                break;
                            case (int)Axisgroup.OverTrack2:
                                YGMode = false;
                                Axis1 = EnumStageAxis.OverTrack2;
                                //stageSystem = EnumStageSystem.None;
                                ////systemAxis = EnumSystemAxis.XY;
                                break;
                            case 0:
                                YGMode = true;
                                //Axis1 = (int)-1;
                                //Axis2 = (int)-1;
                                break;
                        }

                    }
                }


            }
            catch (Exception ex)
            {

            }
        }


    }

    public enum Axisgroup : int
    {
        /// <summary>
        /// (料盒钩爪X,料盒钩爪Y)。
        /// </summary>
        MaterialboxHook = 1,
        /// <summary>
        /// (料盒钩爪Z)。
        /// </summary>
        MaterialboxHookZ = 2,
        /// <summary>
        /// (物料钩爪X,物料钩爪Y)。
        /// </summary>
        MaterialHook = 3,
        /// <summary>
        /// (物料钩爪Z)。
        /// </summary>
        MaterialHookZ = 4,
        /// <summary>
        /// (料盒钩爪抓取)。
        /// </summary>
        MaterialboxHookGrab = 5,
        /// <summary>
        /// (物料钩爪抓取)。
        /// </summary>
        MaterialHookGrab = 6,
        /// <summary>
        /// (烘箱1)。
        /// </summary>
        OverTrack1 = 7,
        /// <summary>
        /// (烘箱2)。
        /// </summary>
        OverTrack2 = 8,


    }

}
