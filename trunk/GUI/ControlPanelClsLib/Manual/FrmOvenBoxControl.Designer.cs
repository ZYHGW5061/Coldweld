
namespace ControlPanelClsLib
{
    partial class FrmOvenBoxControl
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
            this.ovenBoxControlPanel21 = new ControlPanelClsLib.Manual.OvenBoxControlPanel2();
            this.SuspendLayout();
            // 
            // ovenBoxControlPanel21
            // 
            this.ovenBoxControlPanel21.Location = new System.Drawing.Point(13, 13);
            this.ovenBoxControlPanel21.Name = "ovenBoxControlPanel21";
            this.ovenBoxControlPanel21.Size = new System.Drawing.Size(1500, 878);
            this.ovenBoxControlPanel21.TabIndex = 0;
            // 
            // FrmOvenBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1522, 893);
            this.Controls.Add(this.ovenBoxControlPanel21);
            this.Name = "FrmOvenBoxControl";
            this.Text = "烘箱控制";
            this.ResumeLayout(false);

        }

        #endregion

        private Manual.OvenBoxControlPanel2 ovenBoxControlPanel21;
    }
}