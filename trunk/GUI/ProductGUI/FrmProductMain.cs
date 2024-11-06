using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductGUI
{
    public partial class FrmProductMain : Form
    {
        private FrmProductMainPanel frmProductMainPanel1;

        public FrmProductMain()
        {
            

            InitializeComponent();

            this.frmProductMainPanel1 = ProductGUI.FrmProductMainPanel.Instance;
            // 
            // frmProductMainPanel1
            // 
            this.frmProductMainPanel1.Location = new System.Drawing.Point(13, 13);
            this.frmProductMainPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.frmProductMainPanel1.Name = "frmProductMainPanel1";
            this.frmProductMainPanel1.Size = new System.Drawing.Size(880, 529);
            this.frmProductMainPanel1.TabIndex = 0;
            // 
            // FrmProductMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            
            this.Controls.Add(this.frmProductMainPanel1);

        }
    }
}
