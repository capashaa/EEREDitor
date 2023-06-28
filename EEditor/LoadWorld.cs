using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using PlayerIOClient;
using static System.Net.Mime.MediaTypeNames;
using static World;
namespace EEditor
{
    internal class LoadWorld
    {
        public static Semaphore s1 = new Semaphore(0, 1);
        public static Frame LoadData(string roomid, bool bigdb)
        {
            Frame frame = null;
            Client sel = SelectAccount();
            if (sel != null)
            {

                if (bigdb)
                {
                    var world = new World(InputType.BigDB, roomid, sel);
                    frame = new Frame(world.Width, world.Height);
                    frame.nickname = world.OwnerUsername;
                    frame.levelname = world.Title;
                    frame.owner = world.Owner;
                    if (world.BackgroundColorUint != 0)
                    {
                        MainForm.userdata.useColor = true;
                        MainForm.userdata.thisColor = world.BackgroundColor;
                    }
                    foreach (var item in world.Blocks)
                    {
                        int layer = item.Layer;
                        int bid = item.Type;
                        foreach (var pos in item.Locations)
                        {
                            if (layer == 0)
                            {
                                frame.Foreground[pos.Y, pos.X] = bid;
                                if (item.Name != null) frame.BlockData3[pos.Y, pos.X] = item.Name;
                                if (item.TextMessage1 != null) frame.BlockData4[pos.Y, pos.X] = item.TextMessage1;
                                if (item.TextMessage2 != null) frame.BlockData5[pos.Y, pos.X] = item.TextMessage2;
                                if (item.TextMessage3 != null) frame.BlockData6[pos.Y, pos.X] = item.TextMessage3;
                                if (item.Goal.ToString() != null) frame.BlockData[pos.Y, pos.X] = item.Goal;
                                if (item.SignType.ToString() != null) frame.BlockData[pos.Y, pos.X] = item.SignType;
                                if (item.Text != null) frame.BlockData3[pos.Y, pos.X] = item.Text;
                                if (item.Rotation.ToString() != null) frame.BlockData[pos.Y, pos.X] = item.Rotation;
                                if (Convert.ToString(item.Id) != null)
                                {
                                    if (bdata.sound.Contains(bid))
                                    {
                                        frame.BlockData[pos.Y, pos.X] = (int)Convert.ToUInt32(item.Id);
                                    }
                                    else
                                    {
                                        frame.BlockData1[pos.Y, pos.X] = Convert.ToInt32(item.Id);
                                    }
                                }
                                if (bid == 242 || bid == 381) frame.BlockData2[pos.Y, pos.X] = Convert.ToInt32(item.Target);
                                if (bid == 374) frame.BlockData3[pos.Y, pos.X] = Convert.ToString(item.Target);
                                if (bid == 1000)
                                {
                                    if (item.Text != null)
                                    {
                                        int wrap = 200;
                                        string hexcolor = "#FFFFFF";
                                        frame.BlockData3[pos.Y, pos.X] = item.Text;
                                        if (item.Hex != null)
                                        {
                                            hexcolor = item.Hex;
                                        }
                                        if (item.Wrap.ToString() != null)
                                        {
                                            wrap = item.Wrap;
                                        }
                                        frame.BlockData4[pos.Y, pos.X] = hexcolor;
                                        frame.BlockData[pos.Y, pos.X] = wrap;
                                    }
                                }
                            }
                            else
                            {
                                frame.Background[pos.Y, pos.X] = bid;
                            }

                        }
                    }
                    return frame;
                }
                else
                {
                    sel.Multiplayer.CreateJoinRoom(roomid, $"{bdata.normal_room}{sel.BigDB.Load("config", "config")["version"]}", false, null, null, (Connection con) =>
                    {

                        con.Send("init");
                        con.OnMessage += (object sender, PlayerIOClient.Message m) =>
                        {

                            switch (m.Type)
                            {
                                case "init":
                                    frame = Frame.FromMessage(m, m.GetInt(18), m.GetInt(19));
                                    con.Send("init2");
                                    break;
                                case "init2":
                                    s1.Release();
                                    break;
                            }
                        };
                        con.OnDisconnect += (object sender, string reason) =>
                        {
                            Console.WriteLine(reason);
                            s1.Release();
                        };

                    });
                    s1.WaitOne();
                }
                /*int w = 0;
                int h = 0;
                DatabaseObject dbo = client.BigDB.Load("Worlds", MainForm.userdata.level);
                if (dbo != null)
                {
                    Console.WriteLine(dbo.ToString());
                    var name = dbo.Contains("title") ? dbo["title"].ToString() : "Untitled World";
                    owner = dbo.Contains("ownerusername") ? dbo["ownerusername"].ToString() : "Unknown Owner";
                    if (dbo.Contains("bg"))
                    {
                        EEditor.MainForm.userdata.useColor = true;
                        EEditor.MainForm.userdata.thisColor = UIntToColor(Convert.ToUInt32(dbo["bg"]));
                    }
                    if (dbo.Contains("width") && dbo.Contains("height") && dbo.Contains("blocks"))
                    {
                        updateData(name, owner, Convert.ToInt32(dbo["width"]), Convert.ToInt32(dbo["height"]));
                        MapFrame = new Frame(Convert.ToInt32(dbo["width"]), Convert.ToInt32(dbo["height"]));
                    }
                    else
                    {
                        if (dbo.Contains("type"))
                        {
                            switch (dbo["type"])
                            {
                                case "world1":
                                    w = 50;
                                    h = 50;
                                    break;
                                case "world2":
                                    w = 100;
                                    h = 100;
                                    break;
                                default:
                                case "world3":
                                    w = 200;
                                    h = 200;
                                    break;
                                case "world4":
                                    w = 400;
                                    h = 50;
                                    break;
                                case "world5":
                                    w = 400;
                                    h = 200;
                                    break;
                                case "world6":
                                    w = 100;
                                    h = 400;
                                    break;
                                case "world7":
                                    w = 636;
                                    h = 50;
                                    break;
                                case "world8":
                                    w = 110;
                                    h = 110;
                                    break;
                                case "world11":
                                    w = 300;
                                    h = 300;
                                    break;
                                case "world12":
                                    w = 250;
                                    h = 150;
                                    break;
                                case "world13":
                                    w = 150;
                                    h = 150;
                                    break;
                            }
                            if (dbo.Contains("blocks"))
                            {
                                MapFrame = new Frame(w, h);
                                //uid2name(owner, name, w, h);
                                updateData(name, owner, w, h);
                            }
                        }
                        else
                        {
                            //uid2name(owner, name, 200, 200);
                            updateData(name, owner, 200, 200);
                            MapFrame = new Frame(200, 200);
                        }
                    }

                    if (dbo.Contains("blocks"))
                    {
                        MapFrame = Frame.FromMessage2(dbo);
                        if (MapFrame != null)
                        {
                            SizeWidth = MapFrame.Width;
                            SizeHeight = MapFrame.Height;
                            NeedsInit = false;
                            DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Couldn't read mapdata", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        notsaved = true;
                        DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                    Close();
                }*/
            }
            return frame;
        }
        public static Client SelectAccount()
        {

            var value = MainForm.accs[MainForm.selectedAcc].loginMethod;
            if (value == 0 && MainForm.accs.ContainsKey(MainForm.selectedAcc))
            {
                return PlayerIO.QuickConnect.SimpleConnect(bdata.gameID, MainForm.accs[MainForm.selectedAcc].login, MainForm.accs[MainForm.selectedAcc].password, null);
            }
            else
            {
                return null;
            }
        }

    }
}
