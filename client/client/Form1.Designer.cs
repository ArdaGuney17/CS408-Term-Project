
namespace client
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox_if = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox_sps = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox_events = new System.Windows.Forms.RichTextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_if = new System.Windows.Forms.TextBox();
            this.textBox_sps = new System.Windows.Forms.TextBox();
            this.button_if_send = new System.Windows.Forms.Button();
            this.button_sps_send = new System.Windows.Forms.Button();
            this.button_sps_subscribe = new System.Windows.Forms.Button();
            this.button_if_subscribe = new System.Windows.Forms.Button();
            this.button_connect = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox_if
            // 
            this.richTextBox_if.Location = new System.Drawing.Point(38, 164);
            this.richTextBox_if.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox_if.Name = "richTextBox_if";
            this.richTextBox_if.Size = new System.Drawing.Size(266, 312);
            this.richTextBox_if.TabIndex = 0;
            this.richTextBox_if.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(32, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "IF100";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 22);
            this.label2.TabIndex = 30;
            // 
            // richTextBox_sps
            // 
            this.richTextBox_sps.Location = new System.Drawing.Point(335, 164);
            this.richTextBox_sps.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox_sps.Name = "richTextBox_sps";
            this.richTextBox_sps.Size = new System.Drawing.Size(266, 312);
            this.richTextBox_sps.TabIndex = 3;
            this.richTextBox_sps.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(622, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 29);
            this.label3.TabIndex = 8;
            this.label3.Text = "Events";
            // 
            // richTextBox_events
            // 
            this.richTextBox_events.Location = new System.Drawing.Point(628, 164);
            this.richTextBox_events.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox_events.Name = "richTextBox_events";
            this.richTextBox_events.Size = new System.Drawing.Size(266, 354);
            this.richTextBox_events.TabIndex = 6;
            this.richTextBox_events.Text = "";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(119, 42);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(134, 26);
            this.textBox_port.TabIndex = 9;
            this.textBox_port.TextChanged += new System.EventHandler(this.textBox_port_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Port Num:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(269, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "IP:";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(299, 42);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(126, 26);
            this.textBox_ip.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 22);
            this.label6.TabIndex = 29;
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(536, 42);
            this.textBox_username.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(130, 26);
            this.textBox_username.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 22);
            this.label7.TabIndex = 28;
            // 
            // textBox_if
            // 
            this.textBox_if.Enabled = false;
            this.textBox_if.Location = new System.Drawing.Point(38, 492);
            this.textBox_if.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_if.Name = "textBox_if";
            this.textBox_if.Size = new System.Drawing.Size(194, 26);
            this.textBox_if.TabIndex = 19;
            this.textBox_if.TextChanged += new System.EventHandler(this.textBox_if_TextChanged);
            // 
            // textBox_sps
            // 
            this.textBox_sps.Enabled = false;
            this.textBox_sps.Location = new System.Drawing.Point(335, 492);
            this.textBox_sps.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_sps.Name = "textBox_sps";
            this.textBox_sps.Size = new System.Drawing.Size(194, 26);
            this.textBox_sps.TabIndex = 20;
            // 
            // button_if_send
            // 
            this.button_if_send.Enabled = false;
            this.button_if_send.Location = new System.Drawing.Point(238, 492);
            this.button_if_send.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_if_send.Name = "button_if_send";
            this.button_if_send.Size = new System.Drawing.Size(66, 27);
            this.button_if_send.TabIndex = 22;
            this.button_if_send.Text = "Send";
            this.button_if_send.UseVisualStyleBackColor = true;
            // 
            // button_sps_send
            // 
            this.button_sps_send.Enabled = false;
            this.button_sps_send.Location = new System.Drawing.Point(536, 491);
            this.button_sps_send.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_sps_send.Name = "button_sps_send";
            this.button_sps_send.Size = new System.Drawing.Size(66, 28);
            this.button_sps_send.TabIndex = 23;
            this.button_sps_send.Text = "Send";
            this.button_sps_send.UseVisualStyleBackColor = true;
            // 
            // button_sps_subscribe
            // 
            this.button_sps_subscribe.Enabled = false;
            this.button_sps_subscribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_sps_subscribe.Location = new System.Drawing.Point(444, 119);
            this.button_sps_subscribe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_sps_subscribe.Name = "button_sps_subscribe";
            this.button_sps_subscribe.Size = new System.Drawing.Size(158, 39);
            this.button_sps_subscribe.TabIndex = 25;
            this.button_sps_subscribe.Text = "Subscribe";
            this.button_sps_subscribe.UseVisualStyleBackColor = true;
            this.button_sps_subscribe.Click += new System.EventHandler(this.button_sps_subscribe_Click);
            // 
            // button_if_subscribe
            // 
            this.button_if_subscribe.Enabled = false;
            this.button_if_subscribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_if_subscribe.Location = new System.Drawing.Point(140, 119);
            this.button_if_subscribe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_if_subscribe.Name = "button_if_subscribe";
            this.button_if_subscribe.Size = new System.Drawing.Size(164, 39);
            this.button_if_subscribe.TabIndex = 26;
            this.button_if_subscribe.Text = "Subscribe";
            this.button_if_subscribe.UseVisualStyleBackColor = true;
            this.button_if_subscribe.Click += new System.EventHandler(this.button_if_subscribe_Click);
            // 
            // button_connect
            // 
            this.button_connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_connect.Location = new System.Drawing.Point(705, 36);
            this.button_connect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(146, 39);
            this.button_connect.TabIndex = 27;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(330, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 29);
            this.label8.TabIndex = 31;
            this.label8.Text = "SPS101";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(440, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 20);
            this.label9.TabIndex = 32;
            this.label9.Text = "User Name:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 552);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.button_if_subscribe);
            this.Controls.Add(this.button_sps_subscribe);
            this.Controls.Add(this.button_sps_send);
            this.Controls.Add(this.button_if_send);
            this.Controls.Add(this.textBox_sps);
            this.Controls.Add(this.textBox_if);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBox_events);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox_sps);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox_if);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Disucord";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_if;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox_sps;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox_events;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_if;
        private System.Windows.Forms.TextBox textBox_sps;
        private System.Windows.Forms.Button button_if_send;
        private System.Windows.Forms.Button button_sps_send;
        private System.Windows.Forms.Button button_sps_subscribe;
        private System.Windows.Forms.Button button_if_subscribe;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}

