using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;

namespace CommonPanelClsLib
{
    /// <summary>
    /// 所有功能页面的控件基类
    /// </summary>
    public partial class ViewBase : XtraUserControl
    {

        /// <summary>
        /// 功能标题
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 父控件容器
        /// </summary>
        public TabPage OwnerTabPage { get; set; }
        public XtraTabPage OwnerTabPageV2 { get; set; }

        /// <summary>
        /// 获取所在功能区域
        /// </summary>
        //public FunctionalAreaBase ParentFunctionalArea { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ViewBase()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

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

        /// <summary>
        /// 设置等待窗口标题显示
        /// </summary>
        /// <param name="description"></param>
        public void SetWaitDialogCaption(string description)
        {
            if (SplashScreenManager.Default != null)
            {
                SplashScreenManager.Default.SetWaitFormDescription(description);
            }
        }

        /// <summary>
        /// 页面第一次加载时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                this.UpdateLocalizableLanguage();
            }
            finally
            {
                CloseWaitDialog();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.Controls.Count == 0)
            using (StringFormat stringFormat = new StringFormat())
            {
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                using (Font font = new Font("微软雅黑", 35f, FontStyle.Italic))
                {
                    e.Graphics.DrawString("This functionPage is under development...", font, Brushes.Gray, this.ClientRectangle, stringFormat);
                }

            }           
        }

        /// <summary>
        /// 更新本地语言包文字到UI
        /// </summary>
        public virtual void UpdateLocalizableLanguage() { }

        /// <summary>
        /// 为当前用户更新UI权限
        /// </summary>
        public virtual void UpdateRightsForCurrentUser() { }

        /// <summary>
        /// 页面可见性改变时执行，当执行功能切换时调用
        /// </summary>
        public virtual void OnFunctionalControlsVisibleChanged() { }
    }
}
