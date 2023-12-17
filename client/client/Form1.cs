using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;


namespace client
{
    public partial class Form1 : Form
    {

        //initial ststea of connection, subscription and creation of sockets done here.
        bool terminating = false;
        bool connected = false;
        Socket clientSocket;
        Socket serverSocket;
        bool sps_subscribed = false;
        bool if_subscribed = false;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Disucord_DisucordClosing);
        }

        //list created to hold the subscription statuese of clients.
        //clients usernames and the texts written on the subscription/unsubscription button rgiht before clicking the disconnection button held. 
        List<string> subscription_list = new List<string>();


        //function that handles the general operations when subscripe to if chanell button clicked
        //the thread for this operaiton gets created and it calls the SubIF function.
        //this functions decides the color changes, enability and the messages written on the events window on client form after clicking subscription to if button. 
        private void button_if_subscribe_Click(object sender, EventArgs e)
        {
            if (button_if_subscribe.Text == "Subscribe")
            {
                if_subscribed = true;
                button_if_subscribe.Text = "Unsubscribe";
                button_if_subscribe.BackColor = Color.LightCoral;
                richTextBox_events.AppendText("You subscribed to IF100." + "\n");
                textBox_if.Enabled = true;
                button_if_send.Enabled = true;

                Thread sub_if = new Thread(() => SubIF());
                sub_if.Start();
            }

            else if (button_if_subscribe.Text == "Unsubscribe")
            {
                if_subscribed = false;
                button_if_subscribe.Text = "Subscribe";
                button_if_subscribe.BackColor = Color.LightGreen;
                richTextBox_events.AppendText("You unsubscribed from IF100." + "\n");
                richTextBox_if.Clear();
                textBox_if.Enabled = false;
                button_if_send.Enabled = false;

                Thread unsub_if = new Thread(() => UnsubIF());
                unsub_if.Start();

            }
        }

        //function that handles the general operations when subscripe to sps chanell button clicked
        //the thread for this operaiton gets created and it calls the SubSPS function.
        //this functions decides the color changes, enability and the messages written on the events window on client form after clicking subscription to sps button.
        private void button_sps_subscribe_Click(object sender, EventArgs e)
        {
            if (button_sps_subscribe.Text == "Subscribe")
            {
                sps_subscribed = true;
                button_sps_subscribe.Text = "Unsubscribe";
                button_sps_subscribe.BackColor = Color.LightCoral;
                richTextBox_events.AppendText("You subscribed to SPS101." + "\n");
                textBox_sps.Enabled = true;
                button_sps_send.Enabled = true;

                Thread sub_sps = new Thread(() => SubSPS());
                sub_sps.Start();

            }
            else if (button_sps_subscribe.Text == "Unsubscribe")
            {
                sps_subscribed = false;
                button_sps_subscribe.Text = "Subscribe";
                button_sps_subscribe.BackColor = Color.LightGreen;
                richTextBox_events.AppendText("You unsubscribed from SPS101." + "\n");
                richTextBox_sps.Clear();
                textBox_sps.Enabled = false;
                button_sps_send.Enabled = false;

                Thread unsub_sps = new Thread(() => UnsubSPS());
                unsub_sps.Start();

            }

        }

        //this is the function that manages the events happens after clicking the connect/disconnect button.
        //the color changes and enabilty statuses changes necessary after clicking connect/disconnect button helds here by also taking in consedariton if this the cient first time connecting to the server or not.
        //by this function the connection statuses and the subscription statuses during disconnection tracked for reconnection of the same client to subscription_list.
        //the username text written on if subscription and sps subscription button before licking disconnet added to the list if the username is not in the list.
        //one index after the username is that usernamed clients subscription status for if channel
        //two index after the username is that usernamed clients subscription status for sps channel
        private void button_connect_Click_1(object sender, EventArgs e)
        {
            if (button_connect.Text == "Connect" && textBox_ip.Text != "" && textBox_port.Text != "" && textBox_username.Text != "")
            {


                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                string IP = textBox_ip.Text;

                int portNum;

                if (Int32.TryParse(textBox_port.Text, out portNum))
                {
                    try
                    {
                        clientSocket.Connect(IP, portNum);

                        connected = true;
                        richTextBox_events.AppendText("Connected to the server!\n");

                        textBox_username.Enabled = false;


                        button_connect.Text = "Disconnect";
                        textBox_ip.Enabled = false;
                        textBox_port.Enabled = false;

                        if (subscription_list.Contains(textBox_username.Text))
                        {
                            richTextBox_events.Enabled = true;
                            button_if_subscribe.Enabled = true;
                            button_sps_subscribe.Enabled = true;
                            button_if_subscribe.Text = subscription_list[subscription_list.IndexOf(textBox_username.Text) + 1];
                            button_sps_subscribe.Text = subscription_list[subscription_list.IndexOf(textBox_username.Text) + 2];

                            if (button_if_subscribe.Text == "Subscribe")
                            {
                                button_if_subscribe.BackColor = Color.LightGreen;
                            }
                            else if (button_if_subscribe.Text == "Unsubscribe")
                            {
                                button_if_subscribe.BackColor = Color.LightCoral;
                            }

                            if (button_sps_subscribe.Text == "Subscribe")
                            {
                                button_sps_subscribe.BackColor = Color.LightGreen;
                            }
                            else if (button_sps_subscribe.Text == "Unsubscribe")
                            {
                                button_sps_subscribe.BackColor = Color.LightCoral;
                            }

                        }
                        else
                        {
                            richTextBox_events.Enabled = true;
                            button_if_subscribe.Enabled = true;
                            button_sps_subscribe.Enabled = true;

                            button_if_subscribe.Text = "Subscribe";
                            button_if_subscribe.BackColor = Color.White;

                            button_sps_subscribe.Text = "Subscribe";
                            button_sps_subscribe.BackColor = Color.White;

                            Thread send_name = new Thread(() => SendName());
                            send_name.Start();


                            Thread RecieveThread = new Thread(() => Receive(serverSocket));
                            RecieveThread.Start();
                        }


                    }
                    catch
                    {
                        richTextBox_events.AppendText("Could not connect to the server!\n");
                    }
                }
                else
                {
                    richTextBox_events.AppendText("Check the port\n");
                }
            }
            else if (button_connect.Text != "Connect")
            {
                richTextBox_events.AppendText("Disconnected from the server!\n");
                Thread disconnect_name = new Thread(() => DisconnectName());
                disconnect_name.Start();
                button_connect.Text = "Connect";
                textBox_ip.Enabled = true;
                textBox_port.Enabled = true;
                richTextBox_events.Enabled = true;
                textBox_if.Enabled = false;
                button_if_send.Enabled = false;
                textBox_sps.Enabled = false;
                button_sps_send.Enabled = false;
                button_if_subscribe.Enabled = true;
                button_sps_subscribe.Enabled = true;
                textBox_username.Enabled = true;

                if (subscription_list.Contains(textBox_username.Text))
                {
                    subscription_list[subscription_list.IndexOf(textBox_username.Text) + 1] = button_if_subscribe.Text;

                    subscription_list[subscription_list.IndexOf(textBox_username.Text) + 2] = button_sps_subscribe.Text;
                }
                else
                {
                    subscription_list.Add(textBox_username.Text);
                    subscription_list.Add(button_if_subscribe.Text);
                    subscription_list.Add(button_sps_subscribe.Text);
                }

                textBox_username.Clear();

            }
            else
            {
                richTextBox_events.AppendText("Please fill necessary parts!\n");
            }
        }

        //this functions manages the recieve buffer for the messages coming from server inorder to display them on the channels.
        //Also by each incoming message the functions clears the chanell boxex inorder to prevent duplication of the same message chain.
        //the message coming from server always has a three char long string which is a ststus key.
        //if the key is icl it means clearing the if richtextbox to prevent duplicaiton of same text chain.
        //if the key is scl it means clearing the sps richtextbox to prevent duplicaiton of same text chain.
        //if the key is mif it means this recieve buffer has a text ot display at the if richtextbox.
        //if the key is msp it means this recieve buffer has a text ot display at the sps richtextbox.
        //if the key is dbl it means there is already a user with the username out clients wnats to connect and asks him to enter with a different onr.

        private void Receive(Socket thisServer)
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[1028];
                    clientSocket.Receive(buffer);

                    string server_message = Encoding.Default.GetString(buffer);

                    string key = server_message.Substring(0, 3);

                    if (key == "icl")
                    {

                        richTextBox_if.Clear();
                    }
                    else if (key == "scl")
                    {
                        richTextBox_sps.Clear();
                    }
                    else if (key == "mif")
                    {
                        richTextBox_if.AppendText(server_message.Substring(3, (server_message.Length) - 3));
                        richTextBox_if.AppendText("\n");


                        Array.Clear(buffer, 0, buffer.Length);
                    }
                    else if (key == "msp")
                    {
                        richTextBox_sps.AppendText(server_message.Substring(3, (server_message.Length) - 3));
                        richTextBox_sps.AppendText("\n");


                        Array.Clear(buffer, 0, buffer.Length);
                    }
                    
                }
                catch
                {
                    if (!terminating)
                    {
                        richTextBox_events.AppendText("The server has disconnected\n");
                        button_if_subscribe.Enabled = false;
                        button_if_subscribe.BackColor = default;
                        button_sps_subscribe.Enabled = false;
                        button_sps_subscribe.BackColor = default;
                        button_if_send.Enabled = false;
                        button_sps_send.Enabled = false;
                        textBox_if.Enabled = false;
                        textBox_sps.Enabled = false;
                        connected = false;
                    }
                }
            }

        }

        // this functions sends an infomraiton buffer with keyword "dis" to the server for server to handle necessary operaitons for disconnection.
        // by sending key "dis" in front of the username of client.
        private void DisconnectName()
        {
            string disconnectname = ("dis" + textBox_username.Text);

            Byte[] disconnectnameBuffer = Encoding.Default.GetBytes(disconnectname);
            clientSocket.Send(disconnectnameBuffer);
            button_if_subscribe.Enabled = false;
            button_if_subscribe.BackColor = default;
            button_sps_subscribe.Enabled = false;
            button_sps_subscribe.BackColor = default;
            button_if_send.Enabled = false;
            button_sps_send.Enabled = false;
            textBox_if.Enabled = false;
            textBox_sps.Enabled = false;
            connected = false;

        }

        //this functions manages the sending of the username information of the connected client.
        // by sending key "con" in front of the username of client.
        private void SendName()
        {
            string username = ("con" + textBox_username.Text);

            Byte[] usernameBuffer = Encoding.Default.GetBytes(username);
            clientSocket.Send(usernameBuffer);
        }

        //this function manages the sending of the informaiton buffer of a clients unsubcription from IF chanell.
        // by sending key "uif" in front of the username of client.
        private void UnsubIF()
        {
            string unsub_if_name = ("uif" + textBox_username.Text);

            Byte[] unsub_if_nameBuffer = Encoding.Default.GetBytes(unsub_if_name);
            clientSocket.Send(unsub_if_nameBuffer);
        }

        //this function manages the sending of the informaiton buffer of a clients subcription from IF chanell.
        // by sending key "sif" in front of the username of client.
        private void SubIF()
        {
            string sub_if_name = ("sif" + textBox_username.Text);

            Byte[] sub_if_nameBuffer = Encoding.Default.GetBytes(sub_if_name);
            clientSocket.Send(sub_if_nameBuffer);
        }

        //this function manages the sending of the informaiton buffer of a clients unsubcription from SPS chanell.
        // by sending key "usp" in front of the username of client.
        private void UnsubSPS()
        {
            string unsub_sps_name = ("usp" + textBox_username.Text);

            Byte[] unsub_sps_nameBuffer = Encoding.Default.GetBytes(unsub_sps_name);
            clientSocket.Send(unsub_sps_nameBuffer);
        }

        //this function manages the sending of the informaiton buffer of a clients subcription from SPS chanell.
        // by sending key "ssp" in front of the username of client.
        private void SubSPS()
        {
            string sub_sps_name = ("ssp" + textBox_username.Text);

            Byte[] sub_sps_nameBuffer = Encoding.Default.GetBytes(sub_sps_name);
            clientSocket.Send(sub_sps_nameBuffer);
        }

        //this function manages the sending of the text message buffer of a clients to IF chanell.
        // by sending key "mif" in front of the username of client.
        private void SendIF_Message()
        {
            string if_message = ("mif" + textBox_username.Text + ": " + textBox_if.Text);

            Byte[] if_mesaggeBuffer = Encoding.Default.GetBytes(if_message);
            clientSocket.Send(if_mesaggeBuffer);
        }

        //this function manages the sending of the text message buffer of a clients to SPS chanell.
        // by sending key "msp" in front of the username of client.
        private void SendSPS_Message()
        {
            string sps_message = ("msp" + textBox_username.Text + ": " + textBox_sps.Text);

            Byte[] sps_mesaggeBuffer = Encoding.Default.GetBytes(sps_message);
            clientSocket.Send(sps_mesaggeBuffer);
        }

        //thşs functions manages the closing of the page by clicking the closing icon withouth programm crahsing.
        private void Disucord_DisucordClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        //this functions creates the thread for sending a message to if chanell and updates the conditions of subscription buttons according to that.
        private void button_if_send_Click_1(object sender, EventArgs e)
        {

            Thread send_if = new Thread(() => SendIF_Message());
            send_if.Start();
            textBox_if.Clear();

            if (button_if_subscribe.Text == "Unsubscribe")
            {
                textBox_if.Enabled = true;
                button_if_send.Enabled = true;
            }
        }

        //this functions creates the thread for sending a message to sps chanell and updates the conditions of subscription buttons according to that.
        private void button_sps_send_Click_1(object sender, EventArgs e)
        {

            Thread send_sps = new Thread(() => SendSPS_Message());
            send_sps.Start();
            textBox_sps.Clear();

            if (button_sps_subscribe.Text == "Unsubscribe")
            {
                textBox_sps.Enabled = true;
                button_sps_send.Enabled = true;
            }
        }
    }


}

