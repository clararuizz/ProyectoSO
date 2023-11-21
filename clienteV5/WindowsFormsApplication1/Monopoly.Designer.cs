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
            this.Denybutton = new System.Windows.Forms.Button();
            this.AceptarButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.InvitacionesDataGrid = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            // invitePanel
            // 
            this.invitePanel.Controls.Add(this.button1);
            this.invitePanel.Controls.Add(this.inviteButton);
            this.invitePanel.Controls.Add(this.groupBox2);
            this.invitePanel.Controls.Add(this.groupBox1);
            this.invitePanel.Location = new System.Drawing.Point(402, 38);
            this.invitePanel.Name = "invitePanel";
            this.invitePanel.Size = new System.Drawing.Size(487, 316);
            this.invitePanel.TabIndex = 9;
            this.invitePanel.Visible = false;
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
            this.inviteButton.Text = "Invitar";
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
            this.groupBox2.Text = "Jugadores Invitados";
            // 
            // InvitadosDataGrid
            // 
            this.InvitadosDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InvitadosDataGrid.Location = new System.Drawing.Point(6, 40);
            this.InvitadosDataGrid.Name = "InvitadosDataGrid";
            this.InvitadosDataGrid.RowHeadersWidth = 62;
            this.InvitadosDataGrid.Size = new System.Drawing.Size(153, 195);
            this.InvitadosDataGrid.TabIndex = 3;
            this.InvitadosDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InvitadosDataGrid_CellContentClick);
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(913, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gamesToolStripMenuItem
            // 
            this.gamesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createGameToolStripMenuItem,
            this.joinGameToolStripMenuItem});
            this.gamesToolStripMenuItem.Name = "gamesToolStripMenuItem";
            this.gamesToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.gamesToolStripMenuItem.Text = "Game Settings";
            // 
            // createGameToolStripMenuItem
            // 
            this.createGameToolStripMenuItem.Name = "createGameToolStripMenuItem";
            this.createGameToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.createGameToolStripMenuItem.Text = "Create Game";
            this.createGameToolStripMenuItem.Click += new System.EventHandler(this.createGameToolStripMenuItem_Click);
            // 
            // joinGameToolStripMenuItem
            // 
            this.joinGameToolStripMenuItem.Name = "joinGameToolStripMenuItem";
            this.joinGameToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.joinGameToolStripMenuItem.Text = "Join Game";
            this.joinGameToolStripMenuItem.Click += new System.EventHandler(this.joinGameToolStripMenuItem_Click);
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
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(64, 22);
            this.accountToolStripMenuItem.Text = "Account";
            // 
            // statsToolStripMenuItem
            // 
            this.statsToolStripMenuItem.Name = "statsToolStripMenuItem";
            this.statsToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.statsToolStripMenuItem.Text = "Stats";
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
            this.JoinPanel.Location = new System.Drawing.Point(27, 38);
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
            this.groupBox3.Text = "Invitaciones";
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
            this.InvitacionesDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InvitacionesDataGrid_CellContentClick);
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
            // Monopoly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 459);
            this.Controls.Add(this.invitePanel);
            this.Controls.Add(this.JoinPanel);
            this.Controls.Add(this.gameLabel);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
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
    }
}