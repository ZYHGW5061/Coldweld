using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.SerialCommunicationClsLib;

namespace LightControllerClsLib
{
    /// <summary>
    /// 明场光源控制接口
    /// </summary>
    public interface ILightSourceController
    {
        /// <summary>
        /// 获取当前连接状态
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 亮度参考范围最小值
        /// </summary>
        float MinIntensity { get; set; }

        /// <summary>
        /// 亮度参考范围最大值
        /// </summary>
        float MaxIntensity { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        void Connect();

        /// <summary>
        /// 断开
        /// </summary>
        void Disconnect();

        /// <summary>
        /// 设置光强
        /// </summary>
        /// <param name="floatValue"></param>
        /// <param name="channel">0：默认所有通道</param>
        void SetIntensity(float floatValue, int channel = 0);


        /// <summary>
        /// 获取明场光源强度
        /// </summary>
        /// <param name="ChannelAddr"></param>
        /// <returns>4-10V</returns>
        float GetIntensity(int channel = 1);
        SerialPortController SerialPortEngine
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 光源类型
    /// </summary>
    public enum EnumLightSource
    {
        Bright, Dark
    }
}
