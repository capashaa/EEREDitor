using System;
using System.Collections.Generic;
using PlayerIOClient;
using System.Drawing;

namespace EEditor
{
    public static class InitParse
    {
        public static DataChunk[] Parse(Message m)
        {
            if (m == null) throw new ArgumentNullException(nameof(m));
            if (m.Type != "init" && m.Type != "reset") throw new ArgumentException("Invalid message type.", nameof(m));

            // Get world data
            var p = 0u;
            var data = new Stack<object>();
            object[] dataGone = new object[] { 0 };
            //while (m[p++] as string != "ws") { }
            while (m[p] as string != "ws") { p++; }
            //if (!method) while (m[p++] as string != "ws") { }
            //while (m[p++] as string != "ws") { }
            while (m[p] as string != "we") { data.Push(m[++p]); }

            // Parse world data
            var chunks = new List<DataChunk>();
            byte[] yys = null;
            byte[] xxs = null;
            int ttype = 0;
            int llayer = 0;
            while (data.Count > 0)
            {
                var aargs = new Stack<object>();
                while (data.Count > 0 && !(data.Peek() is byte[]))
                    if (data.Count > 0) aargs.Push(data.Pop());

                if (data.Count > 0) yys = (byte[])data.Pop();
                if (data.Count > 0) xxs = (byte[])data.Pop();
                if (data.Count > 0) llayer = (int)data.Pop();
                if (data.Count > 0) ttype = (int)(uint)data.Pop();
                else
                {
                    data.CopyTo(dataGone,0);
                    //Console.WriteLine(dataGone[0]);
                }
                chunks.Add(new DataChunk(llayer, ttype, xxs, yys, aargs.ToArray()));


            }

            return chunks.ToArray();
        }
    }

    public class DataChunk
    {
        public int Layer { get; set; }
        public int Type { get; set; }
        public Point[] Locations { get; set; }
        public object[] Args { get; set; }

        public DataChunk(int layer, int type, byte[] xs, byte[] ys, object[] args)
        {
            this.Layer = layer;
            this.Type = type;
            this.Args = args;
            this.Locations = GetLocations(xs, ys);
        }

        private static Point[] GetLocations(byte[] xs, byte[] ys)
        {
            var points = new List<Point>();
            if (xs != null)
                for (var i = 0; i < xs.Length; i += 2)
                    points.Add(new Point(
                        (xs[i] << 8) | xs[i + 1],
                        (ys[i] << 8) | ys[i + 1]));
                return points.ToArray();
            
        }
    }

    // TODO: Uncomment this if using Console Application
    //public struct Point
    //{
    //    public int X { get; set; }
    //    public int Y { get; set; }

    //    public Point(int x, int y) : this()
    //    {
    //        this.X = x;
    //        this.Y = y;
    //    }
    //}
}
