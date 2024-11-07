using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ConfigurationClsLib;

namespace BoardCardControllerClsLib
{
    public class GoogolBoardCardController : IBoardCardController
    {
        public bool IsConnect => ReadConnect();
        const short CORE = 1;
        short EcatSts;
        static bool[] AxisClsStat = new bool[20];

        /// <summary>
        /// 开卡链接
        /// </summary>
        public void Connect()
        {
            var err = GTN.mc.GTN_Open(5, 2);
           
            StartEcatCommunication();
            err = GTN.mc.GTN_IsEcatReady(CORE, out EcatSts);
            GTN.mc.GTN_AxisOn(CORE,2);
            while (EcatSts != 1)
            {
                Thread.Sleep(1000);
                err = GTN.mc.GTN_IsEcatReady(CORE, out EcatSts);
               
            }
        }


        public bool ReadConnect()
        {
            short Num = -1, Num2 = -1;
            var err = GTN.mc.GTN_GetEcatSlaves(CORE, out Num, out Num2);
            if(Num < 1)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private void StartEcatCommunication()
        {
            short Success;
            try
            {
                var rtn = GTN.mc.GTN_TerminateEcatComm(CORE);
               
                rtn = GTN.mc.GTN_InitEcatComm(CORE);
              
                if (rtn != 0) { return; }
                Thread.Sleep(1000);
                do
                {
                    rtn = GTN.mc.GTN_IsEcatReady(CORE, out Success);
                  
                    Thread.Sleep(10);
                } while (Success == 0);
                rtn = GTN.mc.GTN_StartEcatComm(CORE);
                rtn = GTN.mc.GTN_Reset(CORE);
            }
            catch (Exception ex)
            {
               
            }
        }


        /// <summary>
        /// 关闭总线连接
        /// </summary>
        public void Disconnect()
        {
            GTN.mc.GTN_TerminateEcatComm(CORE);
            GTN.mc.GTN_Close();
        }
        /// <summary>
        /// 获取正极限状态
        /// </summary>
        public bool Get_SoftLimit_AxisStsPositive(EnumStageAxis axis)
        {
           return  AxisControl.mc.SoftLimit_AxisStsPositive((short)axis);
        }

        /// <summary>
        /// 获取负极限状态
        /// </summary>
        public bool Get_SoftLimit_AxisStsNegative(EnumStageAxis axis)
        {

            return AxisControl.mc.SoftLimit_AxisStsNegative((short)axis);

        }
        /// <summary>
        /// 获取轴使能状态
        /// </summary>
        public bool Get_AxisSts_Enable(EnumStageAxis axis)
        {

            return AxisControl.mc.AxisSts_Enable((short)axis);

        }
        /// <summary>
        /// 获取轴正忙
        /// </summary>
        public bool Get_AxisSts_Busy(EnumStageAxis axis)
        {
            return AxisControl.mc.AxisSts_Busy((short)axis);
        }
        /// <summary>
        /// 获取轴位置状态
        /// </summary>
        public bool Get_AxisSts_PosDone(EnumStageAxis axis)
        {
            return AxisControl.mc.AxisSts_PosDone((short)axis);

        }
        /// <summary>
        /// 获取回归原点成功状态
        /// </summary>
        public bool Get_AxisSts_HomeDone(EnumStageAxis axis)
        {
            return AxisControl.mc.AxisSts_HomeDone((short)axis);

        }
        /// <summary>
        /// 获取轴错误状态
        /// </summary>
        public bool Get_AxisEncArr(EnumStageAxis axis)
        {
            return AxisControl.mc.AxisEncArr((short)axis);
        }



        /// <summary>
        /// 电机模组配置参数初始化
        /// </summary>
        public void MotioParaInit()
        {
           // MotorPara.AxisMotionPara[1].EactID = 1;
           // MotorPara.AxisMotionPara[1].DynamicsParaIn.acc = 1;
           // MotorPara.AxisMotionPara[1].DynamicsParaIn.dec = 1;
           // MotorPara.AxisMotionPara[1].DynamicsParaIn.smoothTime = 20;
           // MotorPara.AxisMotionPara[1].DynamicsParaIn.smooth = 0.5;
           // MotorPara.AxisMotionPara[1].DynamicsParaIn.velStart = 10;
           // MotorPara.AxisMotionPara[1].DynamicsParaIn.circlePulse = 10000;
           // MotorPara.AxisMotionPara[1].AXISModule.lead = 1;
           // MotorPara.AxisMotionPara[2].EactID = 2;
           // MotorPara.AxisMotionPara[2].DynamicsParaIn.acc = 1;
           // MotorPara.AxisMotionPara[2].DynamicsParaIn.dec = 1;
           // MotorPara.AxisMotionPara[2].DynamicsParaIn.smoothTime = 20;
           // MotorPara.AxisMotionPara[2].DynamicsParaIn.smooth = 0.5;
           // MotorPara.AxisMotionPara[2].DynamicsParaIn.velStart = 10;
           // MotorPara.AxisMotionPara[2].DynamicsParaIn.circlePulse = 10000;
           // MotorPara.AxisMotionPara[2].AXISModule.lead = 1;
           //MotorPara.AxisMotionPara[3].EactID = 3;
           // MotorPara.AxisMotionPara[3].DynamicsParaIn.acc = 1;
           // MotorPara.AxisMotionPara[3].DynamicsParaIn.dec = 1;
           // MotorPara.AxisMotionPara[3].DynamicsParaIn.smoothTime = 20;
           // MotorPara.AxisMotionPara[3].DynamicsParaIn.smooth = 0.5;
           // MotorPara.AxisMotionPara[3].DynamicsParaIn.velStart = 10;
           // MotorPara.AxisMotionPara[3].DynamicsParaIn.circlePulse = 10000;
           // MotorPara.AxisMotionPara[3].AXISModule.lead = 1;



        }

        public void SetAxisMotionParameters(AxisConfig axisParam)
        {
            var id = axisParam.Index;
            MotorPara.AxisMotionPara[id].EactID = (short)id;
            MotorPara.AxisMotionPara[id].DynamicsParaIn.acc = axisParam.Acceleration;
            MotorPara.AxisMotionPara[id].DynamicsParaIn.dec = axisParam.Deceleration;
            MotorPara.AxisMotionPara[id].DynamicsParaIn.smoothTime = (short)axisParam.SmoothTime;
            MotorPara.AxisMotionPara[id].DynamicsParaIn.smooth = axisParam.Smooth;
            //MotorPara.AxisMotionPara[id].DynamicsParaIn.velStart = axisParam.AxisSpeed;
            MotorPara.AxisMotionPara[id].DynamicsParaIn.circlePulse = axisParam.CirclePulse;
            MotorPara.AxisMotionPara[id].AXISModule.lead = axisParam.Lead;
            MotorPara.AxisMotionPara[id].DynamicsParaIn.velStart = (float)(axisParam.AxisSpeed *(axisParam.CirclePulse/ axisParam.Lead) / 1000);
        }

        /// <summary>
        /// 轴组使能
        /// </summary>
        public void Enable(EnumStageAxis axis)  //轴组使能
        {
            int pos;
            GTN.mc.GTN_AxisOn(CORE, (short)axis);

             GTN.mc.GTN_GetEcatEncPos(1, (short)axis, out pos);
           
            GTN.mc.GTN_SetPrfPos(1, (short)axis, pos);
            GTN.mc.GTN_SetEncPos(1, (short)axis, pos);
            GTN.mc.GTN_SynchAxisPos(1, 1 << (short)axis - 1);

        }
        /// <summary>
        /// 轴组下使能
        /// </summary>
        public void Disable(EnumStageAxis axis)  //轴组下使能后无保持力
        {
            GTN.mc.GTN_AxisOff(CORE, (short)axis);
        }


        /// <summary>
        /// 获取轴加速度
        /// </summary>
        public double GetAcceleration(EnumStageAxis axis)
        {
            int OriPosPulse = 0;
            OriPosPulse = AxisControl.mc.MC_GetPrfAcc((short)axis);
            return (double)OriPosPulse * PulseToMM(axis) * 1000;
        }

        /// <summary>
        /// 获取轴速度
        /// </summary>
        public double GetAxisSpeed(EnumStageAxis axis)
        {
            int OriPosPulse = 0;
            OriPosPulse =AxisControl.mc.MC_GetPrfVel((short)axis);
            return (double)OriPosPulse * PulseToMM(axis)*1000;
        }
        /// <summary>
        /// 获取轴当前位置
        /// </summary>
        public double GetCurrentPosition(EnumStageAxis axis)
        {
            double OriPosPulse = 0;
            OriPosPulse =  AxisControl.mc. MC_GetEncPos((short) axis);
            return (double)OriPosPulse * PulseToMM(axis);

        }
        /// <summary>
        /// 获取轴减速度
        /// </summary>
        public double GetDeceleration(EnumStageAxis axis)
        {
            int OriPosPulse = 0;
            OriPosPulse = AxisControl.mc.MC_GetPrfAcc((short)axis);
            return (double)OriPosPulse * PulseToMM(axis) * 1000;
        }
        
        public double GetKillDeceleration(EnumStageAxis axis)
        {
            throw new NotImplementedException();
        }

        public double GetMaxAcceleration(EnumStageAxis axis)
        {
            throw new NotImplementedException();
        }

        public double GetMaxAxisSpeed(EnumStageAxis axis)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取轴正向限位
        /// </summary>
        public double GetSoftLeftLimit(EnumStageAxis axis)
        {
            int OriPosPulse = 0;
            OriPosPulse = AxisControl.mc.MC_GetSoftLimitNegative((short)axis);
            return (double)OriPosPulse * PulseToMM(axis);
        }
        /// <summary>
        /// 获取轴反向限位
        /// </summary>
        public double GetSoftRightLimit(EnumStageAxis axis)
        {
            int OriPosPulse = 0;
            OriPosPulse = AxisControl.mc.MC_GetSoftLimitPositive((short)axis);
            return (double)OriPosPulse * PulseToMM(axis);
        }
        /// <summary>
        /// 设置轴当前位置为原点
        /// </summary>
        public bool Home(EnumStageAxis axis)
        {
            bool Done = false;
            ClrAlarm(axis);
            if (axis == EnumStageAxis.MaterialboxZ || axis == EnumStageAxis.MaterialZ || axis == EnumStageAxis.MaterialY || axis == EnumStageAxis.MaterialboxX || axis == EnumStageAxis.MaterialboxY || axis == EnumStageAxis.MaterialHook)
            {
                int pos = 0;
                Done = AxisControl.mc.MC_Home((short)axis, 3);
                if(!Done)
                {
                    return false;
                }
                GTN.mc.GTN_GetEcatEncPos(1, (short)axis, out pos);

                GTN.mc.GTN_SetPrfPos(1, (short)axis, pos);
                GTN.mc.GTN_SetEncPos(1, (short)axis, pos);
                GTN.mc.GTN_SynchAxisPos(1, 1 << (short)axis - 1);

            }
            else if (axis == EnumStageAxis.MaterialboxHook || axis == EnumStageAxis.MaterialboxT)
            {
                int pos = 0;
                Done = AxisControl.mc.MC_Home((short)axis,5);
                if (!Done)
                {
                    return false;
                }
                GTN.mc.GTN_GetEcatEncPos(1, (short)axis, out pos);

                GTN.mc.GTN_SetPrfPos(1, (short)axis, pos);
                GTN.mc.GTN_SetEncPos(1, (short)axis, pos);
                GTN.mc.GTN_SynchAxisPos(1, 1 << (short)axis - 1);
            }
            else if(axis == EnumStageAxis.MaterialX)
            {
                int pos = 0;
                Done = AxisControl.mc.MC_Home((short)axis, 6);
                if (!Done)
                {
                    return false;
                }
                GTN.mc.GTN_GetEcatEncPos(1, (short)axis, out pos);

                GTN.mc.GTN_SetPrfPos(1, (short)axis, pos);
                GTN.mc.GTN_SetEncPos(1, (short)axis, pos);
                GTN.mc.GTN_SynchAxisPos(1, 1 << (short)axis - 1);
            }
            else if(axis == EnumStageAxis.Presslifting)
            {
                int pos = 0;
                Done = AxisControl.mc.Step_Go_Home((short)axis, 17);
                if (!Done)
                {
                    return false;
                }
                GTN.mc.GTN_GetEcatEncPos(1, (short)axis, out pos);

                GTN.mc.GTN_SetPrfPos(1, (short)axis, pos);
                GTN.mc.GTN_SetEncPos(1, (short)axis, pos);
                GTN.mc.GTN_SynchAxisPos(1, 1 << (short)axis - 1);
            }

            return true;

        }

        /// <summary>
        /// 设置轴反向点动
        /// </summary>
        public void JogNegative(EnumStageAxis axis, float speed)
        {
            ClrAlarm(axis);
            //speed= (float)(speed*MMToPulse(axis)/1000);
            //MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.velStart = -Math.Abs(speed);
            //AxisControl.mc.MC_MoveJog(MotorPara.AxisMotionPara[(int)axis].EactID, MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.acc,
            //                          MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.dec, MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.smooth,
            //                          MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.velStart);
            speed = (float)(speed * MMToPulse(axis) / 1000);
            //MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.velStart = -Math.Abs(speed);
            AxisControl.mc.MC_MoveJog(MotorPara.AxisMotionPara[(int)axis].EactID, MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.acc,
                                      MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.dec, MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.smooth,
                                      -speed);
        }
        /// <summary>
        /// 设置轴正向点动
        /// </summary>
        public void JogPositive(EnumStageAxis axis, float speed)
        {
            //speed = (float)(speed * MMToPulse(axis) / 1000);
            //MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.velStart = speed;
            //AxisControl.mc.MC_MoveJog(MotorPara.AxisMotionPara[(int)axis].EactID, MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.acc , 
            //                          MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.dec, MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.smooth,
            //                          MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.velStart);
            ClrAlarm(axis);
            speed = (float)(speed * MMToPulse(axis) / 1000);
            //MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.velStart = speed;
            AxisControl.mc.MC_MoveJog(MotorPara.AxisMotionPara[(int)axis].EactID, MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.acc,
                                      MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.dec, MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.smooth,
                                      speed);
        }
        /// <summary>
        /// 绝对运动（同步方式）
        /// </summary>
        /// <param name="Axis">轴号</param>
        /// <param name="Trap">运动参数</param>
        /// <param name="Vel">速度</param>
        /// <param name="Pos">位置</param>
        public void MoveAbsoluteSync(EnumStageAxis axis, double targetPos, double Speed, int millisecondsTimeout = -1)
        {
            ClrAlarm(axis);
            Speed = Speed * MMToPulse(axis) / 1000;
            targetPos = targetPos * MMToPulse(axis);
            AxisControl.mc.MC_MoveAbsolute(MotorPara.AxisMotionPara[(int)axis].EactID,
                                           MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.acc,
                                          MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.dec,
                                         //Speed,
                                        MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.velStart,
                                        MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.smoothTime,
                                        (int)targetPos);
        }
        /// <summary>
        /// 相对运动（同步方式）
        /// </summary>
        /// <param name="Axis">轴号</param>
        /// <param name="Trap">运动参数</param>
        /// <param name="Vel">速度</param>
        /// <param name="Pos">位置</param>
        public void MoveRelativeSync(EnumStageAxis axis, double distance, double Speed, int millisecondsTimeout = -1)
        {
            ClrAlarm(axis);
            Speed = Speed * MMToPulse(axis) / 1000;
            distance = distance * MMToPulse(axis);
            AxisControl.mc.MC_MoveRelative(MotorPara.AxisMotionPara[(int)axis].EactID,
                                           MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.acc,
                                          MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.dec,
                                         //Speed,
                                         MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.velStart,
                                        MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.smoothTime,
                                        (int)distance);
        }
        /// <summary>
        /// 设置轴加速度
        /// </summary>
        public void SetAcceleration(EnumStageAxis axis, double acceleration)
        {
            acceleration = acceleration * MMToPulse(axis) / 1000;
            MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.acc = acceleration;
        }
        /// <summary>
        /// 设置轴速度
        /// </summary>
        public void SetAxisSpeed(EnumStageAxis axis, double speed)
        {
            speed = speed * MMToPulse(axis) / 1000;
            MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.velStart = speed;
        }
        /// <summary>
        /// 设置轴减速度
        /// </summary>
        public void SetDeceleration(EnumStageAxis axis, double deceleration)
        {
            deceleration = deceleration * MMToPulse(axis) / 1000;
            MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.dec = deceleration;
        }

        public void SetJERK(EnumStageAxis axis, double value)
        {
            throw new NotImplementedException();
        }

        public void SetKillDeceleration(EnumStageAxis axis, double deceleration)
        {
            throw new NotImplementedException();
        }

        public void SetMaxAcceleration(EnumStageAxis axis, double value)
        {
            throw new NotImplementedException();
        }

        public void SetMaxAxisSpeed(EnumStageAxis axis, double value)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 设置轴正负软限位
        /// </summary>
        public void SetSoftLeftAndRightLimit(EnumStageAxis axis, double Pvalue, double Nvalue)
        {
            Pvalue= Pvalue * MMToPulse(axis);
            Nvalue = Nvalue * MMToPulse(axis);
            AxisControl.mc.MC_SetSoftLimitNegativeAndPostive((short)axis, (Int32)Pvalue, (Int32)Nvalue);

        }
        /// <summary>
        /// 取消轴正负软限位
        /// </summary>
        public void CloseSoftLeftAndRightLimit(EnumStageAxis axis)
        {
            AxisControl.mc.CloseSoftLimit((short)axis);


        }



        public void StopJogNegative(EnumStageAxis axis)
        {
            GTN.mc.GTN_Stop(CORE, 1 << (short)axis - 1, 1 << (short)axis - 1);
        }

        public void StopJogPositive(EnumStageAxis axis)
        {
            GTN.mc.GTN_Stop(CORE, 1 << (short)axis - 1, 1 << (short)axis - 1);
        }

        /// <summary>
        /// 轴停止命令
        /// </summary>
        public void StopMotion(EnumStageAxis axis)
        {
            AxisControl.mc.Axis_Stop((short)axis);


        }
        /// <summary>
        /// 清除轴错误状态
        /// </summary>
        public void Axis_ClearStatus(EnumStageAxis axis)
        {

            AxisControl.mc.Axis_ClearSt((short)axis);
        }
        /// <summary>
        /// 关闭板卡
        /// </summary>
        public void BoardCardClose()
        {
            AxisControl.mc.ConCardClose();


        }
        /// <summary>
        /// 板卡复位
        /// </summary>
        public void BoardCardReset()
        {
            AxisControl.mc.CardReset();
        }




        //将脉冲单位转换为mm
        public double PulseToMM(EnumStageAxis axis)
        {

            return (double)(AxisControl.mc.Int2DoubleDivide(MotorPara.AxisMotionPara[(int)axis].AXISModule.lead,
                                                            MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.circlePulse));

        
        }
        //将毫米转换成脉冲单位
        public double MMToPulse(EnumStageAxis axis)
        {
            return (MotorPara.AxisMotionPara[(int)axis].DynamicsParaIn.circlePulse 
                    / MotorPara.AxisMotionPara[(int)axis].AXISModule.lead);
        
        
        }
        /// <summary>
        /// 远程IO输出设置 number（1-16） Value 1真 0假
        /// </summary>
        public void IO_WriteOutPut(ushort EcatID, short number, int  Value)
        {
            Thread.Sleep(10);
            AxisControl.IOFun.IO_WriteOutPut( EcatID,  number,  Value);


        }

        /// <summary>
        /// 远程IO输出设置 number（1-16） Value 1真 0假
        /// </summary>
        public void IO_WriteOutPut_2(ushort slaveno, short doIndex, int Value)
        {
            Thread.Sleep(10);
            if(doIndex<16)
            {
                slaveno = 0;
            }
            else if(doIndex<32)
            {
                slaveno = 1;
                doIndex = (short)(doIndex - 16);
            }
            AxisControl.IOFun.IO_WriteOutPut_2(slaveno, doIndex, Value);


        }

        /// <summary>
        /// 远程IO输入设置 number（1-16） Value 1真 0假
        /// </summary>
        public void IO_ReadInput(ushort EcatID, int ioIndex, out int Value)
        {
            AxisControl.IOFun.IO_ReadInput(EcatID, ioIndex, out  Value);

        }

        public void IO_ReadOutput(ushort EcatID, int ioIndex, out int Value)
        {
            AxisControl.IOFun.IO_ReadOutput(EcatID, ioIndex, out Value);

        }

        /// <summary>
        /// 远程数字量IO输入设置 number（1-16） Value 1真 0假
        /// </summary>
        public void IO_ReadInput_D(ushort EcatID, int offset, out int Value)
        {
            AxisControl.IOFun.IO_ReadInput_D(EcatID, (ushort)offset, out Value);

        }
        public void IO_ReadOutput_D(ushort EcatID, int offset, out int Value)
        {
            AxisControl.IOFun.IO_ReadOutput_D(EcatID, (ushort)offset, out Value);

        }


        public void IO_ReadInput_A(ushort slaveno, ushort offset, out int Value)
        {
            AxisControl.IOFun.IO_ReadInput_A(slaveno, (ushort)offset, out Value);

        }
        public void IO_ReadOutput_A(ushort slaveno, ushort offset, out int Value)
        {
            AxisControl.IOFun.IO_ReadOutput_A(slaveno, (ushort)offset, out Value);

        }


        public void IO_ReadAllInput(ushort EcatID, out List<int> Value)
        {
            AxisControl.IOFun.IO_ReadAllInput(EcatID, out Value);
        }
        public void IO_ReadAllOutput(ushort EcatID, out List<int> Value)
        {
            AxisControl.IOFun.IO_ReadAllOutput(EcatID, out Value);
        }

        public void IO_ReadAllInput_2(ushort EcatID, out List<int> Value)
        {
            List<int> Value1, Value2;
            AxisControl.IOFun.IO_ReadAllInput_2(0, out Value1);
            AxisControl.IOFun.IO_ReadAllInput_2(1, out Value2);
            Value = Value1.Concat(Value2).ToList<int>();
        }
        public void IO_ReadAllOutput_2(ushort EcatID, out List<int> Value)
        {
            List<int> Value1, Value2;
            AxisControl.IOFun.IO_ReadAllOutput_2(0, out Value1);
            AxisControl.IOFun.IO_ReadAllOutput_2(1, out Value2);
            Value = Value1.Concat(Value2).ToList<int>();
        }

        public bool IO_GetGLinkCommStatus()
        {
            return AxisControl.IOFun.MC_GetGLinkCommStatus();
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
        public int GetAxisState(EnumStageAxis axis)
        {
            int stats = 0;
            uint clk;
            
            GTN.mc.GTN_GetSts(CORE, Convert.ToInt16(axis), out stats, 1, out clk);
            return stats;
        }

        /// <summary>
        /// 报警清除
        /// </summary>
        public void ClrAlarm(EnumStageAxis axis)
        {
            GTN.mc.GTN_ClrSts(CORE, Convert.ToInt16(axis), 1);
        }

        /// <summary>
        /// 报警 / 限位无效
        /// action动作 ： 1 为生效，0为失效
        /// </summary>
        public void DisableAlarmLimit(EnumStageAxis axis)
        {
            if (axis == EnumStageAxis.None)
            {
                return;
            }

            if (AxisClsStat[(int)axis] == true)
            {
                GTN.mc.GTN_AlarmOff(CORE, Convert.ToInt16(axis));
                GTN.mc.GTN_LmtsOff(CORE, Convert.ToInt16(axis), -1);
                GTN.mc.GTN_ClrSts(CORE, Convert.ToInt16(axis), 1);
            }
            else if (AxisClsStat[(int)axis] == false)
            {
                GTN.mc.GTN_AlarmOn(CORE, Convert.ToInt16(axis));
                GTN.mc.GTN_LmtsOn(CORE, Convert.ToInt16(axis), -1);
            }

            AxisClsStat[(int)axis] = !AxisClsStat[(int)axis];
        }

        public void IO_Init()
        {
            AxisControl.IOFun.IO_Init();
        }

        public void IO_ReadInput_2(ushort slaveno, int doIndex, out int Value)
        {
            if (doIndex < 16)
            {
                slaveno = 0;
            }
            else if (doIndex < 32)
            {
                slaveno = 1;
                doIndex = (short)(doIndex - 16);
            }
            AxisControl.IOFun.IO_ReadInput_2(slaveno, doIndex, out Value);
        }

        public void IO_ReadOutput_2(ushort slaveno, int doIndex, out int Value)
        {
            if (doIndex < 16)
            {
                slaveno = 0;
            }
            else if (doIndex < 32)
            {
                slaveno = 1;
                doIndex = (short)(doIndex - 16);
            }
            AxisControl.IOFun.IO_ReadOutput_2(slaveno, doIndex, out Value);
        }

        
    }
}
