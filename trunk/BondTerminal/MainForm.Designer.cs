
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
            this.用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生产ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.单步ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统校准ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统初始化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.手动校准ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动校准ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iO测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.共晶台测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pP工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.顶针工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnCameraControl = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnLightControl = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.用户ToolStripMenuItem,
            this.生产ToolStripMenuItem,
            this.编程ToolStripMenuItem,
            this.系统ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1924, 31);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 用户ToolStripMenuItem
            // 
            this.用户ToolStripMenuItem.Name = "用户ToolStripMenuItem";
            this.用户ToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.用户ToolStripMenuItem.Text = "用户";
            // 
            // 生产ToolStripMenuItem
            // 
            this.生产ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem1,
            this.单步ToolStripMenuItem});
            this.生产ToolStripMenuItem.Name = "生产ToolStripMenuItem";
            this.生产ToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.生产ToolStripMenuItem.Text = "生产";
            // 
            // 新建ToolStripMenuItem1
            // 
            this.新建ToolStripMenuItem1.Name = "新建ToolStripMenuItem1";
            this.新建ToolStripMenuItem1.Size = new System.Drawing.Size(180, 26);
            this.新建ToolStripMenuItem1.Text = "生产操作";
            this.新建ToolStripMenuItem1.Click += new System.EventHandler(this.新建ToolStripMenuItem1_Click);
            // 
            // 单步ToolStripMenuItem
            // 
            this.单步ToolStripMenuItem.Name = "单步ToolStripMenuItem";
            this.单步ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.单步ToolStripMenuItem.Text = "单步";
            this.单步ToolStripMenuItem.Click += new System.EventHandler(this.单步ToolStripMenuItem_Click);
            // 
            // 编程ToolStripMenuItem
            // 
            this.编程ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.编辑ToolStripMenuItem});
            this.编程ToolStripMenuItem.Name = "编程ToolStripMenuItem";
            this.编程ToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.编程ToolStripMenuItem.Text = "编程";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.新建ToolStripMenuItem.Text = "新建";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.编辑ToolStripMenuItem.Text = "编辑";
            this.编辑ToolStripMenuItem.Click += new System.EventHandler(this.编辑ToolStripMenuItem_Click);
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统校准ToolStripMenuItem,
            this.iO测试ToolStripMenuItem,
            this.共晶台测试ToolStripMenuItem,
            this.工具ToolStripMenuItem,
            this.维护ToolStripMenuItem});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // 系统校准ToolStripMenuItem
            // 
            this.系统校准ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统初始化ToolStripMenuItem,
            this.手动校准ToolStripMenuItem,
            this.自动校准ToolStripMenuItem});
            this.系统校准ToolStripMenuItem.Name = "系统校准ToolStripMenuItem";
            this.系统校准ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.系统校准ToolStripMenuItem.Text = "系统校准";
            // 
            // 系统初始化ToolStripMenuItem
            // 
            this.系统初始化ToolStripMenuItem.Name = "系统初始化ToolStripMenuItem";
            this.系统初始化ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.系统初始化ToolStripMenuItem.Text = "系统初始化";
            this.系统初始化ToolStripMenuItem.Click += new System.EventHandler(this.系统初始化ToolStripMenuItem_Click);
            // 
            // 手动校准ToolStripMenuItem
            // 
            this.手动校准ToolStripMenuItem.Name = "手动校准ToolStripMenuItem";
            this.手动校准ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.手动校准ToolStripMenuItem.Text = "手动校准";
            this.手动校准ToolStripMenuItem.Click += new System.EventHandler(this.手动校准ToolStripMenuItem_Click);
            // 
            // 自动校准ToolStripMenuItem
            // 
            this.自动校准ToolStripMenuItem.Name = "自动校准ToolStripMenuItem";
            this.自动校准ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.自动校准ToolStripMenuItem.Text = "自动校准";
            this.自动校准ToolStripMenuItem.Click += new System.EventHandler(this.自动校准ToolStripMenuItem_Click);
            // 
            // iO测试ToolStripMenuItem
            // 
            this.iO测试ToolStripMenuItem.Name = "iO测试ToolStripMenuItem";
            this.iO测试ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.iO测试ToolStripMenuItem.Text = "IO测试";
            this.iO测试ToolStripMenuItem.Click += new System.EventHandler(this.iO测试ToolStripMenuItem_Click);
            // 
            // 共晶台测试ToolStripMenuItem
            // 
            this.共晶台测试ToolStripMenuItem.Name = "共晶台测试ToolStripMenuItem";
            this.共晶台测试ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.共晶台测试ToolStripMenuItem.Text = "共晶台测试";
            this.共晶台测试ToolStripMenuItem.Click += new System.EventHandler(this.共晶台测试ToolStripMenuItem_Click);
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pP工具ToolStripMenuItem,
            this.顶针工具ToolStripMenuItem});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.工具ToolStripMenuItem.Text = "工具";
            // 
            // pP工具ToolStripMenuItem
            // 
            this.pP工具ToolStripMenuItem.Name = "pP工具ToolStripMenuItem";
            this.pP工具ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.pP工具ToolStripMenuItem.Text = "PP工具";
            this.pP工具ToolStripMenuItem.Click += new System.EventHandler(this.pP工具ToolStripMenuItem_Click);
            // 
            // 顶针工具ToolStripMenuItem
            // 
            this.顶针工具ToolStripMenuItem.Name = "顶针工具ToolStripMenuItem";
            this.顶针工具ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.顶针工具ToolStripMenuItem.Text = "顶针工具";
            this.顶针工具ToolStripMenuItem.Click += new System.EventHandler(this.顶针工具ToolStripMenuItem_Click);
            // 
            // 维护ToolStripMenuItem
            // 
            this.维护ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.运动ToolStripMenuItem,
            this.iOToolStripMenuItem});
            this.维护ToolStripMenuItem.Name = "维护ToolStripMenuItem";
            this.维护ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.维护ToolStripMenuItem.Text = "维护";
            // 
            // 运动ToolStripMenuItem
            // 
            this.运动ToolStripMenuItem.Name = "运动ToolStripMenuItem";
            this.运动ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.运动ToolStripMenuItem.Text = "运动";
            this.运动ToolStripMenuItem.Click += new System.EventHandler(this.运动ToolStripMenuItem_Click);
            // 
            // iOToolStripMenuItem
            // 
            this.iOToolStripMenuItem.Name = "iOToolStripMenuItem";
            this.iOToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.iOToolStripMenuItem.Text = "IO";
            this.iOToolStripMenuItem.Click += new System.EventHandler(this.iOToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(45, 45);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripBtnCameraControl,
            this.toolStripBtnLightControl,
            this.toolStripButton4,
            this.toolStripButton8});
            this.toolStrip1.Location = new System.Drawing.Point(0, 31);
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
            this.toolStripButton6.ToolTipText = "保存";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(49, 49);
            this.toolStripButton7.Text = "toolStripButton7";
            this.toolStripButton7.ToolTipText = "初始化";
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
            // toolStripBtnLightControl
            // 
            this.toolStripBtnLightControl.CheckOnClick = true;
            this.toolStripBtnLightControl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnLightControl.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnLightControl.Image")));
            this.toolStripBtnLightControl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnLightControl.Name = "toolStripBtnLightControl";
            this.toolStripBtnLightControl.Size = new System.Drawing.Size(49, 49);
            this.toolStripBtnLightControl.Text = "toolStripBtnLightControl";
            this.toolStripBtnLightControl.ToolTipText = "光源";
            this.toolStripBtnLightControl.CheckedChanged += new System.EventHandler(this.toolStripBtnLightControl_CheckedChanged);
            this.toolStripBtnLightControl.Click += new System.EventHandler(this.toolStripBtnLightControl_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(49, 49);
            this.toolStripButton4.Text = "toolStripBtnStageControl";
            this.toolStripButton4.ToolTipText = "运动";
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
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // MainForm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1061);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "贴片机";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生产ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;

        private System.Windows.Forms.ToolStripMenuItem 编程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;        private System.Windows.Forms.ToolStripButton toolStripBtnCameraControl;
        private System.Windows.Forms.ToolStripButton toolStripBtnLightControl;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统校准ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统初始化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 手动校准ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自动校准ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripMenuItem iO测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 共晶台测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 单步ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pP工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 顶针工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 维护ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iOToolStripMenuItem;
    }
}

