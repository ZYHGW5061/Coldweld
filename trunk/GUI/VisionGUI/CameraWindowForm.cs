using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using CameraControllerClsLib;


namespace VisionGUI
{
    public partial class CameraWindowForm : Form
    {

        #region Private File

        private static readonly object _lockObj = new object();
        private static volatile CameraWindowForm _instance = null;
        public static CameraWindowForm Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new CameraWindowForm();
                        }
                    }
                }
                return _instance;
            }
        }

        CameraWindowGUI CameraWindow;


        #endregion

        private CameraWindowForm()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(CameraWindowForm_FormClosing);
        }


        public void InitializeWindow(CameraWindowGUI CameraWindow)
        {
            //this.CameraWindow = new AlgorithmClassLibrary.CameraWindowControl();
            // 
            // CameraWindow
            // 
            this.CameraWindow = CameraWindow;
            this.CameraWindow.Location = new System.Drawing.Point(4, 4);
            this.CameraWindow.Margin = new System.Windows.Forms.Padding(2);
            this.CameraWindow.Name = "CameraWindow1";
            this.CameraWindow.Size = new System.Drawing.Size(905, 735);
            this.CameraWindow.TabIndex = 0;

            this.Controls.Add(this.CameraWindow);
        }

        public void ShowLocation(Point? location = null)
        {
            if (location.HasValue)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = location.Value;
            }
        }

        private void CameraWindowForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CameraWindowGUI.Instance.CameraStop();
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception)
            {
            }

        }



    }
}
