﻿
namespace StageCtrlPanelLib
{
    partial class FrmStageControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.stageQuickMove1 = new StageCtrlPanelLib.StageQuickMove();
            this.SuspendLayout();
            // 
            // stageQuickMove1
            // 
            this.stageQuickMove1.Location = new System.Drawing.Point(20, 14);
            this.stageQuickMove1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.stageQuickMove1.Name = "stageQuickMove1";
            this.stageQuickMove1.PositiveQucikMoveAct = null;
            this.stageQuickMove1.SelectedAxisSystem = GlobalDataDefineClsLib.EnumSystemAxis.XY;
            this.stageQuickMove1.SelectedStageSystem = GlobalDataDefineClsLib.EnumStageSystem.MaterialboxHook;
            this.stageQuickMove1.Size = new System.Drawing.Size(359, 396);
            this.stageQuickMove1.TabIndex = 0;
            // 
            // FrmStageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 428);
            this.Controls.Add(this.stageQuickMove1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStageControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "轴点动";
            this.ResumeLayout(false);

        }

        #endregion

        private StageCtrlPanelLib.StageQuickMove stageQuickMove1;
    }
}