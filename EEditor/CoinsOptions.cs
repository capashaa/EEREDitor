using System;
using System.Windows.Forms;

namespace EEditor
{
    public partial class CoinsOptions : Form
    {
        public NumericUpDown NumericUpdown1 { get { return numericUpDown1; } set { numericUpDown1 = value; } }
        public NumericUpDown NumericUpdown2 { get { return numericUpDown2; } set { numericUpDown2 = value; } }
        public NumericUpDown NumericUpdown3 { get { return numericUpDown3; } set { numericUpDown3 = value; } }

        public CoinsOptions()
        {
            InitializeComponent();
        }

        private void CoinsOptions_Load(object sender, EventArgs e)
        {
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            foreach (Control value in this.Controls)
            {
                if (value.GetType() == typeof(NumericUpDown))
                {
                    value.ForeColor = MainForm.themecolors.foreground;
                    value.BackColor = MainForm.themecolors.accent;
                }
            }

        }

        private void CoinsOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void numericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.SuppressKeyPress = true;
        }



        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                e.Handled = e.SuppressKeyPress = true;
               ((NumericUpDown)sender).Value += 1;
            }
            if (e.KeyCode == Keys.S)
            {
                e.Handled = e.SuppressKeyPress = true;
                if (((NumericUpDown)sender).Value >= 1) ((NumericUpDown)sender).Value -= 1;
            }
        }
    }
}
