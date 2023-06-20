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
            TreeNode treeNode2 = new TreeNode("バンクメモリ(工事中)");
            TreeNode treeNode3 = new TreeNode("DJ-X100", new TreeNode[] { treeNode1, treeNode2 });
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
            表示VToolStripMenuItem.Name = "表示VToolStripMenuItem";
            表示VToolStripMenuItem.Size = new Size(58, 20);
            表示VToolStripMenuItem.Text = "表示(&V)";
            // 
            // オプションOToolStripMenuItem
            // 
            オプションOToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cOMポートCToolStripMenuItem, 書き込みToolStripMenuItem });
            オプションOToolStripMenuItem.Name = "オプションOToolStripMenuItem";
            オプションOToolStripMenuItem.Size = new Size(70, 20);
            オプションOToolStripMenuItem.Text = "無線機(&X)";
            // 
            // cOMポートCToolStripMenuItem
            // 
            cOMポートCToolStripMenuItem.Name = "cOMポートCToolStripMenuItem";
            cOMポートCToolStripMenuItem.Size = new Size(142, 22);
            cOMポートCToolStripMenuItem.Text = "COMポート(&C)";
            // 
            // 書き込みToolStripMenuItem
            // 
            書き込みToolStripMenuItem.Name = "書き込みToolStripMenuItem";
            書き込みToolStripMenuItem.Size = new Size(142, 22);
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
            splitContainer1.SplitterDistance = 196;
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
            treeNode2.ImageKey = "kkrn_icon_folder_1.png";
            treeNode2.Name = "バンクメモリ";
            treeNode2.Text = "バンクメモリ(工事中)";
            treeNode3.ImageIndex = 0;
            treeNode3.Name = "ノード0";
            treeNode3.Text = "DJ-X100";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode3 });
            treeView1.SelectedImageIndex = 0;
            treeView1.Size = new Size(196, 662);
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
            memoryChDataGridView.Size = new Size(1084, 662);
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
            Text = "DJ-X100 Memory Writer(非公式)";
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
    }
}