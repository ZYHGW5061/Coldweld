using CommonPanelClsLib;
using ConfigurationClsLib;
using ControlPanelClsLib.Recipe;
using DevExpress.XtraEditors;
using GlobalDataDefineClsLib;
using PositioningSystemClsLib;
using RecipeClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemCalibrationClsLib;
using TransportPanelClsLib;
using VisionClsLib;
using VisionGUI;
using WestDragon.Framework.UtilityHelper;

namespace ControlPanelClsLib
{
    public partial class FrmEditTransportRecipe : BaseForm
    {
        /// <summary>
        /// 系统配置
        /// </summary>
        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }
        /// <summary>
        /// 数据库
        /// </summary>
        //private DBManager _dataBaseManager
        //{
        //    get { return DBManager.DBManagerHandler; }
        //}
        /// <summary>
        /// RightsData数据操作
        /// </summary>
        //private UserRightsManager _rightsDataManage = UserRightsManager.GetHandler();

        //private DataRow _dataRow = null;
        private TransportRecipe _selRecipe = null;
        private TransportRecipe _newRecipe = null;

        private MaterialRecipe _materialRecipe = null;
        private MaterialBoxRecipe _materialboxRecipe = null;

        private string _operation = "Edit";

        private string _selectedMaterialRecipeName = string.Empty;
        private string _selectedMaterialBoxRecipeName = string.Empty;

        private double Oven1Vacuum = 0;
        private double Oven2Vacuum = 0;
        private double BoxVacuum = 0;
        private double VacuumD = 500;
        private double VacuumC = 11000;


        MatchIdentificationParam TrackCameraIdentifyMaterialBoxMatch = new MatchIdentificationParam();
        MatchIdentificationParam TrackCameraIdentifyMaterialMatch = new MatchIdentificationParam();
        MatchIdentificationParam WeldCameraIdentifyMaterialMatch = new MatchIdentificationParam();

        List<XYZTCoordinateConfig> MaterialHooktoTargetPosition = new List<XYZTCoordinateConfig>();

        /// <summary>
        /// 定位系统
        /// </summary>
        private PositioningSystem _positionSystem
        {
            get { return PositioningSystem.Instance; }
        }

        public FrmEditTransportRecipe(string operation, TransportRecipe recipe)
        {
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
            if (recipe != null)
            {
                _selRecipe = recipe;
                LoadParameters();
                TransportControl.Instance.TransportRecipe = _selRecipe;
            }
            _operation = operation;
            BindingData();
        }

        private void BindingData()
        {
            if (_operation == "Add")
            {
                //this.txtID.Text = (LoginHelper.GetHandler.GetUserMaxID() + 1).ToString();
                this.teName.Text = "";
                this.tableLayoutPanel2.Controls.Add(this.btn_Add, 0, 0);
                this.btn_Add.Visible = true;
                this.btn_Edit.Visible = false;
                this.Text = "新增生产配方";
            }
            else if (_operation == "Edit" && _selRecipe != null)
            {
                this.tableLayoutPanel2.Controls.Add(this.btn_Edit, 0, 0);
                this.btn_Add.Visible = false;
                this.btn_Edit.Visible = true;
                this.teName.Text = _selRecipe.RecipeName;
                //this.seTargetTemp.Value = _selRecipe.TargetTemperature;
                //this.seHeatPreservationMinute.Value = _selRecipe.HeatPreservationMinute;
                //this.seOverTemperatureThreshold.Value = _selRecipe.OverTemperatureThreshold;
                this.Text = "编辑生产配方";
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string name = this.teName.Text.Trim();
            //int targetTemp = (int)this.seTargetTemp.Value;
            //int heatPreservationMinute = (int)this.seHeatPreservationMinute.Value;
            //int overTemperatureThreshold = (int)this.seOverTemperatureThreshold.Value;
            if (Name == "")
            {
                WarningBox.FormShow("错误", "参数不符合要求!", "提示");
                return;
            }
            var newRecipe = TransportRecipe.CreateRecipe(name, EnumRecipeType.Transport);
            newRecipe.RecipeName = name;
            SetParameters(newRecipe);
            //newRecipe.TargetTemperature = targetTemp;
            //newRecipe.HeatPreservationMinute = heatPreservationMinute;
            //newRecipe.OverTemperatureThreshold = overTemperatureThreshold;
            newRecipe.SaveRecipe();
            TransportControl.Instance.TransportRecipe = newRecipe;
            WarningBox.FormShow("成功!", "添加配方完成!", "提示");
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            string name = this.teName.Text.Trim();
            //int targetTemp = (int)this.seTargetTemp.Value;
            //int heatPreservationMinute = (int)this.seHeatPreservationMinute.Value;
            //int overTemperatureThreshold = (int)this.seOverTemperatureThreshold.Value;
            if (Name == "")
            {
                WarningBox.FormShow("错误", "参数不符合要求!", "提示");
                return;
            }
            if (_selRecipe != null)
            {
                //_selRecipe.RecipeName = name;
                //_selRecipe.TargetTemperature = targetTemp;
                //_selRecipe.HeatPreservationMinute = heatPreservationMinute;
                //_selRecipe.OverTemperatureThreshold = overTemperatureThreshold;
                SetParameters();
                _selRecipe.SaveRecipe();
                TransportControl.Instance.TransportRecipe = _selRecipe;
                WarningBox.FormShow("成功!", "编辑配方完成!", "提示");
            }
            else
            {
                WarningBox.FormShow("错误", "编辑的配方异常!", "提示");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }


        private void btnSetHookZPreparePosForPullMB_Click(object sender, EventArgs e)
        {
            //if (ckbManualSet.Checked)
            //{
            //    var setValue = 0f;
            //    if (float.TryParse(this.teHookZPreparePosForPullMB.Text.Trim(), out setValue))
            //    {
            //        _selRecipe.HookAxisZPreparePosForPullMaterialBox = setValue;


            //        WarningFormCL.WarningBox.FormShow("成功", "设置完成。", "提示");
            //    }
            //    else
            //    {
            //        WarningFormCL.WarningBox.FormShow("错误", "设置值无效", "提示");
            //    }
            //}
            //else
            //{
            //    _selRecipe.HookAxisZPreparePosForPullMaterialBox = (float)_positionSystem.ReadCurrentStagePosition(EnumStageAxis.HookZForMaterialbox);

            //    LoadParameters();
            //    WarningFormCL.WarningBox.FormShow("成功", "设置完成。", "提示");
            //}
        }



        private void LoadParameters()
        {

            #region 参数

            if(_selRecipe.OverBox1Param.MaterialboxParam.Count > 1)
            {
                this.teOverBox1SelectMaterialBoxName.Text = _selRecipe.OverBox1Param.MaterialboxParam[0].Name;
                this.numOverBox1MaterialBoxLayerNumber.Value = _selRecipe.OverBox1Param.OverBoxMaterialBoxLayerNumber;
                this.numOverBox1MaterialBoxGetInNumber.Value = _selRecipe.OverBox1Param.OverBoxMaterialBoxGetInNumber;
            }

            if(_selRecipe.OverBox2Param.MaterialboxParam.Count > 1)
            {
                this.teOverBox2SelectMaterialBoxName.Text = _selRecipe.OverBox2Param.MaterialboxParam[0].Name;
                this.numOverBox2MaterialBoxLayerNumber.Value = _selRecipe.OverBox2Param.OverBoxMaterialBoxLayerNumber;
                this.numOverBox2MaterialBoxGetInNumber.Value = _selRecipe.OverBox2Param.OverBoxMaterialBoxGetInNumber;
            }

            
            if(_selRecipe.WeldNum == 0)
            {
                _selRecipe.WeldNum = 4;
            }
            this.numWeldNum.Value = _selRecipe.WeldNum;
            this.MaterialHooktoTargetPosition.Clear();
            if(_selRecipe.MaterialHooktoTargetPosition.Count < _selRecipe.WeldNum)
            {
                _selRecipe.MaterialHooktoTargetPosition.Clear();
                for (int i = 0; i < _selRecipe.WeldNum; i++)
                {
                    this.MaterialHooktoTargetPosition.Add(new XYZTCoordinateConfig());
                    _selRecipe.MaterialHooktoTargetPosition.Add(new XYZTCoordinateConfig());
                }
            }
            else if(_selRecipe.MaterialHooktoTargetPosition.Count == _selRecipe.WeldNum)
            {
                this.MaterialHooktoTargetPosition = _selRecipe.MaterialHooktoTargetPosition;
            }




            #endregion


            #region 料盒搬送


            #region 烘箱1出料

            numMaterialboxHookSafePositionX.Value = (decimal)_selRecipe.MaterialboxHookSafePosition.X;
            numMaterialboxHookSafePositionY.Value = (decimal)_selRecipe.MaterialboxHookSafePosition.Y;
            numMaterialboxHookSafePositionZ.Value = (decimal)_selRecipe.MaterialboxHookSafePosition.Z;
            numMaterialboxHookSafePositionT.Value = (decimal)_selRecipe.MaterialboxHookSafePosition.Theta;
            numMaterialboxHookOpen.Value = (decimal)_selRecipe.MaterialboxHookOpen;

            numOverTrackMaterialboxOutofoven.Value = (decimal)_selRecipe.OverTrackMaterialboxOutofoven;

            numMaterialboxHooktoMaterialboxPosition1X.Value = (decimal)_selRecipe.MaterialboxHooktoMaterialboxPosition1.X;
            numMaterialboxHooktoMaterialboxPosition1Y.Value = (decimal)_selRecipe.MaterialboxHooktoMaterialboxPosition1.Y;
            numMaterialboxHooktoMaterialboxPosition1Z.Value = (decimal)_selRecipe.MaterialboxHooktoMaterialboxPosition1.Z;
            numMaterialboxHooktoMaterialboxPosition1T.Value = (decimal)_selRecipe.MaterialboxHooktoMaterialboxPosition1.Theta;
            numMaterialboxHookClose.Value = (decimal)_selRecipe.MaterialboxHookClose;
            numMaterialboxHookUp.Value = (decimal)_selRecipe.MaterialboxHookUp;

            numMaterialboxHooktoTarget1PositionX.Value = (decimal)_selRecipe.MaterialboxHooktoTarget1Position.X;
            numMaterialboxHooktoTarget1PositionY.Value = (decimal)_selRecipe.MaterialboxHooktoTarget1Position.Y;
            numMaterialboxHooktoTarget1PositionZ.Value = (decimal)_selRecipe.MaterialboxHooktoTarget1Position.Z;
            numMaterialboxHooktoTarget1PositionT.Value = (decimal)_selRecipe.MaterialboxHooktoTarget1Position.Theta;

            #endregion

            #region 烘箱2出料

            numOverTrack2MaterialboxOutofoven.Value = (decimal)_selRecipe.OverTrack2MaterialboxOutofoven;

            numMaterialboxHooktoMaterialboxPosition2X.Value = (decimal)_selRecipe.MaterialboxHooktoMaterialboxPosition2.X;
            numMaterialboxHooktoMaterialboxPosition2Y.Value = (decimal)_selRecipe.MaterialboxHooktoMaterialboxPosition2.Y;
            numMaterialboxHooktoMaterialboxPosition2Z.Value = (decimal)_selRecipe.MaterialboxHooktoMaterialboxPosition2.Z;
            numMaterialboxHooktoMaterialboxPosition2T.Value = (decimal)_selRecipe.MaterialboxHooktoMaterialboxPosition2.Theta;
            numMaterialboxHookClose2.Value = (decimal)_selRecipe.MaterialboxHookClose2;
            numMaterialboxHookUp2.Value = (decimal)_selRecipe.MaterialboxHookUp2;


            #endregion

            #region 料盒搬送

            numMaterialboxHooktoTarget2PositionX.Value = (decimal)_selRecipe.MaterialboxHooktoTarget2Position.X;
            numMaterialboxHooktoTarget2PositionY.Value = (decimal)_selRecipe.MaterialboxHooktoTarget2Position.Y;
            numMaterialboxHooktoTarget2PositionZ.Value = (decimal)_selRecipe.MaterialboxHooktoTarget2Position.Z;
            numMaterialboxHooktoTarget2PositionT.Value = (decimal)_selRecipe.MaterialboxHooktoTarget2Position.Theta;

            numMaterialboxHooktoTarget3PositionX.Value = (decimal)_selRecipe.MaterialboxHooktoTarget3Position.X;
            numMaterialboxHooktoTarget3PositionY.Value = (decimal)_selRecipe.MaterialboxHooktoTarget3Position.Y;
            numMaterialboxHooktoTarget3PositionZ.Value = (decimal)_selRecipe.MaterialboxHooktoTarget3Position.Z;
            numMaterialboxHooktoTarget3PositionT.Value = (decimal)_selRecipe.MaterialboxHooktoTarget3Position.Theta;
            if (_selRecipe.OverBox1Param.MaterialboxParam.Count > 1)
                this.TrackCameraIdentifyMaterialBoxMatch = _selRecipe.OverBox1Param.MaterialboxParam[0].TrackCameraIdentifyMaterialBoxMatch;

            numMaterialboxHooktoTarget4PositionX.Value = (decimal)_selRecipe.MaterialboxHooktoTarget4Position.X;
            numMaterialboxHooktoTarget4PositionY.Value = (decimal)_selRecipe.MaterialboxHooktoTarget4Position.Y;
            numMaterialboxHooktoTarget4PositionZ.Value = (decimal)_selRecipe.MaterialboxHooktoTarget4Position.Z;
            numMaterialboxHooktoTarget4PositionT.Value = (decimal)_selRecipe.MaterialboxHooktoTarget4Position.Theta;


            #endregion

            #region 烘箱1进料

            numMaterialboxHookPickupMaterialboxPositionX.Value = (decimal)_selRecipe.MaterialboxHookPickupMaterialboxPosition.X;
            numMaterialboxHookPickupMaterialboxPositionY.Value = (decimal)_selRecipe.MaterialboxHookPickupMaterialboxPosition.Y;
            numMaterialboxHookPickupMaterialboxPositionZ.Value = (decimal)_selRecipe.MaterialboxHookPickupMaterialboxPosition.Z;
            numMaterialboxHookPickupMaterialboxPositionT.Value = (decimal)_selRecipe.MaterialboxHookPickupMaterialboxPosition.Theta;

            numOverTrack1MaterialboxInofoven.Value = (decimal)_selRecipe.OverTrack1MaterialboxInofoven;


            #endregion

            #region 烘箱2进料

            numMaterialboxHookPickupMaterialboxPosition2X.Value = (decimal)_selRecipe.MaterialboxHookPickupMaterialboxPosition2.X;
            numMaterialboxHookPickupMaterialboxPosition2Y.Value = (decimal)_selRecipe.MaterialboxHookPickupMaterialboxPosition2.Y;
            numMaterialboxHookPickupMaterialboxPosition2Z.Value = (decimal)_selRecipe.MaterialboxHookPickupMaterialboxPosition2.Z;
            numMaterialboxHookPickupMaterialboxPosition2T.Value = (decimal)_selRecipe.MaterialboxHookPickupMaterialboxPosition2.Theta;

            numOverTrack2MaterialboxInofoven.Value = (decimal)_selRecipe.OverTrack2MaterialboxInofoven;

            #endregion


            #endregion


            #region 物料搬送

            #region 夹取物料

            numMaterialHookSafePositionX.Value = (decimal)_selRecipe.MaterialHookSafePosition.X;
            numMaterialHookSafePositionY.Value = (decimal)_selRecipe.MaterialHookSafePosition.Y;
            numMaterialHookSafePositionZ.Value = (decimal)_selRecipe.MaterialHookSafePosition.Z;

            numMaterialHookOpen.Value = (decimal)_selRecipe.MaterialHookOpen;

            numMaterialHookPickupMaterialPositionX.Value = (decimal)_selRecipe.MaterialHookPickupMaterialPosition.X;
            numMaterialHookPickupMaterialPositionY.Value = (decimal)_selRecipe.MaterialHookPickupMaterialPosition.Y;
            numMaterialHookPickupMaterialPositionZ.Value = (decimal)_selRecipe.MaterialHookPickupMaterialPosition.Z;
            numMaterialHookClose.Value = (decimal)_selRecipe.MaterialHookClose;
            numMaterialHookUp.Value = (decimal)_selRecipe.MaterialHookUp;
            numericMaterialCompensationAngle.Value = (decimal)_selRecipe.MaterialCompensationAngle;
            numMaterialCompensationZ.Value = (decimal)_selRecipe.MaterialCompensationZ;
            if (_selRecipe.OverBox1Param.MaterialboxParam.Count > 1)
                this.TrackCameraIdentifyMaterialMatch = _selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.TrackCameraIdentifyMaterialMatch;



            #endregion

            #region 物料到焊台

            //this.MaterialHooktoTargetPosition.Clear();
            //if (_selRecipe.MaterialHooktoTargetPosition.Count == 0)
            //{
            //    for (int i = 0; i < _selRecipe.WeldNum; i++)
            //    {
            //        _selRecipe.MaterialHooktoTargetPosition.Add(new XYZTCoordinateConfig());
            //    }
            //}
            this.MaterialHooktoTargetPosition = _selRecipe.MaterialHooktoTargetPosition;
            numMaterialHooktoTargetPositionX.Value = (decimal)_selRecipe.MaterialHooktoTargetPosition[0].X;
            numMaterialHooktoTargetPositionY.Value = (decimal)_selRecipe.MaterialHooktoTargetPosition[0].Y;
            numMaterialHooktoTargetPositionZ.Value = (decimal)_selRecipe.MaterialHooktoTargetPosition[0].Z;
            comboBox1.SelectedIndex = 0;

            numPressliftingSafePosition.Value = (decimal)_selRecipe.PressliftingSafePosition;
            numPressliftingWorkPosition.Value = (decimal)_selRecipe.PressliftingWorkPosition;
            if (_selRecipe.OverBox1Param.MaterialboxParam.Count > 1)
                this.WeldCameraIdentifyMaterialMatch = _selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatch;
                


            #endregion


            #endregion


            #region 焊接

            numWeldTime.Value = (decimal)_selRecipe.WeldTime;
            numWeldPessure.Value = (decimal)_selRecipe.WeldPessure;


            #endregion



        }
        private void SetParameters(TransportRecipe recipe = null)
        {
            if (recipe == null)
            {
                #region 参数

                MaterialBoxRecipe MBparam = new MaterialBoxRecipe();

                if (this.teOverBox1SelectMaterialBoxName.Text != null)
                {
                    MBparam = MaterialBoxRecipe.LoadRecipe(this.teOverBox1SelectMaterialBoxName.Text, EnumRecipeType.MaterialBox);
                }
                else
                {
                    return;
                }

                _selRecipe.OverBox1Param.MaterialboxParam.Clear();
                _selRecipe.OverBox1Param.MaterialboxParam.Add(MBparam.MaterialBoxParam);
                _selRecipe.OverBox1Param.MaterialboxParam.Add(MBparam.MaterialBoxParam);
                _selRecipe.OverBox1Param.OverBoxMaterialBoxLayerNumber = (int)this.numOverBox1MaterialBoxLayerNumber.Value;
                _selRecipe.OverBox1Param.OverBoxMaterialBoxGetInNumber = (int)this.numOverBox1MaterialBoxGetInNumber.Value;

                if (this.teOverBox2SelectMaterialBoxName.Text != null)
                {
                    MBparam = MaterialBoxRecipe.LoadRecipe(this.teOverBox2SelectMaterialBoxName.Text, EnumRecipeType.MaterialBox);
                }
                else
                {

                }
                _selRecipe.OverBox2Param.MaterialboxParam.Clear();
                _selRecipe.OverBox2Param.MaterialboxParam.Add(MBparam.MaterialBoxParam);
                _selRecipe.OverBox2Param.MaterialboxParam.Add(MBparam.MaterialBoxParam);
                _selRecipe.OverBox2Param.OverBoxMaterialBoxLayerNumber = (int)this.numOverBox2MaterialBoxLayerNumber.Value;
                _selRecipe.OverBox2Param.OverBoxMaterialBoxGetInNumber = (int)this.numOverBox2MaterialBoxGetInNumber.Value;

                _selRecipe.WeldNum = (int)this.numWeldNum.Value;
                

                
                

                #endregion


                #region 料盒搬送


                #region 烘箱1出料

                _selRecipe.MaterialboxHookSafePosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookSafePositionX.Value,
                    Y = (double)numMaterialboxHookSafePositionY.Value,
                    Z = (double)numMaterialboxHookSafePositionZ.Value,
                    Theta = (double)numMaterialboxHookSafePositionT.Value,
                };
                _selRecipe.MaterialboxHookOpen = (float)numMaterialboxHookOpen.Value;

                _selRecipe.OverTrackMaterialboxOutofoven = (float)numOverTrackMaterialboxOutofoven.Value;

                _selRecipe.MaterialboxHooktoMaterialboxPosition1 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition1X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition1Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition1Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition1T.Value,
                };
                _selRecipe.MaterialboxHookClose = (float)numMaterialboxHookClose.Value;
                _selRecipe.MaterialboxHookUp = (float)numMaterialboxHookUp.Value;

                _selRecipe.MaterialboxHooktoTarget1Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget1PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget1PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget1PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget1PositionT.Value,
                };

                #endregion

                #region 烘箱2出料

                _selRecipe.OverTrack2MaterialboxOutofoven = (float)numOverTrack2MaterialboxOutofoven.Value;

                _selRecipe.MaterialboxHooktoMaterialboxPosition2 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition2T.Value,
                };
                _selRecipe.MaterialboxHookClose2 = (float)numMaterialboxHookClose2.Value;
                _selRecipe.MaterialboxHookUp2 = (float)numMaterialboxHookUp2.Value;

