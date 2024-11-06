
namespace StageCtrlPanelLib
{
    partial class FrmStageAxisMoveControl
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
            this.stageAxisMoveControlPanel1 = new StageCtrlPanelLib.StageAxisMoveControlPanel();
            this.SuspendLayout();
            // 
            // stageAxisMoveControlPanel1
            // 
            this.stageAxisMoveControlPanel1.CurrentStageAxis = GlobalDataDefineClsLib.EnumStageAxis.MaterialboxX;
            this.stageAxisMoveControlPanel1.Location = new System.Drawing.Point(10, 10);
            this.stageAxisMoveControlPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.stageAxisMoveControlPanel1.Name = "stageAxisMoveControlPanel1";
            this.stageAxisMoveControlPanel1.Size = new System.Drawing.Size(349, 368);
            this.stageAxisMoveControlPanel1.TabIndex = 0;
            // 
            // FrmStageAxisMoveControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 389);
            this.Controls.Add(this.stageAxisMoveControlPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmStageAxisMoveControl";
            this.Text = "轴指令移动";
            this.ResumeLayout(false);

        }

        #endregion

        private StageAxisMoveControlPanel stageAxisMoveControlPanel1;
    }
}