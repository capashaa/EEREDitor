using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EEditor
{
    public partial class NPC : Form
    {
        public TextBox message1 { get { return Message1TextBox; } set { Message1TextBox = value; } }
        public TextBox message2 { get { return Message2TextBox; } set { Message2TextBox = value; } }
        public TextBox message3 { get { return Message3TextBox; } set { Message3TextBox = value; } }
        public TextBox nickname { get { return NicknameTextBox; } set { NicknameTextBox = value; } }
        public int blockID { get; set; }
        public NPC()
        {
            InitializeComponent();
        }

        private void NPC_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void NPC_Load(object sender, EventArgs e)
        {
            //listView1.View = View.SmallIcon;
            ImageList list = new ImageList();
            list.ImageSize = new Size(16, 16);
            listView1.Click += ListView1_Click;
            listView1.MultiSelect = false;

            var payvault = MainForm.accs[MainForm.userdata.username].payvault;
            if (payvault.ContainsKey("npcsmile") || MainForm.debug) { addNPC("smile", 1550, list); }
            if (payvault.ContainsKey("npcsad") || MainForm.debug) { addNPC("sad", 1551, list); }
            if (payvault.ContainsKey("npcold") || MainForm.debug) { addNPC("old", 1552, list); }
            if (payvault.ContainsKey("npcangry") || MainForm.debug) { addNPC("angry", 1553, list); }
            if (payvault.ContainsKey("npcslime") || MainForm.debug) { addNPC("slime", 1554, list); }
            if (payvault.ContainsKey("npcrobot") || MainForm.debug) { addNPC("robot", 1555, list); }
            if (payvault.ContainsKey("npcknight") || MainForm.debug) { addNPC("knight", 1556, list); }
            if (payvault.ContainsKey("npcmeh") || MainForm.debug) { addNPC("meh", 1557, list); }
            if (payvault.ContainsKey("npccow") || MainForm.debug) { addNPC("cow", 1558, list); }
            if (payvault.ContainsKey("npcfrog") || MainForm.debug) { addNPC("frog", 1559, list); }
            if (payvault.ContainsKey("npcbruce") || MainForm.debug) { addNPC("bruce", 1570, list); }
            if (payvault.ContainsKey("npcstarfish") || MainForm.debug) { addNPC("starfish", 1569, list); }
            if (payvault.ContainsKey("npcdt") || MainForm.debug) { addNPC("computer", 1571, list); }
            if (payvault.ContainsKey("npcskeleton") || MainForm.debug) { addNPC("skeleton", 1572, list); }
            if (payvault.ContainsKey("npczombie") || MainForm.debug) { addNPC("zombie", 1573, list); }
            if (payvault.ContainsKey("npcghost") || MainForm.debug) { addNPC("ghost", 1574, list); }
            if (payvault.ContainsKey("npcastronaut") || MainForm.debug) { addNPC("astronaut", 1575, list); }
            if (payvault.ContainsKey("npcsanta") || MainForm.debug) { addNPC("santa", 1576, list); }
            if (payvault.ContainsKey("npcsnowman") || MainForm.debug) { addNPC("snowman", 1577, list); }
            if (payvault.ContainsKey("npcwalrus") || MainForm.debug) { addNPC("walrus", 1578, list); }
            if (payvault.ContainsKey("npccrab") || MainForm.debug) { addNPC("crab", 1579, list); }

            //NicknameTextBox.Text = MainForm.userdata.username;
            listView1.ForeColor = MainForm.themecolors.foreground;
            listView1.BackColor = MainForm.themecolors.accent;
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            NicknameTextBox.ForeColor = MainForm.themecolors.foreground;
            NicknameTextBox.BackColor = MainForm.themecolors.accent;
            Message1TextBox.ForeColor = MainForm.themecolors.foreground;
            Message1TextBox.BackColor = MainForm.themecolors.accent;
            Message2TextBox.ForeColor = MainForm.themecolors.foreground;
            Message2TextBox.BackColor = MainForm.themecolors.accent;
            Message3TextBox.ForeColor = MainForm.themecolors.foreground;
            Message3TextBox.BackColor = MainForm.themecolors.accent;


        }

        private void ListView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count != 0)
            {
                if (MainForm.userdata.username != "guest" || MainForm.ihavethese.Any(x => x.Key.StartsWith("npc")))
                {
                    blockID = Convert.ToInt32(listView1.Items[listView1.SelectedIndices[0]].Name);
                }
                else { blockID = 0; }
            }
        }

        private void addNPC(string name, int id, ImageList list)
        {

            Bitmap image = MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[id] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
            list.Images.Add(name, image);
            listView1.SmallImageList = list;
            ListViewItem lvi = new ListViewItem(name);

            if (!MainForm.debug && MainForm.userdata.username != "guest" && MainForm.ihavethese.Any(x => x.Key.StartsWith("npc")))
            {

                lvi.SubItems.Add(MainForm.accs[MainForm.userdata.username].payvault[name == "computer" ? "npcdt" : $"npc{name}"].ToString());

            }
            else
            {
                lvi.SubItems.Add("0");
            }
            lvi.ImageKey = name;
            lvi.Name = id.ToString();
            listView1.Items.Add(lvi);

        }

        private void MessageTextBox_TextChanged(object sender, EventArgs e)
        {
            switch (((TextBox)sender).Name.ToString())
            {
                case "NicknameTextBox":
                    NicknameTextBox.Text = string.Concat(NicknameTextBox.Text.Where(char.IsLetterOrDigit));
                    NicknameTextBox.SelectionStart = NicknameTextBox.Text.Length + 1;
                    break;
            }
        }
    }
}
