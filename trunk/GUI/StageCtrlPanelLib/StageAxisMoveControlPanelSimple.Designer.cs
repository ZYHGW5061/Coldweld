namespace StageCtrlPanelLib
{
    partial class StageAxisMoveControlPanelSimple
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
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.seMoveDistance = new DevExpress.XtraEditors.SpinEdit();
            this.seVelocity = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.teAbsoluteMoveTarget = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxSelAxis = new System.Windows.Forms.ComboBox();
            this.teCurrentPos = new DevExpress.XtraEditors.TextEdit();
            this.btnSetAxisVelocity = new DevExpress.XtraEditors.SimpleButton();
            this.btnReadAxisStatus = new DevExpress.XtraEditors.SimpleButton();
            this.btnRelaticeMove = new DevExpress.XtraEditors.SimpleButton();
            this.btnAbsoluteMove = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seMoveDistance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seVelocity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAbsoluteMoveTarget.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCurrentPos.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panelControl2, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(340, 130);
            this.tableLayoutPanel3.TabIndex = 12;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.seMoveDistance);
            this.panelControl2.Controls.Add(this.seVelocity);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.teAbsoluteMoveTarget);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.comboBoxSelAxis);
            this.panelControl2.Controls.Add(this.teCurrentPos);
            this.panelControl2.Controls.Add(this.btnSetAxisVelocity);
            this.panelControl2.Controls.Add(this.btnReadAxisStatus);
            this.panelControl2.Controls.Add(this.btnRelaticeMove);
            this.panelControl2.Controls.Add(this.btnAbsoluteMove);
            this.panelControl2.Controls.Add(this.labelControl8);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(3, 3);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(334, 124);
            this.panelControl2.TabIndex = 13;
            // 
            // seMoveDistance
            // 
            this.seMoveDistance.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.seMoveDistance.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.seMoveDistance.Location = new System.Drawing.Point(389, 18);
            this.seMoveDistance.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.seMoveDistance.Name = "seMoveDistance";
            this.seMoveDistance.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.seMoveDistance.Properties.AutoHeight = false;
            this.seMoveDistance.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seMoveDistance.Properties.DisplayFormat.FormatString = "0";
            this.seMoveDistance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seMoveDistance.Properties.EditFormat.FormatString = "0";
            this.seMoveDistance.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seMoveDistance.Properties.IsFloatValue = false;
            this.seMoveDistance.Properties.Mask.EditMask = "\\d{1,3}?";
            this.seMoveDistance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.seMoveDistance.Properties.MaxValue = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.seMoveDistance.Size = new System.Drawing.Size(72, 23);
            this.seMoveDistance.TabIndex = 5;
            this.seMoveDistance.Visible = false;
            // 
            // seVelocity
            // 
            this.seVelocity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.seVelocity.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.seVelocity.Location = new System.Drawing.Point(235, 15);
            this.seVelocity.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.seVelocity.Name = "seVelocity";
            this.seVelocity.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.seVelocity.Properties.AutoHeight = false;
            this.seVelocity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seVelocity.Properties.DisplayFormat.FormatString = "0";
            this.seVelocity.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seVelocity.Properties.EditFormat.FormatString = "0";
            this.seVelocity.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seVelocity.Properties.IsFloatValue = false;
            this.seVelocity.Properties.Mask.EditMask = "\\d{1,3}?";
            this.seVelocity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.seVelocity.Properties.MaxValue = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.seVelocity.Size = new System.Drawing.Size(94, 23);
            this.seVelocity.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl1.Location = new System.Drawing.Point(49, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(20, 18);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "轴:";
            // 
            // teAbsoluteMoveTarget
            // 
            this.teAbsoluteMoveTarget.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.teAbsoluteMoveTarget.Location = new System.Drawing.Point(235, 51);
            this.teAbsoluteMoveTarget.Name = "teAbsoluteMoveTarget";
            this.teAbsoluteMoveTarget.Size = new System.Drawing.Size(94, 24);
            this.teAbsoluteMoveTarget.TabIndex = 12;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(189, 11);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(38, 29);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "速度:";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.Location = new System.Drawing.Point(321, 12);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(62, 29);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "位移距离:";
            this.labelControl4.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(174, 44);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 29);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "目标位置:";
            // 
            // comboBoxSelAxis
            // 
            this.comboBoxSelAxis.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxSelAxis.FormattingEnabled = true;
            this.comboBoxSelAxis.Location = new System.Drawing.Point(71, 15);
            this.comboBoxSelAxis.Name = "comboBoxSelAxis";
            this.comboBoxSelAxis.Size = new System.Drawing.Size(90, 26);
            this.comboBoxSelAxis.TabIndex = 2;
            this.comboBoxSelAxis.Text = "MaterialboxX";
            this.comboBoxSelAxis.SelectedValueChanged += new System.EventHandler(this.comboBoxSelAxis_SelectedValueChanged);
            // 
            // teCurrentPos
            // 
            this.teCurrentPos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.teCurrentPos.Location = new System.Drawing.Point(71, 51);
            this.teCurrentPos.Name = "teCurrentPos";
            this.teCurrentPos.Properties.ReadOnly = true;
            this.teCurrentPos.Size = new System.Drawing.Size(90, 24);
            this.teCurrentPos.TabIndex = 12;
            // 
            // btnSetAxisVelocity
            // 
            this.btnSetAxisVelocity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSetAxisVelocity.Location = new System.Drawing.Point(534, 12);
            this.btnSetAxisVelocity.Name = "btnSetAxisVelocity";
            this.btnSetAxisVelocity.Size = new System.Drawing.Size(94, 29);
            this.btnSetAxisVelocity.TabIndex = 3;
            this.btnSetAxisVelocity.Text = "设置速度";
            this.btnSetAxisVelocity.Visible = false;
            this.btnSetAxisVelocity.Click += new System.EventHandler(this.btnSetAxisVelocity_Click);
            // 
            // btnReadAxisStatus
            // 
            this.btnReadAxisStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnReadAxisStatus.Location = new System.Drawing.Point(68, 82);
            this.btnReadAxisStatus.Name = "btnReadAxisStatus";
            this.btnReadAxisStatus.Size = new System.Drawing.Size(94, 29);
            this.btnReadAxisStatus.TabIndex = 3;
            this.btnReadAxisStatus.Text = "读取";
            this.btnReadAxisStatus.Click += new System.EventHandler(this.btnReadAxisStatus_Click);
            // 
            // btnRelaticeMove
            // 
            this.btnRelaticeMove.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRelaticeMove.Location = new System.Drawing.Point(367, 49);
            this.btnRelaticeMove.Name = "btnRelaticeMove";
            this.btnRelaticeMove.Size = new System.Drawing.Size(94, 29);
            this.btnRelaticeMove.TabIndex = 3;
            this.btnRelaticeMove.Text = "相对移动";
            this.btnRelaticeMove.Visible = false;
            this.btnRelaticeMove.Click += new System.EventHandler(this.btnRelaticeMove_Click);
            // 
            // btnAbsoluteMove
            // 
            this.btnAbsoluteMove.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAbsoluteMove.Location = new System.Drawing.Point(235, 80);
            this.btnAbsoluteMove.Name = "btnAbsoluteMove";
            this.btnAbsoluteMove.Size = new System.Drawing.Size(94, 29);
            this.btnAbsoluteMove.TabIndex = 3;
            this.btnAbsoluteMove.Text = "绝对移动";
            this.btnAbsoluteMove.Click += new System.EventHandler(this.btnAbsoluteMove_Click);
            // 
            // labelControl8
            // 
            this.labelControl8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl8.Appearance.Options.UseTextOptions = true;
            this.labelControl8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl8.Location = new System.Drawing.Point(8, 45);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(57, 29);
            this.labelControl8.TabIndex = 1;
            this.labelControl8.Text = "当前位置:";
            // 
            // StageAxisMoveControlPanelSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Name = "StageAxisMoveControlPanelSimple";
            this.Size = new System.Drawing.Size(340, 130);
            this.Load += new System.EventHandler(this.StageAxisAbsoluteMovePanel_Load);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seMoveDistance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seVelocity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAbsoluteMoveTarget.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCurrentPos.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit seVelocity;
        private DevExpress.XtraEditors.SimpleButton btnReadAxisStatus;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox comboBoxSelAxis;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit teCurrentPos;
        private DevExpress.XtraEditors.SimpleButton btnSetAxisVelocity;
        private DevExpress.XtraEditors.TextEdit teAbsoluteMoveTarget;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnAbsoluteMove;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SpinEdit seMoveDistance;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnRelaticeMove;
    }
}
