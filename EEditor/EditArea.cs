using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Imaging;

namespace EEditor
{
    public partial class EditArea : UserControl
    {
        public int BlockWidth { get; set; }
        public int BlockHeight { get; set; }
        public Frame Background { get { return Frames[0]; } }
        public Frame CurFrame { get { return Frames[curFrame]; } }
        public int[,] mouseBlocksF = new int[1, 1];
        public int[,] mouseBlocksB = new int[1, 1];
        public bool started = false;
        public bool IsBackground { get { return curFrame == 0; } }
        public List<Frame> Frames { get; set; }
        public Tool Tool { get; set; }
        public Bitmap Back { get; set; }
        public Bitmap Back1 { get; set; }

        public Bitmap Label { get; set; }
        public Bitmap adminText { get; set; }
        public Bitmap[] Bricks { get; set; }
        public Bitmap unknowBricks { get; set; }
        public Bitmap misc { get; set; }
        public Bitmap[] BricksFade { get; set; }
        public Minimap Minimap { get; set; }
        public MainForm MainForm { get; set; }
        public int mX { get; set; }
        public int mY { get; set; }
        public Rectangle reco { get; set; }
        public bool mouseDown { set { IsMouseDown = value; } }
        public string incfg = null;
        protected int curFrame;
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();
        PrivateFontCollection bfont = new PrivateFontCollection();

        protected bool IsMouseDown = false;
        protected bool IsRightDown = false;
        protected bool IsMouseUp = false;
        Font fontz;
        Rectangle rect;

        public EditArea(MainForm mainForm)
        {
            InitializeComponent();
            this.MainForm = mainForm;

            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer, true);

            Bricks = new Bitmap[4000];
            BricksFade = new Bitmap[6000];
            Tool = new ToolPen(this) { PenID = 9 };
            Frames = new List<Frame>();

            VerticalScroll.SmallChange = 8;
            HorizontalScroll.SmallChange = 8;
            Timer scrollTimer = new Timer() { Interval = 15 };
            scrollTimer.Tick += scrollTimer_Tick;
            scrollTimer.Start();
            this.AllowDrop = true;
            this.DragEnter += EditArea_DragEnter;
            this.DragDrop += EditArea_DragDrop;
            this.Visible = true;



        }

