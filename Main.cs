using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Movies_poster_downloader
{
    public partial class Main : Form
    {
        bool busy = false;
        public Main()
        {
            InitializeComponent();
        }

        private void openFolderBtn_Click(object sender, EventArgs e)
        {
            
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    label1.Text = "";
                    listView1.Items.Clear();
                    string[] fls = Directory.GetDirectories(fbd.SelectedPath);
                    label1.Text = "Folders found: " + fls.Length.ToString();
                    progressBar1.Maximum = fls.Length;
                    foreach (var f in fls)
                    {
                        var dir = new DirectoryInfo(f);
                        var dirName = dir.Name;
                        if (dirName.Contains("("))
                        {
                             dirName = dir.Name.Substring(0, dir.Name.IndexOf("("));
                        }
                        if (dirName.Contains("["))
                        {
                            dirName = dir.Name.Substring(0, dir.Name.IndexOf("["));
                        }
                         if (dirName.Contains("1080p"))
                        {
                            dirName = dir.Name.Substring(0, dir.Name.IndexOf("1080p"));
                        }
                         if (dirName.Contains("720p"))
                        {
                            dirName = dir.Name.Substring(0, dir.Name.IndexOf("720p"));
                        }
                        dirName = dirName.Replace(".", " ").Replace("-", " ").Replace("_", " ");

                        string[] row = { dirName,"",f };
                        var listViewItem = new ListViewItem(row);
                        listView1.Items.Add(listViewItem);
                        progressBar1.Value++;
                    }

                    
                }
            }
        }
    }
}
