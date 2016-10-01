namespace websLab1
{
    partial class SignIN_form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ip_lbl = new System.Windows.Forms.Label();
            this.port_lbl = new System.Windows.Forms.Label();
            this.ip_textBox = new System.Windows.Forms.TextBox();
            this.port_textBox = new System.Windows.Forms.TextBox();
            this.title_lbl = new System.Windows.Forms.Label();
            this.login_lbl = new System.Windows.Forms.Label();
            this.login_textBox = new System.Windows.Forms.TextBox();
            this.connect_btn = new System.Windows.Forms.Button();
            this.error_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ip_lbl
            // 
            this.ip_lbl.AutoSize = true;
            this.ip_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ip_lbl.Location = new System.Drawing.Point(16, 140);
            this.ip_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ip_lbl.Name = "ip_lbl";
            this.ip_lbl.Size = new System.Drawing.Size(36, 25);
            this.ip_lbl.TabIndex = 0;
            this.ip_lbl.Text = "IP:";
            // 
            // port_lbl
            // 
            this.port_lbl.AutoSize = true;
            this.port_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.port_lbl.Location = new System.Drawing.Point(323, 143);
            this.port_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.port_lbl.Name = "port_lbl";
            this.port_lbl.Size = new System.Drawing.Size(53, 25);
            this.port_lbl.TabIndex = 1;
            this.port_lbl.Text = "Port:";
            // 
            // ip_textBox
            // 
            this.ip_textBox.Location = new System.Drawing.Point(93, 143);
            this.ip_textBox.Margin = new System.Windows.Forms.Padding(4);
            this.ip_textBox.Name = "ip_textBox";
            this.ip_textBox.Size = new System.Drawing.Size(193, 22);
            this.ip_textBox.TabIndex = 2;
            this.ip_textBox.Text = "192.168.137.173";
            // 
            // port_textBox
            // 
            this.port_textBox.Location = new System.Drawing.Point(415, 143);
            this.port_textBox.Margin = new System.Windows.Forms.Padding(4);
            this.port_textBox.Name = "port_textBox";
            this.port_textBox.Size = new System.Drawing.Size(132, 22);
            this.port_textBox.TabIndex = 3;
            // 
            // title_lbl
            // 
            this.title_lbl.AutoSize = true;
            this.title_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.title_lbl.Location = new System.Drawing.Point(16, 73);
            this.title_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.title_lbl.Name = "title_lbl";
            this.title_lbl.Size = new System.Drawing.Size(569, 25);
            this.title_lbl.TabIndex = 4;
            this.title_lbl.Text = "Please enter IP address and port of server and unique username";
            // 
            // login_lbl
            // 
            this.login_lbl.AutoSize = true;
            this.login_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login_lbl.Location = new System.Drawing.Point(16, 197);
            this.login_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.login_lbl.Name = "login_lbl";
            this.login_lbl.Size = new System.Drawing.Size(66, 25);
            this.login_lbl.TabIndex = 5;
            this.login_lbl.Text = "Login:";
            // 
            // login_textBox
            // 
            this.login_textBox.Location = new System.Drawing.Point(93, 197);
            this.login_textBox.Margin = new System.Windows.Forms.Padding(4);
            this.login_textBox.Name = "login_textBox";
            this.login_textBox.Size = new System.Drawing.Size(193, 22);
            this.login_textBox.TabIndex = 6;
            // 
            // connect_btn
            // 
            this.connect_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connect_btn.Location = new System.Drawing.Point(255, 277);
            this.connect_btn.Margin = new System.Windows.Forms.Padding(4);
            this.connect_btn.Name = "connect_btn";
            this.connect_btn.Size = new System.Drawing.Size(143, 49);
            this.connect_btn.TabIndex = 7;
            this.connect_btn.Text = "Connect";
            this.connect_btn.UseVisualStyleBackColor = true;
            this.connect_btn.Click += new System.EventHandler(this.connect_btn_Click);
            // 
            // error_lbl
            // 
            this.error_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.error_lbl.Location = new System.Drawing.Point(21, 347);
            this.error_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.error_lbl.Name = "error_lbl";
            this.error_lbl.Size = new System.Drawing.Size(623, 28);
            this.error_lbl.TabIndex = 8;
            this.error_lbl.Text = "default";
            this.error_lbl.Visible = false;
            // 
            // SignIN_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 385);
            this.Controls.Add(this.error_lbl);
            this.Controls.Add(this.connect_btn);
            this.Controls.Add(this.login_textBox);
            this.Controls.Add(this.login_lbl);
            this.Controls.Add(this.title_lbl);
            this.Controls.Add(this.port_textBox);
            this.Controls.Add(this.ip_textBox);
            this.Controls.Add(this.port_lbl);
            this.Controls.Add(this.ip_lbl);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(661, 432);
            this.MinimumSize = new System.Drawing.Size(661, 432);
            this.Name = "SignIN_form";
            this.Text = "Sign in";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ip_lbl;
        private System.Windows.Forms.Label port_lbl;
        private System.Windows.Forms.TextBox ip_textBox;
        private System.Windows.Forms.TextBox port_textBox;
        private System.Windows.Forms.Label title_lbl;
        private System.Windows.Forms.Label login_lbl;
        private System.Windows.Forms.TextBox login_textBox;
        private System.Windows.Forms.Button connect_btn;
        private System.Windows.Forms.Label error_lbl;
    }
}

