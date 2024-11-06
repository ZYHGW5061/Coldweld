
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.SerialCommunicationClsLib;

namespace TurboMolecularPumpControllerClsLib
{
    public class SimulateTurboMolecularPumpController : ITurboMolecularPumpController
    {
        public bool IsConnect => throw new NotImplementedException();

        public SerialPort SerialPortEngine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void FaultReset()
        {
            throw new NotImplementedException();
        }

        public void FreeShutdown()
        {
            throw new NotImplementedException();
        }

        public void Function()
        {
            throw new NotImplementedException();
        }

        public void Lock()
        {
            throw new NotImplementedException();
        }

        public int Read()
        {
            throw new NotImplementedException();
        }

        public TurboMolecularPumpstatus ReadStatus()
        {
            throw new NotImplementedException();
        }

        public void SlowShutdown()
        {
            throw new NotImplementedException();
        }

        public void Unlock()
        {
            throw new NotImplementedException();
        }

        public void Write()
        {
            throw new NotImplementedException();
        }
    }
}
