using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using WestDragon.Framework.UtilityHelper;

namespace IStageControllerClsLib
{

    #region 单轴控制
    /// <summary>
    /// 单轴控制的抽象基类
    /// </summary>
    public abstract class ASingleAxisController : ISingleAxisController
    {
        protected double _maxAxisSpeed;
        protected double _maxAcceleration;
        protected double _softLeftLimit;
        protected double _softRightLimit;
        protected double _axisSpeed;
        protected double _acceleration;
        protected double _deceleration;
        protected double _killDeceleration;

        /// <summary>
        /// 当前轴类型
        /// </summary>
        public abstract EnumStageAxis Axis { get; }

        /// <summary>
        /// 获取当前轴是否使能
        /// </summary>
        public bool IsEnabled
        {
            get { return GetEnabledState(); }
        }

        /// <summary>
        /// Stage控制器核心类对象
        /// </summary>
        protected IStageCore StageCore;

        /// <summary>
        /// 获取轴的最大速度
        /// </summary>
        /// <returns></returns>
        public double GetMaxAxisSpeed()
        {
            _maxAxisSpeed = GetTransaction("XVEL");
            return _maxAxisSpeed;
        }

        /// <summary>
        /// 设置轴的最大速度
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxAxisSpeed(double value)
        {
            if (!IsEnabled)
                return;
            SetTransaction("XVEL", value);
        }

        /// <summary>
        /// 获取轴的最大加速度
        /// </summary>
        /// <returns></returns>
        public double GetMaxAcceleration()
        {
            _maxAcceleration = GetTransaction("XACC");
            return _maxAcceleration;
        }

        /// <summary>
        /// 设置轴的最大加速度
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxAcceleration(double value)
        {
            if (!IsEnabled)
                return;
            SetTransaction("XACC", value);
        }

        /// <summary>
        /// JERK
        /// </summary>
        /// <param name="value"></param>
        public void SetJERK(double value)
        {
            SetTransaction("JERK", value);
        }

        /// <summary>
        /// 获取轴的软件左限位
        /// </summary>
        /// <returns></returns>
        public virtual double GetSoftLeftLimit()
        {
            return _softLeftLimit;
        }

        /// <summary>
        /// 设置轴的软件右限位
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetSoftLeftLimit(double value)
        {
            SetTransaction("SLLIMIT", value);
            _softLeftLimit = GetTransaction("SLLIMIT");
        }

        /// <summary>
        /// 获取轴的软件右限位
        /// </summary>
        /// <returns></returns>
        public virtual double GetSoftRightLimit()
        {
            return _softRightLimit;

        }

        /// <summary>
        /// 设置轴的软件右限位
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetSoftRightLimit(double value)
        {
            SetTransaction("SRLIMIT", value);
            _softRightLimit = GetTransaction("SRLIMIT");
        }

        /// <summary>
        /// 使能当前轴
        /// </summary>
        public void Enable()
        {
        }

        /// <summary>
        /// 当前轴下使能
        /// </summary>
        public void Disable()
        {
        }

        /// <summary>
        /// 当前轴执行 Home 操作 
        /// </summary>
        public abstract void Home();

        public void JogPositive(float speed)
        {
            StageCore.JogPositive(Axis,speed);
        }
        public void JogNegative(float speed)
        {

            StageCore.JogNegative(Axis, speed);
        }
        public void StopJogPositive()
        {
            StageCore.StopJogPositive(Axis);

        }
        /// <summary>
        /// 停止Jogging
        /// </summary>
        public void StopJogNegative()
        {
            StageCore.StopJogNegative(Axis);

        }
        /// <summary>
        /// 获取轴的速度
        /// </summary>
        /// <returns></returns>
        public double GetAxisSpeed()
        {
            var speed=StageCore.GetAxisSpeed(Axis);
            return speed;
        }

        /// <summary>
        /// 设置轴的速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetAxisSpeed(double speed)
        {
            if (!IsEnabled)
                return;
            StageCore.SetAxisSpeed(Axis, (float)speed);
        }


        /// <summary>
        /// 获取轴的加速度
        /// </summary>
        /// <returns></returns>
        public double GetAcceleration()
        {
            return _acceleration;
        }

        /// <summary>
        /// 设置轴的加速度
        /// </summary>
        /// <param name="accelerationSpeed"></param>
        public void SetAcceleration(double accelerationSpeed)
        {

        }

        /// <summary>
        /// 获取轴的减速度
        /// </summary>
        /// <returns></returns>
        public double GetDeceleration()
        {
            return _deceleration;
        }

