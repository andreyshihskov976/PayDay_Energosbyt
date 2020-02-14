using System;
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
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "" )
            {
                try
                {
                    textBox1.Text = textBox1.Text.ToUpper();
                    textBox4.Text = textBox4.Text.ToUpper();
                    string exists = null;
                    string rs = textBox1.Text + textBox2.Text + textBox3.Text + textBox4.Text + textBox5.Text;
                    MySqlOperations.Select_Text(MySqlQueries.Exists_Rasch_Scheta, ref exists, null, rs);
                    if (exists == "0")
                    {
                        MySqlOperations.Insert_Update(MySqlQueries.Insert_Rasch_Scheta, ID, textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString());
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
                MessageBox.Show("Поля не заполнены." + '\n' + "Введите данные.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "")
            {
                try
                {
                    textBox1.Text = textBox1.Text.ToUpper();
                    textBox4.Text = textBox4.Text.ToUpper();
                    string exists = null;
                    string rs = textBox1.Text + textBox2.Text + textBox3.Text + textBox4.Text + textBox5.Text;
                    MySqlOperations.Select_Text(MySqlQueries.Exists_Rasch_Scheta, ref exists, null, rs);
                    if (exists == "0")
                    {

                        MySqlOperations.Insert_Update(MySqlQueries.Update_Rasch_Scheta, ID, textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString());
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
                MessageBox.Show("Поля не заполнены." + '\n' + "Введите данные.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Rachetniki_FormClosed(object sender, FormClosedEventArgs e)
        {
            Raschetniki_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Raschetniki_Closed;
    }
}
