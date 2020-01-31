using System;
using System.Windows.Forms;

namespace PayDay_Energosbyt
{
    public partial class Print_Grafik_Dialog : Form
    {
        string ID = null;
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        public Print_Grafik_Dialog()
        {
            InitializeComponent();
        }

        public Print_Grafik_Dialog(string iD, MySqlOperations mySqlOperations, MySqlQueries mySqlQueries)
        {
            InitializeComponent();
            ID = iD;
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlOperations.Print_Grafik(MySqlQueries, dataGridView1, dateTimePicker1, saveFileDialog1, ID);
            this.Close();
        }
    }
}
