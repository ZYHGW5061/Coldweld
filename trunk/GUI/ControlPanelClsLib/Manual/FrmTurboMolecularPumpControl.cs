using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurboMolecularPumpControllerClsLib;

namespace ControlPanelClsLib.Manual
{
    public partial class FrmTurboMolecularPumpControl : Form
    {

        #region private file

        private TurboMolecularPumpControllerManager _TurboMolecularPumpControllerManager
        {
            get { return TurboMolecularPumpControllerManager.Instance; }
        }

        private SynchronizationContext _syncContext;

        #endregion

        #region public file

        public FrmTurboMolecularPumpControl()
        {
            InitializeComponent();

            //timer1.Enabled = true;

            DataModel.Instance.PropertyChanged += DataModel_PropertyChanged;
            _syncContext = SynchronizationContext.Current;


            seOutputFrequency.Value = (decimal)DataModel.Instance.OvenBox1OutputFrequency;
            seOutputVoltage.Value = (decimal)DataModel.Instance.OvenBox1OutputVoltage;
            seOutputCurrent.Value = (decimal)DataModel.Instance.OvenBox1OutputCurrent;

            laStandbymode.BackColor = (DataModel.Instance.OvenBox1Standbymode ? true : false) ? Color.Red : Color.GreenYellow;
            laFunction.BackColor = (DataModel.Instance.OvenBox1Function ? true : false) ? Color.Red : Color.GreenYellow;
            laerr.BackColor = (DataModel.Instance.OvenBox1err ? true : false) ? Color.Red : Color.GreenYellow;
            laOC.BackColor = (DataModel.Instance.OvenBox1OC ? true : false) ? Color.Red : Color.GreenYellow;
            laOE.BackColor = (DataModel.Instance.OvenBox1OE ? true : false) ? Color.Red : Color.GreenYellow;
            laRetain.BackColor = (DataModel.Instance.OvenBox1Retain ? true : false) ? Color.Red : Color.GreenYellow;
            laRLU.BackColor = (DataModel.Instance.OvenBox1RLU ? true : false) ? Color.Red : Color.GreenYellow;
            laOL2.BackColor = (DataModel.Instance.OvenBox1OL2 ? true : false) ? Color.Red : Color.GreenYellow;
            laSL.BackColor = (DataModel.Instance.OvenBox1SL ? true : false) ? Color.Red : Color.GreenYellow;
            laESP.BackColor = (DataModel.Instance.OvenBox1ESP ? true : false) ? Color.Red : Color.GreenYellow;
            laLU.BackColor = (DataModel.Instance.OvenBox1LU ? true : false) ? Color.Red : Color.GreenYellow;
            laOH.BackColor = (DataModel.Instance.OvenBox1OH ? true : false) ? Color.Red : Color.GreenYellow;

            seOutputFrequency2.Value = (decimal)DataModel.Instance.OvenBox2OutputFrequency;
            seOutputVoltage2.Value = (decimal)DataModel.Instance.OvenBox2OutputVoltage;
            seOutputCurrent2.Value = (decimal)DataModel.Instance.OvenBox2OutputCurrent;

            laStandbymode2.BackColor = (DataModel.Instance.OvenBox2Standbymode ? true : false) ? Color.Red : Color.GreenYellow;
            laFunction2.BackColor = (DataModel.Instance.OvenBox2Function ? true : false) ? Color.Red : Color.GreenYellow;
            laerr2.BackColor = (DataModel.Instance.OvenBox2err ? true : false) ? Color.Red : Color.GreenYellow;
            laOC2.BackColor = (DataModel.Instance.OvenBox2OC ? true : false) ? Color.Red : Color.GreenYellow;
            laOE2.BackColor = (DataModel.Instance.OvenBox2OE ? true : false) ? Color.Red : Color.GreenYellow;
            laRetain2.BackColor = (DataModel.Instance.OvenBox2Retain ? true : false) ? Color.Red : Color.GreenYellow;
            laRLU2.BackColor = (DataModel.Instance.OvenBox2RLU ? true : false) ? Color.Red : Color.GreenYellow;
            laOL22.BackColor = (DataModel.Instance.OvenBox2OL2 ? true : false) ? Color.Red : Color.GreenYellow;
            laSL2.BackColor = (DataModel.Instance.OvenBox2SL ? true : false) ? Color.Red : Color.GreenYellow;
            laESP2.BackColor = (DataModel.Instance.OvenBox2ESP ? true : false) ? Color.Red : Color.GreenYellow;
            laLU2.BackColor = (DataModel.Instance.OvenBox2LU ? true : false) ? Color.Red : Color.GreenYellow;
            laOH2.BackColor = (DataModel.Instance.OvenBox2OH ? true : false) ? Color.Red : Color.GreenYellow;

        }


