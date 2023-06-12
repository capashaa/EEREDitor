using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace EEditor
{
    public partial class NumberChanger : Form
    {
        public NumericUpDown NumberChangerNumeric { get { return numericUpDown1; } set { numericUpDown1 = value; } }
        public NumberChanger()
        {
            InitializeComponent();
        }

        private void NumberChanger_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void NumberChanger_Load(object sender, EventArgs e)
        {
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            numericUpDown1.ForeColor = MainForm.themecolors.foreground;
            numericUpDown1.BackColor = MainForm.themecolors.accent;
        }

        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                e.Handled = e.SuppressKeyPress = true;
                numericUpDown1.Value += 1;
            }
            if (e.KeyCode == Keys.S)
            {
                e.Handled = e.SuppressKeyPress = true;
                if (numericUpDown1.Value > 1) numericUpDown1.Value -= 1;
            }
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.SuppressKeyPress = true;
        }
    }
}
