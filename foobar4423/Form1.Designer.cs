﻿namespace foobar4423
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox_info = new System.Windows.Forms.TextBox();
            this.button_post = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sDKじゃあのToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_OAuth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Config = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox_screenName = new System.Windows.Forms.ToolStripTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_count = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBox_autoPost = new System.Windows.Forms.CheckBox();
            this.button_getNowPlaying = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postNowPlayingPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_info
            // 
            this.textBox_info.Location = new System.Drawing.Point(12, 38);
            this.textBox_info.Multiline = true;
            this.textBox_info.Name = "textBox_info";
            this.textBox_info.Size = new System.Drawing.Size(412, 64);
            this.textBox_info.TabIndex = 0;
            this.textBox_info.TextChanged += new System.EventHandler(this.textBox_info_TextChanged);
            // 
            // button_post
            // 
            this.button_post.Location = new System.Drawing.Point(349, 108);
            this.button_post.Name = "button_post";
            this.button_post.Size = new System.Drawing.Size(75, 23);
            this.button_post.TabIndex = 1;
            this.button_post.Text = "Post";
            this.button_post.UseVisualStyleBackColor = true;
            this.button_post.Click += new System.EventHandler(this.button_post_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sDKじゃあのToolStripMenuItem,
            this.toolStripTextBox_screenName});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(436, 26);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sDKじゃあのToolStripMenuItem
            // 
            this.sDKじゃあのToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_OAuth,
            this.toolStripSeparator1,
            this.toolStripMenuItem_Config,
            this.toolStripSeparator2,
            this.ToolStripMenuItem_Exit});
            this.sDKじゃあのToolStripMenuItem.Name = "sDKじゃあのToolStripMenuItem";
            this.sDKじゃあのToolStripMenuItem.Size = new System.Drawing.Size(57, 22);
            this.sDKじゃあのToolStripMenuItem.Text = "File(&F)";
            // 
            // ToolStripMenuItem_OAuth
            // 
            this.ToolStripMenuItem_OAuth.Name = "ToolStripMenuItem_OAuth";
            this.ToolStripMenuItem_OAuth.Size = new System.Drawing.Size(131, 22);
            this.ToolStripMenuItem_OAuth.Text = "OAuth(&O)";
            this.ToolStripMenuItem_OAuth.Click += new System.EventHandler(this.ToolStripMenuItem_OAuth_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(128, 6);
            // 
            // toolStripMenuItem_Config
            // 
            this.toolStripMenuItem_Config.Name = "toolStripMenuItem_Config";
            this.toolStripMenuItem_Config.Size = new System.Drawing.Size(131, 22);
            this.toolStripMenuItem_Config.Text = "Config(&C)";
            this.toolStripMenuItem_Config.Click += new System.EventHandler(this.toolStripMenuItem_Config_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(128, 6);
            // 
            // ToolStripMenuItem_Exit
            // 
            this.ToolStripMenuItem_Exit.Name = "ToolStripMenuItem_Exit";
            this.ToolStripMenuItem_Exit.Size = new System.Drawing.Size(131, 22);
            this.ToolStripMenuItem_Exit.Text = "Exit(&E)";
            this.ToolStripMenuItem_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
            // 
            // toolStripTextBox_screenName
            // 
            this.toolStripTextBox_screenName.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_screenName.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStripTextBox_screenName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.toolStripTextBox_screenName.Margin = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.toolStripTextBox_screenName.Name = "toolStripTextBox_screenName";
            this.toolStripTextBox_screenName.ReadOnly = true;
            this.toolStripTextBox_screenName.Size = new System.Drawing.Size(250, 22);
            this.toolStripTextBox_screenName.Text = "Please OAuth authentication and config!";
            this.toolStripTextBox_screenName.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel_status,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel_count});
            this.statusStrip1.Location = new System.Drawing.Point(0, 140);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(436, 23);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(51, 18);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // toolStripStatusLabel_status
            // 
            this.toolStripStatusLabel_status.Name = "toolStripStatusLabel_status";
            this.toolStripStatusLabel_status.Size = new System.Drawing.Size(300, 18);
            this.toolStripStatusLabel_status.Spring = true;
            this.toolStripStatusLabel_status.Text = "　　　";
            this.toolStripStatusLabel_status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel_status.TextChanged += new System.EventHandler(this.toolStripStatusLabel_status_TextChanged);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(47, 18);
            this.toolStripStatusLabel2.Text = "Count:";
            // 
            // toolStripStatusLabel_count
            // 
            this.toolStripStatusLabel_count.Name = "toolStripStatusLabel_count";
            this.toolStripStatusLabel_count.Size = new System.Drawing.Size(23, 18);
            this.toolStripStatusLabel_count.Text = "  0";
            this.toolStripStatusLabel_count.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox_autoPost
            // 
            this.checkBox_autoPost.AutoSize = true;
            this.checkBox_autoPost.Location = new System.Drawing.Point(12, 112);
            this.checkBox_autoPost.Name = "checkBox_autoPost";
            this.checkBox_autoPost.Size = new System.Drawing.Size(71, 16);
            this.checkBox_autoPost.TabIndex = 4;
            this.checkBox_autoPost.Text = "AutoPost";
            this.checkBox_autoPost.UseVisualStyleBackColor = true;
            this.checkBox_autoPost.CheckedChanged += new System.EventHandler(this.checkBox_autoPost_CheckedChanged);
            // 
            // button_getNowPlaying
            // 
            this.button_getNowPlaying.Location = new System.Drawing.Point(245, 108);
            this.button_getNowPlaying.Name = "button_getNowPlaying";
            this.button_getNowPlaying.Size = new System.Drawing.Size(98, 23);
            this.button_getNowPlaying.TabIndex = 5;
            this.button_getNowPlaying.Text = "Get NowPlaying";
            this.button_getNowPlaying.UseVisualStyleBackColor = true;
            this.button_getNowPlaying.Click += new System.EventHandler(this.button_getNowPlaying_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "foobar4423";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showSToolStripMenuItem,
            this.postNowPlayingPToolStripMenuItem,
            this.exitEToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 70);
            // 
            // showSToolStripMenuItem
            // 
            this.showSToolStripMenuItem.Name = "showSToolStripMenuItem";
            this.showSToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.showSToolStripMenuItem.Text = "Show(&S)";
            this.showSToolStripMenuItem.Click += new System.EventHandler(this.showSToolStripMenuItem_Click);
            // 
            // postNowPlayingPToolStripMenuItem
            // 
            this.postNowPlayingPToolStripMenuItem.Name = "postNowPlayingPToolStripMenuItem";
            this.postNowPlayingPToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.postNowPlayingPToolStripMenuItem.Text = "Post NowPlaying(&P)";
            this.postNowPlayingPToolStripMenuItem.Click += new System.EventHandler(this.postNowPlayingPToolStripMenuItem_Click);
            // 
            // exitEToolStripMenuItem
            // 
            this.exitEToolStripMenuItem.Name = "exitEToolStripMenuItem";
            this.exitEToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exitEToolStripMenuItem.Text = "Close(&C)";
            this.exitEToolStripMenuItem.Click += new System.EventHandler(this.exitEToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 163);
            this.Controls.Add(this.button_getNowPlaying);
            this.Controls.Add(this.checkBox_autoPost);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_post);
            this.Controls.Add(this.textBox_info);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "foobar4423";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_info;
        private System.Windows.Forms.Button button_post;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sDKじゃあのToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_OAuth;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Config;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Exit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_status;
        private System.Windows.Forms.CheckBox checkBox_autoPost;
        private System.Windows.Forms.Button button_getNowPlaying;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_count;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_screenName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem postNowPlayingPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitEToolStripMenuItem;
    }
}

