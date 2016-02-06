namespace foobar4423
{
    partial class Form_Help
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(289, 108);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "※フォーマットタグについて\r\n\r\n$SONG$\t\t:再生中の曲名\r\n$ARTIST$\t\t:再生中のアーティスト名\r\n$ALBUM$\t\t:再生中のアルバム名\r\n$A" +
    "LBUM_ARTIST$\t:再生中のアルバムアーティスト名\r\n$TRACK_NUMBER$\t:再生中のトラックナンバー";
            // 
            // Form_Help
            // 
            this.ClientSize = new System.Drawing.Size(289, 108);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Form_Help";
            this.Text = "Help";
            this.Load += new System.EventHandler(this.Form_Help_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
    }
}