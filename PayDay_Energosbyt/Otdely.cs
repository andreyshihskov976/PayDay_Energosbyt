using System;
using System.Windows.Forms;

namespace PayDay_Energosbyt
{
    public partial class Otdely : Form
    {
        public string ID = null;
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        public Otdely()
        {
            InitializeComponent();
        }

        public Otdely(MySqlOperations mySqlOperations, MySqlQueries mySqlQueries)
        {
            InitializeComponent();
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    string exists = null;
                    MySqlOperations.Select_Text(MySqlQueries.Exists_Otdely, ref exists, null, textBox1.Text);
                    if (exists == "0")
                    {
                        MySqlOperations.Insert_Update(MySqlQueries.Insert_Otdely, ID, textBox1.Text.ToString());
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
                MessageBox.Show("Поле не заполнено." + '\n' + "Введите наименование отдела.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    string exists = null;
                    MySqlOperations.Select_Text(MySqlQueries.Exists_Otdely, ref exists, null, textBox1.Text);
                    if (exists == "0")
                    {
                        MySqlOperations.Insert_Update(MySqlQueries.Update_Otdely, ID, textBox1.Text.ToString());
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
                MessageBox.Show("Поле не заполнено." + '\n' + "Введите наименование отдела.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Otdely_FormClosed(object sender, FormClosedEventArgs e)
        {
            Otdely_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Otdely_Closed;
    }
}
