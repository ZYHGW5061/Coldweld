using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionGUI
{
    public partial class VisualControlForm : Form
    {
        public event Action<string> OnButtonClicked;

        public VisualControlForm()
        {
            InitializeComponent();

        }


        string theClickButton = "cancel";
        //private GUIClassLibrary.VisualControlGUI visualControlGUI1;

        UserControl visualMatchControlGUI1;

        public VisualControlForm(UserControl visualControlGUI)
        {
            InitializeComponent();

            

            InitializeGui(visualControlGUI);


        }

        public void InitializeGui(UserControl visualControlGUI)
        {
            this.VisualtabPage = new System.Windows.Forms.TabPage();
            // 
            // VisualTabControl
            // 
            this.VisualTabControl.Location = new System.Drawing.Point(0, 0);
            this.VisualTabControl.Name = "VisualTabControl";
            this.VisualTabControl.SelectedIndex = 0;
            this.VisualTabControl.Size = new System.Drawing.Size(200, 100);
            this.VisualTabControl.TabIndex = 4;
            this.VisualTabControl.Visible = true;
            // 
            // VisualtabPage
            // 
            this.VisualtabPage.Location = new System.Drawing.Point(0, 0);
            this.VisualtabPage.Name = "VisualtabPage";
            this.VisualtabPage.Size = new System.Drawing.Size(200, 100);
            this.VisualtabPage.TabIndex = 0;
            // 
            // StageMovetabPage
            // 
            this.StageMovetabPage.Location = new System.Drawing.Point(0, 0);
            this.StageMovetabPage.Name = "StageMovetabPage";
            this.StageMovetabPage.Size = new System.Drawing.Size(200, 100);
            this.StageMovetabPage.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(6, 13);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(381, 71);
            this.textBox1.TabIndex = 1;
            // 
            // NextBtn
            // 
            this.NextBtn.Location = new System.Drawing.Point(10, 732);
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.Size = new System.Drawing.Size(115, 36);
            this.NextBtn.TabIndex = 2;
            this.NextBtn.Text = "下一步";
            this.NextBtn.UseVisualStyleBackColor = true;
            this.NextBtn.Click += new System.EventHandler(this.NextBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(272, 732);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(115, 36);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // stageQuickMove1
            // 
            this.stageQuickMove1.Location = new System.Drawing.Point(0, 0);
            this.stageQuickMove1.Name = "stageQuickMove1";
            this.stageQuickMove1.PositiveQucikMoveAct = null;
            this.stageQuickMove1.SelectedAxisSystem = GlobalDataDefineClsLib.EnumSystemAxis.XY;
            this.stageQuickMove1.SelectedStageSystem = GlobalDataDefineClsLib.EnumStageSystem.MaterialboxHook;
            this.stageQuickMove1.Size = new System.Drawing.Size(269, 317);
            this.stageQuickMove1.TabIndex = 0;

            // 
            // visualMatchControlGUI1
            // 
            visualControlGUI.Location = new System.Drawing.Point(8, 6);
            visualControlGUI.Name = "visualMatchControlGUI1";
            visualControlGUI.Size = new System.Drawing.Size(360, 599);
            visualControlGUI.TabIndex = 0;

            this.visualMatchControlGUI1 = visualControlGUI;

            // 
            // VisualTabControl
            // 
            this.VisualTabControl.Controls.Add(this.VisualtabPage);
            this.VisualTabControl.Controls.Add(this.StageMovetabPage);
            this.VisualTabControl.Location = new System.Drawing.Point(6, 90);
            this.VisualTabControl.Name = "VisualTabControl";
            this.VisualTabControl.Padding = new System.Drawing.Point(62, 3);
            this.VisualTabControl.SelectedIndex = 0;
            this.VisualTabControl.Size = new System.Drawing.Size(380, 640);
            this.VisualTabControl.TabIndex = 0;
            // 
            // VisualtabPage
            // 
            this.VisualtabPage.BackColor = System.Drawing.SystemColors.Control;
            this.VisualtabPage.Controls.Add(this.visualMatchControlGUI1);
            this.VisualtabPage.Location = new System.Drawing.Point(4, 22);
            this.VisualtabPage.Name = "VisualtabPage";
            this.VisualtabPage.Padding = new System.Windows.Forms.Padding(3);
            this.VisualtabPage.Size = new System.Drawing.Size(372, 614);
            this.VisualtabPage.TabIndex = 0;
            this.VisualtabPage.Text = "识别";
            // 
            // StageMovetabPage
            // 
            this.StageMovetabPage.BackColor = System.Drawing.SystemColors.Control;
            this.StageMovetabPage.Controls.Add(this.stageQuickMove1);
            this.StageMovetabPage.Location = new System.Drawing.Point(4, 22);
            this.StageMovetabPage.Name = "StageMovetabPage";
            this.StageMovetabPage.Padding = new System.Windows.Forms.Padding(3);
            this.StageMovetabPage.Size = new System.Drawing.Size(372, 614);
            this.StageMovetabPage.TabIndex = 1;
            this.StageMovetabPage.Text = "移动";

            // 
            // stageQuickMove1
            // 
            this.stageQuickMove1.Location = new System.Drawing.Point(25, 6);
            this.stageQuickMove1.Name = "stageQuickMove1";
            this.stageQuickMove1.PositiveQucikMoveAct = null;
            this.stageQuickMove1.SelectedAxisSystem = GlobalDataDefineClsLib.EnumSystemAxis.XY;
            this.stageQuickMove1.SelectedStageSystem = GlobalDataDefineClsLib.EnumStageSystem.MaterialboxHook;
            this.stageQuickMove1.Size = new System.Drawing.Size(269, 317);
            this.stageQuickMove1.TabIndex = 0;




            //this.Controls.Add(visualControlGUI);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.NextBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.VisualTabControl);
        }

        public void SetText(string title)
        {
            this.textBox1.Text = title;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if(theClickButton == "confirm")
            {
                OnButtonClicked?.Invoke("confirm");
            }
            else
            {
                OnButtonClicked?.Invoke("cancel");
            }
            
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (theClickButton == "confirm")
            {
                OnButtonClicked?.Invoke("confirm");
            }
            else
            {
                OnButtonClicked?.Invoke("cancel");
            }
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            OnButtonClicked?.Invoke("confirm");
            theClickButton = "confirm";
            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            OnButtonClicked?.Invoke("cancel");
            theClickButton = "cancel";
            this.Close();
        }

        public void showMessage(string Name, string title,bool ShowBtn)
        {
            this.Text = Name;
            this.textBox1.Text = title;
            this.NextBtn.Visible = ShowBtn;
            this.CancelBtn.Visible = ShowBtn;
            if(ShowBtn)
            {
                this.Show();
            }
            else
            {
                this.Show();
            }
            
            //return theClickButton;
        }

        public void FormShow(string Name, string title, bool ShowBtn, Action<int> callback, Point? location = null)
        {

            if (location.HasValue)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = location.Value;
            }

            //this.OnButtonClicked += (result) =>
            //{
            //    int output = result == "confirm" ? 1 : 0;
            //    callback?.Invoke(output);
            //};
            showMessage(Name, title, ShowBtn);
        }
    }
}
