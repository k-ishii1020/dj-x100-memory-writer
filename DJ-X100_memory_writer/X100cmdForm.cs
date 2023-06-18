using DJ_X100_memory_writer.domain;
using DJ_X100_memory_writer.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DJ_X100_memory_writer
{
    public partial class X100cmdForm : Form
    {

        public X100cmdForm()
        {
            InitializeComponent();
        }


        public void WriteX100(string arg)
        {
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/k .\\x100cmd.exe {arg} & pause",
                UseShellExecute = true, // ShellExecute を true にする
                CreateNoWindow = false  // 新しいウィンドウを作成する
            };

            process.StartInfo = startInfo;
            process.Start();
        }


    }
}
