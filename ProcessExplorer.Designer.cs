namespace Resolution_Changer
{
    partial class ProcessExplorer
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessExplorer));
            listView_processExplorer = new ListView();
            columnPrName = new ColumnHeader();
            imageListProcesses = new ImageList(components);
            btn_addProcessToList = new Button();
            btn_refreshList = new Button();
            label_addedProcess = new Label();
            SuspendLayout();
            // 
            // listView_processExplorer
            // 
            listView_processExplorer.AutoArrange = false;
            listView_processExplorer.BackColor = Color.FromArgb(52, 54, 69);
            listView_processExplorer.BorderStyle = BorderStyle.None;
            listView_processExplorer.Columns.AddRange(new ColumnHeader[] { columnPrName });
            listView_processExplorer.ForeColor = Color.White;
            listView_processExplorer.HeaderStyle = ColumnHeaderStyle.None;
            listView_processExplorer.LargeImageList = imageListProcesses;
            listView_processExplorer.Location = new Point(12, 12);
            listView_processExplorer.Name = "listView_processExplorer";
            listView_processExplorer.Size = new Size(460, 306);
            listView_processExplorer.SmallImageList = imageListProcesses;
            listView_processExplorer.StateImageList = imageListProcesses;
            listView_processExplorer.TabIndex = 0;
            listView_processExplorer.UseCompatibleStateImageBehavior = false;
            listView_processExplorer.View = View.Details;
            // 
            // columnPrName
            // 
            columnPrName.Text = "Processes";
            columnPrName.Width = 320;
            // 
            // imageListProcesses
            // 
            imageListProcesses.ColorDepth = ColorDepth.Depth32Bit;
            imageListProcesses.ImageSize = new Size(32, 32);
            imageListProcesses.TransparentColor = Color.Transparent;
            // 
            // btn_addProcessToList
            // 
            btn_addProcessToList.BackColor = Color.FromArgb(52, 54, 69);
            btn_addProcessToList.FlatAppearance.BorderSize = 0;
            btn_addProcessToList.FlatStyle = FlatStyle.Flat;
            btn_addProcessToList.ForeColor = Color.White;
            btn_addProcessToList.Location = new Point(205, 326);
            btn_addProcessToList.Name = "btn_addProcessToList";
            btn_addProcessToList.Size = new Size(75, 23);
            btn_addProcessToList.TabIndex = 1;
            btn_addProcessToList.Text = "Add to list";
            btn_addProcessToList.UseVisualStyleBackColor = false;
            btn_addProcessToList.Click += btn_addProcessToList_Click;
            // 
            // btn_refreshList
            // 
            btn_refreshList.BackColor = Color.FromArgb(52, 54, 69);
            btn_refreshList.FlatAppearance.BorderSize = 0;
            btn_refreshList.FlatStyle = FlatStyle.Flat;
            btn_refreshList.ForeColor = Color.White;
            btn_refreshList.Location = new Point(397, 326);
            btn_refreshList.Name = "btn_refreshList";
            btn_refreshList.Size = new Size(75, 23);
            btn_refreshList.TabIndex = 2;
            btn_refreshList.Text = "Refresh";
            btn_refreshList.UseVisualStyleBackColor = false;
            btn_refreshList.Click += btn_refreshList_Click;
            // 
            // label_addedProcess
            // 
            label_addedProcess.AutoSize = true;
            label_addedProcess.ForeColor = Color.White;
            label_addedProcess.Location = new Point(12, 330);
            label_addedProcess.Name = "label_addedProcess";
            label_addedProcess.Size = new Size(0, 15);
            label_addedProcess.TabIndex = 3;
            // 
            // ProcessExplorer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 18, 21);
            ClientSize = new Size(484, 361);
            Controls.Add(label_addedProcess);
            Controls.Add(btn_refreshList);
            Controls.Add(btn_addProcessToList);
            Controls.Add(listView_processExplorer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ProcessExplorer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ProcessExplorer";
            Load += ProcessExplorer_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView_processExplorer;
        private Button btn_addProcessToList;
        private ImageList imageListProcesses;
        private ColumnHeader columnPrName;
        private Button btn_refreshList;
        private Label label_addedProcess;
    }
}