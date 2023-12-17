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
        List<string> connectedClients = new List<string>();

        List<Socket> SPSSubscribersSockets = new List<Socket>();
        List<string> SPSSubscribersAndTimes = new List<string>();

        List<Socket> IFSubscribersSockets = new List<Socket>();
        List<string> IFSubscribersAndTimes = new List<string>();

        List<string> SPSMessages = new List<string>();
        List<string> IFMessages = new List<string>();

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


                Thread acceptUserThread = new Thread(AcceptUser);
                acceptUserThread.Start();

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
            bool subscribedToSPS = false;
            DateTime LatestSubscriptionToSPS = DateTime.MaxValue;
            DateTime LatestSubscriptionToIF = DateTime.MaxValue;
            bool subscribedToIF = false;
            string username = "";

            

            while (connected && !terminating)
            {
                
                try
                {

                    IPEndPoint remoteEndPoint = (IPEndPoint)thisClient.RemoteEndPoint;
                    if (!(clientSockets.Contains(thisClient)))
                    {
                        clientSockets.Add(thisClient);

                    }


                    Byte[] buffer = new Byte[64];
                    thisClient.Receive(buffer);

                    string message = Encoding.Default.GetString(buffer);
                    /*
                    richTextBox_events.AppendText("-------------------\n");
                    richTextBox_events.AppendText(message);
                    richTextBox_events.AppendText("\n-------------------\n");
                    */
                    string key = message.Substring(0, 3);
                    string command = message.Substring(3, message.IndexOf("\0"));



                    richTextBox_connected_clients.Clear();
                    richTextBox_sps_subscribers.Clear();
                    richTextBox_if_subscribers.Clear();
                    
                    if (key == "con")
                    {
                        username = command;
                        if (!connectedClients.Contains(username))
                        {
                            
                            richTextBox_events.AppendText("\n( ͡° ͜ʖ ͡°)\n");
                            connectedClients.Add(username);
                            richTextBox_events.AppendText(username);
                            richTextBox_events.AppendText(" has connected.\n");
                            richTextBox_events.AppendText("\n( ͡° ͜ʖ ͡°)\n");
                        }
                        
                    }

                    else if(key == "dis")
                    {
                        richTextBox_events.AppendText(username);
                        richTextBox_events.AppendText(" has disconnected from the server.\n");

                        connectedClients.RemoveAll(item => item == username);

                        thisClient.Close();
                        clientSockets.Remove(thisClient);
                        connected = false;
                    }
                    else if (key == "ssp")
                    {
                        username = command;

                        DateTime currentDateTime = DateTime.Now;
                        string dateTime = currentDateTime.ToString();
                        LatestSubscriptionToSPS = currentDateTime;
                        string nameAndTime = dateTime + username;
                        SPSSubscribersAndTimes.Add(nameAndTime);
                        SPSSubscribersSockets.Add(thisClient);
                        subscribedToSPS = true;
                    }
                    else if(key == "usp")
                    {

                        username = command;
                        int username_length = username.Length;

                        SPSSubscribersAndTimes.RemoveAll(item => item.Substring(19, username_length) == username);

                        SPSSubscribersSockets.Remove(thisClient);

                        subscribedToSPS = false;

                        richTextBox_events.AppendText(username);

                        richTextBox_events.AppendText(" has unsubscribed from SPS101.\n");

                    }
                    else if (key == "sif")
                    {
                        username = command;

                        DateTime currentDateTime = DateTime.Now;
                        LatestSubscriptionToIF = currentDateTime;
                        string dateTime = currentDateTime.ToString();

                        string nameAndTime = dateTime + username;

                        IFSubscribersSockets.Add(thisClient);
                        IFSubscribersAndTimes.Add(nameAndTime);
                        subscribedToIF = true;
                        richTextBox_events.AppendText(username);
                        richTextBox_events.AppendText(" has subscribed to IF100.\n");
               
                    }
                    else if (key == "uif")
                    {

                        username = command;
                        int username_length = username.Length;
                        IFSubscribersAndTimes.RemoveAll(item => item.Substring(19, username_length) == username);

                        IFSubscribersSockets.Remove(thisClient);

                        subscribedToIF = false;

                        richTextBox_events.AppendText(username);

                        richTextBox_events.AppendText(" has unsubscribed from IF100.\n");

                        
                    }

                    // mif : Text message sent from the IF100 channel. 
                    


                    else if (key == "mif")
                    {

                        DateTime currentDateTime = DateTime.Now;
                        string dateTime = currentDateTime.ToString();

                        // Add message to the IFMessages (List<string>) in the following format: username + ":" text_message + dateTime

                        string text_message = message;


                        IFMessages.Add(text_message);


                        richTextBox_events.AppendText("(IF100) " + "(" + currentDateTime + ") : " + command);
                        richTextBox_events.AppendText("\n\n");



                        //Send messages to the client


                        for(int clientIndex = 0; clientIndex < clientSockets.Count; clientIndex++)
                        {



                            Socket client = clientSockets[clientIndex];


                            string clear = "icl";
                            Byte[] clear_buffer = Encoding.Default.GetBytes(clear);

                            client.Send(clear_buffer);
                            Array.Clear(clear_buffer, 0, clear_buffer.Length);

                            for (int i = 0; i < IFMessages.Count(); i++)
                            {
                                if (true)
                                {
                                    if(IFMessages[i].Length > 0)
                                    {
                                        string message_to_send = IFMessages[i];
                                        Byte[] if_message_buffer = new Byte[64];
                                        if_message_buffer = Encoding.Default.GetBytes(message_to_send);
                                        client.Send(if_message_buffer);
                                        Array.Clear(if_message_buffer, 0, if_message_buffer.Length);
                                    }
                                    
                                }

                            }
                        }

                       

                        
                            

                    }
                    

                    // msp : Text message sent from the SPS101 channel.
                    else if (key == "msp")
                    {
                        DateTime currentDateTime = DateTime.Now;
                        string dateTime = currentDateTime.ToString();

                        // Add message to the IFMessages (List<string>) in the following format: username + ":" text_message + dateTime

                        string text_message = message;


                        SPSMessages.Add(text_message);


                        richTextBox_events.AppendText("(SPS101) " + "(" + currentDateTime + ") : " + command);
                        richTextBox_events.AppendText("\n\n");





                        for (int i = 0; i < SPSMessages.Count(); i++)
                        {
                            if (true)
                            {
                                string message_to_send = SPSMessages[i];
                                Byte[] sps_message_buffer = new Byte[64];
                                sps_message_buffer = Encoding.Default.GetBytes(message_to_send);
                                thisClient.Send(sps_message_buffer);
                                Array.Clear(sps_message_buffer, 0, sps_message_buffer.Length);
                            }

                        }


                    }

                    for (int i = 0; i < connectedClients.Count; i++)
                    {
                        richTextBox_connected_clients.AppendText(connectedClients[i]);
                        richTextBox_connected_clients.AppendText("\n");
                    }
                    for (int j = 0; j < SPSSubscribersAndTimes.Count; j++)
                    {
                        richTextBox_sps_subscribers.AppendText(SPSSubscribersAndTimes[j].Substring(19, username.Length));

                        richTextBox_sps_subscribers.AppendText("\n");
                    }
                    for (int k = 0; k < IFSubscribersAndTimes.Count; k++)
                    {
                        richTextBox_if_subscribers.AppendText(IFSubscribersAndTimes[k].Substring(19, username.Length));
                        richTextBox_if_subscribers.AppendText("\n");
                    }








                }
                catch(Exception e)
                {
                    string name = username;
                    richTextBox_events.AppendText(name);
                    richTextBox_events.AppendText(" has disconnected from the server.\n");
                    connectedClients.RemoveAll(item => item == username);

                    richTextBox_connected_clients.Clear();
                    for (int i = 0; i < connectedClients.Count; i++)
                    {
                        richTextBox_connected_clients.AppendText(connectedClients[i]);
                        richTextBox_connected_clients.AppendText("\n");
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
