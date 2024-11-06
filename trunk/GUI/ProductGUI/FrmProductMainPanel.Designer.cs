
namespace ProductGUI
{
    partial class FrmProductMainPanel
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
            this.components = new System.ComponentModel.Container();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.teTransportRecipeName = new System.Windows.Forms.TextBox();
            this.btnSelectTransportRecipe = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnPausedOrSingle = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.refreshtimer = new System.Windows.Forms.Timer(this.components);
            this.teLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 26);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "配方名称";
            // 
            // teTransportRecipeName
            // 
            this.teTransportRecipeName.Location = new System.Drawing.Point(91, 22);
            this.teTransportRecipeName.Margin = new System.Windows.Forms.Padding(4);
            this.teTransportRecipeName.Name = "teTransportRecipeName";
            this.teTransportRecipeName.Size = new System.Drawing.Size(132, 25);
            this.teTransportRecipeName.TabIndex = 1;
            // 
            // btnSelectTransportRecipe
            // 
            this.btnSelectTransportRecipe.Location = new System.Drawing.Point(231, 21);
            this.btnSelectTransportRecipe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectTransportRecipe.Name = "btnSelectTransportRecipe";
            this.btnSelectTransportRecipe.Size = new System.Drawing.Size(80, 31);
            this.btnSelectTransportRecipe.TabIndex = 5;
            this.btnSelectTransportRecipe.Text = "选择";
            this.btnSelectTransportRecipe.UseVisualStyleBackColor = true;
            this.btnSelectTransportRecipe.Click += new System.EventHandler(this.btnSelectTransportRecipe_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(19, 81);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(160, 62);
            this.btnRun.TabIndex = 6;
            this.btnRun.Text = "自动执行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnPausedOrSingle
            // 
            this.btnPausedOrSingle.Location = new System.Drawing.Point(19, 204);
            this.btnPausedOrSingle.Margin = new System.Windows.Forms.Padding(4);
            this.btnPausedOrSingle.Name = "btnPausedOrSingle";
            this.btnPausedOrSingle.Size = new System.Drawing.Size(160, 62);
            this.btnPausedOrSingle.TabIndex = 7;
            this.btnPausedOrSingle.Text = "暂定/单步执行";
            this.btnPausedOrSingle.UseVisualStyleBackColor = true;
            this.btnPausedOrSingle.Click += new System.EventHandler(this.btnPausedOrSingle_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(19, 326);
            this.btnContinue.Margin = new System.Windows.Forms.Padding(4);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(160, 62);
            this.btnContinue.TabIndex = 8;
            this.btnContinue.Text = "继续执行";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(19, 449);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(160, 62);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "结束";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(187, 81);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(333, 312);
            this.dataGridView1.TabIndex = 11;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(528, 81);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(333, 312);
            this.dataGridView2.TabIndex = 12;
            // 
            // refreshtimer
            // 
            this.refreshtimer.Enabled = true;
            this.refreshtimer.Tick += new System.EventHandler(this.refreshtimer_Tick);
            // 
            // teLog
            // 
            this.teLog.Location = new System.Drawing.Point(187, 400);
            this.teLog.Multiline = true;
            this.teLog.Name = "teLog";
            this.teLog.Size = new System.Drawing.Size(674, 111);
            this.teLog.TabIndex = 13;
            // 
            // FrmProductMainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.teLog);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnPausedOrSingle);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnSelectTransportRecipe);
            this.Controls.Add(this.teTransportRecipeName);
            this.Controls.Add(this.labelControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmProductMainPanel";
            this.Size = new System.Drawing.Size(880, 529);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.TextBox teTransportRecipeName;
        private System.Windows.Forms.Button btnSelectTransportRecipe;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnPausedOrSingle;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Timer refreshtimer;
        private System.Windows.Forms.TextBox teLog;
    }
}
