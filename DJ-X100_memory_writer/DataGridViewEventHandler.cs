using System.Globalization;
using System.Text.RegularExpressions;
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
            var dataGridView = (DataGridView)sender;

            if (dataGridView.Columns[e.ColumnIndex].Name == Columns.MODE.Id)
            {
                var cellValue = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (cellValue != null)
                {
                    var mode = cellValue.ToString();
                    UpdateCellStatusBasedOnMode(dataGridView, e.RowIndex, mode);
                }
            }

        }

        private void UpdateCellStatusBasedOnMode(DataGridView dataGridView, int rowIndex, string mode)
        {
            ResetCells(dataGridView, rowIndex);
            string[] disableCells = new string[0];

            switch (mode)
            {
                case "FM":
                case "NFM":
                    disableCells = new string[]
                    {
                        Columns.UC.Id,
                        Columns.GC.Id,
                        Columns.EC.Id,
                        Columns.WC.Id,
                        Columns.T61_LON.Id,
                        Columns.T61_LAT.Id,
                        Columns.DMR_SLOT.Id,
                        Columns.DMR_CC.Id,
                        Columns.DMR_GC.Id
                    };
                    break;
                case "AM":
                case "NAM":
                    disableCells = new string[]
                    {
                        Columns.SQL_MODE.Id,
                        Columns.CTCSS.Id,
                        Columns.DCS.Id,
                        Columns.UC.Id,
                        Columns.GC.Id,
                        Columns.EC.Id,
                        Columns.WC.Id,
                        Columns.T61_LON.Id,
                        Columns.T61_LAT.Id,
                        Columns.DMR_SLOT.Id,
                        Columns.DMR_CC.Id,
                        Columns.DMR_GC.Id
                    };
                    break;
                case "T98":
                case "T102_B54":
                    disableCells = new string[]
                    {
                        Columns.SQL_MODE.Id,
                        Columns.CTCSS.Id,
                        Columns.DCS.Id,
                        Columns.REV_EC.Id,
                        Columns.REV_EC_FREQ.Id,
                        Columns.T61_LON.Id,
                        Columns.T61_LAT.Id,
                        Columns.DMR_SLOT.Id,
                        Columns.DMR_CC.Id,
                        Columns.DMR_GC.Id
                    };
                    break;
                case "DMR":
                    disableCells = new string[]
                    {
                        Columns.SQL_MODE.Id,
                        Columns.CTCSS.Id,
                        Columns.DCS.Id,
                        Columns.REV_EC.Id,
                        Columns.REV_EC_FREQ.Id,
                        Columns.UC.Id,
                        Columns.GC.Id,
                        Columns.EC.Id,
                        Columns.WC.Id,
                        Columns.T61_LON.Id,
                        Columns.T61_LAT.Id,
                    };
                    break;
                case "T61_typ1":
                case "T61_typ2":
                    disableCells = new string[]
                    {
                        Columns.SQL_MODE.Id,
                        Columns.CTCSS.Id,
                        Columns.DCS.Id,
                        Columns.REV_EC.Id,
                        Columns.REV_EC_FREQ.Id,
                        Columns.UC.Id,
                        Columns.GC.Id,
                        Columns.EC.Id,
                        Columns.DMR_SLOT.Id,
                        Columns.DMR_CC.Id,
                        Columns.DMR_GC.Id
                    };
                    break;
                case "T61_typ3":
                case "T61_typ4":
                case "T61_typx":
                case "ICDU":
                    disableCells = new string[]
                    {
                        Columns.SQL_MODE.Id,
                        Columns.CTCSS.Id,
                        Columns.DCS.Id,
                        Columns.REV_EC.Id,
                        Columns.REV_EC_FREQ.Id,
                        Columns.UC.Id,
                        Columns.GC.Id,
                        Columns.EC.Id,
                        Columns.T61_LON.Id,
                        Columns.T61_LAT.Id,
                        Columns.DMR_SLOT.Id,
                        Columns.DMR_CC.Id,
                        Columns.DMR_GC.Id
                    };
                    break;
                case "dPMR":
                    disableCells = new string[]
                    {
                        Columns.SQL_MODE.Id,
                        Columns.CTCSS.Id,
                        Columns.DCS.Id,
                        Columns.REV_EC.Id,
                        Columns.REV_EC_FREQ.Id,
                        Columns.UC.Id,
                        Columns.GC.Id,
                        Columns.EC.Id,
                        Columns.T61_LON.Id,
                        Columns.T61_LAT.Id,
                        Columns.DMR_SLOT.Id,
                        Columns.DMR_CC.Id,
                        Columns.DMR_GC.Id
                    };
                    break;
                case "DSTAR":
                    disableCells = new string[]
                    {
                        Columns.SQL_MODE.Id,
                        Columns.CTCSS.Id,
                        Columns.DCS.Id,
                        Columns.REV_EC.Id,
                        Columns.REV_EC_FREQ.Id,
                        Columns.UC.Id,
                        Columns.GC.Id,
                        Columns.EC.Id,
                        Columns.WC.Id,
                        Columns.T61_LON.Id,
                        Columns.T61_LAT.Id,
                        Columns.DMR_SLOT.Id,
                        Columns.DMR_CC.Id,
                        Columns.DMR_GC.Id
                    };
                    break;
                case "C4FM":
                    disableCells = new string[]
                    {
                        Columns.SQL_MODE.Id,
                        Columns.CTCSS.Id,
                        Columns.DCS.Id,
                        Columns.REV_EC.Id,
                        Columns.REV_EC_FREQ.Id,
                        Columns.UC.Id,
                        Columns.GC.Id,
                        Columns.EC.Id,
                        Columns.WC.Id,
                        Columns.T61_LON.Id,
                        Columns.T61_LAT.Id,
                        Columns.DMR_SLOT.Id,
                        Columns.DMR_CC.Id,
                        Columns.DMR_GC.Id
                    };
                    break;
                case "AIS":
                case "ACARS":
                case "POCSAG":
                case "12KIF_W":
                case "12KIF_N":
                    disableCells = new string[]
                    {
                        Columns.SQL_MODE.Id,
                        Columns.CTCSS.Id,
                        Columns.DCS.Id,
                        Columns.REV_EC.Id,
                        Columns.REV_EC_FREQ.Id,
                        Columns.UC.Id,
                        Columns.GC.Id,
                        Columns.EC.Id,
                        Columns.WC.Id,
                        Columns.T61_LON.Id,
                        Columns.T61_LAT.Id,
                        Columns.DMR_SLOT.Id,
                        Columns.DMR_CC.Id,
                        Columns.DMR_GC.Id
                    };
                    break;
            }
            if (disableCells != null)
            {
                UpdateDisableCells(dataGridView, rowIndex, disableCells, true, Color.LightGray);
            }
        }

        private void UpdateDisableCells(DataGridView dataGridView, int rowIndex, string[] cellNames, bool isReadOnly, Color cellColor)
        {

            foreach (var cellName in cellNames)
            {
                if (dataGridView.Rows[rowIndex].Cells[cellName] != null)
                {
                    dataGridView.Rows[rowIndex].Cells[cellName].ReadOnly = isReadOnly;

                    // セルの色を設定
                    dataGridView.Rows[rowIndex].Cells[cellName].Style.BackColor = cellColor;
                }
            }
        }
        private void ResetCells(DataGridView dataGridView, int rowIndex)
        {
            foreach (DataGridViewCell cell in dataGridView.Rows[rowIndex].Cells)
            {
                if (cell.OwningColumn.Name != Columns.MEMORY_NO.Id)
                {
                    cell.ReadOnly = false;
                    if (rowIndex % 2 == 0)
                    {
                        cell.Style.BackColor = dataGridView.DefaultCellStyle.BackColor;
                    }
                    else
                    {
                        cell.Style.BackColor = dataGridView.AlternatingRowsDefaultCellStyle.BackColor;
                    }
                }
            }
        }
    }
}
