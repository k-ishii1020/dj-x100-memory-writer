using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DJ_X100_memory_writer.Service
{
    internal class WriteMemoryService
    {
        DataGridView dataGridView = new DataGridView();
        CreateCsvFileService createCsvFileService = new CreateCsvFileService();

        public void Write(DataGridView dataGridView, string selectedPort)
        {
            createCsvFileService.ExportDataGridViewToX100CmdCsv(dataGridView, ".\\x100cmd_temp.csv");
            X100cmdForm x100CmdForm = new X100cmdForm();
            x100CmdForm.Show();
            x100CmdForm.WriteX100(selectedPort);
        }
    }
}

