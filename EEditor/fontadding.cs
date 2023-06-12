using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EEditor
{
    public partial class fontadding : Form
    {
        //int yy = 0;
        private string[,] Area;
        private string[,] Back;
        private string[,] Coins;
        private string[,] Id;
        private string[,] Target;
        private string[,] Text1;
        private string[,] Text2;
        private string[,] Text3;
        private string[,] Text4;
        private string fontname = "Calibri";
        private int fontsize = 20;
        private Bitmap bmp = new Bitmap(25, 25);
        private List<string> messages = new List<string>();
        public fontadding()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var message = textBox1.Text;
            if (!string.IsNullOrEmpty(message))
            {
                Size textSize = TextRenderer.MeasureText(message, new Font(fontname, fontsize / 2));
                if (textSize.Width <= MainForm.editArea.Frames[0].Width)
                {
                    textBox1.MaxLength = MainForm.editArea.Frames[0].Width;
                    //Console.WriteLine("A Map Witdh: " + MainForm.editArea.Frames[0].Width + " Text Width: " + textSize.Width + "\nMap Height: " + MainForm.editArea.Frames[0].Height + " Text Height: " + textSize.Height + "\n\n");
                    bmp = new Bitmap(MainForm.editArea.Frames[0].Width, textSize.Height);
                    Graphics g = Graphics.FromImage(bmp);
                    g.FillRectangle(new SolidBrush(Color.Black), 0, 0, MainForm.editArea.Frames[0].Width, textSize.Height);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
                    g.DrawString(message, new Font(fontname, fontsize / 2), new SolidBrush(Color.White), new RectangleF(0, 0, MainForm.editArea.Frames[0].Width, textSize.Height));
                    pictureBox1.Image = bmp;
                }
                else
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                    textBox1.MaxLength = textBox1.Text.Length - 1;
                }
            }
            else
            {
                Bitmap bitmap = new Bitmap(1, 1);
                Graphics g = Graphics.FromImage(bitmap);
                g.Clear(Color.Empty);
                pictureBox1.Image = bitmap;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            readtext(false);
        }

        private void readtext(bool clipboard)
        {
            var message = textBox1.Text;
            if (!string.IsNullOrWhiteSpace(message))
            {
                Size textSize = TextRenderer.MeasureText(message, new Font(fontname, fontsize / 2));
                //Console.WriteLine("C " + textSize.Width + " - " + MainForm.editArea.Frames[0].Width);
                if (textSize.Width <= MainForm.editArea.Frames[0].Width)
                {
                    Bitmap bitmap = new Bitmap(textSize.Width, textSize.Height);
                    int width = bitmap.Width;
                    int height = bitmap.Height;
                    Graphics g = Graphics.FromImage(bitmap);
                    g.FillRectangle(new SolidBrush(Color.Black), 0, 0, bitmap.Width, bitmap.Height);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
                    g.DrawString(message, new Font(fontname, fontsize / 2), new SolidBrush(Color.White),0, 0);
                    SizeF size = g.MeasureString(message, new Font(fontname, fontsize / 2));
                    Area = new string[height, Size.Round(size).Width];
                    Back = new string[height, Size.Round(size).Width];
                    Coins = new string[height, Size.Round(size).Width];
                    Id = new string[height, Size.Round(size).Width];
                    Target = new string[height, Size.Round(size).Width];
                    Text1 = new string[height, Size.Round(size).Width];
                    Text2 = new string[height, Size.Round(size).Width];
                    Text3 = new string[height, Size.Round(size).Width];
                    Text4 = new string[height, Size.Round(size).Width];
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        for (int y = 0; y < bitmap.Height; y++)
                        {
                            Color clr = bitmap.GetPixel(x, y);
                            if (clr.ToArgb() != -16777216)
                            {
                                if (MainForm.selectedBrick.ID < 500 || MainForm.selectedBrick.ID >= 1001)
                                {
                                    int value = MainForm.selectedBrick.ID == 0 ? 9 : MainForm.selectedBrick.ID;
                                    Area[y, x] = value.ToString();
                                }
                                else if (MainForm.selectedBrick.ID >= 500 || MainForm.selectedBrick.ID <= 999)
                                {
                                    Back[y, x] = Convert.ToString(MainForm.selectedBrick.ID);
                                }


                            }

                        }
                    }
                    if (Area != null && Coins != null)
                    {
                        if (clipboard)
                        {
                            Clipboard.SetData("EEData", new string[][,] { Area, Back, Coins, Id, Target, Text1,Text2,Text3,Text4 });
                            Close();
                        }
                        else
                        {
                            Clipboard.SetData("EEData", new string[][,] { Area, Back, Coins, Id, Target, Text1,Text2,Text3,Text4 });
                            MainForm.editArea.Focus();
                            SendKeys.Send("^{v}");
                            Close();
                        }

                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = fontDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fontname = fontDialog1.Font.Name;
                    fontsize = (int)fontDialog1.Font.Size;
                    textBox1.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fontadding_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(MainForm.editArea.Frames[0].Width, MainForm.editArea.Frames[0].Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);
            pictureBox1.Image = bmp;
            foreach (Control cntrl in this.Controls)
            {
                if (cntrl.GetType() == typeof(Button))
                {
                    cntrl.BackColor = MainForm.themecolors.accent;
                    cntrl.ForeColor = MainForm.themecolors.foreground;
                   ((Button)cntrl).FlatStyle = FlatStyle.Flat;
                }
            }
            button1.BackColor = MainForm.themecolors.accent;
            button1.ForeColor = MainForm.themecolors.foreground;
            button1.FlatStyle = FlatStyle.Flat;
            textBox1.BackColor = MainForm.themecolors.accent;
            textBox1.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            this.ForeColor = MainForm.themecolors.foreground;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            readtext(true);
        }
    }
}
