using ConfigurationClsLib;
using DevExpress.XtraEditors;
using GlobalDataDefineClsLib;
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
using WestDragon.Framework.UtilityHelper;

namespace ControlPanelClsLib
{
    public partial class FrmRecipeSelect : XtraForm
    {
        private string _textRecipePath = "";
        private readonly  int PAGE_NUMBER = 22;
        private  int _pageCount = 1;
        private int _maxCount = 1;
        private List<RecipeInfo> recipeInfo;
        public string SelectedRecipeName { get; set; }
        public EnumRecipeType RecipeType { get; set; }
        public string[] _recipeFiles;
        private string _recipePath = SystemConfiguration.Instance.SystemDefaultDirectory;
        private string _recipeName = string.Empty;

        public FrmRecipeSelect(string recipePath = null, string recipeName = null)
        {
            InitializeComponent();

            if (recipePath != null)
            {
                _recipePath = recipePath;
            }
            _recipeName = recipeName;
        }

        protected override void OnLoad(EventArgs e)
        {
            textPage.Text = "1";
            try
            {
                FindRecipe("");
                textFind.Text = _recipeName;
                LoadRecipe();
            }
            finally
            {
                base.OnLoad(e);
            }
        }

        private void FindRecipe(string findStr)
        {
            string recipeDir = _recipePath + @"Recipes\Welder";
            _textRecipePath = recipeDir;
            CommonProcess.EnsureFolderExist(recipeDir);
            string[] _recipes = Directory.GetDirectories(recipeDir);
            //recipeDir = _recipePath + @"Recipes\Inch12";
            //CommonProcess.EnsureFolderExist(recipeDir);
            //string[] _recipeInch12 = Directory.GetDirectories(recipeDir);
            //_recipeFiles = new string[_recipeInch8.Length + _recipeInch12.Length];
            _recipeFiles = new string[_recipes.Length];
            _recipes.CopyTo(_recipeFiles, 0);
            //_recipeInch12.CopyTo(_recipeFiles, _recipeInch8.Length);
            _recipeFiles = _recipeFiles.ToList().FindAll(x => x .Contains(findStr,StringComparison.OrdinalIgnoreCase)).ToArray();
            _pageCount = _recipeFiles.Length;
            _maxCount = (_pageCount % PAGE_NUMBER) > 0 ? (_pageCount / PAGE_NUMBER) + 1 : (_pageCount / PAGE_NUMBER);
            labelPageCount.Text = "/" + _maxCount.ToString();
        }

        private void LoadRecipe()
        {
            recipeInfo = new List<RecipeInfo>();
            this.Invoke(new Action(() =>
            {
                int id = 0;
                listBoxControl1.Items.Clear();
                int page = PAGE_NUMBER * Convert.ToInt32(textPage.Text) - PAGE_NUMBER;
                int forCount = page + PAGE_NUMBER > _pageCount ? _pageCount : page + PAGE_NUMBER;
                for (int recipeIndex = page; recipeIndex < forCount; recipeIndex++)
                {
                    id++;
                    var recipeName = Path.GetFileName(_recipeFiles[recipeIndex]);
                    var splitors=_recipeFiles[recipeIndex].Split('\\');
                    recipeInfo.Add(new RecipeInfo
                    {
                        Id = id,
                        RecipeName = recipeName,
                        RecipeType = (EnumRecipeType)Enum.Parse(typeof(EnumRecipeType), Path.GetFileName(splitors[splitors.Length-2]))
                    });
                    listBoxControl1.Items.Add(recipeName);
                }
            }));
        }

        private void listBoxControl1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxControl1.SelectedItem!=null)
            {
                RecipeType = recipeInfo.Find(r => r.RecipeName == listBoxControl1.SelectedItem.ToString()).RecipeType;
                _textRecipePath = SystemConfiguration.Instance.SystemDefaultDirectory + string.Format(@"Recipes\{0}\{1}", RecipeType.ToString(), listBoxControl1.SelectedItem.ToString());
            }           
        }

        /// <summary>
        /// 确定选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            var selectRecipe = Path.GetFileName(_textRecipePath);
            if (!string.IsNullOrEmpty(selectRecipe) && true/*必要时增加判断Recipe有效性的逻辑*/)
            {
                SelectedRecipeName = selectRecipe;
                RecipeType = recipeInfo.Find(r => r.RecipeName == selectRecipe).RecipeType;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show("The Recipe Selected is InValid. Please Check it.", "Warning");
            }
        }

        /// <summary>
        /// 取消选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 双击选择确定选择Recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxControl1_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxControl1.SelectedItem != null)
            {
                btnOK.PerformClick();
            }
        }

        private void labHome_Click(object sender, EventArgs e)
        {
            textPage.Text = "1";
            try
            {
                LoadRecipe();
            }
            finally
            {
                base.OnLoad(e);
            }
        }

        private void labUp_Click(object sender, EventArgs e)
        {
            textPage.Text = (Convert.ToInt32(textPage.Text) - 1 < 1 ? 1 : Convert.ToInt32(textPage.Text) - 1).ToString();
            try
            {
                LoadRecipe();
            }
            finally
            {
                base.OnLoad(e);
            }
        }

        private void labGo_Click(object sender, EventArgs e)
        {
            textPage.Text =  Convert.ToInt32(textPage.Text )> _maxCount ?_maxCount.ToString() : textPage.Text ;
            textPage.Text = Convert.ToInt32(textPage.Text) < 1 ? "1" : textPage.Text;
            try
            {
                LoadRecipe();
            }
            finally
            {
                base.OnLoad(e);
            }
        }

        private void LabDown_Click(object sender, EventArgs e)
        {
            textPage.Text = (Convert.ToInt32(textPage.Text) + 1 > _maxCount ? _maxCount : Convert.ToInt32(textPage.Text) + 1).ToString();
            try
            {
                LoadRecipe();
            }
            finally
            {
                base.OnLoad(e);
            }
        }

        private void labEnd_Click(object sender, EventArgs e)
        {
            textPage.Text = _maxCount.ToString();
            try
            {
                LoadRecipe();
            }
            finally
            {
                base.OnLoad(e);
            }
        }


        private void textFind_TextChanged(object sender, EventArgs e)
        {
            textPage.Text = "1";
             try
            {
                FindRecipe(textFind.Text);
                LoadRecipe();
            }
            finally
            {
                base.OnLoad(e);
            } 
        }
        private void FrmRecipeSelect_Shown(object sender, EventArgs e)
        {
            //窗体显式时聚焦光标
            textFind.Focus();
        }
    }

    public class RecipeInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// RecipeName
        /// </summary>
        public string RecipeName { get; set; }

        /// <summary>
        /// WaferSize
        /// </summary>
        //public EnumWaferSize waferSize { get; set; }
        public EnumRecipeType RecipeType { get; set; }
    }
}
