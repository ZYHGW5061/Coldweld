using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCardControllerClsLib
{

    //与轴运动相关的参数配置
    public   class MotorPara
    {


        //轴状态参数
        public struct MotionStatus
        {
            public bool Done;//运动完成；
            public bool Homing;//正在回原点
            public bool WAIT;//等待命令;
            public bool JOG;//点动中;
            public bool ErrSt;//错误状态;

        }

        //轴运动参数输入
        public struct DynamicsParaInput
        {
            public double acc;    //加速度
            public double dec;    //减速度
            public double velStart;  //启动速度
            public double Postion;    //位置
            public short smoothTime;   //平滑时间ms
            public double smooth;   //平滑系数
            public int circlePulse;     //轴旋转一圈所需的脉冲数
        }

        //模组参数
        public struct AXISPARAMETER
        {
            public double lead;  //丝杆导程
            public int ratio;    //机械速比
            public int Acc;   //加速度
            public int Dec;   //减速度
            public int MaxSpeed;   //最大速度
            public int WorkSpeed;  //工作速度
        }

        public struct MotionParaGroup
        {
            public short EactID;
            public MotionStatus MotionSt;  //轴状态参数
            public DynamicsParaInput DynamicsParaIn;
            public AXISPARAMETER AXISModule;
        }
        public static MotionParaGroup[] AxisMotionPara = new MotionParaGroup[13];
      


    }
}
