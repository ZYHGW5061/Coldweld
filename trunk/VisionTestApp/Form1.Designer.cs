﻿
namespace VisionTestApp
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.相机ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CameraWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.识别ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bonding轮廓识别ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BondingMatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BondingLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BondingCircleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bonding边缘识别ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bonding圆识别ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.相机ToolStripMenuItem,
            this.识别ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 相机ToolStripMenuItem
            // 
            this.相机ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CameraWindowToolStripMenuItem});
            this.相机ToolStripMenuItem.Name = "相机ToolStripMenuItem";
            this.相机ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.相机ToolStripMenuItem.Text = "相机";
            // 
            // CameraWindowToolStripMenuItem
            // 
            this.CameraWindowToolStripMenuItem.CheckOnClick = true;
            this.CameraWindowToolStripMenuItem.Name = "CameraWindowToolStripMenuItem";
            this.CameraWindowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.CameraWindowToolStripMenuItem.Text = "相机窗口";
            this.CameraWindowToolStripMenuItem.CheckedChanged += new System.EventHandler(this.CameraWindowToolStripMenuItem_CheckedChanged);
            // 
            // 识别ToolStripMenuItem
            // 
            this.识别ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bonding轮廓识别ToolStripMenuItem,
            this.bonding边缘识别ToolStripMenuItem,
            this.bonding圆识别ToolStripMenuItem});
            this.识别ToolStripMenuItem.Name = "识别ToolStripMenuItem";
            this.识别ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.识别ToolStripMenuItem.Text = "识别";
            // 
            // bonding轮廓识别ToolStripMenuItem
            // 
            this.bonding轮廓识别ToolStripMenuItem.CheckOnClick = true;
            this.bonding轮廓识别ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BondingMatchToolStripMenuItem,
            this.BondingLineToolStripMenuItem,
            this.BondingCircleToolStripMenuItem});
            this.bonding轮廓识别ToolStripMenuItem.Name = "bonding轮廓识别ToolStripMenuItem";
            this.bonding轮廓识别ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bonding轮廓识别ToolStripMenuItem.Text = "Bonding识别";
            // 
            // BondingMatchToolStripMenuItem
            // 
            this.BondingMatchToolStripMenuItem.CheckOnClick = true;
            this.BondingMatchToolStripMenuItem.Name = "BondingMatchToolStripMenuItem";
            this.BondingMatchToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.BondingMatchToolStripMenuItem.Text = "轮廓";
            this.BondingMatchToolStripMenuItem.CheckedChanged += new System.EventHandler(this.BondingMatchToolStripMenuItem_CheckedChanged);
            // 
            // BondingLineToolStripMenuItem
            // 
            this.BondingLineToolStripMenuItem.CheckOnClick = true;
            this.BondingLineToolStripMenuItem.Name = "BondingLineToolStripMenuItem";
            this.BondingLineToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.BondingLineToolStripMenuItem.Text = "边缘";
            this.BondingLineToolStripMenuItem.CheckedChanged += new System.EventHandler(this.BondingLineToolStripMenuItem_CheckedChanged);
            // 
            // BondingCircleToolStripMenuItem
            // 
            this.BondingCircleToolStripMenuItem.CheckOnClick = true;
            this.BondingCircleToolStripMenuItem.Name = "BondingCircleToolStripMenuItem";
            this.BondingCircleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.BondingCircleToolStripMenuItem.Text = "圆";
            this.BondingCircleToolStripMenuItem.CheckedChanged += new System.EventHandler(this.BondingCircleToolStripMenuItem_CheckedChanged);
            // 
            // bonding边缘识别ToolStripMenuItem
            // 
            this.bonding边缘识别ToolStripMenuItem.Name = "bonding边缘识别ToolStripMenuItem";
            this.bonding边缘识别ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bonding边缘识别ToolStripMenuItem.Text = "Bonding边缘识别";
            // 
            // bonding圆识别ToolStripMenuItem
            // 
            this.bonding圆识别ToolStripMenuItem.Name = "bonding圆识别ToolStripMenuItem";
            this.bonding圆识别ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bonding圆识别ToolStripMenuItem.Text = "Bonding圆识别";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 相机ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CameraWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 识别ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bonding轮廓识别ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bonding边缘识别ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bonding圆识别ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BondingMatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BondingLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BondingCircleToolStripMenuItem;
    }
}

