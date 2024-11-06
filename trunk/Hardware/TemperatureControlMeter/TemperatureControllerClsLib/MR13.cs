using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureControllerClsLib
{
    class MR13
    {
        public SerialPort MR13COM = new SerialPort();
        public bool coms = false;
        public int PLCadd = 1;
        public int ByteToInt(byte[] bytes)
        {
            try
            {
                string str = Encoding.ASCII.GetString(bytes);
                int result = Convert.ToInt32(str, 16);
                return result;
            }
            catch (Exception ex) { return -1; }

        }
        public int MR13Connect(string com, int baudrate, int Databits = 7, int Stopbits = 1, int parity = 2)
        {
            MR13COM.PortName = com;//设置端口名
            MR13COM.BaudRate = baudrate;//设置波特率
            MR13COM.DataBits = Databits;
            switch (Stopbits)
            {
                case 0:
                    MR13COM.StopBits = StopBits.None;
                    break;
                case 1:
                    MR13COM.StopBits = StopBits.One;
                    break;
                case 2:
                    MR13COM.StopBits = StopBits.Two;
                    break;
            }
            switch (parity)
            {
                case 0:
                    MR13COM.Parity = Parity.None;
                    break;
                case 1:
                    MR13COM.Parity = Parity.Odd;
                    break;
                case 2:
                    MR13COM.Parity = Parity.Even;
                    break;
                case 3:
                    MR13COM.Parity = Parity.Mark;
                    break;
            }

            try
            {
                if (!MR13COM.IsOpen)
                {
                    MR13COM.Open();//打开串口
                    coms = true;
                    MR13COMConnect();
                }
                else
                {
                    MR13COM.Close();//关闭串口
                    MR13COM.Open();//打开串口
                    coms = true;
                    //MR13COMConnect();
                }
            }
            catch (Exception ex)
            {
                coms = false;
                return -1;
            }

            return 0;
        }
        public int MR13Disconnect()
        {
            try
            {
                if (MR13COM.IsOpen)
                {
                    MR13COMDisconnect();
                    MR13COM.Close();//关闭串口
                    coms = false;
                }
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        public int MR13COMConnect()
        {
            try
            {
                if (coms)
                {

                    string Add = "018C";
                    int num = 1;
                    int Data = 1;
                    int length;
                    length = 19;
                    byte[] data = new byte[length];
                    data[0] = 0x02;
                    data[1] = 0x30;
                    data[2] = 0x31;
                    data[1] = Convert.ToByte(48 + (PLCadd % 100) / 10);
                    data[2] = Convert.ToByte(48 + (PLCadd % 10));
                    data[3] = 0x31;

                    data[4] = 0x57;
                    byte[] pack = new byte[4];
                    pack = Encoding.Default.GetBytes(Add);
                    for (int i = 0; i < pack.Length; i++)
                    {
                        data[5 + i] = pack[0 + i];
                    }
                    //data[9] = 0x31;
                    num = num - 1;
                    data[9] = Convert.ToByte(48 + (num % 10));

                    data[10] = 0x00;
                    data[11] = 0x00;
                    data[12] = 0x00;
                    data[13] = 0x00;
                    data[13] = 0x00;
                    int Data1 = (int)Data;
                    string st = Data1.ToString("X4");
                    byte[] ba1 = new byte[4];
                    ba1 = System.Text.ASCIIEncoding.Default.GetBytes(st);
                    data[10] = 0x2C;
                    data[11 + 0] = ba1[0];
                    data[11 + 1] = ba1[1];
                    data[11 + 2] = ba1[2];
                    data[11 + 3] = ba1[3];

                    data[15] = 0x03;
                    byte[] ZZ = LRC(data, length - 3);
                    data[length - 3] = ZZ[0];
                    data[length - 2] = ZZ[1];
                    //data[16] = 0x2C;
                    //data[17] = 0x2C;
                    data[18] = 0x0D;

                    MR13COM.DiscardInBuffer();
                    MR13COM.DiscardOutBuffer();
                    MR13COM.Write(data, 0, length);
                    System.Threading.Thread.Sleep(100);
                    length = MR13COM.BytesToRead;
                    //length = 15;
                    data = new byte[length];
                    MR13COM.Read(data, 0, length);

                    if (data[6] == 0x30)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                    return 0;
            }
            catch
            { return -1; }
        }
        public void MR13COMDisconnect()
        {
            try
            {
                if (coms)
                {

                    string Add = "018C";
                    int num = 1;
                    int Data = 0;
                    int length;
                    length = 19;
                    byte[] data = new byte[length];
                    data[0] = 0x02;
                    data[1] = 0x30;
                    data[2] = 0x31;
                    data[1] = Convert.ToByte(48 + (PLCadd % 100) / 10);
                    data[2] = Convert.ToByte(48 + (PLCadd % 10));
                    data[3] = 0x31;

                    data[4] = 0x57;
                    byte[] pack = new byte[4];
                    pack = Encoding.Default.GetBytes(Add);
                    for (int i = 0; i < pack.Length; i++)
                    {
                        data[5 + i] = pack[0 + i];
                    }
                    //data[9] = 0x31;
                    num = num - 1;
                    data[9] = Convert.ToByte(48 + (num % 10));

                    data[10] = 0x00;
                    data[11] = 0x00;
                    data[12] = 0x00;
                    data[13] = 0x00;
                    data[13] = 0x00;
                    int Data1 = (int)Data;
                    string st = Data1.ToString("X4");
                    byte[] ba1 = new byte[4];
                    ba1 = System.Text.ASCIIEncoding.Default.GetBytes(st);
                    data[10] = 0x2C;
                    data[11 + 0] = ba1[0];
                    data[11 + 1] = ba1[1];
                    data[11 + 2] = ba1[2];
                    data[11 + 3] = ba1[3];

                    data[15] = 0x03;
                    byte[] ZZ = LRC(data, length - 3);
                    data[length - 3] = ZZ[0];
                    data[length - 2] = ZZ[1];
                    //data[16] = 0x2C;
                    //data[17] = 0x2C;
                    data[18] = 0x0D;

                    MR13COM.DiscardInBuffer();
                    MR13COM.DiscardOutBuffer();
                    MR13COM.Write(data, 0, length);
                    System.Threading.Thread.Sleep(100);
                    length = MR13COM.BytesToRead;
                    //length = 15;
                    data = new byte[length];
                    MR13COM.Read(data, 0, length);

                }
            }
            catch
            { }
        }


        public byte[] MR13read(int PLCadd, string Add, int num)
        {
            try
            {
                if (coms)
                {
                    int length;
                    length = 14;
                    byte[] data = new byte[length];
                    data[0] = 0x02;
                    data[1] = 0x30;
                    data[2] = 0x31;
                    data[1] = Convert.ToByte(48 + (PLCadd % 100) / 10);
                    data[2] = Convert.ToByte(48 + (PLCadd % 10));
                    data[3] = 0x31;

                    data[4] = 0x52;
                    byte[] pack = new byte[4];
                    pack = Encoding.Default.GetBytes(Add);
                    for (int i = 0; i < pack.Length; i++)
                    {
                        data[5 + i] = pack[0 + i];
                    }
                    //data[9] = 0x31;
                    num = num - 1;
                    data[9] = Convert.ToByte(48 + (num % 10));

                    data[10] = 0x03;
                    byte[] ZZ = LRC(data, length - 3);
                    data[length - 3] = ZZ[0];
                    data[length - 2] = ZZ[1];
                    //data[11] = 0x2C;
                    //data[12] = 0x2C;
                    data[13] = 0x0D;

                    MR13COM.DiscardInBuffer();
                    MR13COM.DiscardOutBuffer();
                    MR13COM.Write(data, 0, length);
                    System.Threading.Thread.Sleep(100);
                    length = MR13COM.BytesToRead;
                    //length = 15;
                    data = new byte[length];
                    MR13COM.Read(data, 0, length);

                    byte[] Data0 = data.Skip(8).Take(length - 12).ToArray();
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

        public void MR13write(int PLCadd, string Add, int num, int Data)
        {
            try
            {
                if (coms)
                {
                    int length;
                    length = 19;
                    byte[] data = new byte[length];
                    data[0] = 0x02;
                    data[1] = 0x30;
                    data[2] = 0x31;
                    data[1] = Convert.ToByte(48 + (PLCadd % 100) / 10);
                    data[2] = Convert.ToByte(48 + (PLCadd % 10));
                    data[3] = 0x31;

                    data[4] = 0x57;
                    byte[] pack = new byte[4];
                    pack = Encoding.Default.GetBytes(Add);
                    for (int i = 0; i < pack.Length; i++)
                    {
                        data[5 + i] = pack[0 + i];
                    }
                    //data[9] = 0x31;
                    num = num - 1;
                    data[9] = Convert.ToByte(48 + (num % 10));

                    data[10] = 0x00;
                    data[11] = 0x00;
                    data[12] = 0x00;
                    data[13] = 0x00;
                    data[13] = 0x00;
                    int Data1 = (int)Data;
                    string st = Data1.ToString("X4");
                    byte[] ba1 = new byte[4];
                    ba1 = System.Text.ASCIIEncoding.Default.GetBytes(st);
                    data[10] = 0x2C;
                    data[11 + 0] = ba1[0];
                    data[11 + 1] = ba1[1];
                    data[11 + 2] = ba1[2];
                    data[11 + 3] = ba1[3];

                    data[15] = 0x03;
                    byte[] ZZ = LRC(data, length - 3);
                    data[length - 3] = ZZ[0];
                    data[length - 2] = ZZ[1];
                    //data[16] = 0x2C;
                    //data[17] = 0x2C;
                    data[18] = 0x0D;

                    MR13COM.DiscardInBuffer();
                    MR13COM.DiscardOutBuffer();
                    MR13COM.Write(data, 0, length);
                    System.Threading.Thread.Sleep(100);
                    length = MR13COM.BytesToRead;
                    //length = 15;
                    data = new byte[length];
                    MR13COM.Read(data, 0, length);
                }
            }
            catch
            { }
        }
        public byte[] LRC(byte[] data, int L)
        {
            int lrc = 0;
            for (int i = 0; i < L; i++)
            {
                lrc = lrc + (int)(data[i]);
            }
            string st = lrc.ToString("X");

            byte[] pack = new byte[2];
            pack = Encoding.Default.GetBytes(st);//此函数将十进制转为四个字节的十六进制

            byte[] pack2 = pack.Skip(pack.Length - 2).Take(2).ToArray();
            return pack2;
        }

    }

}
