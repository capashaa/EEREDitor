using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace EEditor
{

    public partial class SettingsForm : Form
    {
        public static bool reset { get; set; }
        public static System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        private bool formload = false;
        private int[] lastColors = new int[7];
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            formload = true;

            #region Setting checkboxes
            ToolTip tp = new ToolTip();
            tp.SetToolTip(usePenToolCheckBox, "Enables Pen tool after switching blocks.");
            tp.SetToolTip(selectAllBorderCheckBox, "Includes bordering blocks when selecting the whole world by hotkey Ctrl+A.");
            tp.SetToolTip(confirmCloseCheckBox, "Prompts when you attempt to close EEditor.");
            tp.SetToolTip(FasterShapeStyleCheckBox, "Showing red lines instead of blocks.");
            tp.SetToolTip(DarkThemeCheckBox, "Choose between light and dark theme.");
            tp.SetToolTip(cBHotkeyBar, "Show or hide HotkeyBar.");
            tp.SetToolTip(rbIgnoreEmpty, "Ignore empty blocks when using selection tool");
            tp.SetToolTip(rbAcceptEmpty, "Use empty blocks when using selection tool");
            usePenToolCheckBox.Checked = MainForm.userdata.usePenTool;
            selectAllBorderCheckBox.Checked = MainForm.userdata.selectAllBorder;
            confirmCloseCheckBox.Checked = MainForm.userdata.confirmClose;
            FasterShapeStyleCheckBox.Checked = MainForm.userdata.fastshape;
            DarkThemeCheckBox.Checked = MainForm.userdata.darkTheme;
            cBHotkeyBar.Checked = MainForm.userdata.HotkeyBar;
            if (rbIgnoreEmpty.Checked) MainForm.userdata.oldmark = true;
            else if (rbAcceptEmpty.Checked) MainForm.userdata.oldmark = false;
            #endregion

            clearComboBox.SelectedIndex = 0; //Show "Clear settings..." by default
            formload = false;
            timer1 = new Timer();
            timer1.Tick += (ss, ee) => { updateMessage(); };
            timer1.Interval = 8000;
            timer1.Start();

            this.BackColor = MainForm.themecolors.background;
            this.ForeColor = MainForm.themecolors.foreground;
            statusStrip1.BackColor = MainForm.themecolors.accent;
            clearComboBox.BackColor = MainForm.themecolors.background;
            clearComboBox.ForeColor = MainForm.themecolors.foreground;
            clearButton.ForeColor = MainForm.themecolors.foreground;
            clearButton.BackColor = MainForm.themecolors.accent;
            clearButton.FlatStyle = FlatStyle.Flat;
            gbSelection.ForeColor = MainForm.themecolors.foreground;
            var items = ((StatusStrip)statusStrip1).Items;
            for (int o = 0; o < items.Count; o++)
            {
                if (items[o].GetType() == typeof(ToolStripStatusLabel))
                {
                    items[o].BackColor = MainForm.themecolors.accent;
                    items[o].ForeColor = MainForm.themecolors.foreground;
                }
            }
        }
        private void updateMessage()
        {
            StatusToolStripStatusLabel.ForeColor = MainForm.userdata.darkTheme ? Color.LightBlue : Color.DarkBlue;
            StatusColorToolStripStatusLabel.BackColor = MainForm.themecolors.accent;
            StatusColorToolStripStatusLabel.ForeColor = MainForm.themecolors.foreground;
        }
        #region Setting checkboxes
        private void FasterShapeStyleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.fastshape = FasterShapeStyleCheckBox.Checked;
        }
        private void usePenToolCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.usePenTool = usePenToolCheckBox.Checked;
        }

        private void selectAllBorderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.selectAllBorder = selectAllBorderCheckBox.Checked;
        }

        private void confirmCloseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.confirmClose = confirmCloseCheckBox.Checked;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!formload)
            {
                MainForm.editArea.MainForm.rebuildGUI(false);
            }
        }
        #endregion

        #region Clear settings
        //Combobox - enable or disable clear button according to value
        private void clearComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clearComboBox.SelectedIndex == 0 || clearComboBox.SelectedIndex == 1 || clearComboBox.SelectedIndex == 4)
            {
                clearButton.Enabled = false;
                clearComboBox.SelectedIndex = 0;
            }
            else
            {
                clearButton.Enabled = true;
            }
        }
        //Button - remove selected settings, prompt on important ones
        private void clearButton_Click(object sender, EventArgs e)
        {
            switch (clearComboBox.SelectedIndex)
            {
                case 0:
                case 1:
                case 4:
                    break;
                case 2: //Block hotkeys
                    MainForm.resethotkeys = true;
                    StatusToolStripStatusLabel.Text = "Block hotkeys have been cleared.";
                    StatusToolStripStatusLabel.ForeColor = Color.DarkGreen;
                    //MessageBox.Show("Block hotkeys have been cleared.", "Hotkeys cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Threading.Thread.Sleep(1000);
                    this.Close();
                    break;
                case 3: //Blocks in unknown tab
                    MainForm.userdata.newestBlocks.Clear();
                    StatusToolStripStatusLabel.Text = "Unknown blocks have been cleared.";
                    StatusToolStripStatusLabel.ForeColor = Color.DarkGreen;
                    break;
                case 5: //Old settings
                    var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "Local");
                    if (Directory.Exists(path + "\\EEditor"))
                    {
                        string[] dir = Directory.GetDirectories(path + "\\EEditor");
                        if (dir.Length > 0)
                        {
                            DialogResult result = MessageBox.Show("Found " + dir.Length + " folders that contain old settings and login information.\nWould you like to remove them?", "Clear old settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                for (int i = 0; i < dir.Length; i++)
                                {
                                    FileSystem.DeleteDirectory(dir[i], UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                                }
                                StatusToolStripStatusLabel.Text = "Old settings have been removed.";
                                StatusToolStripStatusLabel.ForeColor = Color.DarkGreen;
                            }
                        }
                        else
                        {
                            StatusToolStripStatusLabel.Text = "Couldn't find any EEditor logs.";
                            StatusToolStripStatusLabel.ForeColor = Color.DarkRed;

                        }
                    }
                    else
                    {
                        StatusToolStripStatusLabel.Text = "Couldn't find any EEditor logs.";
                        StatusToolStripStatusLabel.ForeColor = Color.DarkRed;
                    }
                    break;
                case 6: //current settings
                    MainForm.userdata = new userData()
                    {
                        username = "guest",
                        newestBlocks = new List<JToken>(),
                        uploadDelay = 5,
                        brickHotkeys = "",
                        sprayr = 5,
                        sprayp = 10,
                        confirmClose = true,
                        uploadOption = 0,
                        themeBorder = false,
                        themeClean = false,
                        imageBackgrounds = true,
                        imageBlocks = true,
                        imageSpecialblocksMorph = false,
                        imageSpecialblocksAction = false,
                        random = false,
                        reverse = false,
                        ColorFG = true,
                        ColorBG = true,
                        ignoreplacing = false,
                        randomLines = false,
                        BPSblocks = 100,
                        BPSplacing = false,
                        IgnoreBlocks = new List<JToken>(),
                        fastshape = true,
                        replaceit = false,
                        oldmark = true,
                        darkTheme = false,
                        HotkeyBar = false,

                    };
                    MainForm.OpenWorld = false;
                    MainForm.userdata.useColor = false;
                    MainForm.userdata.thisColor = Color.Transparent;
                    MainForm.editArea.MainForm.updateTheme();
                    Clipboard.Clear();
                    ToolPen.rotation.Clear();
                    ToolPen.id.Clear();
                    ToolPen.redolist.Clear();
                    ToolPen.undolist.Clear();
                    ToolPen.text.Clear();
                    ToolPen.target.Clear();
                    MainForm.resethotkeys = true;
                    MainForm.userdata.HotkeyBar = false;
                    MainForm.resetLastBlockz = true;
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\settings.json", JsonConvert.SerializeObject(MainForm.userdata, Newtonsoft.Json.Formatting.Indented));
                    reset = true;
                    this.Close();
                    break;
            }
            clearComboBox.SelectedIndex = 0;
        }
        #endregion
        /*for (int i = 0; i < 4; i++)
        {
            Bitmap bmp = new Bitmap(16, 16);
            Graphics gr = Graphics.FromImage(bmp);
            gr.Clear(MainForm.userdata.themeBlock);
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, 15, 15));
        }*/

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            this.DialogResult = DialogResult.OK;
        }

        private void DarkThemeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!formload)
            {
                MainForm.userdata.darkTheme = DarkThemeCheckBox.Checked;
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\settings.json", JsonConvert.SerializeObject(MainForm.userdata, Newtonsoft.Json.Formatting.Indented));
                MainForm.editArea.MainForm.updateTheme();
                reset = true;
                
                this.Close();
            }


        }

        private void cBHotkeyBar_CheckedChanged(object sender, EventArgs e)
        {
            if (!formload)
            {
                MainForm.userdata.HotkeyBar = cBHotkeyBar.Checked;
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\settings.json", JsonConvert.SerializeObject(MainForm.userdata, Newtonsoft.Json.Formatting.Indented));
                reset = true;
                this.Close();
            }
        }

        private void rbIgnoreEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIgnoreEmpty.Checked) { MainForm.userdata.oldmark = true; }
        }

        private void rbAcceptEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAcceptEmpty.Checked) { MainForm.userdata.oldmark = false; }
        }
    }
}
