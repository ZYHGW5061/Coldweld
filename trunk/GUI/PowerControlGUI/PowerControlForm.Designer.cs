﻿
namespace PowerControlGUI
{
    partial class PowerControlForm
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
            this.powerControlPanel1 = new PowerControlGUI.PowerControlPanel();
            this.SuspendLayout();
            // 
            // powerControlPanel1
            // 
            this.powerControlPanel1.Location = new System.Drawing.Point(12, 12);
            this.powerControlPanel1.Name = "powerControlPanel1";
            this.powerControlPanel1.Size = new System.Drawing.Size(1070, 239);
            this.powerControlPanel1.TabIndex = 0;
            // 
            // PowerControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 267);
            this.Controls.Add(this.powerControlPanel1);
            this.Name = "PowerControlForm";
            this.Text = "PowerControlForm";
            this.ResumeLayout(false);

        }

        #endregion

        private PowerControlPanel powerControlPanel1;
    }
}