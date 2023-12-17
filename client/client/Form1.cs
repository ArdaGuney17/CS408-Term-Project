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

        List<string> subscription_list = new List<string>();



        private void Connect()
        {
            try
            {
                string toSend = textBox_username.Text;

                Byte[] sendBuffer = Encoding.Default.GetBytes(toSend);
                clientSocket.Send(sendBuffer);



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

        private void button_connect_Click_1(object sender, EventArgs e)
        {
            if (button_connect.Text == "Connect" && textBox_ip.Text != "" && textBox_port.Text != "" && textBox_username.Text != "")
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
                        richTextBox_events.AppendText("Connected to the server!\n");



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
                        richTextBox_if.AppendText("\n");
                        richTextBox_if.AppendText("\n");
                        richTextBox_if.AppendText(server_message.Substring(3, (server_message.Length) - 3));

                        Array.Clear(buffer, 0, buffer.Length);
                    }
                    else if (key == "msp")
                    {
                        richTextBox_sps.AppendText("\n");
                        richTextBox_sps.AppendText("\n");
                        richTextBox_sps.AppendText(server_message.Substring(3, (server_message.Length) - 3));

                        Array.Clear(buffer, 0, buffer.Length);
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        richTextBox_events.AppendText("The server has disconnected\n");
                        button_if_subscribe.Enabled = false;
                        button_sps_subscribe.Enabled = false;
                        button_if_send.Enabled = false;
                        button_sps_send.Enabled = false;
                        textBox_if.Enabled = false;
                        textBox_sps.Enabled = false;
                    }
                }
            }

        }

        private void DisconnectName()
        {
            string disconnectname = ("dis" + textBox_username.Text);

            Byte[] disconnectnameBuffer = Encoding.Default.GetBytes(disconnectname);
            clientSocket.Send(disconnectnameBuffer);

            serverSocket.Close();
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
            string if_message = ("mif" + textBox_username.Text + ": " + textBox_if.Text);

            Byte[] if_mesaggeBuffer = Encoding.Default.GetBytes(if_message);
            clientSocket.Send(if_mesaggeBuffer);
        }

        private void SendSPS_Message()
        {
            string sps_message = ("msp" + textBox_username.Text + ": " + textBox_sps.Text);

            Byte[] sps_mesaggeBuffer = Encoding.Default.GetBytes(sps_message);
            clientSocket.Send(sps_mesaggeBuffer);
        }

        private void Disucord_DisucordClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_if_send_Click_1(object sender, EventArgs e)
        {

            Thread send_if = new Thread(() => SendIF_Message());
            send_if.Start();

            if (button_if_subscribe.Text == "Unsubscribe")
            {
                textBox_if.Enabled = true;
                button_if_send.Enabled = true;
            }
        }

        private void button_sps_send_Click_1(object sender, EventArgs e)
        {

            Thread send_sps = new Thread(() => SendSPS_Message());
            send_sps.Start();

            if (button_sps_subscribe.Text == "Unsubscribe")
            {
                textBox_sps.Enabled = true;
                button_sps_send.Enabled = true;
            }
        }
    }


}

