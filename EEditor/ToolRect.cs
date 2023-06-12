using System;
using System.Drawing;
using System.Windows.Forms;

namespace EEditor
{
    class ToolRect : Tool
    {
        int yStart = 0;
        int xStart = 0;
        int xEnd = 0;
        int yEnd = 0;
        private Point P;
        private Point Q;
        private Pen borderPen;
        private bool select = false;
        private bool selected = false;
        private int dx;
        private int dy;
        private Bitmap img1 = new Bitmap(3000, 3000);
        public Rectangle Rect { get; set; }

        public ToolRect(EditArea editArea)
            : base(editArea)
        {
            borderPen = new Pen(Color.LightGreen);
            borderPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            editArea.Back1 = editArea.Back;
        }

        private void rect2(Point start, Point end)
        {
            Graphics g = Graphics.FromImage(editArea.Back1);
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
            if (end.X == start.X && end.Y > start.Y)
            {
                xStart = start.X;
                yStart = start.Y;
                xEnd = end.X;
                yEnd = end.Y;
            }
            if (end.X == start.X && start.Y > end.Y)
            {
                xStart = start.X;
                yStart = end.Y;
                xEnd = end.X;
                yEnd = start.Y;
            }
            if (end.Y == start.Y && end.X > start.X)
            {
                xStart = start.X;
                yStart = start.Y;
                xEnd = end.X;
                yEnd = end.Y;
            }
            if (end.Y == start.Y && start.X > end.X)
            {
                xStart = end.X;
                yStart = start.Y;
                xEnd = start.X;
                yEnd = end.Y;
            }
            if (end.X < start.X && end.Y < start.Y)
            {
                xStart = end.X;
                yStart = end.Y;
                xEnd = start.X;
                yEnd = start.Y;
            }
            else if (end.Y > start.Y && end.X > start.X)
            {
                yStart = start.Y;
                xStart = start.X;
                yEnd = end.Y;
                xEnd = end.X;
            }
            else if (start.Y > end.Y && end.X > start.X)
            {
                yStart = end.Y;
                xStart = start.X;
                yEnd = start.Y;
                xEnd = end.X;
            }
            else if (end.Y > start.Y && start.X > end.X)
            {
                yStart = start.Y;
                xStart = end.X;
                yEnd = end.Y;
                xEnd = start.X;
            }
            for (int y = yStart; y <= yEnd; y++)
            {
                for (int x = xStart; x <= xEnd; x++)
                {
                    if (x == start.X || y == start.Y || y == end.Y || x == end.X)
                    {
                        if (start.Y == end.Y && start.X == end.X) { }
                        else
                        {
                            g.DrawImage(img2, x * 16, y * 16);
                        }
                    }
                }
            }

            //editArea.Invalidate();
        }

        private void Recte(Point start, Point end)
        {
            Graphics g = Graphics.FromImage(editArea.Back);
            if (end.X == start.X && end.Y > start.Y)
            {
                xStart = start.X;
                yStart = start.Y;
                xEnd = end.X;
                yEnd = end.Y;
            }
            if (end.X == start.X && start.Y > end.Y)
            {
                xStart = start.X;
                yStart = end.Y;
                xEnd = end.X;
                yEnd = start.Y;
            }
            if (end.Y == start.Y && end.X > start.X)
            {
                xStart = start.X;
                yStart = start.Y;
                xEnd = end.X;
                yEnd = end.Y;
            }
            if (end.Y == start.Y && start.X > end.X)
            {
                xStart = end.X;
                yStart = start.Y;
                xEnd = start.X;
                yEnd = end.Y;
            }
            if (end.X < start.X && end.Y < start.Y)
            {
                xStart = end.X;
                yStart = end.Y;
                xEnd = start.X;
                yEnd = start.Y;
            }
            else if (end.Y > start.Y && end.X > start.X)
            {
                yStart = start.Y;
                xStart = start.X;
                yEnd = end.Y;
                xEnd = end.X;
            }
            else if (start.Y > end.Y && end.X > start.X)
            {
                yStart = end.Y;
                xStart = start.X;
                yEnd = start.Y;
                xEnd = end.X;
            }
            else if (end.Y > start.Y && start.X > end.X)
            {
                yStart = start.Y;
                xStart = end.X;
                yEnd = end.Y;
                xEnd = start.X;
            }
            string incfg = null;
            for (int y = yStart; y <= yEnd; y++)
            {
                for (int x = xStart; x <= xEnd; x++)
                {
                    if (x == start.X || y == start.Y || y == end.Y || x == end.X)
                    {
                        if (IsPaintable(x, y, PenID, true) && IsPaintable(x, y, PenID, false))
                        {
                            if (ToolPen.rotation.ContainsKey(PenID))
                            {
                                if (PenID == 242 || PenID == 381)
                                {
                                    if (ToolPen.rotation.ContainsKey(PenID)) editArea.CurFrame.BlockData[y, x] = ToolPen.rotation[PenID];
                                    else editArea.CurFrame.BlockData[y, x] = 0;
                                    if (ToolPen.id.ContainsKey(PenID)) editArea.CurFrame.BlockData1[y, x] = ToolPen.id[PenID];
                                    else editArea.CurFrame.BlockData1[y, x] = 0;
                                    if (ToolPen.target.ContainsKey(PenID)) editArea.CurFrame.BlockData2[y, x] = ToolPen.target[PenID];
                                    else editArea.CurFrame.BlockData2[y, x] = 0;
                                }
                                else
                                {
                                    if (ToolPen.rotation.ContainsKey(PenID)) editArea.CurFrame.BlockData[y, x] = ToolPen.rotation[PenID];
                                    else editArea.CurFrame.BlockData[y, x] = 0;
                                }
                            }
                            else if (PenID == 374 || PenID == 385)
                            {
                                if (ToolPen.text.ContainsKey(PenID)) editArea.CurFrame.BlockData3[y, x] = ToolPen.text[PenID];
                                else editArea.CurFrame.BlockData3[y, x] = "Unknown";
                            }
                            if (PenID >= 500 && PenID <= 999)
                            {
                                incfg += PenID + ":" + editArea.CurFrame.Background[y, x] + ":" + x + ":" + y + ":";
                                editArea.CurFrame.Background[y, x] = PenID;
                            }
                            else if (PenID < 500 || PenID >= 1001)
                            {
                                incfg += PenID + ":" + editArea.CurFrame.Foreground[y, x] + ":" + x + ":" + y + ":";
                                editArea.CurFrame.Foreground[y, x] = PenID;
                            }
                            editArea.Draw(x, y, g, MainForm.userdata.thisColor);
                        }
                    }
                }
            }
            ToolPen.undolist.Push(incfg);
            editArea.Invalidate();
            //editArea.Tool.CleanUp(true);
        }

