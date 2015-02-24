namespace foobar4423
{
    partial class Form_Config
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_filePath = new System.Windows.Forms.Button();
            this.textBox_filePath = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_balloon = new System.Windows.Forms.CheckBox();
            this.button_reset = new System.Windows.Forms.Button();
            this.button_help = new System.Windows.Forms.Button();
            this.textBox_format = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_save = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button_filePath);
            this.groupBox1.Controls.Add(this.textBox_filePath);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "foobar2000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "foobar2000.exe の場所";
            // 
            // button_filePath
            // 
            this.button_filePath.Location = new System.Drawing.Point(262, 38);
            this.button_filePath.Name = "button_filePath";
            this.button_filePath.Size = new System.Drawing.Size(23, 23);
            this.button_filePath.TabIndex = 1;
            this.button_filePath.Text = "...";
            this.button_filePath.UseVisualStyleBackColor = true;
            this.button_filePath.Click += new System.EventHandler(this.button_filePath_Click);
            // 
            // textBox_filePath
            // 
            this.textBox_filePath.Location = new System.Drawing.Point(8, 40);
            this.textBox_filePath.Name = "textBox_filePath";
            this.textBox_filePath.Size = new System.Drawing.Size(248, 19);
            this.textBox_filePath.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox_balloon);
            this.groupBox2.Controls.Add(this.button_reset);
            this.groupBox2.Controls.Add(this.button_help);
            this.groupBox2.Controls.Add(this.textBox_format);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 130);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "foobar4423";
            // 
            // checkBox_balloon
            // 
            this.checkBox_balloon.AutoSize = true;
            this.checkBox_balloon.Location = new System.Drawing.Point(9, 100);
            this.checkBox_balloon.Name = "checkBox_balloon";
            this.checkBox_balloon.Size = new System.Drawing.Size(148, 16);
            this.checkBox_balloon.TabIndex = 4;
            this.checkBox_balloon.Text = "バルーン通知を有効にする";
            this.checkBox_balloon.UseVisualStyleBackColor = true;
            // 
            // button_reset
            // 
            this.button_reset.Location = new System.Drawing.Point(245, 18);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(40, 19);
            this.button_reset.TabIndex = 3;
            this.button_reset.Text = "reset";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // button_help
            // 
            this.button_help.Location = new System.Drawing.Point(199, 18);
            this.button_help.Name = "button_help";
            this.button_help.Size = new System.Drawing.Size(40, 19);
            this.button_help.TabIndex = 2;
            this.button_help.Text = "help";
            this.button_help.UseVisualStyleBackColor = true;
            this.button_help.Click += new System.EventHandler(this.button_help_Click);
            // 
            // textBox_format
            // 
            this.textBox_format.Location = new System.Drawing.Point(8, 40);
            this.textBox_format.Multiline = true;
            this.textBox_format.Name = "textBox_format";
            this.textBox_format.Size = new System.Drawing.Size(277, 50);
            this.textBox_format.TabIndex = 1;
            this.textBox_format.Text = "$SONG$ - $ARTIST$ via $ALBUM$ - $ALBUM_ARTIST$ / $TRACK_NUMBER$ #nowplaying";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "NowPlayingの投稿フォーマット";
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(141, 238);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 2;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(222, 238);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 3;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form_Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 273);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_Config";
            this.Text = "Config";
            this.Load += new System.EventHandler(this.Form_Config_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_filePath;
        private System.Windows.Forms.TextBox textBox_filePath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_format;
        private System.Windows.Forms.Button button_help;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.CheckBox checkBox_balloon;
    }
}