        #endregion

        #region private mothed


        private void DataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            #region 烘箱1


            if (e.PropertyName == nameof(DataModel.OvenBox1OutputFrequency))
            {
                _syncContext.Post(_ => seOutputFrequency.Value = (decimal)DataModel.Instance.OvenBox1OutputFrequency, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1OutputVoltage))
            {
                _syncContext.Post(_ => seOutputVoltage.Value = (decimal)DataModel.Instance.OvenBox1OutputVoltage, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1OutputCurrent))
            {
                _syncContext.Post(_ => seOutputCurrent.Value = (decimal)DataModel.Instance.OvenBox1OutputCurrent, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1Standbymode))
            {
                _syncContext.Post(_ => laStandbymode.BackColor = (DataModel.Instance.OvenBox1Standbymode ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1Function))
            {
                _syncContext.Post(_ => laFunction.BackColor = (DataModel.Instance.OvenBox1Function ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1err))
            {
                _syncContext.Post(_ => laerr.BackColor = (DataModel.Instance.OvenBox1err ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1OC))
            {
                _syncContext.Post(_ => laOC.BackColor = (DataModel.Instance.OvenBox1OC ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1OE))
            {
                _syncContext.Post(_ => laOE.BackColor = (DataModel.Instance.OvenBox1OE ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1Retain))
            {
                _syncContext.Post(_ => laRetain.BackColor = (DataModel.Instance.OvenBox1Retain ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1RLU))
            {
                _syncContext.Post(_ => laRLU.BackColor = (DataModel.Instance.OvenBox1RLU ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1OL2))
            {
                _syncContext.Post(_ => laOL2.BackColor = (DataModel.Instance.OvenBox1OL2 ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1SL))
            {
                _syncContext.Post(_ => laSL.BackColor = (DataModel.Instance.OvenBox1SL ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1ESP))
            {
                _syncContext.Post(_ => laESP.BackColor = (DataModel.Instance.OvenBox1ESP ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1LU))
            {
                _syncContext.Post(_ => laLU.BackColor = (DataModel.Instance.OvenBox1LU ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1OH))
            {
                _syncContext.Post(_ => laOH.BackColor = (DataModel.Instance.OvenBox1OH ? true : false) ? Color.Red : Color.GreenYellow, null);
            }

            #endregion

            #region 烘箱2



            if (e.PropertyName == nameof(DataModel.OvenBox2OutputFrequency))
            {
                _syncContext.Post(_ => seOutputFrequency2.Value = (decimal)DataModel.Instance.OvenBox2OutputFrequency, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2OutputVoltage))
            {
                _syncContext.Post(_ => seOutputVoltage2.Value = (decimal)DataModel.Instance.OvenBox2OutputVoltage, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2OutputCurrent))
            {
                _syncContext.Post(_ => seOutputCurrent2.Value = (decimal)DataModel.Instance.OvenBox2OutputCurrent, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2Standbymode))
            {
                _syncContext.Post(_ => laStandbymode2.BackColor = (DataModel.Instance.OvenBox2Standbymode ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2Function))
            {
                _syncContext.Post(_ => laFunction2.BackColor = (DataModel.Instance.OvenBox2Function ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2err))
            {
                _syncContext.Post(_ => laerr2.BackColor = (DataModel.Instance.OvenBox2err ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2OC))
            {
                _syncContext.Post(_ => laOC2.BackColor = (DataModel.Instance.OvenBox2OC ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2OE))
            {
                _syncContext.Post(_ => laOE2.BackColor = (DataModel.Instance.OvenBox2OE ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2Retain))
            {
                _syncContext.Post(_ => laRetain2.BackColor = (DataModel.Instance.OvenBox2Retain ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2RLU))
            {
                _syncContext.Post(_ => laRLU2.BackColor = (DataModel.Instance.OvenBox2RLU ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2OL2))
            {
                _syncContext.Post(_ => laOL22.BackColor = (DataModel.Instance.OvenBox2OL2 ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2SL))
            {
                _syncContext.Post(_ => laSL2.BackColor = (DataModel.Instance.OvenBox2SL ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2ESP))
            {
                _syncContext.Post(_ => laESP2.BackColor = (DataModel.Instance.OvenBox2ESP ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2LU))
            {
                _syncContext.Post(_ => laLU2.BackColor = (DataModel.Instance.OvenBox2LU ? true : false) ? Color.Red : Color.GreenYellow, null);
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2OH))
            {
                _syncContext.Post(_ => laOH2.BackColor = (DataModel.Instance.OvenBox2OH ? true : false) ? Color.Red : Color.GreenYellow, null);
            }



            #endregion


        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Updatestatus();
            }
            catch(Exception ex)
            {

            }

        }

        private void Updatestatus()
        {
            ////Task.Run(() =>
            ////{
            //    if (curOven != EnumTurboMolecularPumpType.OvenBox1 && curOven != EnumTurboMolecularPumpType.OvenBox2)
            //    {
            //        return;
            //    }
            //    if (_TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(curOven).IsConnect)
            //    {
            //        TurboMolecularPumpstatus Value = _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(curOven).ReadStatus();
            //        if(Value == null)
            //        {
            //            return;
            //        }

            //        seOutputFrequency.Value = (decimal)Value.OutputFrequency;
            //        seOutputVoltage.Value = (decimal)Value.OutputVoltage;
            //        seOutputCurrent.Value = (decimal)Value.OutputCurrent;

            //        laStandbymode.BackColor = (Value.Standbymode ? true : false) ? Color.Red : Color.GreenYellow;
            //        laFunction.BackColor = (Value.Function ? true : false) ? Color.Red : Color.GreenYellow;
            //        laerr.BackColor = (Value.err ? true : false) ? Color.Red : Color.GreenYellow;
            //        laOC.BackColor = (Value.OC ? true : false) ? Color.Red : Color.GreenYellow;
            //        laOE.BackColor = (Value.OE ? true : false) ? Color.Red : Color.GreenYellow;
            //        laRetain.BackColor = (Value.Retain ? true : false) ? Color.Red : Color.GreenYellow;
            //        laRLU.BackColor = (Value.RLU ? true : false) ? Color.Red : Color.GreenYellow;
            //        laOL2.BackColor = (Value.OL2 ? true : false) ? Color.Red : Color.GreenYellow;
            //        laSL.BackColor = (Value.SL ? true : false) ? Color.Red : Color.GreenYellow;
            //        laESP.BackColor = (Value.ESP ? true : false) ? Color.Red : Color.GreenYellow;
            //        laLU.BackColor = (Value.LU ? true : false) ? Color.Red : Color.GreenYellow;
            //        laOH.BackColor = (Value.OH ? true : false) ? Color.Red : Color.GreenYellow;


            //    }
            ////});
            
        }

        private void btnFunction_Click(object sender, EventArgs e)
        {
            try
            {
                _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).Function();
            }
            catch(Exception ex)
            {

            }

        }

        private void btnSlowShutdown_Click(object sender, EventArgs e)
        {
            try
            {
                _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).SlowShutdown();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnFreeShutdown_Click(object sender, EventArgs e)
        {
            try
            {
                _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).FreeShutdown();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnFaultReset_Click(object sender, EventArgs e)
        {
            try
            {
                _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).FaultReset();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).Unlock();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            try
            {
                _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).Lock();
            }
            catch (Exception ex)
            {

            }
        }


        private void btnFunction2_Click(object sender, EventArgs e)
        {
            try
            {
                _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).Function();
            }
            catch (Exception ex)
            {

            }

        }

        private void btnSlowShutdown2_Click(object sender, EventArgs e)
        {
            try
            {
                _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).SlowShutdown();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnFreeShutdown2_Click(object sender, EventArgs e)
        {
            try
            {
                _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).FreeShutdown();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnFaultReset2_Click(object sender, EventArgs e)
        {
            try
            {
                _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).FaultReset();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnUnlock2_Click(object sender, EventArgs e)
        {
            try
            {
                _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).Unlock();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnLock2_Click(object sender, EventArgs e)
        {
            try
            {
                _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).Lock();
            }
            catch (Exception ex)
            {

            }
        }


        #endregion

        #region public mothed

        #endregion


    }
}
