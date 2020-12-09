
namespace Movies_poster_downloader
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.aboutbtn = new System.Windows.Forms.MenuStrip();
            this.aboutBtnmenu = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.movietitle = new System.Windows.Forms.ColumnHeader();
            this.staus = new System.Windows.Forms.ColumnHeader();
            this.path = new System.Windows.Forms.ColumnHeader();
            this.StartDownloadBtn = new System.Windows.Forms.Button();
            this.openFolderBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.aboutbtn.SuspendLayout();
            this.SuspendLayout();
            // 
            // aboutbtn
            // 
            this.aboutbtn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutBtnmenu});
            this.aboutbtn.Location = new System.Drawing.Point(0, 0);
            this.aboutbtn.Name = "aboutbtn";
            this.aboutbtn.Size = new System.Drawing.Size(562, 24);
            this.aboutbtn.TabIndex = 0;
            this.aboutbtn.Text = "   About   ";
            // 
            // aboutBtnmenu
            // 
            this.aboutBtnmenu.CheckOnClick = true;
            this.aboutBtnmenu.Image = ((System.Drawing.Image)(resources.GetObject("aboutBtnmenu.Image")));
            this.aboutBtnmenu.Name = "aboutBtnmenu";
            this.aboutBtnmenu.Size = new System.Drawing.Size(101, 20);
            this.aboutBtnmenu.Text = "     &About      ";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.movietitle,
            this.staus,
            this.path});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(538, 277);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // movietitle
            // 
            this.movietitle.Name = "movietitle";
            this.movietitle.Text = "Movie title";
            this.movietitle.Width = 410;
            // 
            // staus
            // 
            this.staus.Name = "staus";
            this.staus.Text = "Status";
            this.staus.Width = 100;
            // 
            // path
            // 
            this.path.Name = "path";
            this.path.Text = "Path";
            this.path.Width = 0;
            // 
            // StartDownloadBtn
            // 
            this.StartDownloadBtn.Image = ((System.Drawing.Image)(resources.GetObject("StartDownloadBtn.Image")));
            this.StartDownloadBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StartDownloadBtn.Location = new System.Drawing.Point(386, 340);
            this.StartDownloadBtn.Name = "StartDownloadBtn";
            this.StartDownloadBtn.Size = new System.Drawing.Size(164, 26);
            this.StartDownloadBtn.TabIndex = 2;
            this.StartDownloadBtn.Text = "Start downloading";
            this.StartDownloadBtn.UseVisualStyleBackColor = true;
            this.StartDownloadBtn.Click += new System.EventHandler(this.StartDownloadBtn_Click);
            // 
            // openFolderBtn
            // 
            this.openFolderBtn.Image = ((System.Drawing.Image)(resources.GetObject("openFolderBtn.Image")));
            this.openFolderBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openFolderBtn.Location = new System.Drawing.Point(216, 340);
            this.openFolderBtn.Name = "openFolderBtn";
            this.openFolderBtn.Size = new System.Drawing.Size(164, 26);
            this.openFolderBtn.TabIndex = 3;
            this.openFolderBtn.Text = "Open movies folder";
            this.openFolderBtn.UseVisualStyleBackColor = true;
            this.openFolderBtn.Click += new System.EventHandler(this.openFolderBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 346);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 4;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 310);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(538, 18);
            this.progressBar1.TabIndex = 5;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 378);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.openFolderBtn);
            this.Controls.Add(this.StartDownloadBtn);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.aboutbtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.aboutbtn;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movies poster downloader";
            this.aboutbtn.ResumeLayout(false);
            this.aboutbtn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip aboutbtn;
        private System.Windows.Forms.ToolStripMenuItem aboutBtnmenu;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader movietitle;
        private System.Windows.Forms.ColumnHeader staus;
        private System.Windows.Forms.Button StartDownloadBtn;
        private System.Windows.Forms.Button openFolderBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader path;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

