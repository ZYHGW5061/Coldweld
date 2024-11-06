using ConfigurationClsLib;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamometerControllerClsLib
{
    public enum DynamometerAdd
    {
        /// <summary>
        /// 测量值
        /// </summary>
        Measurementvalue = 0,
        /// <summary>
        /// 小数点 0 个位 1 十分位 2 百分位 3 千分位
        /// </summary>
        Decimalpoint = 1,
        /// <summary>
        /// 1-5，1：MPa；2：Kg；3：T;4:g；5：N；6: KN
        /// </summary>
        Unit = 2,
        /// <summary>
        /// 屏蔽值（小于该值输出为零）
        /// </summary>
        Maskingvalue,
        /// <summary>
        /// 采样频率 1:600pcs；2:300pcs；3:150pcs；4:75PCS; 5:37.5PCS; 6:18.75PCS; 7:10PCS;
        /// </summary>
        samplingfrequency,
        /// <summary>
        /// RC滤波时间常数
        /// </summary>
        RCfilteringtimeconstant,
        /// <summary>
        /// 485 设备地址
        /// </summary>
        deviceaddress,
        /// <summary>
        /// 波特率 0-6:0:1200；1：2400；2：4800；3：9600；4：19200；5：38400；6:115200 
        /// </summary>
        Baudrate,
        /// <summary>
        /// 校验设置（0-3）需要初始化串口
        /// </summary>
        VerificationSetting,
        /// <summary>
        /// 模拟量输出低位对应显示值 
        /// </summary>
        LowValue,
        /// <summary>
        /// 模拟量输出高对应显示值 
        /// </summary>
        HighValue,

        /// <summary>
        /// 校准点 1 期望显示的值 
        /// </summary>
        calibrationpoint1,
        /// <summary>
        /// 校准点 2 期望显示的值 
        /// </summary>
        calibrationpoint2,
        /// <summary>
        /// 校准点 3 期望显示的值 
        /// </summary>
        calibrationpoint3,
        /// <summary>
        /// 校准点 4 期望显示的值 
        /// </summary>
        calibrationpoint4,
        /// <summary>
        /// 校准点 5 期望显示的值 
        /// </summary>
        calibrationpoint5,
        /// <summary>
        /// 校准点数
        /// </summary>
        Calibrationpoints,
        /// <summary>
        /// 去皮
        /// </summary>
        Peeling,

        /// <summary>
        /// 模拟量输出低位校正值 
        /// </summary>
        LowCalibrationValue,
        /// <summary>
        /// 模拟量输出低位校正值 
        /// </summary>
        HighCalibrationValue,
        /// <summary>
        /// 零点跟踪范围
        /// </summary>
        zerotrace,
        /// <summary>
        /// 零点跟踪时间
        /// </summary>
        Zeropointtrackingtime,

        /// <summary>
        /// 清零
        /// </summary>
        Zeroing,

        /// <summary>
        /// 输入极性设置
        /// </summary>
        Inputpolaritysetting,

        /// <summary>
        /// 输出信号类型
        /// </summary>
        Outputsignaltype,

        /// <summary>
        /// 模拟量输出校准值选择
        /// </summary>
        Selectionofcalibrationvalues,

        /// <summary>
        /// 模拟量输出校准确认
        /// </summary>
        outputcalibrationconfirmation,

    }

    public class JNSENSORDynamometer : IDynamometerController
    {
        SerialPort PowerControl = new SerialPort();
        private DynamometerControllerConfig _config = null;
        bool coms = false;
        public int PLCadd = 1;

        public JNSENSORDynamometer(DynamometerControllerConfig config)
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
        /// 连接压力传感器
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
                    PowerControl.Close();
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
            catch (Exception ex)
            {
                coms = false;
                IsConnect = false;
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// 连接压力传感器
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
                    //length = 15;
                    data = new byte[length];
                    PowerControl.Read(data, 0, length);

                    if (length > 0)
                    {
                        if (data[1] == 0x03)
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

        public double ReadValue()
        {
            byte[] BTData = PCread(PLCadd, (int)DynamometerAdd.Measurementvalue, 1);
            int Data = ByteToSignedInt16(BTData);

            double Data1 = (double)Data / 1000;

            return Data;
        }

        public void Write()
        {
            throw new NotImplementedException();
        }
    }
}
