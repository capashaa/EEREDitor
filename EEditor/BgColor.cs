using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EEditor
{
    public partial class BgColor : Form
    {
        public Color setCol { get; set; }
        public BgColor()
        {
            InitializeComponent();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                setCol = c.Color;
                Bitmap bmp = new Bitmap(pbColor.Width, pbColor.Height);
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.Clear(c.Color);
                }
                pbColor.Image = bmp;
            }
        }

        private void BgColor_Load(object sender, EventArgs e)
        {
            if (MainForm.userdata.useColor)
            {
                cbEnable.Checked = true;
                setCol = MainForm.userdata.thisColor;
                Bitmap bmp = new Bitmap(pbColor.Width, pbColor.Height);
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.Clear(MainForm.userdata.thisColor);
                }
                pbColor.Image = bmp;
            }
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            foreach (Control cntr in this.Controls)
            {
                if (cntr.GetType() == typeof(Button))
                {
                    ((Button)cntr).ForeColor = MainForm.themecolors.foreground;
                    ((Button)cntr).BackColor = MainForm.themecolors.accent;
                    ((Button)cntr).FlatStyle = FlatStyle.Flat;
                }
                if (cntr.GetType() == typeof(RadioButton))
                {
                    ((RadioButton)cntr).ForeColor = MainForm.themecolors.foreground;
                    ((RadioButton)cntr).BackColor = MainForm.themecolors.accent;
                    ((RadioButton)cntr).FlatStyle = FlatStyle.Flat;
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (cbEnable.Checked)
            {
                MainForm.userdata.useColor = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MainForm.userdata.useColor = false;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
