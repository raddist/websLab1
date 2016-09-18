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

            int _port = Convert.ToInt32(port_textBox.Text);
            Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress _address = System.Net.IPAddress.Parse(ip_textBox.Text);
            IPEndPoint _endPoint = new IPEndPoint(_address, _port);
            _socket.Bind(_endPoint);
            _socket.Connect(_endPoint);

            if (!_socket.Connected)
            {
                MessageBox.Show("Error!");
            }

            chat_form new_frm = new chat_form(this, login_textBox.Text, ip_textBox.Text, _port, _socket, _endPoint);
            error_lbl.Visible = false;
            this.Hide();
            new_frm.Show();
        }
    }
}
