using CommonPanelClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using HardwareManagerClsLib;
using RecipeClsLib;
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

namespace ControlPanelClsLib
{
    public partial class FrmTemperatureControlPanel : Form
    {
        private SynchronizationContext _syncContext;

        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }

        private OvenBoxProcessControl _plc
        {
            get { return OvenBoxProcessControl.Instance; }
        }

        private TemperatureControllerManager _TemperatureControllerManager
        {
            get { return TemperatureControllerManager.Instance; }
        }

        public FrmTemperatureControlPanel()
        {
            InitializeComponent();

            //timer1.Enabled = true;

            DataModel.Instance.PropertyChanged += DataModel_PropertyChanged;
            _syncContext = SynchronizationContext.Current;

            seTemp.Value = (decimal)DataModel.Instance.BakeOvenDowntemp;

            laHeat.BackColor = (DataModel.Instance.BakeOvenAutoHeat ? true : false) ? Color.Red : Color.GreenYellow;

            seInsulatedMinutes.Value = (decimal)(DataModel.Instance.HeatPreservationResidueMinute);

            seTemp2.Value = (decimal)DataModel.Instance.BakeOven2Downtemp;

            laHeat2.BackColor = (DataModel.Instance.BakeOven2AutoHeat ? true : false) ? Color.Red : Color.GreenYellow;

            seInsulatedMinutes2.Value = (decimal)(DataModel.Instance.HeatPreservationResidueMinute2);

        }

        private void DataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
            #region 烘箱1

            if (e.PropertyName == nameof(DataModel.BakeOvenDowntemp))
            {
                float BakeOvenDowntemp = DataModel.Instance.BakeOvenDowntemp;
                _syncContext.Post(_ => seTemp.Value = (decimal)(BakeOvenDowntemp), null);

            }

            if (e.PropertyName == nameof(DataModel.BakeOvenAutoHeat))
            {
                _syncContext.Post(_ => laHeat.BackColor = (DataModel.Instance.BakeOvenAutoHeat ? true : false) ? Color.Red : Color.GreenYellow, null);
            }



            if (e.PropertyName == nameof(DataModel.HeatPreservationResidueMinute))
            {
                int HeatPreservationResidueMinute = DataModel.Instance.HeatPreservationResidueMinute;
                _syncContext.Post(_ => seInsulatedMinutes.Value = (decimal)(HeatPreservationResidueMinute), null);

            }

            #endregion

            #region 烘箱2


            if (e.PropertyName == nameof(DataModel.BakeOven2Downtemp))
            {
                float BakeOven2Downtemp = DataModel.Instance.BakeOven2Downtemp;
                _syncContext.Post(_ => seTemp2.Value = (decimal)(BakeOven2Downtemp), null);

            }

            if (e.PropertyName == nameof(DataModel.BakeOven2AutoHeat))
            {
                _syncContext.Post(_ => laHeat2.BackColor = (DataModel.Instance.BakeOven2AutoHeat ? true : false) ? Color.Red : Color.GreenYellow, null);

            }

            if (e.PropertyName == nameof(DataModel.HeatPreservationResidueMinute2))
            {
                int HeatPreservationResidueMinute2 = DataModel.Instance.HeatPreservationResidueMinute2;
                _syncContext.Post(_ => seInsulatedMinutes2.Value = (decimal)(HeatPreservationResidueMinute2), null);

            }


