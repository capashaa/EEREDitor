using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Linq;

namespace EEditor
{
    class ToolFill : Tool
    {
        public int id = 0;
        public int target = 0;
        public int[,] dataf;
        public int[,] datafg;
        public bool isbackground = false;
        public Thread thread;
        public int pen = 0;
        public int incr = 0;
        public static bool filling = false;
        public static bool stopfill = false;
        public ToolFill(EditArea editArea)
            : base(editArea)
        {
        }

        private void Fill(int startx, int starty, bool sdown)
        {
            ThreadPool.QueueUserWorkItem((object param0) => {
                filling = true;
                stopfill = false;
                string incfg = null;
                int width = editArea.BlockWidth;
                int height = editArea.BlockHeight;
                startx = Math.Min(startx, width - 1);
                starty = Math.Min(starty, height - 1);
                startx = Math.Max(0, startx);
                starty = Math.Max(0, starty);
                int oldID1 = editArea.CurFrame.Background[starty, startx];
                int oldID0 = editArea.CurFrame.Foreground[starty, startx];
                int coins = pen == 43 ? 1 : 0;

                if (pen == 242) {
                    id = 0;
                    target = 0;
                }
                if (pen >= 500 && pen <= 999) {
                    if (oldID1 >= 500 && oldID1 <= 999) {
                        if (oldID1 == pen) {
                            filling = false;
                            return;
                        }
                    } else {
                        if (oldID1 == pen) {
                            filling = false;
                            return;
                        }
                    }
                } else {
                    if (oldID1 >= 500 && oldID1 <= 999) {
                        if (oldID1 == pen) {
                            filling = false;
                            return;
                        }
                    } else {
                        if (oldID0 == pen) {
                            filling = false;
                            return;
                        }
                    }
                }

                int[] dx = new int[] { 1, -1, 0, 0 };
                int[] dy = new int[] { 0, 0, -1, 1 };

                Queue<int> queue = new Queue<int>();
                queue.Enqueue(startx);
                queue.Enqueue(starty);
                Graphics g = Graphics.FromImage(editArea.Back);
                if (pen >= 500 && pen <= 999) {
                    dataf = editArea.CurFrame.Background;
                }

                if (pen < 500 || pen >= 1001) {
                    if (oldID1 >= 500 && oldID1 <= 999 && pen == 0) {
                        dataf = editArea.CurFrame.Background;
                    } else {
                        dataf = editArea.CurFrame.Foreground;
                    }
                }
                datafg = editArea.CurFrame.Foreground;
                var total = queue.Count;
                while (queue.Count >= 2 && !stopfill) {
                    int x = queue.Dequeue();
                    int y = queue.Dequeue();

                    if (editArea.IsBackground) {
                        if (oldID1 >= 500 && oldID1 <= 999 && pen == 0) {
                            if (dataf[y, x] == oldID1) {
                                incfg += pen + ":" + dataf[y, x] + ":" + x + ":" + y + ":";
                                dataf[y, x] = pen;
                                if (ToolPen.rotation.ContainsKey(pen)) {
                                    editArea.CurFrame.BlockData[y, x] = ToolPen.rotation[pen];
                                }
                                if (editArea.InvokeRequired) {
                                    editArea.Invoke((MethodInvoker)delegate {
                                        editArea.Draw(x, y, g, MainForm.userdata.thisColor);
                                    });
                                } else {
                                    editArea.Draw(x, y, g, MainForm.userdata.thisColor);
                                }
                                //g.DrawImage(editArea.Bricks[pen], x * 16, y * 16);
                                for (int k = 0; k < 4; ++k) {
                                    int nx = x + dx[k];
                                    int ny = y + dy[k];
                                    if (0 <= nx && nx < width && 0 <= ny && ny < height) {
                                        queue.Enqueue(nx);
                                        queue.Enqueue(ny);
                                    }
                                }
                            }
                        } else if (pen >= 500 && pen <= 999) {
                            //if (dataf[y, x] == oldID1 && datafg[y, x] == oldID0 && !MainForm.userdata.IgnoreBlocks.Contains(oldID0)) {
                            if (dataf[y, x] == oldID1  && datafg[y,x] == oldID0) {
                            incfg += pen + ":" + dataf[y, x] + ":" + x + ":" + y + ":";
                                dataf[y, x] = pen;
                                if (editArea.InvokeRequired) {
                                    editArea.Invoke((MethodInvoker)delegate {
                                        editArea.Draw(x, y, g, MainForm.userdata.thisColor);
                                    });
                                } else {
                                    editArea.Draw(x, y, g, MainForm.userdata.thisColor);
                                }
                                //g.DrawImage(editArea.Bricks[pen], x * 16, y * 16);
                                for (int k = 0; k < 4; ++k) {
                                    int nx = x + dx[k];
                                    int ny = y + dy[k];
                                    if (0 <= nx && nx < width && 0 <= ny && ny < height) {
                                        queue.Enqueue(nx);
                                        queue.Enqueue(ny);
                                    }
                                }
                            }
                        } else {
                            if (dataf[y, x] == oldID0) {
                                incfg += pen + ":" + dataf[y, x] + ":" + x + ":" + y + ":";
                                dataf[y, x] = pen;
                                if (bdata.portals.Contains(pen)) {
                                    if (ToolPen.rotation.ContainsKey(pen)) editArea.CurFrame.BlockData[y, x] = ToolPen.rotation[pen];
                                    else editArea.CurFrame.BlockData[y, x] = 0;
                                    if (ToolPen.id.ContainsKey(pen)) editArea.CurFrame.BlockData1[y, x] = ToolPen.id[pen];
                                    else editArea.CurFrame.BlockData1[y, x] = 0;
                                    if (ToolPen.target.ContainsKey(pen)) editArea.CurFrame.BlockData2[y, x] = ToolPen.target[pen];
                                    else editArea.CurFrame.BlockData2[y, x] = 0;
                                } else if (pen == 374 || pen == 385) {
                                    if (pen == 385) {
                                        if (ToolPen.rotation.ContainsKey(pen)) editArea.CurFrame.BlockData[y, x] = ToolPen.rotation[pen];
                                        else editArea.CurFrame.BlockData[y, x] = 0;
                                    }
                                    if (ToolPen.text.ContainsKey(pen)) editArea.CurFrame.BlockData3[y, x] = ToolPen.text[pen];
                                    else editArea.CurFrame.BlockData3[y, x] = "Sign Text";
                                } else {
                                    if (ToolPen.rotation.ContainsKey(pen)) editArea.CurFrame.BlockData[y, x] = ToolPen.rotation[pen];
                                    else editArea.CurFrame.BlockData[y, x] = 0;
                                }
                                if (editArea.InvokeRequired) {
                                    editArea.Invoke((MethodInvoker)delegate {
                                        editArea.Draw(x, y, g, MainForm.userdata.thisColor);
                                    });
                                } else {
                                    editArea.Draw(x, y, g, MainForm.userdata.thisColor);
                                }
                                //g.DrawImage(editArea.Bricks[pen], x * 16, y * 16);
                                for (int k = 0; k < 4; ++k) {
                                    int nx = x + dx[k];
                                    int ny = y + dy[k];
                                    if (0 <= nx && nx < width && 0 <= ny && ny < height) {
                                        queue.Enqueue(nx);
                                        queue.Enqueue(ny);
                                    }
                                }
                            }
                        }
                    }
                    incr += 1;
                    //MainForm.editArea.MainForm.tsp.Value = Convert.ToInt32((double)incr / total * 100);
                }
                ToolPen.undolist.Push(incfg);
                g.Save();
                editArea.Invalidate();
                filling = false;
                stopfill = false;
                /*try
                {
                    thread.Abort();
                }
                catch
                {

                }*/
            });
        }

        public override void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!filling)
                {
                    pen = PenID;
                    Point p = GetLocation(e);
                    if (editArea.ShiftDown)
                    {
                        thread = new Thread(() => Fill(p.X, p.Y, true));
                        thread.Start();
                    }
                    else
                    {
                        thread = new Thread(() => Fill(p.X, p.Y, false));
                        thread.Start();
                    }
                }
            }
        }
    }
}
