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
            TcpClient client = new TcpClient(ip_textBox.Text, _port);
            NetworkStream stream = client.GetStream();

            Byte[] msg = System.Text.Encoding.ASCII.GetBytes("NEW " + login_textBox.Text);
            stream.Write(msg, 0, msg.Length);

            
            Byte[] bytes = new Byte[256];
            int i = stream.Read(bytes, 0, bytes.Length);
            String data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            stream.Close();
            client.Close();

            if (data.Contains("PTR"))
            {
                int new_port = Convert.ToInt32(data.Remove(1, 4));
                client = new TcpClient(ip_textBox.Text, new_port);
                stream = client.GetStream();
            }
            else
                if (data.Contains("ERN"))
                {
                    error_lbl.Visible = true;
                    error_lbl.Text = "Login is already in use";
                    return;
                }
            
            chat_form new_frm = new chat_form(this, login_textBox.Text, ip_textBox.Text, _port, client, stream);
            error_lbl.Visible = false;
            this.Hide();
            new_frm.Show();
        }
    }
}
