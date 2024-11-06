
namespace VisionGUI
{
    partial class CameraWindowGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraWindowGUI));
            this.CameraWindow = new System.Windows.Forms.PictureBox();
            this.CameraControltoolStrip = new System.Windows.Forms.ToolStrip();
            this.CameraComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.CrossBtn = new System.Windows.Forms.ToolStripButton();
            this.GridBtn = new System.Windows.Forms.ToolStripButton();
            this.ClearBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripPixelCoordinates = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.CameraWindow)).BeginInit();
            this.CameraControltoolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // CameraWindow
            // 
            this.CameraWindow.Location = new System.Drawing.Point(4, 68);
            this.CameraWindow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CameraWindow.Name = "CameraWindow";
            this.CameraWindow.Size = new System.Drawing.Size(900, 680);
            this.CameraWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CameraWindow.TabIndex = 0;
            this.CameraWindow.TabStop = false;
            this.CameraWindow.Paint += new System.Windows.Forms.PaintEventHandler(this.CameraWindow_Paint);
            this.CameraWindow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CameraWindow_MouseMove);
            // 
            // CameraControltoolStrip
            // 
            this.CameraControltoolStrip.AutoSize = false;
            this.CameraControltoolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.CameraControltoolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.CameraControltoolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CameraComboBox,
            this.CrossBtn,
            this.GridBtn,
            this.ClearBtn,
            this.toolStripPixelCoordinates});
            this.CameraControltoolStrip.Location = new System.Drawing.Point(0, 0);
            this.CameraControltoolStrip.Name = "CameraControltoolStrip";
            this.CameraControltoolStrip.Size = new System.Drawing.Size(909, 60);
            this.CameraControltoolStrip.TabIndex = 1;
            this.CameraControltoolStrip.Text = "toolStrip1";
            // 
            // CameraComboBox
            // 
            this.CameraComboBox.AutoSize = false;
            this.CameraComboBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.CameraComboBox.Name = "CameraComboBox";
            this.CameraComboBox.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.CameraComboBox.Size = new System.Drawing.Size(39, 35);
            this.CameraComboBox.Text = "TrackCamera";
            this.CameraComboBox.SelectedIndexChanged += new System.EventHandler(this.CameraComboBox_SelectedIndexChanged);
            // 
            // CrossBtn
            // 
            this.CrossBtn.AutoSize = false;
            this.CrossBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CrossBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.CrossBtn.Image = ((System.Drawing.Image)(resources.GetObject("CrossBtn.Image")));
            this.CrossBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CrossBtn.Name = "CrossBtn";
            this.CrossBtn.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.CrossBtn.Size = new System.Drawing.Size(106, 57);
            this.CrossBtn.Text = "十字";
            this.CrossBtn.CheckedChanged += new System.EventHandler(this.CrossBtn_CheckedChanged);
            this.CrossBtn.Click += new System.EventHandler(this.CrossBtn_Click);
            // 
            // GridBtn
            // 
            this.GridBtn.AutoSize = false;
            this.GridBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.GridBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.GridBtn.Image = ((System.Drawing.Image)(resources.GetObject("GridBtn.Image")));
            this.GridBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GridBtn.Name = "GridBtn";
            this.GridBtn.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.GridBtn.Size = new System.Drawing.Size(106, 57);
            this.GridBtn.Text = "网格";
            this.GridBtn.CheckedChanged += new System.EventHandler(this.GridBtn_CheckedChanged);
            this.GridBtn.Click += new System.EventHandler(this.GridBtn_Click);
            // 
            // ClearBtn
            // 
            this.ClearBtn.AutoSize = false;
            this.ClearBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ClearBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.ClearBtn.Image = ((System.Drawing.Image)(resources.GetObject("ClearBtn.Image")));
            this.ClearBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.ClearBtn.Size = new System.Drawing.Size(106, 57);
            this.ClearBtn.Text = "刷新";
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // toolStripPixelCoordinates
            // 
            this.toolStripPixelCoordinates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripPixelCoordinates.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPixelCoordinates.Image")));
            this.toolStripPixelCoordinates.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripPixelCoordinates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPixelCoordinates.Name = "toolStripPixelCoordinates";
            this.toolStripPixelCoordinates.Padding = new System.Windows.Forms.Padding(200, 0, 0, 0);
            this.toolStripPixelCoordinates.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripPixelCoordinates.Size = new System.Drawing.Size(287, 57);
            this.toolStripPixelCoordinates.Text = "(X:0,Y:0)(0)";
            this.toolStripPixelCoordinates.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CameraWindowGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CameraControltoolStrip);
            this.Controls.Add(this.CameraWindow);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CameraWindowGUI";
            this.Size = new System.Drawing.Size(909, 755);
            ((System.ComponentModel.ISupportInitialize)(this.CameraWindow)).EndInit();
            this.CameraControltoolStrip.ResumeLayout(false);
            this.CameraControltoolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox CameraWindow;
        private System.Windows.Forms.ToolStrip CameraControltoolStrip;
        private System.Windows.Forms.ToolStripComboBox CameraComboBox;
        private System.Windows.Forms.ToolStripButton CrossBtn;
        private System.Windows.Forms.ToolStripButton GridBtn;
        private System.Windows.Forms.ToolStripButton ClearBtn;
        private System.Windows.Forms.ToolStripLabel toolStripPixelCoordinates;
    }
}
