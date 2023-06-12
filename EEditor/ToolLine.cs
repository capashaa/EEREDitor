using System;
using System.Drawing;
using System.Windows.Forms;

namespace EEditor
{
    class ToolLine : Tool
    {
        private string incfg = null;
        private Point start = new Point();
        private Point end = new Point();
        private Pen borderPen;
        private Bitmap img1 = new Bitmap(3000, 3000);
        private bool hide = false;

        public ToolLine(EditArea editArea)
            : base(editArea)
        {
            borderPen = new Pen(Color.LightGreen);
            borderPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
        }

        private void Line(Point start, Point end)
        {
            //editArea.Invalidate();
            
            Algorithms.Line(start.X, start.Y, end.X, end.Y, plot);
            if (!hide)
            {
                if (incfg.Length > 0) ToolPen.undolist.Push(incfg);
                incfg = null;
                editArea.Invalidate();
            }
        }

        public bool plot(int x, int y)
        {
            if (PenID >= 500 && PenID <= 999)
            {
                incfg += PenID + ":" + editArea.CurFrame.Foreground[y, x] + ":" + x + ":" + y + ":";
            }
            else
            {
                incfg += PenID + ":" + editArea.CurFrame.Background[y, x] + ":" + x + ":" + y + ":";
            }
            PlaceBlock(x, y);
            return true;
        }

        public void PlaceBlock(int x, int y)
        {
            //var bg = bdata.isBg(PenID);
            if (!hide)
            {
                Graphics g = Graphics.FromImage(editArea.Back);

                if (PenID >= 500 && PenID <= 999)
                {
                    editArea.CurFrame.Background[y, x] = PenID;
                }
                else
                {
                    if (ToolPen.rotation.ContainsKey(PenID))
                    {
                        editArea.CurFrame.BlockData[y, x] = ToolPen.rotation[PenID];
                    }
                    if (IsPaintable(x, y, PenID, true) && IsPaintable(x, y, PenID, false)) editArea.CurFrame.Foreground[y, x] = PenID;
                }
                editArea.Draw(x, y, g, MainForm.userdata.thisColor);
                editArea.Invalidate();
            }
            else
            {
                //editArea.Back1 = editArea.Back;
                Graphics g = Graphics.FromImage(editArea.Back1);
                //editArea.Draw(x, y, g, MainForm.userdata.thisColor);
                if (PenID >= 500 && PenID <= 999)
                {
                    img1 = MainForm.backgroundBMD.Clone(new Rectangle(MainForm.backgroundBMI[PenID] * 16, 0, 16, 16), MainForm.backgroundBMD.PixelFormat);
                }
                else if (PenID < 500 || PenID >= 1001)
                {
                    if (MainForm.miscBMI[PenID] != 0)
                    {
                        img1 = MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[PenID] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
                    }
                    else if (MainForm.decosBMI[PenID] != 0)
                    {
                        img1 = MainForm.decosBMD.Clone(new Rectangle(MainForm.decosBMI[PenID] * 16, 0, 16, 16), MainForm.decosBMD.PixelFormat);
                    }
                    else
                    {
                        img1 = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[PenID] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                    }
                }

                Image img2 = bdata.SetImageOpacity(img1, 0.5f);
                g.DrawImage(img2, x * 16, y * 16);
                editArea.Invalidate();
                //editArea.Draw(x, y, g, MainForm.userdata.thisColor);
            }
        }

        public void PlaceBorder(Point q)
        {
            //Graphics g = Graphics.FromImage(editArea.Back);
            //g.DrawRectangle(borderPen, GetRectangleScaled(P, Q));
            Line(start, q);

            //g.DrawImage(img1, GetRectangleScaled(P,Q));
            //editArea.Invalidate();
        }