        private void EditArea_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void EditArea_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1)
            {
                if (Regex.IsMatch(Path.GetExtension(files[0]).ToLower(), "^.jpg$|^.png$|^.ico$|^.jpeg$|^.gif$|^.bmp$"))
                {
                    InsertImageForm.path2file = files[0];
                    InsertImageForm open = new InsertImageForm();
                    open.ShowDialog();
                }
            }
        }

        [DllImport("user32.dll")]
        public static extern ushort GetAsyncKeyState(int vKey);
        public static bool IsKeyDown(Keys key) { return IsKeyDown((int)key); }
        public static bool IsKeyDown(int key) { return (GetAsyncKeyState(key) & 0x8000) != 0; }

        static int dVScroll = 0;
        static int dHScroll = 0;
        const int dScrollMax = 85;

        void scrollTimer_Tick(object sender, EventArgs e)
        {
            if (!Focused) return;
            bool keyW = IsKeyDown(Keys.W);
            bool keyS = IsKeyDown(Keys.S);

            if (keyW ^ keyS)
            {
                if (dVScroll < dScrollMax) dVScroll += 1;
                AutoScrollPosition = new Point(Math.Abs(AutoScrollPosition.X), Math.Abs(AutoScrollPosition.Y) + (keyS ? dVScroll : -dVScroll));
            }
            else dVScroll = 0;

            bool keyA = IsKeyDown(Keys.A);
            bool keyD = IsKeyDown(Keys.D);
            if (keyA ^ keyD)
            {
                if (dHScroll < dScrollMax) dHScroll += 1;
                AutoScrollPosition = new Point(Math.Abs(AutoScrollPosition.X) + (keyD ? dHScroll : -dHScroll), Math.Abs(AutoScrollPosition.Y));
            }
            else dHScroll = 0;
            if (IsMouseDown && Tool is ToolPen)
            {
                Point mouse = PointToClient(MousePosition);
                if (ClientRectangle.Contains(mouse))
                {
                    mouse = Tool.GetLocation(mouse);
                    if (prevLocation.X >= 0 && prevLocation != mouse)
                        DrawLine(prevLocation, mouse);
                    prevLocation = mouse;
                }
                else
                {
                    prevLocation = new Point(-1, -1);
                }
            }
            else
            {
                prevLocation = new Point(-1, -1);
            }
        }

        private Point prevLocation = new Point(-1, -1);

        void DrawLine(Point P, Point Q)
        {

            int x0 = P.X, y0 = P.Y;
            int x1 = Q.X, y1 = Q.Y;
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;
            List<int> xs = new List<int>();
            List<int> ys = new List<int>();
            while (true)
            {
                xs.Add(x0);
                ys.Add(y0);
                if (x0 == x1 && y0 == y1) break;
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
            Rectangle r = new Rectangle(0, 0, MainForm.Zoom, MainForm.Zoom);
            Graphics g = Graphics.FromImage(Back);
            for (int i = 0; i < xs.Count; ++i)
            {
                if (Tool.PenID >= 500 && Tool.PenID <= 999)
                {
                    if (Tool.PenID != CurFrame.Background[ys[i], xs[i]])
                    {
                        incfg += Tool.PenID + ":" + CurFrame.Background[ys[i], xs[i]] + ":" + xs[i] + ":" + ys[i] + ":";
                    }
                    if (Tool.PenSize == 1) CurFrame.Background[ys[i], xs[i]] = Tool.PenID;
                    else if (Tool.PenSize >= 2 && Tool.PenSize <= 10)
                    {
                        for (int yy = 0; yy < Tool.PenSize; yy++)
                        {
                            for (int xx = 0; xx < Tool.PenSize; xx++)
                            {
                                if (xs[i] + xx <= CurFrame.Width - 1 && ys[i] + yy <= CurFrame.Height - 1)
                                {
                                    CurFrame.Background[ys[i] + yy, xs[i] + xx] = Tool.PenID;
                                }
                            }
                        }
                    }
                }
                if (Tool.PenID < 500 || Tool.PenID >= 1001)
                {
                    if (Tool.PenID != 83 && Tool.PenID != 77 && Tool.PenID != 1520)
                    {
                        if (ToolPen.rotation.ContainsKey(Tool.PenID))
                        {
                            CurFrame.BlockData[ys[i], xs[i]] = ToolPen.rotation[Tool.PenID];
                        }
                        if (!ToolPen.rotation.ContainsKey(Tool.PenID)) { CurFrame.BlockData[ys[i], xs[i]] = 0; }
                        if (ToolPen.text.ContainsKey(Tool.PenID))
                        {
                            CurFrame.BlockData3[ys[i], xs[i]] = ToolPen.text[Tool.PenID];
                        }
                        if (!ToolPen.text.ContainsKey(Tool.PenID)) { CurFrame.BlockData3[ys[i], xs[i]] = "Unknown"; }
                        if (ToolPen.id.ContainsKey(Tool.PenID) && ToolPen.target.ContainsKey(Tool.PenID))
                        {
                            CurFrame.BlockData[ys[i], xs[i]] = ToolPen.rotation[Tool.PenID];
                            CurFrame.BlockData1[ys[i], xs[i]] = ToolPen.id[Tool.PenID];
                            CurFrame.BlockData2[ys[i], xs[i]] = ToolPen.target[Tool.PenID];
                        }
                        if (Tool.PenID != CurFrame.Foreground[ys[i], xs[i]])
                        {
                            incfg += Tool.PenID + ":" + CurFrame.Foreground[ys[i], xs[i]] + ":" + xs[i] + ":" + ys[i] + ":";
                        }

                        if (Tool.PenSize == 1)
                        {
                            CurFrame.Foreground[ys[i], xs[i]] = Tool.PenID;
                        }
                        else if (Tool.PenSize >= 2 && Tool.PenSize <= 10)
                        {
                            for (int yy = 0; yy < Tool.PenSize; yy++)
                            {
                                for (int xx = 0; xx < Tool.PenSize; xx++)
                                {
                                    if (xs[i] + xx <= CurFrame.Width - 1 && ys[i] + yy <= CurFrame.Height - 1)
                                    {
                                        CurFrame.Foreground[ys[i] + yy, xs[i] + xx] = Tool.PenID;
                                    }
                                }
                            }
                        }
                    }
                }

                if (Tool.PenSize == 1) Draw(xs[i], ys[i], g, Color.Transparent);
                else if (Tool.PenSize >= 2 && Tool.PenSize <= 10)
                {
                    for (int yy = 0; yy < Tool.PenSize; yy++)
                    {
                        for (int xx = 0; xx < Tool.PenSize; xx++)
                        {
                            if (xs[i] + xx <= CurFrame.Width - 1 && ys[i] + yy <= CurFrame.Height - 1) { Draw(xs[i] + xx, ys[i] + yy, g, Color.Transparent); }
                        }
                    }
                }
            }

            g.Save();

            for (int i = 0; i < xs.Count; ++i)
            {
                r.X = xs[i];
                r.Y = ys[i];
            }
            Invalidate();
        }

        public bool ownedBid(int bid, int mode)
        {
            bool exists = false;
            if (MainForm.ownedb.Count > 0)
            {
                for (int i = 0; i < MainForm.ownedb.Count; i++)
                {
                    for (int o = 0; o < MainForm.ownedb[i].blocks.Length; o++)
                    {
                        if (MainForm.ownedb[i].blocks[o] == bid && MainForm.ownedb[i].mode == mode)
                        {

                            exists = true;
                            break;
                        }
                    }
                }
            }

            return exists;
        }

        public void hideCursor(bool hide)
        {
            if (hide) Cursor.Hide();
            else Cursor.Show();
        }

        public void Init(int width, int height)
        {
            BlockHeight = height;
            BlockWidth = width;
            int calc1 = BlockWidth * MainForm.Zoom;
            int calc2 = BlockHeight * MainForm.Zoom;
            int calc3 = calc1 + calc2;
            if (calc3 > 23170)
            {
                MessageBox.Show("Can't load this world. It's too big.");
            }
            else
            {
                Size size = new Size(BlockWidth * MainForm.Zoom, BlockHeight * MainForm.Zoom);
                Frame frame = new Frame(BlockWidth, BlockHeight);
                frame.Reset(false);
                Init(frame, false);
            }
        }

        public void Init(Frame frame, bool frme)
        {
            BlockHeight = frame.Height;
            BlockWidth = frame.Width;
            int calc1 = BlockWidth * MainForm.Zoom;
            int calc2 = BlockHeight * MainForm.Zoom;
            int calc3 = calc1 + calc2;
            if (calc3 > 23170)
            {
                started = false;
                MessageBox.Show("Can't load this world. It's too big.");
                
            }
            else
            {
                Frames.Clear();
                Frames.Add(frame);
                if (!string.IsNullOrEmpty(frame.levelname) && !string.IsNullOrEmpty(frame.owner)) MainForm.Text = $"({frame.levelname}) [{frame.owner}] ({frame.Width}x{frame.Height}) - EERditor {bdata.programVersion}";
                curFrame = 0;
                Size size = new Size(BlockWidth * MainForm.Zoom, BlockHeight * MainForm.Zoom);
                //Bricks = new Bitmap[3000];
                //BricksFade = new Bitmap[BlockWidth * MainForm.Zoom];
                Back = new Bitmap(BlockWidth * MainForm.Zoom, BlockHeight * MainForm.Zoom);
                Minimap.Init(BlockWidth, BlockHeight);
                PaintCurFrame();
                this.AutoScrollMinSize = size;
                this.Invalidate();
                started = true;
            }
        }
        public void zoomRefresh()
        {
            curFrame = 0;
            Size size = new Size(BlockWidth * MainForm.Zoom, BlockHeight * MainForm.Zoom);
            Back = new Bitmap(BlockWidth * MainForm.Zoom, BlockHeight * MainForm.Zoom);
            Minimap.Init(BlockWidth, BlockHeight);
            PaintCurFrame();
            this.AutoScrollMinSize = size;
            this.Invalidate();
            started = true;
        }

        protected void PaintCurFrame()
        {
            Graphics g = Graphics.FromImage(Back);
            for (int x = 0; x < BlockWidth; ++x)
                for (int y = 0; y < BlockHeight; ++y)
                    Draw(x, y, g, Color.Transparent);
            g.Save();
        }

        /*public void Draw(int x, int y, Graphics g)
        {
            bool faded = false;
            int id = CurFrame.Foreground[y, x];
            int bid = CurFrame.Background[y, x];
            if (id == 0 && !IsBackground)
            {
                id = Frames[curFrame - 1].Foreground[y, x];
                if (id != 0) faded = true;
            }
            Draw(x, y, g, bid, id, CurFrame.BlockData[y, x], faded);
        }*/

        #region draw
        public void Draw(int x, int y, Graphics g, int bid, int fid, int coins, int id, int target, string text, string text1, string text2, string text3, Color color)
        {
            //Graphics g = Graphics.FromImage(Back);
            if (Bricks[bid] == null || bid == -1)
            {
                if (bid >= 500 && bid <= 999 || bid >= 2507 && bid <= 2513)
                {
                    Bitmap bmp2 = new Bitmap(16, 16);
                    if (bid >= 500 && bid <= 999 && bid < 2500)
                    {
                        bmp2 = unknowBricks.Clone(new Rectangle(3 * 16, 0, 16, 16), unknowBricks.PixelFormat);
                    }
                    else if (bid >= 2507 && bid <= 2513)
                    {
                        bmp2 = unknowBricks.Clone(new Rectangle(4 * 16 + 1, 0, 16, 16), unknowBricks.PixelFormat);
                    }
                    g.DrawImage(bmp2, x * 16, y * 16);
                    if (MainForm.unknown.Count > 0)
                    {
                        unknownBlock bl = new unknownBlock(bid, 1, (int)coins, id, target, null);
                        if (!MainForm.unknown.Contains(bl))
                        {
                            MainForm.unknown.Add(bl);
                            if (!MainForm.userdata.newestBlocks.Contains(bid.ToString()))
                            {
                                MainForm.userdata.newestBlocks.Add(bid.ToString());
                            }
                        }
                    }
                    else
                    {
                        unknownBlock bl = new unknownBlock(bid, 1, (int)coins, id, target, null);
                        MainForm.unknown.Add(bl);
                        if (!MainForm.userdata.newestBlocks.Contains(bid.ToString()))
                        {
                            MainForm.userdata.newestBlocks.Add(bid.ToString());
                        }
                    }
                }
            }
            else if (bid >= 500 && bid <= 999 || bid == 0)
            {
                Bitmap bmp0 = new Bitmap(Bricks[bid], MainForm.Zoom, MainForm.Zoom);
                if (!MainForm.userdata.useColor)
                {

                    g.DrawImage(bmp0, x * MainForm.Zoom, y * MainForm.Zoom);
                }
                else
                {
                    if (bid != 0)
                    {
                        g.DrawImage(Bricks[bid], x * 16, y * 16);
                    }
                    else
                    {
                        //if (BlockWidth < x && BlockHeight < y) mouseBlocksB[x, y] = 0;
                        if (Color.Transparent == color)
                        {
                            if (MainForm.userdata.thisColor != Color.Transparent)
                            {
                                g.FillRectangle(new SolidBrush(MainForm.userdata.thisColor), new Rectangle(x * 16, y * 16, 16, 16));
                            }
                            else
                            {
                                g.DrawImage(Bricks[bid], x * 16, y * 16);
                            }
                        }
                        else
                        {
                            if (MainForm.userdata.thisColor != Color.Transparent)
                            {
                                g.FillRectangle(new SolidBrush(MainForm.userdata.thisColor), new Rectangle(x * 16, y * 16, 16, 16));
                            }
                            else
                            {
                                g.DrawImage(bmp0, x * MainForm.Zoom, y * MainForm.Zoom);
                            }
                        }
                    }
                }
            }
            /*if (fid == -1)
            {
                //g.DrawImage(Bricks[1], x * 16, y * 16);
            }*/
            if (fid > 0 && Bricks[fid] != null && fid != -1 && !bdata.ignore.Contains(fid) && !bdata.morphable.Contains(fid))
            {
                Bitmap bmp1 = new Bitmap(Bricks[fid], MainForm.Zoom, MainForm.Zoom);
                if (!MainForm.userdata.useColor)
                {

                    g.DrawImage(bmp1, x * MainForm.Zoom, y * MainForm.Zoom);
                }
                else
                {
                    if (fid != 0)
                    {
                        g.DrawImage(Bricks[fid], x * 16, y * 16);
                    }
                    else
                    {
                        if (Color.Transparent == color)
                        {
                            if (MainForm.userdata.thisColor != Color.Transparent)
                            {
                                g.FillRectangle(new SolidBrush(MainForm.userdata.thisColor), new Rectangle(x * 16, y * 16, 16, 16));
                            }
                            else
                            {
                                g.DrawImage(Bricks[fid], x * 16, y * 16);
                            }
                        }
                        else
                        {
                            if (MainForm.userdata.thisColor != Color.Transparent)
                            {
                                g.FillRectangle(new SolidBrush(MainForm.userdata.thisColor), new Rectangle(x * 16, y * 16, 16, 16));
                            }
                            else
                            {
                                g.DrawImage(Bricks[fid], x * 16, y * 16);
                            }
                        }
                    }
                }
            }
            else if (fid > 0 && fid != -1 && Bricks[fid] == null)
            {
                if (MainForm.decosBMI[fid] == 0 && MainForm.foregroundBMI[fid] == 0 && fid != 0 && MainForm.miscBMI[fid] == 0)
                {
                    if (fid < 500 || fid >= 1001 && fid < 2500)
                    {
                        Bitmap bmp2 = unknowBricks.Clone(new Rectangle(2 * 16, 0, 16, 16), unknowBricks.PixelFormat);
                        g.DrawImage(bmp2, x * 16, y * 16);
                        if (MainForm.unknown.Count > 0)
                        {
                            unknownBlock bl = new unknownBlock(fid, 0, (int)coins, id, target, null);
                            if (!MainForm.unknown.Contains(bl))
                            {
                                MainForm.unknown.Add(bl);
                                if (!MainForm.userdata.newestBlocks.Contains(fid.ToString()))
                                {
                                    MainForm.userdata.newestBlocks.Add(fid.ToString());
                                }
                            }
                        }
                        else
                        {
                            unknownBlock bl = new unknownBlock(fid, 0, (int)coins, id, target, null);
                            MainForm.unknown.Add(bl);
                            if (!MainForm.userdata.newestBlocks.Contains(fid.ToString()))
                            {
                                MainForm.userdata.newestBlocks.Add(fid.ToString());
                            }
                        }
                    }
                    else if (fid >= 2500)
                    {
                        Bitmap bmp2 = unknowBricks.Clone(new Rectangle(2 * 16, 0, 16, 16), unknowBricks.PixelFormat);
                        g.DrawImage(bmp2, x * 16, y * 16);
                        if (MainForm.unknown.Count > 0)
                        {
                            unknownBlock bl = new unknownBlock(fid, 0, (int)coins, id, target, null);
                            if (!MainForm.unknown.Contains(bl))
                            {
                                MainForm.unknown.Add(bl);
                                if (!MainForm.userdata.newestBlocks.Contains(fid.ToString()))
                                {
                                    MainForm.userdata.newestBlocks.Add(fid.ToString());
                                }
                            }
                        }
                        else
                        {
                            unknownBlock bl = new unknownBlock(fid, 0, (int)coins, id, target, null);
                            MainForm.unknown.Add(bl);
                            if (!MainForm.userdata.newestBlocks.Contains(fid.ToString()))
                            {
                                MainForm.userdata.newestBlocks.Add(fid.ToString());
                            }
                        }
                    }
                }
            }
            else if (fid == 0)
            {
            }
            if (bdata.goal.Contains(fid) && fid != 423 && fid != 417 && fid != 418 && fid != 419 && fid != 420 && fid != 421 && fid != 422 && fid != 453 && fid != 1027 && fid != 1028 && fid != 1582)
            {
                
                int offSet = coins >= 10 ? 4 : 9;
                if (fid == 467 || fid == 1079 || fid == 1080 || fid == 1012 || fid == 113 || fid == 184 || fid == 185 || fid == 461 || fid == 1619 || fid == 1620)
                {
                    
                    if (fid == 461 && coins == 0 || coins > 1 && coins <= 999)
                    {
                        DrawText(coins.ToString(), Back, Brushes.White, bfont.Families[0], 8, "bottom", "right", x * 16, y * 16, false);
                    }
                    else if (fid == 461 && coins == 1000)
                    {
                        DrawText("x", Back, Brushes.White, null, 8, "bottom", "center", x * 16, y * 16, false);
                    }
                    else if (fid == 461 && coins == 1)
                    {
                        Bitmap bmp3 = bdata.getRotation(fid, coins);
                        if (bmp3 != null) g.DrawImage(bmp3, x * 16, y * 16);
                    }
                    else
                    {
                        if (coins <= 999)
                        {
                            DrawText(coins.ToString(), Back, Brushes.White, bfont.Families[0], 8, "bottom", "right", x * 16, y * 16, false);
                        }
                        else
                        {
                            DrawText("999", Back, Brushes.White, bfont.Families[0], 8, "bottom", "right", x * 16, y * 16, false);
                        }
                    }

                    //g.DrawString(coins.ToString(), new Font("Courier", 6), Brushes.White, new PointF(x * 16 + offSet, y * 16 + 8));
                }

                else if (fid == 165 || fid == 43 || fid == 213 || fid == 214)
                {
                    if (coins <= 999)
                    {
                        DrawText(coins.ToString(), Back, fid == 43 ? Brushes.Black:Brushes.White, bfont.Families[0], 8, "bottom", "right", x * 16, y * 16, false);
                    }
                    else
                    {
                        DrawText("999", Back, fid == 43 ? Brushes.Black : Brushes.White, bfont.Families[0], 8, "bottom", "right", x * 16, y * 16, false);
                    }
                }
                else if (fid != 77 && fid != 83 && fid != 1520)
                {
                    //Console.WriteLine(bfont.Families[0]);
                    if (coins <= 999)
                    {
                        DrawText(coins.ToString(), Back, Brushes.Black, bfont.Families[0], 8, "bottom", "right", x * 16, y * 16, false);
                    }
                    else
                    {
                        DrawText("999", Back,Brushes.Black, bfont.Families[0], 8, "bottom", "right", x * 16, y * 16, false);
                    }
                }
            }
            else
            {
                if (fid == 1000)
                {
                    string texta = text;
                    g.DrawRectangle(new Pen(ColorTranslator.FromHtml(text1)), new Rectangle(x * 16, y * 16, 15, 15));
                }
                if (fid == 242 || fid == 381)
                {
                    Bitmap bmp3 = bdata.getRotation(fid, coins);
                    if (bmp3 != null) g.DrawImage(bmp3, x * 16, y * 16);
                    //g.DrawString(id.ToString(), new Font("Courier", 6), Brushes.Black, new PointF(x * 16 + 4, y * 16 + 1));

                    int idd = id;
                    int targget = target;
                    if (id.ToString().Length > 3) { idd = Convert.ToInt32(id.ToString().Substring(0, 3)); }
                    if (target.ToString().Length > 3) { targget = Convert.ToInt32(target.ToString().Substring(0, 3)); }
                    DrawText(idd.ToString(), Back, Brushes.Black, bfont.Families[0], 8, "top", "center", x * 16, y * 16 + 1, false);
                    DrawText(targget.ToString(), Back, Brushes.Red, bfont.Families[0], 8, "bottom", "center", x * 16, y * 16 - 1, false);

                }
                else
                {
                    Bitmap bmp2 = bdata.getRotation(fid, coins);
                    if (bmp2 != null) g.DrawImage(bmp2, x * 16, y * 16);
                }
            } //yes
            if (MainForm.userdata.useColor)
            {
                if (bid == 0 || fid == 0)
                {
                    if (MainForm.userdata.thisColor != Color.Transparent)
                    {
                        if (color != Color.Transparent)
                        {
                            Minimap.SetColor(x, y, MainForm.userdata.thisColor);
                            if (bid != 0) Minimap.SetPixel(x, y, bid);
                            if (fid != 0 && Minimap.ImageColor[fid]) Minimap.SetPixel(x, y, fid);
                        }
                        else
                        {
                            Minimap.SetColor(x, y, MainForm.userdata.thisColor);
                            if (bid != 0) Minimap.SetPixel(x, y, bid);
                            if (fid != 0 && Minimap.ImageColor[fid]) Minimap.SetPixel(x, y, fid);
                        }
                    }
                }
                else
                {
                    Minimap.SetPixel(x, y, bid);
                    if (fid != 0 && Minimap.ImageColor[fid]) Minimap.SetPixel(x, y, fid);
                }
            }
            else
            {
                Minimap.SetPixel(x, y, bid);
                if (fid != -1 && Minimap.ImageColor[fid]) Minimap.SetPixel(x, y, fid);
            }
        }

        public void Draw(int x, int y, Graphics g, Color color)
        {
            int bid = CurFrame.Background[y, x];
            int coins = CurFrame.BlockData[y, x];
            int fid = CurFrame.Foreground[y, x];
            int id = CurFrame.BlockData1[y, x];
            int target = CurFrame.BlockData2[y, x];
            string text = CurFrame.BlockData3[y, x];
            string text1 = CurFrame.BlockData4[y, x];
            string text2 = CurFrame.BlockData5[y, x];
            string text3 = CurFrame.BlockData6[y, x];
            Draw(x, y, g, bid, fid, coins, id, target, text, text1, text2, text3, color);
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int xStart = Math.Abs(AutoScrollPosition.X) + e.ClipRectangle.X;
            int yStart = Math.Abs(AutoScrollPosition.Y) + e.ClipRectangle.Y;
            if (Back != null)
            {
                e.Graphics.DrawImage(Back, e.ClipRectangle, new Rectangle(new Point(xStart, yStart), e.ClipRectangle.Size), GraphicsUnit.Pixel);
            }
            else if (Back1 != null)
            {
                //Console.WriteLine(e.ClipRectangle + " " + e.ClipRectangle.Size);
                e.Graphics.DrawImage(Back1, e.ClipRectangle, new Rectangle(new Point(xStart, yStart), e.ClipRectangle.Size), GraphicsUnit.Pixel);
                //e.Graphics.DrawImage(Back, e.ClipRectangle, new Rectangle(new Point(xStart, yStart), e.ClipRectangle.Size), GraphicsUnit.Pixel);
                //e.Graphics.DrawImage(Back, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
            }

        }

        private void EditArea_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            IsMouseUp = false;
            Tool.MouseDown(e);
        }

        private void EditArea_MouseMove(object sender, MouseEventArgs e)
        {
            Tool.MouseMove(e);
            IsMouseUp = false;
            if (started)
            {
                Point p = Tool.GetLocation(e);
                if (e.X / MainForm.Zoom <= CurFrame.Width && e.Y / MainForm.Zoom <= CurFrame.Height) MainForm.pos.Text = "X: " + p.X + " Y: " + p.Y;
                MainForm.fg.Text = CurFrame.Foreground[p.Y, p.X].ToString();
                MainForm.bg.Text = CurFrame.Background[p.Y, p.X].ToString();
                if (CurFrame.Foreground[p.Y, p.X] == 374)
                {
                    string text = null;
                    if (CurFrame.BlockData3[p.Y, p.X] != null)
                    {
                        if (CurFrame.BlockData3[p.Y, p.X].Length >= 10)
                        {
                            text = CurFrame.BlockData3[p.Y, p.X].Substring(0, 10) + "....";
                        }
                        else
                        {
                            text = CurFrame.BlockData3[p.Y, p.X];
                        }
                        MainForm.txt.Text = text;
                    }
                }
                else if (CurFrame.Foreground[p.Y, p.X] == 385)
                {
                    try
                    {
                        string text = null;
                        if (CurFrame.BlockData3[p.Y, p.X].Length >= 10)
                        {
                            text = CurFrame.BlockData3[p.Y, p.X].Substring(0, 10) + "....";
                        }
                        else
                        {
                            text = CurFrame.BlockData3[p.Y, p.X];
                        }
                        MainForm.rot.Text = CurFrame.BlockData[p.Y, p.X].ToString();
                        MainForm.txt.Text = text;
                    }
                    catch
                    {

                    }
                }
                else if (CurFrame.Foreground[p.Y, p.X] == 1000)
                {
                    string text = null;
                    if (CurFrame.BlockData3[p.Y, p.X] != null)
                    {
                        if (CurFrame.BlockData3[p.Y, p.X].Length >= 10)
                        {
                            text = CurFrame.BlockData3[p.Y, p.X].Substring(0, 10) + "....";
                        }
                        else
                        {
                            text = CurFrame.BlockData3[p.Y, p.X];
                        }
                        MainForm.rot.Text = CurFrame.BlockData[p.Y, p.X].ToString();
                        MainForm.txt.Text = text;
                    }
                }
                else if (bdata.portals.Contains(CurFrame.Foreground[p.Y, p.X]))
                {
                    MainForm.rot.Text = CurFrame.BlockData[p.Y, p.X].ToString();
                    MainForm.idtarget.Text = CurFrame.BlockData1[p.Y, p.X].ToString() + " > " + CurFrame.BlockData2[p.Y, p.X].ToString();
                }
                else
                {
                    MainForm.rot.Text = CurFrame.BlockData[p.Y, p.X].ToString();
                    MainForm.txt.Text = "";
                    MainForm.idtarget.Text = "0 > 0";
                }
            }
        }

        private void EditArea_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
            IsMouseUp = true;
            if (incfg != null) ToolPen.undolist.Push(incfg);
            incfg = null;
            Tool.MouseUp(e);
        }

        public void createFrame(int n)
        {
            Frame frm = new Frame(BlockWidth, BlockHeight);
            Frames.Add(frm);
            //Frames.Add(frame);
            //Frames.Add(frame);
            //Frames[n].Reset();
            changeFrame(n);
        }

        public void changeFrame(int n)
        {
            curFrame = n;
            this.Invalidate();
        }

        public Point GetMousePosition()
        {
            return PointToClient(MousePosition);
        }

        public void SetMarkBlock(string[,] Area, string[,] Back, string[,] Coins, string[,] id, string[,] target, string[,] text, string[,] text1, string[,] text2, string[,] text3)
        {
            SetMarkBlock(Area, Back, Coins, id, target, text, text1, text2, text3, Math.Abs(AutoScrollPosition.X) / 16, Math.Abs(AutoScrollPosition.Y) / 16);
        }

        public void SetMarkBlock(string[,] Area, string[,] Back, string[,] Coins, string[,] id, string[,] target, string[,] text, string[,] text1, string[,] text2, string[,] text3, int xPos, int yPos)
        {
            try
            {
                ToolMark tm = Tool as ToolMark;
                tm.Front = Area;
                tm.Back = Back;
                tm.Coins = Coins;
                tm.Id1 = id;
                tm.Target1 = target;
                tm.Text1 = text;
                tm.Text2 = text1;
                tm.Text3 = text2;
                tm.Text4 = text3;
                tm.Rect = new Rectangle(xPos, yPos, Area.GetLength(1), Area.GetLength(0));
                tm.progress = ToolMark.Progress.Selected;
                tm.PlaceBorderRect();
                MainForm.SetTransFormToolStrip(true);
                Invalidate();
            }
            catch
            {
            }
        }

        protected override void OnKeyDown(KeyEventArgs e) //Tool hotkeys
        {
            Tool.KeyDown(e);
            ShiftDown = e.Shift;
            //CtrlDown = e.Control;
            AltDown = e.Alt;
            SwitchBlock = e.Alt;
            if (e.KeyCode == Keys.Q) ChangeBlock = true;
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (!MainForm.selectionTool) MainForm.SetMarkTool();
                string[][,] data = (string[][,])Clipboard.GetData("EEData");
                if (data?.Length == 9)
                {
                    Tool.CleanUp(false);
                    SetMarkBlock(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8]);
                }
            }
            if ((int)Keys.D0 <= (int)e.KeyCode && (int)e.KeyCode <= (int)Keys.D9)
                MainForm.SetActiveBrick((int)e.KeyCode - (int)Keys.D0);

            if (!e.Control)
            {
                if (e.KeyCode == Keys.F1) MainForm.SetTool(13); // About window
                if (e.KeyCode == Keys.F2) //debug mode
                {
                    if (MainForm.debug)
                    {
                        MainForm.debug = false;
                        MainForm.Text = "EERditor " + bdata.programVersion;
                        MainForm.rebuildGUI(false);

                    }
                    else
                    {
                        MainForm.Text = "EERditor " + bdata.programVersion + " - Using Debug";
                        MainForm.debug = true;
                        MainForm.rebuildGUI(false);


                    }
                }
                if (e.KeyCode == Keys.F5) MainForm.SetTool(7); // Reload level
                if (e.KeyCode == Keys.F6) MainForm.SetTool(6); // LevelTextbox
                if (e.KeyCode == Keys.F7) MainForm.SetTool(8); // CodeTextbox

                if (e.KeyCode == Keys.Z && e.KeyCode != Keys.Control) MainForm.SetPenTool();
                if (e.KeyCode == Keys.X) MainForm.SetFillTool();
                if (e.KeyCode == Keys.C) MainForm.SetSprayTool();
                if (e.KeyCode == Keys.V) MainForm.SetMarkTool();

                if (e.KeyCode == Keys.I) MainForm.SetTool(17); // Rotate selection left
                if (e.KeyCode == Keys.K) MainForm.SetTool(18); // Rotate selection right
                if (e.KeyCode == Keys.J) MainForm.SetTool(19); // Rotate selection left
                if (e.KeyCode == Keys.L) MainForm.SetTool(20); // Rotate selection right

                if (e.KeyCode == Keys.N) MainForm.SetTool(10); //Hide blocks
                if (e.KeyCode == Keys.M && e.KeyCode != Keys.Control) MainForm.SetTool(11); // Minimap

                if (e.KeyCode == Keys.Oemcomma) MainForm.SetRectTool();
                if (e.KeyCode == Keys.OemPeriod) MainForm.SetCircleTool();
                if (e.KeyCode == Keys.OemMinus) MainForm.SetLineTool();
                if (e.KeyCode == Keys.Pause)
                {
                    if (ToolFill.filling)
                    {
                        if (!ToolFill.stopfill) ToolFill.stopfill = true;
                    }
                }
                if (e.KeyCode == Keys.Insert)
                {
                    random rnd = new random();
                    rnd.ShowDialog();
                }
                // OemMinus is wrong, actually this should be used but idk how: https://social.msdn.microsoft.com/Forums/silverlight/en-US/aab11ce4-bdb8-4776-80fb-1f7491f73a46/platform-neutral-key-code-for-dash?forum=silverlightnet

            }
            else
            {
                if (e.Control && e.KeyCode == Keys.N) MainForm.SetTool(0); // New
                if (e.Control && e.KeyCode == Keys.O) MainForm.SetTool(1); // Open
                if (e.Control && e.KeyCode == Keys.S) MainForm.SetTool(2); // Save

                if (e.Control && e.KeyCode == Keys.Z) MainForm.SetTool(14); // Undo
                if (e.Control && e.KeyCode == Keys.Y) MainForm.SetTool(15); // Redo
                if (e.Control && e.Shift && e.KeyCode == Keys.H) MainForm.SetTool(16); // History

                if (e.Control && e.KeyCode == Keys.Oemcomma) MainForm.SetFilledRectTool();
                //if (e.Control && e.KeyCode == Keys.OemPeriod) MainForm.SetFilledCircleTool();

                if (e.Control && e.KeyCode == Keys.I) MainForm.SetTool(3); // Insert image
                if (e.Control && e.KeyCode == Keys.T) MainForm.SetTool(4); // Insert text 
                if (e.Control && e.KeyCode == Keys.P) MainForm.SetColorPicker(); // Find a block by color
                if (e.Control && e.KeyCode == Keys.F) MainForm.SetTool(5); // Find&replace
                if (e.Control && e.KeyCode == Keys.H) MainForm.SetTool(5); // Find&replace

                if (e.Control && e.KeyCode == Keys.U) MainForm.SetTool(9); // Upload level

                if (e.Control && e.KeyCode == Keys.Tab) MainForm.SetView(); // Next block tab
                /*if (e.Control && e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
                {
                    if (MainForm.Zoom < 32)
                    {
                        MainForm.Zoom += 4;
                    }
                    zoomRefresh();
                }
                if (e.Control && e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
                {
                    if (MainForm.Zoom > 4)
                    {
                        MainForm.Zoom -= 4;
                        zoomRefresh();
                    }
                }
                if (e.Control && e.KeyCode == Keys.D0)
                {
                    MainForm.Zoom = 16;
                    zoomRefresh();
                }*/
                if (e.Control && e.KeyCode == Keys.F5)
                {
                    MainForm.rebuildGUI(false); // Rebuild GUI
                    if (Clipboard.ContainsData("EEBrush"))
                    {
                        Clipboard.Clear();
                    }
                }
                else
                {
                    CtrlDown = e.Control;
                }
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            ShiftDown = e.Shift;
            SwitchBlock = e.Alt;
            ChangeBlock = false;
            CtrlDown = e.Control;
            AltDown = e.Alt;
            ShowLines = false;
        }

        public bool ShiftDown = false;
        public bool CtrlDown = false;
        public bool AltDown = false;
        public bool SwitchBlock = false;

        public bool ChangeBlock = false;

        public bool ShowLines = false;

        private void EditArea_Load(object sender, EventArgs e)
        {
            byte[] fontData = Properties.Resources.blocktext;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.blocktext.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.blocktext.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            bfont = fonts;
            //if (File.Exists(Directory.GetCurrentDirectory() + @"\blocktext.ttf")) bfont.AddFontFile(Directory.GetCurrentDirectory() + @"\blocktext.ttf");
        }

        private void DrawText(string line, Bitmap bmp, System.Drawing.Brush brush1, FontFamily fonte, int size, string aligment0, string aligment1, int x, int y, bool admintext)
        {
            Graphics gge = Graphics.FromImage(bmp);
            gge.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            if (fonte != null) fontz = new Font(fonte, size, FontStyle.Regular, GraphicsUnit.Pixel);
            if (fonte == null) fontz = new Font("System", size, FontStyle.Regular, GraphicsUnit.Pixel);
            if (line.Length <= 2) rect = new Rectangle(x - 2, y, 19, 16);
            else { rect = new Rectangle(x - 1, y, 19, 16); }

            if (aligment0 != null && aligment1 != null)
            {
                using (StringFormat sf = new StringFormat())
                {
                    if (aligment0 == "top") { sf.LineAlignment = StringAlignment.Near; }
                    if (aligment0 == "middle") { sf.LineAlignment = StringAlignment.Center; }
                    if (aligment0 == "bottom") { sf.LineAlignment = StringAlignment.Far; }
                    if (aligment1 == "left") { sf.Alignment = StringAlignment.Near; }
                    if (aligment1 == "right") { sf.Alignment = StringAlignment.Far; }
                    if (aligment1 == "center") { sf.Alignment = StringAlignment.Center; }
                    gge.DrawString(line, fontz, brush1, rect, sf);
                    gge.Save();
                }
            }
            else
            {
                //gge.DrawString(line, fontz, brush1, rect);
                //Console.WriteLine(line);
                //gge.DrawRectangle(new Pen(Color.Red), new Rectangle(x, y, 180, 39));
                //gge.DrawString(line, fontz, brush1, new Point(x,y));
            }
            //Draw(x, y, gge, Color.Transparent);
            //Invalidate();
            //Point p = new Point(x - Math.Abs(AutoScrollPosition.X), y- Math.Abs(AutoScrollPosition.Y));
            //Draw(x, y, gge, Properties.Settings.Default.thiscolor);
            //Invalidate(new Rectangle(p, new Size(180, 39)));
        }
    }
}
