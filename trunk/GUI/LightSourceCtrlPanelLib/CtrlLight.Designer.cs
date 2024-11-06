namespace LightSourceCtrlPanelLib
{
    partial class CtrlLight
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
            this.light_brightness_control = new DevExpress.XtraEditors.ZoomTrackBarControl();
            this.labelValue = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelLightType = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.light_brightness_control)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.light_brightness_control.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // light_brightness_control
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.light_brightness_control, 3);
            this.light_brightness_control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.light_brightness_control.EditValue = null;
            this.light_brightness_control.Location = new System.Drawing.Point(3, 29);
            this.light_brightness_control.Name = "light_brightness_control";
            this.light_brightness_control.Properties.Alignment = DevExpress.Utils.VertAlignment.Center;
            this.light_brightness_control.Properties.AutoSize = false;
            this.light_brightness_control.Properties.LargeChange = 1;
            this.light_brightness_control.Properties.Maximum = 100;
            this.light_brightness_control.Properties.Middle = 50;
            this.light_brightness_control.Size = new System.Drawing.Size(304, 24);
            this.light_brightness_control.TabIndex = 25;
            this.light_brightness_control.Scroll += new System.EventHandler(this.light_brightness_control_Scroll);
            //this.light_brightness_control.EditValueChanged += new System.EventHandler(this.light_brightness_control_EditValueChanged);
            //this.light_brightness_control.MouseUp += new System.Windows.Forms.MouseEventHandler(this.light_brightness_control_MouseUp);
            // 
            // labelValue
            // 
            this.labelValue.Appearance.BackColor = System.Drawing.Color.Silver;
            this.labelValue.Appearance.Options.UseBackColor = true;
            this.labelValue.Appearance.Options.UseTextOptions = true;
            this.labelValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelValue.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelValue.Location = new System.Drawing.Point(253, 3);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(54, 20);
            this.labelValue.TabIndex = 27;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.labelLightType, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.light_brightness_control, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelValue, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(310, 56);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // labelLightType
            // 
            this.labelLightType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLightType.Location = new System.Drawing.Point(3, 3);
            this.labelLightType.Name = "labelLightType";
            this.labelLightType.Size = new System.Drawing.Size(136, 20);
            this.labelLightType.TabIndex = 28;
            this.labelLightType.Text = "wafer环光";
            // 
            // CtrlLight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CtrlLight";
            this.Size = new System.Drawing.Size(310, 56);
            ((System.ComponentModel.ISupportInitialize)(this.light_brightness_control.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.light_brightness_control)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ZoomTrackBarControl light_brightness_control;
        private DevExpress.XtraEditors.LabelControl labelValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelLightType;
    }
}
