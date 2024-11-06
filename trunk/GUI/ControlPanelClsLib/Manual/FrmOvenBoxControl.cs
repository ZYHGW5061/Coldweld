using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPanelClsLib
{
    public partial class FrmOvenBoxControl : Form
    {
        public FrmOvenBoxControl()
        {
            InitializeComponent();
        }

        public void ShowLocation(Point? location = null)
        {
            if (location.HasValue)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = location.Value;
            }
        }
    }
}
