﻿using System;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows;
using TweetSharp;
using foobar4423.Properties;
using NowPlayingLib;

namespace foobar4423
{
    public partial class Form1 : Form
    {
        private TwitterService service;
        private IMediaPlayer player = new NowPlayingLib.Foobar2000();

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

            Application.ApplicationExit += (_,__)=>this.notifyIcon1.Dispose();

            try {
                service = new TwitterService(Resources.CK, Resources.CS);
                service.AuthenticateWith(Settings.Default.AT, Settings.Default.ATS);
            }
            catch (Exception) {
                SyncInvoke(() => toolStripStatusLabel_status.Text = "Faild to recognize the token");
            }

            toolStripTextBox_screenName.Text = ScreenName;
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

            toolStripTextBox_screenName.Text = ScreenName;
        }

        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
/*******ツイート*******/
        /// <summary>
        /// textBoxのなうぷれをツイート
        /// </summary>
        private void PostNowPlaying()
        {
            try
            {
                var isSuccess = service.SendTweet(new SendTweetOptions { Status = textBox_info.Text });
                SyncInvoke(() =>
                    toolStripStatusLabel_status.Text = isSuccess != null ? "Success!" : "Failed to post the tweet");
            }
            catch (Exception ex)
            {
                SyncInvoke(() =>
                    toolStripStatusLabel_status.Text = ex.Message);
            }
        }


        private async void button_post_Click(object sender, EventArgs e)
        {
            button_post.Enabled = false;
            button_post.Text = "Posting";

            await Task.Run(() => PostNowPlaying());

            button_post.Enabled = true;
            button_post.Text = "Post";
        }


/*******foobar2000*******/
        /// <summary>
        /// なうぷれを取得
        /// </summary>
        private async void GetNowPlaying()
        {
            IMediaPlayer player = new NowPlayingLib.Foobar2000();
            var p = player as INotifyPlayerStateChanged;
            if (p != null)
            {
                p.CurrentMediaChanged += SetCurrentMedia;
            }
            SetCurrentMedia(player, new CurrentMediaChangedEventArgs(await player.GetCurrentMedia()));            
        }

        private void SetCurrentMedia(object sender, CurrentMediaChangedEventArgs e)
        {
            try {
                // なうぷれ取得
                string text = NowPlayingParser.Parse(Settings.Default.NowPlayingFormat, e.CurrentMedia);
                SyncInvoke(() =>
                {
                    textBox_info.Text = text;
                    toolStripStatusLabel_status.Text = "Success!";
                });
            }
            catch (ArgumentException)
            {
                SyncInvoke(() =>
                {
                    textBox_info.Text = "";
                    toolStripStatusLabel_status.Text = "Faild to generate NowPlaying";
                });
            }
        }

        private async void button_getNowPlaying_Click(object sender, EventArgs e)
        {
            button_getNowPlaying.Enabled = false;
            await Task.Run(() => GetNowPlaying());
            button_getNowPlaying.Enabled = true;
        }


/*******文字数カウント*******/

        private void textBox_info_TextChanged(object sender, EventArgs e)
        {
            int length = textBox_info.Text.Length;
            toolStripStatusLabel_count.Text = length.ToString().PadLeft(3);

            this.button_post.Enabled = length > 0;            
        }
        
        
        private void checkBox_autoPost_CheckedChanged(object sender, EventArgs e)
        {
            var p = this.player as INotifyPlayerStateChanged;
            if (p != null)
            {
                if (checkBox_autoPost.Checked)
                {
                    p.CurrentMediaChanged += SetCurrentMedia;
                }
                else
                {
                    p.CurrentMediaChanged -= SetCurrentMedia;
                }
            }
        }

        #region "  通知領域  "

            //最小化時
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                notifyIcon1.Visible = true;
            }
        }

        //復元
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Display();
        }
       
        private void toolStripStatusLabel_status_TextChanged(object sender, EventArgs e)
        {
            if (!Settings.Default.IsBalloon) return;

            switch (toolStripStatusLabel_status.Text)
            {
                case "Success!":
                    ShowBalloonTip(ToolTipIcon.Info, "Post NowPlaying!", this.textBox_info.Text);
                    break;
                case "Failed to post the tweet":
                    ShowBalloonTip(ToolTipIcon.Error, "Faild to post the tweet", this.textBox_info.Text);
                    break;
            }
        }
       
        private void ShowBalloonTip(ToolTipIcon icon, string title, string text, int time = 1000)
        {
            this.notifyIcon1.BalloonTipIcon = icon;
            this.notifyIcon1.BalloonTipTitle = title;
            this.notifyIcon1.BalloonTipText = text;
            this.notifyIcon1.ShowBalloonTip(time);
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
            this.notifyIcon1.Visible = false;
            this.Activate();
        }
        
        #endregion

/*******コンテキストメニュー*******/
        private void showSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void postNowPlayingPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetNowPlaying();
            PostNowPlaying();
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
