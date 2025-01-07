using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.SerialCommunicationClsLib;

namespace VacuumGaugeControllerClsLib
{

    public class VacuumGauge : IVacuumGaugeController
    {
        SerialPort PowerControl = new SerialPort();
        private VacuumGaugeControllerConfig _config = null;
        bool coms = false;
        public int PLCadd = 1;

        ModbusRtuHelper modbusRtu = new ModbusRtuHelper();

        public VacuumGauge(VacuumGaugeControllerConfig config)
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
        private int SerialConnect(string com, int baudrate = 38400, int Databits = 8, int Stopbits = 1, int parity = 0, int readTimeout = 1000, int writeTimeout = 1000)
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
            PowerControl.ReadTimeout = readTimeout;
            PowerControl.WriteTimeout = writeTimeout;

            try
            {
                if (!PowerControl.IsOpen)
                {
                    PowerControl.Open();//打开串口
                    coms = true;
                    //loop_back();
                    //PLC_state();
                    modbusRtu.Connect(PowerControl);
                }
                else
                {
                    //PowerControl.Close();//关闭串口
                    //PowerControl.Open();//打开串口
                    coms = true;
                    modbusRtu.Connect(PowerControl);
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
                    modbusRtu.Disconnect();
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
        /// 读取节点信息
        /// </summary>
        /// <param name="Add"></param>
        /// <param name="num"></param>
        private bool PCread(int PLCadd, int Add, int num, ref ushort[] Data0)
        {
            try
            {
                if (PowerControl.IsOpen)
                {
                    #region 新

                    ushort[] Values = modbusRtu.ReadMultipleRegisters((byte)PLCadd, (ushort)Add, (ushort)num);

                    Data0 = Values;

                    return true;

                    #endregion
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
            finally
            {
            }
        }

        /// <summary>
        /// 写入节点信息
        /// </summary>
        /// <param name="Add"></param>
        /// <param name="num"></param>
        private bool PCwrite(int PLCadd, int Add, ushort Data)
        {
            try
            {
                if (PowerControl.IsOpen)
                {


                    modbusRtu.WriteSingleRegister((byte)PLCadd, (ushort)Add, Data);


                    return true;
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
            finally
            {
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

                    System.Threading.Thread.Sleep(100);
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

        public bool ReadVacuum(ref float Vacuum)
        {
            PLCadd = _config.ChannelNumber;
            byte[] BTData = new byte[4];
            bool ret = PCread(PLCadd, (int)VacuumGaugeAdd.Vacuumdegree, 2, ref BTData);
            if(BTData == null || !ret)
            {
                return false;
            }
            // 检查返回数据的长度  
            if (BTData.Length != 4)
            {
                return false;
            }

            // 获取真空度的整数、分数和指数部分  
            int integerPart = BTData[0] - '0'; // 35H = '5'  
            int decimalPart = BTData[1] - '0'; // 30H = '0'  
            int exponentSign = BTData[2] == '-' ? -1 : 1; // 2DH = '-'  
            int exponentPart = BTData[3] - '0'; // 31H = '1'  

            // 计算真空度  
            double vacuumValue = integerPart + decimalPart / 10.0; // 5.0  
            vacuumValue *= Math.Pow(10, exponentSign * exponentPart); // 5.0E-1 
            Vacuum = (float)vacuumValue;

            return true;
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

    public class ModbusRtuHelper
    {
        private IModbusMaster _modbusMaster; // Modbus 主站对象  

        /// <summary>  
        /// 初始化并打开串口连接  
        /// </summary>  
        /// <param name="comPort">串口名称（如 "COM1"）</param>  
        /// <param name="baudRate">波特率（如 9600）</param>  
        public void Connect(SerialPort _serialPort)
        {

            _modbusMaster = ModbusSerialMaster.CreateRtu(_serialPort);

            Console.WriteLine("串口连接成功！");
        }

        /// <summary>  
        /// 关闭串口连接  
        /// </summary>  
        public void Disconnect()
        {

            _modbusMaster = null;
            Console.WriteLine("串口已断开！");
        }

        /// <summary>  
        /// 读取单个保持寄存器（Holding Register）  
        /// </summary>  
        /// <param name="slaveId">从站 ID</param>  
        /// <param name="registerAddress">寄存器地址</param>  
        /// <returns>返回寄存器值</returns>  
        public ushort ReadSingleRegister(byte slaveId, ushort registerAddress)
        {
            ushort[] result = _modbusMaster.ReadHoldingRegisters(slaveId, registerAddress, 1);
            return result[0];
        }

        /// <summary>  
        /// 读取多个保持寄存器（Holding Registers）  
        /// </summary>  
        /// <param name="slaveId">从站 ID</param>  
        /// <param name="startAddress">起始寄存器地址</param>  
        /// <param name="numberOfPoints">读取的寄存器数量</param>  
        /// <returns>返回寄存器值数组</returns>  
        public ushort[] ReadMultipleRegisters(byte slaveId, ushort startAddress, ushort numberOfPoints)
        {
            return _modbusMaster.ReadHoldingRegisters(slaveId, startAddress, numberOfPoints);
        }

        /// <summary>  
        /// 写入单个保持寄存器（Holding Register）  
        /// </summary>  
        /// <param name="slaveId">从站 ID</param>  
        /// <param name="registerAddress">寄存器地址</param>  
        /// <param name="value">要写入的值</param>  
        public void WriteSingleRegister(byte slaveId, ushort registerAddress, ushort value)
        {
            try
            {
                _modbusMaster.WriteSingleRegister(slaveId, registerAddress, value);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("操作超时：从站未响应！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"写入失败：{ex.Message}");
            }

        }

        /// <summary>  
        /// 写入多个保持寄存器（Holding Registers）  
        /// </summary>  
        /// <param name="slaveId">从站 ID</param>  
        /// <param name="startAddress">起始寄存器地址</param>  
        /// <param name="values">要写入的值数组</param>  
        public void WriteMultipleRegisters(byte slaveId, ushort startAddress, ushort[] values)
        {
            _modbusMaster.WriteMultipleRegisters(slaveId, startAddress, values);
        }
    }

}
