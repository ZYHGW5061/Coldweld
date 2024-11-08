﻿using CommonPanelClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using StageCtrlPanelLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemCalibrationClsLib;
using VisionControlAppClsLib;
using VisionGUI;
using PowerControlGUI;
using ControlPanelClsLib;
using TransportPanelClsLib;
using ProductGUI;
using System.Threading;
using RecipeClsLib;
using JobClsLib;
using ControlPanelClsLib.Manual;
using PositioningSystemClsLib;
using BoardCardControllerClsLib;
using TemperatureControllerClsLib;
using VacuumGaugeControllerClsLib;
using TurboMolecularPumpControllerClsLib;
using CameraControllerClsLib;
using StageManagerClsLib;

namespace BondTerminal
{
    public partial class MainForm : BaseForm
    {

        #region File

        CameraWindowForm CameraForm;
        /// <summary>
        /// 当前日期时间
        /// </summary>
        private System.Timers.Timer _clockTimer;
        private System.Timers.Timer _refreshRunTimeSpanTimer = new System.Timers.Timer();

        private SynchronizationContext _syncContext;


        List<List<EnumMaterialproperties>> Materialproperties = new List<List<EnumMaterialproperties>>();

        List<EnumMaterialBoxproperties> MaterialBoxproperties = new List<EnumMaterialBoxproperties>();
        public string _selectedHeatRecipeName = "";


        /// <summary>
        /// 系统配置
        /// </summary>
        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }

        protected PositioningSystem _positionSystem
        {
            get
            {
                return PositioningSystem.Instance;
            }
        }


        IBoardCardController _boardCardController;
        private TemperatureControllerManager _TemperatureControllerManager
        {
            get { return TemperatureControllerManager.Instance; }
        }

        private VacuumGaugeControllerManager _VacuumGaugeControllerManager
        {
            get { return VacuumGaugeControllerManager.Instance; }
        }

        private TurboMolecularPumpControllerManager _TurboMolecularPumpControllerManager
        {
            get { return TurboMolecularPumpControllerManager.Instance; }
        }

        private CameraManager _CameraManager
        {
            get { return CameraManager.Instance; }
        }

        private StageManager _StageManager
        {
            get { return StageManager.Instance; }
        }





        public bool Isstop = false;



        private bool stageAxisIsconnect = false;
        private bool stageIOIsconnect = false;
        private bool cameraIsconnect = false;
        private bool temperatureIsconnect = false;
        private bool vacuumIsconnect = false;
        private bool turboMolecularPumpIsconnect = false;

        #endregion


        public MainForm()
        {
            InitializeComponent();
            GlobalCommFunc.MainForm = this;
            InitializeVisualForm();


            _syncContext = SynchronizationContext.Current;

            toolStripStatusLabelRunStatus.BackColor = (DataModel.Instance.Run ? true : false) ? Color.GreenYellow : Color.Transparent;
            toolStripStatusLabelError.BackColor = (DataModel.Instance.Error ? true : false) ? Color.Red : Color.GreenYellow;
            toolStripStatusLabelAxis.BackColor = (DataModel.Instance.StageAxisIsconnect ? true : false) ? Color.GreenYellow : Color.Red;
            toolStripStatusLabelIO.BackColor = (DataModel.Instance.StageIOIsconnect ? true : false) ? Color.GreenYellow : Color.Red;
            toolStripStatusLabelCamera.BackColor = (DataModel.Instance.CameraIsconnect ? true : false) ? Color.GreenYellow : Color.Red;
            toolStripStatusLabelTemperature.BackColor = (DataModel.Instance.TemperatureIsconnect ? true : false) ? Color.GreenYellow : Color.Red;
            toolStripStatusLabelVacuum.BackColor = (DataModel.Instance.VacuumIsconnect ? true : false) ? Color.GreenYellow : Color.Red;
            toolStripStatusLabelPump.BackColor = (DataModel.Instance.TurboMolecularPumpIsconnect ? true : false) ? Color.GreenYellow : Color.Red;


            DataModel.Instance.PropertyChanged += DataModel_PropertyChanged;

            dataGridView2.ReadOnly = true;
            dataGridView2.Enabled = false;


            _boardCardController = BoardCardManager.Instance.GetCurrentController();
        }