        public void RemoveBorder(Point endz, Graphics g = null)
        {
            if (start != null)
            {
                if (g == null) g = Graphics.FromImage(editArea.Back);
                Rectangle r = GetRectangle(start, endz);
                //for (int x = 0; x <= r.Width; ++x) g.DrawImage(editArea.GetBrickID(x + r.X, r.Y), (x + r.X) * 16, r.Y * 16);
                for (int x = 0; x < r.Width; ++x) editArea.Draw(x + r.X, r.Y, g, MainForm.userdata.thisColor);
                //for (int x = 0; x <= r.Width; ++x) g.DrawImage(editArea.GetBrickID(x + r.X, r.Y + r.Height - 1), (x + r.X) * 16, (r.Y + r.Height - 1) * 16);
                for (int x = 0; x < r.Width; ++x) editArea.Draw(x + r.X, r.Y + r.Height - 1, g, MainForm.userdata.thisColor);
                //for (int y = 0; y <= r.Height; ++y) g.DrawImage(editArea.GetBrickID(r.X, y + r.Y), r.X * 16, (y + r.Y) * 16);
                for (int y = 0; y < r.Height; ++y) editArea.Draw(r.X, y + r.Y, g, MainForm.userdata.thisColor);
                //for (int y = 0; y <= r.Height; ++y) g.DrawImage(editArea.GetBrickID(r.X + r.Width - 1, y + r.Y), (r.X + r.Width - 1) * 16, (y + r.Y) * 16);
                for (int y = 0; y < r.Height; ++y) editArea.Draw(r.X + r.Width - 1, y + r.Y, g, MainForm.userdata.thisColor);
                editArea.Invalidate();
                //editArea.Invalidate();
            }
        }

        public Rectangle GetRectangle(Point p, Point q)
        {
            Rectangle r = new Rectangle(Math.Min(p.X, q.X), Math.Min(p.Y, q.Y),
                    Math.Max(p.X, q.X) - Math.Min(p.X, q.X) + 1, Math.Max(p.Y, q.Y) - Math.Min(p.Y, q.Y) + 1);
            return r;
        }

        public override void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (start.IsEmpty)
                {
                    Point p = GetLocation(e);

                    if (IsPaintable(p.X, p.Y))
                    {
                        start = GetLocation(e);
                        ToolPen.undolist.Push(PenID + ":" + editArea.CurFrame.Foreground[start.Y, start.X] + ":" + start.X + ":" + start.Y + ":");
                        PlaceBlock(start.X, start.Y);
                        editArea.Invalidate();
                    }
                }
                else if (!start.IsEmpty)
                {
                    Point p = GetLocation(e);

                    if (IsPaintable(p.X, p.Y))
                    {
                        end = GetLocation(e);
                        ToolPen.undolist.Push(PenID + ":" + editArea.CurFrame.Foreground[end.Y, end.X] + ":" + end.X + ":" + end.Y + ":");
                        PlaceBlock(end.X, end.Y);
                        editArea.Invalidate();
                    }
                }
            }
        }

        public override void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!start.IsEmpty && !end.IsEmpty)
                {
                    Line(start, end);
                    start = new Point();
                    end = new Point();
                }
            }
        }

        public static class Algorithms
        {
            private static void Swap<T>(ref T lhs, ref T rhs) { T temp; temp = lhs; lhs = rhs; rhs = temp; }

            /// <summary>
            /// The plot function delegate
            /// </summary>
            /// <param name="x">The x co-ord being plotted</param>
            /// <param name="y">The y co-ord being plotted</param>
            /// <returns>True to continue, false to stop the algorithm</returns>
            public delegate bool PlotFunction(int x, int y);

            /// <summary>
            /// Plot the line from (x0, y0) to (x1, y10
            /// </summary>
            /// <param name="x0">The start x</param>
            /// <param name="y0">The start y</param>
            /// <param name="x1">The end x</param>
            /// <param name="y1">The end y</param>
            /// <param name="plot">The plotting function (if this returns false, the algorithm stops early)</param>
            public static void Line(int x0, int y0, int x1, int y1, PlotFunction plot)
            {
                bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
                if (steep) { Swap<int>(ref x0, ref y0); Swap<int>(ref x1, ref y1); }
                if (x0 > x1) { Swap<int>(ref x0, ref x1); Swap<int>(ref y0, ref y1); }
                int dX = (x1 - x0), dY = Math.Abs(y1 - y0), err = (dX / 2), ystep = (y0 < y1 ? 1 : -1), y = y0;

                for (int x = x0; x <= x1; ++x)
                {
                    if (!(steep ? plot(y, x) : plot(x, y))) return;
                    err -= dY;
                    if (err < 0) { y += ystep; err += dX; }
                }
            }
        }
    }
}
