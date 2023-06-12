namespace EEditor
{
    partial class EditArea
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
            // EditArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Name = "EditArea";
            this.Size = new System.Drawing.Size(379, 224);
            this.Load += new System.EventHandler(this.EditArea_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EditArea_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EditArea_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EditArea_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
