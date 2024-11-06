using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WestDragon.Framework.BaseLoggerClsLib;

namespace StageControllerClsLib
{
    public class GoogolCore : IStageCore
    {
        #region 单例模式
        private static volatile GoogolCore _instance;
        private static readonly object _lockObj = new object();
        public static GoogolCore Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new GoogolCore();
                        }
                    }

                }
                return _instance;
            }
        }
        private GoogolCore()
        {

        }
        #endregion
        const short CORE = 1;
        short EcatSts;
        public AStageInfo StageInfo { get; set; }
        public bool IsConnect { get; set; }
        public bool IsHomedone { get; set; }

        public void AbloluteMoveSync(EnumStageAxis axis, double target)
        {
            throw new NotImplementedException();
        }

        public void AbloluteMoveSync(EnumStageAxis[] axises, double[] target)
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            var err=GTN.mc.GTN_Open(5, 2);
                LogRecorder.RecordLog(EnumLogContentType.Debug, $"Connect BoardCard:GTN_Open Result:{err}");
            err = GTN.mc.GTN_TerminateEcatComm(1);
            LogRecorder.RecordLog(EnumLogContentType.Debug, $"Connect BoardCard:GTN_TerminateEcatComm Result:{err}");
            err = GTN.mc.GTN_InitEcatComm(1);
            LogRecorder.RecordLog(EnumLogContentType.Debug, $"Connect BoardCard:GTN_InitEcatComm Result:{err}");
            Thread.Sleep(2000);
            err = GTN.mc.GTN_StartEcatComm(1);
            LogRecorder.RecordLog(EnumLogContentType.Debug, $"Connect BoardCard:GTN_StartEcatComm Result:{err}");
            err = GTN.mc.GTN_IsEcatReady(CORE, out EcatSts);
            LogRecorder.RecordLog(EnumLogContentType.Debug, $"Connect BoardCard:GTN_IsEcatReady Result:{err}");
            while (EcatSts!=1)
            {
                Thread.Sleep(1000);
                err=GTN.mc.GTN_IsEcatReady(CORE, out EcatSts);
                LogRecorder.RecordLog(EnumLogContentType.Debug, $"Connect BoardCard:GTN_IsEcatReady Result:{err}");
            }
        }
        private void StartEcatCommunication()
        {
            short Success;
            try
            {
                var rtn = GTN.mc.GTN_TerminateEcatComm(CORE);
                rtn = GTN.mc.GTN_InitEcatComm(CORE);

                if (rtn != 0) { return; }
                Thread.Sleep(1000);
                do
                {
                    rtn = GTN.mc.GTN_IsEcatReady(CORE, out Success);
                    Thread.Sleep(10);
                } while (Success == 0);
                rtn = GTN.mc.GTN_StartEcatComm(CORE);
            }
            catch (Exception ex)
            {
            }
        }
        public void Disconnect()
        {
            //throw new NotImplementedException();
        }

        public float GetAxisSpeed(EnumStageAxis axis)
        {
            throw new NotImplementedException();
        }

        public double GetCurrentPosition(EnumStageAxis axis)
        {
            throw new NotImplementedException();
        }

        public double[] GetCurrentStagePosition()
        {
            throw new NotImplementedException();
        }

        public void JogNegative(EnumStageAxis axis, float jogSpeed = 0)
        {
            AxisControl.mc.MC_MoveJog((short)axis, 1d, 1d, 0.1d, (double)-jogSpeed);
        }

        public void JogPositive(EnumStageAxis axis, float jogSpeed = 0)
        {
            AxisControl.mc.MC_MoveJog((short)axis, 1d, 1d, 0.1d, (double)jogSpeed);
        }
        public void StopJogNegative(EnumStageAxis axis)
        {
            GTN.mc.GTN_Stop(CORE, 1 << (short)axis - 1, 1 << (short)axis - 1);
        }

        public void StopJogPositive(EnumStageAxis axis)
        {
            GTN.mc.GTN_Stop(CORE, 1 << (short)axis - 1, 1 << (short)axis - 1);
        }
        public void RelativeMoveSync(EnumStageAxis axis, float distance)
        {
            WaitRelativeMoveDone(axis);
        }

        public void RelativeMoveSync(EnumStageAxis[] axises, double[] distance)
        {
            throw new NotImplementedException();
        }

        public void SetAxisSpeed(EnumStageAxis axis, float speed)
        {
            throw new NotImplementedException();
        }



        public void WaitAbsoluteMoveDone(EnumStageAxis axis, int timeout = 60000)
        {
            bool isEnd = false;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (!isEnd)
            {
                var ret = !AxisControl.mc.AxisSts((short)axis, 10);//规划运动:true-正在运动
                if (ret)
                {
                    isEnd = true;
                    break;
                }
                if (sw.ElapsedMilliseconds > timeout)
                {
                    sw.Stop();
                    isEnd = true;
                    break;
                }
                Thread.Sleep(50);
            }
        }

        public void WaitAbsoluteMoveDone(EnumStageAxis[] axis)
        {
            throw new NotImplementedException();
        }

        public void WaitRelativeMoveDone(EnumStageAxis axis, int timeout = 60000)
        {
            bool isEnd = false;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (!isEnd)
            {
                var ret = !AxisControl.mc.AxisSts((short)axis, 10);//规划运动:true-正在运动
                if (ret)
                {
                    isEnd = true;
                    break;
                }
                if (sw.ElapsedMilliseconds > timeout)
                {
                    sw.Stop();
                    isEnd = true;
                    break;
                }
                Thread.Sleep(50);
            }
        }

        public void WaitRelativeMoveDone(EnumStageAxis[] axis)
        {
            throw new NotImplementedException();
        }
        public void Enable(EnumStageAxis axis)
        {
            var ss = (short)axis;
            //var rtn = GTN.mc.GTN_SetEcatAxisOnThreshold(CORE, (short)axis, 5000);
            var rtn = 0;
            if (AxisControl.mc.AxisSts((short)axis, 9))
            {
                //rtn = GTN.mc.GTN_AxisOff(CORE, MAxis);
            }
            else
            {
                //rtn = GTN.mc.GT_AlarmOff((short)axis);

                rtn = GTN.mc.GTN_AxisOn(CORE, (short)axis);
                LogRecorder.RecordLog(EnumLogContentType.Debug, $"Enable Axis Result:{rtn}");
            }
        }
        public void Disable(EnumStageAxis axis)
        {
            var rtn = 0;// GTN.mc.GTN_SetEcatAxisOnThreshold(1, (short)axis, 5000);
            if (AxisControl.mc.AxisSts((short)axis, 9))
            {
                rtn = GTN.mc.GTN_AxisOff(CORE, (short)axis);
                LogRecorder.RecordLog(EnumLogContentType.Debug, $"Disable Axis Result:{rtn}");
            }
            else
            {
                //rtn = GTN.mc.GTN_AxisOn(CORE, (short)axis);
            }
        }
    }
}
