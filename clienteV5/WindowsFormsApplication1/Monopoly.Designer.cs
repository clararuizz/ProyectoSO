namespace WindowsFormsApplication1
{
    partial class Monopoly
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monopoly));
            this.invitePanel = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.inviteButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.InvitadosDataGrid = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.conectadosGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joinGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recoverGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invitePlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameLabel = new System.Windows.Forms.Label();
            this.JoinPanel = new System.Windows.Forms.Panel();
            this.Denybutton = new System.Windows.Forms.Button();
            this.AceptarButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.InvitacionesDataGrid = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AboutUsPanel = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.showPassword = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.invitePanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InvitadosDataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conectadosGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.JoinPanel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InvitacionesDataGrid)).BeginInit();
            this.AboutUsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // invitePanel
            // 
            this.invitePanel.Controls.Add(this.label13);
            this.invitePanel.Controls.Add(this.button1);
            this.invitePanel.Controls.Add(this.inviteButton);
            this.invitePanel.Controls.Add(this.groupBox2);
            this.invitePanel.Controls.Add(this.groupBox1);
            this.invitePanel.Location = new System.Drawing.Point(359, 38);
            this.invitePanel.Name = "invitePanel";
            this.invitePanel.Size = new System.Drawing.Size(487, 316);
            this.invitePanel.TabIndex = 9;
            this.invitePanel.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(154, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(213, 39);
            this.label13.TabIndex = 12;
            this.label13.Text = "Click on a connected user to invite him/her.\r\nIf you want to remove him/her from " +
    "your \r\ninvitations, click on Invited users.";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(409, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // inviteButton
            // 
            this.inviteButton.Location = new System.Drawing.Point(224, 175);
            this.inviteButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.inviteButton.Name = "inviteButton";
            this.inviteButton.Size = new System.Drawing.Size(50, 21);
            this.inviteButton.TabIndex = 10;
            this.inviteButton.Text = "Invite";
            this.inviteButton.UseVisualStyleBackColor = true;
            this.inviteButton.Click += new System.EventHandler(this.inviteButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.InvitadosDataGrid);
            this.groupBox2.Location = new System.Drawing.Point(301, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(165, 241);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Invited users";
            // 
            // InvitadosDataGrid
            // 
            this.InvitadosDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InvitadosDataGrid.Location = new System.Drawing.Point(6, 40);
            this.InvitadosDataGrid.Name = "InvitadosDataGrid";
            this.InvitadosDataGrid.RowHeadersWidth = 62;
            this.InvitadosDataGrid.Size = new System.Drawing.Size(153, 195);
            this.InvitadosDataGrid.TabIndex = 3;
            this.InvitadosDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InvitadosDataGrid_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.conectadosGridView);
            this.groupBox1.Location = new System.Drawing.Point(27, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(165, 241);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connected Users";
            // 
            // conectadosGridView
            // 
            this.conectadosGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conectadosGridView.Location = new System.Drawing.Point(6, 40);
            this.conectadosGridView.Name = "conectadosGridView";
            this.conectadosGridView.RowHeadersWidth = 62;
            this.conectadosGridView.Size = new System.Drawing.Size(153, 195);
            this.conectadosGridView.TabIndex = 3;
            this.conectadosGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.conectadosGridView_CellContentClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gamesToolStripMenuItem,
            this.playersToolStripMenuItem,
            this.accountToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(931, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gamesToolStripMenuItem
            // 
            this.gamesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createGameToolStripMenuItem,
            this.joinGameToolStripMenuItem,
            this.recoverGameToolStripMenuItem});
            this.gamesToolStripMenuItem.Name = "gamesToolStripMenuItem";
            this.gamesToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.gamesToolStripMenuItem.Text = "Game Settings";
            // 
            // createGameToolStripMenuItem
            // 
            this.createGameToolStripMenuItem.Name = "createGameToolStripMenuItem";
            this.createGameToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.createGameToolStripMenuItem.Text = "Create Game";
            this.createGameToolStripMenuItem.Click += new System.EventHandler(this.createGameToolStripMenuItem_Click);
            // 
            // joinGameToolStripMenuItem
            // 
            this.joinGameToolStripMenuItem.Name = "joinGameToolStripMenuItem";
            this.joinGameToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.joinGameToolStripMenuItem.Text = "Join Game";
            this.joinGameToolStripMenuItem.Click += new System.EventHandler(this.joinGameToolStripMenuItem_Click);
            // 
            // recoverGameToolStripMenuItem
            // 
            this.recoverGameToolStripMenuItem.Name = "recoverGameToolStripMenuItem";
            this.recoverGameToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.recoverGameToolStripMenuItem.Text = "Recover Game";
            this.recoverGameToolStripMenuItem.Click += new System.EventHandler(this.recoverGameToolStripMenuItem_Click);
            // 
            // playersToolStripMenuItem
            // 
            this.playersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invitePlayersToolStripMenuItem});
            this.playersToolStripMenuItem.Name = "playersToolStripMenuItem";
            this.playersToolStripMenuItem.Size = new System.Drawing.Size(56, 22);
            this.playersToolStripMenuItem.Text = "Players";
            // 
            // invitePlayersToolStripMenuItem
            // 
            this.invitePlayersToolStripMenuItem.Name = "invitePlayersToolStripMenuItem";
            this.invitePlayersToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.invitePlayersToolStripMenuItem.Text = "Invite Players";
            this.invitePlayersToolStripMenuItem.Click += new System.EventHandler(this.invitePlayersToolStripMenuItem_Click);
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statsToolStripMenuItem});
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
            this.accountToolStripMenuItem.Text = "About Us";
            // 
            // statsToolStripMenuItem
            // 
            this.statsToolStripMenuItem.Name = "statsToolStripMenuItem";
            this.statsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.statsToolStripMenuItem.Text = "About Us";
            this.statsToolStripMenuItem.Click += new System.EventHandler(this.statsToolStripMenuItem_Click);
            // 
            // gameLabel
            // 
            this.gameLabel.AutoSize = true;
            this.gameLabel.Location = new System.Drawing.Point(25, 38);
            this.gameLabel.Name = "gameLabel";
            this.gameLabel.Size = new System.Drawing.Size(57, 13);
            this.gameLabel.TabIndex = 11;
            this.gameLabel.Text = "NO GAME";
            // 
            // JoinPanel
            // 
            this.JoinPanel.Controls.Add(this.Denybutton);
            this.JoinPanel.Controls.Add(this.AceptarButton);
            this.JoinPanel.Controls.Add(this.button2);
            this.JoinPanel.Controls.Add(this.RefreshButton);
            this.JoinPanel.Controls.Add(this.groupBox3);
            this.JoinPanel.Controls.Add(this.label3);
            this.JoinPanel.Controls.Add(this.label2);
            this.JoinPanel.Location = new System.Drawing.Point(11, 38);
            this.JoinPanel.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.JoinPanel.Name = "JoinPanel";
            this.JoinPanel.Size = new System.Drawing.Size(330, 316);
            this.JoinPanel.TabIndex = 12;
            // 
            // Denybutton
            // 
            this.Denybutton.Location = new System.Drawing.Point(132, 285);
            this.Denybutton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Denybutton.Name = "Denybutton";
            this.Denybutton.Size = new System.Drawing.Size(77, 21);
            this.Denybutton.TabIndex = 14;
            this.Denybutton.Text = "Deny";
            this.Denybutton.UseVisualStyleBackColor = true;
            this.Denybutton.Click += new System.EventHandler(this.Denybutton_Click);
            // 
            // AceptarButton
            // 
            this.AceptarButton.Location = new System.Drawing.Point(32, 285);
            this.AceptarButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.AceptarButton.Name = "AceptarButton";
            this.AceptarButton.Size = new System.Drawing.Size(77, 21);
            this.AceptarButton.TabIndex = 13;
            this.AceptarButton.Text = "Accept";
            this.AceptarButton.UseVisualStyleBackColor = true;
            this.AceptarButton.Click += new System.EventHandler(this.AceptarButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(252, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(32, 39);
            this.RefreshButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(182, 21);
            this.RefreshButton.TabIndex = 11;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.InvitacionesDataGrid);
            this.groupBox3.Location = new System.Drawing.Point(32, 61);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.groupBox3.Size = new System.Drawing.Size(237, 216);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Invitations";
            // 
            // InvitacionesDataGrid
            // 
            this.InvitacionesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InvitacionesDataGrid.Location = new System.Drawing.Point(14, 29);
            this.InvitacionesDataGrid.Name = "InvitacionesDataGrid";
            this.InvitacionesDataGrid.RowHeadersWidth = 62;
            this.InvitacionesDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.InvitacionesDataGrid.Size = new System.Drawing.Size(219, 176);
            this.InvitacionesDataGrid.TabIndex = 4;
            this.InvitacionesDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InvitacionesDataGrid_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 1;
            // 
            // AboutUsPanel
            // 
            this.AboutUsPanel.Controls.Add(this.pictureBox3);
            this.AboutUsPanel.Controls.Add(this.pictureBox2);
            this.AboutUsPanel.Controls.Add(this.pictureBox1);
            this.AboutUsPanel.Controls.Add(this.showPassword);
            this.AboutUsPanel.Controls.Add(this.label11);
            this.AboutUsPanel.Controls.Add(this.label10);
            this.AboutUsPanel.Controls.Add(this.label9);
            this.AboutUsPanel.Controls.Add(this.label8);
            this.AboutUsPanel.Controls.Add(this.label7);
            this.AboutUsPanel.Controls.Add(this.label6);
            this.AboutUsPanel.Controls.Add(this.label5);
            this.AboutUsPanel.Controls.Add(this.label4);
            this.AboutUsPanel.Controls.Add(this.label1);
            this.AboutUsPanel.Controls.Add(this.button5);
            this.AboutUsPanel.Location = new System.Drawing.Point(168, 27);
            this.AboutUsPanel.Name = "AboutUsPanel";
            this.AboutUsPanel.Size = new System.Drawing.Size(594, 348);
            this.AboutUsPanel.TabIndex = 13;
            this.AboutUsPanel.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(89, 255);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(70, 76);
            this.pictureBox3.TabIndex = 24;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(422, 255);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(62, 76);
            this.pictureBox2.TabIndex = 23;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(422, 110);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 71);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // showPassword
            // 
            this.showPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("showPassword.BackgroundImage")));
            this.showPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.showPassword.Location = new System.Drawing.Point(89, 110);
            this.showPassword.Margin = new System.Windows.Forms.Padding(2);
            this.showPassword.Name = "showPassword";
            this.showPassword.Size = new System.Drawing.Size(70, 71);
            this.showPassword.TabIndex = 21;
            this.showPassword.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(59, 190);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "LLUC FERNÁNDEZ FALCÓ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(396, 190);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "CLARA RUIZ FLORIT";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(314, 208);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(273, 39);
            this.label9.TabIndex = 18;
            this.label9.Text = "Aerospace Systems Engineering and Telecommunication \r\nSystems Engineering student" +
    " at UPC.\r\nclara.ruiz.florit@estudiantat.upc.edu\r\n";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(314, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(273, 39);
            this.label8.TabIndex = 17;
            this.label8.Text = "Aerospace Systems Engineering and Telecommunication \r\nSystems Engineering student" +
    " at UPC.\r\nmarta.de.gea@estudiantat.upc.edu\r\n";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(273, 39);
            this.label7.TabIndex = 16;
            this.label7.Text = "Aerospace Systems Engineering and Telecommunication \r\nSystems Engineering student" +
    " at UPC.\r\njoan.lluc.fernandez@estudiantat.upc.edu\r\n";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(273, 39);
            this.label6.TabIndex = 15;
            this.label6.Text = "Aerospace Systems Engineering and Telecommunication \r\nSystems Engineering student" +
    " at UPC.\r\nmarcel.guim@estudiantat.upc.edu\r\n";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(388, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "MARTA DE GEA TORRES";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "MARCEL GUIM MARZO";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "ABOUT US";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(409, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "Close";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(191, 104);
            this.label12.TabIndex = 15;
            this.label12.Text = resources.GetString("label12.Text");
            // 
            // Monopoly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(931, 427);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.AboutUsPanel);
            this.Controls.Add(this.invitePanel);
            this.Controls.Add(this.JoinPanel);
            this.Controls.Add(this.gameLabel);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "Monopoly";
            this.Text = "MONOPOLY";
            this.Load += new System.EventHandler(this.Monopoly_Load);
            this.invitePanel.ResumeLayout(false);
            this.invitePanel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InvitadosDataGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.conectadosGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.JoinPanel.ResumeLayout(false);
            this.JoinPanel.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InvitacionesDataGrid)).EndInit();
            this.AboutUsPanel.ResumeLayout(false);
            this.AboutUsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel invitePanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView InvitadosDataGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView conectadosGridView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joinGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invitePlayersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statsToolStripMenuItem;
        private System.Windows.Forms.Button inviteButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label gameLabel;
        private System.Windows.Forms.Panel JoinPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView InvitacionesDataGrid;
        private System.Windows.Forms.Button AceptarButton;
        private System.Windows.Forms.Button Denybutton;
        private System.Windows.Forms.Panel AboutUsPanel;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox showPassword;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ToolStripMenuItem recoverGameToolStripMenuItem;
    }
}