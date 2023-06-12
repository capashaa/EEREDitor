using System;
using System.Windows.Forms;

namespace EEditor
{
    public partial class SprayCan : Form
    {
        public SprayCan()
        {
            InitializeComponent();
        }

        private void SprayCan_Load(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            tp.SetToolTip(numericUpDown1, "Size of the spraying area diameter, in blocks.");
            tp.SetToolTip(numericUpDown2, "Blocks per spray, in percentage.");
            numericUpDown1.Value = MainForm.userdata.sprayr;
            numericUpDown2.Value = MainForm.userdata.sprayp;
            numericUpDown1.ForeColor = MainForm.themecolors.foreground;
            numericUpDown1.BackColor = MainForm.themecolors.accent;
            numericUpDown2.ForeColor = MainForm.themecolors.foreground;
            numericUpDown2.BackColor = MainForm.themecolors.accent;
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
        }

        private void SprayCan_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            MainForm.userdata.sprayr = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            MainForm.userdata.sprayp = (int)numericUpDown2.Value;
        }
    }
}
