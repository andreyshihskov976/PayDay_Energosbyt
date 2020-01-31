using System;
using System.Windows.Forms;

namespace PayDay_Energosbyt
{
    public partial class Main : Form
    {
        public MySqlOperations MySqlOperations = null;
        public MySqlQueries MySqlQueries = null;
        public string identify = null;
        public Main()
        {
            InitializeComponent();
            MySqlOperations = new MySqlOperations(MySqlQueries);
            MySqlQueries = new MySqlQueries();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlOperations.OpenConnection();
        }
        private void Kostyl_Event(object sender, EventArgs e){}

        private void Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void DataGrid_Load(string query)
        {
            MySqlOperations.Select_DataGridView(query, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }
        private void отделыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            identify = "otdely";
            DataGrid_Load(MySqlQueries.Select_Otdely);
            this.Text = "Отделы";
        }

        private void окладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            identify = "oklad";
            DataGrid_Load(MySqlQueries.Select_Oklad);
            this.Text = "Оклад";
        }

        private void должностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            identify = "doljnosti";
            DataGrid_Load(MySqlQueries.Select_Doljnosti);
            this.Text = "Должности";
        }

        private void сотрудникиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            identify = "sotrudniki";
            DataGrid_Load(MySqlQueries.Select_Sotrudniki);
            this.Text = "Сотрудники";
        }

        private void расчетныеСчетаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            identify = "raschetniki";
            DataGrid_Load(MySqlQueries.Select_Rasch_Scheta);
            this.Text = "Расчетные счета";
        }
        private void выплатыЗарплатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            identify = "vyplaty";
            DataGrid_Load(MySqlQueries.Select_Vyplaty);
            this.Text = "Выплаты зарплаты";
        }

        private void вставкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Insert_String();
        }

        private void Insert_String()
        {
            if (identify == "raschetniki")
            {
                Raschetniki raschetniki = new Raschetniki(MySqlOperations,MySqlQueries);
                raschetniki.Raschetniki_Closed += расчетныеСчетаToolStripMenuItem_Click;
                raschetniki.Owner = this;
                raschetniki.button1.Visible = true;
                raschetniki.button3.Visible = false;
                raschetniki.Show();
            }
            else if (identify == "otdely")
            {
                Otdely otdely = new Otdely(MySqlOperations, MySqlQueries);
                otdely.Otdely_Closed += отделыToolStripMenuItem_Click;
                otdely.Owner = this;
                otdely.button1.Visible = true;
                otdely.button3.Visible = false;
                otdely.Show();
            }
            else if (identify == "sotrudniki")
            {
                Sotrudniki sotrudniki = new Sotrudniki(MySqlOperations,MySqlQueries);
                sotrudniki.Sotrudniki_Closed += сотрудникиToolStripMenuItem1_Click;
                sotrudniki.Owner = this;
                sotrudniki.button1.Visible = true;
                sotrudniki.button3.Visible = false;
                if (sotrudniki.comboBox1.Items.Count == 0)
                    Sotrudniki_After_Otdely(sotrudniki);
                if (sotrudniki.comboBox2.Items.Count == 0)
                    Sotrudniki_After_Doljnosti(sotrudniki);
                if (sotrudniki.comboBox1.Items.Count == 0)
                    Sotrudniki_After_Oklad(sotrudniki);
                if (sotrudniki.comboBox1.Items.Count == 0)
                    Sotrudniki_After_RS(sotrudniki);
                sotrudniki.Show();
            }
            else if (identify == "oklad")
            {
                Oklad oklad = new Oklad(MySqlOperations,MySqlQueries);
                oklad.Oklad_Closed += окладToolStripMenuItem_Click;
                oklad.Owner = this;
                oklad.button1.Visible = true;
                oklad.button3.Visible = false;
                oklad.Show();
            }
            else if (identify == "doljnosti")
            {
                Doljnosti doljnosti = new Doljnosti(MySqlOperations, MySqlQueries);
                doljnosti.Doljnosti_Closed += должностиToolStripMenuItem_Click;
                doljnosti.Owner = this;
                doljnosti.button1.Visible = true;
                doljnosti.button3.Visible = false;
                doljnosti.Show();
            }
            else if (identify == "vyplaty")
            {
                Vyplaty vyplaty = new Vyplaty(MySqlOperations, MySqlQueries);
                vyplaty.Vyplaty_Closed += выплатыЗарплатыToolStripMenuItem_Click;
                vyplaty.Owner = this;
                vyplaty.Show();
            }
        }

        private Sotrudniki Sotrudniki_After_RS(Sotrudniki sotrudniki)
        {
            if (MessageBox.Show("Отсутствуют свободные записи в таблице Расчетные счета." + '\n' + "Желаете добавить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Raschetniki raschetniki = new Raschetniki(MySqlOperations, MySqlQueries);
                raschetniki.Raschetniki_Closed += Kostyl_Event;
                raschetniki.Owner = this.Owner;
                raschetniki.button1.Visible = true;
                raschetniki.button3.Visible = false;
                raschetniki.ShowDialog();
                sotrudniki = new Sotrudniki(MySqlOperations, MySqlQueries);
                sotrudniki.Sotrudniki_Closed += сотрудникиToolStripMenuItem1_Click;
                sotrudniki.Owner = this;
                sotrudniki.button1.Visible = true;
                sotrudniki.button3.Visible = false;
            }
            return sotrudniki;
        }

        private Sotrudniki Sotrudniki_After_Otdely(Sotrudniki sotrudniki)
        {
            if (MessageBox.Show("Отсутствуют записи в таблице Отделы." + '\n' + "Желаете добавить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Otdely otdely = new Otdely(MySqlOperations, MySqlQueries);
                otdely.Otdely_Closed += Kostyl_Event;
                otdely.Owner = this.Owner;
                otdely.button1.Visible = true;
                otdely.button3.Visible = false;
                otdely.ShowDialog();
                sotrudniki = new Sotrudniki(MySqlOperations, MySqlQueries);
                sotrudniki.Sotrudniki_Closed += сотрудникиToolStripMenuItem1_Click;
                sotrudniki.Owner = this;
                sotrudniki.button1.Visible = true;
                sotrudniki.button3.Visible = false;
            }
            return sotrudniki;
        }

        private Sotrudniki Sotrudniki_After_Oklad(Sotrudniki sotrudniki)
        {
            if (MessageBox.Show("Отсутствуют записи в таблице Отделы." + '\n' + "Желаете добавить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Oklad oklad = new Oklad(MySqlOperations, MySqlQueries);
                oklad.Oklad_Closed += Kostyl_Event;
                oklad.Owner = this.Owner;
                oklad.button1.Visible = true;
                oklad.button3.Visible = false;
                oklad.ShowDialog();
                sotrudniki = new Sotrudniki(MySqlOperations, MySqlQueries);
                sotrudniki.Sotrudniki_Closed += сотрудникиToolStripMenuItem1_Click;
                sotrudniki.Owner = this;
                sotrudniki.button1.Visible = true;
                sotrudniki.button3.Visible = false;
            }
            return sotrudniki;
        }

        private Sotrudniki Sotrudniki_After_Doljnosti(Sotrudniki sotrudniki)
        {
            if (MessageBox.Show("Отсутствуют записи в таблице Отделы." + '\n' + "Желаете добавить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Doljnosti doljnosti = new Doljnosti(MySqlOperations, MySqlQueries);
                doljnosti.Doljnosti_Closed += Kostyl_Event;
                doljnosti.Owner = this.Owner;
                doljnosti.button1.Visible = true;
                doljnosti.button3.Visible = false;
                doljnosti.ShowDialog();
                sotrudniki = new Sotrudniki(MySqlOperations, MySqlQueries);
                sotrudniki.Sotrudniki_Closed += сотрудникиToolStripMenuItem1_Click;
                sotrudniki.Owner = this;
                sotrudniki.button1.Visible = true;
                sotrudniki.button3.Visible = false;
            }
            return sotrudniki;
        }

        private void выделитьвсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count == 1)
            //{
            //    удалитьToolStripMenuItem.Enabled = true;
            //    редактироватьToolStripMenuItem.Enabled = true;
            //    toolStripMenuItem2.Enabled = true;
            //    toolStripMenuItem7.Enabled = true;
            //}
            //else
            //{
            //    удалитьToolStripMenuItem.Enabled = false;
            //    редактироватьToolStripMenuItem.Enabled = false;
            //    toolStripMenuItem2.Enabled = false;
            //    toolStripMenuItem7.Enabled = false;
            //}
        }

        private void Edit_String()
        {
            if (identify == "raschetniki")
            {
                Raschetniki raschetniki = new Raschetniki(MySqlOperations, MySqlQueries);
                raschetniki.textBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                raschetniki.textBox2.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                raschetniki.textBox3.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                raschetniki.textBox4.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                raschetniki.textBox5.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                raschetniki.ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                raschetniki.Raschetniki_Closed += расчетныеСчетаToolStripMenuItem_Click;
                raschetniki.Owner = this;
                raschetniki.button3.Visible = true;
                raschetniki.button1.Visible = false;
                raschetniki.Show();
            }
            else if (identify == "otdely")
            {
                Otdely otdely = new Otdely(MySqlOperations, MySqlQueries);
                otdely.textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                otdely.ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                otdely.Otdely_Closed += отделыToolStripMenuItem_Click;
                otdely.Owner = this;
                otdely.button3.Visible = true;
                otdely.button1.Visible = false;
                otdely.Show();
            }
            else if (identify == "sotrudniki")
            {
                Sotrudniki sotrudniki = new Sotrudniki(MySqlOperations, MySqlQueries);
                if (sotrudniki.comboBox1.Items.Count == 0 || sotrudniki.comboBox2.Items.Count == 0|| sotrudniki.comboBox3.Items.Count == 0|| sotrudniki.comboBox4.Items.Count == 0)
                {
                    if (sotrudniki.comboBox1.Items.Count == 0)
                        Sotrudniki_After_Otdely(sotrudniki);
                    if (sotrudniki.comboBox2.Items.Count == 0)
                        Sotrudniki_After_Doljnosti(sotrudniki);
                    if (sotrudniki.comboBox3.Items.Count == 0)
                        Sotrudniki_After_Oklad(sotrudniki);
                    if (sotrudniki.comboBox4.Items.Count == 0)
                        Sotrudniki_After_RS(sotrudniki);
                    sotrudniki = new Sotrudniki(MySqlOperations, MySqlQueries);
                    sotrudniki.ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    sotrudniki.textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[0];
                    sotrudniki.textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[1];
                    sotrudniki.textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[2];
                    if (dataGridView1.SelectedRows[0].Cells[5].Value.ToString() != "")
                    {
                        sotrudniki.comboBox4.Items.Add(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
                    }
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), sotrudniki.comboBox1);
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), sotrudniki.comboBox2);
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[4].Value.ToString(), sotrudniki.comboBox3);
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[5].Value.ToString(), sotrudniki.comboBox4);
                    sotrudniki.Sotrudniki_Closed += сотрудникиToolStripMenuItem1_Click;
                    sotrudniki.Owner = this;
                    sotrudniki.button3.Visible = true;
                    sotrudniki.button1.Visible = false;
                    sotrudniki.Show();
                }
                else
                {
                    sotrudniki.ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    sotrudniki.textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[0];
                    sotrudniki.textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[1];
                    sotrudniki.textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[2];
                    if (dataGridView1.SelectedRows[0].Cells[5].Value.ToString() != "")
                    {
                        sotrudniki.comboBox4.Items.Add(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
                    }
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), sotrudniki.comboBox1);
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), sotrudniki.comboBox2);
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[4].Value.ToString(), sotrudniki.comboBox3);
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[5].Value.ToString(), sotrudniki.comboBox4);
                    sotrudniki.Sotrudniki_Closed += сотрудникиToolStripMenuItem1_Click;
                    sotrudniki.Owner = this;
                    sotrudniki.button3.Visible = true;
                    sotrudniki.button1.Visible = false;
                    sotrudniki.Show();
                }
            }
            else if (identify == "oklad")
            {
                Oklad oklad = new Oklad(MySqlOperations,MySqlQueries);
                oklad.dateTimePicker1.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                oklad.dateTimePicker2.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                oklad.textBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                oklad.ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                oklad.Oklad_Closed += окладToolStripMenuItem_Click;
                oklad.Owner = this;
                oklad.button3.Visible = true;
                oklad.button1.Visible = false;
                oklad.Show();
            }
            else if (identify == "doljnosti")
            {
                Doljnosti doljnosti = new Doljnosti(MySqlOperations, MySqlQueries);
                doljnosti.textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                doljnosti.ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                doljnosti.Doljnosti_Closed += должностиToolStripMenuItem_Click;
                doljnosti.Owner = this;
                doljnosti.button3.Visible = true;
                doljnosti.button1.Visible = false;
                doljnosti.Show();
            }
        }

        private void Delete_String()
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись(-и)?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (identify == "raschetniki")
                {
                    foreach (var i in dataGridView1.SelectedRows)
                    {
                        MySqlOperations.Delete(MySqlQueries.Delete_Rasch_Scheta, MySqlQueries.Select_Rasch_Scheta, dataGridView1, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    }
                }
                else if (identify == "otdely")
                {
                    foreach (var i in dataGridView1.SelectedRows)
                    {
                        MySqlOperations.Delete(MySqlQueries.Delete_Otdely, MySqlQueries.Select_Otdely, dataGridView1, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    }
                }
                else if (identify == "sotrudniki")
                {
                    foreach (var i in dataGridView1.SelectedRows)
                    {
                        MySqlOperations.Delete(MySqlQueries.Delete_Sotrudniki, MySqlQueries.Select_Sotrudniki, dataGridView1, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    }
                }
                else if (identify == "oklad")
                {
                    foreach (var i in dataGridView1.SelectedRows)
                    {
                        MySqlOperations.Delete(MySqlQueries.Delete_Oklad, MySqlQueries.Select_Oklad, dataGridView1, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    }
                }
                else if (identify == "doljnosti")
                {
                    foreach (var i in dataGridView1.SelectedRows)
                    {
                        MySqlOperations.Delete(MySqlQueries.Delete_Doljnosti, MySqlQueries.Select_Doljnosti, dataGridView1, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    }
                }
                else if (identify == "vyplaty")
                {
                    foreach (var i in dataGridView1.SelectedRows)
                    {
                        MySqlOperations.Delete(MySqlQueries.Delete_Vyplaty, MySqlQueries.Select_Vyplaty, dataGridView1, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    }
                }
            }
        }
        public void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit_String();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete_String();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Edit_String();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Delete_String();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Insert_String();
        }

        private void табелиУчетаРабВремениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show_Tabel();
        }

        private void Show_Tabel()
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (identify == "sotrudniki")
                    Open_Tabel(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), MySqlQueries.Select_Tabel, dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                if (identify == "vyplaty")
                    Open_Tabel(dataGridView1.SelectedRows[0].Cells[4].Value.ToString(), MySqlQueries.Select_Tabel_FIO, dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
            }
            else
            {
                MessageBox.Show("Пожалуйста выберите одну запись," + '\n' + " по которой хотите просмотреть табель учета рабочего времени", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Open_Tabel(string Sotrudnik, string query, string title)
        {
            Tabel tabel = new Tabel(Sotrudnik, MySqlOperations, MySqlQueries, query);
            tabel.Text = "Табель отработанного времения сотрудника " + title;
            tabel.Show();
        }

        private void графикиРаботыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show_Grafik();
        }

        private void Show_Grafik()
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (identify == "doljnosti")
                    Open_Grafik(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), MySqlQueries.Select_Grafik_Raboty, dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                if (identify == "sotrudniki")
                {
                    if (dataGridView1.SelectedRows[0].Cells[3].Value.ToString() != "")
                        Open_Grafik(dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), MySqlQueries.Select_Grafik_Raboty_FIO, dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                    else
                        MessageBox.Show("У выбранного вами сотрудника отсутствует должность." + '\n' + "Пожалуйста измените запись данного сотрудника", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (identify == "vyplaty")
                {
                    string output = null;
                    MySqlOperations.Select_Text(MySqlQueries.Select_ID_Doljnosti_Sotrudnika, ref output, null, dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                    if (output != null)
                        Open_Grafik(dataGridView1.SelectedRows[0].Cells[4].Value.ToString(), MySqlQueries.Select_Grafik_Raboty_FIO, dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                    else
                        MessageBox.Show("У выбранного вами сотрудника отсутствует должность." + '\n' + "Пожалуйста измените запись данного сотрудника", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("Пожалуйста выберите одну запись," + '\n' + " по которой хотите просмотреть график работы", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Open_Grafik(string Doljnost, string query, string title)
        {
            Grafik grafik = new Grafik(Doljnost, MySqlOperations, MySqlQueries, query);
            grafik.Text = "График работы для " + '"' + title + '"';
            grafik.Show();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (identify != "vyplaty")
            {
                var result = MessageBox.Show("Хотите отредактировать запись?", "Вопрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Edit_String();
                }
                else if (result == DialogResult.No && identify == "doljnosti")
                {
                    if (MessageBox.Show("Хотите просмотреть график работы для данной должности?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Show_Grafik();
                    }
                }
                else if (result == DialogResult.No && identify == "sotrudniki")
                {
                    if (MessageBox.Show("Хотите просмотреть табель отработанного времени для данного сотрудника?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Show_Tabel();
                    }
                    else if (MessageBox.Show("Хотите просмотреть график работы для данного сотрудника?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Show_Grafik();
                    }
                }
            }
            else if(identify == "vyplaty")
            {
                if (MessageBox.Show("Хотите просмотреть график работы для данного сотрудника?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Show_Grafik();
                }
                else if (MessageBox.Show("Хотите просмотреть табель отработанного времени для данного сотрудника?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Show_Tabel();
                }
            }
        }

        private void табельУчетаРабВремениСотрудникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string ID = string.Empty;
                if (identify == "sotrudniki")
                {
                    ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    Print_Tabel_Dialog print_Tabel_Dialog = new Print_Tabel_Dialog(ID, MySqlOperations, MySqlQueries);
                    print_Tabel_Dialog.ShowDialog();
                }
                else if (identify == "vyplaty")
                {
                    MySqlOperations.Select_Text(MySqlQueries.Select_ID_Sotrudnika, ref ID, null, dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                    Print_Tabel_Dialog print_Tabel_Dialog = new Print_Tabel_Dialog(ID, MySqlOperations, MySqlQueries);
                    print_Tabel_Dialog.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста выберите одну запись на которой будет основан табель.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void графикРаботыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string ID = string.Empty;
                if (identify == "doljnosti")
                {
                    ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    Print_Grafik_Dialog print_Grafik_Dialog = new Print_Grafik_Dialog(ID, MySqlOperations, MySqlQueries);
                    print_Grafik_Dialog.ShowDialog();
                }
                if (identify == "sotrudniki")
                {
                    MySqlOperations.Select_Text(MySqlQueries.Select_ID_Doljnosti_Sotrudnika, ref ID, null, dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                    Print_Grafik_Dialog print_Grafik_Dialog = new Print_Grafik_Dialog(ID, MySqlOperations, MySqlQueries);
                    print_Grafik_Dialog.ShowDialog();
                }
                if (identify == "vyplaty")
                {
                    MySqlOperations.Select_Text(MySqlQueries.Select_ID_Doljnosti_Sotrudnika, ref ID, null, dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                    Print_Grafik_Dialog print_Grafik_Dialog = new Print_Grafik_Dialog(ID, MySqlOperations, MySqlQueries);
                    print_Grafik_Dialog.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста выберите одну запись на которой будет основан график.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void расчетныйЛистокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (identify == "vyplaty")
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    MySqlOperations.Print_Listok(MySqlQueries, dataGridView1, saveFileDialog1);
                }
                else
                {
                    MessageBox.Show("Невозможно распечатать расчетный листок с выбранными вами записями." + '\n' + "Пожалуйста выберите одну запись для печати расчетного листка.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Из данной таблицы невозможно распечатать расчетный листок." + '\n' + "Пожалуйста перейдите на таблицу Выплаты зарплат.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void расчетноплатежнаяВедомостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (identify == "vyplaty")
            {
                Print_Vedomost_Dialog print_Vedomost_Dialog = new Print_Vedomost_Dialog(MySqlOperations, MySqlQueries);
                print_Vedomost_Dialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Из данной таблицы невозможно распечатать расчетно-платежную ведомость." + '\n' + "Пожалуйста перейдите на таблицу Выплаты зарплат.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
