using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonPanelClsLib
{
    public partial class BaseUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public BaseUserControl()
        {
            InitializeComponent();
        }
        public void CreateWaitDialog()
        {
            SplashScreenManager.ShowForm(this.FindForm(), typeof(FrmWait), false, true, false);
            SplashScreenManager.Default.SetWaitFormCaption("Please Wait...");
        }

        /// <summary>
        /// 关闭等待窗口
        /// </summary>
        public void CloseWaitDialog()
        {
            if (SplashScreenManager.Default != null)
            {
                SplashScreenManager.CloseForm();
            }
        }
    }
}
