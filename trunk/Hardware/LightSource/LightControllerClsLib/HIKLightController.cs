using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.LoggerManagerClsLib;
using WestDragon.Framework.SerialCommunicationClsLib;
using WestDragon.Framework.UtilityHelper;

namespace LightControllerClsLib
{
    /// <summary>
    /// 海康光源控制器
    /// </summary>
    public class HIKLightController : ILightSourceController
    {
        private LEDConfig _ledConfig = null;
        private string _name = string.Empty;
        private object _obj = new object();
        /// <summary>
        /// 异常日志记录器
        /// </summary>
        protected IBaseLogger _systemLogger
        {
            get { return LoggerManager.GetHandler().GetFileLogger(GlobalParameterSetting.SYSTEM_DEBUG_LOGGER_ID);}
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

        public HIKLightController(LEDConfig config)
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
            if(_ledConfig.CommunicationType==EnumCommunicationType.Ethernet)
            {
                //var ret = _optControllerAPI.CreateEthernetConnectionByIP(_ledConfig.IPAddress);
                //if (ret != 0)
                //{
                //    throw new Exception(string.Format("{0} Connection failed. Error code: {1}", _ledConfig.ID, ret));
                //}
            }
            else if (_ledConfig.CommunicationType == EnumCommunicationType.SerialPort)
            {
                if (_serialPortEngine != null&& _serialPortEngine.IsPortOpened)
                {
                    _isInitialized = true;
                    _isConnected = true;
                }
                else
                {
                    Initialize();
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
                if (_serialPortEngine.IsPortOpened)
                {
                    //_serialPortEngine.DataReceivedAct -= comSerialPort_DataReceived;
                    _serialPortEngine.ClosePort();
                }
                _isConnected = false;
            }
            
            if (ret != 0)
            {
                _systemLogger.AddDebugContent(string.Format("{0} Failed to destroy the connection Error code:{1}",_ledConfig.LightSourceName, ret));
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
        /// <param name="value">0-1023</param>
        public void SetIntensity(float value, int channel = 0)
        {
            //此处通道数表示：1通道表示只用Channel1，2，表示使用Channel1和Channel2
            if (_ledConfig.CommunicationType == EnumCommunicationType.Ethernet)
            {
                //ret = _optControllerAPI.DestroyEthernetConnect(); 
            }
            else
            {
                channel = _ledConfig.ChannelNumber;
                var channelStr = "";
                switch (channel)
                {
                    case 1:
                        channelStr = "A";
                        break;
                    case 2:
                        channelStr = "B";
                        break;
                    case 3:
                        channelStr = "C";
                        break;
                    case 4:
                        channelStr = "D";
                        break;
                    default:
                        channelStr = "A";
                        break;
                }
                string sendStr = String.Format("S{0}{1}#", channelStr, value.ToString("0000"));
                _retryMechanismOperation = new RetryMechanismOperation()
                {
                    MaxRetryCount = 50,
                    ProcessFunc = () =>
                    {
                        _readStr += ReadSingleData();
                        if (String.IsNullOrEmpty(_readStr) || !_readStr.Contains(channelStr))
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
                    _systemLogger.AddErrorContent("DarkField SetIntensity Failed：" + _readStr);
                }
                else
                {
                    _systemLogger.AddInfoContent("DarkField SetIntensity Success：" + _readStr);
                }
            }
        }

       /// <summary>
       /// 获取光源强度
       /// </summary>
       /// <param name="channel">指定的通道</param>
       /// <returns></returns>
        public float GetIntensity(int channel = 1)
        {
            var ret = 0;
            //此处通道数表示：1通道表示只用Channel1，2，表示使用Channel1和Channel2
            if (_ledConfig.CommunicationType == EnumCommunicationType.Ethernet)
            {
                //ret = _optControllerAPI.DestroyEthernetConnect(); 
            }
            else
            {
                channel = _ledConfig.ChannelNumber;
                var channelStr = "";
                switch (channel)
                {
                    case 1:
                        channelStr = "A";
                        break;
                    case 2:
                        channelStr = "B";
                        break;
                    case 3:
                        channelStr = "C";
                        break;
                    case 4:
                        channelStr = "D";
                        break;
                    default:
                        channelStr = "Z";
                        break;
                }
                string sendStr = String.Format("S{0}#", channelStr);
                _retryMechanismOperation = new RetryMechanismOperation()
                {
                    MaxRetryCount = 50,
                    ProcessFunc = () =>
                    {
                        _readStr += ReadSingleData();
                        if (String.IsNullOrEmpty(_readStr) || !_readStr.Contains(channelStr.ToLower()))
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
                    throw new EmergencyException("DarkField SetIntensity Failed：" + _readStr);
                }
                else
                {
                    ret = Int32.Parse(_readStr.Substring(1));
                    _systemLogger.AddInfoContent("DarkField SetIntensity Success：" + _readStr);
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
        }
    }
}
