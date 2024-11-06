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
using System.Threading;
using GlobalDataDefineClsLib;
using StageManagerClsLib;
using PositioningSystemClsLib;
using ConfigurationClsLib;
using WestDragon.Framework.UtilityHelper;
using CommonPanelClsLib;

namespace StageCtrlPanelLib
{
    /// <summary>
    /// Motion Control 单轴控制移动
    /// </summary>
    public partial class StageAxisParameterPanel : DevExpress.XtraEditors.XtraUserControl
    {
        protected PositioningSystem _positionSystem
        {
            get
            {
                return PositioningSystem.Instance;
            }
        }
        /// <summary>
        /// Stage控制器引擎
        /// </summary>
        //private IStageController _stageControlEngine
        //{
        //    get { return HardwareManager.Instance.Stage; }
        //}
        /// <summary>
        /// 硬件配置
        /// </summary>
        private HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }
        /// <summary>
        /// 当前控制的Stage的坐标轴
        /// </summary>
        private EnumStageAxis _currentStageAxis = EnumStageAxis.MaterialboxX;

        /// <summary>
        /// 当前控制的Stage的坐标轴
        /// </summary>
        public EnumStageAxis CurrentStageAxis
        {
            get
            {
                return _currentStageAxis;
            }
            set
            {
                UpdateUnitToUI(false);
                _currentStageAxis = value;
            }
        }

        /// <summary>
        /// 系统监控器
        /// </summary>
        //private SystemMonitor _systemMonitor
        //{
        //    get { return SystemMonitor.GetHandler(); }
        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        public StageAxisParameterPanel()
        {
            InitializeComponent();

            if (!CommonProcess.IsInDesigntime)
            {
                LoadAxisParameters();
            }
            foreach (var item in Enum.GetValues(typeof(EnumStageAxis)))
            {
                this.comboBoxSelAxis.Items.Add(item);
            }
        }





        /// <summary>
        /// 设置限位限制界面输入
        /// </summary>
        private void SetLimitParams()
        {

            //var leftLimit = _stageControlEngine[CurrentStageAxis].GetSoftLeftLimit().Value;
            //var rightLimit = _stageControlEngine[CurrentStageAxis].GetSoftRightLimit().Value;
        }

        /// <summary>
        /// 更新界面上的Stage轴的移动参数
        /// </summary>
        void UpdateMotionParametersToUI()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateMotionParametersToUI));
                return;
            }

            EnumUnit unitType = EnumUnit.Millimeter;
            unitType = EnumUnit.Millimeter;
        }

        /// <summary>
        /// 更新值得单位:当前轴为Theta轴时单位为Degree，XYZ时为mm
        /// </summary>
        /// <param name="isThetaAxis"></param>
        void UpdateUnitToUI(bool isThetaAxis)
        {

        }





        private void btnApplySettings_Click_1(object sender, EventArgs e)
        {
            //保存到配置文件，并更新给下位
            var curAxisConfig=_hardwareConfig.StageConfig.GetAixsConfigByType(_currentStageAxis);
            curAxisConfig.AxisSpeed = Convert.ToDouble(this.seVelocity.Text.Trim());
            curAxisConfig.Acceleration = Convert.ToDouble(this.seAcceleration.Text.Trim());
            curAxisConfig.Deceleration = Convert.ToDouble(this.seDeceleration.Text.Trim());
            curAxisConfig.SoftLeftLimit = Convert.ToDouble(this.seSoftLeftLimit.Text.Trim());
            curAxisConfig.SoftRightLimit = Convert.ToDouble(this.seSoftRightLimit.Text.Trim());
            //curAxisConfig.KillDeceleration = Convert.ToDouble(this.seKillDeceleration.Text.Trim());
            _hardwareConfig.SaveConfig();

            MessageBox.Show("Apply OK!");

        }

        private void btnEnableAxis_Click(object sender, EventArgs e)
        {

        }

        private void btnDisableAxis_Click(object sender, EventArgs e)
        {

        }

        private void btnHomeAxis_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxSelAxis_SelectedValueChanged(object sender, EventArgs e)
        {
            var curAxis = (EnumStageAxis)Enum.Parse(typeof(EnumStageAxis), this.comboBoxSelAxis.SelectedItem.ToString());
            _currentStageAxis = curAxis;
            LoadAxisParameters();
        }
        private void LoadAxisParameters()
        {
            if (SystemConfiguration.Instance.SystemRunningType == EnumRunningType.Actual)
            {
                var curAxisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(_currentStageAxis);
                this.seVelocity.Text = curAxisConfig.AxisSpeed.ToString();
                this.seAcceleration.Text = curAxisConfig.Acceleration.ToString();
                this.seDeceleration.Text = curAxisConfig.Deceleration.ToString();
                this.seSoftLeftLimit.Text = curAxisConfig.SoftLeftLimit.ToString();
                this.seSoftRightLimit.Text = curAxisConfig.SoftRightLimit.ToString();
                this.seMaxSpeed.Text = curAxisConfig.MaxAxisSpeed.ToString();
                //this.seKillDeceleration.Text = curAxisConfig.KillDeceleration.ToString();
            }

        }

        private void StageAxisCtrlPanel_Load(object sender, EventArgs e)
        {
            //LoadAxisParameters();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            //保存到配置文件，并更新给下位
            var curAxisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(_currentStageAxis);
            curAxisConfig.AxisSpeed = Convert.ToDouble(this.seVelocity.Text.Trim());
            curAxisConfig.Acceleration = Convert.ToDouble(this.seAcceleration.Text.Trim());
            curAxisConfig.Deceleration = Convert.ToDouble(this.seDeceleration.Text.Trim());
            curAxisConfig.SoftLeftLimit = Convert.ToDouble(this.seSoftLeftLimit.Text.Trim());
            curAxisConfig.SoftRightLimit = Convert.ToDouble(this.seSoftRightLimit.Text.Trim());
            curAxisConfig.MaxAxisSpeed = Convert.ToDouble(this.seMaxSpeed.Text.Trim());
            //curAxisConfig.KillDeceleration = Convert.ToDouble(this.seKillDeceleration.Text.Trim());
            _hardwareConfig.SaveConfig();
            WarningBox.FormShow("完成","保存完成！","提示");
        }
    }
}
