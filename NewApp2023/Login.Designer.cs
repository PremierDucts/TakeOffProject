namespace NewApp2023
{
    partial class Login
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            richTextBox1 = new RichTextBox();
            notifyIcon1 = new NotifyIcon(components);
            label1 = new Label();
            usernameLabel = new Label();
            passwordLabel = new Label();
            username = new TextBox();
            password = new TextBox();
            loginButton = new Button();
            label4 = new Label();
            fileSystemWatcher1 = new FileSystemWatcher();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            tb_ip = new TextBox();
            tb_macAddress = new TextBox();
            tb_uid = new TextBox();
            UID = new Label();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox1.Location = new Point(9, 64);
            richTextBox1.Margin = new Padding(4);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(962, 429);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // notifyIcon1
            // 
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipText = "Premier Ducts - Auto upload database is now running in background.";
            notifyIcon1.Text = "notifyIcon1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(4, 11);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(165, 37);
            label1.TabIndex = 1;
            label1.Text = "Instruction";
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            usernameLabel.Location = new Point(2, 646);
            usernameLabel.Margin = new Padding(4, 0, 4, 0);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(164, 37);
            usernameLabel.TabIndex = 2;
            usernameLabel.Text = "Username";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            passwordLabel.Location = new Point(2, 777);
            passwordLabel.Margin = new Padding(4, 0, 4, 0);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(158, 37);
            passwordLabel.TabIndex = 3;
            passwordLabel.Text = "Password";
            // 
            // username
            // 
            username.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            username.Location = new Point(237, 639);
            username.Margin = new Padding(4);
            username.Name = "username";
            username.Size = new Size(505, 44);
            username.TabIndex = 4;
            // 
            // password
            // 
            password.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            password.Location = new Point(237, 774);
            password.Margin = new Padding(4);
            password.Name = "password";
            password.PasswordChar = '*';
            password.Size = new Size(505, 44);
            password.TabIndex = 5;
            // 
            // loginButton
            // 
            loginButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            loginButton.Location = new Point(303, 1278);
            loginButton.Margin = new Padding(4);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(162, 75);
            loginButton.TabIndex = 6;
            loginButton.Text = "Login";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(2, 527);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(592, 37);
            label4.TabIndex = 7;
            label4.Text = "Please login before openning CAMDucts";
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(78, 32);
            label2.TabIndex = 8;
            label2.Text = "label2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(10, 902);
            label3.Name = "label3";
            label3.Size = new Size(47, 45);
            label3.TabIndex = 9;
            label3.Text = "IP";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(4, 1029);
            label5.Name = "label5";
            label5.Size = new Size(199, 45);
            label5.TabIndex = 10;
            label5.Text = "Mac address";
            // 
            // tb_ip
            // 
            tb_ip.Location = new Point(237, 911);
            tb_ip.Name = "tb_ip";
            tb_ip.Size = new Size(505, 39);
            tb_ip.TabIndex = 11;
            // 
            // tb_macAddress
            // 
            tb_macAddress.Location = new Point(232, 1034);
            tb_macAddress.Name = "tb_macAddress";
            tb_macAddress.Size = new Size(510, 39);
            tb_macAddress.TabIndex = 12;
            // 
            // tb_uid
            // 
            tb_uid.Location = new Point(232, 1143);
            tb_uid.Name = "tb_uid";
            tb_uid.Size = new Size(510, 39);
            tb_uid.TabIndex = 14;
            // 
            // UID
            // 
            UID.AutoSize = true;
            UID.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            UID.Location = new Point(4, 1138);
            UID.Name = "UID";
            UID.Size = new Size(73, 45);
            UID.TabIndex = 13;
            UID.Text = "UID";
            // 
            // button2
            // 
            button2.Location = new Point(594, 1278);
            button2.Name = "button2";
            button2.Size = new Size(159, 75);
            button2.TabIndex = 15;
            button2.Text = "Logout";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1101, 1438);
            Controls.Add(button2);
            Controls.Add(tb_uid);
            Controls.Add(UID);
            Controls.Add(tb_macAddress);
            Controls.Add(tb_ip);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(loginButton);
            Controls.Add(password);
            Controls.Add(username);
            Controls.Add(passwordLabel);
            Controls.Add(usernameLabel);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Margin = new Padding(4);
            Name = "Login";
            Text = "Premier Ducts - Build on excellence";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label label4;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button button1;
        private TextBox tb_macAddress;
        private TextBox tb_ip;
        private Label label5;
        private Label label3;
        private Label label2;
        private TextBox tb_uid;
        private Label UID;
        private Button button2;
    }
}