using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamometerControllerClsLib
{
    public interface IDynamometerController
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
        /// 建立连接
        /// </summary>
        void Connect(SerialPort serial);
        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnect();

        /// <summary>
        /// 
        /// </summary>
        void Write();

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <returns></returns>
        double ReadValue();

    }
}
