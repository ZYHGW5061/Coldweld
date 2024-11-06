using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.LoggerManagerClsLib;
using WestDragon.Framework.SerialCommunicationClsLib;
using WestDragon.Framework.UtilityHelper;

using static SerialCommunicate.ASCII_Data;//引入DLL
using System.IO.Ports;//检索串口
using System.Text.RegularExpressions;

namespace LightControllerClsLib
{
    public class ZHIYUELightController : ILightSourceController
    {
        private LEDConfig _ledConfig = null;
        private string _name = string.Empty;
        private object _obj = new object();
        /// <summary>
        /// 异常日志记录器
        /// </summary>
        protected IBaseLogger _systemLogger
        {
            get { return LoggerManager.GetHandler().GetFileLogger(GlobalParameterSetting.SYSTEM_DEBUG_LOGGER_ID); }
        }

        /// <summary>
        /// 亮度参考范围最小值
        /// </summary>
        public float MinIntensity { get; set; }

        /// <summary>
        /// 亮度参考范围最大值
        /// </summary>
        public float MaxIntensity { get; set; }
        /// <summary>
        /// 记录模拟连接状态的变量
        /// </summary>
        private bool _isConnected;

        /// <summary>
        /// 记录获取到的光源亮度
        /// </summary>
        private float _brightness = 0.0f;

        /// <summary>
        /// 线程同步的信号变量
        /// </summary>
        private EventWaitHandle _getBrightnessEventWaitHandle = new AutoResetEvent(false);
        /// <summary>
        /// 记录串口接收到的数据信息
        /// </summary>
        private byte[] _receiveBytes;
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

        /// <summary>
        /// 串口信息
        /// </summary>
        private SerialPortConnectionInfo _spConnInfo;
        /// <summary>
        /// 记录串口通信返回的数据
        /// </summary>
        private string _readStr;
        /// <summary>
        /// 重试机制
        /// </summary>
        RetryMechanismOperation _retryMechanismOperation = new RetryMechanismOperation();

        //是否已经初始化
        private bool _isInitialized = false;

        public bool IsConnected
        {
            get { return _isConnected; }
        }

        public ZHIYUELightController(LEDConfig config)
        {
            MinIntensity = config.MinIntensity;
            MaxIntensity = config.MaxIntensity;
            _ledConfig = config;

        }
        /// <summary>
        /// 初始化串口并打开串口
        /// </summary>
        private void Initialize()
        {
            lock (_obj)
            {
                if (_isInitialized)
                {
                    return;
                }
                SerialPortConfig serialPortConfig = _ledConfig.SerialCommunicator;
                _serialPortEngine = new SerialPortController();
                _spConnInfo = new SerialPortConnectionInfo()
                {
                    PortNum = serialPortConfig.Port,
                    BaudRate = serialPortConfig.BaudRate,
                    DataBits = serialPortConfig.DataBits,
                    ParityType = serialPortConfig.Parity,
                    StopBitsType = serialPortConfig.StopBits
                };
                
                //_serialPortEngine.DataReceivedAct += comSerialPort_DataReceived;
                //SerialPortConfig serialPortConfig = _ledConfig.SerialCommunicator;
                //_serialPort = new SerialPortCom(string.Format("COM{0}", serialPortConfig.Port));
                ////波特率
                //_serialPort.BaudRate = serialPortConfig.BaudRate;
                ////数据位
                //_serialPort.DataBits = serialPortConfig.DataBits;
                ////两个停止位
                //_serialPort.StopBits = serialPortConfig.StopBits;
                ////无奇偶校验位
                //_serialPort.Parity = serialPortConfig.Parity;
                ////读超时
                //_serialPort.ReadTimeout = 1000;
                //_serialPort.WriteTimeout = -1;
                //_serialPort.DataReceived += new SerialPortCom.EventHandle(comSerialPort_DataReceived);
                _isInitialized = true;
            }
        }
        public void Connect()
        {
            
            if (_ledConfig.CommunicationType == EnumCommunicationType.Ethernet)
            {
                //var ret = _optControllerAPI.CreateEthernetConnectionByIP(_ledConfig.IPAddress);
                //if (ret != 0)
                //{
                //    throw new Exception(string.Format("{0} Connection failed. Error code: {1}", _ledConfig.ID, ret));
                //}
            }
            else if (_ledConfig.CommunicationType == EnumCommunicationType.SerialPort)
            {
                if (_serialPortEngine != null && _serialPortEngine.IsPortOpened)
                {
                    _isInitialized = true;
                    _isConnected = true;
                }
                else
                {
                    Initialize();

                    if(serialPort!=null && !_isConnected)
                    {
                        //Open_SerialPort($"COM{_spConnInfo.PortNum }", _spConnInfo.BaudRate);

                        //if (serialPort.IsOpen)
                        //{
                        //    ASCII_SET_ON_OFF(1, 1, 20);
                        //}
                        //ASCII_SET_ON_OFF(1, 1, 20);
                    }

                    if (_serialPortEngine != null && !_isConnected)
                    {
                        _serialPortEngine.OpenPort(_spConnInfo);
                    }
                    _isConnected = true;
                }
                //var ret = _optControllerAPI.InitSerialPort(string.Format("COM{0}", _ledConfig.SerialPort));
                //if (ret != 0)
                //{
                //    throw new Exception(string.Format("{0} Connection failed. Error code: {1}", _ledConfig.ID, ret));
                //}
            }
        }

