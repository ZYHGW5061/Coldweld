using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using WestDragon.Framework.UtilityHelper;

namespace StageControllerClsLib
{
    public interface IStageCore
    {
        AStageInfo StageInfo { get; set; }
        bool IsConnect { get; set; }
        bool IsHomedone { get; set; }
        void Connect();
        void Disconnect();

        /// <summary>
        /// Stage的信息
        /// </summary>
        /// <summary>
        /// 单轴相对移动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="distance"></param>
        void RelativeMoveSync(EnumStageAxis axis, float distance);
        /// <summary>
        /// 多轴相对移动
        /// </summary>
        /// <param name="axises"></param>
        /// <param name="distance"></param>
        void RelativeMoveSync(EnumStageAxis[] axises, double[] distance);
        /// <summary>
        /// 单轴绝对移动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="target"></param>
        void AbloluteMoveSync(EnumStageAxis axis, double target);
        /// <summary>
        /// 多轴绝对移动
        /// </summary>
        /// <param name="axises"></param>
        /// <param name="target"></param>
        void AbloluteMoveSync(EnumStageAxis[] axises, double[] target);
        /// <summary>
        /// 获取单轴位置
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        double GetCurrentPosition(EnumStageAxis axis);
        /// <summary>
        /// 获取所有轴当前位置
        /// </summary>
        /// <returns></returns>
        double[] GetCurrentStagePosition();
        /// <summary>
        /// 单轴正向点动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="jogSpeed"></param>
        void JogPositive(EnumStageAxis axis, float jogSpeed = 0);
        /// <summary>
        /// 停止单轴正向点动
        /// </summary>
        /// <param name="axis"></param>
        void StopJogPositive(EnumStageAxis axis);
        //单轴负向点动
        void JogNegative(EnumStageAxis axis, float jogSpeed = 0);
        /// <summary>
        /// 停止单轴负向点动
        /// </summary>
        /// <param name="axis"></param>
        void StopJogNegative(EnumStageAxis axis);
        /// <summary>
        /// 等待单轴相对移动结束
        /// </summary>
        /// <param name="axis"></param>
        void WaitRelativeMoveDone(EnumStageAxis axis, int timeout = 60000);

        /// <summary>
        /// 等待多轴相对移动结束
        /// </summary>
        /// <param name="axis"></param>
        void WaitRelativeMoveDone(EnumStageAxis[] axis);
        /// <summary>
        /// 等待单轴绝对移动结束
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="timeout"></param>
        void WaitAbsoluteMoveDone(EnumStageAxis axis, int timeout = 60000);

        /// <summary>
        /// 等待多轴绝对移动结束
        /// </summary>
        /// <param name="axis"></param>
        void WaitAbsoluteMoveDone(EnumStageAxis[] axis);
        /// <summary>
        /// 设置轴速
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="speed"></param>
        void SetAxisSpeed(EnumStageAxis axis, float speed);
        /// <summary>
        /// 获取轴速
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        float GetAxisSpeed(EnumStageAxis axis);
        void Enable(EnumStageAxis axis);
        void Disable(EnumStageAxis axis);
    }
}
