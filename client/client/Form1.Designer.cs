namespace client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBox_channels = new ComboBox();
            button_subscribe = new Button();
            richTextBox_logs = new RichTextBox();
            button_send = new Button();
            textBox_message = new TextBox();
            textBox_ip = new TextBox();
            textBox_port = new TextBox();
            button_connect = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox_username = new TextBox();
            SuspendLayout();
            // 
            // comboBox_channels
            // 
            comboBox_channels.Enabled = false;
            comboBox_channels.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            comboBox_channels.FormattingEnabled = true;
            comboBox_channels.Items.AddRange(new object[] { "SPS101", "IF100" });
            comboBox_channels.Location = new Point(34, 208);
            comboBox_channels.Name = "comboBox_channels";
            comboBox_channels.Size = new Size(327, 44);
            comboBox_channels.TabIndex = 0;
            comboBox_channels.Text = "Select channel...";
            comboBox_channels.SelectedIndexChanged += comboBox_channels_SelectedIndexChanged;
            // 
            // button_subscribe
            // 
            button_subscribe.BackColor = Color.LightGreen;
            button_subscribe.Enabled = false;
            button_subscribe.FlatStyle = FlatStyle.Flat;
            button_subscribe.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button_subscribe.Location = new Point(386, 208);
            button_subscribe.Name = "button_subscribe";
            button_subscribe.Size = new Size(197, 51);
            button_subscribe.TabIndex = 1;
            button_subscribe.Text = "Subscribe";
            button_subscribe.UseVisualStyleBackColor = false;
            button_subscribe.Click += button_subscribe_Click;
            // 
            // richTextBox_logs
            // 
            richTextBox_logs.Enabled = false;
            richTextBox_logs.Location = new Point(34, 297);
            richTextBox_logs.Name = "richTextBox_logs";
            richTextBox_logs.Size = new Size(548, 489);
            richTextBox_logs.TabIndex = 2;
            richTextBox_logs.Text = "";
            // 
            // button_send
            // 
            button_send.Enabled = false;
            button_send.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button_send.Location = new Point(485, 827);
            button_send.Margin = new Padding(3, 4, 3, 4);
            button_send.Name = "button_send";
            button_send.Size = new Size(105, 44);
            button_send.TabIndex = 4;
            button_send.Text = "Send";
            button_send.UseVisualStyleBackColor = true;
            button_send.Click += button_send_Click;
            // 
            // textBox_message
            // 
            textBox_message.Cursor = Cursors.IBeam;
            textBox_message.Enabled = false;
            textBox_message.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            textBox_message.Location = new Point(34, 827);
            textBox_message.Margin = new Padding(3, 4, 3, 4);
            textBox_message.Name = "textBox_message";
            textBox_message.Size = new Size(443, 39);
            textBox_message.TabIndex = 5;
            // 
            // textBox_ip
            // 
            textBox_ip.Location = new Point(34, 129);
            textBox_ip.Margin = new Padding(3, 4, 3, 4);
            textBox_ip.Name = "textBox_ip";
            textBox_ip.Size = new Size(179, 27);
            textBox_ip.TabIndex = 6;
            // 
            // textBox_port
            // 
            textBox_port.Location = new Point(233, 129);
            textBox_port.Margin = new Padding(3, 4, 3, 4);
            textBox_port.Name = "textBox_port";
            textBox_port.Size = new Size(220, 27);
            textBox_port.TabIndex = 7;
            // 
            // button_connect
            // 
            button_connect.Location = new Point(473, 129);
            button_connect.Margin = new Padding(3, 4, 3, 4);
            button_connect.Name = "button_connect";
            button_connect.Size = new Size(110, 31);
            button_connect.TabIndex = 8;
            button_connect.Text = "Connect";
            button_connect.UseVisualStyleBackColor = true;
            button_connect.Click += button_connect_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 105);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 9;
            label1.Text = "IP Address";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(233, 105);
            label2.Name = "label2";
            label2.Size = new Size(35, 20);
            label2.TabIndex = 10;
            label2.Text = "Port";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(37, 39);
            label3.Name = "label3";
            label3.Size = new Size(75, 20);
            label3.TabIndex = 11;
            label3.Text = "Username";
            // 
            // textBox_username
            // 
            textBox_username.Location = new Point(34, 63);
            textBox_username.Margin = new Padding(3, 4, 3, 4);
            textBox_username.Name = "textBox_username";
            textBox_username.Size = new Size(179, 27);
            textBox_username.TabIndex = 12;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 908);
            Controls.Add(textBox_username);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button_connect);
            Controls.Add(textBox_port);
            Controls.Add(textBox_ip);
            Controls.Add(textBox_message);
            Controls.Add(button_send);
            Controls.Add(richTextBox_logs);
            Controls.Add(button_subscribe);
            Controls.Add(comboBox_channels);
            Name = "Form1";
            Text = "DiSucord";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox_channels;
        private Button button_subscribe;
        private RichTextBox richTextBox_logs;
        private Button button_send;
        private TextBox textBox_message;
        private TextBox textBox_ip;
        private TextBox textBox_port;
        private Button button_connect;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox_username;
    }
}