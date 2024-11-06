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
using HardwareManagerClsLib;
using LightControllerClsLib;

namespace LightSourceCtrlPanelLib
{
    public partial class CtrlLight : XtraUserControl
    {
        /// <summary>
        /// 当前暗场光源类型
        /// </summary>
        private EnumLightSourceType _curLightType;
        /// <summary>
        ///  当前暗场光源类型
        /// </summary>
        public EnumLightSourceType CurrentLightType
        {
            get { return _curLightType; }
            set 
            { 
                _curLightType = value;
                if (_curLightType == EnumLightSourceType.TrackRingField)
                {
                    labelLightType.Text="传送环光";
                }
                else if (_curLightType == EnumLightSourceType.WeldRingField)
                {
                    labelLightType.Text = "焊接环光";
                }
                if (ApplyIntensityToHardware)
                {
                    //读取当前亮度
                    ReadCurrentBrightness();
                }
            }
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
        private ILightSourceController _controller
        {
            get
            {
                if (_curLightType == EnumLightSourceType.TrackRingField)
                {
                    return HardwareManager.Instance.TrackRingLightController;
                }
                else
                {
                    return HardwareManager.Instance.WeldRingLightController;
                }
                
            }
        }
        private int _channelNumber
        {
            get
            {
                if (_curLightType == EnumLightSourceType.TrackRingField)
                {
                    return _hardwareConfig.TrackRingLightConfig.ChannelNumber;
                }
                else
                {
                    return _hardwareConfig.WeldRingLightConfig.ChannelNumber;
                }
                
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CtrlLight()
        {
            InitializeComponent();
            if (!DesignTimeTools.IsDesignMode)
            {
                InitUIData();
            }
            ReadCurrentBrightness();
        }
        /// <summary>
        /// 初始化UI数据
        /// </summary>
        private void InitUIData()
        {
            if (_curLightType == EnumLightSourceType.TrackRingField)
            {
                light_brightness_control.Properties.Minimum = (int)_hardwareConfig.TrackRingLightConfig.MinIntensity;
                light_brightness_control.Properties.Maximum = (int)_hardwareConfig.TrackRingLightConfig.MaxIntensity;
            }
            else if (_curLightType == EnumLightSourceType.WeldRingField)
            {
                light_brightness_control.Properties.Minimum = (int)_hardwareConfig.WeldRingLightConfig.MinIntensity;
                light_brightness_control.Properties.Maximum = (int)_hardwareConfig.WeldRingLightConfig.MaxIntensity;
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
                if (_controller != null)
                {
                    float brightness = light_brightness_control.Value;
                    if (ApplyIntensityToHardware || brightness == 0)
                    {
                        _controller.SetIntensity(brightness, _channelNumber);
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
                //if (ApplyIntensityToHardware)
                //{
                //    //读取当前亮度
                //    ReadCurrentBrightness();
                //}
            }
        }

        /// <summary>
        /// 读取当前亮度
        /// </summary>
        public void ReadCurrentBrightness()
        {
            try
            {
                if (_controller != null)
                {
                    var voltage = _controller.GetIntensity(_channelNumber);
                    light_brightness_control.EditValue = voltage;
                    labelValue.Text = voltage.ToString();
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

        }

        private void light_brightness_control_MouseUp(object sender, MouseEventArgs e)
        {
            //btnSet_Click(sender, e);
        }

        private void light_brightness_control_Scroll(object sender, EventArgs e)
        {
            labelValue.Text = (light_brightness_control.EditValue ?? "0").ToString();
            Apply();
        }
    }
}
