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
    public partial class MessageBox0 : BaseForm
    {
        string theClickButton = "cancel";
        public MessageBox0()
        {
            InitializeComponent();
            memoEdit1.BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            theClickButton = "confirm";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            theClickButton = "cancel";
            this.Close();
        }
        public string showMessage(string text1, string text2, string text3="警告")
        {
            this.Text = text3;
            memoEdit1.Text = text1;
            memoEdit2.Text = text2;
            this.ShowDialog();
            return theClickButton;
        }
    }
}
