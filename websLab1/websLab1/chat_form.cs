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
        bool testMode;

        NetworkStream stream;
        TcpClient client;
        Thread myThread;                    // thread for listening socket
        Byte[] bytes = new Byte[256];       // coding text for sending
        String data = String.Empty;         // text of input message
        String code = String.Empty;         // code of input message
        LinkedList<string> logins;                 // count of online user
        int lblHeight = 25;

        ///  @brief production ctor
        public chat_form(SignIN_form old_frm, string username, string ip, int port, TcpClient TcpClient, NetworkStream NetworkStream)
        {
            InitializeComponent();
            client = TcpClient;
            login_lbl.Text = username;
            ip_lbl.Text = ip;
            port_lbl.Text = Convert.ToString(port);
            stream = NetworkStream;
            this.old_frm = old_frm;
            logins = new LinkedList<string>();
            userList_panel.AutoScroll = true;

            myThread = new Thread(listen);
            myThread.Start();
        }

        ///  @brief test ctor
        public chat_form(SignIN_form old_frm, string username, string ip, int port, bool itIsTest)
        {
            InitializeComponent();
            login_lbl.Text = username;
            port_lbl.Text = Convert.ToString(port);
            this.old_frm = old_frm;
            logins = new LinkedList<string>();

            testMode = itIsTest;
        }

        ///  @brief onDisconnectBtn event handler
        private void disconnect_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ///  @brief onSentBtn event handler
        private void sent_btn_Click(object sender, EventArgs e)
        {
            if (input_TextBox.Text.Equals(""))
                return;

            /// @brief check is it testing
            if (input_TextBox.Text.Remove(3).Equals("::T") || testMode)
            {
                if (!testMode)
                {
                    handleTextData(input_TextBox.Text.Remove(0, 4));
                }
                else
                {
                    handleTextData(input_TextBox.Text);
                }
            }
            else
            {
                Byte[] msg;
                if (input_TextBox.Text.Remove(8).Equals("private:"))
                {
                    if (logins.Find(input_TextBox.Text.Substring(9, input_TextBox.Text.IndexOf(" "))) != null)
                    {
                        msg = System.Text.Encoding.ASCII.GetBytes("PVT " + input_TextBox.Text.Remove(0, 8));
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    msg = System.Text.Encoding.ASCII.GetBytes("MSG " + input_TextBox.Text);
                }
                stream.Write(msg, 0, msg.Length);
            }           
            input_TextBox.Clear();
        }

        ///  @brief handle closing of the window
        ///  @note starts after window was closed
        private void chat_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!testMode)
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

        ///  @brief onEnterPressed event handler
        private void input_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                input_TextBox.Text = input_TextBox.Text.Remove(input_TextBox.Text.Length - 1);
                sent_btn_Click(sender, e);
            }
        }

        ///  @brief main function of thread
        void listen()
        {
            int i = 0;
            output_textBox.Text = "Welcome, " + login_lbl.Text + "!";
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                handleTextData(data);
            }
        }

        /// @brief handle data
        private void handleTextData(string data)
        {
            cMsgHandler msgHandler = new cMsgHandler(data, output_textBox, ref logins);
            int change = msgHandler.Sort();
            rebuildPanel(logins, change);
        }

        private void rebuildPanel(LinkedList<string> logins, int change)
        {
            userList_panel.Controls.Clear();
            LinkedListNode<string> node;
            int i = 0;
            for (node = logins.First; node != null; node = node.Next, ++i)
            {
                Label lb1 = new Label();
                lb1.Parent = userList_panel;
                lb1.Name = "lbl" + i.ToString();
                lb1.Text = node.Value;
                lb1.BorderStyle = BorderStyle.FixedSingle;
                lb1.Size = new Size(185,20);
                lb1.Font = new Font("Arial", 12.0F);


                lb1.Left = 1;
                lb1.Top = i*lblHeight;
            }
        }
    }
}
