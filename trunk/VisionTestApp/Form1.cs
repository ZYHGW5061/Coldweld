using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using CameraControllerClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using HardwareManagerClsLib;
using LightControllerClsLib;
using VisionClsLib;
using VisionControlAppClsLib;
using VisionGUI;

namespace VisionTestApp
{

    public partial class Form1 : Form
    {

        /// <summary>
        /// 页面是否初始化
        /// </summary>
        public bool IsIntialized = false;
        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }


        private bool _systemInitializeSuccess = false;
        private bool _ConnectPLCSuccess = false;
        private bool _ConnectStageSuccess = false;
        private bool _ConnectCameraSuccess = false;
        private bool _ConnectDarkFieldSuccess = false;
        private bool _ConnectBrightFieldSuccess = false;
        private bool _ConnectPowerControllerSuccess = false;
        private int _timeOut = 0;


        private float ImageWidth = 0;
        private float ImageHeight = 0;

        private RectangleF ROI = new RectangleF();

        CameraController SubstrateCameraApp = new CameraController();

        CameraController WaferCameraApp = new CameraController();

        CameraController UplookCameraApp = new CameraController();

        LightController light = LightController.Instance;

        CameraWindowGUI CameraWindow = new CameraWindowGUI(900, 680);

        //VisualControlApplications

        VisualAlgorithms Substratevisual = new VisualAlgorithms();

        VisualAlgorithms Wafervisual = new VisualAlgorithms();

        VisualAlgorithms Uplookvisual = new VisualAlgorithms();

        VisualControlApplications SubstrateVCApp;

        VisualControlApplications WaferVCApp;

        VisualControlApplications UplookVCApp;



        VisualMatchControlGUI SubstrateMatchGUI = new VisualMatchControlGUI();
        VisualLineFindControlGUI SubstrateLineFindGUI = new VisualLineFindControlGUI();
        VisualCircleFindControlGUI SubstrateCircleFindGUI = new VisualCircleFindControlGUI();

        VisualControlForm SubstrateMatchVForm;
        VisualControlForm SubstrateLineFindVForm;
        VisualControlForm SubstrateCircleFindVForm;

        CameraWindowForm CameraForm;



        public Form1()
        {
            InitializeComponent();

            Init();

            
        }
        ~Form1()
        {
            if (SubstrateCameraApp.IsConnected())
            {
                SubstrateCameraApp.DisConnect();
            }
            if (WaferCameraApp.IsConnected())
            {
                WaferCameraApp.DisConnect();
            }
            if (UplookCameraApp.IsConnected())
            {
                UplookCameraApp.DisConnect();
            }
        }

        private void Init()
        {
            try
            {
                //var systemConfig = SystemConfiguration.Instance;
                //var hardware = HardwareConfiguration.Instance;


                //Task.Factory.StartNew(new Action(() =>
                //{
                //    while (!this.Created) { continue; }
                //    //首先同步方式连接PLC，否则启动监控可能会报错
                //    //ConnectingPLC();
                //    //InitializeSystem();
                //    ////初始化硬件环境
                //    //ConnectingStage();
                //    //ConnectingDarkField();
                //    //ConnectingBrightField();
                //    //ConnectingPowerController();
                //    ConnectingCameras();

                //    _timeOut = 0;
                //    while (true)
                //    {
                //        if (_ConnectCameraSuccess)
                //        {
                //            this.Invoke((MethodInvoker)delegate
                //            {
                //                Thread.Sleep(500);
                //                this.Close();
                //            });
                //            break;
                //        }
                //        Thread.Sleep(100);
                //        _timeOut += 100;
                //        if (_timeOut >= 180000)
                //        {
                //            //this.Invoke(new Action(() => { this.btnExit.Visible = true; XtraMessageBox.Show("初始化超时!"); this.DialogResult = DialogResult.No; }));
                //            break;
                //        }
                //    }
                //}));



                //SubstrateCameraApp.CameraId = "192.168.1.96";
                SubstrateCameraApp.SetCameraID("192.168.1.96");

                //WaferCameraApp.CameraId = "192.168.1.97";
                WaferCameraApp.SetCameraID("192.168.1.97");

                //UplookCameraApp.CameraId = "192.168.1.98";
                UplookCameraApp.SetCameraID("192.168.1.98");

                bool Done1 = SubstrateCameraApp.Connect();

                bool Done2 = WaferCameraApp.Connect();

                bool Done3 = UplookCameraApp.Connect();

                CameraForm = new CameraWindowForm(CameraWindow);

                //CameraForm.InitVisualControl(SubstrateCameraApp, WaferCameraApp, UplookCameraApp);


                light.LightConnect("COM6");

                Substratevisual.Init();
                Wafervisual.Init();
                Uplookvisual.Init();


                SubstrateVCApp = new VisualControlApplications(SubstrateCameraApp, light, DirectLightNum.SubstrateDirectLight, RingLightNum.SubstrateRingLight, Substratevisual);

                WaferVCApp = new VisualControlApplications(WaferCameraApp, light, DirectLightNum.WaferDirectLight, RingLightNum.WaferRingLight, Wafervisual);

                UplookVCApp = new VisualControlApplications(UplookCameraApp, light, DirectLightNum.UplookDirectLight, RingLightNum.UplookRingLight, Uplookvisual);

                SubstrateVCApp.ImageShow = true;

                SubstrateMatchGUI.InitVisualControl(CameraWindow, SubstrateVCApp);
                SubstrateLineFindGUI.InitVisualControl(CameraWindow, SubstrateVCApp);
                SubstrateCircleFindGUI.InitVisualControl(CameraWindow, SubstrateVCApp);

                SubstrateMatchGUI.InitForm();
                SubstrateLineFindGUI.InitForm();
                SubstrateCircleFindGUI.InitForm();

                SubstrateMatchVForm = new VisualControlForm(SubstrateMatchGUI,"", "");
                SubstrateLineFindVForm = new VisualControlForm(SubstrateLineFindGUI, "", "");
                SubstrateCircleFindVForm = new VisualControlForm(SubstrateCircleFindGUI, "", "");

            }
            catch (Exception ex)
            {

            }

        }

