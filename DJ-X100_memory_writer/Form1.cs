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

            this.Load += Form1_Load;

            InitComPort();
            treeViewSetup();
            var configurer = new MemoryChannnelSetupService(memoryChDataGridView);
            configurer.SetupDataGridView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "このソフトは公開前のベータ版です。バンクネーム登録は作成中です。\n" +
                "DJ-X100本体などの不具合発生時の責任について\n" +
                "作者は一切の責任を負いかねます。\n\n" +
                "アプリケーションを立ち上げると同意したものとします。\n" +
                "よろしいですか？",
                "DJ-X100 Memory Writer(非公式)",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (dialogResult != DialogResult.Yes)
            {
                Application.Exit();
            }
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
            if (!IsDataGridViewEmpty() &&
                MessageBox.Show("作成中のデータは破棄されます。よろしいですか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                memoryChDataGridView.Rows.Clear();
                memoryChDataGridView.Columns.Clear();
                var configurer = new MemoryChannnelSetupService(memoryChDataGridView);
                configurer.SetupDataGridView();
            }
        }

        private void 開くNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsDataGridViewEmpty() &&
                MessageBox.Show("作成中のデータは破棄されます。よろしいですか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "CSVファイルを開く",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Filter = "CSVファイル(*.csv)|*csv|すべてのファイル(*.*)|*.*",
                    FilterIndex = 0,
                    Multiselect = false
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    csvUtils.ImportCsvToDataGridView(memoryChDataGridView, openFileDialog.FileName);
                }
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
            DialogResult dialogResult = MessageBox.Show(
                "この表に登録していないメモリは全て消えます。\n" +
                "また、不具合によってメモリが消える、もしくは動作が不安定になる\n" +
                "恐れがありますので必ず事前にバックアップを取ってください。\n" +
                "x100cmd.exe export --ext backup.csv\n" +
                "本当に書き込みを続行してもよろしいですか？",
                "警告",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );

            if (dialogResult == DialogResult.OK)
            {
                writeMemory.Write(memoryChDataGridView, selectedPort);
            }
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

        private bool IsDataGridViewEmpty()
        {
            foreach (DataGridViewRow row in memoryChDataGridView.Rows)
            {
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Value != null && !string.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void バンク設定BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.Show();
        }

        public void UpdateTreeView(List<string> bankNames)
        {
            TreeNode parentNode = treeView1.Nodes
                .OfType<TreeNode>()
                .FirstOrDefault(n => n.Name == "djx100Node");

            if (parentNode != null)
            {
                TreeNode bankMemoryNode = parentNode.Nodes
                    .OfType<TreeNode>()
                    .FirstOrDefault(n => n.Name == "bankMemoryNode");

                if (bankMemoryNode != null)
                {
                    bankMemoryNode.Nodes.Clear();

                    // 新しい子ノードを追加
                    for (int i = 0; i < bankNames.Count; i++)
                    {
                        // アルファベットをAから順に取得し、それを名前の先頭に追加
                        string bankName = ((char)('A' + i)).ToString() + ": " + bankNames[i];

                        // imageKeyを指定してノードを作成
                        TreeNode node = new TreeNode(bankName)
                        {
                            ImageKey = "kkrn_icon_folder_1.png",
                            SelectedImageKey = "kkrn_icon_folder_1.png"
                        };
                        bankMemoryNode.Nodes.Add(node);
                    }
                }
            }
        }

        public TreeNode GetBankNode(string bankLabel)
        {
            TreeNode parentNode = treeView1.Nodes
                .OfType<TreeNode>()
                .FirstOrDefault(n => n.Name == "djx100Node");

            if (parentNode != null)
            {
                TreeNode bankMemoryNode = parentNode.Nodes
                    .OfType<TreeNode>()
                    .FirstOrDefault(n => n.Name == "bankMemoryNode");

                if (bankMemoryNode != null)
                {
                    TreeNode bankNode = bankMemoryNode.Nodes
                        .OfType<TreeNode>()
                        .FirstOrDefault(n => n.Text.StartsWith(bankLabel + ":"));

                    return bankNode;
                }
            }

            return null;
        }




    }
}