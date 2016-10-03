using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace websLab1
{
    class cMsgHandler
    {
        string m_data;
        RichTextBox m_textBox;
        LinkedList<string> m_logins;

        /// @brief ctor
        public cMsgHandler(string i_data, RichTextBox io_textBox, ref LinkedList<string> i_logins)
        {
            m_data = i_data;
            m_textBox = io_textBox;
            m_logins = i_logins;
        }

        ///  @brief sort messages by its type
        public void Sort(int a = 0)
        {
            /// @note prepare code and text
            string code = m_data.Remove(3);
            m_data = m_data.Substring(4, m_data.Length - 5);

            switch (code)
            {
                case "MSG":
                    {
                        m_textBox.Invoke(new Action(() =>
                        {
                            printMsg(false);
                        }));
                    }
                    break;
                case "PVT":
                    {
                        m_textBox.Invoke(new Action(() =>
                        {
                            printMsg(true);
                        }));
                    }
                    break;
                case "LST":
                    {
                        m_textBox.Invoke(new Action(() =>
                        {
                            showList();
                        }));
                    }
                    break;
                case "NEW":
                    {
                        m_textBox.Invoke(new Action(() =>
                        {
                            newConnection();
                        }));
                    }
                    break;
                case "DCT":
                    {
                        m_textBox.Invoke(new Action(() =>
                        {
                            disconnection();
                        }));
                    }
                    break;
                default:
                    break;
            }

        }

        /// @brief print basic or private message in textbox
        private void printMsg(bool isPrivate)
        {
            m_textBox.AppendText(System.Environment.NewLine + DateTime.Now.ToString("HH:mm:ss tt") + " ");

            string login = m_data.Remove(m_data.IndexOf(" "));
            if (isPrivate)
            {
                m_textBox.AppendText("[Private message from] " + login + ": ", Color.Blue);
            }
            else
            {
                m_textBox.AppendText(login + ": ");
            }


            string text = m_data.Remove(0, m_data.IndexOf(" "));
            m_textBox.AppendText(text);
            m_textBox.ScrollToCaret();
        }

        /// @brief print info about connected user
        private void newConnection()
        {
            m_textBox.AppendText(System.Environment.NewLine + DateTime.Now.ToString("HH:mm:ss tt") + " " + m_data + " is online");
            m_logins.AddFirst(m_data);
        }

        /// @brief print info about disconnected user
        private int disconnection()
        {
            m_textBox.AppendText(System.Environment.NewLine + DateTime.Now.ToString("HH:mm:ss tt") + " " + m_data + " disconnected");
            m_logins.Remove(m_data);
            return -1;
        }

        /// @brief print info about disconnected user
        private int showList()
        {
            int numOfUsers = Convert.ToInt32(m_data.Remove(1));
            string[] logins = m_data.Remove(0, 2).Split(' ');
            for (int i = 0; i < numOfUsers; ++i)
            {
                m_logins.AddFirst(logins[i]);
            }
            return numOfUsers;
        }
    }
}
