namespace EEditor
{
    partial class ToolpickerSettings
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
            this.SettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.SettingsFGCheckBox = new System.Windows.Forms.CheckBox();
            this.SettingsBGCheckBox = new System.Windows.Forms.CheckBox();
            this.SelectColorButton = new System.Windows.Forms.Button();
            this.SettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingsGroupBox
            // 
            this.SettingsGroupBox.Controls.Add(this.SettingsBGCheckBox);
            this.SettingsGroupBox.Controls.Add(this.SettingsFGCheckBox);
            this.SettingsGroupBox.Location = new System.Drawing.Point(12, 12);
            this.SettingsGroupBox.Name = "SettingsGroupBox";
            this.SettingsGroupBox.Size = new System.Drawing.Size(187, 72);
            this.SettingsGroupBox.TabIndex = 0;
            this.SettingsGroupBox.TabStop = false;
            this.SettingsGroupBox.Text = "Choose Blocks";
            // 
            // SettingsFGCheckBox
            // 
            this.SettingsFGCheckBox.AutoSize = true;
            this.SettingsFGCheckBox.Checked = true;
            this.SettingsFGCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SettingsFGCheckBox.Location = new System.Drawing.Point(6, 19);
            this.SettingsFGCheckBox.Name = "SettingsFGCheckBox";
            this.SettingsFGCheckBox.Size = new System.Drawing.Size(80, 17);
            this.SettingsFGCheckBox.TabIndex = 0;
            this.SettingsFGCheckBox.Text = "Foreground";
            this.SettingsFGCheckBox.UseVisualStyleBackColor = true;
            this.SettingsFGCheckBox.CheckedChanged += new System.EventHandler(this.SettingsFGCheckBox_CheckedChanged);
            // 
            // SettingsBGCheckBox
            // 
            this.SettingsBGCheckBox.AutoSize = true;
            this.SettingsBGCheckBox.Checked = true;
            this.SettingsBGCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SettingsBGCheckBox.Location = new System.Drawing.Point(6, 42);
            this.SettingsBGCheckBox.Name = "SettingsBGCheckBox";
            this.SettingsBGCheckBox.Size = new System.Drawing.Size(84, 17);
            this.SettingsBGCheckBox.TabIndex = 1;
            this.SettingsBGCheckBox.Text = "Background";
            this.SettingsBGCheckBox.UseVisualStyleBackColor = true;
            this.SettingsBGCheckBox.CheckedChanged += new System.EventHandler(this.SettingsBGCheckBox_CheckedChanged);
            // 
            // SelectColorButton
            // 
            this.SelectColorButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SelectColorButton.Location = new System.Drawing.Point(12, 90);
            this.SelectColorButton.Name = "SelectColorButton";
            this.SelectColorButton.Size = new System.Drawing.Size(86, 23);
            this.SelectColorButton.TabIndex = 1;
            this.SelectColorButton.Text = "Select Color";
            this.SelectColorButton.UseVisualStyleBackColor = true;
            this.SelectColorButton.Click += new System.EventHandler(this.SelectColorButton_Click);
            // 
            // ToolpickerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(211, 127);
            this.Controls.Add(this.SelectColorButton);
            this.Controls.Add(this.SettingsGroupBox);
            this.Name = "ToolpickerSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Color Settings";
            this.Load += new System.EventHandler(this.ToolpickerSettings_Load);
            this.SettingsGroupBox.ResumeLayout(false);
            this.SettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SettingsGroupBox;
        private System.Windows.Forms.CheckBox SettingsBGCheckBox;
        private System.Windows.Forms.CheckBox SettingsFGCheckBox;
        private System.Windows.Forms.Button SelectColorButton;
    }
}