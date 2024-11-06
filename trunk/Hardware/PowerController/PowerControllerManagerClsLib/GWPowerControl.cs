using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;

namespace PowerControllerManagerClsLib
{
    

    public class GWPowerControl: IPowerController
    {
        SerialPort PowerControl = new SerialPort();
        private PowerControllerConfig _config = null;
        bool coms = false;
        public int PLCadd = 1;

        public bool IsConnect { get { return PowerControl.IsOpen; } }
        public GWPowerControl(PowerControllerConfig config)
        {
            _config = config;
        }

        private int ByteToInt(byte[] BTData)
        {
            try
            {
                if (BTData!=null)
                {
                    int offset = BTData.Length;
                    int Data = 0;
                    for (int i=0;i<offset;i++)
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
            catch(Exception ex) { return -1; }
            
        }
        private static byte[] CRC16(byte[] value,int Length, ushort poly = 0xA001, ushort crcInit = 0xFFFF)
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
            buffer[0]=lo;//低字节
            buffer[1]=hi;//高字节

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
                    PowerControl.Close();//关闭串口
                    PowerControl.Open();//打开串口
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
                    byte[] data1 = CRC16(data,6);

                    data[6]=data1[0];
                    data[7]=data1[1];


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
                    return ;
                }
            }
            catch
            {
                return ;
            }
        }

        public void 电源_连接(string com, int baudrate = 38400, int Databits = 8, int Stopbits = 1, int parity = 0)
        {
            SerialConnect(com,baudrate,Databits,Stopbits,parity);
        }
        public void 电源_断开()
        {
            SerialDisconnect();
        }
        public int[] 电源_读所有参数()
        {
            int[] Data = new int[12];
            Data[0] = 电源_读I1热量();
            Data[1] = 电源_读I2热量();
            Data[2] = 电源_读I3热量();
            Data[3] = 电源_读I4热量();
            Data[4] = 电源_读n1上升脉冲();
            Data[5] = 电源_读n2稳定1脉冲();
            Data[6] = 电源_读n3下降1脉冲();
            Data[7] = 电源_读n4下降2脉冲();
            Data[8] = 电源_读t1预压时间();
            Data[9] = 电源_读th脉冲加热时间();
            Data[10] = 电源_读tc脉冲间隔时间();
            Data[11] = 电源_读t2保持时间();
            return Data;
        }
        public int 电源_读I1热量()
        {
            byte[] BTData = PCread(PLCadd, 0, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写I1热量(int Data)
        {
            PCwrite(PLCadd, 0, Data);
        }
        public int 电源_读I2热量()
        {
            byte[] BTData = PCread(PLCadd, 1, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写I2热量(int Data)
        {
            PCwrite(PLCadd, 1, Data);
        }
        public int 电源_读I3热量()
        {
            byte[] BTData = PCread(PLCadd, 2, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写I3热量(int Data)
        {
            PCwrite(PLCadd, 2, Data);
        }
        public int 电源_读I4热量()
        {
            byte[] BTData = PCread(PLCadd, 3, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写I4热量(int Data)
        {
            PCwrite(PLCadd, 3, Data);
        }
        public int 电源_读n1上升脉冲()
        {
            byte[] BTData = PCread(PLCadd, 4, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写n1上升脉冲(int Data)
        {
            PCwrite(PLCadd, 4, Data);
        }
        public int 电源_读n2稳定1脉冲()
        {
            byte[] BTData = PCread(PLCadd, 5, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写n2稳定1脉冲(int Data)
        {
            PCwrite(PLCadd, 5, Data);
        }
        public int 电源_读n3下降1脉冲()
        {
            byte[] BTData = PCread(PLCadd, 6, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写n3下降1脉冲(int Data)
        {
            PCwrite(PLCadd, 6, Data);
        }
        public int 电源_读n4下降2脉冲()
        {
            byte[] BTData = PCread(PLCadd, 7, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写n4下降2脉冲(int Data)
        {
            PCwrite(PLCadd, 7, Data);
        }
        public int 电源_读t1预压时间()
        {
            byte[] BTData = PCread(PLCadd, 8, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写t1预压时间(int Data)
        {
            PCwrite(PLCadd, 8, Data);
        }
        public int 电源_读th脉冲加热时间()
        {
            byte[] BTData = PCread(PLCadd, 9, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写th脉冲加热时间(int Data)
        {
            PCwrite(PLCadd, 9, Data);
        }
        public int 电源_读tc脉冲间隔时间()
        {
            byte[] BTData = PCread(PLCadd, 10, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写tc脉冲间隔时间(int Data)
        {
            PCwrite(PLCadd, 10, Data);
        }
        public int 电源_读t2保持时间()
        {
            byte[] BTData = PCread(PLCadd, 11, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写t2保持时间(int Data)
        {
            PCwrite(PLCadd, 11, Data);
        }
        public int 电源_读gp参数组()
        {
            byte[] BTData = PCread(PLCadd, 36, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写gp参数组(int Data)
        {
            PCwrite(PLCadd, 36, Data);
        }
        public int 电源_读电源38()
        {
            byte[] BTData = PCread(PLCadd, 40, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写电源38(int Data)
        {
            PCwrite(PLCadd, 40, Data);
        }
        public int 电源_读电源39()
        {
            byte[] BTData = PCread(PLCadd, 41, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写电源39(int Data)
        {
            PCwrite(PLCadd, 41, Data);
        }
        public int 电源_读电源40()
        {
            byte[] BTData = PCread(PLCadd, 42, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写电源40(int Data)
        {
            PCwrite(PLCadd, 42, Data);
        }
        public int 电源_读电源41()
        {
            byte[] BTData = PCread(PLCadd, 43, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写电源41(int Data)
        {
            PCwrite(PLCadd, 43, Data);
        }
        public int 电源_读电源42()
        {
            byte[] BTData = PCread(PLCadd, 44, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public void 电源_写电源42(int Data)
        {
            PCwrite(PLCadd, 44, Data);
        }
        public int 电源_读电源43()
        {
            byte[] BTData = PCread(PLCadd, 45, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public int 电源_读电源44()
        {
            byte[] BTData = PCread(PLCadd, 46, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public int 电源_读电源45()
        {
            byte[] BTData = PCread(PLCadd, 47, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public int 电源_读电源46()
        {
            byte[] BTData = PCread(PLCadd, 48, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public int 电源_读电源47()
        {
            byte[] BTData = PCread(PLCadd, 49, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }
        public int 电源_读电源48()
        {
            byte[] BTData = PCread(PLCadd, 50, 1);
            int Data = ByteToInt(BTData);
            return Data;
        }

        public void Connect()
        {
            SerialConnect($"COM{_config.SerialCommunicator.Port}", _config.SerialCommunicator.BaudRate
                , _config.SerialCommunicator.DataBits, (int)_config.SerialCommunicator.StopBits, (int)_config.SerialCommunicator.Parity);
        }

        public void Disconnect()
        {
            SerialDisconnect();
        }

        public void Write(PowerAdd Add, int value)
        {
            throw new NotImplementedException();
        }

        public int Read(PowerAdd Add)
        {
            throw new NotImplementedException();
        }

        public PowerParam ReadAll()
        {
            throw new NotImplementedException();
        }

        public void WriteAll(PowerParam param)
        {
            throw new NotImplementedException();
        }
    }
}
