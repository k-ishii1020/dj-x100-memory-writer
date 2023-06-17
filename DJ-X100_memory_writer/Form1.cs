using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace DJ_X100_memory_writer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitComPort();
            var configurer = new MemoryChannnelSetup(memoryChDataGridView);
            configurer.SetupDataGridView();
        }

        private void InitComPort()
        {
            foreach (String portName in GetPortLists())
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(portName);
                menuItem.Click += (s, e) =>
                {
                    var clickedItem = s as ToolStripMenuItem;
                    string selectedPort = clickedItem.Text;

                    selectedComportLabel.Text = "選択中のCOMポート: " + selectedPort;
                };

                cOMポートCToolStripMenuItem.DropDownItems.Add(menuItem);
            }
        }
        private static String[] GetPortLists()
        {
            String[] portList = SerialPort.GetPortNames();
            Array.Sort(portList);
            return portList;
        }






        private void 新規作成NToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void 開くNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "CSVファイルを開く";
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "CSVファイル(*.csv)|*csv|すべてのファイル(*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.Multiselect = false;

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {

                ImportCsvToDataGridView(openFileDialog.FileName);
            }
        }

        private void 終了NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void 名前を付けて保存NToolStrpMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "名前をつけてCSVファイルを保存";
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.Filter = "CSVファイル(*.csv)|*csv|すべてのファイル(*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.AddExtension = true;
            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                ExportDataGridViewToCsv(saveFileDialog.FileName);


            }

        }
        private void ExportDataGridViewToCsv(string filename)
        {
            try
            {
                // 出力ストリームを開き、UTF-8エンコーディングで書き込む
                using (StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8))
                {
                    IEnumerable<string> headerValues = memoryChDataGridView.Columns
                        .OfType<DataGridViewColumn>()
                        .OrderBy(column => column.DisplayIndex)
                        .Select(column => column.HeaderText);

                    string headerLine = string.Join(",", headerValues);
                    writer.WriteLine(headerLine);

                    foreach (DataGridViewRow row in memoryChDataGridView.Rows)
                    {
                        IEnumerable<string> cellValues = row.Cells
                            .OfType<DataGridViewCell>()
                            .OrderBy(cell => cell.OwningColumn.DisplayIndex)
                            .Select(cell =>
                            {
                                if (cell.Value == null)
                                {
                                    return "";
                                }
                                else if (cell.Value is bool)
                                {
                                    return ((bool)cell.Value) ? "1" : "0";
                                }
                                else
                                {
                                    return cell.Value.ToString();
                                }
                            });

                        string line = string.Join(",", cellValues);
                        writer.WriteLine(line);
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("他のプロセスによってファイルが開かれています" + ex.Message);
            }
        }




        private void ImportCsvToDataGridView(string filename)
        {
            // Clear the DataGridView rows
            memoryChDataGridView.Rows.Clear();

            // Read the contents of the CSV file into a list of lines
            List<string> lines = File.ReadAllLines(filename).Skip(1).ToList(); // Skip the first line

            // Add the rest of the lines to the DataGridView rows
            for (int i = 0; i < lines.Count; i++)
            {
                string[] cells = lines[i].Split(',');

                // Create a new DataGridView row
                DataGridViewRow row = new DataGridViewRow();
                row.Height = memoryChDataGridView.RowTemplate.Height;
                row.CreateCells(memoryChDataGridView);

                for (int j = 0; j < cells.Length; j++)
                {
                    // Process the data for the checkbox columns
                    if (memoryChDataGridView.Columns[j] is DataGridViewCheckBoxColumn)
                    {
                        row.Cells[j].Value = cells[j] == "1" ? true : false;
                    }
                    else
                    {
                        row.Cells[j].Value = cells[j];
                    }
                }

                // Add the row to the DataGridView
                memoryChDataGridView.Rows.Add(row);
            }
        }




    }
}