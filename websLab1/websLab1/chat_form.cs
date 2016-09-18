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
    public partial class chat_form : Form
    {
        //pointer to Sign In form
        SignIN_form old_frm;

        public chat_form( SignIN_form old_frm, string username)
        {
            InitializeComponent();

            login_lbl.Text = username;
            this.old_frm = old_frm;
        }

        private void disconnect_btn_Click(object sender, EventArgs e)
        {
            old_frm.Show();
            this.Close();
        }

        private void sent_btn_Click(object sender, EventArgs e)
        {
            //sent message handler
        }

        private void chat_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                old_frm.Close();
            }
        }

        private void chat_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //need to and work with server
        }
    }
}
