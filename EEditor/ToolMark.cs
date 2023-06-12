using System;
using System.Drawing;
using System.Windows.Forms;

namespace EEditor
{
    class ToolMark : Tool
    {
        protected Point P { get; set; }
        protected Point Q { get; set; }
        public string[,] Front { get; set; }
        public string[,] Back { get; set; }
        public string[,] Coins { get; set; }
        public string[,] Id1 { get; set; }
        public string[,] Target1 { get; set; }
        public string[,] Text1 { get; set; }
        public string[,] Text2 { get; set; }
        public string[,] Text3 { get; set; }
        public string[,] Text4 { get; set; }
        public Rectangle Rect { get; set; }
        public enum Progress { Select, Selected }
        public Progress progress;

        private Pen borderPen;
        private int dx;
        private int dy;
        private Bitmap CutBitmap { get; set; }
        private Bitmap BackBitmap { get; set; }


        public ToolMark(EditArea editArea)
            : base(editArea)
        {
            progress = Progress.Select;
            borderPen = new Pen(Color.White);
            borderPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }

        public void MouseDown2(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if (progress == Progress.Selected)
            {
                Point cur = GetLocation(e);
                if (Rect.Contains(cur))
                {
                    dx = cur.X - Rect.X;
                    dy = cur.Y - Rect.Y;
                }
                else
                {
                    progress = Progress.Select;
                    editArea.MainForm.SetTransFormToolStrip(false);
                    Size size = new Size(16 * Rect.Width, 16 * Rect.Height);
                    CutBitmap = editArea.Back.Clone(new Rectangle(new Point(16 * Rect.X, 16 * Rect.Y), size), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                    BackBitmap = new Bitmap(size.Width, size.Height);
                }
            }

            if (progress == Progress.Select)
            {
                //Front = null;
                //Back = null;
                P = GetLocation(e);
                PlaceBorder(P);
            }
        }

        public override void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (progress == Progress.Selected)
                {
                    Point cur = GetLocation(e);
                    if (Rect.Contains(cur))
                    {
                        dx = cur.X - Rect.X;
                        dy = cur.Y - Rect.Y;
                    }
                    else
                    {
                        RemoveBorder();
                        PlaceBlock();
                        editArea.MainForm.SetTransFormToolStrip(false);
                        progress = Progress.Select;
                    }
                }

                if (progress == Progress.Select)
                {
                    //Front = null;
                    //Back = null;
                    P = GetLocation(e);
                    PlaceBorder(P);
                }
            }
        }

