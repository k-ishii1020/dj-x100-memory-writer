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
            TreeNode treeNode1 = new TreeNode("メモリーチャンネル");
            TreeNode treeNode2 = new TreeNode("A:");
            TreeNode treeNode3 = new TreeNode("B:");
            TreeNode treeNode4 = new TreeNode("C:");
            TreeNode treeNode5 = new TreeNode("D:");
            TreeNode treeNode6 = new TreeNode("E:");
            TreeNode treeNode7 = new TreeNode("F:");
            TreeNode treeNode8 = new TreeNode("G:");
            TreeNode treeNode9 = new TreeNode("H:");
            TreeNode treeNode10 = new TreeNode("I:");
            TreeNode treeNode11 = new TreeNode("J:");
            TreeNode treeNode12 = new TreeNode("K:");
            TreeNode treeNode13 = new TreeNode("L:");
            TreeNode treeNode14 = new TreeNode("M:");
            TreeNode treeNode15 = new TreeNode("N:");
            TreeNode treeNode16 = new TreeNode("O:");
            TreeNode treeNode17 = new TreeNode("P:");
            TreeNode treeNode18 = new TreeNode("Q:");
            TreeNode treeNode19 = new TreeNode("R:");
            TreeNode treeNode20 = new TreeNode("S:");
            TreeNode treeNode21 = new TreeNode("T:");
            TreeNode treeNode22 = new TreeNode("U:");
            TreeNode treeNode23 = new TreeNode("V:");
            TreeNode treeNode24 = new TreeNode("W:");
            TreeNode treeNode25 = new TreeNode("X:");
            TreeNode treeNode26 = new TreeNode("Y:");
            TreeNode treeNode27 = new TreeNode("Z:");
            TreeNode treeNode28 = new TreeNode("バンクメモリ", new TreeNode[] { treeNode2, treeNode3, treeNode4, treeNode5, treeNode6, treeNode7, treeNode8, treeNode9, treeNode10, treeNode11, treeNode12, treeNode13, treeNode14, treeNode15, treeNode16, treeNode17, treeNode18, treeNode19, treeNode20, treeNode21, treeNode22, treeNode23, treeNode24, treeNode25, treeNode26, treeNode27 });
            TreeNode treeNode29 = new TreeNode("DJ-X100", new TreeNode[] { treeNode1, treeNode28 });
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            ファイルFToolStripMenuItem = new ToolStripMenuItem();
            新規作成NToolStripMenuItem = new ToolStripMenuItem();
            開くNToolStripMenuItem = new ToolStripMenuItem();
            上書き保存NToolStripMenuItem = new ToolStripMenuItem();
            名前を付けて保存NToolStrpMenuItem = new ToolStripMenuItem();
            エクスポートEToolStripMenuItem = new ToolStripMenuItem();
            x100cmdexe用CSVToolStripMenuItem = new ToolStripMenuItem();
            終了NToolStripMenuItem = new ToolStripMenuItem();
            表示VToolStripMenuItem = new ToolStripMenuItem();
            バンク設定BToolStripMenuItem = new ToolStripMenuItem();
            オプションOToolStripMenuItem = new ToolStripMenuItem();
            cOMポートCToolStripMenuItem = new ToolStripMenuItem();
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
            読み込みRToolStripMenuItem = new ToolStripMenuItem();
            読み込みRToolStripMenuItem1 = new ToolStripMenuItem();
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
            ファイルFToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 新規作成NToolStripMenuItem, 開くNToolStripMenuItem, 上書き保存NToolStripMenuItem, 名前を付けて保存NToolStrpMenuItem, エクスポートEToolStripMenuItem, 終了NToolStripMenuItem });
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
            // 上書き保存NToolStripMenuItem
            // 
            上書き保存NToolStripMenuItem.Name = "上書き保存NToolStripMenuItem";
            上書き保存NToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            上書き保存NToolStripMenuItem.Size = new Size(257, 22);
            上書き保存NToolStripMenuItem.Text = "上書き保存(&S)";
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
            バンク設定BToolStripMenuItem.Size = new Size(141, 22);
            バンク設定BToolStripMenuItem.Text = "バンク設定(&B)";
            バンク設定BToolStripMenuItem.Click += バンク設定BToolStripMenuItem_Click;
            // 
            // オプションOToolStripMenuItem
            // 
            オプションOToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cOMポートCToolStripMenuItem, 読み込みRToolStripMenuItem, 書き込みToolStripMenuItem, 読み込みRToolStripMenuItem1 });
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
            treeNode1.ImageKey = "kkrn_icon_folder_1.png";
            treeNode1.Name = "メモリーチャンネル";
            treeNode1.Text = "メモリーチャンネル";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "bankA";
            treeNode2.Tag = "bankA";
            treeNode2.Text = "A:";
            treeNode3.ImageKey = "kkrn_icon_folder_1.png";
            treeNode3.Name = "bankB";
            treeNode3.Tag = "bankB";
            treeNode3.Text = "B:";
            treeNode4.ImageKey = "kkrn_icon_folder_1.png";
            treeNode4.Name = "bankC";
            treeNode4.Text = "C:";
            treeNode5.ImageKey = "kkrn_icon_folder_1.png";
            treeNode5.Name = "bankD";
            treeNode5.Text = "D:";
            treeNode6.ImageKey = "kkrn_icon_folder_1.png";
            treeNode6.Name = "bankE";
            treeNode6.Text = "E:";
            treeNode7.ImageKey = "kkrn_icon_folder_1.png";
            treeNode7.Name = "bankF";
            treeNode7.Text = "F:";
            treeNode8.ImageKey = "kkrn_icon_folder_1.png";
            treeNode8.Name = "bankG";
            treeNode8.Text = "G:";
            treeNode9.ImageKey = "kkrn_icon_folder_1.png";
            treeNode9.Name = "bankH";
            treeNode9.Text = "H:";
            treeNode10.ImageKey = "kkrn_icon_folder_1.png";
            treeNode10.Name = "bankI";
            treeNode10.Text = "I:";
            treeNode11.ImageKey = "kkrn_icon_folder_1.png";
            treeNode11.Name = "bankJ";
            treeNode11.Text = "J:";
            treeNode12.ImageKey = "kkrn_icon_folder_1.png";
            treeNode12.Name = "bankK";
            treeNode12.Text = "K:";
            treeNode13.ImageKey = "kkrn_icon_folder_1.png";
            treeNode13.Name = "bankL";
            treeNode13.Text = "L:";
            treeNode14.ImageKey = "kkrn_icon_folder_1.png";
            treeNode14.Name = "bankM";
            treeNode14.Text = "M:";
            treeNode15.ImageKey = "kkrn_icon_folder_1.png";
            treeNode15.Name = "bankN";
            treeNode15.Text = "N:";
            treeNode16.ImageKey = "kkrn_icon_folder_1.png";
            treeNode16.Name = "bankO";
            treeNode16.Text = "O:";
            treeNode17.ImageKey = "kkrn_icon_folder_1.png";
            treeNode17.Name = "bankP";
            treeNode17.Text = "P:";
            treeNode18.ImageKey = "kkrn_icon_folder_1.png";
            treeNode18.Name = "bankQ";
            treeNode18.Text = "Q:";
            treeNode19.ImageKey = "kkrn_icon_folder_1.png";
            treeNode19.Name = "bankR";
            treeNode19.Text = "R:";
            treeNode20.ImageKey = "kkrn_icon_folder_1.png";
            treeNode20.Name = "bankS";
            treeNode20.Text = "S:";
            treeNode21.ImageKey = "kkrn_icon_folder_1.png";
            treeNode21.Name = "bankT";
            treeNode21.Text = "T:";
            treeNode22.ImageKey = "kkrn_icon_folder_1.png";
            treeNode22.Name = "bankU";
            treeNode22.Text = "U:";
            treeNode23.ImageKey = "kkrn_icon_folder_1.png";
            treeNode23.Name = "bankV";
            treeNode23.Text = "V:";
            treeNode24.ImageKey = "kkrn_icon_folder_1.png";
            treeNode24.Name = "bankW";
            treeNode24.Text = "W:";
            treeNode25.ImageKey = "kkrn_icon_folder_1.png";
            treeNode25.Name = "bankX";
            treeNode25.Text = "X:";
            treeNode26.ImageKey = "kkrn_icon_folder_1.png";
            treeNode26.Name = "bankY:";
            treeNode26.Text = "Y:";
            treeNode27.ImageKey = "kkrn_icon_folder_1.png";
            treeNode27.Name = "bankZ";
            treeNode27.Text = "Z:";
            treeNode28.ImageKey = "kkrn_icon_folder_1.png";
            treeNode28.Name = "bankMemoryNode";
            treeNode28.Text = "バンクメモリ";
            treeNode29.ImageIndex = 0;
            treeNode29.Name = "djx100Node";
            treeNode29.Text = "DJ-X100";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode29 });
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
            // 読み込みRToolStripMenuItem
            // 
            読み込みRToolStripMenuItem.Name = "読み込みRToolStripMenuItem";
            読み込みRToolStripMenuItem.Size = new Size(180, 22);
            読み込みRToolStripMenuItem.Text = "読み込み(&R)";
            読み込みRToolStripMenuItem.Click += 読み込みRToolStripMenuItem_Click;
            // 
            // 読み込みRToolStripMenuItem1
            // 
            読み込みRToolStripMenuItem1.Name = "読み込みRToolStripMenuItem1";
            読み込みRToolStripMenuItem1.Size = new Size(180, 22);
            読み込みRToolStripMenuItem1.Text = "読み込み(&R)";
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
            Text = "DJ-X100 Memory Writer (非公式) v0.9.0 (β版)";
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
        private ToolStripMenuItem 上書き保存NToolStripMenuItem;
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
        private ToolStripMenuItem 読み込みRToolStripMenuItem1;
    }
}