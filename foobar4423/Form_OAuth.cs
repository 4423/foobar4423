using System;
using System.Diagnostics;
using System.Windows.Forms;
using TweetSharp;
using System.Threading.Tasks;

namespace foobar4423
{
    public partial class Form_OAuth : Form
    {
        private string consumerKey;
        private string consumerSecret;
        private string accessToken;
        private string accessTokenSecret;

        private string screenName;

        private TwitterService service;
        private OAuthRequestToken requestToken;
        

        public Form_OAuth()
        {
            InitializeComponent();

            consumerKey = Properties.Resources.CK;
            consumerSecret = Properties.Resources.CS;
        }

        private async void button_OAuth_Click(object sender, EventArgs e)
        {
            button_OAuth.Enabled = false;

            await Task.Run(() =>
            {
                service = new TwitterService(consumerKey, consumerSecret);
                requestToken = service.GetRequestToken();
                Uri uri = service.GetAuthorizationUri(requestToken);
                Process.Start(uri.ToString());
            });

            button_OAuth.Enabled = true;
        }

        private void button_token_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_pin.Text))
            {
                MessageBox.Show("PINを入力して下さい。", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string pin = textBox_pin.Text;

            OAuthAccessToken access = service.GetAccessToken(requestToken, pin);
            accessToken = access.Token;
            accessTokenSecret = access.TokenSecret;
            screenName = access.ScreenName;
            service.AuthenticateWith(accessToken, accessTokenSecret);
            SaveToken();

            MessageBox.Show("Welcom @" + screenName, "Hello", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Close();
        }

        /// <summary>
        /// 大事な情報を保存
        /// </summary>
        internal void SaveToken()
        {
            foobar4423.Properties.Settings.Default.accessToken = accessToken;
            foobar4423.Properties.Settings.Default.accessTokenSecret = accessTokenSecret;
            foobar4423.Properties.Settings.Default.screenName = screenName;

            Properties.Settings.Default.Save();
        }
    }
}
