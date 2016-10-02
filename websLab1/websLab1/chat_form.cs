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
        Thread myThread;
        Byte[] bytes = new Byte[256];
        String data = String.Empty;
        String code = String.Empty;

        void listen()
        {
            int i = 0;
            output_textBox.Text = "Welcome, " + login_lbl.Text + "!";
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                output_textBox.AppendText(System.Environment.NewLine + DateTime.Now.ToString("HH:mm:ss tt") + " ");
                code = data.Remove(3);
                data = data.Substring(4, data.Length - 5);
                switch(code)
                {
                    case "MSG":
                        output_textBox.AppendText(data.Remove(data.IndexOf(" ")) + ": ");
                        data = data.Remove(0,data.IndexOf(" "));
                        output_textBox.AppendText(data);
                        break;
                    case "PVT":
                        output_textBox.AppendText("Private: " + data);
                        break;
                    case "LST":
                        output_textBox.AppendText("Online: " + data);
                        break;
                    case "NEW":
                        output_textBox.AppendText(data + " is online");
                        break;
                    case "DCT":
                        output_textBox.AppendText(data + " is offline");
                        break;
                    default:
                        break;
                }
            }
        }

        public chat_form(SignIN_form old_frm, string username, string ip, int port, TcpClient TcpClient, NetworkStream NetworkStream)
        {
            InitializeComponent();
            client = TcpClient;
            login_lbl.Text = username;
            ip_lbl.Text = ip;
            port_lbl.Text = Convert.ToString(port);
            stream = NetworkStream;
            this.old_frm = old_frm;

            myThread = new Thread(listen);
            myThread.Start();
        }

        private void disconnect_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sent_btn_Click(object sender, EventArgs e)
        {
            if (input_TextBox.Text.Equals(""))
                return;
            Byte[] msg = System.Text.Encoding.ASCII.GetBytes("MSG " + input_TextBox.Text);
            stream.Write(msg, 0, msg.Length);
            input_TextBox.Clear();
        }

        private void chat_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Byte[] msg = System.Text.Encoding.ASCII.GetBytes("DCT " + login_lbl.Text);
            stream.Write(msg, 0, msg.Length);
            try
            {
                myThread.Abort();
                stream.Close();
                client.Close();
            }
            catch (ThreadAbortException)
            {
                old_frm.Close();
            }
            if (e.CloseReason != CloseReason.UserClosing)
            {
                old_frm.Close();
            }
            else
            {
                old_frm.Show();
            }
        }

        private void input_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                input_TextBox.Text = input_TextBox.Text.Remove(input_TextBox.Text.Length - 1);
                sent_btn_Click(sender, e);
            }
        }
    }
}
