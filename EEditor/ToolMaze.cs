using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows;
namespace EEditor
{
    class ToolMaze : Tool
    {
        int yStart = 0;
        int xStart = 0;
        int xEnd = 0;
        int yEnd = 0;
        private System.Drawing.Point P;
        private System.Drawing.Point Q;
        private Pen borderPen;
        private bool select = false;
        private bool selected = false;
        private int dx;
        private int dy;
        private Bitmap img1 = new Bitmap(3000, 3000);
        public Rectangle Rect { get; set; }

        public ToolMaze(EditArea editArea)
            : base(editArea)
        {
            borderPen = new Pen(Color.LightGreen);
            borderPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
        }


        private void Recte(System.Drawing.Point start, System.Drawing.Point end)
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
            int num = (int)Math.Min(xStart, xEnd);
            int num2 = (int)Math.Max(xStart, xEnd) + 1;
            int num3 = (int)Math.Min(yStart, yEnd);
            int num4 = (int)Math.Max(yStart, yEnd) + 1;
            int num5 = num2 - num;
            int num6 = num4 - num3;
            int num7 = 1;
            int num8 = 1;
            int num9 = (num5 - num8) / (num7 + num8);
            int num10 = (num6 - num8) / (num7 + num8);
            if (num9 < 1 || num10 < 1)
            {
                MessageBox.Show("The selected zone is too small to draw a maze.");
                return;
            }
            bool[,] array = new bool[num9, num10];
            
            List<System.Windows.Point> list = new List<System.Windows.Point>();
            list.Add(new System.Windows.Point(0.0, 0.0));
            List<byte> list2 = new List<byte>();
            for (int i = 0; i < num9; i++)
            {
                for (int j = 0; j < num10; j++)
                {
                    array[i, j] = true;
                }
            }
            for (int k = 0; k < num9 + 1; k++)
            {
                for (int l = k * (num8 + num7) + num; l < Math.Min(num2, k * (num8 + num7) + num8 + num); l++)
                {
                    for (int m = num3; m < num4; m++)
                    {
                        editArea.CurFrame.Foreground[l, m] = 0;
                        editArea.Draw(l, m, g, MainForm.userdata.thisColor);
                    }
                }
            }
            for (int n = 0; n < num10 + 1; n++)
            {
                for (int num11 = n * (num8 + num7) + num3; num11 < Math.Min(num4, n * (num8 + num7) + num8 + num3); num11++)
                {
                    for (int num12 = num; num12 < num2; num12++)
                    {
                        editArea.CurFrame.Foreground[num11, num12] = 0;
                        editArea.Draw(num11, num12, g, MainForm.userdata.thisColor);
                    }
                }
            }
            while (list.Count > 0)
            {
                System.Windows.Point point = list[list.Count - 1];
                int num13 = (int)point.X;
                int num14 = (int)point.Y;
                array[num13, num14] = false;
                list2.Clear();
                if (num13 > 0 && array[num13 - 1, num14])
                {
                    list2.Add(1);
                }
                if (num14 > 0 && array[num13, num14 - 1])
                {
                    list2.Add(2);
                }
                if (num13 < num9 - 1 && array[num13 + 1, num14])
                {
                    list2.Add(3);
                }
                if (num14 < num10 - 1 && array[num13, num14 + 1])
                {
                    list2.Add(4);
                }
                if (list2.Count <= 1)
                {
                    list.RemoveAt(list.Count - 1);
                }
                if (list2.Count != 0)
                {
                    Random vala = new Random();
                    var value = vala.Next(list2.Count);
                    int num15 = (int)list2[value];
                    int param_block = PenID;
                    switch (num15)
                    {
                        case 1:
                            list.Add(new System.Windows.Point((double)(num13 - 1), (double)num14));
                            for (int num16 = (num13 - 1) * (num8 + num7) + num8 + num; num16 < (num13 + 1) * (num8 + num7) + num; num16++)
                            {
                                for (int num17 = num14 * (num8 + num7) + num8 + num3; num17 < (num14 + 1) * (num8 + num7) + num3; num17++)
                                {
                                    editArea.CurFrame.Foreground[num17, num16] = param_block;
                                    editArea.Draw(num16, num17, g, MainForm.userdata.thisColor);
                                }
                            }
                            break;
                        case 2:
                            list.Add(new System.Windows.Point((double)num13, (double)(num14 - 1)));
                            for (int num18 = (num14 - 1) * (num8 + num7) + num8 + num3; num18 < (num14 + 1) * (num8 + num7) + num3; num18++)
                            {
                                for (int num19 = num13 * (num8 + num7) + num8 + num; num19 < (num13 + 1) * (num8 + num7) + num; num19++)
                                {
                                    editArea.CurFrame.Foreground[num18, num19] = param_block;
                                    editArea.Draw(num18, num19, g, MainForm.userdata.thisColor);
                                }
                            }
                            break;
                        case 3:
                            list.Add(new System.Windows.Point((double)(num13 + 1), (double)num14));
                            for (int num20 = num13 * (num8 + num7) + num8 + num; num20 < (num13 + 2) * (num8 + num7) + num; num20++)
                            {
                                for (int num21 = num14 * (num8 + num7) + num8 + num3; num21 < (num14 + 1) * (num8 + num7) + num3; num21++)
                                {
                                    editArea.CurFrame.Foreground[num21, num20] = param_block;
                                    editArea.Draw(num20, num21, g, MainForm.userdata.thisColor);
                                }
                            }
                            break;
                        case 4:
                            list.Add(new System.Windows.Point((double)num13, (double)(num14 + 1)));
                            for (int num22 = num14 * (num8 + num7) + num8 + num3; num22 < (num14 + 2) * (num8 + num7) + num3; num22++)
                            {
                                for (int num23 = num13 * (num8 + num7) + num8 + num; num23 < (num13 + 1) * (num8 + num7) + num; num23++)
                                {
                                    editArea.CurFrame.Foreground[num22, num23] = param_block;
                                    editArea.Draw(num22, num23, g, MainForm.userdata.thisColor);
                                }
                            }
                            break;
                    }
                }
            }
            //ToolPen.undolist.Push(incfg);
            list.Clear();
            list2.Clear();
            editArea.Invalidate();
        }