        /// <summary>
        /// 设置轴的减速度
        /// </summary>
        /// <param name="deceleration"></param>
        public void SetDeceleration(double deceleration)
        {
        }

        /// <summary>
        /// 设置紧急停止减速度
        /// </summary>
        /// <param name="deceleration"></param>
        public void SetKillDeceleration(double deceleration)
        {
        }

        /// <summary>
        /// 获取紧急停止减速度
        /// </summary>
        /// <returns></returns>
        public double GetKillDeceleration()
        {
            return _killDeceleration;
        }

        /// <summary>
        /// 绝对移动
        /// </summary>
        /// <param name="targetPos"></param>
        /// <param name="millisecondsTimeout"></param>
        public void MoveAbsoluteSync(double targetPos, int millisecondsTimeout = -1)
        {
            if (!IsEnabled)
                return;
            StageCore.AbloluteMoveSync(Axis, targetPos);
        }

        /// <summary>
        /// 相对移动
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="millisecondsTimeout"></param>
        public void MoveRelativeSync(double distance, int millisecondsTimeout = -1)
        {
            if (!IsEnabled)
                return;
            StageCore.RelativeMoveSync(Axis, (float)distance);
        }

        /// <summary>
        /// 获取当前轴的位置
        /// </summary>
        /// <returns></returns>
        public virtual double GetCurrentPosition()
        {
            return StageCore.GetCurrentPosition(Axis);
        }
        public virtual double[] GetCurrentStagePosition()
        {
            return StageCore.GetCurrentStagePosition();
        }
        /// <summary>
        /// 获取使能状态
        /// </summary>
        /// <returns></returns>
        protected bool GetEnabledState()
        {
            try
            {
                return true;
            }
            catch
            {
                return true;
            }

        }



        /// <summary>
        /// 计算安全位置
        /// </summary>
        /// <param name="targetPos"></param>
        /// <returns></returns>
        protected virtual double CalculateSafePosition(double targetPos)
        {
            double targetPosValue = targetPos;
            double softLeftLimit = GetSoftLeftLimit();
            double softRightLimit = GetSoftRightLimit();
            targetPosValue = Math.Max(targetPosValue, softLeftLimit);
            targetPosValue = Math.Min(targetPosValue, softRightLimit);
            return targetPosValue;
        }

        /// <summary>
        /// 计算安全距离
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        protected virtual double CalculateSafeDistance(double currentPos, double distance)
        {
            double currentPosValue = currentPos;
            double distanceValue = distance;
            double softLeftLimit = GetSoftLeftLimit();
            double softRightLimit = GetSoftRightLimit();
            double targetPosValue = currentPosValue + distanceValue;
            targetPosValue = Math.Max(targetPosValue, softLeftLimit);
            targetPosValue = Math.Min(targetPosValue, softRightLimit);
            double safeDistanceValue = targetPosValue - currentPosValue;
            double safeDistance = safeDistanceValue;
            return safeDistance;
        }

        /// <summary>
        /// 发送命令得到返回值
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected double GetTransaction(string command)
        {

            return 0d;
        }

        /// <summary>
        /// 发送命令到ACS控制器
        /// </summary>
        /// <param name="command"></param>
        /// <param name="double_value"></param>
        protected void SetTransaction(string command, double double_value)
        {

        }

        /// <summary>
        /// 等待移动完成
        /// </summary>
        protected void WaitMoveDone()
        {
            
        }
    }


