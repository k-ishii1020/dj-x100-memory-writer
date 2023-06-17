using System.Text;
using static DJ_X100_memory_writer.MemoryChannnelConfig;

namespace DJ_X100_memory_writer
{
    internal class DataGridViewEventHandler
    {
        private DataGridView memoryChDataGridView;

        public DataGridViewEventHandler(DataGridView _memoryChDataGridView)
        {
            memoryChDataGridView = _memoryChDataGridView;
        }

        MemoryChannnelConfig config = new MemoryChannnelConfig();
        DataGridViewUtils dataGridViewUtils = new DataGridViewUtils();



        public void MemoryChDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var comboBox = e.Control as DataGridViewComboBoxEditingControl;
            var tb = e.Control as TextBox;

            if (comboBox != null)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                // 既存のイベントハンドラをクリアします（2回以上追加されないように）
                comboBox.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);

                // 新しいイベントハンドラを追加します。
                comboBox.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as DataGridViewComboBoxEditingControl;
            if (comboBox != null)
            {
                memoryChDataGridView.CurrentCell.Value = comboBox.Text;
            }
        }


        // セル入力開始時に全文選択
        public void MemoryChDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            var tb = memoryChDataGridView.EditingControl as TextBox;
            var columnName = memoryChDataGridView.Columns[e.ColumnIndex].Name;

            if (tb != null && columnName == "mode")
            {
                tb.SelectionStart = 0;
                tb.SelectionLength = tb.Text.Length;
            }
        }


        public void MemoryChDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewCell cell in memoryChDataGridView.SelectedCells)
                {
                    if (cell.OwningColumn.Name != "memoryNo") // If the cell is not in the "No." column
                    {
                        cell.Value = null;
                    }
                }
                e.Handled = true;
            }
        }


        public void MemoryChDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (memoryChDataGridView.Columns[e.ColumnIndex].Name == Columns.MEMORY_NAME.Id)
            {
                string input = e.FormattedValue.ToString();
                int inputLength = dataGridViewUtils.GetDisplayLength(input);

                if (inputLength > 28)
                {
                    string convertedInput = dataGridViewUtils.ConvertToHalfWidth(input);
                    int convertedInputLength = dataGridViewUtils.GetDisplayLength(convertedInput);

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

                    string cutInput = dataGridViewUtils.CutToLength(input, 28);
                    memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = cutInput;
                }
            }






        }







        public void MemoryChDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (memoryChDataGridView.Columns[e.ColumnIndex].Name == Columns.MEMORY_NAME.Id && memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag != null)
            {
                memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
                memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = null;
            }
        }


        public void MemoryChDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (!(this.memoryChDataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn))
                {
                    this.memoryChDataGridView.BeginEdit(true);
                }
            }
        }

        public void MemoryChDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // エラーが発生した場合にセルの値をクリアします。
            if (memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode)
            {
                memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
            }
        }



        public void MemoryChDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
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
                        Columns.DMR_DLOT.Id,
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
                        Columns.DMR_DLOT.Id,
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
                        Columns.DMR_DLOT.Id,
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
                        Columns.DMR_DLOT.Id,
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
                        Columns.DMR_DLOT.Id,
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
                        Columns.DMR_DLOT.Id,
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
                        Columns.DMR_DLOT.Id,
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
                        Columns.DMR_DLOT.Id,
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
                        Columns.DMR_DLOT.Id,
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
                // If the cell is not from the "memoryNo" column
                if (cell.OwningColumn.Name != "memoryNo")
                {
                    cell.ReadOnly = false;
                    cell.Style.BackColor = Color.White; // Or the appropriate color to reset to.
                }
            }
        }



    }
}
