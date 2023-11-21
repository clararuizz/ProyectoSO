namespace WindowsFormsApplication1
{
    partial class Partida
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Partida));
            this.chatTextBox = new System.Windows.Forms.TextBox();
            this.writeTextBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.player1Label = new System.Windows.Forms.Label();
            this.player3Label = new System.Windows.Forms.Label();
            this.player2Label = new System.Windows.Forms.Label();
            this.player4Label = new System.Windows.Forms.Label();
            this.PartidaDGV = new System.Windows.Forms.DataGridView();
            this.Tirar = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.dadoPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PartidaDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // chatTextBox
            // 
            this.chatTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.chatTextBox.Location = new System.Drawing.Point(549, 9);
            this.chatTextBox.Multiline = true;
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.ReadOnly = true;
            this.chatTextBox.Size = new System.Drawing.Size(254, 145);
            this.chatTextBox.TabIndex = 0;
            // 
            // writeTextBox
            // 
            this.writeTextBox.Location = new System.Drawing.Point(549, 165);
            this.writeTextBox.Name = "writeTextBox";
            this.writeTextBox.Size = new System.Drawing.Size(177, 20);
            this.writeTextBox.TabIndex = 1;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(732, 165);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(70, 23);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(89, 61);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(420, 303);
            this.panel1.TabIndex = 3;
            // 
            // player1Label
            // 
            this.player1Label.AutoSize = true;
            this.player1Label.Location = new System.Drawing.Point(86, 35);
            this.player1Label.Name = "player1Label";
            this.player1Label.Size = new System.Drawing.Size(35, 13);
            this.player1Label.TabIndex = 4;
            this.player1Label.Text = "label1";
            this.player1Label.Visible = false;
            // 
            // player3Label
            // 
            this.player3Label.AutoSize = true;
            this.player3Label.Location = new System.Drawing.Point(86, 376);
            this.player3Label.Name = "player3Label";
            this.player3Label.Size = new System.Drawing.Size(35, 13);
            this.player3Label.TabIndex = 5;
            this.player3Label.Text = "label1";
            this.player3Label.Visible = false;
            // 
            // player2Label
            // 
            this.player2Label.AutoSize = true;
            this.player2Label.Location = new System.Drawing.Point(474, 35);
            this.player2Label.Name = "player2Label";
            this.player2Label.Size = new System.Drawing.Size(35, 13);
            this.player2Label.TabIndex = 6;
            this.player2Label.Text = "label1";
            this.player2Label.Visible = false;
            // 
            // player4Label
            // 
            this.player4Label.AutoSize = true;
            this.player4Label.Location = new System.Drawing.Point(474, 376);
            this.player4Label.Name = "player4Label";
            this.player4Label.Size = new System.Drawing.Size(35, 13);
            this.player4Label.TabIndex = 7;
            this.player4Label.Text = "label1";
            this.player4Label.Visible = false;
            // 
            // PartidaDGV
            // 
            this.PartidaDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PartidaDGV.Location = new System.Drawing.Point(558, 220);
            this.PartidaDGV.Margin = new System.Windows.Forms.Padding(2);
            this.PartidaDGV.Name = "PartidaDGV";
            this.PartidaDGV.RowHeadersWidth = 62;
            this.PartidaDGV.RowTemplate.Height = 28;
            this.PartidaDGV.Size = new System.Drawing.Size(287, 144);
            this.PartidaDGV.TabIndex = 8;
            // 
            // Tirar
            // 
            this.Tirar.Location = new System.Drawing.Point(216, 391);
            this.Tirar.Margin = new System.Windows.Forms.Padding(2);
            this.Tirar.Name = "Tirar";
            this.Tirar.Size = new System.Drawing.Size(59, 48);
            this.Tirar.TabIndex = 10;
            this.Tirar.Text = "Tirar";
            this.Tirar.UseVisualStyleBackColor = true;
            this.Tirar.Click += new System.EventHandler(this.tirarButton_Click);
            // 
            // timer
            // 
            this.timer.Interval = 200;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // dadoPicture
            // 
            this.dadoPicture.Location = new System.Drawing.Point(289, 376);
            this.dadoPicture.Name = "dadoPicture";
            this.dadoPicture.Size = new System.Drawing.Size(80, 80);
            this.dadoPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.dadoPicture.TabIndex = 11;
            this.dadoPicture.TabStop = false;
            // 
            // Partida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 487);
            this.Controls.Add(this.dadoPicture);
            this.Controls.Add(this.Tirar);
            this.Controls.Add(this.PartidaDGV);
            this.Controls.Add(this.player4Label);
            this.Controls.Add(this.player2Label);
            this.Controls.Add(this.player3Label);
            this.Controls.Add(this.player1Label);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.writeTextBox);
            this.Controls.Add(this.chatTextBox);
            this.Name = "Partida";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Partida_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PartidaDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox chatTextBox;
        private System.Windows.Forms.TextBox writeTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label player1Label;
        private System.Windows.Forms.Label player3Label;
        private System.Windows.Forms.Label player2Label;
        private System.Windows.Forms.Label player4Label;
        private System.Windows.Forms.DataGridView PartidaDGV;
        private System.Windows.Forms.Button Tirar;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox dadoPicture;
    }
}