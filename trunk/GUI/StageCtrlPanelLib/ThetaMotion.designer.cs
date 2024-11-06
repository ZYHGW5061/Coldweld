
namespace StageCtrlPanelLib
{
    partial class ThetaMotion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThetaMotion));
            this.btnMainTContourclockwise90Degree = new DevExpress.XtraEditors.SimpleButton();
            this.btnMianTClockwise90Degree = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnMainTContourclockwise90Degree
            // 
            this.btnMainTContourclockwise90Degree.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMainTContourclockwise90Degree.ImageOptions.Image")));
            this.btnMainTContourclockwise90Degree.Location = new System.Drawing.Point(108, 12);
            this.btnMainTContourclockwise90Degree.Name = "btnMainTContourclockwise90Degree";
            this.btnMainTContourclockwise90Degree.Size = new System.Drawing.Size(86, 40);
            this.btnMainTContourclockwise90Degree.TabIndex = 0;
            this.btnMainTContourclockwise90Degree.Text = "90度";
            this.btnMainTContourclockwise90Degree.Click += new System.EventHandler(this.btnMainTContourclockwise90Degree_Click);
            this.btnMainTContourclockwise90Degree.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMainTContourclockwise90Degree_MouseUp);
            // 
            // btnMianTClockwise90Degree
            // 
            this.btnMianTClockwise90Degree.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMianTClockwise90Degree.ImageOptions.Image")));
            this.btnMianTClockwise90Degree.Location = new System.Drawing.Point(13, 12);
            this.btnMianTClockwise90Degree.Name = "btnMianTClockwise90Degree";
            this.btnMianTClockwise90Degree.Size = new System.Drawing.Size(86, 40);
            this.btnMianTClockwise90Degree.TabIndex = 0;
            this.btnMianTClockwise90Degree.Text = "90度";
            this.btnMianTClockwise90Degree.Click += new System.EventHandler(this.btnMianTClockwise90Degree_Click);
            this.btnMianTClockwise90Degree.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMianTClockwise90Degree_MouseUp);
            // 
            // ThetaMotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnMianTClockwise90Degree);
            this.Controls.Add(this.btnMainTContourclockwise90Degree);
            this.Name = "ThetaMotion";
            this.Size = new System.Drawing.Size(208, 64);
            this.Load += new System.EventHandler(this.StageTMotionController_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnMainTContourclockwise90Degree;
        private DevExpress.XtraEditors.SimpleButton btnMianTClockwise90Degree;
    }
}
