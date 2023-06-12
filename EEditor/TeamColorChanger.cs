using System;
using System.Drawing;
using System.Windows.Forms;

namespace EEditor
{
    public partial class TeamColorChanger : Form
    {
        public int color = 0;
        public Point point;
        public int SetColorId { get { return color; } set { color = value; } }
        public Point StartAt { get { return point; } set { point = value; } }
        public TeamColorChanger()
        {
            InitializeComponent();
        }

        private void TeamColorChanger_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("None");
            comboBox1.Items.Add("Red");
            comboBox1.Items.Add("Blue");
            comboBox1.Items.Add("Green");
            comboBox1.Items.Add("Cyan");
            comboBox1.Items.Add("Magenta");
            comboBox1.Items.Add("Yellow");
            comboBox1.SelectedIndex = color;
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            comboBox1.ForeColor = MainForm.themecolors.foreground;
            comboBox1.BackColor = MainForm.themecolors.accent;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    color = 0;
                    break;
                case 1:
                    color = 1;
                    break;
                case 2:
                    color = 2;
                    break;
                case 3:
                    color = 3;
                    break;
                case 4:
                    color = 4;
                    break;
                case 5:
                    color = 5;
                    break;
                case 6:
                    color = 6;
                    break;
                default:
                    color = 0;
                    break;
            }
        }

        private void TeamColorChanger_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
