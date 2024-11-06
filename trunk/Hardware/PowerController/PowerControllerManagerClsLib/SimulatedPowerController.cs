using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PowerControllerManagerClsLib
{
    /// <summary>
    /// 模拟的Stage控制器
    /// </summary>
    internal class SimulatedPowerController : IPowerController
    {
        public bool IsConnect { get { return true; } }

        public void Connect()
        {
            
        }

        public void Disconnect()
        {
    
        }

        public int Read(PowerAdd Add)
        {
            return -1;
        }

        public void Write(PowerAdd Add, int value)
        {
            
        }

        public PowerParam ReadAll()
        {
            return null;
        }

        public void WriteAll(PowerParam param)
        {
            throw new NotImplementedException();
        }
    }
}
