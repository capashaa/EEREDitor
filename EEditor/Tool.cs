using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace EEditor
{
    abstract public class Tool
    {
        protected EditArea editArea;
        public int PenID { get; set; }
        public bool border { get; set; }
        public string NPCtempMessage1 { get; set; }
        public string NPCtempMessage2 { get; set; }
        public string NPCtempMessage3 { get; set; }
        public string NPCtempMessage4 { get; set; }
        public int NPCId { get; set; }
        public static int PenSize { get; set; }

        protected Tool(EditArea editArea)
        {
            this.editArea = editArea;
        }

        public Point GetLocation(MouseEventArgs e)
        {
            return GetLocation(e.Location);
        }

        public Point GetLocation(Point p)
        {
            int x = (p.X + Math.Abs(editArea.AutoScrollPosition.X)) / MainForm.Zoom;
            int y = (p.Y + Math.Abs(editArea.AutoScrollPosition.Y)) / MainForm.Zoom;
            x = Math.Max(0, x);
            y = Math.Max(0, y);
            x = Math.Min(x, editArea.BlockWidth - 1);
            y = Math.Min(y, editArea.BlockHeight - 1);
            return new Point(x, y);
        }

        public bool IsPaintable(int x, int y) { return IsPaintable(x, y, PenID, border); }

        public bool IsPaintable(int x, int y, int id, bool border)
        {
            if (border)
            {
                if (x == 0 || x == editArea.CurFrame.Width - 1 || y == 0 || y == editArea.CurFrame.Height - 1)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (x == 0 || x == editArea.CurFrame.Width - 1 || y == 0 || y == editArea.CurFrame.Height - 1)
                {
                    if (MainForm.OpenWorld)
                    {
                            if (MainForm.OpenWorldCode && MainForm.OpenWorld) return true;
                            else if (!MainForm.OpenWorldCode && MainForm.OpenWorld && y > 4) return true;
                            else return false;
                    }
                    else
                    {
                            return true;
                    }
                }
                else
                {
                    if (MainForm.OpenWorld)
                    {
                            if (MainForm.OpenWorldCode && MainForm.OpenWorld) return true;
                            else if (!MainForm.OpenWorldCode && MainForm.OpenWorld && y > 4) return true;
                            else return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        
        public virtual void MouseDown(MouseEventArgs e) { }
        public virtual void MouseMove(MouseEventArgs e) { }
        public virtual void MouseMoveHover(MouseEventArgs e) { }
        public virtual void MouseUp(MouseEventArgs e) { }
        public virtual void KeyDown(KeyEventArgs e) { }
        public virtual void CleanUp(bool derp) { }
    }
}
