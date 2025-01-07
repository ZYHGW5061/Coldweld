using CommonPanelClsLib;
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
using WestDragon.Framework.BaseLoggerClsLib;
using IOUtilityClsLib;
using UserManagerClsLib;

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

        private BackgroundWorker backgroundWorker;


        List<List<EnumMaterialproperties>> Materialproperties = new List<List<EnumMaterialproperties>>();

        List<EnumMaterialBoxproperties> MaterialBoxproperties = new List<EnumMaterialBoxproperties>();
        public string _selectedHeatRecipeName = "";
        private DataTable previousDataTable;


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


            

            toolStripStatusLabelRunStatus.BackColor = (DataModel.Instance.Run ? true : false) ? Color.GreenYellow : Color.Transparent;
            toolStripStatusLabelError.BackColor = (DataModel.Instance.Error ? true : false) ? Color.Red : Color.GreenYellow;
            toolStripStatusLabelAxis.BackColor = (DataModel.Instance.StageAxisIsconnect ? true : false) ? Color.GreenYellow : Color.Red;
            toolStripStatusLabelIO.BackColor = (DataModel.Instance.StageIOIsconnect ? true : false) ? Color.GreenYellow : Color.Red;
            toolStripStatusLabelCamera.BackColor = (DataModel.Instance.CameraIsconnect ? true : false) ? Color.GreenYellow : Color.Red;
            toolStripStatusLabelTemperature.BackColor = (DataModel.Instance.TemperatureIsconnect ? true : false) ? Color.GreenYellow : Color.Red;
            toolStripStatusLabelVacuum.BackColor = (DataModel.Instance.VacuumIsconnect ? true : false) ? Color.GreenYellow : Color.Red;
            toolStripStatusLabelPump.BackColor = (DataModel.Instance.TurboMolecularPumpIsconnect ? true : false) ? Color.GreenYellow : Color.Red;


            DataModel.Instance.PropertyChanged += DataModel_PropertyChanged;
            _syncContext = SynchronizationContext.Current;

            dataGridView2.ReadOnly = true;
            dataGridView2.Enabled = false;


            _boardCardController = BoardCardManager.Instance.GetCurrentController();

            UpdateState();

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;

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

            Login login = new Login();
            if (login.ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
                return;
            }
            login.Dispose();
            login = null;

            UpdataUser();

            toolStrip1.Focus();

            //this._clockTimer = new System.Timers.Timer();
            //this._clockTimer.Enabled = true;
            //this._clockTimer.SynchronizingObject = this;
            //this._clockTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.ClockTimerElapsedEventHandler);
            ////定时刷新状态
            //this.toolStripStatusLabelRunTime.Text = $"设备已运行：0 分钟";
            //_refreshRunTimeSpanTimer.AutoReset = true;
            //_refreshRunTimeSpanTimer.Interval = 60000;
            //_refreshRunTimeSpanTimer.Elapsed += OnTimerElapsedEvt;
            //_refreshRunTimeSpanTimer.Start();


        }

        private void UpdataUser()
        {
            if (UserManager.Instance.CurrentUserType == 3 || UserManager.Instance.CurrentUserType == 2)
            {
                btnJump.Enabled = false;
                btnJump.Visible = false;
            }
            else if (UserManager.Instance.CurrentUserType == 1 || UserManager.Instance.CurrentUserType == 4)
            {
                btnJump.Enabled = true;
                btnJump.Visible = true;
            }
        }


        #region 下拉菜单

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


        private void iO测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void toolStripBtnCameraControl_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    if (!DataModel.Instance.OvenBox1Function && !DataModel.Instance.OvenBox2Function && !DataModel.Instance.CondenserPump)
                    {
                        if (WarningBox.FormShow("确认关闭？", "确认退出软件？", "提示") == 0)
                        {
                            SystemConfiguration.Instance.SaveConfig();
                            e.Cancel = true;
                        }
                        else
                        {
                            IOUtilityHelper.Instance.Stop();
                            DataModel.Instance.PropertyChanged -= DataModel_PropertyChanged;
                            SystemConfiguration.Instance.SaveConfig();
                            e.Cancel = false;
                        }
                    }
                    else
                    {
                        if (DataModel.Instance.OvenBox1Function)
                        {
                            if (WarningBox.FormShow("确认关闭？", "请先停止烘箱1抽真空！停止分子泵！", "警告") == 0)
                            {
                                if (WarningBox.FormShow("确认关闭？", "确认退出软件？", "提示") == 0)
                                {
                                    SystemConfiguration.Instance.SaveConfig();
                                    e.Cancel = true;
                                }
                                else
                                {
                                    IOUtilityHelper.Instance.Stop();
                                    DataModel.Instance.PropertyChanged -= DataModel_PropertyChanged;
                                    SystemConfiguration.Instance.SaveConfig();
                                    e.Cancel = false;
                                }
                            }
                            else
                            {
                                SystemConfiguration.Instance.SaveConfig();
                                e.Cancel = true;
                            }
                        }
                        if (DataModel.Instance.OvenBox2Function)
                        {
                            if (WarningBox.FormShow("确认关闭？", "请先停止烘箱2抽真空！停止分子泵！", "警告") == 0)
                            {
                                if (WarningBox.FormShow("确认关闭？", "确认退出软件？", "提示") == 0)
                                {
                                    SystemConfiguration.Instance.SaveConfig();
                                    e.Cancel = true;
                                }
                                else
                                {
                                    IOUtilityHelper.Instance.Stop();
                                    DataModel.Instance.PropertyChanged -= DataModel_PropertyChanged;
                                    SystemConfiguration.Instance.SaveConfig();
                                    e.Cancel = false;
                                }
                            }
                            else
                            {
                                SystemConfiguration.Instance.SaveConfig();
                                e.Cancel = true;
                            }
                        }
                        if (DataModel.Instance.CondenserPump)
                        {
                            if (WarningBox.FormShow("确认关闭？", "请先停止方舱抽真空！停止冷凝泵！", "警告") == 0)
                            {
                                if (WarningBox.FormShow("确认关闭？", "确认退出软件？", "提示") == 0)
                                {
                                    SystemConfiguration.Instance.SaveConfig();
                                    e.Cancel = true;
                                }
                                else
                                {
                                    IOUtilityHelper.Instance.Stop();
                                    DataModel.Instance.PropertyChanged -= DataModel_PropertyChanged;
                                    SystemConfiguration.Instance.SaveConfig();
                                    e.Cancel = false;
                                }
                            }
                            else
                            {
                                SystemConfiguration.Instance.SaveConfig();
                                e.Cancel = true;
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


        private void btnAllAxisStop_Click(object sender, EventArgs e)
        {
            //_positionSystem.StopAll();
            if (!Isstop)
            {
                _positionSystem.TriggerStop();
                Isstop = true;
                btnAllAxisStop.BackColor = Color.GreenYellow;
                btnAllAxisStop.Text = "解除急停";
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

        private void 焊接统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CountForm form = (Application.OpenForms["CountForm"]) as CountForm;
            if (form == null)
            {
                form = new CountForm();
                form.Location = this.PointToScreen(new Point(500, 500));
                form.Owner = this.FindForm();
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }

        #endregion

        #region 配方编程


        /// <summary>
        /// 配方编程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// 加热配方编程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        #endregion

        #region 快捷菜单

        /// <summary>
        /// IO控制弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 轴控制弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            //FrmStageAxisMoveControl form1 = (Application.OpenForms["FrmStageAxisMoveControl"]) as FrmStageAxisMoveControl;
            //if (form1 == null)
            //{
            //    form1 = new FrmStageAxisMoveControl();
            //    form1.Location = this.PointToScreen(new Point(500, 600));
            //    form1.ShowLocation(new Point(500, 600));
            //    form1.Owner = this.FindForm();
            //    form1.Show();
            //}
            //else
            //{
            //    form1.Activate();
            //}
        }

        /// <summary>
        /// 相机弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                CameraForm.Owner = this.FindForm();
                CameraForm.Show();
            }
        }

        /// <summary>
        /// 回空闲位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolSafeButton1_Click(object sender, EventArgs e)
        {
            double X = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialboxX);
            double Y = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialboxY);

            if (Math.Abs(X) < 5 && Math.Abs(Y) < 5)
            {
                if (WarningBox.FormShow("错误！", "是否确认运动轴已经全部回零！", "警告") == 0)
                {
                    return;
                }
                else
                {
                    
                }
            }

            SystemCalibration.Instance.MaterialBoxHookReturnSafeLocation();
            SystemCalibration.Instance.MaterialHookReturnSafeLocation();
        }

        /// <summary>
        /// 回零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolHomeButton7_Click(object sender, EventArgs e)
        {
            SystemCalibration.Instance.ReturnHome();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            if (login.ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
                return;
            }
            login.Dispose();
            login = null;

            UpdataUser();
        }

        /// <summary>
        /// 系统配置保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            int data = SystemCalibration.Instance.ShowMessage("保存", "是否保存系统配置?", "提示");
            if (data == 1)
            {
                SystemConfiguration.Instance.SaveConfig();
            }
            else
            {

            }
        }


        #endregion

        #region 生产

        private void StartTask(string taskName)
        {
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync(taskName);
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string taskName = e.Argument as string;

            // 根据参数执行不同的任务  
            if (taskName == "SelectTransportRecipe")
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
            else if (taskName == "Run")
            {
                try
                {
                    var runningType = SystemConfiguration.Instance.JobConfig.RunningType;
                    if (runningType == EnumRunningType.Actual)
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
            else if (taskName == "PausedOrSingle")
            {
                try
                {
                    JobProcessControl.Instance.PausedOrSingle();
                }
                catch (Exception ex)
                {

                }
            }
            else if (taskName == "Continue")
            {
                try
                {
                    JobProcessControl.Instance.Continue();
                }
                catch (Exception ex)
                {

                }
            }
            else if (taskName == "Stop")
            {
                try
                {
                    JobProcessControl.Instance.Stop();
                }
                catch (Exception ex)
                {

                }
            }
            else if (taskName == "Jump")
            {
                int Index = (int)numProcessIndex.Value;
                if (Index > -1 && Index < ProcesslistView.Items.Count)
                {
                    DataModel.Instance.ProcessIndex = Index;
                }
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("任务完成!");
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // 更新UI进度  
            Console.WriteLine($"进度: {e.ProgressPercentage}%");
        }


        private void btnSelectTransportRecipe_Click(object sender, EventArgs e)
        {
            //StartTask("SelectTransportRecipe");

            //LogRecorder.RecordLog(EnumLogContentType.Info, string.Format("JobControlPanel: User clicked <{0}> Button", (sender as Control).Text));
            //选择一个recipe
            FrmTransportRecipeEditor selectRecipeDialog = (Application.OpenForms["FrmTransportRecipeEditor"]) as FrmTransportRecipeEditor;
            if (selectRecipeDialog == null)
            {
                selectRecipeDialog = new FrmTransportRecipeEditor(null, this.teTransportRecipeName.Text.ToUpper().Trim());
                selectRecipeDialog.Location = this.PointToScreen(new Point(500, 500));
                selectRecipeDialog.Owner = this.FindForm();
                //selectRecipeDialog.Show(this);

                
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
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            //StartTask("Run");

            try
            {
                int data = SystemCalibration.Instance.ShowMessage("自动生产", $"是否开始自动生产?", "提示");
                if (data == 1)
                {
                    var runningType = SystemConfiguration.Instance.JobConfig.RunningType;
                    if (runningType == EnumRunningType.Actual)
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

                
            }
            catch (Exception ex)
            {

            }

        }

        private void btnPausedOrSingle_Click(object sender, EventArgs e)
        {
            //StartTask("PausedOrSingle");

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
            //StartTask("Continue");

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
            //StartTask("Stop");

            try
            {
                int data = SystemCalibration.Instance.ShowMessage("停止流程", $"是否停止流程?", "提示");
                if (data == 1)
                {
                    JobProcessControl.Instance.Stop();
                }

                
            }
            catch (Exception ex)
            {

            }

        }

        private void btnJump_Click(object sender, EventArgs e)
        {
            //StartTask("Jump");

            int Index = (int)numProcessIndex.Value;
            if (Index > -1 && Index < ProcesslistView.Items.Count)
            {
                int data = SystemCalibration.Instance.ShowMessage("跳步", $"是否跳转到第{Index}步?", "提示");
                if (data == 1)
                {
                    DataModel.Instance.ProcessIndex = Index;
                }
                
            }
        }

        private void numProcessIndex_ValueChanged(object sender, EventArgs e)
        {
            //if((int)numProcessIndex.Value > -1 && (int)numProcessIndex.Value < ProcesslistView.Items.Count)
            //{
            //    DataModel.Instance.ProcessIndex = (int)numProcessIndex.Value - 1;
            //}

        }


        private void UpdateJobLogText(string str)
        {
            if (teCurrentState.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                teCurrentState.Invoke(new Action(() => UpdateJobLogText1(str)
                )); ;
            }
            else
            {
                UpdateJobLogText1(str);
            }
        }

        private void UpdateJobLogText1(string str)
        {
            this.teCurrentState.Text = str;
            this.teCurrentState.Refresh();
        }

        private void UpdateState1()
        {
            if (teCurrentState1.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                teCurrentState1.Invoke(new Action(() => UpdateNum()));
            }
            else
            {
                UpdateNum();
            }
        }

        private void UpdateBakeOvenDowntemp()
        {
            if (laOven1Temp.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven1Temp.Invoke(new Action(() => 
                {
                    laOven1Temp.Text = DataModel.Instance.BakeOvenDowntemp.ToString();
                }));
            }
            else
            {
                laOven1Temp.Text = DataModel.Instance.BakeOvenDowntemp.ToString();
            }
        }

        private void UpdateBakeOven2Downtemp()
        {
            if (laOven2Temp.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven2Temp.Invoke(new Action(() =>
                {
                    laOven2Temp.Text = DataModel.Instance.BakeOven2Downtemp.ToString();
                }));
            }
            else
            {
                laOven2Temp.Text = DataModel.Instance.BakeOven2Downtemp.ToString();
            }
        }

        private void UpdateOvenBox1Heating()
        {
            if (laOven1Heating.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven1Heating.Invoke(new Action(() =>
                {
                    if (DataModel.Instance.BakeOvenAutoHeat)
                    {
                        laOven1Heating.Text = "加热";
                        laOven1Heating.BackColor = Color.Red;
                    }
                    else
                    {
                        laOven1Heating.Text = "待机";
                        laOven1Heating.BackColor = Color.Yellow;
                    }
                }));
            }
            else
            {
                if (DataModel.Instance.BakeOvenAutoHeat)
                {
                    laOven1Heating.Text = "加热";
                    laOven1Heating.BackColor = Color.Red;
                }
                else
                {
                    laOven1Heating.Text = "待机";
                    laOven1Heating.BackColor = Color.Yellow;
                }
            }
        }

        private void UpdateOvenBox2Heating()
        {
            if (laOven2Heating.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven2Heating.Invoke(new Action(() =>
                {
                    if (DataModel.Instance.BakeOven2AutoHeat)
                    {
                        laOven2Heating.Text = "加热";
                        laOven2Heating.BackColor = Color.Red;
                    }
                    else
                    {
                        laOven2Heating.Text = "待机";
                        laOven2Heating.BackColor = Color.Yellow;
                    }
                }));
            }
            else
            {
                if (DataModel.Instance.BakeOven2AutoHeat)
                {
                    laOven2Heating.Text = "加热";
                    laOven2Heating.BackColor = Color.Red;
                }
                else
                {
                    laOven2Heating.Text = "待机";
                    laOven2Heating.BackColor = Color.Yellow;
                }
            }
        }

        private void UpdateHeatPreservationResidueMinute()
        {
            if (laOven1Time.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven1Time.Invoke(new Action(() =>
                {
                    laOven1Time.Text = DataModel.Instance.HeatPreservationResidueMinute.ToString();
                }));
            }
            else
            {
                laOven1Time.Text = DataModel.Instance.HeatPreservationResidueMinute.ToString();
            }
        }

        private void UpdateHeatPreservationResidueMinute2()
        {
            if (laOven2Time.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven2Time.Invoke(new Action(() =>
                {
                    laOven2Time.Text = DataModel.Instance.HeatPreservationResidueMinute2.ToString();
                }));
            }
            else
            {
                laOven2Time.Text = DataModel.Instance.HeatPreservationResidueMinute2.ToString();
            }
        }

        private void UpdateBakeOvenVacuum()
        {
            if (laOven1Vacuum.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven1Vacuum.Invoke(new Action(() =>
                {
                    laOven1Vacuum.Text = DataModel.Instance.BakeOvenVacuum.ToString("E1");
                }));
            }
            else
            {
                laOven1Vacuum.Text = DataModel.Instance.BakeOvenVacuum.ToString("E1");
            }
        }
        private void UpdateBakeOven2Vacuum()
        {
            if (laOven2Vacuum.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven2Vacuum.Invoke(new Action(() =>
                {
                    laOven2Vacuum.Text = DataModel.Instance.BakeOven2Vacuum.ToString("E1");
                }));
            }
            else
            {
                laOven2Vacuum.Text = DataModel.Instance.BakeOven2Vacuum.ToString("E1");
            }
        }

        private void UpdateBoxVacuum()
        {
            if (laBoxVacuum.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laBoxVacuum.Invoke(new Action(() =>
                {
                    laBoxVacuum.Text = DataModel.Instance.BoxVacuum.ToString("E1");
                }));
            }
            else
            {
                laBoxVacuum.Text = DataModel.Instance.BoxVacuum.ToString("E1");
            }
        }

        private void UpdateBakeOvenOuterdoorClosestatus()
        {
            if (laOven1OutDoorSta.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven1OutDoorSta.Invoke(new Action(() =>
                {
                    if (DataModel.Instance.BakeOvenOuterdoorClosestatus)
                    {
                        laOven1OutDoorSta.Text = "关";
                        laOven1OutDoorSta.BackColor = Color.Yellow;
                    }
                    else
                    {
                        laOven1OutDoorSta.Text = "开";
                        laOven1OutDoorSta.BackColor = Color.Red;
                    }
                }));
            }
            else
            {
                if (DataModel.Instance.BakeOvenOuterdoorClosestatus)
                {
                    laOven1OutDoorSta.Text = "关";
                    laOven1OutDoorSta.BackColor = Color.Yellow;
                }
                else
                {
                    laOven1OutDoorSta.Text = "开";
                    laOven1OutDoorSta.BackColor = Color.Red;
                }
            }
        }

        private void UpdateBakeOven2OuterdoorClosestatus()
        {
            if (laOven2OutDoorSta.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven2OutDoorSta.Invoke(new Action(() =>
                {
                    if (DataModel.Instance.BakeOven2OuterdoorClosestatus)
                    {
                        laOven2OutDoorSta.Text = "关";
                        laOven2OutDoorSta.BackColor = Color.Yellow;
                    }
                    else
                    {
                        laOven2OutDoorSta.Text = "开";
                        laOven2OutDoorSta.BackColor = Color.Red;
                    }
                }));
            }
            else
            {
                if (DataModel.Instance.BakeOven2OuterdoorClosestatus)
                {
                    laOven2OutDoorSta.Text = "关";
                    laOven2OutDoorSta.BackColor = Color.Yellow;
                }
                else
                {
                    laOven2OutDoorSta.Text = "开";
                    laOven2OutDoorSta.BackColor = Color.Red;
                }
            }
        }

        private void UpdateBoxOuterdoorClosestatus()
        {
            if (laBoxOutDoorSta.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laBoxOutDoorSta.Invoke(new Action(() =>
                {
                    if (DataModel.Instance.BoxOuterdoorClosetatus)
                    {
                        laBoxOutDoorSta.Text = "关";
                        laBoxOutDoorSta.BackColor = Color.Yellow;
                    }
                    else
                    {
                        laBoxOutDoorSta.Text = "开";
                        laBoxOutDoorSta.BackColor = Color.Red;
                    }
                }));
            }
            else
            {
                if (DataModel.Instance.BoxOuterdoorClosetatus)
                {
                    laBoxOutDoorSta.Text = "关";
                    laBoxOutDoorSta.BackColor = Color.Yellow;
                }
                else
                {
                    laBoxOutDoorSta.Text = "开";
                    laBoxOutDoorSta.BackColor = Color.Red;
                }
            }
        }

        private void UpdateBakeOvenPressureSensor()
        {
            if (laOven1Atmosphere.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven1Atmosphere.Invoke(new Action(() =>
                {
                    if (DataModel.Instance.BakeOvenPressureSensor)
                    {
                        laOven1Atmosphere.Text = "是";
                        laOven1Atmosphere.BackColor = Color.Yellow;
                    }
                    else
                    {
                        laOven1Atmosphere.Text = "否";
                        laOven1Atmosphere.BackColor = Color.Red;
                    }
                }));
            }
            else
            {
                if (DataModel.Instance.BakeOvenPressureSensor)
                {
                    laOven1Atmosphere.Text = "是";
                    laOven1Atmosphere.BackColor = Color.Yellow;
                }
                else
                {
                    laOven1Atmosphere.Text = "否";
                    laOven1Atmosphere.BackColor = Color.Red;
                }
            }
        }

        private void UpdateBakeOven2PressureSensor()
        {
            if (laOven2Atmosphere.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven2Atmosphere.Invoke(new Action(() =>
                {
                    if (DataModel.Instance.BakeOven2PressureSensor)
                    {
                        laOven2Atmosphere.Text = "是";
                        laOven2Atmosphere.BackColor = Color.Yellow;
                    }
                    else
                    {
                        laOven2Atmosphere.Text = "否";
                        laOven2Atmosphere.BackColor = Color.Red;
                    }
                }));
            }
            else
            {
                if (DataModel.Instance.BakeOven2PressureSensor)
                {
                    laOven2Atmosphere.Text = "是";
                    laOven2Atmosphere.BackColor = Color.Yellow;
                }
                else
                {
                    laOven2Atmosphere.Text = "否";
                    laOven2Atmosphere.BackColor = Color.Red;
                }
            }
        }

        private void UpdateBoxPressureSensor()
        {
            if (laBoxAtmosphere.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laBoxAtmosphere.Invoke(new Action(() =>
                {
                    if (DataModel.Instance.BoxPressureSensor)
                    {
                        laBoxAtmosphere.Text = "是";
                        laBoxAtmosphere.BackColor = Color.Yellow;
                    }
                    else
                    {
                        laBoxAtmosphere.Text = "否";
                        laBoxAtmosphere.BackColor = Color.Red;
                    }
                }));
            }
            else
            {
                if (DataModel.Instance.BoxPressureSensor)
                {
                    laBoxAtmosphere.Text = "是";
                    laBoxAtmosphere.BackColor = Color.Yellow;
                }
                else
                {
                    laBoxAtmosphere.Text = "否";
                    laBoxAtmosphere.BackColor = Color.Red;
                }
            }
        }

        private void UpdateOvenBox1Function()
        {
            if (laOven1MolecularPump.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven1MolecularPump.Invoke(new Action(() =>
                {
                    if (DataModel.Instance.OvenBox1Function)
                    {
                        laOven1MolecularPump.Text = "运行";
                        laOven1MolecularPump.BackColor = Color.Red;
                    }
                    else
                    {
                        laOven1MolecularPump.Text = "待机";
                        laOven1MolecularPump.BackColor = Color.Yellow;
                    }

                }));
            }
            else
            {
                if (DataModel.Instance.OvenBox1Function)
                {
                    laOven1MolecularPump.Text = "运行";
                    laOven1MolecularPump.BackColor = Color.Red;
                }
                else
                {
                    laOven1MolecularPump.Text = "待机";
                    laOven1MolecularPump.BackColor = Color.Yellow;
                }
            }
        }

        private void UpdateOvenBox2Function()
        {
            if (laOven2MolecularPump.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laOven2MolecularPump.Invoke(new Action(() =>
                {
                    if (DataModel.Instance.OvenBox2Function)
                    {
                        laOven2MolecularPump.Text = "运行";
                        laOven2MolecularPump.BackColor = Color.Red;
                    }
                    else
                    {
                        laOven2MolecularPump.Text = "待机";
                        laOven2MolecularPump.BackColor = Color.Yellow;
                    }

                }));
            }
            else
            {
                if (DataModel.Instance.OvenBox2Function)
                {
                    laOven2MolecularPump.Text = "运行";
                    laOven2MolecularPump.BackColor = Color.Red;
                }
                else
                {
                    laOven2MolecularPump.Text = "待机";
                    laOven2MolecularPump.BackColor = Color.Yellow;
                }
            }
        }

        private void UpdateCondenserPump()
        {
            if (laBoxCondensatePump.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laBoxCondensatePump.Invoke(new Action(() =>
                {
                    if (DataModel.Instance.CondenserStar)
                    {
                        laBoxCondensatePump.Text = "运行";
                        laBoxCondensatePump.BackColor = Color.Red;
                    }
                    else
                    {
                        laBoxCondensatePump.Text = "待机";
                        laBoxCondensatePump.BackColor = Color.Yellow;
                    }
                }));
            }
            else
            {
                if (DataModel.Instance.CondenserPump)
                {
                    laBoxCondensatePump.Text = "运行";
                    laBoxCondensatePump.BackColor = Color.Red;
                }
                else
                {
                    laBoxCondensatePump.Text = "待机";
                    laBoxCondensatePump.BackColor = Color.Yellow;
                }
            }
        }

        private void UpdateWeldMaterialNumber()
        {
            if (laWeldMaterial.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laWeldMaterial.Invoke(new Action(() =>
                {
                    laWeldMaterial.Text = DataModel.Instance.WeldMaterialNumber.ToString();
                }));
            }
            else
            {
                laWeldMaterial.Text = DataModel.Instance.WeldMaterialNumber.ToString();
            }
        }

        private void UpdatePressWorkNumber()
        {
            if (laPressNum.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laPressNum.Invoke(new Action(() =>
                {
                    laPressNum.Text = DataModel.Instance.PressWorkNumber.ToString();
                }));
            }
            else
            {
                laPressNum.Text = DataModel.Instance.PressWorkNumber.ToString();
            }
        }

        private void UpdateEquipmentOperatingTime()
        {
            if (laRunTime.InvokeRequired)
            {
                // 使用 Invoke 来确保在 UI 线程上执行  
                laRunTime.Invoke(new Action(() =>
                {
                    laRunTime.Text = DataModel.Instance.EquipmentOperatingTime.ToString();
                }));
            }
            else
            {
                laRunTime.Text = DataModel.Instance.EquipmentOperatingTime.ToString();
            }
        }


        private void DataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(_syncContext == null)
            {
                return;
            }


            #region 生产状态


            if (e.PropertyName == nameof(DataModel.JobLogText))
            {
                _syncContext.Post(_ => teCurrentState.Text = DataModel.Instance.JobLogText, null);
                //UpdateLogSafely(DataModel.Instance.JobLogText);
                //UpdateJobLogText(DataModel.Instance.JobLogText);
            }
            if (e.PropertyName == nameof(DataModel.MaterialMat))
            {
                HandleMaterialMapLogChange(DataModel.Instance.MaterialMat);
            }
            if (e.PropertyName == nameof(DataModel.ProcessTable))
            {
                _syncContext.Post(_ => UpdateListView(DataModel.Instance.ProcessTable), null);
            }
            if (e.PropertyName == nameof(DataModel.ProcessIndex))
            {
                //_syncContext.Post(_ => numProcessIndex.Value = DataModel.Instance.ProcessIndex, null);
            }
            if (e.PropertyName == nameof(DataModel.Ovennum))
            {
                UpdateNum(DataModel.Instance.Ovennum);
                //UpdateState1();
            }
            if (e.PropertyName == nameof(DataModel.Materialboxnum))
            {
                UpdateNum(DataModel.Instance.Materialboxnum);
                //UpdateState1();
            }
            if (e.PropertyName == nameof(DataModel.Materialnum))
            {
                UpdateNum(DataModel.Instance.Materialnum);
                //UpdateState1();
            }
            if (e.PropertyName == nameof(DataModel.Materialrow))
            {
                UpdateNum(DataModel.Instance.Materialrow);
                //UpdateState1();
            }
            if (e.PropertyName == nameof(DataModel.Materialcol))
            {
                UpdateNum(DataModel.Instance.Materialcol);
                //UpdateState1();
            }


            if (e.PropertyName == nameof(DataModel.BakeOvenDowntemp))
            {
                _syncContext.Post(_ => laOven1Temp.Text = DataModel.Instance.BakeOvenDowntemp.ToString(), null);
                //UpdateBakeOvenDowntemp();
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2Downtemp))
            {
                _syncContext.Post(_ => laOven2Temp.Text = DataModel.Instance.BakeOven2Downtemp.ToString(), null);
                //UpdateBakeOven2Downtemp();
            }

            if (e.PropertyName == nameof(DataModel.BakeOvenAutoHeat))
            {
                if (DataModel.Instance.OvenBox1Heating)
                {
                    _syncContext.Post(_ => laOven1Heating.Text = "加热", null);
                    _syncContext.Post(_ => laOven1Heating.BackColor = Color.Red, null);
                }
                else
                {
                    _syncContext.Post(_ => laOven1Heating.Text = "待机", null);
                    _syncContext.Post(_ => laOven1Heating.BackColor = Color.Yellow, null);
                }
                //UpdateOvenBox1Heating();
            }

            if (e.PropertyName == nameof(DataModel.BakeOven2AutoHeat))
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
                //UpdateOvenBox2Heating();
            }

            if (e.PropertyName == nameof(DataModel.HeatPreservationResidueMinute))
            {
                _syncContext.Post(_ => laOven1Time.Text = DataModel.Instance.HeatPreservationResidueMinute.ToString(), null);
                //UpdateHeatPreservationResidueMinute();
            }
            if (e.PropertyName == nameof(DataModel.HeatPreservationResidueMinute2))
            {
                _syncContext.Post(_ => laOven2Time.Text = DataModel.Instance.HeatPreservationResidueMinute2.ToString(), null);
                //UpdateHeatPreservationResidueMinute2();
            }

            if (e.PropertyName == nameof(DataModel.BakeOvenVacuum))
            {
                _syncContext.Post(_ => laOven1Vacuum.Text = DataModel.Instance.BakeOvenVacuum.ToString("E1"), null);
                //UpdateBakeOvenVacuum();
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2Vacuum))
            {
                _syncContext.Post(_ => laOven2Vacuum.Text = DataModel.Instance.BakeOven2Vacuum.ToString("E1"), null);
                //UpdateBakeOven2Vacuum();
            }
            if (e.PropertyName == nameof(DataModel.BoxVacuum))
            {
                _syncContext.Post(_ => laBoxVacuum.Text = DataModel.Instance.BoxVacuum.ToString("E1"), null);
                //UpdateBoxVacuum();
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
                //UpdateBakeOvenOuterdoorClosestatus();
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
                //UpdateBakeOven2OuterdoorClosestatus();
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
                //UpdateBoxOuterdoorClosestatus();
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
                //UpdateBakeOvenPressureSensor();
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
                //UpdateBakeOven2PressureSensor();
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
                //UpdateBoxPressureSensor();
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
                //UpdateOvenBox1Function();
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2Function))
            {
                if (DataModel.Instance.OvenBox2Function)
                {
                    _syncContext.Post(_ => laOven2MolecularPump.Text = "运行", null);
                    _syncContext.Post(_ => laOven2MolecularPump.BackColor = Color.Red, null);
                }
                else
                {
                    _syncContext.Post(_ => laOven2MolecularPump.Text = "待机", null);
                    _syncContext.Post(_ => laOven2MolecularPump.BackColor = Color.Yellow, null);
                }
                //UpdateOvenBox2Function();
            }
            if (e.PropertyName == nameof(DataModel.CondenserStar))
            {
                if (DataModel.Instance.CondenserStar)
                {
                    _syncContext.Post(_ => laBoxCondensatePump.Text = "运行", null);
                    _syncContext.Post(_ => laBoxCondensatePump.BackColor = Color.Red, null);
                    LogRecorder.RecordLog(EnumLogContentType.Info, "冷凝泵启动");
                }
                else
                {
                    _syncContext.Post(_ => laBoxCondensatePump.Text = "待机", null);
                    _syncContext.Post(_ => laBoxCondensatePump.BackColor = Color.Yellow, null);
                    LogRecorder.RecordLog(EnumLogContentType.Info, "冷凝泵关闭");
                }
                //UpdateCondenserPump();
            }
            if (e.PropertyName == nameof(DataModel.CompressorAlarm))
            {
                if (DataModel.Instance.CompressorAlarm)
                {
                    LogRecorder.RecordLog(EnumLogContentType.Info, "压缩机报警");
                }
                else
                {
                    LogRecorder.RecordLog(EnumLogContentType.Info, "压缩机停止报警");
                }
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1Standbymode))
            {
                if (DataModel.Instance.OvenBox1Standbymode)
                {
                    LogRecorder.RecordLog(EnumLogContentType.Info, "烘箱A分子泵待机");
                }
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1Function))
            {
                if (DataModel.Instance.OvenBox1Function)
                {
                    LogRecorder.RecordLog(EnumLogContentType.Info, "烘箱A分子泵运行");
                }
            }
            if (e.PropertyName == nameof(DataModel.OvenBox1err))
            {
                if (DataModel.Instance.OvenBox1err)
                {
                    LogRecorder.RecordLog(EnumLogContentType.Info, "烘箱A分子泵报警");
                }
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2Standbymode))
            {
                if (DataModel.Instance.OvenBox2Standbymode)
                {
                    LogRecorder.RecordLog(EnumLogContentType.Info, "烘箱B分子泵待机");
                }
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2Function))
            {
                if (DataModel.Instance.OvenBox2Function)
                {
                    LogRecorder.RecordLog(EnumLogContentType.Info, "烘箱B分子泵运行");
                }
            }
            if (e.PropertyName == nameof(DataModel.OvenBox2err))
            {
                if (DataModel.Instance.OvenBox2err)
                {
                    LogRecorder.RecordLog(EnumLogContentType.Info, "烘箱B分子泵报警");
                }
            }



            #endregion

            #region 统计

            if (e.PropertyName == nameof(DataModel.Sysdatetime))
            {
                _syncContext.Post(_ => toolStripStatusLabelNowTime.Text = DataModel.Instance.Sysdatetime.ToString(), null);
                //UpdateWeldMaterialNumber();
            }

            if (e.PropertyName == nameof(DataModel.ThisEquipmentOperatingTime))
            {
                _syncContext.Post(_ => laRunTime.Text = "本次运行:" + DataModel.Instance.ThisEquipmentOperatingTime.ToString() + "分钟", null);
                //_syncContext.Post(_ => toolStripStatusLabelRunTime.Text = DataModel.Instance.EquipmentOperatingTime.ToString(), null);

                //UpdateEquipmentOperatingTime();
            }

            //if (e.PropertyName == nameof(DataModel.WeldMaterialNumber))
            //{
            //    _syncContext.Post(_ => laWeldMaterial.Text = DataModel.Instance.WeldMaterialNumber.ToString(), null);
            //    //UpdateWeldMaterialNumber();
            //}

            //if (e.PropertyName == nameof(DataModel.PressWorkNumber))
            //{
            //    _syncContext.Post(_ => laPressNum.Text = DataModel.Instance.PressWorkNumber.ToString(), null);
            //    //UpdatePressWorkNumber();
            //}

            //if (e.PropertyName == nameof(DataModel.EquipmentOperatingTime))
            //{
            //    _syncContext.Post(_ => laRunTime.Text = DataModel.Instance.EquipmentOperatingTime.ToString(), null);
            //    //_syncContext.Post(_ => toolStripStatusLabelRunTime.Text = DataModel.Instance.EquipmentOperatingTime.ToString(), null);

            //    //UpdateEquipmentOperatingTime();
            //}


            #endregion

            #region 硬件状态

            //if (e.PropertyName == nameof(DataModel.Linkstatusofmodules))
            //{
            //    IODisconnect(DataModel.Instance.Linkstatusofmodules);
            //}

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
                //StageAxisIsDisconnect(DataModel.Instance.StageAxisIsconnect);
            }
            if (e.PropertyName == nameof(DataModel.StageIOIsconnect))
            {
                _syncContext.Post(_ => toolStripStatusLabelIO.BackColor = (DataModel.Instance.StageIOIsconnect ? true : false) ? Color.GreenYellow : Color.Red, null);
                //StageIOIsDisconnect(DataModel.Instance.StageIOIsconnect);
            }
            if (e.PropertyName == nameof(DataModel.CameraIsconnect))
            {
                _syncContext.Post(_ => toolStripStatusLabelCamera.BackColor = (DataModel.Instance.CameraIsconnect ? true : false) ? Color.GreenYellow : Color.Red, null);
                //CameraIsDisconnect(DataModel.Instance.CameraIsconnect);
            }
            if (e.PropertyName == nameof(DataModel.TemperatureIsconnect))
            {
                _syncContext.Post(_ => toolStripStatusLabelTemperature.BackColor = (DataModel.Instance.TemperatureIsconnect ? true : false) ? Color.GreenYellow : Color.Red, null);
                //TemperatureIsDisconnect(DataModel.Instance.TemperatureIsconnect);
            }
            if (e.PropertyName == nameof(DataModel.VacuumIsconnect))
            {
                _syncContext.Post(_ => toolStripStatusLabelVacuum.BackColor = (DataModel.Instance.VacuumIsconnect ? true : false) ? Color.GreenYellow : Color.Red, null);
                //VacuumDisconnect(DataModel.Instance.VacuumIsconnect);
            }
            if (e.PropertyName == nameof(DataModel.TurboMolecularPumpIsconnect))
            {
                _syncContext.Post(_ => toolStripStatusLabelPump.BackColor = (DataModel.Instance.TurboMolecularPumpIsconnect ? true : false) ? Color.GreenYellow : Color.Red, null);
                //PumpDisconnect(DataModel.Instance.TurboMolecularPumpIsconnect);
            }


            #endregion




        }


        private void UpdateState()
        {
            #region 生产状态


            teCurrentState.Text = DataModel.Instance.JobLogText;
            laOven1Temp.Text = DataModel.Instance.BakeOvenDowntemp.ToString();
            laOven2Temp.Text = DataModel.Instance.BakeOven2Downtemp.ToString();

            if (DataModel.Instance.OvenBox1Heating)
            {
                laOven1Heating.Text = "加热";
                laOven1Heating.BackColor = Color.Red;
            }
            else
            {
                laOven1Heating.Text = "待机";
                laOven1Heating.BackColor = Color.Yellow;
            }


            if (DataModel.Instance.OvenBox2Heating)
            {
                laOven2Heating.Text = "加热";
                laOven2Heating.BackColor = Color.Red;
            }
            else
            {
                laOven2Heating.Text = "待机";
                laOven2Heating.BackColor = Color.Yellow;
            }


            laOven1Time.Text = DataModel.Instance.HeatPreservationResidueMinute.ToString();
            laOven2Time.Text = DataModel.Instance.HeatPreservationResidueMinute2.ToString();

            laOven1Vacuum.Text = DataModel.Instance.BakeOvenVacuum.ToString("E1");
            laOven2Vacuum.Text = DataModel.Instance.BakeOven2Vacuum.ToString("E1");
            laBoxVacuum.Text = DataModel.Instance.BoxVacuum.ToString("E1");

            if (DataModel.Instance.BakeOvenOuterdoorClosestatus)
            {
                laOven1OutDoorSta.Text = "关";
                laOven1OutDoorSta.BackColor = Color.Yellow;
            }
            else
            {
                laOven1OutDoorSta.Text = "开";
                laOven1OutDoorSta.BackColor = Color.Red;
            }
            if (DataModel.Instance.BakeOven2OuterdoorClosestatus)
            {
                laOven2OutDoorSta.Text = "关";
                laOven2OutDoorSta.BackColor = Color.Yellow;
            }
            else
            {
                laOven1OutDoorSta.Text = "开";
                laOven1OutDoorSta.BackColor = Color.Red;
            }
            if (DataModel.Instance.BoxOuterdoorClosetatus)
            {
                laBoxOutDoorSta.Text = "关";
                laBoxOutDoorSta.BackColor = Color.Yellow;
            }
            else
            {
                laBoxOutDoorSta.Text = "开";
                laBoxOutDoorSta.BackColor = Color.Red;
            }


            if (DataModel.Instance.BakeOvenPressureSensor)
            {
                laOven1Atmosphere.Text = "是";
                laOven1Atmosphere.BackColor = Color.Yellow;
            }
            else
            {
                laOven1Atmosphere.Text = "否";
                laOven1Atmosphere.BackColor = Color.Red;
            }
            if (DataModel.Instance.BakeOven2PressureSensor)
            {
                laOven2Atmosphere.Text = "是";
                laOven2Atmosphere.BackColor = Color.Yellow;
            }
            else
            {
                laOven2Atmosphere.Text = "否";
                laOven2Atmosphere.BackColor = Color.Red;
            }
            if (DataModel.Instance.BoxPressureSensor)
            {
                laBoxAtmosphere.Text = "是";
                laBoxAtmosphere.BackColor = Color.Yellow;
            }
            else
            {
                laBoxAtmosphere.Text = "否";
                laBoxAtmosphere.BackColor = Color.Red;
            }

            if (DataModel.Instance.OvenBox1Function)
            {
                laOven1MolecularPump.Text = "运行";
                laOven1MolecularPump.BackColor = Color.Red;
            }
            else
            {
                laOven1MolecularPump.Text = "待机";
                laOven1MolecularPump.BackColor = Color.Yellow;
            }
            if (DataModel.Instance.OvenBox2Function)
            {
                laOven2MolecularPump.Text = "运行";
                laOven2MolecularPump.BackColor = Color.Red;
            }
            else
            {
                laOven2MolecularPump.Text = "待机";
                laOven2MolecularPump.BackColor = Color.Yellow;
            }
            if (DataModel.Instance.CondenserPump)
            {
                laBoxCondensatePump.Text = "运行";
                laBoxCondensatePump.BackColor = Color.Red;
            }
            else
            {
                laBoxCondensatePump.Text = "待机";
                laBoxCondensatePump.BackColor = Color.Yellow;
            }



            #endregion

            #region 统计

            //laWeldMaterial.Text = DataModel.Instance.WeldMaterialNumber.ToString();

            //laPressNum.Text = DataModel.Instance.PressWorkNumber.ToString();

            //laRunTime.Text = DataModel.Instance.EquipmentOperatingTime.ToString();


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
            string str = $"当前烘箱：（ {DataModel.Instance.Ovennum} ）;" +
                $"当前料盘：（ {DataModel.Instance.Materialboxnum} ）;" +
                $"当前物料：（ {DataModel.Instance.Materialnum} ）;" +
                $"（ 行： {DataModel.Instance.Materialrow} " +
                $"列：{DataModel.Instance.Materialcol} ）;";
            //this.teCurrentState1.Text = str;
            _syncContext.Post(_ => teCurrentState1.Text = str, null);
            //if (teCurrentState.InvokeRequired)
            //{
            //    // 使用 Invoke 来确保在 UI 线程上执行  
            //    teCurrentState.Invoke(new Action(() => UpdateNum()));
            //}
            //else
            //{
            //    UpdateNum();
            //}
        }

        private void UpdateNum()
        {
            string str = $"当前烘箱：（ {DataModel.Instance.Ovennum} ）;"+
                $"当前料盘：（ {DataModel.Instance.Materialboxnum} ）;" +
                $"当前物料：（ {DataModel.Instance.Materialnum} ）;" +
                $"（ 行： {DataModel.Instance.Materialrow} " +
                $"列：{DataModel.Instance.Materialcol} ）;";
            this.teCurrentState1.Text = str;
            this.teCurrentState1.Refresh();
        }


        private void UpdateListView(DataTable newDataTable)
        {
            // 检查新旧DataTable的行数是否一致  
            if (previousDataTable == null || (newDataTable.Rows.Count != previousDataTable.Rows.Count) || previousDataTable.Rows.Count < 1 || DataModel.Instance.ProcessIndex == 0)
            {
                // 如果行数不一致，清空并重新填充ListView  
                ProcesslistView.Items.Clear();
                foreach (DataRow row in newDataTable.Rows)
                {
                    ListViewItem item = new ListViewItem("未执行");
                    //if ((ProcessTaskStatus)row["Status"] == ProcessTaskStatus.InProgress)
                    //{
                    //    item.SubItems.Add("正在进行");
                    //}
                    //else if ((ProcessTaskStatus)row["Status"] == ProcessTaskStatus.Completed)
                    //{
                    //    item.SubItems.Add("完成");
                    //}
                    //else if ((ProcessTaskStatus)row["Status"] == ProcessTaskStatus.NotExecuted)
                    //{
                    //    item.SubItems.Add("未执行");
                    //}
                    //else if ((ProcessTaskStatus)row["Status"] == ProcessTaskStatus.Paused)
                    //{
                    //    item.SubItems.Add("暂停");
                    //}
                    item.SubItems.Add(row["Index"].ToString());
                    item.SubItems.Add(row["Name"].ToString());

                    //// 根据状态设置颜色  
                    //switch ((ProcessTaskStatus)row["Status"])
                    //{
                    //    case ProcessTaskStatus.InProgress:
                    //        item.SubItems[0].ForeColor = Color.Yellow; // 进行中为黄色  
                    //        break;
                    //    case ProcessTaskStatus.Completed:
                    //        item.SubItems[0].ForeColor = Color.Green; // 已完成为绿色  
                    //        break;
                    //    case ProcessTaskStatus.Paused:
                    //        item.SubItems[0].ForeColor = Color.Red; // 已暂停为红色  
                    //        break;
                    //    case ProcessTaskStatus.NotExecuted:
                    //        item.SubItems[0].ForeColor = Color.Gray; // 未执行为灰色  
                    //        break;
                    //}

                    ProcesslistView.Items.Add(item);
                }
            }
            else
            {
                UpdateListViewItem(DataModel.Instance.ProcessIndex, newDataTable.Rows[DataModel.Instance.ProcessIndex]);
                if (DataModel.Instance.ProcessIndex > 0)
                {
                    UpdateListViewItem(DataModel.Instance.ProcessIndex - 1, newDataTable.Rows[DataModel.Instance.ProcessIndex - 1]);
                }
                if (DataModel.Instance.ProcessIndex < newDataTable.Rows.Count - 1)
                {
                    UpdateListViewItem(DataModel.Instance.ProcessIndex + 1, newDataTable.Rows[DataModel.Instance.ProcessIndex + 1]);
                }
            }
            previousDataTable = newDataTable;


        }
        private void AddListViewItem(DataRow row)
        {
            ListViewItem item = new ListViewItem(row["Status"].ToString());
            item.SubItems.Add(row["Index"].ToString());
            item.SubItems.Add(row["Name"].ToString());

            // 根据状态设置颜色  
            SetItemColor(item, (ProcessTaskStatus)row["Status"]);

            ProcesslistView.Items.Add(item);
        }
        // 更新ListView项的方法  
        private void UpdateListViewItem(int index, DataRow row)
        {
            if (index >= 0 && index < ProcesslistView.Items.Count)
            {
                ListViewItem item = ProcesslistView.Items[index];
                if((ProcessTaskStatus)row["Status"] == ProcessTaskStatus.InProgress)
                {
                    item.Text = "正在进行";
                }
                else if((ProcessTaskStatus)row["Status"] == ProcessTaskStatus.Completed)
                {
                    item.Text = "完成";
                }
                else if ((ProcessTaskStatus)row["Status"] == ProcessTaskStatus.NotExecuted)
                {
                    item.Text = "未执行";
                }
                else if ((ProcessTaskStatus)row["Status"] == ProcessTaskStatus.Paused)
                {
                    item.Text = "暂停";
                }

                item.SubItems[1].Text = row["Index"].ToString();
                item.SubItems[2].Text = row["Name"].ToString();

                // 根据状态设置颜色  
                SetItemColor(item, (ProcessTaskStatus)row["Status"]);
            }
        }

        // 设置ListViewItem颜色的方法  
        private void SetItemColor(ListViewItem item, ProcessTaskStatus status)
        {
            //switch (status)
            //{
            //    case ProcessTaskStatus.InProgress:
            //        item.SubItems[0].ForeColor = Color.Yellow; // 进行中为黄色  
            //        break;
            //    case ProcessTaskStatus.Completed:
            //        item.SubItems[0].ForeColor = Color.Green; // 已完成为绿色  
            //        break;
            //    case ProcessTaskStatus.Paused:
            //        item.SubItems[0].ForeColor = Color.Red; // 已暂停为红色  
            //        break;
            //    case ProcessTaskStatus.NotExecuted:
            //        item.SubItems[0].ForeColor = Color.Gray; // 未执行为灰色  
            //        break;
            //    default:
            //        item.SubItems[0].ForeColor = Color.White; // 默认颜色  
            //        break;
            //}
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

        
    }


}
