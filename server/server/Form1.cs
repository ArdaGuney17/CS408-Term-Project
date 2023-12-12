using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
    public partial class Form1 : Form
    {

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();

        bool terminating = false;
        bool listening = false;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_Load);

            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(textBox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);



                listening = true;
                button_listen.Enabled = false;

                Thread acceptUsernameThread = new Thread(AcceptUser);
                acceptUsernameThread.Start();

                richTextBox_events.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                richTextBox_events.AppendText("Please check port number \n");
            }

        }

        private void AcceptUser()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);
                    richTextBox_events.AppendText("A client is connected.\n");

                    Byte[] userNameBuffer = new Byte[64];
                    newClient.Receive(userNameBuffer);


                    string ConnectionAndClientUsername = Encoding.Default.GetString(userNameBuffer);

                    

                    richTextBox_connected_clients.AppendText($"{ConnectionAndClientUsername}\n");

                    Thread receiveThread = new Thread(() => ReceiveClientUsername(newClient)); // updated
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        richTextBox_events.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void ReceiveClientUsername(Socket thisClient) // updated
        {
            bool connected = true;

            while (connected && !terminating)
            {
                try
                {
                    Byte[] message_buffer = new Byte[64];
                    thisClient.Receive(message_buffer);


                    string incomingMessage = Encoding.Default.GetString(message_buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    richTextBox_events.AppendText("Client: " + incomingMessage + "\n");
                }
                catch
                {
                    if (!terminating)
                    {
                        richTextBox_events.AppendText("A client has disconnected\n");
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
