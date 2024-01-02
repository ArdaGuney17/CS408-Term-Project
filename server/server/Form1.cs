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

                // For successful inputs from the textbox_port, server starts to listen to any connections. 
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_listen.Enabled = false;

                // For any connection, a thread starts.
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

                    //Any connection is from a client is defined as a Socket and a new thread starts for each new Socket.
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

                    // A client socket list is kept to indicate connected clients at a time. The list is shown in the server GUI.
                    // There is a uniqueness check to avoid multiple connections with the same username.

                    IPEndPoint remoteEndPoint = (IPEndPoint)thisClient.RemoteEndPoint;
                    if (!(clientSockets.Contains(thisClient)))
                    {
                        clientSockets.Add(thisClient);

                    }


                    // A single buffer is used to take all of the inputs from the client.
                    // The first 3 characters of the input is used as a "key" that corresponds to particular operations. 
                    /*  "con" : Connect to the server.
                        "dis" : Disconnect from the server.
                        "ssp" : Subscribe to SPS101.
                        "usp" : Unsubscribe from SPS101.
                        "sif" : Subscribe to IF100.
                        "uif" : Unsubscribe from SPS101 channel.
                        "mif" : Send message from the IF100 channel.
                        "sif" : Send message from the SPS101 channel.

                    // The rest of the input is interpreted as "command" that is used differently for some operations.


                    Also, the first 3 characters of the output is used as a "key" that corresponds to particular operations. 
                        "dbl" : To indicate there is another client connected with same username. No "double" connection is allowed.
                        "icl" : Clear the IF100 channel monitor.
                        "scl" : Clear the SPS101 channel monitor.
                    */



                    Byte[] buffer = new Byte[1028];
                    thisClient.Receive(buffer);

                    string message = Encoding.Default.GetString(buffer);
                    string key = message.Substring(0, 3);
                    string command = message.Substring(3, message.IndexOf("\0"));


                    // For any input from any client, the monitors are refreshed.


                    richTextBox_connected_clients.Clear();
                    richTextBox_sps_subscribers.Clear();
                    richTextBox_if_subscribers.Clear();
                    
                    if (key == "con") // Connect to the server.
                    {
                        username = command;
                        if (!connectedClients.Contains(username))
                        {
                            // The client is added to the connected clients list.
                            // There is a control if the a client already is connected with the same username. If not, the user is allowed to connect.
                            if (connectedClients.Contains(username))
                            {
                                string clear = "dbl";
                                Byte[] clear_buffer = Encoding.Default.GetBytes(clear);

                                thisClient.Send(clear_buffer);
                                Array.Clear(clear_buffer, 0, clear_buffer.Length);
                                thisClient.Close();
                                connected = false;

                            }
                            else
                            {
                                connectedClients.Add(username);
                            }
                            richTextBox_events.AppendText(username);
                            richTextBox_events.AppendText(" has connected.\n");
                        }
                        
                    }

                    else if(key == "dis") // Disconnect from the server.
                    {
                        richTextBox_events.AppendText(username);
                        richTextBox_events.AppendText(" has disconnected from the server.\n");

                        // The client is removed from the connected clients list.
                        // Connection is closed from the socket.

                        connectedClients.RemoveAll(item => item == username);

                        thisClient.Close();
                        clientSockets.Remove(thisClient);
                        connected = false;
                    }
                    else if (key == "ssp") // Subscription to SPS101.
                    {

                        username = command;

                        DateTime currentDateTime = DateTime.Now;
                        string dateTime = currentDateTime.ToString();
                        string nameAndTime = dateTime + username;

                        //  An additional list of subscription time is kept to decide whether a particular message should be sent to a given client. 

                        if (!(SPSSubscribersAndTimes.Contains(nameAndTime)))
                        {
                            SPSSubscribersAndTimes.Add(nameAndTime);
                        }
                        SPSSubscribersAndTimes.Add(nameAndTime);

                        // Another list of Sockets is used to multicast.

                        if (!(SPSSubscribersSockets.Contains(thisClient)))
                        {
                            SPSSubscribersSockets.Add(thisClient);
                        }
                        subscribedToSPS = true;
                    }
                    else if(key == "usp") // Unsubscribe from SPS101.
                    {

                        username = command;
                        int username_length = username.Length;

                        // The client is removed from the SPS101 subscribers list.

                        SPSSubscribersAndTimes.RemoveAll(item => item.Substring(19, username_length) == username);

                        SPSSubscribersSockets.Remove(thisClient);

                        subscribedToSPS = false;

                        richTextBox_events.AppendText(username);

                        richTextBox_events.AppendText(" has unsubscribed from SPS101.\n");

                    }
                    else if (key == "sif") // Subscription to IF100.
                    {
                        username = command;


                        //  An additional list of subscription time is kept to decide whether a particular message should be sent to a given client. 

                        DateTime currentDateTime = DateTime.Now;
                        LatestSubscriptionToIF = currentDateTime;
                        string dateTime = currentDateTime.ToString();

                        string nameAndTime = dateTime + username;



                        if (!(IFSubscribersSockets.Contains(thisClient)))
                        {
                            IFSubscribersSockets.Add(thisClient);
                        }

                        // Another list of Sockets is used to multicast.

                        if (!(IFSubscribersAndTimes.Contains(nameAndTime)))
                        {
                            IFSubscribersAndTimes.Add(nameAndTime);
                        }
                        subscribedToIF = true;
                        richTextBox_events.AppendText(username);
                        richTextBox_events.AppendText(" has subscribed to IF100.\n");
               
                    }
                    else if (key == "uif")
                    {
                        // The client is removed from the SPS101 subscribers list.
                        username = command;
                        int username_length = username.Length;

                        IFSubscribersAndTimes.RemoveAll(item => item.Substring(19, username_length) == username);

                        IFSubscribersSockets.Remove(thisClient);

                        subscribedToIF = false;

                        richTextBox_events.AppendText(username);

                        richTextBox_events.AppendText(" has unsubscribed from IF100.\n");

                        
                    }

                    
                    


                    else if (key == "mif") // Send message from the IF100 channel. 
                    {

                        DateTime currentDateTime = DateTime.Now;
                        string dateTime = currentDateTime.ToString();

                        // Add message to the IFMessages (List<string>) in the following format: username + ":" text_message + dateTime

                        string text_message = message + dateTime;


                        IFMessages.Add(text_message);


                        richTextBox_events.AppendText("(IF100) " + "(" + currentDateTime + ") : " + command);
                        richTextBox_events.AppendText("\n\n");



                        //Multicasting

                        // The messages are kept in the lists for each channel (IFMessagesList, SPSMessagesList)
                        // The subscribers are also kept in lists so that the messages are sent only to the subcribers.
                        // The messages are filtered for each subscriber based on their subscription date and time to the particular channel.
                        // For each message, the subscription time and the time that message is sent is compared.
                        // If the message is sent later, the subscriber gets the message, if not, it does not.


                        for (int clientIndex = 0; clientIndex < IFSubscribersSockets.Count; clientIndex++)
                        {



                            Socket client = IFSubscribersSockets[clientIndex];
                            IPEndPoint clientEndPoint = (IPEndPoint)client.RemoteEndPoint;

                            DateTime subscriptionTime = DateTime.Parse(IFSubscribersAndTimes[clientIndex].Substring(0, 19));



                            string clear = "icl";
                            Byte[] clear_buffer = Encoding.Default.GetBytes(clear);

                            client.Send(clear_buffer);
                            Array.Clear(clear_buffer, 0, clear_buffer.Length);


                            for (int i = 0; i < IFMessages.Count(); i++)
                            {

                                string time_of_message = IFMessages[i].Substring((IFMessages[i].Length - 19), 19);

                                DateTime timeOfMessage = DateTime.Parse(time_of_message);


                                if (timeOfMessage.CompareTo(subscriptionTime) > 0)
                                {
                                    if (IFMessages[i].Length > 0)
                                    {
                                        string message_to_send = IFMessages[i];
                                        Byte[] if_message_buffer = new Byte[1028];
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
                        richTextBox_events.AppendText("\n");



                        //Send messages to the clients.


                        for (int clientIndex = 0; clientIndex < SPSSubscribersSockets.Count; clientIndex++)
                        {



                            Socket client = SPSSubscribersSockets[clientIndex];
                            IPEndPoint clientEndPoint = (IPEndPoint)client.RemoteEndPoint;

                            DateTime subscriptionTime = DateTime.Parse(SPSSubscribersAndTimes[clientIndex].Substring(0, 19));



                            string clear = "icl";
                            Byte[] clear_buffer = Encoding.Default.GetBytes(clear);

                            client.Send(clear_buffer);
                            Array.Clear(clear_buffer, 0, clear_buffer.Length);


                            for (int i = 0; i < SPSMessages.Count(); i++)
                            {

                                string time_of_message = SPSMessages[i].Substring((SPSMessages[i].Length - 19), 19);

                                DateTime timeOfMessage = DateTime.Parse(time_of_message);


                                if (timeOfMessage.CompareTo(subscriptionTime) > 0)
                                {
                                    if (SPSMessages[i].Length > 0)
                                    {
                                        string message_to_send = IFMessages[i];
                                        Byte[] sps_message_buffer = new Byte[1028];
                                        sps_message_buffer = Encoding.Default.GetBytes(message_to_send);
                                        client.Send(sps_message_buffer);
                                        Array.Clear(sps_message_buffer, 0, sps_message_buffer.Length);
                                    }

                                }

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
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }
        
    }
}
