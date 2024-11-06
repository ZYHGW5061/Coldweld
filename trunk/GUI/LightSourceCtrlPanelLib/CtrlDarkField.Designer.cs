namespace Skyverse.Sweetgum.BacksideTerminal.LightSourceCtrLib
{
    partial class CtrlDarkField
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
            this.btnSet = new DevExpress.XtraEditors.SimpleButton();
            this.labelValue = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.light_brightness_control)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.light_brightness_control.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // light_brightness_control
            // 
            this.light_brightness_control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.light_brightness_control.EditValue = 3;
            this.light_brightness_control.Location = new System.Drawing.Point(63, 3);
            this.light_brightness_control.Name = "light_brightness_control";
            this.light_brightness_control.Properties.Alignment = DevExpress.Utils.VertAlignment.Center;
            this.light_brightness_control.Properties.AutoSize = false;
            this.light_brightness_control.Properties.LargeChange = 1;
            this.light_brightness_control.Properties.Maximum = 1000;
            this.light_brightness_control.Properties.Middle = 5;
            this.light_brightness_control.Properties.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            this.light_brightness_control.Size = new System.Drawing.Size(184, 33);
            this.light_brightness_control.TabIndex = 23;
            this.light_brightness_control.Value = 3;
            this.light_brightness_control.EditValueChanged += new System.EventHandler(this.light_brightness_control_EditValueChanged);
            // 
            // btnSet
            // 
            this.btnSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSet.Location = new System.Drawing.Point(253, 3);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(54, 33);
            this.btnSet.TabIndex = 24;
            this.btnSet.Text = "Apply";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // labelValue
            // 
            this.labelValue.Appearance.BackColor = System.Drawing.Color.Silver;
            this.labelValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelValue.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelValue.Location = new System.Drawing.Point(3, 3);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(54, 33);
            this.labelValue.TabIndex = 25;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Controls.Add(this.labelValue, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSet, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.light_brightness_control, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(310, 39);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // CtrlDarkField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CtrlDarkField";
            this.Size = new System.Drawing.Size(310, 39);
            ((System.ComponentModel.ISupportInitialize)(this.light_brightness_control.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.light_brightness_control)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ZoomTrackBarControl light_brightness_control;
        private DevExpress.XtraEditors.SimpleButton btnSet;
        private DevExpress.XtraEditors.LabelControl labelValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}
