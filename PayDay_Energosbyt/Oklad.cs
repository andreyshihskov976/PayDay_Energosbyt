using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayDay_Energosbyt
{
    public partial class Oklad : Form
    {
        public string ID = null;
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        public Oklad()
        {
            InitializeComponent();
        }

        public Oklad(MySqlOperations mySqlOperations, MySqlQueries mySqlQueries)
        {
            InitializeComponent();
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.MaxDate = DateTime.Parse(DateTime.Now.ToShortDateString());
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value.AddDays(1);
            dateTimePicker2.MaxDate = dateTimePicker1.Value.AddYears(10);
            dateTimePicker2.Value = dateTimePicker2.MinDate;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    string exists = null;
                    string date1 = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
                    string date2 = dateTimePicker2.Value.Year.ToString() + "-" + dateTimePicker2.Value.Month.ToString() + "-" + dateTimePicker2.Value.Day.ToString();
                    MySqlOperations.Select_Text(MySqlQueries.Exists_Oklad, ref exists, null, textBox1.Text, date1, date2);
                    if (exists == "0")
                    {
                        MySqlOperations.Insert_Update(MySqlQueries.Insert_Oklad, ID, textBox1.Text.ToString(), date1, date2);
                        MessageBox.Show("Операция выполнена успешно.", "Успех");
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
                MessageBox.Show("Поле не заполнено." + '\n' + "Введите значение оклада.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    string exists = null;
                    string date1 = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
                    string date2 = dateTimePicker2.Value.Year.ToString() + "-" + dateTimePicker2.Value.Month.ToString() + "-" + dateTimePicker2.Value.Day.ToString();
                    MySqlOperations.Select_Text(MySqlQueries.Exists_Oklad, ref exists, null, textBox1.Text, date1, date2);
                    if (exists == "0")
                    {
                        MySqlOperations.Insert_Update(MySqlQueries.Update_Oklad, ID, textBox1.Text.ToString(), date1, date2);
                        MessageBox.Show("Операция выполнена успешно.", "Успех");
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
                MessageBox.Show("Поле не заполнено." + '\n' + "Введите значение оклада.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Oklad_FormClosed(object sender, FormClosedEventArgs e)
        {
            Oklad_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Oklad_Closed;
    }
}
