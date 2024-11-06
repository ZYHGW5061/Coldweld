
namespace ControlPanelClsLib
{
    partial class FrmTemperatureControlPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labPassWord = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.seTargetTemp = new DevExpress.XtraEditors.SpinEdit();
            this.seHeatPreservationMinute = new DevExpress.XtraEditors.SpinEdit();
            this.seOverTemperatureThreshold = new DevExpress.XtraEditors.SpinEdit();
            this.btnHeat = new System.Windows.Forms.Button();
            this.btnStopHeat = new System.Windows.Forms.Button();
            this.btnSelfTuning = new System.Windows.Forms.Button();
            this.laHeat = new DevExpress.XtraEditors.LabelControl();
            this.seTemp = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSelectHeatRecipe2 = new System.Windows.Forms.Button();
            this.teTransportRecipeName2 = new System.Windows.Forms.TextBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.teTransportRecipeName = new System.Windows.Forms.TextBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.seTargetTemp2 = new DevExpress.XtraEditors.SpinEdit();
            this.seHeatPreservationMinute2 = new DevExpress.XtraEditors.SpinEdit();
            this.seOverTemperatureThreshold2 = new DevExpress.XtraEditors.SpinEdit();
            this.seTemp2 = new DevExpress.XtraEditors.SpinEdit();
            this.laHeat2 = new DevExpress.XtraEditors.LabelControl();
            this.btnHeat2 = new System.Windows.Forms.Button();
            this.btnStopHeat2 = new System.Windows.Forms.Button();
            this.btnSelfTuning2 = new System.Windows.Forms.Button();
            this.btnSelectHeatRecipe = new System.Windows.Forms.Button();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.seInsulatedMinutes = new DevExpress.XtraEditors.SpinEdit();
            this.seInsulatedMinutes2 = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.seTargetTemp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seHeatPreservationMinute.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seOverTemperatureThreshold.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTemp.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seTargetTemp2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seHeatPreservationMinute2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seOverTemperatureThreshold2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTemp2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seInsulatedMinutes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seInsulatedMinutes2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labPassWord
            // 
            this.labPassWord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labPassWord.Location = new System.Drawing.Point(4, 68);
            this.labPassWord.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labPassWord.Name = "labPassWord";
            this.labPassWord.Size = new System.Drawing.Size(144, 24);
            this.labPassWord.TabIndex = 1;
            this.labPassWord.Text = "烘箱A:目标温度/℃:";
            // 
            // labelControl1
            // 
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(4, 100);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(144, 24);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "烘箱A:保温时间/分钟:";
            // 
            // labelControl2
            // 
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl2.Location = new System.Drawing.Point(4, 132);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(144, 24);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "烘箱A:超温报警温度/℃:";
            // 
            // seTargetTemp
            // 
            this.seTargetTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seTargetTemp.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seTargetTemp.Location = new System.Drawing.Point(157, 69);
            this.seTargetTemp.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.seTargetTemp.Name = "seTargetTemp";
            this.seTargetTemp.Properties.AutoHeight = false;
            this.seTargetTemp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seTargetTemp.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seTargetTemp.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seTargetTemp.Properties.MaskSettings.Set("mask", "n0");
            this.seTargetTemp.Size = new System.Drawing.Size(69, 22);
            this.seTargetTemp.TabIndex = 41;
            // 
            // seHeatPreservationMinute
            // 
            this.seHeatPreservationMinute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seHeatPreservationMinute.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seHeatPreservationMinute.Location = new System.Drawing.Point(157, 101);
            this.seHeatPreservationMinute.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.seHeatPreservationMinute.Name = "seHeatPreservationMinute";
            this.seHeatPreservationMinute.Properties.AutoHeight = false;
            this.seHeatPreservationMinute.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seHeatPreservationMinute.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seHeatPreservationMinute.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seHeatPreservationMinute.Properties.MaskSettings.Set("mask", "n0");
            this.seHeatPreservationMinute.Size = new System.Drawing.Size(69, 22);
            this.seHeatPreservationMinute.TabIndex = 42;
            // 
            // seOverTemperatureThreshold
            // 
            this.seOverTemperatureThreshold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seOverTemperatureThreshold.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seOverTemperatureThreshold.Location = new System.Drawing.Point(157, 133);
            this.seOverTemperatureThreshold.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.seOverTemperatureThreshold.Name = "seOverTemperatureThreshold";
            this.seOverTemperatureThreshold.Properties.AutoHeight = false;
            this.seOverTemperatureThreshold.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seOverTemperatureThreshold.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seOverTemperatureThreshold.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seOverTemperatureThreshold.Properties.MaskSettings.Set("mask", "n0");
            this.seOverTemperatureThreshold.Size = new System.Drawing.Size(69, 22);
            this.seOverTemperatureThreshold.TabIndex = 43;
            // 
            // btnHeat
            // 
            this.btnHeat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHeat.Location = new System.Drawing.Point(233, 66);
            this.btnHeat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHeat.Name = "btnHeat";
            this.btnHeat.Size = new System.Drawing.Size(62, 28);
            this.btnHeat.TabIndex = 44;
            this.btnHeat.Text = "加热";
            this.btnHeat.UseVisualStyleBackColor = true;
            this.btnHeat.Click += new System.EventHandler(this.btnHeat_Click);
            // 
            // btnStopHeat
            // 
            this.btnStopHeat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStopHeat.Location = new System.Drawing.Point(233, 98);
            this.btnStopHeat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStopHeat.Name = "btnStopHeat";
            this.btnStopHeat.Size = new System.Drawing.Size(62, 28);
            this.btnStopHeat.TabIndex = 45;
            this.btnStopHeat.Text = "停止加热";
            this.btnStopHeat.UseVisualStyleBackColor = true;
            this.btnStopHeat.Click += new System.EventHandler(this.btnStopHeat_Click);
            // 
            // btnSelfTuning
            // 
            this.btnSelfTuning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelfTuning.Location = new System.Drawing.Point(233, 130);
            this.btnSelfTuning.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSelfTuning.Name = "btnSelfTuning";
            this.btnSelfTuning.Size = new System.Drawing.Size(62, 28);
            this.btnSelfTuning.TabIndex = 46;
            this.btnSelfTuning.Text = "自整定";
            this.btnSelfTuning.UseVisualStyleBackColor = true;
            this.btnSelfTuning.Click += new System.EventHandler(this.btnSelfTuning_Click);
            // 
            // laHeat
            // 
            this.laHeat.Appearance.BackColor = System.Drawing.Color.YellowGreen;
            this.laHeat.Appearance.Options.UseBackColor = true;
            this.laHeat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laHeat.Location = new System.Drawing.Point(156, 196);
            this.laHeat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.laHeat.Name = "laHeat";
            this.laHeat.Size = new System.Drawing.Size(71, 24);
            this.laHeat.TabIndex = 66;
            this.laHeat.Text = "  ";
            // 
            // seTemp
            // 
            this.seTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seTemp.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seTemp.Location = new System.Drawing.Point(157, 229);
            this.seTemp.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.seTemp.Name = "seTemp";
            this.seTemp.Properties.AutoHeight = false;
            this.seTemp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seTemp.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seTemp.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seTemp.Properties.MaskSettings.Set("mask", "n0");
            this.seTemp.Properties.ReadOnly = true;
            this.seTemp.Size = new System.Drawing.Size(69, 22);
            this.seTemp.TabIndex = 68;
            // 
            // labelControl4
            // 
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl4.Location = new System.Drawing.Point(4, 228);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(144, 24);
            this.labelControl4.TabIndex = 67;
            this.labelControl4.Text = "烘箱A:当前温度/℃:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Controls.Add(this.btnSelectHeatRecipe2, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.teTransportRecipeName2, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl3, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.teTransportRecipeName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.seOverTemperatureThreshold, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.seTargetTemp, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.seHeatPreservationMinute, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelControl1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelControl4, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.seTemp, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.labPassWord, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSelfTuning, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnStopHeat, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnHeat, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl5, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.laHeat, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelControl7, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl8, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelControl9, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelControl10, 4, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelControl11, 4, 7);
            this.tableLayoutPanel1.Controls.Add(this.seTargetTemp2, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.seHeatPreservationMinute2, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.seOverTemperatureThreshold2, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.seTemp2, 5, 7);
            this.tableLayoutPanel1.Controls.Add(this.laHeat2, 5, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnHeat2, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnStopHeat2, 6, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnSelfTuning2, 6, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectHeatRecipe, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl12, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.labelControl13, 4, 8);
            this.tableLayoutPanel1.Controls.Add(this.seInsulatedMinutes, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.seInsulatedMinutes2, 5, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(662, 323);
            this.tableLayoutPanel1.TabIndex = 69;
            // 
            // btnSelectHeatRecipe2
            // 
            this.btnSelectHeatRecipe2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectHeatRecipe2.Location = new System.Drawing.Point(563, 2);
            this.btnSelectHeatRecipe2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSelectHeatRecipe2.Name = "btnSelectHeatRecipe2";
            this.btnSelectHeatRecipe2.Size = new System.Drawing.Size(62, 28);
            this.btnSelectHeatRecipe2.TabIndex = 89;
            this.btnSelectHeatRecipe2.Text = "选择";
            this.btnSelectHeatRecipe2.UseVisualStyleBackColor = true;
            this.btnSelectHeatRecipe2.Click += new System.EventHandler(this.btnSelectHeatRecipe2_Click);
            // 
            // teTransportRecipeName2
            // 
            this.teTransportRecipeName2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teTransportRecipeName2.Location = new System.Drawing.Point(485, 3);
            this.teTransportRecipeName2.Name = "teTransportRecipeName2";
            this.teTransportRecipeName2.Size = new System.Drawing.Size(73, 21);
            this.teTransportRecipeName2.TabIndex = 88;
            // 
            // labelControl3
            // 
            this.labelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl3.Location = new System.Drawing.Point(333, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(146, 26);
            this.labelControl3.TabIndex = 87;
            this.labelControl3.Text = "烘箱B:配方名称";
            // 
            // teTransportRecipeName
            // 
            this.teTransportRecipeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teTransportRecipeName.Location = new System.Drawing.Point(155, 3);
            this.teTransportRecipeName.Name = "teTransportRecipeName";
            this.teTransportRecipeName.Size = new System.Drawing.Size(73, 21);
            this.teTransportRecipeName.TabIndex = 85;
            // 
            // labelControl5
            // 
            this.labelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl5.Location = new System.Drawing.Point(4, 196);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(144, 24);
            this.labelControl5.TabIndex = 69;
            this.labelControl5.Text = "烘箱A:加热状态:";
            // 
            // labelControl7
            // 
            this.labelControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl7.Location = new System.Drawing.Point(334, 68);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(144, 24);
            this.labelControl7.TabIndex = 71;
            this.labelControl7.Text = "烘箱B:目标温度/℃:";
            // 
            // labelControl8
            // 
            this.labelControl8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl8.Location = new System.Drawing.Point(334, 100);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(144, 24);
            this.labelControl8.TabIndex = 72;
            this.labelControl8.Text = "烘箱B:保温时间/分钟:";
            // 
            // labelControl9
            // 
            this.labelControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl9.Location = new System.Drawing.Point(334, 132);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(144, 24);
            this.labelControl9.TabIndex = 73;
            this.labelControl9.Text = "烘箱B:超温报警温度/℃:";
            // 
            // labelControl10
            // 
            this.labelControl10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl10.Location = new System.Drawing.Point(334, 196);
            this.labelControl10.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(144, 24);
            this.labelControl10.TabIndex = 74;
            this.labelControl10.Text = "烘箱B:加热状态:";
            // 
            // labelControl11
            // 
            this.labelControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl11.Location = new System.Drawing.Point(334, 228);
            this.labelControl11.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(144, 24);
            this.labelControl11.TabIndex = 75;
            this.labelControl11.Text = "烘箱B:当前温度/℃:";
            // 
            // seTargetTemp2
            // 
            this.seTargetTemp2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seTargetTemp2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seTargetTemp2.Location = new System.Drawing.Point(487, 69);
            this.seTargetTemp2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.seTargetTemp2.Name = "seTargetTemp2";
            this.seTargetTemp2.Properties.AutoHeight = false;
            this.seTargetTemp2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seTargetTemp2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seTargetTemp2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seTargetTemp2.Properties.MaskSettings.Set("mask", "n0");
            this.seTargetTemp2.Size = new System.Drawing.Size(69, 22);
            this.seTargetTemp2.TabIndex = 76;
            // 
            // seHeatPreservationMinute2
            // 
            this.seHeatPreservationMinute2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seHeatPreservationMinute2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seHeatPreservationMinute2.Location = new System.Drawing.Point(487, 101);
            this.seHeatPreservationMinute2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.seHeatPreservationMinute2.Name = "seHeatPreservationMinute2";
            this.seHeatPreservationMinute2.Properties.AutoHeight = false;
            this.seHeatPreservationMinute2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seHeatPreservationMinute2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seHeatPreservationMinute2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seHeatPreservationMinute2.Properties.MaskSettings.Set("mask", "n0");
            this.seHeatPreservationMinute2.Size = new System.Drawing.Size(69, 22);
            this.seHeatPreservationMinute2.TabIndex = 77;
            // 
            // seOverTemperatureThreshold2
            // 
            this.seOverTemperatureThreshold2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seOverTemperatureThreshold2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seOverTemperatureThreshold2.Location = new System.Drawing.Point(487, 133);
            this.seOverTemperatureThreshold2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.seOverTemperatureThreshold2.Name = "seOverTemperatureThreshold2";
            this.seOverTemperatureThreshold2.Properties.AutoHeight = false;
            this.seOverTemperatureThreshold2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seOverTemperatureThreshold2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seOverTemperatureThreshold2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seOverTemperatureThreshold2.Properties.MaskSettings.Set("mask", "n0");
            this.seOverTemperatureThreshold2.Size = new System.Drawing.Size(69, 22);
            this.seOverTemperatureThreshold2.TabIndex = 78;
            // 
            // seTemp2
            // 
            this.seTemp2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seTemp2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seTemp2.Location = new System.Drawing.Point(487, 229);
            this.seTemp2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.seTemp2.Name = "seTemp2";
            this.seTemp2.Properties.AutoHeight = false;
            this.seTemp2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seTemp2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seTemp2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seTemp2.Properties.MaskSettings.Set("mask", "n0");
            this.seTemp2.Properties.ReadOnly = true;
            this.seTemp2.Size = new System.Drawing.Size(69, 22);
            this.seTemp2.TabIndex = 79;
            // 
            // laHeat2
            // 
            this.laHeat2.Appearance.BackColor = System.Drawing.Color.YellowGreen;
            this.laHeat2.Appearance.Options.UseBackColor = true;
            this.laHeat2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laHeat2.Location = new System.Drawing.Point(486, 196);
            this.laHeat2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.laHeat2.Name = "laHeat2";
            this.laHeat2.Size = new System.Drawing.Size(71, 24);
            this.laHeat2.TabIndex = 80;
            this.laHeat2.Text = "  ";
            // 
            // btnHeat2
            // 
            this.btnHeat2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHeat2.Location = new System.Drawing.Point(563, 66);
            this.btnHeat2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHeat2.Name = "btnHeat2";
            this.btnHeat2.Size = new System.Drawing.Size(62, 28);
            this.btnHeat2.TabIndex = 81;
            this.btnHeat2.Text = "加热";
            this.btnHeat2.UseVisualStyleBackColor = true;
            this.btnHeat2.Click += new System.EventHandler(this.btnHeat2_Click);
            // 
            // btnStopHeat2
            // 
            this.btnStopHeat2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStopHeat2.Location = new System.Drawing.Point(563, 98);
            this.btnStopHeat2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStopHeat2.Name = "btnStopHeat2";
            this.btnStopHeat2.Size = new System.Drawing.Size(62, 28);
            this.btnStopHeat2.TabIndex = 82;
            this.btnStopHeat2.Text = "停止加热";
            this.btnStopHeat2.UseVisualStyleBackColor = true;
            this.btnStopHeat2.Click += new System.EventHandler(this.btnStopHeat2_Click);
            // 
            // btnSelfTuning2
            // 
            this.btnSelfTuning2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelfTuning2.Location = new System.Drawing.Point(563, 130);
            this.btnSelfTuning2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSelfTuning2.Name = "btnSelfTuning2";
            this.btnSelfTuning2.Size = new System.Drawing.Size(62, 28);
            this.btnSelfTuning2.TabIndex = 83;
            this.btnSelfTuning2.Text = "自整定";
            this.btnSelfTuning2.UseVisualStyleBackColor = true;
            this.btnSelfTuning2.Click += new System.EventHandler(this.btnSelfTuning2_Click);
            // 
            // btnSelectHeatRecipe
            // 
            this.btnSelectHeatRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectHeatRecipe.Location = new System.Drawing.Point(233, 2);
            this.btnSelectHeatRecipe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSelectHeatRecipe.Name = "btnSelectHeatRecipe";
            this.btnSelectHeatRecipe.Size = new System.Drawing.Size(62, 28);
            this.btnSelectHeatRecipe.TabIndex = 86;
            this.btnSelectHeatRecipe.Text = "选择";
            this.btnSelectHeatRecipe.UseVisualStyleBackColor = true;
            this.btnSelectHeatRecipe.Click += new System.EventHandler(this.btnSelectHeatRecipe_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl6.Location = new System.Drawing.Point(3, 3);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(146, 26);
            this.labelControl6.TabIndex = 84;
            this.labelControl6.Text = "烘箱A:配方名称";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(4, 260);
            this.labelControl12.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(141, 14);
            this.labelControl12.TabIndex = 90;
            this.labelControl12.Text = "烘箱A:剩余保温时间/分钟:";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(334, 260);
            this.labelControl13.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(140, 14);
            this.labelControl13.TabIndex = 91;
            this.labelControl13.Text = "烘箱B:剩余保温时间/分钟:";
            // 
            // seInsulatedMinutes
            // 
            this.seInsulatedMinutes.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seInsulatedMinutes.Location = new System.Drawing.Point(157, 261);
            this.seInsulatedMinutes.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.seInsulatedMinutes.Name = "seInsulatedMinutes";
            this.seInsulatedMinutes.Properties.AutoHeight = false;
            this.seInsulatedMinutes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seInsulatedMinutes.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seInsulatedMinutes.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seInsulatedMinutes.Properties.MaskSettings.Set("mask", "n0");
            this.seInsulatedMinutes.Properties.ReadOnly = true;
            this.seInsulatedMinutes.Size = new System.Drawing.Size(68, 22);
            this.seInsulatedMinutes.TabIndex = 92;
            // 
            // seInsulatedMinutes2
            // 
            this.seInsulatedMinutes2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seInsulatedMinutes2.Location = new System.Drawing.Point(487, 261);
            this.seInsulatedMinutes2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.seInsulatedMinutes2.Name = "seInsulatedMinutes2";
            this.seInsulatedMinutes2.Properties.AutoHeight = false;
            this.seInsulatedMinutes2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seInsulatedMinutes2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seInsulatedMinutes2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seInsulatedMinutes2.Properties.MaskSettings.Set("mask", "n0");
            this.seInsulatedMinutes2.Properties.ReadOnly = true;
            this.seInsulatedMinutes2.Size = new System.Drawing.Size(68, 22);
            this.seInsulatedMinutes2.TabIndex = 93;
            // 
            // FrmTemperatureControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 323);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmTemperatureControlPanel";
            this.Text = "烘箱加热";
            ((System.ComponentModel.ISupportInitialize)(this.seTargetTemp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seHeatPreservationMinute.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seOverTemperatureThreshold.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTemp.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seTargetTemp2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seHeatPreservationMinute2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seOverTemperatureThreshold2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTemp2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seInsulatedMinutes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seInsulatedMinutes2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labPassWord;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit seTargetTemp;
        private DevExpress.XtraEditors.SpinEdit seHeatPreservationMinute;
        private DevExpress.XtraEditors.SpinEdit seOverTemperatureThreshold;
        private System.Windows.Forms.Button btnHeat;
        private System.Windows.Forms.Button btnStopHeat;
        private System.Windows.Forms.Button btnSelfTuning;
        private DevExpress.XtraEditors.LabelControl laHeat;
        private DevExpress.XtraEditors.SpinEdit seTemp;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.SpinEdit seTargetTemp2;
        private DevExpress.XtraEditors.SpinEdit seHeatPreservationMinute2;
        private DevExpress.XtraEditors.SpinEdit seOverTemperatureThreshold2;
        private DevExpress.XtraEditors.SpinEdit seTemp2;
        private DevExpress.XtraEditors.LabelControl laHeat2;
        private System.Windows.Forms.Button btnHeat2;
        private System.Windows.Forms.Button btnStopHeat2;
        private System.Windows.Forms.Button btnSelfTuning2;
        private System.Windows.Forms.Button btnSelectHeatRecipe2;
        private System.Windows.Forms.TextBox teTransportRecipeName2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.TextBox teTransportRecipeName;
        private System.Windows.Forms.Button btnSelectHeatRecipe;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.SpinEdit seInsulatedMinutes;
        private DevExpress.XtraEditors.SpinEdit seInsulatedMinutes2;
    }
}