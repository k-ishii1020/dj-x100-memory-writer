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

                string fileName = openFileDialog.FileName;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
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
            DialogResult result = saveFileDialog.ShowDialog();

            saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                ExportDataGridViewToCsv(saveFileDialog.FileName);


            }

        }

        private void ExportDataGridViewToCsv(string filename)
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
                        .Select(cell => cell.Value == null ? "" : cell.Value.ToString());

                    string line = string.Join(",", cellValues);
                    writer.WriteLine(line);
                }
            }
        }


        private string QuoteValue(string value)
        {
            return string.Concat("\"", value.Replace("\"", "\"\""), "\"");
        }

    }
}