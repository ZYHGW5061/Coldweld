using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using PositioningSystemClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WestDragon.Framework.UtilityHelper;

namespace StageCtrlPanelLib
{
    public partial class StageQuickMove : UserControl
    {
        private object _refreshLockObj = new object();
        protected PositioningSystem _positionSystem
        {
            get
            {
                return PositioningSystem.Instance;
            }
        }
        private System.Windows.Forms.Timer _readPosTimer = new System.Windows.Forms.Timer();
        public StageQuickMove()
        {
            InitializeComponent();
            InitialControl();
            cmbSelectStageSystem.SelectedIndex = 0;
            cmbSelectAxis.SelectedIndex = 0;
            //var joyStickController = JoyStickManager.Instance.GetCurrentController();
            //if(joyStickController!=null)
            //{
            //    if(!joyStickController.ActStageSystemChanged.CheckDelegateRegistered((Action<EnumStageSystem>)StageSystemChanged))
            //    {
            //        joyStickController.ActStageSystemChanged += StageSystemChanged;
            //    }
            //    if (!joyStickController.ActSystemAxisChanged.CheckDelegateRegistered((Action<EnumSystemAxis>)SystemAxisChanged))
            //    {
            //        joyStickController.ActSystemAxisChanged += SystemAxisChanged;
            //    }
            //}
            _readPosTimer.Interval = 500;
            _readPosTimer.Tick += OnTimedEvent;
            //_readPosTimer.Stop();
            _readPosTimer.Start();
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            UpdateAxisPosition();
        }

        private void StageSystemChanged(EnumStageSystem obj)
        {
            cmbSelectStageSystem.SelectedIndex = (int)obj;
        }
        private void SystemAxisChanged(EnumSystemAxis obj)
        {
            cmbSelectAxis.SelectedIndex= (int)obj;
        }
        private bool _positiveFlag = false;
        public Action<bool> PositiveQucikMoveAct { get; set; }
        public EnumStageSystem SelectedStageSystem { get; set; }
        public EnumSystemAxis SelectedAxisSystem { get; set; }
        EnumStageAxis yAxis = EnumStageAxis.MaterialboxY;
        EnumStageAxis xAxis = EnumStageAxis.MaterialboxX;
        private void InitialControl()
        {
            foreach (var item in Enum.GetValues(typeof(EnumStageSystem)))
            {
                cmbSelectStageSystem.Items.Add(item);
            }
            foreach (var item in Enum.GetValues(typeof(EnumSystemAxis)))
            {
                cmbSelectAxis.Items.Add(item);
            }
        }


        private void btnControlField_Click(object sender, EventArgs e)
        {
            EnumRunningType type = HardwareConfiguration.Instance.StageConfig.RunningType;
            if (type == EnumRunningType.Simulated)
            {
                return;
            }
            if (_positiveFlag)
            {
                groupBoxControlField.Text = "Position";
                btnControlField.BackColor = Color.Gray;
                _positiveFlag = false;
                //JoyStickManager.Instance.GetCurrentController().HandleEnable(false);
                //if (_HCFAPLCController != null)
                //{
                //    _HCFAPLCController.UnsubscribeAxisPosition(AxisPositionChangeAct);
                //}
                this.btnControlField.MouseMove -= ControlField_MouseMove;

                //JoyStickControl.Instance.JoyStickEnable(false);
                UnsubscribeIO();
            }
            else
            {
                groupBoxControlField.Text = "快捷移动";
                btnControlField.BackColor = Color.Yellow;
                _positiveFlag = true;
                //JoyStickManager.Instance.GetCurrentController().HandleEnable(true);
                //if (_HCFAPLCController != null)
                //{
                //    _HCFAPLCController.SubscribeAxisPosition(AxisPositionChangeAct);
                //}
                this.btnControlField.MouseMove += ControlField_MouseMove;

                //JoyStickControl.Instance.JoyStickEnable(true);
            }
        }