            #endregion


        }

        private void btnHeat_Click(object sender, EventArgs e)
        {
            try
            {
                float BakeOvenTargettemp = float.Parse(seTargetTemp.Text);
                _systemConfig.OvenBoxConfig.HeatTargetTemperature = (int)BakeOvenTargettemp;

                float BakeOvenAlarmtemp = float.Parse(seOverTemperatureThreshold.Text);
                _systemConfig.OvenBoxConfig.OverTemperatureThreshold = (int)BakeOvenAlarmtemp;

                short BakeOvenHoldingTimeM = short.Parse(seHeatPreservationMinute.Text);
                _systemConfig.OvenBoxConfig.HeatPreservationMinute = (int)BakeOvenHoldingTimeM;


                _plc.ManualHeat(EnumOvenBoxNum.Oven1, _systemConfig.OvenBoxConfig.HeatTargetTemperature, _systemConfig.OvenBoxConfig.HeatPreservationMinute, _systemConfig.OvenBoxConfig.OverTemperatureThreshold);
            }
            catch
            {

            }
        }

        private void btnStopHeat_Click(object sender, EventArgs e)
        {
            try
            {
                _plc.StopManualHeat();

            }
            catch
            {

            }
        }

        private void btnSelfTuning_Click(object sender, EventArgs e)
        {
            try
            {
                if (_TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).IsConnect)
                {
                    _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Write(TemperatureRtuAdd.FIX_SV1, (int)seTargetTemp.Value * 10);

                    _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Write(TemperatureRtuAdd.RUN, 1);

                    _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Write(TemperatureRtuAdd.AT, 1);

                }

            }
            catch
            {

            }
        }

        private void btnHeat2_Click(object sender, EventArgs e)
        {
            try
            {
                float BakeOvenTargettemp = float.Parse(seTargetTemp2.Text);
                _systemConfig.OvenBoxConfig.HeatTargetTemperature = (int)BakeOvenTargettemp;

                float BakeOvenAlarmtemp = float.Parse(seOverTemperatureThreshold2.Text);
                _systemConfig.OvenBoxConfig.OverTemperatureThreshold = (int)BakeOvenAlarmtemp;

                short BakeOvenHoldingTimeM = short.Parse(seHeatPreservationMinute2.Text);
                _systemConfig.OvenBoxConfig.HeatPreservationMinute = (int)BakeOvenHoldingTimeM;


                _plc.ManualHeat2(EnumOvenBoxNum.Oven2, _systemConfig.OvenBoxConfig.HeatTargetTemperature, _systemConfig.OvenBoxConfig.HeatPreservationMinute, _systemConfig.OvenBoxConfig.OverTemperatureThreshold);
            }
            catch
            {

            }
        }

        private void btnStopHeat2_Click(object sender, EventArgs e)
        {
            try
            {
                _plc.StopManualHeat2();

            }
            catch
            {

            }
        }

        private void btnSelfTuning2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).IsConnect)
                {
                    _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Write(TemperatureRtuAdd.FIX_SV1, (int)seTargetTemp2.Value * 10);

                    _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Write(TemperatureRtuAdd.RUN, 1);

                    _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Write(TemperatureRtuAdd.AT, 1);

                }

            }
            catch
            {

            }
        }

        private void btnSelectHeatRecipe_Click(object sender, EventArgs e)
        {
            string _selectedHeatRecipeName;
            FrmHeatRecipeSelect selectRecipeDialog = new FrmHeatRecipeSelect(null, this.teTransportRecipeName.Text.ToUpper().Trim());
            if (selectRecipeDialog.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                try
                {
                    _selectedHeatRecipeName = selectRecipeDialog.SelectedRecipeName;
                    //验证Recipe是否完整
                    if (!HeatRecipe.Validate(_selectedHeatRecipeName, selectRecipeDialog.RecipeType))
                    {
                        WarningBox.FormShow("错误！", "配方无效！", "提示");
                        return;
                    }
                    else
                    {
                        //var heatRecipe = TransportRecipe.LoadRecipe(_selectedHeatRecipeName, selectRecipeDialog.RecipeType);
                        teTransportRecipeName.Text = selectRecipeDialog.SelectedRecipeName;
                        var heatRecipe = HeatRecipe.LoadRecipe(selectRecipeDialog.SelectedRecipeName, selectRecipeDialog.RecipeType);

                        seTargetTemp.Value = heatRecipe.TargetTemperature;
                        seHeatPreservationMinute.Value = heatRecipe.HeatPreservationMinute;
                        seOverTemperatureThreshold.Value = heatRecipe.OverTemperatureThreshold;


                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(EnumLogContentType.Error, "JobControlPanel: Exception occured while Loading Recipe.", ex);
                }
            }
        }

        private void btnSelectHeatRecipe2_Click(object sender, EventArgs e)
        {
            string _selectedHeatRecipeName;
            FrmHeatRecipeSelect selectRecipeDialog = new FrmHeatRecipeSelect(null, this.teTransportRecipeName.Text.ToUpper().Trim());
            if (selectRecipeDialog.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                try
                {
                    _selectedHeatRecipeName = selectRecipeDialog.SelectedRecipeName;
                    //验证Recipe是否完整
                    if (!HeatRecipe.Validate(_selectedHeatRecipeName, selectRecipeDialog.RecipeType))
                    {
                        WarningBox.FormShow("错误！", "配方无效！", "提示");
                        return;
                    }
                    else
                    {
                        //var heatRecipe = TransportRecipe.LoadRecipe(_selectedHeatRecipeName, selectRecipeDialog.RecipeType);
                        teTransportRecipeName2.Text = selectRecipeDialog.SelectedRecipeName;
                        var heatRecipe = HeatRecipe.LoadRecipe(selectRecipeDialog.SelectedRecipeName, selectRecipeDialog.RecipeType);

                        seTargetTemp2.Value = heatRecipe.TargetTemperature;
                        seHeatPreservationMinute2.Value = heatRecipe.HeatPreservationMinute;
                        seOverTemperatureThreshold2.Value = heatRecipe.OverTemperatureThreshold;


                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(EnumLogContentType.Error, "JobControlPanel: Exception occured while Loading Recipe.", ex);
                }
            }
        }
    }
}
