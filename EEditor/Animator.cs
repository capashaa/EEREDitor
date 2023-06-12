using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PlayerIOClient;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EEditor
{
    internal class Animator
    {
        Connection conn;
        private List<BlockCollection> placedBlocks = new List<BlockCollection>();
        private List<BlockCollection> placeBlocks = new List<BlockCollection>();
        List<Frame> frames;
        System.Timers.Timer timer = new System.Timers.Timer();
        List<string[]> firstFrame = new List<string[]>();
        bool correctWay = false;
        private static Semaphore locker;
        Frame remoteFrame;
        List<Frame> remoteFrame_;
        string levelPassword;
        bool shuffle;
        bool reversed;
        bool vertical;
        static int Gtotal;
        static int Gcurrent;
        static int Gcurrent1;
        static int Max1000 = 0;
        static int maxBlocks = 0;
        int botid = 0;
        DateTime epochStartTime;
        System.Threading.Timer passTimer;
        DateTime start;
        object[] param;
        bool restart = false;
        public static System.Windows.Forms.ProgressBar pb; //Make AnimateForm.cs' progressbar work with this upload status
        public static IntPtr afHandle; //Make TaskbarProgress.cs' progressbar work with this upload status
        string[] ignoreMessages = new string[] { "m", "updatemeta", "show", "hide", "k", "init2", "add", "left", "b" };
        public Animator(List<Frame> frames, Connection conn, string levelPassword, bool shuffle, bool reversed, bool vertical)
        {
            locker = new Semaphore(0, 1);
            this.frames = frames;
            this.remoteFrame_ = frames;
            this.conn = conn;
            this.levelPassword = levelPassword;
            this.shuffle = shuffle;
            this.reversed = reversed;
            this.vertical = vertical;
            conn.OnMessage += OnMessage;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public event EventHandler<StatusChangedArgs> StatusChanged;

        protected void OnStatusChanged(string msg, DateTime epoch, bool done = false, int totallines = 0, int countedlines = 0)
        {
            StatusChanged?.Invoke(this, new StatusChangedArgs(msg, epoch, done, totallines, countedlines));
        }

        public void Shuffle(List<string[]> l)
        {
            Random r = new Random();
            for (int i = 0; i < l.Count; ++i)
            {
                int j = r.Next(l.Count);
                string[] t = l[i];
                l[i] = l[j];
                l[j] = t;
            }
        }

        public void reverse(List<string[]> l)
        {
            l.Reverse();
        }

        public void Run()
        {
            conn.Send("init");
            locker.WaitOne();
            Gcurrent1 = 0;
            bool drawblocks = false;
            //var inca = 0;
            //if (frames.Count == 1) inca = 0;
            //else inca = 1;
            List<string[]> diff = null;
            //restart:
            //for (int i = inca; i < frames.Count; i++)
            //{
            start = DateTime.Now;

            firstFrame = frames[0].Diff(remoteFrame);
            ModifyProgressBarColor.SetState(pb, 1);
            TaskbarProgress.SetState(afHandle, TaskbarProgress.TaskbarStates.Normal);
            var dt = System.DateTime.UtcNow;
            if (pb.InvokeRequired) pb.Invoke((MethodInvoker)delegate { pb.Maximum = firstFrame.Count; });
            else pb.Maximum = firstFrame.Count;
            epochStartTime = dt;
            int savexblocks = 0;

        stopit:
            if (frames.Count == 1)
            {
                diff = frames[0].Diff(remoteFrame);
            }
            else
            {
                //diff = frames[i].Diff(remoteFrame);
            }
            if (MainForm.userdata.drawMixed) Shuffle(diff);
            if (!MainForm.userdata.drawMixed && MainForm.userdata.reverse) diff.Reverse();
            if (!MainForm.userdata.drawMixed && MainForm.userdata.random)
            {
                diff.Sort((a, b) => (a.ToString()[0].CompareTo(b.ToString()[0])));
            }
            Gtotal = diff.Count;
            Gcurrent = 0;
            maxBlocks = 0;
            int total = diff.Count;
            //OnStatusChanged("Uploading blocks to level. (Total: " + Gcurrent1 + "/" + Gtotal + ")", dt, false, Gtotal, Gcurrent);
            TaskbarProgress.SetValue(afHandle, Gcurrent, Gtotal); //Set TaskbarProgress.cs' values
            //Queue<string[]> queue = new Queue<string[]>(diff);
            List<string[]> blocks1 = new List<string[]>(diff);
            Queue<string[]> queue = new Queue<string[]>(diff);
            while (queue.Count > 0)
            {
                string[] cur = queue.Dequeue();
                int x, y;
                if (cur[0] != null)
                {
                    x = Convert.ToInt32(cur[0]);
                    y = Convert.ToInt32(cur[1]);
                    if (MainForm.OpenWorld && !MainForm.OpenWorldCode)
                    {
                        drawblocks = y > 4;
                    }
                    else { drawblocks = true; }
                    if (drawblocks)
                    {
                        
                        int blockId = Convert.ToInt32(cur[2]);
                        int layer = Convert.ToInt32(cur[3]);

                        if (layer == 0)
                        {
                            if (remoteFrame.Foreground[y, x] != blockId)
                            {
                                if (bdata.morphable.Contains(blockId))
                                {
                                    correctWay = true;
                                }

                                else if (bdata.goal.Contains(blockId) && blockId != 83 && blockId != 77 && blockId != 1520)
                                {
                                    if (cur.Length == 5)
                                    {
                                        correctWay = true;
                                    }
                                }
                                else if (bdata.rotate.Contains(blockId) && blockId != 385 && blockId != 374)
                                {
                                    if (cur.Length == 5)
                                    {
                                        correctWay = true;
                                    }
                                }
                                else if (blockId == 385)
                                {
                                    if (cur.Length == 6)
                                    {
                                        correctWay = true;
                                    }
                                }
                                else if (blockId == 374)
                                {
                                    if (cur.Length == 6)
                                    {
                                        correctWay = true;
                                    }
                                }
                                else if (blockId == 83 || blockId == 77 || blockId == 1520)
                                {
                                    if (cur.Length == 5)
                                    {
                                        correctWay = true;
                                    }
                                }
                                else if (bdata.portals.Contains(blockId))
                                {
                                    if (cur.Length == 7)
                                    {
                                        correctWay = true;
                                    }
                                }
                                else if (bdata.isNPC(blockId))
                                {
                                    if (cur.Length == 8)
                                    {
                                        correctWay = true;
                                    }
                                }
                                else
                                {
                                    correctWay = true;
                                }
                            }
                            else if (remoteFrame.Foreground[y, x] == blockId)
                            {
                                if (bdata.morphable.Contains(blockId))
                                {
                                    if (cur.Length == 5)
                                    {
                                        if (remoteFrame.BlockData[y, x] != Convert.ToInt32(cur[4]))
                                        {
                                            correctWay = true;
                                        }
                                    }
                                }
                                else if (bdata.goal.Contains(blockId) && blockId != 83 && blockId != 77 && blockId != 1520)
                                {
                                    if (cur.Length == 5)
                                    {
                                        if (remoteFrame.BlockData[y, x] != Convert.ToInt32(cur[4]))
                                        {
                                            correctWay = true;
                                        }
                                    }
                                }
                                else if (bdata.rotate.Contains(blockId) && blockId != 385 && blockId != 374)
                                {
                                    if (cur.Length == 5)
                                    {
                                        if (remoteFrame.BlockData[y, x] != Convert.ToInt32(cur[4]))
                                        {
                                            correctWay = true;
                                        }
                                    }
                                }
                                else if (blockId == 385)
                                {
                                    if (cur.Length == 6)
                                    {
                                        if (remoteFrame.BlockData3[y, x] != cur[5] || remoteFrame.BlockData[y, x] != Convert.ToInt32(cur[4]))
                                        {
                                            correctWay = true;
                                        }
                                    }
                                }
                                else if (bdata.isNPC(blockId))
                                {
                                    if (cur.Length == 8)
                                    {
                                        if (remoteFrame.BlockData3[y, x] != cur[5] || remoteFrame.BlockData4[y, x] != cur[6] || remoteFrame.BlockData5[y, x] != cur[7] || remoteFrame.BlockData6[y, x] != cur[8])
                                        {
                                            correctWay = true;
                                        }
                                    }
                                }
                                else if (blockId == 374)
                                {
                                    if (cur.Length == 6)
                                    {
                                        if (remoteFrame.BlockData3[y, x] != cur[4] || remoteFrame.BlockData[y, x] != Convert.ToInt32(cur[5]))
                                        {
                                            correctWay = true;
                                        }
                                    }
                                }
                                else if (blockId == 83 || blockId == 77 || blockId == 1520)
                                {
                                    if (cur.Length == 5)
                                    {
                                        if (remoteFrame.BlockData[y, x] != Convert.ToInt32(cur[4]))
                                        {
                                            correctWay = true;
                                        }
                                    }
                                }
                                else if (bdata.portals.Contains(blockId))
                                {
                                    if (cur.Length == 7)
                                    {
                                        if (remoteFrame.BlockData[y, x] != Convert.ToInt32(cur[4]) || remoteFrame.BlockData1[y, x] != Convert.ToInt32(cur[5]) || remoteFrame.BlockData2[y, x] != Convert.ToInt32(cur[6]))
                                        {
                                            correctWay = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (layer == 1)
                        {
                            if (remoteFrame.Background[y, x] != blockId)
                            {
                                correctWay = true;
                            }
                        }
                        if (correctWay)
                        {
                            if (bdata.morphable.Contains(blockId) && blockId != 385)
                            {
                                param = new object[] { layer, x, y, blockId, Convert.ToInt32(cur[4]) };
                            }
                            else if (bdata.goal.Contains(blockId))
                            {
                                if (cur.Length == 5)
                                {
                                    param = new object[] { layer, x, y, blockId, Convert.ToInt32(cur[4]) };
                                }
                            }
                            else if (bdata.rotate.Contains(blockId) && blockId != 385 && blockId != 374)
                            {
                                if (cur.Length == 5)
                                {
                                    param = new object[] { layer, x, y, blockId, Convert.ToInt32(cur[4]) };
                                }
                            }
                            else if (blockId == 385)
                            {
                                if (cur.Length == 6)
                                {
                                    param = new object[] { layer, x, y, blockId, cur[5], Convert.ToInt32(cur[4]) };
                                }
                            }
                            else if (blockId == 374)
                            {
                                if (cur.Length == 6)
                                {
                                    param = new object[] { layer, x, y, blockId, cur[4], Convert.ToInt32(cur[5]) };
                                }
                            }
                            else if (bdata.isNPC(blockId))
                            {
                                if (cur.Length == 8)
                                {
                                    param = new object[] { layer, x, y, blockId, cur[4], cur[5], cur[6], cur[7] };
                                }
                            }
                            else if (blockId == 77 || blockId == 83 || blockId == 1520)
                            {
                                if (cur.Length == 5)
                                {
                                    param = new object[] { layer, x, y, blockId, int.Parse(cur[4]) };
                                }
                            }
                            else if (bdata.portals.Contains(blockId))
                            {
                                if (cur.Length == 7)
                                {
                                    param = new object[] { layer, x, y, blockId, Convert.ToInt32(cur[4]), Convert.ToInt32(cur[5]), Convert.ToInt32(cur[6]) };
                                }
                            }
                            else
                            {
                                if (MainForm.userdata.level.StartsWith("OW") && MainForm.userdata.levelPass.Length > 0)
                                {
                                    param = new object[] { layer, x, y, blockId };
                                }
                                else if (MainForm.userdata.level.StartsWith("OW") && MainForm.userdata.levelPass.Length == 0)
                                {
                                    if (y > 4)
                                    {
                                        param = new object[] { layer, x, y, blockId };
                                    }
                                }
                                else
                                {
                                    param = new object[] { layer, x, y, blockId };
                                }
                            }
                            if (conn == null)
                            {
                                OnStatusChanged("Lost connection!", DateTime.MinValue, true, Gtotal, Gcurrent);
                                return;
                            }
                            if (MainForm.userdata.SaveXBlocks == savexblocks && MainForm.userdata.saveWorldCrew)
                            {

                                    if (AnimateForm.saveRights)
                                    {
                                        {
                                            conn.Send("save");
                                        }
                                    }
                                
                                savexblocks = 0;
                            }
                            else if (savexblocks < MainForm.userdata.SaveXBlocks && MainForm.userdata.saveWorldCrew)
                            {
                                savexblocks += 1;
                            }
                            
                            OnStatusChanged("", epochStartTime, false, firstFrame.Count, Gcurrent1);
                            int progress = (int)Math.Round((double)(100 * Gcurrent) / Gtotal);
                            if (progress == 50) goto stopit;
                            else if (progress == 90) goto stopit;
                            if (restart) { restart = false; goto stopit; }

                            if (param != null)
                            {
                                sendParam(conn, param);
                                Thread.Sleep(MainForm.userdata.uploadDelay);
                            }
                        }
                        else
                        {
                            if (Gcurrent1 >= firstFrame.Count)
                            {
                                break;
                            }
                            else
                            {
                                queue.Enqueue(cur);
                            }
                            OnStatusChanged("Uploading blocks to level. (Total: " + Gcurrent1 + "/" + firstFrame.Count + ")", epochStartTime, false, Gtotal, Gcurrent);
                            if (pb.InvokeRequired) pb.Invoke((MethodInvoker)delegate { pb.Maximum = firstFrame.Count; });
                            TaskbarProgress.SetValue(afHandle, Gcurrent, firstFrame.Count);
                            break;
                        }
                    }
                }


            }
            OnStatusChanged("Level upload complete!", DateTime.MinValue, true, firstFrame.Count, Gcurrent1);
            Gcurrent1 = 0;
            TaskbarProgress.SetValue(afHandle, Gcurrent1, firstFrame.Count);
            if (MainForm.userdata.saveWorldCrew)
            {
                if (AnimateForm.saveRights)
                {
                    {
                        conn.Send("save");
                    }
                }
            }
        }

        static async void sendParam(Connection con, object[] param)
        {
            await Task.Run(() => con.Send("b", param));
        }

        void OnMessage(object sender, PlayerIOClient.Message e)
        {
            if (e.Type == "b")
            {
                if (botid == (int)e.GetInt(4))
                {
                    int layer = e.GetInt(0), x = e.GetInt(1), y = e.GetInt(2), id = e.GetInt(3);
                    if (layer == 0) { remoteFrame.Foreground[y, x] = id; } else { remoteFrame.Background[y, x] = id; }
                    ++Gcurrent;
                    ++Gcurrent1;
                    OnStatusChanged("Uploading blocks to level. (Total: " + Gcurrent1 + "/" + firstFrame.Count + ")", DateTime.MinValue, false, Gtotal, Gcurrent);
                    if (Convert.ToDouble(Gcurrent1) <= pb.Maximum && Convert.ToDouble(Gcurrent1) >= pb.Minimum) 
                    { 
                        if (pb.InvokeRequired) pb.Invoke((MethodInvoker)delegate { pb.Value = Gcurrent1; }); 
                        TaskbarProgress.SetValue(afHandle, Gcurrent1, firstFrame.Count); 
                    }
                    
                }
            }
            else if (e.Type == "br")
            {
                if (botid == (int)e.GetInt(5))
                {
                    int x = e.GetInt(0), y = e.GetInt(1);
                    remoteFrame.Foreground[y, x] = e.GetInt(2);
                    remoteFrame.BlockData[y, x] = (int)e.GetInt(3);
                    ++Gcurrent;
                    ++Gcurrent1;
                    OnStatusChanged("Uploading rotation blocks to level. (Total: " + Gcurrent1 + "/" + firstFrame.Count + ")", DateTime.MinValue, false, Gtotal, Gcurrent);
                    if (Convert.ToDouble(Gcurrent1) <= pb.Maximum && Convert.ToDouble(Gcurrent1) >= pb.Minimum) { if (pb.InvokeRequired) pb.Invoke((MethodInvoker)delegate { pb.Value = Gcurrent1; }); TaskbarProgress.SetValue(afHandle, Gcurrent1, firstFrame.Count); }
                }
            }
            else if (e.Type == "bc")
            {
                if (botid == (int)e.GetInt(4))
                {
                    int x = e.GetInt(0), y = e.GetInt(1);
                    remoteFrame.Foreground[y, x] = e.GetInt(2);
                    remoteFrame.BlockData[y, x] = e.GetInt(3);
                    ++Gcurrent;
                    ++Gcurrent1;
                    OnStatusChanged("Uploading numbered action blocks to level. (Total: " + Gcurrent1 + "/" + firstFrame.Count + ")", DateTime.MinValue, false, Gtotal, Gcurrent);
                    if (Convert.ToDouble(Gcurrent1) <= pb.Maximum && Convert.ToDouble(Gcurrent1) >= pb.Minimum) { if (pb.InvokeRequired) pb.Invoke((MethodInvoker)delegate { pb.Value = Gcurrent1; }); TaskbarProgress.SetValue(afHandle, Gcurrent1, firstFrame.Count); }
                    //placeBlocks.Remove(new blocks() { layer = 0, x = e.GetInt(0), y = e.GetInt(1), bid = e.GetInt(2), data = new object[] { e.GetInt(3) } });

                }
            }
            else if (e.Type == "wp")
            {
                if (botid == (int)e.GetInt(5))
                {
                    int x = e.GetInt(0), y = e.GetInt(1);
                    remoteFrame.Foreground[y, x] = e.GetInt(2);
                    remoteFrame.BlockData3[y, x] = e.GetString(3);
                    ++Gcurrent;
                    ++Gcurrent1;
                    OnStatusChanged("Uploading world portals to level. (Total: " + Gcurrent1 + "/" + firstFrame.Count + ")", DateTime.MinValue, false, Gtotal, Gcurrent);
                    if (Convert.ToDouble(Gcurrent1) <= pb.Maximum && Convert.ToDouble(Gcurrent1) >= pb.Minimum) { if (pb.InvokeRequired) pb.Invoke((MethodInvoker)delegate { pb.Value = Gcurrent1; }); TaskbarProgress.SetValue(afHandle, Gcurrent1, firstFrame.Count); }
                    //placeBlocks.Remove(new blocks() { layer = 0, x = e.GetInt(0), y = e.GetInt(1), bid = e.GetInt(2), data = new object[] { e[3], e.GetInt(4) } });
                }

            }
            else if (e.Type == "ts")
            {
                if (botid == (int)e.GetInt(5))
                {
                    int x = e.GetInt(0), y = e.GetInt(1);
                    remoteFrame.Foreground[y, x] = e.GetInt(2);
                    remoteFrame.BlockData3[y, x] = e.GetString(3);
                    remoteFrame.BlockData[y, x] = e.GetInt(4);
                    ++Gcurrent;
                    ++Gcurrent1;
                    OnStatusChanged("Uploading signs to level. (Total: " + Gcurrent1 + "/" + firstFrame.Count + ")", DateTime.MinValue, false, Gtotal, Gcurrent);
                    if (Convert.ToDouble(Gcurrent1) <= pb.Maximum && Convert.ToDouble(Gcurrent1) >= pb.Minimum) { if (pb.InvokeRequired) pb.Invoke((MethodInvoker)delegate { pb.Value = Gcurrent1; }); TaskbarProgress.SetValue(afHandle, Gcurrent1, firstFrame.Count); }
                    //placeBlocks.Remove(new blocks() { layer = 0, x = e.GetInt(0), y = e.GetInt(1), bid = e.GetInt(2), data = new object[] { e[3], e.GetInt(4)} });
                }
            }
            else if (e.Type == "bn")
            {
                if (botid == (int)e.GetInt(7))
                {
                    int x = e.GetInt(0), y = e.GetInt(1);
                    remoteFrame.Foreground[y, x] = e.GetInt(2);
                    remoteFrame.BlockData3[y, x] = e.GetString(3);
                    remoteFrame.BlockData4[y, x] = e.GetString(4);
                    remoteFrame.BlockData5[y, x] = e.GetString(5);
                    remoteFrame.BlockData6[y, x] = e.GetString(6);
                    ++Gcurrent;
                    ++Gcurrent1;
                    OnStatusChanged("Uploading NPC's to level. (Total: " + Gcurrent1 + "/" + firstFrame.Count + ")", DateTime.MinValue, false, Gtotal, Gcurrent);
                    if (Convert.ToDouble(Gcurrent1) <= pb.Maximum && Convert.ToDouble(Gcurrent1) >= pb.Minimum) { if (pb.InvokeRequired) pb.Invoke((MethodInvoker)delegate { pb.Value = Gcurrent1; }); TaskbarProgress.SetValue(afHandle, Gcurrent1, firstFrame.Count); }
                    //placeBlocks.Remove(new blocks() { layer = 0, x = e.GetInt(0), y = e.GetInt(1), bid = e.GetInt(2), data = new object[] { e[3], e[4], e[5], e[6] } });
                }
            }
            else if (e.Type == "pt")
            {
                if (botid == (int)e.GetInt(6))
                {
                    int x = e.GetInt(0), y = e.GetInt(1);
                    remoteFrame.Foreground[y, x] = e.GetInt(2);
                    remoteFrame.BlockData[y, x] = e.GetInt(3);
                    remoteFrame.BlockData1[y, x] = e.GetInt(4);
                    remoteFrame.BlockData2[y, x] = e.GetInt(5);
                    ++Gcurrent;
                    ++Gcurrent1;
                    OnStatusChanged("Uploading portals to level. (Total: " + Gcurrent1 + "/" + firstFrame.Count + ")", DateTime.MinValue, false, Gtotal, Gcurrent);
                    if (Convert.ToDouble(Gcurrent1) <= pb.Maximum && Convert.ToDouble(Gcurrent1) >= pb.Minimum) { if (pb.InvokeRequired) pb.Invoke((MethodInvoker)delegate { pb.Value = Gcurrent1; }); TaskbarProgress.SetValue(afHandle, Gcurrent1, firstFrame.Count); }
                    //placeBlocks.Remove(new blocks() { layer = 0, x = e.GetInt(0), y = e.GetInt(1), bid = e.GetInt(2), data = new object[] { e.GetInt(3), e.GetInt(4), e.GetInt(5) } });
                }
            }
            else if (e.Type == "bs")
            {
                if (botid == (int)e.GetInt(4))
                {
                    int x = e.GetInt(0), y = e.GetInt(1);
                    remoteFrame.Foreground[y, x] = e.GetInt(2);
                    remoteFrame.BlockData[y, x] = e.GetInt(3);
                    ++Gcurrent;
                    ++Gcurrent1;
                    OnStatusChanged("Uploading noteblocks to level. (Total: " + Gcurrent1 + "/" + firstFrame.Count + ")", DateTime.MinValue, false, Gtotal, Gcurrent);
                    if (Convert.ToDouble(Gcurrent1) <= pb.Maximum && Convert.ToDouble(Gcurrent1) >= pb.Minimum) { if (pb.InvokeRequired) pb.Invoke((MethodInvoker)delegate { pb.Value = Gcurrent1; }); TaskbarProgress.SetValue(afHandle, Gcurrent1, firstFrame.Count); }
                    //placeBlocks.Remove(new blocks() { layer = 0, x = e.GetInt(0), y = e.GetInt(1), bid = e.GetInt(2), data = new object[] { e.GetInt(3) } });
                }
            }
            else if (e.Type == "access")
            {
                passTimer.Dispose();
                locker.Release();
            }
            else if (e.Type == "init")
            {
                botid = e.GetInt(5);
                AnimateForm.editRights = false;
                AnimateForm.saveRights = false;
                AnimateForm.crewEdit = false;
                AnimateForm.crewWorld = false;
                remoteFrame = Frame.FromMessage(e);
                conn.Send("init2");
                OnStatusChanged("Connected to the world.", DateTime.MinValue, false, 0, 0);
                if (frames[0].Width <= remoteFrame.Width && frames[0].Height <= remoteFrame.Height)
                {
                }
                else
                {
                    Gtotal = Gcurrent = pb.Maximum = pb.Value = 1;
                    ModifyProgressBarColor.SetState(pb, 2);
                    TaskbarProgress.SetValue(afHandle, Gcurrent, Gtotal);
                    TaskbarProgress.SetState(afHandle, TaskbarProgress.TaskbarStates.Error);
                    OnStatusChanged("Wrong level size. Please create a level with the size of " + remoteFrame.Width + "x" + remoteFrame.Height + ".", DateTime.MinValue, true, 0, 0);
                    return;
                }
                if (e.GetBoolean(34))
                {
                    AnimateForm.crewWorld = true;
                    if (e.GetBoolean(14))
                    {
                        AnimateForm.crewEdit = true;
                        if (e.GetBoolean(31))
                        {
                            AnimateForm.saveRights = true;
                            if (MainForm.userdata.useColor)
                            {
                                if (MainForm.userdata.thisColor != Color.Transparent)
                                {
                                    var hex = ColorTranslator.ToHtml(MainForm.userdata.thisColor);
                                    conn.Send("say", "/bgcolor " + hex);
                                }
                                else
                                {
                                    conn.Send("say", "/bgcolor none");
                                }
                            }
                            else
                            {
                                conn.Send("say", "/bgcolor none");
                            }
                        }
                        locker.Release();
                    }
                    else
                    {
                        OnStatusChanged("Crew: You doesn't have edit rights in this world", DateTime.MinValue, true, 0, 0);
                        return;
                    }
                }
                else if (e.GetString(0) != e.GetString(13))
                {
                    if (MainForm.userdata.level.StartsWith("OW") && levelPassword.Length == 0)
                    {
                        if (e.GetBoolean(14))
                        {
                            MainForm.OpenWorld = true;
                            MainForm.OpenWorldCode = false;
                            locker.Release();
                        }
                        else
                        {
                            MainForm.OpenWorld = false;
                            MainForm.OpenWorldCode = false;
                            OnStatusChanged("You need a password for this world", DateTime.MinValue, true, 0, 0);
                            return;
                        }
                    }
                    else if (MainForm.userdata.level.StartsWith("OW") && levelPassword.Length > 0)
                    {
                        if (!e.GetBoolean(14))
                        {
                            MainForm.OpenWorld = true;
                            MainForm.OpenWorldCode = true;
                            conn.Send("access", levelPassword);
                            passTimer = new System.Threading.Timer(x => OnStatusChanged("Wrong level code. Please enter the right one and retry.", DateTime.MinValue, true, 0, 0), null, 5000, Timeout.Infinite);
                        }
                        else
                        {
                            MainForm.OpenWorld = true;
                            MainForm.OpenWorldCode = false;
                            OnStatusChanged("This world isn't password protected", DateTime.MinValue, true, 0, 0);
                            return;
                        }
                    }
                    else
                    {
                        if (MainForm.userdata.saveWorldCrew)
                        {
                            OnStatusChanged("You are not the owner of this world. You can't save.", DateTime.MinValue, true, 0, 0);
                            return;
                        }
                        else
                        {
                            if (levelPassword.Length > 0)
                            {
                                Gtotal = Gcurrent = pb.Maximum = pb.Value = 1;
                                ModifyProgressBarColor.SetState(pb, 3);
                                TaskbarProgress.SetValue(afHandle, Gcurrent, Gtotal);
                                TaskbarProgress.SetState(afHandle, TaskbarProgress.TaskbarStates.Paused);
                                conn.Send("access", levelPassword);
                                passTimer = new System.Threading.Timer(x => OnStatusChanged("Wrong level code. Please enter the right one and retry.", DateTime.MinValue, true, 0, 0), null, 5000, Timeout.Infinite);
                            }
                            else if (e.GetBoolean(14))
                            {
                                AnimateForm.editRights = true;
                                locker.Release();
                            }
                            else
                            {
                                passTimer = new System.Threading.Timer(x => OnStatusChanged("Didn't get edit. Timer stopped.", DateTime.MinValue, true, 0, 0), null, 20000, Timeout.Infinite);
                            }
                        }
                    }
                }
                else if (e.GetString(0) == e.GetString(13))
                {
                    if (MainForm.userdata.useColor)
                    {
                        if (MainForm.userdata.thisColor != Color.Transparent)
                        {
                            var hex = ColorTranslator.ToHtml(MainForm.userdata.thisColor);
                            conn.Send("say", "/bgcolor " + hex);
                        }
                        else
                        {
                            conn.Send("say", "/bgcolor none");
                        }
                    }
                    else
                    {
                        conn.Send("say", "/bgcolor none");
                    }
                    AnimateForm.editRights = true;
                    AnimateForm.saveRights = true;
                    locker.Release();
                }
            }
            else
            {
                switch (e.Type)
                {
                    case "info":
                        switch (e[0].ToString())
                        {
                            case "Limit reached":
                                OnStatusChanged("Limit Reached.", DateTime.MinValue, true, 0, 0);
                                break;
                            case "World not available":
                                OnStatusChanged("World is not availabe.", DateTime.MinValue, true, 0, 0);
                                break;
                            case "You are banned":
                                OnStatusChanged("You have been kicked.", DateTime.MinValue, true, 0, 0);
                                break;
                            default:
                                Console.WriteLine(e.ToString());
                                break;
                        }
                        break;
                }
            }
        }
    }

    public static class upl
    {
        public static object[] blockdata { get; set; }
    }
    public struct BlockCollection : IEquatable<BlockCollection>
    {
        public int Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int BlockId { get; set; }
        public object[] BlockData { get; set; }

        public override bool Equals(object other)
            => other is BlockCollection collection
            && Equals(collection);

        public bool Equals(BlockCollection other)
            => Layer == other.Layer
            && X == other.X
            && Y == other.Y
            && BlockId == other.BlockId
            && BlockData.SequenceEqual(other.BlockData);

        public override int GetHashCode()
        {
            var hashCode = -523089506;
            hashCode = hashCode * -1521134295 + Layer.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + BlockId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<object[]>.Default.GetHashCode(BlockData);
            return hashCode;
        }

        public static bool operator ==(BlockCollection left, BlockCollection right) => left.Equals(right);
        public static bool operator !=(BlockCollection left, BlockCollection right) => !(left == right);
    }
}
