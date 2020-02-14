using System;
using System.Windows.Forms;

namespace PayDay_Energosbyt
{
    public partial class Print_Vedomost_Dialog : Form
    {
        string ID = null;
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        public Print_Vedomost_Dialog()
        {
            InitializeComponent();
        }

        public Print_Vedomost_Dialog(MySqlOperations mySqlOperations, MySqlQueries mySqlQueries, string iD = null)
        {
            InitializeComponent();
            ID = iD;
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
            dateTimePicker1.Value = DateTime.Now;
            mySqlOperations.Select_ComboBox(mySqlQueries.Select_Otdely_ComboBox, comboBox1);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MySqlOperations.Select_Text(MySqlQueries.Select_ID_Otdela, ref ID, null, comboBox1.Text);
            MySqlOperations.Print_Vedomost(MySqlQueries, dataGridView1,dateTimePicker1, saveFileDialog1,ID);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            this.Close();
        }
    }
}
