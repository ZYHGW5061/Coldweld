using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IJoyStickControllerClsLib;
//using PLCControllerManagerClsLib;
using SharpDX.DirectInput;


namespace JoyStickControllerClsLib
{
    public class JoystickObj
    {
        #region File

        CancellationTokenSource cts = new CancellationTokenSource();
        public bool Enable = false;//初始化
        public bool Enable1 = false;//使能

        public delegate void JoystickMoveDelegate(int JoyX, int JoyY);//手柄回调
        public event JoystickMoveDelegate JoystickMoveEvent;//手柄事件

        public delegate void JoystickButtonDelegate(int KeyNum);//按键回调
        public event JoystickButtonDelegate JoystickButtonEvent;//按键事件

        public delegate void JoystickConnectStateDelegate();
        public event JoystickConnectStateDelegate JoystickConnectEvent;//连接事件
        public event JoystickConnectStateDelegate JoystickDisConnectEvent;//断开事件
                                                                          //public Action<int, int> JoystickMoveEvent;
                                                                          //public Action<int> JoystickButtonEvent;
                                                                          //public Action JoystickConnectEvent;
                                                                          //public Action JoystickDisConnectEvent;

        public bool IsFocusZoomButtonDown = false;

        private DirectInput dirInput;
        private SharpDX.DirectInput.Joystick curJoystick;
        public bool isAcquire = false;
        private int JoystickX = 0;
        private int JoystickY = 0;
        private int JoystickB = 0;
        private string jsName = string.Empty;//手柄名称

        #endregion



        public JoystickObj(string Name)
        {
            jsName = Name;//手柄名称
            dirInput = new DirectInput();
        }

        public void Connect()
        {
            Task.Run(new Action(JoystickAcquire));
        }
        public void Disconnect()
        { }
        public void JoystickAcquire()
        {
            while (true)
            {
                try
                {
                    if (Enable == false)
                    {
                        return;
                    }
                    if (!isAcquire)
                    {
                        IList<DeviceInstance> allDevices = dirInput.GetDevices();
                        curJoystick = new SharpDX.DirectInput.Joystick(dirInput, allDevices.First(name => name.ProductName == jsName).InstanceGuid);
                        curJoystick.Acquire();
                        Task.Run(new Action(JoystickTask));
                        isAcquire = true;
                        JoystickConnectEvent?.Invoke();
                    }
                }
                catch
                {
                    isAcquire = false;
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        public void JoystickTask()
        {
            try
            {
                while (true)
                {
                    if (Enable1)
                    {
                        JoystickState joys = curJoystick.GetCurrentState();

                        if (joys.Buttons.Count(B => B == true) > 0)
                        {
                            for (int Key = 0; Key < 30; Key++)
                            {
                                if (joys.Buttons[Key])
                                {
                                    if (JoystickB != Key + 1)
                                    {
                                        JoystickB = Key + 1;
                                        JoystickButtonEvent?.Invoke(JoystickB);
                                        break;
                                    }
                                    else { break; }
                                }
                            }
                            IsFocusZoomButtonDown = true;
                        }
                        else
                        {
                            JoystickB = 0;
                            if (IsFocusZoomButtonDown)
                            {
                                JoystickButtonEvent?.Invoke(JoystickB);
                                IsFocusZoomButtonDown = false;
                            }
                        }

                        if (joys.X - 32767 != JoystickX || joys.Y - 32767 != JoystickY)
                        {
                            JoystickX = joys.X - 32767;
                            JoystickY = joys.Y - 32767;
                            JoystickMoveEvent?.Invoke(joys.X, joys.Y);
                        }
                        System.Threading.Thread.Sleep(50);
                    }

                }
            }
            catch
            {
                isAcquire = false;
                JoystickDisConnectEvent?.Invoke();
            }
        }
    }




}
