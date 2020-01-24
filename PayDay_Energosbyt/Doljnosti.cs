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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    MySqlOperations.Insert_Update(MySqlQueries.Insert_Doljnosti, ID, textBox1.Text.ToString());
                    MessageBox.Show("Операция выполнена успешно.", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message+'.', "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Поле не заполнено." + '\n' + "Введите наименование отдела.","Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MySqlOperations.Insert_Update(MySqlQueries.Update_Doljnosti, ID, textBox1.Text.ToString());
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

        private void Doljnosti_FormClosed(object sender, FormClosedEventArgs e)
        {
            Doljnosti_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Doljnosti_Closed;
    }
}
