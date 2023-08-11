using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using SharpCompress.Archives;
using SharpCompress.Readers;
using System.Diagnostics;
namespace EEditor
{
    public partial class About : Form
    {
        MainForm Frm1;
        public About(MainForm F)
        {
            InitializeComponent();
            this.Text = "About EERditor " + this.ProductVersion;
            Frm1 = F;
        }

        private void About_Load(object sender, EventArgs e)
        {
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            foreach (Control cntrls in this.Controls)
            {
                if (cntrls.GetType() == typeof(GroupBox))
                {
                    //cntrls.ForeColor = MainForm.themecolors.groupbox;
                    foreach (var cntr in cntrls.Controls)
                    {
                        if (cntr.GetType() == typeof(System.Windows.Forms.Label))
                        {
                            ((System.Windows.Forms.Label)cntr).ForeColor = MainForm.themecolors.foreground;
                            ((System.Windows.Forms.Label)cntr).BackColor = MainForm.themecolors.background;
                        }
                        if (cntr.GetType() == typeof(LinkLabel))
                        {
                            ((LinkLabel)cntr).ForeColor = MainForm.themecolors.foreground;
                            ((LinkLabel)cntr).BackColor = MainForm.themecolors.background;
                            ((LinkLabel)cntr).LinkColor = MainForm.themecolors.link;
                            ((LinkLabel)cntr).VisitedLinkColor = MainForm.themecolors.visitedlink;
                            ((LinkLabel)cntr).ActiveLinkColor = MainForm.themecolors.activelink;
                        }
                        if (cntr.GetType() == typeof(Button))
                        {
                            ((Button)cntr).ForeColor = MainForm.themecolors.foreground;
                            ((Button)cntr).BackColor = MainForm.themecolors.accent;
                            ((Button)cntr).FlatStyle = FlatStyle.Flat;
                        }
                    }
                }
            }
        }

        
        private void Button_Click(object sender, EventArgs e)
        {
            string link1 = null;
            switch (((Button)sender).Name.ToString())
            {
                case "ForumButton":
                    link1 = null;
                    break;
                case "BugsOrFeatureButton":
                    link1 = "https://github.com/capashaa/EEREDitor/issues";
                    break;
                case "CreditButton":
                    link1 = "https://github.com/capasha/EEOEditor/wiki/Credits";
                    break;
                case "WikiButton":
                    link1 = "https://github.com/capashaa/EEOEditor/wiki";
                    break;
                case "HomepageButton":
                    link1 = "https://github.com/capashaa/EEREDitor";
                    break;
                case "btnDiscord":
                    link1 = "https://discord.gg/6eetnHqc";
                    break;
            }
            if (link1 != null)
            {
                DialogResult dgresult = MessageBox.Show($"Do you want to open {link1}\nin your webbrowser?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dgresult == DialogResult.Yes)
                {
                    Process.Start(link1);
                }
            }
        }
        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string link = null;
            switch (((LinkLabel)sender).Text)
            {
                case "font and text insertion":
                    link = "https://forums.everybodyedits.com/viewtopic.php?pid=515488#p515488";
                    break;
                case "Google":
                    link = "https://github.com/google/material-design-icons";
                    break;
                case "Material Design Icons contributors":
                    link = "https://materialdesignicons.com/";
                    break;
                case "Fatcow Icons":
                    link = "https://www.fatcow.com/free-icons";
                    break;
                case "Bresenham's Algorithm (Ellipse,Line)":
                    link = "http://members.chello.at/~easyfilter/bresenham.html";
                    break;
                case "Json.NET Newtonsoft":
                    link = "https://www.newtonsoft.com/json";
                    break;
                case "PlayerIO SDK":
                    link = "https://playerio.com/download/";
                    break;
                case "SharpCompress - ZIP tool":
                    link = "https://github.com/adamhathcock/sharpcompress";
                    break;
                case "Lukem's .eelvl parser":
                    link = "https://gitlab.com/LukeM212/EELVL/tree/legacy";
                    break;
            }
            DialogResult dr = MessageBox.Show($"Do you want to open {link}\nin your webbrowser?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                Process.Start(link);
            }
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            /*Updater updater = new Updater();
            updater.ShowDialog();*/
        }

        private void linkLabel_MouseHover(object sender, EventArgs e)
        {
            ((LinkLabel)sender).LinkColor = MainForm.themecolors.activelink;
        }

        private void linkLabel_MouseLeave(object sender, EventArgs e)
        {
            ((LinkLabel)sender).LinkColor = MainForm.themecolors.link;
        }

    }

}
