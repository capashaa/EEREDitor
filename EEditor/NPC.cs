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
            if (payvault.ContainsKey("npcsmile") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("smile", 1592, list); }
            if (payvault.ContainsKey("npcsad") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("sad", 1593, list); }
            if (payvault.ContainsKey("npcold") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("old", 1594, list); }
            if (payvault.ContainsKey("npcangry") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("angry", 1595, list); }
            if (payvault.ContainsKey("npcslime") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("slime", 1596, list); }
            if (payvault.ContainsKey("npcrobot") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("robot", 1597, list); }
            if (payvault.ContainsKey("npcknight") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("knight", 1598, list); }
            if (payvault.ContainsKey("npcmeh") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin ) { addNPC("meh", 1599, list); }
            if (payvault.ContainsKey("npccow") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("cow", 1600, list); }
            if (payvault.ContainsKey("npcfrog") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("frog", 1601, list); }
            if (payvault.ContainsKey("npcbruce") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("bruce", 1602, list); }
            if (payvault.ContainsKey("npcstarfish") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("starfish", 1603, list); }
            if (payvault.ContainsKey("npcdt") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("computer", 1604, list); }
            if (payvault.ContainsKey("npcskeleton") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("skeleton", 1605, list); }
            if (payvault.ContainsKey("npczombie") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("zombie", 1606, list); }
            if (payvault.ContainsKey("npcghost") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("ghost", 1607, list); }
            if (payvault.ContainsKey("npcastronaut") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("astronaut", 1608, list); }
            if (payvault.ContainsKey("npcsanta") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("santa", 1609, list); }
            if (payvault.ContainsKey("npcsnowman") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("snowman", 1610, list); }
            if (payvault.ContainsKey("npcwalrus") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("walrus", 1651, list); }
            if (payvault.ContainsKey("npccrab") || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin) { addNPC("crab", 1652, list); }

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
                if (MainForm.userdata.username != "guest" || MainForm.ihavethese.Any(x => x.Key.StartsWith("npc")) || MainForm.debug || MainForm.accs[MainForm.userdata.username].admin)
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

                lvi.SubItems.Add(MainForm.accs[MainForm.userdata.username].payvault[name].ToString());

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
