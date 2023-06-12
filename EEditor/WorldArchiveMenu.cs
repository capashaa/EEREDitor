using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading;
using SharpCompress.Archives;

namespace EEditor
{
    public partial class WorldArchiveMenu : Form
    {
        private const string worldArchiveUri = "http://vps.eejesse.net:8080/";
        private List<JWorld> worlds { get; set; }
        private string loadedUsername { get; set; } = "";

        internal MainForm parentForm;

        public WorldArchiveMenu(MainForm parent)
        {
            this.parentForm = parent;
            InitializeComponent();
        }

        private void DisplayArchive(IEnumerable<JWorld> worlds)
        {
            lvWorldArchive.Items.Clear();
            lvWorldArchive.Groups.AddRange(worlds.Select(world => new ListViewGroup(world.Key, world.Key)).ToArray());
            lvWorldArchive.Items.AddRange(worlds.Select(world => {
                var item = new ListViewItem(world.Name) { Group = lvWorldArchive.Groups[world.Key] };
                item.SubItems.Add(world?.Version ?? "?");
                return item;
            }).ToArray());

            this.worlds = worlds.ToList();
        }

        public void LoadArchiveFromFile(string filepath)
        {
            try {
                this.loadedUsername = "";

                using (var archive = File.Open(filepath, FileMode.Open)) {
                    var (keys, json_worlds) = Helpers.ExtractArchive(archive);
                    var worlds = keys.Select((key, index) => new JWorld(key, json_worlds[index]));

                    this.DisplayArchive(worlds);
                }
            }
            catch (Exception ex) {
                if (FlexibleMessageBox.Show($"An error has occured while loading the archive!\n\nAdditional details: {ex}", "Unable to load archive.",
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry) {
                    this.LoadArchiveFromFile(filepath);
                }
            }
        }

        public void LoadArchiveFromAPI(string username)
        {
            var requestUri = $"{worldArchiveUri}/archive/username/{username}";

            try {
                using (var archive = new MemoryStream(new WebClient() { Proxy = null }.DownloadData(requestUri))) {
                    Console.WriteLine("yes");
                    var (keys, json_worlds) = Helpers.ExtractArchive(archive);
                    var worlds = keys.Select((key, index) => new JWorld(key, json_worlds[index]));

                    this.DisplayArchive(worlds);
                    this.loadedUsername = username;
                }
            }
            catch (Exception ex) {
                if (FlexibleMessageBox.Show($"An error has occured while loading the archive!\n\nAdditional details: {ex}", "Unable to load archive.",
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry) {
                    this.LoadArchiveFromAPI(username);
                }
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
        }

        private void LoadWorld(object sender, MouseEventArgs e)
        {
            if (lvWorldArchive.SelectedItems.Count < 1)
                return;

            var selectedItem = lvWorldArchive.SelectedItems[0];
            var world = this.worlds.Find(w =>
                w.Key == selectedItem.Group.Name && w.Name == selectedItem.Text && selectedItem.SubItems[1].Text == w.Version
            );

            if (world != null)
                MainForm.editArea.Init(world.Frame, false);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            FlexibleMessageBox.Show("World Service is a continously updating historical archive for Everybody Edits worlds.\n\n" +
                            "As inspired by the Wayback Machine, the project started in 2016 as the replacement for the now deprecated EEBackup.\n" +
                            "The project has been running nearly uninterrupted since then, continously scanning the lobby for updated worlds.\n\n" +
                            "If you have any questions, post a reply & they can be addressed at the Everybody Edits forum topic located here:\n" +
                            "https://forums.everybodyedits.com/viewtopic.php?id=40554 \n\n" +
                            "- Atilla Lonny | atil.la | contact@atil.la", "World Service Historical Archive", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RefreshArchive(object sender, EventArgs e)
        {
            this.LoadArchiveFromAPI(this.loadedUsername);
        }

        private void btnLoadUserArchive_Click(object sender, EventArgs e)
        {
            this.LoadArchiveFromAPI(tbLoadUserArchiveUsername.Text);
        }
    }

    public static class Helpers
    {
        public static (string[] keys, string[] json_worlds) ExtractArchive(Stream masterArchive)
        {
            string sanitize(string key) => key.Length >= 13 ? key.Substring(0, 13) : key.Substring(0, key.IndexOf('.'));

            using (var tarStream = new MemoryStream()) {
                ArchiveFactory.Open(masterArchive).Entries.First().WriteTo(tarStream);

                var entries = ArchiveFactory.Open(tarStream).Entries.Where(entry => !entry.IsDirectory);
                return (entries.Select(entry => sanitize(entry.Key)).ToArray(), entries.Select(entry => {
                    var stream = new MemoryStream();
                    entry.WriteTo(stream);
                    return stream;
                }).Select(stream => ArchiveFactory.Open(stream).Entries).Select(entry => {
                    var stream = new MemoryStream();
                    entry.First().WriteTo(stream);
                    return stream;
                }).Select(stream => Encoding.UTF8.GetString(stream.ToArray())).ToArray());
            }
        }
    }

    public class JWorld
    {
        public string Key { get; }
        public string JSON { get; }
        public string Name { get; }
        public string Version { get; }
        public Frame Frame => Frame.LoadJSONDatabaseWorld(this.JSON, false);

        public JWorld(string key, string json)
        {
            this.Key = key;
            this.JSON = json;

            this.Name = JObject.Parse(json).TryGetValue("name", StringComparison.CurrentCultureIgnoreCase, out var name) ? name.Value<string>() : "Untitled World";
            this.Version = JObject.Parse(json).TryGetValue("version", StringComparison.CurrentCultureIgnoreCase, out var version) ? version.Value<string>() : "?";
        }
    }
}
