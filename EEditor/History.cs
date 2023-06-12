using System;
using System.Drawing;
using System.Windows.Forms;
namespace EEditor
{
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
        }

        public static ImageList imglist = new ImageList();
        //public static ImageList imgl { get { return imglist; } set { imglist = value; } }
        private void History_Load(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
            listView2.View = View.Tile;
            listView1.TileSize = new Size(200, 36);
            listView2.TileSize = new Size(200, 36);
            var width = MainForm.decosBMD.Width / 16 + MainForm.miscBMD.Width / 16 + MainForm.foregroundBMD.Width / 16 + MainForm.backgroundBMD.Width / 16;
            if (imglist.Images.Count == 0)
            {
                Bitmap img1 = new Bitmap(width, 16);
                for (int i = 0; i < width; i++)
                {
                    if (i < 500 || i >= 1001)
                    {
                        if (MainForm.decosBMI[i] != 0)
                        {
                            img1 = MainForm.decosBMD.Clone(new Rectangle(MainForm.decosBMI[i] * 16, 0, 16, 16), MainForm.decosBMD.PixelFormat);
                        }
                        else if (MainForm.miscBMI[i] != 0)
                        {
                            img1 = MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[i] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
                        }
                        else if (MainForm.foregroundBMI[i] != 0)
                        {
                            img1 = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[i] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                        }
                    }
                    else if (i >= 500 && i <= 999)
                    {
                        if (MainForm.backgroundBMI[i] != 0)
                        {
                            img1 = MainForm.backgroundBMD.Clone(new Rectangle(MainForm.backgroundBMI[i] * 16, 0, 16, 16), MainForm.backgroundBMD.PixelFormat);
                        }
                    }
                    imglist.Images.Add(img1);
                }
            }
            /*imglist.ImageSize = new Size(16, 16);
            listView1.LargeImageList = imglist;
            listView2.LargeImageList = imglist;
            string[] arr1 = ToolPen.redolist.ToArray();
            string[] arr = ToolPen.undolist.ToArray();
            int incr2 = 0;
            if (arr.Length > 0)
            {
                for (int a = 0; a < arr.Length; a++)
                {
                    if (arr[a].Contains(':'))
                    {
                        string[] split = arr[a].Split(':');
                        if (split.Length == 4)
                        {
                            listView1.Items.Add("PenID: " + split[0] + " Before: " + split[1] + " X: " + split[2] + " Y: " + split[3]);
                            listView1.Items[incr2].ImageIndex = Convert.ToInt32(split[0]);
                            incr2 += 1;
                        }
                        else
                        {
                            int bidAfter = 0;
                            int bidBefore = 0;
                            int xx = 0;
                            int yy = 0;
                            int incr = 0;
                            for (int i = 0; i < split.Length; i++)
                            {
                                if (split[i] != "")
                                {
                                    if (incr == 0)
                                    {
                                        bidAfter = Convert.ToInt32(split[i]);
                                        incr += 1;
                                    }
                                    else if (incr == 1)
                                    {
                                        bidBefore = Convert.ToInt32(split[i]);
                                        incr += 1;
                                    }
                                    else if (incr == 2)
                                    {
                                        xx = Convert.ToInt32(split[i]);
                                        incr += 1;
                                    }
                                    else if (incr == 3)
                                    {

                                        yy = Convert.ToInt32(split[i]);
                                        listView1.Items.Add("PenID: " + bidAfter + " Before: " + bidBefore + " X: " + xx + " Y: " + yy);
                                        listView1.Items[incr2].ImageIndex = Convert.ToInt32(bidAfter);
                                        bidAfter = 0;
                                        bidBefore = 0;
                                        xx = 0;
                                        yy = 0;
                                        incr = 0;
                                        incr2 += 1;


                                    }
                                }
                            }
                        }
                    }
                }
            }
            incr2 = 0;
            if (arr1.Length > 0) {
                for (int a = 0; a < arr1.Length; a++)
                {
                    if (arr1[a].Contains(':'))
                    {
                        string[] split = arr1[a].Split(':');
                        if (split.Length == 4)
                        {
                            listView2.Items.Add("PenID: " + split[0] + " Before: " + split[1] + " X: " + split[2] + " Y: " + split[3]);
                            listView2.Items[incr2].ImageIndex = Convert.ToInt32(split[0]);
                            incr2 += 1;
                        }
                        else
                        {
                            int bidAfter = 0;
                            int bidBefore = 0;
                            int xx = 0;
                            int yy = 0;
                            int incr = 0;
                            for (int i = 0; i < split.Length; i++)
                            {
                                if (split[i] != "")
                                {
                                    if (incr == 0)
                                    {
                                        bidAfter = Convert.ToInt32(split[i]);
                                        incr += 1;
                                    }
                                    else if (incr == 1)
                                    {
                                        bidBefore = Convert.ToInt32(split[i]);
                                        incr += 1;
                                    }
                                    else if (incr == 2)
                                    {
                                        xx = Convert.ToInt32(split[i]);
                                        incr += 1;
                                    }
                                    else if (incr == 3)
                                    {

                                        yy = Convert.ToInt32(split[i]);
                                        listView2.Items.Add("PenID: " + bidAfter + " Before: " + bidBefore + " X: " + xx + " Y: " + yy);
                                        listView2.Items[incr2].ImageIndex = Convert.ToInt32(bidAfter);
                                        bidAfter = 0;
                                        bidBefore = 0;
                                        xx = 0;
                                        yy = 0;
                                        incr = 0;
                                        incr2 += 1;


                                    }
                                }
                            }
                        }
                    }
                }
            }*/
            //listView1.Items[0].ImageIndex = 512;
            //listView1
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                string[] list = listView1.SelectedItems[0].Text.Split(' ');
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ToolPen.undolist.Clear();
            listView1.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ToolPen.redolist.Clear();
            listView2.Items.Clear();
        }
    }
}
