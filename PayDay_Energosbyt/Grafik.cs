using System;
using System.Windows.Forms;

namespace PayDay_Energosbyt
{
    public partial class Grafik : Form
    {
        public string ID = null;
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        DateTime DateTime = new DateTime();
        public Grafik()
        {
            InitializeComponent();
        }

        public Grafik(string id, MySqlOperations mySqlOperations, MySqlQueries mySqlQueries, string query)
        {
            InitializeComponent();
            ID = id;
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
            MySqlOperations.Select_DataGridView(query, dataGridView1, ID);
            int result = 0;
            if (int.TryParse(ID, out result) != true)
            {
                MySqlOperations.Select_Text(MySqlQueries.Select_ID_Doljnosti_Sotrudnika, ref ID, null, ID);
            }
            Cancel_Update();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Items[comboBox1.SelectedIndex].ToString()=="П - праздничный" || comboBox1.Items[comboBox1.SelectedIndex].ToString() == "В - выходной")
            {
                numericUpDown1.Minimum = 0;
                numericUpDown1.Value = 0;
                numericUpDown1.Maximum = 0;
            }
            else
            {
                numericUpDown1.Maximum = decimal.Parse("8,2");
                numericUpDown1.Minimum = decimal.Parse("1,2");
                numericUpDown1.Value = numericUpDown1.Minimum;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cancel_Update();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Update_Table();
        }

        private void Update_Table(string Month = null, string Year = null)
        {
            if (Month == null && Year == null)
            {
                if (dateTimePicker2.Checked)
                {
                    Month = dateTimePicker2.Value.Month.ToString();
                    Year = dateTimePicker2.Value.Year.ToString();
                    MySqlOperations.Select_DataGridView(MySqlQueries.Select_Grafik_Raboty_Filter, dataGridView1, ID, Month, Year);
                }
                else
                {
                    MySqlOperations.Select_DataGridView(MySqlQueries.Select_Grafik_Raboty, dataGridView1, ID);
                }
            }
            else
            {
                MySqlOperations.Select_DataGridView(MySqlQueries.Select_Grafik_Raboty_Filter, dataGridView1, ID, Month, Year);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Grafik_Raboty, dataGridView1, ID);
            dateTimePicker2.Checked = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DateTime = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            MySqlOperations.Search_In_ComboBox_Identify(dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), comboBox1);
            comboBox1.Enabled = true;
            numericUpDown1.Value = decimal.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            numericUpDown1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlOperations.Insert_Update(MySqlQueries.Update_Grafik_Raboty, ID, comboBox1.Text[0].ToString(),numericUpDown1.Value.ToString().Replace(',','.'),dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            Update_Table();
            Cancel_Update();
        }

        private void Cancel_Update()
        {
            comboBox1.SelectedIndex = 0;
            comboBox1.Enabled = false;
            numericUpDown1.Value = numericUpDown1.Minimum;
            numericUpDown1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlOperations.Insert_Grafik(MySqlQueries, dateTimePicker1,MySqlQueries.Insert_Grafik_Raboty,ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + '.', "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Update_Table(dateTimePicker1.Value.Month.ToString(),dateTimePicker1.Value.Year.ToString());
                dateTimePicker2.Value = dateTimePicker1.Value;
                dateTimePicker2.Checked = true;
            }
        }

        
    }
}
