
namespace VisionGUI
{
    partial class VisualControlForm
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
            this.VisualTabControl = new System.Windows.Forms.TabControl();
            this.StageMovetabPage = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.NextBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.stageQuickMove1 = new StageCtrlPanelLib.StageQuickMove();
            this.SuspendLayout();
            // 
            // VisualTabControl
            // 
            this.VisualTabControl.Location = new System.Drawing.Point(0, 0);
            this.VisualTabControl.Name = "VisualTabControl";
            this.VisualTabControl.SelectedIndex = 0;
            this.VisualTabControl.Size = new System.Drawing.Size(200, 100);
            this.VisualTabControl.TabIndex = 0;
            // 
            // StageMovetabPage
            // 
            this.StageMovetabPage.Location = new System.Drawing.Point(0, 0);
            this.StageMovetabPage.Name = "StageMovetabPage";
            this.StageMovetabPage.Size = new System.Drawing.Size(200, 100);
            this.StageMovetabPage.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 25);
            this.textBox1.TabIndex = 0;
            // 
            // NextBtn
            // 
            this.NextBtn.Location = new System.Drawing.Point(0, 0);
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.Size = new System.Drawing.Size(75, 23);
            this.NextBtn.TabIndex = 0;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(0, 0);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 0;
            // 
            // stageQuickMove1
            // 
            this.stageQuickMove1.Location = new System.Drawing.Point(0, 0);
            this.stageQuickMove1.Name = "stageQuickMove1";
            this.stageQuickMove1.PositiveQucikMoveAct = null;
            this.stageQuickMove1.SelectedAxisSystem = GlobalDataDefineClsLib.EnumSystemAxis.XY;
            this.stageQuickMove1.SelectedStageSystem = GlobalDataDefineClsLib.EnumStageSystem.MaterialboxHook;
            this.stageQuickMove1.Size = new System.Drawing.Size(269, 317);
            this.stageQuickMove1.TabIndex = 0;
            // 
            // VisualControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 962);
            this.Location = new System.Drawing.Point(1550, 150);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualControlForm";
            this.Text = "VisualControlForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl VisualTabControl;
        private System.Windows.Forms.TabPage VisualtabPage;
        private System.Windows.Forms.TabPage StageMovetabPage;
        private System.Windows.Forms.TextBox textBox1;
        //private VisualMatchControlGUI visualMatchControlGUI1;
        private StageCtrlPanelLib.StageQuickMove stageQuickMove1;
        private System.Windows.Forms.Button NextBtn;
        private System.Windows.Forms.Button CancelBtn;
        //private VisualMatchControlGUI visualMatchControlGUI1;
    }
}