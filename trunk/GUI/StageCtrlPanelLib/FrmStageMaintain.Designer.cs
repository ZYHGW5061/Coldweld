
namespace StageCtrlPanelLib
{
    partial class FrmStageMaintain
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
            this.stageAxisParameterPanel1 = new StageCtrlPanelLib.StageAxisParameterPanel();
            this.stageAxisMoveControlPanel1 = new StageCtrlPanelLib.StageAxisMoveControlPanel();
            this.SuspendLayout();
            // 
            // stageQuickMove1
            // 
            this.stageQuickMove1.Location = new System.Drawing.Point(843, 48);
            this.stageQuickMove1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.stageQuickMove1.Name = "stageQuickMove1";
            this.stageQuickMove1.PositiveQucikMoveAct = null;
            this.stageQuickMove1.SelectedAxisSystem = GlobalDataDefineClsLib.EnumSystemAxis.XY;
            this.stageQuickMove1.SelectedStageSystem = GlobalDataDefineClsLib.EnumStageSystem.MaterialboxHook;
            this.stageQuickMove1.Size = new System.Drawing.Size(359, 396);
            this.stageQuickMove1.TabIndex = 2;
            // 
            // stageAxisParameterPanel1
            // 
            this.stageAxisParameterPanel1.CurrentStageAxis = GlobalDataDefineClsLib.EnumStageAxis.MaterialboxX;
            this.stageAxisParameterPanel1.Location = new System.Drawing.Point(464, 4);
            this.stageAxisParameterPanel1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.stageAxisParameterPanel1.Name = "stageAxisParameterPanel1";
            this.stageAxisParameterPanel1.Size = new System.Drawing.Size(323, 485);
            this.stageAxisParameterPanel1.TabIndex = 1;
            // 
            // stageAxisMoveControlPanel1
            // 
            this.stageAxisMoveControlPanel1.CurrentStageAxis = GlobalDataDefineClsLib.EnumStageAxis.MaterialboxX;
            this.stageAxisMoveControlPanel1.Location = new System.Drawing.Point(3, 4);
            this.stageAxisMoveControlPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.stageAxisMoveControlPanel1.Name = "stageAxisMoveControlPanel1";
            this.stageAxisMoveControlPanel1.Size = new System.Drawing.Size(453, 485);
            this.stageAxisMoveControlPanel1.TabIndex = 0;
            // 
            // FrmStageMaintain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 508);
            this.Controls.Add(this.stageQuickMove1);
            this.Controls.Add(this.stageAxisParameterPanel1);
            this.Controls.Add(this.stageAxisMoveControlPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStageMaintain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "运动控制";
            this.ResumeLayout(false);

        }

        #endregion

        private StageAxisMoveControlPanel stageAxisMoveControlPanel1;
        private StageAxisParameterPanel stageAxisParameterPanel1;
        private StageQuickMove stageQuickMove1;
    }
}