        private void ConnectingCameras()
        {
            #region Connecting Camera
            Task.Factory.StartNew(new Action(() =>
            {
                bool success = true;
                //连接相机
                try
                {
                    //SetProgressCaption(string.Format("Connecting Inspection Cameras..."), labelLineCameraText);
                    if (_systemConfig.SystemRunningType == EnumRunningType.Actual)
                    {
                        HardwareManager.Instance.ConnectHardware(EnumHardwareType.Camera);
                        success = HardwareManager.Instance.CheckCameraEngineValid();
                        _ConnectCameraSuccess = success;
                        if (success)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                //SetProgressCaption("相机初始化完成。", labelRunningType);
                            });
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            //SetProgressCaption("相机初始化完成。", labelRunningType);
                        });
                        _ConnectCameraSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    //_systemLogger.AddErrorContent("Failed to connect Inspection Cameras", ex);
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Failed to connect Cameras.", ex);
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        this.Invoke(new Action(() => {
                            //SetProgressCaption("相机初始化失败!", labelFailInfo);
                            //this.labelFailInfo.Visible = true; this.btnExit.Visible = true;
                        }));
                    }
                }
                if (!success) { return; }
            }));
            #endregion                                                                                                                                       
        }


        private void CameraFormShowBtn()
        {
            if (CameraForm != null)
            {
                if (CameraWindowToolStripMenuItem.Checked)
                {
                    CameraForm.CameraShow(true);
                    CameraForm.Show();
                }
                else
                {
                    CameraForm.CameraShow(false);
                    CameraForm.Hide();
                }
            }
        }

        private void MatchBtn()
        {

            if (BondingMatchToolStripMenuItem.Checked)
            {
                if (BondingLineToolStripMenuItem.Checked)
                {
                    BondingLineToolStripMenuItem.Checked = false;

                }
                if (BondingCircleToolStripMenuItem.Checked)
                {
                    BondingCircleToolStripMenuItem.Checked = false;

                }
                if (SubstrateMatchVForm != null)
                    SubstrateMatchVForm.Show();
            }
            else
            {
                SubstrateMatchVForm.Hide();
            }
        }

        private void LineFindBtn()
        {
            if (BondingLineToolStripMenuItem.Checked)
            {
                if (BondingMatchToolStripMenuItem.Checked)
                {
                    BondingMatchToolStripMenuItem.Checked = false;

                }
                if (BondingCircleToolStripMenuItem.Checked)
                {
                    BondingCircleToolStripMenuItem.Checked = false;

                }
                if (SubstrateLineFindVForm != null)
                    SubstrateLineFindVForm.Show();
            }
            else
            {
                SubstrateLineFindVForm.Hide();
            }
        }

        private void CircleFindBtn()
        {
            if (BondingCircleToolStripMenuItem.Checked)
            {
                if (BondingMatchToolStripMenuItem.Checked)
                {
                    BondingMatchToolStripMenuItem.Checked = false;

                }
                if (BondingLineToolStripMenuItem.Checked)
                {
                    BondingLineToolStripMenuItem.Checked = false;

                }
                if (SubstrateCircleFindVForm != null)
                    SubstrateCircleFindVForm.Show();
            }
            else
            {
                SubstrateCircleFindVForm.Hide();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (SubstrateCameraApp.IsConnected())
            {
                SubstrateCameraApp.DisConnect();
            }
            if (WaferCameraApp.IsConnected())
            {
                WaferCameraApp.DisConnect();
            }
            if (UplookCameraApp.IsConnected())
            {
                UplookCameraApp.DisConnect();
            }
        }

        private void CameraWindowToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            this.SendToBack();
            CameraFormShowBtn();
            //this.BringToFront();
        }

        private void BondingMatchToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            this.SendToBack();
            MatchBtn();
            //this.BringToFront();
        }

        private void BondingLineToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            this.SendToBack();
            LineFindBtn();
            //this.BringToFront();
        }

        private void BondingCircleToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            this.SendToBack();
            CircleFindBtn();
            //this.BringToFront();
        }
    }
}
