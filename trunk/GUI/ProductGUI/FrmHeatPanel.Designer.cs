
namespace ProductGUI
{
    partial class FrmHeatPanel
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
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnSelectTransportRecipe = new System.Windows.Forms.Button();
            this.teTransportRecipeName = new System.Windows.Forms.TextBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(19, 449);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(160, 62);
            this.btnStop.TabIndex = 14;
            this.btnStop.Text = "结束";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(19, 81);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(160, 62);
            this.btnRun.TabIndex = 13;
            this.btnRun.Text = "自动执行";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // btnSelectTransportRecipe
            // 
            this.btnSelectTransportRecipe.Location = new System.Drawing.Point(231, 21);
            this.btnSelectTransportRecipe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectTransportRecipe.Name = "btnSelectTransportRecipe";
            this.btnSelectTransportRecipe.Size = new System.Drawing.Size(80, 31);
            this.btnSelectTransportRecipe.TabIndex = 12;
            this.btnSelectTransportRecipe.Text = "选择";
            this.btnSelectTransportRecipe.UseVisualStyleBackColor = true;
            // 
            // teTransportRecipeName
            // 
            this.teTransportRecipeName.Location = new System.Drawing.Point(91, 22);
            this.teTransportRecipeName.Margin = new System.Windows.Forms.Padding(4);
            this.teTransportRecipeName.Name = "teTransportRecipeName";
            this.teTransportRecipeName.Size = new System.Drawing.Size(132, 25);
            this.teTransportRecipeName.TabIndex = 11;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 26);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 18);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "配方名称";
            // 
            // FrmHeatPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnSelectTransportRecipe);
            this.Controls.Add(this.teTransportRecipeName);
            this.Controls.Add(this.labelControl1);
            this.Name = "FrmHeatPanel";
            this.Size = new System.Drawing.Size(880, 529);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnSelectTransportRecipe;
        private System.Windows.Forms.TextBox teTransportRecipeName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
