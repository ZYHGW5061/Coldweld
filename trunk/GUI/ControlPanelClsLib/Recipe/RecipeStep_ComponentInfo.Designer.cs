
namespace ControlPanelClsLib.Recipe
{
    partial class RecipeStep_ComponentInfo
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.CameraComboBox = new System.Windows.Forms.ComboBox();
            this.RingLightlabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sePPYIdlePos = new DevExpress.XtraEditors.SpinEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.spinEdit2 = new DevExpress.XtraEditors.SpinEdit();
            this.cmbComponentCarrierType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.sePPYIdlePos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // CameraComboBox
            // 
            this.CameraComboBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CameraComboBox.FormattingEnabled = true;
            this.CameraComboBox.Items.AddRange(new object[] {
            "无",
            "仰视相机",
            "校准台"});
            this.CameraComboBox.Location = new System.Drawing.Point(145, 193);
            this.CameraComboBox.Name = "CameraComboBox";
            this.CameraComboBox.Size = new System.Drawing.Size(121, 20);
            this.CameraComboBox.TabIndex = 2;
            this.CameraComboBox.Text = "无";
            // 
            // RingLightlabel
            // 
            this.RingLightlabel.AutoSize = true;
            this.RingLightlabel.Location = new System.Drawing.Point(50, 196);
            this.RingLightlabel.Name = "RingLightlabel";
            this.RingLightlabel.Size = new System.Drawing.Size(91, 14);
            this.RingLightlabel.TabIndex = 3;
            this.RingLightlabel.Text = "二次校准方式：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "芯片厚度/mm：";
            // 
            // sePPYIdlePos
            // 
            this.sePPYIdlePos.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sePPYIdlePos.Location = new System.Drawing.Point(145, 42);
            this.sePPYIdlePos.Name = "sePPYIdlePos";
            this.sePPYIdlePos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sePPYIdlePos.Properties.DisplayFormat.FormatString = "0.000";
            this.sePPYIdlePos.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sePPYIdlePos.Properties.EditFormat.FormatString = "0.000";
            this.sePPYIdlePos.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sePPYIdlePos.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.sePPYIdlePos.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.sePPYIdlePos.Properties.MaskSettings.Set("mask", "f1");
            this.sePPYIdlePos.Properties.MaxValue = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.sePPYIdlePos.Size = new System.Drawing.Size(121, 20);
            this.sePPYIdlePos.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 312);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "吸嘴工具：";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "无",
            "仰视相机",
            "校准台"});
            this.comboBox1.Location = new System.Drawing.Point(145, 310);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 2;
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "无",
            "仰视相机",
            "校准台"});
            this.comboBox2.Location = new System.Drawing.Point(145, 352);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "顶针工具：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "芯片载体类型：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "芯片载体厚度/mm：";
            // 
            // spinEdit2
            // 
            this.spinEdit2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit2.Location = new System.Drawing.Point(145, 105);
            this.spinEdit2.Name = "spinEdit2";
            this.spinEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit2.Properties.DisplayFormat.FormatString = "0.000";
            this.spinEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit2.Properties.EditFormat.FormatString = "0.000";
            this.spinEdit2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit2.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.spinEdit2.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.spinEdit2.Properties.MaskSettings.Set("mask", "f1");
            this.spinEdit2.Properties.MaxValue = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.spinEdit2.Size = new System.Drawing.Size(121, 20);
            this.spinEdit2.TabIndex = 10;
            // 
            // cmbComponentCarrierType
            // 
            this.cmbComponentCarrierType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbComponentCarrierType.FormattingEnabled = true;
            this.cmbComponentCarrierType.Items.AddRange(new object[] {
            "晶圆",
            "华夫盒"});
            this.cmbComponentCarrierType.Location = new System.Drawing.Point(145, 74);
            this.cmbComponentCarrierType.Name = "cmbComponentCarrierType";
            this.cmbComponentCarrierType.Size = new System.Drawing.Size(121, 20);
            this.cmbComponentCarrierType.TabIndex = 2;
            this.cmbComponentCarrierType.Text = "晶圆";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(314, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 3;
            this.label6.Text = "识别方式：";
            // 
            // comboBox5
            // 
            this.comboBox5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "轮廓匹配",
            "圆识别",
            "边缘识别"});
            this.comboBox5.Location = new System.Drawing.Point(145, 142);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(121, 20);
            this.comboBox5.TabIndex = 2;
            this.comboBox5.Text = "轮廓匹配";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 14);
            this.label7.TabIndex = 3;
            this.label7.Text = "识别方式：";
            // 
            // comboBox4
            // 
            this.comboBox4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "轮廓匹配",
            "圆识别",
            "边缘识别"});
            this.comboBox4.Location = new System.Drawing.Point(385, 193);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(121, 20);
            this.comboBox4.TabIndex = 2;
            this.comboBox4.Text = "轮廓匹配";
            // 
            // RecipeStep_ComponentInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spinEdit2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sePPYIdlePos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RingLightlabel);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.cmbComponentCarrierType);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.comboBox5);
            this.Controls.Add(this.CameraComboBox);
            this.Name = "RecipeStep_ComponentInfo";
            this.Size = new System.Drawing.Size(883, 494);
            ((System.ComponentModel.ISupportInitialize)(this.sePPYIdlePos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CameraComboBox;
        private System.Windows.Forms.Label RingLightlabel;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SpinEdit sePPYIdlePos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SpinEdit spinEdit2;
        private System.Windows.Forms.ComboBox cmbComponentCarrierType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox4;
    }
}
