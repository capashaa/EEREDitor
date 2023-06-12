namespace EEditor
{
    partial class Label
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnColor = new System.Windows.Forms.Button();
            this.nupdWrap = new System.Windows.Forms.NumericUpDown();
            this.txtbText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupdWrap)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(328, 99);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(233, 141);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 0);
            this.panel1.TabIndex = 8;
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(259, 147);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(81, 23);
            this.btnColor.TabIndex = 7;
            this.btnColor.Text = "Color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // nupdWrap
            // 
            this.nupdWrap.Location = new System.Drawing.Point(259, 121);
            this.nupdWrap.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nupdWrap.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nupdWrap.Name = "nupdWrap";
            this.nupdWrap.Size = new System.Drawing.Size(81, 20);
            this.nupdWrap.TabIndex = 6;
            this.nupdWrap.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nupdWrap.ValueChanged += new System.EventHandler(this.nupdWrap_ValueChanged);
            // 
            // txtbText
            // 
            this.txtbText.Location = new System.Drawing.Point(12, 121);
            this.txtbText.Multiline = true;
            this.txtbText.Name = "txtbText";
            this.txtbText.Size = new System.Drawing.Size(241, 49);
            this.txtbText.TabIndex = 5;
            this.txtbText.TextChanged += new System.EventHandler(this.txtbText_TextChanged);
            // 
            // Label
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 199);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.nupdWrap);
            this.Controls.Add(this.txtbText);
            this.MaximumSize = new System.Drawing.Size(405, 238);
            this.MinimumSize = new System.Drawing.Size(405, 238);
            this.Name = "Label";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Label";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Label_FormClosing);
            this.Load += new System.EventHandler(this.Label_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupdWrap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.NumericUpDown nupdWrap;
        private System.Windows.Forms.TextBox txtbText;
    }
}