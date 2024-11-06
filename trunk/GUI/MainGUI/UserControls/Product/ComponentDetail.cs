using ModelConfigClsLib.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainGUI.UserControls.Product
{
    public partial class ComponentDetail : UserControl
    {
        public ComponentDetail()
        {
            InitializeComponent();
        }

        public void fillComponentDetail(ComponentConfig comConf)
        {
            if (comConf == null)
            {
                this.teComponentName.Text = null;
                return;
            }

            this.teComponentName.Text = comConf.ConfigName;
        }
    }
}
