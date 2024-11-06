using ConfigurationClsLib;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LaserSensorControllerClsLib
{
    public enum LaserAdd
    {
        /// <summary>
        /// 距离/（连续输出/不连续输出）
        /// </summary>
        Distance = 0,
        /// <summary>
        /// 工作模式
        /// </summary>
        WorkMode = 1,
        /// <summary>
        /// 常开/常闭
        /// </summary>
        Normallyopenandnormallyclosed = 2,
        /// <summary>
        /// 输出检测
        /// </summary>
        Detectionoutput,
        /// <summary>
        /// 应差
        /// </summary>
        Stressdifference,
        /// <summary>
        /// 外部输入
        /// </summary>
        Externalinput,
        /// <summary>
        /// 输出定时
        /// </summary>
        Outputtiming,
        /// <summary>
        /// 输出定时时间
        /// </summary>
        Outputtimingtime,
        /// <summary>
        /// 显示模式
        /// </summary>
        DisplayMode,
        /// <summary>
        /// 保持
        /// </summary>
        Keep,
        /// <summary>
        /// 熄屏选择
        /// </summary>
        Screenshutdownselection,

        /// <summary>
        /// 调零值
        /// </summary>
        Zeroingvalue,
        /// <summary>
        /// 阈值1
        /// </summary>
        threshold1,
        /// <summary>
        /// 阈值2
        /// </summary>
        threshold2,
        /// <summary>
        /// 波特率
        /// </summary>
        Baudrate,

    }



    public class BOJKESensor : ILaserSensorController
    {
        SerialPort PowerControl = new SerialPort();
        private LaserSensorControllerConfig _config = null;
        bool coms = false;
        public int PLCadd = 1;

        public BOJKESensor(LaserSensorControllerConfig config)
        {
            _config = config;
        }

        public bool IsConnect { get; set; } = false;



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
        /// 连接激光测距传感器
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
                if (!PowerControl.IsOpen)
                {
                    PowerControl.Open();//打开串口
                    coms = true;
                    //loop_back();
                    //PLC_state();
                }
                else
                {
                    PowerControl.Close();//关闭串口
                    PowerControl.Open();//打开串口
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
        /// 连接激光测距传感器
        /// </summary>
        /// <returns></returns>
        private int SerialConnect(SerialPort PowerControl)
        {
            if(PowerControl.PortName != $"COM{_config.SerialCommunicator.Port}")
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
        /// 断开激光测距传感器
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
        private byte[] PCread(int PLCadd, int Add, int num)
        {
            try
            {
                if (coms)
                {
                    int length = 8;
                    byte[] data = new byte[length];

                    data[0] = Convert.ToByte(PLCadd);
                    data[1] = 0x04;
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
                    //length = 15;
                    data = new byte[length];
                    PowerControl.Read(data, 0, length);

                    if (length > 0)
                    {
                        if (data[1] == 0x04)
                        {
                            byte[] Data0 = data.Skip(3).Take(length - 5).ToArray();
                            return Data0;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
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
        private void PCwrite(int PLCadd, int Add, int Data)
        {
            try
            {
                if (coms)
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
                    //length = 15;
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
        public void Connect(SerialPort serial)
        {
            SerialConnect(serial);
            PLCadd = _config.SerialCommunicator.DeviceAddress;
        }

        public void Disconnect()
        {
            SerialDisconnect();
        }

        public double ReadDistance()
        {
            byte[] BTData = PCread(PLCadd, (int)LaserAdd.Distance, 1);
            int Data = ByteToInt(BTData);

            if(Data < 65000 || Data > 105000)
            {
                Data = -1;
            }

            return Data;
        }

        public void Write()
        {
            throw new NotImplementedException();
        }
    }
}