        public void Disconnect()
        {
            int ret = 0;
            if (_ledConfig.CommunicationType == EnumCommunicationType.Ethernet)
            {
                //ret = _optControllerAPI.DestroyEthernetConnect(); 
            }
            else
            {
                for(int i=0;i<8;i++)
                {
                    //string V = ASCII_SET_LEVEL(1, (int)0, i, 20);
                    //string R = Data_received;
                    //Thread.Sleep(100);
                    SetIntensity(0, i);
                    Thread.Sleep(10);
                }
                
                
                if (serialPort.IsOpen)
                {
                    ASCII_SET_ON_OFF(1, 0, 20);
                    serialPort.Close();
                }

                //if (_serialPortEngine.IsPortOpened)
                //{
                //    //_serialPortEngine.DataReceivedAct -= comSerialPort_DataReceived;
                //    _serialPortEngine.ClosePort();
                //}
                _isConnected = false;
            }

            if (ret != 0)
            {
                _systemLogger.AddDebugContent(string.Format("{0} Failed to destroy the connection Error code:{1}", _ledConfig.LightSourceName, ret));
            }
        }
        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        /// <param name="readBuffer"></param>
        private void comSerialPort_DataReceived(byte[] readBuffer)
        {
            _receiveBytes = readBuffer;
            _brightness = ((readBuffer[3]) * 256 + readBuffer[4]) / 100f;
            _getBrightnessEventWaitHandle.Set();
        }
        /// <summary>
        /// 设置光源强度
        /// </summary>
        /// <param name="value">0-255</param>
        public void SetIntensity(float value, int channel=0)
        {
            try
            {

                string sendStr = String.Format("$L{0}={1}#", channel, value);
                _retryMechanismOperation = new RetryMechanismOperation()
                {
                    MaxRetryCount = 50,
                    ProcessFunc = () =>
                    {
                        _readStr += ReadSingleData();
                        if (String.IsNullOrEmpty(_readStr) || !_readStr.Contains("OK"))
                        {
                            System.Threading.Thread.Sleep(100);
                            return false;
                        }
                        return true;
                    }
                };
                _serialPortEngine.SerialPortWrite(sendStr);
                _readStr = "";
                _retryMechanismOperation.Run();
                if (!_retryMechanismOperation.IsSuccess)
                {
                    //throw new EmergencyException("DarkField SetIntensity Failed：" + _readStr);
                    _systemLogger.AddErrorContent("SetIntensity Failed：" + _readStr);
                }
                else
                {
                    _systemLogger.AddInfoContent("SetIntensity Success：" + _readStr);
                }

                //string V = ASCII_SET_LEVEL(1, (int)value, channel, 20);
                //for(int i=0;i<2;i++)
                //{
                //    string V = ASCII_SET_LEVEL(1, (int)value, channel, 20);
                //    Thread.Sleep(100);
                //    string R = Data_received;
                //}                        
            }
            catch (Exception ex)
            {
                _systemLogger.AddErrorContent("SetIntensity Error.",ex);
            }
        }

