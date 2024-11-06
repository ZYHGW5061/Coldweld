using CommonPanelClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using StageControllerClsLib;
using StageManagerClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.UtilityHelper;

namespace PositioningSystemClsLib
{
    /// <summary>
    /// Stage运动控制集，带互锁判断
    /// </summary>
    public class StageMotionControl
    {
        private static volatile StageMotionControl _instance;
        private static readonly object _lockObj = new object();
        /// <summary>
        /// 获取单例对象
        /// </summary>
        /// <returns></returns>
        public static StageMotionControl Istance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_lockObj)
                    {
                        if(_instance==null)
                        {
                            _instance = new StageMotionControl();
                        }    
                    }
                }
                return _instance;
            }
        }

        
        private StageManager _stageManager
        {
            get { return StageManager.Instance; }
        }
        /// <summary>
        /// 硬件配置
        /// </summary>
        protected HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }
        /// <summary>
        /// STAGE
        /// </summary>
        private IStageController _stageEngine
        {
            get { return _stageManager.GetCurrentController(); }
        }

        /// <summary>
        /// 多轴回零
        /// </summary>
        /// <returns></returns>
        public StageMotionResult Home()
        {
            return StageMotionResult.Success;
        }

        public StageMotionResult JogPositive(EnumStageAxis axisIndex, float speed)
        {
            if (!InterlockValidate()) return StageMotionResult.Fail;
            var singleAxis = _stageEngine[axisIndex] as ISingleAxisController;
            singleAxis.JogPositive(speed);
            return StageMotionResult.Success;
        }
        public StageMotionResult JogNegative(EnumStageAxis axisIndex, float speed)
        {
            if (!InterlockValidate()) return StageMotionResult.Fail;
            var singleAxis = _stageEngine[axisIndex] as ISingleAxisController;
            singleAxis.JogNegative(speed);
            return StageMotionResult.Success;
        }
        public StageMotionResult StopJogPositive(EnumStageAxis axisIndex)
        {
            if (!InterlockValidate()) return StageMotionResult.Fail;
            var singleAxis = _stageEngine[axisIndex] as ISingleAxisController;
            singleAxis.StopJogPositive();
            return StageMotionResult.Success;
        }
        public StageMotionResult StopJogNegative(EnumStageAxis axisIndex)
        {
            if (!InterlockValidate()) return StageMotionResult.Fail;
            var singleAxis = _stageEngine[axisIndex] as ISingleAxisController;
            singleAxis.StopJogNegative();
            return StageMotionResult.Success;
        }

        public StageMotionResult Stop(EnumStageAxis axisIndex)
        {
            if (!InterlockValidate()) return StageMotionResult.Fail;
            var singleAxis = _stageEngine[axisIndex] as ISingleAxisController;
            singleAxis.Stop();
            return StageMotionResult.Success;
        }

        public void ShellMovePositive90Degree()
        {
            //var singleAxis = _stageEngine[EnumStageAxis.ShellTheta] as ShellThetaSingleAxisController;
            //if(singleAxis!=null)
            //{
            //    singleAxis.ShellMovePositive90Degree();
            //}
        }
        public void ShellMoveNegative90Degree()
        {
            //var singleAxis = _stageEngine[EnumStageAxis.ShellTheta] as ShellThetaSingleAxisController;
            //if (singleAxis != null)
            //{
            //    singleAxis.ShellMoveNegative90Degree();
            //}
        }
        public void ShellMovePositive90DegreeReset()
        {
            //var singleAxis = _stageEngine[EnumStageAxis.ShellTheta] as ShellThetaSingleAxisController;
            //if (singleAxis != null)
            //{
            //    singleAxis.ShellMovePositive90DegreeReset();
            //}
        }
        public void ShellMoveNegative90DegreeReset()
        {
            //var singleAxis = _stageEngine[EnumStageAxis.ShellTheta] as ShellThetaSingleAxisController;
            //if (singleAxis != null)
            //{
            //    singleAxis.ShellMoveNegative90DegreeReset();
            //}
        }
        /// <summary>
        /// 多轴绝对移动
        /// </summary>
        /// <param name="targetPos">目标位置</param>
        public StageMotionResult AbsoluteMovingSync(EnumStageAxis[] axesIndex,double[] targetPos)
        {
            if (!InterlockValidate()) return StageMotionResult.Fail;
            _stageEngine[axesIndex].MoveAbsoluteSync(targetPos);

            return StageMotionResult.Success;
        }
        /// <summary>
        /// 多轴相对移动
        /// </summary>
        /// <param name="distance"></param>
        public StageMotionResult RelativeMovingSync(EnumStageAxis[] axesIndex, double[] distance)
        {
            if (!InterlockValidate()) return StageMotionResult.Fail;
            _stageEngine[axesIndex].MoveRelativeSync(distance);
            return StageMotionResult.Success;
        }

        /// <summary>
        /// 单轴绝对移动
        /// </summary>
        /// <param name="targetPos">目标位置</param>
        public StageMotionResult AbsoluteMovingSync(EnumStageAxis axisIndex, double target)
        {
            if (!InterlockValidate()) return StageMotionResult.Fail;
            //if (IsAbsoluteMoveExceedSoftLimit(axisIndex, target.Value))
            //{
            //    WarningFormCL.WarningBox.FormShow("Warnning", "The Motion Will Exceed the Soft Limit!");
            //    return StageMotionResult.Fail;
            //}
            _stageEngine[axisIndex].MoveAbsoluteSync(target);
            return StageMotionResult.Success;
        }
        /// <summary>
        /// 单轴相对移动
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="millisecondsTimeout">超时时间  毫秒</param>
        public StageMotionResult RelativeMovingSync(EnumStageAxis axisIndex, double distance)
        {
            if (!InterlockValidate()) return StageMotionResult.Fail;

            _stageEngine[axisIndex].MoveRelativeSync(distance);
            return StageMotionResult.Success;
        }

        /// <summary>
        /// 获取所有轴当前位置
        /// </summary>
        /// <returns></returns>
        public double[] GetCurrentPosition()
        {
            if (_stageEngine != null)
            {
                //return _stageEngine[EnumStageAxis.BondZ].GetCurrentStagePosition();
            }
            return new double[11];
        }

        /// <summary>
        /// 获取单轴Stage读数
        /// </summary>
        /// <param name="axesIndex"></param>
        /// <returns></returns>
        public double GetCurrentPosition(EnumStageAxis axisIndex)
        {
            if(_stageEngine == null)
            {
                return -1;
            }
            return _stageEngine[axisIndex].GetCurrentPosition();
        }

        /// <summary>
        /// 设置多轴速度
        /// </summary>
        /// <param name="speed"></param>
        public bool SetAxisSpeed(EnumStageAxis[] axesIndex,double[] speed)
        {
            _stageEngine[axesIndex].SetAxisSpeed(speed);
            return true;
        }
        /// <summary>
        /// 设置单轴速度
        /// </summary>
        /// <param name="speed"></param>
        public bool SetAxisSpeed(EnumStageAxis axisIndex, double speed)
        {
            if(IsSettedSpeedExceedSoftLimit(axisIndex,(float)speed))
            {
                WarningBox.FormShow("错误", "设定的速度值超出范围!","提示");
                return false;
            }
            _stageEngine[axisIndex].SetAxisSpeed(speed);
            return true;
        }

        /// <summary>
        /// 获取单轴速度
        /// </summary>
        /// <param name="speed"></param>
        public double GetAxisSpeed(EnumStageAxis axisIndex)
        {
            if(_stageEngine == null)
            {
                return -1;
            }
            return _stageEngine[axisIndex].GetAxisSpeed();
        }

        /// <summary>
        /// 读取轴状态
        /// 1 报警
        /// 5 正限位
        /// 6 负限位 
        /// 7 平滑停止 
        /// 8 急停 
        /// 9 使能 
        /// 10 规划运动 
        /// 11 电机到位
        /// </summary>
        public int GetAxisState(EnumStageAxis axisIndex)
        {
            return _stageEngine[axisIndex].GetAxisState();
        }

        /// <summary>
        /// 报警清除
        /// </summary>
        public void ClrAlarm(EnumStageAxis axisIndex)
        {
            _stageEngine[axisIndex].ClrAlarm();
        }

        /// <summary>
        /// 报警 / 限位无效
        /// action动作 ： 1 为生效，0为失效
        /// </summary>
        public void DisableAlarmLimit(EnumStageAxis axisIndex)
        {
            _stageEngine[axisIndex].DisableAlarmLimit();
        }


        /// <summary>
        /// Stage移动前的互锁验证，内部只考虑Pin是否在底部
        /// </summary>
        /// <returns>true: 允许移动； false: 不允许移动</returns>
        private bool InterlockValidate()
        {

            return true;
        }
        private bool IsRelativeMoveExceedSoftLimit(EnumStageAxis axisIndex,double distance)
        {
            var ret = false;
            var axisConfig = _hardwareConfig.StageConfig.AxisConfigList.FirstOrDefault(i => i.Type == axisIndex);
            var curPos = _stageEngine[axisIndex].GetCurrentPosition();
            if ((curPos + distance) < axisConfig.SoftLeftLimit|| (curPos + distance) > axisConfig.SoftRightLimit)
            {
                LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error
                    , $"RelativeMoveExceedSoftLimit:Axis:{axisIndex},CurPos:{curPos},Distance:{distance},SoftLeftLimit:{axisConfig.SoftLeftLimit},SoftRightLimit:{axisConfig.SoftRightLimit}.");
                ret = true;
            }
            return ret;
        }
        private bool IsAbsoluteMoveExceedSoftLimit(EnumStageAxis axisIndex, double targetPos)
        {
            var ret = false;
            var axisConfig = _hardwareConfig.StageConfig.AxisConfigList.FirstOrDefault(i => i.Type == axisIndex);
            if (targetPos < axisConfig.SoftLeftLimit || targetPos > axisConfig.SoftRightLimit)
            {
                LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error
                    , $"AbsoluteMoveExceedSoftLimit:Axis:{axisIndex},Target:{targetPos},SoftLeftLimit:{axisConfig.SoftLeftLimit},SoftRightLimit:{axisConfig.SoftRightLimit}.");
                ret = true;
            }
            return ret;
        }
        private bool IsSettedSpeedExceedSoftLimit(EnumStageAxis axisIndex, float speed)
        {
            var ret = false;
            var axisConfig = _hardwareConfig.StageConfig.AxisConfigList.FirstOrDefault(i => i.Type == axisIndex);
            if (speed > axisConfig.MaxAxisSpeed)
            {
                LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error
                    , $"AbsoluteMoveExceedSoftLimit:Axis:{axisIndex},SetSpeedValue:{speed},MaxAxisSpeed:{axisConfig.MaxAxisSpeed}.");
                ret = true;
            }
            return ret;
        }

    }

    public enum StageMotionResult
    {
        TimeOut=0,
        Success=1,
        Fail=2
    }
}
