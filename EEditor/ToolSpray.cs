using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EEditor
{
    class ToolSpray : Tool
    {
        int yStart = 0;
        int xStart = 0;
        public ToolSpray(EditArea editArea) : base(editArea) { }

        public override void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
            }
        }

        public override void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = GetLocation(e);
                if (IsPaintable(p.X, p.Y))
                {
                    xStart = GetLocation(e).X;
                    yStart = GetLocation(e).Y;
                    Point p1 = GetLocation(e);
                    Recte(p1);
                }

            }
            if (e.Button == MouseButtons.Right)
            {
            }
        }

        private void Recte(Point start)
        {
            string incfg = null;
            Graphics g = Graphics.FromImage(editArea.Back);
            int radius = MainForm.userdata.sprayr;
            int radius2 = radius * 2;
            Random _rnd = new Random();
            double x, y;
            for (int i = 0; i < MainForm.userdata.sprayp; ++i)
            {
                do
                {
                    x = (_rnd.NextDouble() * radius2) - radius;
                    y = (_rnd.NextDouble() * radius2) - radius;

                } while ((x * x + y * y) > (radius * radius));

                x += xStart;
                y += yStart;

                var xx = Math.Abs(x);
                var yy = Math.Abs(y);
                if (yy <= editArea.CurFrame.Height && xx <= editArea.CurFrame.Width)
                {
                    if (IsPaintable((int)xx, (int)yy, PenID, true) && IsPaintable((int)xx, (int)yy, PenID, false))
                    {
                        if (ToolPen.rotation.ContainsKey(PenID) && PenID != 374 && PenID != 385)
                        {
                            if (bdata.portals.Contains(PenID))
                            {
                                editArea.CurFrame.BlockData[(int)yy, (int)xx] = ToolPen.rotation[PenID];
                                editArea.CurFrame.BlockData1[(int)yy, (int)xx] = ToolPen.id[PenID];
                                editArea.CurFrame.BlockData2[(int)yy, (int)xx] = ToolPen.target[PenID];
                            }
                            else
                            {
                                editArea.CurFrame.BlockData[(int)yy, (int)xx] = ToolPen.rotation[PenID];
                            }
                        }
                        else if (PenID == 385)
                        {
                            if (ToolPen.text.ContainsKey(PenID))
                            {
                                editArea.CurFrame.BlockData3[(int)yy, (int)xx] = ToolPen.text[PenID];
                            }
                            if (!ToolPen.text.ContainsKey(PenID))
                            {
                                editArea.CurFrame.BlockData3[(int)yy, (int)xx] = "Sign Text";
                            }
                            if (ToolPen.rotation.ContainsKey(PenID))
                            {
                                editArea.CurFrame.BlockData[(int)yy, (int)xx] = ToolPen.rotation[PenID];
                            }
                            if (!ToolPen.rotation.ContainsKey(PenID))
                            {
                                editArea.CurFrame.BlockData[(int)yy, (int)xx] = 0;
                            }
                        }
                        else if (PenID == 374)
                        {
                            if (ToolPen.text.ContainsKey(PenID))
                            {
                                editArea.CurFrame.BlockData3[(int)yy, (int)xx] = ToolPen.text[PenID];
                            }
                            if (!ToolPen.text.ContainsKey(PenID))
                            {
                                editArea.CurFrame.BlockData3[(int)yy, (int)xx] = "PW01";
                            }
                        }
                        if (PenID < 500 || PenID >= 1001)
                        {
                            if (PenID != editArea.CurFrame.Foreground[(int)yy, (int)xx])
                            {
                                incfg += PenID + ":" + editArea.CurFrame.Foreground[(int)yy, (int)xx] + ":" + (int)xx + ":" + (int)yy + ":";
                            }
                            editArea.CurFrame.Foreground[(int)yy, (int)xx] = PenID;
                        }
                        if (PenID >= 500 && PenID <= 999)
                        {
                            if (PenID != editArea.CurFrame.Background[(int)yy, (int)xx])
                            {
                                incfg += PenID + ":" + editArea.CurFrame.Background[(int)yy, (int)xx] + ":" + (int)xx + ":" + (int)yy + ":";
                            }
                            if (editArea.CurFrame.Background[(int)yy, (int)xx] != 0)
                            {
                                editArea.CurFrame.Background[(int)yy, (int)xx] = PenID;
                            }
                            else
                            {
                                editArea.CurFrame.Background[(int)yy, (int)xx] = PenID;
                            }
                        }
                        editArea.Draw((int)xx, (int)yy, g, MainForm.userdata.thisColor);
                    }
                }
                else
                {

                }
            }
            ToolPen.undolist.Push(incfg);
            editArea.Invalidate();
            //editArea.Tool.CleanUp(true);
        }
    }
}
