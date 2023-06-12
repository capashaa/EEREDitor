using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace EEditor
{
    public partial class Drums : Form
    {
        Rectangle[] rec = new Rectangle[10];
        Bitmap bmp;
        //public Label Label2 { get { return label2; } set { label2 = value; } }
        public Drums()
        {
            InitializeComponent();
        }

        private void Drums_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(Properties.Resources.drum.Width, Properties.Resources.drum.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(Properties.Resources.drum, 0, 0);
                g.Save();
            }
            pictureBox1.Width = bmp.Width;
            pictureBox1.Height = bmp.Height;
            this.Width = bmp.Width + 16;
            pictureBox1.Image = bmp;
            createHitBoxes();

            if (!MainForm.soundsErrorShown)
            {
                if (Directory.Exists(Directory.GetCurrentDirectory() + @"\sounds"))
                {
                }
                else
                {
                    MessageBox.Show("Couldn't find the folder 'sounds'. You can continue using the drums without them, otherwise try re-installing EEditor.", "No sounds", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    MainForm.soundsErrorShown = true;
                }
            }
        }

        private void createHitBoxes()
        {
            for (int x = 0; x < 10; x++)
            {
                switch (x)
                {
                    case 0:
                        Graphics g = Graphics.FromImage(bmp);
                        Rectangle area = new Rectangle(x * 12 + 7, 2 * 12 + 15, 10, 10);
                        if (MainForm.userdata.showhitboxes) g.DrawRectangle(new Pen(Color.Red), area);
                        pictureBox1.Image = bmp;
                        rec[0] = area;
                        break;
                    case 1:
                        Graphics g1 = Graphics.FromImage(bmp);
                        Rectangle area1 = new Rectangle(x * 12 + 12, 2 * 12 + 15, 10, 10);
                        if (MainForm.userdata.showhitboxes) g1.DrawRectangle(new Pen(Color.Red), area1);
                        pictureBox1.Image = bmp;
                        rec[1] = area1;
                        break;
                    case 2:
                        Graphics g2 = Graphics.FromImage(bmp);
                        Rectangle area2 = new Rectangle(3 * 12 + 15, 2 * 12 + 15, 10, 10);
                        if (MainForm.userdata.showhitboxes) g2.DrawRectangle(new Pen(Color.Red), area2);
                        pictureBox1.Image = bmp;
                        rec[2] = area2;
                        break;
                    case 3:
                        Graphics g3 = Graphics.FromImage(bmp);
                        Rectangle area3 = new Rectangle(5 * 12 + 7, 2 * 12 + 15, 10, 10);
                        if (MainForm.userdata.showhitboxes) g3.DrawRectangle(new Pen(Color.Red), area3);
                        pictureBox1.Image = bmp;
                        rec[3] = area3;
                        break;
                    case 4:
                        Graphics g4 = Graphics.FromImage(bmp);
                        Rectangle area4 = new Rectangle(7 * 12 + 5, 2 * 12 + 15, 10, 10);
                        if (MainForm.userdata.showhitboxes) g4.DrawRectangle(new Pen(Color.Red), area4);
                        pictureBox1.Image = bmp;
                        rec[4] = area4;
                        break;
                    case 5:
                        Graphics g5 = Graphics.FromImage(bmp);
                        Rectangle area5 = new Rectangle(8 * 12 + 5, 2 * 12 + 15, 10, 10);
                        if (MainForm.userdata.showhitboxes) g5.DrawRectangle(new Pen(Color.Red), area5);
                        pictureBox1.Image = bmp;
                        rec[5] = area5;
                        break;
                    case 6:
                        Graphics g6 = Graphics.FromImage(bmp);
                        Rectangle area6 = new Rectangle(9 * 12 + 6, 2 * 12 + 15, 10, 10);
                        if (MainForm.userdata.showhitboxes) g6.DrawRectangle(new Pen(Color.Red), area6);
                        pictureBox1.Image = bmp;
                        rec[6] = area6;
                        break;
                    case 7:
                        Graphics g7 = Graphics.FromImage(bmp);
                        Rectangle area7 = new Rectangle(10 * 12 + 8, 0, 42, 42);
                        if (MainForm.userdata.showhitboxes) g7.DrawRectangle(new Pen(Color.Red), area7);
                        pictureBox1.Image = bmp;
                        rec[7] = area7;
                        break;
                    case 8:
                        Graphics g8 = Graphics.FromImage(bmp);
                        Rectangle area8 = new Rectangle(14 * 12 + 2, 0, 42, 42);
                        if (MainForm.userdata.showhitboxes) g8.DrawRectangle(new Pen(Color.Red), area8);
                        pictureBox1.Image = bmp;
                        rec[8] = area8;
                        break;
                    case 9:
                        Graphics g9 = Graphics.FromImage(bmp);
                        Rectangle area9 = new Rectangle(17 * 12 + 7, 0, 42, 42);
                        if (MainForm.userdata.showhitboxes) g9.DrawRectangle(new Pen(Color.Red), area9);
                        pictureBox1.Image = bmp;
                        rec[9] = area9;
                        break;
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int id = 0; id < 10; id++)
                {
                    if (e.X >= rec[id].X && e.X <= rec[id].Width + rec[id].X && e.Y >= rec[id].Y && e.Y <= rec[id].Height + rec[id].Y)
                    {
                        label2.Text = Convert.ToString(id);
                        var derp = id + 1;
                        if (Directory.Exists(Directory.GetCurrentDirectory() + "\\sounds"))
                        {
                            var file = Directory.GetCurrentDirectory() + "\\sounds\\drums" + derp + ".wav";
                            if (File.Exists(file))
                            {
                                var player = new System.Media.SoundPlayer(file);
                                player.Play();
                            }
                        }
                    }
                }
            }
        }

        private void Drums_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
