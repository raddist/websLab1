﻿namespace websLab1
{
    partial class chat_form
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
            this.userList_panel = new System.Windows.Forms.Panel();
            this.output_textBox = new System.Windows.Forms.RichTextBox();
            this.disconnect_btn = new System.Windows.Forms.Button();
            this.title_login_lbl = new System.Windows.Forms.Label();
            this.login_lbl = new System.Windows.Forms.Label();
            this.title_ip_lbl = new System.Windows.Forms.Label();
            this.ip_lbl = new System.Windows.Forms.Label();
            this.title_port_lbl = new System.Windows.Forms.Label();
            this.port_lbl = new System.Windows.Forms.Label();
            this.sent_btn = new System.Windows.Forms.Button();
            this.input_TextBox = new System.Windows.Forms.RichTextBox();
            this.title_private = new System.Windows.Forms.Label();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userList_panel
            // 
            this.userList_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userList_panel.Location = new System.Drawing.Point(0, 43);
            this.userList_panel.Name = "userList_panel";
            this.userList_panel.Size = new System.Drawing.Size(187, 481);
            this.userList_panel.TabIndex = 0;
            // 
            // output_textBox
            // 
            this.output_textBox.Location = new System.Drawing.Point(194, 43);
            this.output_textBox.Name = "output_textBox";
            this.output_textBox.Size = new System.Drawing.Size(600, 348);
            this.output_textBox.TabIndex = 1;
            this.output_textBox.Text = "";
            // 
            // disconnect_btn
            // 
            this.disconnect_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.disconnect_btn.Location = new System.Drawing.Point(13, 13);
            this.disconnect_btn.Name = "disconnect_btn";
            this.disconnect_btn.Size = new System.Drawing.Size(129, 24);
            this.disconnect_btn.TabIndex = 2;
            this.disconnect_btn.Text = "Disconnect";
            this.disconnect_btn.UseVisualStyleBackColor = true;
            this.disconnect_btn.Click += new System.EventHandler(this.disconnect_btn_Click);
            // 
            // title_login_lbl
            // 
            this.title_login_lbl.AutoSize = true;
            this.title_login_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.title_login_lbl.Location = new System.Drawing.Point(194, 13);
            this.title_login_lbl.Name = "title_login_lbl";
            this.title_login_lbl.Size = new System.Drawing.Size(47, 17);
            this.title_login_lbl.TabIndex = 3;
            this.title_login_lbl.Text = "Login:";
            // 
            // login_lbl
            // 
            this.login_lbl.AutoSize = true;
            this.login_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login_lbl.Location = new System.Drawing.Point(260, 13);
            this.login_lbl.Name = "login_lbl";
            this.login_lbl.Size = new System.Drawing.Size(51, 17);
            this.login_lbl.TabIndex = 4;
            this.login_lbl.Text = "default";
            // 
            // title_ip_lbl
            // 
            this.title_ip_lbl.AutoSize = true;
            this.title_ip_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.title_ip_lbl.Location = new System.Drawing.Point(315, 13);
            this.title_ip_lbl.Name = "title_ip_lbl";
            this.title_ip_lbl.Size = new System.Drawing.Size(24, 17);
            this.title_ip_lbl.TabIndex = 5;
            this.title_ip_lbl.Text = "IP:";
            // 
            // ip_lbl
            // 
            this.ip_lbl.AutoSize = true;
            this.ip_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ip_lbl.Location = new System.Drawing.Point(383, 13);
            this.ip_lbl.Name = "ip_lbl";
            this.ip_lbl.Size = new System.Drawing.Size(51, 17);
            this.ip_lbl.TabIndex = 6;
            this.ip_lbl.Text = "default";
            // 
            // title_port_lbl
            // 
            this.title_port_lbl.AutoSize = true;
            this.title_port_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.title_port_lbl.Location = new System.Drawing.Point(444, 13);
            this.title_port_lbl.Name = "title_port_lbl";
            this.title_port_lbl.Size = new System.Drawing.Size(37, 17);
            this.title_port_lbl.TabIndex = 7;
            this.title_port_lbl.Text = "port:";
            // 
            // port_lbl
            // 
            this.port_lbl.AutoSize = true;
            this.port_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.port_lbl.Location = new System.Drawing.Point(524, 13);
            this.port_lbl.Name = "port_lbl";
            this.port_lbl.Size = new System.Drawing.Size(51, 17);
            this.port_lbl.TabIndex = 8;
            this.port_lbl.Text = "default";
            // 
            // sent_btn
            // 
            this.sent_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sent_btn.Location = new System.Drawing.Point(693, 428);
            this.sent_btn.Name = "sent_btn";
            this.sent_btn.Size = new System.Drawing.Size(75, 68);
            this.sent_btn.TabIndex = 9;
            this.sent_btn.Text = "Sent";
            this.sent_btn.UseVisualStyleBackColor = true;
            this.sent_btn.Click += new System.EventHandler(this.sent_btn_Click);
            // 
            // input_TextBox
            // 
            this.input_TextBox.Location = new System.Drawing.Point(194, 417);
            this.input_TextBox.Name = "input_TextBox";
            this.input_TextBox.Size = new System.Drawing.Size(471, 96);
            this.input_TextBox.TabIndex = 10;
            this.input_TextBox.Text = "";
            // 
            // title_private
            // 
            this.title_private.AutoSize = true;
            this.title_private.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.title_private.Location = new System.Drawing.Point(194, 394);
            this.title_private.Name = "title_private";
            this.title_private.Size = new System.Drawing.Size(51, 17);
            this.title_private.TabIndex = 11;
            this.title_private.Text = "default";
            this.title_private.Visible = false;
            // 
            // cancel_btn
            // 
            this.cancel_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancel_btn.Location = new System.Drawing.Point(421, 391);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.cancel_btn.TabIndex = 12;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Visible = false;
            // 
            // chat_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 525);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.title_private);
            this.Controls.Add(this.input_TextBox);
            this.Controls.Add(this.sent_btn);
            this.Controls.Add(this.port_lbl);
            this.Controls.Add(this.title_port_lbl);
            this.Controls.Add(this.ip_lbl);
            this.Controls.Add(this.title_ip_lbl);
            this.Controls.Add(this.login_lbl);
            this.Controls.Add(this.title_login_lbl);
            this.Controls.Add(this.disconnect_btn);
            this.Controls.Add(this.output_textBox);
            this.Controls.Add(this.userList_panel);
            this.Name = "chat_form";
            this.Text = "chat_form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.chat_form_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.chat_form_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel userList_panel;
        private System.Windows.Forms.RichTextBox output_textBox;
        private System.Windows.Forms.Button disconnect_btn;
        private System.Windows.Forms.Label title_login_lbl;
        private System.Windows.Forms.Label login_lbl;
        private System.Windows.Forms.Label title_ip_lbl;
        private System.Windows.Forms.Label ip_lbl;
        private System.Windows.Forms.Label title_port_lbl;
        private System.Windows.Forms.Label port_lbl;
        private System.Windows.Forms.Button sent_btn;
        private System.Windows.Forms.RichTextBox input_TextBox;
        private System.Windows.Forms.Label title_private;
        private System.Windows.Forms.Button cancel_btn;
    }
}