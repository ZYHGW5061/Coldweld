
namespace BondTerminal
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.编程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HeatRecipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TransportRecipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统校准ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.手动校准ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动校准ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iO测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OvenHeatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分子泵控制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.真空计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolHomeButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolSafeButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnCameraControl = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelNowTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRunTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRunStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelAlarm = new System.Windows.Forms.ToolStripStatusLabel();
            this.teLog = new System.Windows.Forms.TextBox();
            this.btnSelectTransportRecipe = new System.Windows.Forms.Button();
            this.teTransportRecipeName = new System.Windows.Forms.TextBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnPausedOrSingle = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.teCurrentState = new System.Windows.Forms.TextBox();
            this.btnAllAxisStop = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.编程ToolStripMenuItem,
            this.系统ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1924, 37);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 编程ToolStripMenuItem
            // 
            this.编程ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HeatRecipeToolStripMenuItem,
            this.TransportRecipeToolStripMenuItem});
            this.编程ToolStripMenuItem.Name = "编程ToolStripMenuItem";
            this.编程ToolStripMenuItem.Size = new System.Drawing.Size(66, 31);
            this.编程ToolStripMenuItem.Text = "编程";
            // 
            // HeatRecipeToolStripMenuItem
            // 
            this.HeatRecipeToolStripMenuItem.Name = "HeatRecipeToolStripMenuItem";
            this.HeatRecipeToolStripMenuItem.Size = new System.Drawing.Size(178, 32);
            this.HeatRecipeToolStripMenuItem.Text = "加热配方";
            this.HeatRecipeToolStripMenuItem.Click += new System.EventHandler(this.HeatRecipeToolStripMenuItem_Click);
            // 
            // TransportRecipeToolStripMenuItem
            // 
            this.TransportRecipeToolStripMenuItem.Name = "TransportRecipeToolStripMenuItem";
            this.TransportRecipeToolStripMenuItem.Size = new System.Drawing.Size(178, 32);
            this.TransportRecipeToolStripMenuItem.Text = "生产配方";
            this.TransportRecipeToolStripMenuItem.Click += new System.EventHandler(this.TransportRecipeToolStripMenuItem_Click);
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统校准ToolStripMenuItem,
            this.iO测试ToolStripMenuItem,
            this.维护ToolStripMenuItem});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(66, 31);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // 系统校准ToolStripMenuItem
            // 
            this.系统校准ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.手动校准ToolStripMenuItem,
            this.自动校准ToolStripMenuItem});
            this.系统校准ToolStripMenuItem.Name = "系统校准ToolStripMenuItem";
            this.系统校准ToolStripMenuItem.Size = new System.Drawing.Size(178, 32);
            this.系统校准ToolStripMenuItem.Text = "系统校准";
            // 
            // 手动校准ToolStripMenuItem
            // 
            this.手动校准ToolStripMenuItem.Name = "手动校准ToolStripMenuItem";
            this.手动校准ToolStripMenuItem.Size = new System.Drawing.Size(278, 32);
            this.手动校准ToolStripMenuItem.Text = "勾爪与相机位置校准";
            this.手动校准ToolStripMenuItem.Click += new System.EventHandler(this.手动校准ToolStripMenuItem_Click);
            // 
            // 自动校准ToolStripMenuItem
            // 
            this.自动校准ToolStripMenuItem.Name = "自动校准ToolStripMenuItem";
            this.自动校准ToolStripMenuItem.Size = new System.Drawing.Size(278, 32);
            this.自动校准ToolStripMenuItem.Text = "勾爪空闲位置校准";
            this.自动校准ToolStripMenuItem.Click += new System.EventHandler(this.自动校准ToolStripMenuItem_Click);
            // 
            // iO测试ToolStripMenuItem
            // 
            this.iO测试ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OvenHeatToolStripMenuItem,
            this.分子泵控制ToolStripMenuItem,
            this.真空计ToolStripMenuItem});
            this.iO测试ToolStripMenuItem.Name = "iO测试ToolStripMenuItem";
            this.iO测试ToolStripMenuItem.Size = new System.Drawing.Size(178, 32);
            this.iO测试ToolStripMenuItem.Text = "设备";
            this.iO测试ToolStripMenuItem.Click += new System.EventHandler(this.iO测试ToolStripMenuItem_Click);
            // 
            // OvenHeatToolStripMenuItem
            // 
            this.OvenHeatToolStripMenuItem.Name = "OvenHeatToolStripMenuItem";
            this.OvenHeatToolStripMenuItem.Size = new System.Drawing.Size(198, 32);
            this.OvenHeatToolStripMenuItem.Text = "烘箱加热";
            this.OvenHeatToolStripMenuItem.Click += new System.EventHandler(this.OvenHeatToolStripMenuItem_Click);
            // 
            // 分子泵控制ToolStripMenuItem
            // 
            this.分子泵控制ToolStripMenuItem.Name = "分子泵控制ToolStripMenuItem";
            this.分子泵控制ToolStripMenuItem.Size = new System.Drawing.Size(198, 32);
            this.分子泵控制ToolStripMenuItem.Text = "分子泵控制";
            this.分子泵控制ToolStripMenuItem.Click += new System.EventHandler(this.分子泵控制ToolStripMenuItem_Click);
            // 
            // 真空计ToolStripMenuItem
            // 
            this.真空计ToolStripMenuItem.Name = "真空计ToolStripMenuItem";
            this.真空计ToolStripMenuItem.Size = new System.Drawing.Size(198, 32);
            this.真空计ToolStripMenuItem.Text = "真空";
            this.真空计ToolStripMenuItem.Click += new System.EventHandler(this.真空计ToolStripMenuItem_Click);
            // 
            // 维护ToolStripMenuItem
            // 
            this.维护ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.运动ToolStripMenuItem,
            this.iOToolStripMenuItem});
            this.维护ToolStripMenuItem.Name = "维护ToolStripMenuItem";
            this.维护ToolStripMenuItem.Size = new System.Drawing.Size(178, 32);
            this.维护ToolStripMenuItem.Text = "维护";
            // 
            // 运动ToolStripMenuItem
            // 
            this.运动ToolStripMenuItem.Name = "运动ToolStripMenuItem";
            this.运动ToolStripMenuItem.Size = new System.Drawing.Size(138, 32);
            this.运动ToolStripMenuItem.Text = "运动";
            this.运动ToolStripMenuItem.Click += new System.EventHandler(this.运动ToolStripMenuItem_Click);
            // 
            // iOToolStripMenuItem
            // 
            this.iOToolStripMenuItem.Name = "iOToolStripMenuItem";
            this.iOToolStripMenuItem.Size = new System.Drawing.Size(138, 32);
            this.iOToolStripMenuItem.Text = "IO";
            this.iOToolStripMenuItem.Click += new System.EventHandler(this.iOToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(45, 45);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolHomeButton7,
            this.toolSafeButton1,
            this.toolStripBtnCameraControl,
            this.toolStripButton4,
            this.toolStripButton8});
            this.toolStrip1.Location = new System.Drawing.Point(0, 37);
            this.toolStrip1.MaximumSize = new System.Drawing.Size(0, 71);
            this.toolStrip1.MinimumSize = new System.Drawing.Size(0, 52);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1924, 52);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(49, 49);
            this.toolStripButton5.Text = "toolStripButton5";
            this.toolStripButton5.ToolTipText = "登出";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(49, 49);
            this.toolStripButton6.Text = "toolStripButton6";
            this.toolStripButton6.ToolTipText = "保存系统配置";
            // 
            // toolHomeButton7
            // 
            this.toolHomeButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolHomeButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolHomeButton7.Image")));
            this.toolHomeButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolHomeButton7.Name = "toolHomeButton7";
            this.toolHomeButton7.Size = new System.Drawing.Size(49, 49);
            this.toolHomeButton7.Text = "toolStripButton7";
            this.toolHomeButton7.ToolTipText = "轴回零点";
            this.toolHomeButton7.Click += new System.EventHandler(this.toolHomeButton7_Click);
            // 
            // toolSafeButton1
            // 
            this.toolSafeButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSafeButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolSafeButton1.Image")));
            this.toolSafeButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSafeButton1.Name = "toolSafeButton1";
            this.toolSafeButton1.Size = new System.Drawing.Size(49, 49);
            this.toolSafeButton1.Text = "轴回安全位置";
            this.toolSafeButton1.ToolTipText = "轴回空闲位置";
            this.toolSafeButton1.Click += new System.EventHandler(this.toolSafeButton1_Click);
            // 
            // toolStripBtnCameraControl
            // 
            this.toolStripBtnCameraControl.CheckOnClick = true;
            this.toolStripBtnCameraControl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnCameraControl.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnCameraControl.Image")));
            this.toolStripBtnCameraControl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnCameraControl.Name = "toolStripBtnCameraControl";
            this.toolStripBtnCameraControl.Size = new System.Drawing.Size(49, 49);
            this.toolStripBtnCameraControl.Text = "toolStripBtnCameraControl";
            this.toolStripBtnCameraControl.ToolTipText = "相机";
            this.toolStripBtnCameraControl.CheckedChanged += new System.EventHandler(this.toolStripBtnCameraControl_CheckedChanged);
            this.toolStripBtnCameraControl.Click += new System.EventHandler(this.toolStripBtnCameraControl_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(49, 49);
            this.toolStripButton4.Text = "toolStripBtnStageControl";
            this.toolStripButton4.ToolTipText = "轴运动控制";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripBtnStageControl_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(49, 49);
            this.toolStripButton8.Text = "toolStripButton8";
            this.toolStripButton8.ToolTipText = "IO控制";
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelTime,
            this.toolStripStatusLabelNowTime,
            this.toolStripStatusLabelRunTime,
            this.toolStripStatusLabelRunStatus,
            this.toolStripStatusLabelAlarm});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1037);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1924, 24);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelTime
            // 
            this.toolStripStatusLabelTime.Name = "toolStripStatusLabelTime";
            this.toolStripStatusLabelTime.Size = new System.Drawing.Size(0, 18);
            // 
            // toolStripStatusLabelNowTime
            // 
            this.toolStripStatusLabelNowTime.AutoSize = false;
            this.toolStripStatusLabelNowTime.Name = "toolStripStatusLabelNowTime";
            this.toolStripStatusLabelNowTime.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.toolStripStatusLabelNowTime.Size = new System.Drawing.Size(200, 18);
            this.toolStripStatusLabelNowTime.Text = "时间";
            // 
            // toolStripStatusLabelRunTime
            // 
            this.toolStripStatusLabelRunTime.AutoSize = false;
            this.toolStripStatusLabelRunTime.Name = "toolStripStatusLabelRunTime";
            this.toolStripStatusLabelRunTime.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.toolStripStatusLabelRunTime.Size = new System.Drawing.Size(200, 18);
            this.toolStripStatusLabelRunTime.Text = "运行时长";
            // 
            // toolStripStatusLabelRunStatus
            // 
            this.toolStripStatusLabelRunStatus.AutoSize = false;
            this.toolStripStatusLabelRunStatus.Name = "toolStripStatusLabelRunStatus";
            this.toolStripStatusLabelRunStatus.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.toolStripStatusLabelRunStatus.Size = new System.Drawing.Size(150, 18);
            this.toolStripStatusLabelRunStatus.Text = "运行状态";
            // 
            // toolStripStatusLabelAlarm
            // 
            this.toolStripStatusLabelAlarm.AutoSize = false;
            this.toolStripStatusLabelAlarm.Name = "toolStripStatusLabelAlarm";
            this.toolStripStatusLabelAlarm.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.toolStripStatusLabelAlarm.Size = new System.Drawing.Size(250, 18);
            this.toolStripStatusLabelAlarm.Text = "报警状态";
            // 
            // teLog
            // 
            this.teLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.teLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teLog.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.teLog.Location = new System.Drawing.Point(783, 80);
            this.teLog.Multiline = true;
            this.teLog.Name = "teLog";
            this.tableLayoutPanel1.SetRowSpan(this.teLog, 7);
            this.teLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.teLog.Size = new System.Drawing.Size(514, 571);
            this.teLog.TabIndex = 4;
            this.teLog.Text = "*****日志*****\r\n";
            this.teLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSelectTransportRecipe
            // 
            this.btnSelectTransportRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectTransportRecipe.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectTransportRecipe.Location = new System.Drawing.Point(3, 156);
            this.btnSelectTransportRecipe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectTransportRecipe.Name = "btnSelectTransportRecipe";
            this.btnSelectTransportRecipe.Size = new System.Drawing.Size(189, 73);
            this.btnSelectTransportRecipe.TabIndex = 8;
            this.btnSelectTransportRecipe.Text = "选择配方";
            this.btnSelectTransportRecipe.UseVisualStyleBackColor = true;
            this.btnSelectTransportRecipe.Click += new System.EventHandler(this.btnSelectTransportRecipe_Click);
            // 
            // teTransportRecipeName
            // 
            this.teTransportRecipeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teTransportRecipeName.Enabled = false;
            this.teTransportRecipeName.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.teTransportRecipeName.Location = new System.Drawing.Point(4, 81);
            this.teTransportRecipeName.Margin = new System.Windows.Forms.Padding(4);
            this.teTransportRecipeName.Multiline = true;
            this.teTransportRecipeName.Name = "teTransportRecipeName";
            this.teTransportRecipeName.Size = new System.Drawing.Size(187, 69);
            this.teTransportRecipeName.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(47, 23);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(100, 30);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "配方名称";
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.Location = new System.Drawing.Point(4, 581);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(187, 69);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "结束";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnContinue.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnContinue.Location = new System.Drawing.Point(4, 504);
            this.btnContinue.Margin = new System.Windows.Forms.Padding(4);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(187, 69);
            this.btnContinue.TabIndex = 12;
            this.btnContinue.Text = "继续执行";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnPausedOrSingle
            // 
            this.btnPausedOrSingle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPausedOrSingle.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPausedOrSingle.Location = new System.Drawing.Point(4, 427);
            this.btnPausedOrSingle.Margin = new System.Windows.Forms.Padding(4);
            this.btnPausedOrSingle.Name = "btnPausedOrSingle";
            this.btnPausedOrSingle.Size = new System.Drawing.Size(187, 69);
            this.btnPausedOrSingle.TabIndex = 11;
            this.btnPausedOrSingle.Text = "暂定/单步执行";
            this.btnPausedOrSingle.UseVisualStyleBackColor = true;
            this.btnPausedOrSingle.Click += new System.EventHandler(this.btnPausedOrSingle_Click);
            // 
            // btnRun
            // 
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRun.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRun.Location = new System.Drawing.Point(4, 350);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(187, 69);
            this.btnRun.TabIndex = 10;
            this.btnRun.Text = "自动执行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Enabled = false;
            this.dataGridView2.Location = new System.Drawing.Point(199, 81);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersWidth = 51;
            this.tableLayoutPanel1.SetRowSpan(this.dataGridView2, 7);
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView2.Size = new System.Drawing.Size(577, 569);
            this.dataGridView2.TabIndex = 15;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1306, 796);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.teCurrentState, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.teTransportRecipeName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.teLog, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectTransportRecipe, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnStop, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnContinue, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnPausedOrSingle, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnRun, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnAllAxisStop, 0, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1300, 770);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // teCurrentState
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.teCurrentState, 2);
            this.teCurrentState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teCurrentState.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.teCurrentState.Location = new System.Drawing.Point(198, 3);
            this.teCurrentState.Multiline = true;
            this.teCurrentState.Name = "teCurrentState";
            this.teCurrentState.Size = new System.Drawing.Size(1099, 71);
            this.teCurrentState.TabIndex = 18;
            // 
            // btnAllAxisStop
            // 
            this.btnAllAxisStop.BackColor = System.Drawing.Color.Red;
            this.btnAllAxisStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAllAxisStop.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAllAxisStop.Location = new System.Drawing.Point(4, 658);
            this.btnAllAxisStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnAllAxisStop.Name = "btnAllAxisStop";
            this.btnAllAxisStop.Size = new System.Drawing.Size(187, 69);
            this.btnAllAxisStop.TabIndex = 19;
            this.btnAllAxisStop.Text = "急停";
            this.btnAllAxisStop.UseVisualStyleBackColor = false;
            this.btnAllAxisStop.Click += new System.EventHandler(this.btnAllAxisStop_Click);
            // 
            // MainForm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1061);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("MainForm.IconOptions.Image")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "冷压焊";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;

        private System.Windows.Forms.ToolStripMenuItem 编程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HeatRecipeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TransportRecipeToolStripMenuItem;        private System.Windows.Forms.ToolStripButton toolStripBtnCameraControl;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统校准ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 手动校准ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自动校准ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolHomeButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripMenuItem iO测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 维护ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OvenHeatToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelNowTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRunTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRunStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAlarm;
        private System.Windows.Forms.TextBox teLog;
        private System.Windows.Forms.Button btnSelectTransportRecipe;
        private System.Windows.Forms.TextBox teTransportRecipeName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnPausedOrSingle;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox teCurrentState;
        private System.Windows.Forms.ToolStripButton toolSafeButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAllAxisStop;
        private System.Windows.Forms.ToolStripMenuItem 分子泵控制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 真空计ToolStripMenuItem;
    }
}

