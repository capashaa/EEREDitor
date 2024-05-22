using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlayerIOClient;
using System.Threading;
using System.Xml.Linq;

namespace EEditor
{
    public partial class NewDialogForm : Form
    {
        public int SizeWidth { get; private set; }
        public int SizeHeight { get; private set; }
        public Frame MapFrame { get; set; }
        public bool NeedsInit { get; private set; }
        public bool RealTime { get; }
        public bool notsaved { get; set; }

        private int incr { get; set; }

        public bool usebg { get; set; }
        public Connection Connection { get; set; }
        public MainForm MainForm { get; set; }
        public MainForm mainform { get; set; }
        private Client client_;
        private string worldOwner = "Anonymous";
        private string owner = null;
        private int inputData = 0;
        private Dictionary<string, string> data = new Dictionary<string, string>();
        private Semaphore s = new Semaphore(0, 1);
        private Semaphore s1 = new Semaphore(0, 1);
        private string roomID { get; set; }
        //private bool errors = false;
        public NewDialogForm(MainForm mainform)
        {
            InitializeComponent();
            MainForm = mainform;
            levelTextBox.Text = MainForm.userdata.level;
            //levelPassTextBox.Text = EEditor.Properties.Settings.Default.LevelPass;
            CheckForIllegalCrossThreadCalls = false;
            listBox1.SelectedIndex = 0;
            notsaved = false;
        }

