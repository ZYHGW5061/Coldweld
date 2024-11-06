
namespace VisionGUI
{
    partial class VisualCircleFindControlGUI
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.RingLightBar = new System.Windows.Forms.TrackBar();
            this.RingLightlabel = new System.Windows.Forms.Label();
            this.DirectLightlabel = new System.Windows.Forms.Label();
            this.DirectLightBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.QualityBar = new System.Windows.Forms.TrackBar();
            this.RoiGroupBox = new System.Windows.Forms.GroupBox();
            this.VisualRoiTab = new System.Windows.Forms.TabControl();
            this.TemplatePage = new System.Windows.Forms.TabPage();
            this.TemplateResizeBtn = new DevExpress.XtraEditors.CheckButton();
            this.TemplateMoveBtn = new DevExpress.XtraEditors.CheckButton();
            this.TemplateRoiShowBtn = new System.Windows.Forms.CheckBox();
            this.SearchAreaPage = new System.Windows.Forms.TabPage();
            this.SearchAreaResizeBtn = new DevExpress.XtraEditors.CheckButton();
            this.SearchAreaMoveBtn = new DevExpress.XtraEditors.CheckButton();
            this.SearchAreaRoiShowBtn = new System.Windows.Forms.CheckBox();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.CameraWindowClearBtn = new System.Windows.Forms.Button();
            this.MinimunqualityNumlabel = new System.Windows.Forms.Label();
            this.DirectLightNumlabel = new System.Windows.Forms.Label();
            this.RingLightNumlabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RingLightBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirectLightBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QualityBar)).BeginInit();
            this.RoiGroupBox.SuspendLayout();
            this.VisualRoiTab.SuspendLayout();
            this.TemplatePage.SuspendLayout();
            this.SearchAreaPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // RingLightBar
            // 
            this.RingLightBar.Location = new System.Drawing.Point(14, 35);
            this.RingLightBar.Maximum = 254;
            this.RingLightBar.Name = "RingLightBar";
            this.RingLightBar.Size = new System.Drawing.Size(280, 45);
            this.RingLightBar.TabIndex = 0;
            this.RingLightBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.RingLightBar.Scroll += new System.EventHandler(this.RingLightBar_Scroll);
            // 
            // RingLightlabel
            // 
            this.RingLightlabel.AutoSize = true;
            this.RingLightlabel.Location = new System.Drawing.Point(12, 17);
            this.RingLightlabel.Name = "RingLightlabel";
            this.RingLightlabel.Size = new System.Drawing.Size(59, 12);
            this.RingLightlabel.TabIndex = 1;
            this.RingLightlabel.Text = "RingLight";
            // 
            // DirectLightlabel
            // 
            this.DirectLightlabel.AutoSize = true;
            this.DirectLightlabel.Location = new System.Drawing.Point(12, 79);
            this.DirectLightlabel.Name = "DirectLightlabel";
            this.DirectLightlabel.Size = new System.Drawing.Size(71, 12);
            this.DirectLightlabel.TabIndex = 3;
            this.DirectLightlabel.Text = "DirectLight";
            // 
            // DirectLightBar
            // 
            this.DirectLightBar.Location = new System.Drawing.Point(14, 96);
            this.DirectLightBar.Maximum = 254;
            this.DirectLightBar.Name = "DirectLightBar";
            this.DirectLightBar.Size = new System.Drawing.Size(280, 45);
            this.DirectLightBar.TabIndex = 2;
            this.DirectLightBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.DirectLightBar.Scroll += new System.EventHandler(this.DirectLightBar_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Minimun quality";
            // 
            // QualityBar
            // 
            this.QualityBar.Location = new System.Drawing.Point(14, 157);
            this.QualityBar.Maximum = 100;
            this.QualityBar.Name = "QualityBar";
            this.QualityBar.Size = new System.Drawing.Size(280, 45);
            this.QualityBar.TabIndex = 4;
            this.QualityBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.QualityBar.Value = 50;
            this.QualityBar.Scroll += new System.EventHandler(this.QualityBar_Scroll);
            // 
            // RoiGroupBox
            // 
            this.RoiGroupBox.Controls.Add(this.VisualRoiTab);
            this.RoiGroupBox.Location = new System.Drawing.Point(14, 263);
            this.RoiGroupBox.Name = "RoiGroupBox";
            this.RoiGroupBox.Size = new System.Drawing.Size(264, 160);
            this.RoiGroupBox.TabIndex = 8;
            this.RoiGroupBox.TabStop = false;
            this.RoiGroupBox.Text = "ROI";
            // 
            // VisualRoiTab
            // 
            this.VisualRoiTab.Controls.Add(this.TemplatePage);
            this.VisualRoiTab.Controls.Add(this.SearchAreaPage);
            this.VisualRoiTab.Location = new System.Drawing.Point(7, 21);
            this.VisualRoiTab.Name = "VisualRoiTab";
            this.VisualRoiTab.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VisualRoiTab.SelectedIndex = 0;
            this.VisualRoiTab.Size = new System.Drawing.Size(251, 133);
            this.VisualRoiTab.TabIndex = 0;
            this.VisualRoiTab.SelectedIndexChanged += new System.EventHandler(this.VisualRoiTab_SelectedIndexChanged);
            this.VisualRoiTab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VisualRoiTab_KeyDown);
            // 
            // TemplatePage
            // 
            this.TemplatePage.Controls.Add(this.TemplateResizeBtn);
            this.TemplatePage.Controls.Add(this.TemplateMoveBtn);
            this.TemplatePage.Controls.Add(this.TemplateRoiShowBtn);
            this.TemplatePage.Location = new System.Drawing.Point(4, 22);
            this.TemplatePage.Name = "TemplatePage";
            this.TemplatePage.Padding = new System.Windows.Forms.Padding(3);
            this.TemplatePage.Size = new System.Drawing.Size(243, 107);
            this.TemplatePage.TabIndex = 0;
            this.TemplatePage.Text = "Circle";
            this.TemplatePage.UseVisualStyleBackColor = true;
            // 
            // TemplateResizeBtn
            // 
            this.TemplateResizeBtn.Location = new System.Drawing.Point(129, 28);
            this.TemplateResizeBtn.Name = "TemplateResizeBtn";
            this.TemplateResizeBtn.Size = new System.Drawing.Size(108, 73);
            this.TemplateResizeBtn.TabIndex = 2;
            this.TemplateResizeBtn.Text = "Resize";
            this.TemplateResizeBtn.CheckedChanged += new System.EventHandler(this.TemplateResizeBtn_CheckedChanged);
            // 
            // TemplateMoveBtn
            // 
            this.TemplateMoveBtn.Location = new System.Drawing.Point(7, 28);
            this.TemplateMoveBtn.Name = "TemplateMoveBtn";
            this.TemplateMoveBtn.Size = new System.Drawing.Size(108, 73);
            this.TemplateMoveBtn.TabIndex = 1;
            this.TemplateMoveBtn.Text = "Move";
            this.TemplateMoveBtn.CheckedChanged += new System.EventHandler(this.TemplateMoveBtn_CheckedChanged);
            // 
            // TemplateRoiShowBtn
            // 
            this.TemplateRoiShowBtn.AutoSize = true;
            this.TemplateRoiShowBtn.Location = new System.Drawing.Point(7, 6);
            this.TemplateRoiShowBtn.Name = "TemplateRoiShowBtn";
            this.TemplateRoiShowBtn.Size = new System.Drawing.Size(66, 16);
            this.TemplateRoiShowBtn.TabIndex = 0;
            this.TemplateRoiShowBtn.Text = "RoiShow";
            this.TemplateRoiShowBtn.UseVisualStyleBackColor = true;
            this.TemplateRoiShowBtn.CheckedChanged += new System.EventHandler(this.TemplateRoiShowBtn_CheckedChanged);
            // 
            // SearchAreaPage
            // 
            this.SearchAreaPage.Controls.Add(this.SearchAreaResizeBtn);
            this.SearchAreaPage.Controls.Add(this.SearchAreaMoveBtn);
            this.SearchAreaPage.Controls.Add(this.SearchAreaRoiShowBtn);
            this.SearchAreaPage.Location = new System.Drawing.Point(4, 22);
            this.SearchAreaPage.Name = "SearchAreaPage";
            this.SearchAreaPage.Padding = new System.Windows.Forms.Padding(3);
            this.SearchAreaPage.Size = new System.Drawing.Size(243, 107);
            this.SearchAreaPage.TabIndex = 1;
            this.SearchAreaPage.Text = "SearchArea";
            this.SearchAreaPage.UseVisualStyleBackColor = true;
            // 
            // SearchAreaResizeBtn
            // 
            this.SearchAreaResizeBtn.Location = new System.Drawing.Point(129, 28);
            this.SearchAreaResizeBtn.Name = "SearchAreaResizeBtn";
            this.SearchAreaResizeBtn.Size = new System.Drawing.Size(108, 73);
            this.SearchAreaResizeBtn.TabIndex = 5;
            this.SearchAreaResizeBtn.Text = "Resize";
            this.SearchAreaResizeBtn.CheckedChanged += new System.EventHandler(this.SearchAreaResizeBtn_CheckedChanged);
            // 
            // SearchAreaMoveBtn
            // 
            this.SearchAreaMoveBtn.Location = new System.Drawing.Point(7, 28);
            this.SearchAreaMoveBtn.Name = "SearchAreaMoveBtn";
            this.SearchAreaMoveBtn.Size = new System.Drawing.Size(108, 73);
            this.SearchAreaMoveBtn.TabIndex = 4;
            this.SearchAreaMoveBtn.Text = "Move";
            this.SearchAreaMoveBtn.CheckedChanged += new System.EventHandler(this.SearchAreaMoveBtn_CheckedChanged);
            // 
            // SearchAreaRoiShowBtn
            // 
            this.SearchAreaRoiShowBtn.AutoSize = true;
            this.SearchAreaRoiShowBtn.Location = new System.Drawing.Point(7, 6);
            this.SearchAreaRoiShowBtn.Name = "SearchAreaRoiShowBtn";
            this.SearchAreaRoiShowBtn.Size = new System.Drawing.Size(66, 16);
            this.SearchAreaRoiShowBtn.TabIndex = 3;
            this.SearchAreaRoiShowBtn.Text = "RoiShow";
            this.SearchAreaRoiShowBtn.UseVisualStyleBackColor = true;
            this.SearchAreaRoiShowBtn.CheckedChanged += new System.EventHandler(this.SearchAreaRoiShowBtn_CheckedChanged);
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(21, 484);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(257, 53);
            this.SearchBtn.TabIndex = 10;
            this.SearchBtn.Text = "Search";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // CameraWindowClearBtn
            // 
            this.CameraWindowClearBtn.Location = new System.Drawing.Point(21, 543);
            this.CameraWindowClearBtn.Name = "CameraWindowClearBtn";
            this.CameraWindowClearBtn.Size = new System.Drawing.Size(257, 53);
            this.CameraWindowClearBtn.TabIndex = 11;
            this.CameraWindowClearBtn.Text = "Clear";
            this.CameraWindowClearBtn.UseVisualStyleBackColor = true;
            this.CameraWindowClearBtn.Click += new System.EventHandler(this.CameraWindowClearBtn_Click);
            // 
            // MinimunqualityNumlabel
            // 
            this.MinimunqualityNumlabel.AutoSize = true;
            this.MinimunqualityNumlabel.Location = new System.Drawing.Point(258, 141);
            this.MinimunqualityNumlabel.Name = "MinimunqualityNumlabel";
            this.MinimunqualityNumlabel.Size = new System.Drawing.Size(23, 12);
            this.MinimunqualityNumlabel.TabIndex = 20;
            this.MinimunqualityNumlabel.Text = "0.9";
            // 
            // DirectLightNumlabel
            // 
            this.DirectLightNumlabel.AutoSize = true;
            this.DirectLightNumlabel.Location = new System.Drawing.Point(258, 79);
            this.DirectLightNumlabel.Name = "DirectLightNumlabel";
            this.DirectLightNumlabel.Size = new System.Drawing.Size(11, 12);
            this.DirectLightNumlabel.TabIndex = 19;
            this.DirectLightNumlabel.Text = "0";
            // 
            // RingLightNumlabel
            // 
            this.RingLightNumlabel.AutoSize = true;
            this.RingLightNumlabel.Location = new System.Drawing.Point(258, 17);
            this.RingLightNumlabel.Name = "RingLightNumlabel";
            this.RingLightNumlabel.Size = new System.Drawing.Size(11, 12);
            this.RingLightNumlabel.TabIndex = 18;
            this.RingLightNumlabel.Text = "0";
            // 
            // VisualCircleFindControlGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MinimunqualityNumlabel);
            this.Controls.Add(this.DirectLightNumlabel);
            this.Controls.Add(this.RingLightNumlabel);
            this.Controls.Add(this.CameraWindowClearBtn);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.RoiGroupBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.QualityBar);
            this.Controls.Add(this.DirectLightlabel);
            this.Controls.Add(this.DirectLightBar);
            this.Controls.Add(this.RingLightlabel);
            this.Controls.Add(this.RingLightBar);
            this.Name = "VisualCircleFindControlGUI";
            this.Size = new System.Drawing.Size(299, 599);
            ((System.ComponentModel.ISupportInitialize)(this.RingLightBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirectLightBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QualityBar)).EndInit();
            this.RoiGroupBox.ResumeLayout(false);
            this.VisualRoiTab.ResumeLayout(false);
            this.TemplatePage.ResumeLayout(false);
            this.TemplatePage.PerformLayout();
            this.SearchAreaPage.ResumeLayout(false);
            this.SearchAreaPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar RingLightBar;
        private System.Windows.Forms.Label RingLightlabel;
        private System.Windows.Forms.Label DirectLightlabel;
        private System.Windows.Forms.TrackBar DirectLightBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar QualityBar;
        private System.Windows.Forms.GroupBox RoiGroupBox;
        private System.Windows.Forms.TabControl VisualRoiTab;
        private System.Windows.Forms.TabPage TemplatePage;
        private System.Windows.Forms.TabPage SearchAreaPage;
        private DevExpress.XtraEditors.CheckButton TemplateMoveBtn;
        private System.Windows.Forms.CheckBox TemplateRoiShowBtn;
        private DevExpress.XtraEditors.CheckButton TemplateResizeBtn;
        private DevExpress.XtraEditors.CheckButton SearchAreaResizeBtn;
        private DevExpress.XtraEditors.CheckButton SearchAreaMoveBtn;
        private System.Windows.Forms.CheckBox SearchAreaRoiShowBtn;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Button CameraWindowClearBtn;
        private System.Windows.Forms.Label MinimunqualityNumlabel;
        private System.Windows.Forms.Label DirectLightNumlabel;
        private System.Windows.Forms.Label RingLightNumlabel;
    }
}
