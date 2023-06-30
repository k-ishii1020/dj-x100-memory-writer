using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DJ_X100_memory_writer.Util;
using static DJ_X100_memory_writer.domain.MemoryChannnelConfig;

namespace DJ_X100_memory_writer
{
    internal class DataGridViewEventHandler
    {
        private DataGridView memoryChDataGridView;
        DataGridViewUtils dataGridViewUtils = new DataGridViewUtils();

        public DataGridViewEventHandler(DataGridView _memoryChDataGridView)
        {
            memoryChDataGridView = _memoryChDataGridView;
        }

        public void MemoryChDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            var comboBox = e.Control as ComboBox;

            // フィールドまたはプロパティとしてComboBoxを保持します
            ComboBox currentComboBox = null;

            if (comboBox != null)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                currentComboBox = comboBox;  // 現在のComboBoxを保持します

                // 現在編集中のセルを取得
                var currentCell = memoryChDataGridView.CurrentCell;

                // セルに値が存在する場合、その値を選択項目として設定
                if (currentCell.Value != null)
                {
                    comboBox.Text = currentCell.Value.ToString();
                }
                else
                {
                    // セルの値が存在しない場合、選択を解除
                    comboBox.SelectedIndex = -1;
                }
            }
            else
            {
                currentComboBox = null;  // ComboBox以外のコントロールが表示されている場合、現在のComboBoxをnullにします
            }
        }

        // DeleteキーでNo列消させない
        public void MemoryChDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewCell cell in memoryChDataGridView.SelectedCells)
                {
                    if (cell.OwningColumn.Name != Columns.MEMORY_NO.Id)
                    {
                        cell.Value = null;
                    }
                }
                e.Handled = true;
            }
            else if ((Control.ModifierKeys & Keys.Control) == Keys.Control && e.KeyCode == Keys.V)
            {
                PasteClipboardData();
            }
            else if (e.KeyData == Keys.Return && memoryChDataGridView.IsCurrentCellInEditMode)
            {
                memoryChDataGridView.EndEdit();
                e.Handled = true;
            }





            // if (e.Control)
            // {
            //     switch (e.KeyCode)
            //     {
            //         case Keys.Up:
            //             // If the Up key is pressed, move the selected rows up
            //             MoveRow(-1);
            //             e.Handled = true;
            //             break;
            // 
            //         case Keys.Down:
            //             // If the Down key is pressed, move the selected rows down
            //             MoveRow(1);
            //             e.Handled = true;
            //             break;
            //     }
            // }


        }

        private void PasteClipboardData()
        {
            DataGridViewCell startCell = memoryChDataGridView.SelectedCells[0];

            string clipboardText = Clipboard.GetText(TextDataFormat.Text);

            var lines = clipboardText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                var cells = lines[i].Split('\t');

                for (int j = 0; j < cells.Length; j++)
                {
                    int pasteCellRowIndex = startCell.RowIndex + i;
                    int pasteCellColumnIndex = startCell.ColumnIndex + j;

                    if (pasteCellRowIndex < memoryChDataGridView.RowCount && pasteCellColumnIndex < memoryChDataGridView.ColumnCount)
                    {
                        DataGridViewCell cell = memoryChDataGridView[pasteCellColumnIndex, pasteCellRowIndex];

                        cell.Value = cells[j];
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void MemoryChDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = memoryChDataGridView.Columns[e.ColumnIndex].Name;
            string input = memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

            if (input == null) return;

            switch (columnName)
            {
                // メモリネームの文字数チェック
                case var name when name == Columns.MEMORY_NAME.Id:

                    int inputLength = dataGridViewUtils.GetAdjustedLength(input);

                    if (inputLength > 28)
                    {
                        int convertedLength = 0;

                        string convertedInput = dataGridViewUtils.GetConvertedByteCountShiftJis(input, ref convertedLength);

                        if (convertedLength <= 28)
                        {
                            DialogResult result = MessageBox.Show($"行 {e.RowIndex + 1} の 'ネーム' 列は半角28文字、全角14文字以内にしてください。\n半角に変換すると規定文字数に収まります。\n変換しますか？",
                                                                  "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                            if (result == DialogResult.OK)
                            {
                                memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = convertedInput;
                                return;
                            }

                        }
                        MessageBox.Show($"エラー: 行 {e.RowIndex + 1} の 'ネーム' 列は半角28文字、全角14文字以内にしてください。\n超えた部分は自動的にカットされます。",
                                        "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        string cutInput = dataGridViewUtils.CutToLength(input, 28);
                        memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cutInput;
                    }
                    break;

                // 周波数のチェック
                case var freq when freq == Columns.FREQ.Id:
                    string validationMessage = ValidateAndFormatDecimalCell(input, e.RowIndex, Columns.FREQ.Name);

                    if (!string.IsNullOrWhiteSpace(validationMessage))
                    {
                        if (!decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal _)
                            || validationMessage.Contains("範囲外です")
                            || validationMessage.Contains("無効な値です"))
                        {
                            MessageBox.Show(validationMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                            return;
                        }

                        memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = validationMessage;
                    }
                    break;
                // BANKのチェック
                case var bank when bank == Columns.BANK.Id:
                    // 空欄、nullの場合は無視
                    if (string.IsNullOrWhiteSpace(input)) return;

                    // A-Zまでの大文字アルファベットであること
                    if (!Regex.IsMatch(input, @"^[A-Z]+$"))
                    {
                        MessageBox.Show($"エラー: 行 {e.RowIndex + 1} の 'BANK' 列はA～Zまでの大文字アルファベットでなければなりません。",
                                        "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                        return;
                    }

                    // 同じアルファベットが重複していないこと
                    if (input.Distinct().Count() != input.Length)
                    {
                        MessageBox.Show($"エラー: 行 {e.RowIndex + 1} の 'BANK' 列に同じアルファベットが重複しています。",
                                        "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                        return;
                    }
                    break;

                case var ec when ec == Columns.EC.Id:
                    // 空欄、nullの場合は無視
                    if (string.IsNullOrWhiteSpace(input)) return;

                    // inputが"OFF"かチェック
                    if (input.Equals("OFF", StringComparison.OrdinalIgnoreCase)) return;

                    // inputが1-32767の整数かチェック
                    if (int.TryParse(input, out int ecVvalue) && ecVvalue >= 1 && ecVvalue <= 32767) return;

                    MessageBox.Show($"エラー: 行 {e.RowIndex + 1} の 'EC' 列が不正な数値です(OFF,1～32767)",
                                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                    break;

                case var gc when gc == Columns.GC.Id:
                    // 空欄、nullの場合は無視
                    if (string.IsNullOrWhiteSpace(input)) return;

                    // inputが"ALL"かチェック
                    if (input.Equals("ALL", StringComparison.OrdinalIgnoreCase)) return;

                    // inputが1-65535の整数かチェック
                    if (int.TryParse(input, out int gcValue) && gcValue >= 1 && gcValue <= 65535) return;

                    MessageBox.Show($"エラー: 行 {e.RowIndex + 1} の 'GC' 列が不正な数値です(ALL,1～65535)",
                                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                    break;
            }
        }

         // エラー検知
        public void memoryChDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string columnName = memoryChDataGridView.Columns[e.ColumnIndex].HeaderText;

            MessageBox.Show($"エラー: 行 {e.RowIndex + 1} の '{columnName}' 列でエラーが発生しました。\n{e.Exception.Message}",
                            "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;

            e.ThrowException = false;
        }
        
        // 周波数チェック
        private string ValidateAndFormatDecimalCell(string cellValue, int rowNumber, string columnName)
        {
            if (string.IsNullOrWhiteSpace(cellValue))
            {
                return string.Empty;
            }

            if (decimal.TryParse(cellValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
            {
                string decimalPart = cellValue.Contains(".") ? cellValue.Split('.')[1] : "";

                if (value < 20m || value > 470m || decimalPart.Length > 6)
                {
                    return $"行{rowNumber + 1}, 列{columnName}: '{cellValue}' は範囲外です。(20MHz～470MHz)";
                }
                else
                {
                    return value.ToString("F6", CultureInfo.InvariantCulture); // 少数を第6位まで表示
                }
            }
            else
            {
                return $"行{rowNumber + 1}, 列{columnName}: '{cellValue}' は無効な値です。";
            }
        }

        public void MemoryChDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            return;
        }

        private void MoveRow(int direction)
        {
            if (memoryChDataGridView.SelectedRows.Count > 0)
            {
                memoryChDataGridView.SuspendLayout();

                List<DataGridViewRow> rowsToMove = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in memoryChDataGridView.SelectedRows)
                {
                    rowsToMove.Add(row);
                }

                // Reverse the order for correct insertion order based on direction
                if (direction > 0) // Reverse only for moving down
                {
                    rowsToMove.Reverse();
                }

                foreach (DataGridViewRow row in rowsToMove)
                {
                    int oldIndex = row.Index;
                    int newIndex = oldIndex + direction;
                    // Check if the new index is within the range
                    if (newIndex >= 0 && newIndex < memoryChDataGridView.Rows.Count)
                    {
                        memoryChDataGridView.Rows.RemoveAt(oldIndex);
                        memoryChDataGridView.Rows.Insert(newIndex, row);
                    }
                }

                memoryChDataGridView.ClearSelection();
                foreach (DataGridViewRow row in rowsToMove)
                {
                    memoryChDataGridView.Rows[row.Index].Selected = true;
                }

                memoryChDataGridView.ResumeLayout();
            }
        }



    }
}
