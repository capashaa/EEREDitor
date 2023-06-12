namespace EEditor
{
    partial class BluePrints
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SaveBPButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.labelYN = new System.Windows.Forms.Label();
            this.tBOwner = new System.Windows.Forms.TextBox();
            this.buttonImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // SaveBPButton
            // 
            this.SaveBPButton.Location = new System.Drawing.Point(212, 418);
            this.SaveBPButton.Name = "SaveBPButton";
            this.SaveBPButton.Size = new System.Drawing.Size(53, 23);
            this.SaveBPButton.TabIndex = 1;
            this.SaveBPButton.Text = "Save";
            this.SaveBPButton.UseVisualStyleBackColor = true;
            this.SaveBPButton.Click += new System.EventHandler(this.SaveBPButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(271, 418);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 2;
            this.LoadButton.Text = "Load File..";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // labelYN
            // 
            this.labelYN.AutoSize = true;
            this.labelYN.Location = new System.Drawing.Point(12, 423);
            this.labelYN.Name = "labelYN";
            this.labelYN.Size = new System.Drawing.Size(52, 13);
            this.labelYN.TabIndex = 3;
            this.labelYN.Text = "Made By:";
            // 
            // tBOwner
            // 
            this.tBOwner.Location = new System.Drawing.Point(81, 420);
            this.tBOwner.Name = "tBOwner";
            this.tBOwner.Size = new System.Drawing.Size(125, 20);
            this.tBOwner.TabIndex = 4;
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(352, 418);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(60, 23);
            this.buttonImport.TabIndex = 7;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // BluePrints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 450);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.tBOwner);
            this.Controls.Add(this.labelYN);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.SaveBPButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "BluePrints";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BluePrints";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BluePrints_FormClosing);
            this.Load += new System.EventHandler(this.BluePrints_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button SaveBPButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Label labelYN;
        private System.Windows.Forms.TextBox tBOwner;
        private System.Windows.Forms.Button buttonImport;
    }
}