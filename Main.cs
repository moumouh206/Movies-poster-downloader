using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Movies_poster_downloader
{
    public partial class Main : Form
    {
        bool busy = false;
        int total = 0;
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
                    label1.Text = string.Empty;
                    listView1.Items.Clear();
                    string[] fls = Directory.GetDirectories(fbd.SelectedPath);
                    label1.Text = "Folders found: " + fls.Length.ToString();
                    progressBar1.Maximum = fls.Length;
                    total = fls.Length;
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
                        if (dirName.Contains("Season"))
                        {
                            dirName = dir.Name.Substring(0, dir.Name.IndexOf("Season"));
                        }
                        if (dirName.Contains("season"))
                        {
                            dirName = dir.Name.Substring(0, dir.Name.IndexOf("season"));
                        }
                        dirName = dirName.Replace(".", " ").Replace("-", " ").Replace("_", " ");

                        string[] row = { dirName, string.Empty, f };
                        var listViewItem = new ListViewItem(row);
                        listView1.Items.Add(listViewItem);
                        progressBar1.Value++;
                    }


                }
            }
        }

        private void StartDownloadBtn_Click(object sender, EventArgs e)
        {

            if (total > 0)
            {
                progressBar1.Value = 0;
                try
                {
                    // MessageBox.Show(listView1.Items.Count.ToString());
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        progressBar1.Value += 1;
                        try
                        {
                            bool found = false;
                            TMDbClient client = new TMDbClient("3107e4ce8b2ea681bbd58cb0208968ea");
                            WebClient webClient = new WebClient();
                            SearchContainer<SearchMovie> results = client.SearchMovieAsync(listView1.Items[i].SubItems[0].Text).Result;
                            listView1.Items[i].SubItems[1].Text = "Searching ...";
                            listView1.Refresh();
                            string uri = string.Empty;
                            foreach (SearchMovie result in results.Results)
                            {
                                //MessageBox.Show(result.PosterPath);
                                if (result.PosterPath != string.Empty)
                                {
                                    listView1.Items[i].SubItems[1].Text = "Downloading ...";
                                    uri = "https://image.tmdb.org/t/p/w500" + result.PosterPath;
                                    Clipboard.SetText(uri);
                                    byte[] bytes = webClient.DownloadData(uri);
                                    MemoryStream ms = new MemoryStream(bytes);
                                    Image artWork = Image.FromStream(ms);
                                    artWork.Save(listView1.Items[i].SubItems[2].Text+"\\"+result.Id.ToString() + ".jpg");
                                    listView1.Items[i].SubItems[1].Text = "Done";
                                    found = true;
                                    listView1.Refresh();
                                    break;
                                }
                                listView1.Items[i].SubItems[1].Text = "Not found.";
                                listView1.Refresh();

                            }
                            if (!found)
                            {
                                listView1.Items[i].SubItems[1].Text = "Not found.";
                                listView1.Refresh();
                            }
                            

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }


            }
            else
            {
                MessageBox.Show("No title found in the list , please search for movies before clicking this button", "No movies found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
