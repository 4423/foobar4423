﻿namespace foobar4423
{
    partial class Form_OAuth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_OAuth));
            this.button_OAuth = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_pin = new System.Windows.Forms.TextBox();
            this.button_token = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_OAuth
            // 
            this.button_OAuth.Location = new System.Drawing.Point(24, 25);
            this.button_OAuth.Margin = new System.Windows.Forms.Padding(6);
            this.button_OAuth.Name = "button_OAuth";
            this.button_OAuth.Size = new System.Drawing.Size(308, 48);
            this.button_OAuth.TabIndex = 0;
            this.button_OAuth.Text = "認証(ブラウザへ飛びます)";
            this.button_OAuth.UseVisualStyleBackColor = true;
            this.button_OAuth.Click += new System.EventHandler(this.button_OAuth_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 104);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "PINコード";
            // 
            // textBox_pin
            // 
            this.textBox_pin.Location = new System.Drawing.Point(132, 98);
            this.textBox_pin.Margin = new System.Windows.Forms.Padding(6);
            this.textBox_pin.Name = "textBox_pin";
            this.textBox_pin.Size = new System.Drawing.Size(196, 31);
            this.textBox_pin.TabIndex = 2;
            // 
            // button_token
            // 
            this.button_token.Location = new System.Drawing.Point(132, 162);
            this.button_token.Margin = new System.Windows.Forms.Padding(6);
            this.button_token.Name = "button_token";
            this.button_token.Size = new System.Drawing.Size(200, 48);
            this.button_token.TabIndex = 3;
            this.button_token.Text = "トークン取得";
            this.button_token.UseVisualStyleBackColor = true;
            this.button_token.Click += new System.EventHandler(this.button_token_Click);
            // 
            // Form_OAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 237);
            this.Controls.Add(this.button_token);
            this.Controls.Add(this.textBox_pin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_OAuth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "Form_OAuth";
            this.Text = "OAuth";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_OAuth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_pin;
        private System.Windows.Forms.Button button_token;
    }
}