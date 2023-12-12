
namespace server
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.button_listen = new System.Windows.Forms.Button();
            this.richTextBox_events = new System.Windows.Forms.RichTextBox();
            this.richTextBox_connected_clients = new System.Windows.Forms.RichTextBox();
            this.richTextBox_if_subscribers = new System.Windows.Forms.RichTextBox();
            this.richTextBox_sps_subscribers = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port:";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(126, 54);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(238, 22);
            this.textBox_port.TabIndex = 1;
            this.textBox_port.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button_listen
            // 
            this.button_listen.Location = new System.Drawing.Point(416, 50);
            this.button_listen.Name = "button_listen";
            this.button_listen.Size = new System.Drawing.Size(101, 30);
            this.button_listen.TabIndex = 2;
            this.button_listen.Text = "Listen";
            this.button_listen.UseVisualStyleBackColor = true;
            this.button_listen.Click += new System.EventHandler(this.button_listen_Click);
            // 
            // richTextBox_events
            // 
            this.richTextBox_events.Location = new System.Drawing.Point(57, 158);
            this.richTextBox_events.Name = "richTextBox_events";
            this.richTextBox_events.Size = new System.Drawing.Size(307, 537);
            this.richTextBox_events.TabIndex = 3;
            this.richTextBox_events.Text = "";
            // 
            // richTextBox_connected_clients
            // 
            this.richTextBox_connected_clients.Location = new System.Drawing.Point(416, 158);
            this.richTextBox_connected_clients.Name = "richTextBox_connected_clients";
            this.richTextBox_connected_clients.Size = new System.Drawing.Size(256, 537);
            this.richTextBox_connected_clients.TabIndex = 4;
            this.richTextBox_connected_clients.Text = "";
            // 
            // richTextBox_if_subscribers
            // 
            this.richTextBox_if_subscribers.Location = new System.Drawing.Point(723, 158);
            this.richTextBox_if_subscribers.Name = "richTextBox_if_subscribers";
            this.richTextBox_if_subscribers.Size = new System.Drawing.Size(256, 537);
            this.richTextBox_if_subscribers.TabIndex = 5;
            this.richTextBox_if_subscribers.Text = "";
            // 
            // richTextBox_sps_subscribers
            // 
            this.richTextBox_sps_subscribers.Location = new System.Drawing.Point(1036, 158);
            this.richTextBox_sps_subscribers.Name = "richTextBox_sps_subscribers";
            this.richTextBox_sps_subscribers.Size = new System.Drawing.Size(256, 537);
            this.richTextBox_sps_subscribers.TabIndex = 6;
            this.richTextBox_sps_subscribers.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(52, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 29);
            this.label2.TabIndex = 7;
            this.label2.Text = "Events";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(411, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(227, 29);
            this.label3.TabIndex = 8;
            this.label3.Text = "Connected Clients";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(718, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(225, 29);
            this.label4.TabIndex = 9;
            this.label4.Text = "IF100 Subscribers";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1031, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(253, 29);
            this.label5.TabIndex = 10;
            this.label5.Text = "SPS101 Subscribers";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 748);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox_sps_subscribers);
            this.Controls.Add(this.richTextBox_if_subscribers);
            this.Controls.Add(this.richTextBox_connected_clients);
            this.Controls.Add(this.richTextBox_events);
            this.Controls.Add(this.button_listen);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Button button_listen;
        private System.Windows.Forms.RichTextBox richTextBox_events;
        private System.Windows.Forms.RichTextBox richTextBox_connected_clients;
        private System.Windows.Forms.RichTextBox richTextBox_if_subscribers;
        private System.Windows.Forms.RichTextBox richTextBox_sps_subscribers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

