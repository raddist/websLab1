using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace websLab1
{
    public partial class SignIN_form : Form
    {
        public SignIN_form()
        {
            InitializeComponent();
        }

        private void connect_btn_Click(object sender, EventArgs e)
        {
            if (ip_textBox.Text.Equals("") ||
                port_textBox.Text.Equals("") ||
                login_textBox.Text.Equals(""))
            {
                error_lbl.Visible = true;
                error_lbl.Text = "No empty fields allowed";
                return;
            }

            // connection with server

            chat_form new_frm = new chat_form(this, login_textBox.Text /* , socket */);

            error_lbl.Visible = false;
            this.Hide();
            new_frm.Show();
        }
    }
}
