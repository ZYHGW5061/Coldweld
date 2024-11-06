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
using TechnologicalClsLib;
using VacuumGaugeControllerClsLib;

namespace ControlPanelClsLib.Manual
{
    public partial class FrmVacuumControl : Form
    {

        private OvenBoxProcessControl _plc
        {
            get { return OvenBoxProcessControl.Instance; }
        }

        private SynchronizationContext _syncContext;
        private VacuumGaugeControllerManager _VacuumGaugeControllerManager
        {
            get { return VacuumGaugeControllerManager.Instance; }
        }

        public FrmVacuumControl()
        {
            InitializeComponent();

            //timer1.Enabled = true;

            DataModel.Instance.PropertyChanged += DataModel_PropertyChanged;
            _syncContext = SynchronizationContext.Current;

            seOven1Vacuum.Value = (decimal)DataModel.Instance.BakeOvenVacuum;

            seOven2Vacuum.Value = (decimal)DataModel.Instance.BakeOven2Vacuum;


            seBoxVacuum.Value = (decimal)DataModel.Instance.BoxVacuum;
        }

        private void DataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            #region 烘箱1


            if (e.PropertyName == nameof(DataModel.BakeOvenVacuum))
            {
                _syncContext.Post(_ => seOven1Vacuum.Value = (decimal)DataModel.Instance.BakeOvenVacuum, null);
            }

            #endregion

            #region 烘箱2



            if (e.PropertyName == nameof(DataModel.BakeOven2Vacuum))
            {
                _syncContext.Post(_ => seOven2Vacuum.Value = (decimal)DataModel.Instance.BakeOven2Vacuum, null);
            }




            #endregion

            #region 方舱

            if (e.PropertyName == nameof(DataModel.BoxVacuum))
            {
                _syncContext.Post(_ => seBoxVacuum.Value = (decimal)DataModel.Instance.BoxVacuum, null);
            }


            #endregion



        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //Task.Run(() =>
            //{
                if (_VacuumGaugeControllerManager.AllVacuumGauges.Count > 0)
                {

                    if (_VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox1).IsConnect)
                    {
                        float Vacuum1 = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox1).ReadVacuum();
                        seOven1Vacuum.Value = (decimal)(Vacuum1);
                    }

                    if (_VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox2).IsConnect)
                    {
                        float Vacuum2 = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox2).ReadVacuum();
                        seOven2Vacuum.Value = (decimal)(Vacuum2);
                    }

                    if (_VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.Box).IsConnect)
                    {

                        float Vacuum3 = _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.Box).ReadVacuum();
                        seBoxVacuum.Value = (decimal)(Vacuum3);
                    }
                }
            //});
            
        }

        private void btnOven1Vacuum_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                _plc.OvenBox1VacuumPumping();
            });

        }

        private void btnStopOven1Vacuum_Click(object sender, EventArgs e)
        {
            
            Task.Run(() =>
            {
                _plc.StopOvenBox1VacuumPumping();
            });
        }

        private void btnOven1GulpValveOpen_Click(object sender, EventArgs e)
        {
            _plc.WriteBoolOvenBoxStates(EnumBoardcardDefineOutputIO.BakeOvenAerate, true);
        }

        private void btnOven1GulpValveClose_Click(object sender, EventArgs e)
        {
            _plc.WriteBoolOvenBoxStates(EnumBoardcardDefineOutputIO.BakeOvenAerate, false);
        }

        private void btnOven2Vacuum_Click(object sender, EventArgs e)
        {
            
            Task.Run(() =>
            {
                _plc.OvenBox2VacuumPumping();
            });
        }

        private void btnStopOven2Vacuum_Click(object sender, EventArgs e)
        {
            
            Task.Run(() =>
            {
                _plc.StopOvenBox2VacuumPumping();
            });
        }

        private void btnOven2GulpValveOpen_Click(object sender, EventArgs e)
        {
            _plc.WriteBoolOvenBoxStates(EnumBoardcardDefineOutputIO.BakeOven2Aerate, true);
        }

        private void btnOven2GulpValveClose_Click(object sender, EventArgs e)
        {
            _plc.WriteBoolOvenBoxStates(EnumBoardcardDefineOutputIO.BakeOven2Aerate, false);
        }

        private void btnBoxVacuum_Click(object sender, EventArgs e)
        {
            
            Task.Run(() =>
            {
                _plc.BoxVacuumPumping();
            });
        }

        private void btnStopBoxVacuum_Click(object sender, EventArgs e)
        {
            
            Task.Run(() =>
            {
                _plc.StopBoxVacuumPumping();
            });
        }

        private void btnBoxGulpValveOpen_Click(object sender, EventArgs e)
        {
            _plc.WriteBoolOvenBoxStates(EnumBoardcardDefineOutputIO.BoxAerate, true);
        }

        private void btnBoxGulpValveClose_Click(object sender, EventArgs e)
        {
            _plc.WriteBoolOvenBoxStates(EnumBoardcardDefineOutputIO.BoxAerate, false);
        }

        private void btnOven1GulpValve_Click(object sender, EventArgs e)
        {
           
            Task.Run(() =>
            {
                _plc.MaterialBoxSupplyValve1();
            });
        }

        private void btnOven2GulpValve_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                _plc.MaterialBoxSupplyValve2();
            });
        }

        private void btnBoxGulpValve_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                _plc.MaterialBoxSupplyValve3();
            });
        }
    }
}
