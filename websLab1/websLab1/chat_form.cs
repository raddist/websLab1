using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace websLab1
{
    public partial class chat_form : Form
    {
        //pointer to Sign In form
        SignIN_form old_frm;

        NetworkStream stream;
        TcpClient client;

        public chat_form(SignIN_form old_frm, string username, string ip, int port, TcpClient TcpClient, NetworkStream NetworkStream)
        {
            InitializeComponent();
            client = TcpClient;
            login_lbl.Text = username;
            ip_lbl.Text = ip;
            port_lbl.Text = Convert.ToString(port);
            stream = NetworkStream;
            this.old_frm = old_frm;
        }

        private void disconnect_btn_Click(object sender, EventArgs e)
        {
            old_frm.Show();
            this.Close();
        }

        private void sent_btn_Click(object sender, EventArgs e)
        {
            Byte[] msg = System.Text.Encoding.ASCII.GetBytes("MSG " + input_TextBox.Text);
            stream.Write(msg, 0, msg.Length);
        }
        private void chat_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                old_frm.Close();
            }
        }


    }
}
