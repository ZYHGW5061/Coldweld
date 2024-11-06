using GlobalDataDefineClsLib;
using IStageControllerClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WestDragon.Framework.UtilityHelper;

namespace SimulatedStageControllerClsLib
{
    /// <summary>
    /// 模拟的Stage控制器
    /// </summary>
    internal class SimulatedStageController : IStageController
    {
        private SimulatedMultiAxisController _multiAxisController;
        private bool _isConnected = false;

        /// <summary>
        /// Stage的信息
        /// </summary>
        public AStageInfo StageInfo { get; set; }

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
                    case EnumStageAxis.ShellTheta:
                        return (StageInfo.AxisControllerDic[axis] as SimulatedThetaSingleAxisController);
                    //case EnumStageAxis.X:
                    //    return (StageInfo.AxisControllerDic[axis] as SimulatedXSingleAxisController);
                    //case EnumStageAxis.Y:
                    //    return (StageInfo.AxisControllerDic[axis] as SimulatedYSingleAxisController);
                    //case EnumStageAxis.Z:
                    //    return (StageInfo.AxisControllerDic[axis] as SimulatedZSingleAxisController);
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
            get { return _isConnected; }
        }

        public bool HomeDoneFlag => throw new NotImplementedException();

        /// <summary>
        /// 构造函数
        /// </summary>
        public SimulatedStageController()
        {
            _multiAxisController = new SimulatedMultiAxisController();
        }

        /// <summary>
        /// 连接到控制器
        /// </summary>
        public void Connect()
        {
            //清空连接列表
            _isConnected = true;
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            if (IsConnect)
            {
                _isConnected = false;
            }
        }

        public void InitialzeAllAxisParameter()
        {
        }


        /// <summary>
        /// 停止移动
        /// </summary>
        public void Stop() { }

        /// <summary>
        /// 3D Mapping
        /// </summary>
        /// <param name="list_3DMappingCoordinate"></param>
        //public void ThreeDimensionsMapping(List<List<ContourMapPoint>> list_3DMappingCoordinate) { }

        /// <summary>
        /// 执行Stage的Home操作
        /// </summary>
        public void Home()
        {
        }

        /// <summary>
        /// 开真空
        /// </summary>
        /// <param name="enumVacuum"></param>
        public void OnVacuum(EnumVacuum enumVacuum)
        {
        }

        /// <summary>
        /// 关真空
        /// </summary>
        /// <param name="enumVacuum"></param>
        public void OffVacuum(EnumVacuum enumVacuum)
        {
        }

        /// <summary>
        /// 启用3d mapping
        /// </summary>
        /// <returns></returns>
        public void Open3dMapping(double x, double stepx, double y, double stepy) { }

        //public void Open3dMappingByNotInterval(List<List<ContourMapPoint>> list_3DMappingCoordinate) { }

        /// <summary>
        /// 关闭3d mapping
        /// </summary>
        /// <returns></returns>
        public void Close3dMapping() { }


        public void StopAllBuffer()
        {

        }

        public void SuspendAllBuffer()
        {

        }

