using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace EEditor
{
    public partial class Piano : Form
    {
        Rectangle[] rec = new Rectangle[25];
        //public Label Label2 { get { return label2; } set { label2 = value; } }
        Bitmap bmp;
        public Piano()
        {
            InitializeComponent();
        }

        private void Piano_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(Properties.Resources.piano.Width, Properties.Resources.piano.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(Properties.Resources.piano, 0, 0);
                g.Save();
            }
            pictureBox1.Width = bmp.Width;
            pictureBox1.Height = bmp.Height;
            this.Width = bmp.Width + 16;
            pictureBox1.Image = bmp;
            MainForm.userdata.showhitboxes = false;
            createHitBoxes();

            if (!MainForm.soundsErrorShown)
            {
                if (Directory.Exists(Directory.GetCurrentDirectory() + @"\sounds"))
                {
                }
                else
                {
                    MessageBox.Show("Couldn't find the folder Sounds. You can continue using the piano without them or redownload EEditor.", "No sounds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MainForm.soundsErrorShown = true;
                }
            }
        }

        private void createHitBoxes()
        {
            for (int x = 0; x < 25; x++)
            {
                switch (x)
                {
                    case 0:
                        //White
                        Graphics g = Graphics.FromImage(bmp);
                        Rectangle area = new Rectangle(x * 12, 0, 12, 40);
                        if (MainForm.userdata.showhitboxes) g.DrawRectangle(new Pen(Color.Red), area);
                        pictureBox1.Image = bmp;
                        rec[0] = area;
                        break;
                    case 1:
                        //Black
                        Graphics g1 = Graphics.FromImage(bmp);
                        Rectangle area1 = new Rectangle(x * 11, 0, 10, 24);
                        if (MainForm.userdata.showhitboxes) g1.DrawRectangle(new Pen(Color.Red), area1);
                        pictureBox1.Image = bmp;
                        rec[1] = area1;
                        break;
                    case 2:
                        //White
                        Graphics g01 = Graphics.FromImage(bmp);
                        Rectangle area01 = new Rectangle(x * 12 - 3, 0, 10, 40);
                        if (MainForm.userdata.showhitboxes) g01.DrawRectangle(new Pen(Color.Red), area01);
                        pictureBox1.Image = bmp;
                        rec[2] = area01;
                        break;
                    case 3:
                        //Black
                        Graphics g2 = Graphics.FromImage(bmp);
                        Rectangle area2 = new Rectangle(x * 11 - 4, 0, 10, 24);
                        if (MainForm.userdata.showhitboxes) g2.DrawRectangle(new Pen(Color.Red), area2);
                        pictureBox1.Image = bmp;
                        rec[3] = area2;
                        break;
                    case 4:
                        //white
                        Graphics g02 = Graphics.FromImage(bmp);
                        Rectangle area02 = new Rectangle(x * 12 - 10, 0, 12, 40);
                        if (MainForm.userdata.showhitboxes) g02.DrawRectangle(new Pen(Color.Red), area02);
                        pictureBox1.Image = bmp;
                        rec[4] = area02;
                        break;
                    case 5:
                        //white
                        Graphics g3 = Graphics.FromImage(bmp);
                        Rectangle area3 = new Rectangle(x * 12 - 10, 0, 12, 40);
                        if (MainForm.userdata.showhitboxes) g3.DrawRectangle(new Pen(Color.Red), area3);
                        pictureBox1.Image = bmp;
                        rec[5] = area3;
                        break;
                    case 6:
                        //Black
                        Graphics g4 = Graphics.FromImage(bmp);
                        Rectangle area4 = new Rectangle(x * 11 - 4, 0, 10, 24);
                        if (MainForm.userdata.showhitboxes) g4.DrawRectangle(new Pen(Color.Red), area4);
                        pictureBox1.Image = bmp;
                        rec[6] = area4;
                        break;
                    case 7:
                        //white
                        Graphics g5 = Graphics.FromImage(bmp);
                        Rectangle area5 = new Rectangle(x * 12 - 11, 0, 9, 40);
                        if (MainForm.userdata.showhitboxes) g5.DrawRectangle(new Pen(Color.Red), area5);
                        pictureBox1.Image = bmp;
                        rec[7] = area5;
                        break;
                    case 8:
                        //Black
                        Graphics g6 = Graphics.FromImage(bmp);
                        Rectangle area6 = new Rectangle(x * 11 - 8, 0, 10, 24);
                        if (MainForm.userdata.showhitboxes) g6.DrawRectangle(new Pen(Color.Red), area6);
                        pictureBox1.Image = bmp;
                        rec[8] = area6;
                        break;
                    case 9:
                        //white
                        Graphics g7 = Graphics.FromImage(bmp);
                        Rectangle area7 = new Rectangle(x * 12 - 18, 0, 9, 40);
                        if (MainForm.userdata.showhitboxes) g7.DrawRectangle(new Pen(Color.Red), area7);
                        pictureBox1.Image = bmp;
                        rec[9] = area7;
                        break;
                    case 10:
                        //Black
                        Graphics g8 = Graphics.FromImage(bmp);
                        Rectangle area8 = new Rectangle(x * 11 - 13, 0, 10, 24);
                        if (MainForm.userdata.showhitboxes) g8.DrawRectangle(new Pen(Color.Red), area8);
                        pictureBox1.Image = bmp;
                        rec[10] = area8;
                        break;
                    case 11:
                        //white
                        Graphics g9 = Graphics.FromImage(bmp);
                        Rectangle area9 = new Rectangle(10 * 12 - 12, 0, 9, 40);
                        if (MainForm.userdata.showhitboxes) g9.DrawRectangle(new Pen(Color.Red), area9);
                        pictureBox1.Image = bmp;
                        rec[11] = area9;
                        break;
                    case 12:
                        //white
                        Graphics g10 = Graphics.FromImage(bmp);
                        Rectangle area10 = new Rectangle(11 * 12 - 11, 0, 9, 40);
                        if (MainForm.userdata.showhitboxes) g10.DrawRectangle(new Pen(Color.Red), area10);
                        pictureBox1.Image = bmp;
                        rec[12] = area10;
                        break;
                    case 13:
                        //Black
                        Graphics g11 = Graphics.FromImage(bmp);
                        Rectangle area11 = new Rectangle(x * 11 - 13, 0, 10, 24);
                        if (MainForm.userdata.showhitboxes) g11.DrawRectangle(new Pen(Color.Red), area11);
                        pictureBox1.Image = bmp;
                        rec[13] = area11;
                        break;
                    case 14:
                        //white
                        Graphics g12 = Graphics.FromImage(bmp);
                        Rectangle area12 = new Rectangle(11 * 12 + 8, 0, 9, 40);
                        if (MainForm.userdata.showhitboxes) g12.DrawRectangle(new Pen(Color.Red), area12);
                        pictureBox1.Image = bmp;
                        rec[14] = area12;
                        break;
                    case 15:
                        //Black
                        Graphics g13 = Graphics.FromImage(bmp);
                        Rectangle area13 = new Rectangle(x * 11 - 16, 0, 10, 24);
                        if (MainForm.userdata.showhitboxes) g13.DrawRectangle(new Pen(Color.Red), area13);
                        pictureBox1.Image = bmp;
                        rec[15] = area13;
                        break;
                    case 16:
                        //white
                        Graphics g14 = Graphics.FromImage(bmp);
                        Rectangle area14 = new Rectangle(13 * 12 + 4, 0, 9, 40);
                        if (MainForm.userdata.showhitboxes) g14.DrawRectangle(new Pen(Color.Red), area14);
                        pictureBox1.Image = bmp;
                        rec[16] = area14;
                        break;
                    case 17:
                        //white
                        Graphics g15 = Graphics.FromImage(bmp);
                        Rectangle area15 = new Rectangle(14 * 12 + 5, 0, 9, 40);
                        if (MainForm.userdata.showhitboxes) g15.DrawRectangle(new Pen(Color.Red), area15);
                        pictureBox1.Image = bmp;
                        rec[17] = area15;
                        break;
                    case 18:
                        //Black
                        Graphics g16 = Graphics.FromImage(bmp);
                        Rectangle area16 = new Rectangle(x * 11 - 16, 0, 10, 24);
                        if (MainForm.userdata.showhitboxes) g16.DrawRectangle(new Pen(Color.Red), area16);
                        pictureBox1.Image = bmp;
                        rec[18] = area16;
                        break;
                    case 19:
                        //white
                        Graphics g17 = Graphics.FromImage(bmp);
                        Rectangle area17 = new Rectangle(16 * 12, 0, 9, 40);
                        if (MainForm.userdata.showhitboxes) g17.DrawRectangle(new Pen(Color.Red), area17);
                        pictureBox1.Image = bmp;
                        rec[19] = area17;
                        break;
                    case 20:
                        //Black
                        Graphics g18 = Graphics.FromImage(bmp);
                        Rectangle area18 = new Rectangle(x * 11 - 20, 0, 10, 24);
                        if (MainForm.userdata.showhitboxes) g18.DrawRectangle(new Pen(Color.Red), area18);
                        pictureBox1.Image = bmp;
                        rec[20] = area18;
                        break;
                    case 21:
                        //white
                        Graphics g19 = Graphics.FromImage(bmp);
                        Rectangle area19 = new Rectangle(17 * 12 + 5, 0, 8, 40);
                        if (MainForm.userdata.showhitboxes) g19.DrawRectangle(new Pen(Color.Red), area19);
                        pictureBox1.Image = bmp;
                        rec[21] = area19;
                        break;
                    case 22:
                        //Black
                        Graphics g20 = Graphics.FromImage(bmp);
                        Rectangle area20 = new Rectangle(20 * 11 - 4, 0, 10, 24);
                        if (MainForm.userdata.showhitboxes) g20.DrawRectangle(new Pen(Color.Red), area20);
                        pictureBox1.Image = bmp;
                        rec[22] = area20;
                        break;
                    case 23:
                        //white
                        Graphics g21 = Graphics.FromImage(bmp);
                        Rectangle area21 = new Rectangle(19 * 12, 0, 8, 40);
                        if (MainForm.userdata.showhitboxes) g21.DrawRectangle(new Pen(Color.Red), area21);
                        pictureBox1.Image = bmp;
                        rec[23] = area21;
                        break;
                    case 24:
                        //white
                        Graphics g22 = Graphics.FromImage(bmp);
                        Rectangle area22 = new Rectangle(20 * 12, 0, 14, 40);
                        if (MainForm.userdata.showhitboxes) g22.DrawRectangle(new Pen(Color.Red), area22);
                        pictureBox1.Image = bmp;
                        rec[24] = area22;
                        break;
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int id = 0; id < 25; id++)
                {
                    if (e.X >= rec[id].X && e.X <= rec[id].Width + rec[id].X && e.Y >= rec[id].Y && e.Y <= rec[id].Height + rec[id].Y)
                    {
                        label2.Text = Convert.ToString(id);
                        var derp = id + 1;
                        if (Directory.Exists(Directory.GetCurrentDirectory() + "\\sounds"))
                        {
                            var file = Directory.GetCurrentDirectory() + "\\sounds\\piano" + derp + ".wav";
                            if (File.Exists(file))
                            {
                                System.Media.SoundPlayer player = new System.Media.SoundPlayer(file);
                                player.Play();
                            }
                        }
                    }
                }
            }
        }

        private void Piano_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
