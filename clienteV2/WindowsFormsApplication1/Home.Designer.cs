namespace WindowsFormsApplication1
{
    partial class Home
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
            this.logInBox = new System.Windows.Forms.GroupBox();
            this.forgotLink = new System.Windows.Forms.LinkLabel();
            this.enterButton = new System.Windows.Forms.Button();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.registerBox = new System.Windows.Forms.GroupBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.conectadosGridView = new System.Windows.Forms.DataGridView();
            this.userListBox = new System.Windows.Forms.GroupBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.checkConnected = new System.Windows.Forms.CheckBox();
            this.logInBox.SuspendLayout();
            this.registerBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conectadosGridView)).BeginInit();
            this.userListBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // logInBox
            // 
            this.logInBox.Controls.Add(this.forgotLink);
            this.logInBox.Controls.Add(this.enterButton);
            this.logInBox.Controls.Add(this.passwordTextBox);
            this.logInBox.Controls.Add(this.passwordLabel);
            this.logInBox.Controls.Add(this.usernameLabel);
            this.logInBox.Controls.Add(this.usernameTextBox);
            this.logInBox.Location = new System.Drawing.Point(110, 74);
            this.logInBox.Name = "logInBox";
            this.logInBox.Size = new System.Drawing.Size(409, 180);
            this.logInBox.TabIndex = 0;
            this.logInBox.TabStop = false;
            this.logInBox.Text = "Log In";
            // 
            // forgotLink
            // 
            this.forgotLink.AutoSize = true;
            this.forgotLink.Location = new System.Drawing.Point(36, 132);
            this.forgotLink.Name = "forgotLink";
            this.forgotLink.Size = new System.Drawing.Size(92, 13);
            this.forgotLink.TabIndex = 9;
            this.forgotLink.TabStop = true;
            this.forgotLink.Text = "Forgot Password?";
            this.forgotLink.Click += new System.EventHandler(this.forgotLink_Click);
            // 
            // enterButton
            // 
            this.enterButton.Location = new System.Drawing.Point(220, 132);
            this.enterButton.Name = "enterButton";
            this.enterButton.Size = new System.Drawing.Size(102, 29);
            this.enterButton.TabIndex = 8;
            this.enterButton.Text = "Enter";
            this.enterButton.UseVisualStyleBackColor = true;
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(176, 90);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(191, 20);
            this.passwordTextBox.TabIndex = 7;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLabel.Location = new System.Drawing.Point(34, 84);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(106, 25);
            this.passwordLabel.TabIndex = 6;
            this.passwordLabel.Text = "Password";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(34, 40);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(110, 25);
            this.usernameLabel.TabIndex = 5;
            this.usernameLabel.Text = "Username";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(176, 40);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(191, 20);
            this.usernameTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(283, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "MONOPOLY";
            // 
            // registerBox
            // 
            this.registerBox.Controls.Add(this.registerButton);
            this.registerBox.Location = new System.Drawing.Point(110, 260);
            this.registerBox.Name = "registerBox";
            this.registerBox.Size = new System.Drawing.Size(409, 55);
            this.registerBox.TabIndex = 2;
            this.registerBox.TabStop = false;
            this.registerBox.Text = "Don\'t have an account?";
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(220, 14);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(102, 35);
            this.registerButton.TabIndex = 0;
            this.registerButton.Text = "Register Now";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // conectadosGridView
            // 
            this.conectadosGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conectadosGridView.Location = new System.Drawing.Point(6, 40);
            this.conectadosGridView.Name = "conectadosGridView";
            this.conectadosGridView.Size = new System.Drawing.Size(153, 195);
            this.conectadosGridView.TabIndex = 3;
            // 
            // userListBox
            // 
            this.userListBox.Controls.Add(this.conectadosGridView);
            this.userListBox.Location = new System.Drawing.Point(553, 74);
            this.userListBox.Name = "userListBox";
            this.userListBox.Size = new System.Drawing.Size(165, 241);
            this.userListBox.TabIndex = 4;
            this.userListBox.TabStop = false;
            this.userListBox.Text = "Connected Users";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(110, 24);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(164, 44);
            this.connectButton.TabIndex = 5;
            this.connectButton.Text = "CONNECT";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(370, 24);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(149, 44);
            this.disconnectButton.TabIndex = 6;
            this.disconnectButton.Text = "DISCONNECT";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // checkConnected
            // 
            this.checkConnected.AutoCheck = false;
            this.checkConnected.AutoSize = true;
            this.checkConnected.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkConnected.Location = new System.Drawing.Point(595, 39);
            this.checkConnected.Name = "checkConnected";
            this.checkConnected.Size = new System.Drawing.Size(78, 17);
            this.checkConnected.TabIndex = 7;
            this.checkConnected.Text = "Connected";
            this.checkConnected.UseVisualStyleBackColor = true;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkConnected);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.userListBox);
            this.Controls.Add(this.registerBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logInBox);
            this.Name = "Home";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.logInBox.ResumeLayout(false);
            this.logInBox.PerformLayout();
            this.registerBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.conectadosGridView)).EndInit();
            this.userListBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox logInBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox registerBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.LinkLabel forgotLink;
        private System.Windows.Forms.Button enterButton;
        private System.Windows.Forms.DataGridView conectadosGridView;
        private System.Windows.Forms.GroupBox userListBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.CheckBox checkConnected;
        private System.Windows.Forms.Button registerButton;
    }
}