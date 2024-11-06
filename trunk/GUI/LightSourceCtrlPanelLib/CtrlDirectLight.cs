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
using System.Threading;
using DevExpress.Utils.Design;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.LoggerManagerClsLib;
using GlobalDataDefineClsLib;
using ConfigurationClsLib;

namespace LightSourceCtrlPanelLib
{
    public partial class CtrlDirectLight : XtraUserControl
    {
        /// <summary>
        /// 当前暗场光源类型
        /// </summary>
        private EnumLightSourceType _cuLightType;
        /// <summary>
        ///  当前暗场光源类型
        /// </summary>
        public EnumLightSourceType CurrentBrightFieldType
        {
            get { return _cuLightType; }
            set { _cuLightType = value; }
        }
        /// <summary>
        /// 系统日志记录器
        /// </summary>
        private static IBaseLogger _systemDebugLogger
        {
            get { return LoggerManager.GetHandler().GetFileLogger(GlobalParameterSetting.SYSTEM_DEBUG_LOGGER_ID); }
        }

        /// <summary>
        /// 硬件配置
        /// </summary>
        private HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }

        /// <summary>
        /// 硬件系统
        /// </summary>
        private HardwareManager _hardwareManager
        {
            get { return HardwareManager.Instance; }
        }

        /// <summary>
        /// 明场亮度或滤光片改变时
        /// </summary>
        public Action BrightFieldBrightnessChanged { get; set; }

        /// <summary>
        /// 是否需要将设置的强度应用到硬件
        /// </summary>
        public bool ApplyIntensityToHardware { get; set; }


        /// <summary>
        /// 获取或设置要使用的亮度
        /// </summary>
        public float Brightness
        {
            get
            {
                //return Convert.ToSingle((light_brightness_control.EditValue ?? "0").ToString());
                //return Convert.ToSingle(labelValue.Text);
                var ret = 0f;
                float.TryParse(labelValue.Text, out ret);
                return ret;
            }
            set
            {
                light_brightness_control.EditValue = value;
            }
        }

        /// <summary>
        /// 当前控件关联的明场光源控制器
        /// </summary>
        private ILightSourceController _brightFieldController
        {
            get
            {
                if (_cuLightType == EnumLightSourceType.LidBrightField)
                {
                    return HardwareManager.Instance.LidBrightField;
                }
                else
                {
                    return HardwareManager.Instance.ShellBrightField;
                }
            }
        }
        private int _channelNumber
        {
            get
            {
                if (_cuLightType == EnumLightSourceType.LidBrightField)
                {
                    return _hardwareConfig.LidBrightFieldConfig.ChannelNumber;
                }
                else
                {
                    return _hardwareConfig.ShellBrightFieldConfig.ChannelNumber;
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CtrlDirectLight()
        {
            InitializeComponent();
            if (!DesignTimeTools.IsDesignMode)
            {
                InitUIData();
            }
        }
        /// <summary>
        /// 初始化UI数据
        /// </summary>
        private void InitUIData()
        {
            //light_brightness_control.Properties.Minimum = 0;// (int)_hardwareConfig.BrightFieldControllerConfig.LineScanLED.MinIntensity;
            //light_brightness_control.Properties.Maximum = 255;// (int)_hardwareConfig.BrightFieldControllerConfig.LineScanLED.MaxIntensity;
            if (_cuLightType == EnumLightSourceType.LidBrightField)
            {
                light_brightness_control.Properties.Minimum = (int)_hardwareConfig.LidBrightFieldConfig.MinIntensity;
                light_brightness_control.Properties.Maximum = (int)_hardwareConfig.LidBrightFieldConfig.MaxIntensity;
            }
            else
            {
                light_brightness_control.Properties.Minimum = (int)_hardwareConfig.ShellBrightFieldConfig.MinIntensity;
                light_brightness_control.Properties.Maximum = (int)_hardwareConfig.ShellBrightFieldConfig.MaxIntensity;
            }
        }

        /// <summary>
        /// 首次加载
        /// </summary>
        protected override void OnFirstLoad()
        {
            if (DesignTimeTools.IsDesignMode)
            {
                return;
            }
            try
            {
            }
            finally
            {
                base.OnFirstLoad();
            }
        }

        /// <summary>
        /// 设置明场亮度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Apply()
        {
            try
            {
                if (_brightFieldController != null)
                {
                    float brightness = light_brightness_control.Value;
                    if (ApplyIntensityToHardware || brightness == 0)
                    {
                        _brightFieldController.SetIntensity(brightness, _channelNumber);
                    }
                    //Brightness = brightness;
                    //XtraMessageBox.Show("DarkField Brightness setting successed!", "Info");
                }
            }
            catch (Exception ex)
            {
                _systemDebugLogger.AddErrorContent("", ex);
            }
            finally
            {
                if (ApplyIntensityToHardware)
                {
                    //读取当前亮度
                    ReadCurrentBrightness();
                }
            }
        }

        /// <summary>
        /// 读取当前亮度
        /// </summary>
        public void ReadCurrentBrightness()
        {
            try
            {
                if (_brightFieldController != null)
                {
                    var voltage = _brightFieldController.GetIntensity(_channelNumber);
                    light_brightness_control.EditValue = voltage;
                    labelValue.Text = (light_brightness_control.EditValue ?? "0").ToString();
                }
            }
            catch (Exception ex)
            {
                _systemDebugLogger.AddErrorContent("ReadCurrentBrightness，Error", ex);
            }
        }

        private void light_brightness_control_EditValueChanged(object sender, EventArgs e)
        {
            //labelValue.Text = (light_brightness_control.EditValue ?? "0").ToString();
            Apply();
        }

        private void light_brightness_control_MouseUp(object sender, MouseEventArgs e)
        {
            //btnSet_Click(sender, e);
        }
    }
}
