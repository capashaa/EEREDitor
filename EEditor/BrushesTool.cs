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
    public partial class BrushesTool : Form
    {
        public BrushesTool()
        {
            InitializeComponent();
        }

        private Bitmap bmp = new Bitmap(3000,3000);
        private void Brushes_Load(object sender, EventArgs e)
        {
            if (Clipboard.ContainsData("EEBrush"))
            {
                string[][,] data = (string[][,])Clipboard.GetData("EEBrush");
                if (data?.Length == 6)
                {
                    for (int y = 0;y < data[0].GetLength(0);y++)
                    {
                        for (int x = 0;x < data[0].GetLength(1); x++)
                        {
                            using (Graphics gr = Graphics.FromImage(bmp))
                            {
                                gr.DrawImage(MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[Convert.ToInt32(data[0][y, x])] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat), x * 16, y * 16);
                            }
                        }
                    }
                    //Clipboard.Clear();
                    //Console.WriteLine(data[0]);
                }
                pictureBox1.Image = bmp;
            }
        }
    }
}