                //_selRecipe.MaterialboxHooktoTarget1Position2 = new XYZTCoordinateConfig()
                //{
                //    X = (double)numMaterialboxHooktoTarget1Position2X.Value,
                //    Y = (double)numMaterialboxHooktoTarget1Position2Y.Value,
                //    Z = (double)numMaterialboxHooktoTarget1Position2Z.Value,
                //    Theta = (double)numMaterialboxHooktoTarget1Position2T.Value,
                //};

                #endregion

                #region 料盒搬送

                _selRecipe.MaterialboxHooktoTarget2Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget2PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget2PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget2PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget2PositionT.Value,
                };

                _selRecipe.MaterialboxHooktoTarget3Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget3PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget3PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget3PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget3PositionT.Value,
                };
                _selRecipe.OverBox1Param.MaterialboxParam[0].TrackCameraIdentifyMaterialBoxMatch = this.TrackCameraIdentifyMaterialBoxMatch;

                _selRecipe.MaterialboxHooktoTarget4Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget4PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget4PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget4PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget4PositionT.Value,
                };


                #endregion

                #region 烘箱1进料

                _selRecipe.MaterialboxHookPickupMaterialboxPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookPickupMaterialboxPositionX.Value,
                    Y = (double)numMaterialboxHookPickupMaterialboxPositionY.Value,
                    Z = (double)numMaterialboxHookPickupMaterialboxPositionZ.Value,
                    Theta = (double)numMaterialboxHookPickupMaterialboxPositionT.Value,
                };

                _selRecipe.OverTrack1MaterialboxInofoven = (float)numOverTrack1MaterialboxInofoven.Value;



                #endregion

                #region 烘箱2进料

                _selRecipe.MaterialboxHookPickupMaterialboxPosition2 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookPickupMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHookPickupMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHookPickupMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHookPickupMaterialboxPosition2T.Value,
                };

                _selRecipe.OverTrack2MaterialboxInofoven = (float)numOverTrack2MaterialboxInofoven.Value;


                #endregion


                #endregion


                #region 物料搬送

                #region 夹取物料

                _selRecipe.MaterialHookSafePosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialHookSafePositionX.Value,
                    Y = (double)numMaterialHookSafePositionY.Value,
                    Z = (double)numMaterialHookSafePositionZ.Value,
                };
                _selRecipe.MaterialHookOpen = (float)numMaterialHookOpen.Value;

                _selRecipe.MaterialHookPickupMaterialPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialHookPickupMaterialPositionX.Value,
                    Y = (double)numMaterialHookPickupMaterialPositionY.Value,
                    Z = (double)numMaterialHookPickupMaterialPositionZ.Value,
                };
                _selRecipe.MaterialHookClose = (float)numMaterialHookClose.Value;
                _selRecipe.MaterialHookUp = (float)numMaterialHookUp.Value;
                _selRecipe.MaterialCompensationAngle = (float)numericMaterialCompensationAngle.Value;
                _selRecipe.MaterialCompensationZ = (float)numMaterialCompensationZ.Value;
                _selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.TrackCameraIdentifyMaterialMatch = this.TrackCameraIdentifyMaterialMatch;



                #endregion

                #region 物料到焊台

                if (_selRecipe.MaterialHooktoTargetPosition?.Count < _selRecipe.WeldNum)
                {
                    _selRecipe.MaterialHooktoTargetPosition.Clear();
                    for (int i = 0; i < _selRecipe.WeldNum; i++)
                    {
                        _selRecipe.MaterialHooktoTargetPosition.Add(new XYZTCoordinateConfig());
                    }
                }

                if (this.MaterialHooktoTargetPosition?.Count < _selRecipe.WeldNum)
                {
                    this.MaterialHooktoTargetPosition.Clear();
                    for (int i = 0; i < _selRecipe.WeldNum; i++)
                    {
                        this.MaterialHooktoTargetPosition.Add(new XYZTCoordinateConfig());
                    }
                }
                else
                {
                    _selRecipe.MaterialHooktoTargetPosition = this.MaterialHooktoTargetPosition;
                }

                _selRecipe.PressliftingSafePosition = (float)numPressliftingSafePosition.Value;
                _selRecipe.PressliftingWorkPosition = (float)numPressliftingWorkPosition.Value;
                _selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatch = this.WeldCameraIdentifyMaterialMatch;



                #endregion


                #endregion


                #region 焊接

                _selRecipe.WeldTime = (float)numWeldTime.Value;
                _selRecipe.WeldPessure = (float)numWeldPessure.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;


                #endregion


            }
            else
            {
                #region 参数

                MaterialBoxRecipe MBparam = new MaterialBoxRecipe();

                if (this.teOverBox1SelectMaterialBoxName.Text != null)
                {
                    MBparam = MaterialBoxRecipe.LoadRecipe(this.teOverBox1SelectMaterialBoxName.Text, EnumRecipeType.MaterialBox);
                }
                else
                {
                    return;
                }

                recipe.OverBox1Param.MaterialboxParam.Clear();
                recipe.OverBox1Param.MaterialboxParam.Add(MBparam.MaterialBoxParam);
                recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber = (int)this.numOverBox1MaterialBoxLayerNumber.Value;
                recipe.OverBox1Param.OverBoxMaterialBoxGetInNumber = (int)this.numOverBox1MaterialBoxGetInNumber.Value;

                if (this.teOverBox2SelectMaterialBoxName.Text != null)
                {
                    MBparam = MaterialBoxRecipe.LoadRecipe(this.teOverBox2SelectMaterialBoxName.Text, EnumRecipeType.MaterialBox);
                }
                else
                {

                }
                recipe.OverBox2Param.MaterialboxParam.Clear();
                recipe.OverBox2Param.MaterialboxParam.Add(MBparam.MaterialBoxParam);
                recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber = (int)this.numOverBox2MaterialBoxLayerNumber.Value;
                recipe.OverBox2Param.OverBoxMaterialBoxGetInNumber = (int)this.numOverBox2MaterialBoxGetInNumber.Value;

                recipe.WeldNum = (int)this.numWeldNum.Value;

                #endregion


                #region 料盒搬送


                #region 烘箱1出料

                recipe.MaterialboxHookSafePosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookSafePositionX.Value,
                    Y = (double)numMaterialboxHookSafePositionY.Value,
                    Z = (double)numMaterialboxHookSafePositionZ.Value,
                    Theta = (double)numMaterialboxHookSafePositionT.Value,
                };
                recipe.MaterialboxHookOpen = (float)numMaterialboxHookOpen.Value;

                recipe.OverTrackMaterialboxOutofoven = (float)numOverTrackMaterialboxOutofoven.Value;

                recipe.MaterialboxHooktoMaterialboxPosition1 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition1X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition1Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition1Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition1T.Value,
                };
                recipe.MaterialboxHookClose = (float)numMaterialboxHookClose.Value;
                recipe.MaterialboxHookUp = (float)numMaterialboxHookUp.Value;

                recipe.MaterialboxHooktoTarget1Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget1PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget1PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget1PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget1PositionT.Value,
                };

                #endregion

                #region 烘箱2出料

                recipe.OverTrack2MaterialboxOutofoven = (float)numOverTrack2MaterialboxOutofoven.Value;

                recipe.MaterialboxHooktoMaterialboxPosition2 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition2T.Value,
                };
                recipe.MaterialboxHookClose2 = (float)numMaterialboxHookClose2.Value;
                recipe.MaterialboxHookUp2 = (float)numMaterialboxHookUp2.Value;

                //recipe.MaterialboxHooktoTarget1Position2 = new XYZTCoordinateConfig()
                //{
                //    X = (double)numMaterialboxHooktoTarget1Position2X.Value,
                //    Y = (double)numMaterialboxHooktoTarget1Position2Y.Value,
                //    Z = (double)numMaterialboxHooktoTarget1Position2Z.Value,
                //    Theta = (double)numMaterialboxHooktoTarget1Position2T.Value,
                //};

                #endregion

                #region 料盒搬送

                recipe.MaterialboxHooktoTarget2Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget2PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget2PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget2PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget2PositionT.Value,
                };

                recipe.MaterialboxHooktoTarget3Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget3PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget3PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget3PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget3PositionT.Value,
                };
                recipe.OverBox1Param.MaterialboxParam[0].TrackCameraIdentifyMaterialBoxMatch = this.TrackCameraIdentifyMaterialBoxMatch;

                recipe.MaterialboxHooktoTarget4Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget4PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget4PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget4PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget4PositionT.Value,
                };


                #endregion

                #region 烘箱1进料

                recipe.MaterialboxHookPickupMaterialboxPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookPickupMaterialboxPositionX.Value,
                    Y = (double)numMaterialboxHookPickupMaterialboxPositionY.Value,
                    Z = (double)numMaterialboxHookPickupMaterialboxPositionZ.Value,
                    Theta = (double)numMaterialboxHookPickupMaterialboxPositionT.Value,
                };

                recipe.OverTrack1MaterialboxInofoven = (float)numOverTrack1MaterialboxInofoven.Value;



                #endregion

                #region 烘箱2进料

                recipe.MaterialboxHookPickupMaterialboxPosition2 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookPickupMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHookPickupMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHookPickupMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHookPickupMaterialboxPosition2T.Value,
                };

                recipe.OverTrack2MaterialboxInofoven = (float)numOverTrack2MaterialboxInofoven.Value;


                #endregion


                #endregion


                #region 物料搬送

                #region 夹取物料

                recipe.MaterialHookSafePosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialHookSafePositionX.Value,
                    Y = (double)numMaterialHookSafePositionY.Value,
                    Z = (double)numMaterialHookSafePositionZ.Value,
                };
                recipe.MaterialHookOpen = (float)numMaterialHookOpen.Value;

                recipe.MaterialHookPickupMaterialPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialHookPickupMaterialPositionX.Value,
                    Y = (double)numMaterialHookPickupMaterialPositionY.Value,
                    Z = (double)numMaterialHookPickupMaterialPositionZ.Value,
                };
                recipe.MaterialHookClose = (float)numMaterialHookClose.Value;
                recipe.MaterialHookUp = (float)numMaterialHookUp.Value;
                recipe.MaterialCompensationAngle = (float)numericMaterialCompensationAngle.Value;
                recipe.MaterialCompensationZ = (float)numMaterialCompensationZ.Value;
                recipe.OverBox1Param.MaterialboxParam[0].MaterialParam.TrackCameraIdentifyMaterialMatch = this.TrackCameraIdentifyMaterialMatch;



                #endregion

                #region 物料到焊台

                if (recipe.MaterialHooktoTargetPosition?.Count < recipe.WeldNum)
                {
                    recipe.MaterialHooktoTargetPosition.Clear();
                    for (int i = 0; i < recipe.WeldNum; i++)
                    {
                        recipe.MaterialHooktoTargetPosition.Add(new XYZTCoordinateConfig());
                    }
                }

                if (this.MaterialHooktoTargetPosition?.Count < recipe.WeldNum)
                {
                    this.MaterialHooktoTargetPosition.Clear();
                    for (int i = 0; i < recipe.WeldNum; i++)
                    {
                        this.MaterialHooktoTargetPosition.Add(new XYZTCoordinateConfig());
                    }
                }
                else
                {
                    recipe.MaterialHooktoTargetPosition = this.MaterialHooktoTargetPosition;
                }



                recipe.PressliftingSafePosition = (float)numPressliftingSafePosition.Value;
                recipe.PressliftingWorkPosition = (float)numPressliftingWorkPosition.Value;
                recipe.OverBox1Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatch = this.WeldCameraIdentifyMaterialMatch;



                #endregion


                #endregion


                #region 焊接

                recipe.WeldTime = (float)numWeldTime.Value;
                recipe.WeldPessure = (float)numWeldPessure.Value;


                #endregion

                TransportControl.Instance.TransportRecipe = recipe;

            }


        }

        private void btnAutoCal_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 检查当前Recipe中该名称是否已存在,若已存在,则返回false
        /// </summary>
        /// <param name="newRecipeName">新名称</param>
        /// <returns></returns>
        private bool IsExistRecipeName(string newRecipeName, EnumRecipeType type)
        {

            string recipeDir = string.Format(@"{0}Recipes\{1}", _systemConfig.SystemDefaultDirectory, type.ToString());
            if (!Directory.Exists(recipeDir))
            {
                Directory.CreateDirectory(recipeDir);
                Console.WriteLine("Directory created: " + recipeDir);
            }
            else
            {
                Console.WriteLine("Directory already exists: " + recipeDir);
            }
            string[] recipeFiles = Directory.GetDirectories(recipeDir);
            foreach (string filePath in recipeFiles)
            {
                string recipeName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                if (recipeName.Equals(newRecipeName)) return true;
            }
            return false;
        }




        #region 参数控件方法

        #region 物料参数


        private void btnSelectMaterial_Click(object sender, EventArgs e)
        {
            FrmMaterialRecipeEditor selectRecipeDialog = new FrmMaterialRecipeEditor(null, this.teMaterialName.Text.ToUpper().Trim());
            if (selectRecipeDialog.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                try
                {
                    _selectedMaterialRecipeName = selectRecipeDialog.SelectedRecipeName;
                    //验证Recipe是否完整
                    if (!MaterialBoxRecipe.Validate(_selectedMaterialRecipeName, selectRecipeDialog.RecipeType))
                    {
                        WarningBox.FormShow("错误！", "配方无效！", "提示");
                        return;
                    }
                    else
                    {
                        //var heatRecipe = TransportRecipe.LoadRecipe(_selectedHeatRecipeName, selectRecipeDialog.RecipeType);
                        teMaterialName.Text = selectRecipeDialog.SelectedRecipeName;
                        _materialRecipe = MaterialRecipe.LoadRecipe(selectRecipeDialog.SelectedRecipeName, EnumRecipeType.Material);
                        numMaterialLength.Value = (decimal)_materialRecipe.MaterialParam.MaterialSize.X;
                        numMaterialWidth.Value = (decimal)_materialRecipe.MaterialParam.MaterialSize.Y;
                        numMaterialHeight.Value = (decimal)_materialRecipe.MaterialParam.MaterialSize.Z;
                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(EnumLogContentType.Error, "JobControlPanel: Exception occured while Loading Recipe.", ex);
                }
            }
        }

        private void btnNewMaterial_Click(object sender, EventArgs e)
        {
            MaterialRecipe recipe = null;
            var addProductDialog = new AddNewRecipeForm();
            if (addProductDialog.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                string newRecipeName = addProductDialog.RecipeName;

                var templateFolderName = $@"{_systemConfig.SystemDefaultDirectory}Recipes\{EnumRecipeType.Material.ToString()}\{newRecipeName}\TemplateConfig\";

                var templateTrainFileName = Path.Combine(templateFolderName, $"VisionTemplateFile_{newRecipeName}_TrackCameraIdentifyMaterialMatch.contourmxml");
                var templateTrainParamName = Path.Combine(templateFolderName, $"VisionTemplateFile_{newRecipeName}_TrackCameraIdentifyMaterialMatchTrainParam.xml");
                var templateRunFileName = Path.Combine(templateFolderName, $"VisionTemplateFile_{newRecipeName}_TrackCameraIdentifyMaterialMatchRun.xml");

                var templateTrainFileName1 = Path.Combine(templateFolderName, $"VisionTemplateFile_{newRecipeName}_WeldCameraIdentifyMaterialMatch.contourmxml");
                var templateTrainParamName1 = Path.Combine(templateFolderName, $"VisionTemplateFile_{newRecipeName}_WeldCameraIdentifyMaterialMatchTrainParam.xml");
                var templateRunFileName1 = Path.Combine(templateFolderName, $"VisionTemplateFile_{newRecipeName}_WeldCameraIdentifyMaterialMatchRun.xml");


                if (!IsExistRecipeName(newRecipeName, EnumRecipeType.Material))
                {
                    CommonProcess.EnsureFolderExist(templateFolderName);
                    MaterialRecipe recipe0 = new MaterialRecipe()
                    {
                        RecipeName = newRecipeName,

                        MaterialParam = new EnumTrainsportMaterialParam()
                        {
                            MaterialSize = new XYZTCoordinateConfig() { X = 8, Y = 8, Z = 8 },
                            Name = newRecipeName,
                            TrackCameraIdentifyMaterialMatch = new MatchIdentificationParam()
                            {
                                RingLightintensity = 0,
                                DirectLightintensity = 0,
                                Templatexml = templateTrainFileName,
                                TemplateParamxml = templateTrainParamName,
                                Runxml = templateRunFileName,
                                Score = 0.4f,
                                MinAngle = -45,
                                MaxAngle = 45,
                                TemplateRoi = new RectangleFV(),
                                SearchRoi = new RectangleFV()
                            },
                            WeldCameraIdentifyMaterialMatch = new MatchIdentificationParam()
                            {
                                RingLightintensity = 0,
                                DirectLightintensity = 0,
                                Templatexml = templateTrainFileName1,
                                TemplateParamxml = templateTrainParamName1,
                                Runxml = templateRunFileName1,
                                Score = 0.4f,
                                MinAngle = -10,
                                MaxAngle = 10,
                                TemplateRoi = new RectangleFV(),
                                SearchRoi = new RectangleFV()
                            },
                            WeldCameraIdentifyMaterialMatchs = new List<MatchIdentificationParam>(),

                        },
                    };

                    teMaterialName.Text = newRecipeName;


                    string fullRecipeName = string.Format(@"{0}Recipes\{1}\{2}\{3}.xml", _systemConfig.SystemDefaultDirectory, EnumRecipeType.Material.ToString(), newRecipeName, newRecipeName);
                    string fullRecipeFolder = string.Format(@"{0}Recipes\{1}\{2}\", _systemConfig.SystemDefaultDirectory, EnumRecipeType.Material.ToString(), newRecipeName);

                    recipe0.SaveRecipe(fullRecipeName, fullRecipeFolder, EnumRecipeStep.Create);
                    recipe = recipe0;
                    _materialRecipe = recipe;
                    numMaterialLength.Value = (decimal)_materialRecipe.MaterialParam.MaterialSize.X;
                    numMaterialWidth.Value = (decimal)_materialRecipe.MaterialParam.MaterialSize.Y;
                    numMaterialHeight.Value = (decimal)_materialRecipe.MaterialParam.MaterialSize.Z;
                }
                else
                {
                    recipe = null;
                    _materialRecipe = null;
                    XtraMessageBox.Show(string.Format("Already exist recipe: {0} ,Please try it again!", newRecipeName), "Warning");
                }
            }
            addProductDialog.Dispose();
            //return recipe;
        }

        private void btnDeleteMaterial_Click(object sender, EventArgs e)
        {
            if ((XtraMessageBox.Show("Are you sure delete this recipe！", "warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)) == DialogResult.OK)
            {
                var recipe = this._materialRecipe;

                if (!IsExistRecipeName(recipe.RecipeName, EnumRecipeType.Material))
                {
                    XtraMessageBox.Show(string.Format("Make sure the recipe {0} is closed!", recipe.RecipeName), "Warning");
                    return;
                }

                //删除Recipe内容
                recipe.Delete();

                numMaterialLength.Value = 0;
                numMaterialWidth.Value = 0;
                numMaterialHeight.Value = 0;

                teMaterialName.Text = "";
            }

        }

        private void btnSaveMaterial_Click(object sender, EventArgs e)
        {
            if ((XtraMessageBox.Show("Are you sure save this recipe！", "warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)) == DialogResult.OK)
            {
                var recipe = this._materialRecipe;

                if (!IsExistRecipeName(recipe.RecipeName, EnumRecipeType.Material))
                {
                    XtraMessageBox.Show(string.Format("Make sure the recipe {0} is exist!", recipe.RecipeName), "Warning");
                    return;
                }

                _materialRecipe.MaterialParam.MaterialSize = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialLength.Value,
                    Y = (double)numMaterialWidth.Value,
                    Z = (double)numMaterialHeight.Value,
                };

                //保存Recipe内容
                recipe.SaveRecipe();
            }
        }

        #endregion


        #region 料盘参数


        private void btnSelectMaterialBox_Click(object sender, EventArgs e)
        {
            FrmMaterialBoxRecipeEditor selectRecipeDialog = new FrmMaterialBoxRecipeEditor(null, this.teMaterialBoxName.Text.ToUpper().Trim());
            if (selectRecipeDialog.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                try
                {
                    _selectedMaterialBoxRecipeName = selectRecipeDialog.SelectedRecipeName;
                    //验证Recipe是否完整
                    if (!MaterialBoxRecipe.Validate(_selectedMaterialBoxRecipeName, selectRecipeDialog.RecipeType))
                    {
                        WarningBox.FormShow("错误！", "配方无效！", "提示");
                        return;
                    }
                    else
                    {
                        //var heatRecipe = TransportRecipe.LoadRecipe(_selectedHeatRecipeName, selectRecipeDialog.RecipeType);
                        teMaterialBoxName.Text = selectRecipeDialog.SelectedRecipeName;
                        _materialboxRecipe = MaterialBoxRecipe.LoadRecipe(selectRecipeDialog.SelectedRecipeName, selectRecipeDialog.RecipeType);

                        teMaterialBoxSelectMaterialName.Text = _materialboxRecipe.MaterialBoxParam.MaterialParam.Name;

                        numMaterialBoxLength.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialboxSize.X;
                        numMaterialBoxWidth.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialboxSize.Y;
                        numMaterialBoxHeight.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialboxSize.Z;

                        numMaterialRows.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialRowNumber;
                        numMaterialCols.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialColNumber;

                        numMaterialRowinterval.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialRowinterval;
                        numMaterialColinterval.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialColinterval;



                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(EnumLogContentType.Error, "JobControlPanel: Exception occured while Loading Recipe.", ex);
                }
            }
        }

        private void btnNewMaterialBox_Click(object sender, EventArgs e)
        {
            MaterialBoxRecipe recipe = null;
            var addProductDialog = new AddNewRecipeForm();
            if (addProductDialog.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                string newRecipeName = addProductDialog.RecipeName;

                var templateFolderName = $@"{_systemConfig.SystemDefaultDirectory}Recipes\{EnumRecipeType.MaterialBox.ToString()}\{newRecipeName}\TemplateConfig\";

                var templateTrainFileName = Path.Combine(templateFolderName, $"VisionTemplateFile_{newRecipeName}_TrackCameraIdentifyMaterialBoxMatch.contourmxml");
                var templateTrainParamName = Path.Combine(templateFolderName, $"VisionTemplateFile_{newRecipeName}_TrackCameraIdentifyMaterialBoxMatchTrainParam.xml");
                var templateRunFileName = Path.Combine(templateFolderName, $"VisionTemplateFile_{newRecipeName}_TrackCameraIdentifyMaterialBoxMatchRun.xml");


                if (!IsExistRecipeName(newRecipeName, EnumRecipeType.MaterialBox))
                {
                    CommonProcess.EnsureFolderExist(templateFolderName);
                    MaterialBoxRecipe recipe0 = new MaterialBoxRecipe()
                    {
                        RecipeName = newRecipeName,

                        MaterialBoxParam = new EnumTrainsportMaterialboxParam()
                        {
                            MaterialboxSize = new XYZTCoordinateConfig() { X = 100, Y = 100, Z = 20 },
                            Name = newRecipeName,
                            MaterialRowNumber = 15,
                            MaterialColNumber = 15,
                            MaterialRowinterval = 10,
                            MaterialColinterval = 10,
                            TrackCameraIdentifyMaterialBoxMatch = new MatchIdentificationParam()
                            {
                                RingLightintensity = 0,
                                DirectLightintensity = 0,
                                Templatexml = templateTrainFileName,
                                TemplateParamxml = templateTrainParamName,
                                Runxml = templateRunFileName,
                                Score = 0.4f,
                                MinAngle = -45,
                                MaxAngle = 45,
                                TemplateRoi = new RectangleFV(),
                                SearchRoi = new RectangleFV()
                            },

                        },
                    };

                    for (int i = 0; i < recipe0.MaterialBoxParam.MaterialRowNumber; i++)
                    {
                        List<EnumMaterialproperties> row = new List<EnumMaterialproperties>();
                        for (int j = 0; j < recipe0.MaterialBoxParam.MaterialColNumber; j++)
                        {
                            row.Add(new EnumMaterialproperties()
                            {
                                Materialstate = EnumMaterialstate.Unwelded,
                                MaterialPosition = new XYZTCoordinateConfig(),
                                MaterialRowNumber = i,
                                MaterialColNumber = j,

                            });
                        }
                        recipe0.MaterialBoxParam.MaterialMat.Add(row);
                    }

                    teMaterialBoxName.Text = newRecipeName;


                    string fullRecipeName = string.Format(@"{0}Recipes\{1}\{2}\{3}.xml", _systemConfig.SystemDefaultDirectory, EnumRecipeType.MaterialBox.ToString(), newRecipeName, newRecipeName);
                    string fullRecipeFolder = string.Format(@"{0}Recipes\{1}\{2}\", _systemConfig.SystemDefaultDirectory, EnumRecipeType.MaterialBox.ToString(), newRecipeName);

                    recipe0.SaveRecipe(fullRecipeName, fullRecipeFolder, EnumRecipeStep.Create);
                    recipe = recipe0;
                    _materialboxRecipe = recipe;

                    teMaterialBoxSelectMaterialName.Text = _materialboxRecipe.MaterialBoxParam.MaterialParam.Name;

                    numMaterialBoxLength.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialboxSize.X;
                    numMaterialBoxWidth.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialboxSize.Y;
                    numMaterialBoxHeight.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialboxSize.Z;

                    numMaterialRows.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialRowNumber;
                    numMaterialCols.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialColNumber;

                    numMaterialRowinterval.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialRowinterval;
                    numMaterialColinterval.Value = (decimal)_materialboxRecipe.MaterialBoxParam.MaterialColinterval;

                }
                else
                {
                    recipe = null;
                    _materialboxRecipe = null;
                    XtraMessageBox.Show(string.Format("Already exist recipe: {0} ,Please try it again!", newRecipeName), "Warning");
                }
            }
            addProductDialog.Dispose();
            //return recipe;
        }

        private void btnDeleteMaterialBox_Click(object sender, EventArgs e)
        {
            if ((XtraMessageBox.Show("Are you sure delete this recipe！", "warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)) == DialogResult.OK)
            {
                var recipe = this._materialboxRecipe;

                if (!IsExistRecipeName(recipe.RecipeName, EnumRecipeType.MaterialBox))
                {
                    XtraMessageBox.Show(string.Format("Make sure the recipe {0} is closed!", recipe.RecipeName), "Warning");
                    return;
                }

                //删除Recipe内容
                recipe.Delete();

                teMaterialBoxName.Text = "";
            }

        }

        private void btnSaveMaterialBox_Click(object sender, EventArgs e)
        {
            if ((XtraMessageBox.Show("Are you sure save this recipe！", "warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)) == DialogResult.OK)
            {
                var recipe = this._materialboxRecipe;

                if (!IsExistRecipeName(recipe.RecipeName, EnumRecipeType.MaterialBox))
                {
                    XtraMessageBox.Show(string.Format("Make sure the recipe {0} is exist!", recipe.RecipeName), "Warning");
                    return;
                }

                var recipe0 = MaterialRecipe.LoadRecipe(teMaterialBoxSelectMaterialName.Text, EnumRecipeType.Material);
                _materialboxRecipe.MaterialBoxParam.MaterialParam = recipe0.MaterialParam;


                _materialboxRecipe.MaterialBoxParam.MaterialboxSize = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialBoxLength.Value,
                    Y = (double)numMaterialBoxWidth.Value,
                    Z = (double)numMaterialBoxHeight.Value,
                };

                _materialboxRecipe.MaterialBoxParam.MaterialRowNumber = (int)numMaterialRows.Value;
                _materialboxRecipe.MaterialBoxParam.MaterialColNumber = (int)numMaterialCols.Value;

                _materialboxRecipe.MaterialBoxParam.MaterialRowinterval = (float)numMaterialRowinterval.Value;
                _materialboxRecipe.MaterialBoxParam.MaterialColinterval = (float)numMaterialColinterval.Value;

                _materialboxRecipe.MaterialBoxParam.InitMaterialMat();

                //保存Recipe内容
                recipe.SaveRecipe();
            }
        }

        private void btnMaterialBoxSelectMaterial_Click(object sender, EventArgs e)
        {
            if (_materialboxRecipe != null)
            {
                FrmMaterialRecipeEditor selectRecipeDialog = new FrmMaterialRecipeEditor(null, this.teMaterialName.Text.ToUpper().Trim());
                if (selectRecipeDialog.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    try
                    {
                        _selectedMaterialRecipeName = selectRecipeDialog.SelectedRecipeName;
                        //验证Recipe是否完整
                        if (!MaterialRecipe.Validate(_selectedMaterialRecipeName, selectRecipeDialog.RecipeType))
                        {
                            WarningBox.FormShow("错误！", "配方无效！", "提示");
                            return;
                        }
                        else
                        {
                            //var heatRecipe = TransportRecipe.LoadRecipe(_selectedHeatRecipeName, selectRecipeDialog.RecipeType);
                            teMaterialBoxSelectMaterialName.Text = selectRecipeDialog.SelectedRecipeName;
                            var recipe = MaterialRecipe.LoadRecipe(selectRecipeDialog.SelectedRecipeName, EnumRecipeType.Material);
                            _materialboxRecipe.MaterialBoxParam.MaterialParam = recipe.MaterialParam;
                        }
                    }
                    catch (Exception ex)
                    {
                        //LogRecorder.RecordLog(EnumLogContentType.Error, "JobControlPanel: Exception occured while Loading Recipe.", ex);
                    }
                }
            }

        }


        private void btnCreateMaterialMatrix_Click(object sender, EventArgs e)
        {
            if (_materialboxRecipe != null)
            {
                _materialboxRecipe.MaterialBoxParam.MaterialRowNumber = (int)numMaterialRows.Value;
                _materialboxRecipe.MaterialBoxParam.MaterialColNumber = (int)numMaterialCols.Value;

                _materialboxRecipe.MaterialBoxParam.MaterialRowinterval = (float)numMaterialRowinterval.Value;
                _materialboxRecipe.MaterialBoxParam.MaterialColinterval = (float)numMaterialColinterval.Value;

                _materialboxRecipe.MaterialBoxParam.InitMaterialMat();

                EnumReturnMaterialproperties propReturn = FrmCreateMaterialMatrixEditorBox.FormShow(_materialboxRecipe.MaterialBoxParam.MaterialMat);

                if (propReturn?.Re == 1)
                {
                    _materialboxRecipe.MaterialBoxParam.MaterialMat = propReturn.MaterialMat;

                    _materialboxRecipe.MaterialBoxParam.Iswelded = false;

                    _materialboxRecipe.MaterialBoxParam.MaterialNumber = 0;

                    if (_materialboxRecipe.MaterialBoxParam.Iswelded == false)
                    {
                        for (int i = 0; i < _materialboxRecipe.MaterialBoxParam.MaterialRowNumber; i++)
                        {
                            for (int j = 0; j < _materialboxRecipe.MaterialBoxParam.MaterialColNumber; j++)
                            {
                                if (_materialboxRecipe.MaterialBoxParam.MaterialMat[i][j].Materialstate == EnumMaterialstate.Unwelded)
                                {
                                    _materialboxRecipe.MaterialBoxParam.MaterialNumber++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    return;
                }

                //_materialboxRecipe.SaveRecipe();
            }

        }




        #endregion

        #region 烘箱参数

        private void btnOverBox1SelectMaterialBox_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                FrmMaterialBoxRecipeEditor selectRecipeDialog = new FrmMaterialBoxRecipeEditor(null, this.teOverBox1SelectMaterialBoxName.Text.ToUpper().Trim());
                if (selectRecipeDialog.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    try
                    {
                        _selectedMaterialBoxRecipeName = selectRecipeDialog.SelectedRecipeName;
                        //验证Recipe是否完整
                        if (!MaterialBoxRecipe.Validate(_selectedMaterialBoxRecipeName, selectRecipeDialog.RecipeType))
                        {
                            WarningBox.FormShow("错误！", "配方无效！", "提示");
                            return;
                        }
                        else
                        {
                            //var heatRecipe = TransportRecipe.LoadRecipe(_selectedHeatRecipeName, selectRecipeDialog.RecipeType);
                            teOverBox1SelectMaterialBoxName.Text = selectRecipeDialog.SelectedRecipeName;
                            var recipe = MaterialBoxRecipe.LoadRecipe(selectRecipeDialog.SelectedRecipeName, selectRecipeDialog.RecipeType);
                            _selRecipe.OverBox1Param.MaterialboxParam.Clear();
                            _selRecipe.OverBox1Param.MaterialboxParam.Add(recipe.MaterialBoxParam);
                            _selRecipe.OverBox1Param.MaterialboxParam.Add(recipe.MaterialBoxParam);
                        }
                    }
                    catch (Exception ex)
                    {
                        //LogRecorder.RecordLog(EnumLogContentType.Error, "JobControlPanel: Exception occured while Loading Recipe.", ex);
                    }
                }
                TransportControl.Instance.TransportRecipe = _selRecipe;
            }
        }

        private void btnSaveOverBox1_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                if ((XtraMessageBox.Show("Are you sure save this recipe！", "warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)) == DialogResult.OK)
                {
                    if (!IsExistRecipeName(teOverBox1SelectMaterialBoxName.Text, EnumRecipeType.MaterialBox))
                    {
                        XtraMessageBox.Show(string.Format("Make sure the recipe {0} is exist!", teOverBox1SelectMaterialBoxName.Text), "Warning");
                        return;
                    }

                    var recipe0 = MaterialBoxRecipe.LoadRecipe(teOverBox1SelectMaterialBoxName.Text, EnumRecipeType.MaterialBox);
                    _selRecipe.OverBox1Param.MaterialboxParam.Clear();
                    _selRecipe.OverBox1Param.MaterialboxParam.Add(recipe0.MaterialBoxParam);
                    _selRecipe.OverBox1Param.MaterialboxParam.Add(recipe0.MaterialBoxParam);

                    _selRecipe.OverBox1Param.OverBoxMaterialBoxLayerNumber = (int)numOverBox1MaterialBoxLayerNumber.Value;
                    _selRecipe.OverBox1Param.OverBoxMaterialBoxGetInNumber = (int)this.numOverBox1MaterialBoxGetInNumber.Value;

                    //保存Recipe内容
                    _selRecipe.SaveRecipe();
                }
                TransportControl.Instance.TransportRecipe = _selRecipe;
            }
        }

        private void btnOverBox2SelectMaterialBox_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                FrmMaterialBoxRecipeEditor selectRecipeDialog = new FrmMaterialBoxRecipeEditor(null, this.teOverBox1SelectMaterialBoxName.Text.ToUpper().Trim());
                if (selectRecipeDialog.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    try
                    {
                        _selectedMaterialBoxRecipeName = selectRecipeDialog.SelectedRecipeName;
                        //验证Recipe是否完整
                        if (!MaterialBoxRecipe.Validate(_selectedMaterialBoxRecipeName, selectRecipeDialog.RecipeType))
                        {
                            WarningBox.FormShow("错误！", "配方无效！", "提示");
                            return;
                        }
                        else
                        {
                            //var heatRecipe = TransportRecipe.LoadRecipe(_selectedHeatRecipeName, selectRecipeDialog.RecipeType);
                            teOverBox2SelectMaterialBoxName.Text = selectRecipeDialog.SelectedRecipeName;
                            var recipe = MaterialBoxRecipe.LoadRecipe(selectRecipeDialog.SelectedRecipeName, EnumRecipeType.MaterialBox);
                            _selRecipe.OverBox2Param.MaterialboxParam.Clear();
                            _selRecipe.OverBox2Param.MaterialboxParam.Add(recipe.MaterialBoxParam);
                            _selRecipe.OverBox2Param.MaterialboxParam.Add(recipe.MaterialBoxParam);
                        }
                    }
                    catch (Exception ex)
                    {
                        //LogRecorder.RecordLog(EnumLogContentType.Error, "JobControlPanel: Exception occured while Loading Recipe.", ex);
                    }
                }
                TransportControl.Instance.TransportRecipe = _selRecipe;
            }
        }

        private void btnSaveOverBox2_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                if ((XtraMessageBox.Show("Are you sure save this recipe！", "warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)) == DialogResult.OK)
                {
                    if (!IsExistRecipeName(teOverBox2SelectMaterialBoxName.Text, EnumRecipeType.MaterialBox))
                    {
                        XtraMessageBox.Show(string.Format("Make sure the recipe {0} is exist!", teOverBox2SelectMaterialBoxName.Text), "Warning");
                        return;
                    }

                    var recipe0 = MaterialBoxRecipe.LoadRecipe(teOverBox2SelectMaterialBoxName.Text, EnumRecipeType.MaterialBox);
                    _selRecipe.OverBox2Param.MaterialboxParam.Clear();
                    _selRecipe.OverBox2Param.MaterialboxParam.Add(recipe0.MaterialBoxParam);
                    _selRecipe.OverBox2Param.MaterialboxParam.Add(recipe0.MaterialBoxParam);

                    _selRecipe.OverBox2Param.OverBoxMaterialBoxLayerNumber = (int)numOverBox2MaterialBoxLayerNumber.Value;
                    _selRecipe.OverBox2Param.OverBoxMaterialBoxGetInNumber = (int)this.numOverBox2MaterialBoxGetInNumber.Value;

                    //保存Recipe内容
                    _selRecipe.SaveRecipe();
                }
                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }



        #endregion


        #region 焊台参数

        private void btnWeldNumSave_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                if ((XtraMessageBox.Show("Are you sure save this recipe！", "warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)) == DialogResult.OK)
                {
                    _selRecipe.WeldNum = (int)numWeldNum.Value;

                    // 清空任何现有项目  
                    comboBox1.Items.Clear();
                    this.MaterialHooktoTargetPosition.Clear();
                    _selRecipe.MaterialHooktoTargetPosition.Clear();

                    if (_selRecipe.OverBox1Param.MaterialboxParam.Count > 0)
                    {
                        if (_selRecipe.OverBox1Param.MaterialboxParam.Count > 1)
                        {

                            _selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                            _selRecipe.OverBox1Param.MaterialboxParam[1].MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                        }
                        else
                        {
                            _selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                        }
                    }
                    if (_selRecipe.OverBox2Param.MaterialboxParam.Count > 0)
                    {
                        if (_selRecipe.OverBox2Param.MaterialboxParam.Count > 1)
                        {

                            _selRecipe.OverBox2Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                            _selRecipe.OverBox2Param.MaterialboxParam[1].MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                        }
                        else
                        {
                            _selRecipe.OverBox2Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                        }
                    }
                    
                    

                    List<XYZTCoordinateConfig> MaterialHooktoTargetPosition0 = new List<XYZTCoordinateConfig>();
                    for (int i = 0; i < _selRecipe.WeldNum; i++)
                    {
                        MaterialHooktoTargetPosition0.Add(new XYZTCoordinateConfig());
                    }

                    this.MaterialHooktoTargetPosition = MaterialHooktoTargetPosition0;

                    

                    for (int i = 0; i < _selRecipe.WeldNum; i++)
                    {
                        comboBox1.Items.Add($"位置 {i}");
                        //this.MaterialHooktoTargetPosition.Add(new XYZTCoordinateConfig());
                        _selRecipe.MaterialHooktoTargetPosition.Add(new XYZTCoordinateConfig());

                        string newRecipeName = _selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.Name;

                        var templateFolderName = $@"{_systemConfig.SystemDefaultDirectory}Recipes\{EnumRecipeType.Material.ToString()}\{newRecipeName}\TemplateConfig\";

                        var templateTrainFileName1 = Path.Combine(templateFolderName, $"VisionTemplateFile_{newRecipeName}_WeldCameraIdentifyMaterialMatch_{i}.contourmxml");
                        var templateTrainParamName1 = Path.Combine(templateFolderName, $"VisionTemplateFile_{newRecipeName}_WeldCameraIdentifyMaterialMatchTrainParam_{i}.xml");
                        var templateRunFileName1 = Path.Combine(templateFolderName, $"VisionTemplateFile_{newRecipeName}_WeldCameraIdentifyMaterialMatchRun_{i}.xml");

                        MatchIdentificationParam param = new MatchIdentificationParam()
                        {
                            RingLightintensity = 0,
                            DirectLightintensity = 0,
                            Templatexml = templateTrainFileName1,
                            TemplateParamxml = templateTrainParamName1,
                            Runxml = templateRunFileName1,
                            Score = 0.4f,
                            MinAngle = -10,
                            MaxAngle = 10,
                            TemplateRoi = new RectangleFV(),
                            SearchRoi = new RectangleFV()
                        };

                        if(_selRecipe.OverBox1Param.MaterialboxParam.Count>0)
                        {
                            if(_selRecipe.OverBox1Param.MaterialboxParam.Count>1)
                            {
                                if (!_selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs.Contains(param))
                                    _selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs.Add(param);
                                if (!_selRecipe.OverBox1Param.MaterialboxParam[1].MaterialParam.WeldCameraIdentifyMaterialMatchs.Contains(param))
                                    _selRecipe.OverBox1Param.MaterialboxParam[1].MaterialParam.WeldCameraIdentifyMaterialMatchs.Add(param);
                            }
                            else
                            {
                                if (!_selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs.Contains(param))
                                    _selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs.Add(param);
                            }
                        }
                        if (_selRecipe.OverBox2Param.MaterialboxParam.Count > 0)
                        {
                            if (_selRecipe.OverBox2Param.MaterialboxParam.Count > 1)
                            {
                                if (!_selRecipe.OverBox2Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs.Contains(param))
                                    _selRecipe.OverBox2Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs.Add(param);
                                if (!_selRecipe.OverBox2Param.MaterialboxParam[1].MaterialParam.WeldCameraIdentifyMaterialMatchs.Contains(param))
                                    _selRecipe.OverBox2Param.MaterialboxParam[1].MaterialParam.WeldCameraIdentifyMaterialMatchs.Add(param);
                            }
                            else
                            {
                                if (!_selRecipe.OverBox2Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs.Contains(param))
                                    _selRecipe.OverBox2Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs.Add(param);
                            }
                        }


                    }



                    if (_selRecipe.OverBox1Param.MaterialboxParam.Count > 0)
                    {
                        if (_selRecipe.OverBox1Param.MaterialboxParam.Count > 1)
                        {
                            MaterialRecipe materialRecipe = MaterialRecipe.LoadRecipe(_selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.Name, EnumRecipeType.Material);
                            materialRecipe.MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                            materialRecipe.MaterialParam.WeldCameraIdentifyMaterialMatchs = _selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs;
                            materialRecipe.SaveRecipe();
                            MaterialRecipe materialRecipe1 = MaterialRecipe.LoadRecipe(_selRecipe.OverBox1Param.MaterialboxParam[1].MaterialParam.Name, EnumRecipeType.Material);
                            materialRecipe1.MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                            materialRecipe1.MaterialParam.WeldCameraIdentifyMaterialMatchs = _selRecipe.OverBox1Param.MaterialboxParam[1].MaterialParam.WeldCameraIdentifyMaterialMatchs;
                            materialRecipe1.SaveRecipe();
                        }
                        else
                        {
                            MaterialRecipe materialRecipe = MaterialRecipe.LoadRecipe(_selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.Name, EnumRecipeType.Material);
                            materialRecipe.MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                            materialRecipe.MaterialParam.WeldCameraIdentifyMaterialMatchs = _selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs;
                            materialRecipe.SaveRecipe();
                        }
                    }
                    if (_selRecipe.OverBox2Param.MaterialboxParam.Count > 0)
                    {
                        if (_selRecipe.OverBox2Param.MaterialboxParam.Count > 1)
                        {

                            MaterialRecipe materialRecipe2 = MaterialRecipe.LoadRecipe(_selRecipe.OverBox2Param.MaterialboxParam[0].MaterialParam.Name, EnumRecipeType.Material);
                            materialRecipe2.MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                            materialRecipe2.MaterialParam.WeldCameraIdentifyMaterialMatchs = _selRecipe.OverBox2Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs;
                            materialRecipe2.SaveRecipe();
                            MaterialRecipe materialRecipe3 = MaterialRecipe.LoadRecipe(_selRecipe.OverBox2Param.MaterialboxParam[1].MaterialParam.Name, EnumRecipeType.Material);
                            materialRecipe3.MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                            materialRecipe3.MaterialParam.WeldCameraIdentifyMaterialMatchs = _selRecipe.OverBox2Param.MaterialboxParam[1].MaterialParam.WeldCameraIdentifyMaterialMatchs;
                            materialRecipe3.SaveRecipe();
                        }
                        else
                        {
                            MaterialRecipe materialRecipe2 = MaterialRecipe.LoadRecipe(_selRecipe.OverBox2Param.MaterialboxParam[0].MaterialParam.Name, EnumRecipeType.Material);
                            materialRecipe2.MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                            materialRecipe2.MaterialParam.WeldCameraIdentifyMaterialMatchs = _selRecipe.OverBox2Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatchs;
                            materialRecipe2.SaveRecipe();
                        }
                    }


                    comboBox1.SelectedIndex = 0;

                    //保存Recipe内容
                    _selRecipe.SaveRecipe();


                }
                TransportControl.Instance.TransportRecipe = _selRecipe;
            }
        }




        #endregion

        #endregion

        #region 料盒搬送控件方法


        #region 烘箱1出料
        private void btnAvoidancePosition_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                TransportControl.Instance.TransportRecipe = _selRecipe;
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHooktoAvoidPositionAction();
                });
                
            }
        }

        private void btnMaterialboxHookSafePositionRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numMaterialboxHookSafePositionX.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                numMaterialboxHookSafePositionY.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                numMaterialboxHookSafePositionZ.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                numMaterialboxHookSafePositionT.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);
                numMaterialboxHookOpen.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxHook);

                _selRecipe.MaterialboxHookSafePosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookSafePositionX.Value,
                    Y = (double)numMaterialboxHookSafePositionY.Value,
                    Z = (double)numMaterialboxHookSafePositionZ.Value,
                    Theta = (double)numMaterialboxHookSafePositionT.Value,
                };
                _selRecipe.MaterialboxHookOpen = (float)numMaterialboxHookOpen.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnMaterialboxHookSafePositionMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHookSafePosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookSafePositionX.Value,
                    Y = (double)numMaterialboxHookSafePositionY.Value,
                    Z = (double)numMaterialboxHookSafePositionZ.Value,
                    Theta = (double)numMaterialboxHookSafePositionT.Value,
                };
                _selRecipe.MaterialboxHookOpen = (float)numMaterialboxHookOpen.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHooktoSafePositionAction();
                });

                

            }
        }

        private void checkbtnOvenBox1Aerates_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbtnOvenBox1Aerates.Checked)
            {
                TransportControl.Instance.OpenOvenBoxAerates(EnumOvenBoxNum.Oven1);
            }
            else
            {
                TransportControl.Instance.CloseOvenBoxAerates(EnumOvenBoxNum.Oven1);
            }
        }
        private void btnOpenExchangeMakeupValve_CheckedChanged(object sender, EventArgs e)
        {
            if (btnOpenExchangeMakeupValve.Checked)
            {
                TransportControl.Instance.OpenOvenBoxAerates(EnumOvenBoxNum.Box);
            }
            else
            {
                TransportControl.Instance.CloseOvenBoxAerates(EnumOvenBoxNum.Box);
            }

        }

        private void btnBakeOven1InnerdoorOpen_Click(object sender, EventArgs e)
        {
            //Oven1Vacuum = TransportControl.Instance.ReadOvenVacuum(EnumOvenBoxNum.Oven1);
            //BoxVacuum = TransportControl.Instance.ReadBoxVacuum();
            //if (TransportControl.Instance.TransportRecipe != null && (Math.Abs(Oven1Vacuum - BoxVacuum) < VacuumD) && (Math.Abs(Oven1Vacuum - VacuumC) < VacuumD))
            //{
            //    TransportControl.Instance.OpenOvenBoxInteriorDoor(EnumOvenBoxNum.Oven1);

            //}
            Task.Run(() =>
            {
                TransportControl.Instance.OpenOvenBoxInteriorDoor(EnumOvenBoxNum.Oven1);
            });
            
        }

        private void btnBakeOven1InnerdoorClose_Click(object sender, EventArgs e)
        {
            
            Task.Run(() =>
            {
                TransportControl.Instance.CloseOvenBoxInteriorDoor(EnumOvenBoxNum.Oven1);
            });
        }

        private void btnOverTrack1MaterialboxOutofovenRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numOverTrackMaterialboxOutofoven.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.OverTrack1);

                _selRecipe.OverTrackMaterialboxOutofoven = (float)numOverTrackMaterialboxOutofoven.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnOverTrack1MaterialboxOutofovenMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                _selRecipe.OverTrackMaterialboxOutofoven = (float)numOverTrackMaterialboxOutofoven.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxOutofovenAction(EnumOvenBoxNum.Oven1);
                });

                

            }
        }

        private void btnMaterialboxHooktoMaterialboxPosition1Read_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numMaterialboxHooktoMaterialboxPosition1X.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                numMaterialboxHooktoMaterialboxPosition1Y.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                numMaterialboxHooktoMaterialboxPosition1Z.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                numMaterialboxHooktoMaterialboxPosition1T.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);
                numMaterialboxHookClose.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxHook);

                _selRecipe.MaterialboxHooktoMaterialboxPosition1 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition1X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition1Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition1Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition1T.Value,
                };
                _selRecipe.MaterialboxHookClose = (float)numMaterialboxHookClose.Value;
                _selRecipe.MaterialboxHookUp = (float)numMaterialboxHookUp.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnMaterialboxHooktoMaterialboxPosition1Move_Click(object sender, EventArgs e)
        {
            
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                _selRecipe.MaterialboxHooktoMaterialboxPosition1 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition1X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition1Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition1Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition1T.Value,
                };
                _selRecipe.MaterialboxHookClose = (float)numMaterialboxHookClose.Value;
                _selRecipe.MaterialboxHookUp = (float)numMaterialboxHookUp.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHooktoMaterialboxAction(EnumOvenBoxNum.Oven1);
                });

                

            }
        }

        private void btnMaterialboxHooktoMaterialboxPosition1Pickup_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                _selRecipe.MaterialboxHooktoMaterialboxPosition1 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition1X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition1Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition1Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition1T.Value,
                };
                _selRecipe.MaterialboxHookClose = (float)numMaterialboxHookClose.Value;
                _selRecipe.MaterialboxHookUp = (float)numMaterialboxHookUp.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPickupMaterialboxAction(_selRecipe.MaterialboxHooktoMaterialboxPosition1.Z, _selRecipe.MaterialboxHookUp);
                });

                

            }
        }

        private void btnMaterialboxHooktoMaterialboxPosition1Putdown_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                _selRecipe.MaterialboxHooktoMaterialboxPosition1 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition1X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition1Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition1Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition1T.Value,
                };
                _selRecipe.MaterialboxHookClose = (float)numMaterialboxHookClose.Value;
                _selRecipe.MaterialboxHookUp = (float)numMaterialboxHookUp.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPutdownMaterialboxAction(_selRecipe.MaterialboxHooktoMaterialboxPosition1.Z, _selRecipe.MaterialboxHookUp);
                });
                

            }
        }

        private void btnMaterialboxHooktoTarget1PositionRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numMaterialboxHooktoTarget1PositionX.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                numMaterialboxHooktoTarget1PositionY.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                numMaterialboxHooktoTarget1PositionZ.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                numMaterialboxHooktoTarget1PositionT.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                _selRecipe.MaterialboxHooktoTarget1Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget1PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget1PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget1PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget1PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnMaterialboxHooktoTarget1PositionMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                _selRecipe.MaterialboxHooktoTarget1Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget1PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget1PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget1PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget1PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHooktoTargetPositionAction(_selRecipe.MaterialboxHooktoTarget1Position, _selRecipe.MaterialboxHookUp);
                });

                

            }
        }

        private void btnMaterialboxHooktoTarget1PositionPickup_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHooktoTarget1Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget1PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget1PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget1PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget1PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPickupMaterialboxAction(_selRecipe.MaterialboxHooktoTarget1Position.Z, _selRecipe.MaterialboxHookUp);
                });
               

            }
        }

        private void btnMaterialboxHooktoTarget1PositionPutdown_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHooktoTarget1Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget1PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget1PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget1PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget1PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPutdownMaterialboxAction(_selRecipe.MaterialboxHooktoTarget1Position.Z, _selRecipe.MaterialboxHookUp);
                });

               

            }
        }

        private void OvenBox1OutputSave_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {

                #region 烘箱1出料

                _selRecipe.MaterialboxHookSafePosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookSafePositionX.Value,
                    Y = (double)numMaterialboxHookSafePositionY.Value,
                    Z = (double)numMaterialboxHookSafePositionZ.Value,
                    Theta = (double)numMaterialboxHookSafePositionT.Value,
                };
                _selRecipe.MaterialboxHookOpen = (float)numMaterialboxHookOpen.Value;

                _selRecipe.OverTrackMaterialboxOutofoven = (float)numOverTrackMaterialboxOutofoven.Value;

                _selRecipe.MaterialboxHooktoMaterialboxPosition1 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition1X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition1Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition1Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition1T.Value,
                };
                _selRecipe.MaterialboxHookClose = (float)numMaterialboxHookClose.Value;
                _selRecipe.MaterialboxHookUp = (float)numMaterialboxHookUp.Value;

                _selRecipe.MaterialboxHooktoTarget1Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget1PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget1PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget1PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget1PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                _selRecipe.SaveRecipe();

                #endregion

            }
        }




        #endregion

        #region 烘箱2出料


        private void checkButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkButton3.Checked)
            {
                TransportControl.Instance.OpenOvenBoxAerates(EnumOvenBoxNum.Oven2);
            }
            else
            {
                TransportControl.Instance.CloseOvenBoxAerates(EnumOvenBoxNum.Oven2);
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            //Oven2Vacuum = TransportControl.Instance.ReadOvenVacuum(EnumOvenBoxNum.Oven2);
            //BoxVacuum = TransportControl.Instance.ReadBoxVacuum();
            //if (TransportControl.Instance.TransportRecipe != null && (Math.Abs(Oven2Vacuum - BoxVacuum) < VacuumD) && (Math.Abs(Oven2Vacuum - VacuumC) < VacuumD))
            //{
            //    TransportControl.Instance.OpenOvenBoxInteriorDoor(EnumOvenBoxNum.Oven2);

            //}

            Task.Run(() =>
            {
                TransportControl.Instance.OpenOvenBoxInteriorDoor(EnumOvenBoxNum.Oven2);
            });
            
        }

        private void button44_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                TransportControl.Instance.CloseOvenBoxInteriorDoor(EnumOvenBoxNum.Oven2);
            });
            
        }

        private void btnOverTrack2MaterialboxOutofovenRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numOverTrack2MaterialboxOutofoven.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.OverTrack2);

                _selRecipe.OverTrack2MaterialboxOutofoven = (float)numOverTrack2MaterialboxOutofoven.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnOverTrack2MaterialboxOutofovenMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven2InteriorDoorOpen))
            {
                _selRecipe.OverTrack2MaterialboxOutofoven = (float)numOverTrack2MaterialboxOutofoven.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxOutofovenAction(EnumOvenBoxNum.Oven2);
                });

                

            }
        }

        private void btnMaterialboxHooktoMaterialboxPosition2Read_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numMaterialboxHooktoMaterialboxPosition2X.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                numMaterialboxHooktoMaterialboxPosition2Y.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                numMaterialboxHooktoMaterialboxPosition2Z.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                numMaterialboxHooktoMaterialboxPosition2T.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);
                numMaterialboxHookClose2.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxHook);

                _selRecipe.MaterialboxHooktoMaterialboxPosition2 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition2T.Value,
                };
                _selRecipe.MaterialboxHookClose2 = (float)numMaterialboxHookClose2.Value;
                _selRecipe.MaterialboxHookUp2 = (float)numMaterialboxHookUp2.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnMaterialboxHooktoMaterialboxPosition2Move_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                _selRecipe.MaterialboxHooktoMaterialboxPosition2 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition2T.Value,
                };
                _selRecipe.MaterialboxHookClose2 = (float)numMaterialboxHookClose2.Value;
                _selRecipe.MaterialboxHookUp2 = (float)numMaterialboxHookUp2.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHooktoMaterialboxAction(EnumOvenBoxNum.Oven2);
                });

               

            }
        }

        private void btnMaterialboxHooktoMaterialboxPosition2Pickup_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHooktoMaterialboxPosition2 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition2T.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPickupMaterialboxAction(_selRecipe.MaterialboxHooktoMaterialboxPosition2.Z, _selRecipe.MaterialboxHookUp2);
                });

                

            }

        }

        private void btnMaterialboxHooktoMaterialboxPosition2Putdown_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHooktoMaterialboxPosition2 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition2T.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPutdownMaterialboxAction(_selRecipe.MaterialboxHooktoMaterialboxPosition2.Z, _selRecipe.MaterialboxHookUp2);
                });

            }
        }


        private void btnMaterialboxHooktoTarget1Position2Read_Click(object sender, EventArgs e)
        {

        }

        private void btnMaterialboxHooktoTarget1Position2Move_Click(object sender, EventArgs e)
        {

        }

        private void btnMaterialboxHooktoTarget1Position2Pickup_Click(object sender, EventArgs e)
        {

        }

        private void btnMaterialboxHooktoTarget1Position2Putdown_Click(object sender, EventArgs e)
        {

        }

        private void OvenBox2OutputSave_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {


                #region 烘箱2出料

                _selRecipe.OverTrack2MaterialboxOutofoven = (float)numOverTrack2MaterialboxOutofoven.Value;

                _selRecipe.MaterialboxHooktoMaterialboxPosition2 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHooktoMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHooktoMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHooktoMaterialboxPosition2T.Value,
                };
                _selRecipe.MaterialboxHookClose2 = (float)numMaterialboxHookClose2.Value;
                _selRecipe.MaterialboxHookUp2 = (float)numMaterialboxHookUp2.Value;


                TransportControl.Instance.TransportRecipe = _selRecipe;

                _selRecipe.SaveRecipe();

                #endregion

            }
        }


        #endregion

        #region 料盒搬送

        private void btnMaterialboxHooktoTarget2PositionRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numMaterialboxHooktoTarget2PositionX.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                numMaterialboxHooktoTarget2PositionY.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                numMaterialboxHooktoTarget2PositionZ.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                numMaterialboxHooktoTarget2PositionT.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                _selRecipe.MaterialboxHooktoTarget2Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget2PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget2PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget2PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget2PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnMaterialboxHooktoTarget2PositionMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                _selRecipe.MaterialboxHooktoTarget2Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget2PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget2PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget2PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget2PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHooktoTargetPositionAction(_selRecipe.MaterialboxHooktoTarget2Position, _selRecipe.MaterialboxHookUp);
                });

                

            }
        }

        private void btnMaterialboxHooktoTarget2PositionPickup_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHooktoTarget2Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget2PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget2PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget2PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget2PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPickupMaterialboxAction(_selRecipe.MaterialboxHooktoTarget2Position.Z, _selRecipe.MaterialboxHookUp);
                });

            }
        }

        private void btnMaterialboxHooktoTarget2PositionPutdown_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHooktoTarget2Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget2PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget2PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget2PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget2PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPutdownMaterialboxAction(_selRecipe.MaterialboxHooktoTarget2Position.Z, _selRecipe.MaterialboxHookUp);
                });

            }
        }

        private void btnMaterialboxHooktoTarget3PositionRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numMaterialboxHooktoTarget3PositionX.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                numMaterialboxHooktoTarget3PositionY.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                numMaterialboxHooktoTarget3PositionZ.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                numMaterialboxHooktoTarget3PositionT.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                _selRecipe.MaterialboxHooktoTarget3Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget3PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget3PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget3PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget3PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnMaterialboxHooktoTarget3PositionMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                _selRecipe.MaterialboxHooktoTarget3Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget3PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget3PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget3PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget3PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHooktoTargetPositionAction(_selRecipe.MaterialboxHooktoTarget3Position, _selRecipe.MaterialboxHookUp);
                });
            }
        }

        private void btnMaterialboxHooktoTarget3PositionPickup_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHooktoTarget3Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget3PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget3PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget3PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget3PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPickupMaterialboxAction(_selRecipe.MaterialboxHooktoTarget3Position.Z, _selRecipe.MaterialboxHookUp);
                });

            }
        }

        private void btnMaterialboxHooktoTarget3PositionPutdown_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHooktoTarget3Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget3PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget3PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget3PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget3PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPutdownMaterialboxAction(_selRecipe.MaterialboxHooktoTarget3Position.Z, _selRecipe.MaterialboxHookUp);
                });

            }
        }

        private void btnGreateMaterialBoxRecognition_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                if(comboBox2.SelectedIndex == 0)
                {
                    this.TrackCameraIdentifyMaterialBoxMatch = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].TrackCameraIdentifyMaterialBoxMatch;
                }
                else if(comboBox2.SelectedIndex == 1)
                {
                    this.TrackCameraIdentifyMaterialBoxMatch = _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].TrackCameraIdentifyMaterialBoxMatch;
                }

                

                string name = "搬送相机创建料盒识别";
                string title = "";
                VisualMatchControlGUI visualMatch = new VisualMatchControlGUI();
                visualMatch.InitVisualControl(CameraWindowGUI.Instance, SystemCalibration.Instance.TrackCameraVisual);

                visualMatch.SetVisualParam(this.TrackCameraIdentifyMaterialBoxMatch);

                int Done = SystemCalibration.Instance.ShowVisualForm(visualMatch, name, title);

                if (Done == 0)
                {
                    return;
                }
                else
                {
                    this.TrackCameraIdentifyMaterialBoxMatch = visualMatch.GetVisualParam();



                    MatchResult match = visualMatch.Matchresult;

                    match = SystemCalibration.Instance.IdentificationAsync(EnumCameraType.TrackCamera, this.TrackCameraIdentifyMaterialBoxMatch);

                    double MaterialboxX = 0, MaterialboxY = 0;

                    if (match != null)
                    {
                        (MaterialboxX, MaterialboxY) = SystemCalibration.Instance.ImageOffsetToXY(match.MatchBox.Benchmark);
                    }
                    else
                    {
                        SystemCalibration.Instance.ShowMessage("识别错误", "没有识别结果", "警告");
                    }

                    double x = _systemConfig.PositioningConfig.TrackCameraCenterMaterialHook.X + MaterialboxX;
                    double y = _systemConfig.PositioningConfig.TrackCameraCenterMaterialHook.Y + MaterialboxY;

                    if (comboBox2.SelectedIndex == 0)
                    {
                        _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter = new XYZTCoordinateConfig()
                        {
                            X = x,
                            Y = y,
                        };

                        _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].TrackCameraIdentifyMaterialBoxMatch = this.TrackCameraIdentifyMaterialBoxMatch;

                       
                    }
                    else if (comboBox2.SelectedIndex == 1)
                    {
                        _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter = new XYZTCoordinateConfig()
                        {
                            X = x,
                            Y = y,
                        };

                        _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].TrackCameraIdentifyMaterialBoxMatch = this.TrackCameraIdentifyMaterialBoxMatch;

                        
                    }

                    

                    TransportControl.Instance.TransportRecipe = _selRecipe;
                }

            }

        }

        private void btnMaterialboxHooktoTarget4PositionRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numMaterialboxHooktoTarget4PositionX.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                numMaterialboxHooktoTarget4PositionY.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                numMaterialboxHooktoTarget4PositionZ.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                numMaterialboxHooktoTarget4PositionT.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                _selRecipe.MaterialboxHooktoTarget4Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget4PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget4PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget4PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget4PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnMaterialboxHooktoTarget4PositionMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                _selRecipe.MaterialboxHooktoTarget4Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget4PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget4PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget4PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget4PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHooktoTargetPositionAction(_selRecipe.MaterialboxHooktoTarget4Position, _selRecipe.MaterialboxHookUp);
                });
               

            }
        }

        private void btnMaterialboxHooktoTarget4PositionPickup_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHooktoTarget4Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget4PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget4PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget4PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget4PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPickupMaterialboxAction(_selRecipe.MaterialboxHooktoTarget4Position.Z, _selRecipe.MaterialboxHookUp);
                });

            }
        }

        private void btnMaterialboxHooktoTarget4PositionPutdown_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHooktoTarget4Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget4PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget4PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget4PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget4PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPutdownMaterialboxAction(_selRecipe.MaterialboxHooktoTarget4Position.Z, _selRecipe.MaterialboxHookUp);
                });

            }
        }


        private void OvenBox1MaterialBoxToTragetSave_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                #region 料盒搬送

                _selRecipe.MaterialboxHooktoTarget2Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget2PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget2PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget2PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget2PositionT.Value,
                };

                _selRecipe.MaterialboxHooktoTarget3Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget3PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget3PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget3PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget3PositionT.Value,
                };
                _selRecipe.OverBox1Param.MaterialboxParam[0].TrackCameraIdentifyMaterialBoxMatch = this.TrackCameraIdentifyMaterialBoxMatch;

                _selRecipe.MaterialboxHooktoTarget4Position = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHooktoTarget4PositionX.Value,
                    Y = (double)numMaterialboxHooktoTarget4PositionY.Value,
                    Z = (double)numMaterialboxHooktoTarget4PositionZ.Value,
                    Theta = (double)numMaterialboxHooktoTarget4PositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                _selRecipe.SaveRecipe();

                #endregion

            }

        }




        #endregion

        #region 烘箱1进料


        private void btnMaterialboxHookPickupMaterialboxPositionRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numMaterialboxHookPickupMaterialboxPositionX.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                numMaterialboxHookPickupMaterialboxPositionY.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                numMaterialboxHookPickupMaterialboxPositionZ.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                numMaterialboxHookPickupMaterialboxPositionT.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                _selRecipe.MaterialboxHookPickupMaterialboxPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookPickupMaterialboxPositionX.Value,
                    Y = (double)numMaterialboxHookPickupMaterialboxPositionY.Value,
                    Z = (double)numMaterialboxHookPickupMaterialboxPositionZ.Value,
                    Theta = (double)numMaterialboxHookPickupMaterialboxPositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnMaterialboxHookPickupMaterialboxPositionMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                _selRecipe.MaterialboxHookPickupMaterialboxPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookPickupMaterialboxPositionX.Value,
                    Y = (double)numMaterialboxHookPickupMaterialboxPositionY.Value,
                    Z = (double)numMaterialboxHookPickupMaterialboxPositionZ.Value,
                    Theta = (double)numMaterialboxHookPickupMaterialboxPositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHooktoTargetPositionAction(_selRecipe.MaterialboxHookPickupMaterialboxPosition, _selRecipe.MaterialboxHookUp);
                });

            }
        }

        private void btnMaterialboxHookPickupMaterialboxPositionPickup_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHookPickupMaterialboxPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookPickupMaterialboxPositionX.Value,
                    Y = (double)numMaterialboxHookPickupMaterialboxPositionY.Value,
                    Z = (double)numMaterialboxHookPickupMaterialboxPositionZ.Value,
                    Theta = (double)numMaterialboxHookPickupMaterialboxPositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPickupMaterialboxAction(_selRecipe.MaterialboxHookPickupMaterialboxPosition.Z, _selRecipe.MaterialboxHookUp);
                });

            }
        }

        private void btnMaterialboxHookPickupMaterialboxPositionPutdown_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHookPickupMaterialboxPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookPickupMaterialboxPositionX.Value,
                    Y = (double)numMaterialboxHookPickupMaterialboxPositionY.Value,
                    Z = (double)numMaterialboxHookPickupMaterialboxPositionZ.Value,
                    Theta = (double)numMaterialboxHookPickupMaterialboxPositionT.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPutdownMaterialboxAction(_selRecipe.MaterialboxHookPickupMaterialboxPosition.Z, _selRecipe.MaterialboxHookUp);
                });
            }
        }

        private void checkButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkButton2.Checked)
            {
                TransportControl.Instance.OpenOvenBoxAerates(EnumOvenBoxNum.Oven1);
            }
            else
            {
                TransportControl.Instance.CloseOvenBoxAerates(EnumOvenBoxNum.Oven1);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Oven1Vacuum = TransportControl.Instance.ReadOvenVacuum(EnumOvenBoxNum.Oven1);
            BoxVacuum = TransportControl.Instance.ReadBoxVacuum();
            if (TransportControl.Instance.TransportRecipe != null && (Math.Abs(Oven1Vacuum - BoxVacuum) < VacuumD) && (Math.Abs(Oven1Vacuum - VacuumC) < VacuumD))
            {
                Task.Run(() =>
                {
                    TransportControl.Instance.OpenOvenBoxInteriorDoor(EnumOvenBoxNum.Oven1);
                });
                

            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                TransportControl.Instance.CloseOvenBoxInteriorDoor(EnumOvenBoxNum.Oven1);
            });
            
        }

        private void btnOverTrack1MaterialboxInofovenRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numOverTrack1MaterialboxInofoven.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.OverTrack1);

                _selRecipe.OverTrack1MaterialboxInofoven = (float)numOverTrack1MaterialboxInofoven.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnOverTrack1MaterialboxInofovenMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                _selRecipe.OverTrack1MaterialboxInofoven = (float)numOverTrack1MaterialboxInofoven.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxInofovenAction(EnumOvenBoxNum.Oven1);
                });

            }
        }

        private void OvenBox1InputSave_Click(object sender, EventArgs e)
        {
            #region 烘箱1进料

            _selRecipe.MaterialboxHookPickupMaterialboxPosition = new XYZTCoordinateConfig()
            {
                X = (double)numMaterialboxHookPickupMaterialboxPositionX.Value,
                Y = (double)numMaterialboxHookPickupMaterialboxPositionY.Value,
                Z = (double)numMaterialboxHookPickupMaterialboxPositionZ.Value,
                Theta = (double)numMaterialboxHookPickupMaterialboxPositionT.Value,
            };

            _selRecipe.OverTrack1MaterialboxInofoven = (float)numOverTrack1MaterialboxInofoven.Value;

            TransportControl.Instance.TransportRecipe = _selRecipe;

            _selRecipe.SaveRecipe();

            #endregion

        }

        #endregion



        #region 烘箱2进料



        private void btnMaterialboxHookPickupMaterialboxPosition2Read_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numMaterialboxHookPickupMaterialboxPosition2X.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                numMaterialboxHookPickupMaterialboxPosition2Y.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                numMaterialboxHookPickupMaterialboxPosition2Z.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                numMaterialboxHookPickupMaterialboxPosition2T.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);

                _selRecipe.MaterialboxHookPickupMaterialboxPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookPickupMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHookPickupMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHookPickupMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHookPickupMaterialboxPosition2T.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnMaterialboxHookPickupMaterialboxPosition2Move_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven2InteriorDoorOpen))
            {
                _selRecipe.MaterialboxHookPickupMaterialboxPosition2 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookPickupMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHookPickupMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHookPickupMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHookPickupMaterialboxPosition2T.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHooktoTargetPositionAction(_selRecipe.MaterialboxHookPickupMaterialboxPosition2, _selRecipe.MaterialboxHookUp2);
                });

            }
        }

        private void btnMaterialboxHookPickupMaterialboxPosition2Pickup_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHookPickupMaterialboxPosition2 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookPickupMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHookPickupMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHookPickupMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHookPickupMaterialboxPosition2T.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPickupMaterialboxAction(_selRecipe.MaterialboxHookPickupMaterialboxPosition2.Z, _selRecipe.MaterialboxHookUp);
                });

            }
        }

        private void btnMaterialboxHookPickupMaterialboxPosition2Putdown_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialboxHookPickupMaterialboxPosition2 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookPickupMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHookPickupMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHookPickupMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHookPickupMaterialboxPosition2T.Value,
                };

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxHookPutdownMaterialboxAction(_selRecipe.MaterialboxHookPickupMaterialboxPosition2.Z, _selRecipe.MaterialboxHookUp);
                });

            }
        }

        private void checkButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkButton4.Checked)
            {
                TransportControl.Instance.OpenOvenBoxAerates(EnumOvenBoxNum.Oven2);
            }
            else
            {
                TransportControl.Instance.CloseOvenBoxAerates(EnumOvenBoxNum.Oven2);
            }
        }

        private void button66_Click(object sender, EventArgs e)
        {
            Oven2Vacuum = TransportControl.Instance.ReadOvenVacuum(EnumOvenBoxNum.Oven2);
            BoxVacuum = TransportControl.Instance.ReadBoxVacuum();
            if (TransportControl.Instance.TransportRecipe != null && (Math.Abs(Oven2Vacuum - BoxVacuum) < VacuumD) && (Math.Abs(Oven2Vacuum - VacuumC) < VacuumD))
            {
                Task.Run(() =>
                {
                    TransportControl.Instance.OpenOvenBoxInteriorDoor(EnumOvenBoxNum.Oven2);
                });
               

            }
        }

        private void button65_Click(object sender, EventArgs e)
        {
            
            Task.Run(() =>
            {
                TransportControl.Instance.CloseOvenBoxInteriorDoor(EnumOvenBoxNum.Oven2);
            });
        }

        private void btnOverTrack2MaterialboxInofovenRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numOverTrack2MaterialboxInofoven.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.OverTrack2);

                _selRecipe.OverTrack2MaterialboxInofoven = (float)numOverTrack1MaterialboxInofoven.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnOverTrack2MaterialboxInofovenMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven2InteriorDoorOpen))
            {
                _selRecipe.OverTrack2MaterialboxInofoven = (float)numOverTrack2MaterialboxInofoven.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialboxInofovenAction(EnumOvenBoxNum.Oven2);
                });

            }
        }

        private void OvenBox2InputSave_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                #region 烘箱2进料

                _selRecipe.MaterialboxHookPickupMaterialboxPosition2 = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialboxHookPickupMaterialboxPosition2X.Value,
                    Y = (double)numMaterialboxHookPickupMaterialboxPosition2Y.Value,
                    Z = (double)numMaterialboxHookPickupMaterialboxPosition2Z.Value,
                    Theta = (double)numMaterialboxHookPickupMaterialboxPosition2T.Value,
                };

                _selRecipe.OverTrack2MaterialboxInofoven = (float)numOverTrack2MaterialboxInofoven.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                _selRecipe.SaveRecipe();

                #endregion


            }

        }


        #endregion



        #endregion

        #region 物料搬送

        #region 夹取物料

        private void btnAvoidancePosition1_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                TransportControl.Instance.TransportRecipe = _selRecipe;
                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialHooktoAvoidPositionAction();
                });
            }
        }
        private void btnMaterialHookSafePositionRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numMaterialHookSafePositionX.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialX);
                numMaterialHookSafePositionY.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialY);
                numMaterialHookSafePositionZ.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialZ);
                numMaterialHookOpen.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialHook);

                _selRecipe.MaterialHookSafePosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialHookSafePositionX.Value,
                    Y = (double)numMaterialHookSafePositionY.Value,
                    Z = (double)numMaterialHookSafePositionZ.Value,
                };
                _selRecipe.MaterialHookOpen = (float)numMaterialHookOpen.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }

        }

        private void btnMaterialHookSafePositionMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialHookSafePosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialHookSafePositionX.Value,
                    Y = (double)numMaterialHookSafePositionY.Value,
                    Z = (double)numMaterialHookSafePositionZ.Value,
                };
                _selRecipe.MaterialHookOpen = (float)numMaterialHookOpen.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialHooktoSafePositionAction();
                });

            }
        }

        private void btnMaterialHookPickupMaterialPositionRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numMaterialHookPickupMaterialPositionX.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialX);
                numMaterialHookPickupMaterialPositionY.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialY);
                numMaterialHookPickupMaterialPositionZ.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialZ);
                numMaterialHookClose.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialHook);

                _selRecipe.MaterialHookPickupMaterialPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialHookPickupMaterialPositionX.Value,
                    Y = (double)numMaterialHookPickupMaterialPositionY.Value,
                    Z = (double)numMaterialHookPickupMaterialPositionZ.Value,
                };
                _selRecipe.MaterialHookClose = (float)numMaterialHookClose.Value;
                _selRecipe.MaterialHookUp = (float)numMaterialHookUp.Value;
                _selRecipe.MaterialCompensationAngle = (float)numericMaterialCompensationAngle.Value;
                _selRecipe.MaterialCompensationZ = (float)numMaterialCompensationZ.Value;

                //if (comboBox2.SelectedIndex == 0)
                //{
                //    _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].Iswelded = false;

                //    if (_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].Iswelded == false)
                //    {
                //        _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].SetMaterialMat(_selRecipe.MaterialHookPickupMaterialPosition, _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter, _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter);

                //        var _materialboxRecipe = MaterialBoxRecipe.LoadRecipe(_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].Name, EnumRecipeType.MaterialBox);

                //        if (_materialboxRecipe.RecipeName == teMaterialBoxName_2.Text)
                //        {
                //            _materialboxRecipe.MaterialBoxParam.SetMaterialMat(_selRecipe.MaterialHookPickupMaterialPosition, _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter, _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter);
                //            _materialboxRecipe.SaveRecipe();
                //        }
                //    }
                //}
                //else if (comboBox2.SelectedIndex == 1)
                //{
                //    _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].Iswelded = false;

                //    if (_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].Iswelded == false)
                //    {
                //        _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].SetMaterialMat(_selRecipe.MaterialHookPickupMaterialPosition, _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter, _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter);

                //        var _materialboxRecipe = MaterialBoxRecipe.LoadRecipe(_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].Name, EnumRecipeType.MaterialBox);

                //        if (_materialboxRecipe.RecipeName == teMaterialBoxName_2.Text)
                //        {
                //            _materialboxRecipe.MaterialBoxParam.SetMaterialMat(_selRecipe.MaterialHookPickupMaterialPosition, _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter, _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter);
                //            _materialboxRecipe.SaveRecipe();
                //        }
                //    }
                //}


                TransportControl.Instance.TransportRecipe = _selRecipe;

                _selRecipe.SaveRecipe();

            }

        }

        private void btnMaterialHookPickupMaterialPositionMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialHookPickupMaterialPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialHookPickupMaterialPositionX.Value,
                    Y = (double)numMaterialHookPickupMaterialPositionY.Value,
                    Z = (double)numMaterialHookPickupMaterialPositionZ.Value,
                };
                _selRecipe.MaterialHookClose = (float)numMaterialHookClose.Value;
                _selRecipe.MaterialHookUp = (float)numMaterialHookUp.Value;
                _selRecipe.MaterialCompensationAngle = (float)numericMaterialCompensationAngle.Value;
                _selRecipe.MaterialCompensationZ = (float)numMaterialCompensationZ.Value;

                

                TransportControl.Instance.TransportRecipe = _selRecipe;


                if (numMaterialRawNum.Value >= 0 && numMaterialRawNum.Value <= 14 && numMaterialColNum.Value >= 0 && numMaterialColNum.Value <= 14)
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        if (numMaterialRawNum.Value < _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialRowNumber && numMaterialColNum.Value < _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialColNumber)
                        {
                            //numMaterialHookPickupMaterialPositionX1.Value = (decimal)_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.X;
                            //numMaterialHookPickupMaterialPositionY1.Value = (decimal)_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.Y;
                            //numMaterialHookPickupMaterialPositionZ1.Value = (decimal)_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.Z;
                            //XYZTCoordinateConfig xyzt = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition;

                            XYZTCoordinateConfig xyzt = new XYZTCoordinateConfig()
                            {
                                X = (double)numMaterialHookPickupMaterialPositionX1.Value,
                                Y = (double)numMaterialHookPickupMaterialPositionY1.Value,
                                Z = (double)numMaterialHookPickupMaterialPositionZ1.Value,
                            };

                            if(xyzt.X == 0 || xyzt.Y == 0 || xyzt.Z == 0)
                            {
                                return;
                            }

                            Task.Run(() =>
                            {
                                TransportControl.Instance.MaterialHooktoMaterialAction(xyzt, _selRecipe.MaterialHookUp);
                            });
                        }

                    }
                    else if (comboBox2.SelectedIndex == 1)
                    {
                        if (numMaterialRawNum.Value < _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialRowNumber && numMaterialColNum.Value < _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialColNumber)
                        {
                            XYZTCoordinateConfig xyzt = new XYZTCoordinateConfig()
                            {
                                X = (double)numMaterialHookPickupMaterialPositionX1.Value,
                                Y = (double)numMaterialHookPickupMaterialPositionY1.Value,
                                Z = (double)numMaterialHookPickupMaterialPositionZ1.Value,
                            };

                            if (xyzt.X == 0 || xyzt.Y == 0 || xyzt.Z == 0)
                            {
                                return;
                            }

                            Task.Run(() =>
                            {
                                TransportControl.Instance.MaterialHooktoMaterialAction(xyzt, _selRecipe.MaterialHookUp);
                            });
                        }

                    }
                }

            }

        }

        private void numMaterialRawNum_ValueChanged(object sender, EventArgs e)
        {
            if (numMaterialRawNum.Value >= 0 && numMaterialRawNum.Value <= 14 && numMaterialColNum.Value >= 0 && numMaterialColNum.Value <= 14)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    if (numMaterialRawNum.Value < _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialRowNumber && numMaterialColNum.Value < _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialColNumber)
                    {
                        numMaterialHookPickupMaterialPositionX1.Value = (decimal)_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.X;
                        numMaterialHookPickupMaterialPositionY1.Value = (decimal)_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.Y;
                        numMaterialHookPickupMaterialPositionZ1.Value = (decimal)_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.Z;
                    }

                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    if (numMaterialRawNum.Value < _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialRowNumber && numMaterialColNum.Value < _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialColNumber)
                    {
                        numMaterialHookPickupMaterialPositionX1.Value = (decimal)_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.X;
                        numMaterialHookPickupMaterialPositionY1.Value = (decimal)_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.Y;
                        numMaterialHookPickupMaterialPositionZ1.Value = (decimal)_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.Z;
                    }

                }
            }

        }

        private void numMaterialColNum_ValueChanged(object sender, EventArgs e)
        {
            if (numMaterialRawNum.Value >= 0 && numMaterialRawNum.Value <= 14 && numMaterialColNum.Value >= 0 && numMaterialColNum.Value <= 14)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    if (numMaterialRawNum.Value < _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialRowNumber && numMaterialColNum.Value < _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialColNumber)
                    {
                        numMaterialHookPickupMaterialPositionX1.Value = (decimal)_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.X;
                        numMaterialHookPickupMaterialPositionY1.Value = (decimal)_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.Y;
                        numMaterialHookPickupMaterialPositionZ1.Value = (decimal)_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.Z;
                    }

                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    if (numMaterialRawNum.Value < _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialRowNumber && numMaterialColNum.Value < _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialColNumber)
                    {
                        numMaterialHookPickupMaterialPositionX1.Value = (decimal)_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.X;
                        numMaterialHookPickupMaterialPositionY1.Value = (decimal)_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.Y;
                        numMaterialHookPickupMaterialPositionZ1.Value = (decimal)_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialMat[(int)numMaterialRawNum.Value][(int)numMaterialColNum.Value].MaterialPosition.Z;
                    }

                }
            }

        }

        private void btnMaterialHookPickupMaterialPositionPickup_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialHookPickupMaterialPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialHookPickupMaterialPositionX.Value,
                    Y = (double)numMaterialHookPickupMaterialPositionY.Value,
                    Z = (double)numMaterialHookPickupMaterialPositionZ.Value,
                };
                _selRecipe.MaterialHookClose = (float)numMaterialHookClose.Value;
                _selRecipe.MaterialHookUp = (float)numMaterialHookUp.Value;
                _selRecipe.MaterialCompensationAngle = (float)numericMaterialCompensationAngle.Value;
                _selRecipe.MaterialCompensationZ = (float)numMaterialCompensationZ.Value;

                if (numMaterialRawNum.Value >= 0 && numMaterialRawNum.Value <= 14 && numMaterialColNum.Value >= 0 && numMaterialColNum.Value <= 14)
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        if (numMaterialRawNum.Value < _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialRowNumber && numMaterialColNum.Value < _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialColNumber)
                        {
                            float Z = (float)numMaterialHookPickupMaterialPositionZ1.Value;

                            if (Z == 0)
                            {
                                return;
                            }
                            Task.Run(() =>
                            {
                                TransportControl.Instance.MaterialHookPickupMaterialAction(Z, _selRecipe.MaterialHookUp);
                            });
                        }

                    }
                    else if (comboBox2.SelectedIndex == 1)
                    {
                        if (numMaterialRawNum.Value < _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialRowNumber && numMaterialColNum.Value < _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialColNumber)
                        {
                            float Z = (float)numMaterialHookPickupMaterialPositionZ1.Value;

                            if (Z == 0)
                            {
                                return;
                            }
                            Task.Run(() =>
                            {
                                TransportControl.Instance.MaterialHookPickupMaterialAction(Z, _selRecipe.MaterialHookUp);
                            });
                        }

                    }
                }

                TransportControl.Instance.TransportRecipe = _selRecipe;

               
               

            }

        }

        private void btnMaterialHookPickupMaterialPositionPutdown_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.MaterialHookPickupMaterialPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialHookPickupMaterialPositionX.Value,
                    Y = (double)numMaterialHookPickupMaterialPositionY.Value,
                    Z = (double)numMaterialHookPickupMaterialPositionZ.Value,
                };
                _selRecipe.MaterialHookClose = (float)numMaterialHookClose.Value;
                _selRecipe.MaterialHookUp = (float)numMaterialHookUp.Value;
                _selRecipe.MaterialCompensationAngle = (float)numericMaterialCompensationAngle.Value;
                _selRecipe.MaterialCompensationZ = (float)numMaterialCompensationZ.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                if (numMaterialRawNum.Value >= 0 && numMaterialRawNum.Value <= 14 && numMaterialColNum.Value >= 0 && numMaterialColNum.Value <= 14)
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        if (numMaterialRawNum.Value < _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialRowNumber && numMaterialColNum.Value < _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialColNumber)
                        {
                            float Z = (float)numMaterialHookPickupMaterialPositionZ1.Value;

                            if (Z == 0)
                            {
                                return;
                            }
                            Task.Run(() =>
                            {
                                TransportControl.Instance.MaterialHookPutdownMaterialAction(Z, _selRecipe.MaterialHookUp);
                            });
                        }

                    }
                    else if (comboBox2.SelectedIndex == 1)
                    {
                        if (numMaterialRawNum.Value < _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialRowNumber && numMaterialColNum.Value < _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialColNumber)
                        {
                            float Z = (float)numMaterialHookPickupMaterialPositionZ1.Value;

                            if (Z == 0)
                            {
                                return;
                            }
                            Task.Run(() =>
                            {
                                TransportControl.Instance.MaterialHookPutdownMaterialAction(Z, _selRecipe.MaterialHookUp);
                            });
                        }

                    }
                }

            }
        }


        private void btnGreateMaterialRecognition_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    this.TrackCameraIdentifyMaterialMatch = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.TrackCameraIdentifyMaterialMatch;
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    this.TrackCameraIdentifyMaterialMatch = _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.TrackCameraIdentifyMaterialMatch;
                }

                string name = "搬送相机创建物料识别";
                string title = "";
                VisualMatchControlGUI visualMatch = new VisualMatchControlGUI();
                visualMatch.InitVisualControl(CameraWindowGUI.Instance, SystemCalibration.Instance.TrackCameraVisual);

                visualMatch.SetVisualParam(this.TrackCameraIdentifyMaterialMatch);

                int Done = SystemCalibration.Instance.ShowVisualForm(visualMatch, name, title);

                if (Done == 0)
                {
                    return;
                }
                else
                {
                    this.TrackCameraIdentifyMaterialMatch = visualMatch.GetVisualParam();

                    if (comboBox2.SelectedIndex == 0)
                    {
                        _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.TrackCameraIdentifyMaterialMatch = this.TrackCameraIdentifyMaterialMatch;
                    }
                    else if (comboBox2.SelectedIndex == 1)
                    {
                        _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.TrackCameraIdentifyMaterialMatch = this.TrackCameraIdentifyMaterialMatch;
                    }

                    TransportControl.Instance.TransportRecipe = _selRecipe;
                }

            }

        }



        private void btnSelectMaterialBox_2_Click(object sender, EventArgs e)
        {
            FrmMaterialBoxRecipeEditor selectRecipeDialog = new FrmMaterialBoxRecipeEditor(null, this.teMaterialBoxName_2.Text.ToUpper().Trim());
            if (selectRecipeDialog.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                try
                {
                    _selectedMaterialBoxRecipeName = selectRecipeDialog.SelectedRecipeName;
                    //验证Recipe是否完整
                    if (!MaterialBoxRecipe.Validate(_selectedMaterialBoxRecipeName, selectRecipeDialog.RecipeType))
                    {
                        WarningBox.FormShow("错误！", "配方无效！", "提示");
                        return;
                    }
                    else
                    {
                        //var heatRecipe = TransportRecipe.LoadRecipe(_selectedHeatRecipeName, selectRecipeDialog.RecipeType);
                        teMaterialBoxName_2.Text = selectRecipeDialog.SelectedRecipeName;
                        _materialboxRecipe = MaterialBoxRecipe.LoadRecipe(selectRecipeDialog.SelectedRecipeName, selectRecipeDialog.RecipeType);
                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(EnumLogContentType.Error, "JobControlPanel: Exception occured while Loading Recipe.", ex);
                }
            }
        }

        private void PickupMaterialSave_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                #region 夹取物料

                _selRecipe.MaterialHookSafePosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialHookSafePositionX.Value,
                    Y = (double)numMaterialHookSafePositionY.Value,
                    Z = (double)numMaterialHookSafePositionZ.Value,
                };
                _selRecipe.MaterialHookOpen = (float)numMaterialHookOpen.Value;

                _selRecipe.MaterialHookPickupMaterialPosition = new XYZTCoordinateConfig()
                {
                    X = (double)numMaterialHookPickupMaterialPositionX.Value,
                    Y = (double)numMaterialHookPickupMaterialPositionY.Value,
                    Z = (double)numMaterialHookPickupMaterialPositionZ.Value,
                };
                _selRecipe.MaterialHookClose = (float)numMaterialHookClose.Value;
                _selRecipe.MaterialHookUp = (float)numMaterialHookUp.Value;
                _selRecipe.MaterialCompensationAngle = (float)numericMaterialCompensationAngle.Value;
                _selRecipe.MaterialCompensationZ = (float)numMaterialCompensationZ.Value;

                float offsetT = (float)numericMaterialCompensationAngle.Value;
                float offsetZ = (float)numMaterialCompensationZ.Value;

                if (comboBox2.SelectedIndex == 0)
                {
                    _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].Iswelded = false;

                    if (_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].Iswelded == false)
                    {
                        //XYZTCoordinateConfig offset = new XYZTCoordinateConfig()
                        //{
                        //    X = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter.X,
                        //    Y = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter.Y,
                        //    Z = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter.Z,
                        //    Theta = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter.Theta + offsetT,

                        //};
                        //_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].SetMaterialMat(_selRecipe.MaterialHookPickupMaterialPosition, _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter, offset);

                        XYZTCoordinateConfig offset = new XYZTCoordinateConfig()
                        {
                            X = _selRecipe.MaterialHookPickupMaterialPosition.X,
                            Y = _selRecipe.MaterialHookPickupMaterialPosition.Y,
                            Z = _selRecipe.MaterialHookPickupMaterialPosition.Z + offsetZ,
                            Theta = _selRecipe.MaterialHookPickupMaterialPosition.Theta + offsetT,

                        };
                        _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].SetMaterialMat(_selRecipe.MaterialHookPickupMaterialPosition, _selRecipe.MaterialHookPickupMaterialPosition, offset, 
                            SystemConfiguration.Instance.PositioningConfig.MaterialArrayQxX, SystemConfiguration.Instance.PositioningConfig.MaterialArrayQxY,
                            SystemConfiguration.Instance.PositioningConfig.MaterialArrayQyX, SystemConfiguration.Instance.PositioningConfig.MaterialArrayQyY,
                            SystemConfiguration.Instance.PositioningConfig.MaterialArrayQzX, SystemConfiguration.Instance.PositioningConfig.MaterialArrayQzY);

                        var _materialboxRecipe = MaterialBoxRecipe.LoadRecipe(_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].Name, EnumRecipeType.MaterialBox);

                        if (_materialboxRecipe.RecipeName == teMaterialBoxName_2.Text)
                        {
                            _materialboxRecipe.MaterialBoxParam.SetMaterialMat(_selRecipe.MaterialHookPickupMaterialPosition, _selRecipe.MaterialHookPickupMaterialPosition, offset,
                            SystemConfiguration.Instance.PositioningConfig.MaterialArrayQxX, SystemConfiguration.Instance.PositioningConfig.MaterialArrayQxY,
                            SystemConfiguration.Instance.PositioningConfig.MaterialArrayQyX, SystemConfiguration.Instance.PositioningConfig.MaterialArrayQyY,
                            SystemConfiguration.Instance.PositioningConfig.MaterialArrayQzX, SystemConfiguration.Instance.PositioningConfig.MaterialArrayQzY);
                            _materialboxRecipe.SaveRecipe();
                        }
                    }
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].Iswelded = false;

                    if (_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].Iswelded == false)
                    {
                        //XYZTCoordinateConfig offset = new XYZTCoordinateConfig()
                        //{
                        //    X = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter.X,
                        //    Y = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter.Y,
                        //    Z = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter.Z,
                        //    Theta = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter.Theta + offsetT,

                        //};
                        //_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].SetMaterialMat(_selRecipe.MaterialHookPickupMaterialPosition, _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialboxCenter, offset);

                        XYZTCoordinateConfig offset = new XYZTCoordinateConfig()
                        {
                            X = _selRecipe.MaterialHookPickupMaterialPosition.X,
                            Y = _selRecipe.MaterialHookPickupMaterialPosition.Y,
                            Z = _selRecipe.MaterialHookPickupMaterialPosition.Z + offsetZ,
                            Theta = _selRecipe.MaterialHookPickupMaterialPosition.Theta + offsetT,

                        };
                        _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].SetMaterialMat(_selRecipe.MaterialHookPickupMaterialPosition, _selRecipe.MaterialHookPickupMaterialPosition, offset,
                            SystemConfiguration.Instance.PositioningConfig.MaterialArrayQxX, SystemConfiguration.Instance.PositioningConfig.MaterialArrayQxY,
                            SystemConfiguration.Instance.PositioningConfig.MaterialArrayQyX, SystemConfiguration.Instance.PositioningConfig.MaterialArrayQyY,
                            SystemConfiguration.Instance.PositioningConfig.MaterialArrayQzX, SystemConfiguration.Instance.PositioningConfig.MaterialArrayQzY);

                        var _materialboxRecipe = MaterialBoxRecipe.LoadRecipe(_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].Name, EnumRecipeType.MaterialBox);

                        if (_materialboxRecipe.RecipeName == teMaterialBoxName_2.Text)
                        {
                            _materialboxRecipe.MaterialBoxParam.SetMaterialMat(_selRecipe.MaterialHookPickupMaterialPosition, _selRecipe.MaterialHookPickupMaterialPosition, offset,
                            SystemConfiguration.Instance.PositioningConfig.MaterialArrayQxX, SystemConfiguration.Instance.PositioningConfig.MaterialArrayQxY,
                            SystemConfiguration.Instance.PositioningConfig.MaterialArrayQyX, SystemConfiguration.Instance.PositioningConfig.MaterialArrayQyY,
                            SystemConfiguration.Instance.PositioningConfig.MaterialArrayQzX, SystemConfiguration.Instance.PositioningConfig.MaterialArrayQzY);
                            _materialboxRecipe.SaveRecipe();
                        }
                    }
                }

                if (comboBox2.SelectedIndex == 0)
                {
                    _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.TrackCameraIdentifyMaterialMatch = this.TrackCameraIdentifyMaterialMatch;
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.TrackCameraIdentifyMaterialMatch = this.TrackCameraIdentifyMaterialMatch;
                }



                TransportControl.Instance.TransportRecipe = _selRecipe;

                _selRecipe.SaveRecipe();

                #endregion


            }
        }


        #endregion

        #region 物料到焊台


        private void btnMaterialHooktoTargetPositionRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numMaterialHooktoTargetPositionX.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialX);
                numMaterialHooktoTargetPositionY.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialY);
                numMaterialHooktoTargetPositionZ.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.MaterialZ);

                if (_selRecipe.MaterialHooktoTargetPosition.Count > this.comboBox1.SelectedIndex)
                {
                    _selRecipe.MaterialHooktoTargetPosition[this.comboBox1.SelectedIndex] = new XYZTCoordinateConfig()
                    {
                        X = (double)numMaterialHooktoTargetPositionX.Value,
                        Y = (double)numMaterialHooktoTargetPositionY.Value,
                        Z = (double)numMaterialHooktoTargetPositionZ.Value,
                    };
                    
                }
                if (this.MaterialHooktoTargetPosition.Count > this.comboBox1.SelectedIndex)
                {
                    this.MaterialHooktoTargetPosition[this.comboBox1.SelectedIndex] = new XYZTCoordinateConfig()
                    {
                        X = (double)numMaterialHooktoTargetPositionX.Value,
                        Y = (double)numMaterialHooktoTargetPositionY.Value,
                        Z = (double)numMaterialHooktoTargetPositionZ.Value,
                    };

                }
                _selRecipe.MaterialHookUp2 = 6.75f;

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }

        }

        private void btnMaterialHooktoTargetPositionMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                if (this.MaterialHooktoTargetPosition.Count > this.comboBox1.SelectedIndex)
                {
                    _selRecipe.MaterialHooktoTargetPosition[this.comboBox1.SelectedIndex] = new XYZTCoordinateConfig()
                    {
                        X = (double)numMaterialHooktoTargetPositionX.Value,
                        Y = (double)numMaterialHooktoTargetPositionY.Value,
                        Z = (double)numMaterialHooktoTargetPositionZ.Value,
                    };
                }

                _selRecipe.MaterialHookUp2 = 6.75f;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                int Index = this.comboBox1.SelectedIndex;



                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialHooktoTargetPositionAction(_selRecipe.MaterialHooktoTargetPosition[Index], _selRecipe.MaterialHookUp2);
                });

            }

        }

        private void btnMaterialHooktoTargetPositionPickup_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                if (this.MaterialHooktoTargetPosition.Count > this.comboBox1.SelectedIndex)
                {
                    _selRecipe.MaterialHooktoTargetPosition[this.comboBox1.SelectedIndex] = new XYZTCoordinateConfig()
                    {
                        X = (double)numMaterialHooktoTargetPositionX.Value,
                        Y = (double)numMaterialHooktoTargetPositionY.Value,
                        Z = (double)numMaterialHooktoTargetPositionZ.Value,
                    };
                }

                TransportControl.Instance.TransportRecipe = _selRecipe;

                int Index = this.comboBox1.SelectedIndex;
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialHookPickupMaterialAction(_selRecipe.MaterialHooktoTargetPosition[Index].Z, _selRecipe.MaterialHookUp2);
                });

            }

        }

        private void btnMaterialHooktoTargetPositionPutdown_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                if (this.MaterialHooktoTargetPosition.Count > this.comboBox1.SelectedIndex)
                {
                    _selRecipe.MaterialHooktoTargetPosition[this.comboBox1.SelectedIndex] = new XYZTCoordinateConfig()
                    {
                        X = (double)numMaterialHooktoTargetPositionX.Value,
                        Y = (double)numMaterialHooktoTargetPositionY.Value,
                        Z = (double)numMaterialHooktoTargetPositionZ.Value,
                    };
                }

                TransportControl.Instance.TransportRecipe = _selRecipe;

                int Index = this.comboBox1.SelectedIndex;
                Task.Run(() =>
                {
                    TransportControl.Instance.MaterialHookPutdownMaterialAction(_selRecipe.MaterialHooktoTargetPosition[Index].Z, _selRecipe.MaterialHookUp2);
                });

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.MaterialHooktoTargetPosition.Count > this.comboBox1.SelectedIndex)
            {
                numMaterialHooktoTargetPositionX.Value = (decimal)_selRecipe.MaterialHooktoTargetPosition[this.comboBox1.SelectedIndex].X;
                numMaterialHooktoTargetPositionY.Value = (decimal)_selRecipe.MaterialHooktoTargetPosition[this.comboBox1.SelectedIndex].Y;
                numMaterialHooktoTargetPositionZ.Value = (decimal)_selRecipe.MaterialHooktoTargetPosition[this.comboBox1.SelectedIndex].Z;

            }
        }

        private void btnPressliftingSafePositionRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numPressliftingSafePosition.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.Presslifting);

                _selRecipe.PressliftingSafePosition = (float)numPressliftingSafePosition.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnPressliftingSafePositionMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                _selRecipe.PressliftingSafePosition = (float)numPressliftingSafePosition.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                

                Task.Run(() =>
                {
                    TransportControl.Instance.PressliftingPutdownMaterialAction();
                });

            }

        }

        private void btnPressliftingWorkPositionRead_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                numPressliftingWorkPosition.Value = (decimal)TransportControl.Instance.ReadCurrentAxisposition(EnumStageAxis.Presslifting);

                _selRecipe.PressliftingWorkPosition = (float)numPressliftingWorkPosition.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

            }
        }

        private void btnPressliftingWorkPositionMove_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null && TransportControl.Instance.Readsensor(EnumSensor.Oven1InteriorDoorOpen))
            {
                _selRecipe.PressliftingWorkPosition = (float)numPressliftingWorkPosition.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                
                Task.Run(() =>
                {
                    TransportControl.Instance.PressliftingLiftUpMaterialAction();
                });

            }
        }

        private void btnGreateMaterialStatsRecognition_Click(object sender, EventArgs e)
        {
            if (_selRecipe != null)
            {
                if(comboBox2.SelectedIndex == 0)
                {
                    if (_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatchs.Count == _selRecipe.WeldNum)
                    {
                        this.WeldCameraIdentifyMaterialMatch = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatchs[comboBox1.SelectedIndex];
                    }
                    else
                    {
                        this.WeldCameraIdentifyMaterialMatch = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatch;
                    }
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    if (_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatchs.Count == _selRecipe.WeldNum)
                    {
                        this.WeldCameraIdentifyMaterialMatch = _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatchs[comboBox1.SelectedIndex];
                    }
                    else
                    {
                        this.WeldCameraIdentifyMaterialMatch = _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatch;
                    }
                }





                string name = "焊台相机创建物料识别";
                string title = "";
                VisualMatchControlGUI visualMatch = new VisualMatchControlGUI();
                visualMatch.InitVisualControl(CameraWindowGUI.Instance, SystemCalibration.Instance.WeldCameraVisual);

                visualMatch.SetVisualParam(this.WeldCameraIdentifyMaterialMatch);

                int Done = SystemCalibration.Instance.ShowVisualForm(visualMatch, name, title);

                if (Done == 0)
                {
                    return;
                }
                else
                {
                    this.WeldCameraIdentifyMaterialMatch = visualMatch.GetVisualParam();

                    if (comboBox2.SelectedIndex == 0)
                    {
                        _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatch = this.WeldCameraIdentifyMaterialMatch;
                        _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatchs[comboBox1.SelectedIndex] = this.WeldCameraIdentifyMaterialMatch;

                        MaterialRecipe materialRecipe = MaterialRecipe.LoadRecipe(_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.Name, EnumRecipeType.Material);
                        materialRecipe.MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                        materialRecipe.MaterialParam.WeldCameraIdentifyMaterialMatchs = _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatchs;
                        materialRecipe.SaveRecipe();
                    }
                    else if (comboBox2.SelectedIndex == 1)
                    {
                        _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatch = this.WeldCameraIdentifyMaterialMatch;
                        _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatchs[comboBox1.SelectedIndex] = this.WeldCameraIdentifyMaterialMatch;

                        MaterialRecipe materialRecipe = MaterialRecipe.LoadRecipe(_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.Name, EnumRecipeType.Material);
                        materialRecipe.MaterialParam.WeldCameraIdentifyMaterialMatchs.Clear();
                        materialRecipe.MaterialParam.WeldCameraIdentifyMaterialMatchs = _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatchs;
                        materialRecipe.SaveRecipe();
                    }

                    

                    TransportControl.Instance.TransportRecipe = _selRecipe;
                }

            }
        }

        private void MaterialMovetoWeldTableSave_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                #region 物料到焊台

                if (_selRecipe.MaterialHooktoTargetPosition?.Count < _selRecipe.WeldNum)
                {
                    _selRecipe.MaterialHooktoTargetPosition.Clear();
                    for (int i = 0; i < _selRecipe.WeldNum; i++)
                    {
                        _selRecipe.MaterialHooktoTargetPosition.Add(new XYZTCoordinateConfig());
                    }
                }

                if (this.MaterialHooktoTargetPosition?.Count < _selRecipe.WeldNum)
                {
                    this.MaterialHooktoTargetPosition.Clear();
                    for (int i = 0; i < _selRecipe.WeldNum; i++)
                    {
                        this.MaterialHooktoTargetPosition.Add(new XYZTCoordinateConfig());
                    }
                }
                else
                {
                    _selRecipe.MaterialHooktoTargetPosition = this.MaterialHooktoTargetPosition;
                }

                _selRecipe.PressliftingSafePosition = (float)numPressliftingSafePosition.Value;
                _selRecipe.PressliftingWorkPosition = (float)numPressliftingWorkPosition.Value;

                if (comboBox2.SelectedIndex == 0)
                {
                    MaterialRecipe materialRecipe = MaterialRecipe.LoadRecipe(_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.Name, EnumRecipeType.Material);
                    MaterialBoxRecipe materialBoxRecipe = MaterialBoxRecipe.LoadRecipe(_selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].Name, EnumRecipeType.MaterialBox);
                    _selRecipe.OverBox1Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatchs = materialRecipe.MaterialParam.WeldCameraIdentifyMaterialMatchs;
                    materialBoxRecipe.MaterialBoxParam.MaterialParam.WeldCameraIdentifyMaterialMatchs = materialRecipe.MaterialParam.WeldCameraIdentifyMaterialMatchs;
                    materialBoxRecipe.SaveRecipe();

                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    MaterialRecipe materialRecipe = MaterialRecipe.LoadRecipe(_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.Name, EnumRecipeType.Material);
                    MaterialBoxRecipe materialBoxRecipe = MaterialBoxRecipe.LoadRecipe(_selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].Name, EnumRecipeType.MaterialBox);
                    _selRecipe.OverBox2Param.MaterialboxParam[comboBox3.SelectedIndex].MaterialParam.WeldCameraIdentifyMaterialMatchs = materialRecipe.MaterialParam.WeldCameraIdentifyMaterialMatchs;
                    materialBoxRecipe.MaterialBoxParam.MaterialParam.WeldCameraIdentifyMaterialMatchs = materialRecipe.MaterialParam.WeldCameraIdentifyMaterialMatchs;
                    materialBoxRecipe.SaveRecipe();
                }

                //_selRecipe.OverBox1Param.MaterialboxParam[0].MaterialParam.WeldCameraIdentifyMaterialMatch = this.WeldCameraIdentifyMaterialMatch;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                _selRecipe.SaveRecipe();

                #endregion



            }
        }


        #endregion

        #endregion

        #region 焊接

        private void btnWeld_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                _selRecipe.WeldTime = (float)numWeldTime.Value;

                _selRecipe.WeldPessure = (float)numWeldPessure.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                

                Task.Run(() =>
                {
                    TransportControl.Instance.WeldMaterialAction();
                });

            }
        }

        private void btnWeldReset_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                
                Task.Run(() =>
                {
                    TransportControl.Instance.WeldResetAction();
                });

            }
        }

        private void btnStopWeld_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
               
                Task.Run(() =>
                {
                    TransportControl.Instance.StopWeldMaterialAction();
                });

            }
        }

        private void btnStopWeldReset_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                
                Task.Run(() =>
                {
                    TransportControl.Instance.StopWeldResetAction();
                });

            }
        }


        private void WeldSave_Click(object sender, EventArgs e)
        {
            if (TransportControl.Instance.TransportRecipe != null)
            {
                #region 焊接

                _selRecipe.WeldTime = (float)numWeldTime.Value;
                _selRecipe.WeldPessure = (float)numWeldPessure.Value;

                TransportControl.Instance.TransportRecipe = _selRecipe;

                _selRecipe.SaveRecipe();

                #endregion



            }

        }







        #endregion

       
    }

}
