using DevExpress.XtraEditors;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CommonPanelClsLib;
using ConfigurationClsLib;
using HardwareManagerClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;

using VisionControlAppClsLib;
using VisionGUI;

namespace BondTerminal
{
    public partial class FrmInitialize : BaseForm
    {
        /// <summary>
        /// 页面是否初始化
        /// </summary>
        public bool IsIntialized = false;
        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public FrmInitialize(Form parent)
        {
            InitializeComponent();
        }

        private bool _systemInitializeSuccess = false;
        private bool _ConnectPLCSuccess = false;
        private bool _ConnectStageSuccess = false;
        private bool _ConnectCameraSuccess = false;
        private bool _ConnectLightControllerSuccess = false;
        private bool _ConnectBrightFieldSuccess = false;
        private bool _ConnectPowerControllerSuccess = false;
        private bool _VisualControlInitializeSuccess = false;
        private bool _ConnectLaserSensorSuccess = false;
        private bool _ConnectDynamometerSuccess = false;
        private bool _ConnectTemperatureControllerSuccess = false;
        private bool _ConnectTurboMolecularPumpControllerSuccess = false;
        private bool _ConnectVacuumGaugeControllerSuccess = false;
        private bool _ConnectDewPointMeterControllerSuccess = false;
        private bool _ConnectPressureSensorControllerSuccess = false;


