using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.SerialCommunicationClsLib;

namespace LightControllerClsLib
{
    public class SimulatedLightController : ILightSourceController
    {
        public SerialPortController SerialPortEngine
        {
            get;
            set;
        }
        /// <summary>
        /// 记录模拟连接状态的变量
        /// </summary>
        bool _isConnected;

        /// <summary>
        /// 模拟明场光源的亮度
        /// </summary>
        float _brightFieldIntensity = 4f;

        /// <summary>
        /// 获取是否已经连接到该设备
        /// </summary>
        public bool IsConnected
        {
            get { return _isConnected; }
        }

        /// <summary>
        /// 连接
        /// </summary>
        public void Connect()
        {
            _isConnected = true;
        }

        /// <summary>
        /// 断开
        /// </summary>
        public void Disconnect()
        {
            _isConnected = false;
        }

        /// <summary>
        /// 设置明场光源强度
        /// </summary>
        /// <param name="floatValue">4-10V</param>
        /// <param name="ChannelAddr"></param>
        public void SetIntensity(float floatValue, int channel = 0)
        {
            _brightFieldIntensity = floatValue;
        }

        /// <summary>
        /// 获取明场光源强度
        /// </summary>
        /// <param name="ChannelAddr"></param>
        /// <returns>4-10V</returns>
        public float GetIntensity(int channel = 0)
        {
            return _brightFieldIntensity;
        }

        /// <summary>
        /// 亮度参考范围最小值
        /// </summary>
        public float MinIntensity { get; set; }

        /// <summary>
        /// 亮度参考范围最大值
        /// </summary>
        public float MaxIntensity { get; set; }
    }
}
