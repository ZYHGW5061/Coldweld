using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StageControllerClsLib
{
    public class SimulateStageController : IStageController
    {
        public ISingleAxisController this[EnumStageAxis axis]
        {
            get
            {
                return new SimulatedSingleAxisController();
            }

        }

        public IMultiAxisController this[params EnumStageAxis[] axis] => throw new NotImplementedException();

        public bool IsConnect { get { return true; } }

        public bool IsHomedone { get { return true; } }

        public bool CheckHomeDone()
        {
            return true; 
        }

        public void Connect()
        {

        }

        public void Disconnect()
        {

        }

        public void Home()
        {

        }

        public void InitialzeAllAxisParameter()
        {

        }

        public void InitialzeAllIO()
        {
            throw new NotImplementedException();
        }

        public void OffVacuum(EnumVacuum enumVacuum)
        {

        }

        public void OnVacuum(EnumVacuum enumVacuum)
        {

        }

        public void Stop()
        {

        }
    }
}