        /// <summary>
        /// 获取光源强度
        /// </summary>
        /// <param name="channel">指定的通道</param>
        /// <returns></returns>
        public float GetIntensity(int channel = 1)
        {
            var ret = 0f;
            //此处通道数表示：1通道表示只用Channel1，2，表示使用Channel1和Channel2
            if (_ledConfig.CommunicationType == EnumCommunicationType.Ethernet)
            {
                //ret = _optControllerAPI.DestroyEthernetConnect(); 
            }
            else
            {
                //string valuestr = ASCII_READ_ONLY_DAT(1, channel, 20);
                //string pattern = @"\d+";
                //Match match = Regex.Match(valuestr, pattern);

                //if (match.Success)
                //{
                //    int value = int.Parse(match.Value);
                //    Console.WriteLine($"Extracted value: {value}");
                //    ret = value;
                //}
                //else
                //{
                //    Console.WriteLine("No number found in the string.");
                //}
                var valuestr = $"$RD={channel}#";

                _retryMechanismOperation = new RetryMechanismOperation()
                {
                    MaxRetryCount = 50,
                    ProcessFunc = () =>
                    {
                        _readStr += ReadSingleData();
                        if (String.IsNullOrEmpty(_readStr) || !_readStr.Contains("$L"))
                        {
                            System.Threading.Thread.Sleep(100);
                            return false;
                        }
                        return true;
                    }
                };
                _serialPortEngine.SerialPortWrite(valuestr);
                _readStr = "";
                _retryMechanismOperation.Run();
                if (!_retryMechanismOperation.IsSuccess)
                {
                    //throw new EmergencyException("DarkField SetIntensity Failed：" + _readStr);
                    _systemLogger.AddErrorContent("Get Intensity Failed：" + _readStr);
                }
                else
                {
                    string[] subStrings = _readStr.Split(',');
                    var retStr = subStrings[0];
                    var valueStr=retStr.Substring(_readStr.IndexOf('=') + 1);
                    ret = float.Parse(valueStr);
                    _systemLogger.AddInfoContent("Get Intensity Success：" + _readStr);
                }


            }
            return ret;
        }
        public void TurnOffChannel(int channel = 0)
        {
            ////0:turn off all channels
            //var ret = _optControllerAPI.TurnOffChannel(channel);
            //if(ret != 0)
            //{
            //    _systemLogger.AddDebugContent(string.Format("{0} Failed to turn off the all channel. Error Code:{1}", _ledConfig.ID, ret));
            //}
        }

        public void TurnOnChannel(int channel)
        {
            //0:turn off all channels
            //var ret = _optControllerAPI.TurnOnChannel(channel);
            //if (ret != 0)
            //{
            //    _systemLogger.AddDebugContent(string.Format("{0} Failed to turn on the all channel. Error Code:{1}", _ledConfig.ID, ret));
            //}
        }

        public void SetIPAddress()
        {
            //StringBuilder ip = new StringBuilder("192.168.1.17");
            //StringBuilder subnetMask = new StringBuilder("255.255.255.0");
            //StringBuilder defaultGateway = new StringBuilder("192.168.1.1");
            //long lret = _optControllerAPI.SetIPConfiguration(ip, subnetMask, defaultGateway);
            //if (lret == 0)
            //{
            //    _systemLogger.AddDebugContent(_ledConfig.ID + " Set IP configuration successfully.");
            //}
            //else
            //{
            //    _systemLogger.AddDebugContent(_ledConfig.ID + " Set IP configuration failed.");
            //}

        }
        /// <summary>
        /// 读取单次查询的返回数据
        /// </summary>
        /// <returns></returns>
        private string ReadSingleData()
        {
            string dataReceived = "";
            byte[] readData = _serialPortEngine.SerialPortRead();
            if (readData == null || readData.Length == 0)
            {
                return dataReceived;
            }
            dataReceived = System.Text.Encoding.ASCII.GetString(readData);
            return dataReceived;
            //return null;
        }


    }
}
