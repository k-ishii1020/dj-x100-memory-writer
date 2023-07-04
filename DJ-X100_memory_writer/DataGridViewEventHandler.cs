using System.Globalization;
using System.Text;
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

        public void memoryChDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // DataGridViewのClipboardCopyModeをEnableWithoutHeaderTextに設定します。
                this.memoryChDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;

                // コンテクストメニューの作成
                ContextMenuStrip menu = new ContextMenuStrip();

                // クリア
                ToolStripMenuItem itemClear = new ToolStripMenuItem("クリア  Del");
                itemClear.Click += ItemClear_Click;
                menu.Items.Add(itemClear);

                // 挿入
                ToolStripMenuItem itemInsert = new ToolStripMenuItem("挿入   Ctrl + +");
                itemInsert.Click += ItemInsert_Click;
                menu.Items.Add(itemInsert);

                // 削除
                ToolStripMenuItem itemDelete = new ToolStripMenuItem("削除   Ctrl + -");
                itemDelete.Click += ItemDelete_Click;
                menu.Items.Add(itemDelete);

                // コピー
                ToolStripMenuItem itemCopy = new ToolStripMenuItem("コピー  Ctrl + C");
                itemCopy.Click += ItemCopy_Click;
                menu.Items.Add(itemCopy);

                // 貼り付け
                ToolStripMenuItem itemPaste = new ToolStripMenuItem("貼り付け  Ctrl + V");
                itemPaste.Click += ItemPaste_Click;
                menu.Items.Add(itemPaste);

                // カーソルの現在の位置にメニューを表示します。
                menu.Show(Cursor.Position);

            }
        }



        private void ItemClear_Click(object sender, EventArgs e)
        {
            memoryChDataGridView.CurrentCell.OwningRow.Selected = true;
            CellDelete();
        }

        private void ItemInsert_Click(object sender, EventArgs e)
        {
            memoryChDataGridView.CurrentCell.OwningRow.Selected = true;
            AddRowAndRenumber();
        }

        private void ItemDelete_Click(object sender, EventArgs e)
        {
            memoryChDataGridView.CurrentCell.OwningRow.Selected = true;
            DeleteRowAndRenumber();
        }

        private List<DataGridViewRow> copiedRows = new List<DataGridViewRow>();

        private void ItemCopy_Click(object sender, EventArgs e)
        {
            memoryChDataGridView.CurrentCell.OwningRow.Selected = true;
            if (this.memoryChDataGridView.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    copiedRows.Clear();
                    foreach (DataGridViewRow row in memoryChDataGridView.SelectedRows)
                    {
                        DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            var cellValue = row.Cells[i].Value.ToString();
                            if (cellValue.StartsWith("\t"))
                            {
                                cellValue = cellValue.Substring(1);
                            }
                            clonedRow.Cells[i].Value = cellValue;
                        }
                        copiedRows.Add(clonedRow);
                    }

                    var clipboardContent = new StringBuilder();
                    foreach (DataGridViewRow row in copiedRows)
                    {
                        var cellValues = row.Cells.Cast<DataGridViewCell>()
                                                  .Select(cell => cell.Value.ToString())
                                                  .ToArray();
                        clipboardContent.AppendLine(string.Join("\t", cellValues));
                    }

                    // Set clipboard content
                    Clipboard.SetText(clipboardContent.ToString());
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    MessageBox.Show("コピーに失敗しました。");
                }
            }
        }

        private void ItemPaste_Click(object sender, EventArgs e)
        {
            memoryChDataGridView.CurrentCell.OwningRow.Selected = true;
            // 貼り付け操作
            if (copiedRows.Count > 0 && memoryChDataGridView.SelectedCells.Count > 0)
            {
                int startRowIndex = memoryChDataGridView.SelectedCells[0].RowIndex;

                for (int rowIndex = copiedRows.Count - 1; rowIndex >= 0; rowIndex--)
                {
                    DataGridViewRow row = copiedRows[rowIndex];
                    if (startRowIndex < memoryChDataGridView.Rows.Count)
                    {
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            memoryChDataGridView.Rows[startRowIndex].Cells[i].Value = row.Cells[i].Value;
                        }
                        startRowIndex++;
                    }
                    else
                    {
                        DataGridViewRow newRow = (DataGridViewRow)memoryChDataGridView.RowTemplate.Clone();
                        newRow.CreateCells(memoryChDataGridView);
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            newRow.Cells[i].Value = row.Cells[i].Value;
                        }
                        memoryChDataGridView.Rows.Add(newRow);
                        startRowIndex++;
                    }
                }

                // 左列の数値を採番し直す
                for (int i = 0; i < memoryChDataGridView.Rows.Count; i++)
                {
                    memoryChDataGridView.Rows[i].Cells[0].Value = i.ToString("D3");
                }
            }
        }



        public void MemoryChDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var dataGridView = (DataGridView)sender;

            if (dataGridView.CurrentCell is DataGridViewComboBoxCell comboBoxCell)
            {
                if (!comboBoxCell.Items.Contains(e.FormattedValue))
                {
                    return;
                }

                // 新しい値を適用
                comboBoxCell.Value = e.FormattedValue;
            }
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

        public void MemoryChDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // DeleteキーでNo列消させない
            if (e.KeyCode == Keys.Delete)
            {
                CellDelete();
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

            // Ctrl + ↑↓で行移動 
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        MoveRow(-1);
                        e.Handled = true;
                        break;

                    case Keys.Down:
                        MoveRow(1);
                        e.Handled = true;
                        break;
                }
            }

            if (e.Control && e.Shift && e.KeyCode == Keys.Oemplus)
            {
                AddRowAndRenumber();

                e.Handled = true;
            }

            if (e.Control && e.Shift && e.KeyCode == Keys.OemMinus)
            {
                DeleteRowAndRenumber();

                e.Handled = true;
            }
            // コピー
            if (e.Control && e.KeyCode == Keys.C)
            {
                ItemCopy_Click(sender, e);
            }

            // 貼り付け
            if (e.Control && e.KeyCode == Keys.V)
            {
                ItemPaste_Click(sender, e);
            }


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

                var rowsToMove = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in memoryChDataGridView.SelectedRows)
                {
                    rowsToMove.Add(row);
                }

                rowsToMove = rowsToMove.OrderBy(r => r.Index).ToList();

                if (direction > 0)
                {
                    rowsToMove.Reverse();
                }

                int? lastNewIndex = null;

                foreach (DataGridViewRow row in rowsToMove)
                {
                    int oldIndex = row.Index;
                    int newIndex = oldIndex + direction;

                    if (newIndex >= 0 && newIndex < memoryChDataGridView.Rows.Count && newIndex != lastNewIndex)
                    {
                        lastNewIndex = newIndex;

                        memoryChDataGridView.Rows.RemoveAt(oldIndex);
                        memoryChDataGridView.Rows.Insert(newIndex, row);
                    }
                }

                memoryChDataGridView.ClearSelection();

                foreach (DataGridViewRow row in rowsToMove)
                {
                    memoryChDataGridView.Rows[row.Index].Selected = true;
                }

                for (int i = 0; i < memoryChDataGridView.Rows.Count; i++)
                {
                    memoryChDataGridView.Rows[i].Cells[0].Value = i.ToString("D3");
                }

                memoryChDataGridView.ResumeLayout();
            }
        }

        public void AddRowAndRenumber()
        {
            // 左から2番目のセル（インデックス1）に値がある場合は行を追加しない
            if (memoryChDataGridView.Rows[999].Cells[1].Value != null)
            {
                MessageBox.Show("999行目にデータがあるため、これ以上行を追加することはできません。");
                return;
            }

            memoryChDataGridView.Rows.RemoveAt(999);

            if (memoryChDataGridView.CurrentRow != null) // 現在の行が存在するか確認
            {
                // 現在の行の次に新しい行を挿入
                int currentIndex = memoryChDataGridView.CurrentRow.Index;
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(memoryChDataGridView);
                newRow.Height = 20;
                memoryChDataGridView.Rows.Insert(currentIndex, newRow);
            }

            // 左列の数値を採番し直す
            for (int i = 0; i < memoryChDataGridView.Rows.Count; i++)
            {
                memoryChDataGridView.Rows[i].Cells[0].Value = i.ToString("D3");
            }
        }

        public void DeleteRowAndRenumber()
        {
            if (memoryChDataGridView.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in memoryChDataGridView.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        memoryChDataGridView.Rows.Remove(row);
                    }
                }

                // 削除後に最終行に行を追加
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(memoryChDataGridView);
                newRow.Height = 20;
                memoryChDataGridView.Rows.Add(newRow);

                // 左列の数値を採番し直す
                for (int i = 0; i < memoryChDataGridView.Rows.Count; i++)
                {
                    memoryChDataGridView.Rows[i].Cells[0].Value = i.ToString("D3");
                }
            }
        }

        public void CellDelete()
        {
            foreach (DataGridViewCell cell in memoryChDataGridView.SelectedCells)
            {
                if (cell.OwningColumn.Name != Columns.MEMORY_NO.Id)
                {
                    cell.Value = null;
                }
            }
        }
    }
}
