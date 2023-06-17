using static DJ_X100_memory_writer.MemoryChannnelConfig;

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
            memoryChDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;



            // 表示部
            memoryChDataGridView.DefaultCellStyle.BackColor = Color.White;
            memoryChDataGridView.RowTemplate.Height = 20;
            memoryChDataGridView.AllowUserToResizeRows = false;
            memoryChDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            memoryChDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            memoryChDataGridView.Dock = DockStyle.Fill;


            foreach (var columnSetup in config.GetColumnsAsset())
            {
                DataGridViewColumn viewColumn;

                switch (columnSetup.Type)
                {
                    case ColumnType.Dropdown:
                        viewColumn = new DataGridViewComboBoxColumn
                        {
                            DataSource = columnSetup.Options,
                            FlatStyle = FlatStyle.Flat,
                        };
                        break;

                    case ColumnType.Checkbox:
                        viewColumn = new DataGridViewCheckBoxColumn
                        {
                            ThreeState = false
                        };
                        break;

                    case ColumnType.Text:
                    default:
                        viewColumn = new DataGridViewTextBoxColumn();
                        break;
                }

                viewColumn.Name = columnSetup.Id;
                viewColumn.HeaderText = columnSetup.HeaderText;
                viewColumn.ReadOnly = columnSetup.ReadOnly;
                viewColumn.Width = columnSetup.Width;

                memoryChDataGridView.Columns.Add(viewColumn);
            }




            for (int i = 1; i <= 999; i++)
            {
                int index = memoryChDataGridView.Rows.Add();
                memoryChDataGridView.Rows[index].Cells["memoryNo"].Value = i.ToString("D3");

            }

            foreach (DataGridViewColumn column in memoryChDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}
