﻿namespace SelfService.Client
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxMachines = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageMsmq = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewQueues = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewMessages = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.tabPageRegistry = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.treeViewRegistryKeys = new System.Windows.Forms.TreeView();
            this.dataGridViewRegistry = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageFiles = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.listBoxFileSources = new System.Windows.Forms.ListBox();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.treeViewFiles = new System.Windows.Forms.TreeView();
            this.buttonTail = new System.Windows.Forms.Button();
            this.buttonShow = new System.Windows.Forms.Button();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageMsmq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQueues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMessages)).BeginInit();
            this.tabPageRegistry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegistry)).BeginInit();
            this.tabPageFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1334, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxMachines);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Enabled = false;
            this.splitContainer1.Size = new System.Drawing.Size(1334, 746);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 1;
            // 
            // listBoxMachines
            // 
            this.listBoxMachines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMachines.FormattingEnabled = true;
            this.listBoxMachines.Location = new System.Drawing.Point(0, 0);
            this.listBoxMachines.Name = "listBoxMachines";
            this.listBoxMachines.Size = new System.Drawing.Size(266, 746);
            this.listBoxMachines.TabIndex = 0;
            this.listBoxMachines.SelectedIndexChanged += new System.EventHandler(this.listBoxMachines_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageMsmq);
            this.tabControl1.Controls.Add(this.tabPageRegistry);
            this.tabControl1.Controls.Add(this.tabPageFiles);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1064, 746);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageMsmq
            // 
            this.tabPageMsmq.Controls.Add(this.splitContainer2);
            this.tabPageMsmq.Location = new System.Drawing.Point(4, 22);
            this.tabPageMsmq.Name = "tabPageMsmq";
            this.tabPageMsmq.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMsmq.Size = new System.Drawing.Size(1056, 720);
            this.tabPageMsmq.TabIndex = 0;
            this.tabPageMsmq.Text = "MSMQ";
            this.tabPageMsmq.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridViewQueues);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(1050, 714);
            this.splitContainer2.SplitterDistance = 350;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridViewQueues
            // 
            this.dataGridViewQueues.AllowUserToAddRows = false;
            this.dataGridViewQueues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewQueues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridViewQueues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewQueues.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewQueues.Name = "dataGridViewQueues";
            this.dataGridViewQueues.Size = new System.Drawing.Size(350, 714);
            this.dataGridViewQueues.TabIndex = 0;
            this.dataGridViewQueues.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewQueues_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 250;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "#";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dataGridViewMessages);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.textBoxMessage);
            this.splitContainer3.Size = new System.Drawing.Size(696, 714);
            this.splitContainer3.SplitterDistance = 400;
            this.splitContainer3.TabIndex = 0;
            // 
            // dataGridViewMessages
            // 
            this.dataGridViewMessages.AllowUserToAddRows = false;
            this.dataGridViewMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.dataGridViewMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMessages.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewMessages.Name = "dataGridViewMessages";
            this.dataGridViewMessages.Size = new System.Drawing.Size(400, 714);
            this.dataGridViewMessages.TabIndex = 0;
            this.dataGridViewMessages.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMessages_CellContentClick);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Size";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Arrived";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMessage.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMessage.Location = new System.Drawing.Point(0, 0);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(292, 714);
            this.textBoxMessage.TabIndex = 0;
            // 
            // tabPageRegistry
            // 
            this.tabPageRegistry.Controls.Add(this.splitContainer4);
            this.tabPageRegistry.Location = new System.Drawing.Point(4, 22);
            this.tabPageRegistry.Name = "tabPageRegistry";
            this.tabPageRegistry.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRegistry.Size = new System.Drawing.Size(1056, 720);
            this.tabPageRegistry.TabIndex = 1;
            this.tabPageRegistry.Text = "Registry";
            this.tabPageRegistry.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.treeViewRegistryKeys);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.dataGridViewRegistry);
            this.splitContainer4.Size = new System.Drawing.Size(1050, 714);
            this.splitContainer4.SplitterDistance = 350;
            this.splitContainer4.TabIndex = 0;
            // 
            // treeViewRegistryKeys
            // 
            this.treeViewRegistryKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewRegistryKeys.Location = new System.Drawing.Point(0, 0);
            this.treeViewRegistryKeys.Name = "treeViewRegistryKeys";
            this.treeViewRegistryKeys.Size = new System.Drawing.Size(350, 714);
            this.treeViewRegistryKeys.TabIndex = 0;
            this.treeViewRegistryKeys.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewRegistryKeys_AfterSelect);
            // 
            // dataGridViewRegistry
            // 
            this.dataGridViewRegistry.AllowUserToAddRows = false;
            this.dataGridViewRegistry.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewRegistry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRegistry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewRegistry.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewRegistry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRegistry.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewRegistry.Name = "dataGridViewRegistry";
            this.dataGridViewRegistry.ReadOnly = true;
            this.dataGridViewRegistry.Size = new System.Drawing.Size(696, 714);
            this.dataGridViewRegistry.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Value";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 300;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Type";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // tabPageFiles
            // 
            this.tabPageFiles.Controls.Add(this.splitContainer5);
            this.tabPageFiles.Location = new System.Drawing.Point(4, 22);
            this.tabPageFiles.Name = "tabPageFiles";
            this.tabPageFiles.Size = new System.Drawing.Size(1056, 720);
            this.tabPageFiles.TabIndex = 2;
            this.tabPageFiles.Text = "Files";
            this.tabPageFiles.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.listBoxFileSources);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer6);
            this.splitContainer5.Size = new System.Drawing.Size(1056, 720);
            this.splitContainer5.SplitterDistance = 352;
            this.splitContainer5.TabIndex = 0;
            // 
            // listBoxFileSources
            // 
            this.listBoxFileSources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFileSources.FormattingEnabled = true;
            this.listBoxFileSources.Location = new System.Drawing.Point(0, 0);
            this.listBoxFileSources.Name = "listBoxFileSources";
            this.listBoxFileSources.Size = new System.Drawing.Size(352, 720);
            this.listBoxFileSources.TabIndex = 0;
            this.listBoxFileSources.SelectedIndexChanged += new System.EventHandler(this.listBoxFileSources_SelectedIndexChanged);
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.treeViewFiles);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.buttonTail);
            this.splitContainer6.Panel2.Controls.Add(this.buttonShow);
            this.splitContainer6.Panel2.Controls.Add(this.buttonDownload);
            this.splitContainer6.Panel2.Controls.Add(this.textBoxFile);
            this.splitContainer6.Size = new System.Drawing.Size(700, 720);
            this.splitContainer6.SplitterDistance = 233;
            this.splitContainer6.TabIndex = 0;
            // 
            // treeViewFiles
            // 
            this.treeViewFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFiles.Location = new System.Drawing.Point(0, 0);
            this.treeViewFiles.Name = "treeViewFiles";
            this.treeViewFiles.Size = new System.Drawing.Size(233, 720);
            this.treeViewFiles.TabIndex = 0;
            this.treeViewFiles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFiles_AfterSelect);
            // 
            // buttonTail
            // 
            this.buttonTail.Location = new System.Drawing.Point(168, 4);
            this.buttonTail.Name = "buttonTail";
            this.buttonTail.Size = new System.Drawing.Size(75, 23);
            this.buttonTail.TabIndex = 3;
            this.buttonTail.Text = "Tail";
            this.buttonTail.UseVisualStyleBackColor = false;
            this.buttonTail.Click += new System.EventHandler(this.buttonTail_Click);
            // 
            // buttonShow
            // 
            this.buttonShow.Location = new System.Drawing.Point(86, 4);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(75, 23);
            this.buttonShow.TabIndex = 2;
            this.buttonShow.Text = "Show";
            this.buttonShow.UseVisualStyleBackColor = true;
            this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // buttonDownload
            // 
            this.buttonDownload.Location = new System.Drawing.Point(4, 4);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(75, 23);
            this.buttonDownload.TabIndex = 1;
            this.buttonDownload.Text = "Download";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // textBoxFile
            // 
            this.textBoxFile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxFile.Location = new System.Drawing.Point(0, 44);
            this.textBoxFile.Multiline = true;
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(463, 676);
            this.textBoxFile.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 748);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1334, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "About ServiceDesk";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 770);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "SelfService";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageMsmq.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQueues)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMessages)).EndInit();
            this.tabPageRegistry.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegistry)).EndInit();
            this.tabPageFiles.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBoxMachines;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageMsmq;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TabPage tabPageRegistry;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        internal System.Windows.Forms.DataGridView dataGridViewQueues;
        internal System.Windows.Forms.DataGridView dataGridViewMessages;
        internal System.Windows.Forms.TextBox textBoxMessage;
        internal System.Windows.Forms.TreeView treeViewRegistryKeys;
        internal System.Windows.Forms.DataGridView dataGridViewRegistry;
        private System.Windows.Forms.TabPage tabPageFiles;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.SplitContainer splitContainer6;
        internal System.Windows.Forms.ListBox listBoxFileSources;
        internal System.Windows.Forms.TreeView treeViewFiles;
        internal System.Windows.Forms.Button buttonTail;
        internal System.Windows.Forms.Button buttonShow;
        internal System.Windows.Forms.Button buttonDownload;
        internal System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.StatusStrip statusStrip1;
        internal System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}
