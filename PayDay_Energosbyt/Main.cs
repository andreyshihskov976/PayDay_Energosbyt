using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace PayDay_Energosbyt
{
    public partial class Main : Form
    {
        public MySqlOperations MySqlOperations = null;
        public MySqlQueries MySqlQueries = null;
        public string identify = null;
        public string SelectedRowIndex = null;
        //DataGridViewColumn OrderByColumn = null;
        //public string SortOrder = null;
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
            DataGrid_Load(MySqlQueries.Select_Otdely);
            identify = "otdely";
        }

        private void окладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGrid_Load(MySqlQueries.Select_Oklad);
            identify = "oklad";
        }

        private void должностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGrid_Load(MySqlQueries.Select_Doljnosti);
            identify = "doljnosti";
        }

        private void сотрудникиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataGrid_Load(MySqlQueries.Select_Sotrudniki);
            identify = "sotrudniki";
        }

        private void расчетныеСчетаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGrid_Load(MySqlQueries.Select_Rasch_Scheta);
            identify = "raschetniki";
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
                if (sotrudniki.comboBox4.Items.Count == 0)
                {
                    sotrudniki = Kostyl(sotrudniki);
                }
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
        }

        private Sotrudniki Kostyl(Sotrudniki sotrudniki)
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

        private void выделитьвсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                удалитьToolStripMenuItem.Enabled = true;
                редактироватьToolStripMenuItem.Enabled = true;
                contextMenuStrip1.Enabled = true;
                if (identify == "doljnosti")
                {
                    просмотретьГрафикРаботыToolStripMenuItem.Enabled = true;
                    графикиРаботыToolStripMenuItem.Enabled = true;
                    просмотретьТабельОтрВремениToolStripMenuItem.Enabled = false;
                    табелиУчетаРабВремениToolStripMenuItem.Enabled = false;
                }
                else if (identify == "sotrudniki")
                {
                    просмотретьГрафикРаботыToolStripMenuItem.Enabled = false;
                    графикиРаботыToolStripMenuItem.Enabled = false;
                    просмотретьТабельОтрВремениToolStripMenuItem.Enabled = true;
                    табелиУчетаРабВремениToolStripMenuItem.Enabled = true;
                }
                else
                {
                    просмотретьГрафикРаботыToolStripMenuItem.Enabled = false;
                    графикиРаботыToolStripMenuItem.Enabled = false;
                    просмотретьТабельОтрВремениToolStripMenuItem.Enabled = false;
                    табелиУчетаРабВремениToolStripMenuItem.Enabled = false;
                }
                    
            }
            else
            {
                удалитьToolStripMenuItem.Enabled = false;
                редактироватьToolStripMenuItem.Enabled = false;
                contextMenuStrip1.Enabled = false;
                просмотретьГрафикРаботыToolStripMenuItem.Enabled = false;
                табелиУчетаРабВремениToolStripMenuItem.Enabled = false;
                графикиРаботыToolStripMenuItem.Enabled = false;
                просмотретьТабельОтрВремениToolStripMenuItem.Enabled = false;
            }
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
                SelectedRowIndex = dataGridView1.SelectedRows[0].Index.ToString();
                //OrderByColumn = dataGridView1.SortedColumn;
                //SortOrder = dataGridView1.SortOrder.ToString();
                Sotrudniki sotrudniki = new Sotrudniki(MySqlOperations, MySqlQueries);
                if (sotrudniki.comboBox4.Items.Count == 0)
                {
                    Kostyl(sotrudniki);
                    sotrudniki = new Sotrudniki(MySqlOperations, MySqlQueries);
                    dataGridView1.Rows[int.Parse(SelectedRowIndex)].Selected = true;
                    sotrudniki.ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    sotrudniki.textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[0];
                    sotrudniki.textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[1];
                    sotrudniki.textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[2];
                    if (dataGridView1.SelectedRows[0].Cells[6].Value.ToString() != "")
                    {
                        sotrudniki.comboBox4.Items.Add(dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
                    }
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), sotrudniki.comboBox1);
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), sotrudniki.comboBox2);
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[4].Value.ToString(), sotrudniki.comboBox3);
                    sotrudniki.numericUpDown1.Value = int.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[6].Value.ToString(), sotrudniki.comboBox4);
                    sotrudniki.Sotrudniki_Closed += сотрудникиToolStripMenuItem1_Click;
                    sotrudniki.Owner = this;
                    sotrudniki.button3.Visible = true;
                    sotrudniki.button1.Visible = false;
                    sotrudniki.Show();
                }
                else
                {
                    dataGridView1.Rows[int.Parse(SelectedRowIndex)].Selected = true;
                    sotrudniki.ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    sotrudniki.textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[0];
                    sotrudniki.textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[1];
                    sotrudniki.textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[2];
                    if (dataGridView1.SelectedRows[0].Cells[6].Value.ToString() != "")
                    {
                        sotrudniki.comboBox4.Items.Add(dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
                    }
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), sotrudniki.comboBox1);
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), sotrudniki.comboBox2);
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[4].Value.ToString(), sotrudniki.comboBox3);
                    sotrudniki.numericUpDown1.Value = int.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
                    MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[6].Value.ToString(), sotrudniki.comboBox4);
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
            if (identify == "raschetniki")
            {
                MySqlOperations.Delete(MySqlQueries.Delete_Rasch_Scheta, MySqlQueries.Select_Rasch_Scheta, dataGridView1, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
            else if (identify == "otdely")
            {
                MySqlOperations.Delete(MySqlQueries.Delete_Otdely, MySqlQueries.Select_Otdely, dataGridView1, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
            else if (identify == "sotrudniki")
            {
                MySqlOperations.Delete(MySqlQueries.Delete_Sotrudniki, MySqlQueries.Select_Sotrudniki, dataGridView1, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
            else if (identify == "oklad")
            {
                MySqlOperations.Delete(MySqlQueries.Delete_Oklad, MySqlQueries.Select_Oklad, dataGridView1, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
            else if (identify == "doljnosti")
            {
                MySqlOperations.Delete(MySqlQueries.Delete_Doljnosti, MySqlQueries.Select_Doljnosti, dataGridView1, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            Open_Tabel();
        }

        private void Open_Tabel()
        {
            Tabel tabel = new Tabel(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), MySqlOperations, MySqlQueries);
            tabel.Text = "Табель отработанного времения сотрудника " + dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            tabel.Show();
        }

        private void графикиРаботыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open_Grafik();
        }

        private void Open_Grafik()
        {
            Grafik grafik = new Grafik(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), MySqlOperations, MySqlQueries);
            grafik.Text = "График работы для должности " + '"' + dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + '"';
            grafik.Show();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //var result = MessageBox.Show("Хотите отредактировать запись?", "Вопрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    Edit_String();
            //}
            //else if (result == DialogResult.No && identify== "doljnosti")
            //{
            //    if (MessageBox.Show("Хотите просмотреть график работы для данной должности?","Вопрос",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        Open_Grafik();
            //    }
            //}
            //else if (result == DialogResult.No && identify == "sotrudniki")
            //{
            //    if (MessageBox.Show("Хотите просмотреть табель отработанного времени для данного сотрудника?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        Open_Tabel();
            //    }
            //}
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                    Open_Grafik();
                }
            }
            else if (result == DialogResult.No && identify == "sotrudniki")
            {
                if (MessageBox.Show("Хотите просмотреть табель отработанного времени для данного сотрудника?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Open_Tabel();
                }
            }
        }
    }
}