        #region Mouse Events
        public override void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
               System.Drawing.Point cur1 = GetLocation(e);
                if (IsPaintable(cur1.X, cur1.Y))
                {
                    if (selected)
                    {
                        System.Drawing.Point cur = GetLocation(e);
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
                System.Drawing.Point cur1 = GetLocation(e);
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
                System.Drawing.Point cur1 = GetLocation(e);
                if (IsPaintable(cur1.X, cur1.Y))
                {
                    if (select)
                    {
                        System.Drawing.Point p = GetLocation(e);
                        PlaceBorder(p);
                    }
                    if (selected)
                    {
                        System.Drawing.Point cur = GetLocation(e);
                        Rectangle nextRect = new Rectangle(new System.Drawing.Point(cur.X - dx, cur.Y - dy), Rect.Size);
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
        public void PlaceBorder(System.Drawing.Point q)
        {
            Graphics g = Graphics.FromImage(editArea.Back);
            RemoveBorder(g);
            Q = q;
            g.DrawRectangle(borderPen, GetRectangleScaled(P, Q));

            //g.DrawImage(img1, GetRectangleScaled(P,Q));
            editArea.Invalidate();
        }

        public Rectangle GetRectangleScaled(System.Drawing.Point p, System.Drawing.Point q)
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
                
                for (int y = yStart; y <= yEnd; y++)
                {
                    for (int x = xStart; x <= xEnd; x++)
                    {
                        editArea.Draw(x, y, g, MainForm.userdata.thisColor);
                    }
                }
                g.Save();
                editArea.Invalidate();
            }
        }

        public Rectangle GetRectangle(System.Drawing.Point p, System.Drawing.Point q)
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
            System.Drawing.Point p1 = r.Location;
            System.Drawing.Point p2 = new System.Drawing.Point(r.Location.X + r.Width - 1, r.Location.Y + r.Height - 1);
            //g.DrawRectangle(borderPen, GetRectangleScaled(p1, p2));
            g.Save();
            editArea.Invalidate();
        }
        #endregion
    }
}
