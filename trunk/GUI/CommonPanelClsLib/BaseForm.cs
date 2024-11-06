using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPanelClsLib
{
    public class BaseForm : DevExpress.XtraEditors.XtraForm
    {
        public static DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
        /// <summary>
        /// 创建等待窗口
        /// </summary>
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
