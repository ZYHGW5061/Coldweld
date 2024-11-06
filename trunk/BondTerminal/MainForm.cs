using BonderGUI.Forms;
using BonderGUI.Manual;
using CommonPanelClsLib;
using ConfigurationClsLib;
using ControlPanelClsLib.Recipe;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using StageCtrlPanelLib;
using MainGUI.Forms.ProductMenu;
using MainGUI.Forms.SysMenu;
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

namespace BondTerminal
{
    public partial class MainForm : BaseForm
    {

        #region File

        CameraWindowForm CameraForm;

        private bool toolStripBtnCameraControlChecked = false;
        private bool CameraWindowGUIInited = false;

        /// <summary>
        /// 系统配置
        /// </summary>
        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }

        private VisionControlAppClsLib.VisualControlManager _VisualManager
        {
            get { return VisionControlAppClsLib.VisualControlManager.Instance; }
        }
        public VisualControlApplications BondCameraVisual
        {
            get { return _VisualManager.GetCameraByID(EnumCameraType.BondCamera); }
        }
        public VisualControlApplications UplookingCameraVisual
        {
            get { return _VisualManager.GetCameraByID(EnumCameraType.UplookingCamera); }
        }
        public VisualControlApplications WaferCameraVisual
        {
            get { return _VisualManager.GetCameraByID(EnumCameraType.WaferCamera); }
        }

        #endregion


        public MainForm()
        {
            InitializeComponent();
            GlobalCommFunc.MainForm = this;
            InitializeVisualForm();
        }

        private void InitializeVisualForm()
        {
            
            CameraForm = CameraWindowForm.Instance;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            IOTestForm ioTestForm = new IOTestForm();
            ioTestForm.Show();
        }        

        private void MenuItemBondHeaderTest_Click(object sender, EventArgs e)
        {
            BondHeaderTest test = new BondHeaderTest();
            test.Show();
        }

        private void MenuItemTsTest_Click(object sender, EventArgs e)
        {
            TSTest test = new TSTest();
            test.Show();
        }

        private void MenuItemWaferTableTest_Click(object sender, EventArgs e)
        {
            WaferTableTest test = new WaferTableTest();
            test.Show();
        }

        private void MenuItemNeedleTest_Click(object sender, EventArgs e)
        {
            NeedleSysTest test = new NeedleSysTest();
            test.Show();
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
        }

        private void toolStripBtnStageControl_Click(object sender, EventArgs e)
        {
            FrmStageControl form = (Application.OpenForms["FrmStageControl"]) as FrmStageControl;
            if (form == null)
            {
                form = new FrmStageControl();
                form.Location = this.PointToScreen(new Point(1550, 150));
                form.Owner = this.FindForm();
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
                form1.Location = this.PointToScreen(new Point(1550, 500));
                //form1.ShowLocation(new Point(1550, 600));
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
                CameraWindowGUI.Instance.Size = new Size(909, 755);
                CameraWindowGUI.Instance.SelectCamera(CurrentCameraNum);
                CameraWindowForm.Instance.Size = new System.Drawing.Size(950, 800);
                CameraWindowForm.Instance.ShowLocation(new Point(100, 200));
                CameraWindowForm.Instance.ControlBox = true;
                //CameraForm.Owner = this.FindForm();
                CameraForm.Show();
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                CreateWaitDialog();
                FrmRecipePrograming form = (Application.OpenForms["FrmRecipePrograming"]) as FrmRecipePrograming;
                if (form == null)
                {
                    form = new FrmRecipePrograming();
                    //form.Location = this.PointToScreen(new Point(0, 350));
                    form.Location = new Point(100, 120);
                    form.Owner = this.FindForm();
                    form.Show(this);
                }
                else
                {
                    form.Activate();
                }

                
            }
            catch (Exception)
            {
            }
            finally
            {
                CloseWaitDialog();
            }
        }

        private void 手动校准ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemCalibration.Instance.ManualRun(1);
        }

        private void 自动校准ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SystemCalibration.Instance.AutoRun();
        }

        private void 系统初始化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //_systemConfig.SaveConfig();
            //HardwareConfiguration.Instance.SaveConfig();
            //SystemCalibration.Instance.Initialization();
        }

        private void toolStripBtnLightControl_CheckedChanged(object sender, EventArgs e)
        {
            //if (toolStripBtnLightControl.Checked)
            //{
            //    string name = "榜头相机识别";
            //    string title = "";
            //    VisualMatchControlGUI visualMatch = new VisualMatchControlGUI();

            //    visualMatch.InitVisualControl(CameraWindowGUI.Instance, BondCameraVisual);

            //    MatchIdentificationParam param = new MatchIdentificationParam();
            //    try
            //    {
            //        int REF = -1;
            //        using (VisualControlForm VForm = VisualControlForm.Instance)
            //        {
            //            VForm.InitializeGui(visualMatch);

            //            string hh = VForm.showMessage(Name, title, true);
            //            if (hh == "next")
            //            {
            //                REF = 1;
            //            }
            //            else
            //            {
            //                REF = 0;
            //            }
            //        }
            //        return REF;
            //    }
            //    catch
            //    {
            //        return -1;
            //    }
            //}
        }

        private void 新建ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProductConfigForm form = new ProductConfigForm();
            form.Show();
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
            IOTestForm ioTestForm = new IOTestForm();
            ioTestForm.Show();
        }

        private void iO测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IOTestForm ioTestForm = new IOTestForm();
            ioTestForm.Show();
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
                form.Location = this.PointToScreen(new Point(1250, 150));
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
                    if (WarningBox.FormShow("确认关闭？", "确认退出软件？", "提示") == 0)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
            }
            catch (Exception)
            {
            }

        }

        private void 共晶台测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PowerControlForm form = (Application.OpenForms["PowerControl"]) as PowerControlForm;
            if (form == null)
            {
                form = new PowerControlForm();
                form.Location = this.PointToScreen(new Point(700, 150));
                form.ShowLocation(new Point(700, 150));
                form.Owner = this.FindForm();
                //lightform.StartPosition = FormStartPosition.CenterScreen;
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }

        private void 单步ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmSingleStepRun form = new FrmSingleStepRun();
            FrmSingleStepRun form = (Application.OpenForms["FrmSingleStepRun"]) as FrmSingleStepRun;
            if (form == null)
            {
                form = new FrmSingleStepRun();
                //form.Location = this.PointToScreen(new Point(1550, 150));
                form.Owner = this.FindForm();
                //lightform.StartPosition = FormStartPosition.CenterScreen;
                form.Show(this);
            }
            else
            {
                form.Activate();
            }
        }

        private void pP工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPPTool frm = new FrmPPTool();
            frm.ShowDialog();
        }

        private void 顶针工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEjectionSystemTool frm = new FrmEjectionSystemTool();
            frm.ShowDialog();

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
            FrmIOMaintain form = (Application.OpenForms["FrmIOMaintain"]) as FrmIOMaintain;
            if (form == null)
            {
                form = new FrmIOMaintain();
                form.Location = this.PointToScreen(new Point(600, 500));
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
