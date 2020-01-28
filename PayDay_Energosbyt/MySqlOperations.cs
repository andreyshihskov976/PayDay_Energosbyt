using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace PayDay_Energosbyt
{
    public class MySqlOperations
    {
        public Microsoft.Office.Interop.Word.Application WordApp = new Microsoft.Office.Interop.Word.Application();
        public Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();

        public MySqlConnection mySqlConnection = new MySqlConnection("server=localhost; user=root; database=payday; port=3306; password=; charset=utf8;");
        public MySqlQueries MySqlQueries = null;

        MySqlDataReader sqlDataReader = null;

        MySqlDataAdapter dataAdapter = null;

        DataSet dataSet = null;

        MySqlCommand sqlCommand = null;

        public MySqlOperations(MySqlQueries sqlQueries)
        {
            this.MySqlQueries = sqlQueries;
        }
        //Подключение (Закрытие подключения) к Базе Данных
        public void OpenConnection()
        {
            mySqlConnection.Open();
        }
        public void CloseConnection()
        {
            mySqlConnection.Close();
        }
        //Подключение (Закрытие подключения) к Базе Данных

        //Универсальные методы
        public void Select_DataGridView(string query, DataGridView dataGridView, string ID = null, string Value1 = null, string Value2 = null, string Value3 = null)
        {
            try
            {
                dataGridView.DataSource = null;
                dataSet = new DataSet();
                sqlCommand = new MySqlCommand(query, mySqlConnection);
                sqlCommand.Parameters.AddWithValue("ID", ID);
                sqlCommand.Parameters.AddWithValue("Value1", Value1);
                sqlCommand.Parameters.AddWithValue("Value2", Value2);
                sqlCommand.Parameters.AddWithValue("Value3", Value3);
                dataAdapter = new MySqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataSet);
                dataGridView.DataSource = dataSet.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Select_ComboBox(string query, ComboBox comboBox)
        {
            try
            {
                sqlCommand = new MySqlCommand(query, mySqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    comboBox.Items.Add(Convert.ToString(sqlDataReader[0]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
                if (comboBox.Items.Count != 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
        }
        public void Select_ComboBox_Editing(string query, ComboBox comboBox)
        {
            try
            {
                sqlCommand = new MySqlCommand(query, mySqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    comboBox.Items.Add(Convert.ToString(sqlDataReader[0]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
                if (comboBox.Items.Count != 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
        }
        public void Search_In_ComboBox(string In, ComboBox comboBox)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString() == In)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        public void Search_In_ComboBox_Identify(string Identify, ComboBox comboBox)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString().Contains(Identify))
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        public string Select_ID_From_ComboBox(string query, string Value)
        {
            string ID = null;
            try
            {
                sqlCommand = new MySqlCommand(query, mySqlConnection);
                sqlCommand.Parameters.AddWithValue("Value1", Value);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    ID = Convert.ToString(sqlDataReader[0]);
                    break;
                }
                return ID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
                
            }
            
        }

        public void Delete(string query, string query2, DataGridView dataGridView, string ID)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sqlCommand = new MySqlCommand(query, mySqlConnection);
                sqlCommand.Parameters.AddWithValue("ID", ID);
                sqlCommand.ExecuteNonQuery();
            }
            Select_DataGridView(query2, dataGridView);
        }

        public void Print_String(DataGridView dataGridView1)
        {

            try
            {
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
            }
        }

        public void Select_Text(string query, ref string output, string ID = null, string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null, string Value6 = null, string Value7 = null, string Value8 = null)
        {
            try
            {
                sqlCommand = new MySqlCommand(query, mySqlConnection);
                sqlCommand.Parameters.AddWithValue("Value1", Value1);
                sqlCommand.Parameters.AddWithValue("Value2", Value2);
                sqlCommand.Parameters.AddWithValue("Value3", Value3);
                sqlCommand.Parameters.AddWithValue("Value4", Value4);
                sqlCommand.Parameters.AddWithValue("Value5", Value5);
                sqlCommand.Parameters.AddWithValue("Value6", Value6);
                sqlCommand.Parameters.AddWithValue("Value7", Value7);
                sqlCommand.Parameters.AddWithValue("Value8", Value8);
                sqlCommand.Parameters.AddWithValue("ID", ID);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    output = Convert.ToString(sqlDataReader[0]);
                    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
            }
        }

        //Универсальные методы

        //Методы для таблиц Должности, Отдел

        public void Insert_Update(string query, string ID = null, string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null, string Value6 = null, string Value7 = null, string Value8 = null)
        {
            sqlCommand = new MySqlCommand(query, mySqlConnection);
            sqlCommand.Parameters.AddWithValue("Value1", Value1);
            sqlCommand.Parameters.AddWithValue("Value2", Value2);
            sqlCommand.Parameters.AddWithValue("Value3", Value3);
            sqlCommand.Parameters.AddWithValue("Value4", Value4);
            sqlCommand.Parameters.AddWithValue("Value5", Value5);
            sqlCommand.Parameters.AddWithValue("Value6", Value6);
            sqlCommand.Parameters.AddWithValue("Value7", Value7);
            sqlCommand.Parameters.AddWithValue("Value8", Value8);
            sqlCommand.Parameters.AddWithValue("ID", ID);
            sqlCommand.ExecuteNonQuery();
        }

        //Методы для таблиц Должности, Отдел

        //Методы для таблиц График, Табель

        public void Insert_Grafik(MySqlQueries mySqlQueries, DateTimePicker dateTimePicker1,string query, string ID)
        {
            string output = string.Empty;
            string Identify = string.Empty;
            string RabVrem = string.Empty;
            DateTime Begin = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);
            DateTime End = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, DateTime.DaysInMonth(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month));
            Select_Text(mySqlQueries.Exists_Grafik_Raboty, ref output, ID, Begin.Year.ToString(), Begin.Month.ToString(), Begin.Day.ToString());
            if (output == "0")
            {
                while (Begin <= End)
                {
                    if (Begin.DayOfWeek.ToString() == "Friday")
                    {
                        Identify = "Р";
                        RabVrem = "7.2";
                    }
                    else if (Begin.DayOfWeek.ToString() == "Saturday" || Begin.DayOfWeek.ToString() == "Sunday")
                    {
                        Identify = "В";
                        RabVrem = "0";
                    }
                    else
                    {
                        Identify = "Р";
                        RabVrem = "8.2";
                    }
                    Insert_Update(query, ID, Begin.Year.ToString(), Begin.Month.ToString(), Begin.Day.ToString(), Identify, RabVrem);
                    Begin = Begin.AddDays(1);
                }
                MessageBox.Show("Операция выполнена успешно.", "Успех");
            }
            else
            {
                MessageBox.Show("Записи уже существуют.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void Insert_Tabel(MySqlQueries mySqlQueries, DataGridView dataGridView, DateTimePicker dateTimePicker1, string query, string ID)
        {
            string output = string.Empty;
            string Identify = string.Empty;
            string RabVrem = string.Empty;
            int index = 0;
            DateTime Begin = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);
            DateTime End = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, DateTime.DaysInMonth(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month));
            Select_DataGridView(mySqlQueries.Select_Grafik_For_Tabel, dataGridView, ID, dateTimePicker1.Value.Month.ToString(), dateTimePicker1.Value.Year.ToString());
            Select_Text(mySqlQueries.Exists_Tabel, ref output, ID, Begin.Year.ToString(), Begin.Month.ToString(), Begin.Day.ToString());
            if (output == "0")
            {
                while (Begin <= End)
                {
                    Insert_Update(query, ID, dataGridView.Rows[index].Cells[1].Value.ToString(), dataGridView.Rows[index].Cells[2].Value.ToString(), dataGridView.Rows[index].Cells[3].Value.ToString(), dataGridView.Rows[index].Cells[4].Value.ToString(), dataGridView.Rows[index].Cells[5].Value.ToString().Replace(',','.'));
                    Begin = Begin.AddDays(1);
                    index++;
                }
                MessageBox.Show("Операция выполнена успешно.", "Успех");
            }
            else
            {
                MessageBox.Show("Записи уже существуют.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Методы для таблиц График, Табель

    }
}
