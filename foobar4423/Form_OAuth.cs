using System;
using System.Diagnostics;
using System.Windows.Forms;
using TweetSharp;
using System.Threading.Tasks;
using foobar4423.Properties;

namespace foobar4423
{
    public partial class Form_OAuth : Form
    {
        private TwitterService service;
        private OAuthRequestToken requestToken;
                

        public Form_OAuth()
        {
            InitializeComponent();
        }


        private async void button_OAuth_Click(object sender, EventArgs e)
        {
            button_OAuth.Enabled = false;

            await Task.Run(() =>
            {
                service = new TwitterService(Resources.CK, Resources.CS);
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

            OAuthAccessToken access = service.GetAccessToken(requestToken, textBox_pin.Text);
            service.AuthenticateWith(access.Token, access.TokenSecret);
            SaveToken(access);

            MessageBox.Show("Welcom @" + access.ScreenName, "Hello", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Close();
        }

        private void SaveToken(OAuthAccessToken access)
        {
            Settings.Default.AT = access.Token;
            Settings.Default.ATS = access.TokenSecret;
            Settings.Default.ScreenName = access.ScreenName;

            Settings.Default.Save();
        }
    }
}
