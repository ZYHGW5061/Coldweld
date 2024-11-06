using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.SerialCommunicationClsLib;

namespace PressureSensorControllerClsLib
{
    public interface IPressureSensorController
    {
        /// <summary>
        /// 链接状态
        /// </summary>
        bool IsConnect { get; }
        /// <summary>
        /// 建立连接
        /// </summary>
        void Connect();
        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnect();
        /// <summary>
        /// 写入参数
        /// </summary>
        /// <param name="Add"></param>
        /// <param name="value"></param>
        void Write();
        /// <summary>
        /// 读取参数
        /// </summary>
        /// <param name="Add"></param>
        /// <returns></returns>
        int Read();

        SerialPortController SerialPortEngine
        {
            get;
            set;
        }

    }
}