        private void InitializeVisualForm()
        {
            
            CameraForm = CameraWindowForm.Instance;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FrmInitialize startFrom = new FrmInitialize(this);
            if (startFrom.ShowDialog(this) == DialogResult.No)
            {
                Application.Exit();
                return;
            }
            //释放窗体资源
            startFrom.Dispose();
            startFrom = null;


            this._clockTimer = new System.Timers.Timer();
            this._clockTimer.Enabled = true;
            this._clockTimer.SynchronizingObject = this;
            this._clockTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.ClockTimerElapsedEventHandler);
            //定时刷新状态
            this.toolStripStatusLabelRunTime.Text = $"设备已运行：0 分钟";
            _refreshRunTimeSpanTimer.AutoReset = true;
            _refreshRunTimeSpanTimer.Interval = 60000;
            _refreshRunTimeSpanTimer.Elapsed += OnTimerElapsedEvt;
            _refreshRunTimeSpanTimer.Start();


        }

        /// <summary>
        /// 系统时间更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClockTimerElapsedEventHandler(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime time = DateTime.Now;
            this.toolStripStatusLabelNowTime.Text = time.ToShortDateString() + "  " + time.ToLongTimeString();
        }
        private int _systemRunTimeMin = 0;
        private void OnTimerElapsedEvt(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                _systemRunTimeMin++;
                this.toolStripStatusLabelRunTime.Text = $"设备已运行：{_systemRunTimeMin} 分钟";
            }
            finally
            {
            }
        }

        private void toolStripBtnStageControl_Click(object sender, EventArgs e)
        {
            FrmStageControl form = (Application.OpenForms["FrmStageControl"]) as FrmStageControl;
            if (form == null)
            {
                form = new FrmStageControl();
                form.Location = this.PointToScreen(new Point(500, 150));
                form.Owner = this.FindForm();
                //form.ShowLocation(new Point(500, 150));
                //lightform.StartPosition = FormStartPosition.CenterScreen;
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
            FrmStageAxisMoveControl form1 = (Application.OpenForms["FrmStageAxisMoveControl"]) as FrmStageAxisMoveControl;
            if (form1 == null)
            {
                form1 = new FrmStageAxisMoveControl();
                form1.Location = this.PointToScreen(new Point(500, 600));
                form1.ShowLocation(new Point(500, 600));
                form1.Owner = this.FindForm();
                form1.Show();
            }
            else
            {
                form1.Activate();
            }
        }


        private void toolStripBtnCameraControl_CheckedChanged(object sender, EventArgs e)
        {
            if (CameraForm != null)
            {
                int CurrentCameraNum = CameraWindowGUI.Instance.CurrentCameraNum;
                CameraWindowGUI.Instance.Size = new Size(1100, 900);
                CameraWindowGUI.Instance.SelectCamera(CurrentCameraNum);
                CameraWindowForm.Instance.Size = new System.Drawing.Size(1150, 950);
                CameraWindowForm.Instance.ShowLocation(new Point(300, 10));
                CameraWindowForm.Instance.ControlBox = true;
                //CameraForm.Owner = this.FindForm();
                CameraForm.Show();
            }
        }



        private void 手动校准ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemCalibration.Instance.ManualRun();
        }

        private void 自动校准ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemCalibration.Instance.MaterialHookReturnSafeLocation(2);
        }

        private void 系统初始化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string name = "搬送相机识别测试";
            //string title = "";
            //VisualMatchControlGUI visualMatch = new VisualMatchControlGUI();
            //visualMatch.InitVisualControl(CameraWindowGUI.Instance, SystemCalibration.Instance.TrackCameraVisual);

            //visualMatch.SetVisualParam(_systemConfig.SystemCalibrationConfig.BondIdentifyBondOrigionMatch);

            //int Done = SystemCalibration.Instance.ShowVisualForm(visualMatch, name, title);

            ////_systemConfig.SaveConfig();
            ////HardwareConfiguration.Instance.SaveConfig();
            ////SystemCalibration.Instance.Initialization();
        }

        private void toolStripBtnLightControl_CheckedChanged(object sender, EventArgs e)
        {
        }


        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                SingleStepRunUtility.Instance.Continue();
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            FrmIOtest form = (Application.OpenForms["FrmIOtest"]) as FrmIOtest;
            if (form == null)
            {
                form = new FrmIOtest();
                form.Location = this.PointToScreen(new Point(750, 750));
                form.Owner = this.FindForm();
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }

        private void iO测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void toolStripBtnCameraControl_Click(object sender, EventArgs e)
        {

        }

        private void toolStripBtnLightControl_Click(object sender, EventArgs e)
        {
            FrmLightControl form = (Application.OpenForms["FrmLightControl"]) as FrmLightControl;
            if (form == null)
            {
                form = new FrmLightControl();
                form.Location = this.PointToScreen(new Point(1350, 10));
                form.Owner = this.FindForm();
                //lightform.StartPosition = FormStartPosition.CenterScreen;
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    if(!DataModel.Instance.OvenBox1Function && !DataModel.Instance.OvenBox2Function && !DataModel.Instance.CondenserPump)
                    {
                        if (WarningBox.FormShow("确认关闭？", "确认退出软件？", "提示") == 0)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            e.Cancel = false;
                        }
                    }
                    else
                    {
                        if(DataModel.Instance.OvenBox1Function)
                        {
                            if (WarningBox.FormShow("确认关闭？", "请先停止烘箱1抽真空！停止分子泵！", "警告") == 0)
                            {
                                e.Cancel = false;
                            }
                            else
                            {
                                e.Cancel = false;
                            }
                        }
                        if (DataModel.Instance.OvenBox2Function)
                        {
                            if (WarningBox.FormShow("确认关闭？", "请先停止烘箱2抽真空！停止分子泵！", "警告") == 0)
                            {
                                e.Cancel = false;
                            }
                            else
                            {
                                e.Cancel = false;
                            }
                        }
                        if (DataModel.Instance.CondenserPump)
                        {
                            if (WarningBox.FormShow("确认关闭？", "请先停止方舱抽真空！停止冷凝泵！", "警告") == 0)
                            {
                                e.Cancel = false;
                            }
                            else
                            {
                                e.Cancel = false;
                            }
                        }


                    }
                    

                }
            }
            catch (Exception)
            {
            }

        }



        private void 运动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStageMaintain form = (Application.OpenForms["FrmStageMaintain"]) as FrmStageMaintain;
            if (form == null)
            {
                form = new FrmStageMaintain();
                form.Location = this.PointToScreen(new Point(500, 500));
                form.Owner = this.FindForm();
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }

        private void iOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmIOtest form = (Application.OpenForms["FrmIOtest"]) as FrmIOtest;
            if (form == null)
            {
                form = new FrmIOtest();
                form.Location = this.PointToScreen(new Point(500, 500));
                form.Owner = this.FindForm();
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }

        private void OvenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOvenBoxControl form = (Application.OpenForms["FrmOvenBoxControl"]) as FrmOvenBoxControl;
            if (form == null)
            {
                form = new FrmOvenBoxControl();
                form.Location = this.PointToScreen(new Point(500, 500));
                form.Owner = this.FindForm();
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }

        private void OvenHeatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTemperatureControlPanel form = (Application.OpenForms["FrmTemperatureControlPanel"]) as FrmTemperatureControlPanel;
            if (form == null)
            {
                form = new FrmTemperatureControlPanel();
                form.Location = this.PointToScreen(new Point(500, 500));
                form.Owner = this.FindForm();
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }

        private void TransportRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTransportRecipeEditor form = (Application.OpenForms["FrmTransportRecipeEditor"]) as FrmTransportRecipeEditor;
            if (form == null)
            {
                form = new FrmTransportRecipeEditor();
                form.Location = this.PointToScreen(new Point(500, 500));
                form.Owner = this.FindForm();
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }

        private void HeatRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHeatRecipeSelect form = (Application.OpenForms["FrmHeatRecipeSelect"]) as FrmHeatRecipeSelect;
            if (form == null)
            {
                form = new FrmHeatRecipeSelect();
                form.Location = this.PointToScreen(new Point(500, 500));
                form.Owner = this.FindForm();
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }

        private void ProductToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmProductMain form = (Application.OpenForms["FrmProductMainPanel"]) as FrmProductMain;
            if (form == null)
            {
                form = new FrmProductMain();
                form.Location = this.PointToScreen(new Point(1000, 500));
                form.Owner = this.FindForm();
                form.Show(this);
            }
            else
            {
                form.Activate();
            }

        }

        private void HeatRunToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolSafeButton1_Click(object sender, EventArgs e)
        {
            double X = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialboxX);
            double Y = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialboxY);

            if (Math.Abs(X) < 5 && Math.Abs(Y) < 5)
            {
                if (WarningBox.FormShow("错误！", "是否确认运动轴已经全部回零！", "警告") == 0)
                {

                }
                else
                {
                    return;
                }
            }

            SystemCalibration.Instance.MaterialBoxHookReturnSafeLocation();
            SystemCalibration.Instance.MaterialHookReturnSafeLocation();
        }
        private void toolHomeButton7_Click(object sender, EventArgs e)
        {
            SystemCalibration.Instance.ReturnHome();
        }

        #region 生产


        private void btnSelectTransportRecipe_Click(object sender, EventArgs e)
        {
            //LogRecorder.RecordLog(EnumLogContentType.Info, string.Format("JobControlPanel: User clicked <{0}> Button", (sender as Control).Text));
            //选择一个recipe
            FrmTransportRecipeEditor selectRecipeDialog = new FrmTransportRecipeEditor(null, this.teTransportRecipeName.Text.ToUpper().Trim());
            if (selectRecipeDialog.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                try
                {
                    _selectedHeatRecipeName = selectRecipeDialog.SelectedRecipeName;
                    //验证Recipe是否完整
                    if (!TransportRecipe.Validate(_selectedHeatRecipeName, selectRecipeDialog.RecipeType))
                    {
                        WarningBox.FormShow("错误！", "配方无效！", "提示");
                        return;
                    }
                    else
                    {
                        //var heatRecipe = TransportRecipe.LoadRecipe(_selectedHeatRecipeName, selectRecipeDialog.RecipeType);
                        teTransportRecipeName.Text = selectRecipeDialog.SelectedRecipeName;
                        var transportRecipe = TransportRecipe.LoadRecipe(selectRecipeDialog.SelectedRecipeName, selectRecipeDialog.RecipeType);
                        JobProcessControl.Instance.SetRecipe(transportRecipe);
                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(EnumLogContentType.Error, "JobControlPanel: Exception occured while Loading Recipe.", ex);
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                double X = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialboxX);
                double Y = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialboxY);

                if (Math.Abs(X) < 5 && Math.Abs(Y) < 5)
                {
                    if (WarningBox.FormShow("错误！", "是否确认运动轴已经全部回零！", "警告") == 0)
                    {
                        
                    }
                    else
                    {
                        return;
                    }
                }

                _selectedHeatRecipeName = teTransportRecipeName.Text;
                //验证Recipe是否完整
                if (!TransportRecipe.Validate(_selectedHeatRecipeName, EnumRecipeType.Transport))
                {
                    WarningBox.FormShow("错误！", "配方无效！", "提示");
                    return;
                }
                else
                {
                    var transportRecipe = TransportRecipe.LoadRecipe(teTransportRecipeName.Text, EnumRecipeType.Transport);
                    JobProcessControl.Instance.SetRecipe(transportRecipe);
                    JobProcessControl.Instance.Run();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnPausedOrSingle_Click(object sender, EventArgs e)
        {
            try
            {
                JobProcessControl.Instance.PausedOrSingle();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                JobProcessControl.Instance.Continue();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                JobProcessControl.Instance.Stop();
            }
            catch (Exception ex)
            {

            }
        }

        private void DataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            #region 生产状态


            if (e.PropertyName == nameof(DataModel.JobLogText))
            {
                _syncContext.Post(_ => teCurrentState.Text = DataModel.Instance.JobLogText, null);
                //UpdateLogSafely(DataModel.Instance.JobLogText);
            }
            if (e.PropertyName == nameof(DataModel.MaterialMat))
            {
                HandleMaterialMapLogChange(DataModel.Instance.MaterialMat);
            }
            //if (e.PropertyName == nameof(DataModel.MaterialBoxMapLog))
            //{
            //    HandleMaterialBoxMapLogChange(DataModel.Instance.MaterialBoxMapLog);
            //}
            if (e.PropertyName == nameof(DataModel.Ovennum))
            {
                UpdateNum(DataModel.Instance.Ovennum);
            }
            if (e.PropertyName == nameof(DataModel.Materialboxnum))
            {
                UpdateNum(DataModel.Instance.Materialboxnum);
            }
            if (e.PropertyName == nameof(DataModel.Materialnum))
            {
                UpdateNum(DataModel.Instance.Materialnum);
            }
            if (e.PropertyName == nameof(DataModel.Materialrow))
            {
                UpdateNum(DataModel.Instance.Materialrow);
            }
            if (e.PropertyName == nameof(DataModel.Materialcol))
            {
                UpdateNum(DataModel.Instance.Materialcol);
            }


            if (e.PropertyName == nameof(DataModel.BakeOvenDowntemp))
            {
                _syncContext.Post(_ => laOven1Temp.Text = DataModel.Instance.BakeOvenDowntemp.ToString(), null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2Downtemp))
            {
                _syncContext.Post(_ => laOven2Temp.Text = DataModel.Instance.BakeOven2Downtemp.ToString(), null);
            }

            if (e.PropertyName == nameof(DataModel.OvenBox1Heating))
            {
                if(DataModel.Instance.OvenBox1Heating)
                {
                    _syncContext.Post(_ => laOven1Heating.Text = "加热", null);
                    _syncContext.Post(_ => laOven1Heating.BackColor = Color.Red, null);
                }
                else
                {
                    _syncContext.Post(_ => laOven1Heating.Text = "待机", null);
                    _syncContext.Post(_ => laOven1Heating.BackColor = Color.Yellow, null);
                }
                
            }

            if (e.PropertyName == nameof(DataModel.OvenBox2Heating))
            {
                if (DataModel.Instance.OvenBox2Heating)
                {
                    _syncContext.Post(_ => laOven2Heating.Text = "加热", null);
                    _syncContext.Post(_ => laOven2Heating.BackColor = Color.Red, null);
                }
                else
                {
                    _syncContext.Post(_ => laOven2Heating.Text = "待机", null);
                    _syncContext.Post(_ => laOven2Heating.BackColor = Color.Yellow, null);
                }

            }

            if (e.PropertyName == nameof(DataModel.HeatPreservationResidueMinute))
            {
                _syncContext.Post(_ => laOven1Time.Text = DataModel.Instance.HeatPreservationResidueMinute.ToString(), null);
            }
            if (e.PropertyName == nameof(DataModel.HeatPreservationResidueMinute2))
            {
                _syncContext.Post(_ => laOven2Time.Text = DataModel.Instance.HeatPreservationResidueMinute2.ToString(), null);
            }

            if (e.PropertyName == nameof(DataModel.BakeOvenVacuum))
            {
                _syncContext.Post(_ => laOven1Vacuum.Text = DataModel.Instance.BakeOvenVacuum.ToString("E"), null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2Vacuum))
            {
                _syncContext.Post(_ => laOven2Vacuum.Text = DataModel.Instance.BakeOven2Vacuum.ToString("E"), null);
            }
            if (e.PropertyName == nameof(DataModel.BoxVacuum))
            {
                _syncContext.Post(_ => laBoxVacuum.Text = DataModel.Instance.BoxVacuum.ToString("E"), null);
            }


            if (e.PropertyName == nameof(DataModel.BakeOvenOuterdoorClosestatus))
            {
                if (DataModel.Instance.BakeOvenOuterdoorClosestatus)
                {
                    _syncContext.Post(_ => laOven1OutDoorSta.Text = "关", null);
                    _syncContext.Post(_ => laOven1OutDoorSta.BackColor = Color.Yellow, null);
                }
                else
                {
                    _syncContext.Post(_ => laOven1OutDoorSta.Text = "开", null);
                    _syncContext.Post(_ => laOven1OutDoorSta.BackColor = Color.Red, null);
                }

            }
            if (e.PropertyName == nameof(DataModel.BakeOven2OuterdoorClosestatus))
            {
                if (DataModel.Instance.BakeOven2OuterdoorClosestatus)
                {
                    _syncContext.Post(_ => laOven2OutDoorSta.Text = "关", null);
                    _syncContext.Post(_ => laOven2OutDoorSta.BackColor = Color.Yellow, null);
                }
                else
                {
                    _syncContext.Post(_ => laOven1OutDoorSta.Text = "开", null);
                    _syncContext.Post(_ => laOven1OutDoorSta.BackColor = Color.Red, null);
                }

            }
            if (e.PropertyName == nameof(DataModel.BoxOuterdoorClosetatus))
            {
                if (DataModel.Instance.BoxOuterdoorClosetatus)
                {
                    _syncContext.Post(_ => laBoxOutDoorSta.Text = "关", null);
                    _syncContext.Post(_ => laBoxOutDoorSta.BackColor = Color.Yellow, null);
                }
                else
                {
                    _syncContext.Post(_ => laBoxOutDoorSta.Text = "开", null);
                    _syncContext.Post(_ => laBoxOutDoorSta.BackColor = Color.Red, null);
                }

            }


            if (e.PropertyName == nameof(DataModel.BakeOvenPressureSensor))
            {
                if (DataModel.Instance.BakeOvenPressureSensor)
                {
                    _syncContext.Post(_ => laOven1Atmosphere.Text = "是", null);
                    _syncContext.Post(_ => laOven1Atmosphere.BackColor = Color.Yellow, null);
                }
                else
                {
                    _syncContext.Post(_ => laOven1Atmosphere.Text = "否", null);
                    _syncContext.Post(_ => laOven1Atmosphere.BackColor = Color.Red, null);
                }

            }
            if (e.PropertyName == nameof(DataModel.BakeOven2PressureSensor))
            {
                if (DataModel.Instance.BakeOven2PressureSensor)
                {
                    _syncContext.Post(_ => laOven2Atmosphere.Text = "是", null);
                    _syncContext.Post(_ => laOven2Atmosphere.BackColor = Color.Yellow, null);
                }
                else
                {
                    _syncContext.Post(_ => laOven2Atmosphere.Text = "否", null);
                    _syncContext.Post(_ => laOven2Atmosphere.BackColor = Color.Red, null);
                }

            }
            if (e.PropertyName == nameof(DataModel.BoxPressureSensor))
            {
                if (DataModel.Instance.BoxPressureSensor)
                {
                    _syncContext.Post(_ => laBoxAtmosphere.Text = "是", null);
                    _syncContext.Post(_ => laBoxAtmosphere.BackColor = Color.Yellow, null);
                }
                else
                {
                    _syncContext.Post(_ => laBoxAtmosphere.Text = "否", null);
                    _syncContext.Post(_ => laBoxAtmosphere.BackColor = Color.Red, null);
                }

            }

            if (e.PropertyName == nameof(DataModel.OvenBox1Function))
            {
                if (DataModel.Instance.OvenBox1Function)
                {
                    _syncContext.Post(_ => laOven1MolecularPump.Text = "运行", null);
                    _syncContext.Post(_ => laOven1MolecularPump.BackColor = Color.Red, null);
                }
                else
                {
                    _syncContext.Post(_ => laOven1MolecularPump.Text = "待机", null);
                    _syncContext.Post(_ => laOven1MolecularPump.BackColor = Color.Yellow, null);
                }

            }
            if (e.PropertyName == nameof(DataModel.OvenBox2Function))
            {
                if (DataModel.Instance.OvenBox1Function)
                {
                    _syncContext.Post(_ => laOven2MolecularPump.Text = "运行", null);
                    _syncContext.Post(_ => laOven2MolecularPump.BackColor = Color.Red, null);
                }
                else
                {
                    _syncContext.Post(_ => laOven2MolecularPump.Text = "待机", null);
                    _syncContext.Post(_ => laOven2MolecularPump.BackColor = Color.Yellow, null);
                }

            }
            if (e.PropertyName == nameof(DataModel.CondenserPump))
            {
                if (DataModel.Instance.CondenserPump)
                {
                    _syncContext.Post(_ => laBoxCondensatePump.Text = "运行", null);
                    _syncContext.Post(_ => laBoxCondensatePump.BackColor = Color.Red, null);
                }
                else
                {
                    _syncContext.Post(_ => laBoxCondensatePump.Text = "待机", null);
                    _syncContext.Post(_ => laBoxCondensatePump.BackColor = Color.Yellow, null);
                }

            }



            #endregion

            #region 统计




            #endregion

            #region 硬件状态

            if (e.PropertyName == nameof(DataModel.Linkstatusofmodules))
            {
                IODisconnect(DataModel.Instance.Linkstatusofmodules);
            }

            if (e.PropertyName == nameof(DataModel.Run))
            {
                _syncContext.Post(_ => toolStripStatusLabelRunStatus.BackColor = (DataModel.Instance.Run ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.Error))
            {
                _syncContext.Post(_ => toolStripStatusLabelError.BackColor = (DataModel.Instance.Error ? true : false) ? Color.Red : Color.GreenYellow, null);
            }

            if (e.PropertyName == nameof(DataModel.StageAxisIsconnect))
            {
                _syncContext.Post(_ => toolStripStatusLabelAxis.BackColor = (DataModel.Instance.StageAxisIsconnect ? true : false) ? Color.GreenYellow : Color.Red, null);
                StageAxisIsDisconnect(DataModel.Instance.StageAxisIsconnect);
            }
            if (e.PropertyName == nameof(DataModel.StageIOIsconnect))
            {
                _syncContext.Post(_ => toolStripStatusLabelIO.BackColor = (DataModel.Instance.StageIOIsconnect ? true : false) ? Color.GreenYellow : Color.Red, null);
                StageIOIsDisconnect(DataModel.Instance.StageIOIsconnect);
            }
            if (e.PropertyName == nameof(DataModel.CameraIsconnect))
            {
                _syncContext.Post(_ => toolStripStatusLabelCamera.BackColor = (DataModel.Instance.CameraIsconnect ? true : false) ? Color.GreenYellow : Color.Red, null);
                CameraIsDisconnect(DataModel.Instance.CameraIsconnect);
            }
            if (e.PropertyName == nameof(DataModel.TemperatureIsconnect))
            {
                _syncContext.Post(_ => toolStripStatusLabelTemperature.BackColor = (DataModel.Instance.TemperatureIsconnect ? true : false) ? Color.GreenYellow : Color.Red, null);
                TemperatureIsDisconnect(DataModel.Instance.TemperatureIsconnect);
            }
            if (e.PropertyName == nameof(DataModel.VacuumIsconnect))
            {
                _syncContext.Post(_ => toolStripStatusLabelVacuum.BackColor = (DataModel.Instance.VacuumIsconnect ? true : false) ? Color.GreenYellow : Color.Red, null);
                VacuumDisconnect(DataModel.Instance.VacuumIsconnect);
            }
            if (e.PropertyName == nameof(DataModel.TurboMolecularPumpIsconnect))
            {
                _syncContext.Post(_ => toolStripStatusLabelPump.BackColor = (DataModel.Instance.TurboMolecularPumpIsconnect ? true : false) ? Color.GreenYellow : Color.Red, null);
                PumpDisconnect(DataModel.Instance.TurboMolecularPumpIsconnect);
            }


            #endregion




        }

        private void StageAxisIsDisconnect(bool Done)
        {
            if (Done == false && stageAxisIsconnect)
            {
                stageAxisIsconnect = false;
                int ret = WarningBox.FormShow("错误！", "运动轴断开连接，是否重新连接运动轴", "提示");
                if(ret == 1)
                {
                    _StageManager.Shutdown();
                    _StageManager.InitializeAxis();

                    stageAxisIsconnect = _StageManager.ReadAxisConnectsta();
                }
                else
                {

                }
            }
        }

        private void StageIOIsDisconnect(bool Done)
        {
            if (Done == false && stageIOIsconnect)
            {
                stageIOIsconnect = false;
                int ret = WarningBox.FormShow("错误！", "IO模块断开连接，是否重新连接IO模块", "提示");
                if (ret == 1)
                {
                    _StageManager.InitializeIO();

                    stageIOIsconnect = _StageManager.ReadIOConnectsta();
                }
                else
                {

                }
            }
        }

        private void CameraIsDisconnect(bool Done)
        {
            if (Done == false && cameraIsconnect)
            {
                cameraIsconnect = false;
                int ret = WarningBox.FormShow("错误！", "相机断开连接，是否重新连接相机", "提示");
                if (ret == 1)
                {
                    _CameraManager.Shutdown();
                    _CameraManager.InitializeCameras();

                    cameraIsconnect = (_CameraManager.GetCameraByID(EnumCameraType.TrackCamera).IsConnect && _CameraManager.GetCameraByID(EnumCameraType.WeldCamera).IsConnect);
                }
                else
                {

                }
            }
        }

        private void TemperatureIsDisconnect(bool Done)
        {
            if (Done == false && temperatureIsconnect)
            {
                temperatureIsconnect = false;
                int ret = WarningBox.FormShow("错误！", "温控表断开连接，是否重新连接温控表", "提示");
                if (ret == 1)
                {
                    _TemperatureControllerManager.Shutdown();
                    _TemperatureControllerManager.Initialize();

                    temperatureIsconnect = (_TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).IsConnect && _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).IsConnect);

                }
                else
                {

                }
            }
        }

        private void VacuumDisconnect(bool Done)
        {
            if (Done == false && vacuumIsconnect)
            {
                vacuumIsconnect = false;
                int ret = WarningBox.FormShow("错误！", "真空计断开连接，是否重新连接真空计", "提示");
                if (ret == 1)
                {
                    _VacuumGaugeControllerManager.Shutdown();
                    _VacuumGaugeControllerManager.Initialize();

                    vacuumIsconnect = (_VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox1).IsConnect 
                        && _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.OvenBox2).IsConnect
                         && _VacuumGaugeControllerManager.GetVacuumGaugeController(EnumVacuumGaugeType.Box).IsConnect);

                }
                else
                {

                }
            }
        }

        private void PumpDisconnect(bool Done)
        {
            if (Done == false && turboMolecularPumpIsconnect)
            {
                turboMolecularPumpIsconnect = false;
                int ret = WarningBox.FormShow("错误！", "真空计断开连接，是否重新连接真空计", "提示");
                if (ret == 1)
                {
                    _TurboMolecularPumpControllerManager.Shutdown();
                    _TurboMolecularPumpControllerManager.Initialize();

                    turboMolecularPumpIsconnect = (_TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox1).IsConnect && _TurboMolecularPumpControllerManager.GetTurboMolecularPumpController(EnumTurboMolecularPumpType.OvenBox2).IsConnect);

                }
                else
                {

                }
            }
        }


        private void IODisconnect(bool Done)
        {
            if(Done == false)
            {
                WarningBox.FormShow("错误！", "IO模块断开连接，请重新启动程序", "提示");
            }
        }

        private void UpdateNum(int num)
        {
            if (teCurrentState.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                teCurrentState.Invoke(new Action(() => UpdateNum()));
            }
            else
            {
                UpdateNum();
            }
        }

        private void UpdateNum()
        {
            string str = $"当前烘箱：（ {DataModel.Instance.Ovennum} ）;"+
                $"当前料盘：（ {DataModel.Instance.Materialboxnum} ）;" +
                $"当前物料：（ {DataModel.Instance.Materialnum} ）;" +
                $"（ 行： {DataModel.Instance.Materialrow} " +
                $"列：{DataModel.Instance.Materialcol} ）;";
            this.teCurrentState.Text = str;
            this.teCurrentState.Refresh();
        }




        private void HandleMaterialMapLogChange(List<List<EnumMaterialproperties>> MaterialMat)
        {
            if (dataGridView2.InvokeRequired)
            {
                if (MaterialMat != null)
                {
                    dataGridView2.Invoke(new Action(() => InitMatrix0(MaterialMat)));
                }
                
            }
            else
            {
                if (MaterialMat != null)
                {
                    InitMatrix0(MaterialMat);
                }
            }
        }


        public void InitMatrix0(List<List<EnumMaterialproperties>> materialMat)
        {
            Materialproperties = materialMat;
            DataGridView2_Data();
        }


        public List<List<EnumMaterialproperties>> GetMatrix()
        {



            return Materialproperties;
        }

        private void DataGridView2_Data()
        {
            try
            {
                if (Materialproperties == null)
                {
                    return;
                }
                else
                {
                    if (Materialproperties.Count <= 0)
                    {
                        return;
                    }
                    else
                    {
                        if (Materialproperties[0].Count <= 0)
                        {
                            return;
                        }
                    }
                }

                int DataGridViewH = dataGridView2.Height;

                int rowIndex = Materialproperties.Count;

                int columnIndex = Materialproperties[0].Count;

                if (dataGridView2.InvokeRequired)
                {
                    dataGridView2.Invoke(new Action(() => {
                        dataGridView2.Rows.Clear();
                        dataGridView2.Columns.Clear();
                        dataGridView2.Dock = DockStyle.Fill;
                        //dataGridView2.Dock = DockStyle.Fill;
                        dataGridView2.AutoGenerateColumns = false;
                        dataGridView2.ColumnHeadersVisible = false;
                        dataGridView2.RowHeadersVisible = false;
                        //dataGridView2.ReadOnly = true;
                        //dataGridView2.Enabled = false;
                        dataGridView2.TabStop = false;

                        dataGridView2.ReadOnly = true;
                        dataGridView2.Enabled = false;

                        dataGridView2.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                        dataGridView2.MultiSelect = false;
                        for (int i = 0; i < rowIndex; i++)
                        {
                            dataGridView2.Columns.Add($"Column{i}", $"Column{i}");
                        }
                        for (int i = 0; i < columnIndex; i++)
                        {
                            dataGridView2.Rows.Add();
                            dataGridView2.Rows[i].Height = Convert.ToInt32((float)DataGridViewH / (float)columnIndex);
                        }
                        for (int i = 0; i < rowIndex; i++)
                        {
                            for (int j = 0; j < columnIndex; j++)
                            {
                                if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Welded)
                                    dataGridView2.Rows[i].Cells[j].Style.BackColor = Color.Green;
                                if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Jumping)
                                    dataGridView2.Rows[i].Cells[j].Style.BackColor = Color.Red;
                                if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Unwelded)
                                    dataGridView2.Rows[i].Cells[j].Style.BackColor = Color.White;
                                if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Totheweldingstation)
                                    dataGridView2.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                            }
                        }
                        dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView2.ClearSelection();
                        dataGridView2.CurrentCell = null;
                        dataGridView2.FirstDisplayedCell = null;
                        dataGridView2.Refresh();
                    }));
                }
                else
                {
                    dataGridView2.Rows.Clear();
                    dataGridView2.Columns.Clear();
                    dataGridView2.Dock = DockStyle.Fill;
                    dataGridView2.Dock = DockStyle.Fill;
                    dataGridView2.AutoGenerateColumns = false;
                    dataGridView2.ColumnHeadersVisible = false;
                    dataGridView2.RowHeadersVisible = false;
                    //dataGridView2.ReadOnly = true;
                    //dataGridView2.Enabled = false;
                    dataGridView2.TabStop = false;

                    dataGridView2.ReadOnly = true;
                    dataGridView2.Enabled = false;

                    dataGridView2.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                    dataGridView2.MultiSelect = false;
                    for (int i = 0; i < rowIndex; i++)
                    {
                        dataGridView2.Columns.Add($"Column{i}", $"Column{i}");
                    }
                    for (int i = 0; i < columnIndex; i++)
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Height = Convert.ToInt32((float)DataGridViewH / (float)columnIndex);
                    }
                    for (int i = 0; i < rowIndex; i++)
                    {
                        for (int j = 0; j < columnIndex; j++)
                        {
                            if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Welded)
                                dataGridView2.Rows[i].Cells[j].Style.BackColor = Color.Green;
                            if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Jumping)
                                dataGridView2.Rows[i].Cells[j].Style.BackColor = Color.Red;
                            if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Unwelded)
                                dataGridView2.Rows[i].Cells[j].Style.BackColor = Color.White;
                            if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Totheweldingstation)
                                dataGridView2.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                        }
                    }


                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.ClearSelection();
                    dataGridView2.CurrentCell = null;
                    dataGridView2.FirstDisplayedCell = null;
                    dataGridView2.Refresh();
                }

            }
            catch (Exception ex)
            {
            }
        }



        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 检查是否点击了有效的单元格（非标题行和列）
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cell = dataGridView2[e.ColumnIndex, e.RowIndex];
                cell.Style.BackColor = cell.Style.BackColor == Color.Red ? Color.White : Color.Red;

                Materialproperties[e.RowIndex][e.ColumnIndex].Materialstate = Materialproperties[e.RowIndex][e.ColumnIndex].Materialstate == EnumMaterialstate.Jumping ? EnumMaterialstate.Unwelded : EnumMaterialstate.Jumping;

                dataGridView2.ClearSelection();
                dataGridView2.CurrentCell = null;
                dataGridView2.FirstDisplayedCell = null;
            }
        }

        public void SetDataGridView2_Color(int rowIndex, int columnIndex, int COLOR)
        {
            try
            {
                if (rowIndex > Materialproperties.Count || columnIndex > Materialproperties[0].Count)
                    return;
                if (rowIndex > -1 && columnIndex > -1)
                {
                    if (COLOR == 1)
                    {
                        Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Welded;
                        dataGridView2.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Green;
                    }
                    if (COLOR == 2)
                    {
                        Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Jumping;
                        dataGridView2.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Red;
                    }
                    if (COLOR == 0)
                    {
                        Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Unwelded;
                        dataGridView2.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.White;
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private List<List<EnumMaterialproperties>> GetDataGridViewCellStates(DataGridView dataGridView)
        {
            try
            {
                int rowCount = dataGridView.RowCount;
                int columnCount = dataGridView.ColumnCount;
                int[,] cellStates = new int[rowCount, columnCount];

                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                    {
                        //int actualRowIndex = rowCount - 1 - rowIndex;
                        var cell = dataGridView[columnIndex, rowIndex];

                        if (cell.Style.BackColor == Color.Red)
                        {
                            Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Jumping;
                        }
                        else if (cell.Style.BackColor == Color.White)
                        {
                            Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Unwelded;
                        }
                        else
                        {
                            Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Welded;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }


            return Materialproperties;
        }






        #endregion

        private void 箱体控制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOvenBoxControl form = (Application.OpenForms["FrmOvenBoxControl"]) as FrmOvenBoxControl;
            if (form == null)
            {
                form = new FrmOvenBoxControl();
                form.Location = this.PointToScreen(new Point(500, 500));
                form.Owner = this.FindForm();
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }

        private void 测试按键ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemConfiguration.Instance.SaveConfig();
        }

        
        private void btnAllAxisStop_Click(object sender, EventArgs e)
        {
            //_positionSystem.StopAll();
            if(!Isstop)
            {
                _positionSystem.TriggerStop();
                Isstop = true;
                btnAllAxisStop.BackColor = Color.GreenYellow;
                btnAllAxisStop.Text = "接触急停";
            }
            else
            {
                _positionSystem.ReleaseStop();
                Isstop = false;
                btnAllAxisStop.BackColor = Color.Red;
                btnAllAxisStop.Text = "急停";
            }

        }

        private void 分子泵控制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTurboMolecularPumpControl form = (Application.OpenForms["FrmTurboMolecularPumpontrolPanel"]) as FrmTurboMolecularPumpControl;
            if (form == null)
            {
                form = new FrmTurboMolecularPumpControl();
                form.Location = this.PointToScreen(new Point(500, 500));
                form.Owner = this.FindForm();
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }

        private void 真空计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVacuumControl form = (Application.OpenForms["FrmVacuumControlPanel"]) as FrmVacuumControl;
            if (form == null)
            {
                form = new FrmVacuumControl();
                form.Location = this.PointToScreen(new Point(500, 500));
                form.Owner = this.FindForm();
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }
    }


}
