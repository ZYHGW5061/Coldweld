using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightSourceCtrlPanelLib
{
    public partial class LightControl : UserControl
    {
        public LightControl()
        {
            InitializeComponent();
            foreach (var item in Enum.GetValues(typeof(EnumLightSourceType)))
            {
                cmbSelectLight.Items.Add(item);
            }
        }

        private void cmbSelectLight_SelectedIndexChanged(object sender, EventArgs e)
        {

                if (!string.IsNullOrEmpty(cmbSelectLight.Text))
                {
                    ctrlLight1.CurrentLightType = (EnumLightSourceType)Enum.Parse(typeof(EnumLightSourceType), cmbSelectLight.Text);
                }

        }
    }
}
