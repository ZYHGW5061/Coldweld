using GlobalToolClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WestDragon.Framework.BaseLoggerClsLib;
using static GTN.glink;

namespace AxisControl
{

    public class mc
    {
        const short AxisCrd = 1;
        private short rtn;
        private static uint clk;
        /// <summary>
        /// 按位获取轴状态
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static bool AxisSts(short axis, short bit)
        {
            int pSts;
            GTN.mc.GTN_GetSts(AxisCrd, axis, out pSts, 1, out clk);
            if ((pSts & (1 << bit)) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取轴的正极限软限位状态
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static bool SoftLimit_AxisStsPositive(short axis)
        {
            int pSts;
            short bit = 5;
            GTN.mc.GTN_GetSts(AxisCrd, axis, out pSts, 1, out clk);
            if ((pSts & (1 << bit)) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取轴的负极限软限位状态
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static bool SoftLimit_AxisStsNegative(short axis)
        {
            int pSts;
            short bit = 6;
            GTN.mc.GTN_GetSts(AxisCrd, axis, out pSts, 1, out clk);
            if ((pSts & (1 << bit)) != 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 获取轴的使能标志
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static bool AxisSts_Enable(short axis)
        {
            int pSts;
            short bit = 9;
            GTN.mc.GTN_GetSts(AxisCrd, axis, out pSts, 1, out clk);
            if ((pSts & (1 << bit)) != 0)
            {
                return true;
            }
            return false;
        }



        /// <summary>
        /// 获取轴正忙标志
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static bool AxisSts_Busy(short axis)
        {
            int pSts;
            short bit = 10;
            GTN.mc.GTN_GetSts(AxisCrd, axis, out pSts, 1, out clk);
            if ((pSts & (1 << bit)) != 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 获取轴到位标志
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static bool AxisSts_PosDone(short axis)
        {
            int pSts;
            short bit = 10;//11;
            GTN.mc.GTN_GetSts(AxisCrd, axis, out pSts, 1, out clk);
            if ((pSts & (1 << bit)) == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取回原点到位标志
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static bool AxisSts_HomeDone(short axis)
        {
            ushort sHomeSts;
            GTN.mc.GTN_GetEcatHomingStatus(1, axis, out sHomeSts);
            if (3 == sHomeSts)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 规划到位信号
        /// </summary>
        /// <param name="axis">轴号</param>
        public static bool AxisArr(short axis)
        {
            bool run;
            do
            {
                run = AxisSts(axis, 10);
            } while (run);
            return !run;
        }

        /// <summary>
        /// 轴错误信号
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static bool AxisEncArr(short axis)
        {
            bool run;
            do
            {
                run = AxisSts(axis, 11);

            } while (!run);
            return run;
        }
        /// <summary>
        /// 绝对运动
        /// </summary>
        /// <param name="Axis">轴号</param>
        /// <param name="Trap">运动参数</param>
        /// <param name="Vel">速度</param>
        /// <param name="Pos">位置</param>
        public static short MC_MoveAbsolute(short Axis, double acc, double dec, double Vel, short sm, int Pos)
        {
            short rtn = 0;
            GTN.mc.TTrapPrm trap;
            rtn = GTN.mc.GTN_PrfTrap(AxisCrd, Axis);
            //FileData.Log.Commandhder("GTN_PrfTrap", rtn);
            rtn = GTN.mc.GTN_GetTrapPrm(AxisCrd, Axis, out trap);
            //FileData.Log.Commandhder("GTN_GetTrapPrm", rtn);
            trap.acc = acc;
            trap.dec = dec;
            trap.smoothTime = sm;
            rtn = GTN.mc.GTN_SetTrapPrm(AxisCrd, Axis, ref trap);
            //FileData.Log.Commandhder("GTN_SetTrapPrm", rtn);
            rtn = GTN.mc.GTN_SetVel(AxisCrd, Axis, Vel);
            //FileData.Log.Commandhder("GTN_SetVel", rtn);
            rtn = GTN.mc.GTN_SetPos(AxisCrd, Axis, Pos);
            //FileData.Log.Commandhder("GTN_SetPos", rtn);
            rtn = GTN.mc.GTN_Update(AxisCrd, 1 << Axis - 1);
            //FileData.Log.Commandhder("GTN_Update", rtn);

            return rtn;
        }
        /// <summary>
        /// 相对运动
        /// </summary>
        /// <param name="Axis">轴号</param>
        /// <param name="Trap">运动参数</param>
        /// <param name="Vel">速度</param>
        /// <param name="Pos">相对位置</param>
        public static short MC_MoveRelative(short Axis, double Acc, double Dec, double Vel, short Sm, int Pos)
        {
            short rtn = 0;
            rtn = GTN.mc.GTN_PrfTrap(AxisCrd, Axis);
            //FileData.Log.Commandhder("GTN_PrfTrap", rtn);
            GTN.mc.TTrapPrm trap;
            double pPos;
            uint clk;
            rtn = GTN.mc.GTN_GetTrapPrm(AxisCrd, Axis, out trap);
            // FileData.Log.Commandhder("GTN_GetTrapPrm", rtn);
            trap.acc = Acc;
            trap.dec = Dec;
            trap.smoothTime = Sm;
            trap.velStart = 0;
            rtn = GTN.mc.GTN_SetTrapPrm(AxisCrd, Axis, ref trap);
            //FileData.Log.Commandhder("GTN_SetTrapPrm", rtn);
            rtn = GTN.mc.GTN_SetVel(AxisCrd, Axis, Vel);
            //FileData.Log.Commandhder("GTN_SetVel", rtn);
            rtn = GTN.mc.GTN_GetPrfPos(AxisCrd, Axis, out pPos, 1, out clk);
            //FileData.Log.Commandhder("GTN_GetPrfPos", rtn);
            rtn = GTN.mc.GTN_SetPos(AxisCrd, Axis, (int)(pPos + Pos));
            //FileData.Log.Commandhder("GTN_SetPos", rtn);
            rtn = GTN.mc.GTN_Update(AxisCrd, 1 << Axis - 1);
            //FileData.Log.Commandhder("GTN_Update", rtn);
            return rtn;
        }
        /// <summary>
        /// Jog运动
        /// </summary>
        /// <param name="Axis">轴号</param>
        /// <param name="Jog">运动参数</param>
        /// <param name="Vel">速度</param>
        public static short MC_MoveJog(short Axis, double Acc, double Dec, double Sm, double Vel)
        {
            GTN.mc.TJogPrm jog;
            short rtn = 0;
            rtn = GTN.mc.GTN_PrfJog(AxisCrd, Axis);
            //FileData.Log.Commandhder("GTN_PrfJog", rtn);
            rtn = GTN.mc.GTN_GetJogPrm(AxisCrd, Axis, out jog);
            //FileData.Log.Commandhder("GTN_GetJogPrm", rtn);
            jog.acc = Acc;
            jog.dec = Dec;
            jog.smooth = Sm;
            rtn = GTN.mc.GTN_SetJogPrm(AxisCrd, Axis, ref jog);
            //FileData.Log.Commandhder("GTN_SetJogPrm", rtn);
            rtn = GTN.mc.GTN_SetVel(AxisCrd, Axis, Vel);
            //FileData.Log.Commandhder("GTN_SetVel", rtn);
            rtn = GTN.mc.GTN_Update(AxisCrd, 1 << Axis - 1);
            //FileData.Log.Commandhder("GTN_Update", rtn);
            return rtn;
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="Axis">轴号</param>
        public static void MC_Stop(short Axis)
        {
            GTN.mc.GTN_Stop(AxisCrd, 1 << Axis - 1, 1 << Axis - 1);
        }

        /// <summary>
        /// 回零方法
        /// </summary>
        /// <param name="Axis"></param>
        /// <param name="mode">4 正向回零 6 反向回零</param>
        /// <returns></returns>
        public static bool MC_Home(short Axis, short mode = 3)
        {
            short rtn;
            ushort HomeSts;


            GTN.mc.GTN_ClrSts(AxisCrd, Axis, 1);//清除当前错误
            rtn = GTN.mc.GTN_SetHomingMode(AxisCrd, Axis, 6);  //切换到回零模式
            rtn = GTN.mc.GTN_SetEcatHomingPrm(AxisCrd, Axis, mode, 10000, 500, 20000, 0, 0);  //速度加速度参数填入

            rtn = GTN.mc.GTN_StartEcatHoming(AxisCrd, Axis);//启动回零


            do
            {
                GTN.mc.GTN_GetEcatHomingStatus(AxisCrd, Axis, out HomeSts);
                if ((HomeSts & 1 << 2) != 0 || AxisSts(Axis, 11)) //失败
                {


                    return false;
                }

            } while (HomeSts != 3);//成功
            rtn = GTN.mc.GTN_SetHomingMode(AxisCrd, Axis, 8);//切换到位置控制模式
            GTN.mc.GTN_ClrSts(AxisCrd, Axis, 1);//清除发生过的回原点错误、
            return true;

        }
        /// <summary>
        /// 步进回零
        /// </summary>
        /// <param name="Axis">轴号</param>
        /// <param name="Home">回零参数</param>
        /// 
        public static  bool Step_Go_Home(short Axis, short mode = 18)
        {
            short rtn;
            ushort HomeSts;


            GTN.mc.GTN_ClrSts(AxisCrd, Axis, 1);//清除当前错误
            rtn = GTN.mc.GTN_SetHomingMode(AxisCrd, Axis, 6);  //切换到回零模式
            rtn = GTN.mc.GTN_SetEcatHomingPrm(AxisCrd, Axis, mode, 3000, 500, 1000, 0, 0);  //速度加速度参数填入
            LogRecorder.RecordLog(EnumLogContentType.Debug, $"Home:{Axis}-Start.");
            rtn = GTN.mc.GTN_StartEcatHoming(AxisCrd, Axis);//启动回零


            do
            {
                GTN.mc.GTN_GetEcatHomingStatus(AxisCrd, Axis, out HomeSts);
                if ((HomeSts & 1 << 2) != 0) //失败
                {


                    return false;
                }

            } while (HomeSts != 3);//成功
            LogRecorder.RecordLog(EnumLogContentType.Debug, $"Home:{Axis}-End.");
            rtn = GTN.mc.GTN_SetHomingMode(AxisCrd, Axis, 8);//切换到位置控制模式
            //Thread.Sleep(3000);
            GTN.mc.GTN_ClrSts(AxisCrd, Axis, 1);//清除发生过的回原点错误、
            return true;




        }




        /// <summary>
        /// 使能
        /// </summary>
        /// <param name="Axis">轴号</param>
        /// <param name="Enable">使能</param>
        public static short MC_Power(short Axis, bool Enable)
        {
            short rtn = 0;
            if (Enable)
            {
                rtn += GTN.mc.GTN_AxisOn(AxisCrd, Axis);
            }
            else
            {
                rtn += GTN.mc.GTN_AxisOff(AxisCrd, Axis);
            }
            return rtn;
        }
        /// <summary>
        /// 获取编码器位置(pulse)
        /// </summary>
        /// <param name="Axis"></param>
        /// <returns></returns>
        public static double MC_GetEncPos(short Axis)
        {
            
            double encpos;
            uint clk;
            GTN.mc.GTN_GetEncPos(AxisCrd, Axis, out encpos,1,out clk);
          //  GTN.mc.GTN_GetEncPos(1, Form_Msg.MAxis, out encpos, 1, out clk);
            return encpos;


        }
        /// <summary>
        /// 获取规划位置(pulse)
        /// </summary>
        /// <param name="Axis"></param>
        /// <returns></returns>
        public static int MC_GetPrfPos(short Axis)
        {
            uint clk;
            double prfpos;
            GTN.mc.GTN_GetPrfPos(AxisCrd, Axis, out prfpos, 1, out clk);
            return (int)Math.Round(prfpos);
        }
        public static int MC_GetPrfVel(short Axis)
        {
            uint clk;
            double prfvel;
            GTN.mc.GTN_GetPrfVel(AxisCrd, Axis, out prfvel, 1, out clk);
            return (int)Math.Round(prfvel);
        }
        public static int MC_GetPrfAcc(short Axis)
        {
            uint clk;
            double prfvel;
            GTN.mc.GTN_GetPrfAcc(AxisCrd, Axis, out prfvel, 1, out clk);
            return (int)Math.Round(prfvel);
        }

        public static int MC_GetSoftLimitPositive(short Axis)
        {
            Int32 Postive;
            Int32 Negative;
            GTN.mc.GTN_GetSoftLimit(AxisCrd, Axis, out Postive, out Negative);
            return Postive;

        }

        public static int MC_GetSoftLimitNegative(short Axis)
        {
            Int32 Postive;
            Int32 Negative;
            GTN.mc.GTN_GetSoftLimit(AxisCrd, Axis, out Postive, out Negative);
            return Negative;

        }

        public static void MC_SetSoftLimitNegativeAndPostive(short Axis, Int32 P,Int32  N)
        {
  
            GTN.mc.GTN_SetSoftLimitMode(AxisCrd, Axis, 1);
            GTN.mc.GTN_SetSoftLimit(AxisCrd, Axis, P, N);
        }

        public static void CloseSoftLimit(short Axis)
        {

            GTN.mc.GTN_SetSoftLimitMode(AxisCrd, Axis, 1);
         
            GTN.mc.GTN_SetSoftLimit(AxisCrd, Axis, 0x7fffffff,unchecked((int)0x80000000) );
        }

        public static void Axis_Stop(short Axis)
        {

            GTN.mc.GTN_Stop(AxisCrd, 1 << (Axis - 1), 1 << (Axis - 1));
        }

        public static void Axis_ClearSt(short Axis)
        {

            GTN.mc.GTN_ClrSts(AxisCrd, Axis, 1);
        }
       
        public static void ConCardClose()
        {

            GTN.mc.GTN_TerminateEcatComm(AxisCrd);
            GTN.mc.GTN_Close();
        }
        public static void  CardReset()
        {

            GTN.mc.GTN_Reset(AxisCrd);


        }

        public static int MC_GetEncVel(short Axis)
        {
            uint clk;
            double prfvel;
            GTN.mc.GTN_GetEncVel(AxisCrd, Axis, out prfvel, 1, out clk);
            return (int)Math.Round(prfvel);
        }

        public static double Int2DoubleDivide(double numerator, int denominator)
        {
            // 确保分母不为零
            if (denominator == 0)
            {
                throw new DivideByZeroException("分母不能为零。");
            }

            // 将其中一个操作数转换为 double
            return (double)(numerator / denominator);
        }


        

    }

    class IOFun
    {
        const short AxisCrd = 1;
        private short rtn;

        public static void IO_Init()
        {
            short rtn;
            byte count;
            //rtn = GTN.glink.GT_GLinkInitEx(0, 1);
            rtn = GTN.glink.GT_GLinkInitEx(0, 3);

            rtn = GTN.glink.GT_GetGLinkOnlineSlaveNum(out count);

        }

        public static void IO_WriteOutPut(ushort EcatID, short number,int Value)
        {

            short rtn = 0;
            number = (short)(number - 1);
            // 确定字节和位的位置
            int ioIndex = number;
            int byteIndex = ioIndex / 8; // 每个字节包含8个位
            int bitIndex = ioIndex % 8;  // 确定在字节中的位位置
            byte[] BUFF = new byte[2];
            rtn = GTN.mc.GTN_EcatIOReadInput(1, EcatID, 0, 2, out BUFF[0]); //读取当前IO状态
            BUFF[byteIndex] = (byte)(BUFF[byteIndex] | (Value << bitIndex));
            if (1 == Value)
            {
                BUFF[byteIndex] = (byte)(BUFF[byteIndex] | (Value << bitIndex));
            }
            else if (0 == Value)
            {
                BUFF[byteIndex] = (byte)(BUFF[byteIndex] & (~(1 << bitIndex)));

            }


            //  rtn = GTN.mc.GTN_EcatIOBitWriteOutput(1, EcatID, 0, number,(byte) Value);
            GTN.mc.GTN_EcatIOWriteOutput(1, EcatID,0,2, ref BUFF[0]);
            GTN.mc.GTN_EcatIOSynch(1);


        }

        public static void IO_WriteOutPut_2(ushort slaveno, short doIndex, int Value)
        {
            byte v = 0;
            if(Value == 0)
            {
                v = 0;
            }
            else if(Value == 1)
            {
                v = 1;
            }
            short rtn = GTN.glink.GT_SetGLinkDoBit((short)slaveno, (short)doIndex, v);

        }


        public static void IO_ReadInput(ushort EcatID, int ioIndex,out int Value)
        {
            // 确定字节和位的位置
            ioIndex = ioIndex - 1;//偏移位从1开始
            int byteIndex = ioIndex / 8; // 每个字节包含8个位
            int bitIndex = ioIndex % 8;  // 确定在字节中的位位置
            byte[] BUFF = new byte[2];

            short rtn = 0;
            rtn = GTN.mc.GTN_EcatIOReadInput(1, EcatID, 6, 2, out BUFF[0]);
            int isSet = (BUFF[byteIndex] & (1 << bitIndex)) != 0 ? 1 : 0;
            Value = isSet;


        }

        public static void IO_ReadInput_2(ushort slaveno, int doIndex, out int Value)
        {
            // 确定字节和位的位置
            //doIndex = doIndex - 1;//偏移位从1开始
            int byteIndex = doIndex / 8; // 每个字节包含8个位
            int bitIndex = doIndex % 8;  // 确定在字节中的位位置
            byte[] BUFF = new byte[2];

            short rtn = 0;
            byte[] di0 = new byte[4];

            GTN.glink.GT_GetGLinkDi((short)slaveno, 0, out di0[0], 2);

            int isSet = (di0[byteIndex] & (1 << bitIndex)) != 0 ? 1 : 0;
            Value = isSet;


        }


        public static void IO_ReadOutput(ushort EcatID, int ioIndex, out int Value)
        {
            // 确定字节和位的位置
            ioIndex = ioIndex - 1;//偏移位从1开始
            int byteIndex = ioIndex / 8; // 每个字节包含8个位
            int bitIndex = ioIndex % 8;  // 确定在字节中的位位置
            byte[] BUFF = new byte[2];

            short rtn = 0;
            rtn = GTN.mc.GTN_EcatIOReadInput(1, EcatID, 0, 2, out BUFF[0]);
            int isSet = (BUFF[byteIndex] & (1 << bitIndex)) != 0 ? 1 : 0;
            Value = isSet;


        }

        public static void IO_ReadOutput_2(ushort slaveno, int doIndex, out int Value)
        {
            // 确定字节和位的位置
            doIndex = doIndex - 1;//偏移位从1开始
            int byteIndex = doIndex / 8; // 每个字节包含8个位
            int bitIndex = doIndex % 8;  // 确定在字节中的位位置
            byte[] BUFF = new byte[2];

            short rtn = 0;
            byte[] do0 = new byte[4];

            GTN.glink.GT_GetGLinkDo((short)slaveno, 0, ref do0[0], 2);

            int isSet = (do0[byteIndex] & (1 << bitIndex)) != 0 ? 1 : 0;
            Value = isSet;


        }


        static byte[] ShortArrayToByteArray(short[] shortArray)
        {
            // 创建一个 byte 数组，大小是 short 数组大小的 2 倍  
            byte[] byteArray = new byte[shortArray.Length * 2];

            // 将每个 short 转换成对应的 byte 数组  
            for (int i = 0; i < shortArray.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(shortArray[i]);
                Array.Copy(bytes, 0, byteArray, i * 2, 2);
            }

            return byteArray;
        }

        static short[] ByteArrayToShortArray(byte[] byteArray)
        {
            if (byteArray.Length % 2 != 0)
            {
                throw new ArgumentException("字节数组的长度必须是2的倍数。");
            }

            // 创建一个 short 数组，大小为 byte 数组长度的一半  
            short[] shortArray = new short[byteArray.Length / 2];

            // 将每 2 个字节转换为一个 short  
            for (int i = 0; i < shortArray.Length; i++)
            {
                shortArray[i] = BitConverter.ToInt16(byteArray, i * 2);
            }

            return shortArray;
        }

        public static void IO_WriteOutPut_D(ushort EcatID, ushort offset, short[] Value)
        {
            ushort Num = 2;
            Num = (ushort)(Value.Length * 2);

            byte[] byteArray = ShortArrayToByteArray(Value);

            short rtn = 0;

            byte[] BUFF = new byte[Num];
            rtn = GTN.mc.GTN_EcatIOReadInput(1, EcatID, offset, Num, out BUFF[0]); //读取当前IO状态

            BUFF = byteArray;


            //  rtn = GTN.mc.GTN_EcatIOBitWriteOutput(1, EcatID, 0, number,(byte) Value);
            GTN.mc.GTN_EcatIOWriteOutput(1, EcatID, offset, Num, ref BUFF[0]);
            GTN.mc.GTN_EcatIOSynch(1);


        }

        public static void IO_ReadInput_D(ushort EcatID, ushort offset, out int Value)
        {
            byte[] BUFF = new byte[2];
            short[] value;

            short rtn = 0;
            rtn = GTN.mc.GTN_EcatIOReadInput(1, EcatID, (ushort)(offset + 6), 2, out BUFF[0]);
            value = ByteArrayToShortArray(BUFF);



            Value = value[0];


        }

        public static void IO_ReadOutput_D(ushort EcatID, ushort offset, out int Value)
        {
            byte[] BUFF = new byte[2];
            short[] value;

            short rtn = 0;
            rtn = GTN.mc.GTN_EcatIOReadInput(1, EcatID, offset, 2, out BUFF[0]); //读取当前IO状态
            value = ByteArrayToShortArray(BUFF);
            Value = value[0];

        }

        public static void IO_ReadInput_A(ushort slaveno, ushort offset, out int Value)
        {
            short[] Ai = new short[4];
            GTN.glink.GT_GetGLinkAi((short)slaveno, 0, out Ai[0], 4);

            Value = Ai[offset];


        }

        public static void IO_ReadOutput_A(ushort slaveno, ushort offset, out int Value)
        {
            short[] Ai = new short[4];
            GTN.glink.GT_GetGLinkAo((short)slaveno, 0, out Ai[0], 4);

            Value = Ai[offset];

        }


        public static void IO_ReadAllInput(ushort EcatID, out List<int> Values)
        {
            Values = new List<int>();
            byte[] BUFF = new byte[2];
            short rtn = GTN.mc.GTN_EcatIOReadInput(1, EcatID, 6, 2, out BUFF[0]);

            for (int i = 0; i < 8; i++)
            {
                Values.Add((BUFF[0] >> i) & 1);
            }
            for (int i = 0; i < 8; i++)
            {
                Values.Add((BUFF[1] >> i) & 1);
            }
        }
        public static void IO_ReadAllOutput(ushort EcatID, out List<int> Values)
        {
            Values = new List<int>();
            byte[] BUFF = new byte[2];
            short rtn = GTN.mc.GTN_EcatIOReadInput(1, EcatID, 0, 2, out BUFF[0]);

            for (int i = 0; i < 8; i++)
            {
                Values.Add((BUFF[0] >> i) & 1);
            }
            for (int i = 0; i < 8; i++)
            {
                Values.Add((BUFF[1] >> i) & 1);
            }
        }

        public static void IO_ReadAllInput_2(ushort slaveno, out List<int> Values)
        {
            Values = new List<int>();
            byte[] di0 = new byte[4];
            short rtn = GTN.glink.GT_GetGLinkDi((short)slaveno, 0, out di0[0], 2);

            for (int i = 0; i < 8; i++)
            {
                Values.Add((di0[0] >> i) & 1);
            }
            for (int i = 0; i < 8; i++)
            {
                Values.Add((di0[1] >> i) & 1);
            }
        }
        public static void IO_ReadAllOutput_2(ushort slaveno, out List<int> Values)
        {
            Values = new List<int>();
            byte[] do0 = new byte[4];
            short rtn = GTN.glink.GT_GetGLinkDo((short)slaveno, 0, ref do0[0], 2);

            for (int i = 0; i < 8; i++)
            {
                Values.Add((do0[0] >> i) & 1);
            }
            for (int i = 0; i < 8; i++)
            {
                Values.Add((do0[1] >> i) & 1);
            }
        }


        public static bool MC_GetGLinkCommStatus()
        {
            GLINK_COMM_STS G;
            GTN.glink.GT_GetGLinkCommStatus(out G);
            if (G.commStatus == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }

    class AxisPvt
    {
        public short Axis;
        public const short CORE = 1;
        short rtn;
        //PVT转盘测试
        public double velBegin = 0;
        public int PvtDataNum = 0;
        public double[] Pos = new double[1024];
        public double[] Time = new double[1024];
        public double[] Percent = new double[1024];
        List<double> Math_Pos = new List<double>();
        List<double> Math_Vel = new List<double>();
        List<double> Math_Percent = new List<double>();
        /// <summary>
        /// 解析PVT数据
        /// </summary>
        /// <param name="strdata">数据</param>
        private void ReadData(string strdata)
        {
            try
            {
                double[] PvtPos = new double[1024];
                double[] PvtTime = new double[1024];
                double[] PvtPercent = new double[1024];
                int count = 0;
                double startvel = 0;
                string[] data = strdata.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (data[0] != "type=PvtPercent")
                {
                    throw new Exception("数据第一行错误误");
                }
                if (!data[1].Contains("velBegin"))
                {
                    throw new Exception("数据第二行错误");
                }
                else
                {
                    startvel = Convert.ToDouble(data[1].Split('=')[1].Trim());
                }
                if (!data[2].Contains("count"))
                {
                    throw new Exception("数据第三行错误");
                }
                else
                {
                    count = Convert.ToInt32(data[2].Split('=')[1].Trim());
                }
                if (count != data.Length - 3)
                {
                    throw new Exception("数据数量错误");
                }

                for (int i = 3; i < data.Length; i++)
                {
                    string[] param = data[i].Split(',');
                    PvtTime[i - 3] = Convert.ToDouble(param[0].Trim());
                    PvtPos[i - 3] = Convert.ToDouble(param[1].Trim());
                    PvtPercent[i - 3] = Convert.ToDouble(param[2].Trim());
                }
                PvtPos.CopyTo(this.Pos, 0);
                PvtTime.CopyTo(this.Time, 0);
                PvtPercent.CopyTo(this.Percent, 0);
                this.velBegin = startvel;
                this.PvtDataNum = count;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 读取TXT文件信息
        /// </summary>
        /// <param name="fileName"></param>
        public void File_ReadData(string fileName)
        {
            string strdata = System.IO.File.ReadAllText(fileName);
            ReadData(strdata);

        }
        public void m_Pvt()
        {
            rtn = GTN.mc.GTN_PrfPvt(CORE, Axis);

            rtn = GTN.mc.GTN_PvtTablePercent(CORE, Axis, this.PvtDataNum, ref this.Time[0], ref Pos[0], ref Percent[0], this.velBegin);

            rtn = GTN.mc.GTN_PvtTableSelect(CORE, Axis, Axis);

            rtn = GTN.mc.GTN_SetPvtLoop(CORE, Axis, 1);
        }
        public void PvtData(double s, double Maxvel, double acc)
        {
            double VelRun;//运行速度
            double t, t0, t1, t2;
            double s0, s1, s2;
            VelRun = Math.Min(Math.Pow(s * acc, 0.5), Maxvel);
            t0 = VelRun / acc;
            t2 = VelRun / acc;
            s0 = t0 * VelRun / 2;
            s2 = t2 * VelRun / 2;
            s1 = s - s0 - s2;
            t1 = s1 / VelRun;

        }


    }

}
