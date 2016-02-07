﻿using System;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows;
using TweetSharp;
using foobar4423.Properties;
using NowPlayingLib;
using System.Diagnostics;

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

            Application.ApplicationExit += (_, __) => this.NotifyIcon.Dispose();

            try {
                service = new TwitterService(Resources.CK, Resources.CS);
                service.AuthenticateWith(Settings.Default.AT, Settings.Default.ATS);
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
            service.AuthenticateWith(Settings.Default.AT, Settings.Default.ATS);
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
                var status = service.SendTweet(new SendTweetOptions { Status = TweetText.Text });
                SyncInvoke(() => StatusLabel.Text = status != null ? "Tweet succeeded" : "Failed to tweet");
                TweetStatusChanged(status);
            }
            catch (Exception ex)
            {
                SyncInvoke(() => StatusLabel.Text = ex.Message);
            }
        }

        private void TweetStatusChanged(TwitterStatus status)
        {
            if (!Settings.Default.IsBalloon) return;

            if (status != null)
            {
                ShowBalloonTip(ToolTipIcon.Info, "Tweet succeeded", this.TweetText.Text);
            }
            else
            {
                ShowBalloonTip(ToolTipIcon.Error, "Faild to tweet", this.TweetText.Text);
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
            SetCurrentMedia(player, new CurrentMediaChangedEventArgs(await player.GetCurrentMedia()));            
        }

        private void SetCurrentMedia(object sender, CurrentMediaChangedEventArgs e)
        {
            if (player.PlayerState != PlayerState.Playing)
            {
                SyncInvoke(() =>
                {
                    TweetText.Text = "";
                    StatusLabel.Text = "foobar2000 is not playing";
                });
                return;
            }

            try {
                // なうぷれ取得
                string text = NowPlayingParser.Parse(Settings.Default.NowPlayingFormat, e.CurrentMedia);
                SyncInvoke(() =>
                {
                    TweetText.Text = text;
                    StatusLabel.Text = "NowPlaying succeeded";
                });

                if (checkBox_autoPost.Checked)
                {
                    PostNowPlaying();
                }
            }
            catch (Exception)
            {
                SyncInvoke(() =>
                {
                    TweetText.Text = "";
                    StatusLabel.Text = "Faild to generate NowPlaying";
                });
            }
        }

        private async void button_getNowPlaying_Click(object sender, EventArgs e)
        {
            button_getNowPlaying.Enabled = false;

            if (Process.GetProcessesByName("foobar2000").Length != 0)
            {
                await Task.Run(() => GetNowPlaying());
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
