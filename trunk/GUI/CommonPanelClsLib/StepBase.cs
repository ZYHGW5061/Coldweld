using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.LoggerManagerClsLib;
using GlobalDataDefineClsLib;

namespace CommonPanelClsLib
{
    /// <summary>
    /// 单步定义recipe的UI交互基类
    /// </summary>
    public class StepBase : XtraUserControl, IStepBase
    {

        /// <summary>
        /// 异常日志记录器
        /// </summary>
        protected IBaseLogger _systemDebugLogger
        {
            get { return LoggerManager.GetHandler().GetFileLogger(GlobalParameterSetting.SYSTEM_DEBUG_LOGGER_ID); }
        }

        /// <summary>
        /// 上一步的页面Id
        /// </summary>
        public virtual Type PrePageView { get; set; }
        /// <summary>
        /// 上一步页面描述
        /// </summary>
        public virtual string PrePageViewDescription { get; set; }

        /// <summary>
        /// 下一步的页面Id
        /// </summary>
        public virtual Type NextPageView { get; set; }
        /// <summary>
        /// 下一步的的页面描述
        /// </summary>
        public virtual string NextPageViewDescription { get; set; }

        /// <summary>
        /// Recipe定义完成
        /// </summary>
        public virtual Action NotifySingleStepDefineFinished { get; set; }
        /// <summary>
        /// 验证单步定义是否完成
        /// </summary>
        public virtual void VertifyAndNotifySingleStepDefineFinished(out bool finished, out EnumRecipeStep currengStep) { finished = false; currengStep = EnumRecipeStep.Create; }


        /// <summary>
        /// 是否允许切换到前一页面
        /// </summary>
        /// <param name="enableGoto"></param>
        public virtual void GotoProviousStepPage(out bool enableGoto)
        {
            enableGoto = true;
        }

        /// <summary>
        /// 初始化UI控件
        /// </summary>
        protected virtual void InitControls() { }

        /// <summary>
        /// 通知页面运动系统运动过程已完成。
        /// </summary>
        protected virtual void NotifyTargetPositionReached() { }

        /// <summary>
        /// 加载时执行
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.InitControls();
            base.OnLoad(e);
        }

        /// <summary>
        /// 显示等待窗口
        /// </summary>
        protected void CreateWaitDialog(string description = null)
        {
            if (SplashScreenManager.Default == null)
            {
                SplashScreenManager.ShowForm(this.FindForm(), typeof(DemoWaitForm), false, true);
            }
            //SplashScreenManager.Default.Properties.ParentForm = this.FindForm();
            SplashScreenManager.Default.SetWaitFormCaption("");
            SplashScreenManager.Default.SetWaitFormDescription(string.IsNullOrEmpty(description) ? "Loading..." : description);
        }

        /// <summary>
        /// 关闭等待窗口
        /// </summary>
        protected void CloseWaitDialog()
        {
            if (SplashScreenManager.Default != null)
            {
                SplashScreenManager.CloseForm();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DefineRecipeControl
            // 
            this.Name = "DefineRecipeControl";
            this.Size = new System.Drawing.Size(240, 214);
            this.ResumeLayout(false);

        }

    }
}
