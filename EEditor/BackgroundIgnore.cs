using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace EEditor
{
    public partial class BackgroundIgnore : Form
    {
        private ImageList imglist = new ImageList();
        public BackgroundIgnore()
        {
            InitializeComponent();
        }

        private void BackgroundIgnore_Load(object sender, EventArgs e)
        {
            loaddata();
        }

        private void loaddata()
        {
            Console.WriteLine(MainForm.userdata.IgnoreBlocks.Count);
            listView1.View = View.Tile;
            listView1.TileSize = new Size(200, 24);
            listView1.Items.Clear();
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
                listView1.LargeImageList = imglist;
            }

            if (MainForm.userdata.IgnoreBlocks.Count > 0)
            {
                for (int i = 0; i < MainForm.userdata.IgnoreBlocks.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem() {
                        Text = "BlockID: " + MainForm.userdata.IgnoreBlocks[i],
                        Name = MainForm.userdata.IgnoreBlocks[i].ToString(),
                        ImageIndex = (int)MainForm.userdata.IgnoreBlocks[i]
                    };

                    listView1.Items.Add(lvi);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                JToken val = Convert.ToInt32(listView1.Items[0].Name);
                MainForm.userdata.IgnoreBlocks.Remove(val);
                loaddata();
            }
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            MainForm.userdata.IgnoreBlocks.Clear();
            loaddata();
        }
    }
}
