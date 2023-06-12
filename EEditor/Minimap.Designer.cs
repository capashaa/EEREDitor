namespace EEditor
{
    partial class Minimap
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Minimap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Minimap";
            this.Size = new System.Drawing.Size(27, 24);
            this.Load += new System.EventHandler(this.Minimap_Load);
            this.Click += new System.EventHandler(this.Minimap_Click);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Minimap_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Minimap_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Minimap_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Minimap_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
