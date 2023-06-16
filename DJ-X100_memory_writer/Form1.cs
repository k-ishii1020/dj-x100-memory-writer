using System;

namespace DJ_X100_memory_writer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var configurer = new DataGridViewConfigurer(memoryChDataGridView);
            configurer.SetupDataGridView();
        }







        private void 新規作成NToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void 開くNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Title = "CSVファイルを開く";
            od.InitialDirectory = @"C:\";
            od.Filter = "CSVファイル(*.csv)|*csv|すべてのファイル(*.*)|*.*";
            od.FilterIndex = 0;
            od.Multiselect = false;

            DialogResult result = od.ShowDialog();

            if (result == DialogResult.OK)
            {

                string fileName = od.FileName;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }

        }

        private void 終了NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }







    }
}