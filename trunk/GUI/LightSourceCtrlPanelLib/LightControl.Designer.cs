
namespace LightSourceCtrlPanelLib
{
    partial class LightControl
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
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbSelectLight
            // 
            this.cmbSelectLight.FormattingEnabled = true;
            this.cmbSelectLight.Location = new System.Drawing.Point(87, 14);
            this.cmbSelectLight.Name = "cmbSelectLight";
            this.cmbSelectLight.Size = new System.Drawing.Size(176, 22);
            this.cmbSelectLight.TabIndex = 47;
            this.cmbSelectLight.Text = "WaferRingField";
            this.cmbSelectLight.SelectedIndexChanged += new System.EventHandler(this.cmbSelectLight_SelectedIndexChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(23, 16);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 14);
            this.labelControl4.TabIndex = 48;
            this.labelControl4.Text = "选择光源:";
            // 
            // ctrlLight1
            // 
            this.ctrlLight1.ApplyIntensityToHardware = true;
            this.ctrlLight1.BrightFieldBrightnessChanged = null;
            this.ctrlLight1.Brightness = 0F;
            this.ctrlLight1.CurrentLightType = GlobalDataDefineClsLib.EnumLightSourceType.TrackRingField;
            this.ctrlLight1.Location = new System.Drawing.Point(21, 65);
            this.ctrlLight1.Name = "ctrlLight1";
            this.ctrlLight1.Size = new System.Drawing.Size(242, 70);
            this.ctrlLight1.TabIndex = 49;
            // 
            // panelControl6
            // 
            this.panelControl6.Controls.Add(this.cmbSelectLight);
            this.panelControl6.Controls.Add(this.ctrlLight1);
            this.panelControl6.Controls.Add(this.labelControl4);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl6.Location = new System.Drawing.Point(0, 0);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(286, 151);
            this.panelControl6.TabIndex = 50;
            // 
            // LightControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl6);
            this.Name = "LightControl";
            this.Size = new System.Drawing.Size(286, 151);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            this.panelControl6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSelectLight;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private LightSourceCtrlPanelLib.CtrlLight ctrlLight1;
        private DevExpress.XtraEditors.PanelControl panelControl6;
    }
}