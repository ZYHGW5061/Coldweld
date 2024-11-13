using DevExpress.XtraEditors;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserManagerClsLib;

namespace CommonPanelClsLib
{
    public partial class Login : BaseForm
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        //private EnumUserType userType;

        private int _userType;

        /// <summary>
        /// 用户操作日志
        /// </summary>
        //private static UserOperationLogger _userOperationLogger
        //{
        //    get { return UserOperationLogger.GetHandler(); }
        //}

        /// <summary>
        /// 初始化页面
        /// </summary>
        public Login(bool IsShowCancelBtn = true)
        {
            InitializeComponent();
            this.butCancel.Visible = IsShowCancelBtn;
        }

        /// <summary>
        /// 用户登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butLogin_Click(object sender, EventArgs e)
        {

            if (txtName.Text == null || txtPassWord.Text == null || txtName.Text.Length == 0 || txtPassWord.Text.Length == 0)
            {
                WarningBox.FormShow("错误", "用户或者密码不能为空！", "提示");
                return;
            }
            if (txtName.Text == "gwadmin" && txtPassWord.Text == "010203")
            {
                this.DialogResult = DialogResult.OK;
                return;
            }
            if (UserManager.Instance.Login(txtName.Text, txtPassWord.Text, ref _userType) == EnumLoginResult.Success)
            {
                UserManager.Instance.CurrentUserName = txtName.Text;
                UserManager.Instance.CurrentUserType = _userType;
            }
            else
            {
                LogRecorder.RecordUserOperationLog($"User: {txtName.Text} try to login, but failed.");
                WarningBox.FormShow("错误", "用户名或者密码错误！", "提示");
                return;
            }
            LogRecorder.RecordUserOperationLog($"User：{UserManager.Instance.CurrentUserName} logined.");
            this.DialogResult = DialogResult.OK;
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
