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
            try
            {
                error_lbl.Visible = false;
                if (ip_textBox.Text.Equals("") || port_textBox.Text.Equals("") || login_textBox.Text.Equals(""))
                    throw new System.FormatException("No empty fields allowed");
                int port = Convert.ToInt32(port_textBox.Text);
                if (!ip_textBox.Text.Contains("."))
                    throw new System.FormatException("IP is not correct");
                TcpClient client = new TcpClient(ip_textBox.Text, port);
                client.ReceiveTimeout = 100;
                client.SendTimeout = 100;

                NetworkStream stream = client.GetStream();
                Byte[] msg = System.Text.Encoding.ASCII.GetBytes("NEW " + login_textBox.Text);
                stream.Write(msg, 0, msg.Length);

                Byte[] bytes = new Byte[256];
                String data = String.Empty;
                int i = stream.Read(bytes, 0, bytes.Length);
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                stream.Close();
                client.Close();

                String code = data.Remove(3);
                if (code.Equals("PRT"))
                {
                    int new_port = Convert.ToInt32(data.Remove(0, 4));
                    client = new TcpClient(ip_textBox.Text, new_port);
                    stream = client.GetStream();
                }
                else
                {
                    if (code.Equals("ERN"))
                    {
                        error_lbl.Visible = true;
                        error_lbl.Text = "Login is already in use: " + login_textBox.Text;
                        
                    }
                    return;
                }
                    
                chat_form new_frm = new chat_form(this, login_textBox.Text, ip_textBox.Text, port, client, stream);
                error_lbl.Visible = false;
                this.Hide();
                new_frm.Show();
            }
            catch (ArgumentNullException a)
            {
                error_lbl.Visible = true;
                error_lbl.Text = "No response from server: " + a;
            }
            catch (SocketException b)
            {
                error_lbl.Visible = true;
                error_lbl.Text = "No server connection: " + b;
            }
            catch (InvalidOperationException c)
            {
                error_lbl.Visible = true;
                error_lbl.Text = "" + c;
            }
            catch (FormatException d)
            {
                error_lbl.Visible = true;
                error_lbl.Text = "" + d;
            }
        }

    }
}
