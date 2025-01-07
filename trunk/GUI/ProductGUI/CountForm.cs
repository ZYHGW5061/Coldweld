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

namespace ProductGUI
{
    public partial class CountForm : Form
    {
        private SynchronizationContext _syncContext;

        public CountForm()
        {
            InitializeComponent();

            #region 统计

            laWeldMaterial.Text = DataModel.Instance.WeldMaterialNumber.ToString();

            laPressNum.Text = DataModel.Instance.PressWorkNumber.ToString();

            laRunTime.Text = DataModel.Instance.EquipmentOperatingTime.ToString();


            #endregion

            DataModel.Instance.PropertyChanged += DataModel_PropertyChanged;
            _syncContext = SynchronizationContext.Current;
        }


        private void DataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_syncContext == null)
            {
                return;
            }

            #region 统计


            if (e.PropertyName == nameof(DataModel.WeldMaterialNumber))
            {
                _syncContext.Post(_ => laWeldMaterial.Text = DataModel.Instance.WeldMaterialNumber.ToString(), null);
                //UpdateWeldMaterialNumber();
            }

            if (e.PropertyName == nameof(DataModel.PressWorkNumber))
            {
                _syncContext.Post(_ => laPressNum.Text = DataModel.Instance.PressWorkNumber.ToString(), null);
                //UpdatePressWorkNumber();
            }

            if (e.PropertyName == nameof(DataModel.EquipmentOperatingTime))
            {
                _syncContext.Post(_ => laRunTime.Text = DataModel.Instance.EquipmentOperatingTime.ToString(), null);
                //_syncContext.Post(_ => toolStripStatusLabelRunTime.Text = DataModel.Instance.EquipmentOperatingTime.ToString(), null);

                //UpdateEquipmentOperatingTime();
            }


            #endregion





        }

        private void CountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataModel.Instance.PropertyChanged -= DataModel_PropertyChanged;
            e.Cancel = false;
        }
    }
}
