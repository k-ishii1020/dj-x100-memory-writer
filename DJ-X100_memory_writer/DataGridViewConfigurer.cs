using System.Text;

namespace DJ_X100_memory_writer
{
    internal class DataGridViewConfigurer
    {
        private DataGridView memoryChDataGridView;

        public DataGridViewConfigurer(DataGridView _memoryChDataGridView)
        {
            memoryChDataGridView = _memoryChDataGridView;
        }

        public void SetupDataGridView()
        {
            memoryChDataGridView.AllowUserToDeleteRows = false;
            memoryChDataGridView.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            memoryChDataGridView.KeyDown += MemoryChDataGridView_KeyDown;
            memoryChDataGridView.CellValidating += MemoryChDataGridView_CellValidating;
            memoryChDataGridView.CellValidated += MemoryChDataGridView_CellValidated;

            memoryChDataGridView.RowTemplate.Height = 20;
            memoryChDataGridView.AllowUserToResizeRows = false;

            memoryChDataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;

            var columnsSetup = new List<ColumnSetup>
            {
                new ColumnSetup { Name = "memoryNo", HeaderText = "No.", ReadOnly = true, Width = 40 },
                new ColumnSetup { Name = "freq", HeaderText = "周波数" },
                new ColumnSetup { Name = "memoryName", HeaderText = "ネーム" },
                new ColumnSetup { Name = "mode", HeaderText = "モード" },
                new ColumnSetup { Name = "step", HeaderText = "ステップ" },
            };

            foreach (var columnSetup in columnsSetup)
            {
                var viewColumn = new DataGridViewTextBoxColumn
                {
                    Name = columnSetup.Name,
                    HeaderText = columnSetup.HeaderText,
                    ReadOnly = columnSetup.ReadOnly,
                    Width = columnSetup.Width
                };

                memoryChDataGridView.Columns.Add(viewColumn);
            }

            for (int i = 1; i <= 999; i++)
            {
                int index = memoryChDataGridView.Rows.Add();
                memoryChDataGridView.Rows[index].Cells["memoryNo"].Value = i.ToString("D3");
            }
        }

        private void MemoryChDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewCell cell in memoryChDataGridView.SelectedCells)
                {
                    cell.Value = null;
                }
                e.Handled = true;
            }
        }

        private void MemoryChDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (memoryChDataGridView.Columns[e.ColumnIndex].Name == "memoryName")
            {
                string input = e.FormattedValue.ToString();
                int inputLength = GetDisplayLength(input);

                if (inputLength > 28)
                {
                    string convertedInput = ConvertToHalfWidth(input);
                    int convertedInputLength = GetDisplayLength(convertedInput);

                    if (convertedInputLength <= 28)
                    {
                        DialogResult result = MessageBox.Show($"行 {e.RowIndex + 1} の 'ネーム' 列は半角28文字、全角14文字以内にしてください。\n半角に変換すると規定文字数に収まります。\n変換しますか？",
                                                              "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                        if (result == DialogResult.OK)
                        {
                            memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = convertedInput;
                            return;
                        }
                    }
                    // either we cannot fit the input by converting to half-width, or the user cancelled the operation
                    MessageBox.Show($"エラー: 行 {e.RowIndex + 1} の 'ネーム' 列は半角28文字、全角14文字以内にしてください。\n超えた部分は自動的にカットされます。",
                                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    string cutInput = CutToLength(input, 28);
                    memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = cutInput;
                }
            }
        }



        private string CutToLength(string input, int maxLength)
        {
            StringBuilder output = new StringBuilder();
            int currentLength = 0;

            foreach (char c in input)
            {
                int charDisplayLength = c > '\u007f' ? 2 : 1;

                if (currentLength + charDisplayLength > maxLength)
                {
                    break;
                }

                output.Append(c);
                currentLength += charDisplayLength;
            }

            return output.ToString();
        }



        private void MemoryChDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (memoryChDataGridView.Columns[e.ColumnIndex].Name == "memoryName" && memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag != null)
            {
                memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
                memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = null;
            }
        }


        private string ConvertToHalfWidth(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if ('\uFF01' <= c && c <= '\uFF5E') // 全角記号・英数字・かな・カナ
                {
                    sb.Append((char)(c - 0xFEE0));
                }
                else if (c == '\u3000') // 全角スペース
                {
                    sb.Append('\u0020');
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private void MemoryChDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // エラーが発生した場合にセルの値をクリアします。
            if (memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode)
            {
                memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
            }
        }






        private int GetDisplayLength(string str)
        {
            int len = 0;
            foreach (char c in str)
            {
                if (c > '\u007f')
                {
                    len += 2;
                }
                else
                {
                    len += 1;
                }
            }
            return len;
        }


        private class ColumnSetup
        {
            public string Name { get; set; } = string.Empty;
            public string HeaderText { get; set; } = string.Empty;
            public bool ReadOnly { get; set; } = false;
            public int Width { get; set; } = 100;
        }
    }
}
