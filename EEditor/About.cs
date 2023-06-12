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
            this.Text = "About EBEditor " + this.ProductVersion;
            Frm1 = F;
        }

        private void About_Load(object sender, EventArgs e)
        {
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].GetType() == typeof(GroupBox))
                {
                    this.Controls[i].ForeColor = MainForm.themecolors.foreground;
                    for (int a = 0; a < this.Controls[i].Controls.Count; a++)
                    {
                        if (this.Controls[i].Controls[a].GetType() == typeof(Label))
                        {
                            this.Controls[i].Controls[a].ForeColor = MainForm.themecolors.foreground;
                            this.Controls[i].Controls[a].BackColor = MainForm.themecolors.background;
                        }
                        if (this.Controls[i].Controls[a].GetType() == typeof(LinkLabel))
                        {
                            ((LinkLabel)this.Controls[i].Controls[a]).ForeColor = MainForm.themecolors.foreground;
                            ((LinkLabel)this.Controls[i].Controls[a]).BackColor = MainForm.themecolors.background;
                            ((LinkLabel)this.Controls[i].Controls[a]).LinkColor = MainForm.themecolors.link;
                            ((LinkLabel)this.Controls[i].Controls[a]).VisitedLinkColor = MainForm.themecolors.visitedlink;
                            ((LinkLabel)this.Controls[i].Controls[a]).ActiveLinkColor = MainForm.themecolors.activelink;



                        }
                        if (this.Controls[i].Controls[a].GetType() == typeof(Button))
                        {
                            ((Button)this.Controls[i].Controls[a]).ForeColor = MainForm.themecolors.foreground;
                            ((Button)this.Controls[i].Controls[a]).BackColor = MainForm.themecolors.accent;
                            ((Button)this.Controls[i].Controls[a]).FlatStyle = FlatStyle.Flat;
                        }
                    }
                }
                //if (this.Controls[i].name)
            }
        }
    }

    
}
