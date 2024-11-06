using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionGUI
{
    public partial class CameraParamForm : Form
    {
        string theClickButton = "cancel";
        public CameraParamForm()
        {
            InitializeComponent();
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
        private bool IsTextNumeric(string text)
        {
            if (double.TryParse(text, out _))
            {
                return true;
            }

            return false;
        }
        public (double, double, double, bool) showMessage(string text3 = "Bond相机参数设置",double ImageWidthPixelsize = 1, double ImageHeightPixelsize = 1, double Angle = 0)
        {
            this.Text = text3;
            double imageWidthPixelsize = ImageWidthPixelsize;
            double imageHeightPixelsize = ImageHeightPixelsize;
            double angle = Angle;
            bool confirm = true;

            ImageWidthPixelsizetextBox.Text = imageWidthPixelsize.ToString();
            ImageHeightPixelsizetextBox.Text = imageHeightPixelsize.ToString();
            ImageAngletextBox.Text = angle.ToString();

            this.ShowDialog();

            if (theClickButton == "cancel")
            {
                confirm = false;
            }
            else
            {
                confirm = true;

                string text = ImageWidthPixelsizetextBox.Text;
                if (string.IsNullOrWhiteSpace(text))
                {
                    //MessageBox.Show("The textbox is empty or contains only whitespace.");
                }
                else if (IsTextNumeric(text))
                {
                    ImageWidthPixelsize = double.Parse(text);
                }
                text = ImageHeightPixelsizetextBox.Text;
                if (string.IsNullOrWhiteSpace(text))
                {
                    //MessageBox.Show("The textbox is empty or contains only whitespace.");
                }
                else if (IsTextNumeric(text))
                {
                    ImageHeightPixelsize = double.Parse(text);
                }
                text = ImageAngletextBox.Text;
                if (string.IsNullOrWhiteSpace(text))
                {
                    //MessageBox.Show("The textbox is empty or contains only whitespace.");
                }
                else if (IsTextNumeric(text))
                {
                    Angle = double.Parse(text);
                }
            }
            
            


            
            return (ImageWidthPixelsize, ImageHeightPixelsize, Angle, confirm);
        }
    }
}
