﻿using System;
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

using CommonPanelClsLib;
using WestDragon.Framework.UtilityHelper;
using ConfigurationClsLib;
using PositioningSystemClsLib;
using GlobalToolClsLib;
using WestDragon.Framework.BaseLoggerClsLib;

namespace StageCtrlPanelLib
{
    /// <summary>
    /// Motion Control 单轴控制移动
    /// </summary>
    public partial class StageAxisMoveControlPanelSimple : BaseUserControl
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
        /// 构造函数
        /// </summary>
        public StageAxisMoveControlPanelSimple()
        {
            InitializeComponent();

            if (!CommonProcess.IsInDesigntime)
            {
                BindingEventHandlersAndInitGui();
            }
            foreach (var item in Enum.GetValues(typeof(EnumStageAxis)))
            {
                this.comboBoxSelAxis.Items.Add(item);
            }
            //LoadAxisParameters();
        }

        /// <summary>
        /// 绑定事件，并且初始化界面
        /// </summary>
        private void BindingEventHandlersAndInitGui()
        {

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
            //if (labelValueUnit1.InvokeRequired)
            //{
            //    this.Invoke(new Action<bool>(UpdateUnitToUI));
            //    return;
            //}

            //if (isThetaAxis)
            //{
            //    labelValueUnit1.Text = "degree";
            //    labelValueUnit2.Text = "degree";
            //}
            //else
            //{
            //    labelValueUnit1.Text = "mm";
            //    labelValueUnit2.Text = "mm";
            //}
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
            ReadAxis();
        }

        private void btnReadAxisStatus_Click(object sender, EventArgs e)
        {
            ReadAxis();
        }
        private void ReadAxis()
        {
            if (SystemConfiguration.Instance.SystemRunningType == EnumRunningType.Actual)
            {
                var pos = _positionSystem.ReadCurrentStagePosition();
                this.teCurrentPos.Text = pos[(int)_currentStageAxis].ToString("0.0000");
                this.seVelocity.Text = _positionSystem.GetAxisSpeed(_currentStageAxis).ToString("0.0000");
            }
        }

        private void btnOpenPPVacuum_Click(object sender, EventArgs e)
        {
            //HCFAMessageHelper.Instance.OpenPPVacuum();
        }

        private void btnClosePPVacuum_Click(object sender, EventArgs e)
        {
            //HCFAMessageHelper.Instance.ClosePPVacuum();
        }

        private void btnSetAxisVelocity_Click(object sender, EventArgs e)
        {
            var speed = 0f;
            if (float.TryParse(this.seVelocity.Text.Trim(), out speed))
            {
                if(_positionSystem.SetAxisSpeed(_currentStageAxis, speed))
                {
                    WarningBox.FormShow("成功", "设置速度完成。", "Success");
                }
            }
            else
            {
                WarningBox.FormShow("错误", "设置速度值无效。", "Error");
            }
        }


        private void btnAbsoluteMove_Click(object sender, EventArgs e)
        {
            try
            {
                var speed = 0f;
                if (float.TryParse(this.seVelocity.Text.Trim(), out speed))
                {
                    if (_positionSystem.SetAxisSpeed(_currentStageAxis, speed))
                    {
                        WarningBox.FormShow("成功", "设置速度完成。", "Success");
                    }
                }
                else
                {
                    WarningBox.FormShow("错误", "设置速度值无效。", "Error");
                }

                CreateWaitDialog();
                var target = 0d;
                if (double.TryParse(this.teAbsoluteMoveTarget.Text.Trim(), out target))
                {
                    _positionSystem.MoveAixsToStageCoord(_currentStageAxis, target, EnumCoordSetType.Absolute);
                }
                else
                {
                    CloseWaitDialog();
                    WarningBox.FormShow("错误", "绝对移动目标无效。", "Error");
                }
            }
            catch(Exception ex)
            {
                LogRecorder.RecordLog(EnumLogContentType.Error, $"AbsoluteMove,Error.Axis:{_currentStageAxis}", ex);
            }
            finally
            {
                ReadAxis();
                CloseWaitDialog();
            }
                
        }

        private void StageAxisAbsoluteMovePanel_Load(object sender, EventArgs e)
        {
            //ReadAxis();
        }

        private void btnRelaticeMove_Click(object sender, EventArgs e)
        {
            try
            {
                CreateWaitDialog();
                if (!string.IsNullOrEmpty(this.seMoveDistance.Text.Trim()))
                {
                    var distance = 0d;
                    if (double.TryParse(this.seMoveDistance.Text.Trim(), out distance))
                    {
                        _positionSystem.MoveAixsToStageCoord(_currentStageAxis, distance, EnumCoordSetType.Relative);
                        ReadAxis();
                    }
                    else
                    {
                        CloseWaitDialog();
                        MessageBox.Show("Parameter is invalid.");
                    }
                }
                else
                {
                    CloseWaitDialog();
                    MessageBox.Show("Parameter is invalid.");
                }
            }
            catch (Exception ex)
            {
                LogRecorder.RecordLog(EnumLogContentType.Error, $"ShellXMoveSpecDiatance,Error.", ex);
            }
            finally
            {
                ReadAxis();
                CloseWaitDialog();
            }
        }
    }
}
