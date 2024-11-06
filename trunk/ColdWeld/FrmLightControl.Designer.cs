
namespace BondTerminal
{
    partial class FrmLightControl
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
            this.cmbSelectLight = new System.Windows.Forms.ComboBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.ctrlLight1 = new LightSourceCtrlPanelLib.CtrlLight();
            this.SuspendLayout();
            // 
            // cmbSelectLight
            // 
            this.cmbSelectLight.FormattingEnabled = true;
            this.cmbSelectLight.Location = new System.Drawing.Point(117, 25);
            this.cmbSelectLight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbSelectLight.Name = "cmbSelectLight";
            this.cmbSelectLight.Size = new System.Drawing.Size(264, 23);
            this.cmbSelectLight.TabIndex = 47;
            this.cmbSelectLight.Text = "TrackRingField";
            this.cmbSelectLight.SelectedIndexChanged += new System.EventHandler(this.cmbSelectLight_SelectedIndexChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(32, 28);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(65, 18);
            this.labelControl4.TabIndex = 48;
            this.labelControl4.Text = "选择光源:";
            // 
            // ctrlLight1
            // 
            this.ctrlLight1.ApplyIntensityToHardware = true;
            this.ctrlLight1.BrightFieldBrightnessChanged = null;
            this.ctrlLight1.Brightness = 0F;
            this.ctrlLight1.CurrentLightType = GlobalDataDefineClsLib.EnumLightSourceType.TrackRingField;
            this.ctrlLight1.Location = new System.Drawing.Point(29, 89);
            this.ctrlLight1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ctrlLight1.Name = "ctrlLight1";
            this.ctrlLight1.Size = new System.Drawing.Size(353, 88);
            this.ctrlLight1.TabIndex = 49;
            // 
            // FrmLightControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 196);
            this.Controls.Add(this.ctrlLight1);
            this.Controls.Add(this.cmbSelectLight);
            this.Controls.Add(this.labelControl4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLightControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "光源";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSelectLight;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private LightSourceCtrlPanelLib.CtrlLight ctrlLight1;
    }
}