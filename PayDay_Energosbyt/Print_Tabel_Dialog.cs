﻿using System;
using System.Windows.Forms;

namespace PayDay_Energosbyt
{
    public partial class Print_Tabel_Dialog : Form
    {
        string ID = null;
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        public Print_Tabel_Dialog()
        {
            InitializeComponent();
        }

        public Print_Tabel_Dialog(string iD, MySqlOperations mySqlOperations, MySqlQueries mySqlQueries)
        {
            InitializeComponent();
            ID = iD;
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MySqlOperations.Print_Tabel(MySqlQueries, dataGridView1, dateTimePicker1, saveFileDialog1, ID);
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            this.Close();
        }
    }
}
