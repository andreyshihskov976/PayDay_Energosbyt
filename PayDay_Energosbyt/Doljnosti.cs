using System;
using System.Windows.Forms;

namespace PayDay_Energosbyt
{
    public partial class Doljnosti : Form
    {
        public string ID = null;
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        public Doljnosti()
        {
            InitializeComponent();
        }
  
        public Doljnosti(MySqlOperations mySqlOperations, MySqlQueries mySqlQueries)
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
                    MySqlOperations.Select_Text(MySqlQueries.Exists_Doljnosti, ref exists, null, textBox1.Text);
                    if (exists == "0")
                    {
                        MySqlOperations.Insert_Update(MySqlQueries.Insert_Doljnosti, ID, textBox1.Text.ToString());
                        MessageBox.Show("Операция выполнена успешно.", "Успех");
                    }
                    else
                    {
                        MessageBox.Show("Запись уже существует.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message+'.', "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Поле не заполнено." + '\n' + "Введите наименование должности.","Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MySqlOperations.Select_Text(MySqlQueries.Exists_Doljnosti, ref exists, null, textBox1.Text);
                    if (exists == "0")
                    {
                        MySqlOperations.Insert_Update(MySqlQueries.Update_Doljnosti, ID, textBox1.Text.ToString());
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
                MessageBox.Show("Поле не заполнено." + '\n' + "Введите наименование должности.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Doljnosti_FormClosed(object sender, FormClosedEventArgs e)
        {
            Doljnosti_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Doljnosti_Closed;
    }
}
