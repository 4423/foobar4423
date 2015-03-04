using System;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows;
using TweetSharp;
using foobar4423.Properties;

namespace foobar4423
{
    public partial class Form1 : Form
    {
        private TwitterService service; 

        private string ScreenName
        {
            get
            {
                string screenName = Settings.Default.ScreenName;
                return (screenName == string.Empty) ? Resources.Form_InitialMessage : "@" + screenName;
            }
        }


        public Form1()
        {
            InitializeComponent();

            try {
                service = new TwitterService(Resources.CK, Resources.CS);
                service.AuthenticateWith(Settings.Default.AT, Settings.Default.ATS);
            }
            catch (Exception) {
                SyncInvoke(() => toolStripStatusLabel_status.Text = "Token cannot be recognized.");
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
            string tweetStr = textBox_info.Text;
            if (tweetStr == string.Empty)
            {
                SyncInvoke(() => 
                    toolStripStatusLabel_status.Text = "The number of characters is 0.");
                return;
            }

            TwitterStatus status = new TwitterStatus();
            try
            {
                status = service.SendTweet(new SendTweetOptions { Status = tweetStr });
            }
            catch (Exception ex)
            {
                SyncInvoke(() => toolStripStatusLabel_status.Text = ex.Message);
            }

            if (status != null)
            {
                SyncInvoke(() =>
                    toolStripStatusLabel_status.Text = "Post succeeded.");
                ShowBalloonTip(ToolTipIcon.Info, "Post succeeded.", this.textBox_info.Text);
            }
            else
            {
                SyncInvoke(() =>
                    toolStripStatusLabel_status.Text = "Post failed.");
                ShowBalloonTip(ToolTipIcon.Error, "Post failed.", this.textBox_info.Text);
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
        private void GetNowPlaying()
        {
            //ウィンドウタイトル取得
            string windowTitle;
            try
            {
                windowTitle = Utility.FoobarWindowTitle(Settings.Default.FoobarFilePath);
            }
            catch (Exception ex)
            {
                SyncInvoke(() => toolStripStatusLabel_status.Text = ex.Message);
                return;
            }
            if (windowTitle == null)
            {
                SyncInvoke(() => 
                    toolStripStatusLabel_status.Text = "foobar2000 cannot be detected.");
                return;
            }

            //なうぷれ生成
            string nowPlayingText = "";
            try
            {
                NowPlaying np = new NowPlaying(windowTitle);
                nowPlayingText = np.Format(Settings.Default.NowPlayingFormat);
            }
            catch (ArgumentException)
            {
                SyncInvoke(() => textBox_info.Text = "");
                SyncInvoke(() =>
                    toolStripStatusLabel_status.Text = "NowPlaying cannot be generated.");
                return;
            }
            SyncInvoke(() =>
                    textBox_info.Text = Utility.RemoveContinuousWhiteSpace(nowPlayingText));
            SyncInvoke(() =>
                toolStripStatusLabel_status.Text = "Successful to generate NowPlaying.");
            SyncInvoke(() => Application.DoEvents());
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
            toolStripStatusLabel_count.Text = textBox_info.Text.Length.ToString().PadLeft(3);
        }


        #region "  AutoPost  "

        private bool isAutoPostFirstTime = false;
        private void checkBox_autoPost_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_autoPost.Checked)
            {
                isAutoPostFirstTime = true;
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
        }


        private string preWindowTitle;
        /// <summary>
        /// 指定された時間が経過後に実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            string windowTitle;
            try {
                windowTitle = Utility.FoobarWindowTitle(Settings.Default.FoobarFilePath); ;
            }
            catch (Exception ex)
            {
                SyncInvoke(() => toolStripStatusLabel_status.Text = ex.Message);
                return;
            }

            //AutoPostチェック初回は回避
            if (isAutoPostFirstTime)
            {
                preWindowTitle = windowTitle;
                isAutoPostFirstTime = false;
                timer1.Start();
                return;
            }

            //前回と同じ||タイトルを取得できない なら何もしない
            if (preWindowTitle == windowTitle || String.IsNullOrEmpty(windowTitle))
            {
                timer1.Start();
                return;
            }
            preWindowTitle = windowTitle;

            //投稿
            GetNowPlaying();
            PostNowPlaying();
            
            timer1.Start();
        }

        #endregion

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
       
        private async void ShowBalloonTip(ToolTipIcon icon, string title, string text, int time = 1000)
        {
            if (!Settings.Default.IsBalloon) return;
            
            this.notifyIcon1.BalloonTipIcon = icon;
            this.notifyIcon1.BalloonTipTitle = title;
            this.notifyIcon1.BalloonTipText = text;
            await Task.Run(() =>
                SyncInvoke(() =>
                {
                    this.notifyIcon1.ShowBalloonTip(time);
                    Application.DoEvents();
                }
            ));
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
