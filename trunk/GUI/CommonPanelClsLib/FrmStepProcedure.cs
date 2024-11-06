using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraBars;
using GlobalDataDefineClsLib;
using CommonPanelClsLib;
using ConfigurationClsLib;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.LoggerManagerClsLib;

namespace CommonPanelClsLib
{
    public partial class FrmStepProcedure : BaseForm
    {

        /// <summary>
        /// 系统配置
        /// </summary>
        private SystemConfiguration _systemConfiguration
        {
            get { return SystemConfiguration.Instance; }
        }


        /// <summary>
        /// 页面无参构造函数
        /// </summary>
        public FrmStepProcedure()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 页面加载时执行
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
            }
            finally
            {
                base.OnLoad(e);
            }
        }
        private StepNodePanel _previousFuncClient = null;
        /// <summary>
        /// 通知整个Recipe定义完成
        /// </summary>
        /// <param name="recipe"></param>
        private void NotifyCurrentStepCompleted()
        {


            //提示成功,是否进入下一页面编辑
            TreeListNode nextStepNode = null;
            if (WarningBox.FormShow("保存成功",string.IsNullOrEmpty("") ? "创建完成." : "单击<yes>转到下一步 ? ", "提示") == 1)
            {
                
            }
        }
        /// <summary>
        /// 切换到子节点页面
        /// </summary>
        /// <param name="type"></param>
        private void ChangeToRecipeNodeView(string nodeCaption)
        {
            this.Invoke(new Action(() =>
            {
                StepNodePanel funcClient = null;
                //释放所有动态加载的控件
                while (nodeContainer.Controls.Count > 0)
                {
                    var control = nodeContainer.Controls[0];
                    nodeContainer.Controls.Remove(control);
                    control.Dispose();
                    control = null;
                }
                switch (nodeCaption)
                {
                    case "配置":
                        //recipeRootStep = EnumRecipeRootStep.Configuration;
                        //funcClient = new RecipeNodeControl(_editRecipe, typeof(RecipeStep_Configuration)) { Dock = DockStyle.Fill };
                        //funcClient.Visible = false;
                        break;
                    case "设置":

                        break;
                    //case "烘箱设置":
                    //    funcClient = new RecipeNodeControl(_editRecipe, typeof(OvenSettings), recipeRootStep) { Dock = DockStyle.Fill };
                    //    funcClient.Visible = false;
                    //    break;
                    //case "纯化设置":
                    //    //funcClient = new RecipeNodeControl(_editRecipe, typeof(PureSettings), recipeRootStep) { Dock = DockStyle.Fill };
                    //    //funcClient.Visible = false;
                    //    break;
                    //case "焊接参数":
                    //    funcClient = new RecipeNodeControl(_editRecipe, typeof(WeldSettings), recipeRootStep) { Dock = DockStyle.Fill };
                    //    funcClient.Visible = false;
                    //    break;
                    ////case "Power Settings":
                    ////    funcClient = new RecipeNodeControl(_editRecipe, typeof(PowerSettings), recipeRootStep) { Dock = DockStyle.Fill };
                    ////    funcClient.Visible = false;
                    ////    break;
                    //case "工艺步骤":
                    //    break;
                    default:
                        funcClient = null;
                        break;
                }

                if (funcClient != null)
                {
                    _previousFuncClient = funcClient;
                    funcClient.Size = new Size(this.nodeContainer.Width, this.nodeContainer.Height);
                    funcClient.NotifyCurrentStepComplete = NotifyCurrentStepCompleted;
                    this.nodeContainer.Controls.Add(funcClient);
                    funcClient.Visible = true;
                }
            }));
        }

        private void OnDisplayRecipeStepName(string stepName)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { OnDisplayRecipeStepName(stepName); });
                return;
            }
        }



        /// <summary>
        /// 获取下一步的步骤节点名称
        /// </summary>
        /// <returns></returns>
        private string GetNextStepCaption(EnumRecipeStep currentStep, out TreeListNode nextStepNode)
        {
            string nextStepCaption = "";
            string nextStepRootCaption = "";
            switch (currentStep)
            {
                case EnumRecipeStep.Configuration:
                    nextStepCaption = "设置";
                    nextStepRootCaption = "基板";
                    break;
                //case EnumRecipeStep.TeachSettings:
                //    nextStepCaption = "焊接参数";
                //    break;
                //case EnumRecipeStep.OvenSettings:
                //    nextStepCaption = "焊接参数";
                //    break;
                //case EnumRecipeStep.PureSettings:
                //    nextStepCaption = "焊接参数";
                //    break;
                //case EnumRecipeStep.WeldSettings:
                //    nextStepCaption = string.Empty;
                //    break;
                //case EnumRecipeStep.PowerSettings:
                //    nextStepCaption = string.Empty;
                //    break;
                case EnumRecipeStep.None:
                case EnumRecipeStep.Create:
                default:
                    nextStepCaption = "";
                    break;
            }
            if (string.IsNullOrEmpty(nextStepCaption))
            {
                nextStepNode = null;
            }
            else
            {
                nextStepNode = GetNextStepNode(nextStepCaption, nextStepRootCaption);
            }
            return nextStepCaption;
        }

        /// <summary>
        /// 递归查找下一步功能的节点
        /// </summary>
        /// <param name="nextStepCaption"></param>
        /// <returns></returns>
        private TreeListNode GetNextStepNode(string nextStepCaption,string nextStepRootCaption)
        {
            string rootNodeCaption = nextStepRootCaption;
            string childNodeCaption = nextStepCaption;

            TreeListNode nextStepNode = null;
            //foreach (TreeListNode node in nextStepNodeRoot)
            //{
            //    if (node.GetDisplayText(0) != rootNodeCaption) continue;
            //    foreach (TreeListNode childNode in node.Nodes)
            //    {
            //        if (childNode.GetDisplayText(0) == childNodeCaption)
            //        {
            //            nextStepNode = childNode;
            //            break;
            //        }
            //    }
            //}
            return nextStepNode;
        }
        private DateTime _laseClickTime = DateTime.Now;


        private string _curNodeCaption = string.Empty;


    }
}
