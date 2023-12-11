namespace server
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            richTextBox_events = new RichTextBox();
            richTextBox_if_subs = new RichTextBox();
            richTextBox_sps_subs = new RichTextBox();
            richTextBox_connected_clients = new RichTextBox();
            button_listen = new Button();
            textBox_port = new TextBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(34, 138);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(100, 37);
            label1.TabIndex = 0;
            label1.Text = "Events";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(469, 138);
            label2.Name = "label2";
            label2.Size = new Size(248, 37);
            label2.TabIndex = 1;
            label2.Text = "Connected Clients";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(789, 138);
            label3.Name = "label3";
            label3.Size = new Size(266, 37);
            label3.TabIndex = 2;
            label3.Text = "SPS101 Subscribers";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(1109, 138);
            label4.Name = "label4";
            label4.Size = new Size(242, 37);
            label4.TabIndex = 3;
            label4.Text = "IF100 Subscribers";
            // 
            // richTextBox_events
            // 
            richTextBox_events.BackColor = SystemColors.ControlLightLight;
            richTextBox_events.Location = new Point(34, 179);
            richTextBox_events.Margin = new Padding(3, 4, 3, 4);
            richTextBox_events.Name = "richTextBox_events";
            richTextBox_events.Size = new Size(399, 660);
            richTextBox_events.TabIndex = 4;
            richTextBox_events.Text = "";
            // 
            // richTextBox_if_subs
            // 
            richTextBox_if_subs.Location = new Point(1109, 179);
            richTextBox_if_subs.Margin = new Padding(3, 4, 3, 4);
            richTextBox_if_subs.Name = "richTextBox_if_subs";
            richTextBox_if_subs.Size = new Size(285, 660);
            richTextBox_if_subs.TabIndex = 5;
            richTextBox_if_subs.Text = "";
            // 
            // richTextBox_sps_subs
            // 
            richTextBox_sps_subs.Location = new Point(789, 179);
            richTextBox_sps_subs.Margin = new Padding(3, 4, 3, 4);
            richTextBox_sps_subs.Name = "richTextBox_sps_subs";
            richTextBox_sps_subs.Size = new Size(285, 660);
            richTextBox_sps_subs.TabIndex = 6;
            richTextBox_sps_subs.Text = "";
            // 
            // richTextBox_connected_clients
            // 
            richTextBox_connected_clients.Location = new Point(469, 179);
            richTextBox_connected_clients.Margin = new Padding(3, 4, 3, 4);
            richTextBox_connected_clients.Name = "richTextBox_connected_clients";
            richTextBox_connected_clients.Size = new Size(285, 660);
            richTextBox_connected_clients.TabIndex = 7;
            richTextBox_connected_clients.Text = "";
            // 
            // button_listen
            // 
            button_listen.Location = new Point(339, 66);
            button_listen.Name = "button_listen";
            button_listen.Size = new Size(94, 29);
            button_listen.TabIndex = 8;
            button_listen.Text = "Listen";
            button_listen.UseVisualStyleBackColor = true;
            button_listen.Click += button_listen_Click;
            // 
            // textBox_port
            // 
            textBox_port.Location = new Point(34, 66);
            textBox_port.Name = "textBox_port";
            textBox_port.Size = new Size(299, 27);
            textBox_port.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(34, 43);
            label5.Name = "label5";
            label5.Size = new Size(93, 20);
            label5.TabIndex = 10;
            label5.Text = "Port Number";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(1445, 908);
            Controls.Add(label5);
            Controls.Add(textBox_port);
            Controls.Add(button_listen);
            Controls.Add(richTextBox_connected_clients);
            Controls.Add(richTextBox_sps_subs);
            Controls.Add(richTextBox_if_subs);
            Controls.Add(richTextBox_events);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private RichTextBox richTextBox_events;
        private RichTextBox richTextBox_if_subs;
        private RichTextBox richTextBox_sps_subs;
        private RichTextBox richTextBox_connected_clients;
        private Button button_listen;
        private TextBox textBox_port;
        private Label label5;
    }
}