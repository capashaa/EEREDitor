namespace EEditor
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.HomepageButton = new System.Windows.Forms.Button();
            this.AboutLabel = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // HomepageButton
            // 
            this.HomepageButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HomepageButton.Location = new System.Drawing.Point(97, 67);
            this.HomepageButton.Name = "HomepageButton";
            this.HomepageButton.Size = new System.Drawing.Size(97, 23);
            this.HomepageButton.TabIndex = 1;
            this.HomepageButton.Text = "Homepage";
            this.HomepageButton.UseVisualStyleBackColor = true;
            // 
            // AboutLabel
            // 
            this.AboutLabel.Location = new System.Drawing.Point(7, 16);
            this.AboutLabel.Name = "AboutLabel";
            this.AboutLabel.Size = new System.Drawing.Size(305, 36);
            this.AboutLabel.TabIndex = 0;
            this.AboutLabel.Text = "EEditor is an offline Everybody Edits level editor, \r\nmade to simplify level crea" +
    "tion and manipulation.";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.AboutLabel);
            this.groupBox4.Controls.Add(this.HomepageButton);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(325, 101);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "General info";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 124);
            this.Controls.Add(this.groupBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About EEditor";
            this.Load += new System.EventHandler(this.About_Load);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label AboutLabel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button HomepageButton;
    }
}