using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonPanelClsLib
{
    public partial class ListmessagesBox : Form
    {
        public ListmessagesBox()
        {
            InitializeComponent();
        }

        public void ShowText(string title)
        {
            this.Text = title;
        }

        public void DataGridView1Show(List<TextContent> texts)
        {
            DataTable table1 = new DataTable();
            table1.Columns.Add("名称", typeof(string));
            table1.Columns.Add("X(um)", typeof(string));
            table1.Columns.Add("Y(um)", typeof(string));
            table1.Columns.Add("Theta(°)", typeof(string));

            foreach (TextContent text in texts)
            {
                table1.Rows.Add(text.Name, text.Value1, text.Value2, text.Value3);
            }

            dataGridView1.DataSource = table1;

            dataGridView1.Refresh();

        }

        public void DataGridView2Show(List<TextContent> texts)
        {
            DataTable table2 = new DataTable();
            table2.Columns.Add("名称", typeof(string));
            table2.Columns.Add("X(mm)", typeof(string));
            table2.Columns.Add("Y(mm)", typeof(string));
            table2.Columns.Add("Z(mm)", typeof(string));

            foreach (TextContent text in texts)
            {
                table2.Rows.Add(text.Name, text.Value1, text.Value2, text.Value3);
            }

            dataGridView2.DataSource = table2;

            dataGridView2.Refresh();

        }

    }

    public class TextContent
    {
        public string Name { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }


        public TextContent()
        {

        }

        public TextContent(string Name, string Value1, string Value2, string Value3)
        {
            this.Name = Name;
            this.Value1 = Value1;
            this.Value2 = Value2;
            this.Value3 = Value3;
        }

    }

}
