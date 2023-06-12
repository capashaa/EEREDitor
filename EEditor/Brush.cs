using System;
using System.Windows.Forms;

namespace EEditor
{
    public partial class Brush : Form
    {
        public NumericUpDown BrushSize { get { return numericUpDown1; } set { numericUpDown1 = value; } }
        public Brush()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Tool.PenSize = (int)numericUpDown1.Value;
            this.Close();
        }

        private void Brush_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Brush_Load(object sender, EventArgs e)
        {
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            numericUpDown1.ForeColor = MainForm.themecolors.foreground;
            numericUpDown1.BackColor = MainForm.themecolors.accent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = MainForm.themecolors.foreground;
            button1.BackColor = MainForm.themecolors.accent;
        }
    }
}
