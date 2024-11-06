using ConfigurationClsLib;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WestDragon.Framework.SerialCommunicationClsLib;

namespace LightControllerClsLib
{
    public class LightController:ILightSourceController
    {
        #region File

        public bool IsConnected
        {
            get { return _IsConnected; }
        }

        private static readonly object _lockObj = new object();
        private static volatile LightController _instance = null;
        public static LightController Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new LightController();
                        }
                    }
                }
                return _instance;
            }
        }
        private LightController()
        {
            //AllCamera = new Dictionary<EnumCameraType, CameraController>();
            //Initialize();
        }

        //private static readonly LightControl instance = new LightControl();
        //private LightControl()
        //{
        //}
        //public static LightControl Instance
        //{
        //    get { return instance; }
        //}


        private LEDConfig _ledConfig = null;
        private string _name = string.Empty;
        private object _obj = new object();


        /// <summary>
        /// 亮度参考范围最小值
        /// </summary>
        public float MinIntensity { get; set; }

        /// <summary>
        /// 亮度参考范围最大值
        /// </summary>
        public float MaxIntensity { get; set; }

        public SerialPort LightPort = new SerialPort();
        bool coms = false;
        ~LightController()
        {
            if (coms)
                SerialDisconnect();
        }

        private readonly object lockObject = new object();
        public bool _IsConnected = false;
        private bool _isInitialized = false;

        public LightController(LEDConfig config)
        {
            MinIntensity = config.MinIntensity;
            MaxIntensity = config.MaxIntensity;
            _ledConfig = config;

        }

        public SerialPortController SerialPortEngine
        {
            get { return _serialPortEngine; }
            set { _serialPortEngine = value; }
        }
        /// <summary>
        /// 串口通信
        /// </summary>
        private SerialPortController _serialPortEngine;
        //public SerialPortController UnionSerialPortEngine
        //{
        //    get { return _unionSerialPortEngine; }
        //    set { _unionSerialPortEngine = value}
        //}
        //private SerialPortController _unionSerialPortEngine;



        #endregion

        #region Private Methed


        /// <summary>
        /// 连接光源控制器
        /// </summary>
        /// <param name="com"></param>
        /// <param name="baudrate"></param>
        /// <returns></returns>
        private bool SerialConnect(string com, int baudrate = 19200, int Databits = 8, int Stopbits = 1, int parity = 0)
        {
            LightPort.PortName = com;//设置端口名
            LightPort.BaudRate = baudrate;//设置波特率
            LightPort.DataBits = Databits;
            switch (Stopbits)
            {
                case 0:
                    LightPort.StopBits = StopBits.None;
                    break;
                case 1:
                    LightPort.StopBits = StopBits.One;
                    break;
                case 2:
                    LightPort.StopBits = StopBits.Two;
                    break;
            }
            switch (parity)
            {
                case 0:
                    LightPort.Parity = Parity.None;
                    break;
                case 1:
                    LightPort.Parity = Parity.Odd;
                    break;
                case 2:
                    LightPort.Parity = Parity.Even;
                    break;
                case 3:
                    LightPort.Parity = Parity.Mark;
                    break;
            }

            try
            {
                if (!LightPort.IsOpen)
                {
                    LightPort.Open();//打开串口
                    coms = true;
                }
                else
                {
                    LightPort.Close();//关闭串口
                    LightPort.Open();//打开串口
                    coms = true;
                }
                return true;
            }
            catch
            {
                coms = false;
                return false;
            }
        }

        /// <summary>
        /// 断开光源控制器连接
        /// </summary>
        /// <returns></returns>
        private bool SerialDisconnect()
        {
            try
            {
                if (LightPort.IsOpen)
                {
                    LightPort.Close();//关闭串口
                    coms = false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }


        private byte[] CalculateXOR(byte[] data, int Length)
        {
            byte xor = 0;
            for (int i = 0; i < Length; i++)
            {
                xor ^= data[i];
            }

            int highBytei = (int)((xor >> 4) & 0xFF);
            int lowBytei = (int)(xor & 0x0F);
            string highBytej = highBytei.ToString("X2");
            byte[] highByte = Encoding.ASCII.GetBytes(highBytej);
            string lowBytej = lowBytei.ToString("X2");
            byte[] lowByte = Encoding.ASCII.GetBytes(lowBytej);

            return new byte[] { highByte[1], lowByte[1] };
        }

        #endregion

        //public bool IsConnected;


        public void Initialize()
        {
            lock (_obj)
            {
                if (_isInitialized)
                {
                    return;
                }
                SerialPortConfig serialPortConfig = _ledConfig.SerialCommunicator;
                
                string SerialPort = string.Format($"COM{serialPortConfig.Port}");

                LightConnect(SerialPort, serialPortConfig.BaudRate, serialPortConfig.DataBits, (int)serialPortConfig.Parity, (int)serialPortConfig.StopBits);

            }
        }

        /// <summary>
        /// 连接光源控制器
        /// </summary>
        /// <param name="com">串口号</param>
        /// <param name="baudrate">波特率</param>
        /// <param name="Databits">数据位</param>
        /// <param name="Stopbits">停止位</param>
        /// <param name="parity">校验类型</param>
        /// <returns></returns>
        public bool LightConnect(string com, int baudrate = 19200, int Databits = 8, int Stopbits = 1, int parity = 0)
        {
            try
            {
                _IsConnected = LightPort.IsOpen;

                return SerialConnect(com, baudrate, Databits, Stopbits, parity);

            }
            catch
            {
                return false;
            }
            
        }

        public void Connect()
        {
            try
            {
                SerialPortConfig serialPortConfig = _ledConfig.SerialCommunicator;

                string SerialPort = string.Format($"COM{serialPortConfig.Port}");

                SerialConnect(SerialPort, serialPortConfig.BaudRate, serialPortConfig.DataBits, (int)serialPortConfig.Parity, (int)serialPortConfig.StopBits);

                _IsConnected = LightPort.IsOpen;
            }
            catch
            {
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        public void Disconnect()
        {
            try
            {
                SerialDisconnect();

                _IsConnected = false;
            }
            catch
            {
            }
            
        }

        /// <summary>
        /// 连接状态
        /// </summary>
        /// <returns></returns>
        public bool _Isconnected()
        {
            lock (lockObject)
            {
                try
                {
                    _IsConnected = LightPort.IsOpen;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    _IsConnected = false;
                }
                return IsConnected;
            }
        }


        /// <summary>
        /// 设置第Num光源强度
        /// </summary>
        /// <param name="Num"></param>
        /// <param name="intensity"></param>
        /// <returns></returns>
        public void SetIntensity(float floatValue, int Num)
        {
            try
            {
                if (_Isconnected())
                {
                    int intensity = (int)(floatValue);
                    if (intensity > -1 && intensity < 256)
                    {
                        int length;
                        length = 7;
                        byte[] data = new byte[length];
                        data[0] = 0x53;
                        data[1] = Convert.ToByte(65 + Num);
                        data[2] = 0x30;
                        

                        string hexValue = intensity.ToString("X3");
                        byte[] asciiBytes = Encoding.ASCII.GetBytes(hexValue);
                        data[3] = Convert.ToByte(48 + intensity / 100);
                        data[4] = Convert.ToByte(48 + (intensity % 100) / 10);
                        data[5] = Convert.ToByte(48 + ((intensity % 100) % 10));
                        data[6] = 0x23;

                        //byte[] B = CalculateXOR(data, 7);
                        //data[7] = B[0];
                        //data[8] = B[1];

                        LightPort.DiscardInBuffer();
                        LightPort.DiscardOutBuffer();
                        LightPort.Write(data, 0, length);

                        Thread.Sleep(100);

                        //length = 0;
                        //length = LightPort.BytesToRead;
                        //int I = 0;
                        //while (length > 0)
                        //{
                        //    I++;
                        //    Thread.Sleep(10);
                        //    length = LightPort.BytesToRead;
                        //    if (I > 50)
                        //    {
                        //        break;
                        //    }
                        //}

                        length = 1;
                        byte[] data1 = new byte[length];

                        LightPort.Read(data1, 0, length);

                        if (data1.Length>0)
                        {
                            if (data1[0] == data[1])
                            {
                                //return true;
                            }
                            else
                            {
                                //return false;
                            }
                        }
                        else
                        {
                            //return false;
                        }
                    }
                    else
                    {
                        //return false;
                    }
                }
                else
                {
                    //return false;
                }
            }
            catch
            {
                //return false;
            }
        }

        /// <summary>
        /// 获取第Num光源强度
        /// </summary>
        /// <param name="Num"></param>
        /// <returns>num</returns>
        public float GetIntensity(int Num = 1)
        {
            try
            {
                if (_Isconnected())
                {
                    int length, num = 0;
                    length = 3;
                    byte[] data = new byte[length];
                    data[0] = 0x53;
                    data[1] = Convert.ToByte(65 + Num);
                    data[2] = 0x23;

                    LightPort.DiscardInBuffer();
                    LightPort.DiscardOutBuffer();
                    LightPort.Write(data, 0, length);

                    Thread.Sleep(100);

                    //length = 0;
                    //length = LightPort.BytesToRead;
                    //int I = 0;
                    //while (length > 0)
                    //{
                    //    I++;
                    //    Thread.Sleep(10);
                    //    length = LightPort.BytesToRead;
                    //    if (I>50)
                    //    {
                    //        break;
                    //    }
                    //}

                    
                    length = 5;
                    byte[] data1 = new byte[length];
                    LightPort.Read(data1, 0, length);

                    int N1 = Convert.ToInt16(data1[0]) - 32;
                    int N2 = Convert.ToInt16(data[1]);

                    //int I1 = 0;
                    //while (!(N1 == N2 && (Convert.ToInt16(data1[2]) != 0) && (Convert.ToInt16(data1[3]) != 0) && (Convert.ToInt16(data1[4]) != 0)))
                    //{
                    //    LightPort.Read(data1, 0, length);
                    //    N1 = Convert.ToInt16(data1[0]) - 32;
                    //    N2 = Convert.ToInt16(data[1]);
                    //    I1++;
                    //    Thread.Sleep(10);
                    //    if (I1 > 50)
                    //    {
                    //        break;
                    //    }
                    //}

                    
                    if (N1 == N2 && (Convert.ToInt16(data1[2]) != 0) && (Convert.ToInt16(data1[3]) != 0) && (Convert.ToInt16(data1[4]) != 0))
                    {
                        num = (Convert.ToInt16(data1[2]) - 48) * 100 + (Convert.ToInt16(data1[3]) - 48) * 10 + (Convert.ToInt16(data1[4]) - 48);
                    }
                    else
                    {
                        num = -1;
                    }
                    
                    return num;
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }



    }


    public enum RingLightNum
    {
        SubstrateRingLight = 0,
        WaferRingLight = 1,
        UplookRingLight =  2,
    }

    public enum DirectLightNum
    {
        SubstrateDirectLight = 5,
        WaferDirectLight = 6,
        UplookDirectLight = 3,
    }


}
