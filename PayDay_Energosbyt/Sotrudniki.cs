﻿using System;
using System.Windows.Forms;

namespace PayDay_Energosbyt
{
    public partial class Sotrudniki : Form
    {
        public string ID = null;
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        public Sotrudniki()
        {
            InitializeComponent();
        }

        public Sotrudniki(MySqlOperations mySqlOperations, MySqlQueries mySqlQueries)
        {
            InitializeComponent();
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Otdely_ComboBox, comboBox1);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Doljnosti_ComboBox, comboBox2);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Oklad_ComboBox, comboBox3);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Rasch_Scheta_ComboBox, comboBox4);
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "")
            {
                try
                {
                    string ID_Otdela = MySqlOperations.Select_ID_From_ComboBox(MySqlQueries.Select_ID_Otdela, comboBox1.Text);
                    string ID_Doljnosti = MySqlOperations.Select_ID_From_ComboBox(MySqlQueries.Select_ID_Doljnosti, comboBox2.Text);
                    string ID_Oklada = MySqlOperations.Select_ID_From_ComboBox(MySqlQueries.Select_ID_Oklada, comboBox3.Text);
                    string ID_Rasch_Scheta = MySqlOperations.Select_ID_From_ComboBox(MySqlQueries.Select_ID_Rasch_Scheta, comboBox4.Text);
                    MySqlOperations.Insert_Update(MySqlQueries.Insert_Sotrudniki, ID, textBox1.Text.ToString(),textBox2.Text.ToString(), textBox3.Text.ToString(),ID_Otdela,ID_Doljnosti,ID_Oklada,ID_Rasch_Scheta);
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
                MessageBox.Show("Поля не заполнены." + '\n' + "Введите данные.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "")
            {
                try
                {
                    string ID_Otdela = MySqlOperations.Select_ID_From_ComboBox(MySqlQueries.Select_ID_Otdela, comboBox1.Text);
                    string ID_Doljnosti = MySqlOperations.Select_ID_From_ComboBox(MySqlQueries.Select_ID_Doljnosti, comboBox2.Text);
                    string ID_Oklada = MySqlOperations.Select_ID_From_ComboBox(MySqlQueries.Select_ID_Oklada, comboBox3.Text);
                    string ID_Rasch_Scheta = MySqlOperations.Select_ID_From_ComboBox(MySqlQueries.Select_ID_Rasch_Scheta, comboBox4.Text);
                    MySqlOperations.Insert_Update(MySqlQueries.Update_Sotrudniki, ID, textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString(), ID_Otdela, ID_Doljnosti, ID_Oklada, ID_Rasch_Scheta);
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
                MessageBox.Show("Поля не заполнены." + '\n' + "Введите данные.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Sotrudniki_FormClosed(object sender, FormClosedEventArgs e)
        {
            Sotrudniki_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Sotrudniki_Closed;
    }
}
