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
    public partial class PositionDetail : UserControl
    {
        public PositionDetail()
        {
            InitializeComponent();
        }

        public void fillPositionDetail(BondingPositionConfig posConf)
        {
            if (posConf == null)
            {
                this.tePosName.Text = null;
                return;
            }

            this.tePosName.Text = posConf.ConfigName;
        }
    }
}
