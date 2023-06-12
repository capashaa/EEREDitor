namespace EEditor
{
    partial class MyWorlds
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyWorlds));
            this.listView1 = new System.Windows.Forms.ListView();
            this.NameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SizecolumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RoomIDColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ResetButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LoadWorldButton = new System.Windows.Forms.Button();
            this.panelBg = new System.Windows.Forms.Panel();
            this.cbShowMinimap = new System.Windows.Forms.CheckBox();
            this.cbDB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelBg.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumnHeader,
            this.SizecolumnHeader,
            this.RoomIDColumnHeader});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(359, 334);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // NameColumnHeader
            // 
            this.NameColumnHeader.Text = "Name";
            this.NameColumnHeader.Width = 222;
            // 
            // SizecolumnHeader
            // 
            this.SizecolumnHeader.Text = "Size";
            this.SizecolumnHeader.Width = 131;
            // 
            // RoomIDColumnHeader
            // 
            this.RoomIDColumnHeader.Text = "RoomID";
            this.RoomIDColumnHeader.Width = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 384);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(359, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(12, 352);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 2;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(307, 138);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // LoadWorldButton
            // 
            this.LoadWorldButton.Location = new System.Drawing.Point(93, 352);
            this.LoadWorldButton.Name = "LoadWorldButton";
            this.LoadWorldButton.Size = new System.Drawing.Size(75, 23);
            this.LoadWorldButton.TabIndex = 4;
            this.LoadWorldButton.Text = "Load World";
            this.LoadWorldButton.UseVisualStyleBackColor = true;
            this.LoadWorldButton.Click += new System.EventHandler(this.LoadWorldButton_Click);
            // 
            // panelBg
            // 
            this.panelBg.AutoScroll = true;
            this.panelBg.Controls.Add(this.pictureBox1);
            this.panelBg.Location = new System.Drawing.Point(377, 12);
            this.panelBg.Name = "panelBg";
            this.panelBg.Size = new System.Drawing.Size(313, 144);
            this.panelBg.TabIndex = 5;
            // 
            // cbShowMinimap
            // 
            this.cbShowMinimap.AutoSize = true;
            this.cbShowMinimap.Location = new System.Drawing.Point(174, 356);
            this.cbShowMinimap.Name = "cbShowMinimap";
            this.cbShowMinimap.Size = new System.Drawing.Size(95, 17);
            this.cbShowMinimap.TabIndex = 6;
            this.cbShowMinimap.Text = "Show Minimap";
            this.cbShowMinimap.UseVisualStyleBackColor = true;
            // 
            // cbDB
            // 
            this.cbDB.AutoSize = true;
            this.cbDB.Location = new System.Drawing.Point(275, 356);
            this.cbDB.Name = "cbDB";
            this.cbDB.Size = new System.Drawing.Size(122, 17);
            this.cbDB.TabIndex = 7;
            this.cbDB.Text = "Read from database";
            this.cbDB.UseVisualStyleBackColor = true;
            this.cbDB.CheckedChanged += new System.EventHandler(this.cbDB_CheckedChanged);
            // 
            // MyWorlds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 419);
            this.Controls.Add(this.cbDB);
            this.Controls.Add(this.cbShowMinimap);
            this.Controls.Add(this.panelBg);
            this.Controls.Add(this.LoadWorldButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MyWorlds";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My Own Worlds";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MyWorlds_FormClosing);
            this.Load += new System.EventHandler(this.MyWorlds_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelBg.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader NameColumnHeader;
        private System.Windows.Forms.ColumnHeader SizecolumnHeader;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ColumnHeader RoomIDColumnHeader;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button LoadWorldButton;
        private System.Windows.Forms.Panel panelBg;
        private System.Windows.Forms.CheckBox cbShowMinimap;
        private System.Windows.Forms.CheckBox cbDB;
    }
}