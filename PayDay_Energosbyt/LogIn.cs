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
    public partial class LogIn : Form
    {
        public MySqlOperations MySqlOperations = null;
        public MySqlQueries MySqlQueries = null;
        public string Login = string.Empty;
        public string Password = string.Empty;

        public LogIn()
        {
            InitializeComponent();
            MySqlQueries = new MySqlQueries();
            MySqlOperations = new MySqlOperations(MySqlQueries);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MySqlOperations.OpenConnection();
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string output = string.Empty;
                Login = textBox2.Text;
                Password = textBox1.Text;
                MySqlOperations.Select_Text(MySqlQueries.Exists_User, ref output, null, Login, Password);
                if (output == "1")
                {
                    MySqlOperations.Select_Text(MySqlQueries.Select_FIO_Usera, ref output, null, Login, Password);
                    MessageBox.Show("Вы вошли, как " + output + ".", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    MessageBox.Show("Вы ввели неверный логин или пароль.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Вы не ввели логин или пароль.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            MySqlOperations.CloseConnection();
        }
    }
}
