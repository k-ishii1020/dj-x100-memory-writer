using DJ_X100_memory_writer.Service;
using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace DJ_X100_memory_writer
{
    public partial class Form1 : Form
    {
        CreateCsvFileService csvUtils = new CreateCsvFileService();
        WriteMemoryService writeMemory = new WriteMemoryService();


        public Form1()
        {
            InitializeComponent();
            InitComPort();
            var configurer = new MemoryChannnelSetupService(memoryChDataGridView);
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
            bool isEmpty = true;

            // Check each row in the DataGridView
            foreach (DataGridViewRow row in memoryChDataGridView.Rows)
            {
                // Skip the first column (index 0)
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Value != null && !string.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()))
                    {
                        // There's data in this cell, so the DataGridView isn't empty
                        isEmpty = false;
                        break;
                    }
                }

                if (!isEmpty)
                {
                    break;
                }
            }

            // If the DataGridView isn't empty, ask the user to confirm they want to clear it
            if (!isEmpty)
            {
                var confirmResult = MessageBox.Show("Are you sure you want to clear all data?", "Confirm Clear Data", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // If the user clicked 'Yes', clear the DataGridView
                    memoryChDataGridView.Rows.Clear();
                    var configurer = new MemoryChannnelSetupService(memoryChDataGridView);
                    configurer.SetupDataGridView();
                }
            }
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

                csvUtils.ImportCsvToDataGridView(memoryChDataGridView, openFileDialog.FileName);
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
                csvUtils.ExportDataGridViewToCsv(memoryChDataGridView, saveFileDialog.FileName);
            }
        }

        private void 書き込みToolStripMenuItem_Click(object sender, EventArgs e)
        {
            writeMemory.Write(memoryChDataGridView);
        }

        private void x100cmdexe用CSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "名前をつけてx100cmd用CSVファイルを保存";
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.Filter = "CSVファイル(*.csv)|*csv|すべてのファイル(*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.AddExtension = true;
            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                csvUtils.ExportDataGridViewToX100CmdCsv(memoryChDataGridView, saveFileDialog.FileName);
            }
        }
    }
}