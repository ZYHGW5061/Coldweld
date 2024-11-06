using CommonPanelClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using JobClsLib;
using RecipeClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransportPanelClsLib;

namespace ProductGUI
{
    public partial class FrmProductMainPanel : UserControl
    {
        #region private file

        private static readonly object _lockObj = new object();
        private static volatile FrmProductMainPanel _instance = null;
        public static FrmProductMainPanel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new FrmProductMainPanel();
                        }
                    }
                }
                return _instance;
            }
        }


        List<List<EnumMaterialproperties>> Materialproperties = new List<List<EnumMaterialproperties>>();

        List<EnumMaterialBoxproperties> MaterialBoxproperties = new List<EnumMaterialBoxproperties>();

        private DataModel dataModel;

        public FrmProductMainPanel()
        {
            InitializeComponent();

            teLog = new TextBox()
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical
            };

            DataModel.Instance.PropertyChanged += DataModel_PropertyChanged;
        }


        #endregion



        #region public file

        public string _selectedHeatRecipeName = "";


        #endregion

        #region private mothed

        private void btnSelectTransportRecipe_Click(object sender, EventArgs e)
        {
            //LogRecorder.RecordLog(EnumLogContentType.Info, string.Format("JobControlPanel: User clicked <{0}> Button", (sender as Control).Text));
            //选择一个recipe
            FrmTransportRecipeEditor selectRecipeDialog = new FrmTransportRecipeEditor(null, this.teTransportRecipeName.Text.ToUpper().Trim());
            if (selectRecipeDialog.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                try
                {
                    _selectedHeatRecipeName = selectRecipeDialog.SelectedRecipeName;
                    //验证Recipe是否完整
                    if (!TransportRecipe.Validate(_selectedHeatRecipeName, selectRecipeDialog.RecipeType))
                    {
                        WarningBox.FormShow("错误！", "配方无效！", "提示");
                        return;
                    }
                    else
                    {
                        //var heatRecipe = TransportRecipe.LoadRecipe(_selectedHeatRecipeName, selectRecipeDialog.RecipeType);
                        teTransportRecipeName.Text = selectRecipeDialog.SelectedRecipeName;
                        var transportRecipe = TransportRecipe.LoadRecipe(selectRecipeDialog.SelectedRecipeName, selectRecipeDialog.RecipeType);
                        JobProcessControl.Instance.SetRecipe(transportRecipe);
                    }
                }
                catch (Exception ex)
                {
                    //LogRecorder.RecordLog(EnumLogContentType.Error, "JobControlPanel: Exception occured while Loading Recipe.", ex);
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedHeatRecipeName = teTransportRecipeName.Text;
                //验证Recipe是否完整
                if (!TransportRecipe.Validate(_selectedHeatRecipeName, EnumRecipeType.Transport))
                {
                    WarningBox.FormShow("错误！", "配方无效！", "提示");
                    return;
                }
                else
                {
                    var transportRecipe = TransportRecipe.LoadRecipe(teTransportRecipeName.Text, EnumRecipeType.Transport);
                    JobProcessControl.Instance.SetRecipe(transportRecipe);
                    JobProcessControl.Instance.Run();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void btnPausedOrSingle_Click(object sender, EventArgs e)
        {
            try
            {
                JobProcessControl.Instance.PausedOrSingle();
            }
            catch(Exception ex)
            {

            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                JobProcessControl.Instance.Continue();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                JobProcessControl.Instance.Stop();
            }
            catch (Exception ex)
            {

            }
        }


        private void DataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DataModel.JobLogText))
            {
                UpdateLogSafely(DataModel.Instance.JobLogText);
            }
        }

        private void UpdateLogSafely(string logText)
        {
            //if (teLog.InvokeRequired)
            //{
            //    // 使用 Invoke 来确保在 UI 线程上执行  
            //    teLog.Invoke(new Action(() => UpdateLog(logText)));
            //}
            //else
            //{
            //    UpdateLog(logText);
            //}
        }

        private void UpdateLog(string logText)
        {
            this.teLog.AppendText(logText + Environment.NewLine);
            this.teLog.Refresh();
        }

        private void refreshtimer_Tick(object sender, EventArgs e)
        {
            //this.teLog.Refresh();
        }

        private void HandleMaterialMapLogChange(EnumReturnMaterialproperties newMaterialMapLog)
        {
            if (newMaterialMapLog != null)
            {
                Console.WriteLine("New MaterialMapLog details:");
                Console.WriteLine($"Re: {newMaterialMapLog.Re}");

                InitMatrix(newMaterialMapLog.MaterialMat);
            }
        }

        private void HandleMaterialBoxMapLogChange(EnumReturnMaterialBoxproperties newMaterialBoxMapLog)
        {
            if (newMaterialBoxMapLog != null)
            {
                Console.WriteLine("New MaterialMapLog details:");
                Console.WriteLine($"Re: {newMaterialBoxMapLog.Re}");

                InitMatrix(newMaterialBoxMapLog.MaterialBoxMat);
            }
        }


        public void InitMatrix(List<List<EnumMaterialproperties>> materialMat)
        {
            Materialproperties = materialMat;
            DataGridView2_Data();
        }

        public void InitMatrix(List<EnumMaterialBoxproperties> materialBoxMat)
        {
            MaterialBoxproperties = materialBoxMat;
            DataGridView_Data();
        }

        public List<List<EnumMaterialproperties>> GetMatrix()
        {



            return Materialproperties;
        }

        private void DataGridView_Data()
        {
            try
            {
                if (MaterialBoxproperties == null)
                {
                    return;
                }
                else
                {
                    if (MaterialBoxproperties.Count <= 0)
                    {
                        return;
                    }
                }

                int DataGridViewH = dataGridView1.Height;

                int rowIndex = 2;

                int columnIndex = 2;

                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new Action(() => {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        dataGridView1.Dock = DockStyle.Fill;
                        //dataGridView1.Dock = DockStyle.Fill;
                        dataGridView1.AutoGenerateColumns = false;
                        dataGridView1.ColumnHeadersVisible = false;
                        dataGridView1.RowHeadersVisible = false;
                        //dataGridView1.ReadOnly = true;
                        //dataGridView1.Enabled = false;
                        dataGridView1.TabStop = false;

                        dataGridView1.ReadOnly = false;
                        dataGridView1.Enabled = true;

                        dataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                        dataGridView1.MultiSelect = false;
                        for (int i = 0; i < rowIndex; i++)
                        {
                            dataGridView1.Columns.Add($"Column{i}", $"Column{i}");
                        }
                        for (int i = 0; i < columnIndex; i++)
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Height = Convert.ToInt32((float)DataGridViewH / (float)columnIndex);
                        }
                        for (int i = 0; i < rowIndex; i++)
                        {
                            for (int j = 0; j < columnIndex; j++)
                            {
                                dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.White;
                            }
                        }
                        for (int i = 0; i < MaterialBoxproperties.Count; i++)
                        {
                            if(MaterialBoxproperties[i].MaterialBoxLocationNumber == EnumMaterialBoxLocationNumber.OutOvenArea)
                            {
                                dataGridView1.Rows[1].Cells[0].Style.BackColor = Color.Green;
                            }
                            if (MaterialBoxproperties[i].MaterialBoxLocationNumber == EnumMaterialBoxLocationNumber.FreeArea1)
                            {
                                dataGridView1.Rows[1].Cells[1].Style.BackColor = Color.Green;
                            }
                            if (MaterialBoxproperties[i].MaterialBoxLocationNumber == EnumMaterialBoxLocationNumber.WeldArea)
                            {
                                dataGridView1.Rows[0].Cells[1].Style.BackColor = Color.Green;
                            }
                            if (MaterialBoxproperties[i].MaterialBoxLocationNumber == EnumMaterialBoxLocationNumber.FreeArea2)
                            {
                                dataGridView1.Rows[0].Cells[0].Style.BackColor = Color.Green;
                            }
                        }
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.ClearSelection();
                        dataGridView1.CurrentCell = null;
                        dataGridView1.FirstDisplayedCell = null;
                    }));
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    dataGridView1.Dock = DockStyle.Fill;
                    dataGridView1.Dock = DockStyle.Fill;
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.ColumnHeadersVisible = false;
                    dataGridView1.RowHeadersVisible = false;
                    //dataGridView1.ReadOnly = true;
                    //dataGridView1.Enabled = false;
                    dataGridView1.TabStop = false;

                    dataGridView1.ReadOnly = false;
                    dataGridView1.Enabled = true;

                    dataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                    dataGridView1.MultiSelect = false;
                    for (int i = 0; i < rowIndex; i++)
                    {
                        dataGridView1.Columns.Add($"Column{i}", $"Column{i}");
                    }
                    for (int i = 0; i < columnIndex; i++)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Height = Convert.ToInt32((float)DataGridViewH / (float)columnIndex);
                    }
                    for (int i = 0; i < rowIndex; i++)
                    {
                        for (int j = 0; j < columnIndex; j++)
                        {
                            if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Welded)
                                dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Green;
                            if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Jumping)
                                dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Red;
                            if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Unwelded)
                                dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.White;
                        }
                    }


                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.ClearSelection();
                    dataGridView1.CurrentCell = null;
                    dataGridView1.FirstDisplayedCell = null;
                }

            }
            catch (Exception ex)
            {
            }
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 检查是否点击了有效的单元格（非标题行和列）
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cell = dataGridView1[e.ColumnIndex, e.RowIndex];
                cell.Style.BackColor = cell.Style.BackColor == Color.Red ? Color.White : Color.Red;

                Materialproperties[e.RowIndex][e.ColumnIndex].Materialstate = Materialproperties[e.RowIndex][e.ColumnIndex].Materialstate == EnumMaterialstate.Jumping ? EnumMaterialstate.Unwelded : EnumMaterialstate.Jumping;

                dataGridView1.ClearSelection();
                dataGridView1.CurrentCell = null;
                dataGridView1.FirstDisplayedCell = null;
            }
        }

        public void SetDataGridView_Color(int rowIndex, int columnIndex, int COLOR)
        {
            try
            {
                if (rowIndex > Materialproperties.Count || columnIndex > Materialproperties[0].Count)
                    return;
                if (rowIndex > -1 && columnIndex > -1)
                {
                    if (COLOR == 1)
                    {
                        Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Welded;
                        dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Green;
                    }
                    if (COLOR == 2)
                    {
                        Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Jumping;
                        dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Red;
                    }
                    if (COLOR == 0)
                    {
                        Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Unwelded;
                        dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.White;
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }



        private void DataGridView2_Data()
        {
            try
            {
                if (Materialproperties == null)
                {
                    return;
                }
                else
                {
                    if (Materialproperties.Count <= 0)
                    {
                        return;
                    }
                    else
                    {
                        if (Materialproperties[0].Count <= 0)
                        {
                            return;
                        }
                    }
                }

                int DataGridViewH = dataGridView2.Height;

                int rowIndex = Materialproperties.Count;

                int columnIndex = Materialproperties[0].Count;

                if (dataGridView2.InvokeRequired)
                {
                    dataGridView2.Invoke(new Action(() => {
                        dataGridView2.Rows.Clear();
                        dataGridView2.Columns.Clear();
                        dataGridView2.Dock = DockStyle.Fill;
                        //dataGridView2.Dock = DockStyle.Fill;
                        dataGridView2.AutoGenerateColumns = false;
                        dataGridView2.ColumnHeadersVisible = false;
                        dataGridView2.RowHeadersVisible = false;
                        //dataGridView2.ReadOnly = true;
                        //dataGridView2.Enabled = false;
                        dataGridView2.TabStop = false;

                        dataGridView2.ReadOnly = false;
                        dataGridView2.Enabled = true;

                        dataGridView2.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                        dataGridView2.MultiSelect = false;
                        for (int i = 0; i < rowIndex; i++)
                        {
                            dataGridView2.Columns.Add($"Column{i}", $"Column{i}");
                        }
                        for (int i = 0; i < columnIndex; i++)
                        {
                            dataGridView2.Rows.Add();
                            dataGridView2.Rows[i].Height = Convert.ToInt32((float)DataGridViewH / (float)columnIndex);
                        }
                        for (int i = 0; i < rowIndex; i++)
                        {
                            for (int j = 0; j < columnIndex; j++)
                            {
                                if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Welded)
                                    dataGridView2.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Green;
                                if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Jumping)
                                    dataGridView2.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Red;
                                if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Unwelded)
                                    dataGridView2.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.White;
                            }
                        }
                        dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView2.ClearSelection();
                        dataGridView2.CurrentCell = null;
                        dataGridView2.FirstDisplayedCell = null;
                    }));
                }
                else
                {
                    dataGridView2.Rows.Clear();
                    dataGridView2.Columns.Clear();
                    dataGridView2.Dock = DockStyle.Fill;
                    dataGridView2.Dock = DockStyle.Fill;
                    dataGridView2.AutoGenerateColumns = false;
                    dataGridView2.ColumnHeadersVisible = false;
                    dataGridView2.RowHeadersVisible = false;
                    //dataGridView2.ReadOnly = true;
                    //dataGridView2.Enabled = false;
                    dataGridView2.TabStop = false;

                    dataGridView2.ReadOnly = false;
                    dataGridView2.Enabled = true;

                    dataGridView2.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                    dataGridView2.MultiSelect = false;
                    for (int i = 0; i < rowIndex; i++)
                    {
                        dataGridView2.Columns.Add($"Column{i}", $"Column{i}");
                    }
                    for (int i = 0; i < columnIndex; i++)
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Height = Convert.ToInt32((float)DataGridViewH / (float)columnIndex);
                    }
                    for (int i = 0; i < rowIndex; i++)
                    {
                        for (int j = 0; j < columnIndex; j++)
                        {
                            if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Welded)
                                dataGridView2.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Green;
                            if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Jumping)
                                dataGridView2.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Red;
                            if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Unwelded)
                                dataGridView2.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.White;
                        }
                    }


                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.ClearSelection();
                    dataGridView2.CurrentCell = null;
                    dataGridView2.FirstDisplayedCell = null;
                }

            }
            catch (Exception ex)
            {
            }
        }



        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 检查是否点击了有效的单元格（非标题行和列）
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cell = dataGridView2[e.ColumnIndex, e.RowIndex];
                cell.Style.BackColor = cell.Style.BackColor == Color.Red ? Color.White : Color.Red;

                Materialproperties[e.RowIndex][e.ColumnIndex].Materialstate = Materialproperties[e.RowIndex][e.ColumnIndex].Materialstate == EnumMaterialstate.Jumping ? EnumMaterialstate.Unwelded : EnumMaterialstate.Jumping;

                dataGridView2.ClearSelection();
                dataGridView2.CurrentCell = null;
                dataGridView2.FirstDisplayedCell = null;
            }
        }

        public void SetDataGridView2_Color(int rowIndex, int columnIndex, int COLOR)
        {
            try
            {
                if (rowIndex > Materialproperties.Count || columnIndex > Materialproperties[0].Count)
                    return;
                if (rowIndex > -1 && columnIndex > -1)
                {
                    if (COLOR == 1)
                    {
                        Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Welded;
                        dataGridView2.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Green;
                    }
                    if (COLOR == 2)
                    {
                        Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Jumping;
                        dataGridView2.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Red;
                    }
                    if (COLOR == 0)
                    {
                        Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Unwelded;
                        dataGridView2.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.White;
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private List<List<EnumMaterialproperties>> GetDataGridViewCellStates(DataGridView dataGridView)
        {
            try
            {
                int rowCount = dataGridView.RowCount;
                int columnCount = dataGridView.ColumnCount;
                int[,] cellStates = new int[rowCount, columnCount];

                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                    {
                        //int actualRowIndex = rowCount - 1 - rowIndex;
                        var cell = dataGridView[columnIndex, rowIndex];

                        if (cell.Style.BackColor == Color.Red)
                        {
                            Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Jumping;
                        }
                        else if (cell.Style.BackColor == Color.White)
                        {
                            Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Unwelded;
                        }
                        else
                        {
                            Materialproperties[rowIndex][columnIndex].Materialstate = EnumMaterialstate.Welded;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }


            return Materialproperties;
        }



        #endregion

        #region public mothed




        #endregion




    }
}
