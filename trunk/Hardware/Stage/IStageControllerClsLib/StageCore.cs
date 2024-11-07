using BoardCardControllerClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using IOUtilityClsLib;
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
    public class StageCore
    {
        #region 单例模式
        private static volatile StageCore _instance;
        private static readonly object _lockObj = new object();
        private bool _enablePollingAxis;
        public static StageCore Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new StageCore();
                        }
                    }

                }
                return _instance;
            }
        }
        private StageCore()
        {

        }
        #endregion
        const short CORE = 1;
        short EcatSts;
        public AStageInfo StageInfo { get; set; }
        public bool IsConnect { get { return _boardCardController.IsConnect; } }
        public bool IsHomedone { get; set; }
        IBoardCardController _boardCardController;
        public void AbloluteMoveSync(EnumStageAxis axis, double target)
        {
            if(_boardCardController != null)
            {
                _boardCardController.MoveAbsoluteSync(axis, target,10);
                WaitAbsoluteMoveDone(axis);
            }
        }

        public void AbloluteMoveSync(EnumStageAxis[] axises, double[] target)
        {
            if (_boardCardController != null)
            {
                for (int i = 0; i < axises.Length; i++)
                {
                    _boardCardController.MoveAbsoluteSync(axises[i], target[i], 10);
                }
                for (int i = 0; i < axises.Length; i++)
                {
                    WaitAbsoluteMoveDone(axises[i]);
                }
            }
        }

        public void Connect()
        {
            BoardCardManager.Instance.Initialize();
            //IOUtilityHelper.Instance.Start();
            _boardCardController = BoardCardManager.Instance.GetCurrentController();
        }

        public void Disconnect()
        {
            if (_boardCardController != null)
            {
                _boardCardController.Disconnect();
            }
        }

        public float GetAxisSpeed(EnumStageAxis axis)
        {
            var ret = 0d;
            if (_boardCardController != null)
            {
                ret = _boardCardController.GetAxisSpeed(axis);
            }
            return (float)ret;
        }

        public double GetCurrentPosition(EnumStageAxis axis)
        {
            var ret = 0d;
            if (_boardCardController != null)
            {
                ret = _boardCardController.GetCurrentPosition(axis);
            }
            return ret;
        }

        public double[] GetCurrentStagePosition()
        {
            throw new NotImplementedException();
        }

        public void JogNegative(EnumStageAxis axis, float jogSpeed = 0)
        {
            //AxisControl.mc.MC_MoveJog((short)axis, 1d, 1d, 0.1d, (double)-jogSpeed);
            if (_boardCardController != null)
            {
                _boardCardController.JogNegative(axis,jogSpeed);
            }
        }

        public void JogPositive(EnumStageAxis axis, float jogSpeed = 0)
        {
            //AxisControl.mc.MC_MoveJog((short)axis, 1d, 1d, 0.1d, (double)jogSpeed);
            if (_boardCardController != null)
            {
                _boardCardController.JogPositive(axis, jogSpeed);
            }
        }
        public void StopJogNegative(EnumStageAxis axis)
        {
            //GTN.mc.GTN_Stop(CORE, 1 << (short)axis - 1, 1 << (short)axis - 1);
            if (_boardCardController != null)
            {
                _boardCardController.StopJogNegative(axis);
            }
        }

        public void StopJogPositive(EnumStageAxis axis)
        {
            //GTN.mc.GTN_Stop(CORE, 1 << (short)axis - 1, 1 << (short)axis - 1);
            if (_boardCardController != null)
            {
                _boardCardController.StopJogPositive(axis);
            }
        }

        public void Stop(EnumStageAxis axis)
        {
            if (_boardCardController != null)
            {
                _boardCardController.StopMotion(axis);
            }
        }

        public void RelativeMoveSync(EnumStageAxis axis, float distance)
        {
            if (_boardCardController != null)
            {
                _boardCardController.MoveRelativeSync(axis, distance, 10);
                WaitRelativeMoveDone(axis);
            }
        }

        public void RelativeMoveSync(EnumStageAxis[] axises, double[] distance)
        {

        }

        public void SetAxisSpeed(EnumStageAxis axis, float speed)
        {
            if (_boardCardController != null)
            {
                _boardCardController.SetAxisSpeed(axis,speed);
            }
        }
        public void SetAxisMotionParameters(AxisConfig axisParam)
        {
            if (_boardCardController != null)
            {
                _boardCardController.SetAxisMotionParameters(axisParam);
            }
        }


        public void WaitAbsoluteMoveDone(EnumStageAxis axis, int timeout = 60000)
        {
            if (_boardCardController != null)
            {
                bool isEnd = false;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (!isEnd)
                {
                    var ret = _boardCardController.Get_AxisSts_PosDone(axis);//true-Done
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
        }

        public void WaitAbsoluteMoveDone(EnumStageAxis[] axis)
        {
            //throw new NotImplementedException();
        }

        public void WaitRelativeMoveDone(EnumStageAxis axis, int timeout = 60000)
        {
            if (_boardCardController != null)
            {
                bool isEnd = false;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (!isEnd)
                {
                    var ret = _boardCardController.Get_AxisSts_PosDone(axis);//true-Done
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
        }

        public void WaitRelativeMoveDone(EnumStageAxis[] axis)
        {
            //throw new NotImplementedException();
        }
        public void Enable(EnumStageAxis axis)
        {
            if (_boardCardController != null)
            {
                _boardCardController.Enable(axis);
            }
        }
        public void Disable(EnumStageAxis axis)
        {
            if (_boardCardController != null)
            {
                _boardCardController.Disable(axis);
            }
        }
        /// <summary>
        /// 设置轴正负软限位
        /// </summary>
        public void SetSoftLeftAndRightLimit(EnumStageAxis axis, double Pvalue, double Nvalue)
        {
            if (_boardCardController != null)
            {
                _boardCardController.SetSoftLeftAndRightLimit(axis,Pvalue, Nvalue);
            }

        }
        public double GetSoftLeftLimit(EnumStageAxis axis)
        {
            var ret = 0d;
            if (_boardCardController != null)
            {
                ret=_boardCardController.GetSoftLeftLimit(axis);
            }
            return ret;
        }
        public double GetSoftRightLimit(EnumStageAxis axis)
        {
            var ret = 0d;
            if (_boardCardController != null)
            {
                ret = _boardCardController.GetSoftRightLimit(axis);
            }
            return ret;
        }
        /// <summary>
        /// 取消轴正负软限位
        /// </summary>
        public void CloseSoftLeftAndRightLimit(EnumStageAxis axis)
        {
            if (_boardCardController != null)
            {
                _boardCardController.CloseSoftLeftAndRightLimit(axis);
            }

        }
        /// <summary>
        /// 设置轴当前位置为原点
        /// </summary>
        public bool Home(EnumStageAxis axis)
        {
            if (_boardCardController != null)
            {
                return _boardCardController.Home(axis);
            }
            else
                return false;
        }

        /// <summary>
        /// 读取轴状态
        /// 1 报警
        /// 5 正限位
        /// 6 负限位 
        /// 7 平滑停止 
        /// 8 急停 
        /// 9 使能 
        /// 10 规划运动 
        /// 11 电机到位
        /// </summary>
        public int GetAxisState(EnumStageAxis axis)
        {
            if (_boardCardController != null)
            {
                return _boardCardController.GetAxisState(axis);
            }
            return 0;
        }

        /// <summary>
        /// 报警清除
        /// </summary>
        public void ClrAlarm(EnumStageAxis axis)
        {
            _boardCardController.ClrAlarm(axis);
        }

        /// <summary>
        /// 报警 / 限位无效
        /// action动作 ： 1 为生效，0为失效
        /// </summary>
        public void DisableAlarmLimit(EnumStageAxis axis)
        {
            _boardCardController.DisableAlarmLimit(axis);
        }
        public void StartReadAxisPositionTask()
        {
            _enablePollingAxis = true;
            Task.Run(new Action(ReadAxisPositionTask));
        }
        private void ReadAxisPositionTask()
        {
            //while (_enablePollingIO)
            //{
            //    Thread.Sleep(500);
            //    List<int> data = new List<int>(); ;
            //    _boardCardController.IO_ReadAll(11, out data);
            //    if (data.Count > 0)
            //    {
            //        //ParseDataAndUpdateIOValue(data);
            //    }
            //}
        }
    }
}
