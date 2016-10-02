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
using System.Text.RegularExpressions;

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
                /// @brief check input information
                error_lbl.Visible = false;
                if (ip_textBox.Text.Equals("") || port_textBox.Text.Equals("") || login_textBox.Text.Equals(""))
                    throw new System.FormatException("No empty fields allowed");

                /// @brief prepare regex
                string ipReg = @"^(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])(\.(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])){3}$";
                string loginReg = @"^[a-zA-Z0-9]{3,13}$";

                /// @brief try to convert port to int
                int port = 0;
                try
                {
                    port = Convert.ToInt32(port_textBox.Text);
                }
                catch (Exception ex)
                {
                    throw new System.FormatException("Port is not correct");
                }
                /// @brief check other fields
                if (!Regex.IsMatch(ip_textBox.Text, ipReg))
                {
                    throw new System.FormatException("IP is not correct");
                }
                if (!Regex.IsMatch(login_textBox.Text, loginReg))
                {
                    throw new System.FormatException("Login is not correct");
                }

                /// @brief checkes is it test mode for debuging
                if (!testMode)
                {
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

                    /// @brief open new form    
                    chat_form new_frm = new chat_form(this, login_textBox.Text, ip_textBox.Text, port, client, stream);
                    error_lbl.Visible = false;
                    this.Hide();
                    new_frm.Show();
                }
                else
                {
                    chat_form new_frm = new chat_form(this, login_textBox.Text, ip_textBox.Text, port,  testMode);
                    error_lbl.Visible = false;
                    this.Hide();
                    new_frm.Show();
                }
            }

            /// @brief error handling
            catch (ArgumentNullException anExc)
            {
                error_lbl.Visible = true;
                error_lbl.Text = "No response from server: " + anExc;
            }
            catch(ArgumentOutOfRangeException argOutOfRangeExc)
            {
                error_lbl.Visible = true;
                error_lbl.Text = "No response from server: " + argOutOfRangeExc;
            }
            catch (SocketException socExc)
            {
                error_lbl.Visible = true;
                error_lbl.Text = "No server connection: " + socExc;
            }
            catch (InvalidOperationException ioExc)
            {
                error_lbl.Visible = true;
                error_lbl.Text = "" + ioExc;
            }
            catch (FormatException formatExc)
            {
                error_lbl.Visible = true;
                error_lbl.Text = "" + formatExc;
            }
            catch(OverflowException ofExc)
            {
                error_lbl.Visible = true;
                error_lbl.Text = "" + ofExc;
            }
        }

        private bool testMode = false;

    }
}
