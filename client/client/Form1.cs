﻿using System.Net.Sockets;
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

        bool terminating = false;
        bool connected = false;
        Socket clientSocket;
        bool sps_subscribed = false;
        bool if_subscribed = false;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Disucord_DisucordClosing);
        }

        



        private void button_if_send_Click(object sender, EventArgs e)
        {

            string message = textBox_if.Text;
            richTextBox_events.AppendText("You: " + message + "\n");

            Thread SendReceiveThreadIF = new Thread(SendAndReceiveIF);
            SendReceiveThreadIF.Start();
        }

        private void button_sps_send_Click(object sender, EventArgs e)
        {

            string message = textBox_sps.Text;
            richTextBox_events.AppendText("You: " + message + "\n");

            Thread SendReceiveThreadSPS = new Thread(SendAndReceiveSPS);
            SendReceiveThreadSPS.Start();
        }


        private void SendAndReceiveIF()
        {
            while (connected)
            {
                try
                {

                    string toSend = textBox_if.Text;



                    Byte[] sendBuffer = Encoding.Default.GetBytes(toSend);
                    clientSocket.Send(sendBuffer);

                    Byte[] buffer = new Byte[64];
                    clientSocket.Receive(buffer);


                    string token = Encoding.Default.GetString(buffer);

                }
                catch
                {
                    if (!terminating)
                    {
                        Console.WriteLine("Disconnected");
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }

        }

        private void SendAndReceiveSPS()
        {
            while (connected)
            {
                try
                {

                    string toSend = textBox_sps.Text;



                    Byte[] sendBuffer = Encoding.Default.GetBytes(toSend);
                    clientSocket.Send(sendBuffer);

                    Byte[] buffer = new Byte[64];
                    clientSocket.Receive(buffer);


                    string token = Encoding.Default.GetString(buffer);

                }
                catch
                {
                    if (!terminating)
                    {
                        Console.WriteLine("Disconnected");
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }

        }


        private void Connect()
        {
            try
            {
                string toSend = textBox_username.Text;

                Byte[] sendBuffer = Encoding.Default.GetBytes(toSend);
                clientSocket.Send(sendBuffer);

                Byte[] buffer = new Byte[64];
                int bytesRead = clientSocket.Receive(buffer);

                string receivedData = Encoding.Default.GetString(buffer, 0, bytesRead);

                richTextBox_events.Text = receivedData;
            }
            catch (Exception ex)
            {
                richTextBox_events.AppendText("Your connection failed please try again." + "\n");
            }
        }


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

        private void textBox_port_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_connect_Click_1(object sender, EventArgs e)
        {
            if (button_connect.Text == "Connect" && textBox_ip.Text != "" && textBox_port.Text != "" && textBox_username.Text != "") { 

                Thread connect = new Thread(Connect);
                connect.Start();

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





                        button_connect.Text = "Disconnect";
                        textBox_ip.Enabled = false;
                        textBox_port.Enabled = false;
                        richTextBox_events.Enabled = true;
                        textBox_if.Enabled = true;
                        button_if_send.Enabled = true;
                        textBox_sps.Enabled = true;
                        button_sps_send.Enabled = true;
                        button_if_subscribe.Enabled = true;
                        button_sps_subscribe.Enabled = true;
                        button_if_subscribe.BackColor = Color.LightGreen;
                        button_sps_subscribe.BackColor = Color.LightGreen;

                        Thread send_name = new Thread(() => SendName());
                        send_name.Start();


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
            else if(button_connect.Text != "Connect"){
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

            }
            else
            {
                richTextBox_events.AppendText("Please fill necessary parts!\n");
            }
        }

        private void DisconnectName()
        {
            string disconnectname = ("dis" + textBox_username.Text) ;

            Byte[] disconnectnameBuffer = Encoding.Default.GetBytes(disconnectname);
            clientSocket.Send(disconnectnameBuffer);
        }
        private void SendName()
        {
            string username = ("con" + textBox_username.Text);

            Byte[] usernameBuffer = Encoding.Default.GetBytes(username);
            clientSocket.Send(usernameBuffer);
        }

        private void UnsubIF()
        {
            string unsub_if_name = ("uif" + textBox_username.Text);

            Byte[] unsub_if_nameBuffer = Encoding.Default.GetBytes(unsub_if_name);
            clientSocket.Send(unsub_if_nameBuffer);
        }
        private void SubIF()
        {
            string sub_if_name = ("sif" + textBox_username.Text);

            Byte[] sub_if_nameBuffer = Encoding.Default.GetBytes(sub_if_name);
            clientSocket.Send(sub_if_nameBuffer);
        }

        private void UnsubSPS()
        {
            string unsub_sps_name = ("usp" + textBox_username.Text);

            Byte[] unsub_sps_nameBuffer = Encoding.Default.GetBytes(unsub_sps_name);
            clientSocket.Send(unsub_sps_nameBuffer);
        }
        private void SubSPS()
        {
            string sub_sps_name = ("ssp" + textBox_username.Text);

            Byte[] sub_sps_nameBuffer = Encoding.Default.GetBytes(sub_sps_name);
            clientSocket.Send(sub_sps_nameBuffer);
        }

        private void SendIF_Message()
        {
            string if_message = ("mif" + textBox_username.Text + ":" + textBox_if.Text);

            Byte[] if_mesaggeBuffer = Encoding.Default.GetBytes(if_message);
            clientSocket.Send(if_mesaggeBuffer);
        }

        private void SendSPS_Message()
        {
            string sps_message = textBox_sps.Text;

            Byte[] sps_mesaggeBuffer = Encoding.Default.GetBytes(sps_message);
            clientSocket.Send(sps_mesaggeBuffer);
        }

        private void textBox_if_TextChanged(object sender, EventArgs e)
        {

        }

        private void Disucord_DisucordClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }
    }


}