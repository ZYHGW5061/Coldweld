using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.SerialCommunicationClsLib;

namespace TemperatureControllerClsLib
{

    public enum TemperatureAdd
    {
        /// <summary>
        /// PV值
        /// </summary>
        [Description("0100")]
        PVvalue,
        /// <summary>
        /// 执行SV值
        /// </summary>
        [Description("0101")]
        RunSVvalue,
        /// <summary>
        /// 运行标志
        /// </summary>
        [Description("0104")]
        EXE_FLG,
        /// <summary>
        /// 事件输出标志
        /// </summary>
        [Description("0105")]
        EV_FLG,
        /// <summary>
        /// REM值
        /// </summary>
        [Description("0108")]
        REMvalue,
        /// <summary>
        /// DI状态标志
        /// </summary>
        [Description("010B")]
        DI_FLG,
        /// <summary>
        /// 量程
        /// </summary>
        [Description("0111")]
        Ring,
        /// <summary>
        /// 小数点位置
        /// </summary>
        [Description("0113")]
        DP,
        /// <summary>
        /// PV下限
        /// </summary>
        [Description("0114")]
        PV_SC_L,
        /// <summary>
        /// PV上限
        /// </summary>
        [Description("0115")]
        PV_SC_H,
        /// <summary>
        /// 程序运行标志
        /// </summary>
        [Description("0120")]
        E_PRG,
        /// <summary>
        /// 执行曲线的数量
        /// </summary>
        [Description("0123")]
        E_PRT,
        /// <summary>
        /// 执行的布号
        /// </summary>
        [Description("0124")]
        E_STP,
        /// <summary>
        /// 执行步的剩余时间
        /// </summary>
        [Description("0125")]
        E_TIM,
        /// <summary>
        /// 执行PID号
        /// </summary>
        [Description("0126")]
        E_PID,
        /// <summary>
        /// 自整定
        /// </summary>
        [Description("0184")]
        Self_tuning,
        /// <summary>
        /// 操作
        /// </summary>
        [Description("018C")]
        Operation,
        /// <summary>
        /// 程序运行标志/待机
        /// </summary>
        [Description("0190")]
        ProgramRunStandby,
        /// <summary>
        /// 程序运行时间
        /// </summary>
        [Description("0191")]
        ProgramRunTime,
        /// <summary>
        /// 程序运行剩余时间
        /// </summary>
        [Description("0192")]
        ProgramRemainingTime,
        /// <summary>
        /// 程序运行步号
        /// </summary>
        [Description("0193")]
        ProgramStepNumber,
        /// <summary>
        /// 程序运行步剩余时间
        /// </summary>
        [Description("0194")]
        ProgramStepRemainingTime,
        /// <summary>
        /// 程序运行PID号
        /// </summary>
        [Description("0195")]
        ProgramPIDNumber,
        /// <summary>
        /// 读取通道1 PV值
        /// </summary>
        [Description("0280")]
        PV_CH1,
        /// <summary>
        /// 读取通道2 PV值
        /// </summary>
        [Description("0281")]
        PV_CH2,
        /// <summary>
        /// 读取通道3 PV值
        /// </summary>
        [Description("0282")]
        PV_CH3,
        /// <summary>
        /// SV
        /// </summary>
        [Description("0300")]
        SV,
        /// <summary>
        /// SV Limit_L
        /// </summary>
        [Description("0301")]
        SV_Limit_L,
        /// <summary>
        /// SV Limit_H
        /// </summary>
        [Description("0302")]
        SV_Limit_H,
        /// <summary>
        /// REM下限
        /// </summary>
        [Description("0320")]
        REM_Limit_L,
        /// <summary>
        /// REM上限
        /// </summary>
        [Description("0321")]
        REM_Limit_H,
        /// <summary>
        /// 测量范围
        /// </summary>
        [Description("0322")]
        MeasurementRange,
        /// <summary>
        /// REM回路
        /// </summary>
        [Description("0324")]
        REM_Loop,
        /// <summary>
        /// FIX控制PV
        /// </summary>
        [Description("0400")]
        FIX_Control_PV,
        /// <summary>
        /// FIX控制时间
        /// </summary>
        [Description("0401")]
        FIX_Control_Time,
        /// <summary>
        /// FIX手动控制补偿
        /// </summary>
        [Description("0402")]
        FIX_Manual_Compensation,
        /// <summary>
        /// FIX动作偏差
        /// </summary>
        [Description("0403")]
        FIX_Action_Deviation,
        /// <summary>
        /// FIX输出偏差
        /// </summary>
        [Description("0404")]
        FIX_Output_Deviation,
        /// <summary>
        /// FIX输出上限
        /// </summary>
        [Description("0405")]
        FIX_Output_Upper_Limit,
        /// <summary>
        /// FIX输出下限
        /// </summary>
        [Description("0406")]
        FIX_Output_Lower_Limit,
        /// <summary>
        /// Prog控制PV1
        /// </summary>
        [Description("0410")]
        Prog_Control_PV1,
        /// <summary>
        /// Prog控制时间1
        /// </summary>
        [Description("0411")]
        Prog_Control_Time1,
        /// <summary>
        /// Prog控制PV2
        /// </summary>
        [Description("0412")]
        Prog_Control_PV2,
        /// <summary>
        /// Prog控制时间2
        /// </summary>
        [Description("0413")]
        Prog_Control_Time2,
        /// <summary>
        /// Prog控制PV3
        /// </summary>
        [Description("0414")]
        Prog_Control_PV3,
        /// <summary>
        /// Prog控制时间3
        /// </summary>
        [Description("0415")]
        Prog_Control_Time3,
        /// <summary>
        /// Prog控制PV4
        /// </summary>
        [Description("0416")]
        Prog_Control_PV4,
        /// <summary>
        /// Prog控制时间4
        /// </summary>
        [Description("0417")]
        Prog_Control_Time4,
        /// <summary>
        /// Prog控制PV5
        /// </summary>
        [Description("0418")]
        Prog_Control_PV5,
        /// <summary>
        /// Prog控制时间5
        /// </summary>
        [Description("0419")]
        Prog_Control_Time5,
        /// <summary>
        /// Prog控制PV6
        /// </summary>
        [Description("041A")]
        Prog_Control_PV6,
        /// <summary>
        /// Prog控制时间6
        /// </summary>
        [Description("041B")]
        Prog_Control_Time6,
        /// <summary>
        /// EV1模式
        /// </summary>
        [Description("0500")]
        EV1_Mode,
        /// <summary>
        /// EV1设定值
        /// </summary>
        [Description("0501")]
        EV1_Set_Value,
        /// <summary>
        /// EV1回差
        /// </summary>
        [Description("0502")]
        EV1_Hysteresis,
        /// <summary>
        /// EV1延时
        /// </summary>
        [Description("0503")]
        EV1_Delay,
        /// <summary>
        /// EV1回路
        /// </summary>
        [Description("0506")]
        EV1_Loop,
        /// <summary>
        /// EV2模式
        /// </summary>
        [Description("0510")]
        EV2_Mode,
        /// <summary>
        /// EV2设定值
        /// </summary>
        [Description("0511")]
        EV2_Set_Value,
        /// <summary>
        /// EV2回差
        /// </summary>
        [Description("0512")]
        EV2_Hysteresis,
        /// <summary>
        /// EV2延时
        /// </summary>
        [Description("0513")]
        EV2_Delay,
        /// <summary>
        /// EV2回路
        /// </summary>
        [Description("0516")]
        EV2_Loop,
        /// <summary>
        /// EV3模式
        /// </summary>
        [Description("0520")]
        EV3_Mode,
        /// <summary>
        /// EV3设定值
        /// </summary>
        [Description("0521")]
        EV3_Set_Value,
        /// <summary>
        /// EV3回差
        /// </summary>
        [Description("0522")]
        EV3_Hysteresis,
        /// <summary>
        /// EV3延时
        /// </summary>
        [Description("0523")]
        EV3_Delay,
        /// <summary>
        /// EV3回路
        /// </summary>
        [Description("0526")]
        EV3_Loop,
        /// <summary>
        /// DI
        /// </summary>
        [Description("0580")]
        DI,
        /// <summary>
        /// 存储器
        /// </summary>
        [Description("05B0")]
        Memory,
        /// <summary>
        /// 输出偏差
        /// </summary>
        [Description("0600")]
        Output_Deviation,
        /// <summary>
        /// 输出偏差设定值
        /// </summary>
        [Description("0601")]
        Output_Deviation_Set_Value,
        /// <summary>
        /// 故障动作
        /// </summary>
        [Description("0602")]
        Fault_Action,
        /// <summary>
        /// AT点
        /// </summary>
        [Description("0610")]
        AT_Point,
        /// <summary>
        /// 锁键
        /// </summary>
        [Description("0611")]
        Key_Lock,
        /// <summary>
        /// PV偏移
        /// </summary>
        [Description("0701")]
        PV_Offset,
        /// <summary>
        /// PV 滤波
        /// </summary>
        [Description("0702")]
        PV_Filter,
        /// <summary>
        /// PV跟随显示
        /// </summary>
        [Description("0710")]
        PV_Follow_Display,
        /// <summary>
        /// PV显示
        /// </summary>
        [Description("0711")]
        PV_Display,
        /// <summary>
        /// 设定方式
        /// </summary>
        [Description("0800")]
        Setting_Mode,
        /// <summary>
        /// PV启动
        /// </summary>
        [Description("0801")]
        PV_Start,
        /// <summary>
        /// 步
        /// </summary>
        [Description("0882")]
        Step,
        /// <summary>
        /// 事件输出标志
        /// </summary>
        [Description("0883")]
        Event_Output_Flag,
        /// <summary>
        /// 执行SV值
        /// </summary>
        [Description("0884")]
        Run_SV_Value,
        /// <summary>
        /// 第一步 SV
        /// </summary>
        [Description("08A0")]
        Step1_SV,
        /// <summary>
        /// 第一步 PID 号码
        /// </summary>
        [Description("08A1")]
        Step1_PID_Number,
        /// <summary>
        /// 第二步 SV
        /// </summary>
        [Description("08A2")]
        Step2_SV,
        /// <summary>
        /// 第二步 PID 号码
        /// </summary>
        [Description("08A3")]
        Step2_PID_Number,
        /// <summary>
        /// 第三步 SV
        /// </summary>
        [Description("08A4")]
        Step3_SV,
        /// <summary>
        /// 第三步 PID 号码
        /// </summary>
        [Description("08A5")]
        Step3_PID_Number,
        /// <summary>
        /// 第四步 SV
        /// </summary>
        [Description("08A6")]
        Step4_SV,
        /// <summary>
        /// 第四步 PID 号码
        /// </summary>
        [Description("08A7")]
        Step4_PID_Number,
        /// <summary>
        /// 第五步 SV
        /// </summary>
        [Description("08A8")]
        Step5_SV,
        /// <summary>
        /// 第五步 PID 号码
        /// </summary>
        [Description("08A9")]
        Step5_PID_Number,
        /// <summary>
        /// 第六步 SV
        /// </summary>
        [Description("08AA")]
        Step6_SV,
        /// <summary>
        /// 第六步 PID 号码
        /// </summary>
        [Description("08AB")]
        Step6_PID_Number,
        /// <summary>
        /// 第七步 SV
        /// </summary>
        [Description("08AC")]
        Step7_SV,
        /// <summary>
        /// 第七步 PID 号码
        /// </summary>
        [Description("08AD")]
        Step7_PID_Number,
        /// <summary>
        /// 第八步 SV
        /// </summary>
        [Description("08AE")]
        Step8_SV,
        /// <summary>
        /// 第八步 PID 号码
        /// </summary>
        [Description("08AF")]
        Step8_PID_Number,
        /// <summary>
        /// 第九步 SV
        /// </summary>
        [Description("08B0")]
        Step9_SV,
        /// <summary>
        /// 第九步 PID 号码
        /// </summary>
        [Description("08B1")]
        Step9_PID_Number,
        /// <summary>
        /// 第十步 SV
        /// </summary>
        [Description("08B2")]
        Step10_SV,
        /// <summary>
        /// 第十步 PID 号码
        /// </summary>
        [Description("08B3")]
        Step10_PID_Number,
        /// <summary>
        /// 第十一步 SV
        /// </summary>
        [Description("08B4")]
        Step11_SV,
        /// <summary>
        /// 第十一步 PID 号码
        /// </summary>
        [Description("08B5")]
        Step11_PID_Number,
        /// <summary>
        /// 第十二步 SV
        /// </summary>
        [Description("08B6")]
        Step12_SV,
        /// <summary>
        /// 第十二步 PID 号码
        /// </summary>
        [Description("08B7")]
        Step12_PID_Number,
        /// <summary>
        /// 第十三步 SV
        /// </summary>
        [Description("08B8")]
        Step13_SV,
        /// <summary>
        /// 第十三步 PID 号码
        /// </summary>
        [Description("08B9")]
        Step13_PID_Number,
        /// <summary>
        /// 第十四步 SV
        /// </summary>
        [Description("08BA")]
        Step14_SV,
        /// <summary>
        /// 第十四步 PID 号码
        /// </summary>
        [Description("08BB")]
        Step14_PID_Number,
        /// <summary>
        /// 第十五步 SV
        /// </summary>
        [Description("08BC")]
        Step15_SV,
        /// <summary>
        /// 第十五步 PID 号码
        /// </summary>
        [Description("08BD")]
        Step15_PID_Number,
        /// <summary>
        /// 第十六步 SV
        /// </summary>
        [Description("08BE")]
        Step16_SV,
        /// <summary>
        /// 第十六步 PID 号码
        /// </summary>
        [Description("08BF")]
        Step16_PID_Number,
        /// <summary>
        /// 第十七步 SV
        /// </summary>
        [Description("08C0")]
        Step17_SV,
        /// <summary>
        /// 第十七步 PID 号码
        /// </summary>
        [Description("08C1")]
        Step17_PID_Number,
        /// <summary>
        /// 第十八步 SV
        /// </summary>
        [Description("08C2")]
        Step18_SV,
        /// <summary>
        /// 第十八步 PID 号码
        /// </summary>
        [Description("08C3")]
        Step18_PID_Number,
        /// <summary>
        /// 第十九步 SV
        /// </summary>
        [Description("08C4")]
        Step19_SV,
        /// <summary>
        /// 第十九步 PID 号码
        /// </summary>
        [Description("08C5")]
        Step19_PID_Number,
        /// <summary>
        /// 第二十步 SV
        /// </summary>
        [Description("08C6")]
        Step20_SV,
        /// <summary>
        /// 第二十步 PID 号码
        /// </summary>
        [Description("08C7")]
        Step20_PID_Number,
        /// <summary>
        /// 第二十一步 SV
        /// </summary>
        [Description("08C8")]
        Step21_SV,
        /// <summary>
        /// 第二十一步 PID 号码
        /// </summary>
        [Description("08C9")]
        Step21_PID_Number,
        /// <summary>
        /// 第二十二步 SV
        /// </summary>
        [Description("08CA")]
        Step22_SV,
        /// <summary>
        /// 第二十二步 PID 号码
        /// </summary>
        [Description("08CB")]
        Step22_PID_Number,
        /// <summary>
        /// 第二十三步 SV
        /// </summary>
        [Description("08CC")]
        Step23_SV,
        /// <summary>
        /// 第二十三步 PID 号码
        /// </summary>
        [Description("08CD")]
        Step23_PID_Number,
        /// <summary>
        /// 第二十四步 SV
        /// </summary>
        [Description("08CE")]
        Step24_SV,
        /// <summary>
        /// 第二十四步 PID 号码
        /// </summary>
        [Description("08CF")]
        Step24_PID_Number,
        /// <summary>
        /// 第二十五步 SV
        /// </summary>
        [Description("08D0")]
        Step25_SV,
        /// <summary>
        /// 第二十五步 PID 号码
        /// </summary>
        [Description("08D1")]
        Step25_PID_Number,
        /// <summary>
        /// 第二十六步 SV
        /// </summary>
        [Description("08D2")]
        Step26_SV,
        /// <summary>
        /// 第二十六步 PID 号码
        /// </summary>
        [Description("08D3")]
        Step26_PID_Number,
        /// <summary>
        /// 第二十七步 SV
        /// </summary>
        [Description("08D4")]
        Step27_SV,
        /// <summary>
        /// 第二十七步 PID 号码
        /// </summary>
        [Description("08D5")]
        Step27_PID_Number,
        /// <summary>
        /// 第二十八步 SV
        /// </summary>
        [Description("08D6")]
        Step28_SV,
        /// <summary>
        /// 第二十八步 PID 号码
        /// </summary>
        [Description("08D7")]
        Step28_PID_Number,
        /// <summary>
        /// 第二十九步 SV
        /// </summary>
        [Description("08D8")]
        Step29_SV,
        /// <summary>
        /// 第二十九步 PID 号码
        /// </summary>
        [Description("08D9")]
        Step29_PID_Number,
        /// <summary>
        /// 第三十步 SV
        /// </summary>
        [Description("08DA")]
        Step30_SV,
        /// <summary>
        /// 第三十步 PID 号码
        /// </summary>
        [Description("08DB")]
        Step30_PID_Number,
        /// <summary>
        /// 第三十一步 SV
        /// </summary>
        [Description("08DC")]
        Step31_SV,
        /// <summary>
        /// 第三十一步 PID 号码
        /// </summary>
        [Description("08DD")]
        Step31_PID_Number,
        /// <summary>
        /// 第三十二步 SV
        /// </summary>
        [Description("08DE")]
        Step32_SV,
        /// <summary>
        /// 第三十二步 PID 号码
        /// </summary>
        [Description("08DF")]
        Step32_PID_Number,
    }

