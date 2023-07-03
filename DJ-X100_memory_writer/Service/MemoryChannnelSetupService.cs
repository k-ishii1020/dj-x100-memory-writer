using DJ_X100_memory_writer.domain;
using System.Windows.Forms;
using static DJ_X100_memory_writer.domain.MemoryChannnelConfig;

namespace DJ_X100_memory_writer.Service
{
    internal class MemoryChannnelSetupService
    {
        private DataGridView memoryChDataGridView;
        MemoryChannnelConfig config = new MemoryChannnelConfig();


        public MemoryChannnelSetupService(DataGridView _memoryChDataGridView)
        {
            memoryChDataGridView = _memoryChDataGridView;
        }

        public void SetupDataGridView()
        {
            var handler = new DataGridViewEventHandler(memoryChDataGridView);


            memoryChDataGridView.AllowUserToDeleteRows = false;
            memoryChDataGridView.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            memoryChDataGridView.KeyDown += handler.MemoryChDataGridView_KeyDown;
            memoryChDataGridView.EditingControlShowing += handler.MemoryChDataGridView_EditingControlShowing;
            memoryChDataGridView.CellEndEdit += handler.MemoryChDataGridView_CellEndEdit;
            memoryChDataGridView.DataError += handler.memoryChDataGridView_DataError;
            memoryChDataGridView.CellValueChanged += handler.MemoryChDataGridView_CellValueChanged;
            memoryChDataGridView.MouseClick += handler.memoryChDataGridView_MouseClick;
            memoryChDataGridView.CellValidating += handler.MemoryChDataGridView_CellValidating;

            // 表示部
            memoryChDataGridView.RowHeadersVisible = true;
            memoryChDataGridView.RowTemplate.Height = 20;
            memoryChDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

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

           for (int i = 0; i <= 999; i++)
            {
                int index = memoryChDataGridView.Rows.Add();
                memoryChDataGridView.Rows[index].Cells[Columns.MEMORY_NO.Id].Value = i.ToString("D3");

            }

            foreach (DataGridViewColumn column in memoryChDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}
