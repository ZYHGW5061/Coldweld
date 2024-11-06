namespace StageCtrlPanelLib
{
    partial class StageAxisParameterPanel
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
            this.seVelocity = new DevExpress.XtraEditors.SpinEdit();
            this.seAcceleration = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.seDeceleration = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxSelAxis = new System.Windows.Forms.ComboBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.seSoftLeftLimit = new DevExpress.XtraEditors.SpinEdit();
            this.seSoftRightLimit = new DevExpress.XtraEditors.SpinEdit();
            this.seMaxSpeed = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSetAxisVelocity = new DevExpress.XtraEditors.SimpleButton();
            this.btnSet = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.seVelocity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seAcceleration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seDeceleration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSoftLeftLimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSoftRightLimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seMaxSpeed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // seVelocity
            // 
            this.seVelocity.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seVelocity.Location = new System.Drawing.Point(115, 77);
            this.seVelocity.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.seVelocity.Name = "seVelocity";
            this.seVelocity.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.seVelocity.Properties.AutoHeight = false;
            this.seVelocity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seVelocity.Properties.DisplayFormat.FormatString = "0.000";
            this.seVelocity.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seVelocity.Properties.EditFormat.FormatString = "0.000";
            this.seVelocity.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seVelocity.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.seVelocity.Properties.Mask.EditMask = "f3";
            this.seVelocity.Size = new System.Drawing.Size(94, 23);
            this.seVelocity.TabIndex = 5;
            // 
            // seAcceleration
            // 
            this.seAcceleration.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seAcceleration.Location = new System.Drawing.Point(115, 112);
            this.seAcceleration.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.seAcceleration.Name = "seAcceleration";
            this.seAcceleration.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.seAcceleration.Properties.AutoHeight = false;
            this.seAcceleration.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seAcceleration.Properties.DisplayFormat.FormatString = "0.000";
            this.seAcceleration.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seAcceleration.Properties.EditFormat.FormatString = "0.000";
            this.seAcceleration.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seAcceleration.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.seAcceleration.Properties.Mask.EditMask = "f3";
            this.seAcceleration.Size = new System.Drawing.Size(94, 23);
            this.seAcceleration.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(15, 74);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(94, 29);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "速度:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.Location = new System.Drawing.Point(15, 109);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(94, 29);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "加速度:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.Location = new System.Drawing.Point(15, 144);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(94, 29);
            this.labelControl6.TabIndex = 1;
            this.labelControl6.Text = "减速度:";
            // 
            // seDeceleration
            // 
            this.seDeceleration.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seDeceleration.Location = new System.Drawing.Point(115, 147);
            this.seDeceleration.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.seDeceleration.Name = "seDeceleration";
            this.seDeceleration.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.seDeceleration.Properties.AutoHeight = false;
            this.seDeceleration.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seDeceleration.Properties.DisplayFormat.FormatString = "0.000";
            this.seDeceleration.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seDeceleration.Properties.EditFormat.FormatString = "0.000";
            this.seDeceleration.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seDeceleration.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.seDeceleration.Properties.Mask.EditMask = "f3";
            this.seDeceleration.Size = new System.Drawing.Size(94, 23);
            this.seDeceleration.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(67, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(16, 14);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "轴:";
            // 
            // comboBoxSelAxis
            // 
            this.comboBoxSelAxis.FormattingEnabled = true;
            this.comboBoxSelAxis.Location = new System.Drawing.Point(88, 26);
            this.comboBoxSelAxis.Name = "comboBoxSelAxis";
            this.comboBoxSelAxis.Size = new System.Drawing.Size(121, 22);
            this.comboBoxSelAxis.TabIndex = 2;
            this.comboBoxSelAxis.SelectedValueChanged += new System.EventHandler(this.comboBoxSelAxis_SelectedValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(15, 179);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(94, 29);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "左限位:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.Location = new System.Drawing.Point(15, 214);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(94, 29);
            this.labelControl5.TabIndex = 1;
            this.labelControl5.Text = "右限位:";
            // 
            // seSoftLeftLimit
            // 
            this.seSoftLeftLimit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seSoftLeftLimit.Location = new System.Drawing.Point(115, 182);
            this.seSoftLeftLimit.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.seSoftLeftLimit.Name = "seSoftLeftLimit";
            this.seSoftLeftLimit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.seSoftLeftLimit.Properties.AutoHeight = false;
            this.seSoftLeftLimit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seSoftLeftLimit.Properties.DisplayFormat.FormatString = "0.000";
            this.seSoftLeftLimit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seSoftLeftLimit.Properties.EditFormat.FormatString = "0.000";
            this.seSoftLeftLimit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seSoftLeftLimit.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.seSoftLeftLimit.Properties.Mask.EditMask = "f3";
            this.seSoftLeftLimit.Size = new System.Drawing.Size(94, 23);
            this.seSoftLeftLimit.TabIndex = 5;
            // 
            // seSoftRightLimit
            // 
            this.seSoftRightLimit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seSoftRightLimit.Location = new System.Drawing.Point(115, 217);
            this.seSoftRightLimit.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.seSoftRightLimit.Name = "seSoftRightLimit";
            this.seSoftRightLimit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.seSoftRightLimit.Properties.AutoHeight = false;
            this.seSoftRightLimit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seSoftRightLimit.Properties.DisplayFormat.FormatString = "0.000";
            this.seSoftRightLimit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seSoftRightLimit.Properties.EditFormat.FormatString = "0.000";
            this.seSoftRightLimit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seSoftRightLimit.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.seSoftRightLimit.Properties.Mask.EditMask = "f3";
            this.seSoftRightLimit.Size = new System.Drawing.Size(94, 23);
            this.seSoftRightLimit.TabIndex = 5;
            // 
            // seMaxSpeed
            // 
            this.seMaxSpeed.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seMaxSpeed.Location = new System.Drawing.Point(115, 252);
            this.seMaxSpeed.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.seMaxSpeed.Name = "seMaxSpeed";
            this.seMaxSpeed.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.seMaxSpeed.Properties.AutoHeight = false;
            this.seMaxSpeed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seMaxSpeed.Properties.DisplayFormat.FormatString = "0.000";
            this.seMaxSpeed.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seMaxSpeed.Properties.EditFormat.FormatString = "0.000";
            this.seMaxSpeed.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seMaxSpeed.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.seMaxSpeed.Properties.Mask.EditMask = "f3";
            this.seMaxSpeed.Size = new System.Drawing.Size(94, 23);
            this.seMaxSpeed.TabIndex = 5;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Options.UseTextOptions = true;
            this.labelControl9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl9.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl9.Location = new System.Drawing.Point(15, 249);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(94, 29);
            this.labelControl9.TabIndex = 1;
            this.labelControl9.Text = "最大速度:";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnSetAxisVelocity);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.btnSet);
            this.panelControl2.Controls.Add(this.seVelocity);
            this.panelControl2.Controls.Add(this.comboBoxSelAxis);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.seAcceleration);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.seMaxSpeed);
            this.panelControl2.Controls.Add(this.seDeceleration);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.seSoftLeftLimit);
            this.panelControl2.Controls.Add(this.labelControl9);
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.seSoftRightLimit);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(242, 378);
            this.panelControl2.TabIndex = 14;
            // 
            // btnSetAxisVelocity
            // 
            this.btnSetAxisVelocity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSetAxisVelocity.Location = new System.Drawing.Point(534, 139);
            this.btnSetAxisVelocity.Name = "btnSetAxisVelocity";
            this.btnSetAxisVelocity.Size = new System.Drawing.Size(94, 29);
            this.btnSetAxisVelocity.TabIndex = 3;
            this.btnSetAxisVelocity.Text = "设置速度";
            this.btnSetAxisVelocity.Visible = false;
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(72, 313);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(94, 29);
            this.btnSet.TabIndex = 3;
            this.btnSet.Text = "设置";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // StageAxisParameterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panelControl2);
            this.Name = "StageAxisParameterPanel";
            this.Size = new System.Drawing.Size(242, 378);
            this.Load += new System.EventHandler(this.StageAxisCtrlPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.seVelocity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seAcceleration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seDeceleration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSoftLeftLimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSoftRightLimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seMaxSpeed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit seDeceleration;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SpinEdit seAcceleration;
        private DevExpress.XtraEditors.SpinEdit seVelocity;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox comboBoxSelAxis;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SpinEdit seSoftLeftLimit;
        private DevExpress.XtraEditors.SpinEdit seSoftRightLimit;
        private DevExpress.XtraEditors.SpinEdit seMaxSpeed;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSetAxisVelocity;
        private DevExpress.XtraEditors.SimpleButton btnSet;
    }
}