    public class SHIMADEN:ITemperatureController
    {
        SerialPort PowerControl = new SerialPort();
        private TemperatureControllerConfig _config = null;
        bool coms = false;
        public int PLCadd = 1;

        public SHIMADEN(TemperatureControllerConfig config)
        {
            _config = config;
        }

        public bool IsConnect {
            get
            {
                return _serialPortEngine.IsPortOpened;
            }
            set
            {

            }
        }

        private MR13 TemperatureControl = new MR13();

        public SerialPort SerialPortEngine
        {
            get { return PowerControl; }
            set { PowerControl = value; }
        }
        /// <summary>
        /// 串口通信
        /// </summary>
        private SerialPortController _serialPortEngine;

        #region Private Method
        private int ByteToInt(byte[] BTData)
        {
            try
            {
                if (BTData != null)
                {
                    int offset = BTData.Length;
                    int Data = 0;
                    for (int i = 0; i < offset; i++)
                    {
                        Data = Data + (int)((BTData[i] & 0xFF) << 8 * (offset - i - 1));
                    }
                    //Data = (int)((BTData[offset] & 0xFF)
                    //| ((BTData[offset + 1] & 0xFF) << 8)
                    //| ((BTData[offset + 2] & 0xFF) << 16)
                    //| ((BTData[offset + 3] & 0xFF) << 24));
                    return Data;
                }
                else
                    return -1;

            }
            catch (Exception ex) { return -1; }

        }
        private int ByteToSignedInt16(byte[] BTData)
        {
            if (BTData == null || BTData.Length != 2)
            {
                return -1;
            }

            try
            {
                int result = (BTData[0] << 8) | BTData[1];

                // 如果高位是1，表示这是一个负数
                if ((result & 0x8000) != 0)
                {
                    result -= 0x10000;
                }

                return result;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        private static byte[] CRC16(byte[] value, int Length, ushort poly = 0xA001, ushort crcInit = 0xFFFF)
        {
            if (value == null || !value.Any())
                throw new ArgumentException("");

            //运算
            ushort crc = crcInit;
            for (int i = 0; i < Length; i++)
            {
                crc = (ushort)(crc ^ (value[i]));
                for (int j = 0; j < 8; j++)
                {
                    crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ poly) : (ushort)(crc >> 1);
                }
            }
            byte hi = (byte)((crc & 0xFF00) >> 8);  //高位置
            byte lo = (byte)(crc & 0x00FF);         //低位置

            byte[] buffer = new byte[2];
            //buffer.AddRange(value);
            buffer[0] = lo;//低字节
            buffer[1] = hi;//高字节

            return buffer;
        }
        /// <summary>
        /// 连接温度控制器
        /// </summary>
        /// <param name="com"></param>
        /// <param name="baudrate"></param>
        /// <param name="Databits"></param>
        /// <param name="Stopbits"></param>
        /// <param name="parity"></param>
        /// <returns></returns>
        private int SerialConnect(string com, int baudrate = 115200, int Databits = 8, int Stopbits = 1, int parity = 0)
        {
            PowerControl.PortName = com;//设置端口名
            PowerControl.BaudRate = baudrate;//设置波特率
            PowerControl.DataBits = Databits;
            switch (Stopbits)
            {
                case 0:
                    PowerControl.StopBits = StopBits.None;
                    break;
                case 1:
                    PowerControl.StopBits = StopBits.One;
                    break;
                case 2:
                    PowerControl.StopBits = StopBits.Two;
                    break;
            }
            switch (parity)
            {
                case 0:
                    PowerControl.Parity = Parity.None;
                    break;
                case 1:
                    PowerControl.Parity = Parity.Odd;
                    break;
                case 2:
                    PowerControl.Parity = Parity.Even;
                    break;
                case 3:
                    PowerControl.Parity = Parity.Mark;
                    break;
            }

            try
            {
                if (!TemperatureControl.MR13COM.IsOpen)
                {
                    
                    TemperatureControl.MR13Connect(PowerControl.PortName, PowerControl.BaudRate, PowerControl.DataBits, (int)PowerControl.StopBits, (int)PowerControl.Parity);
                    //PowerControl.Close();
                    //PowerControl.Open();//打开串口
                    coms = true;
                    //loop_back();
                    //PLC_state();
                }
                else
                {
                    TemperatureControl.MR13Disconnect();
                    TemperatureControl.MR13Connect(PowerControl.PortName, PowerControl.BaudRate, PowerControl.DataBits, (int)PowerControl.StopBits, (int)PowerControl.Parity);
                    //PowerControl.Close();//关闭串口
                    //PowerControl.Open();//打开串口
                    coms = true;
                }
                IsConnect = true;
            }
            catch (Exception ex)
            {
                coms = false;
                IsConnect = false;
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// 连接温度控制器
        /// </summary>
        /// <returns></returns>
        private int SerialConnect(SerialPort PowerControl)
        {

            if (PowerControl.PortName != $"COM{_config.SerialCommunicator.Port}")
            {
                PowerControl.PortName = $"COM{_config.SerialCommunicator.Port}";//设置端口名
                PowerControl.BaudRate = _config.SerialCommunicator.BaudRate;//设置波特率
                PowerControl.DataBits = _config.SerialCommunicator.DataBits;
                switch ((int)_config.SerialCommunicator.StopBits)
                {
                    case 0:
                        PowerControl.StopBits = StopBits.None;
                        break;
                    case 1:
                        PowerControl.StopBits = StopBits.One;
                        break;
                    case 2:
                        PowerControl.StopBits = StopBits.Two;
                        break;
                }
                switch ((int)_config.SerialCommunicator.Parity)
                {
                    case 0:
                        PowerControl.Parity = Parity.None;
                        break;
                    case 1:
                        PowerControl.Parity = Parity.Odd;
                        break;
                    case 2:
                        PowerControl.Parity = Parity.Even;
                        break;
                    case 3:
                        PowerControl.Parity = Parity.Mark;
                        break;
                }
            }

            this.PowerControl = PowerControl;
            try
            {
                if (!TemperatureControl.MR13COM.IsOpen)
                {
                    
                    TemperatureControl.MR13Connect(PowerControl.PortName, PowerControl.BaudRate, PowerControl.DataBits, (int)PowerControl.StopBits, (int)PowerControl.Parity);
                    //PowerControl.Close();
                    //PowerControl.Open();//打开串口
                    coms = true;
                    //loop_back();
                    //PLC_state();
                }
                else
                {
                    TemperatureControl.MR13Disconnect();
                    TemperatureControl.MR13Connect(PowerControl.PortName, PowerControl.BaudRate, PowerControl.DataBits, (int)PowerControl.StopBits, (int)PowerControl.Parity);
                    //PowerControl.Close();//关闭串口
                    //PowerControl.Open();//打开串口
                    coms = true;
                }
                IsConnect = true;
            }
            catch
            {
                coms = false;
                IsConnect = false;
                return -1;
            }

            return 0;
        }


        /// <summary>
        /// 断开温度控制器
        /// </summary>
        /// <returns></returns>
        private int SerialDisconnect()
        {
            try
            {
                if (TemperatureControl.MR13COM.IsOpen)
                {
                    TemperatureControl.MR13Disconnect();
                    coms = false;
                    IsConnect = false;
                }
            }
            catch
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 读取节点信息
        /// </summary>
        /// <param name="Add"></param>
        /// <param name="num"></param>
        private byte[] PCread(int PLCadd, TemperatureAdd Add, int num)
        {
            try
            {
                if (coms)
                {
                    string hexAddress = Add.GetDescription();
                    byte[] Data0 = TemperatureControl.MR13read(PLCadd, hexAddress, num);

                    return Data0;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 写入节点信息
        /// </summary>
        /// <param name="Add"></param>
        /// <param name="num"></param>
        private void PCwrite(int PLCadd, TemperatureAdd Add, int Data)
        {
            try
            {
                if (coms)
                {
                    string hexAddress = Add.GetDescription();
                    TemperatureControl.MR13write(PLCadd, hexAddress,1, Data);
                }
                else
                {
                    return;
                }
            }
            catch
            {
                return;
            }
        }


        #endregion



        public void Connect()
        {
            SerialConnect($"COM{_config.SerialCommunicator.Port}", _config.SerialCommunicator.BaudRate
                , _config.SerialCommunicator.DataBits, (int)_config.SerialCommunicator.StopBits, (int)_config.SerialCommunicator.Parity);
            PLCadd = _config.SerialCommunicator.DeviceAddress;
        }
        public void Connect(SerialPort serial)
        {
            SerialConnect(serial);
            PLCadd = _config.SerialCommunicator.DeviceAddress;
        }


        /// <summary>
        /// 自整定
        /// </summary>
        /// <param name="Data">0 不执行 1 执行</param>
        public void Selftuning(int Data)
        {
            PCwrite(PLCadd, TemperatureAdd.Self_tuning, Data);
        }

        /// <summary>
        /// 设置目标温度
        /// </summary>
        /// <param name="Data">温度</param>
        public void SetTargetTemperature(int Data)
        {
            PCwrite(PLCadd, TemperatureAdd.SV, Data);
        }

        /// <summary>
        /// 获取目标温度
        /// </summary>
        /// <returns></returns>
        public int GetTargetTemperature()
        {
            byte[] data = PCread(PLCadd, TemperatureAdd.SV, 1);
            int data1 = ByteToInt(data);
            return data1;
        }

        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="Data">0 待机 1 运行</param>
        public void Run(int Data)
        {
            PCwrite(PLCadd, TemperatureAdd.ProgramRunStandby, Data);
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Write(TemperatureRtuAdd Add, int value)
        {
            throw new NotImplementedException();
        }

        public int Read(TemperatureRtuAdd Add)
        {
            throw new NotImplementedException();
        }
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
