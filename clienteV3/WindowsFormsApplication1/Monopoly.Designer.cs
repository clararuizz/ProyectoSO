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
            this.userListBox = new System.Windows.Forms.GroupBox();
            this.conectadosGridView = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.Invitar = new System.Windows.Forms.Button();
            this.userListBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conectadosGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(580, 426);
            this.panel1.TabIndex = 0;
            // 
            // userListBox
            // 
            this.userListBox.Controls.Add(this.conectadosGridView);
            this.userListBox.Location = new System.Drawing.Point(917, 12);
            this.userListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userListBox.Name = "userListBox";
            this.userListBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userListBox.Size = new System.Drawing.Size(248, 371);
            this.userListBox.TabIndex = 5;
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
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Invitar
            // 
            this.Invitar.Location = new System.Drawing.Point(637, 177);
            this.Invitar.Name = "Invitar";
            this.Invitar.Size = new System.Drawing.Size(107, 55);
            this.Invitar.TabIndex = 7;
            this.Invitar.Text = "Invitar Jugadores";
            this.Invitar.UseVisualStyleBackColor = true;
            this.Invitar.Click += new System.EventHandler(this.Invitar_Click);
            // 
            // Monopoly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 598);
            this.Controls.Add(this.Invitar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.userListBox);
            this.Controls.Add(this.panel1);
            this.Name = "Monopoly";
            this.Text = "Form1";
            this.userListBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.conectadosGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox userListBox;
        private System.Windows.Forms.DataGridView conectadosGridView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Invitar;
    }
}