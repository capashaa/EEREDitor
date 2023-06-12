namespace EEditor
{
    partial class WorldArchiveMenu
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
            if (disposing && (components != null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorldArchiveMenu));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadUserArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbLoadUserArchiveUsername = new System.Windows.Forms.ToolStripTextBox();
            this.btnLoadUserArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRefreshArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.lvWorldArchive = new System.Windows.Forms.ListView();
            this.lvHeaderWorldName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvHeaderRevision = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.statusStrip.Location = new System.Drawing.Point(0, 425);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(306, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadUserArchiveToolStripMenuItem,
            this.btnRefreshArchive,
            this.toolStripSeparator1,
            this.btnAbout});
            this.toolStripDropDownButton1.Image = global::EEditor.Properties.Resources.settings;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 20);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // loadUserArchiveToolStripMenuItem
            // 
            this.loadUserArchiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbLoadUserArchiveUsername,
            this.btnLoadUserArchive});
            this.loadUserArchiveToolStripMenuItem.Image = global::EEditor.Properties.Resources.search;
            this.loadUserArchiveToolStripMenuItem.Name = "loadUserArchiveToolStripMenuItem";
            this.loadUserArchiveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadUserArchiveToolStripMenuItem.Text = "Load";
            // 
            // tbLoadUserArchiveUsername
            // 
            this.tbLoadUserArchiveUsername.AutoToolTip = true;
            this.tbLoadUserArchiveUsername.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbLoadUserArchiveUsername.MaxLength = 20;
            this.tbLoadUserArchiveUsername.Name = "tbLoadUserArchiveUsername";
            this.tbLoadUserArchiveUsername.Size = new System.Drawing.Size(100, 23);
            this.tbLoadUserArchiveUsername.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbLoadUserArchiveUsername.ToolTipText = "Username";
            // 
            // btnLoadUserArchive
            // 
            this.btnLoadUserArchive.Name = "btnLoadUserArchive";
            this.btnLoadUserArchive.Size = new System.Drawing.Size(160, 22);
            this.btnLoadUserArchive.Text = "Load Archive";
            this.btnLoadUserArchive.Click += new System.EventHandler(this.btnLoadUserArchive_Click);
            // 
            // btnRefreshArchive
            // 
            this.btnRefreshArchive.Image = global::EEditor.Properties.Resources.rotate;
            this.btnRefreshArchive.Name = "btnRefreshArchive";
            this.btnRefreshArchive.Size = new System.Drawing.Size(152, 22);
            this.btnRefreshArchive.Text = "Refresh";
            this.btnRefreshArchive.Click += new System.EventHandler(this.RefreshArchive);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // btnAbout
            // 
            this.btnAbout.Image = global::EEditor.Properties.Resources.about;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(152, 22);
            this.btnAbout.Text = "About";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // lvWorldArchive
            // 
            this.lvWorldArchive.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvHeaderWorldName,
            this.lvHeaderRevision});
            this.lvWorldArchive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvWorldArchive.FullRowSelect = true;
            this.lvWorldArchive.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvWorldArchive.Location = new System.Drawing.Point(0, 0);
            this.lvWorldArchive.MultiSelect = false;
            this.lvWorldArchive.Name = "lvWorldArchive";
            this.lvWorldArchive.Size = new System.Drawing.Size(306, 425);
            this.lvWorldArchive.TabIndex = 1;
            this.lvWorldArchive.UseCompatibleStateImageBehavior = false;
            this.lvWorldArchive.View = System.Windows.Forms.View.Details;
            this.lvWorldArchive.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LoadWorld);
            // 
            // lvHeaderWorldName
            // 
            this.lvHeaderWorldName.Text = "World Name";
            this.lvHeaderWorldName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvHeaderWorldName.Width = 162;
            // 
            // lvHeaderRevision
            // 
            this.lvHeaderRevision.Text = "Revision";
            this.lvHeaderRevision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvHeaderRevision.Width = 53;
            // 
            // WorldArchiveMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 447);
            this.Controls.Add(this.lvWorldArchive);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 486);
            this.MinimumSize = new System.Drawing.Size(300, 110);
            this.Name = "WorldArchiveMenu";
            this.Text = "World Archive";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OnLoad);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem loadUserArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox tbLoadUserArchiveUsername;
        private System.Windows.Forms.ToolStripMenuItem btnLoadUserArchive;
        private System.Windows.Forms.ToolStripMenuItem btnRefreshArchive;
        private System.Windows.Forms.ListView lvWorldArchive;
        private System.Windows.Forms.ColumnHeader lvHeaderWorldName;
        private System.Windows.Forms.ColumnHeader lvHeaderRevision;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnAbout;
    }
}