namespace DJ_X100_memory_writer
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            menuStrip1 = new MenuStrip();
            ファイルFToolStripMenuItem = new ToolStripMenuItem();
            メイン画面へ戻るToolStripMenuItem = new ToolStripMenuItem();
            無線機XToolStripMenuItem = new ToolStripMenuItem();
            バンク設定読込RToolStripMenuItem = new ToolStripMenuItem();
            バンク設定書込WToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { ファイルFToolStripMenuItem, 無線機XToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            ファイルFToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { メイン画面へ戻るToolStripMenuItem });
            ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            resources.ApplyResources(ファイルFToolStripMenuItem, "ファイルFToolStripMenuItem");
            // 
            // メイン画面へ戻るToolStripMenuItem
            // 
            メイン画面へ戻るToolStripMenuItem.Name = "メイン画面へ戻るToolStripMenuItem";
            resources.ApplyResources(メイン画面へ戻るToolStripMenuItem, "メイン画面へ戻るToolStripMenuItem");
            // 
            // 無線機XToolStripMenuItem
            // 
            無線機XToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { バンク設定読込RToolStripMenuItem, バンク設定書込WToolStripMenuItem });
            無線機XToolStripMenuItem.Name = "無線機XToolStripMenuItem";
            resources.ApplyResources(無線機XToolStripMenuItem, "無線機XToolStripMenuItem");
            // 
            // バンク設定読込RToolStripMenuItem
            // 
            バンク設定読込RToolStripMenuItem.Name = "バンク設定読込RToolStripMenuItem";
            resources.ApplyResources(バンク設定読込RToolStripMenuItem, "バンク設定読込RToolStripMenuItem");
            バンク設定読込RToolStripMenuItem.Click += バンク設定読込RToolStripMenuItem_Click;
            // 
            // バンク設定書込WToolStripMenuItem
            // 
            バンク設定書込WToolStripMenuItem.Name = "バンク設定書込WToolStripMenuItem";
            resources.ApplyResources(バンク設定書込WToolStripMenuItem, "バンク設定書込WToolStripMenuItem");
            バンク設定書込WToolStripMenuItem.Click += バンク設定書込WToolStripMenuItem_Click;
            // 
            // Form2
            // 
            AccessibleRole = AccessibleRole.ButtonDropDownGrid;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form2";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView bankdataGridView;
        private DataGridViewTextBoxColumn bankTitle;
        private DataGridViewTextBoxColumn bankName;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem ファイルFToolStripMenuItem;
        private ToolStripMenuItem メイン画面へ戻るToolStripMenuItem;
        private ToolStripMenuItem 無線機XToolStripMenuItem;
        private ToolStripMenuItem バンク設定読込RToolStripMenuItem;
        private ToolStripMenuItem バンク設定書込WToolStripMenuItem;
    }
}