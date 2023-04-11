using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;


namespace AutomaconLogs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnServers_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo("Notepad.Exe", @"databases.txt");
            p.StartInfo = psi;
            p.Start();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string[] lineOfContents = File.ReadAllLines("databases.txt");
            foreach (var line in lineOfContents)
            {
            
                comboBox1.Items.Add(line);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo("Notepad.Exe", @"procedures.txt");
            p.StartInfo = psi;
            p.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo("Notepad.Exe", @"parameters.txt");
            p.StartInfo = psi;
            p.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo("Notepad.Exe", @"values.txt");
            p.StartInfo = psi;
            p.Start();
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string[] lineOfContents = File.ReadAllLines("procedures.txt");
            foreach (var line in lineOfContents)
            {

                comboBox2.Items.Add(line);
            }
        }

        private void comboBox3_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            string[] lineOfContents = File.ReadAllLines("parameters.txt");
            foreach (var line in lineOfContents)
            {

                comboBox3.Items.Add(line);
            }
        }

        private void comboBox4_DropDown(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            string[] lineOfContents = File.ReadAllLines("values.txt");
            foreach (var line in lineOfContents)
            {

                comboBox4.Items.Add(line);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //dataGridLog
            // result.txt
            // Определяем путь к файлу CSV
            string filePath = @"result.txt";

            // Определяем таблицу, в которую будем загружать данные
            DataTable dataTable = new DataTable();

            // Открываем файл CSV
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                // Получаем заголовки таблицы и добавляем их к таблице
                string[] headers = streamReader.ReadLine().Split(';');
                foreach (string header in headers)
                {
                    dataTable.Columns.Add(header);
                }

                // Читаем строки из файла CSV и добавляем их в таблицу
                while (!streamReader.EndOfStream)
                {
                    string[] rows = streamReader.ReadLine().Split(';');
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dataRow[i] = rows[i];
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            // загружаем данные в 
            dataGridLog.DataSource = dataTable;


        }

        private void dataGridLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridLog.SelectedCells.Count > 0)
            {
                string value = dataGridLog.SelectedCells[0].Value.ToString();
                // обработка значения ячейки

                rtbJSON.Text = value;
                               
                value = value.Replace( "\"\"", "\"");
                
                
                if (value.EndsWith("\"")&&value.StartsWith("\""))
                {
                    value = value.Substring(1, value.Length - 1);
                    value = value.Substring(0, value.Length - 1);

                    value = value;
                    
                }

                JObject json;
                try
                {
                    json = JObject.Parse(value);
                    string formattedJsonString = JToken.Parse(JsonConvert.SerializeObject(json)).ToString();

                    rtbJSON.Text = formattedJsonString;

                }
                catch (Exception ex)
                {
                    rtbJSON.Text = value;

                }

            }

        }
    }
}
