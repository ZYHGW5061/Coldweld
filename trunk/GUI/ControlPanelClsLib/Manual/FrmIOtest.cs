using BoardCardControllerClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using PositioningSystemClsLib;
using StageControllerClsLib;
using StageManagerClsLib;
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
using TechnologicalClsLib;
using TemperatureControllerClsLib;
using WestDragon.Framework.UtilityHelper;
using static GlobalToolClsLib.GlobalCommFunc;

namespace ControlPanelClsLib.Manual
{
    public partial class FrmIOtest : Form
    {

        #region private file

        private SynchronizationContext _syncContext;
        protected PositioningSystem _positionSystem
        {
            get
            {
                return PositioningSystem.Instance;
            }
        }
        private IStageController _stageEngine
        {
            get { return StageManager.Instance.GetCurrentController(); }
        }

        private IBoardCardController _boardCardController
        {
            get
            {
                return BoardCardManager.Instance.GetCurrentController();
            }
        }

        private TemperatureControllerManager _TemperatureControllerManager
        {
            get { return TemperatureControllerManager.Instance; }
        }

        private HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }

        private OvenBoxProcessControl _plc
        {
            get { return OvenBoxProcessControl.Instance; }
        }


        public EnumStageAxis curActiveAxis = EnumStageAxis.None;
        private double[] allPos = new double[20];
        private double[] allSpeed = new double[20];


        #endregion

        #region public file

        public FrmIOtest()
        {
            InitializeComponent();


            foreach (var item in Enum.GetValues(typeof(EnumStageAxis)))
            {
                this.cbSelectAxis.Items.Add(item);
            }

            foreach (Control control in this.tableLayoutPanel1.Controls)
            {
                if (control is Button button && button.Tag?.ToString() == "btnForward")
                {
                    button.MouseDown += new MouseEventHandler(btnForwardPos_MouseDown);
                    button.MouseUp += new MouseEventHandler(btnForwardPos_MouseUp);

                }
                if (control is Button button1 && button1.Tag?.ToString() == "btnReverse")
                {
                    button1.MouseDown += new MouseEventHandler(btnReversePos_MouseDown);
                    button1.MouseUp += new MouseEventHandler(btnReversePos_MouseUp);
                }
                if (control is Button button2 && button2.Tag?.ToString() == "btnAbsolute")
                {
                    button2.Click += new EventHandler(btnAbsolutePos_Chick);
                }
                if (control is Button button3 && button3.Tag?.ToString() == "btnRelative")
                {
                    button3.Click += new EventHandler(btnRelativePos_Chick);
                } 
            }
            foreach (Control control in this.tableLayoutPanel2.Controls)
            {
                if (control is Button button && button.Tag?.ToString() == "btnForward")
                {
                    button.MouseDown += new MouseEventHandler(btnForwardPos_MouseDown);
                    button.MouseUp += new MouseEventHandler(btnForwardPos_MouseUp);

                }
                if (control is Button button1 && button1.Tag?.ToString() == "btnReverse")
                {
                    button1.MouseDown += new MouseEventHandler(btnReversePos_MouseDown);
                    button1.MouseUp += new MouseEventHandler(btnReversePos_MouseUp);
                }
                if (control is Button button2 && button2.Tag?.ToString() == "btnAbsolute")
                {
                    button2.Click += new EventHandler(btnAbsolutePos_Chick);
                }
                if (control is Button button3 && button3.Tag?.ToString() == "btnRelative")
                {
                    button3.Click += new EventHandler(btnRelativePos_Chick);
                }
            }
            foreach (Control control in this.tableLayoutPanel3.Controls)
            {
                if (control is Button button && button.Tag?.ToString() == "btnForward")
                {
                    button.MouseDown += new MouseEventHandler(btnForwardPos_MouseDown);
                    button.MouseUp += new MouseEventHandler(btnForwardPos_MouseUp);

                }
                if (control is Button button1 && button1.Tag?.ToString() == "btnReverse")
                {
                    button1.MouseDown += new MouseEventHandler(btnReversePos_MouseDown);
                    button1.MouseUp += new MouseEventHandler(btnReversePos_MouseUp);
                }
                if (control is Button button2 && button2.Tag?.ToString() == "btnAbsolute")
                {
                    button2.Click += new EventHandler(btnAbsolutePos_Chick);
                }
                if (control is Button button3 && button3.Tag?.ToString() == "btnRelative")
                {
                    button3.Click += new EventHandler(btnRelativePos_Chick);
                }
            }
            foreach (Control control in this.tableLayoutPanel5.Controls)
            {
                if (control is CheckBox checkBox && checkBox.Tag != null && checkBox.Tag.ToString() == "OutputIO")
                {
                    checkBox.CheckedChanged += chOutputIO_CheckedChanged;
                }

            }
            

            

            Updatetimer.Enabled = true;

            DataModel.Instance.PropertyChanged += DataModel_PropertyChanged;
            _syncContext = SynchronizationContext.Current;

            UpdateAxisSpeed();
            UpdateIO();

        }


        #endregion



        #region private mothed


        private void UpdateIO()
        {
            try
            {
                ReadAllIO();
            }
            catch(Exception ex)
            {

            }

        }

        private void DataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            #region 按键锁

            if (e.PropertyName == nameof(DataModel.OvenBox1Function))
            {
                if(DataModel.Instance.OvenBox1Function)
                {
                    _syncContext.Post(_ => chBakeOvenCoarseExtractionValve.Enabled = false, null);
                    _syncContext.Post(_ => chBakeOvenFrontStageValve.Enabled = false, null);
                    _syncContext.Post(_ => chBakeOvenAerate.Enabled = false, null);
                }
                else
                {
                    _syncContext.Post(_ => chBakeOvenCoarseExtractionValve.Enabled = true, null);
                    _syncContext.Post(_ => chBakeOvenFrontStageValve.Enabled = true, null);
                    _syncContext.Post(_ => chBakeOvenAerate.Enabled = true, null);
                }
                
            }


            if (e.PropertyName == nameof(DataModel.OvenBox2Function))
            {
                if (DataModel.Instance.OvenBox2Function)
                {
                    _syncContext.Post(_ => chBakeOven2CoarseExtractionValve.Enabled = false, null);
                    _syncContext.Post(_ => chBakeOven2FrontStageValve.Enabled = false, null);
                    _syncContext.Post(_ => chBakeOven2Aerate.Enabled = false, null);
                }
                else
                {
                    _syncContext.Post(_ => chBakeOven2CoarseExtractionValve.Enabled = true, null);
                    _syncContext.Post(_ => chBakeOven2FrontStageValve.Enabled = true, null);
                    _syncContext.Post(_ => chBakeOven2Aerate.Enabled = true, null);
                }

            }

            #endregion


            #region 烘箱1