        public override void MouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (progress == Progress.Select)
                {
                    Point cur = GetLocation(e);
                    PlaceBorder(cur);
                }
                else if (progress == Progress.Selected)
                {
                    Point cur = GetLocation(e);
                    Rectangle nextRect = new Rectangle(new Point(cur.X - dx, cur.Y - dy), Rect.Size);
                    if (nextRect != Rect)
                    {
                        RemoveBorderRect();
                        Rect = nextRect;
                        PlaceBorderRect();
                    }
                }
            }
        }

        public override void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (progress == Progress.Select)
                {
                    Q = GetLocation(e);
                    Rect = GetRectangle(P, Q);
                    progress = Progress.Selected;
                    Front = new string[Rect.Height, Rect.Width];
                    Back = new string[Rect.Height, Rect.Width];
                    Coins = new string[Rect.Height, Rect.Width];
                    Id1 = new string[Rect.Height, Rect.Width];
                    Target1 = new string[Rect.Height, Rect.Width];
                    Text1 = new string[Rect.Height, Rect.Width];
                    Text2 = new string[Rect.Height, Rect.Width];
                    Text3 = new string[Rect.Height, Rect.Width];
                    Text4 = new string[Rect.Height, Rect.Width];
                    Frame frame = editArea.CurFrame;
                    for (int y = 0; y < Rect.Height; ++y)
                    {
                        for (int x = 0; x < Rect.Width; ++x)
                        {
                            int xx = x + Rect.X;
                            int yy = y + Rect.Y;
                            Front[y, x] = Convert.ToString(frame.Foreground[yy, xx]);
                            Back[y, x] = Convert.ToString(frame.Background[yy, xx]);
                            Coins[y, x] = Convert.ToString(frame.BlockData[yy, xx]);
                            Id1[y, x] = Convert.ToString(frame.BlockData1[yy, xx]);
                            Target1[y, x] = Convert.ToString(frame.BlockData2[yy, xx]);
                            Text1[y, x] = frame.BlockData3[yy, xx];
                            Text2[y, x] = frame.BlockData4[yy, xx];
                            Text3[y, x] = frame.BlockData5[yy, xx];
                            Text4[y, x] = frame.BlockData6[yy, xx];
                            editArea.CurFrame.Foreground[yy, xx] = (xx == 0 || yy == 0 || xx == frame.Width - 1 || yy == frame.Height - 1) ? 9 : 0;
                            editArea.CurFrame.Background[yy, xx] = 0;
                        }
                    }
                    editArea.MainForm.SetTransFormToolStrip(true);
                    //Clipboard.SetData("SDATA", new int[][,] { Front, Back, Coins, Id1, Target1 });
                }
                else if (progress == Progress.Selected)
                {
                }
            }
        }

        public Rectangle GetRectangle(Point p, Point q)
        {
            /* int x = Math.Min(p.X, q.X);
             x = Math.Max(0, x);
             x = Math.Min(x, editArea.BlockWidth - 1);
             int y = Math.Min(p.Y, q.Y);
             y = Math.Max(0, y);
             y = Math.Min(y, editArea.BlockHeight - 1);

             int w = Math.Max(p.X, q.X) - Math.Min(p.X, q.X) + 1;
             w = Math.Max(0, w);
             w = Math.Min(w, editArea.BlockWidth - x - 1);
            
             int h = Math.Max(p.Y, q.Y) - Math.Min(p.Y, q.Y) + 1;
             h = Math.Max(0, h);
             h = Math.Min(h, editArea.BlockHeight - y - 1);

             return new Rectangle(x, y, w, h);*/

            Rectangle r = new Rectangle(Math.Min(p.X, q.X), Math.Min(p.Y, q.Y),
                    Math.Max(p.X, q.X) - Math.Min(p.X, q.X) + 1, Math.Max(p.Y, q.Y) - Math.Min(p.Y, q.Y) + 1);
            return r;
        }

        public Rectangle GetRectangleScaled(Point p, Point q)
        {
            /* int x = Math.Min(p.X, q.X);
             x = Math.Max(0, x);
             x = Math.Min(x, editArea.BlockWidth - 1);
             x *= 16;
             int y = Math.Min(p.Y, q.Y);
             y = Math.Max(0, y);
             y = Math.Min(y, editArea.BlockHeight - 1);
             y *= 16;

             int w = Math.Max(p.X, q.X) - Math.Min(p.X, q.X) + 1;
             w = Math.Max(0, w);
             w = Math.Min(w, editArea.BlockWidth - x - 1);
             w *= 16;
             w -= 1;
             w = Math.Max(0, w);

             int h = Math.Max(p.Y, q.Y) - Math.Min(p.Y, q.Y) + 1;
             h = Math.Max(0, h);
             h = Math.Min(h, editArea.BlockHeight - y - 1);
             h *= 16;
             h -= 1;
             h = Math.Max(0, h);

             return new Rectangle(x, y, w, h);*/

            Rectangle r = new Rectangle(Math.Min(p.X, q.X) * 16, Math.Min(p.Y, q.Y) * 16,
                    (Math.Max(p.X, q.X) - Math.Min(p.X, q.X) + 1) * 16 - 1, (Math.Max(p.Y, q.Y) - Math.Min(p.Y, q.Y) + 1) * 16 - 1);
            return r;
        }

        public void RemoveBorder(Graphics g = null)
        {
            if (Q != null)
            {
                if (g == null) g = Graphics.FromImage(editArea.Back);
                Rectangle r = GetRectangle(P, Q);
                //for (int x = 0; x <= r.Width; ++x) g.DrawImage(editArea.GetBrickID(x + r.X, r.Y), (x + r.X) * 16, r.Y * 16);
                for (int x = 0; x < r.Width; ++x) editArea.Draw(x + r.X, r.Y, g, MainForm.userdata.thisColor);
                //for (int x = 0; x <= r.Width; ++x) g.DrawImage(editArea.GetBrickID(x + r.X, r.Y + r.Height - 1), (x + r.X) * 16, (r.Y + r.Height - 1) * 16);
                for (int x = 0; x < r.Width; ++x) editArea.Draw(x + r.X, r.Y + r.Height - 1, g, MainForm.userdata.thisColor);
                //for (int y = 0; y <= r.Height; ++y) g.DrawImage(editArea.GetBrickID(r.X, y + r.Y), r.X * 16, (y + r.Y) * 16);
                for (int y = 0; y < r.Height; ++y) editArea.Draw(r.X, y + r.Y, g, MainForm.userdata.thisColor);
                //for (int y = 0; y <= r.Height; ++y) g.DrawImage(editArea.GetBrickID(r.X + r.Width - 1, y + r.Y), (r.X + r.Width - 1) * 16, (y + r.Y) * 16);
                for (int y = 0; y < r.Height; ++y) editArea.Draw(r.X + r.Width - 1, y + r.Y, g, MainForm.userdata.thisColor);
                g.Save();
            }
        }

        protected void PlaceBorder(Point newQ)
        {
            Graphics g = Graphics.FromImage(editArea.Back);
            RemoveBorder(g);
            Q = newQ;
            g.DrawRectangle(borderPen, GetRectangleScaled(P, Q));
            g.Save();
            editArea.Invalidate();
        }

        protected void RemoveBorderRect()
        {
            Graphics g = Graphics.FromImage(editArea.Back);
            Rectangle r = Rect;
            for (int x = 0; x < r.Width; ++x)
                for (int y = 0; y < r.Height; ++y)
                {
                    if (0 <= x + r.X && x + r.X < editArea.BlockWidth && 0 <= y + r.Y && y + r.Y < editArea.BlockHeight)
                        editArea.Draw(x + r.X, y + r.Y, g, MainForm.userdata.thisColor);
                    //g.DrawImage(editArea.GetBrickID(x + r.X, y + r.Y), (x + r.X) * 16, (y + r.Y) * 16);
                }
            g.Save();
        }

        public void PlaceBorderRect()
        {
            Graphics g = Graphics.FromImage(editArea.Back);
            Rectangle r = Rect;
            for (int x = 0; x < r.Width; ++x)
                for (int y = 0; y < r.Height; ++y)
                {
                    if (0 <= x + r.X && x + r.X < editArea.BlockWidth && 0 <= y + r.Y && y + r.Y < editArea.BlockHeight)
                    {

                        editArea.Draw(x + r.X, y + r.Y, g, Convert.ToInt32(Back[y, x]), Convert.ToInt32(Front[y, x]), Convert.ToInt32(Coins[y, x]), Convert.ToInt32(Id1[y, x]), Convert.ToInt32(Target1[y, x]), Text1[y, x], Text2[y, x], Text3[y, x], Text4[y, x], MainForm.userdata.thisColor);

                    }
                    //g.DrawImage(editArea.Bricks[Area[y, x]], (x + r.X) * 16, (y + r.Y) * 16);
                }
            Point p1 = r.Location;
            Point p2 = new Point(r.Location.X + r.Width - 1, r.Location.Y + r.Height - 1);
            g.DrawRectangle(borderPen, GetRectangleScaled(p1, p2));
            g.Save();
            editArea.Invalidate();
        }

        public override void KeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C && progress == Progress.Selected)
            {
                Clipboard.SetData("EEData", new string[][,] { Front, Back, Coins, Id1, Target1, Text1, Text2, Text3, Text4 });
                CleanUp(false);
            }
            if (e.Control && e.KeyCode == Keys.A)
            {
                int x1 = 0;
                int y1 = 0;
                int w = editArea.Frames[0].Width - 1;
                int h = editArea.Frames[0].Height - 1;
                if (!MainForm.userdata.selectAllBorder)
                {
                    x1 = 1;
                    y1 = 1;
                    w -= 1;
                    h -= 1;
                }
                Graphics g = Graphics.FromImage(editArea.Back);
                RemoveBorder(g);
                g.DrawRectangle(borderPen, GetRectangleScaled(new Point(x1, y1), new Point(w, h)));
                g.Save();
                editArea.Invalidate();
                Rect = GetRectangle(new Point(x1, y1), new Point(w, h));
                Front = new string[Rect.Height, Rect.Width];
                Back = new string[Rect.Height, Rect.Width];
                Coins = new string[Rect.Height, Rect.Width];
                Id1 = new string[Rect.Height, Rect.Width];
                Target1 = new string[Rect.Height, Rect.Width];
                Text1 = new string[Rect.Height, Rect.Width];
                Text2 = new string[Rect.Height, Rect.Width];
                Text3 = new string[Rect.Height, Rect.Width];
                Text4 = new string[Rect.Height, Rect.Width];

                Frame frame = editArea.CurFrame;
                for (int y = 0; y < Rect.Height; ++y)
                {
                    for (int x = 0; x < Rect.Width; ++x)
                    {
                        int xx = x + Rect.X;
                        int yy = y + Rect.Y;
                        Front[y, x] = Convert.ToString(frame.Foreground[yy, xx]);
                        Back[y, x] = Convert.ToString(frame.Background[yy, xx]);
                        Coins[y, x] = Convert.ToString(frame.BlockData[yy, xx]);
                        Id1[y, x] = Convert.ToString(frame.BlockData1[yy, xx]);
                        Target1[y, x] = Convert.ToString(frame.BlockData2[yy, xx]);
                        Text1[y, x] = frame.BlockData3[yy, xx];
                        Text2[y, x] = frame.BlockData4[yy, xx];
                        Text3[y, x] = frame.BlockData5[yy, xx];
                        Text4[y, x] = frame.BlockData6[yy, xx];
                        editArea.CurFrame.Foreground[yy, xx] = (xx == 0 || yy == 0 || xx == frame.Width - 1 || yy == frame.Height - 1) ? 9 : 0;
                        editArea.CurFrame.Background[yy, xx] = 0;

                    }
                }
                progress = Progress.Selected;
                //Clipboard.SetData("SDATA", new int[][,] { Front, Back, Coins, Id1, Target1 });
            }
            else if (e.Control && e.KeyCode == Keys.X && progress == Progress.Selected)
            {
                Clipboard.SetData("EEData", new string[][,] { Front, Back, Coins, Id1, Target1, Text1, Text2, Text3, Text4 });
                RemoveBorder();
                Clear();
                progress = Progress.Select;
                editArea.MainForm.SetTransFormToolStrip(false);
                editArea.Invalidate();
            }
            else if (e.Control && e.KeyCode == Keys.G && progress == Progress.Selected)
            {
                Clipboard.SetData("EEBrush", new string[][,] { Front, Back, Coins, Id1, Target1, Text1, Text2, Text3, Text4 });
                CleanUp(true);
            }
            else if (e.Control && e.KeyCode == Keys.M && progress == Progress.Selected)
            {
                Clipboard.SetData("EEBlueprints", new string[][,] { Front, Back, Coins, Id1, Target1, Text1, Text2, Text3, Text4 });
                CleanUp(true);

            }
            else if (e.KeyCode == Keys.Delete)
            {
                RemoveBorder();
                Clear();
                progress = Progress.Select;
                editArea.MainForm.SetTransFormToolStrip(false);
                editArea.Invalidate();
            }
        }

        public override void CleanUp(bool clean)
        {
            RemoveBorder();
            PlaceBlock();
            if (clean)
            {
                Clear();
                progress = Progress.Select;
                editArea.MainForm.SetTransFormToolStrip(false);
                editArea.Invalidate();
            }
            if (!clean)
            {
                editArea.MainForm.SetTransFormToolStrip(false);
                editArea.Invalidate();
            }
        }

        public void ClearR()
        {
            if (Front != null)
            {
                Graphics g = Graphics.FromImage(editArea.Back);
                Frame frame = editArea.CurFrame;
                for (int y = 0; y < Rect.Height; ++y)
                    for (int x = 0; x < Rect.Width; ++x)
                    {
                        int xx = x + Rect.X;
                        int yy = y + Rect.Y;
                        if (0 <= xx && xx < editArea.BlockWidth && 0 <= yy && yy < editArea.BlockHeight)
                        {
                                frame.Foreground[yy, xx] = 0;
                                frame.BlockData[yy, xx] = 0;
                                frame.BlockData1[yy, xx] = 0;
                                frame.BlockData2[yy, xx] = 0;
                                frame.BlockData3[yy, xx] = "Unknown";
                                frame.BlockData4[yy, xx] = "Unknown";
                                frame.BlockData5[yy, xx] = "Unknown";
                                frame.BlockData6[yy, xx] = "Unknown";
                            

                            //frame.Background[yy, xx] = 0;
                            editArea.Draw(xx, yy, g, MainForm.userdata.thisColor);
                            //g.DrawImage(editArea.Bricks[frame.Map[yy, xx]], (x + Rect.X) * 16, (y + Rect.Y) * 16);
                        }
                    }
                g.Save();
            }
        }

        public void Clear()
        {
            if (Front != null)
            {
                Graphics g = Graphics.FromImage(editArea.Back);
                Frame frame = editArea.CurFrame;
                for (int y = 0; y < Rect.Height; ++y)
                    for (int x = 0; x < Rect.Width; ++x)
                    {
                        int xx = x + Rect.X;
                        int yy = y + Rect.Y;
                        if (0 <= xx && xx < editArea.BlockWidth && 0 <= yy && yy < editArea.BlockHeight)
                        {

                                frame.Foreground[yy, xx] = 0;
                                frame.Background[yy, xx] = 0;
                                frame.BlockData[yy, xx] = 0;
                                frame.BlockData1[yy, xx] = 0;
                                frame.BlockData2[yy, xx] = 0;
                                frame.BlockData3[yy, xx] = "Unknown";
                                frame.BlockData4[yy, xx] = "Unknown";
                                frame.BlockData5[yy, xx] = "Unknown";
                                frame.BlockData6[yy, xx] = "Unknown";
                            

                            //if (frame.Foreground[yy, xx] == 0) Back[y, x] = Convert.ToString(frame.Background[yy, xx]);

                            Front[y, x] = Convert.ToString(frame.Foreground[yy, xx]);
                            Back[y, x] = "0";
                            //if (frame.Background[yy, xx] != 0) Back[y, x] = Convert.ToString(frame.Background[yy, xx]);

                            Coins[y, x] = Convert.ToString(frame.BlockData[yy, xx]);
                            Id1[y, x] = Convert.ToString(frame.BlockData1[yy, xx]);
                            Target1[y, x] = Convert.ToString(frame.BlockData2[yy, xx]);
                            Text1[y, x] = frame.BlockData3[yy, xx];
                            Text2[y, x] = frame.BlockData4[yy, xx];
                            Text3[y, x] = frame.BlockData5[yy, xx];
                            Text4[y, x] = frame.BlockData6[yy, xx];
                            editArea.Draw(xx, yy, g, MainForm.userdata.thisColor);
                            //g.DrawImage(editArea.Bricks[frame.Map[yy, xx]], (x + Rect.X) * 16, (y + Rect.Y) * 16);
                        }
                    }
                g.Save();
            }
        }

        public void PlaceBlock()
        {
            if (Front != null)
            {
                Graphics g = Graphics.FromImage(editArea.Back);
                Frame frame = editArea.CurFrame;
                for (int y = 0; y < Rect.Height; ++y)
                {
                    for (int x = 0; x < Rect.Width; ++x)
                    {
                        int xx = x + Rect.X;
                        int yy = y + Rect.Y;
                        if (0 <= xx && xx < editArea.BlockWidth && 0 <= yy && yy < editArea.BlockHeight)
                        {

                            /*if (frame.Foreground[yy, xx] == 0 && Back[y, x] != "0") { frame.Background[yy, xx] = Convert.ToInt32(Back[y, x]); }
                            else { frame.Background[yy, xx] = 0; }
                            */
                            /*if (Back[y, x] == "0") { frame.Foreground[yy, xx] = Convert.ToInt32(Front[y, x]); }
                            else
                            {
                                frame.Foreground[yy, xx] = frame.Foreground[yy, xx];
                            }*/
                            //frame.Foreground[yy, xx] = Convert.ToInt32(Front[y, x]);
                            //if (frame.Background[yy, xx] == 0) frame.Background[yy, xx] = Convert.ToInt32(Back[y, x]);

                            if (MainForm.userdata.oldmark)
                            {
                                frame.Foreground[yy, xx] = Convert.ToInt32(Front[y, x]);

                                if (frame.Background[yy, xx] == 0) frame.Background[yy, xx] = Convert.ToInt32(Back[y, x]);
                                else if (Convert.ToInt32(Back[y, x]) != 0) frame.Background[yy, xx] = Convert.ToInt32(Back[y, x]);
                            }
                            else
                            {
                                frame.Foreground[yy, xx] = Convert.ToInt32(Front[y, x]);
                                if (frame.Background[yy, xx] == 0 || frame.Background[yy, xx] != 0 && Convert.ToInt32(Back[y, x]) != 0) frame.Background[yy, xx] = Convert.ToInt32(Back[y, x]);
                                else if (Convert.ToInt32(Back[y, x]) == 0) frame.Background[yy, xx] = 0;
                            }
                            

                            /*else
                            {
                                if (frame.Foreground[yy, xx] != 0 && Convert.ToInt32(Back[y, x]) != 0)
                                {

                                }
                                else
                                {
                                    frame.Foreground[yy, xx] = Convert.ToInt32(Front[y, x]);
                                    if (frame.Background[yy, xx] == 0) frame.Background[yy, xx] = Convert.ToInt32(Back[y, x]);
                                }
                                
                            }*/

                            //frame.Foreground[yy, xx] = Convert.ToInt32(0);
                            //if (frame.Background[yy, xx] == 0) frame.Background[yy, xx] = Convert.ToInt32(Back[y, x]);
                            //frame.Background[yy, xx] = Convert.ToInt32(Back[y, x]);

                                if (Coins[y, x] != null) frame.BlockData[yy, xx] = Convert.ToInt32(Coins[y, x]);
                                if (Id1[y, x] != null) frame.BlockData1[yy, xx] = Convert.ToInt32(Id1[y, x]);
                                if (Target1[y, x] != null) frame.BlockData2[yy, xx] = Convert.ToInt32(Target1[y, x]);
                                if (Text1[y, x] != null) frame.BlockData3[yy, xx] = Text1[y, x];
                                if (Text2 != null)
                                    if (Text2[y, x] != null) frame.BlockData4[yy, xx] = Text2[y, x];
                                if (Text3[y, x] != null) frame.BlockData5[yy, xx] = Text3[y, x];
                                if (Text4[y, x] != null) frame.BlockData6[yy, xx] = Text4[y, x];
                            
                            editArea.Draw(xx, yy, g, MainForm.userdata.thisColor);
                            //g.DrawImage(editArea.Bricks[frame.Map[yy, xx]], (x + Rect.X) * 16, (y + Rect.Y) * 16);
                        }
                    }
                }
                g.Save();
            }
        }
    }
}