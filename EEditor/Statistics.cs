using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace EEditor
{
    public partial class Statistics : Form
    {
        private Dictionary<int, int> bdata = new Dictionary<int, int>();
        private Semaphore wait = new Semaphore(0, 1);
        public Statistics()
        {
            InitializeComponent();
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            panel1.AutoScroll = true;
            panel1.BackColor = MainForm.themecolors.accent;
            this.BackColor = MainForm.themecolors.background;
            this.ForeColor = MainForm.themecolors.foreground;
            foreach (Control cntrls in this.Controls)
            {
                if (cntrls.GetType() == typeof(RadioButton))
                {
                    ((RadioButton)cntrls).ForeColor = MainForm.themecolors.foreground;
                    ((RadioButton)cntrls).BackColor = MainForm.themecolors.accent;
                }
            }
            sortby(1);
        }

        private void sortby(int id)
        {
            panel1.Controls.Clear();
            bdata.Clear();
            for (int x = 0; x < MainForm.editArea.CurFrame.Width; x++)
            {
                for (int y = 0; y < MainForm.editArea.CurFrame.Height; y++)
                {
                    if (bdata.ContainsKey(MainForm.editArea.CurFrame.Foreground[y, x]) && id >= 0 && id <= 3)
                    {


                        bdata[MainForm.editArea.CurFrame.Foreground[y, x]] += 1;
                    }
                    if (!bdata.ContainsKey(MainForm.editArea.CurFrame.Foreground[y, x]) && id >= 0 && id <= 3)
                    {

                        bdata.Add(MainForm.editArea.CurFrame.Foreground[y, x], 1);
                    }
                    if (bdata.ContainsKey(MainForm.editArea.CurFrame.Background[y, x]) && MainForm.editArea.CurFrame.Background[y, x]  != 0 && (id == 0 || id == 4))
                    {


                        bdata[MainForm.editArea.CurFrame.Background[y, x]] += 1;
                    }
                    if (!bdata.ContainsKey(MainForm.editArea.CurFrame.Background[y, x]) && MainForm.editArea.CurFrame.Background[y, x] != 0 && (id == 0 || id == 4))
                    {

                        bdata.Add(MainForm.editArea.CurFrame.Background[y, x], 1);
                    }
                }
            }
                int position = 0, wposition = 4;
            foreach (var val in bdata)
            {
                PictureBox table = new PictureBox();
                ToolTip tp = new ToolTip();
                tp.SetToolTip(table, val.Key.ToString());
                table.Location = new Point(wposition, position + 4);
                table.Name = $"Table_ID{val.Key}";
                table.Size = new Size(60, 30);
                Bitmap bmp = new Bitmap(table.Width, table.Height);
                Bitmap block = new Bitmap(16, 16);
                if (val.Key < 500 || val.Key >= 1001)
                {
                    if (MainForm.ForegroundBlocks.ContainsKey(val.Key) && (id == 0 || id == 1))
                    {
                        block = MainForm.ForegroundBlocks[val.Key];
                        using (Graphics gr = Graphics.FromImage(bmp))
                        {
                            gr.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(5, 5, 100, 50));
                            gr.DrawRectangle(new Pen(Color.White), new Rectangle(5, 5, 54, 24));
                            gr.DrawImage(block, new Point(8, 8));
                            //gr.DrawString($"{val.Value}", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.Black), new Point(25, 9));
                            gr.DrawString($"{val.Value}", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.White), new Point(24, 8));
                        }
                        table.Image = bmp;
                        wposition += 60;
                        if (wposition == 244) //244
                        {
                            wposition = 4;
                            position += 30;
                        }
                        panel1.Controls.Add(table);
                    }
                    
                    if (MainForm.ActionBlocks.ContainsKey(val.Key) && (id == 0 || id == 2))
                    {
                        
                        block = MainForm.ActionBlocks[val.Key];
                        using (Graphics gr = Graphics.FromImage(bmp))
                        {
                            gr.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(5, 5, 100, 50));
                            gr.DrawRectangle(new Pen(Color.White), new Rectangle(5, 5, 54, 24));
                            gr.DrawImage(block, new Point(8, 8));
                            //gr.DrawString($"{val.Value}", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.Black), new Point(25, 9));
                            gr.DrawString($"{val.Value}", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.White), new Point(24, 8));
                        }
                        table.Image = bmp;
                        wposition += 60;
                        if (wposition == 244) //244
                        {
                            wposition = 4;
                            position += 30;
                        }
                        panel1.Controls.Add(table);
                    }
                    if (MainForm.DecorationBlocks.ContainsKey(val.Key) && (id == 0 || id == 3))
                    {
                        block = MainForm.DecorationBlocks[val.Key];
                        using (Graphics gr = Graphics.FromImage(bmp))
                        {
                            gr.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(5, 5, 100, 50));
                            gr.DrawRectangle(new Pen(Color.White), new Rectangle(5, 5, 54, 24));
                            gr.DrawImage(block, new Point(8, 8));
                            //gr.DrawString($"{val.Value}", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.Black), new Point(25, 9));
                            gr.DrawString($"{val.Value}", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.White), new Point(24, 8));
                        }
                        table.Image = bmp;
                        wposition += 60;
                        if (wposition == 244) //244
                        {
                            wposition = 4;
                            position += 30;
                        }
                        panel1.Controls.Add(table);
                    }

                }
                else
                {
                    if (MainForm.BackgroundBlocks.ContainsKey(val.Key) && val.Key != 0 && (id == 0 || id == 4))
                    {
                        block = MainForm.BackgroundBlocks[val.Key];
                        using (Graphics gr = Graphics.FromImage(bmp))
                        {
                            gr.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(5, 5, 100, 50));
                            gr.DrawRectangle(new Pen(Color.White), new Rectangle(5, 5, 54, 24));
                            gr.DrawImage(block, new Point(8, 8));
                            //gr.DrawString($"{val.Value}", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.Black), new Point(25, 9));
                            gr.DrawString($"{val.Value}", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.White), new Point(24, 8));
                        }
                        table.Image = bmp;
                        wposition += 60;
                        if (wposition == 244) //244
                        {
                            wposition = 4;
                            position += 30;
                        }
                        panel1.Controls.Add(table);
                    }
                }


            }
        }

        private void fgradioButton_CheckedChanged(object sender, EventArgs e)
        {

            sortby(1);
        }

        private void actradioButton_CheckedChanged(object sender, EventArgs e)
        {

            sortby(2);
        }

        private void decorradioButton_CheckedChanged(object sender, EventArgs e)
        {

            sortby(3);
        }

        private void bgradioButton_CheckedChanged(object sender, EventArgs e)
        {
            sortby(4);
        }
    }
}
