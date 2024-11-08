using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.SerialCommunicationClsLib;

namespace TurboMolecularPumpControllerClsLib
{

    public class TurboMolecularPump : ITurboMolecularPumpController
    {
        SerialPort PowerControl = new SerialPort();
        private TurboMolecularPumpControllerConfig _config = null;
        bool coms = false;
        public int PLCadd = 1;

        public TurboMolecularPump(TurboMolecularPumpControllerConfig config)
        {
            _config = config;
        }

        public bool IsConnect { get { return PowerControl.IsOpen; } }

        public SerialPort SerialPortEngine
        {
            get { return PowerControl; }
            set { PowerControl = value; }
        }
        /// <summary>
        /// 串口通信
        /// </summary>
        private SerialPort _serialPortEngine;

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
        /// 连接电源控制器
        /// </summary>
        /// <param name="com"></param>
        /// <param name="baudrate"></param>
        /// <param name="Databits"></param>
        /// <param name="Stopbits"></param>
        /// <param name="parity"></param>
        /// <returns></returns>
        private int SerialConnect(string com, int baudrate = 38400, int Databits = 8, int Stopbits = 1, int parity = 0)
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
                if (!PowerControl.IsOpen)
                {
                    PowerControl.Open();//打开串口
                    coms = true;
                    //loop_back();
                    //PLC_state();
                }
                else
                {
                    //PowerControl.Close();//关闭串口
                    //PowerControl.Open();//打开串口
                    coms = true;
                }
            }
            catch
            {
                coms = false;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 断开电源控制器
        /// </summary>
        /// <returns></returns>
        private int SerialDisconnect()
        {
            try
            {
                if (PowerControl.IsOpen)
                {
                    PowerControl.Close();//关闭串口
                    coms = false;
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
        private bool PCread(int PLCadd, int Add, int num, ref byte[] Data0)
        {
            try
            {
                if (PowerControl.IsOpen)
                {
                    int length = 8;
                    byte[] data = new byte[length];

                    data[0] = Convert.ToByte(PLCadd);
                    data[1] = 0x03;
                    byte[] B0 = BitConverter.GetBytes(Add);
                    byte[] B1 = BitConverter.GetBytes(num);
                    data[2] = B0[1];
                    data[3] = B0[0];
                    data[4] = B1[1];
                    data[5] = B1[0];
                    byte[] data1 = CRC16(data, 6);

                    data[6] = data1[0];
                    data[7] = data1[1];


                    PowerControl.DiscardInBuffer();
                    PowerControl.DiscardOutBuffer();
                    PowerControl.Write(data, 0, length);
                    System.Threading.Thread.Sleep(100);
                    length = PowerControl.BytesToRead;
                    int T = 0;
                    while (length <= 0)
                    {
                        length = PowerControl.BytesToRead;
                        System.Threading.Thread.Sleep(5);
                        T++;
                        if (T > 400)
                        {
                            break;
                        }
                    }
                    //length = 15;
                    data = new byte[length];
                    PowerControl.Read(data, 0, length);

                    System.Threading.Thread.Sleep(200);

                    if (length > 0)
                    {
                        if (data[1] == 0x03 && data[0] == Convert.ToByte(PLCadd))
                        {
                            Data0 = data.Skip(3).Take(length - 5).ToArray();
                            return true;
                        }
                        else
                        {
                            Data0 = null;
                            return false;
                        }
                    }
                    else
                    {
                        Data0 = null;
                        return false;
                    }
                }
                else
                {
                    Data0 = null;
                    return false;
                }
            }
            catch
            {
                Data0 = null;
                return false;
            }
        }
        /// <summary>
        /// 写入节点信息
        /// </summary>
        /// <param name="Add"></param>
        /// <param name="num"></param>
        private bool PCwrite(int PLCadd, int Add, int Data)
        {
            try
            {
                if (PowerControl.IsOpen)
                {
                    int length = 8;
                    byte[] data = new byte[length];

                    data[0] = Convert.ToByte(PLCadd);
                    data[1] = 0x06;
                    byte[] B0 = BitConverter.GetBytes(Add);
                    byte[] B1 = BitConverter.GetBytes(Data);
                    data[2] = B0[1];
                    data[3] = B0[0];
                    data[4] = B1[1];
                    data[5] = B1[0];
                    byte[] data1 = CRC16(data, 6);

                    data[6] = data1[0];
                    data[7] = data1[1];


                    PowerControl.DiscardInBuffer();
                    PowerControl.DiscardOutBuffer();
                    PowerControl.Write(data, 0, length);
                    System.Threading.Thread.Sleep(100);

                    length = PowerControl.BytesToRead;
                    int T = 0;
                    while (length <= 0)
                    {
                        length = PowerControl.BytesToRead;
                        System.Threading.Thread.Sleep(5);
                        T++;
                        if (T > 2000)
                        {
                            break;
                        }
                    }
                    //length = 15;
                    data = new byte[length];
                    PowerControl.Read(data, 0, length);

                    System.Threading.Thread.Sleep(200);

                    if (length > 0)
                    {
                        if (data[1] == 0x06 && data[0] == Convert.ToByte(PLCadd))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private void PCwrites(int PLCadd, int Add, int[] Data)
        {
            try
            {
                if (PowerControl.IsOpen)
                {
                    int length = 9 + Data.Length * 2; // 总长度 = 固定字节(前7字节+1字节'字节数') + 寄存器值字节数（N*2）
                    byte[] data = new byte[length];

                    data[0] = Convert.ToByte(PLCadd);
                    data[1] = 0x10; // 功能码 0x10
                    byte[] B0 = BitConverter.GetBytes(Add);
                    int N = Data.Length;
                    byte[] B1 = BitConverter.GetBytes((short)N);
                    data[2] = B0[1];
                    data[3] = B0[0];
                    data[4] = B1[1];
                    data[5] = B1[0];

                    data[6] = (byte)(N * 2); // 字节数

                    for (int i = 0; i < N; i++)
                    {
                        byte[] regValue = BitConverter.GetBytes((short)Data[i]);
                        data[7 + i * 2] = regValue[1];
                        data[8 + i * 2] = regValue[0];
                    }

                    byte[] crc = CRC16(data, length - 2);
                    data[length - 2] = crc[0];
                    data[length - 1] = crc[1];

                    PowerControl.DiscardInBuffer();
                    PowerControl.DiscardOutBuffer();
                    PowerControl.Write(data, 0, length);
                    System.Threading.Thread.Sleep(100);
                    length = PowerControl.BytesToRead;

                    data = new byte[length];
                    PowerControl.Read(data, 0, length);
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

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Write()
        {
            throw new NotImplementedException();
        }

        public int Read()
        {
            throw new NotImplementedException();
        }

        public bool ReadStatus(ref TurboMolecularPumpstatus param)
        {

            PLCadd = _config.ChannelNumber;
            byte[] BTData = new byte[8];
            bool ret = PCread(PLCadd, (int)TurboMolecularPumpAdd.OutputFrequency, 4, ref BTData);

            if(BTData == null || !ret)
            {
                return false;
            }

            if (BTData.Length < 8)
            {
                return false;
            }

            // 读取输出频率  
            param.OutputFrequency = (BTData[0] << 8) | BTData[1];

            // 读取输出电压  
            param.OutputVoltage = (BTData[2] << 8) | BTData[3];

            // 读取输出电流  
            param.OutputCurrent = (BTData[4] << 8) | BTData[5];

            // 获取控制器状态  
            param.Standbymode = (((BTData[6] << 8) | BTData[7]) == 0);
            param.Function = (((BTData[6] << 8) | BTData[7]) == 1);
            param.err = (((BTData[6] << 8) | BTData[7]) == 2);
            param.OC = (((BTData[6] << 8) | BTData[7]) == 3);
            param.OE = (((BTData[6] << 8) | BTData[7]) == 4);
            param.Retain = (((BTData[6] << 8) | BTData[7]) == 5);
            param.RLU = (((BTData[6] << 8) | BTData[7]) == 6);
            param.OL2 = (((BTData[6] << 8) | BTData[7]) == 7);
            param.SL = (((BTData[6] << 8) | BTData[7]) == 8);
            param.ESP = (((BTData[6] << 8) | BTData[7]) == 9);
            param.LU = (((BTData[6] << 8) | BTData[7]) == 10);
            param.OH = (((BTData[6] << 8) | BTData[7]) == 11);


            return true;
        }

        public bool SlowShutdown()
        {
            PLCadd = _config.ChannelNumber;
            return PCwrite(PLCadd, (int)TurboMolecularPumpAdd.Command, 3);
        }

        public bool FreeShutdown()
        {
            PLCadd = _config.ChannelNumber;
            return PCwrite(PLCadd, (int)TurboMolecularPumpAdd.Command, 4);
        }

        public bool Function()
        {
            if(DataModel.Instance.TurboMolecularPumpRun(_config.ChannelNumber))
            {
                return false;
            }

            PLCadd = _config.ChannelNumber;
            return PCwrite(PLCadd, (int)TurboMolecularPumpAdd.Command, 8);
        }

        public bool FaultReset()
        {
            PLCadd = _config.ChannelNumber;
            return PCwrite(PLCadd, (int)TurboMolecularPumpAdd.Command, 9);
        }

        public bool Unlock()
        {
            PLCadd = _config.ChannelNumber;
            return PCwrite(PLCadd, (int)TurboMolecularPumpAdd.LockParameters, 1);
        }

        public bool Lock()
        {
            PLCadd = _config.ChannelNumber;
            return PCwrite(PLCadd, (int)TurboMolecularPumpAdd.LockParameters, 2);
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
