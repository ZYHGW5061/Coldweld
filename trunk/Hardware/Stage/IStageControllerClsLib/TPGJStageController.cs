using BoardCardControllerClsLib;
using CommonPanelClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using WestDragon.Framework.UtilityHelper;

namespace StageControllerClsLib
{
    /// <summary>
    /// Stage控制器
    /// </summary>
    internal class TPGJStageController : IStageController
    {
        /// <summary>
        /// 硬件配置处理器
        /// </summary>
        private HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }
        /// <summary>
        /// Stage的信息
        /// </summary>
        public AStageInfo StageInfo { get; set; }

        /// <summary>
        /// 多轴控制器
        /// </summary>
        private MultiAxisController _multiAxisController;

        /// <summary>
        /// Stage控制核心对象
        /// </summary>
        private StageCore StageCore = StageControllerClsLib.StageCore.Instance;

        /// <summary>
        /// 获取指定单轴
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public ISingleAxisController this[EnumStageAxis axis]
        {
            get
            {
                switch (axis)
                {
                    case EnumStageAxis.MaterialboxX:
                        return (StageCore.StageInfo.AxisControllerDic[axis] as MaterialboxXSingleAxisController);
                    case EnumStageAxis.MaterialboxY:
                        return (StageCore.StageInfo.AxisControllerDic[axis] as MaterialboxYSingleAxisController);
                    case EnumStageAxis.MaterialboxZ:
                        return (StageCore.StageInfo.AxisControllerDic[axis] as MaterialboxZSingleAxisController);
                    case EnumStageAxis.MaterialboxT:
                        return (StageCore.StageInfo.AxisControllerDic[axis] as MaterialboxTSingleAxisController);
                    case EnumStageAxis.MaterialboxHook:
                        return (StageCore.StageInfo.AxisControllerDic[axis] as MaterialboxHookSingleAxisController);
                    case EnumStageAxis.MaterialX:
                        return (StageCore.StageInfo.AxisControllerDic[axis] as MaterialXSingleAxisController);
                    case EnumStageAxis.MaterialY:
                        return (StageCore.StageInfo.AxisControllerDic[axis] as MaterialYSingleAxisController);
                    case EnumStageAxis.MaterialZ:
                        return (StageCore.StageInfo.AxisControllerDic[axis] as MaterialZSingleAxisController);
                    case EnumStageAxis.MaterialHook:
                        return (StageCore.StageInfo.AxisControllerDic[axis] as MaterialHookSingleAxisController);
                    case EnumStageAxis.OverTrack1:
                        return (StageCore.StageInfo.AxisControllerDic[axis] as OverTrack1SingleAxisController);
                    case EnumStageAxis.OverTrack2:
                        return (StageCore.StageInfo.AxisControllerDic[axis] as OverTrack2SingleAxisController);
                    case EnumStageAxis.Presslifting:
                        return (StageCore.StageInfo.AxisControllerDic[axis] as PressliftingSingleAxisController);
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// 获取指定多轴
        /// </summary>
        /// <param name="axises"></param>
        /// <returns></returns>
        public IMultiAxisController this[params EnumStageAxis[] axises]
        {
            get
            {
                _multiAxisController.Axises = axises;
                return _multiAxisController;
            }
        }

        /// <summary>
        /// 获取是否已连接
        /// </summary>
        public bool IsConnect
        {
            get { return StageCore.IsConnect; }
        }
        private bool _homeDoneFlag = false;
        public bool IsHomedone { get { return _homeDoneFlag; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TPGJStageController()
        {
            _multiAxisController = new MultiAxisController();
            _multiAxisController.StageCore = StageCore;
        }

        /// <summary>
        /// 初始化各轴的限位和速度
        /// </summary>
        public void InitialzeAllAxisParameter()
        {

            this[EnumStageAxis.MaterialboxX].StageCore = StageCore;
            this[EnumStageAxis.MaterialboxY].StageCore = StageCore;
            this[EnumStageAxis.MaterialboxZ].StageCore = StageCore;
            this[EnumStageAxis.MaterialboxT].StageCore = StageCore;
            this[EnumStageAxis.MaterialboxHook].StageCore = StageCore;
            this[EnumStageAxis.MaterialX].StageCore = StageCore;
            this[EnumStageAxis.MaterialY].StageCore = StageCore;
            this[EnumStageAxis.MaterialZ].StageCore = StageCore;
            this[EnumStageAxis.MaterialHook].StageCore = StageCore;
            this[EnumStageAxis.OverTrack1].StageCore = StageCore;
            this[EnumStageAxis.OverTrack2].StageCore = StageCore;
            this[EnumStageAxis.Presslifting].StageCore = StageCore;


            this[EnumStageAxis.MaterialboxX].Enable();
            this[EnumStageAxis.MaterialboxY].Enable();
            this[EnumStageAxis.MaterialboxZ].Enable();
            this[EnumStageAxis.MaterialboxT].Enable();
            this[EnumStageAxis.MaterialboxHook].Enable();


            this[EnumStageAxis.MaterialX].Enable();
            this[EnumStageAxis.MaterialY].Enable();
            this[EnumStageAxis.MaterialZ].Enable();
            this[EnumStageAxis.MaterialHook].Enable();

            this[EnumStageAxis.OverTrack1].Enable();
            this[EnumStageAxis.OverTrack2].Enable();

            this[EnumStageAxis.Presslifting].Enable();

            AxisConfig axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxX);
            this[EnumStageAxis.MaterialboxX].SetAxisMotionParameters(axisConfig);
            //this[EnumStageAxis.MaterialboxX].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxY);
            this[EnumStageAxis.MaterialboxY].SetAxisMotionParameters(axisConfig);
            //this[EnumStageAxis.MaterialboxY].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxZ);
            this[EnumStageAxis.MaterialboxZ].SetAxisMotionParameters(axisConfig);
            //this[EnumStageAxis.MaterialboxZ].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxT);
            this[EnumStageAxis.MaterialboxT].SetAxisMotionParameters(axisConfig);
            //this[EnumStageAxis.MaterialboxT].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxHook);
            this[EnumStageAxis.MaterialboxHook].SetAxisMotionParameters(axisConfig);
            //this[EnumStageAxis.MaterialboxHook].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);


            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialX);
            this[EnumStageAxis.MaterialX].SetAxisMotionParameters(axisConfig);
            //this[EnumStageAxis.MaterialX].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialY);
            this[EnumStageAxis.MaterialY].SetAxisMotionParameters(axisConfig);
            //this[EnumStageAxis.MaterialY].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialZ);
            this[EnumStageAxis.MaterialZ].SetAxisMotionParameters(axisConfig);
            //this[EnumStageAxis.MaterialZ].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialHook);
            this[EnumStageAxis.MaterialHook].SetAxisMotionParameters(axisConfig);
            //this[EnumStageAxis.MaterialHook].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.OverTrack1);
            this[EnumStageAxis.OverTrack1].SetAxisMotionParameters(axisConfig);
            //this[EnumStageAxis.OverTrack1].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.OverTrack2);
            this[EnumStageAxis.OverTrack2].SetAxisMotionParameters(axisConfig);
            //this[EnumStageAxis.OverTrack2].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.Presslifting);
            this[EnumStageAxis.Presslifting].SetAxisMotionParameters(axisConfig);
            //this[EnumStageAxis.Presslifting].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

        }

        /// <summary>
        /// 初始化各IO模块
        /// </summary>
        public void InitialzeAllIO()
        {
            BoardCardManager.Instance.GetCurrentController().IO_Init();

            //BoardCardManager.Instance.GetCurrentController().IO_WriteOutPut_2(11, (short)EnumBoardcardDefineOutputIO.PressDivide, 1);
            BoardCardManager.Instance.GetCurrentController().IO_WriteOutPut_2(11, (short)EnumBoardcardDefineOutputIO.MotorBrake, 1);
            BoardCardManager.Instance.GetCurrentController().IO_WriteOutPut_2(11, (short)EnumBoardcardDefineOutputIO.MotorBrake1, 1);
            BoardCardManager.Instance.GetCurrentController().IO_WriteOutPut_2(11, (short)EnumBoardcardDefineOutputIO.CompressorStops, 1);

        }




        /// <summary>
        /// 连接到控制器
        /// </summary>
        public void Connect()
        {
            StageCore.Connect();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            StageCore.Disconnect();
        }

        /// <summary>
        /// 停止移动
        /// </summary>
        public void Stop()
        {
            BoardCardManager.Instance.GetCurrentController().IO_WriteOutPut_2(11, (short)EnumBoardcardDefineOutputIO.MotorBrake, 0);
            BoardCardManager.Instance.GetCurrentController().IO_WriteOutPut_2(11, (short)EnumBoardcardDefineOutputIO.MotorBrake1, 0);
        }

        private int ShowMessage(string title, string content, string type)
        {
            if (WarningBox.FormShow(title, content, type) == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 执行Stage的Home操作
        /// </summary>
        public void Home()
        {
            int done = ShowMessage("运动轴回零点", "是否可以返回零点", "警告");
            if(done == 0)
            {
                return;
            }

            done = ShowMessage("料盒轴回零点", "是否可以返回零点", "警告");
            if (done == 0)
            {
                //return;
            }
            else
            {
                this[EnumStageAxis.MaterialboxT].Home();
                this[EnumStageAxis.MaterialboxT].MoveAbsoluteSync(SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.Theta);
                this[EnumStageAxis.MaterialboxX].Home();
                this[EnumStageAxis.MaterialboxX].MoveAbsoluteSync(SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.X);
                this[EnumStageAxis.MaterialboxT].MoveAbsoluteSync(SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.Theta - 90);
                this[EnumStageAxis.MaterialboxY].Home();
                this[EnumStageAxis.MaterialboxY].MoveAbsoluteSync(SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.Y);
                this[EnumStageAxis.MaterialboxT].MoveAbsoluteSync(SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.Theta);
                this[EnumStageAxis.MaterialboxZ].Home();

                this[EnumStageAxis.MaterialboxHook].Home();

                this[EnumStageAxis.MaterialboxX].MoveAbsoluteSync(0);

                
            }


            done = ShowMessage("料盒轴回零点", "是否可以返回零点", "警告");
            if (done == 0)
            {
                //return;
            }
            else
            {
                this[EnumStageAxis.MaterialY].Home();
                this[EnumStageAxis.MaterialX].Home();
                this[EnumStageAxis.MaterialZ].Home();


                this[EnumStageAxis.MaterialHook].Home();

                this[EnumStageAxis.MaterialZ].MoveAbsoluteSync(SystemConfiguration.Instance.PositioningConfig.MaterialhookSafeLocation.Z);
                this[EnumStageAxis.MaterialX].MoveAbsoluteSync(SystemConfiguration.Instance.PositioningConfig.MaterialhookSafeLocation.X);
                this[EnumStageAxis.MaterialY].MoveAbsoluteSync(SystemConfiguration.Instance.PositioningConfig.MaterialhookSafeLocation.Y);
                this[EnumStageAxis.MaterialHook].MoveAbsoluteSync(SystemConfiguration.Instance.PositioningConfig.MaterialhookHookSafeLocation);

                this[EnumStageAxis.MaterialboxX].MoveAbsoluteSync(SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.X);
                this[EnumStageAxis.MaterialboxHook].MoveAbsoluteSync(SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookHookSafeLocation);
            }

                


        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="enumVacuum"></param>
        public void OnVacuum(EnumVacuum enumVacuum)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumVacuum"></param>
        public void OffVacuum(EnumVacuum enumVacuum)
        {

        }




        public bool IsHomeDone()
        {
           return StageCore.IsHomedone;
        }

        public bool CheckHomeDone()
        {
            return true;
        }
    }
    public class TPGJStageInfo : AStageInfo
    {
        /// <summary>
        /// 控制器远程IP地址
        /// </summary>
        public string RemoteAddress { get; set; }

        /// <summary>
        /// Theta是否有限制360度移动
        /// </summary>
        public bool ThetaLimit { get; set; }
    }


}
