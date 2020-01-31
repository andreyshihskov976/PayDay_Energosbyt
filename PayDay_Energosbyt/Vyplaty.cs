using System;
using System.Windows.Forms;

namespace PayDay_Energosbyt
{
    public partial class Vyplaty : Form
    {
        public string ID = null;
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        public Vyplaty()
        {
            InitializeComponent();
        }

        public Vyplaty(MySqlOperations mySqlOperations, MySqlQueries mySqlQueries)
        {
            InitializeComponent();
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Sotrudniki_ComboBox, comboBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                try
                {
                    string date1 = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
                    string date2 = dateTimePicker2.Value.Year.ToString() + "-" + dateTimePicker2.Value.Month.ToString() + "-" + dateTimePicker2.Value.Day.ToString();
                    string date3 = dateTimePicker2.Value.Year.ToString() + "-" + dateTimePicker3.Value.Month.ToString() + "-" + dateTimePicker3.Value.Day.ToString();
                    string exists = null;
                    MySqlOperations.Select_Text(MySqlQueries.Exists_Vyplaty, ref exists, ID, date1, date2, date3);
                    if (exists == "0")
                    {
                        MySqlOperations.Select_Text(MySqlQueries.Exists_Grafik_Raboty_Vylaty, ref exists, ID, date2, date3);
                        if (exists == "1")
                        {
                            MySqlOperations.Select_Text(MySqlQueries.Exists_Tabel_Print, ref exists, ID, date2, date3);
                            if (exists == "1")
                            {
                                MySqlOperations.Insert_Update(MySqlQueries.Insert_Vyplaty, ID, date1, date2, date3, textBox1.Text, textBox2.Text, textBox3.Text);
                                MessageBox.Show("Операция выполнена успешно.", "Успех");
                            }
                            else
                                MessageBox.Show("На выбранного вами сотрудника отсутствует табель на выбранные вами числа." + '\n' + "Заполните табель выбранного сотрудника на выбранные вами числа.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                            MessageBox.Show("На выбранного вами сотрудника отсутствует график работы на выбранные вами числа." + '\n' + "Заполните график работы для должности выбранного сотрудника на выбранные вами числа.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Запись уже существует.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + '.', "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Поля не заполнены." + '\n' + "Нажмите кнопку Расчет.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            this.Close();
        }

        private void Vyplaty_FormClosed(object sender, FormClosedEventArgs e)
        {
            Vyplaty_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Vyplaty_Closed;

        private void button3_Click(object sender, EventArgs e)
        {
            //string nachisleno = "0";
            //string date1 = dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString() + '-' + dateTimePicker2.Value.Day.ToString();
            //string date2 = dateTimePicker3.Value.Year.ToString() + '-' + dateTimePicker3.Value.Month.ToString() + '-' + dateTimePicker3.Value.Day.ToString();
            //MySqlOperations.Select_Text(MySqlQueries.Select_Nachisleno, ref nachisleno, ID, date1, date2);
            //textBox1.Text = nachisleno;
            //decimal uderzhano = (decimal.Parse(nachisleno) * 15)/100;
            //textBox2.Text = uderzhano.ToString();
            //textBox3.Text = (decimal.Parse(nachisleno) - uderzhano).ToString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker3.Value = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, DateTime.DaysInMonth(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month));
            Calculating();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox3.Text = (decimal.Parse(textBox1.Text) - decimal.Parse(textBox2.Text)).ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlOperations.Select_Text(MySqlQueries.Select_ID_Sotrudnika, ref ID, null, comboBox1.Text);
            Calculating();
        }

        private void Calculating()
        {
            string nachisleno = "0";
            string date1 = dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString() + '-' + dateTimePicker2.Value.Day.ToString();
            string date2 = dateTimePicker3.Value.Year.ToString() + '-' + dateTimePicker3.Value.Month.ToString() + '-' + dateTimePicker3.Value.Day.ToString();
            MySqlOperations.Select_Text(MySqlQueries.Select_Nachisleno, ref nachisleno, ID, date1, date2);
            textBox1.Text = nachisleno;
            decimal uderzhano = (decimal.Parse(nachisleno) * 15) / 100;
            textBox2.Text = uderzhano.ToString();
            textBox3.Text = (decimal.Parse(nachisleno) - uderzhano).ToString();
        }
    }
}
