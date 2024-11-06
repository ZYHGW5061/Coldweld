//using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJoyStickControllerClsLib
{
    public interface IJobStickController
    {
        //delegate void JoystickMoveDelegate(int JoyX, int JoyY);//手柄回调
        //event JoystickMoveDelegate JoystickMoveEvent;//手柄事件

        //delegate void JoystickButtonDelegate(int KeyNum);//按键回调
        //event JoystickButtonDelegate JoystickButtonEvent;//按键事件


        /// <summary>
        /// 建立连接
        /// </summary>
        void Connect();
        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnect();
        void HandleEnable(bool enable);
        //Action<EnumStageSystem> ActStageSystemChanged { get; set; }
        //Action<EnumSystemAxis> ActSystemAxisChanged { get; set; }



    }
}
