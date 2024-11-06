using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonPanelClsLib
{
    public partial class LogMessageForm : Form
    {
        public LogMessageForm()
        {
            InitializeComponent();
        }

        public void UpdateLog(string logText)
        {
            this.textBox1.AppendText(logText + Environment.NewLine);
        }

    }
}