    /// <summary>
    /// X轴控制的特定实现
    /// </summary>
    internal sealed class BondXSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为X
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.BondX; }
        }

        /// <summary>
        /// 执行X轴的Home操作
        /// </summary>
        public override void Home()
        {
        }
    }
    internal sealed class BondYSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为X
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.BondY; }
        }

        /// <summary>
        /// 执行X轴的Home操作
        /// </summary>
        public override void Home()
        {
        }
    }

    /// <summary>
    /// Y轴控制的特定实现
    /// </summary>
    internal sealed class BondZSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Y
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.BondZ; }
        }

        /// <summary>
        /// 执行Y轴的Home操作
        /// </summary>
        public override void Home()
        {
        }
    }
    /// <summary>
    /// Y轴控制的特定实现
    /// </summary>
    internal sealed class WaferTableXSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Y
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.WaferTableX; }
        }

        /// <summary>
        /// 执行Y轴的Home操作
        /// </summary>
        public override void Home()
        {
        }
    }
    internal sealed class WaferTableYSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Y
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.WaferTalbeY; }
        }

        /// <summary>
        /// 执行Y轴的Home操作
        /// </summary>
        public override void Home()
        {
        }
    }
    /// <summary>
    /// Z轴控制的特定实现
    /// </summary>
    internal sealed class ESZSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Z
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.ESZ; }
        }

        /// <summary>
        /// 执行Z轴的Home操作
        /// </summary>
        public override void Home()
        {
        }
    }
    /// <summary>
    /// Z轴控制的特定实现
    /// </summary>
    internal sealed class WaferTableZSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Z
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.WaferTableZ; }
        }

        /// <summary>
        /// 执行Z轴的Home操作
        /// </summary>
        public override void Home()
        {
        }
    }
    /// <summary>
    /// ChipPPTheta轴控制的特定实现
    /// </summary>
    public sealed class ChipPPTSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 负责Theta轴单位转换的对象
        /// </summary>
        private ThetaUnitConversion _thetaUnitConversion = new ThetaUnitConversion();

        /// <summary>
        /// 当前轴为Theta
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.ChipPPT; }
        }


        /// <summary>
        /// 串口读取到的角度读数
        /// </summary>
        private double _serialReadAngle;

        /// <summary>
        /// 执行Theta轴Home时的角度
        /// </summary>
        private double _homeStartAngle = 10;

        public ChipPPTSingleAxisController()
        {
        }

        /// <summary>
        /// 执行Theta轴的Home操作
        /// </summary>
        public override void Home()
        {
        }
       

    }
    public sealed class SubmountPPTSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 负责Theta轴单位转换的对象
        /// </summary>
        private ThetaUnitConversion _thetaUnitConversion = new ThetaUnitConversion();

        /// <summary>
        /// 当前轴为Theta
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.SubmountPPT; }
        }


        /// <summary>
        /// 串口读取到的角度读数
        /// </summary>
        private double _serialReadAngle;

        /// <summary>
        /// 执行Theta轴Home时的角度
        /// </summary>
        private double _homeStartAngle = 10;

        public SubmountPPTSingleAxisController()
        {
        }

        /// <summary>
        /// 执行Theta轴的Home操作
        /// </summary>
        public override void Home()
        {
        }


    }
    /// <summary>
    /// Theta轴单位转换类
    /// </summary>
    internal sealed class ThetaUnitConversion
    {
        /// <summary>
        /// 绝对读数，初始化读一次
        /// </summary>
        private const double RING_PHYSICAL_PULSE_COUNT = 6815744;
        /// <summary>
        /// 驱动器返回读数，可能会大于这个值
        /// </summary>
        private const double RING_CVT_PULSE_COUNT = 6815744;


        public int ConvertAngleToPhysicalPulse(double angle)
        {
            while (angle < 0)
            {
                angle += 360;
            }
            int pulse = (int)(angle / 360 * RING_PHYSICAL_PULSE_COUNT);
            return pulse;
        }

        public double ConvertPhysicalPulseToAngle(int pulse)
        {
            double angle = pulse / RING_PHYSICAL_PULSE_COUNT * 360;
            return angle;
        }

        public int ConvertAngleToCVTPulse(double angle)
        {
            int pulse = (int)(angle / 360 * RING_CVT_PULSE_COUNT);
            return pulse;
        }

        public double ConvertCVTPulseToAngle(int pulse)
        {
            double angle = pulse / RING_CVT_PULSE_COUNT * 360;
            return angle;
        }
    }
    #endregion

    #region 多轴控制
    internal class MultiAxisController : IMultiAxisController
    {
        /// <summary>
        /// 控制轴列表
        /// </summary>
        public EnumStageAxis[] Axises;

        /// <summary>
        /// Theta轴单位转换类
        /// </summary>
        private ThetaUnitConversion _ThetaUnitConversion = new ThetaUnitConversion();

        /// <summary>
        /// Stage控制器核心类对象
        /// </summary>
        //protected ACSSPiiPlusNETCore StageCore = ACSSPiiPlusNETCore.GetHandler();
        /// <summary>
        /// Stage控制核心对象
        /// </summary>
        private IStageCore StageCore;
        /// <summary>
        /// 获取多轴的速度
        /// </summary>
        /// <returns></returns>
        public double[] GetMaxAxisSpeed()
        {
            return GetTransaction("XVEL");
        }

        /// <summary>
        /// 设置多轴的速度
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxAxisSpeed(double[] value)
        {
            SetTransaction("XVEL", value);
        }

        /// <summary>
        /// 获取多轴的最大加速度
        /// </summary>
        /// <returns></returns>
        public double[] GetMaxAcceleration()
        {
            return GetTransaction("XACC");
        }

        /// <summary>
        /// 设置多轴的最大加速度
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxAcceleration(double[] value)
        {
            SetTransaction("XACC", value);
        }

        /// <summary>
        /// 获取多轴的软件左限位
        /// </summary>
        /// <returns></returns>
        public double[] GetSoftLeftLimit()
        {
            return GetTransaction("SLLIMIT");
        }

        /// <summary>
        /// 设置多轴的软件左限位
        /// </summary>
        /// <param name="value"></param>
        public void SetSoftLeftLimit(double[] value)
        {
            SetTransaction("SLLIMIT", value);
        }

        /// <summary>
        /// 获取多轴的软件右限位
        /// </summary>
        /// <returns></returns>
        public double[] GetSoftRightLimit()
        {
            return GetTransaction("SRLIMIT");
        }

        /// <summary>
        /// 设置多轴的软件右限位
        /// </summary>
        /// <param name="value"></param>
        public void SetSoftRightLimit(double[] value)
        {
            SetTransaction("SRLIMIT", value);
        }

        /// <summary>
        /// 使能多轴
        /// </summary>
        public void Enable()
        {
            for (int i = 0; i < Axises.Length; i++)
            {
                StageCore.StageInfo.AxisControllerDic[Axises[i]].Enable();
            }
        }

        /// <summary>
        /// 下使能
        /// </summary>
        public void Disable()
        {
            for (int i = 0; i < Axises.Length; i++)
            {
                StageCore.StageInfo.AxisControllerDic[Axises[i]].Disable();
            }
        }

        /// <summary>
        /// 获取多轴当前位置
        /// </summary>
        /// <returns></returns>
        public double[] GetCurrentPosition()
        {
            double[] doublePositions = new double[Axises.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                doublePositions[i] = StageCore.StageInfo.AxisControllerDic[Axises[i]].GetCurrentPosition();
            }
            return doublePositions;
        }

        /// <summary>
        /// 设置多轴的速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetAxisSpeed(double[] speed)
        {
            for (int i = 0; i < Axises.Length; i++)
            {
                StageCore.StageInfo.AxisControllerDic[Axises[i]].SetAxisSpeed(speed[i]);
            }
        }

        /// <summary>
        /// 获取多轴速度
        /// </summary>
        /// <returns></returns>
        public double[] GetAxisSpeed()
        {
            double[] doubleAxisSpeed = new double[Axises.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                doubleAxisSpeed[i] = StageCore.StageInfo.AxisControllerDic[Axises[i]].GetAxisSpeed();
            }
            return doubleAxisSpeed;

        }

        /// <summary>
        /// 设置多轴的加速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetAcceleration(double[] speed)
        {
            for (int i = 0; i < Axises.Length; i++)
            {
                StageCore.StageInfo.AxisControllerDic[Axises[i]].SetAcceleration(speed[i]);
            }
        }

        /// <summary>
        /// 获取多轴的加速度
        /// </summary>
        /// <returns></returns>
        public double[] GetAcceleration()
        {
            double[] doubleAxisSpeed = new double[Axises.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                doubleAxisSpeed[i] = StageCore.StageInfo.AxisControllerDic[Axises[i]].GetAcceleration();
            }
            return doubleAxisSpeed;
        }

        /// <summary>
        /// 获取多轴的减速度
        /// </summary>
        /// <returns></returns>
        public double[] GetDeceleration()
        {
            double[] doubleAxisSpeed = new double[Axises.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                doubleAxisSpeed[i] = StageCore.StageInfo.AxisControllerDic[Axises[i]].GetDeceleration();
            }
            return doubleAxisSpeed;

        }

        /// <summary>
        /// 设置多轴的减速度
        /// </summary>
        /// <param name="deceleration"></param>
        public void SetDeceleration(double[] deceleration)
        {
            for (int i = 0; i < Axises.Length; i++)
            {
                StageCore.StageInfo.AxisControllerDic[Axises[i]].SetDeceleration(deceleration[i]);
            }
        }

        /// <summary>
        /// 设置多轴的紧急停止减速度
        /// </summary>
        /// <param name="deceleration"></param>
        public void SetKillDeceleration(double[] deceleration)
        {

            for (int i = 0; i < Axises.Length; i++)
            {
                StageCore.StageInfo.AxisControllerDic[Axises[i]].SetKillDeceleration(deceleration[i]);
            }

        }

        /// <summary>
        /// 获取多轴的紧急停止减速度
        /// </summary>
        /// <returns></returns>
        public double[] GetKillDeceleration()
        {
            double[] doubleAxisSpeed = new double[Axises.Length];
            double[] doubleSpeeds = new double[Axises.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                doubleAxisSpeed[i] = StageCore.StageInfo.AxisControllerDic[Axises[i]].GetKillDeceleration();
            }
            return doubleAxisSpeed;

        }

        /// <summary>
        /// 多轴绝对移动
        /// </summary>
        /// <param name="targetPos"></param>
        /// <param name="millisecondsTimeout"></param>
        public void MoveAbsoluteSync(double[] targetPos, int millisecondsTimeout = -1)
        {

            double[] EndPoints = new double[targetPos.Length];
            for (int i = 0; i < targetPos.Length; i++)
            {
                EndPoints[i] = targetPos[i];
            }

            StageCore.AbloluteMoveSync(Axises, EndPoints);
        }

        /// <summary>
        /// 多轴相对移动
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="millisecondsTimeout"></param>
        public void MoveRelativeSync(double[] distance, int millisecondsTimeout = -1)
        {
            double[] EndPoints = new double[distance.Length];
            for (int i = 0; i < distance.Length; i++)
            {
                EndPoints[i] = distance[i];
            }
            StageCore.RelativeMoveSync(Axises, EndPoints);
        }

        /// <summary>
        /// 多轴坐标转换--转为光栅尺单位
        /// </summary>
        /// <param name="userUnitValue"></param>
        /// <returns></returns>
        protected double[] ConvertFromUserUnitValue(double[] userUnitValue)
        {
            return userUnitValue;
        }


        /// <summary>
        /// 多轴单位转换-转为用户坐标
        /// </summary>
        /// <param name="srcValue"></param>
        /// <returns></returns>
        protected double[] ConvertToUserUnitValue(double[] srcValue)
        {
            //AUnitValue<double>[] returnValue = new AUnitValue<double>[Axises.Length];
            //for (int i = 0; i < Axises.Length; i++)
            //{
            //    if (Axises[i] == EnumStageAxis.ShellTheta)
            //        returnValue[i] = new DegreeUnitValue<double> { Value = _ThetaUnitConversion.ConvertCVTPulseToAngle((int)srcValue[i]) };
            //    else
            //        returnValue[i] = new MillimeterUnitValue<double> { Value = srcValue[i] };
            //}
            return srcValue;
        }

        /// <summary>
        /// 命令获取当前位置
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private double[] GetTransaction(string command)
        {
            double[] returnDouble = new double[Axises.Length];
            return ConvertToUserUnitValue(returnDouble);
        }

        /// <summary>
        /// 命令设置参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="double_values"></param>
        private void SetTransaction(string command, double[] double_values)
        {
        }

        /// <summary>
        /// 计算安全位置
        /// </summary>
        /// <param name="targetPos"></param>
        /// <returns></returns>
        private double[] CalculateSafePosition(double[] targetPos)
        {
            double[] targetPosValues = ConvertFromUserUnitValue(targetPos);
            double[] softLeftLimits = ConvertFromUserUnitValue(GetSoftLeftLimit());
            double[] softRightLimits = ConvertFromUserUnitValue(GetSoftRightLimit());
            for (int i = 0; i < Axises.Length; i++)
            {
                targetPosValues[i] = Math.Max(targetPosValues[i], softLeftLimits[i]);
                targetPosValues[i] = Math.Min(targetPosValues[i], softRightLimits[i]);
            }
            double[] safePos = ConvertToUserUnitValue(targetPosValues);
            return safePos;
        }

        /// <summary>
        /// 计算安全距离
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        protected virtual double[] CalculateSafeDistance(double[] currentPos, double[] distance)
        {
            double[] currentPosValue = ConvertFromUserUnitValue(currentPos);
            double[] distanceValue = ConvertFromUserUnitValue(distance);
            double[] softLeftLimit = ConvertFromUserUnitValue(GetSoftLeftLimit());
            double[] softRightLimit = ConvertFromUserUnitValue(GetSoftRightLimit());
            double[] targetPosValue = new double[currentPos.Length];
            double[] safeDistanceValue = new double[currentPos.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                targetPosValue[i] = currentPosValue[i] + distanceValue[i];
                targetPosValue[i] = Math.Max(targetPosValue[i], softLeftLimit[i]);
                targetPosValue[i] = Math.Min(targetPosValue[i], softRightLimit[i]);
                safeDistanceValue[i] = targetPosValue[i] - currentPosValue[i];
            }
            double[] safeDistance = ConvertToUserUnitValue(safeDistanceValue);
            return safeDistance;
        }

        /// <summary>
        /// 等待移动完成
        /// </summary>
        protected void WaitMoveDone()
        {
        }

    }
    #endregion

}
