using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skyverse.Sweetgum.BacksideTerminal.Hardwares.BaseLightLEDControllerClsLib;
using Skyverse.Sweetgum.BacksideTerminal.Hardwares.DarkFieldControllerManagerClsLib;
using Skyverse.Sweetgum.BacksideTerminal.Hardwares.DarkFieldControllerClsLib;
using Skyverse.Sweetgum.BacksideTerminal.Hardwares.RevoxDarkFieldControllerClsLib;

namespace Skyverse.Sweetgum.BacksideTerminal.LightSourceCtrl
{
    public partial class RevoxLightSourceCtrlPanel : UserControl
    {
        public RevoxLightSourceCtrlPanel()
        {
            InitializeComponent();
        }

        private DarkFieldControllerManager _darkFieldControllerManager
        {
            get { return DarkFieldControllerManager.GetHandler(); }
        }

        private IDarkFieldController _lightController
        {
            get { return _darkFieldControllerManager.GetCurrentHardware(); }
        }

        private void btnSetFilter_Click(object sender, EventArgs e)
        {
        }

        private void btnGetValue_Click(object sender, EventArgs e)
        {
            var intensity = _lightController.GetIntensity();
            this.textEditGetValule.Text = intensity.ToString();
        }

        private void btnSetValue_Click(object sender, EventArgs e)
        {
            var intensity = this.spinEdit_Intensity.Value;
            //var channel = (EnumRevoxChannel)Enum.Parse(typeof(EnumRevoxChannel), intensity);
            _lightController.SetIntensity((float)intensity);
        }

        private void btnLightOn_Click(object sender, EventArgs e)
        {
            ((RevoxDarkFieldController)_lightController).LightOn();
        }

        

        private void btnLightOff_Click(object sender, EventArgs e)
        {
            ((RevoxDarkFieldController)_lightController).LightOff();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            _darkFieldControllerManager.Initialize();
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            _darkFieldControllerManager.Shutdown();
        } 
    }
}
