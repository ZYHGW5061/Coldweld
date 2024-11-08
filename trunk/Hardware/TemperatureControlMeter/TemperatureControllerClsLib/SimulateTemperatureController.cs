using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.SerialCommunicationClsLib;

namespace TemperatureControllerClsLib
{
    public class SimulateTemperatureController : ITemperatureController
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

        public int Read(TemperatureRtuAdd Add)
        {
            throw new NotImplementedException();
        }

        public bool Read(TemperatureRtuAdd Add, ref int Data)
        {
            throw new NotImplementedException();
        }

        public bool Write(TemperatureRtuAdd Add, int value)
        {
            throw new NotImplementedException();
        }
    }
}
