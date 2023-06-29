using System;

namespace DJ_X100_memory_writer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TreeNode treeNode30 = new TreeNode("メモリーチャンネル");
            TreeNode treeNode31 = new TreeNode("A:");
            TreeNode treeNode32 = new TreeNode("B:");
            TreeNode treeNode33 = new TreeNode("C:");
            TreeNode treeNode34 = new TreeNode("D:");
            TreeNode treeNode35 = new TreeNode("E:");
            TreeNode treeNode36 = new TreeNode("F:");
            TreeNode treeNode37 = new TreeNode("G:");
            TreeNode treeNode38 = new TreeNode("H:");
            TreeNode treeNode39 = new TreeNode("I:");
            TreeNode treeNode40 = new TreeNode("J:");
            TreeNode treeNode41 = new TreeNode("K:");
            TreeNode treeNode42 = new TreeNode("L:");
            TreeNode treeNode43 = new TreeNode("M:");
            TreeNode treeNode44 = new TreeNode("N:");
            TreeNode treeNode45 = new TreeNode("O:");
            TreeNode treeNode46 = new TreeNode("P:");
            TreeNode treeNode47 = new TreeNode("Q:");
            TreeNode treeNode48 = new TreeNode("R:");
            TreeNode treeNode49 = new TreeNode("S:");
            TreeNode treeNode50 = new TreeNode("T:");
            TreeNode treeNode51 = new TreeNode("U:");
            TreeNode treeNode52 = new TreeNode("V:");
            TreeNode treeNode53 = new TreeNode("W:");
            TreeNode treeNode54 = new TreeNode("X:");
            TreeNode treeNode55 = new TreeNode("Y:");
            TreeNode treeNode56 = new TreeNode("Z:");
            TreeNode treeNode57 = new TreeNode("バンクメモリ", new TreeNode[] { treeNode31, treeNode32, treeNode33, treeNode34, treeNode35, treeNode36, treeNode37, treeNode38, treeNode39, treeNode40, treeNode41, treeNode42, treeNode43, treeNode44, treeNode45, treeNode46, treeNode47, treeNode48, treeNode49, treeNode50, treeNode51, treeNode52, treeNode53, treeNode54, treeNode55, treeNode56 });
            TreeNode treeNode58 = new TreeNode("DJ-X100", new TreeNode[] { treeNode30, treeNode57 });
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            ファイルFToolStripMenuItem = new ToolStripMenuItem();
            新規作成NToolStripMenuItem = new ToolStripMenuItem();
            開くNToolStripMenuItem = new ToolStripMenuItem();
            名前を付けて保存NToolStrpMenuItem = new ToolStripMenuItem();
            エクスポートEToolStripMenuItem = new ToolStripMenuItem();
            x100cmdexe用CSVToolStripMenuItem = new ToolStripMenuItem();
            終了NToolStripMenuItem = new ToolStripMenuItem();
            表示VToolStripMenuItem = new ToolStripMenuItem();
            バンク設定BToolStripMenuItem = new ToolStripMenuItem();
            オプションOToolStripMenuItem = new ToolStripMenuItem();
            cOMポートCToolStripMenuItem = new ToolStripMenuItem();
            読み込みRToolStripMenuItem = new ToolStripMenuItem();
            書き込みToolStripMenuItem = new ToolStripMenuItem();
            ヘルプHToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            treeView1 = new TreeView();
            imageList1 = new ImageList(components);
            memoryChDataGridView = new DataGridView();
            statusStrip1 = new StatusStrip();
            toolStripProgressBar1 = new ToolStripProgressBar();
            statusLabel1 = new ToolStripStatusLabel();
            selectedComportLabel = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)memoryChDataGridView).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { ファイルFToolStripMenuItem, 表示VToolStripMenuItem, オプションOToolStripMenuItem, ヘルプHToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1284, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            ファイルFToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 新規作成NToolStripMenuItem, 開くNToolStripMenuItem, 名前を付けて保存NToolStrpMenuItem, エクスポートEToolStripMenuItem, 終了NToolStripMenuItem });
            ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            ファイルFToolStripMenuItem.Size = new Size(67, 20);
            ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 新規作成NToolStripMenuItem
            // 
            新規作成NToolStripMenuItem.Name = "新規作成NToolStripMenuItem";
            新規作成NToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            新規作成NToolStripMenuItem.Size = new Size(257, 22);
            新規作成NToolStripMenuItem.Text = "新規作成(&N)";
            新規作成NToolStripMenuItem.Click += 新規作成NToolStripMenuItem_Click;
            // 
            // 開くNToolStripMenuItem
            // 
            開くNToolStripMenuItem.Name = "開くNToolStripMenuItem";
            開くNToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            開くNToolStripMenuItem.Size = new Size(257, 22);
            開くNToolStripMenuItem.Text = "開く(&O)...";
            開くNToolStripMenuItem.Click += 開くNToolStripMenuItem_Click;
            // 
            // 名前を付けて保存NToolStrpMenuItem
            // 
            名前を付けて保存NToolStrpMenuItem.Name = "名前を付けて保存NToolStrpMenuItem";
            名前を付けて保存NToolStrpMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            名前を付けて保存NToolStrpMenuItem.Size = new Size(257, 22);
            名前を付けて保存NToolStrpMenuItem.Text = "名前を付けて保存(&A)...";
            名前を付けて保存NToolStrpMenuItem.Click += 名前を付けて保存NToolStrpMenuItem_Click;
            // 
            // エクスポートEToolStripMenuItem
            // 
            エクスポートEToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { x100cmdexe用CSVToolStripMenuItem });
            エクスポートEToolStripMenuItem.Name = "エクスポートEToolStripMenuItem";
            エクスポートEToolStripMenuItem.Size = new Size(257, 22);
            エクスポートEToolStripMenuItem.Text = "エクスポート(&E)";
            // 
            // x100cmdexe用CSVToolStripMenuItem
            // 
            x100cmdexe用CSVToolStripMenuItem.Name = "x100cmdexe用CSVToolStripMenuItem";
            x100cmdexe用CSVToolStripMenuItem.Size = new Size(174, 22);
            x100cmdexe用CSVToolStripMenuItem.Text = "x100cmd.exe用CSV";
            x100cmdexe用CSVToolStripMenuItem.Click += x100cmdexe用CSVToolStripMenuItem_Click;
            // 
            // 終了NToolStripMenuItem
            // 
            終了NToolStripMenuItem.Name = "終了NToolStripMenuItem";
            終了NToolStripMenuItem.Size = new Size(257, 22);
            終了NToolStripMenuItem.Text = "終了(&X)";
            終了NToolStripMenuItem.Click += 終了NToolStripMenuItem_Click;
            // 
            // 表示VToolStripMenuItem
            // 
            表示VToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { バンク設定BToolStripMenuItem });
            表示VToolStripMenuItem.Name = "表示VToolStripMenuItem";
            表示VToolStripMenuItem.Size = new Size(62, 20);
            表示VToolStripMenuItem.Text = "バンク(&B)";
            // 
            // バンク設定BToolStripMenuItem
            // 
            バンク設定BToolStripMenuItem.Name = "バンク設定BToolStripMenuItem";
            バンク設定BToolStripMenuItem.Size = new Size(180, 22);
            バンク設定BToolStripMenuItem.Text = "バンク設定(&B)";
            バンク設定BToolStripMenuItem.Click += バンク設定BToolStripMenuItem_Click;
            // 
            // オプションOToolStripMenuItem
            // 
            オプションOToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cOMポートCToolStripMenuItem, 読み込みRToolStripMenuItem, 書き込みToolStripMenuItem });
            オプションOToolStripMenuItem.Name = "オプションOToolStripMenuItem";
            オプションOToolStripMenuItem.Size = new Size(70, 20);
            オプションOToolStripMenuItem.Text = "無線機(&X)";
            // 
            // cOMポートCToolStripMenuItem
            // 
            cOMポートCToolStripMenuItem.Name = "cOMポートCToolStripMenuItem";
            cOMポートCToolStripMenuItem.Size = new Size(180, 22);
            cOMポートCToolStripMenuItem.Text = "COMポート(&C)";
            // 
            // 読み込みRToolStripMenuItem
            // 
            読み込みRToolStripMenuItem.Name = "読み込みRToolStripMenuItem";
            読み込みRToolStripMenuItem.Size = new Size(180, 22);
            読み込みRToolStripMenuItem.Text = "読み込み(&R)";
            読み込みRToolStripMenuItem.Click += 読み込みRToolStripMenuItem_Click;
            // 
            // 書き込みToolStripMenuItem
            // 
            書き込みToolStripMenuItem.Name = "書き込みToolStripMenuItem";
            書き込みToolStripMenuItem.Size = new Size(180, 22);
            書き込みToolStripMenuItem.Text = "書き込み(&W)";
            書き込みToolStripMenuItem.Click += 書き込みToolStripMenuItem_Click;
            // 
            // ヘルプHToolStripMenuItem
            // 
            ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
            ヘルプHToolStripMenuItem.Size = new Size(65, 20);
            ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeView1);
            splitContainer1.Panel1.Paint += splitContainer1_Panel1_Paint;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(memoryChDataGridView);
            splitContainer1.Size = new Size(1284, 662);
            splitContainer1.SplitterDistance = 228;
            splitContainer1.TabIndex = 2;
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Fill;
            treeView1.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            treeView1.ImageIndex = 0;
            treeView1.ImageList = imageList1;
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeNode30.ImageKey = "kkrn_icon_folder_1.png";
            treeNode30.Name = "メモリーチャンネル";
            treeNode30.Text = "メモリーチャンネル";
            treeNode31.ImageIndex = 1;
            treeNode31.Name = "bankA";
            treeNode31.Tag = "bankA";
            treeNode31.Text = "A:";
            treeNode32.ImageKey = "kkrn_icon_folder_1.png";
            treeNode32.Name = "bankB";
            treeNode32.Tag = "bankB";
            treeNode32.Text = "B:";
            treeNode33.ImageKey = "kkrn_icon_folder_1.png";
            treeNode33.Name = "bankC";
            treeNode33.Text = "C:";
            treeNode34.ImageKey = "kkrn_icon_folder_1.png";
            treeNode34.Name = "bankD";
            treeNode34.Text = "D:";
            treeNode35.ImageKey = "kkrn_icon_folder_1.png";
            treeNode35.Name = "bankE";
            treeNode35.Text = "E:";
            treeNode36.ImageKey = "kkrn_icon_folder_1.png";
            treeNode36.Name = "bankF";
            treeNode36.Text = "F:";
            treeNode37.ImageKey = "kkrn_icon_folder_1.png";
            treeNode37.Name = "bankG";
            treeNode37.Text = "G:";
            treeNode38.ImageKey = "kkrn_icon_folder_1.png";
            treeNode38.Name = "bankH";
            treeNode38.Text = "H:";
            treeNode39.ImageKey = "kkrn_icon_folder_1.png";
            treeNode39.Name = "bankI";
            treeNode39.Text = "I:";
            treeNode40.ImageKey = "kkrn_icon_folder_1.png";
            treeNode40.Name = "bankJ";
            treeNode40.Text = "J:";
            treeNode41.ImageKey = "kkrn_icon_folder_1.png";
            treeNode41.Name = "bankK";
            treeNode41.Text = "K:";
            treeNode42.ImageKey = "kkrn_icon_folder_1.png";
            treeNode42.Name = "bankL";
            treeNode42.Text = "L:";
            treeNode43.ImageKey = "kkrn_icon_folder_1.png";
            treeNode43.Name = "bankM";
            treeNode43.Text = "M:";
            treeNode44.ImageKey = "kkrn_icon_folder_1.png";
            treeNode44.Name = "bankN";
            treeNode44.Text = "N:";
            treeNode45.ImageKey = "kkrn_icon_folder_1.png";
            treeNode45.Name = "bankO";
            treeNode45.Text = "O:";
            treeNode46.ImageKey = "kkrn_icon_folder_1.png";
            treeNode46.Name = "bankP";
            treeNode46.Text = "P:";
            treeNode47.ImageKey = "kkrn_icon_folder_1.png";
            treeNode47.Name = "bankQ";
            treeNode47.Text = "Q:";
            treeNode48.ImageKey = "kkrn_icon_folder_1.png";
            treeNode48.Name = "bankR";
            treeNode48.Text = "R:";
            treeNode49.ImageKey = "kkrn_icon_folder_1.png";
            treeNode49.Name = "bankS";
            treeNode49.Text = "S:";
            treeNode50.ImageKey = "kkrn_icon_folder_1.png";
            treeNode50.Name = "bankT";
            treeNode50.Text = "T:";
            treeNode51.ImageKey = "kkrn_icon_folder_1.png";
            treeNode51.Name = "bankU";
            treeNode51.Text = "U:";
            treeNode52.ImageKey = "kkrn_icon_folder_1.png";
            treeNode52.Name = "bankV";
            treeNode52.Text = "V:";
            treeNode53.ImageKey = "kkrn_icon_folder_1.png";
            treeNode53.Name = "bankW";
            treeNode53.Text = "W:";
            treeNode54.ImageKey = "kkrn_icon_folder_1.png";
            treeNode54.Name = "bankX";
            treeNode54.Text = "X:";
            treeNode55.ImageKey = "kkrn_icon_folder_1.png";
            treeNode55.Name = "bankY:";
            treeNode55.Text = "Y:";
            treeNode56.ImageKey = "kkrn_icon_folder_1.png";
            treeNode56.Name = "bankZ";
            treeNode56.Text = "Z:";
            treeNode57.ImageKey = "kkrn_icon_folder_1.png";
            treeNode57.Name = "bankMemoryNode";
            treeNode57.Text = "バンクメモリ";
            treeNode58.ImageIndex = 0;
            treeNode58.Name = "djx100Node";
            treeNode58.Text = "DJ-X100";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode58 });
            treeView1.SelectedImageIndex = 0;
            treeView1.Size = new Size(228, 662);
            treeView1.TabIndex = 0;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "radio.ico");
            imageList1.Images.SetKeyName(1, "kkrn_icon_folder_1.png");
            // 
            // memoryChDataGridView
            // 
            memoryChDataGridView.AllowUserToAddRows = false;
            memoryChDataGridView.AllowUserToDeleteRows = false;
            memoryChDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            memoryChDataGridView.Dock = DockStyle.Fill;
            memoryChDataGridView.Location = new Point(0, 0);
            memoryChDataGridView.Name = "memoryChDataGridView";
            memoryChDataGridView.RowTemplate.Height = 25;
            memoryChDataGridView.Size = new Size(1052, 662);
            memoryChDataGridView.TabIndex = 0;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripProgressBar1, statusLabel1, selectedComportLabel });
            statusStrip1.Location = new Point(0, 689);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1284, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(100, 16);
            // 
            // statusLabel1
            // 
            statusLabel1.Name = "statusLabel1";
            statusLabel1.Size = new Size(0, 17);
            // 
            // selectedComportLabel
            // 
            selectedComportLabel.Name = "selectedComportLabel";
            selectedComportLabel.Size = new Size(148, 17);
            selectedComportLabel.Text = "選択中のCOMポート: 未選択";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1284, 711);
            Controls.Add(statusStrip1);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "DJ-X100 Memory Writer (非公式) v0.9.1 (β版)";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)memoryChDataGridView).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem ファイルFToolStripMenuItem;
        private ToolStripMenuItem 新規作成NToolStripMenuItem;
        private ToolStripMenuItem 開くNToolStripMenuItem;
        private ToolStripMenuItem 名前を付けて保存NToolStrpMenuItem;
        private ToolStripMenuItem 終了NToolStripMenuItem;
        private ToolStripMenuItem 表示VToolStripMenuItem;
        private ToolStripMenuItem オプションOToolStripMenuItem;
        private ToolStripMenuItem ヘルプHToolStripMenuItem;
        private SplitContainer splitContainer1;
        private TreeView treeView1;
        private ImageList imageList1;
        private DataGridView memoryChDataGridView;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripMenuItem cOMポートCToolStripMenuItem;
        private ToolStripStatusLabel statusLabel1;
        private ToolStripStatusLabel selectedComportLabel;
        private ToolStripMenuItem 書き込みToolStripMenuItem;
        private ToolStripMenuItem エクスポートEToolStripMenuItem;
        private ToolStripMenuItem x100cmdexe用CSVToolStripMenuItem;
        private ToolStripMenuItem バンク設定BToolStripMenuItem;
        private ToolStripMenuItem 読み込みRToolStripMenuItem;
    }
}