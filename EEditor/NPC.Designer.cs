namespace EEditor
{
    partial class NPC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NPC));
            this.Message1TextBox = new System.Windows.Forms.TextBox();
            this.Message2TextBox = new System.Windows.Forms.TextBox();
            this.Message3TextBox = new System.Windows.Forms.TextBox();
            this.NicknameTextBox = new System.Windows.Forms.TextBox();
            this.Message1Label = new System.Windows.Forms.Label();
            this.Message2Label = new System.Windows.Forms.Label();
            this.Message3Label = new System.Windows.Forms.Label();
            this.NicknameLabel = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // Message1TextBox
            // 
            this.Message1TextBox.Location = new System.Drawing.Point(18, 34);
            this.Message1TextBox.MaxLength = 80;
            this.Message1TextBox.Multiline = true;
            this.Message1TextBox.Name = "Message1TextBox";
            this.Message1TextBox.Size = new System.Drawing.Size(191, 43);
            this.Message1TextBox.TabIndex = 0;
            this.Message1TextBox.TextChanged += new System.EventHandler(this.MessageTextBox_TextChanged);
            // 
            // Message2TextBox
            // 
            this.Message2TextBox.Location = new System.Drawing.Point(18, 100);
            this.Message2TextBox.MaxLength = 80;
            this.Message2TextBox.Multiline = true;
            this.Message2TextBox.Name = "Message2TextBox";
            this.Message2TextBox.Size = new System.Drawing.Size(191, 43);
            this.Message2TextBox.TabIndex = 1;
            this.Message2TextBox.TextChanged += new System.EventHandler(this.MessageTextBox_TextChanged);
            // 
            // Message3TextBox
            // 
            this.Message3TextBox.Location = new System.Drawing.Point(18, 167);
            this.Message3TextBox.MaxLength = 80;
            this.Message3TextBox.Multiline = true;
            this.Message3TextBox.Name = "Message3TextBox";
            this.Message3TextBox.Size = new System.Drawing.Size(191, 43);
            this.Message3TextBox.TabIndex = 2;
            this.Message3TextBox.TextChanged += new System.EventHandler(this.MessageTextBox_TextChanged);
            // 
            // NicknameTextBox
            // 
            this.NicknameTextBox.Location = new System.Drawing.Point(18, 235);
            this.NicknameTextBox.MaxLength = 50;
            this.NicknameTextBox.Multiline = true;
            this.NicknameTextBox.Name = "NicknameTextBox";
            this.NicknameTextBox.Size = new System.Drawing.Size(191, 20);
            this.NicknameTextBox.TabIndex = 3;
            this.NicknameTextBox.TextChanged += new System.EventHandler(this.MessageTextBox_TextChanged);
            // 
            // Message1Label
            // 
            this.Message1Label.AutoSize = true;
            this.Message1Label.Location = new System.Drawing.Point(15, 18);
            this.Message1Label.Name = "Message1Label";
            this.Message1Label.Size = new System.Drawing.Size(66, 13);
            this.Message1Label.TabIndex = 4;
            this.Message1Label.Text = "Message #1";
            // 
            // Message2Label
            // 
            this.Message2Label.AutoSize = true;
            this.Message2Label.Location = new System.Drawing.Point(15, 84);
            this.Message2Label.Name = "Message2Label";
            this.Message2Label.Size = new System.Drawing.Size(66, 13);
            this.Message2Label.TabIndex = 5;
            this.Message2Label.Text = "Message #2";
            // 
            // Message3Label
            // 
            this.Message3Label.AutoSize = true;
            this.Message3Label.Location = new System.Drawing.Point(15, 151);
            this.Message3Label.Name = "Message3Label";
            this.Message3Label.Size = new System.Drawing.Size(66, 13);
            this.Message3Label.TabIndex = 6;
            this.Message3Label.Text = "Message #3";
            // 
            // NicknameLabel
            // 
            this.NicknameLabel.AutoSize = true;
            this.NicknameLabel.Location = new System.Drawing.Point(15, 213);
            this.NicknameLabel.Name = "NicknameLabel";
            this.NicknameLabel.Size = new System.Drawing.Size(55, 13);
            this.NicknameLabel.TabIndex = 7;
            this.NicknameLabel.Text = "Nickname";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Location = new System.Drawing.Point(228, 34);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(178, 221);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Picture";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Max";
            // 
            // NPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 275);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.NicknameLabel);
            this.Controls.Add(this.Message3Label);
            this.Controls.Add(this.Message2Label);
            this.Controls.Add(this.Message1Label);
            this.Controls.Add(this.NicknameTextBox);
            this.Controls.Add(this.Message3TextBox);
            this.Controls.Add(this.Message2TextBox);
            this.Controls.Add(this.Message1TextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NPC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NPC\'s";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NPC_FormClosing);
            this.Load += new System.EventHandler(this.NPC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Message1TextBox;
        private System.Windows.Forms.TextBox Message2TextBox;
        private System.Windows.Forms.TextBox Message3TextBox;
        private System.Windows.Forms.TextBox NicknameTextBox;
        private System.Windows.Forms.Label Message1Label;
        private System.Windows.Forms.Label Message2Label;
        private System.Windows.Forms.Label Message3Label;
        private System.Windows.Forms.Label NicknameLabel;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}