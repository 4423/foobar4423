using System;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows;
using CoreTweet;
using foobar4423.Properties;
using NowPlayingLib;
using System.Diagnostics;
using System.Net;

namespace foobar4423
{
    public partial class Form1 : Form
    {
        private Tokens tokens;
        private IMediaPlayer player;

        private string ScreenName
        {
            get
            {
                string screenName = Settings.Default.ScreenName;
                return (screenName == string.Empty) ? screenName : "@" + screenName;
            }
        }


        public Form1()
        {
            InitializeComponent();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            player = new NowPlayingLib.Foobar2000();
            var p = player as INotifyPlayerStateChanged;
            if (p != null)
            {
                p.CurrentMediaChanged += OnCurrentMediaChanged;
            }

            Application.ApplicationExit += (_, __) =>
            {
                this.NotifyIcon.Dispose();
                this.player.Dispose();
            };

            try {
                tokens = Tokens.Create(SecretResources.CK, SecretResources.CS, Settings.Default.AT, Settings.Default.ATS);
            }
            catch (Exception) {
                StatusLabel.Text = "Faild to recognize the token";
            }

            ScreenNameLabel.Text = ScreenName;
        }


       
/*******メニュー画面*******/
        private void toolStripMenuItem_Config_Click(object sender, EventArgs e)
        {
            Form_Config fc = new Form_Config();
            fc.ShowDialog();
        }

        private void ToolStripMenuItem_OAuth_Click(object sender, EventArgs e)
        {
            Form_OAuth fo = new Form_OAuth();
            fo.ShowDialog();

            ScreenNameLabel.Text = ScreenName;
            tokens = Tokens.Create(SecretResources.CK, SecretResources.CS, Settings.Default.AT, Settings.Default.ATS);
        }

        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
/*******ツイート*******/
        /// <summary>
        /// textBoxのなうぷれをツイート
        /// </summary>
        private async Task PostNowPlaying()
        {
            try
            {
                await tokens.Statuses.UpdateAsync(status => TweetText.Text);
                NotifyTweetResult(isSucceeded: true);
            }
            catch (TwitterException)
            {
                NotifyTweetResult(isSucceeded: false);
            }
        }

        private void NotifyTweetResult(bool isSucceeded)
        {
            var message = isSucceeded ? "Tweet succeeded" : "Failed to tweet";
            var icon = isSucceeded ? ToolTipIcon.Info : ToolTipIcon.Error;

            StatusLabel.Text = message;
            if (Settings.Default.IsBalloon)
            {
                ShowBalloonTip(icon, message, this.TweetText.Text);
            }
        }

        private async void button_post_Click(object sender, EventArgs e)
        {
            button_post.Enabled = false;
            button_post.Text = "Posting";

            await PostNowPlaying();

            button_post.Enabled = true;
            button_post.Text = "Post";
        }


/*******foobar2000*******/
        /// <summary>
        /// なうぷれを取得
        /// </summary>
        private async Task GetNowPlaying()
        {
            try
            {
                var media = await this.player.GetCurrentMedia();
                SetCurrentMedia(media);
            }
            catch (Exception)
            {
                TweetText.Text = "";
                StatusLabel.Text = "Faild to generate NowPlaying";
            }
        }

        private void SetCurrentMedia(MediaItem media)
        {
            if (player.PlayerState != PlayerState.Playing)
            {
                TweetText.Text = "";
                StatusLabel.Text = "foobar2000 is not playing";
                return;
            }

            // なうぷれ取得
            string text = NowPlayingParser.Parse(Settings.Default.NowPlayingFormat, media);
            TweetText.Text = text;
            StatusLabel.Text = "NowPlaying succeeded";
        }

        private async void button_getNowPlaying_Click(object sender, EventArgs e)
        {
            button_getNowPlaying.Enabled = false;

            if (Process.GetProcessesByName("foobar2000").Length != 0)
            {
                await GetNowPlaying();
            }
            else
            {
                StatusLabel.Text = "Faild to detect foobar2000";
            }
            
            button_getNowPlaying.Enabled = true;
        }


/*******文字数カウント*******/

        private void textBox_info_TextChanged(object sender, EventArgs e)
        {
            int length = TweetText.Text.Length;
            CountLabel.Text = length.ToString().PadLeft(3);

            this.button_post.Enabled = length > 0;            
        }
        
        private async void OnCurrentMediaChanged(object sender, CurrentMediaChangedEventArgs e)
        {
            if (checkBox_autoPost.Checked)
            {
                SyncInvoke(() => SetCurrentMedia(e.CurrentMedia));
                await PostNowPlaying();
            }
        }

        #region "  通知領域  "

        //最小化時
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                NotifyIcon.Visible = true;
            }
        }

        //復元
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Display();
        }

        private void ShowBalloonTip(ToolTipIcon icon, string title, string text, int time = 1000)
        {
            this.NotifyIcon.BalloonTipIcon = icon;
            this.NotifyIcon.BalloonTipTitle = title;
            this.NotifyIcon.BalloonTipText = text;
            this.NotifyIcon.ShowBalloonTip(time);
        }
        
        /// <summary>
        /// 再表示
        /// </summary>
        private void Display()
        {
            this.Visible = true;
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.NotifyIcon.Visible = false;
            this.Activate();
        }
        
        #endregion

/*******コンテキストメニュー*******/
        private void showSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Display();
        }

        private async void postNowPlayingPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await GetNowPlaying();
            await PostNowPlaying();
        }

        private void exitEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


/*******ユーティリティ*******/
        /// <summary>
        /// 別スレッド上からUIスレッドを操作
        /// </summary>
        /// <seealso cref="http://xin9le.net/articles/21"/>
        private void SyncInvoke(Action action)
        {
            if (this.InvokeRequired) this.Invoke(action);
            else action();
        }
    }
}
