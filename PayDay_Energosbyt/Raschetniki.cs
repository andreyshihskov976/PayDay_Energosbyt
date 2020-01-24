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
    public partial class Raschetniki : Form
    {
        public string ID = null;
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        public Raschetniki()
        {
            InitializeComponent();
        }

        public Raschetniki(MySqlOperations mySqlOperations, MySqlQueries mySqlQueries)
        {
            InitializeComponent();
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "" )
            {
                try
                {
                    MySqlOperations.Insert_Update(MySqlQueries.Insert_Rasch_Scheta, ID, textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString());
                    MessageBox.Show("Операция выполнена успешно.", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + '.', "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Поле не заполнено." + '\n' + "Введите наименование отдела.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "")
            {
                try
                {
                    MySqlOperations.Insert_Update(MySqlQueries.Update_Rasch_Scheta, ID,textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString());
                    MessageBox.Show("Операция выполнена успешно.", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + '.', "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Поле не заполнено." + '\n' + "Введите наименование отдела.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Rachetniki_FormClosed(object sender, FormClosedEventArgs e)
        {
            Raschetniki_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Raschetniki_Closed;
    }
}
