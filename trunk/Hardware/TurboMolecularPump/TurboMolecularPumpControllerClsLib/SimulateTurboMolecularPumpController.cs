
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

        public bool FaultReset()
        {
            throw new NotImplementedException();
        }

        public bool FreeShutdown()
        {
            throw new NotImplementedException();
        }

        public bool Function()
        {
            throw new NotImplementedException();
        }

        public bool Lock()
        {
            throw new NotImplementedException();
        }

        public int Read()
        {
            throw new NotImplementedException();
        }

        public bool ReadStatus(ref TurboMolecularPumpstatus param)
        {
            throw new NotImplementedException();
        }

        public bool SlowShutdown()
        {
            throw new NotImplementedException();
        }

        public bool Unlock()
        {
            throw new NotImplementedException();
        }

        public bool Write()
        {
            throw new NotImplementedException();
        }

        void ITurboMolecularPumpController.Write()
        {
            throw new NotImplementedException();
        }
    }
}
