﻿namespace TransportPanelClsLib
{
    partial class FrmMaterialBoxRecipeEditor
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labEnd = new DevExpress.XtraEditors.SimpleButton();
            this.LabDown = new DevExpress.XtraEditors.SimpleButton();
            this.labGo = new DevExpress.XtraEditors.SimpleButton();
            this.labUp = new DevExpress.XtraEditors.SimpleButton();
            this.labHome = new DevExpress.XtraEditors.SimpleButton();
            this.textPage = new DevExpress.XtraEditors.TextEdit();
            this.labelPageCount = new DevExpress.XtraEditors.LabelControl();
            this.textFind = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textPage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(689, 402);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 38);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(689, 358);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(112, 38);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确认";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel2.Controls.Add(this.listBoxControl1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnSelect, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.textFind, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOK, 1, 7);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(815, 802);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // listBoxControl1
            // 
            this.listBoxControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.listBoxControl1.Appearance.Options.UseFont = true;
            this.listBoxControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxControl1.ItemHeight = 30;
            this.listBoxControl1.Location = new System.Drawing.Point(2, 58);
            this.listBoxControl1.Margin = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this.listBoxControl1.Name = "listBoxControl1";
            this.tableLayoutPanel2.SetRowSpan(this.listBoxControl1, 8);
            this.listBoxControl1.Size = new System.Drawing.Size(683, 697);
            this.listBoxControl1.TabIndex = 5;
            this.listBoxControl1.SelectedValueChanged += new System.EventHandler(this.listBoxControl1_SelectedValueChanged);
            this.listBoxControl1.DoubleClick += new System.EventHandler(this.listBoxControl1_DoubleClick);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(689, 8);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(112, 38);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "Find";
            this.btnSelect.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel1.Controls.Add(this.labEnd, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabDown, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.labGo, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.labUp, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labHome, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textPage, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelPageCount, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(136, 757);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(551, 45);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // labEnd
            // 
            this.labEnd.Appearance.ForeColor = System.Drawing.Color.Teal;
            this.labEnd.Appearance.Options.UseForeColor = true;
            this.labEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labEnd.Location = new System.Drawing.Point(460, 8);
            this.labEnd.Margin = new System.Windows.Forms.Padding(8);
            this.labEnd.Name = "labEnd";
            this.labEnd.Size = new System.Drawing.Size(86, 29);
            this.labEnd.TabIndex = 5;
            this.labEnd.Text = "最后一页";
            this.labEnd.Click += new System.EventHandler(this.labEnd_Click);
            // 
            // LabDown
            // 
            this.LabDown.Appearance.ForeColor = System.Drawing.Color.Teal;
            this.LabDown.Appearance.Options.UseForeColor = true;
            this.LabDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabDown.Location = new System.Drawing.Point(360, 8);
            this.LabDown.Margin = new System.Windows.Forms.Padding(8);
            this.LabDown.Name = "LabDown";
            this.LabDown.Size = new System.Drawing.Size(84, 29);
            this.LabDown.TabIndex = 4;
            this.LabDown.Text = "下一页";
            this.LabDown.Click += new System.EventHandler(this.LabDown_Click);
            // 
            // labGo
            // 
            this.labGo.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labGo.Appearance.Options.UseForeColor = true;
            this.labGo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labGo.Location = new System.Drawing.Point(332, 8);
            this.labGo.Margin = new System.Windows.Forms.Padding(8);
            this.labGo.Name = "labGo";
            this.labGo.Size = new System.Drawing.Size(12, 29);
            this.labGo.TabIndex = 3;
            this.labGo.Text = "GO";
            this.labGo.Visible = false;
            this.labGo.Click += new System.EventHandler(this.labGo_Click);
            // 
            // labUp
            // 
            this.labUp.Appearance.ForeColor = System.Drawing.Color.Teal;
            this.labUp.Appearance.Options.UseForeColor = true;
            this.labUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labUp.Location = new System.Drawing.Point(108, 8);
            this.labUp.Margin = new System.Windows.Forms.Padding(8);
            this.labUp.Name = "labUp";
            this.labUp.Size = new System.Drawing.Size(84, 29);
            this.labUp.TabIndex = 1;
            this.labUp.Text = "上一页";
            this.labUp.Click += new System.EventHandler(this.labUp_Click);
            // 
            // labHome
            // 
            this.labHome.Appearance.ForeColor = System.Drawing.Color.Teal;
            this.labHome.Appearance.Options.UseForeColor = true;
            this.labHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labHome.Location = new System.Drawing.Point(8, 8);
            this.labHome.Margin = new System.Windows.Forms.Padding(8);
            this.labHome.Name = "labHome";
            this.labHome.Size = new System.Drawing.Size(84, 29);
            this.labHome.TabIndex = 0;
            this.labHome.Text = "第一页";
            this.labHome.Click += new System.EventHandler(this.labHome_Click);
            // 
            // textPage
            // 
            this.textPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textPage.EditValue = "1";
            this.textPage.Location = new System.Drawing.Point(208, 8);
            this.textPage.Margin = new System.Windows.Forms.Padding(8);
            this.textPage.Name = "textPage";
            this.textPage.Properties.AutoHeight = false;
            this.textPage.Size = new System.Drawing.Size(66, 29);
            this.textPage.TabIndex = 2;
            // 
            // labelPageCount
            // 
            this.labelPageCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelPageCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPageCount.Location = new System.Drawing.Point(284, 2);
            this.labelPageCount.Margin = new System.Windows.Forms.Padding(2);
            this.labelPageCount.Name = "labelPageCount";
            this.labelPageCount.Size = new System.Drawing.Size(38, 41);
            this.labelPageCount.TabIndex = 6;
            this.labelPageCount.Text = "labelControl1";
            // 
            // textFind
            // 
            this.textFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textFind.Location = new System.Drawing.Point(2, 2);
            this.textFind.Margin = new System.Windows.Forms.Padding(2);
            this.textFind.Name = "textFind";
            this.textFind.Size = new System.Drawing.Size(683, 34);
            this.textFind.TabIndex = 7;
            this.textFind.TextChanged += new System.EventHandler(this.textFind_TextChanged);
            // 
            // FrmMaterialBoxRecipeEditor
            // 
            this.AcceptButton = this.btnOK;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(815, 802);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmMaterialBoxRecipeEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑生产配方";
            this.Shown += new System.EventHandler(this.FrmRecipeSelect_Shown);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textPage.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.ListBoxControl listBoxControl1;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton labEnd;
        private DevExpress.XtraEditors.SimpleButton LabDown;
        private DevExpress.XtraEditors.SimpleButton labGo;
        private DevExpress.XtraEditors.SimpleButton labUp;
        private DevExpress.XtraEditors.SimpleButton labHome;
        private DevExpress.XtraEditors.TextEdit textPage;
        private System.Windows.Forms.TextBox textFind;
        private DevExpress.XtraEditors.LabelControl labelPageCount;
    }
}