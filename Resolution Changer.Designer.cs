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
            availableResolutionsCB = new ComboBox();
            resolution1Label = new Label();
            applyButton = new Button();
            label1 = new Label();
            availableResolutionsCB2 = new ComboBox();
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
            availableResolutionsCB.Size = new Size(200, 23);
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
            applyButton.Location = new Point(200, 309);
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
            availableResolutionsCB2.Size = new Size(200, 23);
            availableResolutionsCB2.TabIndex = 4;
            // 
            // ResolutionChanger
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 18, 21);
            ClientSize = new Size(484, 361);
            Controls.Add(availableResolutionsCB2);
            Controls.Add(label1);
            Controls.Add(applyButton);
            Controls.Add(resolution1Label);
            Controls.Add(availableResolutionsCB);
            ForeColor = Color.White;
            Name = "ResolutionChanger";
            Text = "Resolution Changer";
            Load += ResolutionChanger_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox availableResolutionsCB;
        private Label resolution1Label;
        private Button applyButton;
        private Label label1;
        private ComboBox availableResolutionsCB2;
    }
}
