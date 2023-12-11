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
using Microsoft.VisualBasic.Logging;
using System.Threading.Channels;

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
        }

        private void button_subscribe_Click(object sender, EventArgs e)
        {

            String channel = comboBox_channels.SelectedItem.ToString();


            if (channel == "SPS101" && button_subscribe.Text == "Subscribe")
            {
                sps_subscribed = true;
                button_subscribe.Text = "Unsubscribe";
                button_subscribe.BackColor = Color.LightCoral;
            }
            else if (channel == "IF100" && button_subscribe.Text == "Subscribe")
            {
                if_subscribed = true;
                button_subscribe.Text = "Unsubscribe";
                button_subscribe.BackColor = Color.LightCoral;

            }
            else if (channel == "SPS101" && button_subscribe.Text == "Unsubscribe")
            {
                sps_subscribed = false;
                button_subscribe.Text = "Subscribe";
                button_subscribe.BackColor = Color.LightGreen;
            }
            else if (channel == "IF100" && button_subscribe.Text == "Unsubscribe")
            {
                if_subscribed = false;
                button_subscribe.Text = "Subscribe";
                button_subscribe.BackColor = Color.LightGreen;

            }

            richTextBox_logs.AppendText("You subscribed to: " + channel + "\n");

        }




        private void comboBox_channels_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_subscribe.Enabled = true;

            String channel = comboBox_channels.SelectedItem.ToString();
            richTextBox_logs.AppendText("You are now in channel: " + channel + "\n");

            if (channel == "SPS101" && sps_subscribed == true)
            {
                button_subscribe.Text = "Unsubscribe";
                button_subscribe.BackColor = Color.LightCoral;
                Thread SPSThread = new Thread(SPSChannel);
                SPSThread.Start();
            }
            else if (channel == "IF100" && if_subscribed == true)
            {
                button_subscribe.Text = "Unsubscribe";
                button_subscribe.BackColor = Color.LightCoral;

            }
            else if (channel == "SPS101" && sps_subscribed == false)
            {
                button_subscribe.Text = "Subscribe";
                button_subscribe.BackColor = Color.LightGreen;
            }
            else if (channel == "IF100" && if_subscribed == false)
            {
                button_subscribe.Text = "Subscribe";
                button_subscribe.BackColor = Color.LightGreen;

            }
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
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
                    richTextBox_logs.AppendText("Connected to the server!\n");




                    comboBox_channels.Enabled = true;
                    button_connect.Text = "Disconnect";
                    textBox_ip.Enabled = false;
                    textBox_port.Enabled = false;
                    richTextBox_logs.Enabled = true;
                    textBox_message.Enabled = true;
                    button_send.Enabled = true;


                }
                catch
                {
                    richTextBox_logs.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                richTextBox_logs.AppendText("Check the port\n");
            }




        }



        private void button_send_Click(object sender, EventArgs e)
        {

            string message = textBox_message.Text;
            richTextBox_logs.AppendText("You: " + message + "\n");

            Thread SendReceiveThread = new Thread(SendAndReceive);
            SendReceiveThread.Start();
        }


        private void SendAndReceive()
        {
            while (connected)
            {
                try
                {

                    string toSend = textBox_message.Text;



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

        private void SPSChannel()
        {

            string toSend = sps_subscribed.ToString();

            Byte[] sendBuffer = Encoding.Default.GetBytes(toSend);
            clientSocket.Send(sendBuffer);

            Byte[] buffer = new Byte[64];
            clientSocket.Receive(buffer);

            richTextBox_logs.Text = buffer.ToString();
        }


        private void Connect()
        {
            string toSend = textBox_username.Text;

            Byte[] sendBuffer = Encoding.Default.GetBytes(toSend);
            clientSocket.Send(sendBuffer);

            Byte[] buffer = new Byte[64];
            clientSocket.Receive(buffer);

            richTextBox_logs.Text = buffer.ToString();
        }

    }

}