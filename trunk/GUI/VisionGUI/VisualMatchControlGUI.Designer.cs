
namespace VisionGUI
{
    partial class VisualMatchControlGUI
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
            this.label1 = new System.Windows.Forms.Label();
            this.AngleBar = new System.Windows.Forms.TrackBar();
            this.RoiGroupBox = new System.Windows.Forms.GroupBox();
            this.VisualRoiTab = new System.Windows.Forms.TabControl();
            this.TemplatePage = new System.Windows.Forms.TabPage();
            this.SetBenchmarkBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BenchmarkYtextBox = new System.Windows.Forms.TextBox();
            this.BenchmarkXtextBox = new System.Windows.Forms.TextBox();
            this.TemplateResizeBtn = new DevExpress.XtraEditors.CheckButton();
            this.TemplateMoveBtn = new DevExpress.XtraEditors.CheckButton();
            this.TemplateRoiShowBtn = new System.Windows.Forms.CheckBox();
            this.SearchAreaPage = new System.Windows.Forms.TabPage();
            this.SearchAreaResizeBtn = new DevExpress.XtraEditors.CheckButton();
            this.SearchAreaMoveBtn = new DevExpress.XtraEditors.CheckButton();
            this.SearchAreaRoiShowBtn = new System.Windows.Forms.CheckBox();
            this.TemplateParamPage = new System.Windows.Forms.TabPage();
            this.textBoxMinChain = new DevExpress.XtraEditors.SpinEdit();
            this.textBoxThresValue = new DevExpress.XtraEditors.SpinEdit();
            this.textBoxFineValue = new DevExpress.XtraEditors.SpinEdit();
            this.textBoxRoughValue = new DevExpress.XtraEditors.SpinEdit();
            this.comboBoxChainFlag = new System.Windows.Forms.ComboBox();
            this.comboBoxWeigthMaskFlag = new System.Windows.Forms.ComboBox();
            this.comboBoxThresFlag = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxGranFlag = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.RunParamPage = new System.Windows.Forms.TabPage();
            this.comboBoxSortType = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textMaxMatchNum = new DevExpress.XtraEditors.SpinEdit();
            this.label14 = new System.Windows.Forms.Label();
            this.textMinScore = new DevExpress.XtraEditors.SpinEdit();
            this.label13 = new System.Windows.Forms.Label();
            this.TemplateBtn = new System.Windows.Forms.Button();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.CameraWindowClearBtn = new System.Windows.Forms.Button();
            this.RingLightNumlabel = new System.Windows.Forms.Label();
            this.DirectLightNumlabel = new System.Windows.Forms.Label();
            this.MinimunqualityNumlabel = new System.Windows.Forms.Label();
            this.AngleDeviationNumlabel = new System.Windows.Forms.Label();
            this.panelStepOperate = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.RingLightBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirectLightBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QualityBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleBar)).BeginInit();
            this.RoiGroupBox.SuspendLayout();
            this.VisualRoiTab.SuspendLayout();
            this.TemplatePage.SuspendLayout();
            this.SearchAreaPage.SuspendLayout();
            this.TemplateParamPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxMinChain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxThresValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFineValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxRoughValue.Properties)).BeginInit();
            this.RunParamPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textMaxMatchNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textMinScore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelStepOperate)).BeginInit();
            this.panelStepOperate.SuspendLayout();
            this.SuspendLayout();
            // 
            // RingLightBar
            // 
            this.RingLightBar.Location = new System.Drawing.Point(19, 39);
            this.RingLightBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RingLightBar.Maximum = 254;
            this.RingLightBar.Name = "RingLightBar";
            this.RingLightBar.Size = new System.Drawing.Size(357, 56);
            this.RingLightBar.TabIndex = 0;
            this.RingLightBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.RingLightBar.Scroll += new System.EventHandler(this.RingLightBar_Scroll);
            // 
            // RingLightlabel
            // 
            this.RingLightlabel.AutoSize = true;
            this.RingLightlabel.Location = new System.Drawing.Point(17, 12);
            this.RingLightlabel.Name = "RingLightlabel";
            this.RingLightlabel.Size = new System.Drawing.Size(53, 18);
            this.RingLightlabel.TabIndex = 1;
            this.RingLightlabel.Text = "环光源";
            // 
            // DirectLightlabel
            // 
            this.DirectLightlabel.AutoSize = true;
            this.DirectLightlabel.Location = new System.Drawing.Point(17, 82);
            this.DirectLightlabel.Name = "DirectLightlabel";
            this.DirectLightlabel.Size = new System.Drawing.Size(53, 18);
            this.DirectLightlabel.TabIndex = 3;
            this.DirectLightlabel.Text = "点光源";
            // 
            // DirectLightBar
            // 
            this.DirectLightBar.Location = new System.Drawing.Point(19, 108);
            this.DirectLightBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DirectLightBar.Maximum = 254;
            this.DirectLightBar.Name = "DirectLightBar";
            this.DirectLightBar.Size = new System.Drawing.Size(357, 56);
            this.DirectLightBar.TabIndex = 2;
            this.DirectLightBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.DirectLightBar.Scroll += new System.EventHandler(this.DirectLightBar_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "分数阈值";
            // 
            // QualityBar
            // 
            this.QualityBar.Location = new System.Drawing.Point(19, 184);
            this.QualityBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.QualityBar.Maximum = 100;
            this.QualityBar.Name = "QualityBar";
            this.QualityBar.Size = new System.Drawing.Size(357, 56);
            this.QualityBar.TabIndex = 4;
            this.QualityBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.QualityBar.Value = 90;
            this.QualityBar.Scroll += new System.EventHandler(this.QualityBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "角度范围";
            // 
            // AngleBar
            // 
            this.AngleBar.Location = new System.Drawing.Point(19, 255);
            this.AngleBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AngleBar.Maximum = 180;
            this.AngleBar.Name = "AngleBar";
            this.AngleBar.Size = new System.Drawing.Size(357, 56);
            this.AngleBar.TabIndex = 6;
            this.AngleBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.AngleBar.Value = 15;
            this.AngleBar.Scroll += new System.EventHandler(this.AngleBar_Scroll);
            // 
            // RoiGroupBox
            // 
            this.RoiGroupBox.Controls.Add(this.VisualRoiTab);
            this.RoiGroupBox.Location = new System.Drawing.Point(24, 291);
            this.RoiGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RoiGroupBox.Name = "RoiGroupBox";
            this.RoiGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RoiGroupBox.Size = new System.Drawing.Size(352, 240);
            this.RoiGroupBox.TabIndex = 8;
            this.RoiGroupBox.TabStop = false;
            this.RoiGroupBox.Text = "ROI";
            // 
            // VisualRoiTab
            // 
            this.VisualRoiTab.Controls.Add(this.TemplatePage);
            this.VisualRoiTab.Controls.Add(this.SearchAreaPage);
            this.VisualRoiTab.Controls.Add(this.TemplateParamPage);
            this.VisualRoiTab.Controls.Add(this.RunParamPage);
            this.VisualRoiTab.Location = new System.Drawing.Point(9, 31);
            this.VisualRoiTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.VisualRoiTab.Name = "VisualRoiTab";
            this.VisualRoiTab.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VisualRoiTab.SelectedIndex = 0;
            this.VisualRoiTab.Size = new System.Drawing.Size(335, 199);
            this.VisualRoiTab.TabIndex = 0;
            this.VisualRoiTab.SelectedIndexChanged += new System.EventHandler(this.VisualRoiTab_SelectedIndexChanged);
            this.VisualRoiTab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VisualRoiTab_KeyDown);
            // 
            // TemplatePage
            // 
            this.TemplatePage.Controls.Add(this.SetBenchmarkBtn);
            this.TemplatePage.Controls.Add(this.label4);
            this.TemplatePage.Controls.Add(this.label2);
            this.TemplatePage.Controls.Add(this.BenchmarkYtextBox);
            this.TemplatePage.Controls.Add(this.BenchmarkXtextBox);
            this.TemplatePage.Controls.Add(this.TemplateResizeBtn);
            this.TemplatePage.Controls.Add(this.TemplateMoveBtn);
            this.TemplatePage.Controls.Add(this.TemplateRoiShowBtn);
            this.TemplatePage.Location = new System.Drawing.Point(4, 27);
            this.TemplatePage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TemplatePage.Name = "TemplatePage";
            this.TemplatePage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TemplatePage.Size = new System.Drawing.Size(327, 168);
            this.TemplatePage.TabIndex = 0;
            this.TemplatePage.Text = "轮廓区域";
            this.TemplatePage.UseVisualStyleBackColor = true;
            // 
            // SetBenchmarkBtn
            // 
            this.SetBenchmarkBtn.Location = new System.Drawing.Point(240, 33);
            this.SetBenchmarkBtn.Name = "SetBenchmarkBtn";
            this.SetBenchmarkBtn.Size = new System.Drawing.Size(75, 30);
            this.SetBenchmarkBtn.TabIndex = 7;
            this.SetBenchmarkBtn.Text = "设置";
            this.SetBenchmarkBtn.UseVisualStyleBackColor = true;
            this.SetBenchmarkBtn.Click += new System.EventHandler(this.SetBenchmarkBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "基准点:  X";
            // 
            // BenchmarkYtextBox
            // 
            this.BenchmarkYtextBox.Location = new System.Drawing.Point(181, 37);
            this.BenchmarkYtextBox.Name = "BenchmarkYtextBox";
            this.BenchmarkYtextBox.Size = new System.Drawing.Size(47, 26);
            this.BenchmarkYtextBox.TabIndex = 4;
            this.BenchmarkYtextBox.Text = "-1";
            this.BenchmarkYtextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BenchmarkXtextBox
            // 
            this.BenchmarkXtextBox.Location = new System.Drawing.Point(96, 37);
            this.BenchmarkXtextBox.Name = "BenchmarkXtextBox";
            this.BenchmarkXtextBox.Size = new System.Drawing.Size(47, 26);
            this.BenchmarkXtextBox.TabIndex = 3;
            this.BenchmarkXtextBox.Text = "-1";
            this.BenchmarkXtextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TemplateResizeBtn
            // 
            this.TemplateResizeBtn.Location = new System.Drawing.Point(171, 69);
            this.TemplateResizeBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TemplateResizeBtn.Name = "TemplateResizeBtn";
            this.TemplateResizeBtn.Size = new System.Drawing.Size(144, 80);
            this.TemplateResizeBtn.TabIndex = 2;
            this.TemplateResizeBtn.Text = "Resize";
            this.TemplateResizeBtn.CheckedChanged += new System.EventHandler(this.TemplateResizeBtn_CheckedChanged);
            // 
            // TemplateMoveBtn
            // 
            this.TemplateMoveBtn.Location = new System.Drawing.Point(9, 69);
            this.TemplateMoveBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TemplateMoveBtn.Name = "TemplateMoveBtn";
            this.TemplateMoveBtn.Size = new System.Drawing.Size(144, 80);
            this.TemplateMoveBtn.TabIndex = 1;
            this.TemplateMoveBtn.Text = "Move";
            this.TemplateMoveBtn.CheckedChanged += new System.EventHandler(this.TemplateMoveBtn_CheckedChanged);
            // 
            // TemplateRoiShowBtn
            // 
            this.TemplateRoiShowBtn.AutoSize = true;
            this.TemplateRoiShowBtn.Location = new System.Drawing.Point(9, 9);
            this.TemplateRoiShowBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TemplateRoiShowBtn.Name = "TemplateRoiShowBtn";
            this.TemplateRoiShowBtn.Size = new System.Drawing.Size(83, 22);
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
            this.SearchAreaPage.Location = new System.Drawing.Point(4, 27);
            this.SearchAreaPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchAreaPage.Name = "SearchAreaPage";
            this.SearchAreaPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchAreaPage.Size = new System.Drawing.Size(327, 168);
            this.SearchAreaPage.TabIndex = 1;
            this.SearchAreaPage.Text = "搜索区域";
            this.SearchAreaPage.UseVisualStyleBackColor = true;
            // 
            // SearchAreaResizeBtn
            // 
            this.SearchAreaResizeBtn.Location = new System.Drawing.Point(171, 69);
            this.SearchAreaResizeBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchAreaResizeBtn.Name = "SearchAreaResizeBtn";
            this.SearchAreaResizeBtn.Size = new System.Drawing.Size(144, 80);
            this.SearchAreaResizeBtn.TabIndex = 5;
            this.SearchAreaResizeBtn.Text = "Resize";
            this.SearchAreaResizeBtn.CheckedChanged += new System.EventHandler(this.SearchAreaResizeBtn_CheckedChanged);
            // 
            // SearchAreaMoveBtn
            // 
            this.SearchAreaMoveBtn.Location = new System.Drawing.Point(9, 69);
            this.SearchAreaMoveBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchAreaMoveBtn.Name = "SearchAreaMoveBtn";
            this.SearchAreaMoveBtn.Size = new System.Drawing.Size(144, 80);
            this.SearchAreaMoveBtn.TabIndex = 4;
            this.SearchAreaMoveBtn.Text = "Move";
            this.SearchAreaMoveBtn.CheckedChanged += new System.EventHandler(this.SearchAreaMoveBtn_CheckedChanged);
            // 
            // SearchAreaRoiShowBtn
            // 
            this.SearchAreaRoiShowBtn.AutoSize = true;
            this.SearchAreaRoiShowBtn.Location = new System.Drawing.Point(9, 9);
            this.SearchAreaRoiShowBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchAreaRoiShowBtn.Name = "SearchAreaRoiShowBtn";
            this.SearchAreaRoiShowBtn.Size = new System.Drawing.Size(83, 22);
            this.SearchAreaRoiShowBtn.TabIndex = 3;
            this.SearchAreaRoiShowBtn.Text = "RoiShow";
            this.SearchAreaRoiShowBtn.UseVisualStyleBackColor = true;
            this.SearchAreaRoiShowBtn.CheckedChanged += new System.EventHandler(this.SearchAreaRoiShowBtn_CheckedChanged);
            // 
            // TemplateParamPage
            // 
            this.TemplateParamPage.Controls.Add(this.textBoxMinChain);
            this.TemplateParamPage.Controls.Add(this.textBoxThresValue);
            this.TemplateParamPage.Controls.Add(this.textBoxFineValue);
            this.TemplateParamPage.Controls.Add(this.textBoxRoughValue);
            this.TemplateParamPage.Controls.Add(this.comboBoxChainFlag);
            this.TemplateParamPage.Controls.Add(this.comboBoxWeigthMaskFlag);
            this.TemplateParamPage.Controls.Add(this.comboBoxThresFlag);
            this.TemplateParamPage.Controls.Add(this.label12);
            this.TemplateParamPage.Controls.Add(this.label11);
            this.TemplateParamPage.Controls.Add(this.label10);
            this.TemplateParamPage.Controls.Add(this.comboBoxGranFlag);
            this.TemplateParamPage.Controls.Add(this.label9);
            this.TemplateParamPage.Controls.Add(this.label8);
            this.TemplateParamPage.Controls.Add(this.label7);
            this.TemplateParamPage.Controls.Add(this.label6);
            this.TemplateParamPage.Controls.Add(this.label5);
            this.TemplateParamPage.Location = new System.Drawing.Point(4, 27);
            this.TemplateParamPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TemplateParamPage.Name = "TemplateParamPage";
            this.TemplateParamPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TemplateParamPage.Size = new System.Drawing.Size(327, 168);
            this.TemplateParamPage.TabIndex = 2;
            this.TemplateParamPage.Text = "训练参数";
            this.TemplateParamPage.UseVisualStyleBackColor = true;
            // 
            // textBoxMinChain
            // 
            this.textBoxMinChain.EditValue = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.textBoxMinChain.Location = new System.Drawing.Point(234, 69);
            this.textBoxMinChain.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMinChain.Name = "textBoxMinChain";
            this.textBoxMinChain.Properties.AutoHeight = false;
            this.textBoxMinChain.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textBoxMinChain.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textBoxMinChain.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textBoxMinChain.Properties.MaskSettings.Set("mask", "d");
            this.textBoxMinChain.Size = new System.Drawing.Size(86, 27);
            this.textBoxMinChain.TabIndex = 45;
            this.textBoxMinChain.EditValueChanged += new System.EventHandler(this.textBoxMinChain_EditValueChanged);
            // 
            // textBoxThresValue
            // 
            this.textBoxThresValue.EditValue = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.textBoxThresValue.Location = new System.Drawing.Point(80, 130);
            this.textBoxThresValue.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxThresValue.Name = "textBoxThresValue";
            this.textBoxThresValue.Properties.AutoHeight = false;
            this.textBoxThresValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textBoxThresValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textBoxThresValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textBoxThresValue.Properties.MaskSettings.Set("mask", "d");
            this.textBoxThresValue.Size = new System.Drawing.Size(76, 27);
            this.textBoxThresValue.TabIndex = 44;
            this.textBoxThresValue.EditValueChanged += new System.EventHandler(this.textBoxThresValue_EditValueChanged);
            // 
            // textBoxFineValue
            // 
            this.textBoxFineValue.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textBoxFineValue.Location = new System.Drawing.Point(80, 69);
            this.textBoxFineValue.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxFineValue.Name = "textBoxFineValue";
            this.textBoxFineValue.Properties.AutoHeight = false;
            this.textBoxFineValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textBoxFineValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textBoxFineValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textBoxFineValue.Properties.MaskSettings.Set("mask", "d");
            this.textBoxFineValue.Size = new System.Drawing.Size(76, 27);
            this.textBoxFineValue.TabIndex = 43;
            this.textBoxFineValue.EditValueChanged += new System.EventHandler(this.textBoxFineValue_EditValueChanged);
            // 
            // textBoxRoughValue
            // 
            this.textBoxRoughValue.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.textBoxRoughValue.Location = new System.Drawing.Point(80, 39);
            this.textBoxRoughValue.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxRoughValue.Name = "textBoxRoughValue";
            this.textBoxRoughValue.Properties.AutoHeight = false;
            this.textBoxRoughValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textBoxRoughValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textBoxRoughValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textBoxRoughValue.Properties.MaskSettings.Set("mask", "f3");
            this.textBoxRoughValue.Properties.MaxValue = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.textBoxRoughValue.Size = new System.Drawing.Size(76, 27);
            this.textBoxRoughValue.TabIndex = 42;
            this.textBoxRoughValue.EditValueChanged += new System.EventHandler(this.textBoxRoughValue_EditValueChanged);
            // 
            // comboBoxChainFlag
            // 
            this.comboBoxChainFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChainFlag.FormattingEnabled = true;
            this.comboBoxChainFlag.Items.AddRange(new object[] {
            "Manual",
            "Auto"});
            this.comboBoxChainFlag.Location = new System.Drawing.Point(234, 40);
            this.comboBoxChainFlag.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxChainFlag.Name = "comboBoxChainFlag";
            this.comboBoxChainFlag.Size = new System.Drawing.Size(86, 26);
            this.comboBoxChainFlag.TabIndex = 17;
            this.comboBoxChainFlag.SelectedIndexChanged += new System.EventHandler(this.comboBoxChainFlag_SelectedIndexChanged);
            // 
            // comboBoxWeigthMaskFlag
            // 
            this.comboBoxWeigthMaskFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWeigthMaskFlag.FormattingEnabled = true;
            this.comboBoxWeigthMaskFlag.Items.AddRange(new object[] {
            "False",
            "True"});
            this.comboBoxWeigthMaskFlag.Location = new System.Drawing.Point(234, 10);
            this.comboBoxWeigthMaskFlag.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxWeigthMaskFlag.Name = "comboBoxWeigthMaskFlag";
            this.comboBoxWeigthMaskFlag.Size = new System.Drawing.Size(86, 26);
            this.comboBoxWeigthMaskFlag.TabIndex = 16;
            this.comboBoxWeigthMaskFlag.SelectedIndexChanged += new System.EventHandler(this.comboBoxWeigthMaskFlag_SelectedIndexChanged);
            // 
            // comboBoxThresFlag
            // 
            this.comboBoxThresFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxThresFlag.FormattingEnabled = true;
            this.comboBoxThresFlag.Items.AddRange(new object[] {
            "Manual",
            "Auto"});
            this.comboBoxThresFlag.Location = new System.Drawing.Point(80, 100);
            this.comboBoxThresFlag.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxThresFlag.Name = "comboBoxThresFlag";
            this.comboBoxThresFlag.Size = new System.Drawing.Size(76, 26);
            this.comboBoxThresFlag.TabIndex = 13;
            this.comboBoxThresFlag.SelectedIndexChanged += new System.EventHandler(this.comboBoxThresFlag_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(163, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 18);
            this.label12.TabIndex = 12;
            this.label12.Text = "最小链长";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(163, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 18);
            this.label11.TabIndex = 11;
            this.label11.Text = "链长使能";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(163, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 18);
            this.label10.TabIndex = 10;
            this.label10.Text = "权重使能";
            // 
            // comboBoxGranFlag
            // 
            this.comboBoxGranFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGranFlag.FormattingEnabled = true;
            this.comboBoxGranFlag.Items.AddRange(new object[] {
            "Manual",
            "Auto"});
            this.comboBoxGranFlag.Location = new System.Drawing.Point(80, 10);
            this.comboBoxGranFlag.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxGranFlag.Name = "comboBoxGranFlag";
            this.comboBoxGranFlag.Size = new System.Drawing.Size(76, 26);
            this.comboBoxGranFlag.TabIndex = 9;
            this.comboBoxGranFlag.SelectedIndexChanged += new System.EventHandler(this.comboBoxGranFlag_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(35, 134);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 18);
            this.label9.TabIndex = 8;
            this.label9.Text = "阈值";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 18);
            this.label8.TabIndex = 7;
            this.label8.Text = "阈值标记";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 18);
            this.label7.TabIndex = 6;
            this.label7.Text = "返回层";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 18);
            this.label6.TabIndex = 5;
            this.label6.Text = "层数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "特征尺度";
            // 
            // RunParamPage
            // 
            this.RunParamPage.Controls.Add(this.comboBoxSortType);
            this.RunParamPage.Controls.Add(this.label15);
            this.RunParamPage.Controls.Add(this.textMaxMatchNum);
            this.RunParamPage.Controls.Add(this.label14);
            this.RunParamPage.Controls.Add(this.textMinScore);
            this.RunParamPage.Controls.Add(this.label13);
            this.RunParamPage.Location = new System.Drawing.Point(4, 27);
            this.RunParamPage.Name = "RunParamPage";
            this.RunParamPage.Padding = new System.Windows.Forms.Padding(3);
            this.RunParamPage.Size = new System.Drawing.Size(327, 168);
            this.RunParamPage.TabIndex = 3;
            this.RunParamPage.Text = "识别参数";
            this.RunParamPage.UseVisualStyleBackColor = true;
            // 
            // comboBoxSortType
            // 
            this.comboBoxSortType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSortType.FormattingEnabled = true;
            this.comboBoxSortType.Items.AddRange(new object[] {
            "None",
            "Score",
            "Angle",
            "X",
            "Y",
            "XY",
            "YX"});
            this.comboBoxSortType.Location = new System.Drawing.Point(80, 79);
            this.comboBoxSortType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxSortType.Name = "comboBoxSortType";
            this.comboBoxSortType.Size = new System.Drawing.Size(76, 26);
            this.comboBoxSortType.TabIndex = 48;
            this.comboBoxSortType.SelectedIndexChanged += new System.EventHandler(this.comboBoxSortType_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(5, 83);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 18);
            this.label15.TabIndex = 47;
            this.label15.Text = "排序类型";
            // 
            // textMaxMatchNum
            // 
            this.textMaxMatchNum.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textMaxMatchNum.Location = new System.Drawing.Point(80, 44);
            this.textMaxMatchNum.Margin = new System.Windows.Forms.Padding(4);
            this.textMaxMatchNum.Name = "textMaxMatchNum";
            this.textMaxMatchNum.Properties.AutoHeight = false;
            this.textMaxMatchNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textMaxMatchNum.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textMaxMatchNum.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textMaxMatchNum.Properties.MaskSettings.Set("mask", "d");
            this.textMaxMatchNum.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.textMaxMatchNum.Size = new System.Drawing.Size(76, 27);
            this.textMaxMatchNum.TabIndex = 46;
            this.textMaxMatchNum.EditValueChanged += new System.EventHandler(this.textMaxMatchNum_EditValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 18);
            this.label14.TabIndex = 45;
            this.label14.Text = "最大个数";
            // 
            // textMinScore
            // 
            this.textMinScore.EditValue = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.textMinScore.Location = new System.Drawing.Point(80, 9);
            this.textMinScore.Margin = new System.Windows.Forms.Padding(4);
            this.textMinScore.Name = "textMinScore";
            this.textMinScore.Properties.AutoHeight = false;
            this.textMinScore.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textMinScore.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textMinScore.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textMinScore.Properties.MaskSettings.Set("mask", "f3");
            this.textMinScore.Properties.MaxValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textMinScore.Size = new System.Drawing.Size(76, 27);
            this.textMinScore.TabIndex = 44;
            this.textMinScore.EditValueChanged += new System.EventHandler(this.textMinScore_EditValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 18);
            this.label13.TabIndex = 43;
            this.label13.Text = "最小分数";
            // 
            // TemplateBtn
            // 
            this.TemplateBtn.Location = new System.Drawing.Point(30, 539);
            this.TemplateBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TemplateBtn.Name = "TemplateBtn";
            this.TemplateBtn.Size = new System.Drawing.Size(343, 46);
            this.TemplateBtn.TabIndex = 9;
            this.TemplateBtn.Text = "Template";
            this.TemplateBtn.UseVisualStyleBackColor = true;
            this.TemplateBtn.Click += new System.EventHandler(this.TemplateBtn_ClickAsync);
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(30, 598);
            this.SearchBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(343, 46);
            this.SearchBtn.TabIndex = 10;
            this.SearchBtn.Text = "Search";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // CameraWindowClearBtn
            // 
            this.CameraWindowClearBtn.Location = new System.Drawing.Point(30, 658);
            this.CameraWindowClearBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CameraWindowClearBtn.Name = "CameraWindowClearBtn";
            this.CameraWindowClearBtn.Size = new System.Drawing.Size(343, 46);
            this.CameraWindowClearBtn.TabIndex = 11;
            this.CameraWindowClearBtn.Text = "Clear";
            this.CameraWindowClearBtn.UseVisualStyleBackColor = true;
            this.CameraWindowClearBtn.Click += new System.EventHandler(this.CameraWindowClearBtn_Click);
            // 
            // RingLightNumlabel
            // 
            this.RingLightNumlabel.AutoSize = true;
            this.RingLightNumlabel.Location = new System.Drawing.Point(345, 12);
            this.RingLightNumlabel.Name = "RingLightNumlabel";
            this.RingLightNumlabel.Size = new System.Drawing.Size(16, 18);
            this.RingLightNumlabel.TabIndex = 12;
            this.RingLightNumlabel.Text = "0";
            // 
            // DirectLightNumlabel
            // 
            this.DirectLightNumlabel.AutoSize = true;
            this.DirectLightNumlabel.Location = new System.Drawing.Point(345, 82);
            this.DirectLightNumlabel.Name = "DirectLightNumlabel";
            this.DirectLightNumlabel.Size = new System.Drawing.Size(16, 18);
            this.DirectLightNumlabel.TabIndex = 13;
            this.DirectLightNumlabel.Text = "0";
            // 
            // MinimunqualityNumlabel
            // 
            this.MinimunqualityNumlabel.AutoSize = true;
            this.MinimunqualityNumlabel.Location = new System.Drawing.Point(345, 161);
            this.MinimunqualityNumlabel.Name = "MinimunqualityNumlabel";
            this.MinimunqualityNumlabel.Size = new System.Drawing.Size(29, 18);
            this.MinimunqualityNumlabel.TabIndex = 14;
            this.MinimunqualityNumlabel.Text = "0.9";
            // 
            // AngleDeviationNumlabel
            // 
            this.AngleDeviationNumlabel.AutoSize = true;
            this.AngleDeviationNumlabel.Location = new System.Drawing.Point(345, 233);
            this.AngleDeviationNumlabel.Name = "AngleDeviationNumlabel";
            this.AngleDeviationNumlabel.Size = new System.Drawing.Size(24, 18);
            this.AngleDeviationNumlabel.TabIndex = 15;
            this.AngleDeviationNumlabel.Text = "15";
            // 
            // panelStepOperate
            // 
            this.panelStepOperate.Controls.Add(this.AngleDeviationNumlabel);
            this.panelStepOperate.Controls.Add(this.RingLightlabel);
            this.panelStepOperate.Controls.Add(this.MinimunqualityNumlabel);
            this.panelStepOperate.Controls.Add(this.DirectLightNumlabel);
            this.panelStepOperate.Controls.Add(this.DirectLightlabel);
            this.panelStepOperate.Controls.Add(this.RingLightNumlabel);
            this.panelStepOperate.Controls.Add(this.CameraWindowClearBtn);
            this.panelStepOperate.Controls.Add(this.label3);
            this.panelStepOperate.Controls.Add(this.SearchBtn);
            this.panelStepOperate.Controls.Add(this.TemplateBtn);
            this.panelStepOperate.Controls.Add(this.label1);
            this.panelStepOperate.Controls.Add(this.RoiGroupBox);
            this.panelStepOperate.Controls.Add(this.RingLightBar);
            this.panelStepOperate.Controls.Add(this.DirectLightBar);
            this.panelStepOperate.Controls.Add(this.QualityBar);
            this.panelStepOperate.Controls.Add(this.AngleBar);
            this.panelStepOperate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStepOperate.Location = new System.Drawing.Point(0, 0);
            this.panelStepOperate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelStepOperate.Name = "panelStepOperate";
            this.panelStepOperate.Size = new System.Drawing.Size(393, 717);
            this.panelStepOperate.TabIndex = 39;
            // 
            // VisualMatchControlGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelStepOperate);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "VisualMatchControlGUI";
            this.Size = new System.Drawing.Size(393, 717);
            ((System.ComponentModel.ISupportInitialize)(this.RingLightBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirectLightBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QualityBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleBar)).EndInit();
            this.RoiGroupBox.ResumeLayout(false);
            this.VisualRoiTab.ResumeLayout(false);
            this.TemplatePage.ResumeLayout(false);
            this.TemplatePage.PerformLayout();
            this.SearchAreaPage.ResumeLayout(false);
            this.SearchAreaPage.PerformLayout();
            this.TemplateParamPage.ResumeLayout(false);
            this.TemplateParamPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxMinChain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxThresValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFineValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxRoughValue.Properties)).EndInit();
            this.RunParamPage.ResumeLayout(false);
            this.RunParamPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textMaxMatchNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textMinScore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelStepOperate)).EndInit();
            this.panelStepOperate.ResumeLayout(false);
            this.panelStepOperate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar RingLightBar;
        private System.Windows.Forms.Label RingLightlabel;
        private System.Windows.Forms.Label DirectLightlabel;
        private System.Windows.Forms.TrackBar DirectLightBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar QualityBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar AngleBar;
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
        private System.Windows.Forms.Button TemplateBtn;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Button CameraWindowClearBtn;
        private System.Windows.Forms.TabPage TemplateParamPage;
        private System.Windows.Forms.Label RingLightNumlabel;
        private System.Windows.Forms.Label DirectLightNumlabel;
        private System.Windows.Forms.Label MinimunqualityNumlabel;
        private System.Windows.Forms.Label AngleDeviationNumlabel;
        private DevExpress.XtraEditors.PanelControl panelStepOperate;
        private System.Windows.Forms.TextBox BenchmarkXtextBox;
        private System.Windows.Forms.TextBox BenchmarkYtextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SetBenchmarkBtn;
        private System.Windows.Forms.TabPage RunParamPage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxGranFlag;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxChainFlag;
        private System.Windows.Forms.ComboBox comboBoxWeigthMaskFlag;
        private System.Windows.Forms.ComboBox comboBoxThresFlag;
        private DevExpress.XtraEditors.SpinEdit textBoxRoughValue;
        private DevExpress.XtraEditors.SpinEdit textBoxFineValue;
        private DevExpress.XtraEditors.SpinEdit textBoxThresValue;
        private DevExpress.XtraEditors.SpinEdit textBoxMinChain;
        private DevExpress.XtraEditors.SpinEdit textMinScore;
        private System.Windows.Forms.Label label13;
        private DevExpress.XtraEditors.SpinEdit textMaxMatchNum;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxSortType;
        private System.Windows.Forms.Label label15;
    }
}
