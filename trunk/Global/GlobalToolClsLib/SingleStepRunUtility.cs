using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalToolClsLib
{
    public class SingleStepRunUtility
    {
        private static readonly object _lockObj = new object();
        private static volatile SingleStepRunUtility _instance = null;

        public static SingleStepRunUtility Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new SingleStepRunUtility();
                        }
                    }
                }
                return _instance;
            }
        }
        private SingleStepRunUtility()
        {

        }

        AutoResetEvent _pauseSignal = new AutoResetEvent(false);
        public bool EnableSingleStep { get; set; }
        public void Wait()
        {
            if (EnableSingleStep)
            {
                lock (_lockObj)
                {
                    _pauseSignal.WaitOne();
                }
            }
        }
        public void Continue()
        {
            if (EnableSingleStep)
            {
                lock (_lockObj)
                {
                    _pauseSignal.Set();
                }
            }
        }
        public void RunAction(Action act)
        {
            Wait();
            if (act != null)
            {
                act();
            }
        }
    }
}
