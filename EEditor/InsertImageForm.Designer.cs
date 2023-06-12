namespace EEditor
{
    partial class InsertImageForm
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
            this.loadImageButton = new System.Windows.Forms.Button();
            this.checkBoxBlocks = new System.Windows.Forms.CheckBox();
            this.checkBoxBackground = new System.Windows.Forms.CheckBox();
            this.CreateImagegroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ActionBlockscheckBox = new System.Windows.Forms.CheckBox();
            this.MorphablecheckBox = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.GeneratorButton = new System.Windows.Forms.Button();
            this.CreateImagegroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadImageButton
            // 
            this.loadImageButton.Image = global::EEditor.Properties.Resources.open;
            this.loadImageButton.Location = new System.Drawing.Point(12, 159);
            this.loadImageButton.Name = "loadImageButton";
            this.loadImageButton.Size = new System.Drawing.Size(33, 27);
            this.loadImageButton.TabIndex = 1;
            this.loadImageButton.UseVisualStyleBackColor = true;
            this.loadImageButton.Click += new System.EventHandler(this.loadImageButton_Click);
            // 
            // checkBoxBlocks
            // 
            this.checkBoxBlocks.AutoSize = true;
            this.checkBoxBlocks.Checked = true;
            this.checkBoxBlocks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBlocks.Location = new System.Drawing.Point(28, 42);
            this.checkBoxBlocks.Name = "checkBoxBlocks";
            this.checkBoxBlocks.Size = new System.Drawing.Size(58, 17);
            this.checkBoxBlocks.TabIndex = 5;
            this.checkBoxBlocks.Text = "Blocks";
            this.checkBoxBlocks.UseVisualStyleBackColor = true;
            this.checkBoxBlocks.CheckedChanged += new System.EventHandler(this.checkBoxBlocks_CheckedChanged);
            // 
            // checkBoxBackground
            // 
            this.checkBoxBackground.AutoSize = true;
            this.checkBoxBackground.Checked = true;
            this.checkBoxBackground.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBackground.Location = new System.Drawing.Point(28, 19);
            this.checkBoxBackground.Name = "checkBoxBackground";
            this.checkBoxBackground.Size = new System.Drawing.Size(84, 17);
            this.checkBoxBackground.TabIndex = 6;
            this.checkBoxBackground.Text = "Background";
            this.checkBoxBackground.UseVisualStyleBackColor = true;
            this.checkBoxBackground.CheckedChanged += new System.EventHandler(this.checkBoxBackground_CheckedChanged);
            // 
            // CreateImagegroupBox
            // 
            this.CreateImagegroupBox.Controls.Add(this.label1);
            this.CreateImagegroupBox.Controls.Add(this.ActionBlockscheckBox);
            this.CreateImagegroupBox.Controls.Add(this.MorphablecheckBox);
            this.CreateImagegroupBox.Controls.Add(this.checkBoxBackground);
            this.CreateImagegroupBox.Controls.Add(this.checkBoxBlocks);
            this.CreateImagegroupBox.Location = new System.Drawing.Point(12, 12);
            this.CreateImagegroupBox.Name = "CreateImagegroupBox";
            this.CreateImagegroupBox.Size = new System.Drawing.Size(157, 141);
            this.CreateImagegroupBox.TabIndex = 7;
            this.CreateImagegroupBox.TabStop = false;
            this.CreateImagegroupBox.Text = "Create image using";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Special Blocks";
            // 
            // ActionBlockscheckBox
            // 
            this.ActionBlockscheckBox.AutoSize = true;
            this.ActionBlockscheckBox.Location = new System.Drawing.Point(28, 111);
            this.ActionBlockscheckBox.Name = "ActionBlockscheckBox";
            this.ActionBlockscheckBox.Size = new System.Drawing.Size(91, 17);
            this.ActionBlockscheckBox.TabIndex = 10;
            this.ActionBlockscheckBox.Text = "Action Blocks";
            this.ActionBlockscheckBox.UseVisualStyleBackColor = true;
            this.ActionBlockscheckBox.CheckedChanged += new System.EventHandler(this.ActionBlockscheckBox_CheckedChanged);
            // 
            // MorphablecheckBox
            // 
            this.MorphablecheckBox.AutoSize = true;
            this.MorphablecheckBox.Location = new System.Drawing.Point(28, 88);
            this.MorphablecheckBox.Name = "MorphablecheckBox";
            this.MorphablecheckBox.Size = new System.Drawing.Size(76, 17);
            this.MorphablecheckBox.TabIndex = 9;
            this.MorphablecheckBox.Text = "Morphable";
            this.MorphablecheckBox.UseVisualStyleBackColor = true;
            this.MorphablecheckBox.CheckedChanged += new System.EventHandler(this.MorphablecheckBox_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 192);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(157, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // GeneratorButton
            // 
            this.GeneratorButton.Location = new System.Drawing.Point(94, 161);
            this.GeneratorButton.Name = "GeneratorButton";
            this.GeneratorButton.Size = new System.Drawing.Size(75, 23);
            this.GeneratorButton.TabIndex = 9;
            this.GeneratorButton.Text = "Generate";
            this.GeneratorButton.UseVisualStyleBackColor = true;
            this.GeneratorButton.Click += new System.EventHandler(this.GeneratorButton_Click);
            // 
            // InsertImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(181, 226);
            this.Controls.Add(this.GeneratorButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.CreateImagegroupBox);
            this.Controls.Add(this.loadImageButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "InsertImageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insert image";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InsertImageForm_FormClosing);
            this.Load += new System.EventHandler(this.InsertImageForm_Load);
            this.CreateImagegroupBox.ResumeLayout(false);
            this.CreateImagegroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button loadImageButton;
        private System.Windows.Forms.CheckBox checkBoxBlocks;
        private System.Windows.Forms.CheckBox checkBoxBackground;
        private System.Windows.Forms.GroupBox CreateImagegroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ActionBlockscheckBox;
        private System.Windows.Forms.CheckBox MorphablecheckBox;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button GeneratorButton;
    }
}