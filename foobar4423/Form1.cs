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
                return (screenName == string.Empty) ? screenName : "@" + screenName;
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
                SyncInvoke(() => toolStripStatusLabel_status.Text = "Cannot recognize the token");
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
            string tweetStr = textBox_info.Text;
            if (tweetStr == string.Empty)
            {
                SyncInvoke(() => 
                    toolStripStatusLabel_status.Text = "Post number of characters is 0");
                return;
            }


            try
            {
                var isSuccess = service.SendTweet(new SendTweetOptions { Status = tweetStr });

                if (isSuccess != null)
                    SyncInvoke(() =>
                        toolStripStatusLabel_status.Text = "Post successful");
                else
                    SyncInvoke(() =>
                        toolStripStatusLabel_status.Text = "Post failed");
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
                    toolStripStatusLabel_status.Text = "Cannot detect foobar2000");
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
                SyncInvoke(() =>
                    toolStripStatusLabel_status.Text = "Cannot generate NowPlaying");
                return;
            }
            SyncInvoke(() =>
                    textBox_info.Text = Utility.RemoveContinuousWhiteSpace(nowPlayingText));
            SyncInvoke(() =>
                toolStripStatusLabel_status.Text = "Successful to generate NowPlaying");
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

            //TODO 例外処理 もともとgetwindowtitle()
            string windowTitle = Utility.FoobarWindowTitle(Settings.Default.FoobarFilePath); ;
            
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
       
        private void toolStripStatusLabel_status_TextChanged(object sender, EventArgs e)
        {
            if (!Settings.Default.IsBalloon) return;

            switch (toolStripStatusLabel_status.Text)
            {
                case "Post successful":
                    ShowBalloonTip(ToolTipIcon.Info, "Post NowPlaying!", this.textBox_info.Text);
                    break;
                case "Post failed":
                    ShowBalloonTip(ToolTipIcon.Error, "Post error!", this.textBox_info.Text);
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
