namespace Skyverse.Sweetgum.BacksideTerminal.LightSourceCtrl
{
    partial class RevoxLightSourceCtrlPanel
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
            this.btnSetFilter = new DevExpress.XtraEditors.SimpleButton();
            this.btnGetValue = new DevExpress.XtraEditors.SimpleButton();
            this.btnLightOn = new DevExpress.XtraEditors.SimpleButton();
            this.btnLightOff = new DevExpress.XtraEditors.SimpleButton();
            this.BtnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDisconnect = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cbFilterNumber = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit_Intensity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGetValule.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbFilterNumber.Properties)).BeginInit();
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
            this.spinEdit_Intensity.Location = new System.Drawing.Point(139, 124);
            this.spinEdit_Intensity.Margin = new System.Windows.Forms.Padding(4);
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
            this.spinEdit_Intensity.Size = new System.Drawing.Size(106, 30);
            this.spinEdit_Intensity.TabIndex = 14;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelControl2.Location = new System.Drawing.Point(42, 19);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(90, 18);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Get Intensity:";
            // 
            // btnSetValue
            // 
            this.btnSetValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSetValue.Location = new System.Drawing.Point(374, 120);
            this.btnSetValue.Name = "btnSetValue";
            this.btnSetValue.Size = new System.Drawing.Size(90, 37);
            this.btnSetValue.TabIndex = 5;
            this.btnSetValue.Text = "SetValue";
            this.btnSetValue.Click += new System.EventHandler(this.btnSetValue_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelControl3.Location = new System.Drawing.Point(50, 75);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(82, 18);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Filter Option:";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelControl1.Location = new System.Drawing.Point(44, 130);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(88, 18);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Set Intensity:";
            // 
            // textEditGetValule
            // 
            this.textEditGetValule.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textEditGetValule.Location = new System.Drawing.Point(138, 13);
            this.textEditGetValule.Name = "textEditGetValule";
            this.textEditGetValule.Properties.AutoHeight = false;
            this.textEditGetValule.Size = new System.Drawing.Size(108, 30);
            this.textEditGetValule.TabIndex = 3;
            // 
            // btnSetFilter
            // 
            this.btnSetFilter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSetFilter.Location = new System.Drawing.Point(374, 65);
            this.btnSetFilter.Name = "btnSetFilter";
            this.btnSetFilter.Size = new System.Drawing.Size(90, 37);
            this.btnSetFilter.TabIndex = 5;
            this.btnSetFilter.Text = "SetFilter";
            this.btnSetFilter.Click += new System.EventHandler(this.btnSetFilter_Click);
            // 
            // btnGetValue
            // 
            this.btnGetValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGetValue.Location = new System.Drawing.Point(374, 9);
            this.btnGetValue.Name = "btnGetValue";
            this.btnGetValue.Size = new System.Drawing.Size(90, 37);
            this.btnGetValue.TabIndex = 5;
            this.btnGetValue.Text = "GetValue";
            this.btnGetValue.Click += new System.EventHandler(this.btnGetValue_Click);
            // 
            // btnLightOn
            // 
            this.btnLightOn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLightOn.Location = new System.Drawing.Point(22, 195);
            this.btnLightOn.Name = "btnLightOn";
            this.btnLightOn.Size = new System.Drawing.Size(90, 37);
            this.btnLightOn.TabIndex = 0;
            this.btnLightOn.Text = "LightOn";
            this.btnLightOn.Click += new System.EventHandler(this.btnLightOn_Click);
            // 
            // btnLightOff
            // 
            this.btnLightOff.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLightOff.Location = new System.Drawing.Point(147, 195);
            this.btnLightOff.Name = "btnLightOff";
            this.btnLightOff.Size = new System.Drawing.Size(90, 37);
            this.btnLightOff.TabIndex = 0;
            this.btnLightOff.Text = "LightOff";
            this.btnLightOff.Click += new System.EventHandler(this.btnLightOff_Click);
            // 
            // BtnConnect
            // 
            this.BtnConnect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnConnect.Location = new System.Drawing.Point(253, 195);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(90, 37);
            this.BtnConnect.TabIndex = 0;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // BtnDisconnect
            // 
            this.BtnDisconnect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnDisconnect.Location = new System.Drawing.Point(374, 195);
            this.BtnDisconnect.Name = "BtnDisconnect";
            this.BtnDisconnect.Size = new System.Drawing.Size(90, 37);
            this.BtnDisconnect.TabIndex = 1;
            this.BtnDisconnect.Text = "Disconnect";
            this.BtnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl4.Location = new System.Drawing.Point(253, 19);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(55, 18);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "(0~255)";
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl5.Location = new System.Drawing.Point(253, 75);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(0, 18);
            this.labelControl5.TabIndex = 4;
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl6.Location = new System.Drawing.Point(253, 130);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(55, 18);
            this.labelControl6.TabIndex = 4;
            this.labelControl6.Text = "(0~255)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.92157F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.07843F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.tableLayoutPanel1.Controls.Add(this.spinEdit_Intensity, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSetValue, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textEditGetValule, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSetFilter, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnGetValue, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnLightOn, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnLightOff, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.BtnConnect, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.BtnDisconnect, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelControl4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelControl6, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbFilterNumber, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 43);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(492, 243);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.tableLayoutPanel1);
            this.groupControl1.Location = new System.Drawing.Point(12, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(526, 318);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Operations";
            // 
            // cbFilterNumber
            // 
            this.cbFilterNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbFilterNumber.EditValue = "1";
            this.cbFilterNumber.Location = new System.Drawing.Point(138, 69);
            this.cbFilterNumber.Name = "cbFilterNumber";
            this.cbFilterNumber.Properties.AutoHeight = false;
            this.cbFilterNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbFilterNumber.Properties.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbFilterNumber.Size = new System.Drawing.Size(108, 30);
            this.cbFilterNumber.TabIndex = 15;
            // 
            // RevoxLightSourceCtrlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "RevoxLightSourceCtrlPanel";
            this.Size = new System.Drawing.Size(550, 338);
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit_Intensity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGetValule.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbFilterNumber.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit spinEdit_Intensity;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSetValue;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEditGetValule;
        private DevExpress.XtraEditors.SimpleButton btnSetFilter;
        private DevExpress.XtraEditors.SimpleButton btnGetValue;
        private DevExpress.XtraEditors.SimpleButton btnLightOn;
        private DevExpress.XtraEditors.SimpleButton btnLightOff;
        private DevExpress.XtraEditors.SimpleButton BtnConnect;
        private DevExpress.XtraEditors.SimpleButton BtnDisconnect;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbFilterNumber;
    }
}