            if (e.PropertyName == nameof(DataModel.BakeOvenPlugInValveOpenstatus))
            {
                _syncContext.Post(_ => laBakeOvenPlugInValveOpen.BackColor = (DataModel.Instance.BakeOvenPlugInValveOpenstatus ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenPlugInValveClosestatus))
            {
                _syncContext.Post(_ => laBakeOvenPlugInValveClose.BackColor = (DataModel.Instance.BakeOvenPlugInValveClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenInnerdoorOpenstatus))
            {
                _syncContext.Post(_ => laBakeOvenInnerdoorOpenstatus.BackColor = (DataModel.Instance.BakeOvenInnerdoorOpenstatus ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenInnerdoorClosestatus))
            {
                _syncContext.Post(_ => laBakeOvenInnerdoorClosestatus.BackColor = (DataModel.Instance.BakeOvenInnerdoorClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenOuterdoorClosestatus))
            {
                _syncContext.Post(_ => laBakeOvenOuterdoorClosestatus.BackColor = (DataModel.Instance.BakeOvenOuterdoorClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenPressureSensor))
            {
                _syncContext.Post(_ => laBakeOvenPressureSensor.BackColor = (DataModel.Instance.BakeOvenPressureSensor ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenAutoHeat))
            {
                _syncContext.Post(_ => laBakeOvenAutoHeat.BackColor = (DataModel.Instance.BakeOvenAutoHeat ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenVacuum))
            {
                _syncContext.Post(_ => seBakeOvenVacuum.Value = (decimal)DataModel.Instance.BakeOvenVacuum, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenPressure))
            {
                _syncContext.Post(_ => seBakeOvenPressure.Value = (decimal)DataModel.Instance.BakeOvenPressure, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenDowntemp))
            {
                _syncContext.Post(_ => seBakeOvenDowntemp.Value = (decimal)DataModel.Instance.BakeOvenDowntemp, null);
            }


            if (e.PropertyName == nameof(DataModel.BakeOvenAerate))
            {
                _syncContext.Post(_ => laBakeOvenAerate.BackColor = (DataModel.Instance.BakeOvenAerate ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenCoarseExtractionValve))
            {
                _syncContext.Post(_ => laBakeOvenCoarseExtractionValve.BackColor = (DataModel.Instance.BakeOvenCoarseExtractionValve ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenFrontStageValve))
            {
                _syncContext.Post(_ => laBakeOvenFrontStageValve.BackColor = (DataModel.Instance.BakeOvenFrontStageValve ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenPlugInValve))
            {
                _syncContext.Post(_ => laBakeOvenPlugInValve.BackColor = (DataModel.Instance.BakeOvenPlugInValve ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenMechanicalPump))
            {
                _syncContext.Post(_ => laBakeOvenMechanicalPump.BackColor = (DataModel.Instance.BakeOvenMechanicalPump ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOvenInnerdoorUpDown))
            {
                _syncContext.Post(_ => laBakeOvenInnerdoorUpDown.BackColor = (DataModel.Instance.BakeOvenInnerdoorUpDown ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }


            #endregion

            #region 烘箱2


            if (e.PropertyName == nameof(DataModel.BakeOven2PlugInValveOpenstatus))
            {
                _syncContext.Post(_ => laBakeOven2PlugInValveOpen.BackColor = (DataModel.Instance.BakeOven2PlugInValveOpenstatus ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2PlugInValveClosestatus))
            {
                _syncContext.Post(_ => laBakeOven2PlugInValveClose.BackColor = (DataModel.Instance.BakeOven2PlugInValveClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2InnerdoorOpenstatus))
            {
                _syncContext.Post(_ => laBakeOven2InnerdoorOpenstatus.BackColor = (DataModel.Instance.BakeOven2InnerdoorOpenstatus ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2InnerdoorClosestatus))
            {
                _syncContext.Post(_ => laBakeOven2InnerdoorClosestatus.BackColor = (DataModel.Instance.BakeOven2InnerdoorClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2OuterdoorClosestatus))
            {
                _syncContext.Post(_ => laBakeOven2OuterdoorClosestatus.BackColor = (DataModel.Instance.BakeOven2OuterdoorClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2PressureSensor))
            {
                _syncContext.Post(_ => laBakeOven2PressureSensor.BackColor = (DataModel.Instance.BakeOven2PressureSensor ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2AutoHeat))
            {
                _syncContext.Post(_ => laBakeOven2AutoHeat.BackColor = (DataModel.Instance.BakeOven2AutoHeat ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2Vacuum))
            {
                _syncContext.Post(_ => seBakeOven2Vacuum.Value = (decimal)DataModel.Instance.BakeOven2Vacuum, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2Pressure))
            {
                _syncContext.Post(_ => seBakeOven2Pressure.Value = (decimal)DataModel.Instance.BakeOven2Pressure, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2Downtemp))
            {
                _syncContext.Post(_ => seBakeOven2Downtemp.Value = (decimal)DataModel.Instance.BakeOven2Downtemp, null);
            }


            if (e.PropertyName == nameof(DataModel.BakeOven2Aerate))
            {
                _syncContext.Post(_ => laBakeOven2Aerate.BackColor = (DataModel.Instance.BakeOven2Aerate ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2CoarseExtractionValve))
            {
                _syncContext.Post(_ => laBakeOven2CoarseExtractionValve.BackColor = (DataModel.Instance.BakeOven2CoarseExtractionValve ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2FrontStageValve))
            {
                _syncContext.Post(_ => laBakeOven2FrontStageValve.BackColor = (DataModel.Instance.BakeOven2FrontStageValve ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2PlugInValve))
            {
                _syncContext.Post(_ => laBakeOven2PlugInValve.BackColor = (DataModel.Instance.BakeOven2PlugInValve ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2MechanicalPump))
            {
                _syncContext.Post(_ => laBakeOven2MechanicalPump.BackColor = (DataModel.Instance.BakeOven2MechanicalPump ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BakeOven2InnerdoorUpDown))
            {
                _syncContext.Post(_ => laBakeOven2InnerdoorUpDown.BackColor = (DataModel.Instance.BakeOven2InnerdoorUpDown ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }



            #endregion

            #region 方舱

            if (e.PropertyName == nameof(DataModel.PressIsPress))
            {
                _syncContext.Post(_ => laPressIsPress.BackColor = (DataModel.Instance.PressIsPress ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.PressIsDivide))
            {
                _syncContext.Post(_ => laPressIsDivide.BackColor = (DataModel.Instance.PressIsDivide ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BoxPlugInValveOpenstatus))
            {
                _syncContext.Post(_ => laBoxPlugInValveOpen.BackColor = (DataModel.Instance.BoxPlugInValveOpenstatus ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BoxPlugInValveClosestatus))
            {
                _syncContext.Post(_ => laBoxPlugInValveClose.BackColor = (DataModel.Instance.BoxPlugInValveClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BoxOuterdoorClosetatus))
            {
                _syncContext.Post(_ => laBoxOuterdoorCloseSta.BackColor = (DataModel.Instance.BoxOuterdoorClosetatus ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.CondenserPumpSignal1))
            {
                _syncContext.Post(_ => laCondenserPumpSignal1.BackColor = (DataModel.Instance.CondenserPumpSignal1 ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.CondenserPumpSignal2))
            {
                _syncContext.Post(_ => laCondenserPumpSignal2.BackColor = (DataModel.Instance.CondenserPumpSignal2 ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.CondenserPumpSignal3))
            {
                _syncContext.Post(_ => laCondenserPumpSignal3.BackColor = (DataModel.Instance.CondenserPumpSignal3 ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.CondenserPumpSignal4))
            {
                _syncContext.Post(_ => laCondenserPumpSignal4.BackColor = (DataModel.Instance.CondenserPumpSignal4 ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.CondenserPumpSignal5))
            {
                _syncContext.Post(_ => laCondenserPumpSignal5.BackColor = (DataModel.Instance.CondenserPumpSignal5 ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.CondenserPumpSignal6))
            {
                _syncContext.Post(_ => laCondenserPumpSignal6.BackColor = (DataModel.Instance.CondenserPumpSignal6 ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.ThermalRelay))
            {
                _syncContext.Post(_ => laThermalRelay.BackColor = (DataModel.Instance.ThermalRelay ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BoxPressureSensor))
            {
                _syncContext.Post(_ => laBoxPressureSensor.BackColor = (DataModel.Instance.BoxPressureSensor ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BoxVacuum))
            {
                _syncContext.Post(_ => seBoxVacuum.Value = (decimal)DataModel.Instance.BoxVacuum, null);
            }
            if (e.PropertyName == nameof(DataModel.BoxDewPoint))
            {
                _syncContext.Post(_ => seBoxDewPoint.Value = (decimal)DataModel.Instance.BoxDewPoint, null);
            }
            if (e.PropertyName == nameof(DataModel.BoxPressure))
            {
                _syncContext.Post(_ => seBoxPressure.Value = (decimal)DataModel.Instance.BoxPressure, null);
            }


            if (e.PropertyName == nameof(DataModel.BoxAerate))
            {
                _syncContext.Post(_ => laBoxAerate.BackColor = (DataModel.Instance.BoxAerate ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BoxCoarseExtractionValve))
            {
                _syncContext.Post(_ => laBoxCoarseExtractionValve.BackColor = (DataModel.Instance.BoxCoarseExtractionValve ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BoxFrontStageValve))
            {
                _syncContext.Post(_ => laBoxFrontStageValve.BackColor = (DataModel.Instance.BoxFrontStageValve ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BoxPlugInValve))
            {
                _syncContext.Post(_ => laBoxPlugInValve.BackColor = (DataModel.Instance.BoxPlugInValve ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.BoxMechanicalPump))
            {
                _syncContext.Post(_ => laBoxMechanicalPump.BackColor = (DataModel.Instance.BoxMechanicalPump ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.PressPressingDivide))
            {
                _syncContext.Post(_ => laPressPressingDivide.BackColor = (DataModel.Instance.PressPressingDivide ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.CompressorStartup))
            {
                _syncContext.Post(_ => laCompressorStartup.BackColor = (DataModel.Instance.CompressorStartup ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.CompressorStops))
            {
                _syncContext.Post(_ => laCompressorStops.BackColor = (DataModel.Instance.CompressorStops ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.CondenserPump))
            {
                _syncContext.Post(_ => laCondenserPump.BackColor = (DataModel.Instance.CondenserPump ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }


            #endregion

            #region 电机

            if (e.PropertyName == nameof(DataModel.MotorBrake))
            {
                _syncContext.Post(_ => laMotorBrake.BackColor = (DataModel.Instance.MotorBrake ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.MotorBrake1))
            {
                _syncContext.Post(_ => laMotorBrake1.BackColor = (DataModel.Instance.MotorBrake1 ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }

            #endregion

            #region 塔灯

            if (e.PropertyName == nameof(DataModel.TowerYellowLight))
            {
                _syncContext.Post(_ => laTowerYellowLight.BackColor = (DataModel.Instance.TowerYellowLight ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.TowerGreenLight))
            {
                _syncContext.Post(_ => laTowerGreenLight.BackColor = (DataModel.Instance.TowerGreenLight ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }
            if (e.PropertyName == nameof(DataModel.TowerRedLight))
            {
                _syncContext.Post(_ => laTowerRedLight.BackColor = (DataModel.Instance.TowerRedLight ? true : false) ? Color.GreenYellow : Color.Transparent, null);
            }

            #endregion

        }


        private void btnAxisStop_Click(object sender, EventArgs e)
        {
            var runningType = _hardwareConfig.StageConfig.RunningType;
            if (runningType == EnumRunningType.Simulated)
            {
                return;
            }

            if (curActiveAxis == EnumStageAxis.None)
            {
                return;
            }

            _positionSystem.Stop(curActiveAxis);
        }

        private void btnAllAxisStop_Click(object sender, EventArgs e)
        {
            _positionSystem.Stop(EnumStageAxis.MaterialboxHook);
            _positionSystem.Stop(EnumStageAxis.MaterialboxT);
            _positionSystem.Stop(EnumStageAxis.MaterialboxX);
            _positionSystem.Stop(EnumStageAxis.MaterialboxY);
            _positionSystem.Stop(EnumStageAxis.MaterialboxZ);
            _positionSystem.Stop(EnumStageAxis.MaterialHook);
            _positionSystem.Stop(EnumStageAxis.MaterialX);
            _positionSystem.Stop(EnumStageAxis.MaterialY);
            _positionSystem.Stop(EnumStageAxis.MaterialZ);
            _positionSystem.Stop(EnumStageAxis.OverTrack1);
            _positionSystem.Stop(EnumStageAxis.OverTrack2);
            _positionSystem.Stop(EnumStageAxis.Presslifting);

        }

        private void UpdateAxisSpeed()
        {
            var runningType = _hardwareConfig.StageConfig.RunningType;
            if (runningType == EnumRunningType.Simulated)
            {
                return;
            }

            allSpeed[(int)EnumStageAxis.MaterialboxX] = _positionSystem.GetAxisSpeed(EnumStageAxis.MaterialboxX);
            seMaterialBoxXSpeed.Text = allSpeed[(int)EnumStageAxis.MaterialboxX].ToString();

            allSpeed[(int)EnumStageAxis.MaterialboxY] = _positionSystem.GetAxisSpeed(EnumStageAxis.MaterialboxY);
            seMaterialBoxYSpeed.Text = allSpeed[(int)EnumStageAxis.MaterialboxY].ToString();

            allSpeed[(int)EnumStageAxis.MaterialboxZ] = _positionSystem.GetAxisSpeed(EnumStageAxis.MaterialboxZ);
            seMaterialBoxZSpeed.Text = allSpeed[(int)EnumStageAxis.MaterialboxZ].ToString();

            allSpeed[(int)EnumStageAxis.MaterialboxT] = _positionSystem.GetAxisSpeed(EnumStageAxis.MaterialboxT);
            seMaterialBoxTSpeed.Text = allSpeed[(int)EnumStageAxis.MaterialboxT].ToString();

            allSpeed[(int)EnumStageAxis.MaterialboxHook] = _positionSystem.GetAxisSpeed(EnumStageAxis.MaterialboxHook);
            seMaterialBoxHSpeed.Text = allSpeed[(int)EnumStageAxis.MaterialboxHook].ToString();

            allSpeed[(int)EnumStageAxis.MaterialX] = _positionSystem.GetAxisSpeed(EnumStageAxis.MaterialX);
            seMaterialXSpeed.Text = allSpeed[(int)EnumStageAxis.MaterialX].ToString();

            allSpeed[(int)EnumStageAxis.MaterialY] = _positionSystem.GetAxisSpeed(EnumStageAxis.MaterialY);
            seMaterialYSpeed.Text = allSpeed[(int)EnumStageAxis.MaterialY].ToString();

            allSpeed[(int)EnumStageAxis.MaterialZ] = _positionSystem.GetAxisSpeed(EnumStageAxis.MaterialZ);
            seMaterialZSpeed.Text = allSpeed[(int)EnumStageAxis.MaterialZ].ToString();

            allSpeed[(int)EnumStageAxis.MaterialHook] = _positionSystem.GetAxisSpeed(EnumStageAxis.MaterialHook);
            seMaterialHSpeed.Text = allSpeed[(int)EnumStageAxis.MaterialHook].ToString();

            allSpeed[(int)EnumStageAxis.OverTrack1] = _positionSystem.GetAxisSpeed(EnumStageAxis.OverTrack1);
            seOvenTrack1Speed.Text = allSpeed[(int)EnumStageAxis.OverTrack1].ToString();

            allSpeed[(int)EnumStageAxis.OverTrack2] = _positionSystem.GetAxisSpeed(EnumStageAxis.OverTrack2);
            seOvenTrack2Speed.Text = allSpeed[(int)EnumStageAxis.OverTrack2].ToString();

            allSpeed[(int)EnumStageAxis.Presslifting] = _positionSystem.GetAxisSpeed(EnumStageAxis.Presslifting);
            sePressliftingSpeed.Text = allSpeed[(int)EnumStageAxis.Presslifting].ToString();

        }

        private void UpdateAxisPosition()
        {
            var runningType = _hardwareConfig.StageConfig.RunningType;
            if (runningType == EnumRunningType.Simulated)
            {
                return;
            }

            allPos[(int)EnumStageAxis.MaterialboxX] = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialboxX);
            seMaterialBoxX.Text = allPos[(int)EnumStageAxis.MaterialboxX].ToString();

            allPos[(int)EnumStageAxis.MaterialboxY] = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialboxY);
            seMaterialBoxY.Text = allPos[(int)EnumStageAxis.MaterialboxY].ToString();

            allPos[(int)EnumStageAxis.MaterialboxZ] = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialboxZ);
            seMaterialBoxZ.Text = allPos[(int)EnumStageAxis.MaterialboxZ].ToString();

            allPos[(int)EnumStageAxis.MaterialboxT] = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialboxT);
            seMaterialBoxT.Text = allPos[(int)EnumStageAxis.MaterialboxT].ToString();

            allPos[(int)EnumStageAxis.MaterialboxHook] = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialboxHook);
            seMaterialBoxH.Text = allPos[(int)EnumStageAxis.MaterialboxHook].ToString();

            allPos[(int)EnumStageAxis.MaterialX] = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialX);
            seMaterialX.Text = allPos[(int)EnumStageAxis.MaterialX].ToString();

            allPos[(int)EnumStageAxis.MaterialY] = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialY);
            seMaterialY.Text = allPos[(int)EnumStageAxis.MaterialY].ToString();

            allPos[(int)EnumStageAxis.MaterialZ] = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialZ);
            seMaterialZ.Text = allPos[(int)EnumStageAxis.MaterialZ].ToString();

            allPos[(int)EnumStageAxis.MaterialHook] = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.MaterialHook);
            seMaterialH.Text = allPos[(int)EnumStageAxis.MaterialHook].ToString();

            allPos[(int)EnumStageAxis.OverTrack1] = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.OverTrack1);
            seOvenTrack1.Text = allPos[(int)EnumStageAxis.OverTrack1].ToString();

            allPos[(int)EnumStageAxis.OverTrack2] = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.OverTrack2);
            seOvenTrack2.Text = allPos[(int)EnumStageAxis.OverTrack2].ToString();

            allPos[(int)EnumStageAxis.Presslifting] = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.Presslifting);
            sePresslifting.Text = allPos[(int)EnumStageAxis.Presslifting].ToString();


        }

        private void UpdateInputIO()
        {
            if(_boardCardController == null)
            {
                return;
            }
            var runningType = _hardwareConfig.StageConfig.RunningType;
            if (runningType == EnumRunningType.Simulated)
            {
                return;
            }

            if (DataModel.Instance.OvenBox1Function)
            {
                _syncContext.Post(_ => chBakeOvenCoarseExtractionValve.Enabled = false, null);
                _syncContext.Post(_ => chBakeOvenFrontStageValve.Enabled = false, null);
                _syncContext.Post(_ => chBakeOvenAerate.Enabled = false, null);
            }
            else
            {
                _syncContext.Post(_ => chBakeOvenCoarseExtractionValve.Enabled = true, null);
                _syncContext.Post(_ => chBakeOvenFrontStageValve.Enabled = true, null);
                _syncContext.Post(_ => chBakeOvenAerate.Enabled = true, null);
            }

            if (DataModel.Instance.OvenBox2Function)
            {
                _syncContext.Post(_ => chBakeOven2CoarseExtractionValve.Enabled = false, null);
                _syncContext.Post(_ => chBakeOven2FrontStageValve.Enabled = false, null);
                _syncContext.Post(_ => chBakeOven2Aerate.Enabled = false, null);
            }
            else
            {
                _syncContext.Post(_ => chBakeOven2CoarseExtractionValve.Enabled = true, null);
                _syncContext.Post(_ => chBakeOven2FrontStageValve.Enabled = true, null);
                _syncContext.Post(_ => chBakeOven2Aerate.Enabled = true, null);
            }

            #region 烘箱1

            laBakeOvenPlugInValveOpen.BackColor = (DataModel.Instance.BakeOvenPlugInValveOpenstatus ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOvenPlugInValveClose.BackColor = (DataModel.Instance.BakeOvenPlugInValveClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOvenInnerdoorOpenstatus.BackColor = (DataModel.Instance.BakeOvenInnerdoorOpenstatus ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOvenInnerdoorClosestatus.BackColor = (DataModel.Instance.BakeOvenInnerdoorClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOvenOuterdoorClosestatus.BackColor = (DataModel.Instance.BakeOvenOuterdoorClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOvenPressureSensor.BackColor = (DataModel.Instance.BakeOvenPressureSensor ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOvenAutoHeat.BackColor = (DataModel.Instance.BakeOvenAutoHeat ? true : false) ? Color.GreenYellow : Color.Transparent;
            seBakeOvenVacuum.Value = (decimal)DataModel.Instance.BakeOvenVacuum;
            seBakeOvenPressure.Value = (decimal)DataModel.Instance.BakeOvenPressure;
            seBakeOvenDowntemp.Value = (decimal)DataModel.Instance.BakeOvenDowntemp;




            #endregion

            #region 烘箱2

            laBakeOven2PlugInValveOpen.BackColor = (DataModel.Instance.BakeOven2PlugInValveOpenstatus ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOven2PlugInValveClose.BackColor = (DataModel.Instance.BakeOven2PlugInValveClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOven2InnerdoorOpenstatus.BackColor = (DataModel.Instance.BakeOven2InnerdoorOpenstatus ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOven2InnerdoorClosestatus.BackColor = (DataModel.Instance.BakeOven2InnerdoorClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOven2OuterdoorClosestatus.BackColor = (DataModel.Instance.BakeOven2OuterdoorClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOven2PressureSensor.BackColor = (DataModel.Instance.BakeOven2PressureSensor ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOven2AutoHeat.BackColor = (DataModel.Instance.BakeOven2AutoHeat ? true : false) ? Color.GreenYellow : Color.Transparent;
            seBakeOven2Vacuum.Value = (decimal)DataModel.Instance.BakeOven2Vacuum;
            seBakeOven2Pressure.Value = (decimal)DataModel.Instance.BakeOven2Pressure;
            seBakeOven2Downtemp.Value = (decimal)DataModel.Instance.BakeOven2Downtemp;




            #endregion

            #region 方舱

            laPressIsPress.BackColor = (DataModel.Instance.PressIsPress ? true : false) ? Color.GreenYellow : Color.Transparent;
            laPressIsDivide.BackColor = (DataModel.Instance.PressIsDivide ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBoxPlugInValveOpen.BackColor = (DataModel.Instance.BoxPlugInValveOpenstatus ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBoxPlugInValveClose.BackColor = (DataModel.Instance.BoxPlugInValveClosestatus ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBoxOuterdoorCloseSta.BackColor = (DataModel.Instance.BoxOuterdoorClosetatus ? true : false) ? Color.GreenYellow : Color.Transparent;
            laCondenserPumpSignal1.BackColor = (DataModel.Instance.CondenserPumpSignal1 ? true : false) ? Color.GreenYellow : Color.Transparent;
            laCondenserPumpSignal2.BackColor = (DataModel.Instance.CondenserPumpSignal2 ? true : false) ? Color.GreenYellow : Color.Transparent;
            laCondenserPumpSignal3.BackColor = (DataModel.Instance.CondenserPumpSignal3 ? true : false) ? Color.GreenYellow : Color.Transparent;
            laCondenserPumpSignal4.BackColor = (DataModel.Instance.CondenserPumpSignal4 ? true : false) ? Color.GreenYellow : Color.Transparent;
            laCondenserPumpSignal5.BackColor = (DataModel.Instance.CondenserPumpSignal5 ? true : false) ? Color.GreenYellow : Color.Transparent;
            laCondenserPumpSignal6.BackColor = (DataModel.Instance.CondenserPumpSignal6 ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBoxPressureSensor.BackColor = (DataModel.Instance.BoxPressureSensor ? true : false) ? Color.GreenYellow : Color.Transparent;
            seBoxVacuum.Value = (decimal)DataModel.Instance.BoxVacuum;
            seBoxPressure.Value = (decimal)DataModel.Instance.BoxPressure;



            #endregion

            #region 电机

            #endregion

            #region 烘箱1

            laBakeOvenAerate.BackColor = (DataModel.Instance.BakeOvenAerate ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOvenCoarseExtractionValve.BackColor = (DataModel.Instance.BakeOvenCoarseExtractionValve ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOvenFrontStageValve.BackColor = (DataModel.Instance.BakeOvenFrontStageValve ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOvenPlugInValve.BackColor = (DataModel.Instance.BakeOvenPlugInValve ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOvenMechanicalPump.BackColor = (DataModel.Instance.BakeOvenMechanicalPump ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOvenInnerdoorUpDown.BackColor = (DataModel.Instance.BakeOvenInnerdoorUpDown ? true : false) ? Color.GreenYellow : Color.Transparent;




            #endregion

            #region 烘箱2

            laBakeOven2Aerate.BackColor = (DataModel.Instance.BakeOven2Aerate ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOven2CoarseExtractionValve.BackColor = (DataModel.Instance.BakeOven2CoarseExtractionValve ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOven2FrontStageValve.BackColor = (DataModel.Instance.BakeOven2FrontStageValve ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOven2PlugInValve.BackColor = (DataModel.Instance.BakeOven2PlugInValve ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOven2MechanicalPump.BackColor = (DataModel.Instance.BakeOven2MechanicalPump ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBakeOven2InnerdoorUpDown.BackColor = (DataModel.Instance.BakeOven2InnerdoorUpDown ? true : false) ? Color.GreenYellow : Color.Transparent;




            #endregion

            #region 方舱

            laBoxAerate.BackColor = (DataModel.Instance.BoxAerate ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBoxCoarseExtractionValve.BackColor = (DataModel.Instance.BoxCoarseExtractionValve ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBoxFrontStageValve.BackColor = (DataModel.Instance.BoxFrontStageValve ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBoxPlugInValve.BackColor = (DataModel.Instance.BoxPlugInValve ? true : false) ? Color.GreenYellow : Color.Transparent;
            laBoxMechanicalPump.BackColor = (DataModel.Instance.BoxMechanicalPump ? true : false) ? Color.GreenYellow : Color.Transparent;
            laPressPressingDivide.BackColor = (DataModel.Instance.PressPressingDivide ? true : false) ? Color.GreenYellow : Color.Transparent;
            laCompressorStartup.BackColor = (DataModel.Instance.CompressorStartup ? true : false) ? Color.GreenYellow : Color.Transparent;
            laCompressorStops.BackColor = (DataModel.Instance.CompressorStops ? true : false) ? Color.GreenYellow : Color.Transparent;
            laCondenserPump.BackColor = (DataModel.Instance.CondenserPump ? true : false) ? Color.GreenYellow : Color.Transparent;



            #endregion

            #region 电机

            laMotorBrake.BackColor = (DataModel.Instance.MotorBrake ? true : false) ? Color.GreenYellow : Color.Transparent;
            laMotorBrake1.BackColor = (DataModel.Instance.MotorBrake1 ? true : false) ? Color.GreenYellow : Color.Transparent;

            #endregion

            #region 塔灯

            laTowerYellowLight.BackColor = (DataModel.Instance.TowerYellowLight ? true : false) ? Color.GreenYellow : Color.Transparent;
            laTowerGreenLight.BackColor = (DataModel.Instance.TowerGreenLight ? true : false) ? Color.GreenYellow : Color.Transparent;
            laTowerRedLight.BackColor = (DataModel.Instance.TowerRedLight ? true : false) ? Color.GreenYellow : Color.Transparent;

            #endregion

        }


        private void UpdateOutputIO()
        {
            if (_boardCardController == null)
            {
                return;
            }
            var runningType = _hardwareConfig.StageConfig.RunningType;
            if (runningType == EnumRunningType.Simulated)
            {
                return;
            }

            List<int> data = new List<int>();
            _boardCardController.IO_ReadAllOutput_2(11, out data);

            laTowerRedLight.BackColor = (data[(int)EnumBoardcardDefineOutputIO.TowerRedLight ] == 1 ? true : false) ? Color.GreenYellow : Color.Transparent;
            laTowerYellowLight.BackColor = (data[(int)EnumBoardcardDefineOutputIO.TowerYellowLight ] == 1 ? true : false) ? Color.GreenYellow : Color.Transparent;
            laTowerGreenLight.BackColor = (data[(int)EnumBoardcardDefineOutputIO.TowerGreenLight ] == 1 ? true : false) ? Color.GreenYellow : Color.Transparent;

            laBakeOvenAerate.BackColor = (data[(int)EnumBoardcardDefineOutputIO.BakeOvenAerate ] == 1 ? true : false) ? Color.GreenYellow : Color.Transparent;


            //烘箱2
            laBakeOven2Aerate.BackColor = (data[(int)EnumBoardcardDefineOutputIO.BakeOven2Aerate ] == 1 ? true : false) ? Color.GreenYellow : Color.Transparent;


            laBoxAerate.BackColor = (data[(int)EnumBoardcardDefineOutputIO.BoxAerate ] == 1 ? true : false) ? Color.GreenYellow : Color.Transparent;

            

            //int num = (_TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Read(TemperatureRtuAdd.E_PRG));
            //bool isBit0Set = (num & (1 << 0)) != 0;
            //bool BakeOvenAutoHeatstatus = isBit0Set;
            //laBakeOvenAutoHeat.BackColor = (BakeOvenAutoHeatstatus) ? Color.GreenYellow : Color.Transparent;

            //int num2 = (_TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Read(TemperatureRtuAdd.E_PRG));
            //bool isBit0Set2 = (num2 & (1 << 0)) != 0;
            //bool BakeOven2AutoHeatstatus = isBit0Set2;
            //laBakeOven2AutoHeat.BackColor = (BakeOven2AutoHeatstatus) ? Color.GreenYellow : Color.Transparent;





        }



        private bool AxisSts(EnumStageAxis axis, short bit)
        {
            var runningType = _hardwareConfig.StageConfig.RunningType;
            if (runningType == EnumRunningType.Simulated)
            {
                return false;
            }

            /// 读取轴状态
            /// 1 报警
            /// 5 正限位
            /// 6 负限位 
            /// 7 平滑停止 
            /// 8 急停 
            /// 9 使能 
            /// 10 规划运动 
            /// 11 电机到位
            if (curActiveAxis == EnumStageAxis.None)
            {
                return false;
            }

            int pSts;
            pSts = _positionSystem.GetAxisState(axis);
            if ((pSts & (1 << bit)) != 0)
            {
                return true;
            }

            return false;
        }

        private void ReadAllIO()
        {
            

            UpdateInputIO();

            //UpdateOutputIO();
        }

        private void UpdateAxisState()
        {
            var runningType = _hardwareConfig.StageConfig.RunningType;
            if (runningType == EnumRunningType.Simulated)
            {
                return;
            }

            teCurActiveAxisName.Text = EnumHelper.GetEnumDescription(curActiveAxis);

            if (curActiveAxis == EnumStageAxis.None)
            {
                return;
            }

            //读取轴数据
            lbAlarmFlag.BackColor = AxisSts(curActiveAxis, 1) ? Color.Red : Color.Lime;
            lbEnableFlag.BackColor = AxisSts(curActiveAxis, 9) ? Color.Red : Color.Lime;
            lbPosLimitFlag.BackColor = AxisSts(curActiveAxis, 5) ? Color.Red : Color.Lime;
            lbNegLimitFlag.BackColor = AxisSts(curActiveAxis, 6) ? Color.Red : Color.Lime;
            lbPlanMoveFlag.BackColor = AxisSts(curActiveAxis, 10) ? Color.Red : Color.Lime;
            lbSmoothStopFlag.BackColor = AxisSts(curActiveAxis, 7) ? Color.Red : Color.Lime;
            lbMotorReadyFlag.BackColor = AxisSts(curActiveAxis, 11) ? Color.Red : Color.Lime;
            lbEStopFlag.BackColor = AxisSts(curActiveAxis, 8) ? Color.Red : Color.Lime;
            teNLimit.Text = _stageEngine[curActiveAxis].GetSoftLeftLimit().ToString();
            tePLimit.Text = _stageEngine[curActiveAxis].GetSoftRightLimit().ToString();
        }

        private void Updatetimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //ReadAllIO();

                UpdateAxisPosition();
                UpdateAxisState();
            }
            catch(Exception ex)
            {

            }
        }

        private void AxisForward(EnumStageAxis axis, float speed)
        {
            var runningType = _hardwareConfig.StageConfig.RunningType;
            if (runningType == EnumRunningType.Simulated)
            {
                return;
            }

            _positionSystem.JogPositive(axis, speed);
        }

        private void AxisForwardStop(EnumStageAxis axis)
        {
            Task.Run(() =>
            {
                var runningType = _hardwareConfig.StageConfig.RunningType;
                if (runningType == EnumRunningType.Simulated)
                {
                    return;
                }

                _positionSystem.StopJogPositive(axis);
            });
        }

        private void AxisReverse(EnumStageAxis axis, float speed)
        {
            var runningType = _hardwareConfig.StageConfig.RunningType;
            if (runningType == EnumRunningType.Simulated)
            {
                return;
            }

            _positionSystem.JogNegative(axis, speed);
        }

        private void AxisReverseStop(EnumStageAxis axis)
        {
            var runningType = _hardwareConfig.StageConfig.RunningType;
            if (runningType == EnumRunningType.Simulated)
            {
                return;
            }

            _positionSystem.StopJogNegative(axis);
        }

        private void AxisAbsolute(EnumStageAxis axis, float Target)
        {
            Task.Run(() =>
            {
                var runningType = _hardwareConfig.StageConfig.RunningType;
                if (runningType == EnumRunningType.Simulated)
                {
                    return;
                }

                _positionSystem.MoveAixsToStageCoord(axis, Target, EnumCoordSetType.Absolute);
            });

            
        }

        private void AxisRelative(EnumStageAxis axis, float Target)
        {
            Task.Run(() =>
            {
                var runningType = _hardwareConfig.StageConfig.RunningType;
                if (runningType == EnumRunningType.Simulated)
                {
                    return;
                }

                _positionSystem.MoveAixsToStageCoord(axis, Target, EnumCoordSetType.Relative);
            });
        }

        private void OutputIOChange(EnumBoardcardDefineOutputIO io, bool en)
        {
            var runningType = _hardwareConfig.StageConfig.RunningType;
            if (runningType == EnumRunningType.Simulated)
            {
                return;
            }

            int Num = -1;
            if (en)
                Num = 1;
            else
                Num = 0;
            _plc.WriteBoolOvenBoxStates(io, en);
            //_boardCardController.IO_WriteOutPut_2(11, (short)io, Num);
        }

        private void HeatChange(EnumOvenBoxNum num, bool en)
        {
            if(_hardwareConfig.TemperatureControllerConfig.Count > 0)
            {
                var runningType = _hardwareConfig.TemperatureControllerConfig[0].RunningType;
                if (runningType == EnumRunningType.Simulated)
                {
                    return;
                }
            }
            else
            {
                return;
            }
            

            int Num = -1;
            if (en)
                Num = 1;
            else
                Num = 0;
            if (num == EnumOvenBoxNum.Oven1)
            {
                _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Write(TemperatureRtuAdd.RUN, Num);
                _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Up).Write(TemperatureRtuAdd.RUN, Num);
            }
            else if(num == EnumOvenBoxNum.Oven2)
            {
                _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Write(TemperatureRtuAdd.RUN, Num);
                _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Up).Write(TemperatureRtuAdd.RUN, Num);
            }
        }

        private void btnForwardPos_MouseDown(object sender, MouseEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string tag = clickedButton.Tag.ToString();

                float speed = 0;

                switch (clickedButton.Name)
                {
                    case "btnMaterialBoxXForward":
                        speed = float.Parse(seMaterialBoxXSpeed.Text);
                        AxisForward(EnumStageAxis.MaterialboxX, speed);
                        break;
                    case "btnMaterialBoxYForward":
                        speed = float.Parse(seMaterialBoxYSpeed.Text);
                        AxisForward(EnumStageAxis.MaterialboxY, speed);
                        break;
                    case "btnMaterialBoxZForward":
                        speed = float.Parse(seMaterialBoxZSpeed.Text);
                        AxisForward(EnumStageAxis.MaterialboxZ, speed);
                        break;
                    case "btnMaterialBoxTForward":
                        speed = float.Parse(seMaterialBoxTSpeed.Text);
                        AxisForward(EnumStageAxis.MaterialboxT, speed);
                        break;
                    case "btnMaterialBoxHForward":
                        speed = float.Parse(seMaterialBoxHSpeed.Text);
                        AxisForward(EnumStageAxis.MaterialboxHook, speed);
                        break;
                    case "btnMaterialXForward":
                        speed = float.Parse(seMaterialXSpeed.Text);
                        AxisForward(EnumStageAxis.MaterialX, speed);
                        break;
                    case "btnMaterialYForward":
                        speed = float.Parse(seMaterialYSpeed.Text);
                        AxisForward(EnumStageAxis.MaterialY, speed);
                        break;
                    case "btnMaterialZForward":
                        speed = float.Parse(seMaterialZSpeed.Text);
                        AxisForward(EnumStageAxis.MaterialZ, speed);
                        break;
                    case "btnMaterialHForward":
                        speed = float.Parse(seMaterialHSpeed.Text);
                        AxisForward(EnumStageAxis.MaterialHook, speed);
                        break;
                    case "btnOvenTrack1Forward":
                        speed = float.Parse(seOvenTrack1Speed.Text);
                        AxisForward(EnumStageAxis.OverTrack1, speed);
                        break;
                    case "btnOvenTrack2Forward":
                        speed = float.Parse(seOvenTrack2Speed.Text);
                        AxisForward(EnumStageAxis.OverTrack2, speed);
                        break;
                    case "btnPressliftingForward":
                        speed = float.Parse(sePressliftingSpeed.Text);
                        AxisForward(EnumStageAxis.Presslifting, speed);
                        break;


                }


            }
        }

        private void btnForwardPos_MouseUp(object sender, MouseEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string tag = clickedButton.Tag.ToString();

                switch (clickedButton.Name)
                {
                    case "btnMaterialBoxXForward":
                        AxisForwardStop(EnumStageAxis.MaterialboxX);
                        break;
                    case "btnMaterialBoxYForward":
                        AxisForwardStop(EnumStageAxis.MaterialboxY);
                        break;
                    case "btnMaterialBoxZForward":
                        AxisForwardStop(EnumStageAxis.MaterialboxZ);
                        break;
                    case "btnMaterialBoxTForward":
                        AxisForwardStop(EnumStageAxis.MaterialboxT);
                        break;
                    case "btnMaterialBoxHForward":
                        AxisForwardStop(EnumStageAxis.MaterialboxHook);
                        break;
                    case "btnMaterialXForward":
                        AxisForwardStop(EnumStageAxis.MaterialX);
                        break;
                    case "btnMaterialYForward":
                        AxisForwardStop(EnumStageAxis.MaterialY);
                        break;
                    case "btnMaterialZForward":
                        AxisForwardStop(EnumStageAxis.MaterialZ);
                        break;
                    case "btnMaterialHForward":
                        AxisForwardStop(EnumStageAxis.MaterialHook);
                        break;
                    case "btnOvenTrack1Forward":
                        AxisForwardStop(EnumStageAxis.OverTrack1);
                        break;
                    case "btnOvenTrack2Forward":
                        AxisForwardStop(EnumStageAxis.OverTrack2);
                        break;
                    case "btnPressliftingForward":
                        AxisForwardStop(EnumStageAxis.Presslifting);
                        break;


                }


            }

        }

        private void btnReversePos_MouseDown(object sender, MouseEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string tag = clickedButton.Tag.ToString();

                float speed = 0;

                switch (clickedButton.Name)
                {
                    case "btnMaterialBoxXReverse":
                        speed = float.Parse(seMaterialBoxXSpeed.Text);
                        AxisReverse(EnumStageAxis.MaterialboxX, speed);
                        break;
                    case "btnMaterialBoxYReverse":
                        speed = float.Parse(seMaterialBoxYSpeed.Text);
                        AxisReverse(EnumStageAxis.MaterialboxY, speed);
                        break;
                    case "btnMaterialBoxZReverse":
                        speed = float.Parse(seMaterialBoxZSpeed.Text);
                        AxisReverse(EnumStageAxis.MaterialboxZ, speed);
                        break;
                    case "btnMaterialBoxTReverse":
                        speed = float.Parse(seMaterialBoxTSpeed.Text);
                        AxisReverse(EnumStageAxis.MaterialboxT, speed);
                        break;
                    case "btnMaterialBoxHReverse":
                        speed = float.Parse(seMaterialBoxHSpeed.Text);
                        AxisReverse(EnumStageAxis.MaterialboxHook, speed);
                        break;
                    case "btnMaterialXReverse":
                        speed = float.Parse(seMaterialXSpeed.Text);
                        AxisReverse(EnumStageAxis.MaterialX, speed);
                        break;
                    case "btnMaterialYReverse":
                        speed = float.Parse(seMaterialYSpeed.Text);
                        AxisReverse(EnumStageAxis.MaterialY, speed);
                        break;
                    case "btnMaterialZReverse":
                        speed = float.Parse(seMaterialZSpeed.Text);
                        AxisReverse(EnumStageAxis.MaterialZ, speed);
                        break;
                    case "btnMaterialHReverse":
                        speed = float.Parse(seMaterialHSpeed.Text);
                        AxisReverse(EnumStageAxis.MaterialHook, speed);
                        break;
                    case "btnOvenTrack1Reverse":
                        speed = float.Parse(seOvenTrack1Speed.Text);
                        AxisReverse(EnumStageAxis.OverTrack1, speed);
                        break;
                    case "btnOvenTrack2Reverse":
                        speed = float.Parse(seOvenTrack2Speed.Text);
                        AxisReverse(EnumStageAxis.OverTrack2, speed);
                        break;
                    case "btnPressliftingReverse":
                        speed = float.Parse(sePressliftingSpeed.Text);
                        AxisReverse(EnumStageAxis.Presslifting, speed);
                        break;


                }


            }
        }

        private void btnReversePos_MouseUp(object sender, MouseEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string tag = clickedButton.Tag.ToString();

                switch (clickedButton.Name)
                {
                    case "btnMaterialBoxXReverse":
                        AxisReverseStop(EnumStageAxis.MaterialboxX);
                        break;
                    case "btnMaterialBoxYReverse":
                        AxisReverseStop(EnumStageAxis.MaterialboxY);
                        break;
                    case "btnMaterialBoxZReverse":
                        AxisReverseStop(EnumStageAxis.MaterialboxZ);
                        break;
                    case "btnMaterialBoxTReverse":
                        AxisReverseStop(EnumStageAxis.MaterialboxT);
                        break;
                    case "btnMaterialBoxHReverse":
                        AxisReverseStop(EnumStageAxis.MaterialboxHook);
                        break;
                    case "btnMaterialXReverse":
                        AxisReverseStop(EnumStageAxis.MaterialX);
                        break;
                    case "btnMaterialYReverse":
                        AxisReverseStop(EnumStageAxis.MaterialY);
                        break;
                    case "btnMaterialZReverse":
                        AxisReverseStop(EnumStageAxis.MaterialZ);
                        break;
                    case "btnMaterialHReverse":
                        AxisReverseStop(EnumStageAxis.MaterialHook);
                        break;
                    case "btnOvenTrack1Reverse":
                        AxisReverseStop(EnumStageAxis.OverTrack1);
                        break;
                    case "btnOvenTrack2Reverse":
                        AxisReverseStop(EnumStageAxis.OverTrack2);
                        break;
                    case "btnPressliftingReverse":
                        AxisReverseStop(EnumStageAxis.Presslifting);
                        break;


                }


            }

        }

        private void btnAbsolutePos_Chick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string tag = clickedButton.Tag.ToString();

                float target = 0;

                switch (clickedButton.Name)
                {
                    case "btnMaterialBoxXAbsolute":
                        target = float.Parse(seMaterialBoxXAbsolute.Text);
                        AxisAbsolute(EnumStageAxis.MaterialboxX, target);
                        break;
                    case "btnMaterialBoxYAbsolute":
                        target = float.Parse(seMaterialBoxYAbsolute.Text);
                        AxisAbsolute(EnumStageAxis.MaterialboxY, target);
                        break;
                    case "btnMaterialBoxZAbsolute":
                        target = float.Parse(seMaterialBoxZAbsolute.Text);
                        AxisAbsolute(EnumStageAxis.MaterialboxY, target);
                        break;
                    case "btnMaterialBoxTAbsolute":
                        target = float.Parse(seMaterialBoxTAbsolute.Text);
                        AxisAbsolute(EnumStageAxis.MaterialboxT, target);
                        break;
                    case "btnMaterialBoxHAbsolute":
                        target = float.Parse(seMaterialBoxHAbsolute.Text);
                        AxisAbsolute(EnumStageAxis.MaterialboxHook, target);
                        break;
                    case "btnMaterialXAbsolute":
                        target = float.Parse(seMaterialXAbsolute.Text);
                        AxisAbsolute(EnumStageAxis.MaterialX, target);
                        break;
                    case "btnMaterialYAbsolute":
                        target = float.Parse(seMaterialYAbsolute.Text);
                        AxisAbsolute(EnumStageAxis.MaterialY, target);
                        break;
                    case "btnMaterialZAbsolute":
                        target = float.Parse(seMaterialZAbsolute.Text);
                        AxisAbsolute(EnumStageAxis.MaterialZ, target);
                        break;
                    case "btnMaterialHAbsolute":
                        target = float.Parse(seMaterialHAbsolute.Text);
                        AxisAbsolute(EnumStageAxis.MaterialHook, target);
                        break;
                    case "btnOvenTrack1Absolute":
                        target = float.Parse(seOvenTrack1Absolute.Text);
                        AxisAbsolute(EnumStageAxis.OverTrack1, target);
                        break;
                    case "btnOvenTrack2Absolute":
                        target = float.Parse(seOvenTrack2Absolute.Text);
                        AxisAbsolute(EnumStageAxis.OverTrack2, target);
                        break;
                    case "btnPressliftingAbsolute":
                        target = float.Parse(sePressliftingAbsolute.Text);
                        AxisAbsolute(EnumStageAxis.Presslifting, target);
                        break;


                }


            }
        }

        private void btnRelativePos_Chick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string tag = clickedButton.Tag.ToString();

                float target = 0;

                switch (clickedButton.Name)
                {
                    case "btnMaterialBoxXRelative":
                        target = float.Parse(seMaterialBoxXRelative.Text);
                        AxisRelative(EnumStageAxis.MaterialboxX, target);
                        break;
                    case "btnMaterialBoxYRelative":
                        target = float.Parse(seMaterialBoxYRelative.Text);
                        AxisRelative(EnumStageAxis.MaterialboxY, target);
                        break;
                    case "btnMaterialBoxZRelative":
                        target = float.Parse(seMaterialBoxZRelative.Text);
                        AxisRelative(EnumStageAxis.MaterialboxY, target);
                        break;
                    case "btnMaterialBoxTRelative":
                        target = float.Parse(seMaterialBoxTRelative.Text);
                        AxisRelative(EnumStageAxis.MaterialboxT, target);
                        break;
                    case "btnMaterialBoxHRelative":
                        target = float.Parse(seMaterialBoxHRelative.Text);
                        AxisRelative(EnumStageAxis.MaterialboxHook, target);
                        break;
                    case "btnMaterialXRelative":
                        target = float.Parse(seMaterialXRelative.Text);
                        AxisRelative(EnumStageAxis.MaterialX, target);
                        break;
                    case "btnMaterialYRelative":
                        target = float.Parse(seMaterialYRelative.Text);
                        AxisRelative(EnumStageAxis.MaterialY, target);
                        break;
                    case "btnMaterialZRelative":
                        target = float.Parse(seMaterialZRelative.Text);
                        AxisRelative(EnumStageAxis.MaterialZ, target);
                        break;
                    case "btnMaterialHRelative":
                        target = float.Parse(seMaterialHRelative.Text);
                        AxisRelative(EnumStageAxis.MaterialHook, target);
                        break;
                    case "btnOvenTrack1Relative":
                        target = float.Parse(seOvenTrack1Relative.Text);
                        AxisRelative(EnumStageAxis.OverTrack1, target);
                        break;
                    case "btnOvenTrack2Relative":
                        target = float.Parse(seOvenTrack2Relative.Text);
                        AxisRelative(EnumStageAxis.OverTrack2, target);
                        break;
                    case "btnPressliftingRelative":
                        target = float.Parse(sePressliftingRelative.Text);
                        AxisRelative(EnumStageAxis.Presslifting, target);
                        break;


                }


            }

        }

        private void cbSelectAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            curActiveAxis = (EnumStageAxis)(cbSelectAxis.SelectedIndex + 1);
        }

        private void btnChangeCurAxis_Click(object sender, EventArgs e)
        {
            UpdateAxisState();
        }

        private void btnClrAlarm_Click(object sender, EventArgs e)
        {
            _positionSystem.ClrAlarm(curActiveAxis);
        }

        private void btnDisableAlarmLimit_Click(object sender, EventArgs e)
        {
            _positionSystem.DisableAlarmLimit(curActiveAxis);
        }

        private void chOutputIO_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                bool myBoolVariable = false;
                switch (checkBox.Name)
                {
                    case "chTowerYellowLight":
                        myBoolVariable = laTowerYellowLight.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.TowerYellowLight, !myBoolVariable);
                        break;
                    case "chTowerRedLight":
                        myBoolVariable = laTowerRedLight.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        DataModel.Instance.Error = checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.TowerRedLight, !myBoolVariable);
                        break;
                    case "chTowerGreenLight":
                        myBoolVariable = laTowerGreenLight.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        DataModel.Instance.Run = checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.TowerGreenLight, !myBoolVariable);
                        break;

                    case "chBakeOvenAerate":
                        myBoolVariable = laBakeOvenAerate.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BakeOvenAerate, !myBoolVariable);
                        break;
                    case "chBakeOvenCoarseExtractionValve":
                        myBoolVariable = laBakeOvenCoarseExtractionValve.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BakeOvenCoarseExtractionValve, !myBoolVariable);
                        break;
                    case "chBakeOvenFrontStageValve":
                        myBoolVariable = laBakeOvenFrontStageValve.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BakeOvenFrontStageValve, !myBoolVariable);
                        break;
                    case "chBakeOvenPlugInValve":
                        myBoolVariable = laBakeOvenPlugInValve.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BakeOvenPlugInValve, !myBoolVariable);
                        break;
                    case "chBakeOvenMechanicalPump":
                        myBoolVariable = laBakeOvenMechanicalPump.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BakeOvenMechanicalPump, !myBoolVariable);
                        break;
                    case "chBakeOvenInnerdoorUpDown":
                        myBoolVariable = laBakeOvenInnerdoorUpDown.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BakeOvenInnerdoorUpDown, !myBoolVariable);
                        break;

                    case "chBakeOven2Aerate":
                        myBoolVariable = laBakeOven2Aerate.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BakeOven2Aerate, !myBoolVariable);
                        break;
                    case "chBakeOven2CoarseExtractionValve":
                        myBoolVariable = laBakeOven2CoarseExtractionValve.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BakeOven2CoarseExtractionValve, !myBoolVariable);
                        break;
                    case "chBakeOven2FrontStageValve":
                        myBoolVariable = laBakeOven2FrontStageValve.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BakeOven2FrontStageValve, !myBoolVariable);
                        break;
                    case "chBakeOven2PlugInValve":
                        myBoolVariable = laBakeOven2PlugInValve.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BakeOven2PlugInValve, !myBoolVariable);
                        break;
                    case "chBakeOven2MechanicalPump":
                        myBoolVariable = laBakeOven2MechanicalPump.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BakeOven2MechanicalPump, !myBoolVariable);
                        break;
                    case "chBakeOven2InnerdoorUpDown":
                        myBoolVariable = laBakeOven2InnerdoorUpDown.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BakeOven2InnerdoorUpDown, !myBoolVariable);
                        break;


                    case "chBoxAerate":
                        myBoolVariable = laBoxAerate.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BoxAerate, !myBoolVariable);
                        break;
                    case "chBoxCoarseExtractionValve":
                        myBoolVariable = laBoxCoarseExtractionValve.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BoxCoarseExtractionValve, !myBoolVariable);
                        break;
                    case "chBoxFrontStageValve":
                        myBoolVariable = laBoxFrontStageValve.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BoxFrontStageValve, !myBoolVariable);
                        break;
                    case "chBoxPlugInValve":
                        myBoolVariable = laBoxPlugInValve.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BoxPlugInValve, !myBoolVariable);
                        break;
                    case "chBoxMechanicalPump":
                        myBoolVariable = laBoxMechanicalPump.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.BoxMechanicalPump, !myBoolVariable);
                        break;


                    case "chPressPressingDivide":
                        myBoolVariable = laPressPressingDivide.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.PressPressingDivide, !myBoolVariable);
                        break;
                    case "chCompressorStartup":
                        myBoolVariable = laCompressorStartup.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.CompressorStartup, !myBoolVariable);
                        break;
                    case "chCompressorStops":
                        myBoolVariable = laCompressorStops.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.CompressorStops, !myBoolVariable);
                        break;
                    case "chCondenserPump":
                        myBoolVariable = laCondenserPump.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.CondenserPump, !myBoolVariable);
                        break;

                    case "chMotorBrake":
                        myBoolVariable = laMotorBrake.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.MotorBrake, !myBoolVariable);
                        break;
                    case "chMotorBrake1":
                        myBoolVariable = laMotorBrake1.BackColor == Color.GreenYellow;
                        myBoolVariable = !checkBox.Checked;
                        OutputIOChange(EnumBoardcardDefineOutputIO.MotorBrake1, !myBoolVariable);
                        break;


                }


            }
        }




        #endregion

        #region public mothed




        #endregion

        private void btnLimitfailure_Click(object sender, EventArgs e)
        {
            _stageEngine[EnumStageAxis.MaterialboxX].CloseSoftLeftAndRightLimit();
            _stageEngine[EnumStageAxis.MaterialboxY].CloseSoftLeftAndRightLimit();
            _stageEngine[EnumStageAxis.MaterialboxZ].CloseSoftLeftAndRightLimit();
            _stageEngine[EnumStageAxis.MaterialboxT].CloseSoftLeftAndRightLimit();
            _stageEngine[EnumStageAxis.MaterialboxHook].CloseSoftLeftAndRightLimit();
            _stageEngine[EnumStageAxis.MaterialX].CloseSoftLeftAndRightLimit();
            _stageEngine[EnumStageAxis.MaterialY].CloseSoftLeftAndRightLimit();
            _stageEngine[EnumStageAxis.MaterialZ].CloseSoftLeftAndRightLimit();
            _stageEngine[EnumStageAxis.MaterialHook].CloseSoftLeftAndRightLimit();
            _stageEngine[EnumStageAxis.OverTrack1].CloseSoftLeftAndRightLimit();
            _stageEngine[EnumStageAxis.OverTrack2].CloseSoftLeftAndRightLimit();
            _stageEngine[EnumStageAxis.Presslifting].CloseSoftLeftAndRightLimit();

        }

        private void btnLimiteffective_Click(object sender, EventArgs e)
        {
            AxisConfig axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxX);
            //_stageEngine[EnumStageAxis.MaterialboxX].SetAxisMotionParameters(axisConfig);
            _stageEngine[EnumStageAxis.MaterialboxX].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxY);
            //_stageEngine[EnumStageAxis.MaterialboxY].SetAxisMotionParameters(axisConfig);
            _stageEngine[EnumStageAxis.MaterialboxY].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxZ);
            //_stageEngine[EnumStageAxis.MaterialboxZ].SetAxisMotionParameters(axisConfig);
            _stageEngine[EnumStageAxis.MaterialboxZ].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxT);
            //_stageEngine[EnumStageAxis.MaterialboxT].SetAxisMotionParameters(axisConfig);
            _stageEngine[EnumStageAxis.MaterialboxT].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialboxHook);
            //_stageEngine[EnumStageAxis.MaterialboxHook].SetAxisMotionParameters(axisConfig);
            _stageEngine[EnumStageAxis.MaterialboxHook].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);


            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialX);
            //_stageEngine[EnumStageAxis.MaterialX].SetAxisMotionParameters(axisConfig);
            _stageEngine[EnumStageAxis.MaterialX].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialY);
            //_stageEngine[EnumStageAxis.MaterialY].SetAxisMotionParameters(axisConfig);
            _stageEngine[EnumStageAxis.MaterialY].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialZ);
            //_stageEngine[EnumStageAxis.MaterialZ].SetAxisMotionParameters(axisConfig);
            _stageEngine[EnumStageAxis.MaterialZ].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.MaterialHook);
            //_stageEngine[EnumStageAxis.MaterialHook].SetAxisMotionParameters(axisConfig);
            _stageEngine[EnumStageAxis.MaterialHook].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.OverTrack1);
            //_stageEngine[EnumStageAxis.OverTrack1].SetAxisMotionParameters(axisConfig);
            _stageEngine[EnumStageAxis.OverTrack1].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.OverTrack2);
            //_stageEngine[EnumStageAxis.OverTrack2].SetAxisMotionParameters(axisConfig);
            _stageEngine[EnumStageAxis.OverTrack2].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);

            axisConfig = _hardwareConfig.StageConfig.GetAixsConfigByType(EnumStageAxis.Presslifting);
            //_stageEngine[EnumStageAxis.Presslifting].SetAxisMotionParameters(axisConfig);
            _stageEngine[EnumStageAxis.Presslifting].SetSoftLeftAndRightLimit(axisConfig.SoftRightLimit, axisConfig.SoftLeftLimit);
        }

        
    }
}