        //Enable-disable level combobox accordingly
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0 || listBox1.SelectedIndex == 15 || listBox1.SelectedIndex == 16)
            {
                groupBox3.Visible = true;
                groupBox2.Visible = false;
            }
            if (listBox1.SelectedIndex == 17)
            {
                groupBox2.Visible = false;
            }
            if (listBox1.SelectedIndex >= 1 && listBox1.SelectedIndex <= 14)
            {
                groupBox3.Visible = false;
                groupBox2.Visible = false;
            }
        }

        private void levelTextBox_Enter(object sender, EventArgs e)
        {
            //rLoadLevel.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MainForm.SetPenTool();
            if (Clipboard.ContainsData("EEBrush")) Clipboard.Clear();
            if (!usebg)
            {
                MainForm.userdata.thisColor = Color.Transparent;
                MainForm.userdata.useColor = false;
            }
            ToolPen.undolist.Clear();
            ToolPen.redolist.Clear();
            ToolPen.rotation.Clear();
            //Clipboard.Clear();
            MainForm.tsc.Items.Clear();
            MainForm.tsc.Items.Add("Background");
            MainForm.tsc.Text = "Background";
            NeedsInit = true;
            MainForm.Text = "EERditor " + bdata.programVersion;
            MainForm.OpenWorld = false;
            MainForm.OpenWorldCode = false;
            #region Listbox selection
            if (listBox1.SelectedIndex == 0)
            {
                if (!string.IsNullOrEmpty(levelTextBox.Text))
                {
                    var level = levelTextBox.Text.Trim();
                    if (level.Contains('/')) level = level.Substring(level.LastIndexOf('/') + 1).Trim();
                    MainForm.userdata.level = level;
                    LoadFromLevel(level, 2);
                }
                return;
            }
            else if (listBox1.SelectedIndex == 1)
            {
                SizeWidth = 25;
                SizeHeight = 25;
            }
            else if (listBox1.SelectedIndex == 2)
            {
                SizeWidth = 40;
                SizeHeight = 30;
            }
            else if (listBox1.SelectedIndex == 3)
            {
                SizeWidth = 50;
                SizeHeight = 50;
            }
            else if (listBox1.SelectedIndex == 4)
            {
                SizeWidth = 100;
                SizeHeight = 100;
            }
            else if (listBox1.SelectedIndex == 5)
            {
                SizeWidth = 100;
                SizeHeight = 400;
            }
            else if (listBox1.SelectedIndex == 6)
            {
                SizeWidth = 150;
                SizeHeight = 150;
            }
            else if (listBox1.SelectedIndex == 7)
            {
                SizeWidth = 110;
                SizeHeight = 110;
            }
            else if (listBox1.SelectedIndex == 8)
            {
                SizeWidth = 200;
                SizeHeight = 200;
            }
            else if (listBox1.SelectedIndex == 9)
            {
                SizeWidth = 200;
                SizeHeight = 400;
            }
            else if (listBox1.SelectedIndex == 10)
            {
                SizeWidth = 300;
                SizeHeight = 300;
            }
            else if (listBox1.SelectedIndex == 11)
            {
                SizeWidth = 400;
                SizeHeight = 50;
            }
            else if (listBox1.SelectedIndex == 12)
            {
                SizeWidth = 400;
                SizeHeight = 200;
            }
            else if (listBox1.SelectedIndex == 13)
            {
                SizeWidth = 636;
                SizeHeight = 50;
            }
            else if (listBox1.SelectedIndex == 14)
            {
                SizeWidth = 200;
                SizeHeight = 200;
                MainForm.OpenWorld = true;
                MainForm.OpenWorldCode = false;
                // restrictions here
            }
            else if (listBox1.SelectedIndex == 15)
            {
                SizeWidth = 200;
                SizeHeight = 200;
                MainForm.OpenWorld = true;
                MainForm.OpenWorldCode = true;
                // restrictions here
            }
            else if (listBox1.SelectedIndex == 16)
            {
                if (!string.IsNullOrEmpty(levelTextBox.Text))
                {
                    var level = levelTextBox.Text.Trim();
                    if (level.Contains('/')) level = level.Substring(level.LastIndexOf('/') + 1).Trim();
                    MainForm.userdata.level = level;
                    LoadFromLevel(level, 0);
                }
                return;
            }
            else if (listBox1.SelectedIndex == 17)
            {
                if (!string.IsNullOrEmpty(levelTextBox.Text))
                {
                    var level = levelTextBox.Text.Trim();
                    if (level.Contains('/')) level = level.Substring(level.LastIndexOf('/') + 1).Trim();
                    MainForm.userdata.level = level;

                    LoadFromLevel(level, 1);
                }
                return;
            }
            else if (listBox1.SelectedIndex == 18)
            {
                /*if (!string.IsNullOrEmpty(levelTextBox.Text))
                {
                    var level = levelTextBox.Text.Trim();
                    if (level.Contains('/')) level = level.Substring(level.LastIndexOf('/') + 1).Trim();
                    Properties.Settings.Default.Level = level;
                    Properties.Settings.Default.loadx = (int)numericUpDown1.Value;
                    Properties.Settings.Default.loady = (int)numericUpDown2.Value;
                    Properties.Settings.Default.loadw = (int)numericUpDown3.Value;
                    Properties.Settings.Default.loadh = (int)numericUpDown4.Value;
                    Properties.Settings.Default.Save();

                    LoadFromLevel(level, 3);
                }*/
                return;
            }
            #endregion
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pbColor.Width, pbColor.Height);
            ColorDialog dg = new ColorDialog();
            if (dg.ShowDialog() == DialogResult.OK)
            {
                MainForm.userdata.useColor = true;
                MainForm.userdata.thisColor = dg.Color;

                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.Clear(dg.Color);
                    gr.DrawRectangle(new Pen(GetContrastColor(dg.Color)), 0, 0, bmp.Width - 1, bmp.Height - 1);
                }
                usebg = true;
            }
            else
            {
                MainForm.userdata.useColor = false;
                MainForm.userdata.thisColor = Color.Transparent;
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.Clear(Color.Transparent);
                    gr.DrawRectangle(new Pen(GetContrastColor(MainForm.themecolors.accent)), new Rectangle(0, 0, bmp.Width - 1, bmp.Height - 1));
                }
            }
            pbColor.Image = bmp;
            //Properties.Settings.Default.usecolor = false;
            //Properties.Settings.Default.Save();
        }
        private void updateData(string title, string owner, int width, int height)
        {
            MainForm.Text = $"({title}) [{owner}] ({width}x{height}) - EERditor {bdata.programVersion}";
        }
        public void LoadFromLevel(string level, int datas)
        {
            //errors = false;
            //EEditor.Properties.Settings.Default.LevelPass = levelPassTextBox.Text;
            if (MainForm.accs.ContainsKey(MainForm.selectedAcc))
            {
                Client client = PlayerIO.QuickConnect.SimpleConnect(bdata.gameID, MainForm.accs[MainForm.selectedAcc].login, MainForm.accs[MainForm.selectedAcc].password, null);
                {
                    if (client != null)
                    {
                        client_ = client;
                        if (datas == 0)
                        {
                            if (MainForm.userdata.level.StartsWith("OW"))
                            {
                                int version = bdata.forceversion ? bdata.version : Convert.ToInt32(client.BigDB.Load("config", "config")["version"]);
                                client.Multiplayer.ListRooms($"{bdata.normal_room}{version}", null, 0, 0,
                                (RoomInfo[] rinfo) =>
                                {
                                    foreach (var val in rinfo)
                                    {
                                        if (val.Id.StartsWith("OW"))
                                        {
                                            if (val.Id == MainForm.userdata.level)
                                            {
                                                MainForm.userdata.level = val.Id;
                                                Connection = client.Multiplayer.CreateJoinRoom(MainForm.userdata.level, $"{bdata.normal_room}{version}", true, null, null);
                                                Connection.OnMessage += OnMessage;
                                                Connection.Send("init");
                                                NeedsInit = false;
                                                break;
                                            }
                                        }
                                    }
                                },
                                (PlayerIOError error) => Console.WriteLine(error.Message));
                                s.WaitOne();
                            }
                            else
                            {
                                if (client != null)
                                {
                                    int version = bdata.forceversion ? bdata.version : Convert.ToInt32(client.BigDB.Load("config", "config")["version"]);
                                    Connection = client.Multiplayer.CreateJoinRoom(level, $"{bdata.normal_room}{version}", true, null, null);
                                    Connection.OnMessage += OnMessage;
                                    Connection.Send("init");
                                    NeedsInit = false;
                                    s.WaitOne();
                                }
                                else
                                {
                                    MessageBox.Show("Client is null");
                                }
                            }
                        }
                        else if (datas == 1)
                        {
                            int w = 0;
                            int h = 0;
                            DatabaseObject dbo = client.BigDB.Load("Worlds", level);
                            {
                                if (dbo != null)
                                {
                                    var name = dbo.Contains("name") ? dbo["name"].ToString() : "Untitled World";
                                    owner = dbo.Contains("owner") ? dbo["owner"].ToString() : "Unknown user";

                                    if (dbo.Contains("backgroundColor") && Convert.ToInt32(dbo["backgroundColor"]) != 0)
                                    {
                                        EEditor.MainForm.userdata.useColor = true;
                                        EEditor.MainForm.userdata.thisColor = UIntToColor(Convert.ToUInt32(dbo["backgroundColor"]));
                                    }
                                    if (dbo.Contains("width") && dbo.Contains("height") && dbo.Contains("worlddata"))
                                    {
                                        //uid2name(owner, name, Convert.ToInt32(dbo["width"]), Convert.ToInt32(dbo["height"]));
                                        //updateData(name, owner, Convert.ToInt32(dbo["width"]), Convert.ToInt32(dbo["height"]));
                                        MapFrame = new Frame(Convert.ToInt32(dbo["width"]), Convert.ToInt32(dbo["height"]));
                                    }
                                    else
                                    {
                                        if (dbo.Contains("type"))
                                        {
                                            switch (dbo["type"])
                                            {
                                                case "world0":
                                                    w = 25;
                                                    h = 25;
                                                    break;
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
                                            if (dbo.Contains("worlddata"))
                                            {
                                                MapFrame = new Frame(w, h);
                                                //uid2name(owner, name, w, h);
                                                //updateData(name, owner, w,h);
                                            }
                                        }
                                        else
                                        {
                                            //uid2name(owner, name, 200, 200);
                                            //updateData(name, owner, 200,200);
                                            MapFrame = new Frame(200, 200);
                                        }
                                    }
                                    if (dbo.Contains("worlddata"))
                                    {
                                        MapFrame = Frame.FromMessage2(dbo);
                                        if (MapFrame != null)
                                        {
                                            SizeWidth = MapFrame.Width;
                                            SizeHeight = MapFrame.Height;
                                            NeedsInit = false;
                                            DialogResult = DialogResult.OK;
                                            Close();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Couldn't read mapdata", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }

                            }

                        }
                        else if (datas == 2)
                        {
                            int w = 0;
                            int h = 0;
                            DatabaseObject dbo = client.BigDB.Load("Worlds", MainForm.userdata.level);
                            {
                                if (dbo != null)
                                {
                                    var name = dbo.Contains("name") ? dbo["name"].ToString() : "Untitled World";
                                    owner = dbo.Contains("owner") ? dbo["owner"].ToString() : null;
                                    if (dbo.Contains("width") && dbo.Contains("height") && dbo.Contains("worlddata"))
                                    {
                                        //uid2name(owner, name, Convert.ToInt32(dbo["width"]), Convert.ToInt32(dbo["height"]));
                                        MapFrame = new Frame(Convert.ToInt32(dbo["width"]), Convert.ToInt32(dbo["height"]));
                                    }
                                    else
                                    {
                                        if (dbo.Contains("type"))
                                        {
                                            switch ((int)dbo["type"])
                                            {
                                                case 0:
                                                    w = 25;
                                                    h = 25;
                                                    break;
                                                case 1:
                                                    w = 50;
                                                    h = 50;
                                                    break;
                                                case 2:
                                                    w = 100;
                                                    h = 100;
                                                    break;
                                                default:
                                                case 3:
                                                    w = 200;
                                                    h = 200;
                                                    break;
                                                case 4:
                                                    w = 400;
                                                    h = 50;
                                                    break;
                                                case 5:
                                                    w = 400;
                                                    h = 200;
                                                    break;
                                                case 6:
                                                    w = 100;
                                                    h = 400;
                                                    break;
                                                case 7:
                                                    w = 636;
                                                    h = 50;
                                                    break;
                                                case 8:
                                                    w = 110;
                                                    h = 110;
                                                    break;
                                                case 11:
                                                    w = 300;
                                                    h = 300;
                                                    break;
                                                case 12:
                                                    w = 250;
                                                    h = 150;
                                                    break;
                                                case 13:
                                                    w = 150;
                                                    h = 150;
                                                    break;
                                            }
                                            //uid2name(owner, name, w, h);
                                            MapFrame = new Frame(w, h);

                                        }
                                        else
                                        {
                                            //uid2name(owner, name, 200, 200);
                                            MapFrame = new Frame(200, 200);
                                        }
                                    }
                                    MapFrame.Reset(false);
                                    SizeWidth = MapFrame.Width;
                                    SizeHeight = MapFrame.Height;
                                    NeedsInit = false;
                                    DialogResult = System.Windows.Forms.DialogResult.OK;
                                    Close();
                                }



                                else
                                {
                                    MessageBox.Show("Client is null");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("no Account");
            }
        }


        private void uid2name(string uid, string title, int width, int height)
        {
            if (uid != null)
            {
                if (client_ != null)
                {
                    client_.BigDB.LoadRange("usernames", "ByOwner", new object[] { uid }, null, null, 1, (DatabaseObject[] dbo) =>
                    {
                        if (dbo != null)
                        {
                            if (dbo.Length == 1)
                            {
                                worldOwner = dbo[0].Key.ToString();
                                MainForm.Text = $"({title}) [{worldOwner}] ({width}x{height}) - EERditor {bdata.programVersion}";
                            }
                            else
                            {
                                MainForm.Text = $"({title}) [Unknown Owner] ({width}x{height}) - EERditor {bdata.programVersion}";
                            }
                        }
                        else
                        {
                            MainForm.Text = $"({title}) [Unknown Owner] ({width}x{height}) - EERditor {bdata.programVersion}";
                        }
                    }, (PlayerIOError error) =>
                    {
                        MainForm.Text = $"({title}) [Unknown Owner] ({width}x{height}) - EERditor {bdata.programVersion}";
                        Console.WriteLine($"Error: {error}");
                    });
                }
                else
                {
                    MainForm.Text = $"({title}) [Unknown Owner] ({width}x{height}) - EERditor {bdata.programVersion}";
                }
            }
            else
            {
                MainForm.Text = $"({title}) [Unknown Owner] ({width}x{height}) - EERditor {bdata.programVersion}";
            }
        }

        private Color UIntToColor(uint color)
        {
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 0);
            return Color.FromArgb(a, r, g, b);
        }
        private Color GetContrastColor(Color color)
        {
            return (color.R * 0.299M) + (color.G * 0.587M) + (color.B * 0.114M) > 149 ?
                Color.Black :
                Color.White;
        }
        public void OnMessage(object sender, PlayerIOClient.Message e)
        {

            if (e.Type == "init")
            {

                MapFrame = Frame.FromMessage(e);
                if (MapFrame != null)
                {
                    if (e.GetUInt(21) == 0) EEditor.MainForm.userdata.thisColor = Color.Transparent;
                    else
                    {
                        EEditor.MainForm.userdata.useColor = true;
                        EEditor.MainForm.userdata.thisColor = UIntToColor(e.GetUInt(21));
                    }

                    var owner = e.GetString(0)?.Length == 0 ? "Unknown" : e.GetString(0);
                    MainForm.Text = $"({e[1]}) [{owner}] ({e[18]}x{e[19]}) - EERditor {bdata.programVersion}";
                    SizeWidth = MapFrame.Width;
                    SizeHeight = MapFrame.Height;
                    
                    Connection.OnMessage -= OnMessage;
                    Connection.Disconnect();
                    s.Release();
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();

                }
                else
                {
                    MessageBox.Show("Couldn't read mapdata", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    s.Release();
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    Close();
                }
            }
            else if (e.Type == "upgrade")
            {
                MessageBox.Show("Game got updated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                s.Release();
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                Close();
            }
            else
            {
                //Console.WriteLine(e.ToString());
                if (e.Type == "info")
                {
                    MessageBox.Show(e.GetString(1), e.GetString(0), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    s.Release();
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    Close();
                }



                //if (e.Type != "b" && e.Type != "m" && e.Type != "hide" && e.Type != "show")Console.WriteLine(e.ToString());
            }
        }

        private void NewDialogForm_Load(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(16);
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            listBox1.BackColor = MainForm.themecolors.accent;
            listBox1.ForeColor = MainForm.themecolors.foreground;
            Bitmap bmp = new Bitmap(pbColor.Width, pbColor.Height);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.Clear(Color.Transparent);
                gr.DrawRectangle(new Pen(GetContrastColor(MainForm.themecolors.accent)), new Rectangle(0, 0, bmp.Width - 1, bmp.Height - 1));
            }
            pbColor.Image = bmp;
            foreach (Control cntr in this.Controls)
            {
                if (cntr.GetType() == typeof(Button))
                {
                    ((Button)cntr).ForeColor = MainForm.themecolors.foreground;
                    ((Button)cntr).BackColor = MainForm.themecolors.accent;
                    ((Button)cntr).FlatStyle = FlatStyle.Flat;
                }
                if (cntr.GetType() == typeof(GroupBox))
                {
                    cntr.ForeColor = MainForm.themecolors.foreground;
                    cntr.BackColor = MainForm.themecolors.background;
                    foreach (Control cntrl in cntr.Controls)
                    {
                        if (cntrl.GetType() == typeof(Button))
                        {
                            ((Button)cntrl).ForeColor = MainForm.themecolors.foreground;
                            ((Button)cntrl).BackColor = MainForm.themecolors.accent;
                            ((Button)cntrl).FlatStyle = FlatStyle.Flat;
                        }
                        if (cntrl.GetType() == typeof(TextBox))
                        {
                            cntrl.ForeColor = MainForm.themecolors.foreground;
                            cntrl.BackColor = MainForm.themecolors.accent;
                        }
                    }
                }
            }
        }

        private void NewDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.userdata.level = levelTextBox.Text;
        }
    }
}
