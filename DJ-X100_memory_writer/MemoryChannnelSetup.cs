namespace DJ_X100_memory_writer
{
    internal class MemoryChannnelSetup
    {
        private DataGridView memoryChDataGridView;
        MemoryChannnelConfig config = new MemoryChannnelConfig();


        public MemoryChannnelSetup(DataGridView _memoryChDataGridView)
        {
            memoryChDataGridView = _memoryChDataGridView;
        }

        public void SetupDataGridView()
        {
            var handler = new DataGridViewEventHandler(memoryChDataGridView);


            memoryChDataGridView.AllowUserToDeleteRows = false;
            memoryChDataGridView.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            memoryChDataGridView.KeyDown += handler.MemoryChDataGridView_KeyDown;
            memoryChDataGridView.CellValidating += handler.MemoryChDataGridView_CellValidating;
            memoryChDataGridView.CellValidated += handler.MemoryChDataGridView_CellValidated;
            memoryChDataGridView.EditingControlShowing += handler.MemoryChDataGridView_EditingControlShowing;
            memoryChDataGridView.CellEndEdit += handler.MemoryChDataGridView_CellEndEdit;
            memoryChDataGridView.CellClick += handler.MemoryChDataGridView_CellClick;

            memoryChDataGridView.CellValueChanged += handler.MemoryChDataGridView_CellValueChanged;




            // 表示部
            memoryChDataGridView.RowTemplate.Height = 20;
            memoryChDataGridView.AllowUserToResizeRows = false;
            memoryChDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            memoryChDataGridView.Dock = DockStyle.Fill;


            foreach (var columnSetup in config.GetColumnsAsset())
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
    }
}
