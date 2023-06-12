using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace EEditor
{
    public partial class InsertImageForm : Form
    {
        private string[,] Area;
        private string[,] Back;
        private string[,] Coins;
        private string[,] Id;
        private string[,] Target;
        private string[,] Text1;
        private string[,] Text2;
        private string[,] Text3;
        private string[,] Text4;
        private bool exit = false;
        int n;

        private Thread thread;
        public static List<int> Blocks = new List<int>();
        public static List<int> Background = new List<int>();
        public static List<int> SpecialMorph = new List<int>();
        public static List<int> SpecialAction = new List<int>();
        public static string path2file = null;

        public InsertImageForm()
        {
            InitializeComponent();
            checkBoxBackground.Checked = MainForm.userdata.imageBackgrounds;
            checkBoxBlocks.Checked = MainForm.userdata.imageBlocks;
            MorphablecheckBox.Checked = MainForm.userdata.imageSpecialblocksMorph;
            ActionBlockscheckBox.Checked = MainForm.userdata.imageSpecialblocksAction;
            n = Minimap.ImageColor.Count();
        }

        public void loadImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog imageFileDialog = new OpenFileDialog()
            {
                Filter = "Images |*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.ico",
                Title = "Choose an Image"
            };
            if (imageFileDialog.ShowDialog() == DialogResult.OK)
            {
                path2file = imageFileDialog.FileName;
                //pictureBox1.Image = image;
                //this.Invalidate();
            }
        }

        public void loadDroppedImage(string filename)
        {
            Bitmap originalImage = new Bitmap(Bitmap.FromFile(filename));
            thread = new Thread(() => Transform(originalImage));
            thread.Start();

            //pictureBox1.Image = image;
            //this.Invalidate();
        }
        #region EEditor from image to ee

        private double Distance(Color a, Color b)
        {
            return Math.Pow(a.R - b.R, 2) + Math.Pow(a.G - b.G, 2) + Math.Pow(a.B - b.B, 2);
        }

        private int BestMatchRGB(Color c)
        {
            int j = 0;
            double d = Distance(c, Color.FromArgb((int)Minimap.Colors[0]));
            if (checkBoxBackground.Checked)
            {
                foreach (int i in Background)
                {
                    if (Minimap.ImageColor[i])
                    {
                        double dist = Distance(c, Color.FromArgb((int)Minimap.Colors[i]));
                        if (dist < d)
                        {
                            d = dist;
                            j = i;
                        }
                    }
                    if (exit) break;
                }
            }
            if (checkBoxBlocks.Checked)
            {
                foreach (int i in Blocks)
                {
                    if (Minimap.ImageColor[i])
                    {
                        double dist = Distance(c, Color.FromArgb((int)Minimap.Colors[i]));
                        if (dist < d)
                        {
                            d = dist;
                            j = i;
                        }
                    }
                    if (exit) break;
                }
            }
            if (MorphablecheckBox.Checked)
            {
                foreach (int i in SpecialMorph)
                {
                    if (Minimap.ImageColor[i])
                    {
                        double dist = Distance(c, Color.FromArgb((int)Minimap.Colors[i]));
                        if (dist < d)
                        {
                            d = dist;
                            j = i;
                        }
                    }
                    if (exit) break;
                }
            }
            if (ActionBlockscheckBox.Checked)
            {
                foreach (int i in SpecialAction)
                {
                    if (Minimap.ImageColor[i])
                    {
                        double dist = Distance(c, Color.FromArgb((int)Minimap.Colors[i]));
                        if (dist < d)
                        {
                            d = dist;
                            j = i;
                        }
                    }
                    if (exit) break;
                }
            }

            return j;
        }
        #endregion

        private void Transform(Bitmap image)
        {
            
            int width = image.Width;
            int height = image.Height;
            int incr = 0;
            Area = new string[height, width];
            Back = new string[height, width];
            Coins = new string[height, width];
            Id = new string[height, width];
            Target = new string[height, width];
            Text1 = new string[height, width];
            Text2 = new string[height, width];
            Text3 = new string[height, width];
            Text4 = new string[height, width];
            
            if (width > MainForm.editArea.BlockWidth || height > MainForm.editArea.BlockHeight)
            {
                DialogResult rs = MessageBox.Show("The image is bigger than the world you have. Do you want to continue?", "Image bigger than world", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        for (int y = 0; y < image.Height; y++)
                        {
                            int c;
                            Color col = image.GetPixel(x, y);
                            if (col.A == 0 && col.R == 0 && col.G == 0 && col.B == 0)
                            {
                                c = 0;
                            }
                            else
                            {
                                c = BestMatchRGB(col);
                            }
                            int xx = x;
                            int yy = y;
                            if (c < 500 || c >= 1001 || c == -1)
                                Area[yy, xx] = Convert.ToString(c);
                            else
                                Back[yy, xx] = Convert.ToString(c);
                            if (exit) break;
                        }
                        if (exit) break;
                        incr += 1;
                        if (progressBar1.InvokeRequired)
                        {
                            progressBar1.Invoke((MethodInvoker)delegate
                            {
                                double progress = ((double)incr / image.Width) * 100;
                                progressBar1.Value = (int)progress;
                            });
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        int c;
                        Color col = image.GetPixel(x, y);
                        if (col.A == 0 && col.R == 0 && col.G == 0 && col.B == 0)
                        {
                            c = 0;
                        }
                        else
                        {
                            c = BestMatchRGB(col);
                        }
                        int xx = x;
                        int yy = y;
                        if (c < 500 || c >= 1001 || c == -1)
                            Area[yy, xx] = Convert.ToString(c);
                        else
                            Back[yy, xx] = Convert.ToString(c);
                        if (exit) break;
                    }
                    if (exit) break;
                    incr += 1;
                    if (progressBar1.InvokeRequired)
                    {
                        progressBar1.Invoke((MethodInvoker)delegate
                        {
                            double progress = ((double)incr / image.Width) * 100;
                            progressBar1.Value = (int)progress;
                        });
                    }
                }
            }
            DialogResult imagedone = MessageBox.Show("The image has been loaded! Would you like to insert it now?\n\nYes - inserts loaded image at current world position\nNo - adds loaded image to clipboard, so you can paste it with Ctrl + V\nCancel - lets you pick another image", "Image loaded", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (imagedone == DialogResult.Yes)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Clipboard.SetData("EEData", new string[][,] { Area, Back, Coins, Id, Target, Text1, Text2, Text3, Text4 });
                    MainForm.editArea.MainForm.SetMarkTool();
                    MainForm.editArea.Focus();
                    SendKeys.Send("^{v}");
                    Close();
                });
            }
            try
            {
                thread.Abort();
            }
            catch
            {
            }
        }


        private void checkBoxBackground_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.imageBackgrounds = checkBoxBackground.Checked;
        }

        private void checkBoxBlocks_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.imageBlocks = checkBoxBlocks.Checked;
        }

        private void MorphablecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.imageSpecialblocksMorph = MorphablecheckBox.Checked;
        }

        private void ActionBlockscheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.imageSpecialblocksAction = ActionBlockscheckBox.Checked;
        }

        private void InsertImageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit = true;
        }

        private void GeneratorButton_Click(object sender, EventArgs e)
        {
            if (path2file != null && File.Exists(path2file))
            {
                Image img = Bitmap.FromFile(path2file);
                thread = new Thread(() => Transform((Bitmap)img));
                thread.Start();
            }
            else
            {
                FlexibleMessageBox.Show("The picture doesn't exist or isn't loaded.");
            }
        }

        private void InsertImageForm_Load(object sender, EventArgs e)
        {
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            foreach (Control value in this.Controls)
            {
                if (value.GetType() == typeof(Button))
                {
                    ((Button)value).ForeColor = MainForm.themecolors.foreground;
                    ((Button)value).BackColor = MainForm.themecolors.accent;
                    ((Button)value).FlatStyle = FlatStyle.Flat;
                    if (((Button)value).Image != null)
                    {
                        Bitmap bmpa = (Bitmap)((Button)value).Image;
                        Bitmap bmpa1 = new Bitmap(((Button)value).Image.Width, ((Button)value).Image.Height);
                        for (int x = 0; x < ((Button)value).Image.Width; x++)
                        {
                            for (int y = 0; y < ((Button)value).Image.Height; y++)
                            {
                                if (bmpa.GetPixel(x, y).A > 80)
                                {
                                    bmpa1.SetPixel(x, y, MainForm.themecolors.imageColors);
                                }
                                else
                                {
                                    bmpa1.SetPixel(x, y, MainForm.themecolors.accent);
                                }
                            }
                        }
                        ((Button)value).Image = bmpa1;
                    }
                }
                if (value.GetType() == typeof(GroupBox))
                {
                    value.ForeColor = MainForm.themecolors.foreground;
                }
            }
        }

    }
}
