using System;
using System.Diagnostics;
using System.Windows.Forms;
using CoreTweet;
using System.Threading.Tasks;
using foobar4423.Properties;

namespace foobar4423
{
    public partial class Form_OAuth : Form
    {
        private OAuth.OAuthSession session;
                

        public Form_OAuth()
        {
            InitializeComponent();
        }


        private async void button_OAuth_Click(object sender, EventArgs e)
        {
            button_OAuth.Enabled = false;

            try
            {
                session = await OAuth.AuthorizeAsync(Resources.CK, Resources.CS);
                Process.Start(session.AuthorizeUri.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("リクエストトークンが生成できませんでした。\nネットワーク接続を確認して下さい。",
                        "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            button_OAuth.Enabled = true;
        }

        private async void button_token_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_pin.Text))
            {
                MessageBox.Show("PINを入力して下さい。", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var token = await OAuth.GetTokensAsync(session, textBox_pin.Text);
            SaveToken(token);

            MessageBox.Show("Welcom @" + token.ScreenName, "Hello", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Close();
        }

        private void SaveToken(Tokens token)
        {
            Settings.Default.AT = token.AccessToken;
            Settings.Default.ATS = token.AccessTokenSecret;
            Settings.Default.ScreenName = token.ScreenName;

            Settings.Default.Save();
        }
    }
}
