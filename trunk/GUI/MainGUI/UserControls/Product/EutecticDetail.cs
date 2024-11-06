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
    public partial class EuticticDetail : UserControl
    {
        public EuticticDetail()
        {
            InitializeComponent();
        }

        public void fillEutecticDetail(EutecticConfig eutecticConf)
        {
            if (eutecticConf == null)
            {
                this.teEutecticName.Text = null;
                return;
            }

            this.teEutecticName.Text = eutecticConf.ConfigName;
        }
    }
}
