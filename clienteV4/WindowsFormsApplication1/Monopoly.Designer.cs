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
            this.panel1 = new System.Windows.Forms.Panel();
            this.invitePanel = new System.Windows.Forms.Panel();
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
            this.playersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invitePlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameLabel = new System.Windows.Forms.Label();
            this.JoinPanel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.InvitacionesDataGrid = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Aceptarbutton = new System.Windows.Forms.Button();
            this.invitePanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InvitadosDataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conectadosGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.JoinPanel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InvitacionesDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(37, 84);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(516, 341);
            this.panel1.TabIndex = 0;
            // 
            // invitePanel
            // 
            this.invitePanel.Controls.Add(this.button1);
            this.invitePanel.Controls.Add(this.inviteButton);
            this.invitePanel.Controls.Add(this.groupBox2);
            this.invitePanel.Controls.Add(this.groupBox1);
            this.invitePanel.Location = new System.Drawing.Point(593, 46);
            this.invitePanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.invitePanel.Name = "invitePanel";
            this.invitePanel.Size = new System.Drawing.Size(649, 389);
            this.invitePanel.TabIndex = 9;
            this.invitePanel.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(545, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 11;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // inviteButton
            // 
            this.inviteButton.Location = new System.Drawing.Point(299, 215);
            this.inviteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.inviteButton.Name = "inviteButton";
            this.inviteButton.Size = new System.Drawing.Size(67, 26);
            this.inviteButton.TabIndex = 10;
            this.inviteButton.Text = "Invitar";
            this.inviteButton.UseVisualStyleBackColor = true;
            this.inviteButton.Click += new System.EventHandler(this.inviteButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.InvitadosDataGrid);
            this.groupBox2.Location = new System.Drawing.Point(401, 75);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(220, 297);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Jugadores Invitados";
            // 
            // InvitadosDataGrid
            // 
            this.InvitadosDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InvitadosDataGrid.Location = new System.Drawing.Point(8, 49);
            this.InvitadosDataGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.InvitadosDataGrid.Name = "InvitadosDataGrid";
            this.InvitadosDataGrid.RowHeadersWidth = 62;
            this.InvitadosDataGrid.Size = new System.Drawing.Size(204, 240);
            this.InvitadosDataGrid.TabIndex = 3;
            this.InvitadosDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InvitadosDataGrid_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.conectadosGridView);
            this.groupBox1.Location = new System.Drawing.Point(36, 71);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(220, 297);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connected Users";
            // 
            // conectadosGridView
            // 
            this.conectadosGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conectadosGridView.Location = new System.Drawing.Point(8, 49);
            this.conectadosGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.conectadosGridView.Name = "conectadosGridView";
            this.conectadosGridView.RowHeadersWidth = 62;
            this.conectadosGridView.Size = new System.Drawing.Size(204, 240);
            this.conectadosGridView.TabIndex = 3;
            this.conectadosGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.conectadosGridView_CellContentClick);
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1388, 28);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gamesToolStripMenuItem
            // 
            this.gamesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createGameToolStripMenuItem,
            this.joinGameToolStripMenuItem});
            this.gamesToolStripMenuItem.Name = "gamesToolStripMenuItem";
            this.gamesToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.gamesToolStripMenuItem.Text = "Game Settings";
            // 
            // createGameToolStripMenuItem
            // 
            this.createGameToolStripMenuItem.Name = "createGameToolStripMenuItem";
            this.createGameToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.createGameToolStripMenuItem.Text = "Create Game";
            this.createGameToolStripMenuItem.Click += new System.EventHandler(this.createGameToolStripMenuItem_Click);
            // 
            // joinGameToolStripMenuItem
            // 
            this.joinGameToolStripMenuItem.Name = "joinGameToolStripMenuItem";
            this.joinGameToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.joinGameToolStripMenuItem.Text = "Join Game";
            this.joinGameToolStripMenuItem.Click += new System.EventHandler(this.joinGameToolStripMenuItem_Click);
            // 
            // playersToolStripMenuItem
            // 
            this.playersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invitePlayersToolStripMenuItem});
            this.playersToolStripMenuItem.Name = "playersToolStripMenuItem";
            this.playersToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
            this.playersToolStripMenuItem.Text = "Players";
            // 
            // invitePlayersToolStripMenuItem
            // 
            this.invitePlayersToolStripMenuItem.Name = "invitePlayersToolStripMenuItem";
            this.invitePlayersToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.invitePlayersToolStripMenuItem.Text = "Invite Players";
            this.invitePlayersToolStripMenuItem.Click += new System.EventHandler(this.invitePlayersToolStripMenuItem_Click);
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statsToolStripMenuItem});
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.accountToolStripMenuItem.Text = "Account";
            // 
            // statsToolStripMenuItem
            // 
            this.statsToolStripMenuItem.Name = "statsToolStripMenuItem";
            this.statsToolStripMenuItem.Size = new System.Drawing.Size(124, 26);
            this.statsToolStripMenuItem.Text = "Stats";
            // 
            // gameLabel
            // 
            this.gameLabel.AutoSize = true;
            this.gameLabel.Location = new System.Drawing.Point(33, 46);
            this.gameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gameLabel.Name = "gameLabel";
            this.gameLabel.Size = new System.Drawing.Size(69, 16);
            this.gameLabel.TabIndex = 11;
            this.gameLabel.Text = "NO GAME";
            // 
            // JoinPanel
            // 
            this.JoinPanel.Controls.Add(this.Aceptarbutton);
            this.JoinPanel.Controls.Add(this.button2);
            this.JoinPanel.Controls.Add(this.RefreshButton);
            this.JoinPanel.Controls.Add(this.groupBox3);
            this.JoinPanel.Controls.Add(this.label3);
            this.JoinPanel.Controls.Add(this.label2);
            this.JoinPanel.Location = new System.Drawing.Point(36, 46);
            this.JoinPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.JoinPanel.Name = "JoinPanel";
            this.JoinPanel.Size = new System.Drawing.Size(440, 389);
            this.JoinPanel.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(336, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 12;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(43, 48);
            this.RefreshButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(243, 26);
            this.RefreshButton.TabIndex = 11;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.InvitacionesDataGrid);
            this.groupBox3.Location = new System.Drawing.Point(43, 75);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(243, 266);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Invitaciones";
            // 
            // InvitacionesDataGrid
            // 
            this.InvitacionesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InvitacionesDataGrid.Location = new System.Drawing.Point(19, 36);
            this.InvitacionesDataGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.InvitacionesDataGrid.Name = "InvitacionesDataGrid";
            this.InvitacionesDataGrid.RowHeadersWidth = 62;
            this.InvitacionesDataGrid.Size = new System.Drawing.Size(217, 217);
            this.InvitacionesDataGrid.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 16);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 1;
            // 
            // Aceptarbutton
            // 
            this.Aceptarbutton.Location = new System.Drawing.Point(43, 351);
            this.Aceptarbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Aceptarbutton.Name = "Aceptarbutton";
            this.Aceptarbutton.Size = new System.Drawing.Size(243, 26);
            this.Aceptarbutton.TabIndex = 13;
            this.Aceptarbutton.Text = "Aceptar";
            this.Aceptarbutton.UseVisualStyleBackColor = true;
            // 
            // Monopoly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1388, 565);
            this.Controls.Add(this.invitePanel);
            this.Controls.Add(this.JoinPanel);
            this.Controls.Add(this.gameLabel);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Monopoly";
            this.Text = "MONOPOLY";
            this.Load += new System.EventHandler(this.Monopoly_Load);
            this.invitePanel.ResumeLayout(false);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.Button Aceptarbutton;
    }
}