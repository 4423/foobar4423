using foobar4423.Properties;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace foobar4423
{
    public partial class Form_Config : Form
    {
        public Form_Config()
        {
            InitializeComponent();
        }

        private void Form_Config_Load(object sender, EventArgs e)
        {
            LoadConfig();
            textBox_format.SelectionStart = 0;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            SaveConfig();
            this.Close();
        }

        /// <summary>
        /// 保存
        /// </summary>
        internal void SaveConfig()
        {
            Settings.Default.NowPlayingFormat = textBox_format.Text;
            Settings.Default.ShowBalloon = checkBox_balloon.Checked;

            Settings.Default.Save();
        }
        
        /// <summary>
        /// 読み込み
        /// </summary>
        private void LoadConfig()
        {
            textBox_format.Text = Settings.Default.NowPlayingFormat;
            checkBox_balloon.Checked = Settings.Default.ShowBalloon;
        }

        private void OnResetButtonClicked(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Reset format to default settings?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                textBox_format.Text = Resources.DefaultFormat;
            }
        }

        private void OnHelpLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = MessageBox.Show("Open a web page.", "Confirmation",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Process.Start(Resources.HelpUrl);
            }
        }
    }
}
