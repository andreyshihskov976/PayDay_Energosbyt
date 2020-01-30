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

        public Print_Grafik_Dialog(MySqlOperations mySqlOperations, MySqlQueries mySqlQueries, string iD)
        {
            InitializeComponent();
            ID = iD;
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlOperations.Print_Grafik(MySqlQueries, dataGridView1, dateTimePicker1, ID);
            this.Close();
        }
    }
}
