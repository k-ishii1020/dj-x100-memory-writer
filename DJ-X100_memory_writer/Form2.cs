using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DJ_X100_memory_writer
{
    public partial class Form2 : Form
    {
        private Form1 form1;
        private DataGridView dgv;  // dgvをクラスのフィールドとして定義

        public Form2(Form1 form1)
        {
            this.form1 = form1;

            InitializeComponent();

            // DataGridViewを作成
            this.dgv = new DataGridView
            {
                ColumnCount = 2,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.AliceBlue },
                EditMode = DataGridViewEditMode.EditOnEnter

            };

            // 列ヘッダーの設定
            dgv.Columns[0].Name = "BANK";
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[0].Width = 30;
            dgv.Columns[1].Name = "BANK_NAME";

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

            // FormにDataGridViewを追加
            this.Controls.Add(dgv);
            this.FormClosing += (sender, e) => UpdateData();
            // DataGridViewの大きさや位置を設定
            dgv.Dock = DockStyle.Fill;
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



        private string ExecuteCommand(string command)
        {
            string output;

            var processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c chcp 65001 && {command}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
            };

            using (var process = new Process { StartInfo = processStartInfo })
            {
                process.Start();

                output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();
            }

            return output;

        }

        // コマンドを実行してデータを取得し、DataGridViewを更新する
        public async Task UpdateDataFromCommandAsync()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                string command = $".\\x100cmd.exe bank read {c}";
                string output = ExecuteCommand(command);

                // コマンドの出力からバンク名を抽出する
                var match = Regex.Match(output, $"\"{c}\",\"(.+)\"");
                string bankName = match.Success ? match.Groups[1].Value.Trim() : "";

                // 適切な行を探し、そのバンク名を更新
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[0].Value.ToString() == c.ToString())
                    {
                        row.Cells[1].Value = bankName;
                        break;  // バンク名を更新したら、次の文字に進む
                    }
                }

                // 100ミリ秒の遅延
                await Task.Delay(100);
            }
        }





        private async void バンク設定読込RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await UpdateDataFromCommandAsync();
        }



    }
}