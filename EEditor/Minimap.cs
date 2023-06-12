using System;
using System.Drawing;
using System.Windows.Forms;

namespace EEditor
{
    public partial class Minimap : UserControl
    {
        public int BlockWidth { get; set; }
        public int BlockHeight { get; set; }
        public static Bitmap Bitmap { get; set; }
        public static uint[] Colors = new uint[3000];
        public static bool[] ImageColor = new bool[Colors.Length];
        private bool mouseDown { get; set; } = false;

        static Minimap()
        {
            for (int i = 0; i < Colors.Length; ++i)
            {
                Colors[i] = 321;
            }
        }

        public Minimap()
        {
            InitializeComponent();
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        public void Init(int width, int height)
        {
            BlockWidth = width;
            BlockHeight = height;
            Size = new Size(width, height);
            Bitmap = new Bitmap(width, height);
            for (int x = 0; x < width; ++x) for (int y = 0; y < height; ++y) Bitmap.SetPixel(x, y, Color.Black);
            Point relativePos = new Point(-25, -25);
            Location = new Point(Parent.ClientSize.Width - Width + relativePos.X, Parent.ClientSize.Height - Height + relativePos.Y);

        }

        public void SetPixel(int x, int y, int id)
        {
            uint color = 4278190080;
            if (id < Colors.Length && Colors[id] != 321) color = Colors[id];

            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { Bitmap.SetPixel(x, y, Color.FromArgb((int)color)); }); }
            else { Bitmap.SetPixel(x, y, Color.FromArgb((int)color)); }
            Invalidate(new Rectangle(x, y, 1, 1));
        }

        public void SetColor(int x, int y, Color color)
        {
            Bitmap.SetPixel(x, y, color);
            Invalidate(new Rectangle(x, y, 1, 1));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(Bitmap, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
        }

        private void Minimap_Load(object sender, EventArgs e)
        {

        }

        private void Minimap_Click(object sender, EventArgs e)
        {
        }

        private void Minimap_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                MainForm.editArea.AutoScrollPosition = new Point((e.X * 16) - 768, (e.Y * 16) - 256);
            }
        }

        private void Minimap_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void Minimap_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Minimap_MouseClick(object sender, MouseEventArgs e)
        {
            MainForm.editArea.AutoScrollPosition = new Point((e.X * 16) - 768, (e.Y * 16) - 256);
        }
    }
}
