
namespace VisionGUI
{
    partial class CameraParamForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.ImageWidthPixelsizetextBox = new System.Windows.Forms.TextBox();
            this.ImageHeightPixelsizetextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ImageAngletextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "相机横向分辨率:";
            // 
            // ImageWidthPixelsizetextBox
            // 
            this.ImageWidthPixelsizetextBox.Location = new System.Drawing.Point(231, 20);
            this.ImageWidthPixelsizetextBox.Name = "ImageWidthPixelsizetextBox";
            this.ImageWidthPixelsizetextBox.Size = new System.Drawing.Size(140, 25);
            this.ImageWidthPixelsizetextBox.TabIndex = 1;
            this.ImageWidthPixelsizetextBox.Text = "1";
            this.ImageWidthPixelsizetextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ImageHeightPixelsizetextBox
            // 
            this.ImageHeightPixelsizetextBox.Location = new System.Drawing.Point(231, 79);
            this.ImageHeightPixelsizetextBox.Name = "ImageHeightPixelsizetextBox";
            this.ImageHeightPixelsizetextBox.Size = new System.Drawing.Size(140, 25);
            this.ImageHeightPixelsizetextBox.TabIndex = 3;
            this.ImageHeightPixelsizetextBox.Text = "1";
            this.ImageHeightPixelsizetextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "相机纵向分辨率:";
            // 
            // ImageAngletextBox
            // 
            this.ImageAngletextBox.Location = new System.Drawing.Point(231, 138);
            this.ImageAngletextBox.Name = "ImageAngletextBox";
            this.ImageAngletextBox.Size = new System.Drawing.Size(140, 25);
            this.ImageAngletextBox.TabIndex = 5;
            this.ImageAngletextBox.Text = "0";
            this.ImageAngletextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(120, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "相机角度:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 210);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 73);
            this.button1.TabIndex = 6;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(244, 210);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 73);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CameraParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(425, 315);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ImageAngletextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ImageHeightPixelsizetextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ImageWidthPixelsizetextBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CameraParamForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "相机参数设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ImageWidthPixelsizetextBox;
        private System.Windows.Forms.TextBox ImageHeightPixelsizetextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ImageAngletextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}