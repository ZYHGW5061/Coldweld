using HardwareManagerClsLib;
using IOUtilityClsLib;
using PowerControllerManagerClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerClsLib
{
    public class PowerManager
    {
        private static volatile PowerManager _instance = new PowerManager();
        private static readonly object _lockObj = new object();

        /// <summary>
        /// 获取单例对象
        /// </summary>
        /// <returns></returns>
        public static PowerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new PowerManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private IPowerController _PowerController
        {
            get { return HardwareManager.Instance.PowerController; }
        }
        //--------

        /// <summary>
        /// 设置组号
        /// </summary>
        /// <returns></returns>
        /// 
        public void SetGP(int GPNum)
        {
            if (GPNum < 0 || GPNum > 19)
            {
                return;
            }
            _PowerController.Write(PowerAdd.GP, GPNum);
        }

        /// <summary>
        /// 运行电源On
        /// </summary>
        /// <returns></returns>
        /// 
        public void PowerRun()
        {
            //_PowerController.Write(PowerAdd.IOUT, 0);
            //IOUtilityHelper.Instance.OpenEctectic();
        }

        /// <summary>
        /// 运行电源Off
        /// </summary>
        /// <returns></returns>
        /// 
        public void PowerStop()
        {
            //_PowerController.Write(PowerAdd.IOUT, 1);
            //IOUtilityHelper.Instance.CloseEctectic();
        }

        ///// <summary>
        ///// 读取结束信号
        ///// </summary>
        ///// <returns></returns>
        ///// 
        //public bool GetStopSignal()
        //{
        //    return IOUtilityHelper.Instance.IsEctecticComplete();
        //}

        //public void Reset()
        //{
        //    IOUtilityHelper.Instance.ResetEctectic();
        //}

        //public bool GetFault()
        //{
        //    return IOUtilityHelper.Instance.IsEctecticFault();
        //}

    }
}
