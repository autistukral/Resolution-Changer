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
            label1 = new Label();
            availableResolutionsCB2 = new ComboBox();
            notifyIcon = new NotifyIcon(components);
            contextMenuStrip = new ContextMenuStrip(components);
            runOnStartupToolStripMenuItem = new ToolStripMenuItem();
            showToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            applyRes1Button = new Button();
            applyRes2Button = new Button();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // availableResolutionsCB
            // 
            availableResolutionsCB.BackColor = Color.FromArgb(52, 54, 69);
            availableResolutionsCB.DropDownStyle = ComboBoxStyle.DropDownList;
            availableResolutionsCB.FlatStyle = FlatStyle.Flat;
            availableResolutionsCB.ForeColor = Color.White;
            availableResolutionsCB.FormattingEnabled = true;
            availableResolutionsCB.Location = new Point(93, 39);
            availableResolutionsCB.Name = "availableResolutionsCB";
            availableResolutionsCB.Size = new Size(120, 23);
            availableResolutionsCB.TabIndex = 0;
            // 
            // resolution1Label
            // 
            resolution1Label.AutoSize = true;
            resolution1Label.Location = new Point(12, 45);
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
            applyButton.Location = new Point(104, 130);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(120, 40);
            applyButton.TabIndex = 2;
            applyButton.Text = "Apply";
            applyButton.UseVisualStyleBackColor = false;
            applyButton.Click += applyButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 79);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 3;
            label1.Text = "Resolution 2:";
            // 
            // availableResolutionsCB2
            // 
            availableResolutionsCB2.BackColor = Color.FromArgb(52, 54, 69);
            availableResolutionsCB2.DropDownStyle = ComboBoxStyle.DropDownList;
            availableResolutionsCB2.FlatStyle = FlatStyle.Flat;
            availableResolutionsCB2.ForeColor = Color.White;
            availableResolutionsCB2.FormattingEnabled = true;
            availableResolutionsCB2.Location = new Point(93, 73);
            availableResolutionsCB2.Name = "availableResolutionsCB2";
            availableResolutionsCB2.Size = new Size(120, 23);
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
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { runOnStartupToolStripMenuItem, showToolStripMenuItem, exitToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(153, 70);
            // 
            // runOnStartupToolStripMenuItem
            // 
            runOnStartupToolStripMenuItem.Name = "runOnStartupToolStripMenuItem";
            runOnStartupToolStripMenuItem.Size = new Size(152, 22);
            runOnStartupToolStripMenuItem.Text = "Run on startup";
            runOnStartupToolStripMenuItem.Click += runOnStartupToolStripMenuItem_Click;
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
            applyRes1Button.Location = new Point(219, 39);
            applyRes1Button.Name = "applyRes1Button";
            applyRes1Button.Size = new Size(90, 23);
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
            applyRes2Button.Location = new Point(219, 73);
            applyRes2Button.Name = "applyRes2Button";
            applyRes2Button.Size = new Size(90, 23);
            applyRes2Button.TabIndex = 6;
            applyRes2Button.Text = "Ctrl+Shift+2";
            applyRes2Button.UseVisualStyleBackColor = false;
            applyRes2Button.Click += applyRes2Button_Click;
            // 
            // ResolutionChanger
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 18, 21);
            ClientSize = new Size(324, 182);
            Controls.Add(applyRes2Button);
            Controls.Add(applyRes1Button);
            Controls.Add(availableResolutionsCB2);
            Controls.Add(label1);
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
            FormClosing += MouseTrailsForm_FormClosing;
            Load += ResolutionChanger_Load;
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox availableResolutionsCB;
        private Label resolution1Label;
        private Button applyButton;
        private Label label1;
        private ComboBox availableResolutionsCB2;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem runOnStartupToolStripMenuItem;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Button applyRes1Button;
        private Button applyRes2Button;
    }
}
