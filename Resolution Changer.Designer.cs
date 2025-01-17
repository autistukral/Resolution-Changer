namespace Resolution_Changer
{
    partial class ResolutionChanger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResolutionChanger));
            availableResolutionsCB = new ComboBox();
            resolution1Label = new Label();
            applyButton = new Button();
            resolution2Label = new Label();
            availableResolutionsCB2 = new ComboBox();
            notifyIcon = new NotifyIcon(components);
            contextMenuStrip = new ContextMenuStrip(components);
            resolution1ToolStripMenuItem = new ToolStripMenuItem();
            resolution2ToolStripMenuItem = new ToolStripMenuItem();
            resolution3ToolStripMenuItem = new ToolStripMenuItem();
            runOnStartupToolStripMenuItem = new ToolStripMenuItem();
            clearRegistryToolStripMenuItem = new ToolStripMenuItem();
            showToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            applyRes1Button = new Button();
            applyRes2Button = new Button();
            resolution3Label = new Label();
            availableResolutionsCB3 = new ComboBox();
            applyRes3Button = new Button();
            btn_addProcess = new Button();
            listView_addedProcesses = new ListView();
            contextMenuStripAddedList = new ContextMenuStrip(components);
            reloadToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            resolution1ToolStripMenuItem1 = new ToolStripMenuItem();
            resolution2ToolStripMenuItem1 = new ToolStripMenuItem();
            resolution3ToolStripMenuItem1 = new ToolStripMenuItem();
            priority1ToolStripMenuItem = new ToolStripMenuItem();
            priority1ToolStripMenuItem1 = new ToolStripMenuItem();
            priority3ToolStripMenuItem = new ToolStripMenuItem();
            priority4ToolStripMenuItem = new ToolStripMenuItem();
            imageListAddedProcs = new ImageList(components);
            contextMenuStrip.SuspendLayout();
            contextMenuStripAddedList.SuspendLayout();
            SuspendLayout();
            // 
            // availableResolutionsCB
            // 
            availableResolutionsCB.BackColor = Color.FromArgb(52, 54, 69);
            availableResolutionsCB.DropDownStyle = ComboBoxStyle.DropDownList;
            availableResolutionsCB.FlatStyle = FlatStyle.Flat;
            availableResolutionsCB.ForeColor = Color.White;
            availableResolutionsCB.FormattingEnabled = true;
            availableResolutionsCB.Location = new Point(93, 15);
            availableResolutionsCB.Name = "availableResolutionsCB";
            availableResolutionsCB.Size = new Size(140, 23);
            availableResolutionsCB.TabIndex = 0;
            // 
            // resolution1Label
            // 
            resolution1Label.AutoSize = true;
            resolution1Label.Location = new Point(12, 21);
            resolution1Label.Name = "resolution1Label";
            resolution1Label.Size = new Size(75, 15);
            resolution1Label.TabIndex = 1;
            resolution1Label.Text = "Resolution 1:";
            // 
            // applyButton
            // 
            applyButton.BackColor = Color.FromArgb(52, 55, 69);
            applyButton.FlatAppearance.BorderSize = 0;
            applyButton.FlatStyle = FlatStyle.Flat;
            applyButton.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            applyButton.Location = new Point(212, 197);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(100, 35);
            applyButton.TabIndex = 2;
            applyButton.Text = "Save";
            applyButton.TextAlign = ContentAlignment.TopCenter;
            applyButton.UseVisualStyleBackColor = false;
            applyButton.Click += applyButton_Click;
            // 
            // resolution2Label
            // 
            resolution2Label.AutoSize = true;
            resolution2Label.Location = new Point(12, 55);
            resolution2Label.Name = "resolution2Label";
            resolution2Label.Size = new Size(75, 15);
            resolution2Label.TabIndex = 3;
            resolution2Label.Text = "Resolution 2:";
            // 
            // availableResolutionsCB2
            // 
            availableResolutionsCB2.BackColor = Color.FromArgb(52, 54, 69);
            availableResolutionsCB2.DropDownStyle = ComboBoxStyle.DropDownList;
            availableResolutionsCB2.FlatStyle = FlatStyle.Flat;
            availableResolutionsCB2.ForeColor = Color.White;
            availableResolutionsCB2.FormattingEnabled = true;
            availableResolutionsCB2.Location = new Point(93, 49);
            availableResolutionsCB2.Name = "availableResolutionsCB2";
            availableResolutionsCB2.Size = new Size(140, 23);
            availableResolutionsCB2.TabIndex = 4;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "Resolution Changer";
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.BackColor = Color.FromArgb(36, 38, 49);
            contextMenuStrip.BackgroundImageLayout = ImageLayout.None;
            contextMenuStrip.ForeColor = Color.White;
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { resolution1ToolStripMenuItem, resolution2ToolStripMenuItem, resolution3ToolStripMenuItem, runOnStartupToolStripMenuItem, clearRegistryToolStripMenuItem, showToolStripMenuItem, exitToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.RenderMode = ToolStripRenderMode.Professional;
            contextMenuStrip.Size = new Size(153, 158);
            // 
            // resolution1ToolStripMenuItem
            // 
            resolution1ToolStripMenuItem.Name = "resolution1ToolStripMenuItem";
            resolution1ToolStripMenuItem.Size = new Size(152, 22);
            resolution1ToolStripMenuItem.Text = "Resolution 1";
            resolution1ToolStripMenuItem.Click += applyRes1Button_Click;
            // 
            // resolution2ToolStripMenuItem
            // 
            resolution2ToolStripMenuItem.Name = "resolution2ToolStripMenuItem";
            resolution2ToolStripMenuItem.Size = new Size(152, 22);
            resolution2ToolStripMenuItem.Text = "Resolution 2";
            resolution2ToolStripMenuItem.Click += applyRes2Button_Click;
            // 
            // resolution3ToolStripMenuItem
            // 
            resolution3ToolStripMenuItem.Name = "resolution3ToolStripMenuItem";
            resolution3ToolStripMenuItem.Size = new Size(152, 22);
            resolution3ToolStripMenuItem.Text = "Resolution 3";
            resolution3ToolStripMenuItem.Click += applyRes3Button_Click;
            // 
            // runOnStartupToolStripMenuItem
            // 
            runOnStartupToolStripMenuItem.Name = "runOnStartupToolStripMenuItem";
            runOnStartupToolStripMenuItem.Size = new Size(152, 22);
            runOnStartupToolStripMenuItem.Text = "Run on startup";
            runOnStartupToolStripMenuItem.Click += runOnStartupToolStripMenuItem_Click;
            // 
            // clearRegistryToolStripMenuItem
            // 
            clearRegistryToolStripMenuItem.Name = "clearRegistryToolStripMenuItem";
            clearRegistryToolStripMenuItem.Size = new Size(152, 22);
            clearRegistryToolStripMenuItem.Text = "Clear Registry";
            clearRegistryToolStripMenuItem.Click += clearRegistryToolStripMenuItem_Click;
            // 
            // showToolStripMenuItem
            // 
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.Size = new Size(152, 22);
            showToolStripMenuItem.Text = "Show";
            showToolStripMenuItem.Click += showToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(152, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // applyRes1Button
            // 
            applyRes1Button.BackColor = Color.FromArgb(52, 55, 69);
            applyRes1Button.FlatAppearance.BorderSize = 0;
            applyRes1Button.FlatStyle = FlatStyle.Flat;
            applyRes1Button.Location = new Point(239, 15);
            applyRes1Button.Name = "applyRes1Button";
            applyRes1Button.Size = new Size(150, 24);
            applyRes1Button.TabIndex = 5;
            applyRes1Button.Text = "Ctrl+Shift+1";
            applyRes1Button.UseVisualStyleBackColor = false;
            applyRes1Button.Click += applyRes1Button_Click;
            // 
            // applyRes2Button
            // 
            applyRes2Button.BackColor = Color.FromArgb(52, 55, 69);
            applyRes2Button.FlatAppearance.BorderSize = 0;
            applyRes2Button.FlatStyle = FlatStyle.Flat;
            applyRes2Button.Location = new Point(239, 49);
            applyRes2Button.Name = "applyRes2Button";
            applyRes2Button.Size = new Size(150, 24);
            applyRes2Button.TabIndex = 6;
            applyRes2Button.Text = "Ctrl+Shift+2";
            applyRes2Button.UseVisualStyleBackColor = false;
            applyRes2Button.Click += applyRes2Button_Click;
            // 
            // resolution3Label
            // 
            resolution3Label.AutoSize = true;
            resolution3Label.Location = new Point(12, 89);
            resolution3Label.Name = "resolution3Label";
            resolution3Label.Size = new Size(75, 15);
            resolution3Label.TabIndex = 7;
            resolution3Label.Text = "Resolution 3:";
            // 
            // availableResolutionsCB3
            // 
            availableResolutionsCB3.BackColor = Color.FromArgb(52, 54, 69);
            availableResolutionsCB3.DropDownStyle = ComboBoxStyle.DropDownList;
            availableResolutionsCB3.DropDownWidth = 120;
            availableResolutionsCB3.FlatStyle = FlatStyle.Flat;
            availableResolutionsCB3.ForeColor = Color.White;
            availableResolutionsCB3.FormattingEnabled = true;
            availableResolutionsCB3.Location = new Point(93, 83);
            availableResolutionsCB3.Name = "availableResolutionsCB3";
            availableResolutionsCB3.Size = new Size(140, 23);
            availableResolutionsCB3.TabIndex = 8;
            // 
            // applyRes3Button
            // 
            applyRes3Button.BackColor = Color.FromArgb(52, 55, 69);
            applyRes3Button.FlatAppearance.BorderSize = 0;
            applyRes3Button.FlatStyle = FlatStyle.Flat;
            applyRes3Button.Location = new Point(239, 83);
            applyRes3Button.Name = "applyRes3Button";
            applyRes3Button.Size = new Size(150, 24);
            applyRes3Button.TabIndex = 9;
            applyRes3Button.Text = "Ctrl+Shift+3";
            applyRes3Button.UseVisualStyleBackColor = false;
            applyRes3Button.Click += applyRes3Button_Click;
            // 
            // btn_addProcess
            // 
            btn_addProcess.BackColor = Color.FromArgb(52, 55, 69);
            btn_addProcess.BackgroundImageLayout = ImageLayout.None;
            btn_addProcess.FlatAppearance.BorderSize = 0;
            btn_addProcess.FlatStyle = FlatStyle.Flat;
            btn_addProcess.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_addProcess.Location = new Point(91, 197);
            btn_addProcess.Margin = new Padding(0);
            btn_addProcess.Name = "btn_addProcess";
            btn_addProcess.Size = new Size(116, 35);
            btn_addProcess.TabIndex = 10;
            btn_addProcess.Text = "Process\r\nExplorer";
            btn_addProcess.UseVisualStyleBackColor = true;
            btn_addProcess.Click += btn_addProcess_Click;
            // 
            // listView_addedProcesses
            // 
            listView_addedProcesses.AutoArrange = false;
            listView_addedProcesses.BackColor = Color.FromArgb(15, 18, 21);
            listView_addedProcesses.BorderStyle = BorderStyle.FixedSingle;
            listView_addedProcesses.ContextMenuStrip = contextMenuStripAddedList;
            listView_addedProcesses.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listView_addedProcesses.ForeColor = Color.White;
            listView_addedProcesses.HeaderStyle = ColumnHeaderStyle.None;
            listView_addedProcesses.LargeImageList = imageListAddedProcs;
            listView_addedProcesses.Location = new Point(9, 112);
            listView_addedProcesses.Margin = new Padding(0);
            listView_addedProcesses.Name = "listView_addedProcesses";
            listView_addedProcesses.Size = new Size(380, 80);
            listView_addedProcesses.SmallImageList = imageListAddedProcs;
            listView_addedProcesses.StateImageList = imageListAddedProcs;
            listView_addedProcesses.TabIndex = 12;
            listView_addedProcesses.TileSize = new Size(180, 36);
            listView_addedProcesses.UseCompatibleStateImageBehavior = false;
            listView_addedProcesses.View = View.Tile;
            // 
            // contextMenuStripAddedList
            // 
            contextMenuStripAddedList.BackColor = Color.FromArgb(36, 38, 49);
            contextMenuStripAddedList.BackgroundImageLayout = ImageLayout.None;
            contextMenuStripAddedList.ForeColor = Color.White;
            contextMenuStripAddedList.Items.AddRange(new ToolStripItem[] { reloadToolStripMenuItem, deleteToolStripMenuItem, resolution1ToolStripMenuItem1, resolution2ToolStripMenuItem1, resolution3ToolStripMenuItem1, priority1ToolStripMenuItem, priority1ToolStripMenuItem1, priority3ToolStripMenuItem, priority4ToolStripMenuItem });
            contextMenuStripAddedList.Name = "contextMenuStripAddedList";
            contextMenuStripAddedList.RenderMode = ToolStripRenderMode.Professional;
            contextMenuStripAddedList.Size = new Size(140, 202);
            // 
            // reloadToolStripMenuItem
            // 
            reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            reloadToolStripMenuItem.Size = new Size(139, 22);
            reloadToolStripMenuItem.Text = "Reload";
            reloadToolStripMenuItem.Click += reloadToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(139, 22);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // resolution1ToolStripMenuItem1
            // 
            resolution1ToolStripMenuItem1.Name = "resolution1ToolStripMenuItem1";
            resolution1ToolStripMenuItem1.Size = new Size(139, 22);
            resolution1ToolStripMenuItem1.Text = "Resolution 1";
            resolution1ToolStripMenuItem1.Click += resolution1ToolStripMenuItem1_Click;
            // 
            // resolution2ToolStripMenuItem1
            // 
            resolution2ToolStripMenuItem1.Name = "resolution2ToolStripMenuItem1";
            resolution2ToolStripMenuItem1.Size = new Size(139, 22);
            resolution2ToolStripMenuItem1.Text = "Resolution 2";
            resolution2ToolStripMenuItem1.Click += resolution2ToolStripMenuItem1_Click;
            // 
            // resolution3ToolStripMenuItem1
            // 
            resolution3ToolStripMenuItem1.Name = "resolution3ToolStripMenuItem1";
            resolution3ToolStripMenuItem1.Size = new Size(139, 22);
            resolution3ToolStripMenuItem1.Text = "Resolution 3";
            resolution3ToolStripMenuItem1.Click += resolution3ToolStripMenuItem1_Click;
            // 
            // priority1ToolStripMenuItem
            // 
            priority1ToolStripMenuItem.Name = "priority1ToolStripMenuItem";
            priority1ToolStripMenuItem.Size = new Size(139, 22);
            priority1ToolStripMenuItem.Text = "Priority #1";
            priority1ToolStripMenuItem.Click += priority1ToolStripMenuItem_Click;
            // 
            // priority1ToolStripMenuItem1
            // 
            priority1ToolStripMenuItem1.Name = "priority1ToolStripMenuItem1";
            priority1ToolStripMenuItem1.Size = new Size(139, 22);
            priority1ToolStripMenuItem1.Text = "Priority #2";
            priority1ToolStripMenuItem1.Click += priority1ToolStripMenuItem1_Click;
            // 
            // priority3ToolStripMenuItem
            // 
            priority3ToolStripMenuItem.Name = "priority3ToolStripMenuItem";
            priority3ToolStripMenuItem.Size = new Size(139, 22);
            priority3ToolStripMenuItem.Text = "Priority #3";
            priority3ToolStripMenuItem.Click += priority3ToolStripMenuItem_Click;
            // 
            // priority4ToolStripMenuItem
            // 
            priority4ToolStripMenuItem.Name = "priority4ToolStripMenuItem";
            priority4ToolStripMenuItem.Size = new Size(139, 22);
            priority4ToolStripMenuItem.Text = "Priority #4";
            priority4ToolStripMenuItem.Click += priority4ToolStripMenuItem_Click;
            // 
            // imageListAddedProcs
            // 
            imageListAddedProcs.ColorDepth = ColorDepth.Depth32Bit;
            imageListAddedProcs.ImageSize = new Size(32, 32);
            imageListAddedProcs.TransparentColor = Color.Transparent;
            // 
            // ResolutionChanger
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 18, 21);
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(398, 241);
            Controls.Add(listView_addedProcesses);
            Controls.Add(btn_addProcess);
            Controls.Add(applyRes3Button);
            Controls.Add(availableResolutionsCB3);
            Controls.Add(resolution3Label);
            Controls.Add(applyRes2Button);
            Controls.Add(applyRes1Button);
            Controls.Add(availableResolutionsCB2);
            Controls.Add(resolution2Label);
            Controls.Add(applyButton);
            Controls.Add(resolution1Label);
            Controls.Add(availableResolutionsCB);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ResolutionChanger";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Resolution Changer";
            FormClosing += ResolutionChanger_FormClosing;
            Load += ResolutionChanger_Load;
            contextMenuStrip.ResumeLayout(false);
            contextMenuStripAddedList.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox availableResolutionsCB;
        private Label resolution1Label;
        private Button applyButton;
        private Label resolution2Label;
        private ComboBox availableResolutionsCB2;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem runOnStartupToolStripMenuItem;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Button applyRes1Button;
        private Button applyRes2Button;
        private Label resolution3Label;
        private ComboBox availableResolutionsCB3;
        private Button applyRes3Button;
        private ToolStripMenuItem resolution1ToolStripMenuItem;
        private ToolStripMenuItem resolution2ToolStripMenuItem;
        private ToolStripMenuItem resolution3ToolStripMenuItem;
        private ToolStripMenuItem clearRegistryToolStripMenuItem;
        private Button btn_addProcess;
        private ListView listView_addedProcesses;
        private ImageList imageListAddedProcs;
        private ContextMenuStrip contextMenuStripAddedList;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem resolution1ToolStripMenuItem1;
        private ToolStripMenuItem resolution2ToolStripMenuItem1;
        private ToolStripMenuItem resolution3ToolStripMenuItem1;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem priority1ToolStripMenuItem;
        private ToolStripMenuItem priority1ToolStripMenuItem1;
        private ToolStripMenuItem priority3ToolStripMenuItem;
        private ToolStripMenuItem priority4ToolStripMenuItem;
    }
}
