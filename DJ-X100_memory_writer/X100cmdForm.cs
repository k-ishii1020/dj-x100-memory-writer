using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace DJ_X100_memory_writer
{
    public partial class X100cmdForm : Form
    {

        public X100cmdForm()
        {
            InitializeComponent();
        }







        public void ClearX100(string selectedPort)
        {
            string port;

            if (selectedPort == "自動選択")
            {
                port = "auto";
            }
            else
            {
                port = selectedPort;
            }

            // Create a batch file
            string batchFilePath = Path.Combine(".\\", Path.GetRandomFileName() + ".bat");
            using (StreamWriter sw = new StreamWriter(batchFilePath))

            {
                sw.WriteLine("timeout /T 4");
                for (int i = 1; i <= 9; i++)
                {
                    sw.WriteLine(".\\x100cmd.exe -p " + port + " clear -y " + i);
                }
            }

            // Run the batch file
            var clearProcess = new Process();
            var clearStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/k " + batchFilePath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.GetEncoding("Shift_JIS"),
                CreateNoWindow = true
            };

            clearProcess.OutputDataReceived += (sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    this.Invoke((Action)delegate
                    {
                        textBox1.AppendText(e.Data + Environment.NewLine);

                        // Indicate that process is running
                        progressBar1.Style = ProgressBarStyle.Marquee;
                    });
                }
            };

            clearProcess.StartInfo = clearStartInfo;
            clearProcess.Start();
            clearProcess.BeginOutputReadLine();

            // Indicate that process has ended
            this.Invoke((Action)delegate
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = progressBar1.Maximum;
            });

            // Delete the batch file
            // File.Delete(batchFilePath);
        }











        public void WriteX100(string selectedPort)
        {

            CheckX100cmdVersion();

            //ClearX100(selectedPort);

            string port;

            if (selectedPort == "自動選択")
            {
                port = "auto";
            }
            else
            {
                port = selectedPort;
            }

            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/k .\\x100cmd.exe -r -p" + port + " import x100cmd_temp.csv",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.UTF8,
                CreateNoWindow = true
            };

            process.OutputDataReceived += (sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    this.Invoke((Action)delegate
                    {
                        textBox1.AppendText(e.Data + Environment.NewLine);

                        // Indicate that process is running
                        progressBar1.Style = ProgressBarStyle.Marquee;

                        // Detecting EOF for process end
                        if (e.Data.Contains("EOF"))
                        {
                            textBox1.AppendText("書き込み処理が終了しました。実行結果を確認してください。" + Environment.NewLine);
                            MessageBox.Show("書き込み処理が終了しました。実行結果を確認してください。", "書き込み完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.okButton.Enabled = true;

                            // Indicate that process has ended
                            progressBar1.Style = ProgressBarStyle.Continuous;
                            progressBar1.Value = progressBar1.Maximum;
                        }
                        // Detecting error message
                        else if (!e.Data.Contains("OK"))
                        {
                            textBox1.AppendText("エラー: " + e.Data + Environment.NewLine);
                            MessageBox.Show("エラーが発生しました。詳細は下記の情報をご確認ください。\n\n" + e.Data, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.okButton.Enabled = true;

                            // Indicate that process has ended with error
                            progressBar1.Style = ProgressBarStyle.Continuous;
                            progressBar1.Value = progressBar1.Minimum;
                        }
                    });
                }
            };

            process.StartInfo = startInfo;
            process.Start();
            process.BeginOutputReadLine();
        }






        private void CheckX100cmdVersion()
        {
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c .\\x100cmd.exe -v",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            var versionMatch = Regex.Match(output, @"version (\d+\.\d+\.\d+)");
            if (versionMatch.Success)
            {
                var versionString = versionMatch.Groups[1].Value;
                var version = new Version(versionString);

                var requiredVersion = new Version("1.3.7");
                if (version < requiredVersion)
                {
                    throw new Exception($"x100cmdのバージョンが要求バージョン以下です。{requiredVersion}以上のバージョンを使用してください。");
                }
            }
            else
            {
                throw new Exception("x100cmdのバージョン情報を取得できませんでした。");
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
