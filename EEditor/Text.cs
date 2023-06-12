using System;
using System.Drawing;
using System.Windows.Forms;

namespace EEditor
{
    public partial class Text : Form
    {
        public TextBox textbox { get { return textBox1; } set { textBox1 = value; } }
        public int cm1 { get; set; } = 0;
        public int id1 { get; set; } = 0;

        public Text()
        {
            InitializeComponent();
        }

        private void Text_Load(object sender, EventArgs e)
        {
            if (this.id1 == 385)
            {
                this.Text = "Sign";
                textBox1.Text = textbox.Text;
                Bitmap image1 = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[43] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                Bitmap image2 = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[213] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                Bitmap image3 = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[1011] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                Bitmap Image4 = MainForm.miscBMD.Clone(new Rectangle(255 * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                Bitmap Image5 = MainForm.miscBMD.Clone(new Rectangle(256 * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                Bitmap Image6 = MainForm.miscBMD.Clone(new Rectangle(257 * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                Bitmap Image7 = MainForm.miscBMD.Clone(new Rectangle(258 * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
               if (MainForm.accs[MainForm.userdata.username].payvault.ContainsKey("goldmember")) { radioButton4.Enabled = true; }
               else { radioButton4.Enabled = false; }
                goldCoinsPictureBox.Image = image1;
                blueCoinsPictureBox.Image = image2;
                deathsPictureBox.Image = image3;
                radioButton1.Image = Image4;
                radioButton2.Image = Image5;
                radioButton3.Image = Image6;
                radioButton4.Image = Image7;
                switch (this.cm1)
                {
                    case 0:
                        radioButton1.Checked = true;
                        break;
                    case 1:
                        radioButton2.Checked = true;
                        break;
                    case 2:
                        radioButton3.Checked = true;
                        break;
                    case 3:
                        radioButton4.Checked = true;
                        break;
                }
                //Tooltips
                ToolTip tp = new ToolTip();
                tp.SetToolTip(goldCoinsPictureBox, "Add the amount of gold coins the player has");
                tp.SetToolTip(blueCoinsPictureBox, "Add the amount of blue coins the player has");
                tp.SetToolTip(deathsPictureBox, "Add the amount of deaths the player has");
                tp.SetToolTip(levelNamePictureBox, "Add the name of the level");
                tp.SetToolTip(usernamePictureBox, "Add the name of the player");
                tp.SetToolTip(newLinePictureBox, "Add a new line to the text");
                SpawnIDLabel.Visible = false;
                SpawnIDNumericUpDown.Visible = false;
                this.Height = 200;
            }
            else
            {
                this.Text = "World Portal";
                textBox1.Text = textbox.Text;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                goldCoinsPictureBox.Visible = false;
                blueCoinsPictureBox.Visible = false;
                deathsPictureBox.Visible = false;
                levelNamePictureBox.Visible = false;
                usernamePictureBox.Visible = false;
                newLinePictureBox.Visible = false;
                this.Height = 200;
                SpawnIDLabel.Visible = true;
                SpawnIDNumericUpDown.Visible = true;
                SpawnIDNumericUpDown.Value = this.cm1;
            }
            textBox1.ForeColor = MainForm.themecolors.foreground;
            textBox1.BackColor = MainForm.themecolors.accent;
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            SpawnIDNumericUpDown.ForeColor = MainForm.themecolors.foreground;
            SpawnIDNumericUpDown.BackColor = MainForm.themecolors.accent;
        }

        private void goldCoinsPictureBox_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("%coins%");
        }

        private void blueCoinsPictureBox_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("%bcoins%");
        }

        private void deathsPictureBox_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("%deaths%");
        }

        private void levelNamePictureBox_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("%levelname%");
        }

        private void usernamePictureBox_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("%username%");
        }

        private void newLinePictureBox_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("\\n");
        }

        private void Text_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                this.cm1 = Convert.ToInt32(rb.Name.Substring(Convert.ToInt32(rb.Name.Length) - 1, 1)) - 1;
            }
        }

        private void SpawnIDNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.cm1 = Convert.ToInt32(SpawnIDNumericUpDown.Value);
        }

        private void SpawnIDNumericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            this.cm1 = Convert.ToInt32(SpawnIDNumericUpDown.Value);
        }
    }
}
