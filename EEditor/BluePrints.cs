using EELVL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EEditor
{
    public partial class BluePrints : Form
    {
        private string[,] foreground = new string[1, 1];
        private string[,] background = new string[1, 1];
        private string[,] Coins = new string[1, 1];
        private string[,] Id1 = new string[1, 1];
        private string[,] Target1 = new string[1, 1];
        private string[,] Text1 = new string[1, 1];
        private string[,] Text2 = new string[1, 1];
        private string[,] Text3 = new string[1, 1];
        private string[,] Text4 = new string[1, 1];
        bool fileworks = false;
        private Bitmap img4 = new Bitmap(25 * 16, 25 * 16);
        private Bitmap img1 = new Bitmap(25 * 16, 25 * 16);
        private BlockContainer container = new BlockContainer();

        public BluePrints()
        {
            InitializeComponent();
            var container = new BlockContainer() { Blocks = new List<Block>() };
        }

        private void BluePrints_Load(object sender, EventArgs e)
        {
            this.BackColor = MainForm.themecolors.background;
            this.ForeColor = MainForm.themecolors.foreground;
            LoadButton.FlatStyle = FlatStyle.Flat;
            LoadButton.ForeColor = MainForm.themecolors.foreground;
            LoadButton.BackColor = MainForm.themecolors.background;
            buttonImport.FlatStyle = FlatStyle.Flat;
            buttonImport.ForeColor = MainForm.themecolors.foreground;
            buttonImport.BackColor = MainForm.themecolors.background;
            SaveBPButton.FlatStyle = FlatStyle.Flat;
            SaveBPButton.ForeColor = MainForm.themecolors.foreground;
            SaveBPButton.BackColor = MainForm.themecolors.background;
            tBOwner.BackColor = MainForm.themecolors.background;
            tBOwner.ForeColor = MainForm.themecolors.foreground;

            LoadButton.Enabled = true;
            buttonImport.Enabled = false;
            SaveBPButton.Enabled = false;
            Bitmap bmp = new Bitmap(25 * 16, 25 * 16);
            Graphics gr2 = Graphics.FromImage(bmp);
            gr2.Clear(Color.DarkGray);
            pictureBox1.Image = bmp;

            if (Clipboard.ContainsData("EEBlueprints"))
            {
                LoadButton.Enabled = false;
                buttonImport.Enabled = false;
                SaveBPButton.Enabled = false;
                string[][,] data = (string[][,])Clipboard.GetData("EEBlueprints");
                if (data?.Length == 9)
                {
                    if (data[0].GetLength(1) <= 25 && data[0].GetLength(0) <= 25)
                    {
                        using (Graphics gr = Graphics.FromImage(img4))
                        {
                            gr.Clear(Color.Gray);
                        }
                        container.width = data[0].GetLength(1);
                        container.height = data[0].GetLength(0);
                        for (int y = 0; y < data[1].GetLength(0); y++)
                        {
                            for (int x = 0; x < data[1].GetLength(1); x++)
                            {
                                int bid1 = Convert.ToInt32(data[1][y, x]);


                                if (bid1 >= 500 && bid1 <= 999)
                                {
                                    if (MainForm.backgroundBMI[bid1] != 0 || bid1 == 500)
                                    {
                                        container.Blocks.Add(new Block() { BlockID = bid1, Layer = 1, X = x, Y = y });
                                        using (Graphics gr1 = Graphics.FromImage(img4))
                                        {
                                            gr1.DrawImage(MainForm.backgroundBMD.Clone(new Rectangle(MainForm.backgroundBMI[bid1] * 16, 0, 16, 16), MainForm.backgroundBMD.PixelFormat), x * 16, y * 16);
                                        }
                                    }
                                }
                            }

                        }
                        for (int y = 0; y < data[0].GetLength(0); y++)
                        {
                            for (int x = 0; x < data[0].GetLength(1); x++)
                            {
                                int bid = Convert.ToInt32(data[0][y, x]);

                                using (Graphics gr = Graphics.FromImage(img4))
                                {
                                    if (bid < 500 || bid >= 1001)
                                    {
                                        if (MainForm.decosBMI[bid] != 0)
                                        {
                                            img1 = bdata.getRotation(bid, Convert.ToInt32(data[2][y, x]));
                                            if (img1 != null)
                                            {
                                                gr.DrawImage(img1, new Rectangle(x * 16, y * 16, 16, 16));
                                                container.Blocks.Add(new Block() { BlockID = bid, Layer = 0, X = x, Y = y, Param = Convert.ToInt32(data[2][y, x]) });
                                            }
                                            else
                                            {
                                                container.Blocks.Add(new Block() { BlockID = bid, Layer = 0, X = x, Y = y });
                                                gr.DrawImage(MainForm.decosBMD.Clone(new Rectangle(MainForm.decosBMI[Convert.ToInt32(data[0][y, x])] * 16, 0, 16, 16), MainForm.decosBMD.PixelFormat), x * 16, y * 16);
                                            }
                                        }
                                        else if (MainForm.miscBMI[bid] != 0 || bid == 119)
                                        {
                                            img1 = bdata.getRotation(bid, Convert.ToInt32(data[2][y, x]));
                                            if (img1 != null)
                                            {
                                                gr.DrawImage(img1, new Rectangle(x * 16, y * 16, 16, 16));
                                                container.Blocks.Add(new Block() { BlockID = bid, Layer = 0, X = x, Y = y, Param = Convert.ToInt32(data[2][y, x]) });
                                            }
                                            else
                                            {
                                                container.Blocks.Add(new Block() { BlockID = bid, Layer = 0, X = x, Y = y });
                                                gr.DrawImage(MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[Convert.ToInt32(data[0][y, x])] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat), x * 16, y * 16);
                                            }
                                        }
                                        else if (bid != 0)
                                        {
                                            container.Blocks.Add(new Block() { BlockID = bid, Layer = 0, X = x, Y = y });
                                            gr.DrawImage(MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[Convert.ToInt32(data[0][y, x])] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat), x * 16, y * 16);
                                        }
                                    }
                                }
                            }
                        }
                        using (Graphics gr = Graphics.FromImage(img4))
                        {
                            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, data[0].GetLength(1) * 16 - 1, data[0].GetLength(0) * 16 - 1));
                        }
                        //Clipboard.Clear();
                        //Console.WriteLine(data[0]);
                        pictureBox1.Image = img4;
                        pictureBox1.Width = data[0].GetLength(1) * 16;
                        pictureBox1.Height = data[0].GetLength(0) * 16;
                        SaveBPButton.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Your BluePrint is too big. Max size: 25 x 25");
                        Clipboard.Clear();
                        this.Close();
                    }
                    //;
                    //Console.WriteLine(data[0]);
                }

                Clipboard.Clear();
            }

            // Clipboard.Clear();
        }

        private void BluePrints_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void SaveBPButton_Click(object sender, EventArgs e)
        {

            if (container.Blocks.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "BluePrints|*.json";
                sfd.InitialDirectory = $"{Directory.GetCurrentDirectory()}\\blueprints";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    container.owner = tBOwner.Text;

                    File.WriteAllText($"{sfd.FileName}", JsonConvert.SerializeObject(container, Newtonsoft.Json.Formatting.Indented));
                    tBOwner.Clear();
                    Bitmap bmp = new Bitmap(25 * 16, 25 * 16);
                    Graphics gr = Graphics.FromImage(bmp);
                    gr.Clear(Color.DarkGray);
                    pictureBox1.Image = bmp;
                    LoadButton.Enabled = true;
                    buttonImport.Enabled = false;
                    SaveBPButton.Enabled = false;
                }
            }
            else
            {
                tBOwner.Clear();
                Bitmap bmp = new Bitmap(25 * 16, 25 * 16);
                Graphics gr = Graphics.FromImage(bmp);
                gr.Clear(Color.DarkGray);
                pictureBox1.Image = bmp;
                LoadButton.Enabled = true;
                buttonImport.Enabled = false;
                SaveBPButton.Enabled = false;
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            SaveBPButton.Enabled = false;
            buttonImport.Enabled = false;
            LoadButton.Enabled = false;
            Bitmap bmp = new Bitmap(25 * 16, 25 * 16);
            Graphics gr2 = Graphics.FromImage(bmp);
            gr2.Clear(Color.DarkGray);
            pictureBox1.Image = bmp;
            string json = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "BluePrints|*.json";
            ofd.InitialDirectory = $"{Directory.GetCurrentDirectory()}\\blueprints";
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                var serializer = new JsonSerializer();
                using (var sw = new StreamReader(ofd.FileName))
                using (var reader = new JsonTextReader(sw))
                {
                    json = serializer.Deserialize(reader).ToString();
                }


                if (json != null)
                {
                    try
                    {
                        container = JsonConvert.DeserializeObject<BlockContainer>(json);
                        if (container.width <= 25 && container.height <= 25)
                        {
                            tBOwner.Text = container.owner;
                            Bitmap img4 = new Bitmap(container.width * 16, container.height * 16);

                            foreground = new string[container.height, container.width];
                            background = new string[container.height, container.width];
                            Coins = new string[container.height, container.width];
                            Id1 = new string[container.height, container.width];
                            Target1 = new string[container.height, container.width];
                            Text1 = new string[container.height, container.width];
                            Text2 = new string[container.height, container.width];
                            Text3 = new string[container.height, container.width];
                            Text4 = new string[container.height, container.width];



                            using (Graphics gr = Graphics.FromImage(img4))
                            {
                                gr.Clear(Color.DarkGray);
                            }
                            foreach (var data in container.Blocks)
                            {
                                if (data.Layer == 1)
                                {
                                    if (data.BlockID >= 500 && data.BlockID <= 999)
                                    {
                                        if (MainForm.backgroundBMI[data.BlockID] != 0 || data.BlockID == 500)
                                        {
                                            background[data.Y, data.X] = Convert.ToString(data.BlockID);
                                            using (Graphics gr1 = Graphics.FromImage(img4))
                                            {
                                                gr1.DrawImage(MainForm.backgroundBMD.Clone(new Rectangle(MainForm.backgroundBMI[data.BlockID] * 16, 0, 16, 16), MainForm.backgroundBMD.PixelFormat), data.X * 16, data.Y * 16);
                                            }
                                        }
                                    }
                                }
                                if (data.Layer == 0)
                                {
                                    using (Graphics gr = Graphics.FromImage(img4))
                                    {
                                        if (data.BlockID < 500 || data.BlockID >= 1001)
                                        {

                                            if (MainForm.decosBMI[data.BlockID] != 0)
                                            {
                                                img1 = bdata.getRotation(data.BlockID, Convert.ToInt32(data.Param));
                                                if (img1 != null)
                                                {
                                                    foreground[data.Y, data.X] = Convert.ToString(data.BlockID);
                                                    Coins[data.Y, data.X] = Convert.ToString(data.Param);
                                                    gr.DrawImage(img1, new Rectangle(data.X * 16, data.Y * 16, 16, 16));

                                                }
                                                else
                                                {
                                                    foreground[data.Y, data.X] = Convert.ToString(data.BlockID);
                                                    gr.DrawImage(MainForm.decosBMD.Clone(new Rectangle(MainForm.decosBMI[data.BlockID] * 16, 0, 16, 16), MainForm.decosBMD.PixelFormat), data.X * 16, data.Y * 16);
                                                }
                                            }
                                            else if (MainForm.miscBMI[data.BlockID] != 0 || data.BlockID == 119)
                                            {
                                                img1 = bdata.getRotation(data.BlockID, Convert.ToInt32(data.Param));
                                                if (img1 != null)
                                                {
                                                    foreground[data.Y, data.X] = Convert.ToString(data.BlockID);
                                                    Coins[data.Y, data.X] = Convert.ToString(data.Param);
                                                    gr.DrawImage(img1, new Rectangle(data.X * 16, data.Y * 16, 16, 16));

                                                }
                                                else
                                                {
                                                    foreground[data.Y, data.X] = Convert.ToString(data.BlockID);
                                                    gr.DrawImage(MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[Convert.ToInt32(data.BlockID)] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat), data.X * 16, data.Y * 16);
                                                }
                                            }
                                            else if (data.BlockID != 0)
                                            {
                                                foreground[data.Y, data.X] = Convert.ToString(data.BlockID);
                                                gr.DrawImage(MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[Convert.ToInt32(data.BlockID)] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat), data.X * 16, data.Y * 16);
                                            }
                                        }
                                    };
                                }
                            }
                            using (Graphics gr = Graphics.FromImage(img4))
                            {
                                gr.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, container.width * 16 - 1, container.height * 16 - 1));
                            }
                            pictureBox1.Image = img4;
                            pictureBox1.Width = container.width * 16;
                            pictureBox1.Height = container.height * 16;
                            fileworks = true;
                            buttonImport.Enabled = true;
                            LoadButton.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Your BluePrint is too big. Max size: 25 x 25");
                            LoadButton.Enabled = true;
                        }
                    }
                    catch
                    {
                        fileworks = false;
                        buttonImport.Enabled = false;
                        LoadButton.Enabled = true;
                    }


                }
            }
        }


        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (foreground.Length > 0 && fileworks)
            {
                Clipboard.SetData("EEData", new string[][,] { foreground, background, Coins, Id1, Target1, Text1, Text2, Text3, Text4 });
                this.Close();
            }
        }
    }
    public class Block
    {
        public int BlockID { get; set; }
        public int Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Param { get; set; }
    }

    public class BlockContainer
    {
        public List<Block> Blocks { get; set; } = new List<Block>();
        public string owner { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
