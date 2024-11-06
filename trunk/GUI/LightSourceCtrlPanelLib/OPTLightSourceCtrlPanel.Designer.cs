namespace Skyverse.Sweetgum.BacksideTerminal.LightSourceCtrl
{
    partial class OPTLightSourceCtrlPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.spinEdit_Intensity = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnSetValue = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEditGetValule = new DevExpress.XtraEditors.TextEdit();
            this.btnGetValue = new DevExpress.XtraEditors.SimpleButton();
            this.btnLightOn = new DevExpress.XtraEditors.SimpleButton();
            this.btnTurnOff = new DevExpress.XtraEditors.SimpleButton();
            this.BtnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDisconnect = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSteIP = new DevExpress.XtraEditors.SimpleButton();
            this.cbChannel = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkEditBrightField = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditFrontLight = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit_Intensity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGetValule.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbChannel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditBrightField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFrontLight.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // spinEdit_Intensity
            // 
            this.spinEdit_Intensity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.spinEdit_Intensity.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spinEdit_Intensity.Location = new System.Drawing.Point(101, 98);
            this.spinEdit_Intensity.Name = "spinEdit_Intensity";
            this.spinEdit_Intensity.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.spinEdit_Intensity.Properties.AutoHeight = false;
            this.spinEdit_Intensity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown)});
            this.spinEdit_Intensity.Properties.DisplayFormat.FormatString = "0.0000";
            this.spinEdit_Intensity.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit_Intensity.Properties.EditFormat.FormatString = "0.0000";
            this.spinEdit_Intensity.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit_Intensity.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.spinEdit_Intensity.Properties.Mask.EditMask = "f4";
            this.spinEdit_Intensity.Size = new System.Drawing.Size(78, 26);
            this.spinEdit_Intensity.TabIndex = 14;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelControl2.Location = new System.Drawing.Point(20, 15);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 14);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Get Intensity:";
            // 
            // btnSetValue
            // 
            this.btnSetValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSetValue.Location = new System.Drawing.Point(278, 96);
            this.btnSetValue.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetValue.Name = "btnSetValue";
            this.btnSetValue.Size = new System.Drawing.Size(68, 30);
            this.btnSetValue.TabIndex = 5;
            this.btnSetValue.Text = "SetValue";
            this.btnSetValue.Click += new System.EventHandler(this.btnSetValue_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelControl3.Location = new System.Drawing.Point(8, 60);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(88, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Channel Option:";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelControl1.Location = new System.Drawing.Point(21, 104);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Set Intensity:";
            // 
            // textEditGetValule
            // 
            this.textEditGetValule.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textEditGetValule.Location = new System.Drawing.Point(100, 10);
            this.textEditGetValule.Margin = new System.Windows.Forms.Padding(2);
            this.textEditGetValule.Name = "textEditGetValule";
            this.textEditGetValule.Properties.AutoHeight = false;
            this.textEditGetValule.Size = new System.Drawing.Size(80, 25);
            this.textEditGetValule.TabIndex = 3;
            // 
            // btnGetValue
            // 
            this.btnGetValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGetValue.Location = new System.Drawing.Point(278, 7);
            this.btnGetValue.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetValue.Name = "btnGetValue";
            this.btnGetValue.Size = new System.Drawing.Size(68, 30);
            this.btnGetValue.TabIndex = 5;
            this.btnGetValue.Text = "GetValue";
            this.btnGetValue.Click += new System.EventHandler(this.btnGetValue_Click);
            // 
            // btnLightOn
            // 
            this.btnLightOn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLightOn.Location = new System.Drawing.Point(15, 155);
            this.btnLightOn.Margin = new System.Windows.Forms.Padding(2);
            this.btnLightOn.Name = "btnLightOn";
            this.btnLightOn.Size = new System.Drawing.Size(68, 30);
            this.btnLightOn.TabIndex = 0;
            this.btnLightOn.Text = "TurnOnCh";
            this.btnLightOn.Click += new System.EventHandler(this.btnLightOn_Click);
            // 
            // btnTurnOff
            // 
            this.btnTurnOff.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTurnOff.Location = new System.Drawing.Point(106, 155);
            this.btnTurnOff.Margin = new System.Windows.Forms.Padding(2);
            this.btnTurnOff.Name = "btnTurnOff";
            this.btnTurnOff.Size = new System.Drawing.Size(68, 30);
            this.btnTurnOff.TabIndex = 0;
            this.btnTurnOff.Text = "TurnOffCh";
            this.btnTurnOff.Click += new System.EventHandler(this.btnLightOff_Click);
            // 
            // BtnConnect
            // 
            this.BtnConnect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnConnect.Location = new System.Drawing.Point(184, 155);
            this.BtnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(68, 30);
            this.BtnConnect.TabIndex = 0;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // BtnDisconnect
            // 
            this.BtnDisconnect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnDisconnect.Location = new System.Drawing.Point(278, 155);
            this.BtnDisconnect.Margin = new System.Windows.Forms.Padding(2);
            this.BtnDisconnect.Name = "BtnDisconnect";
            this.BtnDisconnect.Size = new System.Drawing.Size(68, 30);
            this.BtnDisconnect.TabIndex = 1;
            this.BtnDisconnect.Text = "Disconnect";
            this.BtnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl4.Location = new System.Drawing.Point(184, 15);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(47, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "(0~255)";
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl5.Location = new System.Drawing.Point(184, 60);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(0, 14);
            this.labelControl5.TabIndex = 4;
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl6.Location = new System.Drawing.Point(184, 104);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(47, 14);
            this.labelControl6.TabIndex = 4;
            this.labelControl6.Text = "(0~255)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.92157F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.07843F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanel1.Controls.Add(this.btnSteIP, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.spinEdit_Intensity, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSetValue, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textEditGetValule, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnGetValue, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnLightOn, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnTurnOff, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.BtnConnect, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.BtnDisconnect, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelControl4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelControl6, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbChannel, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 41);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(369, 194);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnSteIP
            // 
            this.btnSteIP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSteIP.Location = new System.Drawing.Point(278, 52);
            this.btnSteIP.Margin = new System.Windows.Forms.Padding(2);
            this.btnSteIP.Name = "btnSteIP";
            this.btnSteIP.Size = new System.Drawing.Size(68, 30);
            this.btnSteIP.TabIndex = 3;
            this.btnSteIP.Text = "SetIP";
            this.btnSteIP.Visible = false;
            this.btnSteIP.Click += new System.EventHandler(this.btnSteIP_Click);
            // 
            // cbChannel
            // 
            this.cbChannel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbChannel.EditValue = "0";
            this.cbChannel.Location = new System.Drawing.Point(100, 55);
            this.cbChannel.Margin = new System.Windows.Forms.Padding(2);
            this.cbChannel.Name = "cbChannel";
            this.cbChannel.Properties.AutoHeight = false;
            this.cbChannel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbChannel.Properties.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cbChannel.Size = new System.Drawing.Size(80, 25);
            this.cbChannel.TabIndex = 15;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.checkEditBrightField);
            this.groupControl1.Controls.Add(this.checkEditFrontLight);
            this.groupControl1.Controls.Add(this.tableLayoutPanel1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(396, 261);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "OPT";
            // 
            // checkEditBrightField
            // 
            this.checkEditBrightField.Location = new System.Drawing.Point(204, 22);
            this.checkEditBrightField.Margin = new System.Windows.Forms.Padding(2);
            this.checkEditBrightField.Name = "checkEditBrightField";
            this.checkEditBrightField.Properties.Caption = "BrightField";
            this.checkEditBrightField.Properties.RadioGroupIndex = 1;
            this.checkEditBrightField.Size = new System.Drawing.Size(169, 19);
            this.checkEditBrightField.TabIndex = 2;
            this.checkEditBrightField.TabStop = false;
            this.checkEditBrightField.CheckedChanged += new System.EventHandler(this.checkEditBrightField_CheckedChanged);
            // 
            // checkEditFrontLight
            // 
            this.checkEditFrontLight.EditValue = true;
            this.checkEditFrontLight.Location = new System.Drawing.Point(20, 22);
            this.checkEditFrontLight.Margin = new System.Windows.Forms.Padding(2);
            this.checkEditFrontLight.Name = "checkEditFrontLight";
            this.checkEditFrontLight.Properties.Caption = "FrontDarkField";
            this.checkEditFrontLight.Properties.RadioGroupIndex = 1;
            this.checkEditFrontLight.Size = new System.Drawing.Size(160, 19);
            this.checkEditFrontLight.TabIndex = 1;
            this.checkEditFrontLight.CheckedChanged += new System.EventHandler(this.checkEditFrontLight_CheckedChanged);
            // 
            // OPTLightSourceCtrlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "OPTLightSourceCtrlPanel";
            this.Size = new System.Drawing.Size(396, 261);
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit_Intensity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGetValule.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbChannel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditBrightField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFrontLight.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit spinEdit_Intensity;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSetValue;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEditGetValule;
        private DevExpress.XtraEditors.SimpleButton btnGetValue;
        private DevExpress.XtraEditors.SimpleButton btnLightOn;
        private DevExpress.XtraEditors.SimpleButton btnTurnOff;
        private DevExpress.XtraEditors.SimpleButton BtnConnect;
        private DevExpress.XtraEditors.SimpleButton BtnDisconnect;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbChannel;
        private DevExpress.XtraEditors.CheckEdit checkEditBrightField;
        private DevExpress.XtraEditors.CheckEdit checkEditFrontLight;
        private DevExpress.XtraEditors.SimpleButton btnSteIP;
    }
}
