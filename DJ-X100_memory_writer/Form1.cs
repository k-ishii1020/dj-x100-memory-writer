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

        private string selectedPort;
        private List<ToolStripMenuItem> portMenuItems;

        public Form1()
        {
            InitializeComponent();
            InitComPort();
            treeViewSetup();
            var configurer = new MemoryChannnelSetupService(memoryChDataGridView);
            configurer.SetupDataGridView();
        }

        private void InitComPort()
        {
            portMenuItems = new List<ToolStripMenuItem>();

            ToolStripMenuItem autoSelectItem = new ToolStripMenuItem("自動選択");
            autoSelectItem.Click += PortSelectClick;
            autoSelectItem.CheckOnClick = true;
            cOMポートCToolStripMenuItem.DropDownItems.Add(autoSelectItem);
            portMenuItems.Add(autoSelectItem);

            foreach (String portName in GetPortLists())
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(portName);
                menuItem.Click += PortSelectClick;
                menuItem.CheckOnClick = true;
                cOMポートCToolStripMenuItem.DropDownItems.Add(menuItem);
                portMenuItems.Add(menuItem);
            }

            autoSelectItem.PerformClick();
        }

        private void PortSelectClick(object sender, EventArgs e)
        {
            // 選択された項目を保存します
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            selectedPort = clickedItem.Text;

            selectedComportLabel.Text = "選択中のCOMポート: " + selectedPort;

            // 他のすべての項目のチェックを解除します
            foreach (var item in portMenuItems)
            {
                if (item != clickedItem)
                {
                    item.Checked = false;
                }
            }
        }

        private static String[] GetPortLists()
        {
            String[] portList = SerialPort.GetPortNames();
            Array.Sort(portList);
            return portList;
        }

        private void treeViewSetup()
        {
            treeView1.ExpandAll();
            string searchText = "メモリーチャンネル";
            SelectNodeByText(treeView1, searchText);
        }

        public void SelectNodeByText(TreeView treeView, string searchText)
        {
            // TreeView内のすべてのノードを検索
            foreach (TreeNode node in treeView.Nodes)
            {
                if (FindNode(node, searchText))
                {
                    break;
                }
            }
        }

        private bool FindNode(TreeNode treeNode, string searchText)
        {
            // 現在のノードのテキストを確認
            if (treeNode.Text == searchText)
            {
                // ノードを選択状態に設定
                treeNode.TreeView.SelectedNode = treeNode;
                // 選択したノードが見えるようにスクロール
                treeNode.EnsureVisible();
                return true;
            }
            // 子ノードが存在する場合は、それらのノードを走査
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (FindNode(node, searchText))
                {
                    return true;
                }
            }
            // マッチするノードが見つからない場合はfalseを返す
            return false;
        }





        private void 新規作成NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isEmpty = true;
            foreach (DataGridViewRow row in memoryChDataGridView.Rows)
            {
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Value != null && !string.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()))
                    {
                        isEmpty = false;
                        break;
                    }
                }

                if (!isEmpty)
                {
                    break;
                }
            }

            if (!isEmpty)
            {
                var confirmResult = MessageBox.Show("作成中のデータは破棄されます", "よろしいですか？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    memoryChDataGridView.Rows.Clear();
                    memoryChDataGridView.Columns.Clear();  // ここで既存の列を削除します。
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
            writeMemory.Write(memoryChDataGridView, selectedPort);
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