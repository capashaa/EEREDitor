using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace EEditor
{
    class ToolPicker : Tool
    {
        public Dictionary<Color, int> colblocks = new Dictionary<Color, int>();
        public ToolPicker(EditArea editArea)
            : base(editArea)
        {
            ToolpickerSettings settings = new ToolpickerSettings();
            if (settings.ShowDialog() == DialogResult.OK)
            {
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    Color2ID(cd.Color);
                }
            }
        }

        private void Color2ID(Color color)
        {
            if (MainForm.userdata.ColorFG)
            {
                foreach (uint lst in InsertImageForm.Blocks)
                {
                    if (!colblocks.ContainsValue((int)lst) && !colblocks.ContainsKey(Color.FromArgb((int)Minimap.Colors[lst])))
                    {
                        colblocks.Add(Color.FromArgb((int)Minimap.Colors[lst]), (int)lst);
                    }
                }
            }
            if (MainForm.userdata.ColorBG)
            {
                foreach (uint lst1 in InsertImageForm.Background)
                {
                    if (!colblocks.ContainsValue((int)lst1) && !colblocks.ContainsKey(Color.FromArgb((int)Minimap.Colors[lst1])))
                    {
                        colblocks.Add(Color.FromArgb((int)Minimap.Colors[lst1]), (int)lst1);
                    }
                }
            }
            var bid = closestColor(color);
            editArea.MainForm.setBrick(bid,true);
        }

        private int closestColor(Color col)
        {
            int black = 0;
            double num = 999.0;
            double d = 0.0;
            foreach (KeyValuePair<Color,int> id in colblocks)
            {
                Color color2 = id.Key;
                d = 0.0;
                if (color2.R >= col.R)
                {
                    d += Math.Pow((double)(color2.R - col.R), 2.0);
                }
                else
                {
                    d += Math.Pow((double)(col.R - color2.R), 2.0);
                }
                if (color2.G >= col.G)
                {
                    d += Math.Pow((double)(color2.G - col.G), 2.0);
                }
                else
                {
                    d += Math.Pow((double)(col.G - color2.G), 2.0);
                }
                if (color2.B >= col.B)
                {
                    d += Math.Pow((double)(color2.B - col.B), 2.0);
                }
                else
                {
                    d += Math.Pow((double)(col.B - color2.B), 2.0);
                }
                d = Math.Sqrt(d);
                if (d < num)
                {
                    num = d;
                    black = id.Value;
                }
            }
            return black;
        }
    }
}
