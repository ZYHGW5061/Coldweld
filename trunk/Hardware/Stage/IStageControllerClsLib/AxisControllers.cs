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

namespace StageControllerClsLib
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
        public StageCore StageCore { get; set; }

        /// <summary>
        /// 获取轴的最大速度
        /// </summary>
        /// <returns></returns>
        public double GetMaxAxisSpeed()
        {
            return 0d;
        }

        /// <summary>
        /// 设置轴的最大速度
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxAxisSpeed(double value)
        {
        }

        /// <summary>
        /// 获取轴的最大加速度
        /// </summary>
        /// <returns></returns>
        public double GetMaxAcceleration()
        {
            return 0d;
        }

        /// <summary>
        /// 设置轴的最大加速度
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxAcceleration(double value)
        {

        }


        public void SetAxisMotionParameters(AxisConfig axisParam)
        {
            StageCore.SetAxisMotionParameters(axisParam);
        }

        /// <summary>
        /// 获取轴的软件左限位
        /// </summary>
        /// <returns></returns>
        public virtual double GetSoftLeftLimit()
        {
            //return _softLeftLimit;
            return StageCore.GetSoftLeftLimit(Axis);
        }

        /// <summary>
        /// 设置轴的软件右限位
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetSoftLeftLimit(double value)
        {

        }

        /// <summary>
        /// 获取轴的软件右限位
        /// </summary>
        /// <returns></returns>
        public virtual double GetSoftRightLimit()
        {
            return StageCore.GetSoftRightLimit(Axis);
        }

        /// <summary>
        /// 设置轴的软件右限位
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetSoftRightLimit(double value)
        {

        }

        /// <summary>
        /// 使能当前轴
        /// </summary>
        public void Enable()
        {
            StageCore.Enable(Axis);
        }
        /// <summary>
        /// 设置轴正负软限位
        /// </summary>
        public void SetSoftLeftAndRightLimit(double Pvalue, double Nvalue)
        {
            StageCore.SetSoftLeftAndRightLimit(Axis, Pvalue, Nvalue);

        }
        /// <summary>
        /// 取消轴正负软限位
        /// </summary>
        public void CloseSoftLeftAndRightLimit()
        {

            StageCore.CloseSoftLeftAndRightLimit(Axis);
        }
        /// <summary>
        /// 当前轴下使能
        /// </summary>
        public void Disable()
        {
            StageCore.Disable(Axis);
        }

        /// <summary>
        /// 当前轴执行 Home 操作 
        /// </summary>
        //public abstract void Home();
        public bool Home()
        {
            return StageCore.Home(Axis);
        }

        public void JogPositive(float speed)
        {
            StageCore.JogPositive(Axis, speed);
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

        public void Stop()
        {
            StageCore.Stop(Axis);
        }


        /// <summary>
        /// 获取轴的速度
        /// </summary>
        /// <returns></returns>
        public double GetAxisSpeed()
        {
            if(StageCore == null)
            {
                return -1;
            }
            var speed = StageCore.GetAxisSpeed(Axis);
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
            if(StageCore == null)
            {
                return -1;
            }
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
        public int GetAxisState()
        {
            return StageCore.GetAxisState(Axis);
        }

        /// <summary>
        /// 报警清除
        /// </summary>
        public void ClrAlarm()
        {
            StageCore.ClrAlarm(Axis);
        }

        /// <summary>
        /// 报警 / 限位无效
        /// action动作 ： 1 为生效，0为失效
        /// </summary>
        public void DisableAlarmLimit()
        {
            StageCore.DisableAlarmLimit(Axis);
        }
    }


    /// <summary>
    /// X轴控制的特定实现
    /// </summary>
    internal sealed class MaterialboxXSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为X
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.MaterialboxX; }
        }

        ///// <summary>
        ///// 执行X轴的Home操作
        ///// </summary>
        //public override void Home()
        //{
        //}
    }
    /// <summary>
    /// X轴控制的特定实现
    /// </summary>
    internal sealed class SimulatedSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为X
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.None; }
        }
        public override double GetCurrentPosition()
        {
            return 0d;
        }
        ///// <summary>
        ///// 执行X轴的Home操作
        ///// </summary>
        //public override void Home()
        //{
        //}
    }
    internal sealed class MaterialboxYSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为X
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.MaterialboxY; }
        }

        ///// <summary>
        ///// 执行X轴的Home操作
        ///// </summary>
        //public override void Home()
        //{
        //}
    }

    /// <summary>
    /// Y轴控制的特定实现
    /// </summary>
    internal sealed class MaterialboxZSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Y
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.MaterialboxZ; }
        }

        ///// <summary>
        ///// 执行Y轴的Home操作
        ///// </summary>
        //public override void Home()
        //{
        //}
    }
    /// <summary>
    /// Z轴控制的特定实现
    /// </summary>
    internal sealed class MaterialboxTSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Z
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.MaterialboxT; }
        }

        ///// <summary>
        ///// 执行Z轴的Home操作
        ///// </summary>
        //public override void Home()
        //{
        //}
    }
    /// <summary>
    /// Hook轴控制的特定实现
    /// </summary>
    internal sealed class MaterialboxHookSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Hook
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.MaterialboxHook; }
        }

        ///// <summary>
        ///// 执行Z轴的Home操作
        ///// </summary>
        //public override void Home()
        //{
        //}
    }

    /// <summary>
    /// Y轴控制的特定实现
    /// </summary>
    internal sealed class MaterialXSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Y
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.MaterialX; }
        }

        ///// <summary>
        ///// 执行Y轴的Home操作
        ///// </summary>
        //public override void Home()
        //{
        //}
    }
    internal sealed class MaterialYSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Y
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.MaterialY; }
        }

        ///// <summary>
        ///// 执行Y轴的Home操作
        ///// </summary>
        //public override void Home()
        //{
        //}
    }

    /// <summary>
    /// Z轴控制的特定实现
    /// </summary>
    internal sealed class MaterialZSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Z
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.MaterialZ; }
        }

        ///// <summary>
        ///// 执行Z轴的Home操作
        ///// </summary>
        //public override void Home()
        //{
        //}
    }

    /// <summary>
    /// Hook轴控制的特定实现
    /// </summary>
    internal sealed class MaterialHookSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Hook
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.MaterialHook; }
        }

        ///// <summary>
        ///// 执行Z轴的Home操作
        ///// </summary>
        //public override void Home()
        //{
        //}
    }

    /// <summary>
    /// Tarck轴控制的特定实现
    /// </summary>
    internal sealed class OverTrack1SingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Tarck
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.OverTrack1; }
        }

        ///// <summary>
        ///// 执行Z轴的Home操作
        ///// </summary>
        //public override void Home()
        //{
        //}
    }

    /// <summary>
    /// Tarck轴控制的特定实现
    /// </summary>
    internal sealed class OverTrack2SingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Tarck
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.OverTrack2; }
        }

        ///// <summary>
        ///// 执行Z轴的Home操作
        ///// </summary>
        //public override void Home()
        //{
        //}
    }

    /// <summary>
    /// Presslifting轴控制的特定实现
    /// </summary>
    internal sealed class PressliftingSingleAxisController : ASingleAxisController
    {
        /// <summary>
        /// 当前轴为Tarck
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.Presslifting; }
        }

        ///// <summary>
        ///// 执行Z轴的Home操作
        ///// </summary>
        //public override void Home()
        //{
        //}
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
        /// Stage控制器核心类对象
        /// </summary>
        //protected ACSSPiiPlusNETCore StageCore = ACSSPiiPlusNETCore.GetHandler();
        /// <summary>
        /// Stage控制核心对象
        /// </summary>
        public StageCore StageCore { get; set; }
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
