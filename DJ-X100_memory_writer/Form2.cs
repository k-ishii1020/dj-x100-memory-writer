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

            // Create a dictionary for bank names
            var bankNames = new Dictionary<char, string>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                char bankChar = row.Cells[0].Value.ToString()[0];  // Assuming the bank character is in the first cell
                string bankName = row.Cells[1].Value?.ToString() ?? "";  // Assuming the bank name is in the second cell

                bankNames[bankChar] = bankName;
            }
            await x100cmdForm.WriteDataToCommandAsync(form1.selectedPort, bankNames);
        }
    }
}