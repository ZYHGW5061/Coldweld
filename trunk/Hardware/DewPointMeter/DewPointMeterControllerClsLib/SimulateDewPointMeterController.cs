
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DewPointMeterControllerClsLib
{
    public class SimulateDewPointMeterController : IDewPointMeterController
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


        public int Read()
        {
            throw new NotImplementedException();
        }

        public float ReadDewPoint()
        {
            throw new NotImplementedException();
        }


        public void Write()
        {
            throw new NotImplementedException();
        }
    }
}
