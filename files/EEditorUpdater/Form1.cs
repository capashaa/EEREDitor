using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using SharpCompress.Archives;
using SharpCompress.Common;

namespace EEditorUpdater
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> changelog = new Dictionary<string, string>();
        private Dictionary<string, string> download = new Dictionary<string, string>();
        private string version = "3.5.0";
        private string githubLink = "https://api.github.com/repos/capasha/eeditor-/releases";
        private bool silent = false;
        private string EEditorCurrentVersion = null;
        private string text = null;
        private System.Timers.Timer timer = new System.Timers.Timer();
        public Form1(string[] args)
        {
            if (args.Count() == 1) if (args[0] == "/silent") silent = true;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = $"EEditor Downloader {this.ProductVersion}";
            TextToolStripStatusLabel.ForeColor = Color.Blue;

            if (File.Exists($"{Directory.GetCurrentDirectory()}\\EEditor.exe"))
            {
                FileVersionInfo.GetVersionInfo(Path.Combine(Directory.GetCurrentDirectory(), "EEditor.exe"));
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo($"{Directory.GetCurrentDirectory()}\\EEditor.exe");
                EEditorCurrentVersion = myFileVersionInfo.FileVersion;



                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(githubLink);
                request.Method = "GET";
                request.Accept = "application/vnd.github.v3+json";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:48.0) Gecko/20100101 Firefox/48.0";

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (request.HaveResponse && response != null)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            text = reader.ReadToEnd();
                        }
                        //Console.WriteLine(text);
                        dynamic stuff1 = Newtonsoft.Json.JsonConvert.DeserializeObject(text);
                        string ver = null;
                        foreach (var value in stuff1)
                        {
                            if (value["tag_name"] != null)
                            {
                                ver = value["tag_name"];
                            }
                            if (value["assets"] != null)
                            {
                                foreach (var val in value["assets"])
                                {
                                    if (val["browser_download_url"] != null)
                                    {
                                        download.Add(ver, Convert.ToString(val["browser_download_url"]));
                                    }
                                }
                            }
                        }
                        //Console.WriteLine(stuff1);
                        /*if (stuff1["assets"] != null)
                        {
                            foreach (var val in stuff1["assets"])
                            {
                                Console.WriteLine(val);
                                if (val["browser_download_url"] != null)
                                {
                                    dload = val["browser_download_url"];

                                }
                            }
                        }*/
                        foreach (var value in stuff1)
                        {
                            if (value["tag_name"] != null) version = value["tag_name"];
                            if (value["body"] != null) changelog.Add(version, value["body"].ToString());

                            ListViewItem lvi = new ListViewItem();
                            lvi.ForeColor = Color.Blue;
                            lvi.Text = version;


                            if (Convert.ToInt32(EEditorCurrentVersion.Replace(".", "")) < Convert.ToInt32(version.Replace(".", "")))
                            {
                                lvi.ForeColor = Color.DarkGreen;
                            }
                            else
                            {
                                lvi.ForeColor = Color.Blue;
                            }
                            listView1.Items.Add(lvi);


                        }
                        string versionlatest = stuff1[0]["tag_name"];
                        //if (stuff1["browser_download_url"] != null) Console.WriteLine(stuff1["browser_download_url"]);
                        //if (stuff1["tag_name"] != null) newversion = stuff1["tag_name"].ToString();
                        //Console.WriteLine("Version: " + stuff1["tag_name"] + "\nDownload Link: " + stuff1["html_url"] + "\n\nChangelog: \n" + stuff1["body"]);
                        if (Convert.ToInt32(EEditorCurrentVersion.Replace(".", "")) < Convert.ToInt32(versionlatest.Replace(".", "")))
                        {

                            DownloadButton.Enabled = false;
                            TextToolStripStatusLabel.ForeColor = Color.Green;
                            TextToolStripStatusLabel.Text = "Found a new update.";
                            bool exist = false;
                            foreach (Process var in Process.GetProcesses())
                            {
                                if (var.ProcessName == "EEditor")
                                {
                                    exist = true;
                                    DownloadButton.Enabled = false;
                                    TextToolStripStatusLabel.ForeColor = Color.Red;
                                    TextToolStripStatusLabel.Text = "You need to close EEditor to continue.";
                                    timer.Start();
                                    timer.Interval = 1000;
                                    timer.Elapsed += Timer_Elapsed;

                                }
                            }
                            if (!exist)
                            {
                                DownloadButton.Enabled = true;
                                TextToolStripStatusLabel.ForeColor = Color.Green;
                                TextToolStripStatusLabel.Text = "You can now download the newest update.";
                            }
                        }
                        else
                        {
                            if (silent) this.Close();
                            else
                            {
                                TextToolStripStatusLabel.ForeColor = Color.Green;
                                TextToolStripStatusLabel.Text = "You have the newest version.";
                            }
                        }

                    }
                }
            }
            else
            {
                TextToolStripStatusLabel.Text = "Where are I? I can't find EEditor!";
                TextToolStripStatusLabel.ForeColor = Color.Red;
            }


        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool found = false;
            foreach (Process var in Process.GetProcesses())
            {
                if (var.ProcessName == "EEditor")
                {
                    found = true;

                }
            }
            if (!found)
            {
                if (DownloadButton.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        DownloadButton.Enabled = true;
                    });
                }
                if (statusStrip1.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        TextToolStripStatusLabel.ForeColor = Color.Green;
                        TextToolStripStatusLabel.Text = "Select version and download.";
                    });
                }
                timer.Stop();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {

                ChangelogRichTextBox.Clear();
                string[] split = changelog[listView1.SelectedItems[0].Text].Split('*');
                for (int i = 0; i < split.Length; i++)
                {
                    if (split[i].Length > 1)
                    {
                        var value = split[i].Substring(1, split[i].Length - 1);
                        if (value.StartsWith("Added"))
                        {
                            ChangelogRichTextBox.SelectionColor = Color.Green;
                            ChangelogRichTextBox.AppendText("Added: ");
                            ChangelogRichTextBox.SelectionColor = Color.Black;
                            ChangelogRichTextBox.AppendText(value.Replace("Added", "").Substring(1, 1).ToUpper() + value.Replace("Added", "").Substring(2, value.Replace("Added", "").Length - 2));
                        }
                        else if (value.StartsWith("Removed"))
                        {
                            ChangelogRichTextBox.SelectionColor = Color.Red;
                            ChangelogRichTextBox.AppendText("Removed: ");
                            ChangelogRichTextBox.SelectionColor = Color.Black;
                            ChangelogRichTextBox.AppendText(value.Replace("Removed", "").Substring(1, 1).ToUpper() + value.Replace("Removed", "").Substring(2, value.Replace("Removed", "").Length - 2));
                        }
                        else if (value.StartsWith("Fixed"))
                        {
                            ChangelogRichTextBox.SelectionColor = Color.Blue;
                            ChangelogRichTextBox.AppendText("Fixed: ");
                            ChangelogRichTextBox.SelectionColor = Color.Black;
                            ChangelogRichTextBox.AppendText(value.Replace("Fixed", "").Substring(1, 1).ToUpper() + value.Replace("Fixed", "").Substring(2, value.Replace("Fixed", "").Length - 2));
                        }
                        else
                        {
                            ChangelogRichTextBox.SelectionColor = Color.Brown;
                            ChangelogRichTextBox.AppendText(value);
                        }
                    }
                }
            }
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                bool processExist = false;
                foreach (Process theprocess in Process.GetProcesses())
                {
                    if (theprocess.ProcessName == "EEditor")
                    {

                        processExist = true;
                        break;
                    }

                }
                if (processExist)
                {
                    DownloadButton.Enabled = false;
                    TextToolStripStatusLabel.Text = "Close EEditor to continue.";
                    TextToolStripStatusLabel.ForeColor = Color.Red;
                    timer.Start();
                    timer.Interval = 1000;
                    timer.Elapsed += Timer_Elapsed;

                }
                else
                {
                    var paths = Path.GetTempPath();
                    WebClient wc = new WebClient();
                    wc.DownloadFileAsync(new Uri(download[listView1.SelectedItems[0].Text]), $"{paths}EEditor_downloaded.zip");
                    wc.DownloadProgressChanged += delegate (object sender2, DownloadProgressChangedEventArgs m)
                    {
                        progressBar1.Value = m.ProgressPercentage;
                    };
                    wc.DownloadFileCompleted += delegate (object sender1, System.ComponentModel.AsyncCompletedEventArgs ee)
                    {
                        if (File.Exists($"{paths}EEditor_downloaded.zip"))
                        {
                            var archive = ArchiveFactory.Open($"{paths}EEditor_downloaded.zip");
                            foreach (var entry in archive.Entries)
                            {
                                if (!entry.IsDirectory)
                                {
                                    if (entry.Key != "EEditorUpdater.exe")
                                        entry.WriteToDirectory(Directory.GetCurrentDirectory(), new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                                }
                            }
                            TextToolStripStatusLabel.Text = "Finished.";
                            TextToolStripStatusLabel.ForeColor = Color.Green;
                            progressBar1.Value = 0;
                        }
                    };
                }
            }
        }
    }
}
