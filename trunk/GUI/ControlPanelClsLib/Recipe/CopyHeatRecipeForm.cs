using DevExpress.XtraEditors;
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

namespace ControlPanelClsLib.Recipe
{
    public partial class CopyHeatRecipeForm : XtraForm
    {
        /// <summary>
        /// 获取输入的Recipe的名称
        /// </summary>
        public string RecipeName
        {
            get
            {
                return GetValidFileName(textEditeRecipeName.Text);
            }
            set 
            {
                this.textEditeRecipeName.Text = value;
            }
        }

        /// <summary>
        /// 页面构造函数，新建Recipe
        /// </summary>
        public CopyHeatRecipeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 输入完毕，点击确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textEditeRecipeName.Text))
            {
                XtraMessageBox.Show("Please enter the recipe name");
                return;
            }
            if (textEditeRecipeName.Text.Trim() == string.Empty)
            {
                XtraMessageBox.Show("Recipe name can not be blank space.");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 取消新建Recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 自动过滤字符串中非法的字符
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetValidFileName(string fileName)
        {
            StringBuilder fileNameBuilder = new StringBuilder(fileName);
            foreach (char inValidChar in Path.GetInvalidFileNameChars())
            {
                fileNameBuilder.Replace(inValidChar.ToString(), string.Empty);
            }
            return fileNameBuilder.ToString();
        }
    }
}
