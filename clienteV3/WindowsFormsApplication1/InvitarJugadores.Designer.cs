namespace WindowsFormsApplication1
{
    partial class InvitarJugadores
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
            this.userListBox = new System.Windows.Forms.GroupBox();
            this.conectadosGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.InvitadosDataGrid = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.userListBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conectadosGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InvitadosDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // userListBox
            // 
            this.userListBox.Controls.Add(this.conectadosGridView);
            this.userListBox.Location = new System.Drawing.Point(13, 65);
            this.userListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userListBox.Name = "userListBox";
            this.userListBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userListBox.Size = new System.Drawing.Size(248, 371);
            this.userListBox.TabIndex = 6;
            this.userListBox.TabStop = false;
            this.userListBox.Text = "Connected Users";
            // 
            // conectadosGridView
            // 
            this.conectadosGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conectadosGridView.Location = new System.Drawing.Point(9, 62);
            this.conectadosGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.conectadosGridView.Name = "conectadosGridView";
            this.conectadosGridView.RowHeadersWidth = 62;
            this.conectadosGridView.Size = new System.Drawing.Size(230, 300);
            this.conectadosGridView.TabIndex = 3;
            this.conectadosGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.conectadosGridView_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.InvitadosDataGrid);
            this.groupBox1.Location = new System.Drawing.Point(475, 65);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(248, 371);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Jugadores Invitados";
            // 
            // InvitadosDataGrid
            // 
            this.InvitadosDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InvitadosDataGrid.Location = new System.Drawing.Point(9, 62);
            this.InvitadosDataGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.InvitadosDataGrid.Name = "InvitadosDataGrid";
            this.InvitadosDataGrid.RowHeadersWidth = 62;
            this.InvitadosDataGrid.Size = new System.Drawing.Size(230, 300);
            this.InvitadosDataGrid.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(170, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(474, 26);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "Para Invitar jugadores haz clic en el Datagrid de Connected Users";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(336, 244);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 9;
            this.button1.Text = "Invitar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InvitarJugadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.userListBox);
            this.Name = "InvitarJugadores";
            this.Text = "Form1";
            this.userListBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.conectadosGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InvitadosDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox userListBox;
        private System.Windows.Forms.DataGridView conectadosGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView InvitadosDataGrid;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}