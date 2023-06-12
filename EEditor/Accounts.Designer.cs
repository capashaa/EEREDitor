namespace EEditor
{
    partial class Accounts
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.accountListBox = new System.Windows.Forms.ListBox();
            this.removeAccount = new System.Windows.Forms.Button();
            this.addAccount = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.reloadPacks = new System.Windows.Forms.Button();
            this.saveAccount = new System.Windows.Forms.Button();
            this.loginField2 = new System.Windows.Forms.TextBox();
            this.loginField1 = new System.Windows.Forms.TextBox();
            this.loginLabel2 = new System.Windows.Forms.Label();
            this.loginLabel1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.accountListBox);
            this.groupBox1.Controls.Add(this.removeAccount);
            this.groupBox1.Controls.Add(this.addAccount);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 301);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Accounts";
            // 
            // accountListBox
            // 
            this.accountListBox.FormattingEnabled = true;
            this.accountListBox.Location = new System.Drawing.Point(6, 19);
            this.accountListBox.Name = "accountListBox";
            this.accountListBox.Size = new System.Drawing.Size(179, 225);
            this.accountListBox.TabIndex = 3;
            this.accountListBox.SelectedIndexChanged += new System.EventHandler(this.accountListBox_SelectedIndexChanged);
            // 
            // removeAccount
            // 
            this.removeAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeAccount.Image = global::EEditor.Properties.Resources.remove;
            this.removeAccount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.removeAccount.Location = new System.Drawing.Point(95, 255);
            this.removeAccount.Name = "removeAccount";
            this.removeAccount.Size = new System.Drawing.Size(90, 35);
            this.removeAccount.TabIndex = 2;
            this.removeAccount.Text = "     Remove";
            this.removeAccount.UseVisualStyleBackColor = true;
            this.removeAccount.Click += new System.EventHandler(this.removeAccount_Click);
            // 
            // addAccount
            // 
            this.addAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addAccount.Image = global::EEditor.Properties.Resources.createnew;
            this.addAccount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addAccount.Location = new System.Drawing.Point(6, 255);
            this.addAccount.Name = "addAccount";
            this.addAccount.Size = new System.Drawing.Size(90, 35);
            this.addAccount.TabIndex = 1;
            this.addAccount.Text = "Create";
            this.addAccount.UseVisualStyleBackColor = true;
            this.addAccount.Click += new System.EventHandler(this.addAccount_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.reloadPacks);
            this.groupBox2.Controls.Add(this.saveAccount);
            this.groupBox2.Controls.Add(this.loginLabel1);
            this.groupBox2.Controls.Add(this.loginLabel2);
            this.groupBox2.Controls.Add(this.loginField2);
            this.groupBox2.Controls.Add(this.loginField1);
            this.groupBox2.Location = new System.Drawing.Point(209, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 138);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Details";
            // 
            // reloadPacks
            // 
            this.reloadPacks.Image = global::EEditor.Properties.Resources.rotate;
            this.reloadPacks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.reloadPacks.Location = new System.Drawing.Point(104, 96);
            this.reloadPacks.Name = "reloadPacks";
            this.reloadPacks.Size = new System.Drawing.Size(90, 35);
            this.reloadPacks.TabIndex = 10;
            this.reloadPacks.Text = "Reload";
            this.reloadPacks.UseVisualStyleBackColor = true;
            this.reloadPacks.Click += new System.EventHandler(this.reloadPacks_Click);
            // 
            // saveAccount
            // 
            this.saveAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveAccount.Image = global::EEditor.Properties.Resources.save;
            this.saveAccount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveAccount.Location = new System.Drawing.Point(6, 96);
            this.saveAccount.Name = "saveAccount";
            this.saveAccount.Size = new System.Drawing.Size(85, 35);
            this.saveAccount.TabIndex = 9;
            this.saveAccount.Text = "Save";
            this.saveAccount.UseVisualStyleBackColor = true;
            this.saveAccount.Click += new System.EventHandler(this.saveAccount_Click);
            // 
            // loginField2
            // 
            this.loginField2.Location = new System.Drawing.Point(69, 60);
            this.loginField2.Name = "loginField2";
            this.loginField2.Size = new System.Drawing.Size(125, 20);
            this.loginField2.TabIndex = 8;
            this.loginField2.UseSystemPasswordChar = true;
            // 
            // loginField1
            // 
            this.loginField1.Location = new System.Drawing.Point(48, 31);
            this.loginField1.Name = "loginField1";
            this.loginField1.Size = new System.Drawing.Size(146, 20);
            this.loginField1.TabIndex = 7;
            // 
            // loginLabel2
            // 
            this.loginLabel2.AutoSize = true;
            this.loginLabel2.Location = new System.Drawing.Point(7, 63);
            this.loginLabel2.Name = "loginLabel2";
            this.loginLabel2.Size = new System.Drawing.Size(56, 13);
            this.loginLabel2.TabIndex = 6;
            this.loginLabel2.Text = "Password:";
            // 
            // loginLabel1
            // 
            this.loginLabel1.AutoSize = true;
            this.loginLabel1.Location = new System.Drawing.Point(7, 34);
            this.loginLabel1.Name = "loginLabel1";
            this.loginLabel1.Size = new System.Drawing.Size(35, 13);
            this.loginLabel1.TabIndex = 5;
            this.loginLabel1.Text = "Email:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 327);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(583, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // Accounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 349);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Accounts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Accounts_FormClosed);
            this.Load += new System.EventHandler(this.Accounts_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button removeAccount;
        private System.Windows.Forms.Button addAccount;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox loginField2;
        private System.Windows.Forms.TextBox loginField1;
        private System.Windows.Forms.Label loginLabel2;
        private System.Windows.Forms.Label loginLabel1;
        private System.Windows.Forms.Button reloadPacks;
        private System.Windows.Forms.Button saveAccount;
        private System.Windows.Forms.ListBox accountListBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}