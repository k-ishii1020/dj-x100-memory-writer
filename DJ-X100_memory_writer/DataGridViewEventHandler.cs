using System.Text;

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
            var tb = e.Control as TextBox;
            if (tb == null)
            {
                return;
            }

            var columnName = memoryChDataGridView.Columns[memoryChDataGridView.CurrentCell.ColumnIndex].Name;

            switch (columnName)
            {
                case "mode":
                    tb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    tb.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                    tb.AutoCompleteCustomSource.AddRange(config.GetModeOptions());
                    break;

                default:
                    tb.AutoCompleteMode = AutoCompleteMode.None;
                    break;
            }
        }

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
                    cell.Value = null;
                }
                e.Handled = true;
            }
        }

        public void MemoryChDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (memoryChDataGridView.Columns[e.ColumnIndex].Name == "memoryName")
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
            if (memoryChDataGridView.Columns[e.ColumnIndex].Name == "memoryName" && memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag != null)
            {
                memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
                memoryChDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = null;
            }
        }


        public void MemoryChDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(this.memoryChDataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn))
            {
                this.memoryChDataGridView.BeginEdit(true);
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

            if (dataGridView.Columns[e.ColumnIndex].Name == "mode")
            {
                var mode = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                UpdateCellStatusBasedOnMode(dataGridView, e.RowIndex, mode);
            }
        }

        private void UpdateCellStatusBasedOnMode(DataGridView dataGridView, int rowIndex, string mode)
        {
            string[] cellNames = { "uc", "gc", "ec", "wc", "t61_lon", "t61_lat" };
            bool isReadOnly;
            string value;

            switch (mode)
            {
                case "FM":
                    isReadOnly = true;
                    value = "-";
                    break;

                default:
                    isReadOnly = false;
                    value = null;
                    break;
            }

            foreach (var cellName in cellNames)
            {
                dataGridView.Rows[rowIndex].Cells[cellName].ReadOnly = isReadOnly;

                if (value != null)
                {
                    dataGridView.Rows[rowIndex].Cells[cellName].Value = value;
                }
            }
        }



    }
}
