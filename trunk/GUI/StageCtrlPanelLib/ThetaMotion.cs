using CommonPanelClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using PositioningSystemClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.UtilityHelper;

namespace StageCtrlPanelLib
{
    public partial class ThetaMotion : BaseUserControl
    {
        public ThetaMotion()
        {
            InitializeComponent();
            //RefreshAxisPosition();
        }
        protected PositioningSystem _positionSystem
        {
            get
            {
                return PositioningSystem.Instance;
            }
        }

        //private void btnPPTJogClockwise_MouseUp(object sender, MouseEventArgs e)
        //{
        //    _positionSystem.StopJogPositive(EnumStageAxis.PPTheta);
        //    RefreshAxisPosition();
        //}
     
        private void btnMianTClockwise90Degree_Click(object sender, EventArgs e)
        {
            try
            {
                CreateWaitDialog();
                _positionSystem.ShellMovePositive90Degree();
                RefreshAxisPosition();
            }
            catch (Exception ex)
            {
                LogRecorder.RecordLog(EnumLogContentType.Error, $"MianTClockwise90Degree,Error.", ex);
            }
            finally
            {
                CloseWaitDialog();
            }
        }

        private void btnMainTContourclockwise90Degree_Click(object sender, EventArgs e)
        {
            try
            {
                CreateWaitDialog();
                _positionSystem.ShellMoveNegative90Degree();
                RefreshAxisPosition();
            }
            catch (Exception ex)
            {
                LogRecorder.RecordLog(EnumLogContentType.Error, $"MainTContourclockwise90Degree,Error.", ex);
            }
            finally
            {
                CloseWaitDialog();
            }
        }
        private void btnMianTClockwise90Degree_MouseUp(object sender, MouseEventArgs e)
        {
            _positionSystem.ShellMovePositive90DegreeReset();
        }

        private void btnMainTContourclockwise90Degree_MouseUp(object sender, MouseEventArgs e)
        {
            _positionSystem.ShellMoveNegative90DegreeReset();
        }
        private void RefreshAxisPosition()
        {
            var pos = _positionSystem.ReadCurrentStagePosition();
            //this.teXLidCurrentStagePos.Text = pos[(int)EnumStageAxis.XLid].ToString();
            //this.teXShellCurrentStagePos.Text = pos[(int)EnumStageAxis.XShell].ToString();
            //this.teXHeadCurrentStagePos.Text = pos[(int)EnumStageAxis.XHead].ToString();

            //this.teMainYCurStagePos.Text = pos[(int)EnumStageAxis.MainY].ToString();
            //this.tePPYCurStagePos.Text = pos[(int)EnumStageAxis.PPY].ToString();

            //this.teZLidCurStagePos.Text = pos[(int)EnumStageAxis.ZLidCamera].ToString();
            //this.teZShellCurStagePos.Text = pos[(int)EnumStageAxis.ZShellCamera].ToString();
            //this.teZHeadCurrentStagePos.Text = pos[(int)EnumStageAxis.ZHead].ToString();
            //this.teZPPCurStagePos.Text = pos[(int)EnumStageAxis.ZPP].ToString();

            //this.teMainTCurrentStagePos.Text = pos[(int)EnumStageAxis.ThetaShell].ToString("0.0000");
            //this.tePPTCurrentStagePos.Text = pos[(int)EnumStageAxis.ThetaPP].ToString("0.0000");

            //this.teZEWCurrentStagePos.Text = pos.ToString("0.000");
        }

        private void StageTMotionController_Load(object sender, EventArgs e)
        {
            RefreshAxisPosition();
        }

        private void btnGetPos_Click(object sender, EventArgs e)
        {
            RefreshAxisPosition();
        }
    }
}
