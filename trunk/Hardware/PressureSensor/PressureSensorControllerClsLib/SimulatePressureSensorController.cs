
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.SerialCommunicationClsLib;

namespace PressureSensorControllerClsLib
{
    public class SimulatePressureSensorController : IPressureSensorController
    {
        public bool IsConnect => throw new NotImplementedException();

        public SerialPortController SerialPortEngine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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


        public void Write()
        {
            throw new NotImplementedException();
        }
    }
}
