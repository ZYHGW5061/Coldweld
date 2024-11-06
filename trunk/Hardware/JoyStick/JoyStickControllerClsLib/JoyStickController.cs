
using IJoyStickControllerClsLib;
//using PositioningSystemClsLib;
//using PLCControllerManagerClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JoyStickControllerClsLib
{
    public class JoyStickController : IJobStickController
    {
        #region File

        bool HeadMode = true;// true 机头模式 false 传送模式

        JoystickObj _joystickOBJ = null;
        int Mode = 1;//轴组
        bool YGMode = false;//摇杆
        public float Lowspeed = 3;//低速
        public float Highspeed = 20;//高速

       


        public delegate void JoystickMoveDelegate(int JoyX, int JoyY);//手柄回调
        public event JoystickMoveDelegate JoystickMoveEvent;//手柄事件

        public delegate void JoystickButtonDelegate(int KeyNum);//按键回调
        public event JoystickButtonDelegate JoystickButtonEvent;//按键事件

        #endregion

        private string _name;
        public JoyStickController(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 连接手柄
        /// </summary>
        /// <param name="Name">手柄名称</param>
        public void HandleConnect(string Name)
        {
            StartJoystickObj("Controller (BEITONG A1T XINPUT GAMEPAD)");

            _joystickOBJ.Enable = true;//初始化
            _joystickOBJ.Enable1 = false;//使能
        }

        /// <summary>
        /// 控制手柄使能
        /// </summary>
        /// <param name="enable">true 响应手柄  false 禁用手柄</param>
        public void HandleEnable(bool enable)
        {
            _joystickOBJ.Enable1 = enable;

            
        }

        /// <summary>
        /// 手柄初始化
        /// </summary>
        /// <param name="Name">手柄名称</param>
        /// <returns></returns>
        private bool StartJoystickObj(string Name)
        {
            try
            {
                _joystickOBJ = new JoystickObj(Name);
                _joystickOBJ.Connect();
                _joystickOBJ.JoystickMoveEvent += _joystickOBJ_JoystickMoveEvent;
                _joystickOBJ.JoystickButtonEvent += _joystickOBJ_JoystickButtonEvent;

            }
            catch (Exception ex)
            {

            }
            return false;
        }

        /// <summary>
        /// 手柄摇杆响应事件
        /// </summary>
        /// <param name="JoyX">左右</param>
        /// <param name="JoyY">上下</param>
        private void _joystickOBJ_JoystickMoveEvent(int JoyX, int JoyY)
        {
            try
            {
                JoystickMoveEvent?.Invoke(JoyX, JoyY);

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 手柄按键响应事件
        /// </summary>
        /// <param name="KeyNum">1-12</param>
        private void _joystickOBJ_JoystickButtonEvent(int KeyNum)
        {
            try
            {
                JoystickButtonEvent?.Invoke(KeyNum);



            }



            catch (Exception ex)
            {

            }
        }

        public void Connect()
        {
            try
            {
                _joystickOBJ = new JoystickObj(_name);
                _joystickOBJ.Connect();
                _joystickOBJ.JoystickMoveEvent += _joystickOBJ_JoystickMoveEvent;
                _joystickOBJ.JoystickButtonEvent += _joystickOBJ_JoystickButtonEvent;
                _joystickOBJ.Enable = true;//初始化
                _joystickOBJ.Enable1 = false;//使能

            }
            catch (Exception ex)
            {

            }
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