        public bool IsHomeDone()
        {
            throw new NotImplementedException();
        }
    }

    #region 单轴控制
    /// <summary>
    /// 单轴控制的抽象基类
    /// </summary>
    internal abstract class SimulatedSingleAxisController : ISingleAxisController
    {
        protected AUnitValue<double> _maxAxisSpeed;
        protected AUnitValue<double> _maxAcceleration;
        protected AUnitValue<double> _softLeftLimit;
        protected AUnitValue<double> _softRightLimit;
        protected AUnitValue<double> _axisSpeed;
        protected AUnitValue<double> _acceleration;
        protected AUnitValue<double> _deceleration;
        protected AUnitValue<double> _killDeceleration;
        private AUnitValue<double> _currentPosition;
        private bool _isEnabled = false;

        /// <summary>
        /// 当前轴类型
        /// </summary>
        public abstract EnumStageAxis Axis { get; }

        /// <summary>
        /// 获取当前轴是否使能
        /// </summary>
        public bool IsEnabled
        {
            get { return true; }
        }

        /// <summary>
        /// 获取轴的最大速度
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double> GetMaxAxisSpeed()
        {
            return _maxAxisSpeed = new MillimeterUnitValue<double>();
        }

        /// <summary>
        /// 设置轴的最大速度
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxAxisSpeed(AUnitValue<double> value)
        {
            _maxAxisSpeed = value;
        }

        /// <summary>
        /// 获取轴的最大加速度
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double> GetMaxAcceleration()
        {
            return _maxAcceleration = new MillimeterUnitValue<double>();
        }

        /// <summary>
        /// 设置轴的最大加速度
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxAcceleration(AUnitValue<double> value)
        {
            _maxAcceleration = value;
        }

        /// <summary>
        /// JERK
        /// </summary>
        /// <param name="value"></param>
        public void SetJERK(AUnitValue<double> value)
        {

        }

        /// <summary>
        /// 获取轴的软件左限位
        /// </summary>
        /// <returns></returns>
        public virtual AUnitValue<double> GetSoftLeftLimit()
        {
            return _softLeftLimit = new MillimeterUnitValue<double>();
        }

        /// <summary>
        /// 设置轴的软件右限位
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetSoftLeftLimit(AUnitValue<double> value)
        {
            _softLeftLimit = value;
        }

        /// <summary>
        /// 获取轴的软件右限位
        /// </summary>
        /// <returns></returns>
        public virtual AUnitValue<double> GetSoftRightLimit()
        {
            return _softRightLimit = new MillimeterUnitValue<double>();

        }

        /// <summary>
        /// 设置轴的软件右限位
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetSoftRightLimit(AUnitValue<double> value)
        {
            _softRightLimit = value;
        }

        /// <summary>
        /// 使能当前轴
        /// </summary>
        public void Enable()
        {
            if (!_isEnabled)
            {
                _isEnabled = true;
            }
        }

        /// <summary>
        /// 当前轴下使能
        /// </summary>
        public void Disable()
        {
            if (_isEnabled)
            {
                _isEnabled = false;
            }
        }

        /// <summary>
        /// 当前轴执行 Home 操作 
        /// </summary>
        public abstract void Home();

        /// <summary>
        /// 当前轴执行 Jog 操作
        /// </summary>
        /// <param name="speed"></param>
        public void Jog(double speed)
        {

        }

        /// <summary>
        /// 停止Jogging
        /// </summary>
        public void StopJogging()
        {
        }

        /// <summary>
        /// 获取轴的速度
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double> GetAxisSpeed()
        {
            return _axisSpeed = new MillimeterUnitValue<double>();
        }

        /// <summary>
        /// 设置轴的速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetAxisSpeed(AUnitValue<double> speed)
        {
            _axisSpeed = speed;
        }


        /// <summary>
        /// 获取轴的加速度
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double> GetAcceleration()
        {
            return _acceleration = new MillimeterUnitValue<double>();
        }

        /// <summary>
        /// 设置轴的加速度
        /// </summary>
        /// <param name="accelerationSpeed"></param>
        public void SetAcceleration(AUnitValue<double> accelerationSpeed)
        {
            _acceleration = accelerationSpeed;
        }

        /// <summary>
        /// 获取轴的减速度
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double> GetDeceleration()
        {
            return _deceleration = new MillimeterUnitValue<double>();
        }

        /// <summary>
        /// 设置轴的减速度
        /// </summary>
        /// <param name="deceleration"></param>
        public void SetDeceleration(AUnitValue<double> deceleration)
        {
            _deceleration = deceleration;
        }

        /// <summary>
        /// 设置紧急停止减速度
        /// </summary>
        /// <param name="deceleration"></param>
        public void SetKillDeceleration(AUnitValue<double> deceleration)
        {
            _killDeceleration = deceleration;

        }

        /// <summary>
        /// 获取紧急停止减速度
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double> GetKillDeceleration()
        {
            return _killDeceleration = new MillimeterUnitValue<double>();
        }

        /// <summary>
        /// 绝对移动
        /// </summary>
        /// <param name="targetPos"></param>
        /// <param name="millisecondsTimeout"></param>
        public void MoveAbsoluteSync(AUnitValue<double> targetPos, int millisecondsTimeout = -1)
        {
        }

        /// <summary>
        /// 相对移动
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="millisecondsTimeout"></param>
        public void MoveRelativeSync(AUnitValue<double> distance, int millisecondsTimeout = -1)
        {

        }

        /// <summary>
        /// 获取当前轴的位置
        /// </summary>
        /// <returns></returns>
        public virtual AUnitValue<double> GetCurrentPosition()
        {
            return _currentPosition = new MillimeterUnitValue<double>();
        }

        /// <summary>
        /// 单位转换-从毫米转到硬件光栅尺单位
        /// </summary>
        /// <param name="userUnitValue"></param>
        /// <returns></returns>
        protected virtual double ConvertFromUserUnitValue(AUnitValue<double> userUnitValue)
        {
            return 0;
        }

        /// <summary>
        /// 单位转换-光栅尺单位转到毫米单位
        /// </summary>
        /// <param name="srcValue"></param>
        /// <returns></returns>
        protected virtual AUnitValue<double> ConvertToUserUnitValue(double srcValue)
        {
            return new MillimeterUnitValue<double> { Value = srcValue };
        }

        /// <summary>
        /// 计算安全位置
        /// </summary>
        /// <param name="targetPos"></param>
        /// <returns></returns>
        protected virtual AUnitValue<double> CalculateSafePosition(AUnitValue<double> targetPos)
        {
            double targetPosValue = ConvertFromUserUnitValue(targetPos);
            double softLeftLimit = ConvertFromUserUnitValue(GetSoftLeftLimit());
            double softRightLimit = ConvertFromUserUnitValue(GetSoftRightLimit());
            targetPosValue = Math.Max(targetPosValue, softLeftLimit);
            targetPosValue = Math.Min(targetPosValue, softRightLimit);
            AUnitValue<double> safePos = ConvertToUserUnitValue(targetPosValue);
            return safePos;
        }

        /// <summary>
        /// 计算安全距离
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        protected virtual AUnitValue<double> CalculateSafeDistance(AUnitValue<double> currentPos, AUnitValue<double> distance)
        {
            double currentPosValue = ConvertFromUserUnitValue(currentPos);
            double distanceValue = ConvertFromUserUnitValue(distance);
            double softLeftLimit = ConvertFromUserUnitValue(GetSoftLeftLimit());
            double softRightLimit = ConvertFromUserUnitValue(GetSoftRightLimit());
            double targetPosValue = currentPosValue + distanceValue;
            targetPosValue = Math.Max(targetPosValue, softLeftLimit);
            targetPosValue = Math.Min(targetPosValue, softRightLimit);
            double safeDistanceValue = targetPosValue - currentPosValue;
            AUnitValue<double> safeDistance = ConvertToUserUnitValue(safeDistanceValue);
            return safeDistance;
        }

        public void JogPositive(double speed)
        {
            throw new NotImplementedException();
        }

        public void StopJogPositive()
        {
            throw new NotImplementedException();
        }

        public void JogNegative(double speed)
        {
            throw new NotImplementedException();
        }

        public void StopJogNegative()
        {
            throw new NotImplementedException();
        }

        public void JogPositive(float speed)
        {
            throw new NotImplementedException();
        }

        public void JogNegative(float speed)
        {
            throw new NotImplementedException();
        }

        public float[] GetCurrentStagePosition()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 实现具有PEG功能的轴控制器接口
    /// </summary>
    internal abstract class APEGSingleAxisController : SimulatedSingleAxisController, IAxisPEGController
    {
        /// <summary>
        /// 触发的起始位置
        /// </summary>
        private PointF _positionStartMoving;

        /// <summary>
        /// 触发的终止位置
        /// </summary>
        private PointF _positionEndMoving;

        /// <summary>
        /// peg触发的位置点集合
        /// </summary>
        private AUnitValueArray<double> _pegFirePositionArray;

        /// <summary>
        /// 获取或设置触发的起始位置
        /// </summary>
        public PointF PositionStartMoving
        {
            get { return _positionStartMoving; }
            set { _positionStartMoving = value; }
        }

        /// <summary>
        /// 获取或设置触发的终止位置
        /// </summary>
        public PointF PositionEndMoving
        {
            get { return _positionEndMoving; }
            set { _positionEndMoving = value; }
        }

        /// <summary>
        /// 获取或设置peg触发的位置点集合
        /// </summary>
        public AUnitValueArray<double> PEGFirePositionArray
        {
            get { return _pegFirePositionArray; }
            set
            {
                _pegFirePositionArray = value;
            }
        }

        #region ???是否还需要保留
        /// <summary>
        /// 
        /// </summary>
        private AUnitValueArray<double> _PEGFireDistanceArray;
        public AUnitValueArray<double> PEGFireDistanceArray
        {
            get { return _PEGFireDistanceArray; }
            set
            {
                _PEGFireDistanceArray = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private AUnitValueArray<double> _PEGFiredRecordArray;
        public AUnitValueArray<double> PEGFiredRecordArray
        {
            get { return _PEGFiredRecordArray; }
        }
        #endregion

        /// <summary>
        /// 开始触发模式
        /// </summary>
        /// <param name="width"></param>
        public void StartPEGTriggerMode(double width = 0.01, bool useBuffer = true)
        {

        }

        #region ???是否还需要保留
        /// <summary>
        /// 将相对距离转为绝对坐标？？？
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private double AbsToRelative(float distance)
        {
            //获取当前位置
            double position = ConvertFromUserUnitValue(GetCurrentPosition());
            double distanceDouble = (double)distance;
            if (position > 0)
            {
                if (position > distanceDouble)
                {
                    return 0 - (position - distanceDouble);
                }
                else
                {
                    return (distanceDouble - position);

                }
            }
            else if (position < 0)
            {
                if (position > distanceDouble)
                {
                    return 0 - (position - distanceDouble);
                }
                else
                {
                    return 0 - (position - distanceDouble);

                }
            }
            else
            {
                return distanceDouble;

            }
        }
        #endregion

        /// <summary>
        /// 停止触发模式
        /// </summary>
        public void StopPEGTriggerMode()
        {

        }

        /// <summary>
        /// 将触发位置信息写入ACS控制器
        /// </summary>
        private void SavePEGFireArrayToStage()
        {

        }

        /// <summary>
        /// 批量坐标转换-转为光栅尺单位
        /// </summary>
        /// <param name="userUnitValue"></param>
        /// <returns></returns>
        private double[] ConvertFromUserUnitValue(AUnitValueArray<double> userUnitValue)
        {
            return userUnitValue.GetValue(EnumUnit.Millimeter);
        }


        public void StartPEGTriggerModeWithBuffer(double width = 0.01)
        {

        }


        public void CreateScanPointsPEGBuffer(List<PEGInfo> pegInfo, double width = 0.01)
        {

        }


        public void SuspendPEGBuffer()
        {

        }

        public void StopPEGBuffer()
        {

        }

        public void StartPEGBuffer()
        {

        }


        public void CreateDiscretePointsPEGBuffer(List<PointF> points)
        {

        }

        //public void CreateLineScanHighLevelBuffer(List<EnumLineKind> lineKinds, List<HighLevelInfo> highLevelInfos)
        //{

        //}
        //public void CreateLineScanHighLevelBuffer(HighLevelInfo highLevelInfo)
        //{

        //}
    }

    /// <summary>
    /// X轴控制的特定实现
    /// </summary>
    internal sealed class SimulatedXSingleAxisController : APEGSingleAxisController
    {
        /// <summary>
        /// 当前轴为X
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.LidX; }
        }

        /// <summary>
        /// 执行X轴的Home操作
        /// </summary>
        public override void Home()
        {
            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// Y轴控制的特定实现
    /// </summary>
    internal sealed class SimulatedYSingleAxisController : SimulatedSingleAxisController
    {
        /// <summary>
        /// 当前轴为Y
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.LidY; }
        }

        /// <summary>
        /// 执行Y轴的Home操作
        /// </summary>
        public override void Home()
        {
            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// Z轴控制的特定实现
    /// </summary>
    internal sealed class SimulatedZSingleAxisController : SimulatedSingleAxisController
    {
        /// <summary>
        /// 当前轴为Z
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.HeadZ; }
        }

        /// <summary>
        /// 执行Z轴的Home操作
        /// </summary>
        public override void Home()
        {
            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// Theta轴控制的特定实现
    /// </summary>
    internal sealed class SimulatedThetaSingleAxisController : SimulatedSingleAxisController
    {
        /// <summary>
        /// 当前轴为Theta
        /// </summary>
        public override EnumStageAxis Axis
        {
            get { return EnumStageAxis.ShellTheta; }
        }

        /// <summary>
        /// Theta轴构造函数
        /// </summary>
        /// <param name="port"></param>
        public SimulatedThetaSingleAxisController(int port)
        {

        }

        /// <summary>
        /// 连接串口
        /// </summary>
        public void Connect()
        {
        }

        /// <summary>
        /// 断开串口
        /// </summary>
        public void Disconnect()
        {
        }

        /// <summary>
        /// 执行Theta轴的Home操作
        /// </summary>
        public override void Home()
        {
            Thread.Sleep(1000);
        }

        /// <summary>
        /// 获取当前位置（区别X\Y\Z的读取方法）
        /// </summary>
        /// <returns></returns>
        public override AUnitValue<double> GetCurrentPosition()
        {
            return new DegreeUnitValue<double>();
        }

        /// <summary>
        /// 计算安全位置
        /// </summary>
        /// <param name="targetPos"></param>
        /// <returns></returns>
        protected override AUnitValue<double> CalculateSafePosition(AUnitValue<double> targetPos)
        {
            double targetPosValue = targetPos.GetValue(EnumUnit.Degree); ;
            double softLeftLimit = GetSoftLeftLimit().GetValue(EnumUnit.Degree);
            double softRightLimit = GetSoftRightLimit().GetValue(EnumUnit.Degree);
            targetPosValue = Math.Max(targetPosValue, softLeftLimit);
            targetPosValue = Math.Min(targetPosValue, softRightLimit);
            AUnitValue<double> safePos = new DegreeUnitValue<double>() { Value = targetPosValue };
            return safePos;
        }

        /// <summary>
        /// 计算安全距离
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        protected override AUnitValue<double> CalculateSafeDistance(AUnitValue<double> currentPos, AUnitValue<double> distance)
        {
            double currentPosValue = currentPos.GetValue(EnumUnit.Degree);
            double distanceValue = distance.GetValue(EnumUnit.Degree);
            double softLeftLimit = GetSoftLeftLimit().GetValue(EnumUnit.Degree);
            double softRightLimit = GetSoftRightLimit().GetValue(EnumUnit.Degree);
            double targetPosValue = currentPosValue + distanceValue;
            targetPosValue = Math.Max(targetPosValue, softLeftLimit);
            targetPosValue = Math.Min(targetPosValue, softRightLimit);
            double safeDistanceValue = targetPosValue - currentPosValue;
            AUnitValue<double> safeDistance = new DegreeUnitValue<double>() { Value = safeDistanceValue };
            return safeDistance;
        }

        /// <summary>
        /// 转为光栅尺坐标
        /// </summary>
        /// <param name="userUnitValue"></param>
        /// <returns></returns>
        protected override double ConvertFromUserUnitValue(AUnitValue<double> userUnitValue)
        {
            return 0;
        }

        /// <summary>
        /// 转为角度坐标
        /// </summary>
        /// <param name="srcValue"></param>
        /// <returns></returns>
        protected override AUnitValue<double> ConvertToUserUnitValue(double srcValue)
        {
            return new DegreeUnitValue<double> { Value = 0 };
        }
    }

    #endregion

    #region 多轴控制
    internal class SimulatedMultiAxisController : IMultiAxisController
    {
        /// <summary>
        /// 控制轴列表
        /// </summary>
        public EnumStageAxis[] Axises;

        /// <summary>
        /// 获取多轴的速度
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double>[] GetMaxAxisSpeed()
        {
            return GetTransaction("XVEL");
        }

        /// <summary>
        /// 设置多轴的速度
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxAxisSpeed(AUnitValue<double>[] value)
        {
            SetTransaction("XVEL", value);
        }

        /// <summary>
        /// 获取多轴的最大加速度
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double>[] GetMaxAcceleration()
        {
            return GetTransaction("XACC");
        }

        /// <summary>
        /// 设置多轴的最大加速度
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxAcceleration(AUnitValue<double>[] value)
        {
            SetTransaction("XACC", value);
        }

        /// <summary>
        /// 获取多轴的软件左限位
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double>[] GetSoftLeftLimit()
        {
            return GetTransaction("SLLIMIT");
        }

        /// <summary>
        /// 设置多轴的软件左限位
        /// </summary>
        /// <param name="value"></param>
        public void SetSoftLeftLimit(AUnitValue<double>[] value)
        {
            SetTransaction("SLLIMIT", value);
        }

        /// <summary>
        /// 获取多轴的软件右限位
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double>[] GetSoftRightLimit()
        {
            return GetTransaction("SRLIMIT");
        }

        /// <summary>
        /// 设置多轴的软件右限位
        /// </summary>
        /// <param name="value"></param>
        public void SetSoftRightLimit(AUnitValue<double>[] value)
        {
            SetTransaction("SRLIMIT", value);
        }

        /// <summary>
        /// 使能多轴
        /// </summary>
        public void Enable()
        {
        }

        /// <summary>
        /// 下使能
        /// </summary>
        public void Disable()
        {
        }

        /// <summary>
        /// 获取多轴当前位置
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double>[] GetCurrentPosition()
        {
            AUnitValue<double>[] doublePositions = new AUnitValue<double>[Axises.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                if (Axises[i] == EnumStageAxis.ShellTheta)
                {
                    doublePositions[i] = new DegreeUnitValue<double>();
                }
                else
                {
                    doublePositions[i] = new MillimeterUnitValue<double>();
                }

            }
            return doublePositions;
        }

        /// <summary>
        /// 设置多轴的速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetAxisSpeed(AUnitValue<double>[] speed)
        {
        }

        /// <summary>
        /// 获取多轴速度
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double>[] GetAxisSpeed()
        {
            AUnitValue<double>[] doubleAxisSpeed = new AUnitValue<double>[Axises.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                if (Axises[i] == EnumStageAxis.ShellTheta)
                {
                    doubleAxisSpeed[i] = new DegreeUnitValue<double>();
                }
                else
                {
                    doubleAxisSpeed[i] = new MillimeterUnitValue<double>();
                }
            }
            return doubleAxisSpeed;

        }

        /// <summary>
        /// 设置多轴的加速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetAcceleration(AUnitValue<double>[] speed)
        {
        }

        /// <summary>
        /// 获取多轴的加速度
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double>[] GetAcceleration()
        {
            AUnitValue<double>[] doubleAxisSpeed = new AUnitValue<double>[Axises.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                if (Axises[i] == EnumStageAxis.ShellTheta)
                {
                    doubleAxisSpeed[i] = new DegreeUnitValue<double>();
                }
                else
                {
                    doubleAxisSpeed[i] = new MillimeterUnitValue<double>();
                }
            }
            return doubleAxisSpeed;
        }

        /// <summary>
        /// 获取多轴的减速度
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double>[] GetDeceleration()
        {
            AUnitValue<double>[] doubleAxisSpeed = new AUnitValue<double>[Axises.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                if (Axises[i] == EnumStageAxis.ShellTheta)
                {
                    doubleAxisSpeed[i] = new DegreeUnitValue<double>();
                }
                else
                {
                    doubleAxisSpeed[i] = new MillimeterUnitValue<double>();
                }
            }
            return doubleAxisSpeed;

        }

        /// <summary>
        /// 设置多轴的减速度
        /// </summary>
        /// <param name="deceleration"></param>
        public void SetDeceleration(AUnitValue<double>[] deceleration)
        {

        }

        /// <summary>
        /// 设置多轴的紧急停止减速度
        /// </summary>
        /// <param name="deceleration"></param>
        public void SetKillDeceleration(AUnitValue<double>[] deceleration)
        {
        }

        /// <summary>
        /// 获取多轴的紧急停止减速度
        /// </summary>
        /// <returns></returns>
        public AUnitValue<double>[] GetKillDeceleration()
        {
            AUnitValue<double>[] doubleAxisSpeed = new AUnitValue<double>[Axises.Length];
            double[] doubleSpeeds = new double[Axises.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                if (Axises[i] == EnumStageAxis.ShellTheta)
                {
                    doubleAxisSpeed[i] = new DegreeUnitValue<double>();
                }
                else
                {
                    doubleAxisSpeed[i] = new MillimeterUnitValue<double>();
                }
            }
            return doubleAxisSpeed;

        }

        /// <summary>
        /// 多轴绝对移动
        /// </summary>
        /// <param name="targetPos"></param>
        /// <param name="millisecondsTimeout"></param>
        public void MoveAbsoluteSync(AUnitValue<double>[] targetPos, int millisecondsTimeout = -1)
        {

        }

        /// <summary>
        /// 多轴相对移动
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="millisecondsTimeout"></param>
        public void MoveRelativeSync(AUnitValue<double>[] distance, int millisecondsTimeout = -1)
        {
        }

        /// <summary>
        /// 多轴坐标转换--转为光栅尺单位
        /// </summary>
        /// <param name="userUnitValue"></param>
        /// <returns></returns>
        protected double[] ConvertFromUserUnitValue(AUnitValue<double>[] userUnitValue)
        {
            double[] returnDouble = new double[Axises.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                if (Axises[i] == EnumStageAxis.ShellTheta)
                {
                    returnDouble[i] = userUnitValue[i].GetValue(EnumUnit.Degree);
                }
                else
                {
                    returnDouble[i] = userUnitValue[i].GetValue(EnumUnit.Millimeter);
                }
            }
            return returnDouble;
        }


        /// <summary>
        /// 多轴单位转换-转为用户坐标
        /// </summary>
        /// <param name="srcValue"></param>
        /// <returns></returns>
        protected AUnitValue<double>[] ConvertToUserUnitValue(double[] srcValue)
        {
            AUnitValue<double>[] returnValue = new AUnitValue<double>[Axises.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                if (Axises[i] == EnumStageAxis.ShellTheta)
                    returnValue[i] = new DegreeUnitValue<double> { Value = srcValue[i] };
                else
                    returnValue[i] = new MillimeterUnitValue<double> { Value = srcValue[i] };
            }
            return returnValue;
        }

        /// <summary>
        /// 命令获取当前位置
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private AUnitValue<double>[] GetTransaction(string command)
        {
            double[] returnDouble = new double[Axises.Length];
            for (int i = 0; i < Axises.Length; i++)
            {
                string axisstr = ((int)Axises[i]).ToString();
                double doubleValue = 0;
                returnDouble[i] = doubleValue;
            }
            return ConvertToUserUnitValue(returnDouble);
        }

        /// <summary>
        /// 命令设置参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="double_values"></param>
        private void SetTransaction(string command, AUnitValue<double>[] double_values)
        {
        }

        /// <summary>
        /// 计算安全位置
        /// </summary>
        /// <param name="targetPos"></param>
        /// <returns></returns>
        private AUnitValue<double>[] CalculateSafePosition(AUnitValue<double>[] targetPos)
        {
            double[] targetPosValues = ConvertFromUserUnitValue(targetPos);
            double[] softLeftLimits = ConvertFromUserUnitValue(GetSoftLeftLimit());
            double[] softRightLimits = ConvertFromUserUnitValue(GetSoftRightLimit());
            for (int i = 0; i < Axises.Length; i++)
            {
                targetPosValues[i] = Math.Max(targetPosValues[i], softLeftLimits[i]);
                targetPosValues[i] = Math.Min(targetPosValues[i], softRightLimits[i]);
            }
            AUnitValue<double>[] safePos = ConvertToUserUnitValue(targetPosValues);
            return safePos;
        }

        /// <summary>
        /// 计算安全距离
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        protected virtual AUnitValue<double>[] CalculateSafeDistance(AUnitValue<double>[] currentPos, AUnitValue<double>[] distance)
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
            AUnitValue<double>[] safeDistance = ConvertToUserUnitValue(safeDistanceValue);
            return safeDistance;
        }
    }
    #endregion

    internal sealed class SimulatedStageInfo : AStageInfo
    {

    }
}
