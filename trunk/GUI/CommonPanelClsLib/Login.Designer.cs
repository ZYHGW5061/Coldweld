namespace CommonPanelClsLib
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelName = new DevExpress.XtraEditors.LabelControl();
            this.labelPassWord = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtPassWord = new DevExpress.XtraEditors.TextEdit();
            this.butCancel = new DevExpress.XtraEditors.SimpleButton();
            this.butLogin = new DevExpress.XtraEditors.SimpleButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.30981F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.3338F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.35639F));
            this.tableLayoutPanel1.Controls.Add(this.labelName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelPassWord, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPassWord, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.butCancel, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.butLogin, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(28, 23);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(544, 194);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelName
            // 
            this.labelName.Appearance.Options.UseTextOptions = true;
            this.labelName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Location = new System.Drawing.Point(196, 2);
            this.labelName.Margin = new System.Windows.Forms.Padding(2);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(112, 60);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "用户名:";
            // 
            // labelPassWord
            // 
            this.labelPassWord.Appearance.Options.UseTextOptions = true;
            this.labelPassWord.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelPassWord.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelPassWord.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelPassWord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPassWord.Location = new System.Drawing.Point(196, 66);
            this.labelPassWord.Margin = new System.Windows.Forms.Padding(2);
            this.labelPassWord.Name = "labelPassWord";
            this.labelPassWord.Size = new System.Drawing.Size(112, 60);
            this.labelPassWord.TabIndex = 1;
            this.labelPassWord.Text = "密码:";
            // 
            // txtName
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtName, 2);
            this.txtName.Location = new System.Drawing.Point(312, 18);
            this.txtName.Margin = new System.Windows.Forms.Padding(2, 18, 2, 8);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtName.Properties.Appearance.Options.UseBackColor = true;
            this.txtName.Properties.AutoHeight = false;
            this.txtName.Size = new System.Drawing.Size(218, 28);
            this.txtName.TabIndex = 2;
            // 
            // txtPassWord
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtPassWord, 2);
            this.txtPassWord.Location = new System.Drawing.Point(312, 82);
            this.txtPassWord.Margin = new System.Windows.Forms.Padding(2, 18, 2, 8);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPassWord.Properties.Appearance.Options.UseBackColor = true;
            this.txtPassWord.Properties.AutoHeight = false;
            this.txtPassWord.Properties.PasswordChar = '*';
            this.txtPassWord.Properties.UseSystemPasswordChar = true;
            this.txtPassWord.Size = new System.Drawing.Size(218, 28);
            this.txtPassWord.TabIndex = 3;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(421, 140);
            this.butCancel.Margin = new System.Windows.Forms.Padding(2, 12, 2, 12);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(84, 28);
            this.butCancel.TabIndex = 6;
            this.butCancel.Text = "取消";
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butLogin
            // 
            this.butLogin.Location = new System.Drawing.Point(312, 140);
            this.butLogin.Margin = new System.Windows.Forms.Padding(2, 12, 2, 12);
            this.butLogin.Name = "butLogin";
            this.butLogin.Size = new System.Drawing.Size(84, 28);
            this.butLogin.TabIndex = 5;
            this.butLogin.Text = "登录";
            this.butLogin.Click += new System.EventHandler(this.butLogin_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(32, 24);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(32, 16, 32, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBox1, 3);
            this.pictureBox1.Size = new System.Drawing.Size(130, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // Login
            // 
            this.AcceptButton = this.butLogin;
            this.Appearance.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(599, 235);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.LookAndFeel.SkinName = "Office 2013 Dark Gray";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtPassWord;
        private DevExpress.XtraEditors.LabelControl labelPassWord;
        private DevExpress.XtraEditors.LabelControl labelName;
        private DevExpress.XtraEditors.SimpleButton butLogin;
        private DevExpress.XtraEditors.SimpleButton butCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}