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
using DevExpress.XtraTab;
using System.Reflection;
using DevExpress.LookAndFeel;
using GlobalDataDefineClsLib;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.LoggerManagerClsLib;

namespace CommonPanelClsLib
{
    /// <summary>
    /// 
    /// </summary>
    public partial class StepNodePanel : XtraUserControl
    {
        /// <summary>
        /// 异常日志记录器
        /// </summary>
        private IBaseLogger _systemExceptionLogger
        {
            get { return LoggerManager.GetHandler().GetFileLogger(GlobalParameterSetting.SYSTEM_DEBUG_LOGGER_ID); }
        }


        /// <summary>
        /// 默认显示的页面
        /// </summary>
        private Type _defaultFirstPageViewType = null;
        /// <summary>
        /// 通知其他页面Recipe已保存
        /// </summary>
        public Action NotifyCurrentStepComplete { get; set; }

        /// <summary>
        /// 构造函数初始化
        /// </summary>
        //public StepNodePanel(BondRecipe editRecipe, Type firstPageViewType, EnumRecipeRootStep rootStep = EnumRecipeRootStep.Configuration)
        //{
        //    InitializeComponent();

        //    this._editRecipe = editRecipe;
        //    this._defaultFirstPageViewType = firstPageViewType;
        //    this._curRecipeStepOwner = rootStep;

        //    //this.tabControl1.SetPXFHStyle();
        //    this.tabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
        //}
        public StepNodePanel(Type firstPageViewType)
        {
            InitializeComponent();
            this._defaultFirstPageViewType = firstPageViewType;
        }
        /// <summary>
        /// 第一次加载时执行页面初始化
        /// </summary>
        protected override void OnFirstLoad()
        {
            try
            {
                //设置默认加载页面
                this.LoadSingleStepView(_defaultFirstPageViewType);
            }
            finally
            {
                base.OnFirstLoad();
            }
        }

        /// <summary>
        /// 上一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            var defineRecipeView = panelControl1.Controls[0] as IStepBase;
            if (defineRecipeView != null)
            {
                bool enableGoto = false;
                defineRecipeView.GotoProviousStepPage(out enableGoto);
                if (!enableGoto)
                {
                    return;
                }
            }

            if (btnNextView.Text == "保存")
            {
                btnNextView.Text = "下一步>>";
            }

            LoadSingleStepView(defineRecipeView.PrePageView);
        }


        /// <summary>
        /// 下一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            var defineView = panelControl1.Controls[0] as IStepBase;
            EnumRecipeStep currentStep = EnumRecipeStep.Create;
            if (defineView != null)
            {
                bool defined = false;
                defineView.VertifyAndNotifySingleStepDefineFinished(out defined, out currentStep);
                if (!defined)
                {
                    return;
                }
            }

            if (defineView != null)
            {

                if (panelControl1.Controls.Count > 0)
                {
                    var defineRecipeView1 = panelControl1.Controls[0] as IStepBase;
                    if (defineRecipeView1 != null && defineRecipeView1.GetType() != defineView.NextPageView)
                    {
                        while (panelControl1.Controls.Count > 0)
                        {
                            panelControl1.Controls[0].Dispose();
                        }
                        panelControl1.Tag = false;
                    }
                }

                //加载页面
                LoadSingleStepView(defineView.NextPageView);

                //判断当前页面是否是定义的最后一个界面
                defineView = panelControl1.Controls[0] as IStepBase;
                if (defineView.NextPageView == null || defineView.NextPageViewDescription == string.Empty)
                {
                    btnNextView.Text = "保存";
                    btnNextView.Enabled = true;
                }
            }

                btnPreviousView.Enabled = true;
        }

        /// <summary>
        /// 加载Recipe编辑的子页面
        /// </summary>
        /// <param name="subViewType"></param>
        /// <param name="tabPage"></param>
        private void LoadSingleStepView(Type subViewType)
        {
            try
            {
                this.LoadUIView(new Action(() =>
                {
                    ConstructorInfo constructor = subViewType.GetConstructor(Type.EmptyTypes);
                    if (constructor == null)
                    {
                        throw new ApplicationException(subViewType.FullName + " doesn't have public constructor with empty parameters");
                    }
                    var objView = constructor.Invoke(null);
                    if (objView is IStepBase) { (objView as IStepBase).NotifySingleStepDefineFinished += SingleStepDefined; }
                    UIFuncHelper.LoadUIView(objView as Control, panelControl1);
                }), (bool)(panelControl1.Tag ?? false), panelControl1);

                if (panelControl1.Controls.Count == 0) return;
                var defineRecipeView = panelControl1.Controls[0] as IStepBase;
                if (defineRecipeView != null)
                {
                    //设置next按钮信息
                    if (defineRecipeView.NextPageView == null)
                        this.btnNextView.Text = "保存";
                    else
                        this.btnNextView.Text = "下一步>>";
                    //设置previouse按钮信息
                    if (defineRecipeView.PrePageView == null)
                        this.btnPreviousView.Enabled = false;
                    else
                        this.btnPreviousView.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                _systemExceptionLogger.AddErrorContent("Load recipe subview：" + subViewType.ToString() + " enccount some exception。", ex);
            }
        }

        /// <summary>
        /// 单步Recipe定义完成
        /// </summary>
        /// <param name="recipeFinished"></param>
        private void SingleStepDefined()
        {
        }

        ///// <summary>
        ///// 通知整个Recipe定义完成
        ///// </summary>
        ///// <param name="recipe"></param>
        //private void NotifyRecipeDefined(BondRecipe recipe, EnumRecipeStep currentStep)
        //{
        //    if (NotifyRecipeSaved != null && _editRecipe != null)
        //    {
        //        NotifyRecipeSaved(_editRecipe, currentStep);
        //    }
        //}
        public void ReleaseResource()
        {
            if (panelControl1.Controls.Count == 0) return;
            var defineRecipeView = panelControl1.Controls[0] as IStepBase;
            if (defineRecipeView != null)
            {
                //var tempView=defineRecipeView as InspectionIllumination;
                //if(tempView!=null)
                //{
                //    tempView.ReleaseResourse();
                //}
            }
        }

        private void btnTestRecipe_Click(object sender, EventArgs e)
        {

        }
    }


    /// <summary>
    /// 界面上常用到的功能
    /// </summary>
    public static class UIFuncHelper
    {
        /// <summary>
        /// 准备加载功能界面
        /// </summary>
        /// <param name="ctrlView"></param>
        /// <param name="tabPage"></param>
        public static void LoadUIView(Control ctrlView,PanelControl panel)
        {
            ctrlView.Visible = false;
            ctrlView.Dock = DockStyle.Fill;
            panel.Controls.Add(ctrlView);
            ctrlView.Visible = true;
        }

        /// <summary>
        /// 加载并初始化一个功能页面
        /// </summary>
        /// <param name="loadAction">功能</param>
        /// <param name="isLoaded">首页面是否已经加载过</param>
        public static void LoadUIView(this Control moduleUi, Action loadAction, bool isLoaded, PanelControl panel)
        {
            if (isLoaded)
            {
                return;
            }
            moduleUi.Invoke(new Action(() =>
            {
                /* WaitFormManager.ShowWaitForm(moduleUi.FindForm(), "Please Wait", "Loading...");*/
                panel.SuspendLayout();
                if (loadAction != null)
                {
                    loadAction();
                }
                panel.ResumeLayout(true);
                //关闭等待画面  
                /*WaitFormManager.CloseWaitForm();*/
            }));
        }

    }  
}
