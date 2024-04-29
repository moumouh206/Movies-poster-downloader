using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
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
        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern void SHChangeNotify(
            int wEventId, int uFlags, IntPtr dwItem1, IntPtr dwItem2);
        bool busy = false;
        int total = 0;
        public Main()
        {
            InitializeComponent();
            Main.CheckForIllegalCrossThreadCalls = false;
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
                    progressBar1.Value = 0;
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
                        if (dirName.Contains("20"))
                        {
                            dirName = dir.Name.Substring(0, dir.Name.IndexOf("20"));
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

                        string[] row = { dirName, string.Empty,"" ,f };
                        var listViewItem = new ListViewItem(row);
                        listView1.Items.Add(listViewItem);
                        progressBar1.Value++;
                    }


                }
            }
        }

        private async void StartDownloadBtn_Click(object sender, EventArgs e)
        {
            if (total > 0)
            {
                progressBar1.Value = 0;
                bool anyIconDownloaded = false; // Flag to track if any icon was downloaded successfully
                try
                {
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        string dir = listView1.Items[i].SubItems[3].Text;
                        progressBar1.Value += 1;
                        if (!File.Exists(dir + @"\poster.ico") || !File.Exists(dir + @"\desktop.ini"))
                        {
                            await Task.Run(async () =>
                            {
                                try
                                {
                                    bool found = false;
                                    TMDbClient client = new TMDbClient("3107e4ce8b2ea681bbd58cb0208968ea");
                                    WebClient webClient = new WebClient();
                                    SearchContainer<SearchMovie> results = await client.SearchMovieAsync(listView1.Items[i].SubItems[0].Text);

                                    // Invoke UI updates on the UI thread
                                    listView1.Invoke((MethodInvoker)delegate
                                    {
                                        listView1.Items[i].SubItems[1].Text = "Searching ...";
                                    });

                                    string uri = string.Empty;
                                    foreach (SearchMovie result in results.Results)
                                    {
                                        if (result.PosterPath != string.Empty)
                                        {
                                            // Invoke UI updates on the UI thread
                                            listView1.Invoke((MethodInvoker)delegate
                                            {
                                                listView1.Items[i].SubItems[1].Text = "Downloading ...";
                                            });

                                            uri = "https://image.tmdb.org/t/p/w500" + result.PosterPath;
                                            try
                                            {
                                                byte[] bytes = await webClient.DownloadDataTaskAsync(uri);
                                                MemoryStream ms = new MemoryStream(bytes);
                                                Image artWork = Image.FromStream(ms);
                                                artWork.Save(dir + "\\" + result.Id.ToString() + ".jpg");
                                                if (!File.Exists(dir + @"\poster.ico"))
                                                {
                                                    ImagingHelper.ConvertToIcon(dir + "\\" + result.Id.ToString() + ".jpg", dir + "\\poster.ico", 256, false);
                                                    File.SetAttributes(dir + @"\poster.ico", File.GetAttributes(dir + @"\poster.ico") | FileAttributes.Hidden | FileAttributes.ReadOnly);
                                                    File.SetAttributes(dir, File.GetAttributes(dir) | FileAttributes.ReadOnly);
                                                    anyIconDownloaded = true; // Set the flag to true if an icon was downloaded
                                                }
                                                if (!File.Exists(dir + @"\desktop.ini"))
                                                {
                                                    string[] lines = { "[.ShellClassInfo]", "IconResource=poster.ico,0" };
                                                    File.WriteAllLines(dir + @"\desktop.ini", lines);
                                                    File.SetAttributes(dir + @"\desktop.ini", File.GetAttributes(dir + @"\desktop.ini") | FileAttributes.Hidden | FileAttributes.ReadOnly);
                                                }
                                                // Invoke UI updates on the UI thread
                                                listView1.Invoke((MethodInvoker)delegate
                                                {
                                                    listView1.Items[i].SubItems[1].Text = "Done";
                                                    found = true;
                                                    Movie movie = client.GetMovieAsync(result.Id).Result;
                                                    listView1.Items[i].SubItems[2].Text = movie.Genres[0].Name;
                                                    listView1.Items[i].SubItems[3].Text = movie.VoteAverage.ToString();
                                                });
                                                break;
                                            }
                                            catch (Exception ex)
                                            {
                                                // Invoke UI updates on the UI thread
                                                listView1.Invoke((MethodInvoker)delegate
                                                {
                                                    listView1.Items[i].SubItems[1].Text = ex.Message;
                                                });
                                            }
                                        }
                                    }
                                    if (!found && !File.Exists(listView1.Items[i].SubItems[3].Text + @"\poster.ico"))
                                    {
                                        // Invoke UI updates on the UI thread
                                        listView1.Invoke((MethodInvoker)delegate
                                        {
                                            listView1.Items[i].SubItems[1].Text = "Not found.";
                                        });
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // Invoke UI updates on the UI thread
                                    listView1.Invoke((MethodInvoker)delegate
                                    {
                                        MessageBox.Show(ex.Message);
                                    });
                                }
                            });
                        }
                        else
                        {
                            // Invoke UI updates on the UI thread
                            listView1.Invoke((MethodInvoker)delegate
                            {
                                listView1.Items[i].SubItems[1].Text = "Icon already exist";
                                listView1.Items[i].SubItems[1].ForeColor = Color.LightGray;
                            });
                        }
                    }

                    // Refresh icons if any icon was downloaded successfully
                    if (anyIconDownloaded)
                    {
                        // Loop through the list and refresh icons
                        for (int i = 0; i < listView1.Items.Count; i++)
                        {
                            string dir = listView1.Items[i].SubItems[3].Text;
                            if (File.Exists(dir + @"\poster.ico") && File.Exists(dir + @"\desktop.ini"))
                            {
                                File.SetAttributes(dir + @"\poster.ico", File.GetAttributes(dir + @"\poster.ico") | FileAttributes.Hidden | FileAttributes.ReadOnly);
                                File.SetAttributes(dir, File.GetAttributes(dir) | FileAttributes.ReadOnly);
                            }
                            if (!File.Exists(dir + @"\desktop.ini"))
                            {
                                string[] lines = { "[.ShellClassInfo]", "IconResource=poster.ico,0" };
                                File.WriteAllLines(dir + @"\desktop.ini", lines);
                                File.SetAttributes(dir + @"\desktop.ini", File.GetAttributes(dir + @"\desktop.ini") | FileAttributes.Hidden | FileAttributes.ReadOnly);
                            }
                        }
                    }

                    // Notify shell of changes after all downloads are complete
                    Invoke((MethodInvoker)delegate
                    {
                        SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
                    });

                    MessageBox.Show("Cover downloading is completed", "Operation completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No title found in the list, please search for movies before clicking this button", "No movies found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void aboutbtn_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void gameFolderIconChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void GameCoverBtn_Click(object sender, EventArgs e)
        {
            if (total > 0)
            {
                progressBar1.Value = 0;
                try
                {
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        string dir = listView1.Items[i].SubItems[3].Text;
                        
                        progressBar1.Value += 1;
                        if (File.Exists(dir + @"\cover.ico") && !File.Exists(dir + @"\desktop.ini"))
                        {

                            try
                            {
                                if (File.Exists(dir + @"\cover.ico"))
                                {
                                    File.SetAttributes(dir + @"\cover.ico", File.GetAttributes(dir + @"\cover.ico") | FileAttributes.Hidden | FileAttributes.ReadOnly);
                                    File.SetAttributes(dir, File.GetAttributes(dir) | FileAttributes.ReadOnly);
                                }
                                if (!File.Exists(dir + @"\desktop.ini"))
                                {
                                    string[] lines = { "[.ShellClassInfo]", "IconResource=cover.ico,0" };
                                    File.WriteAllLines(dir + @"\desktop.ini", lines);
                                    File.SetAttributes(dir + @"\desktop.ini", File.GetAttributes(dir + @"\desktop.ini") | FileAttributes.Hidden | FileAttributes.ReadOnly);
                                }
                                SHChangeNotify(0x08000000, 0x0000, (IntPtr)null, (IntPtr)null);
                                listView1.Items[i].SubItems[1].Text = "Done";
                                listView1.Refresh();
                                //break;
                            }
                            catch (Exception ex)
                            {
                                listView1.Items[i].SubItems[1].Text = ex.Message;
                            }
                        }
                        else
                        {
                            listView1.Items[i].SubItems[1].Text = "Icon already exist";
                            listView1.Items[i].SubItems[1].ForeColor = Color.LightGray;
                        }

                    }
                    MessageBox.Show("Cover downloading is completed", "Operation completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
