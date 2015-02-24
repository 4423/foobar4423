using System;
using System.Threading.Tasks;
using System.Threading;
using System.Threading;
using System.Windows.Forms;
using System.Windows;
using TweetSharp;

namespace foobar4423
{
    public partial class Form1 : Form
    {

        private TwitterService service;

        private string consumerKey;
        private string consumerSecret;
        private string accessToken;
        private string accessTokenSecret;
        private string screenName;
        private string filePath;
        private string nowPlayingFormat;
        private bool isBalloon;

        ProcessInformation pi;
        NowPlayingFormat npf;


        public Form1()
        {
            InitializeComponent();

            consumerKey = "fhtoSvAE9Uaxmnv51Nbalg";
            consumerSecret = "lbHj8x8ghEtmnv6TZarKIKUseWrFxLzesSl8XkapkX4";
            service = new TwitterService(consumerKey, consumerSecret);

            LoadConfig();
            
            pi = new ProcessInformation();
            npf = new NowPlayingFormat();
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


/*******設定*******/
        internal bool LoadConfig()
        {
            accessToken = foobar4423.Properties.Settings.Default.accessToken;
            accessTokenSecret = foobar4423.Properties.Settings.Default.accessTokenSecret;
            screenName = foobar4423.Properties.Settings.Default.screenName;
            filePath = foobar4423.Properties.Settings.Default.exeFilePath;
            nowPlayingFormat = Properties.Settings.Default.nowPlayingFormat;
            isBalloon = Properties.Settings.Default.balloon;

            service.AuthenticateWith(accessToken, accessTokenSecret);

            if(screenName != string.Empty)
                toolStripTextBox_screenName.Text = "@" + screenName;

            return false;
        }

       
/*******メニュー*******/

        private void toolStripMenuItem_Config_Click(object sender, EventArgs e)
        {
            Form_Config fc = new Form_Config();

            fc.ShowDialog();
            LoadConfig();
        }

        private void ToolStripMenuItem_OAuth_Click(object sender, EventArgs e)
        {
            Form_OAuth fo = new Form_OAuth();

            fo.ShowDialog();
            LoadConfig();
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
            if (LoadConfig())
            {
                SyncInvoke(() =>
                    toolStripStatusLabel_status.Text = "cannot recognize the token");
                return;
            }

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
        /// ウィンドウタイトルを取得させる
        /// </summary>
        /// <returns>正常に取得出来なかったらempty</returns>
        private string GetWindowTitle()
        {
            string windowTitle = "";

            try
            {
                windowTitle = pi.GetFoobarTitle(filePath);
            }
            catch (Exception ex)
            {
                SyncInvoke(() => toolStripStatusLabel_status.Text = ex.Message);
                return string.Empty;
            }

            return windowTitle;
        }


        /// <summary>
        /// なうぷれを取得
        /// </summary>
        private void GetNowPlaying()
        {
            string textBoxText = "";
            string windowTitle = GetWindowTitle();

            if (windowTitle == string.Empty) return;

            if (windowTitle == "not found")
            {
                SyncInvoke(() => 
                    toolStripStatusLabel_status.Text = "cannot detect foobar2000");
                return;
            }


            textBoxText = npf.GenerateTweetString(windowTitle, nowPlayingFormat);

            if (textBoxText == "foobar2000")
            {
                SyncInvoke(() => 
                    toolStripStatusLabel_status.Text = "cannot generate NowPlaying");
                return;
            }

            //連続した空白除去
            int whitePos = 0;
            while ((whitePos = textBoxText.IndexOf("  ")) != -1)
                textBoxText = textBoxText.Replace("  ", " ");


            SyncInvoke(() => textBox_info.Text = textBoxText);
            SyncInvoke(() => 
                toolStripStatusLabel_status.Text = "successful to generate NowPlaying");
        }


        /// <summary>
        /// async/awaitを使用
        /// <seealso cref="http://xin9le.net/articles/70"/>
        /// </summary>
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


/*******自動投稿*******/

        bool firstCheckBoxFlag = false;
        string preWindowTitle;


        private void checkBox_autoPost_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_autoPost.Checked == true)
            {
                firstCheckBoxFlag = true;
                timer1.Enabled = true;
            }
            else timer1.Enabled = false;
        }
        

        /// <summary>
        /// 指定された時間が経過後に実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            string windowTitle = GetWindowTitle();
            

            //AutoPostチェック初回は回避
            if (firstCheckBoxFlag == true)
            {
                preWindowTitle = windowTitle;
                firstCheckBoxFlag = false;
                timer1.Start();
                return;
            }

            //前回と同じ||タイトルを取得できない なら何もしない
            if (preWindowTitle == windowTitle || windowTitle == string.Empty
                || windowTitle == "not found")
            {
                timer1.Start();
                return;
            }

            preWindowTitle = windowTitle;

            //前回と変化があるなら なうぷれ取得
            GetNowPlaying();

            //ツイット
            PostNowPlaying();
            
            timer1.Start();
        }


/*******通知領域*******/

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


        //最小化時にtoolStripのテキストを...
        private void toolStripStatusLabel_status_TextChanged(object sender, EventArgs e)
        {
            //本当にやる？
            if (isBalloon == true)
            {
                if (toolStripStatusLabel_status.Text == "Post successful")
                {
                    this.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    this.notifyIcon1.BalloonTipTitle = "Post NowPlaying!";
                    this.notifyIcon1.BalloonTipText = this.textBox_info.Text;
                    this.notifyIcon1.ShowBalloonTip(1000);
                }

                if (toolStripStatusLabel_status.Text == "Post failed")
                {
                    this.notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                    this.notifyIcon1.BalloonTipTitle = "Post error!";
                    this.notifyIcon1.BalloonTipText = this.textBox_info.Text;
                    this.notifyIcon1.ShowBalloonTip(1000);
                }
            }
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

    }
}
