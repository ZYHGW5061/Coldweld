using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils.Design;
using Skyverse.Sweetgum.BaseLoggerClsLib;
using Skyverse.Sweetgum.LoggerManagerClsLib;
using Skyverse.Sweetgum.GlobalSettingClsLib;
using Skyverse.Sweetgum.BacksideTerminal.HardwareManagerClsLib;
using Skyverse.Sweetgum.BacksideTerminal.Hardwares.DarkFieldControllerManagerClsLib;
using Skyverse.Sweetgum.BacksideTerminal.Hardwares.DarkFieldControllerClsLib;
using Skyverse.Sweetgum.BacksideTerminal.SystemConfigurationClsLib;

namespace Skyverse.Sweetgum.BacksideTerminal.LightSourceCtrLib
{
    public partial class CtrlDarkField : XtraUserControl
    {
        /// <summary>
        /// 系统日志记录器
        /// </summary>
        private static IBaseLogger _systemDebugLogger
        {
            get { return LoggerManager.GetHandler().GetFileLogger(GlobalParamSetting.SYSTEM_DEBUG_LOGGER_ID); }
        }

        /// <summary>
        /// 亮度
        /// </summary>
        public float Brightness
        {
            get
            {
                return Convert.ToSingle(labelValue.Text);
            }
        }

        /// <summary>
        /// 系统配置
        /// </summary>
        private SystemConfig _systemConfigure
        {
            get
            {
                return SystemConfig.GetHandler();
            }
        }

        /// <summary>
        /// 获取暗场光源控制器
        /// </summary>
        IDarkFieldController _darkfieldlight
        {
            get
            {
                return HardwareManager.GetHandler().DarkFieldController;
            }
        }
        /// <summary>
        /// 暗场光源亮度改变
        /// </summary>
        public Action DarkFieldBrightnessChanged { get; set; }

        /// <summary>
        /// 暗场页面构造函数
        /// </summary>
        public CtrlDarkField()
        {
            InitializeComponent();
            InitializeDFCtlValue();
        }
        private void InitializeDFCtlValue()
        {
            light_brightness_control.Properties.Minimum = 0;
            light_brightness_control.Properties.Maximum = 511;
        }
        protected override void OnFirstLoad()
        {
            if (DesignTimeTools.IsDesignMode)
            {
                return;
            }
            base.OnFirstLoad();
        }

        public void ReadCurrentBrightness()
        {
            try
            {
                if (_darkfieldlight != null)
                {
                    var voltage = _darkfieldlight.GetIntensity();
                    light_brightness_control.EditValue = voltage;
                    labelValue.Text = (light_brightness_control.EditValue ?? "0").ToString();
                }
            }
            catch (Exception ex)
            {
                _systemDebugLogger.AddErrorContent("", ex);
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {   
                //设置 发光延迟时间
                if (_darkfieldlight != null)
                {
                    float brightness = light_brightness_control.Value;
                    _darkfieldlight.SetIntensity(brightness);
                    if (DarkFieldBrightnessChanged != null)
                    {
                        DarkFieldBrightnessChanged();
                    }
                    XtraMessageBox.Show("DarkField Brightness setting successed!", "Info");                    
                }
            }
            catch (Exception ex)
            {
                _systemDebugLogger.AddErrorContent("", ex);
            }
            finally
            {
                ReadCurrentBrightness();
            }
        }

        private void light_brightness_control_EditValueChanged(object sender, EventArgs e)
        {
            labelValue.Text = (light_brightness_control.EditValue ?? "0").ToString();
        }
    }
}