        #region Mouse Events
        public override void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point cur1 = GetLocation(e);
                if (IsPaintable(cur1.X, cur1.Y))
                {
                    if (selected)
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
                            selected = false;
                            select = false;
                        }
                    }
                    else
                    {
                        P = GetLocation(e);
                        PlaceBorder(P);
                        select = true;
                    }
                }
            }
        }

        public override void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point cur1 = GetLocation(e);
                if (IsPaintable(cur1.X, cur1.Y))
                {
                    if (select)
                    {
                        Q = GetLocation(e);
                        if (P.X == Q.X && P.Y == Q.Y) { }
                        else
                        {
                            Recte(P, Q);
                        }
                        RemoveBorder();
                        select = false;
                        selected = false;
                    }
                }
                else
                {
                    if (select)
                    {
                        RemoveBorder();
                        select = false;
                        selected = false;
                    }
                }
            }
        }

        public override void MouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //Rect(yStart, xStart, p.Y, p.X);
                Point cur1 = GetLocation(e);
                if (IsPaintable(cur1.X, cur1.Y))
                {
                    if (select)
                    {
                        Point p = GetLocation(e);
                        PlaceBorder(p);
                    }
                    if (selected)
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
        }
        #endregion

        #region Draw rectangle
        public void PlaceBorder(Point q)
        {
            Graphics g = Graphics.FromImage(editArea.Back);
            RemoveBorder(g);
            Q = q;
            //g.DrawRectangle(borderPen, GetRectangleScaled(P, Q));
            rect2(P, Q);

            //g.DrawImage(img1, GetRectangleScaled(P,Q));
            editArea.Invalidate();
        }

        public Rectangle GetRectangleScaled(Point p, Point q)
        {
            /*Rectangle r = new Rectangle(Math.Min(p.X, q.X) * 16, Math.Min(p.Y, q.Y) * 16,
                    (Math.Max(p.X, q.X) - Math.Min(p.X, q.X) + 1) * 16 - 1, (Math.Max(p.Y, q.Y) - Math.Min(p.Y, q.Y) + 1) * 16 - 1);
                    */
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
                editArea.Invalidate();
            }
        }

        public Rectangle GetRectangle(Point p, Point q)
        {
            Rectangle r = new Rectangle(Math.Min(p.X, q.X), Math.Min(p.Y, q.Y),
                    Math.Max(p.X, q.X) - Math.Min(p.X, q.X) + 1, Math.Max(p.Y, q.Y) - Math.Min(p.Y, q.Y) + 1);
            return r;
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
                        editArea.Draw(x + r.X, y + r.Y, g, MainForm.userdata.thisColor);
                    }
                    //g.DrawImage(editArea.Bricks[Area[y, x]], (x + r.X) * 16, (y + r.Y) * 16);
                }
            Point p1 = r.Location;
            Point p2 = new Point(r.Location.X + r.Width - 1, r.Location.Y + r.Height - 1);
            //g.DrawRectangle(borderPen, GetRectangleScaled(p1, p2));
            g.Save();
            editArea.Invalidate();
        }
        #endregion
    }
}
