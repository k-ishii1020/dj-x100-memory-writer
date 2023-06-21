using DJ_X100_memory_writer.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static DJ_X100_memory_writer.domain.MemoryChannnelConfig;

namespace DJ_X100_memory_writer
{
    public partial class Form2 : Form
    {
        private Form1 form1;
        private DataGridView dgv;  // dgvをクラスのフィールドとして定義
        DataGridViewUtils dataGridViewUtils = new DataGridViewUtils();

        public Form2(Form1 form1)
        {
            this.form1 = form1;

            InitializeComponent();

            Panel dgvPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 20, 0, 0),
            };

            this.Controls.Add(dgvPanel);

            this.dgv = new DataGridView
            {
                ColumnCount = 2,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.AliceBlue },
                Dock = DockStyle.Fill,
            };

            dgvPanel.Controls.Add(dgv);

            // 列ヘッダーの設定
            dgv.Columns[0].Name = "BANK";
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[0].Width = 60;
            dgv.Columns[1].Name = "BANK_NAME(全角14文字/半角7文字以内)";

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // データの追加
            for (char c = 'A'; c <= 'Z'; c++)
            {
                // Form1のTreeViewから対応するノードを取得
                TreeNode node = form1.GetBankNode(c.ToString());

                string bankName = "";

                if (node != null && node.Text.Length > 3)
                {
                    // ノードのテキストから接頭辞を取り除く
                    bankName = node.Text.Substring(3);
                }

                dgv.Rows.Add(c.ToString(), bankName);  // 2列目は編集可能
            }

            this.FormClosing += (sender, e) => UpdateData();
            dgv.KeyDown += DataGridView1_KeyDown;
            dgv.CellEndEdit += DataGridView1_CellEndEdit;
        }

        public void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = dgv.Columns[e.ColumnIndex].Name;
            string input = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

            if (input == null) return;

            switch (columnName)
            {
                // メモリネームの文字数チェック
                case var name when name == "BANK_NAME(全角14文字/半角7文字以内)":

                    int inputLength = dataGridViewUtils.GetAdjustedLength(input);

                    if (inputLength > 14)
                    {
                        int convertedLength = 0;

                        string convertedInput = dataGridViewUtils.GetConvertedByteCountShiftJis(input, ref convertedLength);

                        if (convertedLength <= 14)
                        {
                            DialogResult result = MessageBox.Show($"行 {e.RowIndex + 1} の 'ネーム' 列は半角14文字、全角7文字以内にしてください。\n半角に変換すると規定文字数に収まります。\n変換しますか？",
                                                                  "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                            if (result == DialogResult.OK)
                            {
                                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = convertedInput;
                                return;
                            }

                        }
                        MessageBox.Show($"エラー: 行 {e.RowIndex + 1} の 'ネーム' 列は半角14文字、全角7文字以内にしてください。\n超えた部分は自動的にカットされます。",
                                        "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        string cutInput = dataGridViewUtils.CutToLength(input, 14);
                        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cutInput;
                    }
                    break;
            }
        }


        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control && e.KeyCode == Keys.V)
            {
                PasteClipboardData();
            }
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewCell cell in dgv.SelectedCells)
                {
                    if (cell.OwningColumn.Name != "BANK")
                    {
                        cell.Value = null;
                    }
                }
                e.Handled = true;
            }
        }

        private void PasteClipboardData()
        {
            DataGridViewCell startCell = dgv.SelectedCells[0];

            string clipboardText = Clipboard.GetText(TextDataFormat.Text);

            var lines = clipboardText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                var cells = lines[i].Split('\t');

                for (int j = 0; j < cells.Length; j++)
                {
                    int pasteCellRowIndex = startCell.RowIndex + i;
                    int pasteCellColumnIndex = startCell.ColumnIndex + j;

                    if (pasteCellRowIndex < dgv.RowCount && pasteCellColumnIndex < dgv.ColumnCount)
                    {
                        DataGridViewCell cell = dgv[pasteCellColumnIndex, pasteCellRowIndex];

                        cell.Value = cells[j];
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void UpdateData()
        {
            var bankNames = new List<string>();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    bankNames.Add(row.Cells[1].Value.ToString());
                }
            }

            form1.UpdateTreeView(bankNames);  // Form1のTreeViewを更新
        }

        private async void バンク設定読込RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x100cmdForm = new X100cmdForm();
            x100cmdForm.Show();
            await x100cmdForm.UpdateDataFromCommandAsync(UpdateBankName);
        }

        private void UpdateBankName(char bankChar, string bankName)
        {
            // 適切な行を探し、そのバンク名を更新
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value.ToString() == bankChar.ToString())
                {
                    row.Cells[1].Value = bankName;
                    break;  // バンク名を更新したら、次の文字に進む
                }
            }
        }

        private async void バンク設定書込WToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x100cmdForm = new X100cmdForm();
            x100cmdForm.Show();

            var bankNames = new Dictionary<char, string>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                char bankChar = row.Cells[0].Value.ToString()[0];
                string bankName = row.Cells[1].Value?.ToString() ?? "";

                bankNames[bankChar] = bankName;
            }
            await x100cmdForm.WriteDataToCommandAsync(form1.selectedPort, bankNames);
        }

        private void メイン画面へ戻るToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