        private void ControlField_MouseMove(object sender, MouseEventArgs e)
        {
            var leftupScreenLoc= btnControlField.PointToScreen(new Point(0,0));
            var rightbottomScreenLoc= btnControlField.PointToScreen(new Point(btnControlField.Width, btnControlField.Height));
            Cursor.Position = new Point(leftupScreenLoc.X + btnControlField.Width / 2, leftupScreenLoc.Y + btnControlField.Height / 2);
        }
        private void btnControlField_KeyUp(object sender, KeyEventArgs e)
        {
            if (_positiveFlag)
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    RefreshCurrentXAxis();
                    if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                    {
                        if (xAxis == EnumStageAxis.None)
                        {
                            return;
                        }
                    }
                    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                    {
                        if (yAxis == EnumStageAxis.None)
                        {
                            return;
                        }
                    }
                    int directFlags_X = 1, directFlags_Y = -1;//X/y方向的标记位
                    int direction = directFlags_X;
                    //if (xAxis == EnumStageAxis.MaterialboxX)
                    //{
                    //    directFlags_X = -1;
                    //}

                    //if (yAxis == EnumStageAxis.ESZ || yAxis == EnumStageAxis.NeedleZ)
                    //{
                    //    directFlags_Y = 1;
                    //}

                    var axis = EnumStageAxis.MaterialboxX;
                    if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                    {
                        axis = xAxis;
                        if (e.KeyCode == Keys.Right)
                        {
                            direction = -directFlags_X;
                        }
                        else
                        {
                            direction = directFlags_X;
                        }
                    }
                    else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                    {
                        axis = yAxis;
                        if (e.KeyCode == Keys.Up)
                        {
                            direction = -directFlags_Y;
                        }
                        else
                        {
                            direction = directFlags_Y;
                        }
                    }

                    if (direction == 1)
                    {
                        _positionSystem.StopJogPositive(axis);
                    }
                    else
                    {
                        _positionSystem.StopJogNegative(axis);
                    }
                    //UpdateAxisPosition();
                }
            }
        }

        private void btnControlField_KeyDown(object sender, KeyEventArgs e)
        {
            if (_positiveFlag)
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    RefreshCurrentXAxis();
                    if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                    {
                        if(xAxis==EnumStageAxis.None)
                        {
                            return;
                        }
                    }
                    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                    {
                        if (yAxis == EnumStageAxis.None)
                        {
                            return;
                        }
                    }
                    var moveSpeed = 5f;
                    var shift = 10f;
                    var ctrl = 0.05f;

                    int directFlags_X = 1, directFlags_Y = -1;//X/Y方向的标记位
                    int direction = directFlags_X;

                    if (xAxis == EnumStageAxis.MaterialboxX)
                    {
                        directFlags_X = -1;
                    }
                    if (xAxis == EnumStageAxis.MaterialX)
                    {
                        directFlags_X = -1;
                    }

                    //if (yAxis == EnumStageAxis.ESZ|| yAxis == EnumStageAxis.NeedleZ)
                    //{
                    //    directFlags_Y = 1;
                    //}
                    //if (yAxis == EnumStageAxis.NeedleZ)
                    //{
                    //    moveSpeed = 0.5f;
                    //}
                    var axis = EnumStageAxis.MaterialboxX;
                    if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                    {
                        axis = xAxis;
                        if (e.KeyCode == Keys.Right)
                        {
                            direction = -directFlags_X;
                        }
                        else
                        {
                            direction = directFlags_X;
                        }
                    }
                    else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                    {
                        axis = yAxis;
                        if (e.KeyCode == Keys.Up)
                        {
                            direction = -directFlags_Y;
                        }
                        else
                        {
                            direction = directFlags_Y;
                        }
                    }
                    //if (axis == EnumStageAxis.ESZ || axis == EnumStageAxis.NeedleZ || axis == EnumStageAxis.BondZ)
                    //{
                    //    moveSpeed = 3f;
                    //}
                    if (e.Control)
                    {
                        moveSpeed = moveSpeed * ctrl;
                    }
                    else if (e.Shift)
                    {
                        moveSpeed = moveSpeed * shift;
                    }
                    if (direction == 1)
                    {
                        _positionSystem.JogPositive(axis, moveSpeed);
                    }
                    else
                    {
                        _positionSystem.JogNegative(axis, moveSpeed);
                    }
                }
            }
        }


        private void cmbSelectStageSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedStageSystem = (EnumStageSystem)this.cmbSelectStageSystem.SelectedIndex;
            RefreshCmbSelectAxisItems(SelectedStageSystem);
        }
        private void RefreshCmbSelectAxisItems(EnumStageSystem stageSystem)
        {          
            cmbSelectAxis.Items.Clear();
            switch (stageSystem)
            {
                case EnumStageSystem.MaterialboxHook:
                    cmbSelectAxis.Items.Add("XY");
                    cmbSelectAxis.Items.Add("Z");
                    cmbSelectAxis.Items.Add("Theta");
                    cmbSelectAxis.Items.Add("Hook");
                    break;
                case EnumStageSystem.MaterialHook:
                    cmbSelectAxis.Items.Add("XY");
                    cmbSelectAxis.Items.Add("Z");
                    cmbSelectAxis.Items.Add("Hook");
                    break;
                case EnumStageSystem.OverTrack1:
                    //cmbSelectAxis.Items.Add("Z");
                    cmbSelectAxis.Items.Add("Track");
                    break;
                case EnumStageSystem.OverTrack2:
                    cmbSelectAxis.Items.Add("Track");
                    break;
                case EnumStageSystem.Weld:
                    cmbSelectAxis.Items.Add("Z");
                    break;
                default:
                    break;
            }
            cmbSelectAxis.SelectedIndex = 0;
        }

        private void cmbSelectAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedAxisSystem = (EnumSystemAxis)Enum.Parse(typeof(EnumSystemAxis),this.cmbSelectAxis.Text);
        }

        private void btnControlField_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }
        private void RefreshCurrentXAxis()
        {
            switch (SelectedStageSystem)
            {
                case EnumStageSystem.MaterialboxHook:
                    switch (SelectedAxisSystem)
                    {
                        case EnumSystemAxis.XY:
                            xAxis = EnumStageAxis.MaterialboxX;
                            yAxis= EnumStageAxis.MaterialboxY;
                            break;
                        case EnumSystemAxis.Z:
                            xAxis = EnumStageAxis.None;
                            yAxis = EnumStageAxis.MaterialboxZ;
                            break;
                        case EnumSystemAxis.Theta:
                            xAxis = EnumStageAxis.MaterialboxT;
                            yAxis = EnumStageAxis.None;
                            break;
                        case EnumSystemAxis.Hook:
                            xAxis = EnumStageAxis.MaterialboxHook;
                            yAxis = EnumStageAxis.None;
                            break;
                        default:
                            xAxis = EnumStageAxis.None;
                            yAxis = EnumStageAxis.None;
                            break;
                    }
                    break;
                case EnumStageSystem.MaterialHook:
                    switch (SelectedAxisSystem)
                    {
                        case EnumSystemAxis.XY:
                            xAxis = EnumStageAxis.MaterialX;
                            yAxis = EnumStageAxis.MaterialY;
                            break;
                        case EnumSystemAxis.Z:
                            xAxis = EnumStageAxis.None;
                            yAxis = EnumStageAxis.MaterialZ;
                            break;
                        case EnumSystemAxis.Hook:
                            xAxis = EnumStageAxis.MaterialHook;
                            yAxis = EnumStageAxis.None;
                            break;
                    }
                    break;
                case EnumStageSystem.OverTrack1:
                    switch (SelectedAxisSystem)
                    {
                        //case EnumSystemAxis.Z:
                        //    xAxis = EnumStageAxis.None;
                        //    yAxis = EnumStageAxis.ChipPPZ;
                        //    break;
                        case EnumSystemAxis.Track:
                            xAxis = EnumStageAxis.OverTrack1;
                            yAxis = EnumStageAxis.None;
                            break;
                        default:
                            xAxis = EnumStageAxis.None;
                            yAxis = EnumStageAxis.None;
                            break;
                    }
                    break;
                case EnumStageSystem.OverTrack2:
                    switch (SelectedAxisSystem)
                    {
                        case EnumSystemAxis.Track:
                            xAxis = EnumStageAxis.OverTrack2;
                            yAxis = EnumStageAxis.None;
                            break;
                        default:
                            xAxis = EnumStageAxis.None;
                            yAxis = EnumStageAxis.None;
                            break;
                    }
                    break;
                case EnumStageSystem.Weld:
                    switch (SelectedAxisSystem)
                    {
                        case EnumSystemAxis.Z:
                            xAxis = EnumStageAxis.None;
                            yAxis = EnumStageAxis.Presslifting;
                            break;
                        default:
                            xAxis = EnumStageAxis.None;
                            yAxis = EnumStageAxis.None;
                            break;
                    }
                    break;
                default:
                    xAxis = EnumStageAxis.None;
                    yAxis = EnumStageAxis.None;
                    break;
            }
        }
        private void UpdateAxisPosition()
        {
            if (xAxis == EnumStageAxis.None && yAxis != EnumStageAxis.None)
            {
                ypos = _positionSystem.ReadCurrentStagePosition(yAxis).ToString("0.000");
                this.labelCurrentAxisPosition.Text = $"{ypos}";
            }
            else if (xAxis != EnumStageAxis.None && yAxis == EnumStageAxis.None)
            {
                xpos = _positionSystem.ReadCurrentStagePosition(xAxis).ToString("0.000");
                this.labelCurrentAxisPosition.Text = $"{xpos}";
            }
            else if (xAxis != EnumStageAxis.None && yAxis != EnumStageAxis.None)
            {
                xpos = _positionSystem.ReadCurrentStagePosition(xAxis).ToString("0.000");
                ypos = _positionSystem.ReadCurrentStagePosition(yAxis).ToString("0.000");
                this.labelCurrentAxisPosition.Text = $"{xpos},{ypos}";
            }
        }
        private void SubscribeIO()
        {
            RefreshCurrentXAxis();
            switch (xAxis)
            {
                case EnumStageAxis.None:
                    break;
                case EnumStageAxis.MaterialboxX:
                    IOManager.Instance.RegisterIOChannelChangedEvent("Stage.MaterialboxXPosition", AxisPositionChangeAct);

                    break;
                case EnumStageAxis.MaterialboxY:
                    IOManager.Instance.RegisterIOChannelChangedEvent("Stage.MaterialboxYPosition", AxisPositionChangeAct);

                    break;
                case EnumStageAxis.MaterialboxZ:
                    IOManager.Instance.RegisterIOChannelChangedEvent("Stage.MaterialboxZPosition", AxisPositionChangeAct);

                    break;
                case EnumStageAxis.MaterialboxT:
                    IOManager.Instance.RegisterIOChannelChangedEvent("Stage.MaterialboxTPosition", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.MaterialboxHook:
                    IOManager.Instance.RegisterIOChannelChangedEvent("Stage.MaterialboxHookPosition", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.MaterialX:
                    IOManager.Instance.RegisterIOChannelChangedEvent("Stage.MaterialXPosition", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.MaterialY:
                    IOManager.Instance.RegisterIOChannelChangedEvent("Stage.MaterialYPosition", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.MaterialZ:
                    IOManager.Instance.RegisterIOChannelChangedEvent("Stage.MaterialZPosition", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.MaterialHook:
                    IOManager.Instance.RegisterIOChannelChangedEvent("Stage.MaterialHookPosition", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.OverTrack1:
                    IOManager.Instance.RegisterIOChannelChangedEvent("Stage.OverTrack1Position", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.OverTrack2:
                    IOManager.Instance.RegisterIOChannelChangedEvent("Stage.OverTrack2Position", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.Presslifting:
                    IOManager.Instance.RegisterIOChannelChangedEvent("Stage.PressliftingPosition", AxisPositionChangeAct);
                    break;
                default:
                    break;
            }

        }
        private void UnsubscribeIO()
        {
            RefreshCurrentXAxis();
            switch (xAxis)
            {
                case EnumStageAxis.None:
                    break;
                case EnumStageAxis.MaterialboxX:
                    IOManager.Instance.UnregisterIOChannelChangedEvent("Stage.MaterialboxXPosition", AxisPositionChangeAct);

                    break;
                case EnumStageAxis.MaterialboxY:
                    IOManager.Instance.UnregisterIOChannelChangedEvent("Stage.MaterialboxYPosition", AxisPositionChangeAct);

                    break;
                case EnumStageAxis.MaterialboxZ:
                    IOManager.Instance.UnregisterIOChannelChangedEvent("Stage.MaterialboxZPosition", AxisPositionChangeAct);

                    break;
                case EnumStageAxis.MaterialboxT:
                    IOManager.Instance.UnregisterIOChannelChangedEvent("Stage.MaterialboxTPosition", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.MaterialboxHook:
                    IOManager.Instance.UnregisterIOChannelChangedEvent("Stage.MaterialboxHookPosition", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.MaterialX:
                    IOManager.Instance.UnregisterIOChannelChangedEvent("Stage.MaterialXPosition", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.MaterialY:
                    IOManager.Instance.UnregisterIOChannelChangedEvent("Stage.MaterialYPosition", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.MaterialZ:
                    IOManager.Instance.UnregisterIOChannelChangedEvent("Stage.MaterialZPosition", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.MaterialHook:
                    IOManager.Instance.UnregisterIOChannelChangedEvent("Stage.MaterialHookPosition", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.OverTrack1:
                    IOManager.Instance.UnregisterIOChannelChangedEvent("Stage.OverTrack1Position", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.OverTrack2:
                    IOManager.Instance.UnregisterIOChannelChangedEvent("Stage.OverTrack2Position", AxisPositionChangeAct);
                    break;
                case EnumStageAxis.Presslifting:
                    IOManager.Instance.UnregisterIOChannelChangedEvent("Stage.PressliftingPosition", AxisPositionChangeAct);
                    break;
                default:
                    break;
            }
        }
        string xpos = "";
        string ypos = "";
        private void AxisPositionChangeAct(string ioName, object preValue, object newValue)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action(() => AxisPositionChangeAct(ioName, preValue, newValue)));
                return;
            }
            try
            {
                lock (_refreshLockObj)
                {
                    RefreshCurrentXAxis();
                    var changedAxis = IOManager.Instance.GetAxisByIOName(ioName);

                    string pos = "";
                    if (xAxis == EnumStageAxis.None && yAxis != EnumStageAxis.None)
                    {
                        if (changedAxis == yAxis)
                        {
                            pos = $"{newValue.ToString()}";
                        }
                    }
                    else if (xAxis != EnumStageAxis.None && yAxis == EnumStageAxis.None)
                    {
                        if (changedAxis == xAxis)
                        {
                            pos = $"{newValue.ToString()}";
                        }
                    }
                    else if (xAxis != EnumStageAxis.None && yAxis != EnumStageAxis.None)
                    {
                        if (changedAxis == xAxis)
                        {
                            xpos = $"{newValue.ToString()}";
                        }
                        if (changedAxis == yAxis)
                        {
                            ypos = $"{newValue.ToString()}";
                        }
                        pos = $"{xpos},{ypos}";
                    }
                    else
                    {
                        pos = "";
                    }

                    this.labelCurrentAxisPosition.Text = pos;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