        public void SetProgressCaption(string caption, LabelControl labText)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { SetProgressCaption(caption, labText); });
                return;
            }

            labText.Text = caption;
            this.Update();
            Application.DoEvents();
        }

        private int _timeOut = 0;

        /// <summary>
        /// 初次Load时执行
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                //this.labelRunningType.Text = _systemConfig.SystemRunningType == EnumRunningType.Actual ? "" : "离线模拟运行模式";
                this.ControlBox = false;
                this.FormBorderStyle = FormBorderStyle.None;
                Task.Factory.StartNew(new Action(() =>
                {
                    while (!this.Created) { continue; }
                    InitializeSystem();
                    //初始化硬件环境
                    ConnectingStage();
                    //ConnectingLightController();
                    //ConnectingPowerController();
                    ConnectingTemperatureController();
                    ConnectingTurboMolecularPumpController();
                    ConnectingVacuumGaugeController();
                    //ConnectingDewPointMeterController();
                    //ConnectingPressureSensorController();
                    ConnectingCameras();
                    //ConnectingLaserSensorController();
                    //ConnectingDynamometerController();
                    InitializeViualControl();



                    //_ConnectCameraSuccess = true;
                    _ConnectLightControllerSuccess = true;
                    //_ConnectStageSuccess = true;
                    //_ConnectPowerControllerSuccess = true;
                    _VisualControlInitializeSuccess = true;
                    _ConnectTemperatureControllerSuccess = true;
                    _ConnectTurboMolecularPumpControllerSuccess = true;
                    _ConnectVacuumGaugeControllerSuccess = true;
                    //_ConnectDewPointMeterControllerSuccess = true;
                    _ConnectPressureSensorControllerSuccess = true;


                    _timeOut = 0;
                    while (true)
                    {
                        if (_systemInitializeSuccess && _ConnectCameraSuccess && _ConnectLightControllerSuccess && _ConnectStageSuccess && _VisualControlInitializeSuccess 
                        && _ConnectTemperatureControllerSuccess && _ConnectTurboMolecularPumpControllerSuccess && _ConnectVacuumGaugeControllerSuccess && _ConnectPressureSensorControllerSuccess)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                Thread.Sleep(500);
                                this.Close();
                            });
                            break;
                        }
                        Thread.Sleep(100);
                        _timeOut += 100;
                        if (_timeOut >= 180000)
                        {
                            this.Invoke(new Action(() => { this.btnExit.Visible = true; XtraMessageBox.Show("初始化超时!"); this.DialogResult = DialogResult.No; }));
                            break;
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "StartSystem Error.", ex);
            }
            finally
            {
                base.OnLoad(e);
            }
        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason != CloseReason.UserClosing)
                {
                    HardwareManager.Instance.DisconnectHardwares();
                }
            }
            catch (Exception ex)
            {
                //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "DisconnectHardwares Error.", ex);
            }
            finally
            {
                base.OnFormClosing(e);
            }
        }

        private async void InitializeSystem()
        {
            await Task.Run(() =>
            {
                bool successful = true;
                try
                {
                    IOManager.Instance.Initialize();
                    //var instance=UserManager.Instance;
                    _systemInitializeSuccess = true;
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetProgressCaption("系统配置初始化完成。", labelRunningType);
                    });
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "InitializeSystem Error.", ex);
                    successful = false;
                }
                if (!successful)
                {
                    this.Invoke(new Action(() => { SetProgressCaption("系统配置初始化失败!", labelFailInfo); this.labelFailInfo.Visible = true; this.btnExit.Visible = true; }));
                }
            });
        }

        private void InitializeViualControl()
        {
            #region ViualControl
            Task.Factory.StartNew(new Action(() =>
            {
                bool success = true;
                //连接相机
                try
                {
                    ////SetProgressCaption(string.Format("Connecting Inspection Cameras..."), labelLineCameraText);
                    //if (_systemConfig.SystemRunningType == EnumRunningType.Actual)
                    //{



                    while (_ConnectCameraSuccess == false || _systemInitializeSuccess == false)
                    {
                        Thread.Sleep(100);
                        _timeOut += 100;
                        if (_timeOut > 180000)
                        {
                            break;
                        }

                    }

                    success = VisualControlManager.Instance.InitializeVisualControls();
                    
                    if (success)
                    {
                        VisualControlGuiManger.Instance.Initialize();
                        this.Invoke((MethodInvoker)delegate
                        {
                            SetProgressCaption("视觉初始化完成。", labelRunningType);
                        });
                    }

                    _VisualControlInitializeSuccess = success;
                    //}
                    //else
                    //{
                    //    this.Invoke((MethodInvoker)delegate
                    //    {
                    //        SetProgressCaption("算法初始化完成。", labelRunningType);
                    //    });
                    //    _ConnectCameraSuccess = true;
                    //}
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Failed to connect Cameras.", ex);
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        this.Invoke(new Action(() => {
                            SetProgressCaption("视觉初始化失败!", labelFailInfo);
                            this.labelFailInfo.Visible = true; this.btnExit.Visible = true;
                        }));
                    }
                }
                if (!success) { return; }
            }));
            #endregion                                                                                                                                       

        }

        private void ConnectingStage()
        {
            #region Connecting Stage
            Task.Factory.StartNew(new Action(() =>
            {
                bool success = true;
                //连接Stage
                try
                {
                    HardwareManager.Instance.ConnectHardware(EnumHardwareType.Stage);
                    _ConnectStageSuccess = success;
                    if (success)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            SetProgressCaption("Stage初始化完成。", labelRunningType);
                        });
                    }

                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Failed to connect Stage.", ex);
                    success = false;
                }
                finally
                {
                    this.Invoke(new Action(() =>
                    {
                        if (!success)
                        {
                            SetProgressCaption("Stage初始化失败!", labelFailInfo);
                            this.labelFailInfo.Visible = true;
                            this.btnExit.Visible = true;
                            return;
                        }
                    }));
                }
                if (!success) { return; }
            }));
            #endregion
        }



        private void ConnectingLightController()
        {
            Task.Factory.StartNew(new Action(() =>
            {
                bool success = true;
                try
                {
                    HardwareManager.Instance.ConnectHardware(EnumHardwareType.Light);

                    _ConnectLightControllerSuccess = success;
                    if (_ConnectLightControllerSuccess)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            //SetProgressCaption("Connect DarkField Success.", labelRunningType);
                            SetProgressCaption("光源初始化完成。", labelRunningType);
                        });
                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Failed to connect Darkfield.", ex);
                    success = false;
                }
                finally
                {
                    this.Invoke(new Action(() => {
                        if (!success)
                        {
                            SetProgressCaption("光源初始化失败!", labelFailInfo);
                            this.labelFailInfo.Visible = true; this.btnExit.Visible = true; return;
                        }
                    }));
                }
                if (!success) { return; }
            }));
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
                    //if (_systemConfig.SystemRunningType == EnumRunningType.Actual)
                    //{
                    HardwareManager.Instance.ConnectHardware(EnumHardwareType.Camera);
                    success = HardwareManager.Instance.CheckCameraEngineValid();
                    _ConnectCameraSuccess = success;
                    if (success)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            SetProgressCaption("相机初始化完成。", labelRunningType);
                        });
                    }

                    if (success && CameraWindowGUI.Instance.InitVisualControled == false)
                    {
                        CameraWindowGUI.Instance.InitVisualControl();
                    }
                    //}
                    //else
                    //{
                    //    this.Invoke((MethodInvoker)delegate
                    //    {
                    //        SetProgressCaption("相机初始化完成。", labelRunningType);
                    //    });
                    //    _ConnectCameraSuccess = true;
                    //}
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Failed to connect Cameras.", ex);
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        this.Invoke(new Action(() => {
                            SetProgressCaption("相机初始化失败!", labelFailInfo);
                            this.labelFailInfo.Visible = true; this.btnExit.Visible = true;
                        }));
                    }
                }
                if (!success) { return; }
            }));
            #endregion                                                                                                                                       
        }

        private void ConnectingPowerController()
        {
            #region Connecting PowerController
            Task.Factory.StartNew(new Action(() =>
            {
                bool success = true;
                //连接电源控制
                try
                {
                    //SetProgressCaption(string.Format("Connecting Inspection Cameras..."), labelLineCameraText);
                    if (_systemConfig.SystemRunningType == EnumRunningType.Actual)
                    {
                        HardwareManager.Instance.ConnectHardware(EnumHardwareType.PowerController);
                        success = HardwareManager.Instance.CheckPowerControllerValid();
                        _ConnectPowerControllerSuccess = success;
                        if (success)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                //SetProgressCaption("Connect Power Controller Success.", labelRunningType);
                                SetProgressCaption("电源控制器初始化完成。", labelRunningType);
                            });
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            SetProgressCaption("电源控制器初始化完成。", labelRunningType);
                        });
                        _ConnectPowerControllerSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Failed to connect PowerController.", ex);
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        this.Invoke(new Action(() => {
                            SetProgressCaption("电源控制器初始化失败!", labelFailInfo);
                            this.labelFailInfo.Visible = true; this.btnExit.Visible = true;
                        }));
                    }
                }
                if (!success) { return; }
            }));
            #endregion    
        }

        private void ConnectingTemperatureController()
        {
            #region Connecting TemperatureController
            Task.Factory.StartNew(new Action(() =>
            {
                bool success = true;
                //连接电源控制
                try
                {
                    //SetProgressCaption(string.Format("Connecting Inspection Cameras..."), labelLineCameraText);
                    if (_systemConfig.SystemRunningType == EnumRunningType.Actual)
                    {
                        HardwareManager.Instance.ConnectHardware(EnumHardwareType.TemperatureController);
                        success = HardwareManager.Instance.CheckTemperatureControllerValid();
                        _ConnectTemperatureControllerSuccess = success;
                        if (success)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                //SetProgressCaption("Connect Power Controller Success.", labelRunningType);
                                SetProgressCaption("温度控制器初始化完成。", labelRunningType);
                            });
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            SetProgressCaption("温度控制器初始化完成。", labelRunningType);
                        });
                        _ConnectTemperatureControllerSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Failed to connect PowerController.", ex);
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        this.Invoke(new Action(() => {
                            SetProgressCaption("温度控制器初始化失败!", labelFailInfo);
                            this.labelFailInfo.Visible = true; this.btnExit.Visible = true;
                        }));
                    }
                }
                if (!success) { return; }
            }));
            #endregion    
        }

        private void ConnectingTurboMolecularPumpController()
        {
            #region Connecting TurboMolecularPumpController
            Task.Factory.StartNew(new Action(() =>
            {
                bool success = true;
                //连接电源控制
                try
                {
                    //SetProgressCaption(string.Format("Connecting Inspection Cameras..."), labelLineCameraText);
                    if (_systemConfig.SystemRunningType == EnumRunningType.Actual)
                    {
                        HardwareManager.Instance.ConnectHardware(EnumHardwareType.TurboMolecularPumpController);
                        success = HardwareManager.Instance.CheckTurboMolecularPumpControllerValid();
                        _ConnectTurboMolecularPumpControllerSuccess = success;
                        if (success)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                //SetProgressCaption("Connect Power Controller Success.", labelRunningType);
                                SetProgressCaption("分子泵控制器初始化完成。", labelRunningType);
                            });
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            SetProgressCaption("分子泵控制器初始化完成。", labelRunningType);
                        });
                        _ConnectTurboMolecularPumpControllerSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Failed to connect PowerController.", ex);
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        this.Invoke(new Action(() => {
                            SetProgressCaption("分子泵控制器初始化失败!", labelFailInfo);
                            this.labelFailInfo.Visible = true; this.btnExit.Visible = true;
                        }));
                    }
                }
                if (!success) { return; }
            }));
            #endregion    
        }

        private void ConnectingVacuumGaugeController()
        {
            #region Connecting VacuumGaugeController
            Task.Factory.StartNew(new Action(() =>
            {
                bool success = true;
                //连接电源控制
                try
                {
                    //SetProgressCaption(string.Format("Connecting Inspection Cameras..."), labelLineCameraText);
                    if (_systemConfig.SystemRunningType == EnumRunningType.Actual)
                    {
                        HardwareManager.Instance.ConnectHardware(EnumHardwareType.VacuumGaugeController);
                        success = HardwareManager.Instance.CheckVacuumGaugeControllerValid();
                        _ConnectVacuumGaugeControllerSuccess = success;
                        if (success)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                //SetProgressCaption("Connect Power Controller Success.", labelRunningType);
                                SetProgressCaption("真空计初始化完成。", labelRunningType);
                            });
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            SetProgressCaption("真空计初始化完成。", labelRunningType);
                        });
                        _ConnectVacuumGaugeControllerSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Failed to connect PowerController.", ex);
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        this.Invoke(new Action(() => {
                            SetProgressCaption("真空计初始化失败!", labelFailInfo);
                            this.labelFailInfo.Visible = true; this.btnExit.Visible = true;
                        }));
                    }
                }
                if (!success) { return; }
            }));
            #endregion    
        }

        private void ConnectingDewPointMeterController()
        {
            #region Connecting DewPointMeterController
            Task.Factory.StartNew(new Action(() =>
            {
                bool success = true;
                //连接露点仪
                try
                {
                    //SetProgressCaption(string.Format("Connecting Inspection Cameras..."), labelLineCameraText);
                    if (_systemConfig.SystemRunningType == EnumRunningType.Actual)
                    {
                        HardwareManager.Instance.ConnectHardware(EnumHardwareType.DewPointMeterController);
                        success = HardwareManager.Instance.CheckDewPointMeterControllerValid();
                        _ConnectDewPointMeterControllerSuccess = success;
                        if (success)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                //SetProgressCaption("Connect Power Controller Success.", labelRunningType);
                                SetProgressCaption("真空计初始化完成。", labelRunningType);
                            });
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            SetProgressCaption("露点仪初始化完成。", labelRunningType);
                        });
                        _ConnectDewPointMeterControllerSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Failed to connect PowerController.", ex);
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        this.Invoke(new Action(() => {
                            SetProgressCaption("露点仪初始化失败!", labelFailInfo);
                            this.labelFailInfo.Visible = true; this.btnExit.Visible = true;
                        }));
                    }
                }
                if (!success) { return; }
            }));
            #endregion    
        }

        private void ConnectingPressureSensorController()
        {
            #region Connecting PressureSensorController
            Task.Factory.StartNew(new Action(() =>
            {
                bool success = true;
                //连接电源控制
                try
                {
                    //SetProgressCaption(string.Format("Connecting Inspection Cameras..."), labelLineCameraText);
                    if (_systemConfig.SystemRunningType == EnumRunningType.Actual)
                    {
                        HardwareManager.Instance.ConnectHardware(EnumHardwareType.PressureSensorController);
                        success = HardwareManager.Instance.CheckPressureSensorControllerValid();
                        _ConnectPressureSensorControllerSuccess = success;
                        if (success)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                //SetProgressCaption("Connect Power Controller Success.", labelRunningType);
                                SetProgressCaption("压力传感器初始化完成。", labelRunningType);
                            });
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            SetProgressCaption("压力传感器初始化完成。", labelRunningType);
                        });
                        _ConnectPressureSensorControllerSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Failed to connect PowerController.", ex);
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        this.Invoke(new Action(() => {
                            SetProgressCaption("压力传感器初始化失败!", labelFailInfo);
                            this.labelFailInfo.Visible = true; this.btnExit.Visible = true;
                        }));
                    }
                }
                if (!success) { return; }
            }));
            #endregion    
        }

        private void ConnectingLaserSensorController()
        {
            #region Connecting LaserSensorController
            Task.Factory.StartNew(new Action(() =>
            {
                bool success = true;
                //连接电源控制
                try
                {
                    //SetProgressCaption(string.Format("Connecting Inspection Cameras..."), labelLineCameraText);
                    if (_systemConfig.SystemRunningType == EnumRunningType.Actual)
                    {
                        HardwareManager.Instance.ConnectHardware(EnumHardwareType.LaserSensor);
                        success = HardwareManager.Instance.CheckLaserSensorControllerValid();
                        _ConnectLaserSensorSuccess = success;
                        if (success)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                //SetProgressCaption("Connect Power Controller Success.", labelRunningType);
                                SetProgressCaption("激光传感器初始化完成。", labelRunningType);
                            });
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            SetProgressCaption("激光传感器初始化完成。", labelRunningType);
                        });
                        _ConnectLaserSensorSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Failed to connect PowerController.", ex);
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        this.Invoke(new Action(() =>
                        {
                            SetProgressCaption("激光传感器初始化失败!", labelFailInfo);
                            this.labelFailInfo.Visible = true; this.btnExit.Visible = true;
                        }));
                    }
                }
                if (!success) { return; }
            }));
            #endregion    
        }

        private void ConnectingDynamometerController()
        {
            #region Connecting LaserSensorController
            Task.Factory.StartNew(new Action(() =>
            {
                bool success = true;
                //连接电源控制
                try
                {
                    while (_ConnectLaserSensorSuccess == false || _systemInitializeSuccess == false)
                    {
                        Thread.Sleep(100);
                        _timeOut += 100;
                        if (_timeOut > 180000)
                        {
                            break;
                        }

                    }
                    //SetProgressCaption(string.Format("Connecting Inspection Cameras..."), labelLineCameraText);
                    //if (_systemConfig.SystemRunningType == EnumRunningType.Actual)
                    //{
                    HardwareManager.Instance.ConnectHardware(EnumHardwareType.Dynamometer);
                    success = HardwareManager.Instance.CheckDynamometerControllerValid();
                    _ConnectDynamometerSuccess = success;
                    if (success)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                                //SetProgressCaption("Connect Power Controller Success.", labelRunningType);
                                SetProgressCaption("测力传感器初始化完成。", labelRunningType);
                        });
                    }
                    //}
                    //else
                    //{
                    //    this.Invoke((MethodInvoker)delegate
                    //    {
                    //        SetProgressCaption("测力传感器初始化完成。", labelRunningType);
                    //    });
                    //    _ConnectDynamometerSuccess = true;
                    //}
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Failed to connect PowerController.", ex);
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        this.Invoke(new Action(() =>
                        {
                            SetProgressCaption("测力传感器初始化失败!", labelFailInfo);
                            this.labelFailInfo.Visible = true; this.btnExit.Visible = true;
                        }));
                    }
                }
                if (!success) { return; }
            }));
            #endregion    
        }

        /// <summary>
        /// 关闭页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

    }
}
