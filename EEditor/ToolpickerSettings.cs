using System;
using System.Windows.Forms;

namespace EEditor
{
    public partial class ToolpickerSettings : Form
    {
        public bool start = false;
        public string hex = null;
        public bool colorExact = false;
        public ToolpickerSettings()
        {
            InitializeComponent();
        }

        private void SettingsFGCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!start) MainForm.userdata.ColorFG = SettingsFGCheckBox.Checked;
        }

        private void SettingsBGCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!start) MainForm.userdata.ColorBG = SettingsBGCheckBox.Checked;
        }

        private void SelectColorButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtbHex.Text)) hex = txtbHex.Text;
            colorExact = checkBox1.Checked;
        }

        private void ToolpickerSettings_Load(object sender, EventArgs e)
        {
            start = true;
            SettingsFGCheckBox.Checked = MainForm.userdata.ColorFG;
            SettingsBGCheckBox.Checked = MainForm.userdata.ColorBG;
            start = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
