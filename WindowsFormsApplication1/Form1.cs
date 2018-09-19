using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = user.UserName + "----" + user.Password;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable table = GetTableData();

            PrintHelp ph = new PrintHelp();
            ph.Print_创新工作室申报书(table, true);
        }

        private static DataTable GetTableData()
        {
            DataTable table = new DataTable();
            DataColumn column1 = new DataColumn("name", typeof(string));
            DataColumn column2 = new DataColumn("id", typeof(int));
            table.Columns.Add(column1);
            table.Columns.Add(column2);
            DataRow row = table.NewRow();
            for (int i = 0; i < 10; i++)
            {
                row = table.NewRow();
                row[0] = "霍金" + i + 1;
                row[1] = i + 1;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
