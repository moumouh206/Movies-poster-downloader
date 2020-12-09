using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
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
                    label1.Text = "";
                    listView1.Items.Clear();
                    string[] fls = Directory.GetDirectories(fbd.SelectedPath);
                    label1.Text = "Folders found: " + fls.Length.ToString();
                    progressBar1.Maximum = fls.Length;
                    total= fls.Length; 
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

        private void StartDownloadBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Downloadling");
            if (total > 0)
            {
                MessageBox.Show("Downloadling");
                TMDbClient client = new TMDbClient("3107e4ce8b2ea681bbd58cb0208968ea");
                WebClient webClient = new WebClient();
                SearchContainer<SearchMovie> results = client.SearchMovieAsync("007").Result;

                TMDbConfig cfg = new TMDbConfig();
                Movie movie = new Movie();
                string uri = "";
                MessageBox.Show($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");
                foreach (SearchMovie result in results.Results)
                {
                    MessageBox.Show(result.Title);
                    //Console.WriteLine(result.PosterPath);
                    //movie = client.GetMovie(MovieMethods.Images );
                    //DateTime value = Convert.ToDateTime(movie.ReleaseDate);

                    //uri = GetMediaInformation.FetchImage(result.Id).ToString();
                    //Console.WriteLine(uri);
                    //if (year == value.Year.ToString())
                    //    break;
                }
                //byte[] bytes = webClient.DownloadData(uri);
                //MemoryStream ms = new MemoryStream(bytes);
                //Image artWork = Image.FromStream(ms);
            }
            else
            {
                MessageBox.Show("No title found in the list , please search for movies before clicking this button", "No movies found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
