using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCardControllerClsLib
{
    public interface IBoardCardController
    {
        void Connect();
        bool IsConnect { get; }
        void Disconnect();

        void MotioParaInit();  //电机参数初始化

        void IO_Init();

        bool Get_SoftLimit_AxisStsPositive(EnumStageAxis axis);
        bool Get_SoftLimit_AxisStsNegative(EnumStageAxis axis);
        bool Get_AxisSts_Enable(EnumStageAxis axis);
        bool Get_AxisSts_Busy(EnumStageAxis axis);
        bool Get_AxisSts_PosDone(EnumStageAxis axis);
        bool Get_AxisSts_HomeDone(EnumStageAxis axis);

        bool Get_AxisEncArr(EnumStageAxis axis);

       


        /// <summary
        /// 最大轴速度
        /// </summary
        double GetMaxAxisSpeed(EnumStageAxis axis);
        /// <summary
        /// 最大轴速度
        /// </summary
        void SetMaxAxisSpeed(EnumStageAxis axis,double value);
        /// <summary
        /// 最大加速度
        /// </summary
        double GetMaxAcceleration(EnumStageAxis axis);
        /// <summary
        /// 最大加速度
        /// </summary
        void SetMaxAcceleration(EnumStageAxis axis,double value);

        /// <summary
        /// JERK
        /// </summary
        /// <param name="value"</param
        void SetJERK(EnumStageAxis axis,double value);

        /// <summary
        /// 软左限位
        /// </summary
        double GetSoftLeftLimit(EnumStageAxis axis);
        /// <summary
        /// 软左限位
        /// </summary
        void SetSoftLeftAndRightLimit(EnumStageAxis axis,double Pvalue, double Nvalue);
        void CloseSoftLeftAndRightLimit(EnumStageAxis axis);

        /// <summary
        /// 软右限位
        /// </summary
        double GetSoftRightLimit(EnumStageAxis axis);


        /// <summary
        /// 软右限位
        /// </summary
        /// <summary
        /// 启用
        /// </summary
        void Enable(EnumStageAxis axis);
        /// <summary
        /// 禁用
        /// </summary
        void Disable(EnumStageAxis axis);
        /// <summary
        /// Home
        /// </summary
        bool Home(EnumStageAxis axis);
        /// <summary
        /// 设置轴速度
        /// </summary
        /// <param name="speed"</param
        void SetAxisSpeed(EnumStageAxis axis,double speed);
        /// <summary
        /// 获取轴速度
        /// </summary
        /// <returns</returns
        double GetAxisSpeed(EnumStageAxis axis);
        /// <summary
        /// 设置轴的加速度
        /// </summary
        /// <param name="acceleration"</param
        void SetAcceleration(EnumStageAxis axis,double acceleration);
        /// <summary
        /// 获取轴的加速度
        /// </summary
        /// <returns</returns
        double GetAcceleration(EnumStageAxis axis);
        /// <summary
        /// 获取轴减速度
        /// </summary
        /// <returns</returns
        double GetDeceleration(EnumStageAxis axis);
        /// <summary
        /// 设置轴减速度
        /// </summary
        /// <returns</returns
        void SetDeceleration(EnumStageAxis axis,double deceleration);
        /// <summary
        /// 设置紧急停止减速度
        /// </summary
        /// <param name="deceleration"</param
        void SetKillDeceleration(EnumStageAxis axis,double deceleration);
        /// <summary
        /// 获取轴的紧急停止减速度
        /// </summary
        /// <returns</returns
        double GetKillDeceleration(EnumStageAxis axis);
        void SetAxisMotionParameters(AxisConfig axisParam);
        /// <summary>
        /// 绝对移动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="targetPos"></param>
        /// <param name="Speed"></param>
        /// <param name="millisecondsTimeout"></param>
        void MoveAbsoluteSync(EnumStageAxis axis,double targetPos, double Speed, int millisecondsTimeout = -1);

        /// <summary>
        /// 相对移动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="distance"></param>
        /// <param name="Speed"></param>
        /// <param name="millisecondsTimeout">超时时间</param>
        void MoveRelativeSync(EnumStageAxis axis,double distance,double Speed, int millisecondsTimeout = -1);

        /// <summary>
        /// 获得当前位置
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        double GetCurrentPosition(EnumStageAxis axis);

        void JogPositive(EnumStageAxis axis,float speed);

        void StopJogPositive(EnumStageAxis axis);
        void JogNegative(EnumStageAxis axis,float speed);

        void StopJogNegative(EnumStageAxis axis);

        //脉冲当量转换：1mm对应多少个脉冲
        double MMToPulse(EnumStageAxis axis);


        //将脉冲转化成mm
        double PulseToMM(EnumStageAxis axis);
        //停止某一轴的运动
        void StopMotion(EnumStageAxis axis);

        //清除轴错误状态
        void Axis_ClearStatus(EnumStageAxis axis);

        void BoardCardClose();
        void BoardCardReset();

        //将某一个IO拉高
        void IO_WriteOutPut(ushort EcatID, short number, int Value);
        //读取某一个IO的输入信号
        void IO_ReadInput(ushort EcatID, int ioIndex, out int Value);
        //读取某一个IO的输出信号
        void IO_ReadOutput(ushort EcatID, int ioIndex, out int Value);
        //将某一个IO拉高
        void IO_WriteOutPut_2(ushort EcatID, short number, int  Value);
        //读取某一个IO的输入信号
        void IO_ReadInput_2(ushort EcatID, int ioIndex, out int Value);
        //读取某一个IO的输出信号
        void IO_ReadOutput_2(ushort EcatID, int ioIndex, out int Value);
        //读取某一个IO的输入信号
        void IO_ReadInput_D(ushort EcatID, int offset, out int Value);
        //读取某一个IO的输出信号
        void IO_ReadOutput_D(ushort EcatID, int offset, out int Value);

        //读取某一个IO的输入信号
        void IO_ReadInput_A(ushort slaveno, ushort offset, out int Value);
        //读取某一个IO的输出信号
        void IO_ReadOutput_A(ushort slaveno, ushort offset, out int Value);

        //读所有输入信号
        void IO_ReadAllInput(ushort EcatID, out List<int> Value);
        //读所有输出信号状态
        void IO_ReadAllOutput(ushort EcatID, out List<int> Value);

        //读所有输入信号
        void IO_ReadAllInput_2(ushort EcatID, out List<int> Value);
        //读所有输出信号状态
        void IO_ReadAllOutput_2(ushort EcatID, out List<int> Value);

        //读IO模块状态
        bool IO_GetGLinkCommStatus();

        //读取轴状态
        int GetAxisState(EnumStageAxis axis);

        // 报警清除
        void ClrAlarm(EnumStageAxis axis);

        // 报警 / 限位无效
        void DisableAlarmLimit(EnumStageAxis axis);

    }
}
