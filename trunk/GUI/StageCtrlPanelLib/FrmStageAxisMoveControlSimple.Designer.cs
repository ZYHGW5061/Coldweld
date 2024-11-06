
namespace StageCtrlPanelLib
{
    partial class FrmStageAxisMoveControlSimple
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
            this.stageAxisMoveControlPanelSimple1 = new StageCtrlPanelLib.StageAxisMoveControlPanelSimple();
            this.SuspendLayout();
            // 
            // stageAxisMoveControlPanelSimple1
            // 
            this.stageAxisMoveControlPanelSimple1.CurrentStageAxis = GlobalDataDefineClsLib.EnumStageAxis.BondX;
            this.stageAxisMoveControlPanelSimple1.Location = new System.Drawing.Point(13, 13);
            this.stageAxisMoveControlPanelSimple1.Name = "stageAxisMoveControlPanelSimple1";
            this.stageAxisMoveControlPanelSimple1.Size = new System.Drawing.Size(340, 130);
            this.stageAxisMoveControlPanelSimple1.TabIndex = 0;
            // 
            // FrmStageAxisMoveControlSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 153);
            this.Controls.Add(this.stageAxisMoveControlPanelSimple1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStageAxisMoveControlSimple";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "轴绝对移动";
            this.ResumeLayout(false);

        }

        #endregion

        private StageAxisMoveControlPanelSimple stageAxisMoveControlPanelSimple1;
    }
}