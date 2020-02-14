using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;

namespace PayDay_Energosbyt
{
    public class MySqlOperations
    {
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

        public void Delete(string query, string ID)
        {
            try
            {
                sqlCommand = new MySqlCommand(query, mySqlConnection);
                sqlCommand.Parameters.AddWithValue("ID", ID);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно удалить запись(-и)." + '\n' + "Возможно она(-и) используются в других записях.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Print_Grafik(MySqlQueries mySqlQueries, DataGridView dataGridView, DateTimePicker dateTimePicker, SaveFileDialog saveFileDialog, string ID)
        {
            ExcelApplication ExcelApp = null;
            Workbooks workbooks = null;
            Workbook workbook = null;
            string output = null;
            string fileName = null;
            Select_Text(mySqlQueries.Select_Doljnost_by_ID, ref output, ID);
            saveFileDialog.Title = "Сохранить график работы как";
            saveFileDialog.FileName = "График работы для " + output;
            saveFileDialog.InitialDirectory = Application.StartupPath + "\\Отчетность\\";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
                Select_Text(mySqlQueries.Exists_Grafik_Raboty_Print, ref output, ID, dateTimePicker.Value.Year.ToString() + "-1-1", dateTimePicker.Value.AddYears(1).Year.ToString() + "-1-0");
                if (output == "1")
                {
                    Select_DataGridView(mySqlQueries.Print_Grafik, dataGridView, ID, dateTimePicker.Value.Year.ToString() + "-1-1", dateTimePicker.Value.AddYears(1).Year.ToString() + "-1-0");
                    if (dataGridView.Rows.Count >= 13)
                    {
                        try
                        {
                            Select_Text(mySqlQueries.Select_Doljnost_by_ID, ref output, ID);
                            ExcelApp = new ExcelApplication();
                            workbooks = ExcelApp.Workbooks;
                            workbook = workbooks.Open(Application.StartupPath + "\\Blanks\\Grafik.xlsx");
                            ExcelApp.Cells[1, 22] = dateTimePicker.Value.Year.ToString();
                            ExcelApp.Cells[2, 13] = output;
                            ExcelApp.Cells[26, 11] = dateTimePicker.Value.Year.ToString();
                            Select_Text(mySqlQueries.Select_Kol_Rab_Dney_Grafik, ref output, ID, dateTimePicker.Value.Year.ToString() + "-1-1", dateTimePicker.Value.AddYears(1).Year.ToString() + "-1-0");
                            ExcelApp.Cells[27, 5] = output;
                            Select_Text(mySqlQueries.Select_Kol_PredPrazdn_Dney_Grafik, ref output, ID, dateTimePicker.Value.Year.ToString() + "-1-1", dateTimePicker.Value.AddYears(1).Year.ToString() + "-1-0");
                            ExcelApp.Cells[27, 20] = output;
                            Select_Text(mySqlQueries.Select_Kol_Poln_Dney_Grafik, ref output, ID, dateTimePicker.Value.Year.ToString() + "-1-1", dateTimePicker.Value.AddYears(1).Year.ToString() + "-1-0");
                            ExcelApp.Cells[27, 11] = output;
                            Select_Text(mySqlQueries.Select_Itogo_Rab_Chasov_Grafik, ref output, ID, dateTimePicker.Value.Year.ToString() + "-1-1", dateTimePicker.Value.AddYears(1).Year.ToString() + "-1-0");
                            ExcelApp.Cells[28, 5] = decimal.Parse(output);
                            int ExCol = 2;
                            int ExRow = 7;
                            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                            {
                                if (ExRow == 10 || ExRow == 14 || ExRow == 18)
                                    ExRow++;
                                ExCol = 2;
                                for (int j = 1; j < dataGridView.Columns.Count; j++)
                                {
                                    ExcelApp.Cells[ExRow, ExCol] = dataGridView.Rows[i].Cells[j].Value.ToString();
                                    ExCol++;
                                }
                                ExRow++;
                            }
                            workbook.SaveAs(fileName);
                            ExcelApp.Visible = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            Marshal.ReleaseComObject(workbook);
                            Marshal.ReleaseComObject(workbooks);
                            Marshal.ReleaseComObject(ExcelApp);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не хватает записей графика работы" + '\n' + "для данной должности на выбранный вами год." + '\n' + "Пожалуйста дополните график работы для данной должности." + '\n' + "Необходимо заполнить график на 12 месяцев для выбранного вами года.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Отсутствует график работы" + '\n' + "для данной должности на выбранный вами год." + '\n' + "Пожалуйста заполните график работы для данной должности.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void Print_Tabel(MySqlQueries mySqlQueries, DataGridView dataGridView, DateTimePicker dateTimePicker, SaveFileDialog saveFileDialog, string ID)
        {
            ExcelApplication ExcelApp = null;
            Workbooks workbooks = null;
            Workbook workbook = null;
            string output = null;
            string fileName = null;
            Select_Text(mySqlQueries.Select_FIO_Sotrudnika_by_ID, ref output, ID);
            saveFileDialog.Title = "Сохранить табель как";
            saveFileDialog.FileName = "Табель учета рабочего времени для " + output;
            saveFileDialog.InitialDirectory = Application.StartupPath + "\\Отчетность\\";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
                Select_Text(mySqlQueries.Exists_Tabel_Print, ref output, ID, dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.Month.ToString() + "-1", dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.AddMonths(1).Month.ToString() + "-0");
                if (output == "1")
                {
                    Select_DataGridView(mySqlQueries.Print_Tabel, dataGridView, ID, dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.Month.ToString() + "-1", dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.AddMonths(1).Month.ToString() + "-0");
                    try
                    {
                        Select_Text(mySqlQueries.Select_FIO_Sotrudnika_by_ID, ref output, ID);
                        ExcelApp = new ExcelApplication();
                        workbooks = ExcelApp.Workbooks;
                        workbook = workbooks.Open(Application.StartupPath + "\\Blanks\\Tabel.xlsx");
                        ExcelApp.Cells[3, 16] = dateTimePicker.Text.ToString();
                        ExcelApp.Cells[11, 3] = output;
                        ExcelApp.Cells[11, 2] = ID;
                        Select_Text(mySqlQueries.Select_Otdel_Sotrudnika_by_ID, ref output, ID);
                        ExcelApp.Cells[4, 3] = output.Split(' ')[0];
                        ExcelApp.Cells[4, 6] = output.Split(' ')[1];
                        Select_Text(mySqlQueries.Select_Doljnost_Sotrudnika_by_ID, ref output, ID);
                        ExcelApp.Cells[11, 19] = output;
                        Select_Text(mySqlQueries.Select_Kol_Rab_Dney_Tabel, ref output, ID, dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.Month.ToString() + "-1", dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.AddMonths(1).Month.ToString() + "-0");
                        ExcelApp.Cells[11, 20] = output;
                        Select_Text(mySqlQueries.Select_VP_Tabel, ref output, ID, dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.Month.ToString() + "-1", dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.AddMonths(1).Month.ToString() + "-0");
                        ExcelApp.Cells[11, 21] = output;
                        Select_Text(mySqlQueries.Select_Otrabotano_Tabel, ref output, ID, dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.Month.ToString() + "-1", dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.AddMonths(1).Month.ToString() + "-0");
                        ExcelApp.Cells[11, 22] = decimal.Parse(output);
                        Select_Text(mySqlQueries.Select_Itogo_Rab_Chasov_Grafik, ref output, ID, dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.Month.ToString() + "-1", dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.AddMonths(1).Month.ToString() + "-0");
                        ExcelApp.Cells[28, 5] = output;
                        int ExCol = 3;
                        int ExRow = 12;
                        for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                        {
                            for (int j = 1; j < dataGridView.Columns.Count; j++)
                            {
                                ExcelApp.Cells[ExRow, ExCol] = dataGridView.Rows[0].Cells[j].Value.ToString();
                                ExCol++;
                                if (ExCol == 18 && j == 15)
                                {
                                    ExRow++;
                                    ExCol = 3;
                                }
                            }
                        }
                        workbook.SaveAs(fileName);
                        ExcelApp.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(workbook);
                        Marshal.ReleaseComObject(workbooks);
                        Marshal.ReleaseComObject(ExcelApp);
                    }
                }
                else
                {
                    MessageBox.Show("Отсутствует табель учета рабочего времени" + '\n' + "для данного сотрудника на выбранный вами месяц." + '\n' + "Пожалуйста заполните табель учета рабочего времени для данного сотрудника.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void Print_Listok(MySqlQueries mySqlQueries, DataGridView dataGridView, SaveFileDialog saveFileDialog, string ID = null)
        {
            ExcelApplication ExcelApp = null;
            Workbooks workbooks = null;
            Workbook workbook = null;
            string output = null;
            string fileName = null;
            saveFileDialog.Title = "Сохранить расчетный листок как";
            saveFileDialog.FileName = "Расчетный листок " + dataGridView.SelectedRows[0].Cells[4].Value.ToString();
            saveFileDialog.InitialDirectory = Application.StartupPath + "\\Отчетность\\";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
                try
                {
                    ExcelApp = new ExcelApplication();
                    workbooks = ExcelApp.Workbooks;
                    workbook = workbooks.Open(Application.StartupPath + "\\Blanks\\Listok.xlsx");
                    ExcelApp.Cells[2, 5] = DateTime.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString()).ToShortDateString();
                    ExcelApp.Cells[5, 3] = dataGridView.SelectedRows[0].Cells[4].Value.ToString();
                    decimal Nachisleno = decimal.Parse(dataGridView.SelectedRows[0].Cells[5].Value.ToString());
                    ExcelApp.Cells[10, 3] = Nachisleno;
                    DateTime date1 = DateTime.Parse(dataGridView.SelectedRows[0].Cells[2].Value.ToString().Split(' ')[0]);
                    DateTime date2 = DateTime.Parse(dataGridView.SelectedRows[0].Cells[3].Value.ToString().Split(' ')[0]);
                    Select_Text(mySqlQueries.Select_Otrabotano, ref output, dataGridView.SelectedRows[0].Cells[4].Value.ToString(),date1.Year.ToString()+"-"+date1.Month.ToString()+"-"+date1.Day.ToString(), date2.Year.ToString() + "-" + date2.Month.ToString() + "-"+date2.Day.ToString());
                    ExcelApp.Cells[10, 5] = decimal.Parse(output);
                    Select_Text(mySqlQueries.Select_Kol_Dney_Otrabotano, ref output, dataGridView.SelectedRows[0].Cells[4].Value.ToString(), date1.Year.ToString() + "-" + date1.Month.ToString() + "-" + date1.Day.ToString(), date2.Year.ToString() + "-" + date2.Month.ToString() + "-" + date2.Day.ToString());
                    ExcelApp.Cells[10, 4] = decimal.Parse(output);
                    ExcelApp.Cells[13, 3] = Nachisleno;
                    ExcelApp.Cells[13, 7] = dataGridView.SelectedRows[0].Cells[6].Value.ToString();
                    ExcelApp.Cells[14, 4] = dataGridView.SelectedRows[0].Cells[7].Value.ToString();
                    ExcelApp.Cells[10, 7] = (Nachisleno * 1) / 100;
                    ExcelApp.Cells[11,7] = (Nachisleno * 1) / 100;
                    ExcelApp.Cells[12,7] = (Nachisleno * 13) / 100;
                    workbook.SaveAs(fileName);
                    ExcelApp.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Marshal.ReleaseComObject(workbook);
                    Marshal.ReleaseComObject(workbooks);
                    Marshal.ReleaseComObject(ExcelApp);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует табель учета рабочего времени" + '\n' + "для данного сотрудника на выбранный вами месяц." + '\n' + "Пожалуйста заполните табель учета рабочего времени для данного сотрудника.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void Print_Vedomost(MySqlQueries mySqlQueries, DataGridView dataGridView,DateTimePicker dateTimePicker, SaveFileDialog saveFileDialog, string ID = null)
        {
            ExcelApplication ExcelApp = null;
            Workbooks workbooks = null;
            Workbook workbook = null;
            string output = null;
            string fileName = null;
            Select_Text(mySqlQueries.Select_Otdel_by_ID, ref output, ID);
            saveFileDialog.Title = "Сохранить расчетно-платежная ведомость как";
            saveFileDialog.FileName = "Расчетно-платежная ведомость за " + dateTimePicker.Text + " ("+output+")";
            saveFileDialog.InitialDirectory = Application.StartupPath + "\\Отчетность\\";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
                try
                {
                    ExcelApp = new ExcelApplication();
                    workbooks = ExcelApp.Workbooks;
                    workbook = workbooks.Open(Application.StartupPath + "\\Blanks\\Vedomost.xlsx");
                    ExcelApp.Cells[2, 2] = dateTimePicker.Text;
                    ExcelApp.Cells[4, 8] = output;
                    Select_Text(mySqlQueries.Itog_Vyplat_Po_Otdelu, ref output, ID);
                    ExcelApp.Cells[5, 8] = output;
                    string date1 = dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.Month.ToString() + "-1";
                    string date2 = dateTimePicker.Value.Year.ToString() + "-" + dateTimePicker.Value.AddMonths(1).Month.ToString() + "-0";
                    Select_DataGridView(mySqlQueries.Select_Vyplaty_Otdela, dataGridView, ID,date1,date2);
                    int ExRow = 4;
                    int ExCol = 1;
                    for (int i = 0; i < dataGridView.RowCount-1; i++)
                    {
                        for (int j = 0; j < dataGridView.ColumnCount; j++)
                        {
                            ExcelApp.Cells[ExRow, ExCol] = dataGridView.Rows[i].Cells[j].Value.ToString();
                            ExCol++;
                        }
                        ExRow++;
                        ExCol = 1;
                    }
                    workbook.SaveAs(fileName);
                    ExcelApp.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Marshal.ReleaseComObject(workbook);
                    Marshal.ReleaseComObject(workbooks);
                    Marshal.ReleaseComObject(ExcelApp);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует табель учета рабочего времени" + '\n' + "для данного сотрудника на выбранный вами месяц." + '\n' + "Пожалуйста заполните табель учета рабочего времени для данного сотрудника.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        public void Search(ToolStripTextBox textBox, DataGridView dataGridView)
        {
            if (textBox.Text != "")
            {
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    dataGridView.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView.ColumnCount; j++)
                        if (dataGridView.Rows[i].Cells[j].Value != null)
                            if (dataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox.Text))
                            {
                                dataGridView.Rows[i].Selected = true;
                                break;
                            }
                }
            }
            else dataGridView.ClearSelection();
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
            DateTime Begin = new DateTime(dateTimePicker1.Value.Year, 1, 1);
            DateTime End = new DateTime(dateTimePicker1.Value.Year, 12, 31);
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
            DateTime Begin = new DateTime(dateTimePicker1.Value.Year, 1, 1);
            DateTime End = new DateTime(dateTimePicker1.Value.Year, 12, 31);
            Select_DataGridView(mySqlQueries.Select_Grafik_For_Tabel, dataGridView, ID, Begin.Year.ToString());
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
