namespace DJ_X100_memory_writer
{
    partial class X100cmdForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            okButton = new Button();
            progressBar1 = new ProgressBar();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(776, 426);
            textBox1.TabIndex = 0;
            // 
            // okButton
            // 
            okButton.Enabled = false;
            okButton.Location = new Point(720, 444);
            okButton.Name = "okButton";
            okButton.Size = new Size(68, 23);
            okButton.TabIndex = 1;
            okButton.Text = "閉じる";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 444);
            progressBar1.MarqueeAnimationSpeed = 20;
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(198, 23);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 2;
            // 
            // X100cmdForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 476);
            ControlBox = false;
            Controls.Add(progressBar1);
            Controls.Add(okButton);
            Controls.Add(textBox1);
            Name = "X100cmdForm";
            Text = "書き込み中…";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button okButton;
        private ProgressBar progressBar1;
    }
}