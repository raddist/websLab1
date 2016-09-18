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
        Socket _socket;
        EndPoint _endPoint;

        static void Listening(Object Info)
        {
            Socket s = (Socket)Info;
            //s.Listen(10);
        }

        public chat_form(SignIN_form old_frm, string username, string ip, int port, Socket _getSocket, IPEndPoint _getEndPoint)
        {
            InitializeComponent();

            login_lbl.Text = username;
            ip_lbl.Text = ip;
            port_lbl.Text = Convert.ToString(port);
            _socket = _getSocket;
            _endPoint = (EndPoint)_getEndPoint;
            Thread _thread = new Thread(new ParameterizedThreadStart(Listening));
            _thread.Start(_socket);
            this.old_frm = old_frm;
        }

        private void disconnect_btn_Click(object sender, EventArgs e)
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
            old_frm.Show();
            this.Close();
        }

        private void sent_btn_Click(object sender, EventArgs e)
        {
            string request = "Connection";
            Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
            _socket.SendTo(bytesSent, _endPoint);
            //Byte[] bytesReceived = new Byte[256];
            //_socket.ReceiveFrom(bytesReceived, ref _endPoint);
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

        }

        private void output_textBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void userList_panel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
