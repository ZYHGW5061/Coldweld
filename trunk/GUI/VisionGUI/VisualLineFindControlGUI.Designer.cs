
namespace VisionGUI
{
    partial class VisualLineFindControlGUI
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
            this.UpEdgePage = new System.Windows.Forms.TabPage();
            this.UpEdgeResizeBtn = new DevExpress.XtraEditors.CheckButton();
            this.UpEdgeMoveBtn = new DevExpress.XtraEditors.CheckButton();
            this.UpEdgeRoiShowBtn = new System.Windows.Forms.CheckBox();
            this.DownEdgePage = new System.Windows.Forms.TabPage();
            this.DownEdgeResizeBtn = new DevExpress.XtraEditors.CheckButton();
            this.DownEdgeMoveBtn = new DevExpress.XtraEditors.CheckButton();
            this.DownEdgeRoiShowBtn = new System.Windows.Forms.CheckBox();
            this.LeftEdgePage = new System.Windows.Forms.TabPage();
            this.LeftEdgeResizeBtn = new DevExpress.XtraEditors.CheckButton();
            this.LeftEdgeMoveBtn = new DevExpress.XtraEditors.CheckButton();
            this.LeftEdgeRoiShowBtn = new System.Windows.Forms.CheckBox();
            this.RightEdgePage = new System.Windows.Forms.TabPage();
            this.RightEdgeResizeBtn = new DevExpress.XtraEditors.CheckButton();
            this.RightEdgeMoveBtn = new DevExpress.XtraEditors.CheckButton();
            this.RightEdgeRoiShowBtn = new System.Windows.Forms.CheckBox();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.CameraWindowClearBtn = new System.Windows.Forms.Button();
            this.TemplateBtn = new System.Windows.Forms.Button();
            this.MinimunqualityNumlabel = new System.Windows.Forms.Label();
            this.DirectLightNumlabel = new System.Windows.Forms.Label();
            this.RingLightNumlabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RingLightBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirectLightBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QualityBar)).BeginInit();
            this.RoiGroupBox.SuspendLayout();
            this.VisualRoiTab.SuspendLayout();
            this.UpEdgePage.SuspendLayout();
            this.DownEdgePage.SuspendLayout();
            this.LeftEdgePage.SuspendLayout();
            this.RightEdgePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // RingLightBar
            // 
            this.RingLightBar.Location = new System.Drawing.Point(16, 41);
            this.RingLightBar.Maximum = 254;
            this.RingLightBar.Name = "RingLightBar";
            this.RingLightBar.Size = new System.Drawing.Size(327, 45);
            this.RingLightBar.TabIndex = 0;
            this.RingLightBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.RingLightBar.Scroll += new System.EventHandler(this.RingLightBar_Scroll);
            // 
            // RingLightlabel
            // 
            this.RingLightlabel.AutoSize = true;
            this.RingLightlabel.Location = new System.Drawing.Point(14, 20);
            this.RingLightlabel.Name = "RingLightlabel";
            this.RingLightlabel.Size = new System.Drawing.Size(57, 14);
            this.RingLightlabel.TabIndex = 1;
            this.RingLightlabel.Text = "RingLight";
            // 
            // DirectLightlabel
            // 
            this.DirectLightlabel.AutoSize = true;
            this.DirectLightlabel.Location = new System.Drawing.Point(14, 92);
            this.DirectLightlabel.Name = "DirectLightlabel";
            this.DirectLightlabel.Size = new System.Drawing.Size(66, 14);
            this.DirectLightlabel.TabIndex = 3;
            this.DirectLightlabel.Text = "DirectLight";
            // 
            // DirectLightBar
            // 
            this.DirectLightBar.Location = new System.Drawing.Point(16, 112);
            this.DirectLightBar.Maximum = 254;
            this.DirectLightBar.Name = "DirectLightBar";
            this.DirectLightBar.Size = new System.Drawing.Size(327, 45);
            this.DirectLightBar.TabIndex = 2;
            this.DirectLightBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.DirectLightBar.Scroll += new System.EventHandler(this.DirectLightBar_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Minimun quality";
            // 
            // QualityBar
            // 
            this.QualityBar.Location = new System.Drawing.Point(16, 183);
            this.QualityBar.Maximum = 100;
            this.QualityBar.Name = "QualityBar";
            this.QualityBar.Size = new System.Drawing.Size(327, 45);
            this.QualityBar.TabIndex = 4;
            this.QualityBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.QualityBar.Value = 90;
            this.QualityBar.Scroll += new System.EventHandler(this.QualityBar_Scroll);
            // 
            // RoiGroupBox
            // 
            this.RoiGroupBox.Controls.Add(this.VisualRoiTab);
            this.RoiGroupBox.Location = new System.Drawing.Point(16, 208);
            this.RoiGroupBox.Name = "RoiGroupBox";
            this.RoiGroupBox.Size = new System.Drawing.Size(308, 187);
            this.RoiGroupBox.TabIndex = 8;
            this.RoiGroupBox.TabStop = false;
            this.RoiGroupBox.Text = "ROI";
            // 
            // VisualRoiTab
            // 
            this.VisualRoiTab.Controls.Add(this.UpEdgePage);
            this.VisualRoiTab.Controls.Add(this.DownEdgePage);
            this.VisualRoiTab.Controls.Add(this.LeftEdgePage);
            this.VisualRoiTab.Controls.Add(this.RightEdgePage);
            this.VisualRoiTab.Location = new System.Drawing.Point(8, 24);
            this.VisualRoiTab.Name = "VisualRoiTab";
            this.VisualRoiTab.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VisualRoiTab.SelectedIndex = 0;
            this.VisualRoiTab.Size = new System.Drawing.Size(293, 155);
            this.VisualRoiTab.TabIndex = 0;
            this.VisualRoiTab.SelectedIndexChanged += new System.EventHandler(this.VisualRoiTab_SelectedIndexChanged);
            this.VisualRoiTab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VisualRoiTab_KeyDown);
            // 
            // UpEdgePage
            // 
            this.UpEdgePage.Controls.Add(this.UpEdgeResizeBtn);
            this.UpEdgePage.Controls.Add(this.UpEdgeMoveBtn);
            this.UpEdgePage.Controls.Add(this.UpEdgeRoiShowBtn);
            this.UpEdgePage.Location = new System.Drawing.Point(4, 23);
            this.UpEdgePage.Name = "UpEdgePage";
            this.UpEdgePage.Padding = new System.Windows.Forms.Padding(3);
            this.UpEdgePage.Size = new System.Drawing.Size(285, 128);
            this.UpEdgePage.TabIndex = 0;
            this.UpEdgePage.Text = "UpEdge";
            this.UpEdgePage.UseVisualStyleBackColor = true;
            // 
            // UpEdgeResizeBtn
            // 
            this.UpEdgeResizeBtn.Location = new System.Drawing.Point(150, 33);
            this.UpEdgeResizeBtn.Name = "UpEdgeResizeBtn";
            this.UpEdgeResizeBtn.Size = new System.Drawing.Size(126, 85);
            this.UpEdgeResizeBtn.TabIndex = 2;
            this.UpEdgeResizeBtn.Text = "Resize";
            this.UpEdgeResizeBtn.CheckedChanged += new System.EventHandler(this.UpEdgeResizeBtn_CheckedChanged);
            // 
            // UpEdgeMoveBtn
            // 
            this.UpEdgeMoveBtn.Location = new System.Drawing.Point(8, 33);
            this.UpEdgeMoveBtn.Name = "UpEdgeMoveBtn";
            this.UpEdgeMoveBtn.Size = new System.Drawing.Size(126, 85);
            this.UpEdgeMoveBtn.TabIndex = 1;
            this.UpEdgeMoveBtn.Text = "Move";
            this.UpEdgeMoveBtn.CheckedChanged += new System.EventHandler(this.UpEdgeMoveBtn_CheckedChanged);
            // 
            // UpEdgeRoiShowBtn
            // 
            this.UpEdgeRoiShowBtn.AutoSize = true;
            this.UpEdgeRoiShowBtn.Location = new System.Drawing.Point(8, 7);
            this.UpEdgeRoiShowBtn.Name = "UpEdgeRoiShowBtn";
            this.UpEdgeRoiShowBtn.Size = new System.Drawing.Size(73, 18);
            this.UpEdgeRoiShowBtn.TabIndex = 0;
            this.UpEdgeRoiShowBtn.Text = "RoiShow";
            this.UpEdgeRoiShowBtn.UseVisualStyleBackColor = true;
            this.UpEdgeRoiShowBtn.CheckedChanged += new System.EventHandler(this.UpEdgeRoiShowBtn_CheckedChanged);
            // 
            // DownEdgePage
            // 
            this.DownEdgePage.Controls.Add(this.DownEdgeResizeBtn);
            this.DownEdgePage.Controls.Add(this.DownEdgeMoveBtn);
            this.DownEdgePage.Controls.Add(this.DownEdgeRoiShowBtn);
            this.DownEdgePage.Location = new System.Drawing.Point(4, 23);
            this.DownEdgePage.Name = "DownEdgePage";
            this.DownEdgePage.Padding = new System.Windows.Forms.Padding(3);
            this.DownEdgePage.Size = new System.Drawing.Size(285, 128);
            this.DownEdgePage.TabIndex = 1;
            this.DownEdgePage.Text = "DownEdge";
            this.DownEdgePage.UseVisualStyleBackColor = true;
            // 
            // DownEdgeResizeBtn
            // 
            this.DownEdgeResizeBtn.Location = new System.Drawing.Point(150, 33);
            this.DownEdgeResizeBtn.Name = "DownEdgeResizeBtn";
            this.DownEdgeResizeBtn.Size = new System.Drawing.Size(126, 85);
            this.DownEdgeResizeBtn.TabIndex = 5;
            this.DownEdgeResizeBtn.Text = "Resize";
            this.DownEdgeResizeBtn.CheckedChanged += new System.EventHandler(this.DownEdgeResizeBtn_CheckedChanged);
            // 
            // DownEdgeMoveBtn
            // 
            this.DownEdgeMoveBtn.Location = new System.Drawing.Point(8, 33);
            this.DownEdgeMoveBtn.Name = "DownEdgeMoveBtn";
            this.DownEdgeMoveBtn.Size = new System.Drawing.Size(126, 85);
            this.DownEdgeMoveBtn.TabIndex = 4;
            this.DownEdgeMoveBtn.Text = "Move";
            this.DownEdgeMoveBtn.CheckedChanged += new System.EventHandler(this.DownEdgeMoveBtn_CheckedChanged);
            // 
            // DownEdgeRoiShowBtn
            // 
            this.DownEdgeRoiShowBtn.AutoSize = true;
            this.DownEdgeRoiShowBtn.Location = new System.Drawing.Point(8, 7);
            this.DownEdgeRoiShowBtn.Name = "DownEdgeRoiShowBtn";
            this.DownEdgeRoiShowBtn.Size = new System.Drawing.Size(73, 18);
            this.DownEdgeRoiShowBtn.TabIndex = 3;
            this.DownEdgeRoiShowBtn.Text = "RoiShow";
            this.DownEdgeRoiShowBtn.UseVisualStyleBackColor = true;
            this.DownEdgeRoiShowBtn.CheckedChanged += new System.EventHandler(this.DownEdgeRoiShowBtn_CheckedChanged);
            // 
            // LeftEdgePage
            // 
            this.LeftEdgePage.Controls.Add(this.LeftEdgeResizeBtn);
            this.LeftEdgePage.Controls.Add(this.LeftEdgeMoveBtn);
            this.LeftEdgePage.Controls.Add(this.LeftEdgeRoiShowBtn);
            this.LeftEdgePage.Location = new System.Drawing.Point(4, 23);
            this.LeftEdgePage.Name = "LeftEdgePage";
            this.LeftEdgePage.Padding = new System.Windows.Forms.Padding(3);
            this.LeftEdgePage.Size = new System.Drawing.Size(285, 128);
            this.LeftEdgePage.TabIndex = 2;
            this.LeftEdgePage.Text = "LeftEdge";
            this.LeftEdgePage.UseVisualStyleBackColor = true;
            // 
            // LeftEdgeResizeBtn
            // 
            this.LeftEdgeResizeBtn.Location = new System.Drawing.Point(150, 33);
            this.LeftEdgeResizeBtn.Name = "LeftEdgeResizeBtn";
            this.LeftEdgeResizeBtn.Size = new System.Drawing.Size(126, 85);
            this.LeftEdgeResizeBtn.TabIndex = 8;
            this.LeftEdgeResizeBtn.Text = "Resize";
            this.LeftEdgeResizeBtn.CheckedChanged += new System.EventHandler(this.LeftEdgeResizeBtn_CheckedChanged);
            // 
            // LeftEdgeMoveBtn
            // 
            this.LeftEdgeMoveBtn.Location = new System.Drawing.Point(8, 33);
            this.LeftEdgeMoveBtn.Name = "LeftEdgeMoveBtn";
            this.LeftEdgeMoveBtn.Size = new System.Drawing.Size(126, 85);
            this.LeftEdgeMoveBtn.TabIndex = 7;
            this.LeftEdgeMoveBtn.Text = "Move";
            this.LeftEdgeMoveBtn.CheckedChanged += new System.EventHandler(this.LeftEdgeMoveBtn_CheckedChanged);
            // 
            // LeftEdgeRoiShowBtn
            // 
            this.LeftEdgeRoiShowBtn.AutoSize = true;
            this.LeftEdgeRoiShowBtn.Location = new System.Drawing.Point(8, 7);
            this.LeftEdgeRoiShowBtn.Name = "LeftEdgeRoiShowBtn";
            this.LeftEdgeRoiShowBtn.Size = new System.Drawing.Size(73, 18);
            this.LeftEdgeRoiShowBtn.TabIndex = 6;
            this.LeftEdgeRoiShowBtn.Text = "RoiShow";
            this.LeftEdgeRoiShowBtn.UseVisualStyleBackColor = true;
            this.LeftEdgeRoiShowBtn.CheckedChanged += new System.EventHandler(this.LeftEdgeRoiShowBtn_CheckedChanged);
            // 
            // RightEdgePage
            // 
            this.RightEdgePage.Controls.Add(this.RightEdgeResizeBtn);
            this.RightEdgePage.Controls.Add(this.RightEdgeMoveBtn);
            this.RightEdgePage.Controls.Add(this.RightEdgeRoiShowBtn);
            this.RightEdgePage.Location = new System.Drawing.Point(4, 23);
            this.RightEdgePage.Name = "RightEdgePage";
            this.RightEdgePage.Padding = new System.Windows.Forms.Padding(3);
            this.RightEdgePage.Size = new System.Drawing.Size(285, 128);
            this.RightEdgePage.TabIndex = 3;
            this.RightEdgePage.Text = "RightEdge";
            this.RightEdgePage.UseVisualStyleBackColor = true;
            // 
            // RightEdgeResizeBtn
            // 
            this.RightEdgeResizeBtn.Location = new System.Drawing.Point(150, 33);
            this.RightEdgeResizeBtn.Name = "RightEdgeResizeBtn";
            this.RightEdgeResizeBtn.Size = new System.Drawing.Size(126, 85);
            this.RightEdgeResizeBtn.TabIndex = 11;
            this.RightEdgeResizeBtn.Text = "Resize";
            this.RightEdgeResizeBtn.CheckedChanged += new System.EventHandler(this.RightEdgeResizeBtn_CheckedChanged);
            // 
            // RightEdgeMoveBtn
            // 
            this.RightEdgeMoveBtn.Location = new System.Drawing.Point(8, 33);
            this.RightEdgeMoveBtn.Name = "RightEdgeMoveBtn";
            this.RightEdgeMoveBtn.Size = new System.Drawing.Size(126, 85);
            this.RightEdgeMoveBtn.TabIndex = 10;
            this.RightEdgeMoveBtn.Text = "Move";
            this.RightEdgeMoveBtn.CheckedChanged += new System.EventHandler(this.RightEdgeMoveBtn_CheckedChanged);
            // 
            // RightEdgeRoiShowBtn
            // 
            this.RightEdgeRoiShowBtn.AutoSize = true;
            this.RightEdgeRoiShowBtn.Location = new System.Drawing.Point(8, 7);
            this.RightEdgeRoiShowBtn.Name = "RightEdgeRoiShowBtn";
            this.RightEdgeRoiShowBtn.Size = new System.Drawing.Size(73, 18);
            this.RightEdgeRoiShowBtn.TabIndex = 9;
            this.RightEdgeRoiShowBtn.Text = "RoiShow";
            this.RightEdgeRoiShowBtn.UseVisualStyleBackColor = true;
            this.RightEdgeRoiShowBtn.CheckedChanged += new System.EventHandler(this.RightEdgeRoiShowBtn_CheckedChanged);
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(19, 454);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(300, 36);
            this.SearchBtn.TabIndex = 10;
            this.SearchBtn.Text = "Search";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // CameraWindowClearBtn
            // 
            this.CameraWindowClearBtn.Location = new System.Drawing.Point(19, 502);
            this.CameraWindowClearBtn.Name = "CameraWindowClearBtn";
            this.CameraWindowClearBtn.Size = new System.Drawing.Size(300, 36);
            this.CameraWindowClearBtn.TabIndex = 11;
            this.CameraWindowClearBtn.Text = "Clear";
            this.CameraWindowClearBtn.UseVisualStyleBackColor = true;
            this.CameraWindowClearBtn.Click += new System.EventHandler(this.CameraWindowClearBtn_Click);
            // 
            // TemplateBtn
            // 
            this.TemplateBtn.Location = new System.Drawing.Point(19, 406);
            this.TemplateBtn.Name = "TemplateBtn";
            this.TemplateBtn.Size = new System.Drawing.Size(300, 36);
            this.TemplateBtn.TabIndex = 9;
            this.TemplateBtn.Text = "Template";
            this.TemplateBtn.UseVisualStyleBackColor = true;
            this.TemplateBtn.Click += new System.EventHandler(this.TemplateBtn_ClickAsync);
            // 
            // MinimunqualityNumlabel
            // 
            this.MinimunqualityNumlabel.AutoSize = true;
            this.MinimunqualityNumlabel.Location = new System.Drawing.Point(301, 164);
            this.MinimunqualityNumlabel.Name = "MinimunqualityNumlabel";
            this.MinimunqualityNumlabel.Size = new System.Drawing.Size(25, 14);
            this.MinimunqualityNumlabel.TabIndex = 17;
            this.MinimunqualityNumlabel.Text = "0.9";
            // 
            // DirectLightNumlabel
            // 
            this.DirectLightNumlabel.AutoSize = true;
            this.DirectLightNumlabel.Location = new System.Drawing.Point(301, 92);
            this.DirectLightNumlabel.Name = "DirectLightNumlabel";
            this.DirectLightNumlabel.Size = new System.Drawing.Size(14, 14);
            this.DirectLightNumlabel.TabIndex = 16;
            this.DirectLightNumlabel.Text = "0";
            // 
            // RingLightNumlabel
            // 
            this.RingLightNumlabel.AutoSize = true;
            this.RingLightNumlabel.Location = new System.Drawing.Point(301, 20);
            this.RingLightNumlabel.Name = "RingLightNumlabel";
            this.RingLightNumlabel.Size = new System.Drawing.Size(14, 14);
            this.RingLightNumlabel.TabIndex = 15;
            this.RingLightNumlabel.Text = "0";
            // 
            // VisualLineFindControlGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MinimunqualityNumlabel);
            this.Controls.Add(this.DirectLightNumlabel);
            this.Controls.Add(this.RingLightNumlabel);
            this.Controls.Add(this.CameraWindowClearBtn);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.TemplateBtn);
            this.Controls.Add(this.RoiGroupBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.QualityBar);
            this.Controls.Add(this.DirectLightlabel);
            this.Controls.Add(this.DirectLightBar);
            this.Controls.Add(this.RingLightlabel);
            this.Controls.Add(this.RingLightBar);
            this.Name = "VisualLineFindControlGUI";
            this.Size = new System.Drawing.Size(344, 558);
            ((System.ComponentModel.ISupportInitialize)(this.RingLightBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirectLightBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QualityBar)).EndInit();
            this.RoiGroupBox.ResumeLayout(false);
            this.VisualRoiTab.ResumeLayout(false);
            this.UpEdgePage.ResumeLayout(false);
            this.UpEdgePage.PerformLayout();
            this.DownEdgePage.ResumeLayout(false);
            this.DownEdgePage.PerformLayout();
            this.LeftEdgePage.ResumeLayout(false);
            this.LeftEdgePage.PerformLayout();
            this.RightEdgePage.ResumeLayout(false);
            this.RightEdgePage.PerformLayout();
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
        private System.Windows.Forms.TabPage UpEdgePage;
        private System.Windows.Forms.TabPage DownEdgePage;
        private DevExpress.XtraEditors.CheckButton UpEdgeMoveBtn;
        private System.Windows.Forms.CheckBox UpEdgeRoiShowBtn;
        private DevExpress.XtraEditors.CheckButton UpEdgeResizeBtn;
        private DevExpress.XtraEditors.CheckButton DownEdgeResizeBtn;
        private DevExpress.XtraEditors.CheckButton DownEdgeMoveBtn;
        private System.Windows.Forms.CheckBox DownEdgeRoiShowBtn;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Button CameraWindowClearBtn;
        private System.Windows.Forms.TabPage LeftEdgePage;
        private DevExpress.XtraEditors.CheckButton LeftEdgeResizeBtn;
        private DevExpress.XtraEditors.CheckButton LeftEdgeMoveBtn;
        private System.Windows.Forms.CheckBox LeftEdgeRoiShowBtn;
        private System.Windows.Forms.TabPage RightEdgePage;
        private DevExpress.XtraEditors.CheckButton RightEdgeResizeBtn;
        private DevExpress.XtraEditors.CheckButton RightEdgeMoveBtn;
        private System.Windows.Forms.CheckBox RightEdgeRoiShowBtn;
        private System.Windows.Forms.Button TemplateBtn;
        private System.Windows.Forms.Label MinimunqualityNumlabel;
        private System.Windows.Forms.Label DirectLightNumlabel;
        private System.Windows.Forms.Label RingLightNumlabel;
    }
}
