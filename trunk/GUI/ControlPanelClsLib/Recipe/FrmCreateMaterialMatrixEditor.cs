using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPanelClsLib
{
    public partial class FrmCreateMaterialMatrixEditor : Form
    {
        string theClickButton = "cancel";

        List<List<EnumMaterialproperties>> Materialproperties = new List<List<EnumMaterialproperties>>();


        public FrmCreateMaterialMatrixEditor()
        {
            InitializeComponent();
        }

        public void InitMatrix(List<List<EnumMaterialproperties>> materialMat)
        {
            Materialproperties = materialMat;
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
                if(Materialproperties == null)
                {
                    return;
                }
                else
                {
                    if(Materialproperties.Count <= 0)
                    {
                        return;
                    }
                    else
                    {
                        if(Materialproperties[0].Count <= 0)
                        {
                            return;
                        }
                    }
                }

                int DataGridViewH = dataGridView1.Height;

                int DataGridViewW = dataGridView1.Width;

                int rowIndex = Materialproperties.Count;

                int columnIndex = Materialproperties[0].Count;

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
                                if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Welded)
                                    dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Green;
                                if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Jumping)
                                    dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Red;
                                if (Materialproperties[i][j].Materialstate == EnumMaterialstate.Unwelded)
                                    dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.White;
                            }
                        }
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                        dataGridView1.Columns[i].Width = Convert.ToInt32((float)DataGridViewW / (float)rowIndex);
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

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

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

        private void btnOK_Click(object sender, EventArgs e)
        {
            theClickButton = "confirm";
            this.Close();
        }

        private void btnCancell_Click(object sender, EventArgs e)
        {
            theClickButton = "cancel";
            this.Close();
        }

        public string showMessage(List<List<EnumMaterialproperties>> materialMat)
        {
            InitMatrix(materialMat);
            this.ShowDialog();
            return theClickButton;
        }
    }

    public static class FrmCreateMaterialMatrixEditorBox
    {
        public static EnumReturnMaterialproperties FormShow(List<List<EnumMaterialproperties>> materialMat)
        {
            try
            {
                EnumReturnMaterialproperties REF = new EnumReturnMaterialproperties();
                using (FrmCreateMaterialMatrixEditor myMessageBox1 = new FrmCreateMaterialMatrixEditor())
                {
                    string hh = myMessageBox1.showMessage(materialMat);
                    if (hh == "confirm")
                    {
                        REF.MaterialMat = myMessageBox1.GetMatrix();
                        REF.Re = 1;
                    }
                    else
                    {
                        REF.Re = 0;
                    }
                }
                return REF;
            }
            catch { return null; }

        }
    }
}
