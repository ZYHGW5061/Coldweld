using CommonPanelClsLib;
using DevExpress.XtraEditors;
using GlobalDataDefineClsLib;
using RecipeClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ControlPanelClsLib
{
    public partial class FrmEditHeatRecipe : BaseForm
    {
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
        private HeatRecipe _selRecipe = null;
        private string _operation = "Edit";

        public FrmEditHeatRecipe(string operation, HeatRecipe recipe)
        {
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
            _selRecipe = recipe;
            _operation = operation;           
            BindingData();
        }

        private void BindingData()
        {
            if (_operation == "Add" && _selRecipe == null)
            {
                //this.txtID.Text = (LoginHelper.GetHandler.GetUserMaxID() + 1).ToString();
                this.teName.Text = "";
                this.tableLayoutPanel2.Controls.Add(this.btn_Add, 0, 0);
                this.btn_Add.Visible = true;
                this.btn_Edit.Visible = false;
                this.Text = "新增加热配方";

                this.teName.Text = "H0";
                this.seTargetTemp.Value = 120;
                this.seHeatPreservationMinute.Value = 60;
                this.seOverTemperatureThreshold.Value = 200;

                this.checkBox2.Checked = false;
                this.checkBox1.Checked = true;

                this.spinEdit1.ReadOnly = true;
                this.spinEdit2.ReadOnly = true;
                this.spinEdit3.ReadOnly = true;
                this.spinEdit4.ReadOnly = true;

                this.spinEdit1.Value = 2;
                this.spinEdit2.Value = 50000;
                this.spinEdit3.Value = 50;
                this.spinEdit4.Value = 10;

            }
            else if (_operation == "Add" && _selRecipe != null)
            {
                //this.txtID.Text = (LoginHelper.GetHandler.GetUserMaxID() + 1).ToString();
                this.teName.Text = "";
                this.tableLayoutPanel2.Controls.Add(this.btn_Add, 0, 0);
                this.btn_Add.Visible = true;
                this.btn_Edit.Visible = false;
                this.Text = "新增加热配方";

                this.teName.Text = _selRecipe.RecipeName;
                this.seTargetTemp.Value = _selRecipe.TargetTemperature;
                this.seHeatPreservationMinute.Value = _selRecipe.HeatPreservationMinute;
                this.seOverTemperatureThreshold.Value = _selRecipe.OverTemperatureThreshold;

                this.checkBox2.Checked = _selRecipe.IsPurge;
                this.checkBox1.Checked = !_selRecipe.IsPurge;

                this.spinEdit1.ReadOnly = !_selRecipe.IsPurge;
                this.spinEdit2.ReadOnly = !_selRecipe.IsPurge;
                this.spinEdit3.ReadOnly = !_selRecipe.IsPurge;
                this.spinEdit4.ReadOnly = !_selRecipe.IsPurge;

                this.spinEdit1.Value = _selRecipe.OverPurgeTime;
                this.spinEdit2.Value = _selRecipe.OvenPurgePressureUp;
                this.spinEdit3.Value = _selRecipe.OvenPurgePressureLower;
                this.spinEdit4.Value = _selRecipe.OvenPurgeInterval;
            }
            else if (_operation == "Edit" && _selRecipe != null)
            {
                this.tableLayoutPanel2.Controls.Add(this.btn_Edit, 0, 0);
                this.btn_Add.Visible = false;
                this.btn_Edit.Visible = true;
                this.teName.Text = _selRecipe.RecipeName;
                this.seTargetTemp.Value = _selRecipe.TargetTemperature;
                this.seHeatPreservationMinute.Value = _selRecipe.HeatPreservationMinute;
                this.seOverTemperatureThreshold.Value = _selRecipe.OverTemperatureThreshold;

                this.checkBox2.Checked = _selRecipe.IsPurge;
                this.checkBox1.Checked = !_selRecipe.IsPurge;

                this.spinEdit1.ReadOnly = !_selRecipe.IsPurge;
                this.spinEdit2.ReadOnly = !_selRecipe.IsPurge;
                this.spinEdit3.ReadOnly = !_selRecipe.IsPurge;
                this.spinEdit4.ReadOnly = !_selRecipe.IsPurge;

                this.spinEdit1.Value = _selRecipe.OverPurgeTime;
                this.spinEdit2.Value = _selRecipe.OvenPurgePressureUp;
                this.spinEdit3.Value = _selRecipe.OvenPurgePressureLower;
                this.spinEdit4.Value = _selRecipe.OvenPurgeInterval;


                this.Text = "编辑加热配方";
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string name = this.teName.Text.Trim();
            int targetTemp = (int)this.seTargetTemp.Value;
            int heatPreservationMinute = (int)this.seHeatPreservationMinute.Value;
            int overTemperatureThreshold = (int)this.seOverTemperatureThreshold.Value;

            bool IsPurge = this.checkBox2.Checked;

            int OverPurgeTime = (int)this.spinEdit1.Value;
            int OvenPurgePressureUp = (int)this.spinEdit2.Value;
            int OvenPurgePressureLower = (int)this.spinEdit3.Value;
            int OvenPurgeInterval = (int)this.spinEdit4.Value;
            if (Name == "" || targetTemp == 0 || heatPreservationMinute == 0|| overTemperatureThreshold==0 
                || (IsPurge && ( OverPurgeTime == 0 || OvenPurgePressureUp == 0 || OvenPurgePressureLower == 0 || OvenPurgeInterval == 0)))
            {
                WarningBox.FormShow("错误", "参数不符合要求!", "提示");
                return;
            }
            var newRecipe=HeatRecipe.CreateRecipe(name, EnumRecipeType.Heat);
            newRecipe.RecipeName = name;
            newRecipe.TargetTemperature = targetTemp;
            newRecipe.HeatPreservationMinute = heatPreservationMinute;
            newRecipe.OverTemperatureThreshold = overTemperatureThreshold;

            newRecipe.IsPurge = this.checkBox2.Checked;
            newRecipe.OverPurgeTime = OverPurgeTime;
            newRecipe.OvenPurgePressureUp = OvenPurgePressureUp;
            newRecipe.OvenPurgePressureLower = OvenPurgePressureLower;
            newRecipe.OvenPurgeInterval = OvenPurgeInterval;

            newRecipe.SaveRecipe();
            WarningBox.FormShow("成功!", "添加配方完成!", "提示");
            //int maxId = LoginHelper.GetHandler.GetUserMaxID() + 1;
            //string Name = this.txtName.Text;
            //string password = this.txtPassWord.Text;
            //string userType = this.cbxType.Text;
            //string description = this.txtDescription.Text;
            //int userTypeID = (int)this.cbxType.SelectedIndex + 1;
            //if (Name == "" || password == "" || userType == "")
            //{
            //    WarningBox.FormShow("错误", "账户信息不能为空!", "Tips");
            //    return;
            //}
            //if (UserManager.Instance.AddUser(Name, password, userTypeID, description))
            //{
            //    WarningBox.FormShow("成功!", "添加账户完成!", "Tips");
            //}
            //else
            //{
            //    WarningBox.FormShow("失败!", "添加账户失败!", "Tips");
            //    return;
            //}
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            string name = this.teName.Text.Trim();
            int targetTemp = (int)this.seTargetTemp.Value;
            int heatPreservationMinute = (int)this.seHeatPreservationMinute.Value;
            int overTemperatureThreshold = (int)this.seOverTemperatureThreshold.Value;
            bool IsPurge = this.checkBox2.Checked;

            int OverPurgeTime = (int)this.spinEdit1.Value;
            int OvenPurgePressureUp = (int)this.spinEdit2.Value;
            int OvenPurgePressureLower = (int)this.spinEdit3.Value;
            int OvenPurgeInterval = (int)this.spinEdit4.Value;
            if (Name == "" || targetTemp == 0 || heatPreservationMinute == 0 || overTemperatureThreshold == 0
                || (IsPurge && (OverPurgeTime == 0 || OvenPurgePressureUp == 0 || OvenPurgePressureLower == 0 || OvenPurgeInterval == 0)))
            {
                WarningBox.FormShow("错误", "参数不符合要求!", "提示");
                return;
            }

            if (_selRecipe != null)
            {
                _selRecipe.RecipeName = name;
                _selRecipe.TargetTemperature = targetTemp;
                _selRecipe.HeatPreservationMinute = heatPreservationMinute;
                _selRecipe.OverTemperatureThreshold = overTemperatureThreshold;

                _selRecipe.IsPurge = this.checkBox2.Checked;

                _selRecipe.OverPurgeTime = OverPurgeTime;
                _selRecipe.OvenPurgePressureUp = OvenPurgePressureUp;
                _selRecipe.OvenPurgePressureLower = OvenPurgePressureLower;
                _selRecipe.OvenPurgeInterval = OvenPurgeInterval;

                _selRecipe.SaveRecipe();
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox2.Checked;

            this.spinEdit1.ReadOnly = !checkBox2.Checked;
            this.spinEdit2.ReadOnly = !checkBox2.Checked;
            this.spinEdit3.ReadOnly = !checkBox2.Checked;
            this.spinEdit4.ReadOnly = !checkBox2.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox1.Checked;

            this.spinEdit1.ReadOnly = !checkBox2.Checked;
            this.spinEdit2.ReadOnly = !checkBox2.Checked;
            this.spinEdit3.ReadOnly = !checkBox2.Checked;
            this.spinEdit4.ReadOnly = !checkBox2.Checked;
        }
    }

}
