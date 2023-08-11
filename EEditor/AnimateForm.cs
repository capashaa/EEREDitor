using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using PlayerIOClient;

namespace EEditor
{
    public partial class AnimateForm : Form
    {
        List<Frame> frames;
        Thread thread;
        Client client;
        Connection conn;
        public Button buttonName {  get { return button1; } set { button1 = value; } }
        private Dictionary<string, string> data = new Dictionary<string, string>();
        public static bool crewEdit = false;
        public static bool crewWorld = false;
        public static bool editRights = false;
        public static bool saveRights = false;
        private Semaphore s1 = new Semaphore(0, 1);
        public AnimateForm(List<Frame> frames)
        {
            InitializeComponent();
            this.frames = frames;
            autoSaveCheckBox.Checked = MainForm.userdata.saveWorldCrew;
            shuffleCheckBox.Checked = MainForm.userdata.drawMixed;
            checkBoxRandom.Checked = MainForm.userdata.random;
            checkBoxReverse.Checked = MainForm.userdata.reverse;
        }

        public void UpdateStatus(object sender, StatusChangedArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate { UpdateStatus(sender, e); });
            }
            else
            {
                if (e.EpochStart == DateTime.MinValue)
                {
                    label1.Text = e.Text;
                    DateTime start = DateTime.Now;
                }
                if (e.EpochStart != DateTime.MinValue)
                {
                    DateTime date1 = System.DateTime.UtcNow;
                    DateTime date2 = e.EpochStart;
                    TimeSpan dateDiff = date1.Subtract(date2);
                    TimeRunningTextBox.Text = dateDiff.ToString(@"hh\:mm\:ss");
                    TimeSpan timeleft = TimeSpan.FromTicks(DateTime.UtcNow.Subtract(e.EpochStart).Ticks * (e.totalLines - (e.CountedLines + 1)) / (e.CountedLines + 1));
                    //TimeLeftTextBox.Text = timeleft.ToString(@"hh\:mm\:ss");//TimeRunningTextBox.Text = Convert.ToDateTime((double)epoch - dt.Ticks).ToString("HH:mm:ss");
                }
                if (e.Complete)
                {
                    thread?.Abort();
                    conn?.Disconnect();
                    button1.Text = "Start";
                    uploadProgressBar.Value = 0;
                }
            }
        }

        private DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start" && !string.IsNullOrEmpty(levelTextBox.Text))
            {
                MainForm.userdata.level = levelTextBox.Text;
                MainForm.userdata.levelPass = levelPassTextBox.Text;
                if (!levelTextBox.Text.StartsWith("OW") && !MainForm.userdata.level.StartsWith("OW") && MainForm.accs[MainForm.selectedAcc].login == "guest" && MainForm.accs[MainForm.selectedAcc].password == "guest")
                {
                    DialogResult dr = MessageBox.Show("You can't upload as a guest, please switch to another account.\nWould you like to go to the Account Manager to add one?", "Guests can't upload", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (dr == DialogResult.Yes)
                    {
                        Form frm = new Accounts();
                        frm.ShowDialog();
                    }
                }
                else
                {
                    if (MainForm.userdata.level.StartsWith("OW") || levelTextBox.Text.StartsWith("OW"))
                    {
                        MainForm.userdata.level = levelTextBox.Text;
                        MainForm.userdata.levelPass = levelPassTextBox.Text;
                        button1.Text = "Stop";
                        label1.Text = "Connecting to level...";
                    }
                    else
                    {
                        MainForm.userdata.level = levelTextBox.Text;
                        MainForm.userdata.levelPass = levelPassTextBox.Text;
                        button1.Text = "Stop";
                        label1.Text = "Connecting to level...";
                    }

                    try
                    {
                        if (MainForm.accs.ContainsKey(MainForm.selectedAcc))
                        {
                            if (MainForm.accs[MainForm.selectedAcc].loginMethod == 0)
                            {
                                client = PlayerIO.QuickConnect.SimpleConnect(bdata.gameID, MainForm.accs[MainForm.selectedAcc].login, MainForm.accs[MainForm.selectedAcc].password, null);
                            }
                            if (MainForm.userdata.level.StartsWith("OW"))
                            {
                                int version = bdata.forceversion ? bdata.version : Convert.ToInt32(client.BigDB.Load("config", "config")["version"]);
                                client.Multiplayer.ListRooms(bdata.normal_room + version, null, 0, 0,
                                (RoomInfo[] rinfo) => {
                                    foreach (var val in rinfo) {
                                        if (val.Id.StartsWith("OW")) {
                                            if (val.Id.Length == MainForm.userdata.level.Length) {
                                                MainForm.userdata.level = val.Id;
                                                levelTextBox.Text = val.Id;
                                                
                                                conn = client.Multiplayer.CreateJoinRoom(MainForm.userdata.level, MainForm.userdata.level.StartsWith("BW") ? "Beta" : bdata.normal_room + version, true, null, null);
                                                Animator anim = new Animator(frames, conn, levelPassTextBox.Text, shuffleCheckBox.Checked, checkBoxReverse.Checked, checkBoxRandom.Checked);
                                                conn.OnDisconnect += Conn_OnDisconnect;
                                                Animator.pb = uploadProgressBar; //Make Animator.cs work with this form's progressbar
                                                Animator.afHandle = this.Handle; //Make TaskbarProgress.cs work with this form's upload progress
                                                anim.StatusChanged += UpdateStatus;
                                                thread = new Thread(new ThreadStart(anim.Run));
                                                thread.Start();
                                                break;
                                            }
                                        }
                                    }
                                },
                                (PlayerIOError error) => Console.WriteLine(error.Message));
                            }
                            else
                            {
                                int version = bdata.forceversion ? bdata.version : Convert.ToInt32(client.BigDB.Load("config", "config")["version"]);
                                conn = client.Multiplayer.CreateJoinRoom(MainForm.userdata.level,bdata.normal_room + version, true, null, null);
                                Animator anim = new Animator(frames, conn, levelPassTextBox.Text, shuffleCheckBox.Checked, checkBoxReverse.Checked, checkBoxRandom.Checked);
                                conn.OnDisconnect += Conn_OnDisconnect;
                                Animator.pb = uploadProgressBar; //Make Animator.cs work with this form's progressbar
                                Animator.afHandle = this.Handle; //Make TaskbarProgress.cs work with this form's upload progress
                                anim.StatusChanged += UpdateStatus;
                                thread = new Thread(new ThreadStart(anim.Run));
                                thread.Start();
                            }
                        }
                    }
                    catch (PlayerIOError err)
                    {
                        label1.Text = "Error: " + err.Message;
                        MessageBox.Show(err.Message);
                        thread?.Abort();
                        conn?.Disconnect();
                        button1.Text = "Start";
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(levelTextBox.Text))
                {
                    if (MainForm.userdata.saveWorldCrew && saveRights)
                    {
                        conn.Send("save");
                    }

                    label1.Text = "Level upload stopped.";
                    button1.Text = "Start";
                    uploadProgressBar.Value = 0;
                    TimeRunningTextBox.Clear();
                    try
                    {
                        thread?.Abort();
                        conn?.Disconnect();
                    }
                    catch { }
                }
            }
        }

        private void Conn_OnDisconnect(object sender, string message)
        {
        }

        private void AnimateForm_Load(object sender, EventArgs e)
        {
            //this.Size = new Size(380, 184);

            DelayNumericUpDown.Value = MainForm.userdata.uploadDelay;
            levelTextBox.Text = MainForm.userdata.level;
            if (MainForm.userdata.uploadOption == 0) uploadOptionButton1.Checked = true;
            SaveEveryXBlocksNumericUpDown.Enabled = MainForm.userdata.saveWorldCrew;
            levelPassTextBox.Enabled = true;
            levelPassTextBox.Text = MainForm.userdata.levelPass;

            ToolTip tp = new ToolTip();
            tp.SetToolTip(levelTextBox, "Insert the world ID you want to upload to.");
            tp.SetToolTip(levelPassTextBox, "Insert the code of the world you are uploading to.");


            tp.SetToolTip(button1, "Starts/stops uploading the world to Everybody Edits.");

            tp.SetToolTip(shuffleCheckBox, "When activated, EEditor uploads blocks in random order.\nWhen none is selected the upload goes by top to bottom.");

            tp.SetToolTip(checkBoxReverse, "When activated, EEditor uploads blocks from bottom and up.\nWhen none is selected the upload goes by top to bottom.");

            tp.SetToolTip(checkBoxRandom, "When activated, EEditor uploads blocks to different places.\nWhen none is selected the upload goes by top to bottom.");

            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            foreach (Control value in this.Controls)
            {
                if (value.GetType() == typeof(GroupBox))
                {
                    if (value.Controls.Count > 0)
                    {
                        foreach (Control cntrl in value.Controls)
                        {
                            if (cntrl.Controls.Count > 0)
                            {
                                foreach (Control cntrl1 in cntrl.Controls)
                                {
                                    if (cntrl1.GetType() == typeof(NumericUpDown))
                                    {
                                        cntrl1.ForeColor = MainForm.themecolors.foreground;
                                        cntrl1.BackColor = MainForm.themecolors.accent;
                                    }
                                }
                                if (cntrl.GetType() == typeof(GroupBox))
                                {
                                    cntrl.ForeColor = MainForm.themecolors.foreground;
                                }
                                if (cntrl.GetType() == typeof(NumericUpDown))
                                {
                                    cntrl.ForeColor = MainForm.themecolors.foreground;
                                    cntrl.BackColor = MainForm.themecolors.accent;
                                }
                            }
                            else
                            {
                                if (cntrl.GetType() == typeof(TextBox))
                                {
                                    cntrl.ForeColor = MainForm.themecolors.foreground;
                                    cntrl.BackColor = MainForm.themecolors.accent;
                                }

                            }
                        }
                    }
                    value.ForeColor = MainForm.themecolors.foreground;
                }
                if (value.GetType() == typeof(TextBox))
                {
                    value.ForeColor = MainForm.themecolors.foreground;
                    value.BackColor = MainForm.themecolors.accent;
                }
                if (value.GetType() == typeof(Button))
                {
                    value.ForeColor = MainForm.themecolors.foreground;
                    value.BackColor = MainForm.themecolors.accent;
                    ((Button)value).FlatStyle = FlatStyle.Flat;
                }
            }
        }


        private void AnimateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.userdata.level = levelTextBox.Text;
            MainForm.userdata.levelPass = levelPassTextBox.Text;
            conn?.Disconnect();
            thread?.Abort();
        }

        private void levelTextBox_TextChanged(object sender, EventArgs e)
        {
            if (levelTextBox.Text.StartsWith("PW") || levelTextBox.Text.StartsWith("BW") || levelTextBox.Text.StartsWith("OW"))
            {
                levelPassTextBox.Enabled = true;
                MainForm.userdata.level = levelTextBox.Text;
            }
        }

        private void autoSaveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.saveWorldCrew = autoSaveCheckBox.Checked;
            SaveEveryXBlocksNumericUpDown.Enabled = autoSaveCheckBox.Checked;
        }

        private void uploadOptionButton1_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.uploadOption = 0;
        }

        private void uploadOptionButton2_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.uploadOption = 1;
        }

        private void shuffleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.drawMixed = shuffleCheckBox.Checked;
            if (shuffleCheckBox.Checked)
            {
                if (checkBoxRandom.Checked) checkBoxRandom.Checked = false;
                if (checkBoxReverse.Checked) checkBoxReverse.Checked = false;
            }
        }

        private void uploadOptionButton3_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.uploadOption = 2;
        }

        private void checkBoxReverse_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.reverse = checkBoxReverse.Checked;
            if (checkBoxReverse.Checked)
            {
                if (shuffleCheckBox.Checked) shuffleCheckBox.Checked = false;
                if (checkBoxRandom.Checked) checkBoxRandom.Checked = false;
            }
        }

        private void checkBoxRandom_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.random = checkBoxRandom.Checked;
            if (checkBoxRandom.Checked)
            {
                if (shuffleCheckBox.Checked) shuffleCheckBox.Checked = false;
                if (checkBoxReverse.Checked) checkBoxReverse.Checked = false;
            }
        }

        private void levelPassTextBox_TextChanged(object sender, EventArgs e)
        {
            if (levelTextBox.Text.StartsWith("PW") || levelTextBox.Text.StartsWith("BW") || levelTextBox.Text.StartsWith("OW"))
            {
                MainForm.userdata.levelPass = levelPassTextBox.Text;
            }
        }


        private void waitForEditcheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void DelayNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MainForm.userdata.uploadDelay = Convert.ToInt32(DelayNumericUpDown.Value);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SaveEveryXBlocksNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MainForm.userdata.SaveXBlocks = Convert.ToInt32(SaveEveryXBlocksNumericUpDown.Value);
        }
    }

    public class StatusChangedArgs : EventArgs
    {
        public string Text { get; }
        public bool Complete { get; }
        public DateTime EpochStart { get; }
        public int totalLines { get; }
        public int CountedLines { get; }

        public StatusChangedArgs(string text,DateTime epochstart, bool complete,int totallines,int countedlines)
        {
            Text = text;
            EpochStart = epochstart;
            totalLines = totallines;
            CountedLines = countedlines;
            Complete = complete;
        }
    }
}
