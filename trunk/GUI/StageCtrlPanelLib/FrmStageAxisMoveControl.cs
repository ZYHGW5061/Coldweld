using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StageCtrlPanelLib
{
    public partial class FrmStageAxisMoveControl : Form
    {
        public FrmStageAxisMoveControl()
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
