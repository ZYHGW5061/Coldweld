
using System.Drawing;

namespace StageCtrlPanelLib
{
    partial class StageQuickMove
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
                UnsubscribeIO();
                _readPosTimer.Stop();
            }
            if (disposing)
            {
                UnsubscribeIO();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelCurrentAxisPosition = new DevExpress.XtraEditors.LabelControl();
            this.groupBoxControlField = new System.Windows.Forms.GroupBox();
            this.btnControlField = new System.Windows.Forms.Button();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cmbSelectAxis = new System.Windows.Forms.ComboBox();
            this.cmbSelectStageSystem = new System.Windows.Forms.ComboBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupBox2.SuspendLayout();
            this.groupBoxControlField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelCurrentAxisPosition);
            this.groupBox2.Location = new System.Drawing.Point(31, 261);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(211, 46);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "轴位置";
            // 
            // labelCurrentAxisPosition
            // 
            this.labelCurrentAxisPosition.Location = new System.Drawing.Point(14, 21);
            this.labelCurrentAxisPosition.Name = "labelCurrentAxisPosition";
            this.labelCurrentAxisPosition.Size = new System.Drawing.Size(46, 14);
            this.labelCurrentAxisPosition.TabIndex = 9;
            this.labelCurrentAxisPosition.Text = "Position:";
            // 
            // groupBoxControlField
            // 
            this.groupBoxControlField.Controls.Add(this.btnControlField);
            this.groupBoxControlField.Location = new System.Drawing.Point(31, 110);
            this.groupBoxControlField.Name = "groupBoxControlField";
            this.groupBoxControlField.Size = new System.Drawing.Size(211, 141);
            this.groupBoxControlField.TabIndex = 10;
            this.groupBoxControlField.TabStop = false;
            this.groupBoxControlField.Text = "快捷移动";
            // 
            // btnControlField
            // 
            this.btnControlField.BackColor = System.Drawing.Color.Gray;
            this.btnControlField.Location = new System.Drawing.Point(23, 29);
            this.btnControlField.Name = "btnControlField";
            this.btnControlField.Size = new System.Drawing.Size(164, 88);
            this.btnControlField.TabIndex = 8;
            this.btnControlField.UseVisualStyleBackColor = false;
            this.btnControlField.Click += new System.EventHandler(this.btnControlField_Click);
            this.btnControlField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnControlField_KeyDown);
            this.btnControlField.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnControlField_KeyUp);
            this.btnControlField.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.btnControlField_PreviewKeyDown);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(31, 21);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(76, 14);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "选择运动系统:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(67, 63);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(40, 14);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "选择轴:";
            // 
            // cmbSelectAxis
            // 
            this.cmbSelectAxis.FormattingEnabled = true;
            this.cmbSelectAxis.Location = new System.Drawing.Point(114, 59);
            this.cmbSelectAxis.Name = "cmbSelectAxis";
            this.cmbSelectAxis.Size = new System.Drawing.Size(128, 22);
            this.cmbSelectAxis.TabIndex = 8;
            this.cmbSelectAxis.SelectedIndexChanged += new System.EventHandler(this.cmbSelectAxis_SelectedIndexChanged);
            // 
            // cmbSelectStageSystem
            // 
            this.cmbSelectStageSystem.FormattingEnabled = true;
            this.cmbSelectStageSystem.Location = new System.Drawing.Point(114, 17);
            this.cmbSelectStageSystem.Name = "cmbSelectStageSystem";
            this.cmbSelectStageSystem.Size = new System.Drawing.Size(128, 22);
            this.cmbSelectStageSystem.TabIndex = 8;
            this.cmbSelectStageSystem.SelectedIndexChanged += new System.EventHandler(this.cmbSelectStageSystem_SelectedIndexChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupBox2);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.groupBoxControlField);
            this.panelControl1.Controls.Add(this.cmbSelectStageSystem);
            this.panelControl1.Controls.Add(this.cmbSelectAxis);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(269, 317);
            this.panelControl1.TabIndex = 36;
            // 
            // StageQuickMove
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.panelControl1);
            this.Name = "StageQuickMove";
            this.Size = new System.Drawing.Size(269, 317);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxControlField.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBoxControlField;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.ComboBox cmbSelectAxis;
        private System.Windows.Forms.ComboBox cmbSelectStageSystem;
        private System.Windows.Forms.Button btnControlField;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelCurrentAxisPosition;
    }
}
