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

namespace ControlPanelClsLib.Recipe
{
    public partial class RecipeStep_ComponentCarrierSettings : RecipeStepBase
    {
        public RecipeStep_ComponentCarrierSettings()
        {
            InitializeComponent();
        }
        private ComponentPositionStepBasePage currentStepPage;
        /// <summary>
        /// 加载Recipe内容
        /// </summary>
        /// <param name="recipe"></param>
        public override void LoadEditedRecipe(BondRecipe recipe) 
        { 
            _editRecipe = recipe;
            LoadNextStepPage();
        }

        /// <summary>
        /// 验证单步定义是否完成
        /// </summary>
        public override void VertifyAndNotifySingleStepDefineFinished(out bool finished, out EnumRecipeStep currengStep) 
        { 
            finished = false; 
            currengStep = EnumRecipeStep.Component_MaterialMap; 
        }
        private void LoadPreviousStepPage()
        {
            if (currentStepPage != null)
            {
                var finished = false;
                var step = EnumDefineSetupRecipeComponentPositionStep.None;
                currentStepPage.NotifyStepFinished(out finished, out step);
                _editRecipe.SaveRecipe();
                this.panelStepOperate.Controls.Clear();
                currentStepPage = GenerateTeachStepPage(currentStepPage.CurrentStep - 1);
                currentStepPage.Dock = DockStyle.Fill;
                this.panelStepOperate.Controls.Add(currentStepPage);
            }
            else
            {
                this.panelStepOperate.Controls.Clear();
                currentStepPage = new ComponentPositionStep_ESHeight();

                currentStepPage.Dock = DockStyle.Fill;
                this.panelStepOperate.Controls.Add(currentStepPage);
            }
            currentStepPage.LoadEditedRecipe(_editRecipe);
            if (currentStepPage.CurrentStep == EnumDefineSetupRecipeComponentPositionStep.SetESHeight)
            {
                this.btnPrevious.Visible = false;
            }
            else
            {
                this.btnPrevious.Visible = true;
            }
            if (currentStepPage.CurrentStep == EnumDefineSetupRecipeComponentPositionStep.SetComponentLeftLowerCorner)
            {
                this.btnNext.Visible = false;
            }
            else
            {
                this.btnNext.Visible = true;
            }
            //this.labelStepInfo.Text = currentStepPage.StepDescription;
            //LoadStepParameters(currentTeachStepPage.CurrentStep);
        }
        private void LoadNextStepPage()
        {
            if (currentStepPage != null)
            {
                var finished = false;
                var step = EnumDefineSetupRecipeComponentPositionStep.None;
                currentStepPage.NotifyStepFinished(out finished, out step);
                _editRecipe.SaveRecipe();
                this.panelStepOperate.Controls.Clear();
                currentStepPage = GenerateTeachStepPage(currentStepPage.CurrentStep + 1);
                currentStepPage.Dock = DockStyle.Fill;
                this.panelStepOperate.Controls.Add(currentStepPage);
            }
            else
            {
                this.panelStepOperate.Controls.Clear();
                currentStepPage = new ComponentPositionStep_ESHeight();

                currentStepPage.Dock = DockStyle.Fill;
                this.panelStepOperate.Controls.Add(currentStepPage);
            }
            currentStepPage.LoadEditedRecipe(_editRecipe);
            if (currentStepPage.CurrentStep == EnumDefineSetupRecipeComponentPositionStep.SetESHeight)
            {
                this.btnPrevious.Visible = false;
            }
            else
            {
                this.btnPrevious.Visible = true;
            }
            if (currentStepPage.CurrentStep == EnumDefineSetupRecipeComponentPositionStep.SetComponentLeftLowerCorner)
            {
                this.btnNext.Visible = false;
            }
            else
            {
                this.btnNext.Visible = true;
            }
            //this.labelStepInfo.Text = currentStepPage.StepDescription;
            //LoadStepParameters(currentTeachStepPage.CurrentStep);
        }

        private ComponentPositionStepBasePage GenerateTeachStepPage(EnumDefineSetupRecipeComponentPositionStep step)
        {
            var ret = new ComponentPositionStepBasePage();
            switch (step)
            {
                case EnumDefineSetupRecipeComponentPositionStep.None:
                    break;
                case EnumDefineSetupRecipeComponentPositionStep.SetESHeight:
                    ret = new ComponentPositionStep_ESHeight();
                    break;
                case EnumDefineSetupRecipeComponentPositionStep.SetComponentLeftUpperCorner:
                    ret = new ComponentPositionStep_SizeLUCorner();
                    break;
                case EnumDefineSetupRecipeComponentPositionStep.SetComponentRightUpperCorner:
                    ret = new ComponentPositionStep_SizeRUCorner();
                    break;
                case EnumDefineSetupRecipeComponentPositionStep.SetComponentRightLowerCorner:
                    ret = new ComponentPositionStep_SizeRLCorner();
                    break;
                case EnumDefineSetupRecipeComponentPositionStep.SetComponentLeftLowerCorner:
                    ret = new ComponentPositionStep_SizeLLCorner();
                    break;
                default:
                    break;
            }
            return ret;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            LoadPreviousStepPage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            LoadNextStepPage();
        }
    }
}
